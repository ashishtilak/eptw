using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class PermitState
    {
        [Key]
        [StringLength(1)] public string Id { get; set; }

        [StringLength(30)] public string StateDesc { get; set; }

        public const string Created = "N";
        public const string PartiallyReleased = "P";
        public const string FullyReleased = "F";
        public const string ClosureStarted = "S";
        public const string PartiallyClosed = "R";
        public const string Closed = "C";
        public const string Deleted = "X";
        public const string ForceClosed = "X";
    }
}