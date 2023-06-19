using AutoMapper;
using Demo.BL.DTO.RewardLevel;
using Demo.DL.Entity;
using Demo.DL.Repository;
using Demo.DL.Repository.RewardLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Service.RewardLevel
{
    public class RewardLevelService : BaseService<RewardLevelEntity, RewardLevelDto, RewardLevelUpdateDto, RewardLevelCreateDto>, IRewardLevelService
    {
        public RewardLevelService(IRewardLevelRepository baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
        }
    }
}
