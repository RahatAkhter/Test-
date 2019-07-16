using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Web.Services;


namespace SpangleERP.Sales
{
    public partial class Product_Categories1 : System.Web.UI.Page
    {
        public static int uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {

                string sd = (string)Session["id"];
                uid = Convert.ToInt32(sd);
            }
            else
            {
                Response.Redirect("~/Index.aspx");
            }
        }

        [WebMethod]
        public static List<ProductCategories_Class> GetProductCatDetail()
        {
            List<ProductCategories_Class> list_item = new List<ProductCategories_Class>();


            DataTable dt = getproductcategoriesdata();
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                ProductCategories_Class u = new ProductCategories_Class();
                u.procat_id = Convert.ToInt32(dt.Rows[i]["pcat_id"].ToString());
                u.procat_name = dt.Rows[i]["pcat_name"].ToString();






                list_item.Add(u);
            }
            return list_item;
        }
        private static DataTable getproductcategoriesdata()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select * from Procut_Catagories", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }




        [WebMethod]
        public static string Save(string Name)
        {
            try
            {
                if (Name != "")
                {


                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Procut_Catagories values (@name)", con);
                    cmd.Parameters.AddWithValue("@name",Name);
                    cmd.ExecuteNonQuery();
                    con.Close();


                    con.Close();
                    return "Save Successfully...!";

                }
                else
                {

                    return "Fill Name First...!";

                }

            }
            catch (Exception ES)
            {
                return ES.Message;
            }

        }

        [WebMethod]
        public static string EditProductCategories(string editedname, string editid)
        {

            if (editedname != "")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("update  Procut_Catagories set pcat_name =@name where pcat_id = @catid", con);

                cmd.Parameters.AddWithValue("@name",editedname);
                cmd.Parameters.AddWithValue("@catid",Convert.ToInt32(editid));
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return "Edit SuccessFully";
        }
        [WebMethod]
        public static string GetProCatId(string proid)
        {
            return proid;
        }
    }
}

public class ProductCategories_Class {

    public int procat_id { get; set; }
    public string procat_name { get; set; }
}