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

        //[WebMethod]
        //public void GetAll_Employees(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        //{
        //    int displayLength = iDisplayLength;
        //    int displayStart = iDisplayStart;
        //    int sortCol = iSortCol_0;
        //    string sortDir = sSortDir_0;
        //    string search = sSearch;

        //    string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

        //    List<All_Employees> listEmployees = new List<All_Employees>();
        //    int filteredCount = 0;
        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("spGetEmployees", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        SqlParameter paramDisplayLength = new SqlParameter()
        //        {
        //            ParameterName = "@DisplayLength",
        //            Value = displayLength
        //        };
        //        cmd.Parameters.Add(paramDisplayLength);

        //        SqlParameter paramDisplayStart = new SqlParameter()
        //        {
        //            ParameterName = "@DisplayStart",
        //            Value = displayStart
        //        };
        //        cmd.Parameters.Add(paramDisplayStart);

        //        SqlParameter paramSortCol = new SqlParameter()
        //        {
        //            ParameterName = "@SortCol",
        //            Value = sortCol
        //        };
        //        cmd.Parameters.Add(paramSortCol);

        //        SqlParameter paramSortDir = new SqlParameter()
        //        {
        //            ParameterName = "@SortDir",
        //            Value = sortDir
        //        };
        //        cmd.Parameters.Add(paramSortDir);

        //        SqlParameter paramSearchString = new SqlParameter()
        //        {
        //            ParameterName = "@Search",
        //            Value = string.IsNullOrEmpty(search) ? null : search
        //        };
        //        cmd.Parameters.Add(paramSearchString);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            All_Employees emp = new All_Employees();
        //            emp.emp_id = Convert.ToInt32(rdr["ID"]);
        //            filteredCount = Convert.ToInt32(rdr["TotalCount"]);
        //            emp.emp_name = rdr["Name"].ToString();
        //            emp.Designation = Convert.ToString(rdr["Designation"]);
        //            emp.mobile = rdr["mobile"].ToString();
        //            emp.Img = Convert.ToString(rdr["Img"]);
        //            emp.status = Convert.ToInt32(rdr["status"]);
        //            listEmployees.Add(emp);
        //        }
        //    }

        //    var result = new
        //    {
        //        iTotalRecords = GetEmployeesTotalCount(),
        //        iTotalDisplayRecords = filteredCount,
        //        aaData = listEmployees
        //    };

        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    Context.Response.Write(js.Serialize(result));

        //}

        //private int GetEmployeesTotalCount()
        //{
        //    int totalEmployeeCount = 0;
        //    string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new
        //            SqlCommand("select count(*) from Employee", con);
        //        con.Open();
        //        totalEmployeeCount = (int)cmd.ExecuteScalar();
        //    }
        //    return totalEmployeeCount;



        //}


    }
}
