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
                Response.Redirect("~/index.aspx");
            }

            Bound();
        }
        public void Bound()
        {
            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand cmd = new SqlCommand(@"Select page_Name,page_id from pages", conn);

            conn.Open();
            DropDownList1.DataSource = cmd.ExecuteReader();
            DropDownList1.DataTextField = "page_Name";
            DropDownList1.DataValueField = "page_id";
            DropDownList1.DataBind();

            conn.Close();
            conn.Dispose();

           



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
        public static string InsertChild(string page_id, string level)
        {

            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd1 = new SqlCommand(@"insert into Roles_Content values(@Role_Id,@User_Id,@Page_Id,@rights)", conn);

                cmd1.Parameters.AddWithValue("@Page_Id",Convert.ToInt32(page_id));
                cmd1.Parameters.AddWithValue("@rights", level);
                cmd1.Parameters.AddWithValue("@Role_Id", Convert.ToInt32(Role_Id));
                cmd1.Parameters.AddWithValue("@User_Id", id);
               
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

        [WebMethod]
        public static string Delete_Parent()
        {
            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd1 = new SqlCommand(@"
Delete from Roles where Role_id=@Role_Id", conn);

                cmd1.Parameters.AddWithValue("@Role_Id", Convert.ToInt32(Role_Id));
                
                conn.Open();
                cmd1.ExecuteNonQuery();

                conn.Close();
                int rid = Convert.ToInt32(Role_Id);
                Role_Id = null;
                return "Deleted"+Role_Id;

            }
            catch(Exception ex)
            {
                return ex.Message+" Role_id :"+Role_Id;
            }
        }

        [WebMethod]
        public static List<Roles> GetRolesContent(string RID)
        {

     
                List<Roles> list_det = new List<Roles>();


                DataTable dt = getprint(Convert.ToInt32(RID));
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    Roles p = new Roles();

                    p.Page_name = Convert.ToString(dt.Rows[i]["Page_name"]);
                    p.Rights = Convert.ToString(dt.Rows[i]["Rights"]);

                    list_det.Add(p);
                }
                return list_det;
           
        }

        private static DataTable getprint(int Rid)
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"Select  page_Name,r.Rights from pages as p
 left join Roles_Content as r
 on p.page_id=r.Page_id
 left join Roles as rc
 on rc.Role_id=r.Role_id
 where r.Role_id=@rid", conn);
            cmd.Parameters.AddWithValue("@rid",Rid);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }

    }
}