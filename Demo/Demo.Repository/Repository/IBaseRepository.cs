using Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DL.Repository
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Hàm mở kết nối database
        /// </summary>
        /// <returns>DbConnection</returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<DbConnection> GetOpenConnectionAsync();

        /// <summary>
        /// Hàm update bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <param name="entity">Giá trị của bản ghi</param>
        /// <returns>TEntity</returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<TEntity?> UpdateAsync(Guid id, TEntity entity);

        /// <summary>
        /// Hàm insert bản ghi
        /// </summary>
        /// <param name="entity">Giá trị của bản ghi</param>
        /// <returns>TEntity</returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<TEntity?> InsertAsync(TEntity entity);

        /// <summary>
        /// Hàm delete bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>int</returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Hàm delete bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>int</returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<int> DeleteMultipleAsync(List<Guid> id);

        /// <summary>
        /// Hàm lấy về tất cả bản ghi
        /// </summary>
        /// <returns><IEnumerable<TEntity?>></returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<IEnumerable<TEntity?>> GetAllAsync();

        /// <summary>
        /// Hàm lấy về một bản ghi theo id
        /// </summary>
        /// <returns><IEnumerable<TEntity?>></returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<TEntity?> GetAsync(Guid id);

        /// <summary>
        /// Tìm kiếm và phân trang bản ghi
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="search">Từ tìm kiếm</param>
        /// <returns>IEnumerable<TEntity?></returns>
        /// auther: NGQuan
        /// created: 05/2023
        Task<IEnumerable<TEntity?>> SearchAsync(int pageIndex, int pageSize, string? search);
    }
}
