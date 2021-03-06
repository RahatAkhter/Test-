﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
namespace SpangleERP.HR_Module
{
    public partial class All_Users : System.Web.UI.Page
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
            Bound();
           Access= PageName().ToString();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(''+'" + Access + "')", true);


        }


        public string  PageName()
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
 where u.User_id=@id and  p.URl=@pname",conn);
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters.AddWithValue("@pname",sRet);

                conn.Open();
                string level = cmd.ExecuteScalar().ToString();
                conn.Close();
                

                return level;
            }
            catch(Exception ex)
            {
                return ex.Message;

            }

              
        }

        public void Bound()
        {
            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand cmd = new SqlCommand(@"Select * from Roles", conn);

            conn.Open();
            DropDownList1.DataSource = cmd.ExecuteReader();
            DropDownList1.DataTextField = "Role_name";
            DropDownList1.DataValueField = "Role_id";
            DropDownList1.DataBind();

            conn.Close();
           

            SqlCommand cmd1 = new SqlCommand(@"Select * from Roles", conn);

            conn.Open();
            DropDownList2.DataSource = cmd1.ExecuteReader();
            DropDownList2.DataTextField = "Role_name";
            DropDownList2.DataValueField = "Role_id";
            DropDownList2.DataBind();

            conn.Close();
            conn.Dispose();



        }

        [WebMethod]
        public static string Access_Levels()
        {

            return Access;
        }


        [WebMethod]
        public static string Update(string uid,string pass,string Role)
        {
            try
            {


                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd1 = new SqlCommand("update Users Set password=@pass, Role=@role where User_id=@uid", conn);
                cmd1.Parameters.AddWithValue("@uid", Convert.ToInt32(uid));
                cmd1.Parameters.AddWithValue("@pass", pass);
                cmd1.Parameters.AddWithValue("@role", Convert.ToInt32(Role));

                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();
                return "Save";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }

        }

        [WebMethod]
        public static string Insert(string emp_id,string email,string password,string Role)
        {
            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd1 = new SqlCommand("Select Count(*) from Users where emp_id=@id",conn);
                cmd1.Parameters.AddWithValue("@id",Convert.ToInt32(emp_id));
                conn.Open();
                int count = Convert.ToInt32(cmd1.ExecuteScalar());
                conn.Close();
                if (count == 0)
                {


                    SqlCommand cmd = new SqlCommand("insert into Users values(@email,@pass,@Role,@emp_id,0)", conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pass", password);
                    cmd.Parameters.AddWithValue("@Role", Convert.ToInt32(Role));
                    cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(emp_id));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "Save";
                }
                else
                {
                    return "Not";
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

    }
}