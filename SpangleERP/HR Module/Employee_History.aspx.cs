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

    public partial class Employee_History : System.Web.UI.Page
    {
        public static int id;
        public static int his_id=0;
        public static string path = "";
        public static string extex = "";

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

            Access = PageName().ToString();
        }

        public string PageName()
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
 where u.User_id=@id and  p.URl=@pname", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@pname", sRet);

                conn.Open();
                string level = cmd.ExecuteScalar().ToString();
                conn.Close();


                return level;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }


        }


        [WebMethod]
        public static string Access_Levels()
        {

            return Access;
        }


        [WebMethod]
        public static string Download(string hid,string Extension)
        {



            path = hid;
            extex = Extension;
            return "Done";
        }

        //public string  File_Download(string path)
        //{
        //    string reason="Reason";
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.ContentType = "application/octect-stream";
        //    HttpContext.Current.Response.AppendHeader("content-disposition","filename="+reason);
        //    HttpContext.Current.Response.TransmitFile(Server.MapPath("Document/rahat.pdf"));
        //    HttpContext.Current.Response.End();
        //    return "Done";

        //}

        [WebMethod]
        public static string Insert(string eid,string Date, string Reason)
        {

            try
            {



                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd = new SqlCommand("select count(*) from Emp_History where emp_id=@eid",conn);
                cmd.Parameters.AddWithValue("@eid",Convert.ToInt32(eid));

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                if (count == 0)
                {
                    SqlCommand cmd1 = new SqlCommand(@"insert into Emp_History OUTPUT inserted.hist_id values(@date,@id,@rea,@sessionid,@status,@document)", conn);
                    cmd1.Parameters.AddWithValue("@date", SqlDbType.Date).Value = Date;
                    cmd1.Parameters.AddWithValue("@id", Convert.ToInt32(eid));
                    cmd1.Parameters.AddWithValue("@rea", Reason);
                    cmd1.Parameters.AddWithValue("@sessionid", id);
                    cmd1.Parameters.AddWithValue("@status", Convert.ToInt32(0));
                    cmd1.Parameters.AddWithValue("@document", DBNull.Value);

                    conn.Open();
                    his_id = Convert.ToInt32(cmd1.ExecuteScalar());

                    conn.Close();
                    conn.Dispose();

                    return "Save";
                }
                else
                {
                    return "This Employee Fired";
                }

            }
            catch (Exception ex)
            {
                return "Some Error " + ex.Message;
            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile != null && FileUpload1.HasFile)
            {
                if (his_id == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Employee First ')", true);

                }
                else
                {


                    string imgFile2 = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    
                    FileUpload1.SaveAs(Server.MapPath("Document/" + imgFile2));
                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd1 = new SqlCommand("update Emp_History set documrnt=@document where hist_id=@hid", conn);
                    cmd1.Parameters.AddWithValue("@document", "Document/" + imgFile2.ToString());
                    cmd1.Parameters.AddWithValue("@hid", his_id);


                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    his_id = 0;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Save Document')", true);

                }


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please Select The File First')", true);

            }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (path != "" && extex!="")
            {

                HttpContext.Current.Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename=Reason."+extex);
                Response.TransmitFile(Server.MapPath("" + path));
                Response.End();
                path = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please Select The File First')", true);

            }

        }



        //protected void Button1_Click(object sender, EventArgs e)
        //{

        //   
        //}

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //   

        //}


    }
    }
