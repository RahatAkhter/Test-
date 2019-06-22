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

namespace SpangleERP.invent
{
    public partial class ItemsCategories : System.Web.UI.Page
    {


        public static int id;
        public static string Access;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Session["id"] != null)
            {
                id = Convert.ToInt32(Session["id"].ToString());
                Access = PageName().ToString();

                GetUserDetail();
            }
            else
            {
                Response.Redirect("~/Index.aspx");  
            }


          
        }

        public string PageName()
        {
            string sPath = Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;


            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@" Select r.Rights from pages as p
 left join Roles_Content as r
 on p.page_id=r.Page_id
 left join Roles as rc
 on rc.Role_id=r.Role_id
 left join users as u
 on u.Role=rc.Role_id
 where u.User_id=@id and  p.URl=@pname", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@pname", sRet);

                conn.Open();
                string level = cmd.ExecuteScalar().ToString();
                conn.Close();


                return level;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }


        }


        [WebMethod]
        public static string Access_Levels()
        {

            return Access;
        }


        [WebMethod]
        public static List<ItemCategories> GetUserDetail()
        {
            List<ItemCategories> list_item = new List<ItemCategories>();


            DataTable dt = getuserdata();
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                ItemCategories u = new ItemCategories();
                u.cat_id = Convert.ToInt32(dt.Rows[i]["cat_id"].ToString());
                u.cat_name = dt.Rows[i]["cat_name"].ToString();
              //u.type = Convert.ToInt32(dt.Rows[i]["type"]);





                list_item.Add(u);
            }
            return list_item;
        }
        private static DataTable getuserdata()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select * from ItemsCategories", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }
        [WebMethod]
        public static string Save(string ItemsName)
        {

            try
            {
                if (ItemsName != "")
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ItemsCategories values('" + ItemsName + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Save Successfully";
                }
                return "Please Fill The Form Correctly";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        //[WebMethod]
        //public static string GotoPopup(string id, string txtname)
        //{

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("select cat_name from ItemsCategories where cat_id ='" + id + "'", con);
        //    string getItemName = cmd.ExecuteScalar().ToString();
        //    cmd.ExecuteNonQuery();

        //    txtname = getItemName;


        //    return id;
        //}


        [WebMethod]
        public static string Edit(string id, string txtname)
        {
            if (txtname != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("update  ItemsCategories  set cat_name ='" + txtname + "'where cat_id ='" + id + "'", con);
                cmd.ExecuteNonQuery();
            }
            else
            {

                return "Please Fill Name";
            }
            return "Edit Succesfully";
        }
        

    }
}