using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ePTW.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ePTW.Helper
{
    public class Helper
    {
        // need static DbContextOptions for using appdbcontext
        private static readonly DbContextOptions<AppDbContext> Options;

        // static helper to load before using static methods
        static Helper()
        {
            Options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(Startup.ConnectionString)
                .Options;
        }

        // get the remote server connection string from database
        public static string GetRemoteServer(string location)
        {
            var context = new AppDbContext(Options);
            Locations loc = context.Locations.FirstOrDefault(c => c.Location == location);
            return loc != null ? loc.RemoteConnection : "";
        }


        // get the remote ESS server connection string from database
        public static string GetRemoteEssServer(string location)
        {
            var context = new AppDbContext(Options);
            Locations loc = context.Locations.FirstOrDefault(c => c.Location == location);
            return loc != null ? loc.RemoteEssConnection : "";
        }

        // sync controller will call this method
        public static void Sync(string location)
        {
            string strRemoteServer = GetRemoteServer(location);
            string strRemoteEssServer = GetRemoteEssServer(location);

            SyncCompany(strRemoteServer, location);
            SyncWrkGrp(strRemoteServer, location);
            SyncUnits(strRemoteServer, location);
            SyncDept(strRemoteServer, location);
            SyncStat(strRemoteServer, location);
            SyncCatg(strRemoteServer, location);
            SyncDesg(strRemoteServer, location);
            SyncGrade(strRemoteServer, location);
            SyncEmpType(strRemoteServer, location);
            SyncEmp(strRemoteServer, location);
            SyncEmpMail(strRemoteEssServer, location);
        }

        // sync companies
        public static void SyncCompany(string strRemoteServer, string location)
        {
            try
            {
                using var cnRemote = new SqlConnection(strRemoteServer);
                cnRemote.Open();
                //first get all masters:

                SqlConnection cnLocal;

                using (cnLocal = new SqlConnection(Startup.ConnectionString))
                {
                    // create a temp table in db
                    string sql = "select top 0 * into #tmpCompanies from Company";
                    cnLocal.Open();
                    var cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();

                    //get data from attendance server
                    sql = "select CompCode, CompName, " +
                          "'" + location + "' as location " +
                          " from MastComp";

                    var da = new SqlDataAdapter(sql, cnRemote);
                    var dt = new DataTable();
                    da.Fill(dt);

                    // use bulk copy to copy data faster to temp table
                    using var bulk = new SqlBulkCopy(cnLocal) {DestinationTableName = "#tmpCompanies"};

                    bulk.ColumnMappings.Add("CompCode", "CompCode");
                    bulk.ColumnMappings.Add("CompName", "CompName");
                    bulk.ColumnMappings.Add("Location", "Location");

                    bulk.WriteToServer(dt);


                    // now use merge command to merge records from temp table to main table
                    sql = "merge into Company as target " +
                          "using #tmpCompanies as Source " +
                          "on " +
                          "Target.CompCode = Source.CompCode and " +
                          "Target.Location = Source.Location " +
                          "when matched then " +
                          "update set Target.CompName = Source.CompName " +
                          "when not matched then " +
                          "insert (compcode, compname, location) values (source.compcode, source.compname," +
                          "'" + location + "' ); ";

                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();

                    sql = "drop table #tmpCompanies";
                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        } // end of synccompany

        public static void SyncWrkGrp(string strRemoteServer, string location)
        {
            try
            {
                using var cnRemote = new SqlConnection(strRemoteServer);
                cnRemote.Open();
                SqlConnection cnLocal;

                using (cnLocal = new SqlConnection(Startup.ConnectionString))
                {
                    string sql = "select top 0 * into #tmpWrkGrp from WorkGroup";
                    cnLocal.Open();
                    var cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();

                    //get data from attendance server
                    sql = "select CompCode, WrkGrp, WrkGrpDesc, " +
                          "'" + location + "' as location " +
                          " from MastWorkGrp";
                    var da = new SqlDataAdapter(sql, cnRemote);
                    var dt = new DataTable();
                    da.Fill(dt);

                    using (var bulk = new SqlBulkCopy(cnLocal))
                    {
                        bulk.DestinationTableName = "#tmpWrkGrp";

                        bulk.ColumnMappings.Add("CompCode", "CompCode");
                        bulk.ColumnMappings.Add("WrkGrp", "WrkGrp");
                        bulk.ColumnMappings.Add("WrkGrpDesc", "WrkGrpDesc");
                        bulk.ColumnMappings.Add("Location", "Location");

                        bulk.WriteToServer(dt);
                    }

                    //bulk copy done, now use MERGE

                    sql = "merge into WorkGroup as target " +
                          "using #tmpWrkGrp as Source " +
                          "on " +
                          "Target.CompCode = Source.CompCode and " +
                          "Target.WrkGrp = Source.WrkGrp and " +
                          "Target.Location = Source.Location " +
                          "when matched then " +
                          "update set Target.WrkGrpDesc = Source.WrkGrpDesc " +
                          "when not matched then " +
                          "insert (compcode, wrkgrp, WrkGrpDesc, location) " +
                          "values (source.compcode, source.wrkgrp, source.wrkgrpdesc," +
                          "'" + location + "' ); ";

                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    sql = "drop table #tmpWrkGrp";
                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        } // end of syncWrkGrp

        public static void SyncUnits(string strRemoteServer, string location)
        {
            try
            {
                using var cnRemote = new SqlConnection(strRemoteServer);
                cnRemote.Open();
                //first get all masters:

                SqlConnection cnLocal;

                using (cnLocal = new SqlConnection(Startup.ConnectionString))
                {
                    string sql = "select top 0 * into #tmpUnits from Unit";
                    cnLocal.Open();
                    var cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    //get data from attendance server
                    sql = "select CompCode, WrkGrp, UnitCode, UnitName, " +
                          "'" + location + "' as location " +
                          " from MastUnit";
                    var da = new SqlDataAdapter(sql, cnRemote);
                    var dt = new DataTable();
                    da.Fill(dt);

                    using (var bulk = new SqlBulkCopy(cnLocal))
                    {
                        bulk.DestinationTableName = "#tmpUnits";

                        bulk.ColumnMappings.Add("CompCode", "CompCode");
                        bulk.ColumnMappings.Add("WrkGrp", "WrkGrp");
                        bulk.ColumnMappings.Add("UnitCode", "UnitCode");
                        bulk.ColumnMappings.Add("UnitName", "UnitName");
                        bulk.ColumnMappings.Add("Location", "Location");

                        bulk.WriteToServer(dt);
                    }

                    //bulk copy done, now use MERGE

                    sql = "merge into Unit as target " +
                          "using #tmpUnits as Source " +
                          "on " +
                          "Target.CompCode = Source.CompCode and " +
                          "Target.WrkGrp = Source.WrkGrp and " +
                          "Target.UnitCode = Source.UnitCode and " +
                          "Target.Location = Source.Location " +
                          "when matched then " +
                          "update set Target.UnitName = Source.UnitName " +
                          "when not matched then " +
                          "insert (compcode, wrkgrp, unitcode, unitname, location) " +
                          "values (source.compcode, source.wrkgrp, source.unitcode, source.unitname, " +
                          "'" + location + "' ); ";

                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    sql = "drop table #tmpUnits";
                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex);
            }
        } // end of Syncunits

        public static void SyncDept(string strRemoteServer, string location)
        {
            try
            {
                using var cnRemote = new SqlConnection(strRemoteServer);
                cnRemote.Open();
                //first get all masters:

                SqlConnection cnLocal;

                //create a temp table
                using (cnLocal = new SqlConnection(Startup.ConnectionString))
                {
                    string sql = "select top 0 * into #tmpDepts from Department";
                    cnLocal.Open();
                    var cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    //get data from attendance server
                    sql = "select CompCode, WrkGrp, UnitCode, DeptCode, DeptDesc, " +
                          "'" + location + "' as location " +
                          " from MastDept";
                    var da = new SqlDataAdapter(sql, cnRemote);
                    var dt = new DataTable();
                    da.Fill(dt);

                    using (var bulk = new SqlBulkCopy(cnLocal))
                    {
                        bulk.DestinationTableName = "#tmpDepts";

                        bulk.ColumnMappings.Add("CompCode", "CompCode");
                        bulk.ColumnMappings.Add("WrkGrp", "WrkGrp");
                        bulk.ColumnMappings.Add("UnitCode", "UnitCode");
                        bulk.ColumnMappings.Add("DeptCode", "DeptCode");
                        bulk.ColumnMappings.Add("DeptDesc", "DeptName");
                        bulk.ColumnMappings.Add("Location", "Location");

                        bulk.WriteToServer(dt);
                    }

                    //bulk copy done, now use MERGE

                    sql = "merge into Department as target " +
                          "using #tmpDepts as Source " +
                          "on " +
                          "Target.CompCode = Source.CompCode and " +
                          "Target.WrkGrp = Source.WrkGrp and " +
                          "Target.UnitCode = Source.UnitCode and " +
                          "Target.DeptCode = Source.DeptCode and " +
                          "Target.Location = Source.Location " +
                          "when matched then " +
                          "update set Target.DeptName = Source.Deptname " +
                          "when not matched then " +
                          "insert (compcode, wrkgrp, unitcode, deptcode, deptname, location ) " +
                          "values (source.compcode, source.wrkgrp, source.unitcode, source.deptcode, source.deptname, " +
                          "'" + location + "' ); ";

                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    sql = "drop table #tmpDepts";
                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        } // end of SyncDept

        public static void SyncStat(string strRemoteServer, string location)
        {
            try
            {
                using var cnRemote = new SqlConnection(strRemoteServer);
                cnRemote.Open();
                //first get all masters:

                SqlConnection cnLocal;

                //create a temp table
                using (cnLocal = new SqlConnection(Startup.ConnectionString))
                {
                    string sql = "select top 0 * into #tmpStat from Station";
                    cnLocal.Open();
                    var cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    //get data from attendance server
                    sql = "select CompCode, WrkGrp, UnitCode, DeptCode, StatCode, StatDesc, " +
                          "'" + location + "' as location " +
                          " from MastStat";
                    var da = new SqlDataAdapter(sql, cnRemote);
                    var dt = new DataTable();
                    da.Fill(dt);

                    using (var bulk = new SqlBulkCopy(cnLocal))
                    {
                        bulk.DestinationTableName = "#tmpStat";

                        bulk.ColumnMappings.Add("CompCode", "CompCode");
                        bulk.ColumnMappings.Add("WrkGrp", "WrkGrp");
                        bulk.ColumnMappings.Add("UnitCode", "UnitCode");
                        bulk.ColumnMappings.Add("DeptCode", "DeptCode");
                        bulk.ColumnMappings.Add("StatCode", "StatCode");
                        bulk.ColumnMappings.Add("StatDesc", "StatName");
                        bulk.ColumnMappings.Add("Location", "Location");

                        bulk.WriteToServer(dt);
                    }

                    //bulk copy done, now use MERGE

                    sql = "merge into Station as target " +
                          "using #tmpStat as Source " +
                          "on " +
                          "Target.CompCode = Source.CompCode and " +
                          "Target.WrkGrp = Source.WrkGrp and " +
                          "Target.UnitCode = Source.UnitCode and " +
                          "Target.DeptCode = Source.DeptCode and " +
                          "Target.StatCode = Source.StatCode and " +
                          "Target.Location = Source.Location " +
                          "when matched then " +
                          "update set Target.StatName = Source.StatName " +
                          "when not matched then " +
                          "insert (compcode, wrkgrp, unitcode, deptcode, statcode, statname, location ) " +
                          "values (source.compcode, source.wrkgrp, source.unitcode, source.deptcode, " +
                          "        source.statcode, source.statname, '" + location + "'  ); ";

                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    sql = "drop table #tmpStat";
                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        } // end of SyncStat

        public static void SyncCatg(string strRemoteServer, string location)
        {
            try
            {
                using var cnRemote = new SqlConnection(strRemoteServer);
                cnRemote.Open();
                //first get all masters:

                SqlConnection cnLocal;

                //create a temp table
                using (cnLocal = new SqlConnection(Startup.ConnectionString))
                {
                    string sql = "select top 0 * into #tmpCat from Category";
                    cnLocal.Open();
                    var cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    //get data from attendance server
                    sql = "select CompCode, WrkGrp, CatCode, CatDesc, " +
                          "'" + location + "' as location " +
                          " from MastCat";
                    var da = new SqlDataAdapter(sql, cnRemote);
                    var dt = new DataTable();
                    da.Fill(dt);

                    using (var bulk = new SqlBulkCopy(cnLocal))
                    {
                        bulk.DestinationTableName = "#tmpCat";

                        bulk.ColumnMappings.Add("CompCode", "CompCode");
                        bulk.ColumnMappings.Add("WrkGrp", "WrkGrp");
                        bulk.ColumnMappings.Add("CatCode", "CatCode");
                        bulk.ColumnMappings.Add("CatDesc", "CatName");
                        bulk.ColumnMappings.Add("Location", "Location");

                        bulk.WriteToServer(dt);
                    }

                    //bulk copy done, now use MERGE

                    sql = "merge into Category as target " +
                          "using #tmpCat as Source " +
                          "on " +
                          "Target.CompCode = Source.CompCode and " +
                          "Target.WrkGrp = Source.WrkGrp and " +
                          "Target.CatCode = Source.CatCode and " +
                          "Target.Location = Source.Location " +
                          "when matched then " +
                          "update set Target.CatName = Source.CatName " +
                          "when not matched then " +
                          "insert (compcode, wrkgrp, catcode, catname, location) " +
                          "values (source.compcode, source.wrkgrp, source.catcode, source.catname, " +
                          "'" + location + "' ); ";

                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    sql = "drop table #tmpCat";
                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        } // end of SyncCatg

        public static void SyncDesg(string strRemoteServer, string location)
        {
            try
            {
                using var cnRemote = new SqlConnection(strRemoteServer);
                cnRemote.Open();
                //first get all masters:

                SqlConnection cnLocal;

                //create a temp table
                using (cnLocal = new SqlConnection(Startup.ConnectionString))
                {
                    string sql = "select top 0 * into #tmpDesg from Designation";
                    cnLocal.Open();
                    var cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    //get data from attendance server
                    sql = "select CompCode, WrkGrp, DesgCode, DesgDesc, " +
                          "'" + location + "' as location " +
                          " from MastDesg";
                    var da = new SqlDataAdapter(sql, cnRemote);
                    var dt = new DataTable();
                    da.Fill(dt);

                    using (var bulk = new SqlBulkCopy(cnLocal))
                    {
                        bulk.DestinationTableName = "#tmpDesg";

                        bulk.ColumnMappings.Add("CompCode", "CompCode");
                        bulk.ColumnMappings.Add("WrkGrp", "WrkGrp");
                        bulk.ColumnMappings.Add("DesgCode", "DesgCode");
                        bulk.ColumnMappings.Add("DesgDesc", "DesgName");
                        bulk.ColumnMappings.Add("Location", "Location");

                        bulk.WriteToServer(dt);
                    }

                    //bulk copy done, now use MERGE

                    sql = "merge into Designation as target " +
                          "using #tmpDesg as Source " +
                          "on " +
                          "Target.CompCode = Source.CompCode and " +
                          "Target.WrkGrp = Source.WrkGrp and " +
                          "Target.DesgCode = Source.DesgCode and " +
                          "Target.Location = Source.Location " +
                          "when matched then " +
                          "update set Target.DesgName = Source.DesgName " +
                          "when not matched then " +
                          "insert (compcode, wrkgrp, desgcode, desgname, location) " +
                          "values (source.compcode, source.wrkgrp, source.desgcode, source.desgname, " +
                          "'" + location + "' ); ";

                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    sql = "drop table #tmpDesg";
                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        } // end of SyncDesg

        public static void SyncGrade(string strRemoteServer, string location)
        {
            try
            {
                using var cnRemote = new SqlConnection(strRemoteServer);
                cnRemote.Open();
                //first get all masters:

                SqlConnection cnLocal;

                //create a temp table
                using (cnLocal = new SqlConnection(Startup.ConnectionString))
                {
                    string sql = "select top 0 * into #tmpGrade from Grade";
                    cnLocal.Open();
                    var cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    //get data from attendance server
                    sql = "select CompCode, WrkGrp, GradeCode, GradeDesc,  " +
                          "'" + location + "' as location " +
                          "from MastGrade";
                    var da = new SqlDataAdapter(sql, cnRemote);
                    var dt = new DataTable();
                    da.Fill(dt);

                    using (var bulk = new SqlBulkCopy(cnLocal))
                    {
                        bulk.DestinationTableName = "#tmpGrade";

                        bulk.ColumnMappings.Add("CompCode", "CompCode");
                        bulk.ColumnMappings.Add("WrkGrp", "WrkGrp");
                        bulk.ColumnMappings.Add("GradeCode", "GradeCode");
                        bulk.ColumnMappings.Add("GradeDesc", "GradeName");
                        bulk.ColumnMappings.Add("Location", "Location");

                        bulk.WriteToServer(dt);
                    }

                    //bulk copy done, now use MERGE

                    sql = "merge into Grade as target " +
                          "using #tmpGrade as Source " +
                          "on " +
                          "Target.CompCode = Source.CompCode and " +
                          "Target.WrkGrp = Source.WrkGrp and " +
                          "Target.GradeCode = Source.GradeCode and " +
                          "Target.location = Source.location " +
                          "when matched then " +
                          "update set Target.GradeName = Source.GradeName " +
                          "when not matched then " +
                          "insert (compcode, wrkgrp, gradecode, gradename, location) " +
                          "values (source.compcode, source.wrkgrp, source.gradecode, source.gradename, " +
                          "'" + location + "' ); ";

                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    sql = "drop table #tmpGrade";
                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        } // end of SyncDesg

        public static void SyncEmpType(string strRemoteServer, string location)
        {
            try
            {
                using (var cnRemote = new SqlConnection(strRemoteServer))
                {
                    cnRemote.Open();
                    //first get all masters:

                    SqlConnection cnLocal;

                    //create a temp table
                    using (cnLocal = new SqlConnection(Startup.ConnectionString))
                    {
                        string sql = "select top 0 * into #tmpEmpTyp from EmpType";
                        cnLocal.Open();
                        var cmd = new SqlCommand(sql, cnLocal);
                        cmd.ExecuteNonQuery();


                        //get data from attendance server
                        sql = "select CompCode, WrkGrp, EmpTypeCode, EmpTypeDesc, " +
                              "'" + location + "' as location " +
                              " from MastEmpType";
                        var da = new SqlDataAdapter(sql, cnRemote);
                        var dt = new DataTable();
                        da.Fill(dt);

                        using (var bulk = new SqlBulkCopy(cnLocal))
                        {
                            bulk.DestinationTableName = "#tmpEmpTyp";

                            bulk.ColumnMappings.Add("CompCode", "CompCode");
                            bulk.ColumnMappings.Add("WrkGrp", "WrkGrp");
                            bulk.ColumnMappings.Add("EmpTypeCode", "EmpTypeCode");
                            bulk.ColumnMappings.Add("EmpTypeDesc", "EmpTypeName");
                            bulk.ColumnMappings.Add("Location", "Location");

                            bulk.WriteToServer(dt);
                        }

                        //bulk copy done, now use MERGE

                        sql = "merge into EmpType as target " +
                              "using #tmpEmpTyp as Source " +
                              "on " +
                              "Target.CompCode = Source.CompCode and " +
                              "Target.WrkGrp = Source.WrkGrp and " +
                              "Target.EmpTypeCode = Source.EmpTypeCode and " +
                              "Target.Location = Source.Location " +
                              "when matched then " +
                              "update set Target.EmpTypeName = Source.EmpTypeName " +
                              "when not matched then " +
                              "insert (compcode, wrkgrp, EmpTypeCode, EmpTypeName, Location ) " +
                              "values (source.compcode, source.wrkgrp, source.EmpTypeCode, source.EmpTypeName, " +
                              "'" + location + "' ); ";

                        cmd = new SqlCommand(sql, cnLocal);
                        cmd.ExecuteNonQuery();


                        sql = "drop table #tmpEmpTyp";
                        cmd = new SqlCommand(sql, cnLocal);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        } // end of SyncEmpType

        public static void SyncEmp(string strRemoteServer, string location)
        {
            try
            {
                using var cnRemote = new SqlConnection(strRemoteServer);
                cnRemote.Open();
                //first get all masters:

                SqlConnection cnLocal;

                //create a temp table
                using (cnLocal = new SqlConnection(Startup.ConnectionString))
                {
                    string sql = "select top 0 * into #tmpEmp from Employee";
                    cnLocal.Open();
                    var cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();

                    if (location == Locations.Kjqtl || location == Locations.Kjsaw)
                    {
                        sql = "select CompCode, EmpUnqId, WrkGrp, EmpName, FatherName, " +
                              "Active, EmpTypeCode, UnitCode, DeptCode, StatCode, CatCode, " +
                              "DesgCode, GradCode, " +
                              "'" + location + "' as location, " +
                              "EmpUnqId as SapId, 0 as CompanyAcc, " +
                              "convert(datetime2, BirthDT) as BirthDate, " +
                              "convert(datetime2, JoinDT) as JoinDate, ContCode " +
                              "from MastEmp where active = 1 ";
                    }
                    else
                    {
                        //get data from attendance server
                        sql = "select CompCode, EmpUnqId, WrkGrp, EmpName, FatherName, " +
                              "Active, EmpTypeCode, UnitCode, DeptCode, StatCode, CatCode, " +
                              "DesgCode, GradCode, " +
                              "'" + location + "' as location, " +
                              "SapId as SapId, 0 as CompanyAcc, " +
                              "convert(datetime2, BirthDT) as BirthDate, " +
                              "convert(datetime2, JoinDT) as JoinDate, ContCode " +
                              "from MastEmp ";
                    }


                    var da = new SqlDataAdapter(sql, cnRemote);
                    var dt = new DataTable();
                    da.Fill(dt);

                    using (var bulk = new SqlBulkCopy(cnLocal))
                    {
                        bulk.DestinationTableName = "#tmpEmp";

                        bulk.ColumnMappings.Add("CompCode", "CompCode");
                        bulk.ColumnMappings.Add("EmpUnqId", "EmpUnqId");
                        bulk.ColumnMappings.Add("WrkGrp", "WrkGrp");
                        bulk.ColumnMappings.Add("EmpTypeCode", "EmpTypeCode");
                        bulk.ColumnMappings.Add("UnitCode", "UnitCode");
                        bulk.ColumnMappings.Add("DeptCode", "DeptCode");
                        bulk.ColumnMappings.Add("StatCode", "StatCode");
                        bulk.ColumnMappings.Add("CatCode", "CatCode");
                        bulk.ColumnMappings.Add("DesgCode", "DesgCode");
                        bulk.ColumnMappings.Add("GradCode", "GradeCode");
                        bulk.ColumnMappings.Add("EmpName", "EmpName");
                        bulk.ColumnMappings.Add("FatherName", "FatherName");
                        bulk.ColumnMappings.Add("Active", "Active");
                        //bulk.ColumnMappings.Add("IsHod", "IsHod");
                        //bulk.ColumnMappings.Add("IsReleaser", "IsReleaser");
                        //bulk.ColumnMappings.Add("IsHrUser", "IsHrUser");
                        //bulk.ColumnMappings.Add("OtFlag", "OtFlag");
                        //bulk.ColumnMappings.Add("IsAdmin", "IsAdmin");
                        //bulk.ColumnMappings.Add("IsGpReleaser", "IsGpReleaser");
                        //bulk.ColumnMappings.Add("IsGaReleaser", "IsGaReleaser");
                        //bulk.ColumnMappings.Add("IsSecUser", "IsSecUser");
                        bulk.ColumnMappings.Add("Location", "Location");
                        bulk.ColumnMappings.Add("SapId", "SapId");
                        bulk.ColumnMappings.Add("CompanyAcc", "CompanyAcc");
                        bulk.ColumnMappings.Add("BirthDate", "BirthDate");
                        bulk.ColumnMappings.Add("JoinDate", "JoinDate");
                        bulk.ColumnMappings.Add("ContCode", "ContCode");

                        bulk.WriteToServer(dt);
                    }

                    //bulk copy done, now use MERGE

                    sql = "merge into Employee as target " +
                          "using #tmpEmp as Source " +
                          "on " +
                          "Target.CompCode = Source.CompCode and " +
                          //"Target.WrkGrp = Source.WrkGrp and " +
                          "Target.EmpUnqId = Source.EmpUnqId " +
                          "when matched then " +
                          "update set " +
                          "Target.WrkGrp = Source.WrkGrp, " +
                          "Target.EmpTypeCode = Source.EmpTypeCode, " +
                          "Target.UnitCode = Source.UnitCode, " +
                          "Target.DeptCode = Source.DeptCode, " +
                          "Target.StatCode = Source.StatCode, " +
                          //"Target.SecCode = Source.SecCode, " +
                          "Target.CatCode = Source.CatCode, " +
                          "Target.DesgCode = Source.DesgCode, " +
                          "Target.GradeCode = Source.GradeCode, " +
                          "Target.EmpName = Source.EmpName, " +
                          "Target.FatherName = Source.FatherName, " +
                          //"Target.OtFlag = Source.OtFlag," +
                          "Target.Active = Source.Active, " +
                          "Target.Location = Source.Location, " +
                          "Target.SapId = Source.SapId, " +
                          "Target.BirthDate = Source.BirthDate, " +
                          "Target.JoinDate = Source.JoinDate, " +
                          "Target.ContCode = Source.ContCode " +
                          "when not matched then " +
                          "insert (empunqid, compcode, wrkgrp, emptypecode, " +
                          "unitcode, deptcode, statcode, " +
                          //"seccode, " +
                          "catcode, " +
                          "desgcode, gradecode, empname, fathername, " +
                          "active, pass, " +
                          "location, sapid, companyacc, " +
                          "birthdate, joindate, contcode ) " +
                          "values (source.empunqid, source.compcode, source.wrkgrp, source.emptypecode, " +
                          "source.unitcode, source.deptcode, source.statcode, " +
                          //"source.seccode, " +
                          "source.catcode, " +
                          "source.desgcode, source.gradecode, source.empname, source.fathername, " +
                          "source.active, source.empunqid, " +
                          "'" + location + "', source.SapId, 0, source.BirthDate, source.JoinDate, " +
                          "source.ContCode  ); ";

                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();


                    sql = "drop table #tmpEmp";
                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        } // end of SyncEmp

        public static void SyncEmpMail(string strRemoteEssServer, string location)
        {
            try
            {
                using var cnRemote = new SqlConnection(strRemoteEssServer);
                cnRemote.Open();

                SqlConnection cnLocal;

                //create a temp table
                using (cnLocal = new SqlConnection(Startup.ConnectionString))
                {
                    string sql = "select top 0 EmpUnqId, Active, Email into #tmpEmp from Employee";
                    cnLocal.Open();
                    var cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();

                    sql = "select EmpUnqId, Active, Email, " +
                          "'" + location + "' as Location " +
                          "from Employees where active = 1 ";

                    var da = new SqlDataAdapter(sql, cnRemote);
                    var dt = new DataTable();
                    da.Fill(dt);

                    using (var bulk = new SqlBulkCopy(cnLocal))
                    {
                        bulk.DestinationTableName = "#tmpEmp";

                        bulk.ColumnMappings.Add("EmpUnqId", "EmpUnqId");
                        bulk.ColumnMappings.Add("Active", "Active");
                        bulk.ColumnMappings.Add("Email", "Email");

                        bulk.WriteToServer(dt);
                    }

                    //bulk copy done, now use MERGE

                    sql = "merge into Employee as target " +
                          "using #tmpEmp as Source " +
                          "on " +
                          "Target.EmpUnqId = Source.EmpUnqId " +
                          "when matched then " +
                          "update set " +
                          "Target.Email = Source.Email " +
                          "; ";

                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();

                    sql = "drop table #tmpEmp";
                    cmd = new SqlCommand(sql, cnLocal);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        }
    }
}