using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class ReleaseStatus
    {
        [Key] [StringLength(1)] public string ReleaseStatusCode { get; set; }
        [StringLength(20)] public string ReleaseStatusDesc { get; set; }

        //ADDING STATIC MEMBERS TO ELIMINATE MAGIC STRINGS
        public static readonly string NotReleased = "N";
        public static readonly string FullyReleased = "F";
        public static readonly string InRelease = "I";
        public static readonly string PartiallyReleased = "P";
        public static readonly string ReleaseRejected = "R";
    }
}
