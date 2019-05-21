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
        public static string Logout()
        {
            HttpContext.Current.Session.Remove(id.ToString());
            HttpContext.Current.Session.Abandon();
           

            return "OK";

        }


    }
}

public class Roles_Content
{

    public string URL { get; set; }
    public string Icon_Name { get; set; }
    public string Page_Name { get; set; }
}