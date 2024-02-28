using AutoMapper;
using JZ.Application.Contract.Dtos;
using JZ.Application.Contract.Dtos.Business;
using JZ.Application.Infrastructure;
using JZ.Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Business
{
    /// <summary>
    /// 管理员服务
    /// </summary>
    public class SysMangerUserService
    {
        private readonly ILogger<SysMangerUserService> logger;
        private readonly IMapper mapper;
        private readonly SysMangerRepository sysMangerRepository;
        private readonly IConfiguration configuration;

        public SysMangerUserService(ILogger<SysMangerUserService> logger, IMapper mapper, SysMangerRepository sysMangerRepository, IConfiguration configuration)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.sysMangerRepository = sysMangerRepository;
            this.configuration = configuration;
        }

        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MangerDto> GetMangerDto(MangerDto dto)
        {
            string newpsw = Utils.GetNewPassWord(dto.LoginPsw);
            var mangerInfo= await sysMangerRepository.GetSingleAsync(n=>n.LoginName==dto.LoginName&&n.LoginPsw== newpsw);
            MangerDto result = mapper.Map<MangerDto>(mangerInfo);
            return result;
        }

        public async Task<PageDto<MangerDto>> GetPageDtoAsync(string Name,int PageIndex,int PageSize)
        {
            RefAsync<int> total = new RefAsync<int>();
            var list = await sysMangerRepository.AsQueryable().WhereIF(!string.IsNullOrWhiteSpace(Name), n => n.Name.Contains( Name))
                .ToPageListAsync(PageIndex, PageSize, total);
            List<MangerDto> mangers = mapper.Map<List<MangerDto>>(list);
            return new PageDto<MangerDto>(total, mangers);
        }

        public async Task<MangerDto> GetModelByID(long id)
        {
            var model= await sysMangerRepository.GetByIdAsync(id);
            return mapper.Map<MangerDto>(model);
        }

        public async Task<bool> Save(MangerDto dto)
        {
            if (dto.ID>0)
            {
                if (!string.IsNullOrEmpty(dto.LoginPsw))
                {
                    dto.LoginPsw = Utils.GetNewPassWord(dto.LoginPsw);
                }
                var insertModel = mapper.Map<Domain.Entitys.JZ_SYS_MANGER>(dto);
                return await sysMangerRepository.AsUpdateable(insertModel)
                    .IgnoreColumnsIF(string.IsNullOrWhiteSpace(dto.LoginPsw),n=>n.LoginPsw)
                    .ExecuteCommandHasChangeAsync();
            }
            else
            {
                dto.LoginPsw = Utils.GetNewPassWord(dto.LoginPsw);
                var insertModel = mapper.Map<Domain.Entitys.JZ_SYS_MANGER>(dto);
                return await sysMangerRepository.InsertAsync(insertModel);
            }
            
        }

        
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns></returns>
        public async Task<bool> Delete(IEnumerable<long> ids)
        {
            return await sysMangerRepository.UpdateSetColumnsTrueAsync(n => new Domain.Entitys.JZ_SYS_MANGER() { Del = 1 }, n => ids.Contains(n.ID));

        }
    }
}
