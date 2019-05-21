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
        public static string Insert(string pname, string page_url, string Icon)
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

                    SqlCommand cmd = new SqlCommand("insert into Pages values(@URl,@Icon,@pName)", conn);
                    cmd.Parameters.AddWithValue("@URl", page_url);
                    cmd.Parameters.AddWithValue("@Icon", Icon);
                    cmd.Parameters.AddWithValue("@pName",pname);

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
    }
}