using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
namespace SpangleERP.HR_Module
{
    public partial class ShiftDetail : System.Web.UI.Page
    {public static  int id;
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

        public void Save(object sender, EventArgs args)
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand cmd = new SqlCommand("insert into Shifts values(@name,@st,@et)", conn);
            
            cmd.Parameters.AddWithValue("@name", shiftname.Text.ToString());
            cmd.Parameters.AddWithValue("@st", s_time.Text.ToString());
            cmd.Parameters.AddWithValue("@et", Endtime.Text.ToString());
            
            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();

            Response.Redirect("ShiftDetail.aspx");


            shiftname.Text = s_time.Text = Endtime.Text = null;



        }


    }
}