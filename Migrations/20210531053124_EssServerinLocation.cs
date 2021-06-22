using Microsoft.EntityFrameworkCore.Migrations;

namespace ePTW.Migrations
{
    public partial class EssServerinLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RemoteEssConnection",
                table: "Locations",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemoteEssConnection",
                table: "Locations");
        }
    }
}
