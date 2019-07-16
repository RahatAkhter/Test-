using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
namespace SpangleERP.HR_Module
{
    /// <summary>
    /// Summary description for Employees
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Employees : System.Web.Services.WebService
    {


        [WebMethod]
        public void GetAll_Employees(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<All_Employees> listEmployees = new List<All_Employees>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployees", con);
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
                    All_Employees emp = new All_Employees();
                   emp.emp_id = Convert.ToInt32(rdr["ID"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.emp_name = rdr["Name"].ToString();
                    emp.Designation = Convert.ToString(rdr["Designation"]);
                    emp.mobile = rdr["mobile"].ToString();
                    emp.Img = Convert.ToString(rdr["Img"]);
                    emp.status= Convert.ToInt32(rdr["status"]);
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
                    SqlCommand("select count(*) from Employee", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }

        // here we get All Users
        [WebMethod]
        public void GetAll_Users(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<Users> listEmployees = new List<Users>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetUsers", con);
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
                    Users emp = new Users();
                    emp.User_id = Convert.ToInt32(rdr["User_id"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.emp_name = rdr["emp_name"].ToString();
                    emp.Desig = Convert.ToString(rdr["Designation"]);
                    emp.Role = rdr["Role_name"].ToString();
                    emp.Img = Convert.ToString(rdr["Img"]);
                    emp.dep = Convert.ToString(rdr["dep_name"]);
                    listEmployees.Add(emp);
                }
            }

            var result = new
            {
                iTotalRecords = GetUsersTotalCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = listEmployees
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(result));

        }

        private int GetUsersTotalCount()
        {
            int totalEmployeeCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new
                    SqlCommand("select count(*) from Users", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }








        //here we All Roles
        [WebMethod]
        public void GetAll_Roles(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<Roles> listEmployees = new List<Roles>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("get_Roles", con);
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
                    Roles emp = new Roles();
                    emp.Role_Id = Convert.ToInt32(rdr["Role_Id"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.Role_Name = rdr["Role_Name"].ToString();
                   
                    listEmployees.Add(emp);
                }
            }

            var result = new
            {
                iTotalRecords = GetRolesTotalCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = listEmployees
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(result));

        }

        private int GetRolesTotalCount()
        {
            int totalEmployeeCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new
                    SqlCommand("select count(*) from Roles", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }


        // here we get EMployee History Data

        [WebMethod]
        public void GetAll_Emp_History(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<EmployeeHistory> listEmployees = new List<EmployeeHistory>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetHistory", con);
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
                    EmployeeHistory emp = new EmployeeHistory();
                    emp.emp_id = Convert.ToInt32(rdr["emp_id"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.emp_name = rdr["emp_name"].ToString();
                    emp.designation = Convert.ToString(rdr["designation"]);
                    emp.Status =Convert.ToInt32( rdr["Status"]);
                    emp.documrnt = Convert.ToString(rdr["documrnt"]);
                    emp.date_of_exit = Convert.ToString(rdr["date_of_exit"]);
                    emp.Reason = Convert.ToString(rdr["Reason"]);
                    emp.hist_id= Convert.ToInt32(rdr["hist_id"]);
                    listEmployees.Add(emp);
                }
            }

            var result = new
            {
                iTotalRecords = GetEmployeesHistTotalCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = listEmployees
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(result));

        }

        private int GetEmployeesHistTotalCount()
        {
            int totalEmployeeCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new
                    SqlCommand("select count(*) from Emp_History", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }




        [WebMethod]
        public void Get_Profiles(string term)
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            List<Get_Emp_Names> profiles = new List<Get_Emp_Names>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spGetNames", con);
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
                Get_Emp_Names obj = new Get_Emp_Names();
                obj.emp_id = Convert.ToInt32(rdr["emp_id"]);
                obj.emp_name = rdr["emp_name"].ToString();
                obj.Img = rdr["Img"].ToString();
                profiles.Add(obj);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(profiles));
        }


        [WebMethod]
        public void GetLeaves_Count(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<Leaves_Count> listEmployees = new List<Leaves_Count>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetUpdate_Leave", con);
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
                int sl, cl, al;
                SqlConnection con1 = new SqlConnection(cs);

                while (rdr.Read())
                {
                   
                    Leaves_Count emp = new Leaves_Count();
                    emp.emp_id = Convert.ToInt32(rdr["emp_id"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.emp_name = rdr["emp_name"].ToString();
                    emp.Desig = Convert.ToString(rdr["Designation"]);
                    emp.sl = Convert.ToInt32(rdr["sl"]);
                    emp.cl = Convert.ToInt32(rdr["cl"]);
                    emp.al = Convert.ToInt32(rdr["al"]);
                    emp.Img = Convert.ToString(rdr["Img"]);
                     SqlCommand cmd1 = new SqlCommand("select ISNULL(Sum(l.days),0) from Leaves as l where emp_id='" + Convert.ToInt32(rdr["emp_id"]) +"' and leave_type='sl'", con1);
                    con1.Open();
                    sl = Convert.ToInt32(cmd1.ExecuteScalar());
                    con1.Close();
                    SqlCommand cmd2 = new SqlCommand("select ISNULL(Sum(l.days),0) from Leaves as l where emp_id='" + Convert.ToInt32(rdr["emp_id"]) + "' and leave_type='cl'", con1);
                    con1.Open();
                    cl = Convert.ToInt32(cmd2.ExecuteScalar());
                    con1.Close();

                    SqlCommand cmd3 = new SqlCommand("select ISNULL(Sum(l.days),0) from Leaves as l where emp_id='" + Convert.ToInt32(rdr["emp_id"]) + "' and leave_type='al'", con1);
                    con1.Open();
                    al = Convert.ToInt32(cmd3.ExecuteScalar());
                    con1.Close();
                    emp.lsl = Convert.ToInt32(rdr["sl"])-sl;
                    emp.lcl = Convert.ToInt32(rdr["cl"]) - cl;
                    emp.lal = Convert.ToInt32(rdr["al"]) - al;


                    listEmployees.Add(emp);
                }
            }

            var result = new
            {
                iTotalRecords = GetUpdate_LeavesCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = listEmployees
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(result));

        }

        private int GetUpdate_LeavesCount()
        {
            int totalEmployeeCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new
                    SqlCommand("select count(*) from Leave_Count", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }


        [WebMethod]
        public void GetALl_Leaves(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<All_Leaves> listEmployees = new List<All_Leaves>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetLeaves", con);
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
                
                SqlConnection con1 = new SqlConnection(cs);

                while (rdr.Read())
                {

                    All_Leaves emp = new All_Leaves();
                    emp.emp_id = Convert.ToInt32(rdr["emp_id"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.emp_name = rdr["emp_name"].ToString();
                    emp.Img = rdr["Img"].ToString();
                    emp.type = rdr["type"].ToString();
                    emp.reason = rdr["reason"].ToString();
                    emp.days =Convert.ToInt32( rdr["days"]);
                    emp.date = rdr["submit_date"].ToString();


                    listEmployees.Add(emp);
                }
            }

            var result = new
            {
                iTotalRecords = GetAll_LeavesCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = listEmployees
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(result));

        }

        private int GetAll_LeavesCount()
        {
            int totalEmployeeCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new
                    SqlCommand(@"select COUNT(*) from Employee as e
left join Leaves as l
on l.emp_id = e.emp_id", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }


        [WebMethod]
        public void Get_EmpShifts(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<Emp_Shifts> listEmployees = new List<Emp_Shifts>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetEmp_Shifts", con);
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

                SqlConnection con1 = new SqlConnection(cs);

                while (rdr.Read())
                {

                    Emp_Shifts emp = new Emp_Shifts();
                    emp.emp_id = Convert.ToInt32(rdr["emp_id"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.emp_name = rdr["emp_name"].ToString();
                    emp.desig = rdr["Designation"].ToString();
                    emp.mon = rdr["Mon"].ToString();
                    emp.tue = rdr["tue"].ToString();
                    emp.wed = rdr["Wed"].ToString();
                    emp.thu = rdr["Thu"].ToString();
                    emp.fri = rdr["Fri"].ToString();
                    emp.sat = rdr["Sat"].ToString();
                    emp.sun = rdr["Sun"].ToString();
                    emp.status =Convert.ToInt32( rdr["status"].ToString());


                    listEmployees.Add(emp);
                }
            }

            var result = new
            {
                iTotalRecords = GetAll_EmpShifts(),
                iTotalDisplayRecords = filteredCount,
                aaData = listEmployees
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(result));

        }

        private int GetAll_EmpShifts()
        {
            int totalEmployeeCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new
                    SqlCommand(@"select COUNT(*) from Employee ", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }

       // here we het All Users
        [WebMethod]
        public void Get_Departments(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            int displayLength = iDisplayLength;
            int displayStart = iDisplayStart;
            int sortCol = iSortCol_0;
            string sortDir = sSortDir_0;
            string search = sSearch;

            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            List<ClsDepartments> listEmployees = new List<ClsDepartments>();
            int filteredCount = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetDepartment", con);
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
                    ClsDepartments emp = new ClsDepartments();
                    emp.dep_name = Convert.ToString(rdr["dep_name"]);
                    filteredCount = Convert.ToInt32(rdr["TotalCount"]);
                    emp.dep_id =Convert.ToInt32(rdr["dep_id"].ToString());
                    emp.desc = Convert.ToString(rdr["dep_discription"]);
                   
                    listEmployees.Add(emp);
                }
            }

            var result = new
            {
                iTotalRecords = GetDepTotalCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = listEmployees
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(result));

        }

        private int GetDepTotalCount()
        {
            int totalEmployeeCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new
                    SqlCommand("select count(*) from Department", con);
                con.Open();
                totalEmployeeCount = (int)cmd.ExecuteScalar();
            }
            return totalEmployeeCount;



        }


    }
}

public class Get_Emp_Names
{
    public int emp_id { get; set; }
    public string emp_name { get; set; }
    public string Img { get; set; }

}