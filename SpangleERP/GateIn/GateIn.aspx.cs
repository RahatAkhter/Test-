using SpangleERP.invent;
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
using System.Diagnostics;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using iTextSharp.text.pdf;
using iTextSharp.text;

using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Drawing;

namespace SpangleERP.WareHouse
{
    public partial class GateIn : System.Web.UI.Page
    {

        public static int uid;
        public static int ?pid;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.bind();

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
        public static string printgate(string value)
        {
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 10f, 10f, 100f, 0f);


            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);


            PdfPTable table = new PdfPTable(2);

            PdfPCell cell = new PdfPCell(new Phrase("Gate In Items"));

            cell.Colspan = 2;

            cell.Border = 0;

            cell.HorizontalAlignment = 1;
           
            table.AddCell(cell);
            table.AddCell("Quantity");
            table.AddCell(" Items_Name");


            table.TotalWidth = 216f;

            //fix the absolute width of the table

            table.LockedWidth = true;



            //relative col widths in proportions - 1/3 and 2/3

            float[] widths = new float[] { 1f, 2f };

            table.SetWidths(widths);

            table.HorizontalAlignment = 1;

            //leave a gap before and after the table

            table.SpacingBefore = 20f;

            table.SpacingAfter = 30f;

            pdfDoc.Open();

            pdfDoc.Add(table);
   


            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString))

            {


                string query = "select vendor_id,vname from vendor";



                SqlCommand cmd = new SqlCommand(query, conn);

          

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())

                    {

                        while (rdr.Read())

                        {
                            string a =rdr[0].ToString();
                             table.AddCell(rdr[0].ToString());
                            string b = rdr[1].ToString();
                            table.AddCell(rdr[1].ToString());
                         

                        }

                    }
                


                pdfDoc.Close();

                return "";
            }
        }



        [WebMethod]
        public static List<GateItemPassClass> approve(string value)
        {
            List<GateItemPassClass> list_item = new List<GateItemPassClass>();
            pid = Convert.ToInt32(value);

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
            SqlCommand cmd = new SqlCommand(@"select c.gate_id,c.I_Quantity,c.Id,c.Item_id,i.items_name from gateitemsin as c left join gatein as p on p.gate_id=c.Id left join Items  as i on i.items_id=c.Item_id where c.gate_id=" + Convert.ToInt32(value) + " ", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

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
            getitems.DataSource = com.ExecuteReader();
            getitems.DataTextField = "items_name";
            getitems.DataValueField = "items_id";

            getitems.DataBind();
            conn.Close();
        }

       



        public void login()
        {
            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            string email = Session["login"].ToString();
            SqlCommand cmd = new SqlCommand(@"select  user_id from Users where email='" + email + "'", conn);
            conn.Open();
            uid = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

        }

        [WebMethod]
        public static List<GateItemPassClass> GateItems()
        {
            List<GateItemPassClass> list_item = new List<GateItemPassClass>();


            DataTable dt = getitemData();
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                GateItemPassClass u = new GateItemPassClass();
                u.ItemIn_Id = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                u.ItemsId = Convert.ToInt32(dt.Rows[i]["gate_id"].ToString());
                u.I_Quantity = Convert.ToInt32(dt.Rows[i]["I_Quantity"].ToString());
                u.GateIn_Id = Convert.ToInt32(dt.Rows[i]["Item_id"].ToString());





                list_item.Add(u);
            }
            return list_item;
        }
        private static DataTable getitemData()
        {

            string con1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            SqlConnection conn = new SqlConnection(con1);
            SqlCommand cmd = new SqlCommand(@"select * from gateitemsin", conn);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            conn.Close();

            return dt;

        }


        [WebMethod]
        public static string Save(string GateDate, string GateTime, string Vehicle, string ReferenceBy, string PoNum, string Drivers)
        {

            try
            {
                if (GateDate != "" && GateTime != "" && Vehicle != "" && ReferenceBy != "")
                {
                    if (PoNum != "")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into gatein values(@DATE,'" + GateTime.ToString() + "','" + Vehicle + "','" + Drivers + "','" + ReferenceBy + " ','0','" + PoNum + "','"+uid+"')", con);
                        cmd.Parameters.AddWithValue("@DATE", SqlDbType.Date).Value = GateDate;
                        cmd.ExecuteNonQuery();
                        con.Close();

                        SqlCommand getid = new SqlCommand("SELECT TOP 1 gate_id FROM gatein ORDER BY gate_id DESC", con);
                        con.Open();
                        int id = Convert.ToInt32(getid.ExecuteScalar());

                        con.Close();
                        return id.ToString();
                    }
                    else {
                        return "Select Value In DropDown";
                    }
                }
                else {
                    return "All Fields Are Required";

                }
            }
            catch (Exception ES) {
                return ES.Message;
            }


        }
        [WebMethod]
        public static string ItemInsert(string ItemsId, string ItemsNames, string Quantities)


        {
            int cn = 0;
            string chk = cn.ToString();
            if (ItemsId != "")
            {
                if (ItemsNames != chk && Quantities != chk)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into gateitemsin values('" + Quantities + "','" + ItemsId + "','" + ItemsNames + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    return "Choose Items And Its Quantity";
                }
            }
            else
            {
                return "No Gate In Found Please Fill Privous Form pROPERLY";
            }
            return "save items";
        }

        [WebMethod]
        public static string GotoPopup(string id, string txtname, string txtid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select items_name from Items where items_id ='" + id + "'", con);
            string getItemName = cmd.ExecuteScalar().ToString();
            cmd.ExecuteNonQuery();

            txtname = getItemName;


            return id;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            var gatid = "";
            var vendor_name = "";
            var date_of = "";

            if (pid != null)
            {

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

              
                Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 10f, 10f, 100f, 0f);

           


                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
          

                PdfPTable table = new PdfPTable(2);

          
                PdfPCell cell = new PdfPCell(new Phrase(""));

                cell.Colspan = 2;

                cell.Border = 0;
               

                cell.HorizontalAlignment = 1;

                table.AddCell(cell);
                
         
                table.AddCell(" Quantity");
                table.AddCell(" Items Name");
                //actual width of table in points

                table.TotalWidth = 506f;

                //fix the absolute width of the table

                table.LockedWidth = true;



                //relative col widths in proportions - 1/3 and 2/3

                float[] widths = new float[] { 3f, 3f };

                table.SetWidths(widths);

                table.HorizontalAlignment = 1;

                //leave a gap before and after the table

                table.SpacingBefore = 20f;

                table.SpacingAfter = 30f;


           
         
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString))

                {

                    string query = @"
Select p.drivername,p.reference,p.date,c.I_Quantity,i.items_name from gatein as p 
left join gateitemsin as c
on p.gate_id=c.gate_id
left join Items as i
on i.items_id=c.Item_id
where p.gate_id= " + pid + "";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    try

                    {

                        conn.Open();

                        using (SqlDataReader rdr = cmd.ExecuteReader())

                        {
                            
                            while (rdr.Read())

                            {
                                   gatid = rdr[0].ToString();
                           
                                   vendor_name=    rdr[1].ToString();
                                   date_of= rdr[2].ToString();

                                table.AddCell(rdr[3].ToString()) ;
                                table.AddCell(rdr[4].ToString());



                            }

                        }

                    }

                    catch (Exception ex)

                    {

                        Response.Write(ex.Message);

                    }

                    pdfDoc.Open();
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\GateIn\\logo\\SpangleMaster.jpg"));

                    image.ScaleAbsoluteWidth(593f);
                    image.ScaleAbsoluteHeight(75f);
                    image.SetAbsolutePosition(0,769);
                    image.Alignment = iTextSharp.text.Image.ALIGN_TOP;
                   

                    Chunk chunk = new Chunk(gatid, FontFactory.GetFont("dax-black"));
                    Chunk chunk1 = new Chunk(vendor_name, FontFactory.GetFont("dax-black"));
                    Chunk chunk2 = new Chunk(date_of, FontFactory.GetFont("dax-black"));
                    Chunk chunk3 = new Chunk(pid.ToString(), FontFactory.GetFont("dax-black"));




                    Paragraph p = new Paragraph("Gate Pass No :   "+chunk3);
                    p.SetLeading(0, 1);
                    p.IndentationLeft=30f;
                    Paragraph p1 = new Paragraph("VendorName  :  "+chunk1);
                    p1.SetLeading(0, 2);
                    p1.IndentationLeft = 30f;
                    Paragraph p2 = new Paragraph("Date  :  " + chunk2);
                    p2.SetLeading(0, 2);
                    p2.IndentationLeft = 30f;
                    Paragraph p3 = new Paragraph("DriverName  :  " + chunk);
                    p3.SetLeading(0, 2);
                    p3.IndentationLeft = 30f;
                    pdfDoc.Add(image);
                
                    pdfDoc.Add(p);
                    pdfDoc.Add(p1);
                    pdfDoc.Add(p2);
                    pdfDoc.Add(p3);

                    pdfDoc.Add(table);
                    pdfDoc.Close();
                 
                }
            }
           
        }

     
        //end this meth

        //end//
    }
}