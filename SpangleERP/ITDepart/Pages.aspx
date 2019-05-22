<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="Pages.aspx.cs" Inherits="SpangleERP.ITDepart.Pages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            Show_Data();
        });

        function Show_Data() {
             
                       
                 $j('#datatable').DataTable({
            "aLengthMenu": [[10, 25,5], [10, 25, 5]],
                "iDisplayLength": 5,
                columns: [
           
                   { 'data': 'URl' },
                    { 'data': 'Icon_Name' },
                    { 'data': 'Page_Name' },
                    { 'data': 'Folder_Name' },
                    
                  {
                                'data': 'page_id',
                                'sortable': false,
                                'searchable': false,
                      'render': function (val) {

                          return ' <button type="button" class=" btn btn-primary" value="' + val + '" data-toggle="modal" data-target="#AddEmployeePopup" onclick="Edit(this.value);" style=" background-color: #0A408A;" >Edit</button>';
                      }
                    }
                   
                            
                                        
                    ],
                bServerSide: true,
                sAjaxSource: 'ITService.asmx/GetAll_Employees',
                sServerMethod: 'post'
            });
        }

        var pid;

        function Edit(Val) {

                    $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Pages.aspx/GetRecord',
                    data: "{'page_id':'" + Val + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {

                        for (var i = 0; i < data.d.length; i++) {
                   $('#Page_Name1').val(data.d[i].Page_Name);
            $('#path1').val(data.d[i].URl);
            $('#fname1').val(data.d[i].Folder_Name);
                            $('#Icon_Name1').val(data.d[i].Icon_Name);
                            pid = Val;
                           
                            $j('#EditModal').modal('show');
                                                   }
                    },
                    Error: function (res) {
                        alert("Error Occure" + res);

                    }
                });

        }


        function Update() {
             var pname = $('#Page_Name1').val();
            var page_url = $('#path1').val();
            var fname = $('#fname1').val();
            var Icon = $('#Icon_Name1').val();


            if (pname != "" && page_url != "" && Icon != "" && fname!="") {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Pages.aspx/Update',
                    data: "{'pname':'" + pname + "','page_url':'" + page_url + "','Icon':'" + Icon + "','fName':'" + fname + "','page_id':'" + pid + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                        if (data.d == "Update") {
                            $('#Page_Name1').val("");
                            $('#path1').val("");
                            $('#Icon_Name1').val("");
                            $('#fname').val("");
                            alert("Update Successfully");
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
                alert("Please Fil The Form Correctly");
            }


        }

        function Save() {
            var pname = $('#Page_Name').val();
            var page_url = $('#path').val();
            var fname = $('#fname').val();
            var Icon = $('#Icon_Name').val();
          

            if (pname != "" && page_url != "" && Icon != "" && fnam!="") {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Pages.aspx/Insert',
                    data: "{'pname':'" + pname + "','page_url':'" + page_url + "','Icon':'" + Icon + "','fName':'" + fname + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                        if (data.d == "Save") {
                            $('#Page_Name').val("");
                            $('#path').val("");
                            $('#Icon_Name').val("");
                            alert("Save Successfully");
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
                alert("Please Fil The Form Correctly");
            }




        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width:100%;margin-top:-26px;">
     <div class="panel-group" style="width:100%;">
       <div class="panel panel-primary"  style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">pages</h2>
          <div class="table-responsive table-striped" style="border-color:#0A408A;border:2px;">
              <table  class="pull-right"  style="border-color:#0A408A;border:2px;">
                  <tr>
                      <td>
         <button type="button" class="btn btn-primary" style=" font-size:18px;background-color:#0A408A;color:white;" data-toggle="modal" data-target="#MachinePopup" data-backdrop="false" >Add New</button>
                  </td></tr>
              </table>


                </div>
          </div>
         <div class="panel-body">
            
             <!--Table-->      
             
             <h2>Employess</h2>
       <div class=" col-md-12 text-center">
               
    <table id="datatable" style=" width:100%; height:300px;">
       <thead>
                    <tr>
                       
                        <th>URl</th>
                        <th>Icon_Name</th>
                        <th>page Name</th>
                        
                        <th>Folder Name</th>
                        <th>Edit</th>
                        
                    </tr>
                </thead>
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
            <h2 style="color:white;">Add New Records</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
       
  <form class="form-horizontal" method="post" runat="server">

                        <br />


        <div class="form-group">
            <label class="control-label col-sm-2" for="Name">Page URl:</label>
            <div class="col-sm-4">
     <input type="text" class="form-control" id="path"  required>
              
            </div>
                <label class="control-label col-sm-2" for="txt">Icon_Name:</label>
          <div class="col-sm-4">
                   <input type="text" class="form-control" id="Icon_Name"    required>
          </div>
                   
                        </div>
             <div class="form-group">
            <label class="control-label col-sm-2" for="txt1">Page Name:</label>
            <div class="col-sm-4">
     <input type="text" class="form-control" id="Page_Name"   required />

            </div>
                <label class="control-label col-sm-2" for="txt1">Folder Name:</label>
            <div class="col-sm-4">
     <input type="text" class="form-control" id="fname"   required />

            </div>
                   
                        </div>

    
    
                   
                        <div class="form-group">
                            <div class="col-xs-12 text-center">
                            <button type="button" class="btn btn-primary"  id="BtnSave" style="background-color:#0A408A;color:white;" onclick="Save();">Save</button>
            
                         
                                </div>
                        </div>
                
    


        </div>
      </div>
    </div>
</div>
        </div>
       <%--        End popup--%>




     <!-- Modal Two-->
    <div class="container">
        <div class="modal fade" id="EditModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <h2 style="color:white;">Add New Records</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
       
 
                        <br />


        <div class="form-group">
            <label class="control-label col-sm-2" for="Name">Page URl:</label>
            <div class="col-sm-4">
     <input type="text" class="form-control" id="path1"  required>
              
            </div>
                <label class="control-label col-sm-2" for="txt">Icon_Name:</label>
          <div class="col-sm-4">
                   <input type="text" class="form-control" id="Icon_Name1"    required>
          </div>
                   
                        </div>
             <div class="form-group">
            <label class="control-label col-sm-2" for="txt1">Page Name:</label>
            <div class="col-sm-4">
     <input type="text" class="form-control" id="Page_Name1"   required />

            </div>
                <label class="control-label col-sm-2" for="txt1">Folder Name:</label>
            <div class="col-sm-4">
     <input type="text" class="form-control" id="fname1"   required />

            </div>
                   
                        </div>

    
    
                   
                        <div class="form-group">
                            <div class="col-xs-12 text-center">
                            <button type="button" class="btn btn-primary"  id="BtnUpdate" style="background-color:#0A408A;color:white;" onclick="Update();">Update</button>
            
                         
                                </div>
                        </div>
                    </form>
    


        </div>
      </div>
    </div>
</div>
        </div>
       <%--        End popup--%>

</asp:Content>
