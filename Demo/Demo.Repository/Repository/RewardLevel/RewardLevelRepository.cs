using Microsoft.Extensions.Configuration;
using Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DL.Repository.RewardLevel
{
    public class RewardLevelRepository : BaseRepository<RewardLevelEntity>, IRewardLevelRepository
    {
        public RewardLevelRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
