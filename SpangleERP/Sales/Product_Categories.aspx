<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="Product_Categories.aspx.cs" Inherits="SpangleERP.Sales.Product_Categories1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
     <link href="../Content/css/style.css" rel="stylesheet" />
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />


    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/bootstrap.js"></script>
    <script src="../Content/js/jquery-2.1.1.min.js"></script>

   

    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" rel="stylesheet" />
<script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
      $(document).ready(function () { 
       $.ajax({
            
                url: 'Product_Categories.aspx/GetProductCatDetail', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
                method: 'post',
                data: "{}",
    
                success: function (data) {  
             
                    var productCategoriesTable = $('#tblproductcategories tbody');  
                   
                    productCategoriesTable.empty(); 
                  
                    for (var i = 0; i < data.d.length; i++) {

                        productCategoriesTable.append('<tr ><td class="control-label" style="text-align:left;display:none;" >' + data.d[i].procat_id + '</td><td class="control-label" >' + data.d[i].procat_name + '</td><td><button type="button" value="' + data.d[i].procat_id + '" data-toggle="modal" data-target="#EditPopup2"    onclick="GetProCatIds(this.value,' + i + ')" class="btn" style="background-color:#0A408A;" id="btn3' + i + '">Edit</button></td></tr>')
                        
                    }

           
                    },  
                error: function (err) {  
                    alert(err);  
                }  
             });
    });


                   function GetProCatIds(Val,i) {
               

             $.ajax({
                     type: "Post",
                 url: "Product_Categories.aspx/GetProCatId",
                 data: "{'proid':'" + Val + "'}",

                     contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 async: false,
                 success: function (data) {
                         $('#txtprocatid').text(data.d);
                    
                     },

                     error: function (err) {
                         alert("SomeThing Is Wrong....!");
                     }
                 })
          }

     function EditProductCategories() {
        
       
    

         var getproCats =  $('#txtprocatid').text();
         var editname = $("#editname").val();
   
              $.ajax({
                url: 'Product_Categories.aspx/EditProductCategories',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                method: 'post',

                  data: "{'editedname':'" + editname + "','editid':'" + getproCats + "'}",

                success: function (data) {
                     $('#editname').val("");
                     window.location = "Product_Categories.aspx";
                    alert(data.d);
           
                 
                },
                error: function (err) {
                    alert(err);
                }
            });

    }

    function EditValidate() {
        var editname = $("#editname").val();
        if (editname != "") {

            EditProductCategories();
        } else {
            alert("Fill Name..!")
        }

    }


    function SaveProductCategoriees() {
        var name = $('#name').val();
          $.ajax({
                url: 'Product_Categories.aspx/Save',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                method: 'post',

                data: "{'Name':'" + name + "'}",

                success: function (data) {

                    alert(data.d);
                    
                },
                error: function (err) {
                    alert(err);
                }
            });

    } 

  

</script>
    <style type="text/css">
      .my-custom-scrollbar {
position: relative;
height: 300px;
overflow:auto;
}
.table-wrapper-scroll-y {
display: block;
}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container" style="width:100%;margin-top:-26px;">
     <div class="panel-group" style="width:100%;">
       <div class="panel panel-primary"  style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">Product Categories</h2>
          <div class="table-responsive table-striped" style="border-color:#0A408A;border:2px;">
              <table  class="pull-right"  style="border-color:#0A408A;border:2px;">
                  <tr>
                      <td>
         <button type="button" class="btn btn-primary" style=" font-size:18px;background-color:#0A408A;color:white;" data-toggle="modal" data-target="#ProductCategoriesPopup" data-backdrop="false" >Add Product Categories</button>
                  </td></tr>
              </table>


                </div>
          </div>
         <div class="panel-body">
            
             <!--Table-->          
   <div class="table-responsive" style="border-color:white;border:2px;"> 
       <form runat="server">
      <div class="table-responsive" style="border-color:white;border:2px;">
          


                </div>
           </form>
       <br />

           <!-- Modal one-->
    <div class="container">
        <div class="modal fade" id="ProductCategoriesPopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
          
            <h2 style="color:white;">Add ProductCategories</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
       <%--  <div class="container">
     <div class="panel-group">
       <div class="panel panel-primary">
      <div class="panel-heading"><h2>Add Employee</h2></div>
      <div class="panel-body">--%>


          <div class="form-group">
            <label class="control-label col-sm-2 offset-1" for="name">Category Name:</label>
            <div class="col-sm-3">
          <input type="text" class="form-control" id="name"  required>
              
            </div>
        
                   
                        </div>
       

       
    
                   
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-3">
                            <button type="button" class="btn btn-primary"  id="BtnSave" style="background-color:#0A408A;color:white;" onclick="SaveProductCategoriees()">Save</button>
            
                         
                                </div>
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
       <%--        End popup--%>
       <div class="table-wrapper-scroll-y my-custom-scrollbar" >
       <table id="tblproductcategories" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>

        <th>ProductCategories_Name</th>
        <th>Edit</th>

      
  
      
        
    
      
      </tr>
    </thead>
    <tbody>
      
    </tbody>
  </table>
 </div>
      <%--scroll bar--%>
           <%--</div>--%>

</div>
  </div>
</div>
     </div>
                </div>



                                                                 <!-- Edit One-->
    <div class="container">
        <div class="modal fade" id="EditPopup2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <label id="txtprocatid" style="display:none;"></label>
            <table class="table-responsive-lg">
                <tr>
                    <td class="col-sm-4"><h2 style="color:white;">Edit Record </h2> </td>

             </tr>
                    </table>
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >



               
                    <div class="form-horizontal">
                                 

                                    <br />


                   
                                              <div class="form-group">
                         <label class="control-label col-sm-3" for="editname">Name:</label>
                                        <div class="col-sm-3">
                                            <input type="text" id="editname" class="form-control" " />
                                            
                                        </div>
                                                               <div class="col-sm-3 offset-1">
                                            <button type="button" class="btn btn-primary"  style="background-color: #0A408A;color:white;" onclick="EditValidate()">Edit Record</button>


                                  
                                    </div>
                                                  </div>
                                           
                               
                        


                                
                           
                        
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

<%---End  Popup----%>
</asp:Content>
