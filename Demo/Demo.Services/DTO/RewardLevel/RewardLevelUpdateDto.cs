using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.DTO.RewardLevel
{
    public class RewardLevelUpdateDto
    {
        /// <summary>
        /// Id cấp khen thưởng
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public Guid RewardLevelId { get; set; }
        /// <summary>
        /// Tên cấp khen thưởng
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public string? RewardLevelName { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public int Status { get; set; }
    }
}
