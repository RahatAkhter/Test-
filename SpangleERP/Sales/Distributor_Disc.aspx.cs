using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
namespace SpangleERP.Sales
{
    public partial class Distributor_Disc : System.Web.UI.Page
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
           
            conn.Dispose();

        }

        [WebMethod]
        public static string Insert(string pcat_id,string dis_id,string dis,string mprice,string supplier)
        {


            try
            {

                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);

                SqlCommand cmd = new SqlCommand(@"select  Count(*) from Distributor_Disc where pcat_id=@catid and dist_id=@id", conn);
                cmd.Parameters.AddWithValue("@catid",Convert.ToInt32(pcat_id));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(dis_id));

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                if (count == 0)
                {
                    SqlCommand cmd1 = new SqlCommand(@"insert into Distributor_Disc values(@cat_id,@dis_id,@dis1,@mprice,@supplier)", conn);
                    cmd1.Parameters.AddWithValue("@cat_id", Convert.ToInt32(pcat_id));
                    cmd1.Parameters.AddWithValue("@dis_id", Convert.ToInt32(dis_id));
                    cmd1.Parameters.AddWithValue("@dis1", Convert.ToDouble(dis));
                    cmd1.Parameters.AddWithValue("@mprice", Convert.ToDouble(mprice));
                    cmd1.Parameters.AddWithValue("@supplier", Convert.ToDouble(supplier));

                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();

                    return "Save Successfully";

                }

                else
                {
                    return "This Catagory Discount Already Exist";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}

public class Cls_Distributor_Dic
{

    public int disc_id { get; set; }
    public string Dis_Discount { get; set; }
    public string Dis_Market_Pass { get; set; }
    public string Dis_Supply_Expense { get; set; }
    public string pcat_name { get; set; }
    public string Zone_Name { get; set; }
    public string dist_name { get; set; }

}