<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="Employee_History.aspx.cs" Inherits="SpangleERP.HR_Module.Employee_History" %>
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
     
     <script src="../jquery-ui.js"></script>
    <link href="../jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        var $j = jQuery.noConflict();

        var view = false;
        var Insert = false;
        var Update = false;
        var Access = "";

        $j(document).ready(function () {
            Access_Levels();
            $j('#hide').hide();
            $j('#download').hide();
            $j('#form').show();

            if (view == true) {
                Show();

            }
            else {
                alert("You Dont Have Rights To View This")
            }

            if (Insert == true) {
                $j('#In').show();
            }
            else {
                $j('#In').hide();
            }


            
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


        });


        function Access_Levels() {

                    $.ajax({
                    url: 'Employee_History.aspx/Access_Levels',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    method: 'post',
                        data: "{}",
                        async: false,
                    success: function (data) {
                        Access = data.d;
                        view = Access.includes("V");
                        Insert = Access.includes("I");
                        Update = Access.includes("U");
                        
                    },
                    error: function (err) {
                        alert(err);
                    }
            });

            
              
                
            }
         
                function Show() {
                    var hid=0;
                    $j('#datatable').DataTable({
                        "aLengthMenu": [[10, 25, 5], [10, 25, 5]],
                        "iDisplayLength": 5,
                        columns: [

                            { 'data': 'hist_id' },
                            { 'data': 'date_of_exit' },
                            { 'data': 'emp_name' },

                            { 'data': 'Reason' },

                            {
                                'data': 'Status',

                                'sortable': false,

                                'render': function (webSite) {
                                    if (webSite == 0) return "Not Aprove"

                                    else return "Aprove";

                                }


                            },
                            { 'data': 'designation' },
                            {
                                'data': 'documrnt',
                                'sortable': false,
                                'render': function (Val) {
                                    return '  <button type="button" class="btn btn-primary" value="'+Val+'" onclick="Download(this.value)">Download</button>';
                                }

                            }
                   
                    
                            
                                        
                    ],
                bServerSide: true,
                sAjaxSource: 'Employees.asmx/GetAll_Emp_History',
                sServerMethod: 'post'
            });

        }

        function Download(Val) {
            alert(Val);

            var name = Val.split(".")[1];
            alert("name "+name);
                  $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                      url: 'Employee_History.aspx/Download',
                      data: "{'hid':'" + Val + "','Extension':'" + name + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                        $j('#form').hide();
                        $j('#download').show();
                        $j('#SalaryPackage').modal('show'); 
                    },
                    Error: function (res) {
                        alert("Error Occure" + res);

                    }
            });


        }


        function Insert() {
            var reason = $('#rea').val();
            
            var x = document.getElementById("date");
           
            var Ldate = x.value.toString();
           
            if (id != "" && reason != "" && Ldate != "") {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Employee_History.aspx/Insert',
                    data: "{'eid':'" + id + "','Date':'" + Ldate + "','Reason':'" + reason + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                        if (data.d == "Save") {


                            $('#hide').show();
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
                alert("Please Fill Fields");
            }

            

        }
    </script>
    
    <style>

        .imageclass
{
    width:50px;
    height:50px;
    
    }
          ul.ui-autocomplete {
    z-index: 1100;
}

        
    </style>
  <style type="text/css">

	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


           <form runat="server">
        <!--Table-->
        <!--start Table Panel-->

     <div class="container" style="width:99%;margin-top:-26px;" >
     <div class="panel-group" style="width:99%;">
       <div class="panel panel-primary" style="border-color:#0A408A;border:2px;" >
      <div class="panel-heading"style="background-color:#0A408A"><h2 style="color:white;font-family:Cambria;font-weight:200;">Employee's History</h2>
              <div class="table-responsive" style="border-color:#0A408A;border:2px;">
              <table  class="pull-right" style="border-color:#0A408A;border:2px;">
                  <tr>
                      <td>
                         <div id="In">
          <button type="button" class="btn btn-primary pull-right" style="font-size:18px;background-color:#0A408A;color:white;" data-toggle="modal" data-target="#SalaryPackage" data-backdrop="false"  >Resigned Employee's History</button>
                       </div>
                      </td><td>

                  </td></tr>
              </table>

                </div>
                          </div>
         
          <div class="panel-body">
            
             <!--Table-->     
              
     
   <div class="table-responsive"> 
   
       <br />
  <table class="table" id="datatable">
    
    <thead>
                    <tr>
                        <th>Id</th>
                        <th>Date</th>
                        <th>Name</th>
                        <th>Reason</th>
                        <th>Status</th>
                        <th>Designation</th>
                       <th>Download</th>
                        
                    </tr>
                </thead>
  </table>
  </div>
</div>
  </div>
</div>
    
    </div>
                   


          <div class="container" id="3">
        <div class="modal fade" id="SalaryPackage" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A">
            <h2 style="color:white;"> Employee Resigned Form</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
       <%--  <div class="container">
     <div class="panel-group">
       <div class="panel panel-primary">
      <div class="panel-heading"><h2>Add Employee</h2></div>
      <div class="panel-body">--%>

         <form class="form-horizontal" method="post">



                            <br />
       
             <div id="form">
                              <div class="form-group">
                                <label class="control-label col-sm-4" for="Emp_Name">Employee Name:</label>
                                <div class="col-sm-8">
                                    <input type="text"  class="form-control" id="txtseach" style=" border-color:aqua; border:ridge" id="Empl_Name" placeholder="Employee Name"  >
                               <input type="text" id="id" style=" display:none;"  />
                                    </div>
                                     
                            </div>
             <br />
           <br />
             <div class="form-group">
                                <label class="control-label col-sm-4" for="Emp_Name">Reason:</label>
                                <div class="col-sm-8">
                                   <textarea class="form-control" rows="2" id="rea"   placeholder="Enter Reason"></textarea>
                                </div>
                                     
      </div>
          <br />
                 <br />
             <div class="form-group">
                                <label class="control-label col-sm-4" for="Emp_Name">Date:</label>
                                <div class="col-sm-8">
                                 <input type="date" id="date" class="form-control" /> 
                                </div>
                                     
       </div>
                           
                       
                          
                            <div class=" col-sm-12 text-center">
                              <button type="button" class="btn btn-primary  text-center" onclick="Insert();" style="background-color:#0A408A;color:white">Save</button>
                              </div>

             <br />
             <br />
             <br />

             
             <div id="hide" class="col-sm-12 text-center">
                  <asp:FileUpload ID="FileUpload1" runat="server"  />
              
          <asp:Button ID="Button1" runat="server" Text="Save Document" CssClass=" btn btn-primary" OnClick="Button1_Click"   />

             </div>
             </div>
             <div class="form-group" id="download">
                                <label class="control-label col-sm-4" for="Emp_Name">Download File</label>
                                <div class="col-sm-8">
                                   <asp:Button ID="Button2" runat="server" Text="Download" CssClass=" btn btn-primary" OnClick="Button2_Click" />

                                    </div>
                 </div>
                        </form>
                  
        
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
