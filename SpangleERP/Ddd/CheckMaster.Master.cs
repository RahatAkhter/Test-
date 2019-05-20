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
    public partial class CheckMaster : System.Web.UI.MasterPage
    {
        public static int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Us = (string)Session["id"];
            id = Convert.ToInt32(Us.ToString());
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + id + "')", true);
            //Response.Redirect(Request.RawUrl.Replace(Request.Url.Query, ""));
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
            conn.Open();
            cmd.Parameters.AddWithValue("@id",id);
            SqlDataReader rea = cmd.ExecuteReader();

            Repeater1.DataSource = rea;
            Repeater1.DataBind();
            conn.Close();
            conn.Dispose();



        }

     
    }
}