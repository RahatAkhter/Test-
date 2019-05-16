using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace SpangleERP.HR_Module
{
    public partial class GazatedForm : System.Web.UI.Page
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
            Button1.Visible = false;
        }
        public void Save(object sender, EventArgs args)
        {

            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                DateTime d = new DateTime();
                d = Convert.ToDateTime(Date1.Text);
                string a = d.DayOfWeek.ToString();

                SqlCommand cmd = new SqlCommand(@"insert into holidays values(@Date,@Day,@disc,@updatedby)", conn);
                cmd.Parameters.AddWithValue("@Date", SqlDbType.Date).Value = Date1.Text;
                cmd.Parameters.AddWithValue("@Day", a);
                cmd.Parameters.AddWithValue("@disc", dis.Text.ToString());
                cmd.Parameters.AddWithValue("@updatedby", 1);


                conn.Open();
                cmd.ExecuteNonQuery();
                Response.Redirect("GazatedForm.aspx");

                conn.Close();
            }
            catch
            {
                Response.Write("<script>alert ('ALREADY EXIST');</script>");
            }





        }


        protected void Button1_Click(object sender, EventArgs e)
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            DateTime d = new DateTime();
            d = Convert.ToDateTime(Date1.Text);
            string a = d.DayOfWeek.ToString();


            string Query = "update holidays set Date='" + this.Date1.Text + "',Day='" + a.ToString() + "',disc='" + this.dis.Text + "' where Holi_id='" + this.GridView1.SelectedDataKey.Value + "';";

            SqlCommand cmd = new SqlCommand(Query, conn);


            conn.Open();
            cmd.ExecuteNonQuery();
            Response.Redirect("GazatedForm.aspx");

            conn.Close();
            Date1.Text = dis.Text = null;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // day.Text = Convert.ToString(GridView1.SelectedRow.Cells[0].Text);
            // dis.Text = Convert.ToString(GridView1.SelectedDataKey.Value);
            Date1.Text = Convert.ToString(GridView1.SelectedRow.Cells[1].Text);
            dis.Text = Convert.ToString(GridView1.SelectedRow.Cells[3].Text);
            //DropDownList1.Text = Convert.ToString(GridView1.SelectedRow.Cells[3].Text);
            Button1.Visible = true;
            b1.Visible = false;
        }
    }
}