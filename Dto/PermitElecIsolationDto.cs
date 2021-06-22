using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePTW.Models;

namespace ePTW.Dto
{
    public class PermitElecIsolationDto
    {
        public uint Id { get; set; }
        public int PermitTypeId { get; set; }
        public int PermitNo { get; set; }
        public string ReqIsolationEquipment { get; set; }
        public string ReqPanel { get; set; }
        public string ReqWork { get; set; }
        public string PanelFeederNo { get; set; }
        public string PanelNo { get; set; }
        public string Remarks { get; set; }
        public string LottoKey { get; set; }
        public Choices Helmet { get; set; }
        public Choices Shoes { get; set; }
        public Choices Belt { get; set; }
        public Choices Goggles { get; set; }
        public Choices HandGloves { get; set; }
        public Choices EarPlug { get; set; }
        public Choices NoseMask { get; set; }
    }
}
