<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Canvas.aspx.cs" Inherits="Canvas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="javascript/jquery-1.4.2.js"></script>
    <script src="javascript/jquery.canvasjs.min.js"></script>
    <script type="text/javascript">
  window.onload = function () {
    var chart = new CanvasJS.Chart("chartContainer",
    {

      title:{
      text: "Courses Per Year"
      },
       data: [
      {         

        dataPoints: [
        <%=jsondata%>
        ]
      }
      ]
    });

    chart.render();
  }
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="chartContainer" style="height: 400px; width: 100%;">
	</div>
    </form>
</body>
</html>
