<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="InventoryOut.aspx.cs" Inherits="SpangleERP.GateIn.InventoryOut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="../Content/css/style.css" rel="stylesheet" />
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />


    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/bootstrap.js"></script>
    <script src="../Content/js/jquery-2.1.1.min.js"></script>

    <!--online-->

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <script type="text/javascript">
 

        function showtable() { 

            var x = document.getElementById("sDate");
            var sdate = x.value;

            var y = document.getElementById("endDate");
            var enddate = y.value;
                  var ddl = document.getElementById("<%=getitems.ClientID%>");
            var siteems = ddl.options[ddl.selectedIndex].value;
    
 
            $.ajax({  
                url: 'InventoryOut.aspx/approve', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                method: 'post',
                data: "{'Sdate':'" + sdate + "','FromDate':'" + enddate + "','ItemdNames':'" + siteems + "'}",
                success: function (data) {  
          
                    var employeeTable = $('#tblEmployee tbody');  
                    employeeTable.empty();  
                    for (var i = 0; i < data.d.length; i++) {

             employeeTable.append('<tr ><td class="control_label">' + data.d[i].Quantity + '</td><td class="control_label">' + data.d[i].Dateof + '</td><td class="control_label">' + data.d[i].grn_id + '</td>  <td class="control_label">' + data.d[i].Date + '</td></tr>');
                     
                    }
                    },  
                error: function (err) {  
                    alert(err);  
                }  
            });  
}

        function remainQuantity() {

     
             var ddl = document.getElementById("<%=getitems.ClientID%>");
            var siteems = ddl.options[ddl.selectedIndex].value;
          

            var txtget = $('#RemaningQuantity').val();
            var txttotal = $('#TotalQuantity').val();
            var txtout = $('#OutQuantity').val();

                  $.ajax({  
                url: 'InventoryOut.aspx/getremainquantity', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                      method: 'post',
                      data: "{'ItemdNames':'" + siteems + "','Remain':'" + txtget + "','ItemTotalQuantitiy':'" + txttotal + "','ItemOutQuantity':'" + txtout + "'}",
                      success: function (data) {

      
                          $('#RemaningQuantity').text(data.d);
                       
                      
                    },  
                error: function (err) {  
                    alert(err);  
                }  
            });  

        }

     function remainTotalQuantity() {

     
             var ddl = document.getElementById("<%=getitems.ClientID%>");
            var siteems = ddl.options[ddl.selectedIndex].value;
          

     
            var txttotal = $('#TotalQuantity').val();
          

                  $.ajax({  
                url: 'InventoryOut.aspx/gettotalquantity', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                      method: 'post',
                      data: "{'ItemdNames':'" + siteems + "','ItemTotalQuantitiy':'" + txttotal + "'}",
                      success: function (data) {

      
                          $('#TotalQuantity').text(data.d);
                       
                      
                    },  
                error: function (err) {  
                    alert(err);  
                }  
            });  

        }
         function OutQuantity() {

     
             var ddl = document.getElementById("<%=getitems.ClientID%>");
            var siteems = ddl.options[ddl.selectedIndex].value;
          

     
            var txttout = $('#OutQuantity').val();
          

                  $.ajax({  
                url: 'InventoryOut.aspx/getOutquantity', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                      method: 'post',
                      data: "{'ItemdNames':'" + siteems + "','ItemOutQuantitiy':'" + txttout + "'}",
                      success: function (data) {

      
                          $('#OutQuantity').text(data.d);
                       
                      
                    },  
                error: function (err) {  
                    alert(err);  
                }  
            });  

        }

                 function ItemStockByDate() {

            var x = document.getElementById("sDate");
            var sdate = x.value;

            var y = document.getElementById("endDate");
            var enddate = y.value;
             var ddl = document.getElementById("<%=getitems.ClientID%>");
            var siteems = ddl.options[ddl.selectedIndex].value;
          

     
            var txttStockDate = $('#StockOutByDate').val();
          

                  $.ajax({  
                url: 'InventoryOut.aspx/TotalStockOutByDate', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                      method: 'post',
                      data: "{'ItemdNames':'" + siteems + "','StockOutDate':'" + txttStockDate + "','startDate':'" + sdate + "','FromDate':'" + enddate + "'}",
                      success: function (data) {


                          $('#StockOutByDate').text(data.d);
                       
                      
                    },  
                error: function (err) {  
                    alert(err);  
                }  
            });  

        }

        

        function Insert() {
             var ddl = document.getElementById("<%=Inv_in.ClientID%>");
             var txtInv = ddl.options[ddl.selectedIndex].value;

            var txtquantity = ($('#quantity').val());
  
            $.ajax({
                url: 'InventoryOut.aspx/Save',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                method: 'post',

                data: "{'ItemsName':'" + txtInv + "','ItemsQuantity':'" + txtquantity + "'}",

                success: function (data) {

                    alert(data.d);
                    $

                },
                error: function (err) {
                    alert(err);
                }
            });
        }

      

        function SearchRecords() {
            var input, filter, table, tr, td,td1,td2, i;
            input = document.getElementById("search");
            filter = input.value.toUpperCase();
            table = document.getElementById("tblEmployee");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[0];
                td1 = tr[i].getElementsByTagName("td")[5];
                td2 = tr[i].getElementsByTagName("td")[2];
                if (td + td1) {
                    if ((td.innerHTML.toUpperCase().indexOf(filter)+td2.innerHTML.toUpperCase().indexOf(filter) + td1.innerHTML.toUpperCase().indexOf(filter)) > -3) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }


        function AllowOnlyNumbers(e) {

            e = (e) ? e : window.event;
            var key = null;
            var charsKeys = [
                97, // a  Ctrl + a Select All
                65, // A Ctrl + A Select All
                99, // c Ctrl + c Copy
                67, // C Ctrl + C Copy
                118, // v Ctrl + v paste
                86, // V Ctrl + V paste
                115, // s Ctrl + s save
                83, // S Ctrl + S save
                112, // p Ctrl + p print
                80 // P Ctrl + P print
            ];

            var specialKeys = [
                8, // backspace
                9, // tab
                27, // escape
                13, // enter
                35, // Home & shiftKey +  #
                36, // End & shiftKey + $
                37, // left arrow &  shiftKey + %
                39, //right arrow & '
                46, // delete & .
                45 //Ins &  -
            ];

            key = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;

            //console.log("e.charCode: " + e.charCode + ", " + "e.which: " + e.which + ", " + "e.keyCode: " + e.keyCode);
            //console.log(String.fromCharCode(key));

            // check if pressed key is not number 
            if (key && key < 48 || key > 57) {

                //Allow: Ctrl + char for action save, print, copy, ...etc
                if ((e.ctrlKey && charsKeys.indexOf(key) != -1) ||
                    //Fix Issue: f1 : f12 Or Ctrl + f1 : f12, in Firefox browser
                    (navigator.userAgent.indexOf("Firefox") != -1 && ((e.ctrlKey && e.keyCode && e.keyCode > 0 && key >= 112 && key <= 123) || (e.keyCode && e.keyCode > 0 && key && key >= 112 && key <= 123)))) {
                    return true
                }
                // Allow: Special Keys
                else if (specialKeys.indexOf(key) != -1) {
                    //Fix Issue: right arrow & Delete & ins in FireFox
                    if ((key == 39 || key == 45 || key == 46)) {
                        return (navigator.userAgent.indexOf("Firefox") != -1 && e.keyCode != undefined && e.keyCode > 0);
                    }
                    //DisAllow : "#" & "$" & "%"
                    //add e.altKey to prevent AltGr chars
                    else if ((e.shiftKey || e.altKey) && (key == 35 || key == 36 || key == 37)) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else {
                    return false;
                }
            }
            else {
                return true;
            }
        }


        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        } function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

        function limit(element, max) {
            var max_chars = max;
            if (element.value.length > max_chars) {
                element.value = element.value.substr(0, max_chars);
            }
        }



        function validates() {
            var ddl = document.getElementById("<%=getitems.ClientID%>");
            var siteems = ddl.options[ddl.selectedIndex].text;

            if ((document.getElementById("sDate").value == "" && document.getElementById("endDate").value == "" && siteems == "" )|| (document.getElementById("sDate").value > document.getElementById("endDate").value && document.getElementById("endDate").value!="" )) {
                alert("Feilds can not be null or Check Start Date To Search Is Less From To Date ");
                document.getElementById("sDate").focus();
                document.getElementById("endDate").focus();
                document.getElementById("siteems").focus();
                return false;

            }
            else {
                showtable();
                remainQuantity();
                remainTotalQuantity();
                OutQuantity();
                ItemStockByDate();
           
            }
        }
        //end//

    </script>

    <style type="text/css">
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">  

            <div class="container" style="width: 100%; margin-top: -26px;">
                <div class="panel-group" style="width: 100%;">
                    <div class="panel panel-primary" style="border-color: #0A408A; border: 2px;">
                        <div class="panel-heading" style="background-color: #0A408A;">
                            <h2 style="color: white; font-family: Cambria; font-weight: 200;">Inventory Out</h2>
                            <div class="table-responsive"  style="border-color:#0A408A;border:2px;">
                                <table class="pull-right">
                                    <tr>
                                        <td>
                                            <button type="button" class="btn btn-primary" style="font-size: 18px; background-color: #0A408A; color: white;" data-toggle="modal" data-target="#MachinePopup" data-backdrop="false">Insert Records</button>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>


                            </div>
                        </div>
                        <div class="panel-body" >

                            <!--Table-->
                     
                                 

                                    <br />
<div class="form-horizontal">

                                    <div class="form-group">
                                        <label class="control-label col-sm-3" style="padding-left:-10px;" for="sDate">Date To Search:</label>
                                        <div class="col-sm-3">
                                            <input type="date" id="sDate" class="form-control" " />
                                            
                                        </div>
                                            <label class="control-label col-sm-3" for="endDate">From To Search</label>
                                        <div class="col-sm-3">
                                      <input type="date" id="endDate" class="form-control" " />                                        </div>

                                    </div>
                                              <div class="form-group">
                                        <label class="control-label col-sm-3" style="padding-left:-10px;"  for="iTemsName">Item Name:</label>
                                        <div class="col-sm-3">
      <asp:DropDownList ID="getitems" runat="server" CssClass="form-control">


      </asp:DropDownList>
                                         
                                        </div>
                                       <label class="control-label col-sm-3" >Item Total Stock </label>
                                        <div class="col-sm-3">
                                    <label class="control-label" id="TotalQuantity"></label>

                                        </div>

                                 


          

                                    </div>

                                  <div class="form-group">
                                                   <label class="control-label col-sm-3" >Item Stock Out</label>
                                        <div class="col-sm-3">
                                               <label class="control-label" id="OutQuantity"></label>

                                        </div>
                                          <label class="control-label col-sm-3" >Item Remaning Stock </label>
                                        <div class="col-sm-3">
                     <label class="control-label"  id="RemaningQuantity" ></label>
                                      </div>
                        </div>


                                    <div class="form-group">
                                        <div class="col-xs-3 offset-5">
                                            <button type="button" class="btn btn-primary"  style="background-color: #0A408A;" onclick="validates();">Search</button>


                                        </div>
                                    </div>
    <div class="form-group"> 

           
                                        <div class="col-sm-5 offset-4">
                  <label class="control-label"  id="StockOutByDate"  ></label>
                                      </div>
    </div>
                        
    </div>
                                <br />
      <table id="tblEmployee" class="table table-responsive-lg table-hover" style="font-size: 15px;">

                                    <thead style="font-family: Cambria; font-size: 14px; text-decoration-style: solid; color: #0A408A;">
                                        <tr>
                                     
                                            <th>Quantity Out</th>
                                            <th>Inventory Out Date</th>
                                          
                                            <th>Grn #</th>
                                            <th>Inventory In Date</th>
                                            
                                      


                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                 
                    </div>
                </div>
    
</div>
            <!-- Modal one-->
            <div class="container">
                <div class="modal fade" id="MachinePopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                    aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #0A408A;">
                                <h2 style="color: white;">Create Items</h2>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>

                            </div>
                            <div class="modal-body mx-3">
                             

                                    <br />

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-2" for="ItemsName">InvIN_ID:</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="Inv_in" runat="server" CssClass="form-control"></asp:DropDownList>
                                            
                                        </div>
                                            <label class="control-label col-sm-2" for="quantity">Quantity</label>
                                        <div class="col-sm-3">
                                            <input type="number" id="quantity" class="form-control" min="0" onkeypress="return AllowOnlyNumbers(event);" onkeydown="limit(this, 5);" onpaste="return false" />
                                        </div>

                                    </div>

                               
                        


                                    <div class="form-group">
                                        <div class="col-xs-12 col-sm-offset-3">
                                            <button type="button" class="btn btn-primary" onclick="Insert()" style="background-color: #0A408A;">Save</button>


                                        </div>
                                    </div>
                       
                                    </div>
                               
    </div>
   
 
      </div>
      </div>
    </div>
                            </div>
              
          
    </form>

</asp:Content>




