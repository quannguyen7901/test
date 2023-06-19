using Microsoft.AspNetCore.Mvc;
using Demo.BL.DTO.Emulatitle;
using Demo.BL.Service;
using Demo.Common.constant;
using Demo.Common.Execption;

namespace Demo.Controllers
{
    [ApiController]
    public abstract class BaseController<TEntityDto, TEnteityUpdateDto, TentityCreateDto> : ControllerBase
    {
        protected readonly IBaseService<TEntityDto, TEnteityUpdateDto, TentityCreateDto> _baseService;
        public BaseController(IBaseService<TEntityDto, TEnteityUpdateDto, TentityCreateDto> baseService)
        {
            _baseService = baseService;
        }

        // GET: api/<EmulatitleController>
        /// <summary>
        /// Hàm lấy về tất cả bản ghi
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="search">Từ tìm kiếm</param>
        /// <returns><IEnumerable<TEntity?>></returns>
        /// auther: NGQuan
        /// created: 05/2023
        [HttpGet]
        public virtual async Task<IEnumerable<TEntityDto?>> GetSearchAsync(int? pageIndex, int? pageSize, string? search)
        {
            var entityDto = await _baseService.GetSearchAsync(pageIndex,pageSize,search);
            return entityDto;
        }

        /// <summary>
        /// Hàm lấy về một bản ghi theo id
        /// </summary>
        /// <returns><IEnumerable<TEntity?>></returns>
        /// auther: NGQuan
        /// created: 05/2023
        [HttpGet("{id}")]
        public virtual async Task<TEntityDto?> GetAsync(Guid id)
        {
            var entityDto = await _baseService.GetAsync(id);
            return entityDto;
        }
        /// <summary>
        /// Hàm delete bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>int</returns>
        /// auther: NGQuan
        /// created: 05/2023
        [HttpDelete("{id}")]
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            int entityDto = await _baseService.DeleteAsync(id);
            return entityDto;
        }

        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <param name="tentityCreate">Dữ liệu bản ghi</param>
        /// <returns>Task<TEntityDto?></returns>
        /// atuther: NgQuan
        /// create: 05/2023
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public virtual Task<TEntityDto?> Create(TentityCreateDto tentityCreate)
        {
            throw new NotBuildException(ErrorMessageConst.NotBuild, errorCode: ErrorCodeConst.NotBuild);
        }

        /// <summary>
        /// Hàm sửa bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="tentiyUpdate">Dữ liệu bản ghi</param>
        /// <returns>Task<TEntityDto?></returns>
        /// atuther: NgQuan
        /// create: 05/2023
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut("{id}")]
        public virtual Task<TEntityDto?> Update(Guid id, TEnteityUpdateDto tentiyUpdate)
        {
            throw new NotBuildException(ErrorMessageConst.NotBuild, errorCode: ErrorCodeConst.NotBuild);
        }

    }
}
