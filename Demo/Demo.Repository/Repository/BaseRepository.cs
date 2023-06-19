using Dapper;
using Microsoft.Extensions.Configuration;
using Demo.Common.constant;
using Demo.Common.Execption;
using Demo.DL.Entity;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DL.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"] ?? "";
        }
        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>int</returns>
        /// auther: NgQuan
        /// created: 05/2023
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = (typeof(TEntity).Name);
            var table = entity.Substring(0, entity.Length - 6);
            var connection = await GetOpenConnectionAsync();
            var para = new { p_Id = id };
            var emu = await connection.ExecuteAsync($"Proc_Delete{table}", param: para, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return emu;
        }

        public Task<int> DeleteMultipleAsync(List<Guid> id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// LẤy tất cả bản ghi
        /// </summary>
        /// <returns>IEnumerable<TEntity?></returns>
        /// auther: NgQuan
        /// created: 05/2023
        public virtual async Task<IEnumerable<TEntity?>> GetAllAsync()
        {
            var entity = (typeof(TEntity).Name);
            var table = entity.Substring(0, entity.Length - 6);
            var connection = await GetOpenConnectionAsync();
            var emu = await connection.QueryAsync<TEntity>($"Proc_GetAll{table}", commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return emu;
        }
        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// auther: NgQuan
        /// created: 05/2023
        public virtual async Task<TEntity?> GetAsync(Guid id)
        {
            var entity = (typeof(TEntity).Name);
            var table = entity.Substring(0, entity.Length - 6);
            var connection = await GetOpenConnectionAsync();
            var para = new { p_Id = id };
            var emu = await connection.QueryFirstOrDefaultAsync<TEntity>($"Proc_GetId{table}", param: para, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return emu;
        }
        /// <summary>
        /// Mở kết nối đến database
        /// </summary>
        /// <returns></returns>
        /// auther: NgQuan
        /// created: 05/2023
        public virtual async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu bản ghi</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// auther: NgQuan
        /// created: 05/2023
        public virtual Task<TEntity?> InsertAsync(TEntity entity)
        {
            throw new NotBuildException(ErrorMessageConst.NotBuild, errorCode: ErrorCodeConst.NotBuild);
        }

        public virtual Task<IEnumerable<TEntity?>> SearchAsync(int pageIndex, int pageSize, string? search)
        {
            throw new NotBuildException(ErrorMessageConst.NotBuild, errorCode: ErrorCodeConst.NotBuild);
        }

        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entity">Dữ liệu của bản ghi</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// auther: NgQuan
        /// created: 05/2023
        public virtual Task<TEntity?> UpdateAsync(Guid id, TEntity entity)
        {
            throw new NotBuildException(ErrorMessageConst.NotBuild, errorCode: ErrorCodeConst.NotBuild);
        }
    }
}
