using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class VpReleasers
    {
        [Key, StringLength(10)] public string EmpUnqId { get; set; }
        [ForeignKey("EmpUnqId")] public Employee Employee { get; set; }
    }
}