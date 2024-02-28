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
    /// <summary>
    /// 评价信息管理
    /// </summary>
    public class AssessService
    {
        private readonly ILogger<AssessService> logger;
        private readonly Contract.Infrastructure.ICacheService cacheService;
        private readonly IMapper mapper;
        private readonly AssessReository assessReository;
        private readonly UserInfoRepository userInfoRepository;

        public AssessService(ILogger<AssessService> logger, Contract.Infrastructure.ICacheService cacheService, IMapper mapper
            , AssessReository assessReository,UserInfoRepository userInfoRepository)
        {
            this.logger = logger;
            this.cacheService = cacheService;
            this.mapper = mapper;
            this.assessReository = assessReository;
            this.userInfoRepository = userInfoRepository;
        }


        public async Task<PageDto<AssessDto>> GetListByPage(int page, int pageSize, string keyword,string isgood)
        {
            RefAsync<int> totalpages = new RefAsync<int>();
            var list = await assessReository.AsQueryable()
                .LeftJoin<Domain.Entitys.JZ_YW_USERINFO>((o, user) => o.UserID == user.ID)
                .Where((o, user)=>o.Del==0)
                .WhereIF(!string.IsNullOrWhiteSpace(keyword), o => o.Text.Contains(keyword))
                .WhereIF(!string.IsNullOrWhiteSpace(isgood), o => o.IsGood==isgood)
                .OrderBy((o, user) => o.AddTime)
                .Select((o, user)=>new AssessDto() { ID= o.ID.SelectAll(),UserImg=user.Photo,UserName=user.NickName,})
                .ToPageListAsync(page, pageSize, totalpages);
            
            return new PageDto<AssessDto>(totalpages, list);
        }

        public async Task<AssessDto> GetModel(long id)
        {
            var model = await assessReository.AsQueryable()
                .LeftJoin<Domain.Entitys.JZ_YW_USERINFO>((o, usr) => o.UserID == usr.ID)
                .Where((o, usr) => o.ID == id)
                .OrderBy((o, usr) => o.AddTime)
                .Select((o, usr) => new AssessDto() { ID = o.ID.SelectAll(), UserImg = usr.Photo, UserName = usr.NickName, })
                .FirstAsync();

            return model;
        }

        public async Task<bool> Delete(IEnumerable<long> ids)
        {
            return await assessReository.UpdateSetColumnsTrueAsync(
                b => new Domain.Entitys.JZ_YW_ASSESS() { Del = 1, DelTime = Utils.GetDateStr() },
                w => ids.Contains(w.ID)
                );
        }

        public async Task<bool> Save(AssessDto addrDto)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_ASSESS>(addrDto);
            if (addrDto.ID <= 0)
            {
                SaveModel.AddTime = Utils.GetDateStr();
                return await assessReository.InsertReturnSnowflakeIdAsync(SaveModel)>0;
            }
            else
            {
                return await assessReository.UpdateSetColumnsTrueAsync(
                   it => new Domain.Entitys.JZ_YW_ASSESS()
                   {
                       EditTime = Utils.GetDateStr(),
                       IsGood=addrDto.IsGood,
                       UserID = addrDto.UserID,
                       Level   = addrDto.Level,
                       Pics = addrDto.Pics,
                       Text = addrDto.Text,
                       OrderID=addrDto.OrderID,

                   }, wh => wh.ID == addrDto.ID);
            }
        }

        /// <summary>
        /// 小程序获取精选评论
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AssessDto>> GetTopAssess()
        {
            return await assessReository.AsQueryable()
                .LeftJoin<Domain.Entitys.JZ_YW_USERINFO>((o, usr) => o.UserID == usr.ID)
                .Where((o, usr) => o.IsGood == "1"&&o.Del==0)
                .OrderBy((o, usr) => o.AddTime)
                .Select((o, usr) => new AssessDto() { ID = o.ID.SelectAll(), UserImg = usr.Photo, UserName = usr.NickName, })
                .ToListAsync();
        }

        /// <summary>
        /// 微信用户上传评价
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> WXSavePJ(AssessInput input)
        {
            var userModel = await userInfoRepository.GetFirstAsync(n => n.WXID == input.openid);
            if (userModel == null)
            {
                return false;
            }
            //判断评价是否存在，不存在就新增，存在就更新
            
            bool exists = await assessReository.IsAnyAsync(n => n.UserID == userModel.ID && n.OrderID == input.OrderID);
            if (exists)
            {
                return await assessReository.AsUpdateable().SetColumns(n => new Domain.Entitys.JZ_YW_ASSESS()
                {
                    EditTime = Utils.GetDateStr(),
                    Level = input.Level,
                    Pics = input.Pics,
                    Text = input.Text,
                }).Where(n => n.UserID == userModel.ID && n.OrderID == input.OrderID)
                .ExecuteCommandAsync()>0;
            }
            else
            {
                return await assessReository.InsertAsync(new Domain.Entitys.JZ_YW_ASSESS() {
                    ID=Utils.GetSnowID(),
                    Del = 0,
                    OrderID = input.OrderID,
                    UserID = userModel.ID,
                    AddTime = Utils.GetDateStr(),
                    IsGood = "0",
                    Level = input.Level,
                    Pics = input.Pics,
                    Text = input.Text,
                });

            }

        }


        /// <summary>
        /// 判断订单是否已经有评价
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public async Task<bool> Exists(long orderid)
        {
            return await assessReository.IsAnyAsync(n => n.OrderID == orderid);
        }

        /// <summary>
        /// 根据订单编码查询评价
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public async Task<AssessDto> GetModelByOrderid(long orderid)
        {
            var model= await assessReository.AsQueryable()
                .LeftJoin<Domain.Entitys.JZ_YW_USERINFO>((o, usr) => o.UserID == usr.ID)
                .Where((o, usr) => o.OrderID==orderid)
                .OrderBy((o, usr) => o.AddTime)
                .Select((o, usr) => new AssessDto() { ID = o.ID.SelectAll(), UserImg = usr.Photo, UserName = usr.NickName, })
                .FirstAsync();
            if (model == null)
            {
                return new AssessDto();
            }
            return mapper.Map<AssessDto>(model);
        }

        /// <summary>
        /// 根据服务id获取评价列表
        /// </summary>
        /// <param name="serviceid"></param>
        /// <returns></returns>
        public async Task<IEnumerable< AssessDto>> GetListByService(long serviceid)
        {
            var query = await assessReository.AsQueryable()
                .LeftJoin<Domain.Entitys.JZ_YW_USERINFO>((o, usr) => o.UserID == usr.ID)
                .Where((o, usr) => SqlFunc.Subqueryable<JZ_YW_ORDER>().Where(n=>n.FWID==serviceid &&n.ID==o.OrderID).Any())
                .OrderBy((o, usr) => o.AddTime)
                .Select((o, usr) => new AssessDto() { ID = o.ID.SelectAll(), UserImg = usr.Photo, UserName = usr.NickName, })
                .ToListAsync();
            if (query == null)
            {
                return new List<AssessDto>();
            }
            return mapper.Map<List< AssessDto>>(query);
        }
    }
}
