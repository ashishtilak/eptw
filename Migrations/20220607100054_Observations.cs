using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ePTW.Migrations
{
    public partial class Observations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Observation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ObsCatg = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    ObsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UnitCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    DeptCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    StatCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ObsDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CorrectiveAction = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PersonResponsible = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ComplianceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ObsStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ObservedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReleaseBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ReleaseStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReleaseRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StatusUpdateReleaseBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StatusUpdateReleaseStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    StatusUpdateReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusUpdateReleaseRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observation_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observation_Department_CompCode_WrkGrp_UnitCode_DeptCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode, x.DeptCode },
                        principalTable: "Department",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observation_Station_CompCode_WrkGrp_UnitCode_DeptCode_StatCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode, x.DeptCode, x.StatCode },
                        principalTable: "Station",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode", "StatCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observation_Unit_CompCode_WrkGrp_UnitCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode },
                        principalTable: "Unit",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observation_Workgroup_CompCode_WrkGrp",
                        columns: x => new { x.CompCode, x.WrkGrp },
                        principalTable: "Workgroup",
                        principalColumns: new[] { "CompCode", "WrkGrp" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObsPhotos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ObsStatus = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermitImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsPhotos", x => new { x.Id, x.ObsStatus });
                    table.ForeignKey(
                        name: "FK_ObsPhotos_Observation_Id",
                        column: x => x.Id,
                        principalTable: "Observation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Observation_CompCode_WrkGrp_UnitCode_DeptCode_StatCode",
                table: "Observation",
                columns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode", "StatCode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObsPhotos");

            migrationBuilder.DropTable(
                name: "Observation");
        }
    }
}
