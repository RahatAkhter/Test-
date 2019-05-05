<%@ Page Title="" Language="C#" MasterPageFile="~/HR Module/HR_Master.Master" AutoEventWireup="true" CodeBehind="ShiftDetail.aspx.cs" Inherits="SpangleERP.HR_Module.ShiftDetail" %>
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
    
    <script>

    </script>
    
  <style type="text/css">

	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <body>
     <div class="container" style="width:99%;margin-top:-26px;">
        <div class="panel-group" style="width:99%;">
            <div class="panel panel-primary" style="border-color:#0A408A;border:2px;">
                <div class="panel-heading" style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">Add New Shift </h2></div>
                <div class="panel-body">

                    <form class="form-horizontal" method="post" runat="server">

                        <div class="form-group">
                            
                            <label class="control-label col-sm-2" for="shift">Shift Name:</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="shiftname" placeholder="shiftname"  runat="server" CssClass="form-control"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="felid required" ControlToValidate="shiftname" ValidationGroup="myValidator"  ForeColor="Red"></asp:RequiredFieldValidator>  
                   
                              
                            </div>
                        
                        </div>
                        <br />
                        <div class="form-group">
                            <label class="control-label col-sm-2" for="shift">Start Time:</label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="s_time" placeholder="Start time" TextMode="Time" runat="server" CssClass="form-control"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="felid required" ControlToValidate="s_time"  ValidationGroup="myValidator" ForeColor="Red"></asp:RequiredFieldValidator>  
                      </div>
                            </div>
                                <br />
                        <div class="form-group">
                            <label class="control-label col-sm-2" for="shift">End Time:</label>
                            <div class="col-sm-3">
                              <asp:TextBox ID="Endtime" placeholder="End time" TextMode="Time" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="felid required" ControlToValidate="Endtime" ValidationGroup="myValidator" ForeColor="Red"></asp:RequiredFieldValidator>  
                        </div>
                            
                            </div>
                        
                        <div class="form-group">
                            <div style="padding-left:250px">
                               <asp:Button ID="b1" Text="Submit" runat="server"  ValidationGroup="myValidator"  OnClick ="Save" Style="background-color:#0A408A;color:white;" CssClass="btn btn-primary"/>
                            </div>
                        </div>
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="shift_id" DataSourceID="SqlDataSource1" Height="186px" Width="100%">
         <Columns>
             <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
             <asp:BoundField DataField="shift_id" HeaderText="SHIFT ID" InsertVisible="False" ReadOnly="True" SortExpression="shift_id" />
             <asp:BoundField DataField="shift_name" HeaderText="SHIFT NAME"    SortExpression="shift_name" />
             <asp:BoundField DataField="s_time"     HeaderText ="START TIME"  />  
             <asp:BoundField DataField="e_time" HeaderText="END TIME" SortExpression="e_time" />
           
         </Columns>
         <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
         <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
         <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
         <RowStyle BackColor="White" ForeColor="#003399" />
         <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
         <SortedAscendingCellStyle BackColor="#EDF6F6" />
         <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
         <SortedDescendingCellStyle BackColor="#D6DFDF" />
         <SortedDescendingHeaderStyle BackColor="#002876" />
     </asp:GridView>
              </form>

                </div>
            </div>


        </div> 
    </div>


       
    
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBMS %>" DeleteCommand="DELETE FROM [Shifts] WHERE [shift_id] = @shift_id" InsertCommand="INSERT INTO [Shifts] ([shift_name], [s_time], [e_time]) VALUES (@shift_name, @s_time, @e_time)" SelectCommand="SELECT * FROM [Shifts]" UpdateCommand="UPDATE [Shifts] SET [shift_name] = @shift_name, [s_time] = @s_time, [e_time] = @e_time WHERE [shift_id] = @shift_id">
  
         <DeleteParameters>
             <asp:Parameter Name="shift_id" Type="Int32" />
         </DeleteParameters>
         
         <UpdateParameters>
             

             <asp:Parameter  Name="shift_name" Type="String"   />
        <asp:Parameter Name="s_time" DbType="Time"    />
             <asp:Parameter Name="e_time" DbType="Time"  />
              
           
             <asp:Parameter Name="shift_id" Type="Int32" />
         </UpdateParameters>
         
     </asp:SqlDataSource>


     
   
       
</body>
</asp:Content>
