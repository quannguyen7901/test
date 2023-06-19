using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DL.Entity
{
    /// <summary>
    /// Lớp cơ sở
    /// </summary>
    /// auther: NgQuan
    /// created: 05/2023
    public abstract class BaseEntity
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        /// auther: NgQuan
        /// created: 05/2023
        public string? ModifiedBy { get; set; }
    }
}
