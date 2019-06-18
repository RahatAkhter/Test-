using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
namespace SpangleERP.HR_Module
{
    public partial class Delete2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var con1 = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmds = new SqlCommand("select User_id(count) * FROM uSERS", conn);
            int getcount = Convert.ToInt32(cmds.ToString());
            SqlCommand cmd = new SqlCommand(@"select * from Users ", conn);
            conn.Open();
            SqlDataReader rea = cmd.ExecuteReader();
            while (rea.Read())
            {
            
                string id = rea["Email"].ToString();
              //  string name = rea["friend_Id"].ToString();

            }
            rea.Close();

            conn.Close();
        }


        [WebMethod]
        public static List<SalarySlip> GetSalarySlip()
        {
            
           
                List<SalarySlip> list_det = new List<SalarySlip>();


                DataTable dt = getprint();
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    SalarySlip p = new SalarySlip();

                    p.emp_name = Convert.ToString(dt.Rows[i]["emp_name"]);
                    p.curadd = Convert.ToString(dt.Rows[i]["currant_address"]);
                    p.per_add = Convert.ToString(dt.Rows[i]["permenant_address"]);
                    p.cnic= Convert.ToString(dt.Rows[i]["cnic"]);
                    p.Designation= Convert.ToString(dt.Rows[i]["Designation"]);
                    
                    p.DOB = Convert.ToString(dt.Rows[i]["date_of_joining"]);
                    p.gender= Convert.ToString(dt.Rows[i]["gender"]);
                    p.bankAccount= Convert.ToString(dt.Rows[i]["bankAccount"]);
                    p.dep_name = Convert.ToString(dt.Rows[i]["dep_name"]);
                    p.latedays = Convert.ToInt32(dt.Rows[i]["latedays"]);
                    p.Worked_days = Convert.ToInt32(dt.Rows[i]["Worked_Days"]);
                    p.HalfDays = Convert.ToInt32(dt.Rows[i]["Half_Days"]);
                    p.salary_pack = Convert.ToInt32(dt.Rows[i]["total"]);
                    p.basic = Convert.ToInt32(dt.Rows[i]["basic_salry"]);
                    p.house_rent = Convert.ToInt32(dt.Rows[i]["House_Rent"]);
                    p.petrol = Convert.ToInt32(dt.Rows[i]["petrol"]);
                //    p.mobile = Convert.ToInt32(dt.Rows[i]["mobile"]);
                    p.car = Convert.ToInt32(dt.Rows[i]["car"]);
                    p.medical = Convert.ToInt32(dt.Rows[i]["medical"]);
                    p.Utility = Convert.ToInt32(dt.Rows[i]["Utility"]);
                    p.lunch = Convert.ToInt32(dt.Rows[i]["lunch"]);
                    p.Driver_Fuel = Convert.ToInt32(dt.Rows[i]["Driver_Fuel"]);
                    p.OverTime = Convert.ToInt32(dt.Rows[i]["Overetime"]);
                    p.bonus = Convert.ToInt32(dt.Rows[i]["bonus"]);
                    p.GrossEarning = Convert.ToInt32(dt.Rows[i]["Gross_Earning"]);
                    p.GrossDeduction = Convert.ToInt32(dt.Rows[i]["Gross_Decution"]);
                    p.ProvidentFund= Convert.ToInt32(dt.Rows[i]["provident_fund"]);
                    p.IncomeTax = Convert.ToInt32(dt.Rows[i]["income_tex"]);
                    p.leave_daysDeduction = (float)Convert.ToDecimal((dt.Rows[i]["Leave_amont_deduction"]));
                    p.NetPay= Convert.ToInt32(dt.Rows[i]["NetAmount"]);


                    list_det.Add(p);
                }
                return list_det;
           


        }

        private static DataTable getprint()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"
Select p.basic_salry,p.House_Rent,p.petrol,p.mobile,p.car,p.medical,p.Utility,p.Driver_Fuel,p.lunch,p.Overetime,p.bonus,(p.basic_salry+p.House_Rent+p.petrol+p.mobile+p.lunch+p.car+p.medical+p.Utility+p.Driver_Fuel+p.Overetime+p.bonus)as Gross_Earning,
(p.provident_fund+p.income_tex+ p.Leave_amont_deduction)as Gross_Decution,p.NetAmount,
p.provident_fund,p.income_tex,p.Leave_amont_deduction,pack.total,e.emp_name,e.currant_address,e.permenant_address,e.cnic,e.Designation,e.mobile,e.date_of_joining,e.bankAccount ,e.gender,d.dep_name,(Select Count(*) from Attendence where status=1 and late=1 and emp_id=21 and date like '2019-03%')as latedays,
(select Count(*) from Attendence where Status=1 and emp_id=21 and date like '2019-03%')as Worked_Days,(select Count(*) from Attendence where Status=1 and half_day=1 and emp_id=21 and date like '2019-03%')as Half_Days
from Emp_Payroll as p
left join Employee as e
on p.emp_id=e.emp_id
left join Packages as pack
on pack.pack_id=e.salary_pack
left join Department as d
on d.dep_id=e.dep_id
where p.month_year='2019-03' and p.emp_id=21", conn);
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


