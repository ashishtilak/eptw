using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class ReleaseStrategyLevels
    {
        [Key] [StringLength(15)] public string ReleaseStrategy { get; set; }
        [Required] [StringLength(20)] public string ReleaseCode { get; set; }
    }
}
