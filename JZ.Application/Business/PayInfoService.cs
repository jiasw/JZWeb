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
    /// 支付信息
    /// </summary>
    public class PayInfoService
    {
        private readonly ILogger<PayInfoService> logger;
        private readonly Contract.Infrastructure.ICacheService cacheService;
        private readonly IMapper mapper;
        private readonly PayInfoRepository payInfoRepository;

        public PayInfoService(ILogger<PayInfoService> logger, Contract.Infrastructure.ICacheService cacheService, IMapper mapper
            , PayInfoRepository payInfoRepository)
        {
            this.logger = logger;
            this.cacheService = cacheService;
            this.mapper = mapper;
            this.payInfoRepository = payInfoRepository;
        }

        public async Task<PageDto<PayInfoDto>> GetListByPage(int page, int pageSize, string keyword)
        {
            RefAsync<int> totalpages = new RefAsync<int>();
            var list = await payInfoRepository.AsQueryable()
                .LeftJoin<JZ_YW_USERINFO>((p, u) => p.UserID==u.ID )
                .WhereIF(
                !string.IsNullOrWhiteSpace(keyword), (p, u) => p.PayTitle.Contains(keyword)||p.PayOrderNO.Contains(keyword)
                ).OrderBy((p, u) => p.AddTime)
                .Select((p, u)=>new PayInfoDto() { ID=p.ID.SelectAll(),UserName=u.NickName})
                .ToPageListAsync(page, pageSize, totalpages);
            return new PageDto<PayInfoDto>(totalpages, list);
        }

        public async Task<PayInfoDto> GetModel(long id)
        {
            var model = await payInfoRepository.GetByIdAsync(id);
            return mapper.Map<PayInfoDto>(model);
        }

        public async Task<bool> Delete(IEnumerable<long> ids)
        {
            return await payInfoRepository.UpdateSetColumnsTrueAsync(
                b => new Domain.Entitys.JZ_YW_PAYINFO() { Del = 1, DelTime = Utils.GetDateStr() },
                w => ids.Contains(w.ID)
                );
        }

        public async Task<bool> Save(PayInfoDto addrDto)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_PAYINFO>(addrDto);
            if (addrDto.ID <= 0)
            {
                SaveModel.AddTime = Utils.GetDateStr();
                return await payInfoRepository.InsertAsync(SaveModel);
            }
            else
            {
                return await payInfoRepository.UpdateSetColumnsTrueAsync(
                   it => new Domain.Entitys.JZ_YW_PAYINFO()
                   {
                       EditTime = Utils.GetDateStr(),
                       CallState=addrDto.CallState,
                       OrderID = addrDto.OrderID,
                       PayOrderNO = addrDto.PayOrderNO,
                       UserID= addrDto.UserID,
                       PayRemark= addrDto.PayRemark,
                       PayState= addrDto.PayState,
                       PayTitle= addrDto.PayTitle,
                       PayType= addrDto.PayType,
                       
                   }, wh => wh.ID == addrDto.ID);
            }
        }
    }
}
