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
using JZ.Application.Contract.Dtos.Input;

namespace JZ.Application.Business
{
    /// <summary>
    /// 地址服务管理
    /// </summary>
    public class AddressService
    {
        private readonly ILogger<AddressService> logger;
        private readonly IMapper mapper;
        private readonly AddressRepository addressRepository;

        public AddressService( ILogger<AddressService> logger,  IMapper mapper, AddressRepository addressRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.addressRepository = addressRepository;
        }

        public async Task<PageDto<AddressDto>> GetListByPage(int page, int pageSize, string keyword)
        {
            RefAsync<int> totalpages = new RefAsync<int>();
            var list = await addressRepository.AsQueryable().WhereIF(
                !string.IsNullOrWhiteSpace(keyword), n => n.Address.Contains(keyword)||n.Phone.Contains(keyword)
                )

                .OrderBy(n=>n.AddTime)

                .ToPageListAsync(page, pageSize, totalpages);
            var pagelist = mapper.Map<List<AddressDto>>(list);
            return new PageDto<AddressDto>(totalpages, pagelist);
        }

        /// <summary>
        /// 获取某个用户的地址列表
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AddressDto>> Getlist(string openid)
        {
            var query= await addressRepository.AsQueryable()
                .LeftJoin<JZ_YW_USERINFO>((a,u)=>a.UserID==u.ID)
                .Where((a,u)=>u.WXID==openid)
                .OrderByDescending((a, u) => a.DefaultFlag)
                .Select(a => a)
                .ToListAsync();
            return mapper.Map<List<AddressDto>>(query);
        }

        public async Task<AddressDto> GetModel(long id)
        {
            var model = await addressRepository.GetByIdAsync(id);
            return mapper.Map<AddressDto>(model);
        }

        public async Task<bool> Delete(IEnumerable<long> ids)
        {
            return await addressRepository.UpdateSetColumnsTrueAsync(
                b => new Domain.Entitys.JZ_YW_ADDR() { Del = 1, DelTime = Utils.GetDateStr() },
                w => ids.Contains(w.ID)
                );
        }

        public async Task<bool> Save(AddressDto addrDto)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_ADDR>(addrDto);
            if (addrDto.ID <= 0)
            {
                SaveModel.AddTime = Utils.GetDateStr();
                return await addressRepository.InsertReturnBigIdentityAsync(SaveModel)>0;
            }
            else
            {
                return await addressRepository.UpdateSetColumnsTrueAsync(
                   it => new Domain.Entitys.JZ_YW_ADDR()
                   {
                       EditTime= Utils.GetDateStr(),
                       Address= addrDto.Address,
                       UserID= addrDto.UserID,
                   }, wh => wh.ID == addrDto.ID);
            }
        }

        /// <summary>
        /// 微信小程序端
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> WXSave(WXAddressInput input)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_ADDR>(input);
            if (input.ID <= 0)
            {
                SaveModel.AddTime = Utils.GetDateStr();
                SaveModel.ID=Utils.GetSnowID();
                return await addressRepository.InsertReturnBigIdentityAsync(SaveModel) > 0;
            }
            else
            {
                return await addressRepository.UpdateSetColumnsTrueAsync(
                   it => new Domain.Entitys.JZ_YW_ADDR()
                   {
                       DefaultFlag=input.DefaultFlag,
                       Address= input.Address,
                       Phone= input.Phone,
                       EditTime = Utils.GetDateStr(),
                   }, wh => wh.ID == input.ID);
            }
        }

        /// <summary>
        /// 将用户的地址保存到地址列表中
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> SaveOrderAddr(WXAddressInput input)
        {
            bool exist= await addressRepository.AsQueryable().Where(n=>n.UserID==input.UserID&&n.Address==input.Address).AnyAsync();
            if (!exist)
            {
                var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_ADDR>(input);
                SaveModel.AddTime = Utils.GetDateStr();
                SaveModel.ID = Utils.GetSnowID();
                SaveModel.Del = 0;
                return await addressRepository.InsertAsync(SaveModel) ;
            }
            return true;
        }
    }
}
