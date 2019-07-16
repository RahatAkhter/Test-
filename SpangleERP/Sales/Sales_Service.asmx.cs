using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
namespace SpangleERP.Sales
{
    /// <summary>
    /// Summary description for Sales_Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Sales_Service : System.Web.Services.WebService
    {

        [WebMethod]
        public void Get_All_products(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<Product> listEmployees = new List<Product>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetProducts", con);
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
                    Product emp = new Product();
                    emp.pid = Convert.ToInt32(rdr["Product_id"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.pname = rdr["Product_Name"].ToString();
                    emp.unit_in_car = Convert.ToInt32(rdr["unit_in_carton"]);
                    emp.price =Convert.ToInt32(rdr["Price"].ToString());
                    emp.uinm = Convert.ToString(rdr["unit_in_measures"]);
                    emp.weight = Convert.ToString(rdr["weight"]);
                    emp.pcat_name = Convert.ToString(rdr["pcat_name"]);
                

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
                    SqlCommand("select count(*) from Products", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }

        [WebMethod]
        public void Get_All_Discount(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<Cls_Distributor_Dic> listEmployees = new List<Cls_Distributor_Dic>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetDist_Disc", con);
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
                    Cls_Distributor_Dic emp = new Cls_Distributor_Dic();
                    //emp.pid = Convert.ToInt32(rdr["Product_id"]);
                    //filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    //emp.pname = rdr["Product_Name"].ToString();
                    //emp.unit_in_car = Convert.ToInt32(rdr["unit_in_carton"]);
                    //emp.price = Convert.ToInt32(rdr["Price"].ToString());
                    //emp.uinm = Convert.ToString(rdr["unit_in_measures"]);
                    //emp.weight = Convert.ToString(rdr["weight"]);
                    //emp.pcat_name = Convert.ToString(rdr["pcat_name"]);


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

        //private int GetEmployeesTotalCount()
        //{
        //    int totalEmployeeCount = 0;
        //    string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new
        //            SqlCommand("select count(*) from Products", con);
        //        con.Open();
        //        totalEmployeeCount = (int)cmd.ExecuteScalar();
        //    }
        //    return totalEmployeeCount;



        //}



        [WebMethod]
        public void Get_Profiles(string term)
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            List<ClsDistributor> profiles = new List<ClsDistributor>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spGetDistNames", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramName = new SqlParameter()
            {
                ParameterName = "@Name",
                Value = term

            };
            cmd.Parameters.Add(paramName);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ClsDistributor obj = new ClsDistributor();
                obj.Distributor_id = Convert.ToInt32(rdr["Distributor_id"]);
                obj.dist_name = rdr["dist_name"].ToString();
               
                profiles.Add(obj);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(profiles));
        }

    }
}
