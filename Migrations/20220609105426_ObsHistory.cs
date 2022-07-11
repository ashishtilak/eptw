using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ePTW.Migrations
{
    public partial class ObsHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ObsHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObsId = table.Column<long>(type: "bigint", nullable: false),
                    ObsCatg = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    ObsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonResponsible = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ComplianceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ObsStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ObservedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StatusUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReleaseBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ReleaseStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusUpdateReleaseBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StatusUpdateReleaseStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    StatusUpdateReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObsHistory");
        }
    }
}
