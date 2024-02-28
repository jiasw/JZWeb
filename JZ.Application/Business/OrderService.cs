using AutoMapper;
using JZ.Application.Contract.Dtos.Business;
using JZ.Application.Contract.Dtos;
using JZ.Application.Contract.Infrastructure;
using JZ.Application.Infrastructure;
using JZ.Domain.Repository;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JZ.Domain.Entitys;

namespace JZ.Application.Business
{
    public class OrderService
    {
        private readonly ILogger<OrderService> logger;
        private readonly Contract.Infrastructure.ICacheService cacheService;
        private readonly IMapper mapper;
        private readonly OrderRepository orderRepository;
        private readonly PayInfoRepository payInfoRepository;

        public OrderService(ILogger<OrderService> logger, Contract.Infrastructure.ICacheService cacheService, IMapper mapper
            , OrderRepository orderRepository,PayInfoRepository payInfoRepository)
        {
            this.logger = logger;
            this.cacheService = cacheService;
            this.mapper = mapper;
            this.orderRepository = orderRepository;
            this.payInfoRepository = payInfoRepository;
        }

        public async Task<PageDto<OrderDto>> GetListByPage(int page, int pageSize, string keyword)
        {
            RefAsync<int> totalpages = new RefAsync<int>();
            var list = await orderRepository.AsQueryable()
                .LeftJoin<Domain.Entitys.JZ_YW_USERINFO>((o, user) => o.UserID == user.ID)
                .LeftJoin<Domain.Entitys.JZ_YW_FWXX>((o, user,fw) => o.FWID == fw.ID)
                .WhereIF(
                !string.IsNullOrWhiteSpace(keyword), (o, user, fw) => o.Title.Contains(keyword)
                ).OrderBy((o, user, fw) => o.AddTime)
                .Select((o, user, fw) => new OrderDto() { ID = o.ID.SelectAll(),  UserName = user.NickName, FWMC = fw.Title })
                .ToPageListAsync(page, pageSize, totalpages);
            return new PageDto<OrderDto>(totalpages, list);
        }

        public async Task<OrderDto> GetModel(long id)
        {
            var model = await orderRepository.AsQueryable()
                .LeftJoin<Domain.Entitys.JZ_YW_USERINFO>((o, user) => o.UserID == user.ID)
                .LeftJoin<Domain.Entitys.JZ_YW_FWXX>((o, user, fw) => o.FWID == fw.ID)
                .Where((o, user, fw) => o.ID == id).OrderBy((o, user, fw) => o.AddTime)
                .Select((o, user, fw) => new OrderDto() { ID = o.ID.SelectAll(), UserName = user.NickName,FWMC=fw.Title })
                .FirstAsync();
            return model;
        }

        public async Task<bool> Delete(IEnumerable<long> ids)
        {
            return await orderRepository.UpdateSetColumnsTrueAsync(
                b => new Domain.Entitys.JZ_YW_ORDER() { Del = 1, DelTime = Utils.GetDateStr() },
                w => ids.Contains(w.ID)
                );
        }

        public async Task<bool> Save(OrderDto addrDto)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_ORDER>(addrDto);
            if (addrDto.ID <= 0)
            {
                SaveModel.AddTime = Utils.GetDateStr();
                return await orderRepository.InsertAsync(SaveModel);
            }
            else
            {
                return await orderRepository.UpdateSetColumnsTrueAsync(
                   it => new Domain.Entitys.JZ_YW_ORDER()
                   {
                       EditTime = Utils.GetDateStr(),
                       FWID = addrDto.FWID,
                       Address = addrDto.Address,
                       Code = addrDto.Code,
                       FWSL = addrDto.FWSL,
                       OrderPhone = addrDto.OrderPhone,
                       OrderState = addrDto.OrderState,
                       OrderTime = addrDto.OrderTime,
                       OrderUserName = addrDto.OrderUserName,
                       Remarks = addrDto.Remarks,
                       Title = addrDto.Title,

                   }, wh => wh.ID == addrDto.ID);
            }
        }

        /// <summary>
        /// 微信端下单
        /// </summary>
        /// <param name="addrDto"></param>
        /// <returns></returns>
        public async Task<bool> WXOrder(OrderDto addrDto)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_ORDER>(addrDto);
            SaveModel.AddTime=Utils.GetDateStr();
            return await orderRepository.InsertAsync(SaveModel);
        }
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderDto>> GetOrderListByOpenID(string openid)
        {
            var list= await orderRepository.AsQueryable()
                .LeftJoin<Domain.Entitys.JZ_YW_FWXX>((o, fw) => o.FWID == fw.ID)
                .LeftJoin<Domain.Entitys.JZ_YW_ASSESS>((o, fw, a) => o.ID == a.OrderID && a.Del == 0)
                .Where((o, fw, a) => o.WXID==openid)
                .OrderBy((o, fw, a)=>o.AddTime)
                .Select((o,  fw, a) => new OrderDto() { ID = o.ID.SelectAll(), FWMC = fw.Title,IsPJ=a.ID>0 })
                .ToListAsync();
            return list;

        }
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOrderState(long orderid,string state)
        {
            //获取订单信息
            var orderModel= await orderRepository.GetByIdAsync(orderid);

            //在支付记录表中插入一条数据
            long payresult= await payInfoRepository.InsertReturnSnowflakeIdAsync(new Domain.Entitys.JZ_YW_PAYINFO() { 
            AddTime= Utils.GetDateStr(),
            Del=0,
            UserID=orderModel.UserID,
            CallState="0",
            OrderID= orderModel.ID,
            PayOrderNO=orderModel.WXPayOrderID,
            PayRemark="",
            PayState= "1",
            PayTitle=$"订单id：{orderid}-订单标题{orderModel.Title}",
            PayType="1",
            });
            return await orderRepository.AsUpdateable().SetColumns(n => n.OrderState == state)
                .Where(w => w.ID == orderid).ExecuteCommandAsync()>0;
        }

        /// <summary>
        /// 微信支付回调时，更新订单状态。如果订单状态为已下单，更新为已支付
        /// </summary>
        /// <param name="orderid">内部订单号</param>
        /// <param name="payorderid">微信支付订单号</param>
        /// <returns></returns>
        public async Task WXPayNotice(string orderid,string payorderid)
        {
            long longorderid = long.Parse(orderid);
            await orderRepository.AsUpdateable().SetColumns(n=>n.OrderState=="1")
                .SetColumns(n=>n.WXPayOrderID==payorderid)
                .Where(n=>n.ID==longorderid&&n.OrderState=="0").ExecuteCommandAsync();

            //更新支付记录表
            await payInfoRepository.AsUpdateable().SetColumns(n => new JZ_YW_PAYINFO() 
            { CallState = "1",PayOrderNO=payorderid,EditTime=Utils.GetDateStr() })
                .Where(n => n.OrderID == longorderid).ExecuteCommandAsync();               


        }

        public async Task<bool> MangeUpdateOrState(long orderid, string state)
        {
            return await orderRepository.AsUpdateable().SetColumns(n => n.OrderState == state).Where( n => n.ID == orderid)
                .ExecuteCommandAsync()>0;
        }

        /// <summary>
        /// 后台管理界面获取订单总状态
        /// </summary>
        /// <returns></returns>
        public async Task<HomoOrderDto> GetHomeOrderState()
        {
            HomoOrderDto dto = new HomoOrderDto();
            string[] wwcorderstate = { "0", "1" };
            dto.Total = await orderRepository.CountAsync(n=>true);
            dto.NewOrder= await orderRepository.CountAsync(n=> wwcorderstate.Contains( n.OrderState));
            dto.Amount = await orderRepository.AsQueryable().Where(n => n.OrderState == "3").SumAsync(n => n.Amount) ?? 0;
            return dto;
        }

    }
}
