<%@ Page Title="" Language="C#" MasterPageFile="~/HR Module/HR_Master.Master" AutoEventWireup="true" CodeBehind="GazatedForm.aspx.cs" Inherits="SpangleERP.HR_Module.GazatedForm" %>
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
                <div class="panel-heading" style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">Gazetted Holidays </h2></div>
                <div class="panel-body">

                    <form class="form-horizontal" method="post" runat="server" >

                        <br />


                        <div class="form-group">
                            <label class="control-label col-sm-2" for="Date">Date:</label>
                            <div class="col-sm-3">
                                
                                 <asp:TextBox ID="Date1" placeholder="Date"  TextMode="Date" DataFormatString="{0:MM/dd/yyyy}" runat="server" CssClass="form-control"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="felid required" ControlToValidate="date1" ValidationGroup="myValidator"  ForeColor="Red"></asp:RequiredFieldValidator>  
                   
                            </div>

                        </div>
                        <br />
                       
                        <br />
                     
                        <div class="form-group">
                            <label class="control-label col-sm-2" for="holiday">On the Account of:</label>
                            <div class="row">
                                <div class="col-sm-3">
                                  
                                     <asp:TextBox ID="dis" placeholder="on the account of"  runat="server" CssClass="form-control"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="felid required" ControlToValidate="dis" ValidationGroup="myValidator"  ForeColor="Red"></asp:RequiredFieldValidator>  
                   
                                </div>
                            </div>
                          
                        </div>
                     
                        <br />
                        <div class="form-group">
                            <div style="padding-left:250px">
                              <asp:Button ID="b1" Text="Submit" runat="server"  ValidationGroup="myValidator" OnClick ="Save" Style="background-color:#0A408A;color:white;" CssClass="btn btn-primary" />
                                   <asp:Button ID="Button1" Text="Update" runat="server" ValidationGroup="myValidator" OnClick="Button1_Click" Style="background-color:#0A408A;color:white;" CssClass="btn btn-primary"  />
                       
                       
                            </div>
                        </div>
                      <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Holi_id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="100%" Height="300px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Holi_id" HeaderText="Holi_id" InsertVisible="False" ReadOnly="True" SortExpression="Holi_id" />
                            <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" SortExpression="Date" />
                            <asp:BoundField DataField="Day" HeaderText="Day" SortExpression="Day" />
                            <asp:BoundField DataField="disc" HeaderText="On The Account of" SortExpression="disc" />
                          <asp:BoundField DataField="defined_by" HeaderText="Updated_By" SortExpression="defined_by" />
                            <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True"  />
                          
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#0A408A" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                    </form>

                </div>

                   


                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBMS %>" SelectCommand="SELECT * FROM [Holidays]" DeleteCommand="DELETE FROM [holiday] WHERE [Holi_id] = @Holi_id" >
                    <DeleteParameters>
                        <asp:Parameter Name="Holi_id" Type="Int32" />
                    </DeleteParameters>
                   
                </asp:SqlDataSource>


</div>
 </div>
    </div>

</body>
</asp:Content>
