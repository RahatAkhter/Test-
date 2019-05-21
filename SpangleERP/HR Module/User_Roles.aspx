<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="User_Roles.aspx.cs" Inherits="SpangleERP.HR_Module.User_Roles" %>
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
    <title>Spangle</title>
 <script type="text/javascript">
     var $j = jQuery.noConflict();

     $j(document).ready(function () {
         $j('#child').hide();
         Show_Data();
         
     });


  function Show_Data() {
                 $j('#datatable').DataTable({
            "aLengthMenu": [[10, 25,5], [10, 25, 5]],
                "iDisplayLength": 5,
                columns: [
           
                   { 'data': 'Role_Name' },
                   
                  {
                                'data': 'Role_Id',
                                'sortable': false,
                                'searchable': false,
                      'render': function (val) {

                          return ' <button type="button" class=" btn btn-primary" value="' + val + '"  onclick="View(this.value);" style=" background-color: #0A408A;" >View</button>';
                      }
                    }
                   
                            
                                        
                    ],
                bServerSide: true,
                sAjaxSource: 'Employees.asmx/GetAll_Roles',
                sServerMethod: 'post'
            });
        }

     function View(Val) {

         var $tbl = $('#datatable1');
            $.ajax({
             url: 'User_Roles.aspx/GetRolesContent',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             method: 'post',
             data: "{'RID':'" + Val + "'}",
             success: function (data) {
                
                  $tbl.empty();
         $tbl.append(' <tr><th>Page Name</th><th>Rights</th></tr>');

                 for (var i = 0; i < data.d.length; i++) {
                    
 $tbl.append('<tr ><td >' + data.d[i].Page_name + '</td><td >' + data.d[i].Rights  + '</td></tr>')
                 }
                 $j('#MyModal').modal('show');
             },
             error: function (err) {
                 alert(err);
             }
         });


     }
  

     function Insert() {
          var xs = document.getElementById("Date");
         var txtdates = xs.value;
      
         var txttime = ($('#Time').val());
         var txtvehicle = ($('#Vehicle').val());
         var txtreferences = ($('#Reference').val());
         var txtRec_By = ($('#Rec_By').val());
         var txtChk_By = ($('#Chk_By').val());
         var txtPO = ($('#POnum').val());
         var txtStatus = ($('#Status').val());
         
     }

     function CallEditPopup(Val,i) {
         var ItemsName = document.getElementById("EditName");
         var getid = ItemsName.innerText;
         var Items_Id = document.getElementById("Item_id");
             var getname = ItemsName.innerText;
     
                     alert(Val);
                


     }

        function EditRecords() {
        
            var txtname = ($('#EditName').val());
            
            var xs = document.getElementById("Item_id");
            var dobs = xs.innerText;
            
   

     }

    function SearchRecords() {
      var input, filter, table, tr, td, i;
      input = document.getElementById("search");
      filter = input.value.toUpperCase();
      table = document.getElementById("tblEmployee");
      tr = table.getElementsByTagName("tr");
      for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        td1 = tr[i].getElementsByTagName("td")[1];
        if (td+td1) {
          if ((td.innerHTML.toUpperCase().indexOf(filter)+td1.innerHTML.toUpperCase().indexOf(filter)) > -2) {
            tr[i].style.display = "";
          } else {
            tr[i].style.display = "none";
          }
        }       
      }
     }

      function SearchRecordItems() {
      var input, filter, table, tr, td, i;
      input = document.getElementById("search1");
      filter = input.value.toUpperCase();
      table = document.getElementById("viewsdata");
      tr = table.getElementsByTagName("tr");
      for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
          td1 = tr[i].getElementsByTagName("td")[1];
            td2 = tr[i].getElementsByTagName("td")[3];
        if (td+td1) {
          if ((td.innerHTML.toUpperCase().indexOf(filter)+td1.innerHTML.toUpperCase().indexOf(filter)+td2.innerHTML.toUpperCase().indexOf(filter)) > -3) {
            tr[i].style.display = "";
          } else {
            tr[i].style.display = "none";
          }
        }       
      }
    }
       


     function Save() {

         var n = $("#CallTable1").find("tr").length;
       
         if (n == 1) {
             alert("Please Select Some Data");
         }
         else {
             for (var i = 0; i < n - 1; i++) {
                 //var Id = $("#txtgetid").text();

                 var Page_Id = $("#CallTable1").find("tr").eq(i + 1).find("td").eq(1).text();

                 // var Icon_Name = $("#CallTable1").find("tr").eq(i + 1).find("td").eq(2).text();
                 var Level = $("#CallTable1").find("tr").eq(i + 1).find("td").eq(2).text();

                 $.ajax({
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     url: 'User_Roles.aspx/InsertChild',
                     data: "{'page_id':'" + Page_Id + "','level':'" + Level + "'}",

                     dataType: "json",
                     async: false,

                     success: function (data) {
                         alert(data.d);
                     },
                     Error: function (res) {
                         alert("Error Occure" + res);

                     }
                 });

             }
             $j('#child').hide();
             $j('#parent').show();
             
             window.location = "user_Roles.aspx";

         }



     }
            
     function ViewRecords() {
         $.ajax({
             url: 'GateIn.aspx/GateItems',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             method: 'post',
             data: "{}",
             success: function (data) {
                 alert("agay");
                 var employeeTable = $('#viewsdata tbody');
                 employeeTable.empty();
                 for (var i = 0; i < data.d.length; i++) {
 employeeTable.append('<tr ><td class="control-label" style="Text-align:left;" >' + data.d[i].ItemIn_Id + '</td><td class="control_label">' + data.d[i].ItemsId  + '</td><td class="control_label">' + data.d[i].I_Quantity + '</td><td class="control_label">' + data.d[i].GateIn_Id + '</td></tr>')
                 }
             },
             error: function (err) {
                 alert(err);
             }
         });
     }
     function AddRow() {

         
         var ddl = document.getElementById("<%=DropDownList1.ClientID%>");
         var Page_Name = ddl.options[ddl.selectedIndex].text;
         var ddl1 = document.getElementById("<%=DropDownList1.ClientID%>");
         var Page_id = ddl.options[ddl1.selectedIndex].value;
       
            var I = document.getElementById("create").checked;
         var U = document.getElementById("Update").checked;
         var D = document.getElementById("delete").checked;
             var V = document.getElementById("View").checked;
         var level= Access();

         $("#CallTable1 tbody").append('<tr><td>' + Page_Name + '</td><td style="display:none;">' + Page_id + '</td><td>' + level + '</td><td> <button type="button" class=" btn btn-danger" onclick="SomeDeleteRowFunction(this);">Remove</button></td></tr>');
        
     }
    


     function SaveParent() {

         var name = $('#name').val();
         var name1 = $('#fname').val();

         if (name != "" && name1!="") {

             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: 'User_Roles.aspx/Insert_Parent',
                 data: "{'Name':'" + name + "','folder_name':'" + name1 + "'}",

                 dataType: "json",
                 async: false,

                 success: function (data) {
                     if (data.d == "Save") {
                         $('#parent').hide();
                         $('#child').show();
                     }
                     else {
                         alert(data.d);
                     }
                 },
                 Error: function (res) {
                     alert("Error Occure" + res);

                 }
             });



         }
         else {
             alert("Please Enter ROle Name");
         }
        
     }


     function Access() {
         var Level = "";
            var I = document.getElementById("create").checked;
         var U = document.getElementById("Update").checked;
         var D = document.getElementById("delete").checked;
         var V = document.getElementById("View").checked;
         //alert(U+" "+V+""+I+""+D);
         var level;
         if (I == true ) {
             Level +="I";
         }
         else {
             level += "";
         }

        
         if (D == true ) {
             Level +="D";
         }
         else {
             level += "";
         }

         if (V == true ) {
             Level +="V";
         }
         else {
             level += "";
         }


         if (U == true) {
             Level +="U";
         }
         else {
             level += "";
         }

        
        // alert(Level);
         
         return Level;
     }

       function SomeDeleteRowFunction(row) {
       
        var d = row.parentNode.parentNode.rowIndex;
           document.getElementById('CallTable1').deleteRow(d);


     }
     

function myFunctions() {
var x = document.getElementById("myUniqueBar");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 5000);
     }

     function SetPopup() {
var x = document.getElementById("setdate");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 5000);
     }
                    function TimeValidation() {
var x = document.getElementById("settime");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 5000);
     }
   function FieldValidation() {
var x = document.getElementById("TextFields");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 5000);
     }

     function SelectItemPopup() {
var x = document.getElementById("SelectItems");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);
     }

          function InsertValidation() {
var x = document.getElementById("GateInInsert");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 5000);
     }
               function ErrorValidation() {
var x = document.getElementById("ErrorVal");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 5000);
     }
                   function AlphabetValidation() {
var x = document.getElementById("Alph");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 5000);
     }
                      function NumbersValidation() {
var x = document.getElementById("Numb");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 5000);
     }                 function BothsValidation() {
var x = document.getElementById("Varc");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 5000);
     }
       function QuantityVal() {
     var x = document.getElementById("Quantitr");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 5000);
     }

     </script>
    
  <style type="text/css">
      .my-custom-scrollbar {
position: relative;
height: 300px;
overflow:auto;
}
.table-wrapper-scroll-y {
display: block;
}

.snackbar {
        visibility: hidden;
        min-width: 250px;
        margin-left: -125px;
        background-color:#0A408A;
        color: #fff;
        text-align: center;
        border-radius: 2px;
        padding: 16px;
        position: fixed;
        z-index: 1;
        left: 50%;
        bottom: 30px;
        font-size: 17px;
        border-radius:20px;
        border-radius:20px;
    }

    .snackbar.show {
        visibility: visible;
        -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
        animation: fadein 0.5s, fadeout 0.5s 2.5s;
    }

    @-webkit-keyframes fadein {
        from {bottom: 0; opacity: 0;} 
        to {bottom: 30px; opacity: 1;}
    }

    @keyframes fadein {
        from {bottom: 0; opacity: 0;}
        to {bottom: 30px; opacity: 1;}
    }

    @-webkit-keyframes fadeout {
        from {bottom: 30px; opacity: 1;}
        to {bottom: 0; opacity: 0;}
    }

    @keyframes fadeout {
        from {bottom: 30px; opacity: 1;}
        to {bottom: 0; opacity: 0;}
    }  


  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
     <div class="container" style="width:100%;margin-top:-26px;">
     <div class="panel-group" style="width:100%;">
       <div class="panel panel-primary"  style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">GateIn</h2>
          <div class="table-responsive table-striped">
              <table  class="pull-right">
                  <tr>
                      <td>
         <button type="button" class="btn btn-primary" style=" font-size:18px;background-color:white;color:#0A408A;" data-toggle="modal" data-target="#MachinePopup" data-backdrop="false" >Add New</button>
                  </td></tr>
              </table>


                </div>
          </div>
         <div class="panel-body">
            
             <!--Table-->          
   <table id="datatable" style=" width:100%; height:300px;">
       <thead>
                    <tr>
                       
                        <th>Role Name</th>
                        <th>View</th>
                        
                        
                    </tr>
                </thead>
    </table>
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
            <h2 style="color:white;">Create Role</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
     
          <div id="parent">
  <form class="form-horizontal" method="post">

                        <br />


     
    
                       <div class="form-group">
           
               
          <div class="col-sm-4 col-sm-offset-4" >
                   <input type="text" class="form-control" id="name"  maxlength="30" placeholder="Enter Role Name" required>
          </div>
                   
                        </div>    
         <div class="form-group">
           
               
          <div class="col-sm-4 col-sm-offset-4" >
                   <input type="text" class="form-control" id="fname"  maxlength="30" placeholder="Enter Folder Name" required>
          </div>
                   
                        </div>  
                        <div class="form-group">
                            <div class="col-sm-12  text-center">
                            <button type="button" class="btn btn-primary"  style="background-color:#0A408A; border-color:#0A408A; box-sizing:border-box;" id="BtnSave" onclick="SaveParent()">Save</button>
            
                         
                                </div>
                        </div>
                    </form>
        </div>
    <div id="child">
        <form runat="server">
        <table id="CallTable" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
        <th>Page</th>
       <th>Rights</th>
  
 
    
      
      </tr>
              
    </thead>
    <tbody>
      <tr>
 
        <td  style="width:20%;">
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
        </td>
         <td style="width:20%;">
                       
         <label class="checkbox-inline"><input type="checkbox" value="" id="create">Create</label></td>

<td style="width:20%;"> <label class="checkbox-inline"><input type="checkbox" value="" id="Update">Edit</label></td>
<td style="width:20%;"><label class="checkbox-inline"><input type="checkbox" id="delete" value="">Delete</label></td>
             <td style="width:20%;">  <label class="checkbox-inline"><input type="checkbox" id="View" value="">View</label>
               </td>
          </tr>
              
        
      
           <tr>
               <td style=" margin-left:200px;" >
        <input type="button"  class="btn btn-primary " id="btnAdd"  value="Add Row" onclick="AddRow()"/>  </td>
           </tr>
              
            
    </tbody>
      
  </table>

       <table id="CallTable1" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
        <th>Page Name</th>
        <th>Access Level</th>
  
 
    
      
      </tr>
              
    </thead>
   <tbody></tbody>
      
  </table>
      <div class=" col-sm-12 text-center">
      <input type="button" id="savetable1" class="btn btn-primary  text-center" value="Save" onclick="Save();" />
          </div>   


    </div>
          </form>
        </div>
      </div>
    </div>
</div>
        </div>
       <%--        End popup--%>

                   
    <!--View MOdal-->
    <div class="container">
        <div class="modal fade" id="MyModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <h2 style="color:white;">Role Detail</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
     

  <table id="datatable1" style=" width:100%; height:300px; border-color:#0A408A" border="1" >
      
        
    </table>
        </div>
      </div>
    </div>
</div>
        </div>
        


    <div id="myUniqueBar" class="snackbar">...!</div>
    <div id="setdate" class="snackbar">Your Data Is Saved</div>
     <div id="Alph" class="snackbar">Only Alphabets Are Allowed In This Field...!</div>
    <div id="Numb" class="snackbar">Only Positive Numberes Are Allowed In This Field...!</div>
    <div id="Varc" class="snackbar">Alphabets And Number Both Are Allowed In This Fields...!</div>
     <div id="settime" class="snackbar">Select Time...!</div>
     <div id="TextFields" class="snackbar">All Fields Required...!</div>
    <div id="SelectItems" class="snackbar">Select Items No And Quantity 0 Not Be Right Value...!</div>
    <div id="GateInInsert" class="snackbar">Save ItemsSuccessfully..!</div>
    <div id="ErrorVal" class="snackbar">Some Thing Wrong Not Be Inserted..!</div>
     <div id="Quantitr" class="snackbar">Quantity Greater Than 0..!</div>


</asp:Content>

