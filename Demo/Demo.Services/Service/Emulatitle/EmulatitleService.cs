using AutoMapper;
using Demo.BL.DTO.Emulatitle;
using Demo.Common.constant;
using Demo.Common.Enum;
using Demo.Common.Execption;
using Demo.DL.Entity;
using Demo.DL.Repository.Emulatitle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Service.Emulatitle
{
    public class EmulatitleService : BaseService<EmulatitleEntity, EmulatitleDto, EmulatitleUpdateDto, EmulatitleCreateDto>, IEmulatitleService
    {
        private readonly IEmulatitleRepository _emulatitleRepository;

        public EmulatitleService(IEmulatitleRepository emulatitleRepository, IMapper mapper) : base(emulatitleRepository, mapper)
        {
            _emulatitleRepository = emulatitleRepository;
        }
        /// <summary>
        /// Kiểm tra mã trùng lặp
        /// </summary>
        /// <param name="emulatitleCode">Mã của bản ghi</param>
        /// <returns>bool</returns>
        /// auther: NgQuan
        /// created: 05/2023
        public Task<bool> CheckEmulatitleCode(string? emulatitleCode)
        {
            if (emulatitleCode == null)
            {
                return Task.FromResult(false);
            }
            return _emulatitleRepository.CheckEmulatitleCode(emulatitleCode);
        }


        /// <summary>
        /// Tìm kiếm và phân trang bản ghi
        /// </summary>
        /// <returns>IEnumerable<EmulatitleEntity?></returns>
        /// atuther: NgQuan
        /// create: 05/2023
        public override async Task<EmulatitleDto> UpdateAsync(Guid id, EmulatitleUpdateDto emulatitleUpdate)
        {
            var emulatitleDto = await GetAsync(id);
            var checkCode = false;
            if (emulatitleDto.EmulatitleCode == emulatitleUpdate.EmulatitleCode)
            {
                checkCode = true;
            }
            else
            {
                checkCode = await CheckEmulatitleCode(emulatitleUpdate.EmulatitleCode);
            }
            var checkFkId = await CheckRewardId(emulatitleUpdate.RewardLevelId);
            if (checkFkId && checkCode)
            {
                var emulatitle = _mapper.Map<EmulatitleEntity>(emulatitleUpdate);
                emulatitle.EmulatitleId = id;
                emulatitle.ModifiedDate = DateTime.UtcNow;
                var entity = await _emulatitleRepository.UpdateAsync(id, emulatitle);
                if (entity == null)
                {
                    throw new NotFoundException(ErrorMessageConst.NotFound, errorCode: ErrorCodeConst.BusinessNotFound);
                }
                else
                {
                    var emulatitleBeforUpdate = _mapper.Map<EmulatitleDto>(entity);
                    return emulatitleBeforUpdate;
                }
            }
            else
            {
                if (!checkFkId)
                {
                    throw new WrongFormatException(ErrorMessageConst.NotFoundFk, errorCode: ErrorCodeConst.WrongFormat);
                }
                else if (!checkCode)
                {
                    throw new WrongFormatException(ErrorMessageConst.DuplicateCode, errorCode: ErrorCodeConst.WrongFormat);
                }
                else
                {
                    throw new WrongFormatException(ErrorMessageConst.WrongFormat, errorCode: ErrorCodeConst.WrongFormat);

                }
            }
        }

        /// <summary>
        /// Tìm kiếm và phân trang bản ghi
        /// </summary>
        /// <returns>IEnumerable<EmulatitleEntity?></returns>
        /// atuther: NgQuan
        /// create: 05/2023
        public override async Task<EmulatitleDto> InsertAsync(EmulatitleCreateDto emulatitleCreateDto)
        {
            var checkCode = await CheckEmulatitleCode(emulatitleCreateDto.EmulatitleCode);
            var checkFkId = await CheckRewardId(emulatitleCreateDto.RewardLevelId);
            if (checkCode && checkFkId)
            {
                var emulatitle = _mapper.Map<EmulatitleEntity>(emulatitleCreateDto);
                emulatitle.EmulatitleId = Guid.NewGuid();
                emulatitle.CreatedDate = DateTime.UtcNow;
                var entity = await _emulatitleRepository.InsertAsync(emulatitle);
                if (entity == null)
                {
                    throw new NotFoundException(ErrorMessageConst.NotFound, errorCode: ErrorCodeConst.BusinessNotFound);
                }
                else
                {
                    var emulatitleBeforUpdate = _mapper.Map<EmulatitleDto>(entity);
                    return emulatitleBeforUpdate;
                }
            }
            else
            {
                if (!checkCode)
                {
                    throw new WrongFormatException(ErrorMessageConst.DuplicateCode, errorCode: ErrorCodeConst.WrongFormat);
                }
                else if (!checkFkId)
                {
                    throw new WrongFormatException(ErrorMessageConst.NotFoundFk, errorCode: ErrorCodeConst.WrongFormat);
                }
                else
                {
                    throw new WrongFormatException(ErrorMessageConst.WrongFormat, errorCode: ErrorCodeConst.WrongFormat);

                }
            }
        }


        /// <summary>
        /// Test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>

        public override async Task DeleteTaskAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(ErrorMessageConst.NotFound, errorCode: ErrorCodeConst.BusinessNotFound);
            }

            if (entity.EmulatitleType == Common.Enum.EmulatitleType.All)
            {
                throw new Exception("Không xóa được");
            }
            await _baseRepository.DeleteAsync(id);

        }

        public async Task<int> SetStatusEmulatitleAsync(Guid emulatitleId, int status)
        {
            if (status == (int)Status.Use || status == (int)Status.UnUse)
            {
                var entity = await _emulatitleRepository.GetAsync(emulatitleId);
                if (entity == null)
                {
                    throw new NotFoundException(ErrorMessageConst.NotFound, errorCode: ErrorCodeConst.BusinessNotFound);
                }
                else
                {
                    var setStats = await _emulatitleRepository.SetStatusEmulatitleAsync(emulatitleId, status);
                    return setStats;
                }
            }
            else
            {
                throw new WrongFormatException(ErrorMessageConst.WrongFormat, errorCode: ErrorCodeConst.WrongFormat);
            }
        }

        public Task<bool> CheckRewardId(Guid id)
        {
            return _emulatitleRepository.CheckRewardId(id);
        }

        public async Task<int> SetMultipleStatusEmulatitleAsync(List<Guid> emulatitleIds, int status)
        {
            if (status == (int)Status.Use || status == (int)Status.UnUse)
            {
                var setStats = await _emulatitleRepository.SetMultipleStatusEmulatitleAsync(emulatitleIds, status);
                return setStats;
            }
            else
            {
                throw new WrongFormatException(ErrorMessageConst.WrongFormat, errorCode: ErrorCodeConst.WrongFormat);
            }
        }

        public async Task<int> GetCount()
        {
            var entity = await _emulatitleRepository.GetAllAsync();
            if (entity == null)
            {
                throw new NotFoundException(ErrorMessageConst.NotFound, errorCode: ErrorCodeConst.BusinessNotFound);
            }
            else
            {
                return entity.Count();
            }
        }
    }
}