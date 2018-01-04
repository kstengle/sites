<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataVisualizationsAjax.aspx.cs" Inherits="Admin_DataVisualizationsAjax" %>
<script src="/bower_components/jquery/dist/jquery.js" type="text/javascript"></script>     
    <script src="/bower_components/jquery/jquery-ui.min.js" type="text/javascript"></script>                
    <script src="/javascript/jquery.canvasjs.min.js" type="text/javascript"></script>
  
    <script type="text/javascript">
  window.onload = function () {
    var chart = new CanvasJS.Chart("chartContainer",
    {

      title:{
      text: "Test"
      },
       data: [
      {         
          type: "bar",
          toolTipContent: "{y}",
        dataPoints: [
        <%=jsondata%>
        ]
      }
      ]
    });

    chart.render();
  }
	</script>
    <div id="chartContainer" style="height: 400px; width: 100%;">
	</div>
