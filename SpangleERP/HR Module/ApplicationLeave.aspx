<%@ Page Title="" Language="C#" MasterPageFile="~/HR Module/HR_Master.Master" AutoEventWireup="true" CodeBehind="ApplicationLeave.aspx.cs" Inherits="SpangleERP.HR_Module.ApplicationLeave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
    
     <link href="../Content/css/style.css" rel="stylesheet" />
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />


    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/bootstrap.js"></script>
    <script src="../Content/js/jquery-2.1.1.min.js"></script>
    <link href="bootstrap-datepicker.css" rel="stylesheet" />
    <script src="bootstrap-datepicker.js"></script>
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
        var id = "";

            $j(document).ready(function () {

                 $j('#datatable').DataTable({
            "aLengthMenu": [[10, 25,5], [10, 25, 5]],
                "iDisplayLength": 5,
                columns: [
           
                   { 'data': 'emp_id' },
                    { 'data': 'emp_name' },
                      {
                        'data': 'Img',

                        'sortable': false,
                                'searchable': false,
                        'render': function (webSite) {
                            return '<img src="' + webSite + '" class=" img img-responsive  img-rounded" style="width:80; height:80px;" />';
                                    
                                }

                    },
                    { 'data': 'type' },
                    { 'data': 'reason' },
                    { 'data': 'days' },
                    { 'data': 'date' }

                   
                   
                            
                                        
                    ],
                bServerSide: true,
                sAjaxSource: 'Employees.asmx/GetALl_Leaves',
                sServerMethod: 'post'
            });



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

        

        function Save() {
            var days = 0;
            alert("agaya");
            var e = document.getElementById("box1");
var strUser = e.options[e.selectedIndex].value;

          var re = $('#reason').val();
            //alert(selectedCountry + " " + sd + " " + ed=" " + re);
          var date = new Date($('#s_date').val());
  day = date.getDate()+1;
  month = date.getMonth() + 1;
  year = date.getFullYear();
 var sd= [day, month, year].join('-');

             var date1 = new Date($('#e_date').val());
  day = date1.getDate()+1;
  month = date1.getMonth() + 1;
            year = date1.getFullYear();
              var x = document.getElementById("s_date");
            var dob = x.value.toString();
             var y = document.getElementById("e_date");
            var doj = y.value.toString();

            var ed = [day, month, year].join('-');
            var res = Math.abs(date - date1) / 1000;
            days = Math.floor(res / 86400);
            var cur_days = days + 1;
            alert(cur_days);
            if (strUser != "" && re != "" && sd != "" && ed != "" && cur_days != "" && id != "") {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'ApplicationLeave.aspx/Insert',

                    data: "{'type':'" + strUser + "','re':'" + re.toString() + "','sd':'" + dob + "','ed':'" + doj + "','days':'" + cur_days + "','id':'" + id + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                        alert(data.d);


                        if (data.d == "Save") {
                           

                            alert("Save Ho gya");

                            $('#reason').val("");
                            $('#txtseach').val("");
                            id = "";
                        }
                        else if (data.d == "Days>") {
                            alert("Dont Have Leaves");

                        }


                    },
                    Error: function (res) {
                        alert("Error Occure" + res);

                    }
                });
            }
            else {

                alert("Please fill the form correctly");
            }
           
        }

    </script>
    <style>
        ul.ui-autocomplete {
    z-index: 1100;
}
               
.imageclass
{
    width:50px;
    height:50px;
    
    }
    </style>
  <</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!--start Table Panel-->
   
     <div class="container" style="width:99%;margin-top:-26px;">
     <div class="panel-group" style="width:99%;">
       <div class="panel panel-primary" style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">View Application</h2>
               <div class="table-responsive" style="border-color:#0A408A;border:2px;">
              <table  class="pull-right" style="border-color:#0A408A;border:2px;">
                  <tr>
                      <td>
          <button type="button" class="btn btn-primary" style="font-size:18px;background-color:#0A408A;color:white;" data-toggle="modal" data-target="#AddEmployeePopup" data-backdrop="false" >Leave Application</button>
  </td><td>

                  </td></tr>
              </table>


                </div>
      </div>
         <div class="panel-body">
            
      
             <br />
             <br />
                           
    <table id="datatable" style="  height:300px; overflow:scroll;" >
       <thead>
                    <tr>
                        <th>Emp_id</th>
                        <th>Name</th>
                        <th>Picture</th>
                        
                      <th>Type</th>
                        <th>Reason</th>
                        <th>Days</th>
                        <th>Submite Date</th>
                    </tr>
                </thead>
    </table>
              
  
  </div>
</div>
     </div>
    

         </div>
           <%-- <div class="panel-group" style="width:99%;">
                <div class="panel panel-primary">
                    <div class="panel-heading"><h2>Leave Application </h2></div>
                    <div class="panel-body">--%>

                       
          <%--      </div>


            </div>
        </div>--%>
        <!--model-->
    
           <div class="container">
        <div class="modal fade" id="AddEmployeePopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A">
            <h2 style="color:white;">Applications</h2>
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

                             

                            <div class="form-group">
                                <label class="control-label col-sm-3" for="Name">Employee Name:</label>
                                <div class="col-sm-3">
                                    <input type="text" class="form-control" id="txtseach" placeholder="Employee Name">
                                     <input type="text" id="id" name="postId" style=" display:none;" />
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <label class="control-label col-sm-3" for="LeaveType">Leave Type:</label>
                                <div class="col-sm-3" id="Leave Type">
                                    <select class="form-control country" id="box1" >
                                     
                                        <option value="cl">Casual_Leave</option>
                                        <option value="sl">Sick_Leave</option>
                                        <option value="al">Annual_Leave</option>

                                    </select>
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <label class="control-label col-sm-3" for="Date">To Date:</label>
                                <div class="col-sm-3">
                                    <input type="date" class="form-control" id="s_date" placeholder="To Date" required>
                                </div>

                            </div>
                            <br />
                            <div class="form-group">
                                <label class="control-label col-xs-3" for="fromDate">From Date:</label>
                                <div class="col-sm-3">
                                    <input type="date" class="form-control" id="e_date" placeholder="From Date" required>
                                </div>

                            </div>
                            <br />
                            <div class="form-group">
                                <label class="control-label col-sm-3" for="Reason">Reason:</label>
                                <div class="row">
                                    <div class="col-sm-4">

                                        <input type="text" class="form-control" id="reason" placeholder="Reason" style="height:100px" required>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div style="padding-left:250px">
                               
                                <button type="button" class="btn btn-primary" onclick="Save();" style="background-color:#0A408A;color:white;">Save</button>
                               

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

</asp:Content>
