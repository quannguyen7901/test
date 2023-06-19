using Demo.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.DTO.Emulatitle
{
    public class EmulatitleDto
    {
        public Guid EmulatitleId { get; set; }
        public string? EmulatitleCode { get; set; }
        public string? EmulatitleName { get; set; }
        public EmulatitleObject EmulatitleObject { get; set; }
        public EmulatitleType EmulatitleType { get; set; }
        public Guid RewardLevelId { get; set; }
        public Status Status { get; set; }
        public string? RewardLevelName { get; set; }
    }
}
