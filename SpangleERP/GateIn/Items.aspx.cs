using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using SpangleERP.WareHouse;

namespace SpangleERP.invent
{
    public partial class Items : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {

              
                GetUserDetail();
            this.bind();
            }
            else
            {
                Response.Redirect("~/Index.aspx");
            }



           
        }


    

        private void bind()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand com = new SqlCommand("SELECT * from ItemsCategories", conn); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            conn.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            cat_id.DataSource = com.ExecuteReader();
            cat_id.DataTextField = "cat_name";
            cat_id.DataValueField = "cat_id";

            cat_id.DataBind();
            conn.Close();
        }
        [WebMethod]
        public static List<ItemsClass> GetUserDetail()
        {
            List<ItemsClass> list_item = new List<ItemsClass>();


            DataTable dt = getuserdata();
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                ItemsClass u = new ItemsClass();
                u.items_id = Convert.ToInt32(dt.Rows[i]["items_id"].ToString());
                u.items_name = dt.Rows[i]["items_name"].ToString();

                //u.cat_id = Convert.ToInt32(dt.Rows[i]["cat_id"].ToString());




                list_item.Add(u);
            }
            return list_item;
        }
        private static DataTable getuserdata()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select * from Items", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }
        [WebMethod]
        public static string Save(string items_name, int cat_id)
        {

            try
            {
                if (cat_id != 0 && items_name != "")
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Items values('" + items_name + "','" + cat_id + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return "Save Successfully";
        }


        [WebMethod]
        public static string GotoPopup(string id, string txtname, string txtid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Items_name from Items where Items_id ='" + id + "'", con);
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
                SqlCommand cmd = new SqlCommand("update  Items  set Items_name ='" + txtname + "'where Items_id ='" + id + "'", con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                return "Please fill Name";
            }
            return "Edit Succesfully";
        }
        //end//
        [WebMethod]
        public static List<ItemCategories> PopulateDropDownList()
        {
            DataTable dt = new DataTable();
            List<ItemCategories> objDept = new List<ItemCategories>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT cat_id,cat_name FROM ItemsCategories", con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objDept.Add(new ItemCategories
                            {
                                cat_id = Convert.ToInt32(dt.Rows[i]["cat_id"]),
                                cat_name = dt.Rows[i]["cat_name"].ToString()

                            });
                        }
                    }
                    return objDept;
                }
            }
        }

        //end//

    }
}
