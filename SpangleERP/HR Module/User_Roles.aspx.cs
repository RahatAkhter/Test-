using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
namespace SpangleERP.HR_Module
{
    public partial class User_Roles : System.Web.UI.Page
    {
        public static int? Role_Id;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static string Insert(string Name)
        {

            try
            {



                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd = new SqlCommand("Select Count(*) from Roles where Role_name=@name", conn);
                cmd.Parameters.AddWithValue("@name", Convert.ToString(Name));

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                if (count == 0)
                {
                    SqlCommand cmd1 = new SqlCommand(@"insert into Roles OUTPUT inserted.Role_id values(@name)", conn);
                    
                    cmd1.Parameters.AddWithValue("@name", Name);

                    conn.Open();
                    Role_Id = Convert.ToInt32(cmd1.ExecuteScalar());

                    conn.Close();
                    conn.Dispose();

                    return "Save";
                }
                else
                {
                    return "This Role Name Already Exist";
                }

            }
            catch (Exception ex)
            {
                return "Some Error ";
            }



        }

    }
}