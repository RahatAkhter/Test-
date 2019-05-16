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
    public partial class UpdateShift : System.Web.UI.Page
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

            Bound();
        }

public void Bound()
        {


            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand cmd = new SqlCommand(@"select  CONCAT(s_time, ' to ', e_time) sh, shift_id from Shifts", conn);

            conn.Open();
            DropDownList1.DataSource = cmd.ExecuteReader();
            DropDownList1.DataTextField = "sh";
            DropDownList1.DataValueField = "shift_id";
            DropDownList1.DataBind();

            conn.Close();


            conn.Open();
            DropDownList2.DataSource = cmd.ExecuteReader();
            DropDownList2.DataTextField = "sh";
            DropDownList2.DataValueField = "shift_id";
            DropDownList2.DataBind();

            conn.Close();



            conn.Open();
            DropDownList3.DataSource = cmd.ExecuteReader();
            DropDownList3.DataTextField = "sh";
            DropDownList3.DataValueField = "shift_id";
            DropDownList3.DataBind();

            conn.Close();



            conn.Open();
            DropDownList4.DataSource = cmd.ExecuteReader();
            DropDownList4.DataTextField = "sh";
            DropDownList4.DataValueField = "shift_id";
            DropDownList4.DataBind();

            conn.Close();


            conn.Open();
            DropDownList5.DataSource = cmd.ExecuteReader();
            DropDownList5.DataTextField = "sh";
            DropDownList5.DataValueField = "shift_id";
            DropDownList5.DataBind();

            conn.Close();



            conn.Open();
            DropDownList6.DataSource = cmd.ExecuteReader();
            DropDownList6.DataTextField = "sh";
            DropDownList6.DataValueField = "shift_id";
            DropDownList6.DataBind();

            conn.Close();


            conn.Open();
            DropDownList7.DataSource = cmd.ExecuteReader();
            DropDownList7.DataTextField = "sh";
            DropDownList7.DataValueField = "shift_id";
            DropDownList7.DataBind();

            conn.Close();


        }
        [WebMethod]
        public static string Insert(string mon,string tue,string wed,string thu,string fri,string sat,string sun,string id)
        {

            try
            {


                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd = new SqlCommand("insert into Emp_Shifts values(@id,@mon,@tue,@wed,@thu,@fri,@sat,@sun)", conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                cmd.Parameters.AddWithValue("@mon", Convert.ToInt32(mon));
                cmd.Parameters.AddWithValue("@tue", Convert.ToInt32(tue));
                cmd.Parameters.AddWithValue("@wed", Convert.ToInt32(wed));
                cmd.Parameters.AddWithValue("@thu", Convert.ToInt32(thu));
                cmd.Parameters.AddWithValue("@fri", Convert.ToInt32(fri));
                cmd.Parameters.AddWithValue("@sat", Convert.ToInt32(sat));
                cmd.Parameters.AddWithValue("@sun", Convert.ToInt32(sun));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Save";
            }
            catch(Exception ex)
            {
                return "" + ex;

            }
            }

        [WebMethod]
        public static string Update(string mon, string tue, string wed, string thu, string fri, string sat, string sun, string id)
        {

            try
            {


                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd = new SqlCommand(@"update Emp_Shifts set monday=@mon,tuesday=@tue,wednesday=@wed,thursday=@thu,friday=@fri,saturday=@sat,sunday=@sun where emp_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                cmd.Parameters.AddWithValue("@mon", Convert.ToInt32(mon));
                cmd.Parameters.AddWithValue("@tue", Convert.ToInt32(tue));
                cmd.Parameters.AddWithValue("@wed", Convert.ToInt32(wed));
                cmd.Parameters.AddWithValue("@thu", Convert.ToInt32(thu));
                cmd.Parameters.AddWithValue("@fri", Convert.ToInt32(fri));
                cmd.Parameters.AddWithValue("@sat", Convert.ToInt32(sat));
                cmd.Parameters.AddWithValue("@sun", Convert.ToInt32(sun));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Save";
            }
            catch (Exception ex)
            {
                return "" + ex;

            }
        }


    }
    }
