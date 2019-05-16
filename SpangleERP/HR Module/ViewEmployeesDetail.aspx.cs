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
using System.Text.RegularExpressions;

namespace SpangleERP.HR_Module
{
    public partial class ViewEmployeesDetail : System.Web.UI.Page
    {
     public static  int? emp_id;
        public static int? upd_id;
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
            Bound_Type();
            Bound_Depart();
            Bound_Packages();
            Bound_Edu();

//            Label1.Text = "Displaying Page" + (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();


        }


        public void Bound_Type()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand cmd = new SqlCommand(@"select * from Emp_Type", conn);

            conn.Open();
            DropDownList1.DataSource = cmd.ExecuteReader();
            DropDownList1.DataTextField = "type_name";
            DropDownList1.DataValueField = "emp_Type";
            DropDownList1.DataBind();

            conn.Close();
            conn.Dispose();

        }

        public void Button2_Click(object sender,EventArgs e)
        {
            

        }

        public void Bound_Packages()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand cmd = new SqlCommand(@"select p.pack_id,p.p_name from Packages as p", conn);

            conn.Open();
            DropDownList3.DataSource = cmd.ExecuteReader();
            DropDownList3.DataTextField = "p_name";
            DropDownList3.DataValueField = "pack_id";
            DropDownList3.DataBind();

            conn.Close();
            conn.Dispose();

        }

        public void Bound_Edu()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand cmd = new SqlCommand(@"select * from Education", conn);

            conn.Open();
            DropDownList4.DataSource = cmd.ExecuteReader();
            DropDownList4.DataTextField = "degree";
            DropDownList4.DataValueField = "edu_id";
            DropDownList4.DataBind();

            conn.Close();
            conn.Dispose();

        }



        public void Bound_Depart()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand cmd = new SqlCommand(@"select * from Department", conn);

            conn.Open();
            DropDownList2.DataSource = cmd.ExecuteReader();
            DropDownList2.DataTextField = "dep_name";
            DropDownList2.DataValueField = "dep_id";
            DropDownList2.DataBind();

            conn.Close();
            conn.Dispose();

        }


        [WebMethod]
        public static string emp(string fname, string lname, string doj, string dob, string desig, string father, string lno, string nic, string mobile, string gender, string cur, string pur, string exp, string bnk, string emr, string pid, string type_id, string edu_id, string dep_id,string job_type)
        {

            try
            {

                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd1 = new SqlCommand(@"insert into Employee OUTPUT inserted.emp_id values(@name,@cnic,@dob,@licn,@mobile,@cur,@pur,@doj,@dep_id,@packid,@contr,@eduid,@gender,@bnk,@emp_type,@hr,@father,@emrg,@exp,@Img,@status)", conn);
                cmd1.Parameters.AddWithValue("@name", fname + " " + lname);
                cmd1.Parameters.AddWithValue("@cnic", nic);
                cmd1.Parameters.AddWithValue("@dob", SqlDbType.Date).Value = dob;
                cmd1.Parameters.AddWithValue("@licn", lno);
                cmd1.Parameters.AddWithValue("@mobile", Convert.ToInt32(mobile));
                cmd1.Parameters.AddWithValue("@cur", cur);
                cmd1.Parameters.AddWithValue("@pur", pur);
                cmd1.Parameters.AddWithValue("@doj", SqlDbType.Date).Value = doj;
                cmd1.Parameters.AddWithValue("@dep_id", Convert.ToInt32(dep_id));
                cmd1.Parameters.AddWithValue("@packid", Convert.ToInt32(pid));
                cmd1.Parameters.AddWithValue("@contr", job_type);
                cmd1.Parameters.AddWithValue("@eduid", Convert.ToInt32(edu_id));
                cmd1.Parameters.AddWithValue("@gender", gender);
                cmd1.Parameters.AddWithValue("@bnk", bnk);
                cmd1.Parameters.AddWithValue("@emp_type", Convert.ToInt32(type_id));
                cmd1.Parameters.AddWithValue("@hr", desig);
                cmd1.Parameters.AddWithValue("@father", father);
                cmd1.Parameters.AddWithValue("@emrg", emr);
                cmd1.Parameters.AddWithValue("@exp", Convert.ToInt32(exp));
                cmd1.Parameters.AddWithValue("@Img", "");
                cmd1.Parameters.AddWithValue("@status", 1);
                conn.Open();
                emp_id = Convert.ToInt32(cmd1.ExecuteScalar());

                conn.Close();
                conn.Dispose();
                // ViewEmployeesDetail obj = new ViewEmployeesDetail();

                return "Save";


            }
            catch(Exception ex)
            {
                return "Some Error " + ex.Message;
            }



        }


        [WebMethod]
        public static string Update(string fname, string lname, string doj, string dob, string desig, string father, string lno, string nic, string mobile, string gender, string cur, string pur, string exp, string bnk, string emr, string pid, string type_id, string edu_id, string dep_id,string eid,string jobtype)
        {

            try
            {


                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd1 = new SqlCommand(@"
update Employee set emp_name=@name,cnic=@cnic,dob=@dob,license=@licn,
mobile=@mobile,currant_address=@cur,permenant_address=@pur,experience=@exp,date_of_joining=@doj,
dep_id=@dep_id,salary_pack=@packid,contract=@contr,edu_id=@eduid,gender=@gender,bankAccount=@bnk,Emp_type=@emp_type,
Designation=@hr,f_name=@father,emer_number=@emrg where emp_id=@emp_id", conn);
                cmd1.Parameters.AddWithValue("@name", fname + " " + lname);
                cmd1.Parameters.AddWithValue("@cnic", nic);
                cmd1.Parameters.AddWithValue("@dob", SqlDbType.Date).Value = dob;
                cmd1.Parameters.AddWithValue("@licn", lno);
                cmd1.Parameters.AddWithValue("@mobile", Convert.ToInt32(mobile));
                cmd1.Parameters.AddWithValue("@cur", cur);
                cmd1.Parameters.AddWithValue("@pur", pur);
                cmd1.Parameters.AddWithValue("@doj", SqlDbType.Date).Value = doj;
                cmd1.Parameters.AddWithValue("@dep_id", Convert.ToInt32(dep_id));
                cmd1.Parameters.AddWithValue("@packid", Convert.ToInt32(pid));
                cmd1.Parameters.AddWithValue("@contr", jobtype);
                cmd1.Parameters.AddWithValue("@eduid", Convert.ToInt32(edu_id));
                cmd1.Parameters.AddWithValue("@gender", gender);
                cmd1.Parameters.AddWithValue("@bnk", bnk);
                cmd1.Parameters.AddWithValue("@emp_type", Convert.ToInt32(type_id));
                cmd1.Parameters.AddWithValue("@hr", desig);
                cmd1.Parameters.AddWithValue("@father", father);
                cmd1.Parameters.AddWithValue("@emrg", emr);
                cmd1.Parameters.AddWithValue("@exp", Convert.ToInt32(exp));
                cmd1.Parameters.AddWithValue("@emp_id", Convert.ToInt32(eid));

                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                // ViewEmployeesDetail obj = new ViewEmployeesDetail();
                upd_id = Convert.ToInt32(eid);
                return "Save";



            }
            catch (Exception ex)
            {
                return "" + ex.Message;
            }



        }




        [WebMethod]
        public static List<All_Employees> GetEditData(string eid)
        {
            List<All_Employees> list_det = new List<All_Employees>();


            DataTable dt = get_all_products(Convert.ToInt32 (eid));
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                All_Employees p = new All_Employees();

                p.emp_name = Convert.ToString(dt.Rows[i]["emp_name"]);
                p.mobile = Convert.ToString(dt.Rows[i]["mobile"]);
                p.Designation = Convert.ToString(dt.Rows[i]["Designation"]);
                p.Img = Convert.ToString(dt.Rows[i]["Img"]);
                p.father = Convert.ToString(dt.Rows[i]["f_name"]);
                p.cnic= Convert.ToString(dt.Rows[i]["cnic"]);
                p.license = Convert.ToString(dt.Rows[i]["license"]);
                p.curadd= Convert.ToString(dt.Rows[i]["currant_address"]);
                p.per_add = Convert.ToString(dt.Rows[i]["permenant_address"]);
                p.emer_num = Convert.ToString(dt.Rows[i]["emer_number"]);
                p.exp = Convert.ToString(dt.Rows[i]["experience"]);
                p.bankAccount = Convert.ToString(dt.Rows[i]["bankAccount"]);



                list_det.Add(p);
            }
            return list_det;

        }

        private static DataTable get_all_products(int eid)
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select * from Employee where emp_id='"+eid+"'", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }

       

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.PostedFile != null && FileUpload1.HasFile)
                {
                    if (emp_id == null && upd_id == null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Employee First ')", true);

                    }
                    else
                    {
                        if (emp_id != null && upd_id == null)
                        {

                            string imgFile2 = Path.GetFileName(FileUpload1.PostedFile.FileName);

                            FileUpload1.SaveAs(Server.MapPath("Images/" + imgFile2));
                            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

                            SqlConnection conn = new SqlConnection(con1);
                            SqlCommand cmd1 = new SqlCommand("update Employee set Img = @img where emp_id = @id", conn);
                            cmd1.Parameters.AddWithValue("@img", "Images/" + imgFile2.ToString());
                            cmd1.Parameters.AddWithValue("@id", emp_id);


                            conn.Open();
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                            conn.Dispose();
                            emp_id = null;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Save Employee Data')", true);
                        }
                        else if(upd_id!=null && emp_id == null)
                        {
                            string imgFile2 = Path.GetFileName(FileUpload1.PostedFile.FileName);

                            FileUpload1.SaveAs(Server.MapPath("Images/" + imgFile2));
                            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;

                            SqlConnection conn = new SqlConnection(con1);
                            SqlCommand cmd1 = new SqlCommand("update Employee set Img = @img where emp_id = @id", conn);
                            cmd1.Parameters.AddWithValue("@img", "Images/" + imgFile2.ToString());
                            cmd1.Parameters.AddWithValue("@id", upd_id);


                            conn.Open();
                            cmd1.ExecuteNonQuery();
                            conn.Close();
                            conn.Dispose();
                            upd_id = null;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Save Employee Data')", true);

                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Image First ')", true);

                }
            }
            catch (SqlException ex)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error : '+'" + ex.Message + "')", true);

            }


        }

        

        [WebMethod]
        public static List<All_Employees> GetPrint_Data(string eid)
        {
            List<All_Employees> list_det = new List<All_Employees>();


            DataTable dt = getprint(Convert.ToInt32(eid));
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                All_Employees p = new All_Employees();

                p.emp_name = Convert.ToString(dt.Rows[i]["emp_name"]);
               p.father= Convert.ToString(dt.Rows[i]["f_name"]);
                p.curadd = Convert.ToString(dt.Rows[i]["currant_address"]);
                p.per_add = Convert.ToString(dt.Rows[i]["permenant_address"]);
                p.cnic = Convert.ToString(dt.Rows[i]["cnic"]);
                p.mobile = Convert.ToString(dt.Rows[i]["mobile"]);
                p.gender = Convert.ToString(dt.Rows[i]["gender"]);
                p.dep_name = Convert.ToString(dt.Rows[i]["dep_name"]);
                p.exp = Convert.ToString(dt.Rows[i]["experience"]);
                p.emp_id= Convert.ToInt32(dt.Rows[i]["total"]);
                p.Designation = Convert.ToString(dt.Rows[i]["Designation"]);
                p.DOB= Convert.ToString(dt.Rows[i]["dob"]);
                p.license = Convert.ToString(dt.Rows[i]["license"]);
                p.degree = Convert.ToString(dt.Rows[i]["degree"]);
                p.emp_type = Convert.ToString(dt.Rows[i]["type_name"]);
                p.bankAccount = Convert.ToString(dt.Rows[i]["bankAccount"]);
                p.Img = Convert.ToString(dt.Rows[i]["Img"]);
                p.emer_num = Convert.ToString(dt.Rows[i]["emer_number"]);
                p.job_type = Convert.ToString(dt.Rows[i]["contract"]);

                list_det.Add(p);
            }
            return list_det;

        }

        private static DataTable getprint(int eid)
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"
select e.emp_name,e.f_name, e.currant_address,e.permenant_address,cnic,e.mobile,e.gender,d.dep_name,e.experience,p.total,e.Designation
,e.dob,e.license,ed.degree,type.type_name,e.bankAccount,e.contract,e.Img,e.emer_number
  from Employee as e
  left join Department as d
  on d.dep_id=e.dep_id
  left join Education as ed
  on ed.edu_id=e.edu_id
  left join Emp_Type as type
  on type.emp_Type=e.Emp_type
  left join Packages as p
  on p.pack_id=e.salary_pack where e.emp_id='"+eid+"'", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }




    }

}
