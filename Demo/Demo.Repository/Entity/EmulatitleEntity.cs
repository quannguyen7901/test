using Demo.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DL.Entity
{
    /// <summary>
    /// Danh hiệu thi đua
    /// </summary>
    /// auther: NgQuan
    /// created: 05/2023
    public class EmulatitleEntity : BaseEntity
    {
        /// <summary>
        /// Id danh hiệu
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public Guid EmulatitleId { get; set; }
        /// <summary>
        /// Mã danh hiệu 
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public string? EmulatitleCode { get; set; }
        /// <summary>
        /// Tên danh hiệu
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public string? EmulatitleName { get; set; }
        /// <summary>
        /// Đối tượng khen thưởng
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public EmulatitleObject EmulatitleObject { get; set; }
        /// <summary>
        /// Loại khen thưởng
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public EmulatitleType EmulatitleType { get; set; }
        /// <summary>
        /// Id cấp khen thưởng
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public Guid RewardLevelId { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public int Status { get; set; }
        /// <summary>
        /// Tên cấp khen thưởng
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public string? RewardLevelName { get; set; }
    }
}
