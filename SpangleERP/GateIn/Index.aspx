<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SpangleERP.GateIn.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
    <title>Spangle Login</title>
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet"/>
<script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <!--     Fonts and icons     -->
  <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700,200" rel="stylesheet" />
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.1/css/all.css"/>
      <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
<!------ Include the above in your HEAD tag ---------->

    <style>
    	body,
		html {
			margin: 0;
			padding: 0;
			height: 100%;
			background: #3f85f5 !important;
		}
		.user_card {
			height: 400px;
			width: 350px;
			margin-top: auto;
			margin-bottom: auto;
			background-color: rgba(0,0,0,0.5) !important;
			position: relative;
			display: flex;
			justify-content: center;
			flex-direction: column;
			padding: 10px;
			box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
			-webkit-box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
			-moz-box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
			border-radius: 5px;
		}
		.brand_logo_container {
			position: absolute;
			height: 170px;
			width: 170px;
			top: -75px;
			border-radius: 56%;
			background-color: #3f85f5 !important;
			padding: 10px;
			text-align: center;
		}
		.brand_logo {
			height: 150px;
			width: 150px;
			border-radius: 56%;
		
		}
		.form_container {
			margin-top: 100px;
		}
		.login_btn {
			width: 70%;
			background: #60a3bc !important;
			color: white !important;
		}
		.login_btn:focus {
			box-shadow: none !important;
			outline: 0px !important;
		}
		.login_container {
			padding: 0 2rem;
		}
		.input-group-text {
			background: #60a3bc !important;
			color: white !important;
			border: 0 !important;
			border-radius: 0.25rem 0 0 0.25rem !important;
		}
		.input_user,
		.input_pass:focus {
			box-shadow: none !important;
			outline: 0px !important;
		}
		.custom-checkbox .custom-control-input:checked~.custom-control-label::before {
			background-color:  #3f85f5  !important;
		}
</style>

    <script type="text/javascript">
        function Login() {
            var name = $('#name').val();
            var pass = $('#password').val();
            if (name != "" && pass != "") {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: 'Index.aspx/Signin',
                    data: "{'name':'" + name + "','pass':'" + pass + "'}",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        var myString = data.d;
                        var ok = myString.substr(0, 2);
                        var path = myString.substr(2);
                      
                        if (data.d == "Incorrect") {
                            alert("Please Insert Correct Email OR Password");
                        }
                        else if ("ok" == ok) {
                            window.location = "GateIn.aspx";
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
                alert("Please Fill The Form Correctly");
            }
        }
      
    </script>

</head>

   <body>
        <div class="container h-100">
		<div class="d-flex justify-content-center h-100">
			<div class="user_card">
				<div class="d-flex justify-content-center">
					<div class="brand_logo_container">
						<img  src="../GateIn/logo/spangle.jpg"  class="brand_logo" alt="Logo"/>
					</div>
				</div>
				<div class="d-flex justify-content-center form_container">
					<form>
						
						 <div class="input-container">
    <i class="fa fa-envelope icon" style=" color:white"></i>
    <input class="input-field" type="text" placeholder="Email" name="email" id="name"  required >
  </div>
                      <br />

  <div class="input-container">
    <i class="fa fa-key icon" style=" color:white"></i>
    <input class="input-field " type="password" placeholder="Password" name="psw" maxlength="8" minlength="4"  id="password" required onkeypress="return AvoidSpace(event)">
  </div>
					</form>
				</div>
                <br />
					<div class="d-flex justify-content-center   login_container">
					<button type="button" name="button" class="btn " style=" background-color:#3f85f5; color:white" onclick="Login();">Login</button>
				</div>
                <br />
                <br />
				<div class="form-group">
							<div class="custom-control ">
								<a style="color:white;" href="#" onclick="ForgetPassword();">Forget Password?</a>
							</div>
						</div>
			</div>
		</div>
	</div>
            	
	
     <!--model-->
           <div class="container">
        <div class="modal fade" id="SalaryPackage" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true">
  <div class="modal-dialog modal-md" role="document"  >
    <div class="modal-content">
        <div class="modal-header" style="background-color:#1F427A">
            <h2 style="color:white;">Forget Password</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  
                </div>
      <div class="modal-body mx-3" >
    

                            <br />

                              <div class="form-group">
                                <label class="control-label col-xs-12" for="pak_Name">Please Enter Your Email</label>
                                <div class="col-xs-12">
                                    <input type="text" class="form-control" id="email"  maxlength="30" placeholder="Your Email" required>
                                </div>
                                     
                            </div>
           <br />
          <br />
          <br />
          <div class="form-group">
                               
                                <div class="col-xs-12  text-center">
                                    <button type="button" class=" btn btn-primary"  onclick="Send();">Send</button>
                                </div>
                            </div>

                       
                          
                           
                  
        
    


        </div>
      </div>
    </div>
</div>
        </div>
</body>
</html>