using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class PermitPhotos
    {
        [Key, Column(Order = 0)] public uint Id { get; set; }
        [ForeignKey("Id")] public Permit Permit { get; set; }
        [Key, Column(Order = 1)] public int Sr { get; set; }
        public byte[] PermitImage { get; set; }
    }
}