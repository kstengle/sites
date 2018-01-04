    <%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="CourseList.aspx.cs" Inherits="Provider_ProviderCourseList" Title="Course List | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var urlstatus = getParameterByName('status', '');

            if (urlstatus != null && urlstatus.length > 0) {
                $('input[value="' + urlstatus + '"]').prop("checked", true);
            } else {
                $('input[class="statusCheckbox"]').prop("checked", true)
            }
            BuildResults();
            $('#uiTxtStartDate, #uiTxtEndDate').attr("readonly", "readonly");
            $("#uiTxtStartDate, #uiTxtEndDate").datepicker({
                beforeShow: function() {
                    setTimeout(function() {
                        //if (window.innerWidth < 480) {
                        //    $('html, body').animate({"scrollTop": 0});
                        //}

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

            $(document).click(function (e) { var ele = $(e.toElement); if (!ele.hasClass("hasDatepicker") && !ele.hasClass("ui-datepicker") && !ele.hasClass("ui-icon") && !$(ele).parent().parents(".ui-datepicker").length) $(".hasDatepicker").datepicker("hide"); });

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
            $('#uiLnkSearch').click(function (e) {
                BuildResults();
            });
            $('#uiPaginationPrevious').click(function (e) {
                var curpage = $('#uihdncurpage').val();
                var newpage = parseInt(curpage) - 1;
                $('#uihdncurpage').val(newpage);
                BuildResults();
            });
            $('.statusCheckbox').click(function (e) {
                BuildResults();
            });
            $('#uiPaginationNext').click(function (e) {
                var curpage = $('#uihdncurpage').val();
                var newpage = parseInt(curpage) + 1;
                $('#uihdncurpage').val(newpage);
                BuildResults();
            });
            $('.itemsPerPage').click(function (e) {
                var txt = $(e.target).text();
                if(txt =='ALL')
                {
                    txt = 999999;
                    $('#uiPaginationPrevious').css('display', 'none');
                    $('#uiPaginationNext').css('display', 'none');
                }
                $('#HiddenRecordsPerPage').val(txt);
                $('#uihdncurpage').val('1');
                BuildResults();                
            });

            function getParameterByName(name, url) {
                if (!url) url = window.location.href;
                name = name.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            }
            function BuildResults()
            {
                
                var recordsperpage = $('#HiddenRecordsPerPage').val();
                var curpage = $('#uihdncurpage').val();
                var keyword = $('#uiTxtKeyword').val();
                var state = $('#drpState').val();
                var Subject = $('#drpSubject').val();
                var startDate = $('#uiTxtStartDate').val();
                var endDate = $('#uiTxtEndDate').val();                
                var status = "";                
                    $('.statusCheckbox').each(function (index, obj) {
                        if (this.checked === true) {
                            if (status.length == 0) {
                                status = obj.getAttribute("value");
                            } else {
                                status = status + ',' + obj.getAttribute("value");
                            }
                        }
                    });
                    if (status == null)
                    {
                        status = '';
                    }
                if (curpage == '1')
                {
                    $('#uiPaginationPrevious').css('display', 'none');
                    $('#uiPaginationNext').css('display', 'block');
                } else 
                {
                    $('#uiPaginationNext').css('display', 'block');
                    $('#uiPaginationPrevious').css('display', 'block');

                }
                var url = "/Provider/CourseListAjaxResults.aspx?itemsperpage=" + recordsperpage + '&page=' + curpage;
                var totalshown = parseInt(recordsperpage) * parseInt(curpage);
                if(keyword.length>0)
                {
                    url = url + '&keyword=' + keyword;
                }                
                if (state.length > 0)
                {
                    url = url + '&state=' + state;
                }
                if (Subject.length > 0)
                {
                    url = url + '&subject=' + Subject;
                }
                if (startDate.length > 0)
                {
                    url = url + '&startdate=' + startDate;
                }
                if (endDate.length > 0) {
                    url = url + '&enddate=' + endDate;
                }
                if (status.length > 0)
                {
                    url = url + '&status=' + status;
                }
                var res;
                $.ajax({
                    type: "GET",
                    url: url.replace('CourseListAjaxResults', 'CourseListAjaxRecordCount'),
                    dataType: "text",
                    success: function (data) {                        
                        $("#uiLblCourseCount").text(data);
                        secondPart(data, url, totalshown);
                        
                    }

                });
                
            }
            function secondPart(data, url, totalshown)
            {                
                $.ajax({
                    type: "GET",
                    url: url,
                    dataType: "html",
                    success: function (data) {
                        $("#results").html(data);
                    }
                });
                if(parseInt($("#uiLblCourseCount").text()) <= totalshown)
                {
                    $('#uiPaginationNext').css('display', 'none');

                }
            }
        });        

    </script>
    <div class="title">Courses</div>
    <br />
    Each course is an individual record in LA CES. To edit the existing course record, click on the course title. 
    <br /><br />
    To add a new version of the course, use the quick form link to change dates, locations, instructors, or other information and create a new course record. 
    <br /><br />
    <div class="title">Course Status:</div>
    <ul>
        <li>Pending Approval: All courses listed as “Pending Approval” are under review by LA CES Administrators.</li>
        <li>Action Required: All courses listed as “Action Required” require additional information by the provider.</li>
        <li>Active: All approved courses will be listed as “Active” until the specified end date.</li>
        <li>Archived: Courses will be listed as “Archived” once its end date has passed.</li>
    </ul>
    <br />


    <div class="uk-grid smallBoxBorder" style="margin-left:0;margin-top:20px;padding-bottom:20px;">
            <div class="uk-width-1-1 uk-padding-small">
                <div class="smallBoxTitle">Find Courses</div>
                <p><strong>To Narrow Your Options, Uncheck:</strong></p>
                <br />
                <input type="checkbox" value="NP" id="uiNP" runat="server" class="statusCheckbox" />Pending Approval
                <input type="checkbox" value="IC" id="uiIC" runat="server" class="statusCheckbox" />Action Required
                <input type="checkbox" value="OP" id="uiOP" runat="server" class="statusCheckbox" />Active
                <input type="checkbox" value="PT" id="uiPT" runat="server" class="statusCheckbox" />Archived                
            </div>
        <div class="uk-width-1-1 uk-padding-small">
                <div class="smallBoxTitle">Search:</div>
            </div>
            <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                      <div class="smallBoxLabel">Keyword</div>
                       <asp:TextBox ID="uiTxtKeyword" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
        <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                      <div class="smallBoxLabel">State/Province</div>
                       <asp:DropDownList ID="drpState" runat="server" Width="90%" ClientIDMode="Static"></asp:DropDownList>
            </div>
        <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                      <div class="smallBoxLabel">Subject</div>
                       <asp:DropDownList ID="drpSubject" runat="server" AppendDataBoundItems="true" Width="95%" ClientIDMode="Static">
                            <asp:ListItem Value="" Text="Select Subject"></asp:ListItem>           
                        </asp:DropDownList>
            </div>
        <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                      <div class="smallBoxLabel">Start Date</div>
                       <asp:TextBox autocomplete="off" ID="uiTxtStartDate" CssClass="frmDateBox" runat="server" MaxLength="20" ClientIDMode="Static"></asp:TextBox>
            </div>
        <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                      <div class="smallBoxLabel">End Date</div>
                       <asp:TextBox autocomplete="off" ID="uiTxtEndDate" CssClass="frmDateBox" runat="server" MaxLength="20" ClientIDMode="Static"></asp:TextBox>
            </div>
        <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3" style="margin:auto; text-align:right;position:relative;">
            <a ID="uiLnkSearch" class="commonButton btn84" style="position:absolute; right:20px;text-align:center;padding-top:8px;">Search</a>
        </div>
    </div>
 <div class="uk-grid" style="margin-bottom:20px;">
        <div class="uk-width-1-1">        
        <asp:Label ID="uiLblCourseCount" runat="server" ClientIDMode="Static"></asp:Label> total courses
        </div>
        <div class="uk-width-1-2" style="margin-top:20px;">
            Sort By
        </div>
        <div class="uk-width-1-2" style="float:right;text-align:right;margin-top:20px;">
            <input id="HiddenRecordsPerPage" type="hidden" value="25"  />
            Show <a id="uiLnkShow25" runat="server" class="itemsPerPage">2</a>
            <a id="uiLnkShow50" runat="server" class="itemsPerPage" style="margin-left:10px;">4</a>
            <a id="uiLnkShowAll" runat="server" class="itemsPerPage" style="margin-left:10px;">ALL</a>
            
        </div>        
        <div class="uk-width-1-1">        
            <hr />
        </div>
    </div>
    
    <!--Start Left Content Place Holder-->
    <div class="title">
        Course List
    </div>
    <br />
    <div id="results"></div>


    <a id="uiPaginationPrevious">&laquo; Pevious</a>
    <a id="uiPaginationNext" style="margin-left:20px;">Next &raquo;</a>
    <input type="hidden" id="uihdncurpage" value="1" />
    <div id="NoResult" runat="server" visible="false">
        No records found.
    </div>
    <!--End Left Content Place Holder-->
</asp:Content>
