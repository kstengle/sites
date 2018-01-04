<%@ Page Title="" Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true" CodeFile="DataVisualizations.aspx.cs" Inherits="Admin_DataVisualizations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">
    <script src="/bower_components/jquery/dist/jquery.js" type="text/javascript"></script>     
    <script src="/bower_components/jquery/jquery-ui.min.js" type="text/javascript"></script>                
    <script src="/javascript/jquery.canvasjs.min.js" type="text/javascript"></script>
  
    <script type="text/javascript">
        $(document).ready(function () {                    
            $('#txtStartDate, #txtEndDate').attr("readonly", "readonly");
            $("#txtStartDate, #txtEndDate").datepicker({          
                beforeShow: function() {
                    setTimeout(function() {
                        if (window.innerWidth < 480) {
                            $('html, body').animate({"scrollTop": 0});
                        }

                        $(document).unbind('mousedown', $.datepicker._checkExternalClick);
                        styleButtonPanel();
                    }, 0);
                    checkEvents($(this).datepicker());
                },                
                //changeMonth: true,
                //changeYear: true,
                //closeText: "Close",
                //currentText: "Reset",
                //minDate: 0,
                // monthNamesShort: $.datepicker.regional["en"].monthNames,
                onChangeMonthYear: function() {
                    setTimeout(function() {
                        styleButtonPanel();
                    }, 0);
                },
                onClose: function() {
                    setTimeout(function() {
                        $('.uk-modal-close').trigger("click");
                    }, 0);
                }
            });
            $(".hasDatepicker").each(function (index, element) {
                var context = $(this);
                context.on("blur", function (e) {
                    // The setTimeout is the key here.
                    setTimeout(function () {
                        if (!context.is(':focus')) {
                            $(context).datepicker("hide");
                        }
                    }, 250);
                });
            });

        //    $(document).click(function (e) { var ele = $(e.toElement); if (!ele.hasClass("hasDatepicker") && !ele.hasClass("ui-datepicker") && !ele.hasClass("ui-icon") && !$(ele).parent().parents(".ui-datepicker").length) $(".hasDatepicker").datepicker("hide"); });

            var styleButtonPanel = function() {
                $('.ui-datepicker-buttonpane').addClass("uk-clearfix");
                $('.ui-datepicker-current').removeClass("ui-priority-secondary ui-state-default ui-priority-primary ui-corner-all").addClass("button__form-btn button__form-btn--white");
                $('.ui-datepicker-close').removeClass("ui-priority-secondary ui-state-default ui-priority-primary ui-corner-all").addClass("uk-modal-close button__form-btn button__form-btn--white");
            }

            var checkEvents = function(pickerID) {
                $('.ui-datepicker').on("click", [pickerID], function(event) {
                    console.log(pickerID);
                    if ($(event.target).hasClass("ui-datepicker-current")) {
                        $(pickerID).datepicker("setDate", new Date());
                        // $(event.target).datepicker("setDate", new Date());
                    }             
                });
            }
            $("#uiLnkGenerateReport").click(function (e) {
                e.preventDefault();
                var reportType = $('#uiDDLReportType').val();
                var startdate = $('#txtStartDate').val();
                var enddate = $('#txtEndDate').val();
                var url = '/admin/DataVisualizationsAjax.aspx?reporttype=' + reportType;
                if (startdate.length > 0) {
                    url += '&startdate=' + startdate;
                }
                if (enddate.length > 0) {
                    url += '&enddate=' + enddate;
                }
                $('#uiIframvis').attr("src", url);                          
            });

        });        

    </script>
    <div class="uk-grid">
        <div class="uk-width-1-1">
                        <div class="smallBoxLabel">Start Date</div>
                        <asp:TextBox ID="txtStartDate" runat="server" MaxLength="12" style="width:20%;z-index:99999;" CssClass="frmMainSearch" ClientIDMode="Static"></asp:TextBox>                        
            </div>
        <div class="uk-width-1-1">
                     <div class="smallBoxLabel">End Date</div>
                        <asp:TextBox ID="txtEndDate" runat="server" MaxLength="12" style="width:20%;z-index:99999;" CssClass="frmMainSearch" ClientIDMode="Static"></asp:TextBox>
                </div>
        <div class="uk-width-1-1">
                     <div class="smallBoxLabel">Report</div>
                        <asp:DropDownList ID="uiDDLReportType" runat="server" Width="20%" ClientIDMode="Static">
                            <asp:ListItem Text="Courses By Subject" Value="coursesbysubject"></asp:ListItem>
                            <asp:ListItem Text="Courses By Location" Value="coursesbylocation"></asp:ListItem>
                            <asp:ListItem Text="Provider by Type" Value="providersbytype"></asp:ListItem>
                        </asp:DropDownList>
                </div>
        <div class="uk-width-1-1" style="margin-top:20px;">
                    <asp:Button id="uiLnkGenerateReport" cssclass="commonButton btn204" runat="server" ClientIDMode="Static" Text="Generate Report"></asp:Button>                     
                </div>      
        <asp:LinkButton id="donwloaddata" runat="server" onclick="uiLnkGenerateReport_Click">Download</asp:LinkButton>  
        <iframe id="uiIframvis" width="100%" height="500px;"></iframe>
    </div>
</asp:Content>

