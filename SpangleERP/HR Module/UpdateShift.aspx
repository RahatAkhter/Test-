<%@ Page Title="" Language="C#" MasterPageFile="~/HR Module/HR_Master.Master" AutoEventWireup="true" CodeBehind="UpdateShift.aspx.cs" Inherits="SpangleERP.HR_Module.UpdateShift" %>
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
        var id = "";
        var edit_id;
        var mon;

        $j(document).ready(function () {
            $('#hide').hide();
                   $j('#datatable').DataTable({
            "aLengthMenu": [[10, 25,5], [10, 25, 5]],
                "iDisplayLength": 5,
                columns: [
           
                    {
                        'data': 'emp_id',
                        'render': function (webSite) {
                            edit_id = webSite;
                            return edit_id;
                                },

                    },
                    { 'data': 'emp_name' },
                      {'data': 'desig', },
                    {
                        'data': 'mon',
                        'render': function (webSite) {
                            mon = webSite;
                            return mon;
                                },

                    },
                    { 'data': 'tue' },
                    { 'data': 'wed' },
                    { 'data': 'thu' },
                    { 'data': 'fri' },
                    { 'data': 'sat' },
                    { 'data': 'sun' },


                     {
                                'data': 'status',
                               
                         'render': function (webSite) {
                             if (webSite == 1) return "Woking";
                             else return "Fired";

                                },
                            },
                   
                   
                   
                     {
                                'data': 'status',
                               
                         'render': function (webSite) {
                             if (webSite == 1 && mon != " to ") {
                                 return ' <button type="button" class=" btn btn-primary" value="' + edit_id + '" onclick="Edit(this.value);" >Edit</button>';
                                 
                             }
                             else if (webSite == 1 && mon == " to ") return 'Add Shifts';
                             else  return "";

                                },
                            },
                   
                            
                                        
                    ],
                bServerSide: true,
                sAjaxSource: 'Employees.asmx/Get_EmpShifts',
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

              var ddl1 = document.getElementById("<%=DropDownList1.ClientID%>");
        var mon = ddl1.options[ddl1.selectedIndex].value;
              var ddl2 = document.getElementById("<%=DropDownList2.ClientID%>");
        var tue = ddl2.options[ddl2.selectedIndex].value;
              var ddl3 = document.getElementById("<%=DropDownList3.ClientID%>");
        var wed = ddl3.options[ddl3.selectedIndex].value;
              var ddl4 = document.getElementById("<%=DropDownList4.ClientID%>");
        var thu = ddl4.options[ddl4.selectedIndex].value;
              var ddl5 = document.getElementById("<%=DropDownList5.ClientID%>");
        var fri = ddl5.options[ddl5.selectedIndex].value;
              var ddl6 = document.getElementById("<%=DropDownList6.ClientID%>");
        var sat = ddl6.options[ddl6.selectedIndex].value;
              var ddl7 = document.getElementById("<%=DropDownList7.ClientID%>");
        var sun = ddl7.options[ddl7.selectedIndex].value;

            alert(mon + "" + tue + "" + wed + "" + thu + "" + fri + "" + sat + "" + sun+""+id);
            if (id != "") {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'UpdateShift.aspx/Insert',
                    data: "{'mon':'" + mon + "','tue':'" + tue + "','wed':'" + wed + "','thu':'" + thu + "','fri':'" + fri + "','sat':'" + sat + "','sun':'" + sun + "','id':'" + id + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                        if (data.d == "Save") {

                            alert("Saved Successfuly");
                        }
                        else {
                            alert("This Employee Timmings Already Added Please Update not Save");

                        }
                    },
                    Error: function (res) {
                        alert("Error Occure" + res);

                    }
                });
            }
            else {
                alert("Please Specify Employee First");
            }

        }


        function Edit(Val) {

            id = Val;
            $('#hide').show();
           
        }

        function Update() {

             alert(id);

            
              var ddl1 = document.getElementById("<%=DropDownList1.ClientID%>");
        var mon = ddl1.options[ddl1.selectedIndex].value;
              var ddl2 = document.getElementById("<%=DropDownList2.ClientID%>");
        var tue = ddl2.options[ddl2.selectedIndex].value;
              var ddl3 = document.getElementById("<%=DropDownList3.ClientID%>");
        var wed = ddl3.options[ddl3.selectedIndex].value;
              var ddl4 = document.getElementById("<%=DropDownList4.ClientID%>");
        var thu = ddl4.options[ddl4.selectedIndex].value;
              var ddl5 = document.getElementById("<%=DropDownList5.ClientID%>");
        var fri = ddl5.options[ddl5.selectedIndex].value;
              var ddl6 = document.getElementById("<%=DropDownList6.ClientID%>");
        var sat = ddl6.options[ddl6.selectedIndex].value;
              var ddl7 = document.getElementById("<%=DropDownList7.ClientID%>");
        var sun = ddl7.options[ddl7.selectedIndex].value;

           // alert(mon + "" + tue + "" + wed + "" + thu + "" + fri + "" + sat + "" + sun+""+id);
            if (id != "") {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'UpdateShift.aspx/Update',
                    data: "{'mon':'" + mon + "','tue':'" + tue + "','wed':'" + wed + "','thu':'" + thu + "','fri':'" + fri + "','sat':'" + sat + "','sun':'" + sun + "','id':'" + id + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                        if (data.d == "Save") {

                            alert("Update Successfuly");
                             $('#hide').hide();
                        }
                        else {
                            alert("This Employee Timmings Already Added Please Update not Save");

                        }
                    },
                    Error: function (res) {
                        alert("Error Occure" + res);

                    }
                });
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
    
  <style type="text/css">

	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <body>
        <form runat="server">
    <div class="container" style="width:99%;margin-top:-26px;">
          <div class="panel-group" style="width:99%;">
            <div class="panel panel-primary" style="border-color:#0A408A;border:2px;">
                <div class="panel-heading" style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">Add Employee's Shift</h2></div>
                <div class="panel-body">

                    <form class="form-horizontal" method="post">

                        <br />



                        <div class="form-group">
                            <label class="control-label col-sm-2" for="Name">Employee Name:</label>
                            <input type="text" id="id" name="postId" style=" display:none;" />
                            <div class="col-sm-3">
                                <input type="text" class="form-control" id="txtseach" placeholder="Employee Name" required>
                            </div>
                        </div>
                        <br />
                        <div class="table-responsive">
                            <table class="table">
<thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
                                        <th scope="col">#</th>
                                        <th scope="col">Monday</th>
                                        <th scope="col">Tuesday</th>
                                        <th scope="col">Wednesday</th>
                                        <th scope="col">Thursday</th>
                                        <th scope="col">Friday</th>
                                        <th scope="col">Saturday</th>
                                        <th scope="col">Sunday</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th scope="row">Shift</th>
                                        <td>
                                            <asp:DropDownList ID="DropDownList1" runat="server" ></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                           <asp:DropDownList ID="DropDownList3" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList4" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                           <asp:DropDownList ID="DropDownList5" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList6" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList7" runat="server"></asp:DropDownList>
                                        </td>
                                       
                                    </tr>

                                </tbody>
                            </table>
                        </div>

                        <br />
                      
                           <div class=" col-sm-6 ">
                            <button type="button" class="btn btn-primary" onclick="Save();" style="background-color:#0A408A;color:white;">Save</button>
                               </div>
                               <div id="hide">
                                   
                            <button type="button" class="btn btn-primary" onclick="Update();" style="background-color:#0A408A;color:white;">Update</button>
                               </div>
  
                      
                        <br /> 
                        <br />
                        <div class="form-group">
                           
                            <br />
                        <br />
                  
                           
    <table id="datatable" style=" width:100%; height:300px; overflow:scroll;" >
       <thead>
                    <tr>
                        <th>Emp_id</th>
                        <th>Name</th>
                        <th>Designation</th>
                        <th>MOn</th>
                        <th>Tue</th>
                        <th>Wed</th>
                        <th>Thu</th>
                        <th>Fri</th>
                        <th>Sat</th>
                        <th>Sun</th>
                        <th>Status</th>
                        <th>Edit</th>
                    </tr>
                </thead>
    </table>

                        </div>




                    </form>
                </div>
            </div>


        </div>
    </div>



        </form>
</body>
</asp:Content>
