using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class Locations
    {
        [Key] [StringLength(5)] public string Location { get; set; }

        [StringLength(255)] public string RemoteConnection { get; set; }
        [StringLength(255)] public string AttendanceServerApi { get; set; }
        [StringLength(255)] public string MailAddress { get; set; }
        [StringLength(255)] public string SmtpClient { get; set; }
        [StringLength(255)] public string PortalAddress { get; set; }
        [StringLength(255)] public string RemoteEssConnection { get; set; }
        
        public static readonly string Ipu = "IPU";
        public static readonly string Nkp = "NKP";
        public static readonly string Kjsaw = "KJSAW";
        public static readonly string Kjqtl = "KJQTL";
        public static readonly string Bellary = "BEL";
        public static readonly string Jfl = "JFL";
    }
}
