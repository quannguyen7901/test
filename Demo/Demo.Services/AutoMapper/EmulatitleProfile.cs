using AutoMapper;
using Demo.BL.DTO.Emulatitle;
using Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.AutoMapper
{
    public class EmulatitleProfile : Profile
    {
        public EmulatitleProfile()
        {
            CreateMap<EmulatitleEntity, EmulatitleDto>();
            CreateMap<EmulatitleCreateDto, EmulatitleEntity>();
            CreateMap<EmulatitleUpdateDto, EmulatitleEntity>();
        }
    }
}
