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
using JZ.Application.Infrastructure;

namespace JZ.Application.Business
{
    /// <summary>
    /// 联系我们
    /// </summary>
    public class LXWMService
    {
        private readonly ILogger<LXWMService> logger;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;
        private readonly LXWMRepository lXWMRepository;

        public LXWMService(ILogger<LXWMService> logger, ICacheService cacheService, IMapper mapper, LXWMRepository lXWMRepository)
        {
            this.logger = logger;
            this.cacheService = cacheService;
            this.mapper = mapper;
            this.lXWMRepository = lXWMRepository;
        }

        

        public async Task<LXWMDto> GetModel()
        {
            var Model= await lXWMRepository.GetFirstAsync(n=>true);
            return mapper.Map<LXWMDto>(Model);
        }


        public async Task<bool> Save(LXWMDto lXWMDto)
        {
            var SaveModel = mapper.Map<Domain.Entitys.JZ_YW_LXWM>(lXWMDto);
            if (lXWMDto.ID <= 0)
            {
                SaveModel.AddTime = Utils.GetDateStr();
                return await lXWMRepository.InsertAsync(SaveModel);
            }
            else
            {
                return await lXWMRepository.UpdateSetColumnsTrueAsync(
                   it => new Domain.Entitys.JZ_YW_LXWM()
                   {
                       Address = lXWMDto.Address,
                       Email= lXWMDto.Email,
                       Phone = lXWMDto.Phone,
                       Summary = lXWMDto.Summary,
                   }, wh => wh.ID == lXWMDto.ID);
            }
        }
    }
}
