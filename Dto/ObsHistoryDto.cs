using System;

namespace ePTW.Dto
{
    public class ObsHistoryDto
    {
        public int Id { get; set; }

        public uint ObsId { get; set; }

        public string ObsCatg { get; set; }
        public DateTime ObsDate { get; set; }

        public string PersonResponsible { get; set; }

        public DateTime TargetDate { get; set; }
        public DateTime? ComplianceDate { get; set; }

        public string ObsStatus { get; set; }

        public string ObservedBy { get; set; }

        public DateTime? StatusUpdateDate { get; set; }

        public string ReleaseBy { get; set; }
        public string ReleaseStatus { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public string StatusUpdateReleaseBy { get; set; }
        public string StatusUpdateReleaseStatus { get; set; }
        public DateTime? StatusUpdateReleaseDate { get; set; }

        public DateTime AddDt { get; set; }
        public string UserId { get; set; }
    }
}
