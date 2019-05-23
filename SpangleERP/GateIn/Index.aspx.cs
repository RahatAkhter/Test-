using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Net.Mail;
using System.Text;

namespace SpangleERP.GateIn
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string Signin(string name, string pass)
        {
            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@"select count(*) from Users where Email=@email and password=@password", conn);
                // cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                cmd.Parameters.AddWithValue("@email", name);
                cmd.Parameters.AddWithValue("@password", pass);

                conn.Open();
                int Count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                if (Count == 0)
                {
                    return "Incorrect".ToString();
                }
                else
                {

                    SqlCommand cmd2 = new SqlCommand("select User_id from Users where email=@email and password=@password", conn);
                    cmd2.Parameters.AddWithValue("@email", name);
                    cmd2.Parameters.AddWithValue("@password", pass);

                    conn.Open();
                    int id = Convert.ToInt32(cmd2.ExecuteScalar());
                    conn.Close();
                    HttpContext.Current.Session["id"] = id.ToString();
                    conn.Close();

                    return "ok";

                }
            }
            catch (Exception ex)
            {
                return "" + ex.ToString();
            }
        }
    }

}

    
