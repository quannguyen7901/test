using AutoMapper;
using Demo.BL.DTO.Emulatitle;
using Demo.BL.DTO.RewardLevel;
using Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.AutoMapper
{
    public class RewardLevelProfile:Profile
    {
        public RewardLevelProfile() {
            CreateMap<RewardLevelEntity, RewardLevelDto>();
            CreateMap<RewardLevelUpdateDto, RewardLevelEntity>();
            CreateMap<RewardLevelCreateDto, RewardLevelEntity>();
        }
    }
}
