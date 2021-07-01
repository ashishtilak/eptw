using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ePTW.Migrations
{
    public partial class PermitHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermitHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermitId = table.Column<long>(type: "bigint", nullable: false),
                    FromDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JobDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedByEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedByEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LastChangeOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SelfRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SelfRelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResponsiblePerson = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DeptInchEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DeptInchRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    DeptInchRelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HodRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AreaInchargeEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AreaInchRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    AreaInchRelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AreaInchRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ElecTechEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ElecTechRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ElecTechRelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ElecTechRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ElecInchargeEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ElecInchRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ElecInchRelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ElecInchRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SafetyInchargeEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SafetyInchargeRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SafetyInchRelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SafetyRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SafetyRejectionRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FullyReleasedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitHistory_Permit_PermitId",
                        column: x => x.PermitId,
                        principalTable: "Permit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermitHistory_PermitId",
                table: "PermitHistory",
                column: "PermitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermitHistory");
        }
    }
}
