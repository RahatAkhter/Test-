<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="Salary_Package.aspx.cs" Inherits="SpangleERP.HR_Module.Salary_Package" %>
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

        var pid = "";
        function Edit(Val) {
            pid = Val;
            

             $.ajax({
                type: "POST",
                contentType: "application/json",
                url: 'Salary_Package.aspx/GetEditData',
                data: "{'pid':'" + pid + "'}",
                datatype: "json",
                 success: function (data) {


                    for (var i = 0; i < data.d.length; i++) {
                       
                       
                        $('#p_Name1').val(data.d[i].p_name);
                        $('#basic1').val(data.d[i].basic);
                        $('#mobile1').val(data.d[i].mobile);
            $('#petrol1').val(data.d[i].petrol);
            $('#Lunch1').val(data.d[i].lunch);
            $('#HouseRent1').val(data.d[i].rent);
            $('#Medical1').val(data.d[i].medical);
            $('#motor_car1').val(data.d[i].car);
            $('#Utility1').val(data.d[i].Utility);
            $('#Driver_Fuel1').val(data.d[i].DriverFuel);
                        $('#Otime1').val(data.d[i].OTime);
                       $('#bonus1').val(data.d[i].bonus);

                    }
                    
                },
                error: function (err) {
                    console.log(err);
                }
            });



        }
        


        function Update() {


            var basic = 0, mobile = 0, petrol = 0, Lunch = 0, HouseRent = 0, Medical = 0, motor_car = 0, Otime = 0,Utility=0,driverFuel=0,bonus=0;
            var pname = $('#p_Name1').val();
            basic += $('#basic1').val();
             mobile +=$('#mobile1').val();
             petrol += $('#petrol1').val();
             Lunch += $('#Lunch1').val();
             HouseRent += $('#HouseRent1').val();
             Medical += $('#Medical1').val();
            motor_car += $('#motor_car1').val();
            Otime += $('#Otime1').val();
            Utility += $('#Utility1').val();
               var bonus = $('#bonus1').val();
            driverFuel += $('#Driver_Fuel1').val();
            if (pname != "" && basic != ""  && pid!="") {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Salary_Package.aspx/Update',

                    //  data: "{'fname':'" + fname.toString() + "','lname':'" + lanme.toString() + "','doj':'" + doj.toString() + "','dob':'" + dob.toString() + "','desig':'" + desig.toString() + "','father':'" + father.toString() + "','lno':'" + lno.toString() + "','nic':'" + nic.toString() + "','mobile':'" + mobile.toString() + "','gender':'" + gender.toString() + "','cur':'" + curaddress.toString() + "','pur':'" + puraddress.toString() + "','exp':'" + exp.toString() + "','bnk':'" + bnk.toString() + "','emr':'" + emr_numb.toString() + "','pid':'" + pack_id.toString() + "','type_id':'" + typeid.toString() + "','edu_id':'" + edu_id.toString() + "','dep_id':'" + dep_id.toString() + "'}",
                    data: "{'pname':'" + pname.toString() + "','basic':'" + basic.toString() + "','mobile':'" + mobile + "','petrol':'" + petrol + "','Lunch':'" + Lunch.toString() + "','HouseRent':'" + HouseRent.toString() + "','Medical':'" + Medical.toString() + "','motor_car':'" + motor_car.toString() + "','pid':'" + pid + "','Ot':'" + Otime + "','utility':'" + Utility + "','driverFuel':'" + driverFuel + "','bonus':'" + bonus + "'}",

                    //   data: "{}",                  
                    dataType: "json",
                    async: false,

                    success: function (data) {
                        if (data.d == "Update") {
                          
                        $('#p_Name1').val("");
                        $('#basic1').val("");
                        $('#mobile1').val("");
            $('#petrol1').val("");
            $('#Lunch1').val("");
            $('#HouseRent1').val("");
            $('#Medical1').val("");
                            $('#motor_car1').val("");
                                 $('#Utility1').val("");
            $('#Driver_Fuel1').val("");
                           $('#Otime1').val("");
                            $('#bonus1').val("");
                            pid = "";
                            
                        }
                        else {
                            alert("Some thing went wromg" + data.d);

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

        function Insert() {
            var mobile = 0, mobile = 0, petrol = 0, Lunch = 0, HouseRent = 0, Medical = 0, motor_car = 0,Otime=0,Bonus=0;
            var pname = $('#p_Name').val();
            var basic = $('#basic').val();
            var bonus = $('#bonus').val();

           mobile +=$('#mobile').val();
             petrol += $('#petrol').val();
             Lunch += $('#Lunch').val();
             HouseRent += $('#HouseRent').val();
             Medical += $('#Medical').val();
             motor_car += $('#motor_car').val();
            Otime = $('#Otime').val();
               var utility = 0;
            var driverfuel = 0;
            utility += $('#Utility').val();
            driverfuel += $('#Driver_Fuel').val();
            alert(mobile + " " + utility);
            if (pname != "" && basic != "" ) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Salary_Package.aspx/Insert',

                    data: "{'pname':'" + pname.toString() + "','basic':'" + basic.toString() + "','mobile':'" + mobile + "','petrol':'" + petrol + "','Lunch':'" + Lunch.toString() + "','HouseRent':'" + HouseRent.toString() + "','Medical':'" + Medical.toString() + "','motor_car':'" + motor_car.toString() + "','utility':'" + utility + "','df':'" + driverfuel + "','Ot':'" + Otime + "','bonus':'" + bonus + "'}",

                                   
                    dataType: "json",
                    async: false,

                    success: function (data) {
                        if (data.d == "Save") {
                            alert("ho gya " + data.d);
                            $('#bonus').val("");
                              $('#p_Name').val("");
                        $('#basic').val("");
                        $('#mobile').val("");
            $('#petrol').val("");
            $('#Lunch').val("");
            $('#HouseRent').val("");
            $('#Medical').val("");
                            $('#motor_car').val("");
                            $('#msg').val("Record Successfully Saved");

        
                            
                        }
                        else {
                            alert("Some thing went wromg" + data.d);

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
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
 <body>

        <!--Table-->
        <!--start Table Panel-->
        <div class="container" style="margin-left:-25px;">
     <div class="container" style="width:99%;margin-top:-26px;">
     <div class="panel-group" style="width:99%;">
       <div class="panel panel-primary"  style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">Salary Packages</h2><button type="button" class="btn btn-primary pull-right" style="margin-top:-35px;font-size:18px;background-color:#0A408A;color:white;" data-toggle="modal" data-target="#SalaryPackage" data-backdrop="false" >Add Salary Packages</button></div>
         <div class="panel-body" style=" height:600px;">
             <div style=" width:100%; height:500px;" class=" col-sm-12">
             
                 <div class=" table-responsive" style=" width:100%;">
             <asp:GridView ID="GridView1"  runat="server" AllowPaging="True" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="pack_id" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical">
                 <AlternatingRowStyle BackColor="#CCCCCC" />
                 <Columns>
                     <asp:BoundField DataField="pack_id" HeaderText="pack_id" InsertVisible="False" ReadOnly="True" SortExpression="pack_id" />
                     <asp:BoundField DataField="basic_salary" HeaderText="basic_salary" SortExpression="basic_salary" />
                     <asp:BoundField DataField="petrol" HeaderText="petrol" SortExpression="petrol" />
                     <asp:BoundField DataField="mobile" HeaderText="mobile" SortExpression="mobile" />
                     <asp:BoundField DataField="lunch" HeaderText="lunch" SortExpression="lunch" />
                     <asp:BoundField DataField="motor_car" HeaderText="motor_car" SortExpression="motor_car" />
                     <asp:BoundField DataField="medical" HeaderText="medical" SortExpression="medical" />
                     <asp:BoundField DataField="house_rent" HeaderText="house_rent" SortExpression="house_rent" />
                     <asp:BoundField DataField="total" HeaderText="total" SortExpression="total" />
                     <asp:BoundField DataField="p_name" HeaderText="p_name" SortExpression="p_name" />
                     <asp:BoundField DataField="Utility" HeaderText="Utility" SortExpression="Utility" />
                     <asp:BoundField DataField="DriverFuel" HeaderText="DriverFuel" SortExpression="DriverFuel" />
                 

                      <asp:TemplateField ShowHeader="true" HeaderText="Edit">
            <ItemTemplate >
                <button  type="button" class=" btn btn-primary" data-toggle="modal" data-target="#SalaryPackageUpdate" value="<%# Eval("pack_id") %>"   onclick="Edit(this.value)" style="background-color:#0A408A;color:white;" >Edit</button>
            </ItemTemplate>
        </asp:TemplateField>                           
               
                 </Columns>
                 <FooterStyle BackColor="#CCCCCC" />
                 <HeaderStyle BackColor="#0A408A" Font-Bold="True" ForeColor="White" />
                 <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                 <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                 <SortedAscendingCellStyle BackColor="#F1F1F1" />
                 <SortedAscendingHeaderStyle BackColor="#808080" />
                 <SortedDescendingCellStyle BackColor="#CAC9C9" />
                 <SortedDescendingHeaderStyle BackColor="#383838" />
             </asp:GridView>
                     </div>
             <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBMS %>" SelectCommand="SELECT * FROM [Packages]"></asp:SqlDataSource>
        </div>
</div>
  </div>
</div>
     </div>
    
</div>
     
         <!--model-->
           <div class="container">
        <div class="modal fade" id="SalaryPackage" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#006699">
            <h2 style="color:white;">Salary Package</h2>
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
                                <label class="control-label col-xs-3" for="pak_Name">Package Name:</label>
                                <div class="col-xs-3">
                                    <input type="text" class="form-control" id="p_Name"  placeholder="Pakage For" required>
                                </div>
                                     <label class="control-label col-xs-3"  for="Reason">Basic Salary:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="basic" placeholder="Basic Saalry" required>
                                </div>
                            </div>


                            <div class="form-group">
                                 <label class="control-label col-xs-3" for="Mobile">Mobile Expense:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="mobile" placeholder="Mobile Expense" required>
                                </div>
                                <label class="control-label col-xs-3" for="Petrol">Petrol:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="petrol" placeholder="Petrol" required>
                                </div>
                            </div>
                  
                            <div class="form-group">
                             <label class="control-label col-xs-3" for="Lunch">Lunch:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="Lunch" placeholder="Lunch" required>
                                </div>
                                <label class="control-label col-xs-3" for="HouseRent">House Rent:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="HouseRent" placeholder="House Rent" required>
                                </div>
                            </div>

              <div class="form-group">
                             <label class="control-label col-xs-3" for="Medical">Medical:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="Medical" placeholder="Medical" required>
                                </div>
                                <label class="control-label col-xs-3" for="Convance">Convance:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="motor_car" placeholder="Car" required>
                                </div>
                            </div>
           <div class="form-group">
                             <label class="control-label col-xs-3" for="Medical">Utility:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="Utility" placeholder="Utility" required>
                                </div>
                                <label class="control-label col-xs-3" for="Convance">Driver Fuel:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="Driver_Fuel" placeholder="DriverFuel" required>
                                </div>
                            </div>
      <div class="form-group">
                             <label class="control-label col-xs-3" for="Medical">Overtime:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="Otime" placeholder="OverTime" required>
                                </div>
                               <label class="control-label col-xs-3" for="Medical">Bonus:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="bonus" placeholder="Bonus" required>
                                </div>
                            </div>
                       
              <div class="form-group">
                             
                                <div class="col-xs-12 text-center text-danger">         
                                    <span id="msg"></span>
                                </div>
                             </div>
                       
                          
                            <div class="table-responsive">
                                <table>
                                    <tr>
                                <td>
                              <button type="button" class="btn btn-primary" onclick="Insert();">Save</button>
                              </td>
                                     
                                    
                                        </tr>
                                    </table>
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
     <!--model-->
           <div class="container">
        <div class="modal fade" id="SalaryPackageUpdate" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#006699">
            <h2 style="color:white;">Salary Package</h2>
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
                                <label class="control-label col-xs-3" for="pak_Name">Package Name:</label>
                                <div class="col-xs-3">
                                    <input type="text" class="form-control" id="p_Name1"  placeholder="Pakage For" required>
                                </div>
                                     <label class="control-label col-xs-3"  for="Reason">Basic Salary:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="basic1" placeholder="Basic Saalry" required>
                                </div>
                            </div>


                            <div class="form-group">
                                 <label class="control-label col-xs-3" for="Mobile">Mobile Expense:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="mobile1" placeholder="Mobile Expense" required>
                                </div>
                                <label class="control-label col-xs-3" for="Petrol">Petrol:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="petrol1" placeholder="Petrol" required>
                                </div>
                            </div>
                  
                            <div class="form-group">
                             <label class="control-label col-xs-3" for="Lunch">Lunch:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="Lunch1" placeholder="Lunch" required>
                                </div>
                                <label class="control-label col-xs-3" for="HouseRent">House Rent:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="HouseRent1" placeholder="House Rent" required>
                                </div>
                            </div>

              <div class="form-group">
                             <label class="control-label col-xs-3" for="Medical">Medical:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="Medical1" placeholder="Medical" required>
                                </div>
                                <label class="control-label col-xs-3" for="Convance">Convance:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="motor_car1" placeholder="Car" required>
                                </div>
                            </div>
           <div class="form-group">
                             <label class="control-label col-xs-3" for="Medical">Utility:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="Utility1" placeholder="Utility" required>
                                </div>
                                <label class="control-label col-xs-3" for="Convance">Driver Fuel:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="Driver_Fuel1" placeholder="DriverFuel" required>
                                </div>
                            </div>
      <div class="form-group">
                             <label class="control-label col-xs-3" for="Medical">Overtime:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="Otime1" placeholder="OverTime" required>
                                </div>
                               <label class="control-label col-xs-3" for="Medical">Bonus:</label>
                                <div class="col-xs-3">
                                    <input type="number" class="form-control" value="0" id="bonus1" placeholder="Bonus" required>
                                </div>
                            </div>
                       
              <div class="form-group">
                             
                                <div class="col-xs-12 text-center text-danger">         
                                    <span id="msg1"></span>
                                </div>
                             </div>
                       
                          
                            <div class="table-responsive">
                                <table>
                                    <tr>
                              
                                     <td>
                                    <button type="button" class="btn btn-primary" onclick="Update();">Edit</button>
                                </td>
                                    
                                        </tr>
                                    </table>
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
</body>
    </form>
</asp:Content>
