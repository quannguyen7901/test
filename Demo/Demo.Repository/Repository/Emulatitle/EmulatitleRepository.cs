using Microsoft.Extensions.Configuration;
using Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using static Dapper.SqlMapper;
using Demo.Common.Enum;

namespace Demo.DL.Repository.Emulatitle
{
    public class EmulatitleRepository : BaseRepository<EmulatitleEntity>, IEmulatitleRepository
    {

        public EmulatitleRepository(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Kiểm tra mã trùng lặp
        /// </summary>
        /// <param name="emulatitleCode">Mã của bản ghi</param>
        /// <returns>bool</returns>
        /// auther: NgQuan
        /// created: 05/2023
        public async Task<bool> CheckEmulatitleCode(string emulatitleCode)
        {
            var connection = await GetOpenConnectionAsync();
            var sql = $"SELECT e.EmulatitleCode FROM emulatitle e WHERE e.EmulatitleCode='{emulatitleCode}' AND e.Status!=-1";
            var emu = await connection.QueryAsync(sql);
            if(emu.Count() != 0) 
            {
                return false;
            }
            else 
            { 
                return true; 
            }
        }
        /// <summary>
        /// Tìm kiếm và phân trang bản ghi
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="search">Từ tìm kiếm</param>
        /// <returns>IEnumerable<EmulatitleEntity?></returns>
        public override async Task<IEnumerable<EmulatitleEntity?>> SearchAsync(int pageIndex, int pageSize, string? search)
        {
            var connection = await GetOpenConnectionAsync();
            var para = new { p_PageIndex = pageIndex,p_PageSize=pageSize,p_Search= search };
            var emulatitleEntities = await connection.QueryAsync<EmulatitleEntity>("Proc_SearchEmulatitle", param: para, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return emulatitleEntities;
        }
        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="emulatitleId">Mã bản ghi</param>
        /// <param name="emulatitle">Dữ liệu bản ghi</param>
        /// <returns>EmulatitleEntity</returns>
        /// auther: NgQuan
        /// created: 05/2023
        public override async Task<EmulatitleEntity?> UpdateAsync(Guid emulatitleId, EmulatitleEntity emulatitle)
        {
            var connection = await GetOpenConnectionAsync();

            var para = new {
                p_EmulatitleId = emulatitle.EmulatitleId, 
                p_EmulatitleCode= emulatitle.EmulatitleCode, 
                p_EmulatitleName=emulatitle.EmulatitleName,
                p_EmulatitleObject=(int)emulatitle.EmulatitleObject,
                p_EmulatitleType=(int)emulatitle.EmulatitleType,
                p_RewardLevelId=emulatitle.RewardLevelId,
                p_Status=(int)emulatitle.Status,
                p_UpdateAt = emulatitle.ModifiedDate
            };
            await connection.ExecuteAsync("Proc_UpdateEmulatitle", param: para, commandType: CommandType.StoredProcedure);
            var emulatitleEntity = await GetAsync(emulatitleId);
            await connection.CloseAsync();
            return emulatitleEntity;
        }

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="emulatitleId">Mã bản ghi</param>
        /// <param name="emulatitle">Dữ liệu bản ghi</param>
        /// <returns>EmulatitleEntity</returns>
        /// auther: NgQuan
        /// created: 05/2023
        public override async Task<EmulatitleEntity?> InsertAsync(EmulatitleEntity emulatitle)
        {
            var connection = await GetOpenConnectionAsync();

            var para = new
            {
                p_EmulatitleId = emulatitle.EmulatitleId,
                p_EmulatitleCode = emulatitle.EmulatitleCode,
                p_EmulatitleName = emulatitle.EmulatitleName,
                p_EmulatitleObject = emulatitle.EmulatitleObject,
                p_EmulatitleType = emulatitle.EmulatitleType,
                p_RewardLevelId = emulatitle.RewardLevelId,
                p_CreateAt = emulatitle.CreatedDate
            };
            await connection.ExecuteAsync("Proc_AddEmulatitle", param: para, commandType: CommandType.StoredProcedure);
            var emulatitleEntity = await GetAsync(emulatitle.EmulatitleId);
            await connection.CloseAsync();
            return emulatitleEntity;
        }

        public async Task<int> SetStatusEmulatitleAsync(Guid emulatitleId, int status)
        {
            var connection = await GetOpenConnectionAsync();
            var sql = $"UPDATE emulatitle e SET e.Status={status} WHERE e.EmulatitleId='{emulatitleId}'";
            var emu = await connection.ExecuteAsync(sql);
            return emu;
        }

        public async Task<bool> CheckRewardId(Guid id)
        {
            var connection = await GetOpenConnectionAsync();
            var sql = $"SELECT e.RewardLevelId FROM rewardlevel e WHERE e.RewardLevelId='{id}'";
            var emu = await connection.QueryAsync(sql);
            if (emu != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> SetMultipleStatusEmulatitleAsync(List<Guid> emulatitleIds, int status)
        {
            var listId = String.Join("','", emulatitleIds.ToArray());
            var connection = await GetOpenConnectionAsync();
            var sql = $"UPDATE emulatitle e SET e.Status={status} WHERE e.EmulatitleId IN ('{listId}')";
            var emu = await connection.ExecuteAsync(sql);
            return emu;
        }
    }
}
