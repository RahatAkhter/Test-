<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="UpdateLeave.aspx.cs" Inherits="SpangleERP.HR_Module.UpdateLeave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <meta charset="utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
     <!--online-->

    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <script src="../Content/js/jquery-2.1.1.min.js"></script>
    
<link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" rel="stylesheet" />
<script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="../jquery-ui.js"></script>
    <link href="../jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        var $j = jQuery.noConflict();
        var id = "";
        var eid = "";
        var view = false;
        var Insert = false;
        var Update = false;
        var Access="";

            $j(document).ready(function () {

                Access_Levels();

                if (view == true) {
                    showData();
                }
                else {
                    alert("you Have no Rights To Show Data");
                }
                if (Insert == true) {
                    $('#In').show();
                }
                else {
                    $('#In').hide();
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

        function showData() {
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
                            return '<img src="' + webSite + '" class=" img img-responsive  img-circle" style="width:80; height:80px;" />';
                                    
                                }

                    },
                    { 'data': 'Desig' },
                    { 'data': 'sl' },
                    { 'data': 'cl' },
                    { 'data': 'al' },
                    { 'data': 'lsl' },
                    { 'data': 'lcl' },
                    { 'data': 'lal' },


                     {
                                'data': 'emp_id',
                                'sortable': false,
                                'searchable': false,
                         'render': function (webSite) {
                             if (Update == true)
                                 return ' <button type="button" class=" btn btn-primary" value="' + webSite + '" onclick="Edit(this.value);" data-toggle="modal" data-target="#AddEmployeePopup"  style="background-color:#0A408A;color:white;">Edit</button>';
                             else return '';
                                }
                            }
                   
                   
                            
                                        
                    ],
                bServerSide: true,
                sAjaxSource: 'Employees.asmx/GetLeaves_Count',
                sServerMethod: 'post'
            });


        }


           function Access_Levels() {

                    $.ajax({
                    url: 'UpdateLeave.aspx/Access_Levels',
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

        function Edit(Val) {
            
            eid = Val;
          
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: 'UpdateLeave.aspx/Get_Data',
                data: "{'eid':'" + eid + "'}",
                datatype: "json",
                success: function (data) {
                    for (var i = 0; i < data.d.length; i++) {
                       
                        // document.getElementById('imgtag').src = data.d[i].p_Img;
                        
                           $('#uSickLeave').val(data.d[i].sl);
                            $('#uCasualLeave').val(data.d[i].cl);
                            $('#uAnnualLeave').val(data.d[i].al);
                    }
                    
                },
                error: function (err) {
                    console.log(err);
                }
            });



        }


        function Save() {

            

             var sl = $('#SickLeave').val();
            var cl = $('#CasualLeave').val();
            var al = $('#AnnualLeave').val();
           

            if (sl != "" && cl != "" && al != "" && id != "") {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'UpdateLeave.aspx/Insert',

                    data: "{'sl':'" + sl.toString() + "','cl':'" + cl.toString() + "','al':'" + al + "','emp_id':'" + id + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                        if (data.d == "Save") {
                            id = "";
                            alert("ho gya " + data.d);
                            $('#txtseach').val("");
                            $('#SickLeave').val("");
                            $('#CasualLeave').val("");
                            $('#AnnualLeave').val("");


                        }
                        else {
                            alert("You Already Insereted That Employee Leave Data ");

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


        
        function Update() {
            
             var sl = $('#uSickLeave').val();
            var cl = $('#uCasualLeave').val();
            var al = $('#uAnnualLeave').val();
           

            if (sl != "" && cl != "" && al != "" && eid != "") {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'UpdateLeave.aspx/Update',

                    data: "{'sl':'" + sl.toString() + "','cl':'" + cl.toString() + "','al':'" + al + "','emp_id':'" + eid + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                        if (data.d == "Save") {
                            eid = "";
                            alert("ho gya " + data.d);
                            $('#uSickLeave').val("");
                            $('#uCasualLeave').val("");
                            $('#uAnnualLeave').val("");


                        }
                        else {
                            alert("Error");

                        }

                    },
                    Error: function (res) {
                        alert("Error Occure" + res);

                    }
                });


            }
            else {

                alert("Please fill the form first");
            }
        }



        </script>
    <style>

        
.imageclass
{
    width:50px;
    height:50px;
    
    }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <body>
 

    <div class="container" style="width:99%;margin-top:-26px;">
        <div class="panel-group" style="width:99%">
            <div class="panel panel-primary" style="border-color:#0A408A;border:2px;">
                <div class="panel-heading" style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">Leave Data Update</h2></div>
                <div class="panel-body">

                    <form class="form-horizontal" method="post">

                        <br />


                        <div id="In">
                        <div class="form-group">
                            <label class="control-label col-sm-2" for="Name">Employee Name:</label>
                             <input type="text" id="id" name="postId" style=" display:none;" />
                            <div class="col-sm-3">
                                <input type="text" class="form-control" id="txtseach" placeholder="Emp Name" required>
                            </div>
                             <label class="control-label col-sm-2" for="CasualLeave">Casual Leave:</label>
                            <div class="col-sm-3">
                                <input type="number" class="form-control" id="CasualLeave" placeholder="Casual Leave">
                            </div>

                        </div>
                        <br />
                    
                      
                        <br />
                        <div class="form-group">
                            <label class="control-label col-sm-2" for="SickLeave">Sick Leave:</label>
                            <div class="col-sm-3">
                                <input type="number" class="form-control" id="SickLeave" placeholder="Sick Leave" required>
                            </div>
                             <label class="control-label col-sm-2" for="AnnualLeave">Annual Leave:</label>
                            <div class="col-sm-3">
                                <input type="number" class="form-control" id="AnnualLeave" placeholder="Annual Leave" required>
                            </div>
                        </div>
                        <br />
                        <div class=" col-sm-12 text-center">
                            <button type="button" class="btn btn-primary" onclick="Save();" style="background-color:#0A408A;color:white;">Save</button>
                          

                        </div>

                   </div>
                           
    <table id="datatable" style=" width:100%; height:300px; overflow:scroll;" >
       <thead>
                    <tr>
                        <th>Emp_id</th>
                        <th>Name</th>
                        <th>Picture</th>
                        <th>Designation</th>
                        <th>Sl</th>
                        <th>Cl</th>
                        <th>Al</th>
                        <th>LSL</th>
                        <th>LCL</th>
                        <th>LAL</th>
                        <th>Edit</th>
                    </tr>
                </thead>
    </table>
       



                    </form>



                </div>
            </div>


        </div>
    </div>

        <br />
        <br />

        <!--here is table Panel-->
        
    

        
    <!-- Modal -->
    <div class="container">
        <div class="modal fade" id="AddEmployeePopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#006699">
            <h2 style="color:white;">Add Employee's</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="close();">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
       <%--  <div class="container">
     <div class="panel-group">
       <div class="panel panel-primary">
      <div class="panel-heading"><h2>Add Employee</h2></div>
      <div class="panel-body">--%>

      <div class="form-group">
                           
                             <label class="control-label col-xs-2" for="CasualLeave">Casual Leave:</label>
                            <div class="col-xs-3">
                                <input type="number" class="form-control" id="uCasualLeave" placeholder="Casual Leave">
                            </div>
           <label class="control-label col-xs-2" for="SickLeave">Sick Leave:</label>
                            <div class="col-xs-3">
                                <input type="number" class="form-control" id="uSickLeave" placeholder="Sick Leave" required>
                            </div>
                        </div>
                        <br />
                    
                      
                        <br />
                        <div class="form-group">
                           
                             <label class="control-label col-xs-2" for="AnnualLeave">Annual Leave:</label>
                            <div class="col-xs-3">
                                <input type="number" class="form-control" id="uAnnualLeave" placeholder="Annual Leave" required>
                            </div>
                        </div>
      
             <br>
        <div class="form-group">
          
                     <div class="col-xs-12 text-center">
                        
                     
                             <button type="button" class="btn btn-primary" onclick="Update();">Update</button>
                           </div>
        </div>
             
             
    </div>
   </div> 
            </div>
        </div>


</body>
</asp:Content>
