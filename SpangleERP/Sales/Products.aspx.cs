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
namespace SpangleERP.Sales
{
    public partial class Product_Categories : System.Web.UI.Page
    {
        public static int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                string Us = (string)Session["id"];
                id = Convert.ToInt32(Us.ToString());

                Bound_Type();
            }
            else
            {
                Response.Redirect("~/index.aspx");
            }
        }


        public void Bound_Type()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand cmd = new SqlCommand(@"select * from Procut_Catagories", conn);

            conn.Open();
            DDCategory.DataSource = cmd.ExecuteReader();
            DDCategory.DataTextField = "pcat_name";
            DDCategory.DataValueField = "pcat_id";
            DDCategory.DataBind();

            conn.Close();
            conn.Open();
            DropDownList2.DataSource = cmd.ExecuteReader();
            DropDownList2.DataTextField = "pcat_name";
            DropDownList2.DataValueField = "pcat_id";
            DropDownList2.DataBind();

            conn.Close();
            conn.Dispose();

        }






[WebMethod]

public static string AddProducts(string pname,string uicartons,string price,string units_in_measure,string cat_id,string weight)

        {

            try
            {

                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd = new SqlCommand(@"select Count(*) from Products where Product_Name = @pname and weight=@wei", conn);
                cmd.Parameters.AddWithValue("@pname",pname);
                cmd.Parameters.AddWithValue("@wei", weight);
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                if (count == 0)
                {
                    SqlCommand cmd1 = new SqlCommand(@"insert into Products values(@pname,@uinc,@price,@unim,@catid,@wei)", conn);
                    cmd1.Parameters.AddWithValue("@pname", pname);
                    cmd1.Parameters.AddWithValue("@uinc", Convert.ToInt32(uicartons));
                    cmd1.Parameters.AddWithValue("@price", (float)Convert.ToDecimal(price));
                    cmd1.Parameters.AddWithValue("@unim", units_in_measure);
                    cmd1.Parameters.AddWithValue("@catid", Convert.ToInt32(cat_id));
                    cmd1.Parameters.AddWithValue("@wei", weight);


                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    return "Products Add Successfully";
                }
                else
                {
                    return "Product with this name and weight already exist";
                }


            }
            catch (Exception ex)
            {
                return ""+ex.Message;
            }
        }


        [WebMethod]

        public static string Update(string pname, string uicartons, string price, string units_in_measure, string cat_id, string weight,string pid)

        {

            try
            {

                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
    SqlCommand cmd1 = new SqlCommand(@"update Products set Product_Name=@name , unit_in_carton=@unic,Price=@price,unit_in_measures=@uinm,pcat_id=@catid,weight=@weight where Product_id=@pid
", conn);
                    cmd1.Parameters.AddWithValue("@name", pname);
                    cmd1.Parameters.AddWithValue("@unic", Convert.ToInt32(uicartons));
                    cmd1.Parameters.AddWithValue("@price", (float)Convert.ToDecimal(price));
                    cmd1.Parameters.AddWithValue("@uinm", units_in_measure);
                    cmd1.Parameters.AddWithValue("@catid", Convert.ToInt32(cat_id));
                    cmd1.Parameters.AddWithValue("@weight", weight);
                cmd1.Parameters.AddWithValue("@pid", Convert.ToInt32(pid));



                conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();

                    return "Products Update Successfully";
              

            }
            catch (Exception ex)
            {
                return "" + ex.Message;
            }
        }

        [WebMethod]
        public static Product GetSpecific(string pid) {
           
            try
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand("select * from Products where Product_id = @pid", conn);
                cmd.Parameters.AddWithValue("@pid",Convert.ToInt32(pid));

                conn.Open();
                SqlDataReader rea = cmd.ExecuteReader();
                Product obj = new Product();

                while (rea.Read())
                {
                    obj.pname = rea["Product_Name"].ToString();
                    obj.weight = rea["weight"].ToString();
                    obj.unit_in_car =int.Parse( rea["unit_in_carton"].ToString());
                    obj.price = (float)Convert.ToDecimal(rea["Price"]);

                }
                return obj;
            }
            catch (Exception ex)
            {
                Product obj = new Product();
                 obj.pcat_name = ex.Message.ToString();
                return obj;
            }

        }


    }
}

public class Product
{
    public int pid { get; set; }
    public string pname { get; set; }
    public int unit_in_car { get; set; }
    public float price { get; set; }
    public string uinm { get; set; }
    public string  weight { get; set; }
    public string pcat_name { get; set; }
}