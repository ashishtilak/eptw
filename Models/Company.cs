using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class Company
    {
        [Key] [Required] [StringLength(2)] public string CompCode { get; set; }
        [StringLength(50)] public string CompName { get; set; }
        [StringLength(5)] public string Location { get; set; }
    }
}
