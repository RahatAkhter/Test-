<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="Distributor_Disc.aspx.cs" Inherits="SpangleERP.Sales.Distributor_Disc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
       <script src="../jquery-ui.js"></script>
    <link href="../jquery-ui.css" rel="stylesheet" />
<script type="text/javascript">
     var $j = jQuery.noConflict();
        var id = "";

                
       $j(document).ready(function () {
           


        $j('#txtseach').autocomplete({
            minLength: 1,
            focus: function (event, ui) {
                $(this).val(ui.item.dist_name);

                return false;
            },
            select: function (event, ui) {
                $j(this).val(ui.item.dist_name);
                $j('#id').val(ui.item.Distributor_id.toString());
                id = $j('#id').val();
                
                return false;

            },
            source: function (request, response) {
                $j.ajax({
                    type: 'POST',
                    url: 'Sales_Service.asmx/Get_Profiles',
                    data: { term: request.term },
                    dataType: "json",
                    success: function (data) {
                        response(data);
                    }

                });

            }

        })
        .autocomplete('instance')._renderItem = function (ul, item) {
                            return $j('<li>')
                    
                    .append('<a>' + item.dist_name + '   </a> </li>')
        .appendTo(ul);


            
        }






        });



    function SaveDisc() {

 var ddl1 = document.getElementById("<%=DDCategory.ClientID%>");
            var Cat_id = ddl1.options[ddl1.selectedIndex].value;

          
        var dis = $('#dis').val();
        var mprice = $('#Mprice').val();
        var supplier = $('#Supllier').val();

        var dis1;
        var mprice1, supplier1;
        if (dis == "" || parseInt(dis)<0) {
            dis1 = 0;
        }
        else {
            dis1 = dis;
        }
        if (mprice == "" || parseInt(mprice)<0) {
            mprice1 = 0;
        }
        else {
            mprice1 = mprice;
        }
        if (supplier == "" || parseInt(supplier) < 0) {
            supplier1 = 0;
        }
        else {
            supplier1 = supplier;
        }

        if (id != "") {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: 'Distributor_Disc.aspx/Insert',


                data: "{'pcat_id':'" + Cat_id + "','dis_id':'" + id + "','dis':'" + dis1 + "','mprice':'" + mprice1 + "','supplier':'" + supplier1 + "'}",


                dataType: "json",
                async: false,

                success: function (data) {

                    $('#dis').val("");
                    $('#Mprice').val("");
                    $('#Supllier').val("");
                  

                    pid = 0;
                    alert(data.d);
                },
                Error: function (res) {
                    alert("Error Occure" + res);

                }
            });
        }
        else {
            alert("Please Select Distributor First");
        }
    }
  

</script>
     <style>
     
         ul.ui-autocomplete {
   height:200px;
   overflow:scroll;
}       
         ul.ui-autocomplete {
    z-index: 1100;
}
.imageclass
{
    width:50px;
    height:50px;
    
    }
    </style>
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server" class="form-horizontal">  
    <div class="container" style="width:100%;margin-top:-26px;">
     <div class="panel-group" style="width:100%;">
       <div class="panel panel-primary"  style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">Distributor_Dis%</h2>
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
      
      <div class="table-responsive" style="border-color:white;border:2px;">
          


                </div>

       <br />

           <!-- Modal one-->
    <div class="container">
        <div class="modal fade" id="ProductCategoriesPopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
         
            <h2 style="color:white;">Add Distributor Dis%</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
    


          <div class="form-group">
               <input type="text" id="id" name="postId" style=" display:none;" />
            <label class="control-label col-sm-2 " for="name">Dist Name:</label>
            <div class="col-sm-3">
          <input type="text" class="form-control" id="txtseach" placeholder="Distributor Name" required>
              
            </div>
               <label class="control-label col-sm-2 offset-1" for="category">Category:</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="DDCategory" runat="server" CssClass="form-control"></asp:DropDownList>
              
            </div>
                        </div>
          <div class="form-group">
                 <label class="control-label col-sm-2 " for="dis">Dist Dis%:</label>
            <div class="col-sm-3">
             <input type="number" min="0"  class="form-control" id="dis"  required>
              
            </div>
               <label class="control-label col-sm-2 offset-1" for="Mprice">Market Price:</label>
            <div class="col-sm-3">
             <input type="number" min="0" class="form-control" id="Mprice"  required>
              
            </div>


          </div>
          <div class="form-group">
              <label class="control-label col-sm-2 " for="Supllier">Supplier:</label>
            <div class="col-sm-3">
             <input type="text" min="0" class="form-control" id="Supllier"  required>
              
            </div>
          </div>
       

       
    
                   
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-3">
                            <button type="button" class="btn btn-primary"  id="BtnSave" style="background-color:#0A408A;color:white;" onclick="SaveDisc();">Save</button>
            
                         
                                </div>
                        </div>
            
        
    


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

        <th>Distributor Name</th>
        <th>Category</th>
        <th>Dist_Dis%</th>
        <th>Market Price%</th>
          <th>Supplier</th>
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
          <!-- Modal one-->
    <div class="container">
        <div class="modal fade" id="EditDist_Dis" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
           
            <h2 style="color:white;">Edit Distributor Dis%</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
       <%--  <div class="container">
     <div class="panel-group">
       <div class="panel panel-primary">
      <div class="panel-heading"><h2>Add Employee</h2></div>
      <div class="panel-body">--%>


          <div class="form-group">
                 <label class="control-label col-sm-2 " for="EditSupllier">Supplier:</label>
            <div class="col-sm-3">
             <input type="text" class="form-control" id="EditSupllier"  required>
              
            </div>
               <label class="control-label col-sm-2 offset-1" for="category">Category:</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="EditDDCat" runat="server" CssClass="form-control"></asp:DropDownList>
              
            </div>
                        </div>
          <div class="form-group">
                 <label class="control-label col-sm-2 " for="Editdis">Dist Dis%:</label>
            <div class="col-sm-3">
             <input type="number" class="form-control" id="Editdis"  required>
              
            </div>
               <label class="control-label col-sm-2 offset-1" for="EditMprice">Market Price:</label>
            <div class="col-sm-3">
             <input type="number" class="form-control" id="EditMprice"  required>
              
            </div>


          </div>
      
       

       
    
                   
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-3">
                            <button type="button" class="btn btn-primary"  id="BtnEdit" style="background-color:#0A408A;color:white;" onclick="()">Edit</button>
            
                         
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

         </form>

</asp:Content>
