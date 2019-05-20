<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="Adjustment.aspx.cs" Inherits="SpangleERP.HR_Module.Adjustment" %>
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






        });

        function Search() {
            var $tbl = $('#datatable');

            var x = document.getElementById("date");
            var dob = x.value.toString();
            var date = new Date(dob);
            var month = date.getMonth()+1;
            alert(month);
             
            var total = dob.substring(0, 7)
            alert(total);
            if (id != "" && dob != "") {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Adjustment.aspx/GetData',

                    data: "{'id':'" + id + "','date':'" + dob + "','month':'" + total + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                         if (data.d.length > 1 ) {
                             

                                $tbl.empty();
                                $tbl.append(' <tr  style="color:#0A408A;style="font-family:Cambria;font-weight:200 bold; font-size:32px;"><th>Name</th><th>Timein</th><th>TimeOut</th><th>Late</th><th>HalfDay</th><th>Date</th><th>Status</th><th>Overtime</th></tr>');

                                for (var i = 0; i < data.d.length; i++) {


                                    $tbl.append('<tr ><td ><label>' + data.d[i].emp_name + '</label></td><td><label >' + data.d[i].timein + '</label></td><td><label>' + data.d[i].time_out + '</label></td><td><label>' + data.d[i].late + '</label></td><td><label>' + data.d[i].half + '</label></td><td><label>' + data.d[i].date + '</label></td><td><label>'+data.d[i].status+'</label></td><td><label>'+data.d[i].Overtime+'</label></td></tr>');
                                }

                                //ye uncomment krna he
                             id = "";
                             $('#txtseach').val("");
                               

                            }
                            else {
                                alert("Dont Have Any Record");

                            }


                    },
                    Error: function (res) {
                        alert("Error Occure" + res);

                    }
                });
            }
            else {
                alert("Please Select Date And Employee");
            }
        }

    </script>
    <style>
     
         ul.ui-autocomplete {
   height:200px;
   overflow:scroll;
}       
.imageclass
{
    width:50px;
    height:50px;
    
    }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 
    <div class="container" style="width:99%;margin-top:-26px;">
        <div class="panel-group" style="width:99%;">
            <div class="panel panel-primary" style="border-color:#0A408A;border:2px;">
                <div class="panel-heading" style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;"> All Employee Adjustment</h2></div>
                <div class="panel-body">

                    <form class="form-horizontal" method="post">
                        <div class="form-group">
                            <label class="control-label col-sm-2" for="Name">Employee Name:</label>
                            <div class="col-sm-3"> <input type="text" class="form-control" id="txtseach" placeholder="Employee Name">
                                <input type="text" id="id" name="postId" style=" display:none;" />
                            </div>
                             <label class="control-label col-sm-2" for="todate">Date:</label>
                            <div class="col-sm-2">
                                <input type="date" class="form-control" id="date"  required>
                            </div>
                        </div>


                        <div class="form-group">
                           
                           
                            <div class="col-sm-12 text-center">
                                <br /><br />
                                <button type="button" onclick="Search()" class="btn" style="background-color:#0A408A;">Search</button>
                            </div >
                        </div>
                        <br />
                       
                            <div class="table-responsive table-striped">
                                <table id="datatable" style=" width:100%; height:300px;">
      
        
    </table>
                            </div>
                        
                        
                        
                        
                        
                 
                      



                    </form>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
