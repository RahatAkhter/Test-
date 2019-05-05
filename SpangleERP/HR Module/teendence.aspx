<%@ Page Title="" Language="C#" MasterPageFile="~/HR Module/HR_Master.Master" AutoEventWireup="true" CodeBehind="teendence.aspx.cs" Inherits="SpangleERP.HR_Module.teendence" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>


    <script type="text/javascript">
        var len;
        
        $(document).ready(function () {
            $('#hide').hide();
            $('#hide2').hide();
            $('#Check').hide();

            //Check

           // Attendence();
        });


        function Attendence() {
            var set;
            var flag;
             var today = new Date();
            var date = (today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate()).toString();
             var x = document.getElementById("date");
            var dob = x.value.toString();
        
            
            var today1 = new Date(dob);
            var a1 = date.toString();
            var a2 = dob.toString();
            var d1 = new Date(a1);
            var d2 = new Date(a2);
            var name = d2.getDay();
            // here we first check the date for attendence

            $.ajax({
                async: false,
                        url: 'teendence.aspx/Check_Date',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        method: 'post',
                        data: "{'date':'" + dob + "'}",
                success: function (data) {
                    if (data.d == "false") {
                        flag = false;
                    }

                            else {
                        flag = true;
                            }
                          
                        },
                        error: function (err) {
                            alert(err);
                        }


                    });



          //a2 selected date he
            if (d2 <= d1) {
                if (flag == true) {

                    var $tbl = $('#datatable');
                    $.ajax({
                        url: 'teendence.aspx/GetUserDetail',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        method: 'post',
                        data: "{'date':'" + dob + "'}",
                        success: function (data) {
                            if (data.d.length > 1 && data != null) {
                                len = data.d.length;

                                $tbl.empty();
                                $tbl.append(' <tr><th>emp_id</th><th>Name</th><th>Date</th><th>TimeIn</th><th>TimeOut</th><th></th></tr>');

                                for (var i = 0; i < data.d.length; i++) {


                                    $tbl.append('<tr ><td ><label  id="emp' + i + '">' + data.d[i].emp_id + '</label></td><td><label  id="dd' + i + '">' + dob.toString() + '</label></td><td>' + data.d[i].emp_name + '</td><td><input type="time" id="txtin' + i + '" class="form-control"  /></td><td><input type="time" id="txtout' + i + '"  class="form-control"/></td><td><label style="color:red" id="lbl' + i + '">Absent</label></td></tr>');
                                }

                                //ye uncomment krna he
                                InsertAbsent();
                                $('#hide').show();

                            }
                            else {
                                alert("This Date Already Attended");

                            }
                        },
                        error: function (err) {
                            alert(err);
                        }


                    });
                }
                else {
                    alert("On This Date"+dob +"Attendence Already Submitted");
                }
               

                }
                else {
                alert("Please Select a Valid Date");
            }

           
           
        }
        

        //function Validate() {
         
        //         for (var i = 0; i < len; i++) {
        //     var id= document.getElementById('emp' + i).textContent;

        //         var timein = ($('#txtin' + i).val());
        //          var timeout = ($('#txtout' + i).val());
        //         var date = document.getElementById('dd' + i).textContent;
        //         var d2 = new Date(date.toString());
        //         var day = d2.getDay();
        //          let lbl = document.getElementById('lbl' + i);
                    

        //            $.ajax({
        //                url: 'teendence.aspx/Validate',
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "json",
        //                method: 'post',
        //                data: "{'date':'" + date + "','id':'" + id + "','time':'" + timein + "','time2':'" + timeout + "','day':'" + day + "'}",
        //                success: function (data) {
        //                    if (data.d == "false") {
        //                        che = 0;
        //                        lbl.innerText = data.d;
        //                    }
        //                    else {
        //                        lbl.innerText = data.d;
        //                    }
                          
        //                },
        //                error: function (err) {
        //                    alert(err);
        //                }


        //            });

                     
        //    }
        //    alert(che.toString());
        //             if (che == 0) {
        //                 alert(che);
        //                 $('#Check').hide();
        //                 $('#hide').show();

        //             }

        //}

     
        function Check_In() {

            return new Promise(resolve => {

                var ch;
            
                for (var i = 0; i < len; i++) {
                    
             var id= document.getElementById('emp' + i).textContent;
                   
                 var timein = ($('#txtin' + i).val());
                  var timeout = ($('#txtout' + i).val());
                 var date = document.getElementById('dd' + i).textContent;
                 var d2 = new Date(date.toString());
                 var day = d2.getDay();
                  let lbl = document.getElementById('lbl' + i);
                    

                    $.ajax({
                        async: false,
                        url: 'teendence.aspx/Validate',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        method: 'post',
                        
                        data: "{'date':'" + date + "','id':'" + id + "','time':'" + timein + "','time2':'" + timeout + "','day':'" + day + "'}",
                   
                        success: function (data) {
                            if (data.d == "false") {
                               // alert("valid "+ i)
                                ch = 1;
                                alert("Validate");
                                lbl.innerText = data.d;
                                lbl.innerHTML='<span  style=" font-size:large;color:red; font-weight:bold">&#10006;</span>';
                            }
                            else {
                                //alert("valid " + i);
                                //resolve(true);
                                lbl.innerText = data.d;
                                //lbl.append('&#10004')

                                lbl.innerHTML = '<span  style=" font-size:large;color:green; font-weight:bold">&#10004;</span>';
                                
                            }

                        
                           
                        },
                        error: function (err) {
                            alert(err);
                        }


                    });

                    if (i == len - 1) {
                        if (ch != null && ch == 1) {
                            resolve(false);
                        }
                        else {
                            resolve(true);
                        }
                    }
                    
                }
             
            });
           
        
        }

        function lagado() {
            //alert("lagado  " +len);
               for (var i = 0; i < len; i++) {
                    var id = document.getElementById('emp' + i).textContent;

                    var timein = ($('#txtin' + i).val());
                    var timeout = ($('#txtout' + i).val());
                    var date = document.getElementById('dd' + i).textContent;
                    var d2 = new Date(date.toString());
                    var day = d2.getDay();
                    let lbl = document.getElementById('lbl' + i);
                    //lbl.innerText = "Absence";
                   //   alert(id + "  date" + date + " txtin" + timein + " timeout" + timeout + " Day " + day);
                
                    $.ajax({
                        async: false,
                        url: 'teendence.aspx/Attendence',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        method: 'post',
                        
                        data: "{'date':'" + date + "','id':'" + id + "','time':'" + timein + "','time2':'" + timeout + "','day':'" + day + "'}",
                        success: function (data) {
                            //alert(data.d);
                            lbl.innerText = data.d;
                            alert(data.d);
                           
                        },
                        error: function (err) {
                            alert(err);
                        }


                    });
                }
        }

        function InsertAbsent() {
            
            if (date != "") {
                for (var i = 0; i < len; i++) {
                    var id = document.getElementById('emp' + i).textContent;
                      var dob= document.getElementById('dd' + i).textContent;

                    var count = 0;
                    $.ajax({
                        url: 'teendence.aspx/InsertApsent',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        method: 'post',
                        data: "{'date':'" + dob + "','id':'" + id + "'}",
                        success: function (data) {
                            if (data.d == "Exception") {
                                alert("On This Date Attendence Already Exist");

                            }
                            else if (data.d == "Save") {
                             //   $('#lbl' + i).text("pehle");
                               
                                $('#hide2').show();
                                $('#hide').show();

                            }
                             else if (data.d == "pehle") {
                                //   document.getElementById('#lbl' + i).innerHTML = 'pehle';
                             //   $('#lbl' + i).text("pehle");
                               
                                $('#hide2').show();
                                $('#Check').show();

                            }

                        },
                        error: function (err) {
                            alert(err);
                        }


                    });
                }
           }
        }

      
        function Check() {
            alert("agaya");
            Check_In().then(data => {

                if (data == false) {
                    alert("please Insert Correct ZTimmings");
                }
                else {
                    lagado();
                    document.getElementById("btn_Submit").disabled = true;
                }
              
            });



        }
    </script>
    <style>
        span.green {color:green;}


    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="form-horizontal"  method="post" runat="server" action="" >
      
     <div class="container" style="width:99%;margin-top:-26px;">
     <div class="panel-group" style="width:99%;">
       <div class="panel panel-primary" style="border-color:#0A408A;border:2px;" >
      <div class="panel-heading"style="background-color:#0A408A;"><h2 style="color:white;font-family:Cambria;font-weight:200;">Mark 


          
      </div>
         <div class="panel-body">
            
   
       <br />
   <h2>Employess</h2>
             
       <div class=" col-md-12 text-center" >
             <div class=" row">
                       

                            
                            
                 <div class=" col-md-4">
                           <label>Select Date</label>   <input type="date" id="date"   />
                     
                 </div>
                            <div class=" col-md-4" >
                            <button type="button" class=" btn btn-primary" onclick="Attendence();" style="background-color:#0A408A;color:white;">Attendence</button>
                           </div>
                       
                 </div>
           <div class="row">
               <div class=" col-sm-12 text-center text-danger">
                  
               </div>
           </div>
                        <br />
           <div id="hide2">
    <table id="datatable" style=" width:100%; height:300px;">
      
        
    </table>
       </div>
      <br />
             <div class=" row">
                 <div class=" col-md-12" id="hide">
                     <button type="button" class=" btn btn-primary" id="btn_Submit" onclick="Check();"  style="background-color:#0A408A;color:white;">Submit</button>
                     
                 </div>
                 
             </div>

            
           </div>
             
</div>

</div>
     </div>

   

  </form>
</asp:Content>
