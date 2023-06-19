using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Demo.BL.DTO.RewardLevel;
using Demo.BL.Service;
using Demo.BL.Service.RewardLevel;

namespace Demo.Controllers
{
    [Route("api/v1/[controller]s")]
    public class RewardLevelController : BaseController<RewardLevelDto, RewardLevelUpdateDto,RewardLevelCreateDto>
    {
        public RewardLevelController(IRewardLevelService rewardLevelService) : base(rewardLevelService)
        {
        }
    }
}
