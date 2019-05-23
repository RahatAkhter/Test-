using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SpangleERP.WareHouse
{
    public partial class WareHouses : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["id"] != null)
            {

                
                  GetUserDetail();
              
            }
            else
            {
                Response.Redirect("Index.aspx");
            }


        
        }
       


        [WebMethod]
        public static List<WareHouseClass> GetUserDetail()
        {
            List<WareHouseClass> list_item = new List<WareHouseClass>();


            DataTable dt = getuserdata();
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                WareHouseClass u = new WareHouseClass();
                u.wh_id = Convert.ToInt32(dt.Rows[i]["wh_id"].ToString());
                u.wh_name = dt.Rows[i]["wh_name"].ToString();
           
                u.city = dt.Rows[i]["city"].ToString();




                list_item.Add(u);
            }
            return list_item;
        }
        private static DataTable getuserdata()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select * from warehouse", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }
        [WebMethod]
        public static List<Users> PopulateDropDownList()
        {
            DataTable dt = new DataTable();
            List<Users> objDept = new List<Users>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT User_id,Email FROM users", con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objDept.Add(new Users
                            {
                                user_id = Convert.ToInt32(dt.Rows[i]["User_id"]),
                                email = dt.Rows[i]["Email"].ToString()

                            });
                        }
                    }
                    return objDept;
                }
            }
        }
        [WebMethod]
        public static string Save(string WhName,  string City)
        {
            
            string StopEmptyMfg = "";
            int i = 0;
            string cn = i.ToString();

    
               

                    if (WhName == "")
                    {
                        return "Fill All Fields Correctly...!";
                    }
                    else
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into warehouse values('" + WhName + "','" + City + "')", con);
                        cmd.ExecuteNonQuery();
                    }
                
               
        

            return "Save Items Sucessfully..!";
        }

        [WebMethod]
        public static string GotoPopup(string id, string txtname, string txtid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select wh_name from warehouse where wh_id ='" + id + "'", con);
            string getItemName = cmd.ExecuteScalar().ToString();
            cmd.ExecuteNonQuery();

            txtname = getItemName;


            return id; 
        }
        // end //
        [WebMethod]
        public static string Edit(string id, string txtname)
        {
          
            if (txtname != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("update  warehouse  set wh_name ='" + txtname + "'where wh_id ='" + id + "'", con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                return "Please fill Name";
            }

            return "Edit Succesfully";
        }
        //end//


       
    }
}