using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
namespace SpangleERP.GateIn
{
    /// <summary>
    /// Summary description for InventoryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InventoryService : System.Web.Services.WebService
    {

        [WebMethod]
        public void Get_All_GateIn(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<Gate_InClass> listEmployees = new List<Gate_InClass>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Gate_In_Proc", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDisplayLength = new SqlParameter()
                {
                    ParameterName = "@DisplayLength",
                    Value = displayLength
                };
                cmd.Parameters.Add(paramDisplayLength);

                SqlParameter paramDisplayStart = new SqlParameter()
                {
                    ParameterName = "@DisplayStart",
                    Value = displayStart
                };
                cmd.Parameters.Add(paramDisplayStart);

                SqlParameter paramSortCol = new SqlParameter()
                {
                    ParameterName = "@SortCol",
                    Value = sortCol
                };
                cmd.Parameters.Add(paramSortCol);

                SqlParameter paramSortDir = new SqlParameter()
                {
                    ParameterName = "@SortDir",
                    Value = sortDir
                };
                cmd.Parameters.Add(paramSortDir);

                SqlParameter paramSearchString = new SqlParameter()
                {
                    ParameterName = "@Search",
                    Value = string.IsNullOrEmpty(search) ? null : search
                };
                cmd.Parameters.Add(paramSearchString);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Gate_InClass emp = new Gate_InClass();
                    emp.G_Date = Convert.ToString(rdr["Date"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.G_Time = rdr["time"].ToString();
                    emp.VehicleNo = Convert.ToString(rdr["vechicalnumber"]);
                    emp.DriverName = rdr["drivername"].ToString();
                    emp.ReferenceBy = Convert.ToString(rdr["reference"]);
                    emp.G_Status = Convert.ToInt32(rdr["status"]);
                    emp.GateIn_Id = Convert.ToInt32(rdr["gate_id"]);
                    emp.PO_Number = Convert.ToString(rdr["po_number"]);

                    listEmployees.Add(emp);
                }
            }

            var result = new
            {
                iTotalRecords = GetEmployeesTotalCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = listEmployees
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(result));

        }

        private int GetEmployeesTotalCount()
        {
            int totalEmployeeCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new
                    SqlCommand("select count(*) from gatein", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }


        [WebMethod]
        public void Get_All_Inventory_Out(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<Inventory_OutClass> listEmployees = new List<Inventory_OutClass>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_Get_Inventory_Out", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDisplayLength = new SqlParameter()
                {
                    ParameterName = "@DisplayLength",
                    Value = displayLength
                };
                cmd.Parameters.Add(paramDisplayLength);

                SqlParameter paramDisplayStart = new SqlParameter()
                {
                    ParameterName = "@DisplayStart",
                    Value = displayStart
                };
                cmd.Parameters.Add(paramDisplayStart);

                SqlParameter paramSortCol = new SqlParameter()
                {
                    ParameterName = "@SortCol",
                    Value = sortCol
                };
                cmd.Parameters.Add(paramSortCol);

                SqlParameter paramSortDir = new SqlParameter()
                {
                    ParameterName = "@SortDir",
                    Value = sortDir
                };
                cmd.Parameters.Add(paramSortDir);

                SqlParameter paramSearchString = new SqlParameter()
                {
                    ParameterName = "@Search",
                    Value = string.IsNullOrEmpty(search) ? null : search
                };
                cmd.Parameters.Add(paramSearchString);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Inventory_OutClass emp = new Inventory_OutClass();
                    emp.Items_name = Convert.ToString(rdr["items_name"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.Dateof = rdr["DateOf"].ToString();
                    emp.Quantity = Convert.ToInt32(rdr["Quantity"]);
                    emp.grn_id = Convert.ToInt32(rdr["grn_id"]);
                    emp.emp_name = Convert.ToString(rdr["emp_name"]);
                    
                    listEmployees.Add(emp);
                }
            }

            var result = new
            {
                iTotalRecords = GetInvOutTotalCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = listEmployees
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(result));

        }

        private int GetInvOutTotalCount()
        {
            int totalEmployeeCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new
                    SqlCommand("select count(*) from InventoryOut", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }


        


        [WebMethod]
        public void Get_All_GRN(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<Gate_InClass> listEmployees = new List<Gate_InClass>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_Get_GRN", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDisplayLength = new SqlParameter()
                {
                    ParameterName = "@DisplayLength",
                    Value = displayLength
                };
                cmd.Parameters.Add(paramDisplayLength);

                SqlParameter paramDisplayStart = new SqlParameter()
                {
                    ParameterName = "@DisplayStart",
                    Value = displayStart
                };
                cmd.Parameters.Add(paramDisplayStart);

                SqlParameter paramSortCol = new SqlParameter()
                {
                    ParameterName = "@SortCol",
                    Value = sortCol
                };
                cmd.Parameters.Add(paramSortCol);

                SqlParameter paramSortDir = new SqlParameter()
                {
                    ParameterName = "@SortDir",
                    Value = sortDir
                };
                cmd.Parameters.Add(paramSortDir);

                SqlParameter paramSearchString = new SqlParameter()
                {
                    ParameterName = "@Search",
                    Value = string.IsNullOrEmpty(search) ? null : search
                };
                cmd.Parameters.Add(paramSearchString);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Gate_InClass emp = new Gate_InClass();
                    emp.G_Date = Convert.ToString(rdr["date"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.GateIn_Id = Convert.ToInt32 (rdr["gate_id"]);
                    emp.G_Status = Convert.ToInt32(rdr["Status"]);

                    listEmployees.Add(emp);
                }
            }

            var result = new
            {
                iTotalRecords = GetGRNTotalCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = listEmployees
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(result));

        }

        private int GetGRNTotalCount()
        {
            int totalEmployeeCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new
                    SqlCommand("select count(*) from Gatein", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }




        [WebMethod]
        public void Get_All_Items(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<ItemsClass2> listEmployees = new List<ItemsClass2>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetItems", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDisplayLength = new SqlParameter()
                {
                    ParameterName = "@DisplayLength",
                    Value = displayLength
                };
                cmd.Parameters.Add(paramDisplayLength);

                SqlParameter paramDisplayStart = new SqlParameter()
                {
                    ParameterName = "@DisplayStart",
                    Value = displayStart
                };
                cmd.Parameters.Add(paramDisplayStart);

                SqlParameter paramSortCol = new SqlParameter()
                {
                    ParameterName = "@SortCol",
                    Value = sortCol
                };
                cmd.Parameters.Add(paramSortCol);

                SqlParameter paramSortDir = new SqlParameter()
                {
                    ParameterName = "@SortDir",
                    Value = sortDir
                };
                cmd.Parameters.Add(paramSortDir);

                SqlParameter paramSearchString = new SqlParameter()
                {
                    ParameterName = "@Search",
                    Value = string.IsNullOrEmpty(search) ? null : search
                };
                cmd.Parameters.Add(paramSearchString);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ItemsClass2 emp = new ItemsClass2();
                    emp.items_name = Convert.ToString(rdr["items_name"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.items_id = Convert.ToInt32(rdr["items_id"]);
                    emp.cat_name = Convert.ToString(rdr["cat_name"]);
                    emp.type = Convert.ToInt32(rdr["type"]);

                    listEmployees.Add(emp);
                }
            }

            var result = new
            {
                iTotalRecords = GetItemsTotalCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = listEmployees
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(result));

        }

        private int GetItemsTotalCount()
        {
            int totalEmployeeCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new
                    SqlCommand("select count(*) from Items", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }

    }
}
public class ItemsClass2
{
    public int items_id { get; set; }
    public string items_name { get; set; }

    public int cat_id { get; set; }
    public string cat_name { get; set; }
    public int type { get; set; }
}