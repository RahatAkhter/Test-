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
    public partial class UpdateLeave : System.Web.UI.Page
    {
        public static int id;

        public static string Access;
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
            Access = PageName().ToString();
        }

        public string PageName()
        {
            string sPath = Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;


            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@" Select r.Rights from pages as p
 left join Roles_Content as r
 on p.page_id=r.Page_id
 left join Roles as rc
 on rc.Role_id=r.Role_id
 left join users as u
 on u.Role=rc.Role_id
 where u.User_id=@id and  p.URl=@pname", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@pname", sRet);

                conn.Open();
                string level = cmd.ExecuteScalar().ToString();
                conn.Close();


                return level;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }


        }


        [WebMethod]
        public static string Access_Levels()
        {

            return Access;
        }



        [WebMethod]
        public static string Insert(string sl,string cl,string al,string emp_id)
        {
            try
            {


                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd1 = new SqlCommand(@"insert into Leave_Count values(@sl,@al,@cl,@emp_id,@Usr_id)", conn);
                cmd1.Parameters.AddWithValue("@sl",Convert.ToInt32(sl));
                cmd1.Parameters.AddWithValue("@al", Convert.ToInt32(al));
                cmd1.Parameters.AddWithValue("@cl",Convert.ToInt32(cl) );
                cmd1.Parameters.AddWithValue("@emp_id", Convert.ToInt32(emp_id));
                cmd1.Parameters.AddWithValue("@Usr_id", 1);
              
                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                
                return "Save";



            }
            catch (Exception ex)
            {
                return "" + ex.Message;
            }



        }

        [WebMethod]
        public static string Update(string sl, string cl, string al, string emp_id)
        {
            try
            {


                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd1 = new SqlCommand(@"update Leave_Count set sl=@sl , al=@al,cl=@cl where emp_id=@emp_id", conn);
                cmd1.Parameters.AddWithValue("@sl", Convert.ToInt32(sl));
                cmd1.Parameters.AddWithValue("@al", Convert.ToInt32(al));
                cmd1.Parameters.AddWithValue("@cl", Convert.ToInt32(cl));
                cmd1.Parameters.AddWithValue("@emp_id", Convert.ToInt32(emp_id));
                

                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                return "Save";



            }
            catch (Exception ex)
            {
                return "" + ex.Message;
            }



        }


        [WebMethod]
        public static List<Leaves_Count> Get_Data(string eid)
        {
            List<Leaves_Count> list_det = new List<Leaves_Count>();


            DataTable dt = get_all_products(Convert.ToInt32(eid));
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                Leaves_Count p = new Leaves_Count();

                p.sl = Convert.ToInt32(dt.Rows[i]["sl"]);
                p.cl = Convert.ToInt32(dt.Rows[i]["cl"]);

                p.al = Convert.ToInt32(dt.Rows[i]["al"]);


                list_det.Add(p);
            }
            return list_det;

        }

        private static DataTable get_all_products(int eid)
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select * from Leave_Count where emp_id='"+eid+"'", conn);
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