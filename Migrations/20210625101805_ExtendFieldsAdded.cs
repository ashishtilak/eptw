using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ePTW.Migrations
{
    public partial class ExtendFieldsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ExtendFlag",
                table: "Permit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "OriginalToDate",
                table: "Permit",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtendFlag",
                table: "Permit");

            migrationBuilder.DropColumn(
                name: "OriginalToDate",
                table: "Permit");
        }
    }
}
