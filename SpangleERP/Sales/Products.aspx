<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="SpangleERP.Sales.Product_Categories" %>
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

            Show();


        });



            function Show() {

         
                    $j('#datatable').DataTable({
                        "aLengthMenu": [[10, 25, 5], [10, 25, 5]],
                        "iDisplayLength": 5,
                           
                        
                        columns: [
                            {
                                'data': 'pid',
                                'sortable': false,
                                
                              'render': function (val) {
                                  return 'FG-'+val;

                                }

                            },
                            { 'data': 'pname' },
                            { 'data': 'unit_in_car' },
                            { 'data': 'price' },

                            { 'data': 'uinm' },
                            { 'data': 'pcat_name' },
                            { 'data': 'weight' },
                             

                            {
                                'data': 'pid',

                                'sortable': false,
                                
                              'render': function (val) {
                                  return '<button type="button" id="vw" value="' + val + '"  onclick="Popup(this.value);"   class="btn btn-primary" style="background-color:#0A408A;">View</button>';

                                }
                              
                            }                   
                    
                            
                                        
                    ],
                bServerSide: true,
                sAjaxSource: 'Sales_Service.asmx/Get_All_products',
                sServerMethod: 'post'
            });

        }
        var pid = 0;
        function Popup(Val) {

             $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Products.aspx/GetSpecific',

                    data: "{'pid':'" + Val + "'}",

                    dataType: "json",
                    async: false,

                 success: function (data) {

                     pid = Val;
                         $('#Pname1').val(data.d.pname);
             $('#UI_Cartons1').val(data.d.unit_in_car);
             $('#price1').val(data.d.price);
             $('#weight1').val(data.d.weight);
                         
                        $j('#EditPopup').modal('show');
                    },
                    Error: function (res) {
                        alert("Error Occure" + res);

                    }
                });

           
        }

        function Update() {

             var pname = $('#Pname1').val();
            var uiCartons = $('#UI_Cartons1').val();
            var price = $('#price1').val();
             var weight = $('#weight1').val();
             var ddl = document.getElementById("<%=DropDownList1.ClientID%>");
            var typeid = ddl.options[ddl.selectedIndex].text;
             var ddl1 = document.getElementById("<%=DropDownList2.ClientID%>");
            var Cat_id = ddl1.options[ddl1.selectedIndex].value;
            if (pname != "" && uiCartons != "" && price != "" && weight != "" &&  pid!=0) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Products.aspx/Update',


                    data: "{'pname':'" + pname + "','uicartons':'" + uiCartons + "','price':'" + price + "','units_in_measure':'" + typeid + "','cat_id':'" + Cat_id + "','weight':'" + weight + "','pid':'" + pid + "'}",


                    dataType: "json",
                    async: false,

                    success: function (data) {

                        $('#Pname1').val("");
                        $('#UI_Cartons1').val("");
                        $('#price1').val("");
                        $('#weight1').val("");

                        pid = 0;
                        alert(data.d);
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

        function AddProducts() {

            var pname = $('#Pname').val();
            var uiCartons = $('#UI_Cartons').val();
            var price = $('#price').val();
             var weight = $('#weight').val();
             var ddl = document.getElementById("<%=DDMeasure.ClientID%>");
            var typeid = ddl.options[ddl.selectedIndex].text;
             var ddl1 = document.getElementById("<%=DDCategory.ClientID%>");
            var Cat_id = ddl1.options[ddl1.selectedIndex].value;
           
           
            if (pname != "" && uiCartons != "" && price != "" && weight != "") {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Products.aspx/AddProducts',

                    data: "{'pname':'" + pname + "','uicartons':'" + uiCartons + "','price':'" + price + "','units_in_measure':'" + typeid + "','cat_id':'" + Cat_id + "','weight':'" + weight + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                         $('#Pname').val("");
             $('#UI_Cartons').val("");
             $('#price').val("");
             $('#weight').val("");
                        alert(data.d);
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

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <form runat="server">
    <div class="container" style="width:100%;margin-top:-26px;">
        
   
     <div class="panel-group" style="width:100%;">
       <div class="panel panel-primary"  style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">Prtoducts</h2>
          <div class="table-responsive">
              <table  class="pull-right">
                  <tr>
                      <td>
                          
         <button type="button" id="In" class="btn btn-primary" style=" font-size:18px;background-color:#0A408A;color:white;" data-toggle="modal" data-target="#MachinePopup" data-backdrop="false" >Add New Product</button>
                 
                              </td><td>
                
              </table>


                </div>
          </div>
         <div class="panel-body">
            
             <!--Table-->          
   <div class="table-responsive"> 
      <div class="table-responsive" style="border-color:#0A408A;border:2px;">
              <table  class="pull-right">
                  <tr>
                      <td>
                  </td>
                      <td>

                  </td></tr>
              </table>


                </div>

       <br />
      <div class="table-responsive"> 
   
       <br />
  <table class="table" id="datatable">
    
    <thead style="color:#0A408A;">
                    <tr>
                        <th>ItemCode</th>
                        <th>Product Name</th>
                        <th>Unit In Cartons</th>
                        <th>Price</th>
                        <th>Unit In Measure</th>
                        <th>Cate_Name</th>
                        <th>Weight</th>
                        <th>Edit</th>

              
                    </tr>
                </thead>
  </table>
  </div>
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
            <h2 style="color:white;">Create   Products</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
      
          <div id="parent" class="form-horizontal">







                        <br />


        <div class="form-group">
            <label class="control-label col-sm-3" for="Pname">Product Name:</label>
            <div class="col-sm-3">
     <input type="text" class="form-control" id="Pname" minlength="4"  placeholder="Product Name" maxlength="22"   onpaste="return false"  required>

            </div>
                <label class="control-label col-sm-3" for="UI_Cartons">Unit In Cartons:</label>
          <div class="col-sm-3">
                   <input type="number"  class="form-control" id="UI_Cartons"   required>
          </div>
                   
                        </div>
             <div class="form-group">
            <label class="control-label col-sm-3" for="price" >Product Price:</label>
            <div class="col-sm-3">
     <input type="number" class="form-control" id="price"  required>

            </div>
                <label class="control-label col-sm-3" for="UI_Measure">Unit In Measure:</label>
          <div class="col-sm-3">
   <asp:DropDownList ID="DDMeasure" runat="server" CssClass="form-control">

       <asp:ListItem>Pcs</asp:ListItem>
       <asp:ListItem>KG</asp:ListItem>
       <asp:ListItem>Ml</asp:ListItem>

   </asp:DropDownList>     

          </div>
                   
                        </div>

             <div class="form-group">
           
              
                  <label class="control-label col-sm-3" for="PCat">Category Name:</label>
          <div class="col-sm-3">
      <asp:DropDownList ID="DDCategory" runat="server" CssClass="form-control"></asp:DropDownList>     

          </div>
              <div class="form-group">
           
              
                  <label class="control-label col-sm-3" for="PCat">Weight:</label>
          <div class="col-sm-3">
<input type="number" class="form-control" id="weight" required  />
          </div>
                  
                   
                        </div>
    
               
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-3">
                            <button type="button" class="btn btn-primary " onclick= "AddProducts();" style="background-color:#0A408A;" id="BtnSave" >Save</button>
            
                         
                                </div>
                        </div>
                 
        
          </div>
 


        </div>
      </div>
    </div>
</div>
        </div>
    

       </div>

<%---End  Popup----%>

            <div class="container">
        <div class="modal fade" id="EditPopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <h2 style="color:white;">Create   Products</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
      
          <div  class="form-horizontal">







                        <br />


        <div class="form-group">
            <label class="control-label col-sm-3" for="Pname">Product Name:</label>
            <div class="col-sm-3">
     <input type="text" class="form-control" id="Pname1" minlength="4"  placeholder="Product Name" maxlength="22"   onpaste="return false"  required>

            </div>
                <label class="control-label col-sm-3" for="UI_Cartons">Unit In Cartons:</label>
          <div class="col-sm-3">
                   <input type="number"  class="form-control" id="UI_Cartons1"   required>
          </div>
                   
                        </div>
             <div class="form-group">
            <label class="control-label col-sm-3" for="price" >Product Price:</label>
            <div class="col-sm-3">
     <input type="number" class="form-control" id="price1"  required>

            </div>
                <label class="control-label col-sm-3" for="UI_Measure">Unit In Measure:</label>
          <div class="col-sm-3">
   <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">

       <asp:ListItem>Pcs</asp:ListItem>
       <asp:ListItem>Bottle</asp:ListItem>
       <asp:ListItem>Box</asp:ListItem>
       <asp:ListItem>Pouch</asp:ListItem>
       <asp:ListItem>Jar</asp:ListItem>
       <asp:ListItem>Sachet</asp:ListItem>

   </asp:DropDownList>     

          </div>
                   
                        </div>

             <div class="form-group">
           
              
                  <label class="control-label col-sm-3" for="PCat">Category Name:</label>
          <div class="col-sm-3">
      <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control"></asp:DropDownList>     

          </div>
              <div class="form-group">
           
              
                  <label class="control-label col-sm-3" for="PCat">Weight:</label>
          <div class="col-sm-3">
<input type="number" class="form-control" id="weight1" required  />
          </div>
                  
                   
                        </div>
    
               
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-3">
                            <button type="button" class="btn btn-primary " onclick= "Update();" style="background-color:#0A408A;"   >Update</button>
            
                         
                                </div>
                        </div>
                 
        
          </div>
 


        </div>
      </div>
    </div>
</div>
        </div>
    

       </div>

            
     

    </form>
</asp:Content>
