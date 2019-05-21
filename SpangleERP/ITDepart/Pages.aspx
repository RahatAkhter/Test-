<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="Pages.aspx.cs" Inherits="SpangleERP.ITDepart.Pages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>

    <script type="text/javascript">

        function Save() {
            var pname = $('#Page_Name').val();
            var page_url = $('#path').val();
            
            var Icon = $('#Icon_Name').val();
            alert(pname + " " + page_url + "" + Icon);


            if (pname != "" && page_url != "" && Icon != "") {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Pages.aspx/Insert',
                    data: "{'pname':'" + pname + "','page_url':'" + page_url + "','Icon':'" + Icon + "'}",

                    dataType: "json",
                    async: false,

                    success: function (data) {
                        if (data.d == "Save") {
                            $('#Page_Name').val("");
                            $('#path').val("");
                            $('#Icon_Name').val("");
                            alert("Save Successfully");
                        }
                        else {
                            alert(data.d);
                        }
                    },
                    Error: function (res) {
                        alert("Error Occure" + res);

                    }
                });
            }
            else {
                alert("Please Fil The Form Correctly");
            }




        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width:100%;margin-top:-26px;">
     <div class="panel-group" style="width:100%;">
       <div class="panel panel-primary"  style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">pages</h2>
          <div class="table-responsive table-striped" style="border-color:#0A408A;border:2px;">
              <table  class="pull-right"  style="border-color:#0A408A;border:2px;">
                  <tr>
                      <td>
         <button type="button" class="btn btn-primary" style=" font-size:18px;background-color:#0A408A;color:white;" data-toggle="modal" data-target="#MachinePopup" data-backdrop="false" >Add New</button>
                  </td></tr>
              </table>


                </div>
          </div>
         <div class="panel-body">
            
             <!--Table-->          
   <div class="table-responsive" style="border-color:white;border:2px;"> 
      <div class="table-responsive" style="border-color:white;border:2px;">
              <table  class="pull-right" style="border-color:white;border:2px;">
                  <tr >
                      <td>
<input type="text" placeholder="Search Here By Id Or Date" id="search" class="form-control"/>
                  </td>
                    </tr>
              </table>


                </div>

       <br />
       <div class="table-wrapper-scroll-y my-custom-scrollbar" >
       <table id="tbldistributors" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
        <th>Page_Id</th>
        <th>Page_Name</th>
        <th>Icon</th>
        <th>Attached with</th>
         <th>Date</th>
      
      
        
    
      
      </tr>
    </thead>
    <tbody>
      
    </tbody>
  </table>
 </div>
      <%--scroll bar--%>
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
            <h2 style="color:white;">Add New Records</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
       
  <form class="form-horizontal" method="post" runat="server">

                        <br />


        <div class="form-group">
            <label class="control-label col-sm-2" for="Name">Page URl:</label>
            <div class="col-sm-4">
     <input type="text" class="form-control" id="path"  required>
              
            </div>
                <label class="control-label col-sm-2" for="txt">Icon_Name:</label>
          <div class="col-sm-4">
                   <input type="text" class="form-control" id="Icon_Name"    required>
          </div>
                   
                        </div>
             <div class="form-group">
            <label class="control-label col-sm-2" for="txt1">Page Name:</label>
            <div class="col-sm-4">
     <input type="text" class="form-control" id="Page_Name"   required />

            </div>
               
                   
                        </div>

    
    
                   
                        <div class="form-group">
                            <div class="col-xs-12 text-center">
                            <button type="button" class="btn btn-primary"  id="BtnSave" style="background-color:#0A408A;color:white;" onclick="Save();">Save</button>
            
                         
                                </div>
                        </div>
                    </form>
    


        </div>
      </div>
    </div>
</div>
        </div>
       <%--        End popup--%>


</asp:Content>
