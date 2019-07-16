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
using SpangleERP.GateIn;

namespace SpangleERP.WareHouse
{
    public partial class GRN : System.Web.UI.Page
    {

        public static string Access;
        public static int id;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["id"] != null)
            {
                id = Convert.ToInt32(Session["id"].ToString());
               
            IssueBy();
            this.bind();
            }
            else
            {
                Response.Redirect("~/Index.aspx");
            }
            Access = PageName().ToString();


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

        private void bind()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand com = new SqlCommand("SELECT * from warehouse", conn); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            conn.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            wr.DataSource = com.ExecuteReader();
            wr.DataTextField = "wh_name";
            wr.DataValueField = "wh_id";

            wr.DataBind();
            conn.Close();
        }



        [WebMethod]
        public static string GotoPopup(string id, string txtname, string txtid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Items_name from Items where Items_id ='" + id + "'", con);
            string getItemName = cmd.ExecuteScalar().ToString();


            txtname = getItemName;


            return "calling";
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



        [WebMethod]
        public static List<Gate_InClass> GateItems()
        {
            List<Gate_InClass> list_item = new List<Gate_InClass>();


            DataTable dt = getitemData();
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                Gate_InClass u = new Gate_InClass();
                u.GateIn_Id = Convert.ToInt32(dt.Rows[i]["gate_id"].ToString());
                u.G_Date = dt.Rows[i]["date"].ToString();
                u.G_Status = Convert.ToInt32(dt.Rows[i]["status"].ToString());







                list_item.Add(u);
            }
            return list_item;
        }

        private static DataTable getitemData()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select * from gatein ORDER BY status ASC", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }

        [WebMethod]
        public static List<GateItemPassClass> Gatepassitem(string value)
        {
            List<GateItemPassClass> list_item = new List<GateItemPassClass>();


            DataTable dt = getrecord(value);
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {


                GateItemPassClass u = new GateItemPassClass();
                u.ItemIn_Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                u.I_Quantity = Convert.ToInt32(dt.Rows[i]["I_Quantity"].ToString());
                u.ItemsId = Convert.ToInt32(dt.Rows[i]["Item_id"].ToString());
                u.GateIn_Id = Convert.ToInt32(dt.Rows[i]["gate_id"].ToString());
                // ic.items_name = dt.Rows[i]["gate_id"].ToString();
                //   u.it.items_name = dt.Rows[i][u.it.items_name].ToString();
                u.Itemsname = dt.Rows[i]["items_name"].ToString();
                u.cat_name = dt.Rows[i]["cat_name"].ToString();

                list_item.Add(u);





            }
            return list_item;

        }

        [WebMethod]
        public static List<GateItemPassClass> approve(string value)
        {
            List<GateItemPassClass> list_item = new List<GateItemPassClass>();


            DataTable dt = getrecord(value);
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {


                GateItemPassClass u = new GateItemPassClass();
                u.ItemIn_Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                u.I_Quantity = Convert.ToInt32(dt.Rows[i]["I_Quantity"].ToString());
                u.ItemsId = Convert.ToInt32(dt.Rows[i]["Item_id"].ToString());
                u.GateIn_Id = Convert.ToInt32(dt.Rows[i]["gate_id"].ToString());
                // ic.items_name = dt.Rows[i]["gate_id"].ToString();
                //   u.it.items_name = dt.Rows[i][u.it.items_name].ToString();
                u.Itemsname = dt.Rows[i]["items_name"].ToString();


                list_item.Add(u);





            }
            return list_item;

        }

        private static DataTable getrecord(string value)
        {
            // valueid = value.ToString();
            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"
select c.gate_id,c.I_Quantity,c.Id,c.Item_id,i.items_name,cat.cat_name from gateitemsin
 as c left join gatein as p on p.gate_id=c.Id
  left join Items  as i on i.items_id=c.Item_id
  left join ItemsCategories as cat
  on cat.cat_id=i.cat_id
   where c.gate_id='"+Convert.ToInt32(value)+"' ", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }

        //[WebMethod]
        //public static string UpdateRecords(string value)
        //{
        //    string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(con1);
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand(@"update gatein set status = '1' where gate_id ='" + value.ToString() + "'", conn);

        //    cmd.ExecuteNonQuery();
        //    conn.Close();



        //    return "Approved";
        //}

        [WebMethod]
        public static string Insert(string Id, string mfg, string exp, string btc, string value, string ware,string cat)
        {
            string con2 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn1 = new SqlConnection(con2);
            conn1.Open();
            SqlCommand getid = new SqlCommand("select p.gate_id , p.status from gatein as p where p.gate_id='" + value.ToString() + "' AND p.status='1'", conn1);

            int chk = Convert.ToInt32(getid.ExecuteScalar());

            conn1.Close();
            if (cat == "POS")
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"insert into GRN values('" + Id + "',@dtmf,@dtex,@btc)", conn);
                cmd.Parameters.AddWithValue("@dtmf",DBNull.Value);
                cmd.Parameters.AddWithValue("@dtex", DBNull.Value);
                cmd.Parameters.AddWithValue("@btc", DBNull.Value);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                SqlCommand getGRN = new SqlCommand("select top 1 grn_id from GRN order By grn_id DESC", conn);
                string GRNget = getGRN.ExecuteScalar().ToString();
                conn.Close();

                conn.Open();

                SqlCommand getItems = new SqlCommand("select c.Item_id from gateitemsin as c left join GRN as p on p.id=c.Id  where p.grn_id='" + GRNget + "'", conn);
                string ItemsGet = getItems.ExecuteScalar().ToString();
                conn.Close();

                conn.Open();

                SqlCommand getquantity = new SqlCommand("select c.I_Quantity from gateitemsin as c left join GRN as p on p.id=c.Id  where p.grn_id='" + GRNget + "'", conn);
                string quantityGet = getquantity.ExecuteScalar().ToString();
                conn.Close();
                conn.Open();
                SqlCommand insertcmd = new SqlCommand("insert into inventry values('" + quantityGet + "','" + ware + "','" + GRNget + "','" + ItemsGet + "','Qurantine')", conn);
                insertcmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                SqlCommand cmdup = new SqlCommand(@"update gatein set status = '1' where gate_id ='" + value.ToString() + "'", conn);
                cmdup.ExecuteNonQuery();
                conn.Close();
            }
            else
            {

                if (mfg != "" && exp != "" && btc != "")
                {

                    DateTime dtMF = DateTime.Parse(mfg.ToString());
                    DateTime dtEx = DateTime.Parse(exp.ToString());



                    if (dtMF < dtEx)
                    {
                        string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                        SqlConnection conn = new SqlConnection(con1);
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(@"insert into GRN values('" + Id + "','" + dtMF + "','" + dtEx + "','" + btc.ToString() + "')", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        SqlCommand getGRN = new SqlCommand("select top 1 grn_id from GRN order By grn_id DESC", conn);
                        string GRNget = getGRN.ExecuteScalar().ToString();
                        conn.Close();

                        conn.Open();

                        SqlCommand getItems = new SqlCommand("select c.Item_id from gateitemsin as c left join GRN as p on p.id=c.Id  where p.grn_id='" + GRNget + "'", conn);
                        string ItemsGet = getItems.ExecuteScalar().ToString();
                        conn.Close();

                        conn.Open();

                        SqlCommand getquantity = new SqlCommand("select c.I_Quantity from gateitemsin as c left join GRN as p on p.id=c.Id  where p.grn_id='" + GRNget + "'", conn);
                        string quantityGet = getquantity.ExecuteScalar().ToString();
                        conn.Close();
                        conn.Open();
                        SqlCommand insertcmd = new SqlCommand("insert into inventry values('" + quantityGet + "','" + ware + "','" + GRNget + "','" + ItemsGet + "','Qurantine')", conn);
                        insertcmd.ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        SqlCommand cmdup = new SqlCommand(@"update gatein set status = '1' where gate_id ='" + value.ToString() + "'", conn);
                        cmdup.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        return "MFG is alaways less then Exp Date";
                    }
                }

                else
                {
                    return "All Feilds Are Required";
                }
            }

            return "Save Successfully";
        }
        //   else
        //   {
        //    return "Already Updated";
        // }


    





        [WebMethod]
        public static List<warehouse> IssueBy()
        {
            DataTable dt = new DataTable();
            List<warehouse> objDept = new List<warehouse>();

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
                            objDept.Add(new warehouse
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
    }
}


class Cls_Grn
{
    public string BatchNumber { get; set; }
    public string EXP { get; set; }
    public int grn_id { get; set; }
    public string MFG { get; set; }
    public string items_name { get; set; }
   

}