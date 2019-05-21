<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="All_Users.aspx.cs" Inherits="SpangleERP.HR_Module.All_Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
     <script src="../jquery-ui.js"></script>
    <link href="../jquery-ui.css" rel="stylesheet" />
    
<link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" rel="stylesheet" />
<script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
      <style>
        * {box-sizing: border-box;}

/* Style the input container */
.input-container {
  display: flex;
  width: 100%;
  margin-bottom: 15px;
}

/* Style the form icons */
.icon {
  padding: 10px;
  background: #0A408A;
  color: white;
  min-width: 80px;
  text-align: center;
}


.input-field {
  width: 100%;
  padding: 10px;
  outline: none;
}

.input-field:focus {
  border: 2px solid #0A408A;
}



.imageclass
{
    width:50px;
    height:50px;
    
    }
.btn:hover {
  opacity: 1;
  background-color:#0A408A;
  color:white;
}
  
    </style>

    <script type="text/javascript">
        var id = "";
        var $j = jQuery.noConflict();

        $j(document).ready(function () {
           
             $j('#txtseach').autocomplete({
            minLength: 1,
            focus: function (event, ui) {
                $(this).val(ui.item.emp_name);

                return false;
            },
            select: function (event, ui) {
                $j(this).val(ui.item.emp_name);
                $j('#id').val(ui.item.emp_id.toString());
                id = $j('#id').val();
                
                return false;

            },
            source: function (request, response) {
                $j.ajax({
                    type: 'POST',
                    url: 'Employees.asmx/Get_Profiles',
                    data: { term: request.term },
                    dataType: "json",
                    success: function (data) {
                        response(data);
                    }

                });

            }

        })
        .autocomplete('instance')._renderItem = function (ul, item) {
            if (item.Img == "") {
                return $j('<li>')
                    .append('<img src="http://ssl.gstatic.com/accounts/ui/avatar_2x.png" class="img-responsive imageclass img-circle" alt="' + item.emp_name + '"/>').append('<label style="display:none;">' + item.emp_id + '</label>')
                    .append('<a>' + item.emp_name + '</a> </li> ')
        .appendTo(ul);

            }
            else {
                return $j('<li>')
                    .append('<img src=' + item.Img + ' class="img-responsive imageclass img-circle" alt="' + item.emp_name + '"/>')
                    .append('<a>' + item.emp_name + '   </a> </li>')
        .appendTo(ul);


            }
        }


            Show_Data();
        });
        
        function Show_Data() {
                 $j('#datatable').DataTable({
            "aLengthMenu": [[10, 25,5], [10, 25, 5]],
                "iDisplayLength": 5,
                columns: [
           
                   { 'data': 'emp_name' },
                    { 'data': 'Role' },
                    { 'data': 'Desig' },
                    { 'data': 'dep' },
                    {
                        'data': 'Img',

                        'sortable': false,
                                'searchable': false,
                        'render': function (webSite) {
                            return '<img src="' + webSite + '" class=" img img-responsive  img-rounded" style="width:80; height:80px;" />';
                                    
                                }



                    },
                  {
                                'data': 'User_id',
                                'sortable': false,
                                'searchable': false,
                      'render': function (val) {

                          return ' <button type="button" class=" btn btn-primary" value="' + val + '" data-toggle="modal" data-target="#AddEmployeePopup" onclick="Edit(this.value);" style=" background-color: #0A408A;" >Edit</button>';
                      }
                    }
                   
                            
                                        
                    ],
                bServerSide: true,
                sAjaxSource: 'Employees.asmx/GetAll_Users',
                sServerMethod: 'post'
            });
        }
        var uid="";
        function Edit(Val) {
           
            $j("#SalaryPackage").modal("show");
            uid = Val.toString();
        }
      

        function Insert() {
           if (id != "" && email != "" && Role != "" && Password != "") {
          
        

            var email = $('#email').val();
             var Password = $('#pass').val();
             var ddl6 = document.getElementById("<%=DropDownList1.ClientID%>");
            var Role = ddl6.options[ddl6.selectedIndex].value;
           
               alert(id);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'All_Users.aspx/Insert',

                    data: "{'emp_id':'" + id + "','email':'" + email + "','password':'" + Password + "','Role':'" + Role + "'}",


                    dataType: "json",
                    async: false,

                    success: function (data) {
                        $('#id').val("");
                        $('#txtseach').val("");
                        if (data.d == "Save") {
                            alert("Insert Successfully " + data.d);
                           

                        }
                        else if (data.d = "Not") {
                            alert("This Employee alreay In Users"+data.d);

                        }
                        else {
                            alert("Something Went Wrong"+data.d);
                        }
                    },
                    Error: function (res) {

                        alert("Error Occure" + res);

                    }
                });
            }
            else {
                alert("Please Fill The Form Correctly");
            }

        }

        function AvoidSpace(event) {
    var k = event ? event.which : window.event.keyCode;
    if (k == 32) return false;
}

       <%-- function AddRow() {
            var quantity = $("#getInQuantity").val();
                var ddl = document.getElementById("<%=getitems.ClientID%>");
             var siteems = ddl.options[ddl.selectedIndex].value;
          
             var markup = "<tr><td>" + siteems + "</td><td>" + quantity + "</td></tr>";
             $("#CallTable tbody").append(markup);
        }


        function InsertData() {

            var n = $("#CallTable").find("tr").length;
            for (var i = 1; i < n - 1; i++) {
                var Id = $("#txtgetid").text();
                var ItemsNames = $("#CallTable").find("tr").eq(i + 1).find("td").eq(0).text();
                var Quantities = $("#CallTable").find("tr").eq(i + 1).find("td").eq(1).text();
                alert(Id);
                alert(ItemsNames);
                alert(Quantities);

                

            }
        }--%>
        

        function Update() {
            alert("agaya"+uid);
            var pass = $('#ipass').val();
              var ddl6 = document.getElementById("<%=DropDownList2.ClientID%>");
            var Role = ddl6.options[ddl6.selectedIndex].value;

            if (pass != "" && uid != "" ) {
               $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'All_Users.aspx/Update',

                   data: "{'uid':'" + uid + "','pass':'" + pass + "','Role':'" + Role + "'}",


                    dataType: "json",
                    async: false,

                    success: function (data) {
                        if (data.d == "Save") {
                            uid = "";
                            alert("Insert Successfully ");
                          
                             
                        }
                        
                        else {
                            alert("Something Went Wrong");
                        }
                    },
                    Error: function (res) {

                        alert("Error Occure" + res);

                    }
                });
               
            }
            else {
                alert("Not");

            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
     <div class="container" style="width:99%;margin-top:-26px;">
     <div class="panel-group" style="width:99%;">
       <div class="panel panel-primary" style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;text-align:center;">Create User</h2>

          </div>
         <div class="panel-body">
             
<div class="input-container">
    <i class="fa fa-user icon"></i>
    <input class="input-field" id="txtseach" type="text" placeholder="Username" name="usrnm">
    <input type="text" id="id" name="postId" style=" display:none;" />
  </div>

  <div class="input-container">
    <i class="fa fa-envelope icon"></i>
    <input class="input-field" type="text" placeholder="Email" name="email" id="email" required>
  </div>

  <div class="input-container">
    <i class="fa fa-key icon"></i>
    <input class="input-field" type="password" placeholder="Password" name="psw" maxlength="8" minlength="4"  id="pass" required onkeypress="return AvoidSpace(event)">
  </div>
                   <div class="input-container">
   <i class="fa fa-street-view icon"></i>
            <asp:DropDownList ID="DropDownList1" runat="server"  class="input-field" >
    
            </asp:DropDownList>
  </div>
                      


  <button type="button" class="btn pull-right" onclick="Insert()" style=" background-color: #0A408A;">Create</button>
 
                 <br />
                 <br />

          
  
           
              </div>
             </div>
           </div>
         </div>

        <br />
        <br />
     <div class="container" style="width:99%;margin-top:-26px;">
     <div class="panel-group" style="width:99%;">
       <div class="panel panel-primary" style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;text-align:center;">All Users</h2>

          </div>
         <div class="panel-body">
             

  <h2>Employess</h2>
       <div class=" col-md-12 text-center">
               
    <table id="datatable" style=" width:100%; height:300px;">
       <thead>
                    <tr>
                       
                        <th>Name</th>
                        <th>Role</th>
                        <th>Designation</th>
                        
                        <th>Department</th>
                        <th>Picture</th>
                        <th>Edit</th>
                        
                    </tr>
                </thead>
    </table>
       
      
           </div>

 
                   

 
               

        
           
              </div>
             </div>
           </div>
         </div>
    
          <!--model-->
           <div class="container">
        <div class="modal fade" id="SalaryPackage" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-md" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A">
            <h2 style="color:white;">Salary Package</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
    

                            <br />

                              <div class="form-group">
                                <label class="control-label col-xs-12" for="pak_Name">Password:</label>
                                <div class="col-xs-12">
                                    <input type="text" class="form-control" id="ipass"  maxlength="8" placeholder="Password" required>
                                </div>
                                     <label class="control-label col-xs-12"  for="Reason">Role:</label>
                                <div class="col-xs-12">
                                    <asp:DropDownList ID="DropDownList2" runat="server" Width="100%"></asp:DropDownList>
                                </div>
                            </div>
           <br />
          <br />
          <div class="form-group">
                               
                                <div class="col-xs-12  text-center">
                                    <button type="button" class=" btn btn-primary"  onclick="Update();" style=" background-color:#0A408A">Update</button>
                                </div>
                            </div>

      </div>
    </div>
</div>
        </div>

          <div class="container">
        <div class="modal fade" id="EditPopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <table class="table-responsive-lg">
                <tr>
                    <td class="col-sm-4"><h2 style="color:white;">Add Items</h2> </td>

             </tr>
                    </table>
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
  
 
                        <br />
      <div class="form-group">
          <div>
  <label class="form-control col-sm-9" id="txtgetid" hidden></label>
              

          </div>
      </div>
    
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
<td><asp:DropDownList ID="getitems" runat="server">

    <asp:ListItem Value="1" Text="boomb"></asp:ListItem>
    </asp:DropDownList>

   </td>
 
        <td>
            <input type ="number" id="getInQuantity" class="form-control" min="1"      />
        </td>
          <td><input type="button"  class="btn btn-primary" id="btnAdd" value="Add Row" onclick="AddRow() "/></td>
               </tr>
  
    </tbody>
      
  </table>
      <input type="button" id="savetable" class="btn btn-primary" value="Save" onclick="InsertData();" />
    
        </div>
      </div>
    </div>
</div>
        </div>
        </form>


</asp:Content>
