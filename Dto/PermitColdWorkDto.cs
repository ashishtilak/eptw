using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ePTW.Models;

namespace ePTW.Dto
{
    public class PermitColdWorkDto
    {
        public uint Id { get; set; }
        public int PermitTypeId { get; set; }
        public int PermitNo { get; set; }
        public string Contractor { get; set; }
        public string ContractorName { get; set; }

        public Choices Isolationdone { get; set; }
        public Choices SafetyTags { get; set; }
        public Choices PipeConnectionBlanked { get; set; }
        public string PipeConnectionDetail { get; set; }
        public Choices WorkAreaCleaned { get; set; }
        public Choices PossibilityOfFlammable { get; set; }
        public Choices PossibilityOfGasPressure { get; set; }
        public Choices ElectricalIsolationDone { get; set; }
        public Choices AreaBarricated { get; set; }
        public string OtherCheck { get; set; }
        public Choices Helmet { get; set; }
        public Choices Shoes { get; set; }
        public Choices Belt { get; set; }
        public Choices Goggles { get; set; }
        public Choices HandGloves { get; set; }
        public Choices EarPlug { get; set; }
        public Choices NoseMask { get; set; }
    }
}
