using Dapper;
using Microsoft.AspNetCore.Mvc;
using Demo.DL.Repository;
using Demo.DL.Entity;
//using Demo.Entity;
using Demo.Enum;
using MySqlConnector;
using System.Data;
using Demo.BL.DTO.Emulatitle;
using Demo.BL.Service.Emulatitle;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    [Route("api/v1/[controller]s")]
    public class EmulatitleController : BaseController<EmulatitleDto,EmulatitleUpdateDto, EmulatitleCreateDto>
    {
        private readonly IEmulatitleService _emulatitleService;
        public EmulatitleController( IEmulatitleService emulatitleService):base(emulatitleService)
        {
          _emulatitleService= emulatitleService;
        }

        /// <summary>
        /// Thêm bản ghi mới
        /// </summary>
        /// <param name="emulatitleCreateDto">Dữ liệu của bản ghi</param>
        /// <returns>Task<EmulatitleDto?></returns>
        /// atuther: NgQuan
        /// create: 05/2023
        public override async Task<EmulatitleDto?> Create(EmulatitleCreateDto emulatitleCreateDto)
        {
            var emulatitleDto = await _baseService.InsertAsync(emulatitleCreateDto);
            return emulatitleDto;
        }

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="emulatitleCreateDto">Dữ liệu sửa bản ghi</param>
        /// <returns>Task<EmulatitleDto?></returns>
        /// atuther: NgQuan
        /// create: 05/2023
        public override async Task<EmulatitleDto?> Update(Guid id,EmulatitleUpdateDto emulatitleCreateDto)
        {
            var emulatitleDto = await _baseService.UpdateAsync(id,emulatitleCreateDto);
            return emulatitleDto;
        }

        [HttpPut ("{id}/Status")]
        public async Task<int> SetStatusEmulatitleAsync(Guid id, int status)
        {
            var setStatus =await _emulatitleService.SetStatusEmulatitleAsync(id,status);
            return setStatus;
        }

        [HttpPut("Status")]
        public async Task<int> SetMultipleStatusEmulatitleAsync([FromBody]List<Guid> id, int status)
        {
            var setStatus = await _emulatitleService.SetMultipleStatusEmulatitleAsync(id, status);
            return setStatus;
        }


        [HttpGet("Counts")]
        public async Task<int> GetCount()
        {
            var count = await _emulatitleService.GetCount();
            return count;
        }
    }
}
