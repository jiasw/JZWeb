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
using JZ.Application.Contract.Dtos.Input;

namespace JZ.Application.Business
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfoService
    {
        private readonly ILogger<UserInfoService> logger;
        
        private readonly IMapper mapper;
        private readonly UserInfoRepository userInfoRepository;

        public UserInfoService(ILogger<UserInfoService> logger, IMapper mapper, UserInfoRepository userInfoRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userInfoRepository = userInfoRepository;
        }


        public async Task<PageDto<UserInfoDto>> GetListByPage(int page, int pageSize, string keyword)
        {
            RefAsync<int> totalpages = new RefAsync<int>();
            var list = await userInfoRepository.AsQueryable().WhereIF(
                !string.IsNullOrWhiteSpace(keyword), n => n.NickName.Contains(keyword) || n.Phone.Contains(keyword)
                ).OrderBy(n => n.AddTime)
                .ToPageListAsync(page, pageSize, totalpages);
            var pagelist = mapper.Map<List<UserInfoDto>>(list);
            return new PageDto<UserInfoDto>(totalpages, pagelist);
        }




        public async Task<UserInfoDto> GetModel(long id)
        {
            var model = await userInfoRepository.GetByIdAsync(id);
            return mapper.Map<UserInfoDto>(model);
        }

        public async Task<UserInfoDto> GetModelByOpenID(string openid)
        {
            var model = await userInfoRepository.GetFirstAsync(n=>n.WXID== openid);
            return mapper.Map<UserInfoDto>(model);
        }

        public async Task<bool> Delete(IEnumerable<long> ids)
        {
            return await userInfoRepository.UpdateSetColumnsTrueAsync(
                b => new Domain.Entitys.JZ_YW_USERINFO() { Del = 1, DelTime = Utils.GetDateStr() },
                w => ids.Contains(w.ID)
                );
        }

        public async Task<bool> Save(UserInfoDto addrDto)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_USERINFO>(addrDto);
            if (addrDto.ID <= 0)
            {
                SaveModel.AddTime = Utils.GetDateStr();
                return await userInfoRepository.InsertReturnSnowflakeIdAsync(SaveModel)>0;
            }
            else
            {
                return await userInfoRepository.UpdateSetColumnsTrueAsync(
                   it => new Domain.Entitys.JZ_YW_USERINFO()
                   {
                       EditTime = Utils.GetDateStr(),
                       NickName = addrDto.NickName,
                       Sex=addrDto.Sex,
                       Phone=addrDto.Phone,
                       Photo=addrDto.Photo,
                       WXID=addrDto.WXID,
                       YHLY=addrDto.YHLY,
                   }, wh => wh.ID == addrDto.ID);
            }
        }

        /// <summary>
        /// 小程序端修改用户名
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveUserName(WXSaveUserNameInput input)
        {
            return await userInfoRepository.AsUpdateable().SetColumns(n=>n.NickName==input.UserName)
                .Where(n=>n.WXID==input.openID).ExecuteCommandAsync()>0;
        }
        /// <summary>
        /// 小程序端修改用户头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> SaveUserPhoto(WXSavePhotoInput input)
        {
            return await userInfoRepository.AsUpdateable().SetColumns(n => n.Photo == input.PhotoUrl)
                .Where(n => n.WXID == input.openID).ExecuteCommandAsync() > 0;
        }

    }
}
