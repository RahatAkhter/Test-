<%@ Page Title="" Language="C#" MasterPageFile="~/Ddd/CheckMaster.Master" AutoEventWireup="true" CodeBehind="GRN.aspx.cs" Inherits="SpangleERP.WareHouse.GRN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
      <link href="../Content/css/style.css" rel="stylesheet" />
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />


    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/bootstrap.js"></script>
    <script src="../Content/js/jquery-2.1.1.min.js"></script>
   

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
       <link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" rel="stylesheet" />
<script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript"> 
        var count;
        var $j = jQuery.noConflict();
     $j(document).ready(function () {  


         var gate_id;
                    $j('#datatable').DataTable({
                        "aLengthMenu": [[10, 25, 5], [10, 25, 5]],
                        "iDisplayLength": 5,
                           
                        
                        columns: [

                            
                            { 'data': 'G_Date' },
                            {
                                'data': 'GateIn_Id',

                                'render': function (val) {
                                    gate_id = val;
                                    return val;
                                }


                            },
                              

                            {
                                'data': 'G_Status',

                                'sortable': false,

                                'render': function (webSite) {
                                    if (webSite == 0)
                                        return '<button type="button" id="vw" value="' + gate_id + '"  data-toggle="modal" data-target="#EditPopup" onclick="CallEditPopup(this.value);"    class="btn btn-primary" style="background-color:#0A408A;">Pending</button>';


                                    else
                                        return '<button type="button" id="vw" value="' + gate_id + '" data-toggle="modal" data-target="#Popup"  onclick="Popup(this.value);"    class="btn btn-primary" style="background-color:#0A408A;">View</button>';


                                }


                            }
                              
                    ],
                bServerSide: true,
                sAjaxSource: 'InventoryService.asmx/Get_All_GRN',
                sServerMethod: 'post'
            });

            //$.ajax({  
            //    url: 'GRN.aspx/GateItems', 
            //    contentType: "application/json; charset=utf-8", 
            //    dataType: "json",  
            //    method: 'post',
            //    data: "{}",
            //    success: function (data) {  
                 
            //        var employeeTable = $('#tblEmployee tbody');  
            //        employeeTable.empty();  
            //        for (var i = 0; i < data.d.length; i++) {
            //            if (data.d[i].G_Status == 0) {
            //                  employeeTable.append('<tr ><td class="control_label">' + data.d[i].G_Date + '</td><td><button type="button" id="pen"  value="' + data.d[i].GateIn_Id + '" data-toggle="modal" data-target="#EditPopup"  onclick="CallEditPopup(this.value,' + i + ')" class="btn btn-sucess" >Pending</button></td></tr>');
            //            } else {
            //                employeeTable.append('<tr ><td class="control_label">' + data.d[i].G_Date + '</td><td><button type="button" id="vw"  value="' + data.d[i].GateIn_Id + '" data-toggle="modal" data-target="#Popup"  onclick="Popup(this.value,' + i + ')" class="btn btn-primary" style="background-color:#0A408A;">View</button></td></tr>')
            //            }
            //        }
            //        },  
            //    error: function (err) {  
            //        alert(err);  
            //    }  
            //});  
        });

        function CallEditPopup(Val) {


            alert(Val);
          
  $.ajax({  
                url: 'GRN.aspx/Gatepassitem', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
      method: 'post',
      data: "{'value':'" + Val + "'}",
      success: function (data) {
          $('#ok').text(data.d[0].GateIn_Id);
         
                    var employeeTable = $('#tblEmployee1 tbody');  
          employeeTable.empty();
          count = data.d.length;
                    for (var i = 0; i < data.d.length; i++) {

                           employeeTable.append('<tr ><td class="control-label" style="display:none;" >' + data.d[i].ItemIn_Id + '</td><td class="control_label">' + data.d[i].I_Quantity + '</td><td class="control_label" style="display:none;">' + data.d[i].ItemsId + '</td><td class="control_label">' + data.d[i].Itemsname + '</td><td><Input type="date"  id="mfg' + i + '" class="form-control" /></td ><td><Input type="date"  id="exp' + i + '" class="form-control" /></td ><td><Input type="text"  id= "btch' + i + '" class="form-control" /></td >')
          }

                    },  
                error: function (err) {  
                    alert(err);  
                }  
            });

            

        }



  function Popup(Val) {
           
      alert(Val);
  $.ajax({  
                url: 'GRN.aspx/approve', 
                contentType: "application/json; charset=utf-8", 
                dataType: "json",  
      method: 'post',
      data: "{'value':'" + Val + "'}",
      success: function (data) {
          $('#ok').text(data.d[0].GateIn_Id);
         
                    var employeeTable = $('#tblEmployee2 tbody');  
          employeeTable.empty();
          count = data.d.length;
                    for (var i = 0; i < data.d.length; i++) {

                                      employeeTable.append('<tr ><td class="control_label" >' + data.d[i].Itemsname + '</td><td class="control_label">' + data.d[i].I_Quantity + '</td></tr>')
          }

                    },  
                error: function (err) {  
                    alert(err);  
                }  
            });

            

        }


          function SearchRecords() {
      var input, filter, table, tr, td, i;
      input = document.getElementById("search");
      filter = input.value.toUpperCase();
      table = document.getElementById("tblEmployee");
      tr = table.getElementsByTagName("tr");
      for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
          td1 = tr[i].getElementsByTagName("td")[2];
        
        if (td+td1) {
          if ((td.innerHTML.toUpperCase().indexOf(filter)+td1.innerHTML.toUpperCase().indexOf(filter)) > -2) {
            tr[i].style.display = "";
          } else {
            tr[i].style.display = "none";
          }
        }       
      }
        }


       
              function validate()  
              {
                   var flag=true;
                  for (var i = 0; i < count; i++) {

                      var check = ($('#btch' + i).val());
                      var checkmfg = ($('#mfg' + i).val());
                           var checkexp = ($('#exp' + i).val());
                      if (check == "" || check == null || checkmfg > checkexp ) {
                          flag = false;
                         
                          
                      }
                     
                  }
                  if (flag == false) {
                      alert("Please Fill The Form Correctly and Also Check Exp or Mfg date");
                  }
                  else if (flag == true) {
                      accept();
                  }
      
     }

        function accept() {
        
     
                 var getid = document.getElementById('ok');

           
            var txtid = getid.innerText;
             var ddl = document.getElementById("<%=wr.ClientID%>");
                               var wh = ddl.options[ddl.selectedIndex].value;

         
         var n = $("#tblEmployee1").find("tr").length;

             for (var i = 1; i < n; i++) {
              
                 var id = $("#tblEmployee1").find("tr").eq(i).find("td").eq(0).text();
              
                
                 var mfg =  $("#tblEmployee1").find("tr").eq(i).find('td:eq(4) input[type="date"]').val();
                 var exp = $("#tblEmployee1").find("tr").eq(i).find('td:eq(5) input[type="date"]').val(); 
                  var btc =  $("#tblEmployee1").find("tr").eq(i).find('td:eq(6) input[type="text"]').val(); 
               
                 

                     $.ajax({
                        type: "Post",
                         url: "GRN.aspx/Insert",
                         data: "{'Id':" + id + ",'mfg':'" + mfg + "','exp':'" + exp + "','btc':'" + btc + "','value':'" + txtid + "','ware':'" + wh + "'}",

                      contentType: "application/json; charset=utf-8",
                        dataType: "json",
                         success: function (data) {
                             alert(data.d);

                             window.location = "GRN.aspx";
                           
                         },
                        
            
                         error: function (err) {
                            alert(err);

                         }
                     })

                 }

            
        
     }

       


         $(document).ready(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //url is the path of our web method (Page name/function name)
                url: "GRN.aspx/IssueBy",
                data: "{}",
                dataType: "json",
                //called on jquery ajax call success
                success: function (result) {
                    var ddl = document.getElementById("<%=wr.ClientID%>");
                               var siteems = ddl.options[ddl.selectedIndex].value;

                },
                //called on jquery ajax call failure
                error: function ajaxError(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        });



        


         
            </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
   <form runat="server">
          
        
     <div class="container" style="width:100%;margin-top:-26px;">
     <div class="panel-group" style="width:100%;">
       <div class="panel panel-primary"  style="border-color:#0A408A;border:2px;">
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">GRN</h2>
       
          </div>
             <br />
          
          
          
             <div class="panel-body">
             
                 <!--Table-->          
 <div class="table-responsive"> 
      <div class="table-responsive" style="border-color:#0A408A;border:2px;" >
              <table  class="pull-right">
                  <tr>
                      <td>
<input type="text" placeholder="Search Here By Id Or Status" id="search" class="form-control fas fa-search" style="border-radius:6px;font-family:Cambria;font:bold;height:30px;" onkeyup="SearchRecords()" />
                  </td><td>
   
                  </td></tr>
              </table>


                </div>

       <br />
      
     <table class="table" id="datatable">
    
    <thead>
                    <tr>
                        <th>Date</th>
                        <th>Gate Id</th>
                        <th>View</th>
                        
                        
                    </tr>
                </thead>
  </table>

  </div>
</div>


    <!-- Modal one-->

    <div class="container" style="width:100%">
        <div class="modal fade" id="MachinePopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#006699">
            <h2 style="color:white;">GRN</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body " >
   <br />
     
          <div class="form-horizontal">
                  
        
          </div>
        
           
         
         
       
        <br />
       

              
          </div>
        
          </div>
       
      </div>
</div>
        </div>
         
               
  </div>
         </div>
         </div>


                <div class="container" style="width:90%">
        <div class="modal fade" id="EditPopup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true" >
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <h2 style="color:white;">View Gatepass Items</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
    
   <div class="panel-body">

       <div class="form-horizontal">
         <label class="control-label col-sm-3" for="Chk_By">WareHouse:</label>
          <div class="col-sm-3">
              <asp:DropDownList ID="wr" runat="server" CssClass="form-control"></asp:DropDownList>
       
</div>
           </div>
           <div class="table-responsive"> 
      <div class="table-responsive">
              <table  class="pull-right">
                  <tr>
                      <td>
               </td><td>
     
                  </td></tr>
              </table>


                </div>

       <br />
               <label id="ok" class="form-control" hidden></label>
       <table id="tblEmployee1" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
     
        <th>Quantity</th>
      
          <th>Item Name</th>
           <th>MFG</th>
          <th>EXP</th>
          <th>Batch#</th>
      
      
      </tr>
    </thead>
    <tbody>
      
    </tbody>
  </table>
  </div>
                 
                      
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-3">
                            <button type="button" class="btn btn-primary" onclick="validate();" id="app" style="background-color:#0A408A;">Approve</button>
            </div>
                         
                                </div>
                        </div>
                  
      

        </div>
          </div>
      </div>
    </div>
</div>

             
 
                <div class="container" style="width:90%">
        <div class="modal fade" id="Popup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true" >
  <div class="modal-dialog modal-lg" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#0A408A;">
            <h2 style="color:white;">View Approved Items</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
    
   <div class="panel-body">


           <div class="table-responsive"> 
      <div class="table-responsive">
              <table  class="pull-right">
                  <tr>
                      <td>
               </td><td>
     
                  </td></tr>
              </table>


                </div>

       <br />
               <label id="ok1" class="form-control" hidden></label>
       <table id="tblEmployee2" class="table table-responsive-lg table-hover" style="font-size:15px;"> 
               
           <thead style="font-family:Cambria ;font-size:14px;text-decoration-style:solid;color:#0A408A;">
      <tr>
               
          <th>Item Name</th>
        <th>Quantity</th>
       
         
      
      
      </tr>
    </thead>
    <tbody>
      
    </tbody>
  </table>
  </div>
                 
                      
                       
                        </div>
                  
      

        </div>
          </div>
      </div>
    </div>
</div>


    
              



        </form>

 
  

</asp:Content>
