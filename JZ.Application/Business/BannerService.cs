using AutoMapper;
using JZ.Application.Contract.Dtos.Business;
using JZ.Application.Contract.Infrastructure;
using JZ.Application.Infrastructure;
using JZ.Domain.Repository;
using JZ.Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Business
{
    /// <summary>
    /// banner图管理
    /// </summary>
    public class BannerService
    {
        private readonly ILogger<BannerService> logger;
        private readonly ICacheService cacheService;
        
        private readonly BannerRepository bannerRepository;
        private readonly IMapper mapper;

        public BannerService(ILogger<BannerService> logger,ICacheService cacheService, BannerRepository bannerRepository, IMapper mapper)
        {
            this.logger = logger;
            this.cacheService = cacheService;
            
            this.bannerRepository = bannerRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BannerDto>> GetBannerListWithCache()
        {
            logger.LogInformation("进入获取banner图后台服务");
            IEnumerable<BannerDto> bannerDtos = cacheService.Get<IEnumerable<BannerDto>>(HardCode.CacheBannerKey);
            if (bannerDtos != null)
            {
                return bannerDtos;
            }
            var list= await bannerRepository.GetListAsync(n => true);
            list=list.OrderBy(n=>n.PicOrder).ToList();
            bannerDtos = mapper.Map<IEnumerable<BannerDto>>(list);
            if (bannerDtos!=null&& bannerDtos.Count()>0)
            {
                cacheService.Set<IEnumerable<BannerDto>>(HardCode.CacheBannerKey, bannerDtos);
            }
            
            return bannerDtos;
        }


        public async Task<IEnumerable<BannerDto>> GetBannerList()
        {
            var list = await bannerRepository.GetListAsync(n => true);
            list = list.OrderBy(n => n.PicOrder).ToList();
            return mapper.Map<IEnumerable<BannerDto>>(list);
        }

        public async Task<BannerDto> GetBannerDtoByID(long id)
        {
            var EditModel= await bannerRepository.GetByIdAsync(id);
            return mapper.Map<BannerDto>(EditModel);
        }

        public async Task<bool> Delete(IEnumerable<long> ids)
        {
            return await bannerRepository.UpdateSetColumnsTrueAsync(
                b=>new Domain.Entitys.JZ_YW_PIC() { Del=1,DelTime=Utils.GetDateStr()},
                w=>ids.Contains(w.ID)
                );
        }

        public async Task<bool> Save(BannerDto bannerDto)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_PIC>(bannerDto);
            if (bannerDto.ID<=0)
            {
                SaveModel.AddTime=Utils.GetDateStr();
                return await bannerRepository.InsertAsync(SaveModel);
            }
            else
            {

                return await bannerRepository.UpdateSetColumnsTrueAsync(
                   it => new Domain.Entitys.JZ_YW_PIC()
                   {
                       Title = SaveModel.Title,
                       ID = SaveModel.ID,
                       Addr = SaveModel.Addr,
                       PicOrder=SaveModel.PicOrder,
                       EditTime = Utils.GetDateStr(),

                   }, wh => wh.ID == bannerDto.ID);
            }
        }
    }
}
