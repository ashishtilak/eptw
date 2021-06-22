using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePTW.Models
{
    public class Workgroup
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string CompCode { get; set; }

        public virtual Company Company { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        [StringLength(10)]
        public string WrkGrp { get; set; }

        [StringLength(50)] public string WrkGrpDesc { get; set; }


        [StringLength(5)] public string Location { get; set; }

        public DateTime? AddDt { get; set; }

        [StringLength(8)] // Ex. 20005116
        public string AddUser { get; set; }
    }
}