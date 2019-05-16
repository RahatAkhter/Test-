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
    public partial class Payroll : System.Web.UI.Page
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
        public static string GetHoli(string month)
        {

            try
            {

                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@"select Count(*) from Holidays where date Like @month + '%'", conn);
                // cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                cmd.Parameters.AddWithValue("@month", Convert.ToString(month));
                
                conn.Open();
                int holidays = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return holidays.ToString();
            }
            catch(Exception ex)
            {
                return ""+ex.ToString();
            }
        }
        [WebMethod]
         public static string getsundays(string y, string m)
        {

            int year = Convert.ToInt32(y);
            int month = Convert.ToInt32(m);
            var firstDay = new DateTime(year, month, 1);

            var day29 = firstDay.AddDays(28);
            var day30 = firstDay.AddDays(29);
            var day31 = firstDay.AddDays(30);

            if ((day29.Month == month && day29.DayOfWeek == DayOfWeek.Sunday)
            || (day30.Month == month && day30.DayOfWeek == DayOfWeek.Sunday)
            || (day31.Month == month && day31.DayOfWeek == DayOfWeek.Sunday))
            {
                return "5";
            }
            else
            {
                return "4";
            }
        }



        [WebMethod]
        public static string Check_month(string month)
        {
            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(" select Count(*) from Emp_payroll where month_year=@month",conn);
                cmd.Parameters.AddWithValue("@month",month);
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                if (count == 0)
                {
                    return "generate";
                }
                else
                {
                    return "View";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [WebMethod]
        public static List<Cls_Payroll> ViewPayroll(string month)
        {
            List<Cls_Payroll> list_det = new List<Cls_Payroll>();


            DataTable dt = get_all_products(month);
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                Cls_Payroll p = new Cls_Payroll();

                p.emp_id = Convert.ToInt32(dt.Rows[i]["emp_id"]);
                p.mobile =(float) Convert.ToDecimal((dt.Rows[i]["mobile"]));
                p.petrol = (float)Convert.ToDecimal( (dt.Rows[i]["petrol"]));
                p.car = (float) Convert.ToDecimal(dt.Rows[i]["car"]);
                p.basic = (float)Convert.ToDecimal((dt.Rows[i]["basic_salry"]));
                p.advance = (float)Convert.ToDecimal((dt.Rows[i]["advance"]));
                p.Driver_Fuel = (float)Convert.ToDecimal((dt.Rows[i]["Driver_Fuel"]));
                p.house_rent = (float)Convert.ToDecimal((dt.Rows[i]["House_Rent"]));
                p.OverTime= (float)Convert.ToDecimal((dt.Rows[i]["Overetime"]));
                p.total = (float)Convert.ToDecimal((dt.Rows[i]["total"]));
                p.IncomeTax =(float)Convert.ToDecimal((dt.Rows[i]["income_tex"]));
                p.ProvidentFund = (float)Convert.ToDecimal((dt.Rows[i]["provident_fund"]));
                p.medical = (float)Convert.ToDecimal((dt.Rows[i]["medical"]));
                p.late = (float)Convert.ToDecimal((dt.Rows[i]["Leave_amont_deduction"]));
                p.netAmount= (float)Convert.ToDecimal((dt.Rows[i]["NetAmount"]));
                p.working_days= (Int32)Convert.ToDecimal((dt.Rows[i]["working_days"]));
                p.lunch= (float)Convert.ToDecimal((dt.Rows[i]["lunch"]));
                p.Utility= (float)Convert.ToDecimal((dt.Rows[i]["Utility"]));
                list_det.Add(p);
            }
            return list_det;

        }

        private static DataTable get_all_products(string month)
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@" select * from Emp_Payroll where month_year=@month", conn);
            cmd.Parameters.AddWithValue("@month",month);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }


        [WebMethod]
        public static List<Cls_Payroll> GetPayroll(string month, string sundays, string NumberOfDays, string holidays)
        {


            List<Cls_Payroll> list_det = new List<Cls_Payroll>();
            try
            {
                List<int> list = new List<int>();
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@"select Count(*) from Employee where status=1", conn);


                conn.Open();
                int size = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                int[] idarr = new int[size];

                SqlCommand cmd1 = new SqlCommand(@"select emp_id from Employee where status=1 and Emp_type=2", conn);
                conn.Open();
                SqlDataReader rea = cmd1.ExecuteReader();

                while (rea.Read())
                {

                    list.Add(Convert.ToInt32(rea["emp_id"]));
                }
                conn.Close();


                // here calculation begins
                foreach (int i in list)
                {
                    float total = 0;
                    string name = "", Desig = "";
                    float petrol = 0, fuel = 0, lunch = 0, car = 0, Leaves_total = 0, Basic = 0, mobile = 0, medical = 0, House_rent = 0, Utility = 0, DriverFuel = 0;
                    int bonus = 0;
                    SqlCommand cmdforexception = new SqlCommand(@"select exception  from Employee where emp_id=@id", conn);
                    cmdforexception.Parameters.AddWithValue("@id", i);
                    conn.Open();
                    int exception = Convert.ToInt32(cmdforexception.ExecuteScalar());
                    conn.Close();
                    SqlCommand cmdforname = new SqlCommand(@" select e.emp_name, e.Designation from Employee as e where e.emp_id=@id", conn);

                    cmdforname.Parameters.AddWithValue("@id", Convert.ToInt32(i));

                    conn.Open();
                    SqlDataReader read1 = cmdforname.ExecuteReader();
                    while (read1.Read())
                    {

                        name = read1["emp_name"].ToString();
                        Desig = read1["Designation"].ToString();


                    }

                    // Like @month + '%'
                    conn.Close();

                    float totalAmount = 0;
                    SqlCommand cmd2 = new SqlCommand(@"select p.total 
from Packages as p
left join Employee as e
on e.salary_pack=p.pack_id
where e.emp_id='" + i + "'", conn);

                    conn.Open();
                    total = Convert.ToInt32(cmd2.ExecuteScalar());
                    conn.Close();

                    if (exception == 1)
                    {

                        Cls_Payroll obj = new Cls_Payroll();
                        // obj.total = (float)total / Convert.ToInt32(NumberOfDays);
                        obj.total = total;
                        obj.basic = 0;
                        obj.petrol = 0;
                        obj.mobile = 0;
                        obj.lunch = 0;
                        obj.car = 0;
                        obj.emp_id = i;
                        obj.OverTime = 0;
                        obj.late = 0;
                        obj.halfday = 0;
                        obj.working_days = 0;
                        obj.leave_days = 0.ToString();
                        obj.medical = 0;
                        obj.house_rent = 0;
                        obj.Utility = 0;
                        obj.Driver_Fuel = 0;
                        obj.IncomeTax = 0;
                        obj.ProvidentFund = 0;
                        obj.bonus = 0;
                        obj.name = name;
                        obj.desig = Desig;
                        list_det.Add(obj);
                        SqlCommand insert = new SqlCommand(@" insert into Emp_Payroll values(@emp_id,@wdays,@petrol,@mobile,@lunch,@car,@ptotal,@advance,@preAmount,@Overtime,@totalAmount,@monthyear,@medical,@houserent,@Utility,@driverFuel,@incometax,@providentfund,@leaveamountDedction,@basic,@bonus)", conn);
                        insert.Parameters.AddWithValue("@emp_id", i);
                        insert.Parameters.AddWithValue("@wdays", 0);
                        insert.Parameters.AddWithValue("@petrol", 0);
                        insert.Parameters.AddWithValue("@mobile", 0);
                        insert.Parameters.AddWithValue("@lunch", 0);
                        insert.Parameters.AddWithValue("@car", 0);
                        insert.Parameters.AddWithValue("@ptotal", total);
                        insert.Parameters.AddWithValue("@advance", 0);
                        insert.Parameters.AddWithValue("@preAmount", 0);
                        insert.Parameters.AddWithValue("@Overtime", 0);
                        insert.Parameters.AddWithValue("@totalAmount", total);
                        insert.Parameters.AddWithValue("@monthyear", month);
                        insert.Parameters.AddWithValue("@medical", 0);
                        insert.Parameters.AddWithValue("@houserent", 0);
                        insert.Parameters.AddWithValue("@Utility", 0);
                        insert.Parameters.AddWithValue("@driverFuel", 0);
                        insert.Parameters.AddWithValue("@incometax", 0);
                        insert.Parameters.AddWithValue("@providentfund", 0);
                        insert.Parameters.AddWithValue("@leaveamountDedction", 0);
                        insert.Parameters.AddWithValue("@basic", 0);
                        insert.Parameters.AddWithValue("@bonus", 0);
                        conn.Open();
                        insert.ExecuteNonQuery();
                        conn.Close();

                    }
                    else
                    {
                        int Overtime_Amount = 0;
                        float perday_Salary = (float)total / Convert.ToInt32(NumberOfDays);
                        SqlCommand cmdforexpenses = new SqlCommand(@"select p.*
from Packages as p
left join Employee as e
on e.salary_pack=p.pack_id
where e.emp_id=@id
", conn);

                        cmdforexpenses.Parameters.AddWithValue("@id", Convert.ToInt32(i));

                        conn.Open();
                        SqlDataReader read = cmdforexpenses.ExecuteReader();
                        while (read.Read())
                        {

                            petrol = (float)Convert.ToInt32(read["petrol"]) / Convert.ToInt32(NumberOfDays);
                            fuel = (float)Convert.ToInt32(read["DriverFuel"]) / Convert.ToInt32(NumberOfDays);
                            lunch = (float)Convert.ToInt32(read["lunch"]) / Convert.ToInt32(NumberOfDays);
                            car = (float)Convert.ToInt32(read["motor_car"]) / Convert.ToInt32(NumberOfDays);
                            Overtime_Amount = Convert.ToInt32(read["Overtime"]);
                            Basic = (float)Convert.ToInt32(read["basic_salary"]) / Convert.ToInt32(NumberOfDays);
                            mobile = (float)Convert.ToInt32(read["mobile"]) / Convert.ToInt32(NumberOfDays);
                            House_rent = (float)Convert.ToInt32(read["house_rent"]) / Convert.ToInt32(NumberOfDays);
                            medical = (float)Convert.ToInt32(read["medical"]) / Convert.ToInt32(NumberOfDays);
                            Utility = (float)Convert.ToInt32(read["Utility"]) / Convert.ToInt32(NumberOfDays);
                            bonus = Convert.ToInt32(read["bonus"]);
                            Leaves_total += petrol + fuel + lunch + car;

                        }

                        // Like @month + '%'
                        conn.Close();

                        SqlCommand cmdforOntime = new SqlCommand(@"select Count(*) from Attendence where emp_id=@id and late=0 and half_day=0 and date Like @month + '%'", conn);
                        cmdforOntime.Parameters.AddWithValue("@month", month);
                        cmdforOntime.Parameters.AddWithValue("@id", i);
                        conn.Open();
                        int Ontime = Convert.ToInt32(cmdforOntime.ExecuteScalar());
                        conn.Close();
                        SqlCommand cmdforlate = new SqlCommand("select Count(*) from Attendence where date  Like @month + '%' and emp_id=@id and late=1", conn);
                        cmdforlate.Parameters.AddWithValue("@month", month);
                        cmdforlate.Parameters.AddWithValue("@id", i);
                        conn.Open();
                        int late = Convert.ToInt32(cmdforlate.ExecuteScalar());
                        conn.Close();

                        SqlCommand cmdforhalf = new SqlCommand(@"select isnull(Count(*),0)  from Attendence where date Like @month + '%' and emp_id=@id and half_day=1", conn);
                        cmdforhalf.Parameters.AddWithValue("@id", i);
                        cmdforhalf.Parameters.AddWithValue("@month", month);
                        conn.Open();
                        int half = Convert.ToInt32(cmdforhalf.ExecuteScalar());
                        conn.Close();

                        SqlCommand cmdforovertime = new SqlCommand("select isnull(Sum(overtime),0) from Attendence where date Like @month + '%' and emp_id=@id", conn);
                        cmdforovertime.Parameters.AddWithValue("@id", i);
                        cmdforovertime.Parameters.AddWithValue("@month", month);

                        conn.Open();
                        int Overtime = Convert.ToInt32(cmdforovertime.ExecuteScalar());
                        conn.Close();

                        SqlCommand cmdforpresents = new SqlCommand(@"select Count(*) from Attendence where date Like @month + '%' and emp_id=@id and status=1", conn);

                        cmdforpresents.Parameters.AddWithValue("@id", i);
                        cmdforpresents.Parameters.AddWithValue("@month", month);
                        conn.Open();
                        int presents = Convert.ToInt32(cmdforpresents.ExecuteScalar());
                        conn.Close();

                        SqlCommand cmdforontimePlusLeaves = new SqlCommand(@"select Count(*) from Attendence where date Like @month + '%' and emp_id=@id and status=1 and half_day=0", conn);
                        cmdforontimePlusLeaves.Parameters.AddWithValue("id", i);
                        cmdforontimePlusLeaves.Parameters.AddWithValue("month", month);
                        conn.Open();
                        int ontimepluslatedays = Convert.ToInt32(cmdforontimePlusLeaves.ExecuteScalar());
                        conn.Close();
                        

                        SqlCommand cmdforLeaves = new SqlCommand(@"select  isnull(Sum(days),0) from Leaves where emp_id=@id and submit_date Like  @month + '%' ", conn);

                        cmdforLeaves.Parameters.AddWithValue("@id", i);
                        cmdforLeaves.Parameters.AddWithValue("@month", month);
                        conn.Open();
                        int Leaves = Convert.ToInt32(cmdforLeaves.ExecuteScalar());
                        conn.Close();

                        SqlCommand cmdforWorkingdays = new SqlCommand("Select Count(*) from Attendence where emp_id=@id and status=1 and date Like @month + '%' ", conn);
                        cmdforWorkingdays.Parameters.AddWithValue("@id", i);
                        cmdforWorkingdays.Parameters.AddWithValue("@month", month);

                        conn.Open();
                        int WorkingDays = Convert.ToInt32(cmdforWorkingdays.ExecuteScalar());
                        conn.Close();
                        SqlCommand cmdforIncomeTax = new SqlCommand(@"select p.IncomeTax
 from Packages as p
 left join Employee as e
 on e.salary_pack = p.pack_id
 where e.emp_id = @id", conn);
                        cmdforIncomeTax.Parameters.AddWithValue("@id", i);

                        conn.Open();
                        float IncomeTax = (float)Convert.ToInt32(cmdforIncomeTax.ExecuteScalar());
                        conn.Close();
                        float amountBeforePF = 0;
                        int wd = Convert.ToInt32(NumberOfDays) - (Convert.ToInt32(sundays) + Convert.ToInt32(holidays));
                        int count = 0;
                        if (Ontime == wd)
                        {
                            count = 1;
                        }
                        else
                        {
                            bonus = 0;
                        }

                        if (IncomeTax == 5.0)
                        {
                            
                            int Late_absents = late / 3;
                            WorkingDays = WorkingDays - Late_absents;
                            totalAmount += ontimepluslatedays * perday_Salary;
                            totalAmount += perday_Salary * Convert.ToInt32(sundays);
                            totalAmount += perday_Salary * Convert.ToInt32(holidays);
                            totalAmount += half * perday_Salary / 2;
                            totalAmount += Leaves * perday_Salary;
                            totalAmount -= Leaves_total * Leaves;
                            totalAmount -= Late_absents * perday_Salary;
                            totalAmount += Overtime * Overtime_Amount;
                            float tb = totalAmount;
                            totalAmount -= totalAmount * (float)(0.05);
                            float IncomeaxForView = totalAmount * (float)(0.05);
                            amountBeforePF = totalAmount;
                            
                                totalAmount += bonus;
                            
                            totalAmount -= totalAmount * (float)(0.083);
                            petrol = petrol * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves)-(half * petrol / 2);
                            medical = medical * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves) - (half * medical / 2);
                            Basic = Basic * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves) - (half * Basic / 2);
                            mobile = mobile * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves) - (half * mobile / 2);
                            lunch = lunch * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves) - (half * lunch / 2);
                            car = car * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves) - (half * car / 2);
                            House_rent = House_rent * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves) - (half * House_rent / 2);
                            Utility = Utility * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves) - (half * Utility / 2);
                            fuel = fuel * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves) - (half * fuel / 2);
                            Cls_Payroll obj = new Cls_Payroll();
                            // obj.total = (float)total / Convert.ToInt32(NumberOfDays)
                            obj.total = totalAmount;
                            obj.basic = (float)Basic;
                            obj.petrol = (float)petrol;
                            obj.mobile = (float)mobile;
                            obj.lunch = (float)lunch;
                            obj.car = (float)car;
                            obj.emp_id = i;
                            obj.OverTime = Overtime * Overtime_Amount;
                            obj.late = Late_absents;
                            obj.halfday = half * (perday_Salary / 2);
                            obj.working_days = WorkingDays;
                            obj.leave_days = Convert.ToString(Late_absents * perday_Salary);
                            obj.medical = medical;
                            obj.house_rent = House_rent;
                            obj.Utility = Utility;
                            obj.Driver_Fuel = fuel;
                            obj.ProvidentFund = amountBeforePF * (float)(0.083);
                            obj.IncomeTax = IncomeaxForView;
                            obj.total_before = tb;
                            obj.name = name;
                            obj.desig = Desig;
                            obj.bonus = bonus;
                            
                            list_det.Add(obj);
                            SqlCommand insert = new SqlCommand(@" insert into Emp_Payroll values(@emp_id,@wdays,@petrol,@mobile,@lunch,@car,@ptotal,@advance,@preAmount,@Overtime,@totalAmount,@monthyear,@medical,@houserent,@Utility,@driverFuel,@incometax,@providentfund,@leaveamountDedction,@basic,@bonus)", conn);
                            insert.Parameters.AddWithValue("@emp_id", i);
                            insert.Parameters.AddWithValue("@wdays", WorkingDays);
                            insert.Parameters.AddWithValue("@petrol", petrol);
                            insert.Parameters.AddWithValue("@mobile", mobile);
                            insert.Parameters.AddWithValue("@lunch", lunch);
                            insert.Parameters.AddWithValue("@car", car);
                            insert.Parameters.AddWithValue("@ptotal", total);
                            insert.Parameters.AddWithValue("@advance", 0);
                            insert.Parameters.AddWithValue("@preAmount", 0);
                            insert.Parameters.AddWithValue("@Overtime", Overtime * Overtime_Amount);
                            insert.Parameters.AddWithValue("@totalAmount", totalAmount);
                            insert.Parameters.AddWithValue("@monthyear", month);
                            insert.Parameters.AddWithValue("@medical", medical);
                            insert.Parameters.AddWithValue("@houserent", House_rent);
                            insert.Parameters.AddWithValue("@Utility", Utility);
                            insert.Parameters.AddWithValue("@driverFuel", fuel);
                            insert.Parameters.AddWithValue("@incometax", IncomeaxForView);
                            insert.Parameters.AddWithValue("@providentfund", amountBeforePF * (float)(0.083));
                            insert.Parameters.AddWithValue("@leaveamountDedction", Leaves_total * Leaves);
                            insert.Parameters.AddWithValue("@basic", Basic);
                            insert.Parameters.AddWithValue("@bonus", bonus);

                            conn.Open();
                            insert.ExecuteNonQuery();
                            conn.Close();



                        }
                        else
                        {
                            float totalbefore = 0;
                            int Late_absents = late / 3;
                            WorkingDays = WorkingDays - Late_absents;
                            totalAmount += ontimepluslatedays * perday_Salary;
                            totalAmount += perday_Salary * Convert.ToInt32(sundays);
                            totalAmount += perday_Salary * Convert.ToInt32(holidays);
                            totalAmount += half * perday_Salary / 2;
                            totalAmount += Leaves * perday_Salary;
                            totalAmount -= Leaves_total * Leaves;
                            totalAmount -= Late_absents * perday_Salary;
                            totalAmount += Overtime * Overtime_Amount;
                            totalbefore = totalAmount;
                            totalAmount -= IncomeTax;
                            amountBeforePF = totalAmount;
                            // float tbbb = totalAmount;
                            totalAmount += bonus;
                            totalAmount -= totalAmount * (float)(0.083);
                            petrol = petrol * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves)-(half * petrol / 2);
                            medical = medical * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves)- (half * medical / 2);
                            Basic = Basic * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves)- (half * Basic / 2);
                            mobile = mobile * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves)- (half * mobile / 2);
                            lunch = lunch * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves)- (half * lunch / 2);
                            car = car * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves)- (half * car / 2);
                            House_rent = House_rent * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves)- (half * House_rent / 2);
                            Utility = Utility * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves)- (half * Utility / 2);
                            fuel = fuel * (WorkingDays + Convert.ToInt32(sundays) + Convert.ToInt32(holidays) + Leaves)- (half * fuel / 2);
                            Cls_Payroll obj = new Cls_Payroll();
                            // obj.total = (float)total / Convert.ToInt32(NumberOfDays)
                            obj.total = totalAmount;
                            obj.basic = (float)Basic;
                            obj.petrol = (float)petrol;
                            obj.mobile = (float)mobile;
                            obj.lunch = (float)lunch;
                            obj.car = (float)car;
                            obj.emp_id = i;
                            obj.OverTime = Overtime * Overtime_Amount;
                            obj.late = Late_absents;
                            obj.halfday = half * (perday_Salary / 2);
                            obj.working_days = WorkingDays;
                            obj.leave_days = Convert.ToString(Late_absents * perday_Salary);
                            obj.medical = medical;
                            obj.house_rent = House_rent;
                            obj.Utility = Utility;
                            obj.Driver_Fuel = fuel;
                            obj.ProvidentFund = amountBeforePF * (float)(0.083);
                            obj.IncomeTax = IncomeTax;
                            obj.total_before = totalbefore;
                            obj.name = name;
                            obj.desig = Desig;
                            obj.bonus = bonus;
                            list_det.Add(obj);

                            SqlCommand insert = new SqlCommand(@" insert into Emp_Payroll values(@emp_id,@wdays,@petrol,@mobile,@lunch,@car,@ptotal,@advance,@preAmount,@Overtime,@totalAmount,@monthyear,@medical,@houserent,@Utility,@driverFuel,@incometax,@providentfund,@leaveamountDedction,@basic,@bonus)", conn);
                            insert.Parameters.AddWithValue("@emp_id", i);
                            insert.Parameters.AddWithValue("@wdays", WorkingDays);
                            insert.Parameters.AddWithValue("@petrol", petrol);
                            insert.Parameters.AddWithValue("@mobile", mobile);
                            insert.Parameters.AddWithValue("@lunch", lunch);
                            insert.Parameters.AddWithValue("@car", car);
                            insert.Parameters.AddWithValue("@ptotal", total);
                            insert.Parameters.AddWithValue("@advance", 0);
                            insert.Parameters.AddWithValue("@preAmount", 0);
                            insert.Parameters.AddWithValue("@Overtime", Overtime * Overtime_Amount);
                            insert.Parameters.AddWithValue("@totalAmount", totalAmount);
                            insert.Parameters.AddWithValue("@monthyear", month);
                            insert.Parameters.AddWithValue("@medical", medical);
                            insert.Parameters.AddWithValue("@houserent", House_rent);
                            insert.Parameters.AddWithValue("@Utility", Utility);
                            insert.Parameters.AddWithValue("@driverFuel", fuel);
                            insert.Parameters.AddWithValue("@incometax", IncomeTax);
                            insert.Parameters.AddWithValue("@providentfund", amountBeforePF * (float)(0.083));
                            insert.Parameters.AddWithValue("@leaveamountDedction", Leaves_total * Leaves);
                            insert.Parameters.AddWithValue("@basic", Basic);
                            insert.Parameters.AddWithValue("@bonus", bonus);
                            conn.Open();
                            insert.ExecuteNonQuery();
                            conn.Close();

                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Cls_Payroll obj = new Cls_Payroll();

                List<Cls_Payroll> list = new List<Cls_Payroll>();
                obj.name = ex.ToString();
                list.Add(obj);
                return list;
            }


        
            return list_det;
        }


        //here we get the Salary Slip 
        [WebMethod]
        public static List<SalarySlip> GetSalarySlip(string month,string id)
        {


            List<SalarySlip> list_det = new List<SalarySlip>();


            DataTable dt = getprint(month,Convert.ToInt32(id));
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                SalarySlip p = new SalarySlip();

                p.emp_name = Convert.ToString(dt.Rows[i]["emp_name"]);
                p.curadd = Convert.ToString(dt.Rows[i]["currant_address"]);
                p.per_add = Convert.ToString(dt.Rows[i]["permenant_address"]);
                p.cnic = Convert.ToString(dt.Rows[i]["cnic"]);
                p.Designation = Convert.ToString(dt.Rows[i]["Designation"]);
               
                p.DOB = Convert.ToString(dt.Rows[i]["date_of_joining"]);
                p.gender = Convert.ToString(dt.Rows[i]["gender"]);
                p.bankAccount = Convert.ToString(dt.Rows[i]["bankAccount"]);
                p.dep_name = Convert.ToString(dt.Rows[i]["dep_name"]);
                p.latedays = Convert.ToInt32(dt.Rows[i]["latedays"]);
                p.Worked_days = Convert.ToInt32(dt.Rows[i]["Worked_Days"]);
                p.HalfDays = Convert.ToInt32(dt.Rows[i]["Half_Days"]);
                p.salary_pack = Convert.ToInt32(dt.Rows[i]["total"]);
                p.basic = Convert.ToInt32(dt.Rows[i]["basic_salry"]);
                p.house_rent = Convert.ToInt32(dt.Rows[i]["House_Rent"]);
                p.petrol = Convert.ToInt32(dt.Rows[i]["petrol"]);
                p.mob = Convert.ToInt32(dt.Rows[i]["mobile"]);
                p.car = Convert.ToInt32(dt.Rows[i]["car"]);
                p.medical = Convert.ToInt32(dt.Rows[i]["medical"]);
                p.Utility = Convert.ToInt32(dt.Rows[i]["Utility"]);
                p.lunch = Convert.ToInt32(dt.Rows[i]["lunch"]);
                p.Driver_Fuel = Convert.ToInt32(dt.Rows[i]["Driver_Fuel"]);
                p.OverTime = Convert.ToInt32(dt.Rows[i]["Overetime"]);
                p.bonus = Convert.ToInt32(dt.Rows[i]["bonus"]);
                p.GrossEarning = Convert.ToInt32(dt.Rows[i]["Gross_Earning"]);
                p.GrossDeduction = Convert.ToInt32(dt.Rows[i]["Gross_Decution"]);
                p.ProvidentFund = Convert.ToInt32(dt.Rows[i]["provident_fund"]);
                p.IncomeTax = Convert.ToInt32(dt.Rows[i]["income_tex"]);
                p.leave_daysDeduction = (float)Convert.ToDecimal((dt.Rows[i]["Leave_amont_deduction"]));
                p.NetPay = Convert.ToInt32(dt.Rows[i]["NetAmount"]);
                p.ms= Convert.ToInt32(dt.Rows[i]["ms"]);
                p.month_year = Convert.ToString(dt.Rows[i]["month_year"]);
                list_det.Add(p);
            }
            return list_det;



        }

        private static DataTable getprint(string month,int id)
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"
Select p.basic_salry,p.House_Rent,p.petrol,p.mobile as ms,p.car,p.medical,p.month_year,p.Utility,p.Driver_Fuel,p.lunch,p.Overetime,p.bonus,(p.basic_salry+p.House_Rent+p.petrol+p.mobile+p.lunch+p.car+p.medical+p.Utility+p.Driver_Fuel+p.Overetime+p.bonus)as Gross_Earning,
(p.provident_fund+p.income_tex+ p.Leave_amont_deduction)as Gross_Decution,p.NetAmount,
p.provident_fund,p.income_tex,p.Leave_amont_deduction,pack.total,e.emp_name,e.currant_address,e.permenant_address,e.cnic,e.Designation,e.mobile,e.date_of_joining,e.bankAccount ,e.gender,d.dep_name,(Select Count(*) from Attendence where status=1 and late=1 and emp_id=@id and date like @month + '%')as latedays,
(select Count(*) from Attendence where Status=1 and emp_id=@id and date like @month + '%')as Worked_Days,(select Count(*) from Attendence where Status=1 and half_day=1 and emp_id=@id and date like @month + '%')as Half_Days
from Emp_Payroll as p
left join Employee as e
on p.emp_id=e.emp_id
left join Packages as pack
on pack.pack_id=e.salary_pack
left join Department as d
on d.dep_id=e.dep_id
where p.month_year=@month and p.emp_id=@id", conn);
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@id", id);
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

public class Cls_Payroll
{
    public int emp_id { get; set; }
    public string name { get; set; }
    public string desig { get; set; }
    public int working_days { get; set; }
    public float basic { get; set; }
    public float petrol { get; set; }
    public float mobile { get; set; }
    public float lunch { get; set; }
    public float medical { get; set; }
    public float house_rent { get; set; }
    public float Utility { get; set; }
    public float Driver_Fuel { get; set; }
    public float total { get; set; }
    public float advance { get; set; }
    public float car { get; set; }
    public float OverTime { get; set; }
    public float netAmount { get; set; }
    public float paid_amount{get;set;}
   
    public float late { get; set; }
    public float halfday { get; set; }
   public float IncomeTax { get; set; }
    public float ProvidentFund { get; set; }
    public string leave_days { get; set; }
    public float total_before { get; set; }
    public int bonus { get; set; }
}