using Demo.BL.DTO.Emulatitle;
using Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Service.Emulatitle
{
    public interface IEmulatitleService : IBaseService<EmulatitleDto, EmulatitleUpdateDto, EmulatitleCreateDto>
    {
        /// <summary>
        /// Kiểm tra mã trùng lặp
        /// </summary>
        /// <param name="emulatitleCode">Mã của bản ghi</param>
        /// <returns>bool</returns>
        /// auther: NgQuan
        /// created: 05/2023
        Task<bool> CheckEmulatitleCode(string emulatitleCode);

        /// <summary>
        /// Kiểm tra mã trùng lặp
        /// </summary>
        /// <param name="emulatitleCode">Mã của bản ghi</param>
        /// <returns>bool</returns>
        /// auther: NgQuan
        /// created: 05/2023
        Task<bool> CheckRewardId(Guid id);

        /// <summary>
        /// Set trạng thái của danh hiệu thi đua
        /// </summary>
        /// <param name="emulatitleId">Id danh hiệu thi đua</param>
        /// <param name="status">Trạng thái danh hiệu</param>
        /// <returns>int</returns>
        Task<int> SetStatusEmulatitleAsync(Guid emulatitleId, int status);

        /// <summary>
        /// Set trạng thái của danh hiệu thi đua
        /// </summary>
        /// <param name="emulatitleId">Id danh hiệu thi đua</param>
        /// <param name="status">Trạng thái danh hiệu</param>
        /// <returns>int</returns>
        Task<int> SetMultipleStatusEmulatitleAsync(List<Guid> emulatitleId, int status);

        Task<int> GetCount();

    }
}
