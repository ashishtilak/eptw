using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ePTW.Models
{
    public static class AppModelBuilderExt
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            // Seed Permit types
            modelBuilder.Entity<PermitType>().HasData(
                new PermitType
                {
                    Id = 1, PermitTypeDesc = "Height Permit",
                    DocNo = "JSL/IPU/HSE/FR-06", Version = "3.0",
                    EffectiveDate = new DateTime(2020, 01, 01), Active = true
                },
                new PermitType
                {
                    Id = 2, PermitTypeDesc = "Hot Work Permit",
                    DocNo = "JSL/IPU/HSE/FR-05", Version = "3.0",
                    EffectiveDate = new DateTime(2020, 01, 01), Active = true
                },
                new PermitType
                {
                    Id = 3, PermitTypeDesc = "Electrical Isolation Permit",
                    DocNo = "JSL/IPU/HSE/FR-07", Version = "2.0",
                    EffectiveDate = new DateTime(2020, 01, 01), Active = true
                },
                new PermitType
                {
                    Id = 4, PermitTypeDesc = "Cold Work Permit",
                    DocNo = "JSL/IPU/HSE/FR-04", Version = "1.2",
                    EffectiveDate = new DateTime(2019, 10, 01), Active = true
                },
                new PermitType
                {
                    Id = 5, PermitTypeDesc = "Vessel Entry Work Permit",
                    DocNo = "JSL/IPU/HSE/FR-08", Version = "1.1",
                    EffectiveDate = new DateTime(2016, 09, 01), Active = true
                }
            );

            // Seed Permit states
            modelBuilder.Entity<PermitState>().HasData(
                new PermitState {Id = "N", StateDesc = "Created"},
                new PermitState {Id = "P", StateDesc = "Partial Released"},
                new PermitState {Id = "F", StateDesc = "Fully Released"},
                new PermitState {Id = "S", StateDesc = "Closure Started"},
                new PermitState {Id = "R", StateDesc = "Partially Closed"},
                new PermitState {Id = "C", StateDesc = "Closed"},
                new PermitState {Id = "D", StateDesc = "Deleted"},
                new PermitState {Id = "X", StateDesc = "Force Closed"}
            );


            // Seed Release statuses
            modelBuilder.Entity<ReleaseStatus>().HasData(
                new ReleaseStatus {ReleaseStatusCode = "N", ReleaseStatusDesc = "Not released"},
                new ReleaseStatus {ReleaseStatusCode = "I", ReleaseStatusDesc = "In release"},
                new ReleaseStatus {ReleaseStatusCode = "R", ReleaseStatusDesc = "Release rejected"},
                new ReleaseStatus {ReleaseStatusCode = "F", ReleaseStatusDesc = "Fully released"}
            );

            // seed VP Releasers -- CAN'T SEED BECAUSE OF FOREIGN KEY
            //modelBuilder.Entity<VpReleasers>().HasData(
            //    new VpReleasers {EmpUnqId = "103437"}, // V. Rajasekaran
            //    new VpReleasers {EmpUnqId = "111391"}, // B. Dharwarkar
            //    new VpReleasers {EmpUnqId = "106428"}, // C. Prakash
            //    new VpReleasers {EmpUnqId = "103717"}, // JD. Chandel
            //    new VpReleasers {EmpUnqId = "104036"}, // Sanjeev Chaudhary
            //    new VpReleasers {EmpUnqId = "103741"} // Diler Singh
            //);
        }
    }
}