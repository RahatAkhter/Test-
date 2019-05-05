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
    public partial class Time_Practice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string Check(string time, string time2)
        {
            try {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.monday =s.shift_id
where es.emp_id=4", conn);

                conn.Open();
                string t = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();

                SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.monday =s.shift_id
where es.emp_id=4", conn);

                conn.Open();
                string te = Convert.ToString(cmd1.ExecuteScalar());
                conn.Close();



                TimeSpan ts = new TimeSpan(int.Parse(t.Split(':')[0]),
                               int.Parse(t.Split(':')[1]),
                               0);
                TimeSpan ts1 = new TimeSpan(int.Parse(t.Split(':')[0]),    // hours
                               int.Parse(t.Split(':')[1]),    // minutes
                               0);

                ts += TimeSpan.FromMinutes(90);
                ts1 += TimeSpan.FromHours(3);

                TimeSpan timein = new TimeSpan(int.Parse(time.Split(':')[0]),    // hours
                              int.Parse(time.Split(':')[1]),    // minutes
                              0);
                TimeSpan timeout = new TimeSpan(int.Parse(time2.Split(':')[0]),    // hours
                              int.Parse(time2.Split(':')[1]),    // minutes
                              0);

                TimeSpan shiftSTime = new TimeSpan(int.Parse(t.Split(':')[0]),    // hours
                             int.Parse(t.Split(':')[1]),    // minutes
                             0);

                TimeSpan shiftETime = new TimeSpan(int.Parse(te.Split(':')[0]),    // hours
                             int.Parse(te.Split(':')[1]),    // minutes
                             0);
                TimeSpan TotalHours = shiftETime.Subtract(shiftSTime);
                TimeSpan WorkingHours = timeout.Subtract(timein);
                TimeSpan half = shiftETime.Subtract(shiftSTime);
                TimeSpan hh = new TimeSpan(half.Ticks / 2);
                hh += TimeSpan.FromHours(1);
                //  half = TimeSpan.FromHours(1);
                TimeSpan beforecheckout = shiftETime.Subtract(shiftSTime);
                beforecheckout += TimeSpan.FromHours(1);
                TimeSpan Check = timeout.Subtract(timein);
                TimeSpan Overtime = timeout.Subtract(shiftETime);
                if (timeout > timein)
                {

                    if (timein <= shiftSTime && timeout >= shiftETime)
                    {//ye ontime ki condition agai

                        return "On Time he Or Over Time" + Convert.ToString(Overtime);


                    }
                    else if ((timein > shiftSTime && timein <= ts) && timeout >= shiftETime)
                    {// ye sirft late ki condition he
                        if (Overtime.Hours < 0)
                        {
                            return " Sirf Late" + Convert.ToString(0);
                        }
                        else
                        {
                            return " Sirf Late" + Convert.ToString(Overtime.Hours);
                        }
                    }
                    //timein > ts && timein <= ts1
                    else if ((timein <= shiftSTime && (Check.Hours >= hh.Hours)) || (timein > ts && timein < ts1 && timeout >= shiftETime))
                    {
                        if (Overtime.Hours < 0)
                        {
                            // late nhi he lekin half day he
                            return "Half Day" + Convert.ToString(0);
                        }
                        else
                        {
                            return "Half Day" + Convert.ToString(Overtime.Hours);
                        }
                    }
                    else if ((timein > shiftSTime && Check.Hours < hh.Hours))
                    {
                        return "absent" + Convert.ToString(TotalHours.Hours) + " Work Hours" + Convert.ToString(WorkingHours.Hours);
                    }

                    else if ((timein > shiftSTime && timein <= ts) && (timeout < shiftETime || Check.Hours > hh.Hours))
                    {
                        if (Overtime.Hours < 0)
                        {
                            //(timein>shiftSTime && timein<=ts1) && (Check.Hours >= hh.Hours && timeout < shiftETime || timeout > ts1)
                            //timein > shiftSTime && (timeout < shiftETime && timeout.Hours > hh.Hours)
                            return "Late b he half day b he  " +Convert.ToString(0) ;
                        }
                        else
                        {
                            return "Late b he half day b he  " + Convert.ToString(Overtime.Hours);
                        }
                    }

                    else
                    {
                        return "Absent" + Convert.ToString(Overtime.Hours);
                    }

                }
                else
                {
                    return "Not Valid";
                }
                // return "Shift Start Tiem" + Convert.ToString(shiftSTime) + " End Time Is" + Convert.ToString(shiftETime) + " minus=" + Convert.ToString(half.Hours) + " Half hourse" + Convert.ToString(hh)+ " CheckOurs"+ Convert.ToString(Check.Hours);

            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
            }
    }
}