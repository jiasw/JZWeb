using AutoMapper;
using JZ.Application.Contract.Dtos.Business;
using JZ.Application.Contract.Dtos;
using JZ.Application.Contract.Infrastructure;
using JZ.Domain.Repository;
using JZ.Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using JZ.Application.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace JZ.Application.Business
{
    /// <summary>
    /// 服务项目信息
    /// </summary>
    public class FWXXService
    {
        private readonly ILogger<FWXXService> logger;
        
        private readonly IMapper mapper;
        private readonly FWXXRepository fWXXRepository;
        private readonly IConfiguration configuration;

        public FWXXService(ILogger<FWXXService> logger, IMapper mapper, FWXXRepository fWXXRepository,IConfiguration configuration)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.fWXXRepository = fWXXRepository;
            this.configuration = configuration;
        }
        


        public async Task<IEnumerable<FWXXListDto>> GetListByLXBM(string lxbm)
        {
            var list = await fWXXRepository.GetListAsync(n => n.FWLX == lxbm);
            
            return mapper.Map<List<FWXXListDto>>(list);
        }

        /// <summary>
        /// 获取优质服务
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FWXXListDto>> GetTopService()
        {
            var list = await fWXXRepository.GetListAsync(n => n.IsGood == "1");
            

            return mapper.Map<List<FWXXListDto>>(list);
        }

        public async Task<PageDto<FWXXDto>> GetListByPage(int page, int pageSize, string lxbm)
        {
            RefAsync<int> totalpages = new RefAsync<int>();
            var list = await fWXXRepository.AsQueryable().WhereIF(
                !string.IsNullOrWhiteSpace(lxbm), n => n.FWLX==lxbm
                )
                
                .OrderBy("FWLX,SX")
                
                .ToPageListAsync(page, pageSize, totalpages);
            var pagelist = mapper.Map<List<FWXXDto>>(list);
            return new PageDto<FWXXDto>(totalpages, pagelist);
        }


        

        public async Task<FWXXDto> GetModel(long id)
        {
            var model = await fWXXRepository.GetByIdAsync(id);
            return mapper.Map<FWXXDto>(model);
        }

        public async Task<bool> Delete(IEnumerable<long> ids)
        {
            return await fWXXRepository.UpdateSetColumnsTrueAsync(
                b => new Domain.Entitys.JZ_YW_FWXX() { Del = 1,DelTime=Utils.GetDateStr() },
                w => ids.Contains(w.ID)
                );
        }

        public async Task<bool> Save(FWXXDto fwxxDto)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_FWXX>(fwxxDto);
            if (fwxxDto.ID <= 0)
            {
                SaveModel.AddTime=Utils.GetDateStr();
                return await fWXXRepository.InsertAsync(SaveModel);
            }
            else
            {
                return await fWXXRepository.UpdateSetColumnsTrueAsync(
                   it => new Domain.Entitys.JZ_YW_FWXX()
                   {
                       FWLX=SaveModel.FWLX,
                       Pic=SaveModel.Pic,
                       Title=SaveModel.Title,
                       Summary=SaveModel.Summary,
                       Context=SaveModel.Context,
                       Price=SaveModel.Price,
                       Unit=SaveModel.Unit,
                       SX=SaveModel.SX,
                       EditTime=SaveModel.EditTime,
                       IsGood=SaveModel.IsGood,
                       Banner=SaveModel.Banner,
                   }, wh => wh.ID == fwxxDto.ID);
            }
        }
    }
}
