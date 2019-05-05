using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.IO;
namespace SpangleERP.HR_Module
{
    public partial class ApplicationLeave : System.Web.UI.Page
    {
        public static int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                string Us = (string)Session["id"];
                id = Convert.ToInt32(Us.ToString());
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }




        [WebMethod]
        public static string Insert(string type, string re, string sd, string ed, string days, string id)
        {
            int tleaves, leaves_count, left;
          

                if (type == "sl") {

                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd = new SqlCommand("select sl from Leave_Count where emp_id='" + Convert.ToInt32(id) + "'", conn);
                    conn.Open();

                    tleaves = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    SqlCommand cmd1 = new SqlCommand("select days from Leaves where emp_id=@empid and leave_type=@type", conn);
                    cmd1.Parameters.AddWithValue("@empid", Convert.ToInt32(id));
                    cmd1.Parameters.AddWithValue("@type", type);
                    conn.Open();
                    leaves_count = Convert.ToInt32(cmd1.ExecuteScalar());
                    conn.Close();

                    left = tleaves - leaves_count;
                    if (left >= Convert.ToInt32(days))
                    {

                        SqlCommand cmd2 = new SqlCommand(@"insert into Leaves values(@emp_id,@type_id,@rea,1,@sd,@ed,@days,@subdate)", conn);


                        cmd2.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                        cmd2.Parameters.AddWithValue("@type_id", type);
                        cmd2.Parameters.AddWithValue("@rea", re);
                        cmd2.Parameters.AddWithValue("@sd",SqlDbType.Date).Value=sd;
                    cmd2.Parameters.AddWithValue("@ed",SqlDbType.Date).Value=ed ;
                        cmd2.Parameters.AddWithValue("@days", Convert.ToInt32(days));
                        cmd2.Parameters.AddWithValue("@subdate", SqlDbType.Date).Value =System.DateTime.Now.Date;



                        conn.Open();
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                        return "Save";
                    }
                    else
                    {
                       return "Days>";
                    }

                    

                }
                else if (type == "cl")
                {

                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd = new SqlCommand("select cl from Leave_Count where emp_id='" + Convert.ToInt32(id) + "'", conn);
                    conn.Open();

                    tleaves = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    SqlCommand cmd1 = new SqlCommand("select days from Leaves where emp_id=@empid and leave_type=@type", conn);
                    cmd1.Parameters.AddWithValue("@empid", Convert.ToInt32(id));
                    cmd1.Parameters.AddWithValue("@type", type);
                    conn.Open();
                    leaves_count = Convert.ToInt32(cmd1.ExecuteScalar());
                    conn.Close();

                    left = tleaves - leaves_count;
                    if (left >= Convert.ToInt32(days))
                    {
                        SqlCommand cmd3 = new SqlCommand(@"insert into Leaves values(@emp_id,@type_id,@rea,1,@sd,@ed,@days,@subdate)", conn);




                        cmd3.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                        cmd3.Parameters.AddWithValue("@type_id", type);
                        cmd3.Parameters.AddWithValue("@rea", re);
                        cmd3.Parameters.AddWithValue("@sd", SqlDbType.Date).Value = sd;
                        cmd3.Parameters.AddWithValue("@ed", SqlDbType.Date).Value = ed;
                        cmd3.Parameters.AddWithValue("@days", Convert.ToInt32(days));
                        cmd3.Parameters.AddWithValue("@subdate", SqlDbType.Date).Value = System.DateTime.Now;

                        conn.Open();
                        cmd3.ExecuteNonQuery();
                        conn.Close();
                        conn.Dispose();
                        return "Save";
                    }
                    else
                    {

                        return "Days>";
                    }

                    
                }

                else if (type == "al")
                {

                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd = new SqlCommand("select al from Leave_Count where emp_id='" + Convert.ToInt32(id) + "'", conn);
                    conn.Open();

                    tleaves = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    SqlCommand cmd1 = new SqlCommand("select days from Leaves where emp_id=@empid and leave_type=@type", conn);
                    cmd1.Parameters.AddWithValue("@empid", Convert.ToInt32(id));
                    cmd1.Parameters.AddWithValue("@type", type);
                    conn.Open();
                    leaves_count = Convert.ToInt32(cmd1.ExecuteScalar());
                    conn.Close();

                    left = tleaves - leaves_count;

                    if (left >= Convert.ToInt32(days))
                    {
                        SqlCommand cmd4 = new SqlCommand(@"insert into Leaves values(@emp_id,@type_id,@rea,1,@sd,@ed,@days,@subdate)", conn);




                        cmd4.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                        cmd4.Parameters.AddWithValue("@type_id", type);
                        cmd4.Parameters.AddWithValue("@rea", re);
                        cmd4.Parameters.AddWithValue("@sd", SqlDbType.Date).Value = sd;
                        cmd4.Parameters.AddWithValue("@ed", SqlDbType.Date).Value = ed;
                        cmd4.Parameters.AddWithValue("@days", Convert.ToInt32(days));
                        cmd4.Parameters.AddWithValue("@subdate", SqlDbType.Date).Value = System.DateTime.Now;

                        conn.Open();
                        cmd4.ExecuteNonQuery();
                        conn.Close();
                        conn.Dispose();
                        return "Save";
                    }

                    else
                    {

                        return "Days>";
                    }

                }
                return "";
        
            





        }


    }
}