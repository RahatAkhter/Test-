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
    public partial class teendence : System.Web.UI.Page
    { public static string t, te,t1,te1;
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
        }

        [WebMethod]
        public static List<All_Employees> GetUserDetail(string date)
        {
            
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

           

            SqlCommand cmd = new SqlCommand(@"select Count(*) from Attendence where date=@date and time_in=null", conn);

                cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            if (count == 0)
            {
                List<All_Employees> list_det = new List<All_Employees>();


                DataTable dt = getprint();
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    All_Employees p = new All_Employees();

                    p.emp_name = Convert.ToString(dt.Rows[i]["emp_name"]);
                    p.emp_id = Convert.ToInt32(dt.Rows[i]["emp_id"]);

                    list_det.Add(p);
                }
                return list_det;
            }

            All_Employees obj = new All_Employees();

            List<All_Employees> list = new List<All_Employees>();
           
            list.Add(obj);
            return list;
        }

        private static DataTable getprint()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select * from Employee  where status=1  and exception=0", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }
        [WebMethod]
        public static string InsertApsent(string date,string id)
        {
            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand cmd1 = new SqlCommand(@"select Count(*) from Attendence where date=@date and emp_id=@id ", conn);

            cmd1.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
            cmd1.Parameters.AddWithValue("@id",Convert.ToInt32(id));
            conn.Open();
            int count = Convert.ToInt32(cmd1.ExecuteScalar());
            conn.Close();

            if (count == 0)
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(@"insert into Attendence values(@emp_id,@timein,@late,@halfday,@timeout,1,@date,@status,@overtime)", conn);
                    cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                    cmd.Parameters.AddWithValue("@timein", DBNull.Value);
                    cmd.Parameters.AddWithValue("@timeout", DBNull.Value);
                    cmd.Parameters.AddWithValue("@overtime", DBNull.Value);
                    cmd.Parameters.AddWithValue("@update", 1);
                    cmd.Parameters.AddWithValue("@late", DBNull.Value);
                    cmd.Parameters.AddWithValue("@halfday", DBNull.Value);
                    cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                    cmd.Parameters.AddWithValue("@status", 0);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "Save";
                }
                catch (Exception ex)
                {

                    return "Exception";
                }
            }
            else
            {
                return "pehle";
            }
        }

        [WebMethod]
        public static string Check_Date(string date)
        {
            try
            {

                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@"select Count(*) from
 Attendence as a
 left join Employee as e
 on e.emp_id=a.emp_id
 left join Emp_Type as t
 on e.Emp_type=t.emp_Type
  where date=@date and time_in!='' and e.Emp_type=2", conn);
               
                cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                if (count ==0) {
                    return "true";
                }
                else
                {
                    return "false";
                }

            }
            catch(Exception ex)
            {
                return ex.ToString();
            }

        }

        [WebMethod]
        public static string Attendence(string date, string id,string time, string time2, string day)
        {
            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            try
            {
                if (time == "" && time2 == "")
                {
                    SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,time_out=@timeout,late=@late,half_day=@halfday,status=@status,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                    cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                    cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value= "00:00:00";
                    cmd.Parameters.AddWithValue("@late", 0);
                    cmd.Parameters.AddWithValue("@halfday", 0);
                    cmd.Parameters.AddWithValue("@timeout",SqlDbType.Time).Value= "00:00:00";
                    cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                    cmd.Parameters.AddWithValue("@status", 0);
                    cmd.Parameters.AddWithValue("@overtime", Convert.ToInt32(0));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "Absent";

                }





                if (Convert.ToInt32(day) == 0)
                {

                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.monday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.monday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();
                }
                else if (Convert.ToInt32(day) == 1)
                {

                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.tuesday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.tuesday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();
                }
                else if (Convert.ToInt32(day) == 2)
                {

                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.wednesday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.wednesday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();
                }

                else if (Convert.ToInt32(day) == 3)
                {


                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.thursday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.friday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();

                }
                else if (Convert.ToInt32(day) == 4)
                {

                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.friday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.friday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();

                }

                else if (Convert.ToInt32(day) == 5)
                {

                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.saturday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.saturday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();


                }
                else if (Convert.ToInt32(day) == 6)
                {


                    TimeSpan st = new TimeSpan(int.Parse(time.Split(':')[0]),
                               int.Parse(time.Split(':')[1]),
                               0);

                        TimeSpan et = new TimeSpan(int.Parse(time2.Split(':')[0]),
                                       int.Parse(time2.Split(':')[1]),
                                       0);

                        TimeSpan duration = et.Subtract(st);
                       SqlCommand cmds = new SqlCommand("update Attendence set time_in=@timein,time_out=@timeout,late=@late,half_day=@halfday,status=@status,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                        cmds.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                        cmds.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = time;
                        cmds.Parameters.AddWithValue("@late", 0);
                        cmds.Parameters.AddWithValue("@halfday", 0);
                        cmds.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = time2;
                        cmds.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                        cmds.Parameters.AddWithValue("@status", 0);
                        cmds.Parameters.AddWithValue("@overtime", Convert.ToInt32(duration.Hours));

                        conn.Open();
                        cmds.ExecuteNonQuery();
                        conn.Close();
                        return "Sunday Apsent" + Convert.ToString(duration.Hours);

                        
                    

                }
                // hamare pass Shift Ki Timming AA Chuki he



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
                        SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,time_out=@timeout,late=@late,half_day=@halfday,status=@status,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                        cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = timein;
                        cmd.Parameters.AddWithValue("@late", 0);
                        cmd.Parameters.AddWithValue("@halfday", 0);
                        cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = timeout;
                        cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                        cmd.Parameters.AddWithValue("@status", 1);
                        cmd.Parameters.AddWithValue("@overtime", Convert.ToInt32(Overtime.Hours));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "OnTime" + Convert.ToString(Overtime.Hours);


                    }
                    else if (time == "" && time2 == "")
                    {
                        SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,time_out=@timeout,late=@late,half_day=@halfday,status=@status,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                        cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value= "00:00:00";
                        cmd.Parameters.AddWithValue("@late", 0);
                        cmd.Parameters.AddWithValue("@halfday", 0);
                        cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value= "00:00:00";
                        cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                        cmd.Parameters.AddWithValue("@status", 0);
                        cmd.Parameters.AddWithValue("@overtime", 0);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "Absent";
                    }
                    else if ((timein > shiftSTime && timein <= ts) && timeout >= shiftETime)
                    {// ye sirft late ki condition he
                        if (Overtime.Hours < 0)
                        {
                            SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,time_out=@timeout,late=@late,half_day=@halfday,status=@status,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                            cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = timein;
                            cmd.Parameters.AddWithValue("@late", 1);
                            cmd.Parameters.AddWithValue("@halfday", 0);
                            cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = timeout;
                            cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.Parameters.AddWithValue("@overtime", 0);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return "Sirf Late";
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,late=@late,time_out=@timeout,half_day=@halfday,status=@status,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                            cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = timein;
                            cmd.Parameters.AddWithValue("@late", 1);
                            cmd.Parameters.AddWithValue("@halfday", 0);
                            cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = timeout;
                            cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.Parameters.AddWithValue("@overtime", Convert.ToInt32(Overtime.Hours));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();


                            return "Sirf Late" + Convert.ToString(Overtime.Hours);
                        }
                    }
                    //timein > ts && timein <= ts1
                    else if ((timein <= shiftSTime && (Check.Hours >= hh.Hours)) || (timein > ts && timein < ts1 && timeout >= shiftETime))
                    {
                        if (Overtime.Hours < 0)
                        {
                            SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,late=@late,half_day=@halfday,time_out=@timeout,status=@status,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                            cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = timein;
                            cmd.Parameters.AddWithValue("@late", 0);
                            cmd.Parameters.AddWithValue("@halfday", 1);
                            cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = timeout;
                            cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.Parameters.AddWithValue("@overtime", 0);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            // late nhi he lekin half day he
                            return "Half Day" + Convert.ToString(0);
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,late=@late,half_day=@halfday,status=@status,time_out=@timeout,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                            cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = timein;
                            cmd.Parameters.AddWithValue("@late", 0);
                            cmd.Parameters.AddWithValue("@halfday", 1);
                            cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = timeout;
                            cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.Parameters.AddWithValue("@overtime", Convert.ToInt32(Overtime.Hours));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();


                            return "Half Day" + Convert.ToString(Overtime.Hours);
                        }
                    }
                    else if ((timein > shiftSTime && Check.Hours < hh.Hours))
                    {
                        if (Overtime.Hours < 0)
                        {

                            SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,late=@late,half_day=@halfday,status=@status,time_out=@timeout,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                            cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = timein;
                            cmd.Parameters.AddWithValue("@late", 0);
                            cmd.Parameters.AddWithValue("@halfday", 0);
                            cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = timeout;
                            cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                            cmd.Parameters.AddWithValue("@status", 0);
                            cmd.Parameters.AddWithValue("@overtime", 0);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            return "Absent";
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,late=@late,half_day=@halfday,status=@status,time_out=@timeout,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                            cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = timein;
                            cmd.Parameters.AddWithValue("@late", 0);
                            cmd.Parameters.AddWithValue("@halfday", 0);
                            cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = timeout;
                            cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                            cmd.Parameters.AddWithValue("@status", 0);
                            cmd.Parameters.AddWithValue("@overtime", Convert.ToInt32(Overtime.Hours));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return "Absent";
                        }
                    }

                    else if ((timein > shiftSTime && timein <= ts) && (timeout < shiftETime || Check.Hours > hh.Hours))
                    {
                        if (Overtime.Hours < 0)
                        {
                            //(timein>shiftSTime && timein<=ts1) && (Check.Hours >= hh.Hours && timeout < shiftETime || timeout > ts1)
                            //timein > shiftSTime && (timeout < shiftETime && timeout.Hours > hh.Hours)

                            SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,time_out=@timeout,late=@late,half_day=@halfday,status=@status,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                            cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = timein;
                            cmd.Parameters.AddWithValue("@late", 1);
                            cmd.Parameters.AddWithValue("@halfday", 1);
                            cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = timeout;
                            cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.Parameters.AddWithValue("@overtime", 0);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            return "Late And Halfday" + Convert.ToString(0);
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,late=@late,time_out=@timeout,half_day=@halfday,status=@status,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                            cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = timein;
                            cmd.Parameters.AddWithValue("@late", 1);
                            cmd.Parameters.AddWithValue("@halfday", 1);
                            cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = timeout;
                            cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.Parameters.AddWithValue("@overtime", Convert.ToInt32(Overtime.Hours));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            return "Late And Halfday " + Convert.ToString(Overtime.Hours);
                        }
                    }

                    else
                    {
                        if (Overtime.Hours < 0)
                        {
                            SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,late=@late,time_out=@timeout,half_day=@halfday,status=@status,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                            cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = timein;
                            cmd.Parameters.AddWithValue("@late", 0);
                            cmd.Parameters.AddWithValue("@halfday", 0);
                            cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = timeout;
                            cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                            cmd.Parameters.AddWithValue("@status", 0);
                            cmd.Parameters.AddWithValue("@overtime", 0);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            return "Absent" + Convert.ToString(0);
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("update Attendence set time_in=@timein,late=@late,time_out=@timeout,half_day=@halfday,status=@status,overtime=@overtime where emp_id=@emp_id and date=@date", conn);
                            cmd.Parameters.AddWithValue("@emp_id", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@timein", SqlDbType.Time).Value = timein;
                            cmd.Parameters.AddWithValue("@late", 0);
                            cmd.Parameters.AddWithValue("@halfday", 0);
                            cmd.Parameters.AddWithValue("@timeout", SqlDbType.Time).Value = timeout;
                            cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                            cmd.Parameters.AddWithValue("@status", 0);
                            cmd.Parameters.AddWithValue("@overtime", Convert.ToInt32(Overtime.Hours));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            return "Absent" + Convert.ToString(Overtime.Hours);
                        }
                    }

                }
                else
                {
                    return "Not Valid";
                }
            }
            catch(Exception ex)
            {
                return "Please Insert Correct Timmings";
            }

           
        }


        [WebMethod]
        public static string Validate(string date, string id, string time, string time2, string day)
        {
            if(time=="" && time2 == "")
            {
                return "true";

            }
            else if(time!="" && time2 == "")
            {
                return "false";
            }
            else if(time=="" && time2 != "")
            {
                return "false";
            }
            

            try
            {


                 if (Convert.ToInt32(day) == 0)
                {
                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);

                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.monday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t1 = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.monday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te1 = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();
                }
                else if (Convert.ToInt32(day) == 1)
                {
                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);

                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.tuesday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t1 = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.tuesday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te1 = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();
                }
                else if (Convert.ToInt32(day) == 2)
                {
                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);

                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.wednesday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t1 = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.wednesday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te1 = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();
                }

                else if (Convert.ToInt32(day) == 3)
                {

                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);

                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.thursday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t1 = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.friday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te1 = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();

                }
                else if (Convert.ToInt32(day) == 4)
                {
                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);

                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.friday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t1 = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.friday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te1 = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();

                }

                else if (Convert.ToInt32(day) == 5)
                {
                    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                    SqlConnection conn = new SqlConnection(con1);

                    SqlCommand cmd = new SqlCommand(@"select s_time from Shifts as s
left join Emp_Shifts as es
on es.saturday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    t1 = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();

                    SqlCommand cmd1 = new SqlCommand(@"select e_time from Shifts as s
left join Emp_Shifts as es
on es.saturday =s.shift_id
where es.emp_id='" + Convert.ToInt32(id) + "'", conn);

                    conn.Open();
                    te1 = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();


                }
                else if (Convert.ToInt32(day) == 6)
                {
                   
                   
                        TimeSpan st = new TimeSpan(int.Parse(time.Split(':')[0]),
                               int.Parse(time.Split(':')[1]),
                               0);

                        TimeSpan et = new TimeSpan(int.Parse(time2.Split(':')[0]),
                                       int.Parse(time2.Split(':')[1]),
                                       0);

                        if (st > et)
                        {
                            return "false";
                        }
                        else
                        {
                            return "True";
                        }
                    }


              
                 

                TimeSpan ShiftStime= new TimeSpan(int.Parse(t1.Split(':')[0]),
                               int.Parse(t1.Split(':')[1]),
                               0);

                TimeSpan ShiftEtime = new TimeSpan(int.Parse(te1.Split(':')[0]),
                               int.Parse(t1.Split(':')[1]),
                               0);

                TimeSpan timein = new TimeSpan(int.Parse(time.Split(':')[0]),
                     int.Parse(time.Split(':')[1]),
                     0);
                TimeSpan timeout = new TimeSpan(int.Parse(time2.Split(':')[0]),
                     int.Parse(time2.Split(':')[1]),
                     0);
                //  ShiftStime.Subtract(oneHour);
                TimeSpan anhour = new TimeSpan();
                anhour += TimeSpan.FromHours(1);
                TimeSpan oneHour = ShiftStime.Subtract(anhour);

                if (timein<oneHour || timein>ShiftEtime)
                {
                    return "false"  ;
                }
                else if(time =="" && time2 == "")
                {
                    return "True";
                }
                else if (timein > timeout)
                {
                    return "false";
                }
                else if (timein > ShiftStime)
                {
                    return "True";
                }
                return "True"+ oneHour.ToString();
            }
            catch (Exception ex)
            {
                return "True";
            }
        }
    }
}