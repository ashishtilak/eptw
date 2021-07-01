using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace ePTW.Models
{
    public class AppDbContext : DbContext
    {
        // constructor for accessing settings
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // copied from 
            // https://github.com/dotnet/efcore/issues/11003#issuecomment-492333796
            // for resolving composite primary key issue

            base.OnModelCreating(modelBuilder);

            // find all entities having count of KeyAttributes greater than one.
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes()
                .Where(t =>
                    t.ClrType.GetProperties()
                        .Count(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute))) > 1))
            {
                // get the keys in the appropriate order
                var orderedKeys = entity.ClrType
                    .GetProperties()
                    .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)))
                    .OrderBy(p =>
                        p.CustomAttributes.Single(x => x.AttributeType == typeof(ColumnAttribute))?
                            .NamedArguments?.Single(y => y.MemberName == nameof(ColumnAttribute.Order))
                            .TypedValue.Value ?? 0)
                    .Select(x => x.Name)
                    .ToArray();

                // apply the keys to the model builder
                modelBuilder.Entity(entity.ClrType).HasKey(orderedKeys);
            }


            // change default delete behaviour
            foreach (IMutableForeignKey key in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                key.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //singular table names
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }

            // data seeding extension method:
            modelBuilder.SeedData();
        }

        // declarations

        // sync following emp data tables
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Workgroup> Workgroups { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<EmpType> EmpTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleAuth> RoleAuths { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }

        public DbSet<ReleaseStatus> ReleaseStatus { get; set; } // seed
        public DbSet<ReleaseAuth> ReleaseAuths { get; set; }
        public DbSet<ReleaseStrategyLevels> ReleaseStrategyLevels { get; set; }

        public DbSet<SafetyDeptStat> SafetyDeptStats { get; set; }

        public DbSet<PermitType> PermitTypes { get; set; } // seed
        public DbSet<PermitState> PermitStates { get; set; } // seed
        public DbSet<PermitReleaseConf> PermitReleaseConfs { get; set; }

        public DbSet<Permit> Permits { get; set; }
        public DbSet<PermitCrossRef> PermitCrossRef { get; set; }
        public DbSet<PermitPerson> PermitPersons { get; set; }

        public DbSet<PermitHeight> HeightPermits { get; set; }
        public DbSet<PermitHotWork> HotWorkPermits { get; set; }
        public DbSet<PermitColdWork> ColdWorkPermits { get; set; }
        public DbSet<PermitVesselEntry> VesselEntryPermits { get; set; }
        public DbSet<PermitElecIsolation> ElecIsolationPermits { get; set; }
        public DbSet<PermitPhotos> PermitPhotos { get; set; }

        public DbSet<VpReleasers> VpReleasers { get; set; }
        public DbSet<ElecReleasers> ElecReleasers { get; set; }

        public DbSet<PermitHistory> PermitHistories { get; set; }
    }
}