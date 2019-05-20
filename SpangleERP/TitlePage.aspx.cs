using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
namespace SpangleERP.HR_Module
{
    public partial class delete3 : System.Web.UI.Page
    {
        public static int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                string Us = (string)Session["id"];
                id = Convert.ToInt32(Us.ToString());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+id+"')", true);
                //Response.Redirect(Request.RawUrl.Replace(Request.Url.Query, ""));
            }
            else
            {
                Response.Redirect("~/index.aspx");
            }
        }

        [WebMethod]
        public static List<Roles_Content> GetRolos()
        {

           
                List<Roles_Content> list_det = new List<Roles_Content>();


                DataTable dt = getprint();
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    Roles_Content p = new Roles_Content();

                    p.URL = Convert.ToString(dt.Rows[i]["URL"]);
                    p.Icon_Name = Convert.ToString(dt.Rows[i]["Icon_Name"]);
                p.Page_Name = Convert.ToString(dt.Rows[i]["Page_Name"]);

                list_det.Add(p);
                }
                return list_det;
            
        }

        private static DataTable getprint()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"
Select  p.page_Name,r.Rights,p.URl,p.Icon_Name from pages as p
 left join Roles_Content as r
 on p.page_id=r.Page_id
 left join Roles as rc
 on rc.Role_id=r.Role_id
 left join Users as u
 on u.Role=rc.Role_id
 where u.User_id=@id", conn);
            cmd.Parameters.AddWithValue("@id",id);
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

public class Roles_Content
{

    public string URL { get; set; }
    public string Icon_Name { get; set; }
    public string Page_Name { get; set; }
}