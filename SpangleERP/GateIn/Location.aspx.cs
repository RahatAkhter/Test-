using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpangleERP.WareHouse
{
    public partial class Location : System.Web.UI.Page
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
        public static List<Locations> GetUserDetail()
        {
            List<Locations> list_item = new List<Locations>();


            DataTable dt = getuserdata();
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
               Locations u = new Locations();
                u.loc_id = Convert.ToInt32(dt.Rows[i]["loc_id"].ToString());
                u.loc_name = dt.Rows[i]["loc_name"].ToString();
                u.loc_space = Convert.ToInt32(dt.Rows[i]["loc_space"].ToString());
                u.wh_id = Convert.ToInt32(dt.Rows[i]["wh_id"].ToString());
              





                list_item.Add(u);
            }
            return list_item;
        }
        private static DataTable getuserdata()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select * from locations", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }
        [WebMethod]
        public static List<WareHouseClass> PopulateDropDownList()
        {
            DataTable dt = new DataTable();
            List<WareHouseClass> objDept = new List<WareHouseClass>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT wh_id,wh_name FROM warehouse", con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objDept.Add(new WareHouseClass
                            {
                                wh_id = Convert.ToInt32(dt.Rows[i]["wh_id"]),
                                wh_name = dt.Rows[i]["wh_name"].ToString()
                                

                            });
                        }
                    }
                    return objDept;
                }


            }
        }

        //end//
        [WebMethod]
        public static string Save(string InsLocation, string WareHouse ,string spaces) {
            //string StopEmptyMfg = "";
            int i = 0;
            string cn = i.ToString();


            if ((WareHouse != "" && WareHouse != cn) && (spaces != "" && spaces != cn) )
            {

                if (InsLocation == "")
            {
                return "Fill All Fields Correctly...!";
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString); 
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into locations values('"+ InsLocation + "','" + spaces.ToString() + "','"+ WareHouse.ToString()+"')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else
            {
                return "All Field Reqired And Not Take 0 Aa A Value !";
            }


            return "Save Items Sucessfully..!";
        }
        //end//
        [WebMethod]
        public static string GotoPopup(string id, string txtname, string txtid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select loc_name from Locations where loc_id ='" + id + "'", con);
            string getItemName = cmd.ExecuteScalar().ToString();
            cmd.ExecuteNonQuery();

            txtname = getItemName;


            return id;
        }
        [WebMethod]
        public static string Edit(string id, string txtname)
        {
            if (txtname != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("update  Locations  set loc_name ='" + txtname + "'where loc_id ='" + id + "'", con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                return "Please fill Name";
            }
            return "Edit Succesfully";
        }

    }
}