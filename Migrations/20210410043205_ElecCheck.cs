using Microsoft.EntityFrameworkCore.Migrations;

namespace ePTW.Migrations
{
    public partial class ElecCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElecReleasers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Releaser = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UnitCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    DeptCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    StatCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    CatCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    GradeCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElecReleasers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElecReleasers_Category_CompCode_WrkGrp_CatCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.CatCode },
                        principalTable: "Category",
                        principalColumns: new[] { "CompCode", "WrkGrp", "CatCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElecReleasers_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElecReleasers_Department_CompCode_WrkGrp_UnitCode_DeptCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode, x.DeptCode },
                        principalTable: "Department",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElecReleasers_Grade_CompCode_WrkGrp_GradeCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.GradeCode },
                        principalTable: "Grade",
                        principalColumns: new[] { "CompCode", "WrkGrp", "GradeCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElecReleasers_Station_CompCode_WrkGrp_UnitCode_DeptCode_StatCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode, x.DeptCode, x.StatCode },
                        principalTable: "Station",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode", "StatCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElecReleasers_Unit_CompCode_WrkGrp_UnitCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode },
                        principalTable: "Unit",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElecReleasers_Workgroup_CompCode_WrkGrp",
                        columns: x => new { x.CompCode, x.WrkGrp },
                        principalTable: "Workgroup",
                        principalColumns: new[] { "CompCode", "WrkGrp" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElecReleasers_CompCode_WrkGrp_CatCode",
                table: "ElecReleasers",
                columns: new[] { "CompCode", "WrkGrp", "CatCode" });

            migrationBuilder.CreateIndex(
                name: "IX_ElecReleasers_CompCode_WrkGrp_GradeCode",
                table: "ElecReleasers",
                columns: new[] { "CompCode", "WrkGrp", "GradeCode" });

            migrationBuilder.CreateIndex(
                name: "IX_ElecReleasers_CompCode_WrkGrp_UnitCode_DeptCode_StatCode",
                table: "ElecReleasers",
                columns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode", "StatCode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElecReleasers");
        }
    }
}
