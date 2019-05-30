<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="SpangleERP.invent.Items" %>
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
                url: 'Items.aspx/GetUserDetail', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                method: 'post',
                data: "{}",
                success: function (data) {  
                  
                    var employeeTable = $('#tblEmployee tbody');  
                    employeeTable.empty();  
                    for (var i = 0; i < data.d.length; i++) {

                        //employeeTable.append('<tr><td>' + data.d[i].Emp_id + '</td><td>' +data.d[i].Emp_name.toString() + '</td><td> <Input type="time"  id="Txt' + i + '"  class="form-control"/></td><td><button type="button" id="btnAdd" class="btn btn-xs btn-primary   value="' + data.d[i].Emp_id + '"  onclick="add(this.value,' + i + ');">Check In</button></td><td> <Input type="time"  id="Txts' + i + '" class="form-control" /></td ><td><button type="button"  class="btn btn-xs btn-primary value="' + data.d[i].Emp_id + '" onclick="update(this.value,' + i + ');" >Check Out</button></td> </tr > ');
                        employeeTable.append('<tr ><td class="control-label" >' + data.d[i].items_id + '</td><td class="control_label">' + data.d[i].items_name + '</td><td><button type="button" value="' + data.d[i].items_id + '" data-toggle="modal" data-target="#EditPopup"  onclick="CallEditPopup(this.value,' + i + ')" class="btn btn-primary" style="background-color:#0A408A;">Edit</button></td></tr>')
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
                url: "Items.aspx/PopulateDropDownList",
                data: "{}",
                dataType: "json",
                //called on jquery ajax call success
             success: function (result) {


                var ddl = document.getElementById("<%=cat_id.ClientID%>");
                   var siteems = ddl.options[ddl.selectedIndex].value;
                   // $('#cat_id').empty();
                   // $('#cat_id').append("<option value='0'>Cat_ID </option>");
                  //  $.each(result.d, function (key, value) {
                      //  $("#cat_id").append($("<option></option>").text(value.cat_id));
                   // });
                },
                //called on jquery ajax call failure
                error: function ajaxError(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
                     });

     function Insert() {
      
         var txtname = ($('#ItemsName').val());
         //alert(txtname);
         
                var ddl = document.getElementById("<%=cat_id.ClientID%>");
                   var txtHead = ddl.options[ddl.selectedIndex].value;
         //var txtHead = ($('#cat_id').val());
       //  alert(txtHead)
   
      
                  $.ajax({  
                url: 'Items.aspx/Save', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                      method: 'post',
              data: "{'items_name':'" + txtname + "','cat_id':'" + txtHead + "'}",
                  
                     success: function (data) {
                      
                    alert(data.d);
                         $('#ItemsName').val("");
                    
                    
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
            alert(dobs);
                  $.ajax({  
                url: 'Items.aspx/Edit', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                      method: 'post',
                    data: "{'id':'" + dobs + "','txtname':'" + txtname + "'}",
                           
                      success: function (data) { 
                     
                          alert(data.d);
                           window.location = "Items.aspx";
                
                      },  
           
                error: function (err) {  
                    alert(err);  
                }  
            });  
         

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

      
        //end//
     
     </script>
    
  <style type="text/css">
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <form runat="server">     <div class="container" style="width:100%;margin-top:-26px;">
     <div class="panel-group" style="width:100%;">
       <div class="panel panel-primary"  style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">Items Details</h2>
          <div class="table-responsive"  style="border-color:#0A408A;border:2px;">
              <table  class="pull-right">
                  <tr>
                      <td>
         <button type="button" class="btn btn-primary" style=" font-size:18px;background-color:#0A408A;color:white;" data-toggle="modal" data-target="#MachinePopup" data-backdrop="false" >Create New Items</button>
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
                  </td><td>
   
                  </td></tr>
              </table>


                </div>

       <br />
       <table id="tblEmployee" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
        <th>Items_Id</th>
        <th>Items_Name</th>
        
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
            <h2 style="color:white;">Create Items</h2>
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
            <label class="control-label col-sm-2" for="ItemsName">Items Name:</label>
            <div class="col-sm-3">
     <input type="text" class="form-control" id="ItemsName" placeholder="Items Name" maxlength="30"   required>

            </div>
                <label class="control-label col-sm-3" for="Cat_Id">Cat_id:</label>
          <div class="col-sm-3">
                <asp:DropDownList ID="cat_id" runat="server" CssClass="form-control"></asp:DropDownList>

          </div>
                   
                        </div>
      <br />
      
                <br />
    
                      
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-3">
                            <button type="button" class="btn btn-primary" onclick= "Insert()" style="background-color:#0A408A;">Save</button>
            
                         
                                </div>
                        </div>

</div>
                    
        
      <%--</div>
    </div>
   
 
      </div>
      </div>
    </div>--%>


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
  <div class="modal-dialog modal-md" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <h2 style="color:white;">Edit Items</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
     


                        <br />

          <div class="form-horizontal">
        <div class="form-group" style="display:none;">
              <label class="control-label col-sm-2" for="ItemsName">Item_id</label>
            <div class="col-sm-3">
                <label class="control-label col-sm-2" for="ItemsName" id="Item_id"></label>
            </div>
            </div>
      <div class="form-group">
            <label class="control-label col-sm-4" for="ItemsName">Items Name:</label>
            <div class="col-sm-8">
     <input type="text" class="form-control" id="EditName" placeholder="Items Name" required maxlength="30" onkeypress="return onlyAlphabets(event,this);"  required>

            </div>
          </div>
                         
                      
                        <div class="form-group">
                            <div class="col-xs-12 text-center">
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
             
   </form>

</asp:Content>
