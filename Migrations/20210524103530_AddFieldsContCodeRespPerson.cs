using Microsoft.EntityFrameworkCore.Migrations;

namespace ePTW.Migrations
{
    public partial class AddFieldsContCodeRespPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsiblePerson",
                table: "Permit",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContCode",
                table: "Employee",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsiblePerson",
                table: "Permit");

            migrationBuilder.DropColumn(
                name: "ContCode",
                table: "Employee");
        }
    }
}
