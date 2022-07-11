using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePTW.Models
{
    public class ObsHistory
    {
        [Key] public int Id { get; set; }

        public uint ObsId { get; set; }

        [StringLength(2)] public string ObsCatg { get; set; }
        [Column(TypeName = "datetime2")] public DateTime ObsDate { get; set; }

        [StringLength(10)] public string PersonResponsible { get; set; }

        [Column(TypeName = "datetime2")] public DateTime TargetDate { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? ComplianceDate { get; set; }

        [StringLength(1)] public string ObsStatus { get; set; }

        [StringLength(10)] public string ObservedBy { get; set; }

        [Column(TypeName = "datetime2")] public DateTime? StatusUpdateDate { get; set; }

        [StringLength(10)] public string ReleaseBy { get; set; }
        [StringLength(1)] public string ReleaseStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? ReleaseDate { get; set; }

        [StringLength(10)] public string StatusUpdateReleaseBy { get; set; }
        [StringLength(1)] public string StatusUpdateReleaseStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? StatusUpdateReleaseDate { get; set; }

        [Column(TypeName = "datetime2")] public DateTime AddDt { get; set; }
        [StringLength(10)] public string UserId { get; set; }
    }
}