using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Net.Mail;
namespace SpangleERP
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string Signin(string name,string pass)
        {

            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@"select count(*) from Users where Email=@email and password=@pass", conn);
                // cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                cmd.Parameters.AddWithValue("@email",name);
                cmd.Parameters.AddWithValue("@pass", pass);

                conn.Open();
                int Count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                if (Count == 0)
                {
                    return "Incorrect".ToString();
                }
                else
                {
                    
                    
                        SqlCommand cmd2 = new SqlCommand("select User_id from Users where email=@email and password=@pass",conn);
                        cmd2.Parameters.AddWithValue("@email",name);
                        cmd2.Parameters.AddWithValue("@pass",pass);

                        conn.Open();
                        int id = Convert.ToInt32(cmd2.ExecuteScalar());
                        conn.Close();
                       HttpContext.Current.Session["id"] = id.ToString();
                       
                        return "ok";

                        
                    
                }
               
            }
            catch(Exception ex)
            {
                return ""+ ex.ToString();
            }
        }


        [WebMethod]
        public static string Send_Pass(string Email)
        {
            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

                Random random = new Random();
                const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
                var builder = new StringBuilder();

                for (var i = 0; i < 8; i++)
                {
                    var c = pool[random.Next(0, pool.Length)];
                    builder.Append(c);
                }

                SqlConnection con = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand("select Count(*) from Users where email=@email ", con);
                cmd.Parameters.AddWithValue("@email", Email);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                if (count != 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update  Users set Password=@pass where Email=@email", con);
                    cmd1.Parameters.AddWithValue("@email", Email);
                    cmd1.Parameters.AddWithValue("@pass", builder.ToString());
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    SqlCommand cmd2 = new SqlCommand(@"
 select e.emp_name
 from Users as u
 left join Employee as e
 on e.emp_id = u.emp_id
 where u.Email = @email", con);
                    cmd2.Parameters.AddWithValue("@email", Email);
                    con.Open();
                    string User_Name = cmd2.ExecuteScalar().ToString();
                    con.Close();
                    con.Dispose();
                    sendcode(Email, User_Name, builder.ToString());
                    return "Send";
                }
                else
                {

                    return "Not";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }


        private static void sendcode(string email, string fname, string pass)
        {

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.zoho.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("info@spanglepk.com", "Karachi1@");
            smtp.EnableSsl = true;
            MailMessage msg = new MailMessage();
            msg.Subject = "Reset Password";
            msg.Body = "Dear " + fname.ToString() + " Your New Password Is  : " + pass.ToString() + "\n\n\nThank you & Regars Spangle.pk Team";
            string toaddress = email.ToString();
            msg.To.Add(toaddress);
            string fromadd = "info@spanglepk.com";
            msg.From = new MailAddress(fromadd);

            try
            {
                smtp.Send(msg);

            }
            catch
            {
                throw;

            }
        }

    }
}