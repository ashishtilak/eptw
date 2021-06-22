using Microsoft.EntityFrameworkCore.Migrations;

namespace ePTW.Migrations
{
    public partial class AddedLottoKeyInElecIsolation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LottoKey",
                table: "PermitElecIsolation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LottoKey",
                table: "PermitElecIsolation");
        }
    }
}
