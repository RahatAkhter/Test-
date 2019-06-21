<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="GateIn.aspx.cs" Inherits="SpangleERP.WareHouse.GateIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <meta charset="utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
    
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
    <link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" rel="stylesheet" />
<script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
 <script type="text/javascript">
     var $j = jQuery.noConflict();
     var Access = "";
     
        var view = false;
        var Create = false;
        var Update = false;

     $j(document).ready(function () {
         $('#hide').hide();
        
          Access_Levels();

         if (Create == true) {
                $j('#In').show();
            }
            else {
                $j('#In').hide();
            }

            if (view == true) {
                Show();
            }
            else {
                alert("You Have Not Rights to View Data");

            }


          
     });
     function Access_Levels() {

                    $.ajax({
                    url: 'GateIn.aspx/Access_Levels',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    method: 'post',
                        data: "{}",
                        async: false,
                        success: function (data) {
                           
                        Access = data.d;
                        view = Access.includes("V");
                        Create = Access.includes("I");
                        Update = Access.includes("U");
                        
                    },
                    error: function (err) {
                        alert(err);
                    }
            });

            
              
                
            }
      function Show() {

         
                    $j('#datatable').DataTable({
                        "aLengthMenu": [[10, 25, 5], [10, 25, 5]],
                        "iDisplayLength": 5,
                           
                        
                        columns: [

                            { 'data': 'G_Date' },
                            { 'data': 'G_Time' },
                            { 'data': 'VehicleNo' },

                            { 'data': 'DriverName' },
                            { 'data': 'ReferenceBy' },
                            { 'data': 'PO_Number' },
                             

                            {
                                'data': 'G_Status',

                                'sortable': false,

                                'render': function (webSite) {
                                    if (webSite == 0) return "Not Aprove"

                                    else return "Aprove";

                                }


                            },
                           
                          {
                                'data': 'GateIn_Id',

                                

                              'render': function (val) {
                                  return '<button type="button" id="vw" value="' + val + '"  onclick="Popup(this.value);"   data-toggle="modal" data-target="#Popup" class="btn btn-primary" style="background-color:#0A408A;">View</button>';

                                }


                            }
                   
                    
                            
                                        
                    ],
                bServerSide: true,
                sAjaxSource: 'InventoryService.asmx/Get_All_GateIn',
                sServerMethod: 'post'
            });

        }


      function Popup(Val) {
         

  $.ajax({  
                url: 'GateIn.aspx/approve', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
      method: 'post',

      data: "{'value':'" + Val + "'}",
      async: false,
      success: function (data) {
          $('#ok').text(data.d[0].GateIn_Id);
           $('#ok2').text(data.d[0].GateIn_Id);
    
                    var employeeTable = $('#tblEmployee2 tbody');  
          employeeTable.empty();
          count = data.d.length;
                    for (var i = 0; i < data.d.length; i++) {
 employeeTable.append('<tr ><td class="control_label">' + data.d[i].I_Quantity + '</td><td class="control_label">' + data.d[i].Itemsname + '</td>')
          }

                    },  
                error: function (err) {  
                    alert(err);  
                }  
            });

            

        }


   

     function SomeDeleteRowFunction(row) {
        var d = row.parentNode.parentNode.rowIndex;
      document.getElementById('CallTable').deleteRow(d);
     }
     
    
      
     

     function Insert() {
         //g sir g??
       var xs = document.getElementById("Date");
                 var txtdates = xs.value
        
         var txttime = ($('#Time').val());
         var txtvehicle = ($('#Vehicle').val());
         var txtreferences = ($('#Reference').val());
        
         var txtPO = ($('#POnum').val());
         var txtdriver = ($('#Status').val());
          $.ajax({  
                url: 'GateIn.aspx/Save', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                     method: 'post',
              data: "{'GateDate':'" + txtdates + "','GateTime':'" + txttime + "','Vehicle':'" + txtvehicle + "','ReferenceBy':'" + txtreferences + "','PoNum':'" + txtPO + "','Drivers':'" + txtdriver + "'}",
                  
              success: function (data) {
                  $('#txtgetid').text(data.d); 
                      
                    },  
                error: function (err) {  
                    alert(err);  
                }  
               });  
     }

     function CallEditPopup(Val,i) {
         var ItemsName = document.getElementById("EditName");
         var getid = ItemsName.innerText;
         var Items_Id = document.getElementById("Item_id");
             var getname = ItemsName.innerText;
     
                    
                  $.ajax({  
                url: 'Items.aspx/GotoPopup', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                      method: 'post',
                      data: "{'id':'" + Val + "','txtname':'" + getid + "','txtid':'" + Items_Id + "'}",
                     
                      success: function (data) {
                          $('#Item_id').text(data.d);

                         
                          
                 
                    },  
                error: function (err) {  
                    alert(err);  
                }  
            });  


     }




        function EditRecords() {
        
            var txtname = ($('#EditName').val());
            
            var xs = document.getElementById("Item_id");
            var dobs = xs.innerText;
           
                  $.ajax({  
                url: 'Items.aspx/Edit', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                      method: 'post',
                    data: "{'id':'" + dobs + "','txtname':'" + txtname + "'}",
                           
                      success: function (data) { 
                     
                          alert(data.d);
                         
                
                      },  
           
                error: function (err) {  
                    alert(err);  
                }  
            });  
         

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
        }function onlyAlphabets(e, t) {
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
    if(element.value.length > max_chars) {
        element.value = element.value.substr(0, max_chars);
    } 
       }
     function savetable() {
             
        
                 var n = $("#CallTable").find("tr").length;
                 for (var i = 1; i < n - 1; i++) {
                     var Id = $("#txtgetid").text();
                    
                     var ItemsNames = $("#CallTable").find("tr").eq(i + 1).find("td").eq(0).text();
                     var Quantities = $("#CallTable").find("tr").eq(i + 1).find("td").eq(1).text();
                     
                     $.ajax({
                         type: "Post",
                         url: "GateIn.aspx/ItemInsert",
                         data: "{'ItemsId':" + Id + ",'ItemsNames':'" + ItemsNames + "','Quantities':'" + Quantities + "'}",
                          contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         success: function (data) {
                             
                             window.location = "GateIn.aspx";
                             //alert("complete");
                         },

                         error: function (err) {
                             alert(err);
                         }
                     })

         }
         $('#parent').show();
         $('#hide').hide();

            // })
        
  }//)
     function ViewRecords() {
         $.ajax({
             url: 'GateIn.aspx/getitems',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             method: 'post',
             data: "{}",
             success: function (data) {
                
                 var employeeTable = $('#viewsdata tbody');
                 employeeTable.empty();
                 for (var i = 0; i < data.d.length; i++) {

                     //employeeTable.append('<tr><td>' + data.d[i].Emp_id + '</td><td>' +data.d[i].Emp_name.toString() + '</td><td> <Input type="time"  id="Txt' + i + '"  class="form-control"/></td><td><button type="button" id="btnAdd" class="btn btn-xs btn-primary   value="' + data.d[i].Emp_id + '"  onclick="add(this.value,' + i + ');">Check In</button></td><td> <Input type="time"  id="Txts' + i + '" class="form-control" /></td ><td><button type="button"  class="btn btn-xs btn-primary value="' + data.d[i].Emp_id + '" onclick="update(this.value,' + i + ');" >Check Out</button></td> </tr > ');
                     employeeTable.append('<tr ><td class="control-label" style="Text-align:center;" >' + data.d[i].ItemIn_Id + '</td><td class="control_label">' + data.d[i].ItemsId  + '</td><td class="control_label">' + data.d[i].I_Quantity + '</td><td class="control_label">' + data.d[i].GateIn_Id + '</td></tr>')
                 }
             },
             error: function (err) {
                 alert(err);
             }
         });
     }
              function validate()  
              {  
                       if (  document.getElementById("getInQuantity").value <= 0 )
      {  
                 alert("Please Insert Quantity or Quantity Not be zero");  
                 document.getElementById("getInQuantity").focus();  
                           return false; 
                                 
                  }
                   else {
                               var quantity = $("#getInQuantity").val();
                               var ddl = document.getElementById("<%=getitems.ClientID%>");
                               var siteems = ddl.options[ddl.selectedIndex].value;

                            var n = $("#CallTable").find("tr").length;
         var flag = true;
         for (var i = 0; i < n - 1; i++) {

             var pid = $("#CallTable").find("tr").eq(i + 1).find("td").eq(0).text();

            

             if (siteems == pid) {
                 flag = false;
             }


         }

                            if (flag == false) {
             alert("You Already Select This Item");
         }
         else {

             var markup = "<tr><td>" + siteems + "</td><td>" + quantity + "</td><td><input type='button'; value='Delete Row' class='btn btn-danger' onclick='SomeDeleteRowFunction(this)'> </td></tr>";
                               $("#CallTable tbody").append(markup);
         }
                               
                           } 
      
     }

     function validate1()  
{  
     
              if ( document.getElementById("Date").value=="" )
      {  
                 alert("date");  
                 document.getElementById("Date").focus();  
                 return false;  
             }
              if ( document.getElementById("Time").value=="" )
      {  
                 alert("Please Enter Time");  
                 document.getElementById("Time").focus();  
                 return false;  
             }
             
               if ( document.getElementById("Vehicle").value=="" )
      {  
                 alert("Please Enter Vehicle Number");  
                 document.getElementById("Vehicle").focus();  
                 return false;  
         }
          if ( document.getElementById("Status").value=="" )
      {  
                 alert("Please Enter Driver Name");  
                 document.getElementById("Status").focus();  
                 return false;  
             }
               if ( document.getElementById("Reference").value=="" )
      {  
                 alert("Please Enter Vendor Name");  
                 document.getElementById("Reference").focus();  
                 return false;  
             }
         if (document.getElementById("POnum").value == "") {
             alert("Please Enter purchase order number");
             document.getElementById("POnum").focus();

             return false;
         }
         $j('#parent').hide();
         $j('#hide').show();

       
         
             
      
             
         }
     </script>
    
  <style type="text/css">
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <form runat="server">
    <div class="container" style="width:100%;margin-top:-26px;">
        
   
     <div class="panel-group" style="width:100%;">
       <div class="panel panel-primary"  style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">GateIn</h2>
          <div class="table-responsive">
              <table  class="pull-right">
                  <tr>
                      <td>
                          
         <button type="button" id="In" class="btn btn-primary" style=" font-size:18px;background-color:#0A408A;color:white;" data-toggle="modal" data-target="#MachinePopup" data-backdrop="false" >Add New</button>
                 
                              </td><td>
                
              </table>


                </div>
          </div>
         <div class="panel-body">
            
             <!--Table-->          
   <div class="table-responsive"> 
      <div class="table-responsive" style="border-color:#0A408A;border:2px;">
              <table  class="pull-right">
                  <tr>
                      <td>
                  </td>
                      <td>

                  </td></tr>
              </table>


                </div>

       <br />
      <div class="table-responsive"> 
   
       <br />
  <table class="table" id="datatable">
    
    <thead>
                    <tr>
                        <th>Date</th>
                        <th>Time</th>
                        <th>VehicalNumb</th>
                        <th>Driver</th>
                        <th>Reference</th>
                        <th>PO Numb</th>
                       <th>Status</th>
                        <th>djh</th>
                    </tr>
                </thead>
  </table>
  </div>
  </div>
</div>
  </div>
</div>
     </div>

    <!-- Modal one-->
    
    <div class="container">
        <div class="modal fade" id="MachinePopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <h2 style="color:white;">Create Items</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
      
          <div id="parent" class="form-horizontal">







                        <br />


        <div class="form-group">
            <label class="control-label col-sm-3" for="Date">Date:</label>
            <div class="col-sm-3">
     <input type="Date" class="form-control" id="Date"   required>

            </div>
                <label class="control-label col-sm-3" for="Time">Time:</label>
          <div class="col-sm-3">
                   <input type="Time" class="form-control" id="Time"   required>
          </div>
                   
                        </div>
             <div class="form-group">
            <label class="control-label col-sm-3" for="Vehicle" >Vehicle Number:</label>
            <div class="col-sm-3">
     <input type="text" class="form-control" id="Vehicle" placeholder="Vehicle Number"   required>

            </div>
                <label class="control-label col-sm-3" for="Reference">Vendor Name:</label>
          <div class="col-sm-3">
                   <input type="text" class="form-control" id="Reference"  placeholder="Vendor Name"   required>
          </div>
                   
                        </div>

             <div class="form-group">
           
              
                  <label class="control-label col-sm-3" for="Status">Driver Name:</label>
          <div class="col-sm-3">
                   <input type="text" class="form-control" id="Status" placeholder="Driver Name"  maxlength="14" onkeypress="return onlyAlphabets(event,this);" onpaste="return false"   required>
          </div>
                <label class="control-label col-sm-3" for="Po NuMBER">Purchase Order #:</label>
            <div class="col-sm-3">
     <input type="text" class="form-control" id="POnum"  onkeydown="limit(this, 7);" onpaste="return false" placeholder="Purchase Order No"  required>

            </div>
                  
                   
                        </div>
    
               
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-3">
                            <button type="button" class="btn btn-primary " onclick= "Insert(),validate1()" style="background-color:#0A408A;" id="BtnSave" >Save</button>
            
                         
                                </div>
                        </div>
                 
        
          </div>
          <div id="hide">
               <table class="table-responsive-lg">
                <tr>
                    <td class="col-sm-4"><h2 style="color:white;">Add Items</h2> </td>

             </tr>
                    </table>
   
      <div class="form-group">
          <div>
  <label class="form-control col-sm-9" id="txtgetid" hidden></label>
              

          </div>
      </div>
    <div style="">
  <table id="CallTable" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
        <th>Items_Id</th>
        <th>Quantity</th>
        <th>Add</th>
 
    
      
      </tr>
    </thead>
    <tbody>
      <tr>
<td>
    <asp:DropDownList ID="getitems" runat="server" CssClass="form-control"></asp:DropDownList>

   </td>
 
        <td>

            <input type ="number" id="getInQuantity" class="form-control"/>
        </td>
          <td><input type="button"  class="btn btn-primary" id="btnAdd" value="Add Row" onclick="validate()" style="background-color:#0A408A;color:white;"/></td>
            
          </tr>
  
    </tbody>
      
  </table>
        </div>
      <input type="button" id="savetae" class="btn btn-primary" value="Save" onclick="savetable()" style="background-color:#0A408A;color:white;"/>
                 
  

          </div>


        </div>
      </div>
    </div>
</div>
        </div>
    

                <div class="container">
        <div class="modal fade" id="viewtable" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
           
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
       
 
    
  <table id="viewsdata" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
        <th>GatePass</th>
        <th>Items_Id</th>
        <th>Quantity</th>
        <th>Add</th>
 
    
      
      </tr>
    </thead>
    <tbody>

  
    </tbody>
      
  </table>


  

        </div>
      </div>
    </div>
</div>
        </div>

<%---End  Popup----%>

       <div class="container" style="width:90%">
        <div class="modal fade" id="Popup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true" >
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <h2 style="color:white;">Gate In Items</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
        <label id="ok2" hidden></label>
      <div class="modal-body mx-3" >
    
   <div class="panel-body">
       <div class="table-responsive">
              <table  class="pull-right">
                  <tr>
                      <td>
               </td><td>
     
                  </td></tr>
              </table>


                </div>

       <br />
               <label id="ok1" class="form-control" hidden></label>
       <table id="tblEmployee2" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
       
        <th>Quantity</th>
               
          <th>Item Name</th>
         
      
      
      </tr>
    </thead>
    <tbody>
      
    </tbody>
  </table>
  </div>

           <div class="col-sm-12 text-center ">
            <asp:Button ID="Button2" runat="server" Text="Print" OnClick="Button1_Click" UseSubmitBehavior="false" CssClass="btn btn-primary" BackColor="#0A408A" ForeColor="White"/>              
                       </div>
                        </div>
                  
      

        </div>
          </div>
      </div>
    </div>
</div>

    </form>
</asp:Content>
