using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
namespace SpangleERP.HR_Module
{
    public partial class Department : System.Web.UI.Page
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
        }
        [WebMethod]
        public static string Save(string name,string des)
        {
            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd = new SqlCommand(@" select Count(*) from Department where dep_name=@name",conn);
                cmd.Parameters.AddWithValue("@name",name);
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                if(count==0)
                {
                    SqlCommand cmd1 = new SqlCommand(" Insert into Department values(@name,@des)",conn);

                    cmd1.Parameters.AddWithValue("@name",name);
                    cmd1.Parameters.AddWithValue("@des", des);

                    conn.Open();

                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    return "Save Successfully";

                }
                else
                {
                    return "Department Name Already Exist";
                }



            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            
        }


        [WebMethod]
        public static string Update(string name, string des,string did)
        {
            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd = new SqlCommand(@" select Count(*) from Department where dep_name=@name", conn);
                cmd.Parameters.AddWithValue("@name", name);
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                if (count == 0)
                {
                    SqlCommand cmd1 = new SqlCommand("update Department set dep_name=@name , dep_discription=@desc where dep_id=@did", conn);

                    cmd1.Parameters.AddWithValue("@name", name);
                    cmd1.Parameters.AddWithValue("@desc", des);
                    cmd1.Parameters.AddWithValue("@did", Convert.ToInt32(did));


                    conn.Open();

                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    return "Update Successfully";

                }
                else
                {
                    return "Department Name Already Exist";
                }



            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }


        [WebMethod]
        public static ClsDepartments GetEditData(string Val)
        {

            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd = new SqlCommand(@"select dep_name,dep_discription from Department where dep_id=@id",conn);
                cmd.Parameters.AddWithValue("@id",Convert.ToInt32(Val));

                conn.Open();
                SqlDataReader rea = cmd.ExecuteReader();
                ClsDepartments obj = new ClsDepartments();
                while (rea.Read())
                {
                    obj.dep_name = rea["dep_name"].ToString();
                    obj.desc= rea["dep_discription"].ToString();
                }
                return obj;
            }
            catch (Exception ex)
            {
                ClsDepartments dep = new ClsDepartments();
                dep.dep_name=""+ex.Message;
                return dep;

            }

        }
    }
}