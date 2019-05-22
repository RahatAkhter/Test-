using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
namespace SpangleERP.ITDepart
{
    public partial class Pages : System.Web.UI.Page
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
                Response.Redirect("~/index.aspx");
            }
        }


        [WebMethod]
        public static string Insert(string pname, string page_url, string Icon,string fName)
        {
            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd1 = new SqlCommand("Select count(*) from Pages where Url =@url or Page_Name=@pname",conn);
                cmd1.Parameters.AddWithValue("@url",page_url);
                cmd1.Parameters.AddWithValue("@pname", pname);

                conn.Open();
                int count = Convert.ToInt32(cmd1.ExecuteScalar());
                conn.Close();

                if (count == 0)
                {

                    SqlCommand cmd = new SqlCommand("insert into Pages values(@URl,@Icon,@pName,@fname)", conn);
                    cmd.Parameters.AddWithValue("@URl", page_url);
                    cmd.Parameters.AddWithValue("@Icon", Icon);
                    cmd.Parameters.AddWithValue("@pName",pname);
                    cmd.Parameters.AddWithValue("@fName", fName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "Save";
                }
                else
                {
                    return "Already Have Please Choose Other Name OR URl";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        [WebMethod]
        public static string Update(string pname, string page_url, string Icon, string fName,string page_id)
        {
            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd1 = new SqlCommand(@"
Select count(*) from Pages where (Url = @url or Page_Name = @pname)
and page_id != @pid", conn);
                cmd1.Parameters.AddWithValue("@url", page_url);
                cmd1.Parameters.AddWithValue("@pname", pname);
                cmd1.Parameters.AddWithValue("@pid", Convert.ToInt32(page_id));

                conn.Open();
                int count = Convert.ToInt32(cmd1.ExecuteScalar());
                conn.Close();

                if (count == 0)
                {

                    SqlCommand cmd = new SqlCommand("Update Pages set URl=@url , Icon_Name=@icon, page_Name=@pageName , Folder_Name=@fname where page_id=@pid", conn);
                    cmd.Parameters.AddWithValue("@url", page_url);
                    cmd.Parameters.AddWithValue("@icon", Icon);
                    cmd.Parameters.AddWithValue("@pageName", pname);
                    cmd.Parameters.AddWithValue("@fname", fName);
                    cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(page_id));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "Update";
                }
                else
                {
                    return "Already Have Please Choose Other Name OR URl";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }




        [WebMethod]

        public static List<AllPage> GetRecord(string page_id)
        {
            
                List<AllPage> list_det = new List<AllPage>();


                DataTable dt = getPageData(Convert.ToInt32(page_id));
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    AllPage p = new AllPage();

                    p.Page_Name = Convert.ToString(dt.Rows[i]["Page_Name"]);
                    p.URl = Convert.ToString(dt.Rows[i]["URl"]);
                    p.Folder_Name = Convert.ToString(dt.Rows[i]["Folder_Name"]);
                    p.Icon_Name = Convert.ToString(dt.Rows[i]["Icon_Name"]);
                 
                    list_det.Add(p);
                }
                return list_det;

        }

        private static DataTable getPageData(int pid)
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"Select * from pages where page_id=@pid", conn);
            cmd.Parameters.AddWithValue("@pid",pid);
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