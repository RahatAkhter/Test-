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
    public partial class Adjustment : System.Web.UI.Page
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
        }

        //[WebMethod]
        //public static List<int> GetMasterData()
        //{



        //    List<int> list_det = new List<int>();


           
        //    for (Int32 i = 0; i < 10; i++)
        //    {
              
        //        list_det.Add(i);
        //    }
        //    return list_det;



        //}


        [WebMethod]
        public static List<AttendenceData> GetData(string id, string date,string month)
        {

           
            
                List<AttendenceData> list_det = new List<AttendenceData>();


                DataTable dt = getprint(date,id,month);
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    AttendenceData p = new AttendenceData();

                    p.emp_name = Convert.ToString(dt.Rows[i]["emp_name"]);
                    p.Img = Convert.ToString(dt.Rows[i]["Img"]);
                    p.timein = Convert.ToString(dt.Rows[i]["timein"]);
                    p.time_out = Convert.ToString(dt.Rows[i]["timeout"]);
                    p.half = Convert.ToString(dt.Rows[i]["half"]);
                    p.late = Convert.ToString(dt.Rows[i]["late"]);
                    p.date = Convert.ToString(dt.Rows[i]["date"]);
                    p.status = Convert.ToInt32(dt.Rows[i]["status"]);
                    p.Overtime = Convert.ToString(dt.Rows[i]["overtime"]);



                list_det.Add(p);
                }
                return list_det;
          

          
        }

        private static DataTable getprint(string date,string id,string month)
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"Select e.emp_name,e.Img,isnull(a.time_in,'') as timein,isnull(a.time_out,'')as timeout,isnull(a.late,'')as late,isnull(a.half_day,'') as half,a.date,a.status,isnull(a.overtime,'') as overtime  
from Attendence as a
left join Employee as e
on e.emp_id=a.emp_id
where a.date LIKE @month +'%' and e.emp_id=@id ", conn);
           // cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
            cmd.Parameters.AddWithValue("@month",Convert.ToString(month));
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));

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
