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
        public static string Insert_Parent(string Name)
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

        [WebMethod]
        public static string InsertChild(string path1,string pname,string icon_name,string level)
        {

            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd1 = new SqlCommand(@"insert into Roles_Content values(@URl,@rights,@Icon_Name,@Role_Id,@User_Id,@Page_Name)", conn);

                cmd1.Parameters.AddWithValue("@URl", path1);
                cmd1.Parameters.AddWithValue("@rights", level);
                cmd1.Parameters.AddWithValue("@Icon_Name", icon_name);
                cmd1.Parameters.AddWithValue("@Role_Id", Convert.ToInt32(Role_Id));
                cmd1.Parameters.AddWithValue("@User_Id", id);
                cmd1.Parameters.AddWithValue("@Page_Name", pname);


                conn.Open();
                cmd1.ExecuteNonQuery();

                conn.Close();
               

                return "Save";
            }
            catch(Exception ex)
            {
                return "Exception Error";
            }
        }


    }
}