using AutoMapper;
using Demo.Common.constant;
using Demo.Common.Execption;
using Demo.DL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Service
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityUpdateDto, TentityCreateDto> : IBaseService<TEntityDto, TEntityUpdateDto, TentityCreateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Hàm delete bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>int</returns>
        /// auther: NGQuan
        /// created: 05/2023
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(ErrorMessageConst.NotFound, errorCode: ErrorCodeConst.BusinessNotFound);
            }
            else
            {
                int deleteInt = await _baseRepository.DeleteAsync(id);
                return deleteInt;
            }
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>

        public virtual async Task DeleteTaskAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(ErrorMessageConst.NotFound, errorCode: ErrorCodeConst.BusinessNotFound);
            }
            else
            {
                await _baseRepository.DeleteAsync(id);
            }
        }

        /// <summary>
        /// Hàm lấy về tất cả bản ghi
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="search">Từ tìm kiếm</param>
        /// <returns><IEnumerable<TEntity?>></returns>
        /// auther: NGQuan
        /// created: 05/2023
        public virtual async Task<IEnumerable<TEntityDto>> GetSearchAsync(int? pageIndex, int? pageSize, string? search)
        {
            if (pageIndex == null || pageSize == null)
            {
                var entity = await _baseRepository.GetAllAsync();
                if (entity == null)
                {
                    throw new NotFoundException(ErrorMessageConst.NotFound, errorCode: ErrorCodeConst.BusinessNotFound);
                }
                else
                {
                    var entityDtos = new List<TEntityDto>();
                    foreach (var entityDto in entity)
                    {
                        entityDtos.Add(_mapper.Map<TEntityDto>(entityDto));
                    }
                    return entityDtos;
                }
            }
            else
            {
                var entity = await _baseRepository.SearchAsync((int)pageIndex,(int)pageSize,search);
                if (entity == null)
                {
                    throw new NotFoundException(ErrorMessageConst.NotFound, errorCode: ErrorCodeConst.BusinessNotFound);
                }
                else
                {
                    var entityDtos = new List<TEntityDto>();
                    foreach (var entityDto in entity)
                    {
                        entityDtos.Add(_mapper.Map<TEntityDto>(entityDto));
                    }
                    return entityDtos;
                }
            }
        }
        /// <summary>
        /// Hàm lấy về một bản ghi theo id
        /// </summary>
        /// <returns><IEnumerable<TEntity?>></returns>
        /// auther: NGQuan
        /// created: 05/2023
        public virtual async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(ErrorMessageConst.NotFound, errorCode: ErrorCodeConst.BusinessNotFound);
            }
            else
            {
                var entityDto = _mapper.Map<TEntityDto>(entity);
                return entityDto;
            }
        }
        /// <summary>
        /// Hàm insert một bản ghi
        /// </summary>
        /// <param name="entity">dữ liệu bản ghi</param>
        /// <returns>TEntityDto</returns>
        /// auther: NGQuan
        /// created: 05/2023
        public virtual Task<TEntityDto> InsertAsync(TentityCreateDto tentityCreateDto)
        {
            throw new NotBuildException(ErrorMessageConst.NotBuild, errorCode: ErrorCodeConst.NotBuild);
        }

        /// <summary>
        /// Hàm update bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <param name="entity">dữ liệu bản ghi</param>
        /// <returns>TEntityDto</returns>
        /// auther: NGQuan
        /// created: 05/2023
        public virtual Task<TEntityDto> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            throw new NotBuildException(ErrorMessageConst.NotBuild, errorCode: ErrorCodeConst.NotBuild);
        }
    }
}
