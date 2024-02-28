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
using System.Linq.Expressions;
using System.Xml.Linq;
using JZ.Application.Infrastructure;

namespace JZ.Application.Business
{
    /// <summary>
    /// 动态管理
    /// </summary>
    public class DTGLService
    {
        private readonly ILogger<DTGLService> logger;
        private readonly IMapper mapper;
        private readonly DTGLRepository dTGLRepository;

        public DTGLService(ILogger<DTGLService> logger, IMapper mapper, DTGLRepository dTGLRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.dTGLRepository = dTGLRepository;
        }

        

        public async Task<PageDto<DTGLDto>> GetListByPage(int page, int pageSize, string keyword)
        {
            RefAsync<int> totalpages=new RefAsync<int>();
            var list = await dTGLRepository.AsQueryable().WhereIF(
                !string.IsNullOrWhiteSpace(keyword), n => n.Title.Contains(keyword) || n.Content.Contains(keyword)
                )
                .OrderByDescending(n => n.AddTime)
                
                .ToPageListAsync(page, pageSize, totalpages);

            var pagelist= mapper.Map<List<DTGLDto>>(list);
            return new PageDto<DTGLDto>(totalpages, pagelist);
        }

        /// <summary>
        /// 小程序调用新闻列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DTXWListDto>> GetList()
        {
            var list = await dTGLRepository.AsQueryable()
                .OrderByDescending(n => n.AddTime).ToListAsync();
            var pagelist = mapper.Map<List<DTXWListDto>>(list);
            if (pagelist!=null)
            {
                foreach (var item in pagelist)
                {
                    item.AddTime= Utils.FormatDateTime(item.AddTime,"yyyy-MM-dd");
                }
            }
            
            return pagelist;
        }


        public async Task<DTGLDto> GetModel(long id)
        {
            var model = await dTGLRepository.GetByIdAsync(id);
            if (model==null)
            {
                model = new Domain.Entitys.JZ_YW_DTGL();
            }
           
            return mapper.Map<DTGLDto>(model);
        }

        public async Task<bool> Delete(IEnumerable<long> ids)
        {
            return await dTGLRepository.UpdateSetColumnsTrueAsync(
                b => new Domain.Entitys.JZ_YW_DTGL() { Del = 1 },
                w => ids.Contains(w.ID)
                );
        }

        public async Task<bool> Save(DTGLDto dtglDto)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_DTGL>(dtglDto);
            if (dtglDto.ID <= 0)
            {
                SaveModel.AddTime = Utils.GetDateStr();
                return await dTGLRepository.InsertAsync(SaveModel);
            }
            else
            {
                return await dTGLRepository.UpdateSetColumnsTrueAsync(
                   it => new Domain.Entitys.JZ_YW_DTGL()
                   {
                       Title=SaveModel.Title,
                       Content=SaveModel.Content,
                       EditTime=Utils.GetDateStr(),
                       Pic=SaveModel.Pic,
                   }, wh => wh.ID == dtglDto.ID);
            }
        }
    }
}
