using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ePTW.Migrations
{
    public partial class InitialConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CompName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompCode);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Location = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    RemoteConnection = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AttendanceServerApi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SmtpClient = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PortalAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Location);
                });

            migrationBuilder.CreateTable(
                name: "PermitState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    StateDesc = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermitType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermitTypeDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonEntryRequired = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseStatus",
                columns: table => new
                {
                    ReleaseStatusCode = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ReleaseStatusDesc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseStatus", x => x.ReleaseStatusCode);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseStrategyLevels",
                columns: table => new
                {
                    ReleaseStrategy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ReleaseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseStrategyLevels", x => x.ReleaseStrategy);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "RoleAuth",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAuth", x => new { x.RoleId, x.MenuId });
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    EmpUnqId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UpdateUserId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RoleId, x.EmpUnqId });
                });

            migrationBuilder.CreateTable(
                name: "SafetyDeptStat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UnitCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    DeptCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    StatCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyDeptStat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workgroup",
                columns: table => new
                {
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    WrkGrpDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    AddDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddUser = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workgroup", x => new { x.CompCode, x.WrkGrp });
                    table.ForeignKey(
                        name: "FK_Workgroup_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermitReleaseConf",
                columns: table => new
                {
                    PermitTypeId = table.Column<int>(type: "int", nullable: false),
                    DeptInchargeRelReq = table.Column<bool>(type: "bit", nullable: false),
                    AreaInchargeRelReq = table.Column<bool>(type: "bit", nullable: false),
                    ElecTechRelReq = table.Column<bool>(type: "bit", nullable: false),
                    ElecInchargeRelReq = table.Column<bool>(type: "bit", nullable: false),
                    DeptInchargeCloseReq = table.Column<bool>(type: "bit", nullable: false),
                    AreaInchargeCloseReq = table.Column<bool>(type: "bit", nullable: false),
                    ElecTechCloseReq = table.Column<bool>(type: "bit", nullable: false),
                    ElecInchargeCloseReq = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitReleaseConf", x => x.PermitTypeId);
                    table.ForeignKey(
                        name: "FK_PermitReleaseConf_PermitType_PermitTypeId",
                        column: x => x.PermitTypeId,
                        principalTable: "PermitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CatCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CatName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => new { x.CompCode, x.WrkGrp, x.CatCode });
                    table.ForeignKey(
                        name: "FK_Category_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Workgroup_CompCode_WrkGrp",
                        columns: x => new { x.CompCode, x.WrkGrp },
                        principalTable: "Workgroup",
                        principalColumns: new[] { "CompCode", "WrkGrp" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UnitCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    DeptCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    DeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => new { x.CompCode, x.WrkGrp, x.UnitCode, x.DeptCode });
                    table.ForeignKey(
                        name: "FK_Department_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Department_Workgroup_CompCode_WrkGrp",
                        columns: x => new { x.CompCode, x.WrkGrp },
                        principalTable: "Workgroup",
                        principalColumns: new[] { "CompCode", "WrkGrp" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Designation",
                columns: table => new
                {
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DesgCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    DesgName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => new { x.CompCode, x.WrkGrp, x.DesgCode });
                    table.ForeignKey(
                        name: "FK_Designation_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Designation_Workgroup_CompCode_WrkGrp",
                        columns: x => new { x.CompCode, x.WrkGrp },
                        principalTable: "Workgroup",
                        principalColumns: new[] { "CompCode", "WrkGrp" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpType",
                columns: table => new
                {
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EmpTypeCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    EmpTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpType", x => new { x.CompCode, x.WrkGrp, x.EmpTypeCode });
                    table.ForeignKey(
                        name: "FK_EmpType_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpType_Workgroup_CompCode_WrkGrp",
                        columns: x => new { x.CompCode, x.WrkGrp },
                        principalTable: "Workgroup",
                        principalColumns: new[] { "CompCode", "WrkGrp" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GradeCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => new { x.CompCode, x.WrkGrp, x.GradeCode });
                    table.ForeignKey(
                        name: "FK_Grade_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grade_Workgroup_CompCode_WrkGrp",
                        columns: x => new { x.CompCode, x.WrkGrp },
                        principalTable: "Workgroup",
                        principalColumns: new[] { "CompCode", "WrkGrp" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UnitCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => new { x.CompCode, x.WrkGrp, x.UnitCode });
                    table.ForeignKey(
                        name: "FK_Unit_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unit_Workgroup_CompCode_WrkGrp",
                        columns: x => new { x.CompCode, x.WrkGrp },
                        principalTable: "Workgroup",
                        principalColumns: new[] { "CompCode", "WrkGrp" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UnitCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    DeptCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    StatCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    StatName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => new { x.CompCode, x.WrkGrp, x.UnitCode, x.DeptCode, x.StatCode });
                    table.ForeignKey(
                        name: "FK_Station_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Station_Department_CompCode_WrkGrp_UnitCode_DeptCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode, x.DeptCode },
                        principalTable: "Department",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Station_Unit_CompCode_WrkGrp_UnitCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode },
                        principalTable: "Unit",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Station_Workgroup_CompCode_WrkGrp",
                        columns: x => new { x.CompCode, x.WrkGrp },
                        principalTable: "Workgroup",
                        principalColumns: new[] { "CompCode", "WrkGrp" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmpUnqId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EmpTypeCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    UnitCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    DeptCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    StatCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    CatCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    DesgCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    GradeCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    EmpName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Pass = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    SapId = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    CompanyAcc = table.Column<bool>(type: "bit", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmpUnqId);
                    table.ForeignKey(
                        name: "FK_Employee_Category_CompCode_WrkGrp_CatCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.CatCode },
                        principalTable: "Category",
                        principalColumns: new[] { "CompCode", "WrkGrp", "CatCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Department_CompCode_WrkGrp_UnitCode_DeptCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode, x.DeptCode },
                        principalTable: "Department",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Designation_CompCode_WrkGrp_DesgCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.DesgCode },
                        principalTable: "Designation",
                        principalColumns: new[] { "CompCode", "WrkGrp", "DesgCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_EmpType_CompCode_WrkGrp_EmpTypeCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.EmpTypeCode },
                        principalTable: "EmpType",
                        principalColumns: new[] { "CompCode", "WrkGrp", "EmpTypeCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Grade_CompCode_WrkGrp_GradeCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.GradeCode },
                        principalTable: "Grade",
                        principalColumns: new[] { "CompCode", "WrkGrp", "GradeCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Station_CompCode_WrkGrp_UnitCode_DeptCode_StatCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode, x.DeptCode, x.StatCode },
                        principalTable: "Station",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode", "StatCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Unit_CompCode_WrkGrp_UnitCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode },
                        principalTable: "Unit",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Workgroup_CompCode_WrkGrp",
                        columns: x => new { x.CompCode, x.WrkGrp },
                        principalTable: "Workgroup",
                        principalColumns: new[] { "CompCode", "WrkGrp" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PermitTypeId = table.Column<int>(type: "int", nullable: false),
                    PermitNo = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    FromDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    WrkGrp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UnitCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    DeptCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    StatCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    WorkLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JobDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedByEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedByEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LastChangeOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SelfRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SelfRelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeptInchargeRelReq = table.Column<bool>(type: "bit", nullable: false),
                    AreaInchargeRelReq = table.Column<bool>(type: "bit", nullable: false),
                    ElecTechRelReq = table.Column<bool>(type: "bit", nullable: false),
                    ElecInchargeRelReq = table.Column<bool>(type: "bit", nullable: false),
                    DeptInchargeCloseReq = table.Column<bool>(type: "bit", nullable: false),
                    AreaInchargeCloseReq = table.Column<bool>(type: "bit", nullable: false),
                    ElecTechCloseReq = table.Column<bool>(type: "bit", nullable: false),
                    ElecInchargeCloseReq = table.Column<bool>(type: "bit", nullable: false),
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
                    FullyReleasedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SelfCloseEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SelfCloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SelfCloseRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SelfCloseRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CloseDeptInchEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DeptInchCloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeptInchCloseRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    DeptInchCloseRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CloseAreaInchEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AreaInchCloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AreaInchCloseRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    AreaInchCloseRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CloseElecTechEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ElecTechCloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ElecTechCloseRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ElecTechCloseRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CloseElecInchEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ElecInchCloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ElecInchCloseRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ElecInchCloseRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CloseSafetyInchEmpId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SafetyInchCloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SafetyInchCloseRelStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SafetyInchCloseRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClosedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AllowUserEdit = table.Column<bool>(type: "bit", nullable: false),
                    AllowSafetyEdit = table.Column<bool>(type: "bit", nullable: false),
                    AllowClose = table.Column<bool>(type: "bit", nullable: false),
                    AllowFinalRelease = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permit_Company_CompCode",
                        column: x => x.CompCode,
                        principalTable: "Company",
                        principalColumn: "CompCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permit_Department_CompCode_WrkGrp_UnitCode_DeptCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode, x.DeptCode },
                        principalTable: "Department",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permit_PermitState_CurrentState",
                        column: x => x.CurrentState,
                        principalTable: "PermitState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permit_PermitType_PermitTypeId",
                        column: x => x.PermitTypeId,
                        principalTable: "PermitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permit_Station_CompCode_WrkGrp_UnitCode_DeptCode_StatCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode, x.DeptCode, x.StatCode },
                        principalTable: "Station",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode", "StatCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permit_Unit_CompCode_WrkGrp_UnitCode",
                        columns: x => new { x.CompCode, x.WrkGrp, x.UnitCode },
                        principalTable: "Unit",
                        principalColumns: new[] { "CompCode", "WrkGrp", "UnitCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permit_Workgroup_CompCode_WrkGrp",
                        columns: x => new { x.CompCode, x.WrkGrp },
                        principalTable: "Workgroup",
                        principalColumns: new[] { "CompCode", "WrkGrp" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseAuth",
                columns: table => new
                {
                    ReleaseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmpUnqId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseAuth", x => new { x.ReleaseCode, x.EmpUnqId });
                    table.ForeignKey(
                        name: "FK_ReleaseAuth_Employee_EmpUnqId",
                        column: x => x.EmpUnqId,
                        principalTable: "Employee",
                        principalColumn: "EmpUnqId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VpReleasers",
                columns: table => new
                {
                    EmpUnqId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpReleasers", x => x.EmpUnqId);
                    table.ForeignKey(
                        name: "FK_VpReleasers_Employee_EmpUnqId",
                        column: x => x.EmpUnqId,
                        principalTable: "Employee",
                        principalColumn: "EmpUnqId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermitColdWork",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PermitTypeId = table.Column<int>(type: "int", nullable: false),
                    PermitNo = table.Column<int>(type: "int", nullable: false),
                    Contractor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Isolationdone = table.Column<int>(type: "int", nullable: false),
                    SafetyTags = table.Column<int>(type: "int", nullable: false),
                    PipeConnectionBlanked = table.Column<int>(type: "int", nullable: false),
                    PipeConnectionDetail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WorkAreaCleaned = table.Column<int>(type: "int", nullable: false),
                    PossibilityOfFlammable = table.Column<int>(type: "int", nullable: false),
                    PossibilityOfGasPressure = table.Column<int>(type: "int", nullable: false),
                    ElectricalIsolationDone = table.Column<int>(type: "int", nullable: false),
                    AreaBarricated = table.Column<int>(type: "int", nullable: false),
                    OtherCheck = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Helmet = table.Column<int>(type: "int", nullable: false),
                    Shoes = table.Column<int>(type: "int", nullable: false),
                    Belt = table.Column<int>(type: "int", nullable: false),
                    Goggles = table.Column<int>(type: "int", nullable: false),
                    HandGloves = table.Column<int>(type: "int", nullable: false),
                    EarPlug = table.Column<int>(type: "int", nullable: false),
                    NoseMask = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitColdWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitColdWork_Permit_Id",
                        column: x => x.Id,
                        principalTable: "Permit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermitColdWork_PermitType_PermitTypeId",
                        column: x => x.PermitTypeId,
                        principalTable: "PermitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermitCrossRef",
                columns: table => new
                {
                    PermitId = table.Column<long>(type: "bigint", nullable: false),
                    CrossRefPermitId = table.Column<long>(type: "bigint", nullable: false),
                    PermitTypeId = table.Column<int>(type: "int", nullable: false),
                    CrossRefPermitTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitCrossRef", x => new { x.PermitId, x.CrossRefPermitId });
                    table.ForeignKey(
                        name: "FK_PermitCrossRef_Permit_PermitId",
                        column: x => x.PermitId,
                        principalTable: "Permit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermitCrossRef_PermitType_CrossRefPermitTypeId",
                        column: x => x.CrossRefPermitTypeId,
                        principalTable: "PermitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermitCrossRef_PermitType_PermitTypeId",
                        column: x => x.PermitTypeId,
                        principalTable: "PermitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermitElecIsolation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PermitTypeId = table.Column<int>(type: "int", nullable: false),
                    PermitNo = table.Column<int>(type: "int", nullable: false),
                    ReqIsolationEquipment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReqPanel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReqWork = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PanelFeederNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PanelNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Helmet = table.Column<int>(type: "int", nullable: false),
                    Shoes = table.Column<int>(type: "int", nullable: false),
                    Belt = table.Column<int>(type: "int", nullable: false),
                    Goggles = table.Column<int>(type: "int", nullable: false),
                    HandGloves = table.Column<int>(type: "int", nullable: false),
                    EarPlug = table.Column<int>(type: "int", nullable: false),
                    NoseMask = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitElecIsolation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitElecIsolation_Permit_Id",
                        column: x => x.Id,
                        principalTable: "Permit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermitElecIsolation_PermitType_PermitTypeId",
                        column: x => x.PermitTypeId,
                        principalTable: "PermitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermitHeight",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PermitTypeId = table.Column<int>(type: "int", nullable: false),
                    PermitNo = table.Column<int>(type: "int", nullable: false),
                    Contractor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Scaffolding = table.Column<int>(type: "int", nullable: false),
                    SafetyBelt = table.Column<int>(type: "int", nullable: false),
                    SafetyLadder = table.Column<int>(type: "int", nullable: false),
                    FallArrestor = table.Column<int>(type: "int", nullable: false),
                    LifeLine = table.Column<int>(type: "int", nullable: false),
                    InstructionsGiven = table.Column<int>(type: "int", nullable: false),
                    ApproxHeight = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LiftingTools = table.Column<int>(type: "int", nullable: false),
                    SafetyNet = table.Column<int>(type: "int", nullable: false),
                    AreaBarricated = table.Column<int>(type: "int", nullable: false),
                    Helmet = table.Column<int>(type: "int", nullable: false),
                    Shoes = table.Column<int>(type: "int", nullable: false),
                    Belt = table.Column<int>(type: "int", nullable: false),
                    Goggles = table.Column<int>(type: "int", nullable: false),
                    HandGloves = table.Column<int>(type: "int", nullable: false),
                    EarPlug = table.Column<int>(type: "int", nullable: false),
                    NoseMask = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitHeight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitHeight_Permit_Id",
                        column: x => x.Id,
                        principalTable: "Permit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermitHeight_PermitType_PermitTypeId",
                        column: x => x.PermitTypeId,
                        principalTable: "PermitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermitHotWork",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PermitTypeId = table.Column<int>(type: "int", nullable: false),
                    PermitNo = table.Column<int>(type: "int", nullable: false),
                    Contractor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsolationDone = table.Column<int>(type: "int", nullable: false),
                    FreeFromOrgVapour = table.Column<int>(type: "int", nullable: false),
                    VesselFlushed = table.Column<int>(type: "int", nullable: false),
                    VesselPurged = table.Column<int>(type: "int", nullable: false),
                    NearEquipmentCovered = table.Column<int>(type: "int", nullable: false),
                    ElectricalIsolationReq = table.Column<int>(type: "int", nullable: false),
                    FlushCleanContainer = table.Column<int>(type: "int", nullable: false),
                    SurroundingAreaCleaned = table.Column<int>(type: "int", nullable: false),
                    SparkArrangement = table.Column<int>(type: "int", nullable: false),
                    EquipmentInGoodCondition = table.Column<int>(type: "int", nullable: false),
                    LineBlinded = table.Column<int>(type: "int", nullable: false),
                    ManHolesOpen = table.Column<int>(type: "int", nullable: false),
                    Helmet = table.Column<int>(type: "int", nullable: false),
                    Shoes = table.Column<int>(type: "int", nullable: false),
                    Belt = table.Column<int>(type: "int", nullable: false),
                    Goggles = table.Column<int>(type: "int", nullable: false),
                    HandGloves = table.Column<int>(type: "int", nullable: false),
                    EarPlug = table.Column<int>(type: "int", nullable: false),
                    NoseMask = table.Column<int>(type: "int", nullable: false),
                    ToolsTackles = table.Column<int>(type: "int", nullable: false),
                    ExtinguisherAvailable = table.Column<int>(type: "int", nullable: false),
                    UseOfDrum = table.Column<int>(type: "int", nullable: false),
                    ContinuousSupervision = table.Column<int>(type: "int", nullable: false),
                    SewersCovered = table.Column<int>(type: "int", nullable: false),
                    LelLevel = table.Column<double>(type: "float", nullable: true),
                    LelLevelChecked = table.Column<int>(type: "int", nullable: false),
                    CoLevel = table.Column<double>(type: "float", nullable: true),
                    CoLevelChecked = table.Column<int>(type: "int", nullable: false),
                    WatcherName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EquipmentIsolated = table.Column<int>(type: "int", nullable: false),
                    EquipmentEarthen = table.Column<int>(type: "int", nullable: false),
                    WeldMachineConnection = table.Column<int>(type: "int", nullable: false),
                    PersonTrained = table.Column<int>(type: "int", nullable: false),
                    PpeProvided = table.Column<int>(type: "int", nullable: false),
                    ToolsProvided = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitHotWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitHotWork_Permit_Id",
                        column: x => x.Id,
                        principalTable: "Permit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermitHotWork_PermitType_PermitTypeId",
                        column: x => x.PermitTypeId,
                        principalTable: "PermitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermitPerson",
                columns: table => new
                {
                    PermitId = table.Column<long>(type: "bigint", nullable: false),
                    EmpUnqId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitPerson", x => new { x.PermitId, x.EmpUnqId });
                    table.ForeignKey(
                        name: "FK_PermitPerson_Permit_PermitId",
                        column: x => x.PermitId,
                        principalTable: "Permit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermitPhotos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Sr = table.Column<int>(type: "int", nullable: false),
                    PermitImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitPhotos", x => new { x.Id, x.Sr });
                    table.ForeignKey(
                        name: "FK_PermitPhotos_Permit_Id",
                        column: x => x.Id,
                        principalTable: "Permit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermitVesselEntry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PermitTypeId = table.Column<int>(type: "int", nullable: false),
                    PermitNo = table.Column<int>(type: "int", nullable: false),
                    Contractor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VesselFreeFromToxicMaterial = table.Column<int>(type: "int", nullable: false),
                    LinesBlinded = table.Column<int>(type: "int", nullable: false),
                    ElectricalIsolationReq = table.Column<int>(type: "int", nullable: false),
                    HandLampProvided = table.Column<int>(type: "int", nullable: false),
                    OxygenLevel = table.Column<double>(type: "float", nullable: true),
                    OxygenLevelCheck = table.Column<int>(type: "int", nullable: false),
                    WorkAreaSafe = table.Column<int>(type: "int", nullable: false),
                    ExplosiveLevelTestReq = table.Column<int>(type: "int", nullable: false),
                    Helmet = table.Column<int>(type: "int", nullable: false),
                    Shoes = table.Column<int>(type: "int", nullable: false),
                    Belt = table.Column<int>(type: "int", nullable: false),
                    Goggles = table.Column<int>(type: "int", nullable: false),
                    HandGloves = table.Column<int>(type: "int", nullable: false),
                    EarPlug = table.Column<int>(type: "int", nullable: false),
                    NoseMask = table.Column<int>(type: "int", nullable: false),
                    Ladder = table.Column<int>(type: "int", nullable: false),
                    RespiratoryProtectionReq = table.Column<int>(type: "int", nullable: false),
                    LockOutArrangement = table.Column<int>(type: "int", nullable: false),
                    ExplosiveLevel = table.Column<double>(type: "float", nullable: true),
                    WatcherName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EquipmentIsolated = table.Column<int>(type: "int", nullable: false),
                    EquipmentEarthen = table.Column<int>(type: "int", nullable: false),
                    HandLampExtension = table.Column<int>(type: "int", nullable: false),
                    PersonTrained = table.Column<int>(type: "int", nullable: false),
                    PpeProvided = table.Column<int>(type: "int", nullable: false),
                    ToolsProvided = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitVesselEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermitVesselEntry_Permit_Id",
                        column: x => x.Id,
                        principalTable: "Permit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermitVesselEntry_PermitType_PermitTypeId",
                        column: x => x.PermitTypeId,
                        principalTable: "PermitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PermitState",
                columns: new[] { "Id", "StateDesc" },
                values: new object[,]
                {
                    { "N", "Created" },
                    { "P", "Partial Released" },
                    { "F", "Fully Released" },
                    { "S", "Closure Started" },
                    { "R", "Partially Closed" },
                    { "C", "Closed" },
                    { "D", "Deleted" },
                    { "X", "Force Closed" }
                });

            migrationBuilder.InsertData(
                table: "PermitType",
                columns: new[] { "Id", "Active", "DocNo", "EffectiveDate", "PermitTypeDesc", "PersonEntryRequired", "Version" },
                values: new object[,]
                {
                    { 5, true, "JSL/IPU/HSE/FR-08", new DateTime(2016, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vessel Entry Work Permit", false, "1.1" },
                    { 4, true, "JSL/IPU/HSE/FR-04", new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cold Work Permit", false, "1.2" },
                    { 1, true, "JSL/IPU/HSE/FR-06", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Height Permit", false, "3.0" },
                    { 2, true, "JSL/IPU/HSE/FR-05", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hot Work Permit", false, "3.0" },
                    { 3, true, "JSL/IPU/HSE/FR-07", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electrical Isolation Permit", false, "2.0" }
                });

            migrationBuilder.InsertData(
                table: "ReleaseStatus",
                columns: new[] { "ReleaseStatusCode", "ReleaseStatusDesc" },
                values: new object[,]
                {
                    { "R", "Release rejected" },
                    { "N", "Not released" },
                    { "I", "In release" },
                    { "F", "Fully released" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompCode_WrkGrp_CatCode",
                table: "Employee",
                columns: new[] { "CompCode", "WrkGrp", "CatCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompCode_WrkGrp_DesgCode",
                table: "Employee",
                columns: new[] { "CompCode", "WrkGrp", "DesgCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompCode_WrkGrp_EmpTypeCode",
                table: "Employee",
                columns: new[] { "CompCode", "WrkGrp", "EmpTypeCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompCode_WrkGrp_GradeCode",
                table: "Employee",
                columns: new[] { "CompCode", "WrkGrp", "GradeCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompCode_WrkGrp_UnitCode_DeptCode_StatCode",
                table: "Employee",
                columns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode", "StatCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Permit_CompCode_WrkGrp_UnitCode_DeptCode_StatCode",
                table: "Permit",
                columns: new[] { "CompCode", "WrkGrp", "UnitCode", "DeptCode", "StatCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Permit_CurrentState",
                table: "Permit",
                column: "CurrentState");

            migrationBuilder.CreateIndex(
                name: "IX_Permit_PermitTypeId",
                table: "Permit",
                column: "PermitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitColdWork_PermitTypeId",
                table: "PermitColdWork",
                column: "PermitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitCrossRef_CrossRefPermitTypeId",
                table: "PermitCrossRef",
                column: "CrossRefPermitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitCrossRef_PermitTypeId",
                table: "PermitCrossRef",
                column: "PermitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitElecIsolation_PermitTypeId",
                table: "PermitElecIsolation",
                column: "PermitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitHeight_PermitTypeId",
                table: "PermitHeight",
                column: "PermitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitHotWork_PermitTypeId",
                table: "PermitHotWork",
                column: "PermitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PermitVesselEntry_PermitTypeId",
                table: "PermitVesselEntry",
                column: "PermitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseAuth_EmpUnqId",
                table: "ReleaseAuth",
                column: "EmpUnqId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "PermitColdWork");

            migrationBuilder.DropTable(
                name: "PermitCrossRef");

            migrationBuilder.DropTable(
                name: "PermitElecIsolation");

            migrationBuilder.DropTable(
                name: "PermitHeight");

            migrationBuilder.DropTable(
                name: "PermitHotWork");

            migrationBuilder.DropTable(
                name: "PermitPerson");

            migrationBuilder.DropTable(
                name: "PermitPhotos");

            migrationBuilder.DropTable(
                name: "PermitReleaseConf");

            migrationBuilder.DropTable(
                name: "PermitVesselEntry");

            migrationBuilder.DropTable(
                name: "ReleaseAuth");

            migrationBuilder.DropTable(
                name: "ReleaseStatus");

            migrationBuilder.DropTable(
                name: "ReleaseStrategyLevels");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RoleAuth");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "SafetyDeptStat");

            migrationBuilder.DropTable(
                name: "VpReleasers");

            migrationBuilder.DropTable(
                name: "Permit");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "PermitState");

            migrationBuilder.DropTable(
                name: "PermitType");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Designation");

            migrationBuilder.DropTable(
                name: "EmpType");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Station");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Workgroup");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
