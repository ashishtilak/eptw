using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class PermitElecIsolation
    {
        [Key] public uint Id { get; set; }
        [ForeignKey("Id")] public Permit Permit { get; set; }

        public int PermitTypeId { get; set; }
        [ForeignKey("PermitTypeId")] public PermitType PermitType { get; set; }

        public int PermitNo { get; set; }

        [StringLength(50)] public string ReqIsolationEquipment { get; set; }
        [StringLength(50)] public string ReqPanel { get; set; }
        [StringLength(50)] public string ReqWork { get; set; }
        [StringLength(50)] public string PanelFeederNo { get; set; }
        [StringLength(50)] public string PanelNo { get; set; }
        [StringLength(50)] public string Remarks { get; set; }
        [StringLength(50)] public string LottoKey { get; set; }
        public Choices Helmet { get; set; }
        public Choices Shoes { get; set; }
        public Choices Belt { get; set; }
        public Choices Goggles { get; set; }
        public Choices HandGloves { get; set; }
        public Choices EarPlug { get; set; }
        public Choices NoseMask { get; set; }
    }
}