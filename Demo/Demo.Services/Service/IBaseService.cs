using Demo.BL.DTO.Emulatitle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Demo.BL.Service
{
    public interface IBaseService<TentityDto, TentityUpdateDto, TentityCreateDto>
    {
        /// <summary>
        /// Hàm update bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <param name="tentityUpdateDto">dữ liệu bản ghi</param>
        /// <returns>TentityDto</returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<TentityDto> UpdateAsync(Guid id, TentityUpdateDto tentityUpdateDto);
        /// <summary>
        /// Hàm insert một bản ghi
        /// </summary>
        /// <param name="tentityCreateDto">dữ liệu bản ghi</param>
        /// <returns>TentityDto</returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<TentityDto> InsertAsync(TentityCreateDto tentityCreateDto);
        /// <summary>
        /// Hàm delete bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>int</returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<int> DeleteAsync(Guid id);
        /// <summary>
        /// Hàm lấy về tất cả bản ghi
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="search">Từ tìm kiếm</param>
        /// <returns><IEnumerable<TEntity?>></returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<IEnumerable<TentityDto>> GetSearchAsync(int? pageIndex, int? pageSize, string? search);
        /// <summary>
        /// Hàm lấy về một bản ghi theo id
        /// </summary>
        /// <returns><IEnumerable<TEntity?>></returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<TentityDto> GetAsync(Guid id);
    }
}
