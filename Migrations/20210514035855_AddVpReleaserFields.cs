using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ePTW.Migrations
{
    public partial class AddVpReleaserFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SafetyRejectionRemarks",
                table: "Permit",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VpEmpId",
                table: "Permit",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VpRelDate",
                table: "Permit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VpRelStatus",
                table: "Permit",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VpRemarks",
                table: "Permit",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SafetyRejectionRemarks",
                table: "Permit");

            migrationBuilder.DropColumn(
                name: "VpEmpId",
                table: "Permit");

            migrationBuilder.DropColumn(
                name: "VpRelDate",
                table: "Permit");

            migrationBuilder.DropColumn(
                name: "VpRelStatus",
                table: "Permit");

            migrationBuilder.DropColumn(
                name: "VpRemarks",
                table: "Permit");
        }
    }
}
