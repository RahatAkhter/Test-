<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Time Practice.aspx.cs" Inherits="SpangleERP.HR_Module.Time_Practice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            
        });

//        function aja() {
//            var time = $('#time').val();
//            //timeout
//            var timeout = $('#timeout').val();
//            alert(time);
//               $.ajax({
//                        url: 'Time Practice.aspx/Check',
//                        contentType: "application/json; charset=utf-8",
//                        dataType: "json",
//                   method: 'post',
//                   data: "{'time':'" + time + "','time2':'" + timeout + "'}",
//                        success: function (data) {
                         
//                            alert(data.d);
//                        },
//                        error: function (err) {
//                            alert(err);
//                        }


//                    });
//        }



//        function addMinutes(time, minsToAdd) {
//  function D(J){ return (J<10? '0':'') + J;};
//  var piece = time.split(':');
//  var mins = piece[0]*60 + +piece[1] + +minsToAdd;

//  return D(mins%(24*60)/60 | 0) + ':' + D(mins%60);  
//}  
        
//        function ss() {
//            var hrs = 0;
//            //var time = $('#time').val();
//            //var date = new Date(time);
//            //alert(date.getTime());

//            //alert(addMinutes(time, 90));
//            //alert(addMinutes(time, 180));
//            //alert(time);
//            //alert(timeSummation(time, 180));
//            var time = document.getElementById("time");
            
//            hrs = time.value.split(":")[0];
//            var nhrs = 0;
//            nhrs = parseInt(hrs) + 3;
//            alert(time);
//            var mins = time.value.split(":")[1];
//            var newTime = ampm(nhrs, mins);
//            // document.getElementById("mySpan").innerHTML = newTime;
//            alert(newTime);
//        }

//        function ampm(hrs, mins) {
//            if (hrs > 24) {
//                hrs = parseInt( hrs) - 24;
//                return ( hrs % 12 || 12 ) + ":" + mins + (( hrs >= 12 ) ? "PM" : "AM" );
//            }
//            return ( hrs % 12 || 12 ) + ":" + mins + (( hrs >= 12 ) ? "PM" : "AM" );
    
//}
////        function timeSummation(a,b) {
////  var t1 = a.value.split(':');
////  var t2 = b.value.split(':');
////  var mins = Number(t1[1])+Number(t2[1]);
////  var hrs = Math.floor(parseInt(mins / 60));
////  hrs = Number(t1[0])+Number(t2[0])+hrs;
////  mins = mins % 60;
////  return hrs+':'+mins;
//////}
////        function addTimes (startTime, endTime) {
////  var times = [ 0, 0, 0 ]
////  var max = times.length

////            var a = startTime;
////            var b = endTime;

////  // normalize time values
////  for (var i = 0; i < max; i++) {
////    a[i] = isNaN(parseInt(a[i])) ? 0 : parseInt(a[i])
////    b[i] = isNaN(parseInt(b[i])) ? 0 : parseInt(b[i])
////  }

////  // store time values
////  for (var i = 0; i < max; i++) {
////    times[i] = a[i] + b[i]
////  }

////  var hours = times[0]
////  var minutes = times[1]
////  var seconds = times[2]

////  if (seconds >= 60) {
////    var m = (seconds / 60) << 0
////    minutes += m
////    seconds -= 60 * m
////  }

////  if (minutes >= 60) {
////    var h = (minutes / 60) << 0
////    hours += h
////    minutes -= 60 * h
////  }

////  return ('0' + hours).slice(-2) + ':' + ('0' + minutes).slice(-2) + ':' + ('0' + seconds).slice(-2)
////}
        function aja() {

            var date = "2019-04-04"
            var total = date.substring(0, 7)
            alert(total);

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        
        <input  type="time" id="time"/>
      <br />  TimeOut<input  type="time" id="timeout"/>
<br />
        <button type="button" onclick="aja();">Click</button>
    </form>
</body>
</html>
