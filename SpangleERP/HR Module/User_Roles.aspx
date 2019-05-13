<%@ Page Title="" Language="C#" MasterPageFile="~/HR Module/HR_Master.Master" AutoEventWireup="true" CodeBehind="User_Roles.aspx.cs" Inherits="SpangleERP.HR_Module.User_Roles" %>
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
    <title>Spangle</title>
 <script type="text/javascript">
     

     $(document).ready(function () {
         myFunctions()
     });
 
      

  

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
        //end//
     // insert data by loop//
     $(function () {
         $("#savetable").click(function () {
        
                 var n = $("#CallTable").find("tr").length;
                 for (var i = 1; i < n - 1; i++) {
                     var Id = $("#txtgetid").text();
                     var ItemsNames = $("#CallTable").find("tr").eq(i + 1).find("td").eq(0).text();
                     var Quantities = $("#CallTable").find("tr").eq(i + 1).find("td").eq(1).text();
   
           

                 }

             })
        
     })
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

                     //employeeTable.append('<tr><td>' + data.d[i].Emp_id + '</td><td>' +data.d[i].Emp_name.toString() + '</td><td> <Input type="time"  id="Txt' + i + '"  class="form-control"/></td><td><button type="button" id="btnAdd" class="btn btn-xs btn-primary   value="' + data.d[i].Emp_id + '"  onclick="add(this.value,' + i + ');">Check In</button></td><td> <Input type="time"  id="Txts' + i + '" class="form-control" /></td ><td><button type="button"  class="btn btn-xs btn-primary value="' + data.d[i].Emp_id + '" onclick="update(this.value,' + i + ');" >Check Out</button></td> </tr > ');
                     employeeTable.append('<tr ><td class="control-label" style="Text-align:left;" >' + data.d[i].ItemIn_Id + '</td><td class="control_label">' + data.d[i].ItemsId  + '</td><td class="control_label">' + data.d[i].I_Quantity + '</td><td class="control_label">' + data.d[i].GateIn_Id + '</td></tr>')
                 }
             },
             error: function (err) {
                 alert(err);
             }
         });
     }
     function AddRow() {
            
             var markup = "<tr><td>jghjgh </td><td>sddd</td><td> <button type='button' class=' btn btn-danger' onclick='SomeDeleteRowFunction(this);'>Remove</button></td></tr>";
             $("#CallTable tbody").append(markup);
        }
    
  
       function SomeDeleteRowFunction(row) {
       
        var d = row.parentNode.parentNode.rowIndex;
           document.getElementById('CallTable').deleteRow(d);


     }
     

function myFunctions() {
var x = document.getElementById("myUniqueBar");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);
     }

     function SetPopup() {
var x = document.getElementById("setdate");
x.classList.add("show");
setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);
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
   <div class="table-responsive"> 
      <div class="table-responsive">
              <table  class="pull-right">
                  <tr>
                      <td>
<input type="text" placeholder="Search Here By Id Or Date" id="search" class="form-control fas fa-search" style="border-radius:6px;font-family:Cambria;font:bold;height:30px;" onkeyup="SearchRecords()" onfocus="myFunctions()" />
                  </td>
                      <td>
     <select id="maxRows" name="state" class="form-control" style="width:120px;height:31px;">
         <option value="1000">Show All</option>
         <option value="10">10</option>
         <option value="20">25</option>
         <option value="30">50</option>
         <option value="100">100</option>
     </select>
                  </td></tr>
              </table>

         
                </div>

       <br />
       <div class="table-wrapper-scroll-y my-custom-scrollbar">
       <table id="tblEmployee" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
        <th>GateIn_Id</th>
        <th>Date</th>
        <th>Time</th>
        <th>Rec_By</th>
        <th>Chk_By</th>
      
        <th>Status</th>
            <th>PO_NO</th>
            <th>Vehicle No</th>
    
      
      </tr>
    </thead>
    <tbody>
      
    </tbody>
  </table>
 </div>
      <%--scroll bar--%>
           </div>
</div>
  </div>
</div>
     </div>

    <!-- Modal one-->
    <div class="container">
        <div class="modal fade" id="MachinePopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-md" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <h2 style="color:white;">Create Items</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
     

  <form class="form-horizontal" method="post">

                        <br />


     
    
                       <div class="form-group">
           
               
          <div class="col-sm-12">
                   <input type="text" class="form-control" id="Status"  maxlength="30" placeholder="Enter Role Name" required>
          </div>
                   
                        </div>    
                        <div class="form-group">
                            <div class="col-sm-12  text-center">
                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#EditPopup" style="background-color:#0A408A; border-color:#0A408A; box-sizing:border-box;" id="BtnSave">Save</button>
            
                         
                                </div>
                        </div>
                    </form>
        
    
        </div>
      </div>
    </div>
</div>
        </div>
       <%--        End popup--%>

                   <!-- Modal two-->
    <div class="container">
        <div class="modal fade" id="EditPopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg"  role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <table class="table-responsive-lg">
                <tr>
                    <td class="col-sm-4"><h2 style="color:white;">Role Content</h2> </td>

             </tr>
                    </table>
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
     
  <form class="form-horizontal" method="post">

                        <br />
      <div class="form-group">
          <div>
  
              

          </div>
      </div>
    
  <table id="CallTable" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
        <th>URl</th>
        <th>Page Name</th>
        <th>Icon_Name</th>
       
  
 
    
      
      </tr>
              
    </thead>
    <tbody>
      <tr>
<td style="width:25%;"><input type ="text" id="URl" class="form-control" min="4"  placeholder="URL" /></td>
 
        <td  style="width:25%;">
            <input type ="text" id="Page_Name" class="form-control" min="4"  placeholder="PName" />
        </td>
          <td  style="width:25%;">
            <input type ="text" id="Icon_Name" class="form-control" min="4"  placeholder="PName" />
        </td>
          </tr>
               <tr >
                   <td  style="width:20%;">Rights</td>
                   <td style="width:20%;">
                       
         <label class="checkbox-inline"><input type="checkbox" value="">Create</label></td>

<td style="width:20%;"> <label class="checkbox-inline"><input type="checkbox" value="">Edit</label></td>
<td style="width:20%;"><label class="checkbox-inline"><input type="checkbox" value="">Delete</label></td>
             <td style="width:20%;">  <label class="checkbox-inline"><input type="checkbox" value="">View</label>
               </td>
                   </tr>
        
      
           <tr>
               <td style=" margin-left:200px;" >
        <input type="button"  class="btn btn-primary " id="btnAdd"  value="Add Row" onclick="AddRow()"/>  </td>
           </tr>
              
            
    </tbody>
      
  </table>
      <div class=" col-sm-12 text-center">
      <input type="button" id="savetable" class="btn btn-primary  text-center" value="Save" />
          </div>          
          </form>

     
        </div>
      </div>
    </div>
</div>
        </div>

<%---End  Popup----%>


             
<%---End  Popup----%>


    <div id="myUniqueBar" class="snackbar">Search By Id Or Date...!</div>
    <div id="setdate" class="snackbar">Select Date...!</div>
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

