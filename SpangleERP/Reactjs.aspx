<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reactjs.aspx.cs" Inherits="SpangleERP.Reactjs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <!--online-->
    
    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>

<script type="text/javascript">

    $(document).ready(function () {


        //$.ajax({
        //   url: 'http://localhost:51955/EmpService.svc/Gettrains',
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "json",
        //                method: 'post',
        //                data: "{}",

        //    success: function (data) {

        //        for (var i = 0; i < data.d.length; i++) {
        //            alert(data.d[i].sname);
        //        }

        //    },
        //    error: function (err) {
        //        alert("Error  he");
        //    }

        //});


    });

//class Car {
//  constructor(name) {
//    this.brand = name;
//  }

//  present() {
//    return 'I have a ' + this.brand;
//  }
//    }

//    class Modal extends Car {

//        constructor(name, type) {
//            super(name);
//            this.modal = type;
//        }

//         show() {
//      return this.present() + ', it is a ' + this.model
//        }
//        // this is Arrow Function
//        func = (val, val2) => "Hello" + val + " "+val2;
//    }

//mycar = new Car("Ford");
//    document.write(mycar.present());


//    alert(mycar.present());

//    //child = new Modal("Ponka", "Poor");
//    //alert(child.show());
//    //alert(child.func("rahat", "Akhter"));

//  let myelement = (
//  <table>
//    <tr>
//      <th>Name</th>
//    </tr>
//    <tr>
//      <td>John</td>
//    </tr>
//    <tr>
//      <td>Elsa</td>
//    </tr>
//  </table>
//);

//ReactDOM.render(myelement, document.getElementById('root'));

</script>

</head>
    
<body>
    <form id="form1" runat="server">
         <div id="root"></div>
         <input type="text" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>       


        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"  />
        </ContentTemplate>

                </asp:UpdatePanel>
            
    </form>
</body>
</html>
