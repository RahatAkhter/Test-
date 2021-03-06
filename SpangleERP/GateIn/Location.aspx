﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="SpangleERP.WareHouse.Location" %>
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
    
<script type="text/javascript">
       $(document).ready(function () {  
            $.ajax({  
                url: 'Location.aspx/GetUserDetail', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                method: 'post',
                data: "{}",
                success: function (data) {  
                 
                    var employeeTable = $('#tblEmployee tbody');  
                    employeeTable.empty();  
                    for (var i = 0; i < data.d.length; i++) {

                        //employeeTable.append('<tr><td>' + data.d[i].Emp_id + '</td><td>' +data.d[i].Emp_name.toString() + '</td><td> <Input type="time"  id="Txt' + i + '"  class="form-control"/></td><td><button type="button" id="btnAdd" class="btn btn-xs btn-primary   value="' + data.d[i].Emp_id + '"  onclick="add(this.value,' + i + ');">Check In</button></td><td> <Input type="time"  id="Txts' + i + '" class="form-control" /></td ><td><button type="button"  class="btn btn-xs btn-primary value="' + data.d[i].Emp_id + '" onclick="update(this.value,' + i + ');" >Check Out</button></td> </tr > ');
                        employeeTable.append('<tr ><td class="control-label" >' + data.d[i].loc_id + '</td><td class="control_label">' + data.d[i].loc_name + '</td><td class="control_label">' + data.d[i].loc_space + '</td><td class="control_label">' + data.d[i].wh_id + '</td><td><button type="button" value="' + data.d[i].loc_id + '" data-toggle="modal" data-target="#EditPopup"  onclick="CallEditPopup(this.value,' + i + ')" class="btn btn-primary" style="background-color:#0A408A;">Edit</button></td></tr>')
                    }
                    },  
                error: function (err) {  
                    alert(err);  
                }  
            });  
    });
         $(document).ready(function () {  
         $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //url is the path of our web method (Page name/function name)
                url: "Location.aspx/PopulateDropDownList",
                data: "{}",
                dataType: "json",
                //called on jquery ajax call success
                success: function (result) {
                    $('#wh_id').empty();
                    $('#wh_id').append("<option value='0'>WH_ID </option>");
                    $.each(result.d, function (key, value) {
                        $("#wh_id").append($("<option></option>").text(value.wh_id));
                    });
                },
                //called on jquery ajax call failure
                error: function ajaxError(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
    });

    function Insert() {
        var txtname = ($('#LocationName').val());
  
        var txtWareHouse = ($('#wh_id').val());
        var txtspace = ($('#spacecov').val());
       
                 $.ajax({  
                url: 'Location.aspx/Save', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                method: 'post',
                     

              data: "{'InsLocation':'" + txtname + "','WareHouse':'" + txtWareHouse + "','spaces':'" + txtspace + "'}",
                     
                success: function (data) {  
                    alert(data.d);
                    
                    },  
                     error: function (err) {
                         alert(data.err);  
                }  
          });

    }
    
        function EditRecords() {
        
            var txtname = ($('#EditName').val());
            
            var xs = document.getElementById("Item_id");
            var dobs = xs.innerText;
            alert(dobs);
                  $.ajax({  
                url: 'Location.aspx/Edit', 
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
             function SearchRecords() {
  var input, filter, table, tr, td, i, txtValue;
  input = document.getElementById("search");
  filter = input.value.toUpperCase();
  table = document.getElementById("tblEmployee");
  tr = table.getElementsByTagName("tr");
  for (i = 0; i < tr.length; i++) {
    td = tr[i].getElementsByTagName("td")[1];
    if (td) {
      txtValue = td.textContent || td.innerText;
      if (txtValue.toUpperCase().indexOf(filter) > -1) {
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

        //end//
         function CallEditPopup(Val,i) {
         var ItemsName = document.getElementById("EditName");
         var getid = ItemsName.innerText;
         var Items_Id = document.getElementById("Item_id");
             var getname = ItemsName.innerText;
     
                     alert(Val);
                  $.ajax({  
                url: 'Location.aspx/GotoPopup', 
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

    </script>
    
  <style type="text/css">
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <div class="container" style="width:100%;margin-top:-26px;">
     <div class="panel-group" style="width:100%;">
       <div class="panel panel-primary" style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">All Location </h2>
          <div class="table-responsive"  style="border-color:#0A408A;border:2px;">
              <table  class="pull-right">
                  <tr>
                      <td>
         <button type="button" class="btn btn-primary" style=" font-size:18px;color:white;background-color:#0A408A;" data-toggle="modal" data-target="#MachinePopup" data-backdrop="false" >New Location</button>
                  </td><td>

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
<input type="text" placeholder="Search By Location" id="search" style="border-radius:6px;font-family:Cambria;font:bold;height:30px;" onkeyup="SearchRecords()" onkeypress="return onlyAlphabets(event,this);" onpaste="return false"/>
                  </td>

                 </tr>
              </table>


                </div>

       <br />
       <table id="tblEmployee" class="table table-responsive-lg" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
        <th>Location_ID</th>
        <th>Location_Name</th>
          <th>Space</th>
         <th>Wh_id</th>
        <th>Edit</th>
   
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

    <!-- Modal one-->
    <div class="container">
        <div class="modal fade" id="MachinePopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <h2 style="color:white;">Add New Location</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
       <%--  <div class="container">
     <div class="panel-group">
       <div class="panel panel-primary">
      <div class="panel-heading"><h2>Add Employee</h2></div>
      <div class="panel-body">--%>

  
     
     <br />
    
 <div class="form-horizontal">
       <div class="form-group">
            <label class="control-label col-sm-3" for="LocationName" >Location :</label>
            <div class="col-sm-3">
            <input type="text" class="form-control" id="LocationName" placeholder=" Location Name"  maxlength="14" onkeypress="return onlyAlphabets(event,this);" onpaste="return false"  required  >
           </div>
      
            <label class="control-label col-sm-2" for="WH_ID">WareHouse Id:</label>
                  <div class="col-sm-3">
                <select class="form-control" id="wh_id" >
     
 </select>
          
 </div>
           </div>
              <div class="form-group">
                     <label class="control-label col-sm-3" for="WH_ID">Space:</label>
                  <div class="col-sm-3">
                      <input type="number" id="spacecov" class="form-control"  min="0"  onkeypress="return AllowOnlyNumbers(event);"  onkeydown="limit(this, 5);" onpaste="return false" />
                  </div>
              </div>



         
           <br />
    
     
        <div class="form-group">


                     <div class="col-sm-3 col-sm-offset-3">
            <input type="Button" class="btn btn-primary" value="Save" id="Btn_AddEmploee"  onclick="Insert()" style="background-color:#0A408A;">

            </div>
        </div>

        </div>



        </div>
      </div>
    </div>
</div>
        </div>

            <!--end model one-->
                      <!-- Modal two-->
    <div class="container">
        <div class="modal fade" id="EditPopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <h2 style="color:white;">Edit Location</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
    


                        <br />

          <div class="form-horizontal">
        <div class="form-group">
              <label class="control-label col-sm-3" for="ItemsName">Location_id</label>
            <div class="col-sm-3">
                <label class="control-label col-sm-3" for="ItemsName" id="Item_id"></label>
            </div>
            </div>
      <div class="form-group">
            <label class="control-label col-sm-3" for="ItemsName">Location Name:</label>
            <div class="col-sm-3">
     <input type="text" class="form-control" id="EditName" placeholder="Items Name" maxlength="14" onkeypress="return onlyAlphabets(event,this);" onpaste="return false"   required>

            </div>
          </div>
                         
                      
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-3">
                            <button type="button" class="btn btn-primary" onclick="EditRecords()" style="background-color:#0A408A;">Saved</button>
            
                         
                                </div>
                        </div>
     </div>
        
  


        </div>
      </div>
    </div>
</div>
        </div>

<%---End  Popup----%>       
            
     

           

</asp:Content>
