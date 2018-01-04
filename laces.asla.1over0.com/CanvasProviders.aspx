<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CanvasProviders.aspx.cs" Inherits="CanvasProviders" %>
<head runat="server">
    <title></title>
    <script src="javascript/jquery-1.4.2.js"></script>
    <script src="javascript/jquery.canvasjs.min.js"></script>
    <script type="text/javascript">
  window.onload = function () {
    var chart = new CanvasJS.Chart("chartContainer",
    {

      title:{
      text: "Leading Providers"
      },
       data: [
      {
          type: "bar",

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
