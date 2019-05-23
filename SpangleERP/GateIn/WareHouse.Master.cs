using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpangleERP.invent
{
    public partial class WareHouse : System.Web.UI.MasterPage
    {
        public static int id; 
        protected void Page_Load(object sender, EventArgs e)
        {
            string id1 = (string)Session["id"];
            id = Convert.ToInt32(id1);
        }

        protected void log_Click(object sender, EventArgs e)
        {

            Session.Remove(id.ToString());
            Session.Abandon();
            Response.Redirect("Index.aspx", true);
        }
    }
}