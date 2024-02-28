using AutoMapper;
using JZ.Application.Contract.Dtos;
using JZ.Application.Contract.Dtos.Business;
using JZ.Application.Contract.Infrastructure;
using JZ.Application.Infrastructure;
using JZ.Domain.Repository;
using JZ.Domain.Shared;
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
    /// 字典信息服务
    /// </summary>
    public class ZDXXService
    {
        private readonly ILogger<ZDXXService> logger;
        private readonly Contract.Infrastructure.ICacheService cacheService;
        private readonly IMapper mapper;
        private readonly ZDXXRepository zDXXRepository;

        public ZDXXService(ILogger<ZDXXService> logger, Contract.Infrastructure.ICacheService cacheService, IMapper mapper, ZDXXRepository zDXXRepository)
        {
            this.logger = logger;
            this.cacheService = cacheService;
            this.mapper = mapper;
            this.zDXXRepository = zDXXRepository;
        }

        /// <summary>
        /// 获取字典类型
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ZDXXDto>> GetZDLXList()
        {
            var list= await zDXXRepository.GetListAsync();
            var zdlxlist= list.Select(n=>new ZDXXDto() { ZDFLBM=n.ZDFLBM,ZDFLMC=n.ZDFLMC})
                .GroupBy(n=>n.ZDFLBM)
                .Select(n=>n.First())
                .ToList();
            return mapper.Map<IEnumerable<ZDXXDto>>(zdlxlist);
        }


        public async Task<IEnumerable<ZDXXDto>> GetZDXXListWithCache()
        {
            IEnumerable<ZDXXDto> zdxxdtos = cacheService.Get<IEnumerable<ZDXXDto>>(HardCode.CacheZDXXKey);
            if (zdxxdtos != null&&zdxxdtos.Count()>0)
            {
                return zdxxdtos;
            }
            var list = await zDXXRepository.GetListAsync(n => true);
            list = list.OrderBy(n => n.ZDFLBM).ThenBy(n=>n.ZDSX).ToList();
            zdxxdtos = mapper.Map<IEnumerable<ZDXXDto>>(list);
            if (zdxxdtos != null && zdxxdtos.Count() > 0)
            {
                cacheService.Set<IEnumerable<ZDXXDto>>(HardCode.CacheZDXXKey, zdxxdtos);
            }
            return zdxxdtos;
        }



        public async Task<PageDto<ZDXXDto>> GetListByPage(int page, int pageSize, string lxbm)
        {
            RefAsync<int> total = new RefAsync<int>();
            var pagelist = await zDXXRepository.AsQueryable().WhereIF(!string.IsNullOrWhiteSpace(lxbm), n => n.ZDFLBM == lxbm)
                .OrderBy(n=>n.ZDSX)
                .ToPageListAsync(page, pageSize, total);
            var list = mapper.Map<List<ZDXXDto>>(pagelist);
            return new PageDto<ZDXXDto>(total, list);
        }


        public async Task<IEnumerable<ZDXXDto>> GetZDXXListByLXBM(string lxbm)
        {
            var zdlist =await GetZDXXListWithCache();
            zdlist = zdlist.Where(n => n.ZDFLBM == lxbm);
            return zdlist;
        }

        public async Task<ZDXXDto> GetZDModel(string lxbm,string zdbm)
        {
            var zdlist = await GetZDXXListWithCache();
            var model = zdlist.Where(n => n.ZDFLBM == lxbm&&n.ZDBM==zdbm).First();
            return model;
        }

        public async Task<string> GetZDMC(string lxbm,string zdbm)
        {
            string result = "";
            var zdlist = await GetZDXXListWithCache();
            var model = zdlist.Where(n => n.ZDFLBM == lxbm && n.ZDBM == zdbm).FirstOrDefault();
            if (model!=null)
            {
                result = model.ZDMC;
            }

            return result;
        }

        public async Task<ZDXXDto> GetZDModel(long id)
        {
            var zdlist = await GetZDXXListWithCache();
            var model = zdlist.Where(n => n.ID==id).First();
            return model;
        }

        public async Task<string> GetFLMC(string flbm)
        {
            var zdlist = await GetZDXXListWithCache();
            string mc = "";
            var list = zdlist.Where(n => n.ZDFLBM == flbm);
            if (list!=null&&list.Count()>0)
            {
                mc = list.First().ZDFLMC;
            }

            return mc;
        }

        public async Task<bool> Delete(IEnumerable<long> ids)
        {
            cacheService.Remove(HardCode.CacheZDXXKey);
            return await zDXXRepository.UpdateSetColumnsTrueAsync(
                b => new Domain.Entitys.JZ_SYS_ZDXX() { Del = 1 },
                w => ids.Contains(w.ID)
                );
        }

        public async Task<bool> Save(ZDXXDto bannerDto)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_SYS_ZDXX>(bannerDto);
            if (string.IsNullOrWhiteSpace( SaveModel.ZDFLMC))
            {
                SaveModel.ZDFLMC = await GetFLMC(SaveModel.ZDFLBM);
            }

            //清空缓存
            IEnumerable<ZDXXDto> zdlist = new List<ZDXXDto>();
            cacheService.Set<IEnumerable<ZDXXDto>>(HardCode.CacheZDXXKey, zdlist);

            if (bannerDto.ID <= 0)
            {
                return await zDXXRepository.InsertAsync(SaveModel);
            }
            else
            {
                return await zDXXRepository.UpdateSetColumnsTrueAsync(
                   it => new Domain.Entitys.JZ_SYS_ZDXX()
                   {
                       ZDBM = SaveModel.ZDBM,
                       ID = SaveModel.ID,
                       ZDMC = SaveModel.ZDMC,
                       ZDFLBM = SaveModel.ZDFLBM,
                       ZDFLMC = SaveModel.ZDFLMC,
                       ZDSX = SaveModel.ZDSX,
                       BY1 = SaveModel.BY1
                   }, wh => wh.ID == bannerDto.ID);
            }
            
        }

    }
}
