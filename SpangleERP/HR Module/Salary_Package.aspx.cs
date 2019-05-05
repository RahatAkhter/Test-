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
    public partial class Salary_Package : System.Web.UI.Page
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
                Response.Redirect("~/Login.aspx");
            }
            Label1.Text = "Displaying Page" + (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();

        }
        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            Label1.Text = "Displaying Page" + (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();

        }



        [WebMethod]
        public static string Update(string pname, string basic, string mobile, string petrol, string Lunch, string HouseRent, string Medical, string motor_car,string pid,string Ot,string utility,string driverFuel,string bonus)
        {
            try
            {

                int total = Convert.ToInt32(basic) + Convert.ToInt32(mobile) + Convert.ToInt32(petrol) + Convert.ToInt32(Lunch) + Convert.ToInt32(HouseRent) + Convert.ToInt32(Medical) + Convert.ToInt32(motor_car) + Convert.ToInt32(utility) + Convert.ToInt32(driverFuel);
                if (total < 400000)
                {
                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd1 = new SqlCommand(@"update Packages set basic_salary=@basic,petrol=@petrol,mobile=@mobile,lunch=@lunch,Utility=@ut,DriverFuel=@df,motor_car=@car,medical=@medical,house_rent=@rent,total=@total,p_name=@pname,Overtime=@ot,IncomeTax=@It,Bonus=@bonus where pack_id=@pid", conn);
                    cmd1.Parameters.AddWithValue("@pname", pname);
                    cmd1.Parameters.AddWithValue("@basic", Convert.ToInt32(basic));
                    cmd1.Parameters.AddWithValue("@petrol", Convert.ToInt32(petrol));
                    cmd1.Parameters.AddWithValue("@mobile", Convert.ToInt32(mobile));
                    cmd1.Parameters.AddWithValue("@lunch", Convert.ToInt32(Lunch));
                    cmd1.Parameters.AddWithValue("@car", Convert.ToInt32(motor_car));
                    cmd1.Parameters.AddWithValue("@medical", Convert.ToInt32(Medical));
                    cmd1.Parameters.AddWithValue("@rent", Convert.ToInt32(HouseRent));
                    cmd1.Parameters.AddWithValue("@total", Convert.ToInt32(total));
                    cmd1.Parameters.AddWithValue("@pid", Convert.ToInt32(pid));
                    cmd1.Parameters.AddWithValue("@ot", Convert.ToInt32(Ot));
                    cmd1.Parameters.AddWithValue("@It", Convert.ToInt32(0));
                    cmd1.Parameters.AddWithValue("@ut", Convert.ToInt32(utility));
                    cmd1.Parameters.AddWithValue("@df", Convert.ToInt32(driverFuel));
                    cmd1.Parameters.AddWithValue("@bonus", Convert.ToInt32(bonus));
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();


                    return "Update";

                }
                else if (total >= 400000 && total <= 800000)
                {
                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd1 = new SqlCommand(@"update Packages set basic_salary=@basic,petrol=@petrol,mobile=@mobile,lunch=@lunch,Utility=@ut,DriverFuel=@df,motor_car=@car,medical=@medical,house_rent=@rent,total=@total,p_name=@pname,Overtime=@ot,IncomeTax=@It,Bonus=@bonus where pack_id=@pid", conn);
                    cmd1.Parameters.AddWithValue("@pname", pname);
                    cmd1.Parameters.AddWithValue("@basic", Convert.ToInt32(basic));
                    cmd1.Parameters.AddWithValue("@petrol", Convert.ToInt32(petrol));
                    cmd1.Parameters.AddWithValue("@mobile", Convert.ToInt32(mobile));
                    cmd1.Parameters.AddWithValue("@lunch", Convert.ToInt32(Lunch));
                    cmd1.Parameters.AddWithValue("@car", Convert.ToInt32(motor_car));
                    cmd1.Parameters.AddWithValue("@medical", Convert.ToInt32(Medical));
                    cmd1.Parameters.AddWithValue("@rent", Convert.ToInt32(HouseRent));
                    cmd1.Parameters.AddWithValue("@total", Convert.ToInt32(total));
                    cmd1.Parameters.AddWithValue("@pid", Convert.ToInt32(pid));
                    cmd1.Parameters.AddWithValue("@ot", Convert.ToInt32(Ot));
                    cmd1.Parameters.AddWithValue("@It", 83.33);
                    cmd1.Parameters.AddWithValue("@ut", Convert.ToInt32(utility));
                    cmd1.Parameters.AddWithValue("@df", Convert.ToInt32(driverFuel));
                    cmd1.Parameters.AddWithValue("@bonus", Convert.ToInt32(bonus));
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();


                    return "Update";
                }

                else if (total > 800000 && total <= 1200000)
                {
                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd1 = new SqlCommand(@"update Packages set basic_salary=@basic,petrol=@petrol,mobile=@mobile,lunch=@lunch,Utility=@ut,DriverFuel=@df,motor_car=@car,medical=@medical,house_rent=@rent,total=@total,p_name=@pname,Overtime=@ot,IncomeTax=@It,Bonus=@bonus where pack_id=@pid", conn);
                    cmd1.Parameters.AddWithValue("@pname", pname);
                    cmd1.Parameters.AddWithValue("@basic", Convert.ToInt32(basic));
                    cmd1.Parameters.AddWithValue("@petrol", Convert.ToInt32(petrol));
                    cmd1.Parameters.AddWithValue("@mobile", Convert.ToInt32(mobile));
                    cmd1.Parameters.AddWithValue("@lunch", Convert.ToInt32(Lunch));
                    cmd1.Parameters.AddWithValue("@car", Convert.ToInt32(motor_car));
                    cmd1.Parameters.AddWithValue("@medical", Convert.ToInt32(Medical));
                    cmd1.Parameters.AddWithValue("@rent", Convert.ToInt32(HouseRent));
                    cmd1.Parameters.AddWithValue("@total", Convert.ToInt32(total));
                    cmd1.Parameters.AddWithValue("@pid", Convert.ToInt32(pid));
                    cmd1.Parameters.AddWithValue("@ot", Convert.ToInt32(Ot));
                    cmd1.Parameters.AddWithValue("@It", 166.66);
                    cmd1.Parameters.AddWithValue("@ut", Convert.ToInt32(utility));
                    cmd1.Parameters.AddWithValue("@df", Convert.ToInt32(driverFuel));
                    cmd1.Parameters.AddWithValue("@bonus", Convert.ToInt32(bonus));
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();


                    return "Update";

                }

                else if (total > 1200000)
                {
                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd1 = new SqlCommand(@"update Packages set basic_salary=@basic,petrol=@petrol,mobile=@mobile,lunch=@lunch,Utility=@ut,DriverFuel=@df,motor_car=@car,medical=@medical,house_rent=@rent,total=@total,p_name=@pname,Overtime=@ot,IncomeTax=@It,Bonus=@bonus where pack_id=@pid", conn);
                    cmd1.Parameters.AddWithValue("@pname", pname);
                    cmd1.Parameters.AddWithValue("@basic", Convert.ToInt32(basic));
                    cmd1.Parameters.AddWithValue("@petrol", Convert.ToInt32(petrol));
                    cmd1.Parameters.AddWithValue("@mobile", Convert.ToInt32(mobile));
                    cmd1.Parameters.AddWithValue("@lunch", Convert.ToInt32(Lunch));
                    cmd1.Parameters.AddWithValue("@car", Convert.ToInt32(motor_car));
                    cmd1.Parameters.AddWithValue("@medical", Convert.ToInt32(Medical));
                    cmd1.Parameters.AddWithValue("@rent", Convert.ToInt32(HouseRent));
                    cmd1.Parameters.AddWithValue("@total", Convert.ToInt32(total));
                    cmd1.Parameters.AddWithValue("@pid", Convert.ToInt32(pid));
                    cmd1.Parameters.AddWithValue("@ot", Convert.ToInt32(Ot));
                    cmd1.Parameters.AddWithValue("@It", 5.00);
                    cmd1.Parameters.AddWithValue("@ut", Convert.ToInt32(utility));
                    cmd1.Parameters.AddWithValue("@df", Convert.ToInt32(driverFuel));
                    cmd1.Parameters.AddWithValue("@bonus", Convert.ToInt32(bonus));
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();


                    return "Update";
                }

                return "Nor Updated";

            }
            catch(Exception ex)
            {
                return "Some Error";
            }
        }





        [WebMethod]
        public static string Insert(string pname, string basic, string mobile, string petrol, string Lunch, string HouseRent, string Medical, string motor_car,string utility,string df,string Ot,string bonus)
        {
            try
            {
                int total = Convert.ToInt32(basic) + Convert.ToInt32(mobile) + Convert.ToInt32(petrol) + Convert.ToInt32(Lunch) + Convert.ToInt32(HouseRent) + Convert.ToInt32(Medical) + Convert.ToInt32(motor_car) + Convert.ToInt32(utility) + Convert.ToInt32(df);
                if (total < 400000)
                {

                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd1 = new SqlCommand(@"insert into Packages values(@basic,@petrol,@mobile,@lunch,@car,@medical,@rent,@total,@pname,@ut,@df,@Ot,@incometax,@bonus)", conn);
                    cmd1.Parameters.AddWithValue("@pname", pname);
                    cmd1.Parameters.AddWithValue("@basic", Convert.ToInt32(basic));
                    cmd1.Parameters.AddWithValue("@petrol", Convert.ToInt32(petrol));
                    cmd1.Parameters.AddWithValue("@mobile", Convert.ToInt32(mobile));
                    cmd1.Parameters.AddWithValue("@lunch", Convert.ToInt32(Lunch));
                    cmd1.Parameters.AddWithValue("@car", Convert.ToInt32(motor_car));
                    cmd1.Parameters.AddWithValue("@medical", Convert.ToInt32(Medical));
                    cmd1.Parameters.AddWithValue("@rent", Convert.ToInt32(HouseRent));
                    cmd1.Parameters.AddWithValue("@total", Convert.ToInt32(total));
                    cmd1.Parameters.AddWithValue("@ut", Convert.ToInt32(utility));
                    cmd1.Parameters.AddWithValue("@df", Convert.ToInt32(df));
                    cmd1.Parameters.AddWithValue("@Ot", Convert.ToInt32(Ot));
                    cmd1.Parameters.AddWithValue("@incometax", 0);
                    cmd1.Parameters.AddWithValue("@bonus", Convert.ToInt32(bonus));
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();


                    return "Save";

                }
                else if (total >= 400000 && total <= 800000)
                {


                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd1 = new SqlCommand(@"insert into Packages values(@basic,@petrol,@mobile,@lunch,@car,@medical,@rent,@total,@pname,@ut,@df,@Ot,@incometax,@bonus)", conn);
                    cmd1.Parameters.AddWithValue("@pname", pname);
                    cmd1.Parameters.AddWithValue("@basic", Convert.ToInt32(basic));
                    cmd1.Parameters.AddWithValue("@petrol", Convert.ToInt32(petrol));
                    cmd1.Parameters.AddWithValue("@mobile", Convert.ToInt32(mobile));
                    cmd1.Parameters.AddWithValue("@lunch", Convert.ToInt32(Lunch));
                    cmd1.Parameters.AddWithValue("@car", Convert.ToInt32(motor_car));
                    cmd1.Parameters.AddWithValue("@medical", Convert.ToInt32(Medical));
                    cmd1.Parameters.AddWithValue("@rent", Convert.ToInt32(HouseRent));
                    cmd1.Parameters.AddWithValue("@total", Convert.ToInt32(total));
                    cmd1.Parameters.AddWithValue("@ut", Convert.ToInt32(utility));
                    cmd1.Parameters.AddWithValue("@df", Convert.ToInt32(df));
                    cmd1.Parameters.AddWithValue("@Ot", Convert.ToInt32(Ot));
                    cmd1.Parameters.AddWithValue("@incometax", 83.33);
                    cmd1.Parameters.AddWithValue("@bonus", Convert.ToInt32(bonus));
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();


                    return "Save";
                }

                else if (total > 800000 && total <= 1200000)
                {

                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd1 = new SqlCommand(@"insert into Packages values(@basic,@petrol,@mobile,@lunch,@car,@medical,@rent,@total,@pname,@ut,@df,@Ot,@incometax,@bonus)", conn);
                    cmd1.Parameters.AddWithValue("@pname", pname);
                    cmd1.Parameters.AddWithValue("@basic", Convert.ToInt32(basic));
                    cmd1.Parameters.AddWithValue("@petrol", Convert.ToInt32(petrol));
                    cmd1.Parameters.AddWithValue("@mobile", Convert.ToInt32(mobile));
                    cmd1.Parameters.AddWithValue("@lunch", Convert.ToInt32(Lunch));
                    cmd1.Parameters.AddWithValue("@car", Convert.ToInt32(motor_car));
                    cmd1.Parameters.AddWithValue("@medical", Convert.ToInt32(Medical));
                    cmd1.Parameters.AddWithValue("@rent", Convert.ToInt32(HouseRent));
                    cmd1.Parameters.AddWithValue("@total", Convert.ToInt32(total));
                    cmd1.Parameters.AddWithValue("@ut", Convert.ToInt32(utility));
                    cmd1.Parameters.AddWithValue("@df", Convert.ToInt32(df));
                    cmd1.Parameters.AddWithValue("@Ot", Convert.ToInt32(Ot));
                    cmd1.Parameters.AddWithValue("@incometax", 166.66);
                    cmd1.Parameters.AddWithValue("@bonus", Convert.ToInt32(bonus));
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();


                    return "Save";
                }

                else if (total > 1200000)
                {


                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);
                    SqlCommand cmd1 = new SqlCommand(@"insert into Packages values(@basic,@petrol,@mobile,@lunch,@car,@medical,@rent,@total,@pname,@ut,@df,@Ot,@incometax,@bonus)", conn);
                    cmd1.Parameters.AddWithValue("@pname", pname);
                    cmd1.Parameters.AddWithValue("@basic", Convert.ToInt32(basic));
                    cmd1.Parameters.AddWithValue("@petrol", Convert.ToInt32(petrol));
                    cmd1.Parameters.AddWithValue("@mobile", Convert.ToInt32(mobile));
                    cmd1.Parameters.AddWithValue("@lunch", Convert.ToInt32(Lunch));
                    cmd1.Parameters.AddWithValue("@car", Convert.ToInt32(motor_car));
                    cmd1.Parameters.AddWithValue("@medical", Convert.ToInt32(Medical));
                    cmd1.Parameters.AddWithValue("@rent", Convert.ToInt32(HouseRent));
                    cmd1.Parameters.AddWithValue("@total", Convert.ToInt32(total));
                    cmd1.Parameters.AddWithValue("@ut", Convert.ToInt32(utility));
                    cmd1.Parameters.AddWithValue("@df", Convert.ToInt32(df));
                    cmd1.Parameters.AddWithValue("@Ot", Convert.ToInt32(Ot));
                    cmd1.Parameters.AddWithValue("@incometax", 5.00);
                    cmd1.Parameters.AddWithValue("@bonus", Convert.ToInt32(bonus));
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();


                    return "Save";
                }
                return "Not";
                //   }
                //catch (Exception ex)
                //{
                //    return "" + ex;
                //}
            }
            catch(Exception ex)
            {
                return "Some Error";
            }
        }
              [WebMethod]
        public static List<Packages> GetEditData(string pid)
        {
            List<Packages> list_det = new List<Packages>();


            DataTable dt = get_all_products(Convert.ToInt32(pid));
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                Packages p = new Packages();

                p.p_name = Convert.ToString(dt.Rows[i]["p_name"]);
                p.mobile = Convert.ToInt32(dt.Rows[i]["mobile"]);
                p.lunch = Convert.ToInt32(dt.Rows[i]["lunch"]);
                p.medical = Convert.ToInt32(dt.Rows[i]["medical"]);
                p.petrol = Convert.ToInt32(dt.Rows[i]["petrol"]);
                p.basic = Convert.ToInt32(dt.Rows[i]["basic_salary"]);
                p.car= Convert.ToInt32(dt.Rows[i]["motor_car"]);
                p.rent= Convert.ToInt32(dt.Rows[i]["house_rent"]);
                p.Utility= Convert.ToInt32(dt.Rows[i]["Utility"]);
                p.DriverFuel = Convert.ToInt32(dt.Rows[i]["DriverFuel"]);
               p.OTime= Convert.ToInt32(dt.Rows[i]["Overtime"]);
                p.bonus= Convert.ToInt32(dt.Rows[i]["Bonus"]);

                list_det.Add(p);
            }
            return list_det;

        }

        private static DataTable get_all_products(int eid)
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select * from Packages where pack_id=@pid", conn);
            cmd.Parameters.AddWithValue("@pid",eid);
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
