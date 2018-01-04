<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CanvasPieChart.aspx.cs" Inherits="CanvasPieChart" %>

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
      text: "Active vs Inactive"
      },
       data: [
      {         
          type: "pie",
          showInLegend: true,
          toolTipContent: "{y} - #percent %",
          legendText: "{indexLabel}",
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
