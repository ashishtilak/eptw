using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePTW.Models
{
    public class ObsPhotos
    {
        [Key, Column(Order = 0)] public uint Id { get; set; }
        [ForeignKey("Id")] public Observation Observation { get; set; }
        [Key, Column(Order = 1)] public string ObsStatus { get; set; }  // Open, Close
        public byte[] PermitImage { get; set; }
    }
}
