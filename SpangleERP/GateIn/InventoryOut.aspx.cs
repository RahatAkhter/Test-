using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpangleERP.GateIn
{
    public partial class InventoryOut : System.Web.UI.Page
    {
        public static int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {

                string uid = (string)Session["id"].ToString();
                id = Convert.ToInt32(uid);

                this.bind();
                this.bind1();
            }
            else
            {
                Response.Redirect("~/Index.aspx");
            }



        }

        [WebMethod]
        public static List<Inventory_OutClass> getall()
        {
            List<Inventory_OutClass> list_item = new List<Inventory_OutClass>();




            DataTable dt = getrecord1();

            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {


                    Inventory_OutClass u = new Inventory_OutClass();
                    u.Inv_Out = Convert.ToInt32(dt.Rows[i]["Inv_Out"].ToString());
                    u.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"].ToString());
                    u.Dateof = dt.Rows[i]["DateOf"].ToString();



                    list_item.Add(u);

                }
            }
            else
            {

                //return list_item;
            }
            return list_item;

        }


        private static DataTable getrecord1()
        {
            // valueid = value.ToString();

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select inv_out,Quantity ,DateOf from inventoryout ", conn);
            conn.Open();

            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }



        private void bind1()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand com = new SqlCommand("SELECT * from Items", conn); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            conn.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            getitems.DataSource = com.ExecuteReader();
            getitems.DataTextField = "items_name";
            getitems.DataValueField = "items_id";

            getitems.DataBind();
            getitems.Items.Insert(0, new ListItem("", ""));
            conn.Close();
        }



        [WebMethod]
        public static List<Inventory_OutClass> approve(string Sdate, string FromDate, string ItemdNames)
        {
            List<Inventory_OutClass> list_item = new List<Inventory_OutClass>();




            DataTable dt = getrecord(Sdate, FromDate, ItemdNames);

            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {


                    Inventory_OutClass u = new Inventory_OutClass();

                    u.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"].ToString());
                    u.Dateof = dt.Rows[i]["DateOf"].ToString();
                    u.grn_id = Convert.ToInt32(dt.Rows[i]["grn_id"].ToString());
                    u.Date = dt.Rows[i]["date"].ToString();



                    list_item.Add(u);

                }
            }
            else
            {

                //return list_item;
            }
            return list_item;

        }


        private static DataTable getrecord(string Sdate, string FromDate, string ItemdNames)
        {
            DataTable dt;
            // valueid = value.ToString();


            if (Sdate != "" && FromDate == "" && ItemdNames == "")
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@"select ino.DateOf,inv.grn_id,ino.Inv_Out,ino.Quantity,gin.date from InventoryOut as ino
left join inventry as inv
on ino.Invent_Id = inv.invent_id
left join GRN as grn
on grn.grn_id = inv.grn_id
left join gateitemsin as gt
on grn.id = gt.id
left join gatein as gin
on gin.gate_id = gt.gate_id
where ino.dateof = '" + Sdate + "'", conn);

                conn.Open();
                dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                conn.Close();
            }




            else if (Sdate != "" && ItemdNames != "" && FromDate == "")
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@" select ino.DateOf,inv.grn_id,ino.Inv_Out,ino.Quantity,gin.date from InventoryOut as ino
left join inventry as inv
on ino.Invent_Id = inv.invent_id
left join GRN as grn
on grn.grn_id = inv.grn_id
left join gateitemsin as gt
on grn.id = gt.id
left join gatein as gin
on gin.gate_id = gt.gate_id
where ino.Items_Id = '" + ItemdNames + "' and ino.dateof = '" + Sdate + "'", conn);
                conn.Open();
                dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                conn.Close();


            }




            else if (Sdate != "" && FromDate != "" && ItemdNames == "")
            {

                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@"select ino.DateOf, inv.grn_id,ino.Inv_Out,ino.Quantity,gin.date from InventoryOut as ino
left join inventry as inv
on ino.Invent_Id = inv.invent_id
left join GRN as grn
on grn.grn_id = inv.grn_id
left join gateitemsin as gt
on grn.id = gt.id
left join gatein as gin
on gin.gate_id = gt.gate_id
where ino.dateof between  '"+Sdate+"' and '"+FromDate+"'", conn);


                conn.Open();
                dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                conn.Close();


            }


            else if (Sdate != "" && FromDate != "" && ItemdNames != "")
            {

                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@"select ino.DateOf ,inv.grn_id,ino.Inv_Out,ino.Quantity,gin.date from InventoryOut as ino
left join inventry as inv
on ino.Invent_Id = inv.invent_id
left join GRN as grn
on grn.grn_id = inv.grn_id
left join gateitemsin as gt
on grn.id = gt.id
left join gatein as gin
on gin.gate_id = gt.gate_id
where ino.dateof between  '" + Sdate + "' and '" + FromDate + "' and ino.Items_Id = '" + ItemdNames + "'", conn);
                conn.Open();
                dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                conn.Close();


            }

            else
            {
                string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn = new SqlConnection(con1);
                SqlCommand cmd = new SqlCommand(@"select ino.DateOf, inv.grn_id,ino.Inv_Out,ino.Quantity,gin.date from InventoryOut as ino
left join inventry as inv
on ino.Invent_Id = inv.invent_id
left join GRN as grn
on grn.grn_id = inv.grn_id
left join gateitemsin as gt
on grn.id = gt.id
left join gatein as gin
on gin.gate_id = gt.gate_id
where ino.Items_Id ='" + ItemdNames + "'", conn);
                conn.Open();
                dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                conn.Close();

            }
            return dt;
        }


        [WebMethod]
        public static string Save(string ItemsName, string ItemsQuantity)
        {

            int i = 0;
            string cn = i.ToString();


            int getquantity = 0;


            int quantities = 0;

            int getnextrow = 0;
            int quantitiesok = 0;



            try
            {
                int orderItems = Convert.ToInt32(ItemsQuantity);
                if ((ItemsQuantity != cn && ItemsQuantity != "") && (ItemsName != ""))
                {
                    //if (MfgDate != StopEmptyMfg && ExpDate != StopEmptyMfg)
                    //{



                    DateTime now = DateTime.Now;
                    DateTime next = DateTime.Now;
                    //if (now <= next)
                    //{
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
                    con.Open();
                    int checkinventoryItems = Convert.ToInt16(ItemsName);
                    SqlCommand findOutIn = new SqlCommand("select Items_Id from Inventry where items_id= '" + ItemsName + "'", con);
                    int rowedcheckIn = Convert.ToInt32(findOutIn.ExecuteScalar());
                    if (rowedcheckIn == checkinventoryItems)
                    {
                        SqlCommand findOut = new SqlCommand("select count(Invent_Id) from InventoryOut where items_id= '" + ItemsName + "'", con);
                        int rowedcheck = Convert.ToInt32(findOut.ExecuteScalar());
                        SqlCommand findOut12 = new SqlCommand("select Invent_Id from Inventry where items_id= '" + ItemsName + "' Order by invent_id ASC", con);
                        int rowedcheck12 = Convert.ToInt32(findOut.ExecuteScalar());



                        if (rowedcheck > 0)
                        {

                            SqlCommand getlastidOut = new SqlCommand("select top 1 Invent_id from inventoryout where items_id='" + ItemsName + "' order by invent_id Desc ", con);
                            int lastidout = Convert.ToInt32(getlastidOut.ExecuteScalar());
                            SqlCommand getlastquantityOut = new SqlCommand("select top 1 sum(Quantity) from inventoryout where items_id='" + ItemsName + "' and invent_id ='" + lastidout + "' ", con);
                            int lastidoutquantity = Convert.ToInt32(getlastquantityOut.ExecuteScalar());
                            SqlCommand inventrowquantity = new SqlCommand("select top 1 Quantity from inventry where items_id='" + ItemsName + "' and invent_id ='" + lastidout + "' ", con);
                            int inventmatchrow = Convert.ToInt32(inventrowquantity.ExecuteScalar());


                            if (inventmatchrow >= lastidoutquantity)
                            {

                                SqlCommand getrowid = new SqlCommand("SELECT invent_id From Inventoryout  where items_id ='" + ItemsName + "'ORDER BY Invent_Id Desc", con);
                                int getfirstrow = Convert.ToInt32(getrowid.ExecuteScalar());

                                SqlCommand FIFO = new SqlCommand("SELECT Top 1 Quantity From Inventry  where items_id ='" + ItemsName + "'and  Invent_Id ='" + lastidout + "'", con);

                                quantities = Convert.ToInt32(FIFO.ExecuteScalar());
                                SqlCommand countquantityRow = new SqlCommand("SELECT count(Quantity)FROM Inventry WHERE Items_id = '" + ItemsName + "'", con);
                                int CountRows = Convert.ToInt32(countquantityRow.ExecuteScalar());
                                int LeftQuantity = quantities - lastidoutquantity;

                                SqlCommand StkOut = new SqlCommand("select top 1 sum(Quantity) from inventoryout where items_id='" + ItemsName + "'", con);
                                int TotalStockOut = Convert.ToInt32(StkOut.ExecuteScalar());
                                SqlCommand StkIn = new SqlCommand("select top 1 sum(Quantity) from inventry where items_id='" + ItemsName + "'", con);
                                int TotalStockIn = Convert.ToInt32(StkIn.ExecuteScalar());
                                int getleftTotal = TotalStockIn - TotalStockOut;
                                if (TotalStockOut <= TotalStockIn)
                                {
                                    if (orderItems <= getleftTotal)
                                    {
                                        while (i < CountRows)
                                        {
                                            if (getquantity <= orderItems)
                                            {
                                                SqlCommand getrowid1 = new SqlCommand("SELECT invent_id From Inventoryout  where items_id ='" + ItemsName + "'ORDER BY Invent_Id Desc", con);
                                                int getfirstrow1 = Convert.ToInt32(getrowid1.ExecuteScalar());
                                                SqlCommand second_row = new SqlCommand("SELECT Top 1 Quantity From Inventry  where items_id ='" + ItemsName + "'and Invent_Id >  '" + getfirstrow1 + "'", con);
                                                quantitiesok = Convert.ToInt32(second_row.ExecuteScalar());

                                                SqlCommand getother = new SqlCommand("SELECT Top 1 Invent_Id From Inventry where items_id ='" + ItemsName + "'and Invent_Id > '" + getfirstrow1 + "'", con);
                                                getnextrow = Convert.ToInt32(getother.ExecuteScalar());
                                                SqlCommand getgrns = new SqlCommand("select grn_id from inventry where invent_id ='" + getnextrow + "'", con);
                                                int grngets = Convert.ToInt32(getgrns.ExecuteScalar());
                                                SqlCommand expdates = new SqlCommand("select EXP from GRN where grn_id ='" + grngets + "'", con);
                                                DateTime gettexpdates = Convert.ToDateTime(expdates.ExecuteScalar());
                                                // DateTime twoMonthsBack = gettexpdates.AddMonths(-3);

                                                if (inventmatchrow > lastidoutquantity)
                                                {
                                                    if (orderItems > LeftQuantity)
                                                    {
                                                        SqlCommand cmd2 = new SqlCommand("insert into InventoryOut values('" + lastidout + "','" + LeftQuantity.ToString() + "','" + now.ToString() + "','"+ItemsName+"','" + id + "')", con);
                                                        cmd2.ExecuteNonQuery();
                                                    }
                                                    else
                                                    {
                                                        int hg = LeftQuantity - orderItems;
                                                        int lgh = LeftQuantity - hg;
                                                        SqlCommand cmd23 = new SqlCommand("insert into InventoryOut values('" + lastidout + "','" + lgh.ToString() + "','" + now.ToString() + "','"+ItemsName+"','" + id + "')", con);
                                                        cmd23.ExecuteNonQuery();
                                                        getquantity += LeftQuantity;
                                                    }

                                                    lastidoutquantity += quantitiesok;
                                                    getquantity += LeftQuantity;
                                                }


                                                int k = orderItems - getquantity;
                                                if (k > quantitiesok)
                                                {
                                                    SqlCommand cmd21 = new SqlCommand("insert into InventoryOut values('" + getnextrow + "','" + quantitiesok.ToString() + "','" + now.ToString() + "','"+ItemsName+"','" + id + "')", con);
                                                    cmd21.ExecuteNonQuery();
                                                }
                                                k = orderItems - getquantity;
                                                if (k <= quantitiesok && k > 0)
                                                {

                                                    int n = quantitiesok - k;
                                                    int all = quantitiesok - n;
                                                    SqlCommand cmd21 = new SqlCommand("insert into InventoryOut values('" + getnextrow + "','" + all.ToString() + "','" + now.ToString() + "','"+ItemsName+"','" + id + "')", con);
                                                    cmd21.ExecuteNonQuery();
                                                }
                                                getquantity += quantitiesok;


                                                i++;
                                            }
                                            else
                                            {

                                                return "All Items Get";
                                            }

                                        }

                                    }
                                    else
                                    {
                                        return "Items Not Stock";
                                    }
                                }

                                else
                                {
                                    return "All Items Sold";
                                }

                            }

                            else
                            {
                                return "Items Sold Out";
                            }


                        }





                        //
                        else
                        {

                            SqlCommand cmdcheckquantity = new SqlCommand("SELECT sum(Quantity)FROM Inventry WHERE items_id = '" + ItemsName + "'", con);
                            int TotalItemsQuantity = Convert.ToInt32(cmdcheckquantity.ExecuteScalar());
                            SqlCommand countquantityRow = new SqlCommand("SELECT count(Quantity)FROM Inventry WHERE items_id = '" + ItemsName + "'", con);
                            int CountRows = Convert.ToInt32(countquantityRow.ExecuteScalar());
                            if (TotalItemsQuantity >= orderItems)
                            {
                                SqlCommand getrowid = new SqlCommand("SELECT invent_id From Inventry  where items_id ='" + ItemsName + "'ORDER BY Invent_Id ASC", con);
                                int getfirstrow = Convert.ToInt32(getrowid.ExecuteScalar());
                                SqlCommand FIFO = new SqlCommand("SELECT Top 1 Quantity From Inventry  where items_id ='" + ItemsName + "'ORDER BY  Invent_Id ASC", con);
                                quantities = Convert.ToInt32(FIFO.ExecuteScalar());
                                SqlCommand getgrns = new SqlCommand("select grn_id from inventry where invent_id ='" + getfirstrow + "'", con);
                                int grngets = Convert.ToInt32(getgrns.ExecuteScalar());
                                SqlCommand expdates = new SqlCommand("select EXP from GRN where grn_id ='" + grngets + "'", con);
                                DateTime gettexpdates = Convert.ToDateTime(expdates.ExecuteScalar());
                                

                                while (i < CountRows)
                                {
                                    if (getquantity <= orderItems)
                                    {

                                        SqlCommand second_row = new SqlCommand("SELECT Top 1 Quantity From Inventry  where items_id ='" + ItemsName + "'and Invent_Id > '" + getnextrow + "'", con);
                                        quantitiesok = Convert.ToInt32(second_row.ExecuteScalar());
                                        SqlCommand getother = new SqlCommand("SELECT Top 1 Invent_Id From Inventry where items_id ='" + ItemsName + "'and Invent_Id > '" + getnextrow + "'", con);
                                        getnextrow = Convert.ToInt32(getother.ExecuteScalar());
                                        int k = orderItems - getquantity;
                                        if (k > quantitiesok)
                                        {
                                            SqlCommand cmd2 = new SqlCommand("insert into InventoryOut values('" + getnextrow + "','" + quantitiesok.ToString() + "','" + now.ToString() + "','"+ItemsName+"','" + id + "')", con);
                                            cmd2.ExecuteNonQuery();
                                        }
                                        if (k <= quantitiesok && k > 0)
                                        {
                                            int n = quantitiesok - k;
                                            int all = quantitiesok - n;
                                            SqlCommand cmd2 = new SqlCommand("insert into InventoryOut values('" + getnextrow + "','" + all.ToString() + "','" + now.ToString() + "','"+ItemsName+"','" + id + "')", con);
                                            cmd2.ExecuteNonQuery();
                                        }
                                        getquantity += quantitiesok;


                                        i++;

                                    }
                                    else
                                    {

                                        return "All Items Get";
                                    }
                                }






                            }
                            else
                            {
                                return "Items Not Stock";

                            }

                        }



                    }
                    else
                    {
                        return "Items Not IN Inventory";
                    }



                    //}
                    //else
                    //{
                    //    return "Choose CorrectDate";
                    //}
                    return "Save Items Sucessfully";
                }
                else
                {
                    return "fill Are Required";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + "Not Valid";
            }



        }



        private void bind()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);

            SqlCommand com = new SqlCommand("SELECT * from Items", conn); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            conn.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            Inv_in.DataSource = com.ExecuteReader();
            Inv_in.DataTextField = "items_name";
            Inv_in.DataValueField = "items_id";

            Inv_in.DataBind();
            conn.Close();
        }

        [WebMethod]
        public static List<string> getCustomerNames(string prefixText)
        {
            List<string> customers = new List<string>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select InvIn_Name As Name  from InventoryIn where " +
                    "InvIn_Name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            customers.Add(dr["Name"].ToString());
                        }
                    }
                    conn.Close();
                }

            }
            return customers;
        }
        //End//
        [WebMethod]
        public static string getremainquantity(string ItemdNames, string Remain, string ItemTotalQuantitiy, string ItemOutQuantity)
        {


            string con11 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn1 = new SqlConnection(con11);
            conn1.Open();
            SqlCommand counter = new SqlCommand("select count(Quantity) from inventry where items_id='" + ItemdNames + "'", conn1);
            int count11 = Convert.ToInt32(counter.ExecuteScalar());


            if (count11 > 0)
            {
                SqlCommand counter22 = new SqlCommand("select count(Quantity) from INVENTORYOUT where items_id='" + ItemdNames + "'", conn1);
                int count20 = Convert.ToInt32(counter22.ExecuteScalar());
                if (count20 > 0)
                {
                    SqlCommand getlastquantityOut = new SqlCommand("select sum(Quantity) from inventoryout where items_id='" + ItemdNames + "'", conn1);
                    int lastidoutquantity = Convert.ToInt32(getlastquantityOut.ExecuteScalar());
                    SqlCommand inventrowquantity = new SqlCommand("select sum(Quantity) from inventry where items_id='" + ItemdNames + "' ", conn1);
                    int inventmatchrow = Convert.ToInt32(inventrowquantity.ExecuteScalar());
                    ItemTotalQuantitiy = inventmatchrow.ToString();
                    ItemOutQuantity = lastidoutquantity.ToString();
                    int get = inventmatchrow - lastidoutquantity;

                    Remain = get.ToString();
                }
                else
                {

                    SqlCommand inventrowquantity = new SqlCommand("select sum(Quantity) from inventry where items_id='" + ItemdNames + "' ", conn1);
                    int inventmatchrow = Convert.ToInt32(inventrowquantity.ExecuteScalar());
                    Remain = inventmatchrow.ToString();
                    ItemTotalQuantitiy = inventmatchrow.ToString();
                }

            }
            else
            {

                return "Items Not Found In Inventory";
            }
            return Remain;
    }


    [WebMethod]
        public static string gettotalquantity(string ItemdNames, string ItemTotalQuantitiy)
        {


            string con11 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn1 = new SqlConnection(con11);
            conn1.Open();
            SqlCommand counter = new SqlCommand("select count(Quantity) from inventry where items_id='" + ItemdNames + "'", conn1);
            int count11 = Convert.ToInt32(counter.ExecuteScalar());


            if (count11 > 0)
            {

                SqlCommand inventrowquantity = new SqlCommand("select sum(Quantity) from inventry where items_id='" + ItemdNames + "' ", conn1);
                int inventmatchrow = Convert.ToInt32(inventrowquantity.ExecuteScalar());



                ItemTotalQuantitiy = inventmatchrow.ToString();


            }
            else
            {

                return "Items Not Found In Inventory";
            }
            return ItemTotalQuantitiy;
        }

        [WebMethod]
        public static string getOutquantity(string ItemdNames, string ItemOutQuantitiy)
        {


            string con11 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn1 = new SqlConnection(con11);
            conn1.Open();
            SqlCommand counter = new SqlCommand("select count(Quantity) from inventry where items_id='" + ItemdNames + "'", conn1);
            int count11 = Convert.ToInt32(counter.ExecuteScalar());


            if (count11 > 0)
            {

                SqlCommand counter22 = new SqlCommand("select count(Quantity) from INVENTORYOUT where items_id='" + ItemdNames + "'", conn1);
                int count20 = Convert.ToInt32(counter22.ExecuteScalar());
                if (count20 > 0)
                {
                    SqlCommand getlastquantityOut = new SqlCommand("select sum(Quantity) from inventoryout where items_id='" + ItemdNames + "'", conn1);
                    int lastidoutquantity = Convert.ToInt32(getlastquantityOut.ExecuteScalar());



                    ItemOutQuantitiy = lastidoutquantity.ToString();
                }
                else {
                    int j = 0;
                    ItemOutQuantitiy = j.ToString(); 

                }
            }
            else
            {

                return "Items Not Found In Inventory";
            }
            return ItemOutQuantitiy;
        }



        [WebMethod]
        public static string TotalStockOutByDate(string ItemdNames, string StockOutDate, string FromDate,string startDate)
        {

            if (ItemdNames != "")
            {
                string con11 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
                SqlConnection conn1 = new SqlConnection(con11);
                conn1.Open();
                SqlCommand counter = new SqlCommand("select count(Quantity) from inventry where items_id='" + ItemdNames + "'", conn1);
                int count11 = Convert.ToInt32(counter.ExecuteScalar());


                if (count11 > 0)
                {

                    SqlCommand counter22 = new SqlCommand("select count(Quantity) from INVENTORYOUT where items_id='" + ItemdNames + "'", conn1);
                    int count20 = Convert.ToInt32(counter22.ExecuteScalar());
                    if (count20 > 0)
                    {
                        SqlCommand counter32 = new SqlCommand("select count(Quantity) from INVENTORYOUT where items_id='" + ItemdNames + "' and DateOf = '"+startDate+"'", conn1);
                        int count30 = Convert.ToInt32(counter32.ExecuteScalar());
                        SqlCommand counter42 = new SqlCommand("select count(Quantity) from INVENTORYOUT where items_id='" + ItemdNames + "' and DateOf  Between  '" + startDate + "'And '"+FromDate+"'", conn1);
                        int count40 = Convert.ToInt32(counter42.ExecuteScalar());
                        if (startDate != "" && FromDate == "" && count30 > 0)
                        {
                            SqlCommand getlastquantityOut = new SqlCommand("select sum(Quantity) from inventoryout where items_id='" + ItemdNames + "' and DateOf = '" + startDate + "'", conn1);
                            int lastidoutquantity = Convert.ToInt32(getlastquantityOut.ExecuteScalar());



                            StockOutDate = lastidoutquantity.ToString();
                            return StockOutDate + "   Quantity Out By This Date";
                        }
                 
                        else if (startDate != "" && FromDate != "" && count40 > 0)
                        {
                            SqlCommand getlastquantityOut = new SqlCommand("select sum(Quantity) from inventoryout where items_id='" + ItemdNames + "' and DateOf between '" + startDate + "' and '" + FromDate + "'", conn1);
                            int lastidoutquantity = Convert.ToInt32(getlastquantityOut.ExecuteScalar());



                            StockOutDate = lastidoutquantity.ToString();
                            return StockOutDate + "  Quantity Out Between This Dates";
                        }


                    }
                    else
                    {
                        StockOutDate = "Stock Not Out By This Item   ";
                    }
                }
                else
                {

                    return "Items Not Found In Inventory";
                }
            }
            else {

                return "Select Item First";
            }
            return StockOutDate + "" ;
        }
    }
}