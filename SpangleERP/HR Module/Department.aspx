 <%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="SpangleERP.HR_Module.Department" %>
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


        $j(document).ready(function () {


            showData();
        });

        function showData() {
                $j('#datatable').DataTable({
            "aLengthMenu": [[10, 25,5], [10, 25, 5]],
                "iDisplayLength": 5,
                columns: [
           
                   { 'data': 'dep_name' },
                    { 'data': 'desc' },
                   


                     {
                                'data': 'dep_id',
                                'sortable': false,
                                'searchable': false,
                         'render': function (webSite) {
                            
                                 return ' <button type="button" class=" btn btn-primary" value="' + webSite + '" onclick="Edit(this.value);" data-toggle="modal" data-target="#AddEmployeePopup"  style="background-color:#0A408A;color:white;">Edit</button>';
                            
                                }
                            }
                   
                   
                            
                                        
                    ],
                bServerSide: true,
                sAjaxSource: 'Employees.asmx/Get_Departments',
                sServerMethod: 'post'
            });


        }
        var id;
        function Edit(Val) {

                  $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Department.aspx/GetEditData",
                    data: "{'Val':'" + Val + "'}",
                    dataType: "json",
                    success: function (data) {
                          $('#dname1').val(data.d.dep_name);
             $('#ddesig1').val(data.d.desc);
                     
            $j('#distPopup1').modal('show');
                        id = Val;
                    },
                    error: function (err) {
                        alert(err);
                    }

                    
                });
           
        }


        
        function Save() {

            var name = $('#dname').val();
            var des = $('#ddesig').val();

            if (name!="" && des!="") {


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Department.aspx/Save",
                    data: "{'name':'" + name + "','des':'" + des + "'}",
                    dataType: "json",
                    success: function (data) {

                        alert(data.d);

                        if (data.d == "Save Successfully") {
                            window.location = "Department.aspx";
                        }
                        
                    },
                    error: function (err) {
                        alert(err);
                    }

                    
                });

            }
            else {
                alert("Please Fill The Form Correctly");
            }



        }

        function Update() {
              var name = $('#dname1').val();
            var des = $('#ddesig1').val();

            if (name != "" && des != "" && id != "") {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Department.aspx/Update",
                    data: "{'name':'" + name + "','des':'" + des + "','did':'" + id + "'}",
                    dataType: "json",
                    success: function (data) {

                        alert(data.d);

                        if (data.d == "Update Successfully") {
                            id = "";
                            window.location = "Department.aspx";
                        }

                    },
                    error: function (err) {
                        alert(err);
                    }


                });

            }

            else {
                alert("Please Fill The Form Correctly");
            }

        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form class="form-horizontal" method="post" runat="server">
    <div class="container" style="width:100%;margin-top:-26px;">
     <div class="panel-group" style="width:100%;">
       <div class="panel panel-primary"  style="border-color:#0A408A;border:2px;">
       <div class="panel-heading" style="background-color: #0A408A;">
                            <h2 style="color: white; font-family: Cambria; font-weight: 200;">Departments</h2>
                            <div class="table-responsive" style="border-color: #0A408A; border: 2px;">
                                <table class="pull-right" >
                                    <tr>
                                        <td>
                                            <button type="button" class="btn btn-primary" style="font-size: 18px; background-color: #0A408A; color: white;" data-toggle="modal" data-target="#distPopup" data-backdrop="false">Add New Department</button>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>


                            </div>
                        </div>
         <div class="panel-body">
            
             <!--Table--> 
           
   <div class="table-responsive" style="border-color:white;border:2px;"> 
  

       <br />
       <div class="table-wrapper-scroll-y my-custom-scrollbar">
       
                                   
    <table id="datatable" style=" width:100%; height:300px; overflow:scroll;" >
       <thead>
                    <tr>
                        <th>Dep_Name</th>
                        <th>Discription</th>
                        
                        <th>Edit</th>
                    </tr>
                </thead>
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
        <div class="modal fade" id="distPopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <label id="dimagepath" style="display:none;"></label>
            <h2 style="color:white;">Add Department</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
       <%--  <div class="container">
     <div class="panel-group">
       <div class="panel panel-primary">
      <div class="panel-heading"><h2>Add Employee</h2></div>
      <div class="panel-body">--%>



                        <br />


          <div class="form-group">
            <label class="control-label col-sm-3" for="dname">Department Name:</label>
            <div class="col-sm-3">
           <input type="text" style="border:groove;" class="form-control" id="dname"  required>
      
            </div>
            
                        </div>
          <div class="form-group">
                      <label class="control-label col-sm-3" for="ddesig">Discription:</label>
          <div class="col-sm-3">
                   <input type="text" style="border:groove;" class="form-control" id="ddesig" required>
          </div>
            
          </div>

        



       
    
                   
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-3">
                            <button type="button" class="btn btn-primary"   id="BtnSave" style="background-color:#0A408A;color:white;" onclick="Save();">Add Records</button>
                         

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

           <div class="container">
        <div class="modal fade" id="distPopup1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <label id="dimagepath2" style="display:none;"></label>
            <h2 style="color:white;">Add Department</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
       <%--  <div class="container">
     <div class="panel-group">
       <div class="panel panel-primary">
      <div class="panel-heading"><h2>Add Employee</h2></div>
      <div class="panel-body">--%>



                        <br />


          <div class="form-group">
            <label class="control-label col-sm-3" for="dname">Department Name:</label>
            <div class="col-sm-3">
           <input type="text" style="border:groove;" class="form-control" id="dname1"  required>
      
            </div>
            
                        </div>
          <div class="form-group">
                      <label class="control-label col-sm-3" for="ddesig">Discription:</label>
          <div class="col-sm-9">
                   <input type="text" style="border:groove; width:100%;height:100px;" class="form-control" id="ddesig1" required>
          </div>
            
          </div>

        



       
    
                   
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-3">
                            <button type="button" class="btn btn-primary"   id="BtnSave1" style="background-color:#0A408A;color:white;" onclick="Update();">Update</button>
                         

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
                </form>

</asp:Content>
