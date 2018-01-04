<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="ManageAttendees.aspx.cs" Inherits="Provider_ManageAttendees" Title="Upload or Edit Attendees | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
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
            var urlcourseid = getParameterByName('courseid', '');
            if (urlcourseid == null) {
                BuildResults();
            } else {
                $('#uiPaginationPrevious').css('display', 'none');
                $('#uiPaginationNext').css('display', 'none');
                $('.showpagesdiv').css('display', 'none');
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
                if (txt == 'ALL') {
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
            function BuildResults() {

                var recordsperpage = $('#HiddenRecordsPerPage').val();
                var curpage = $('#uihdncurpage').val();
                var title = $('#txtTitle').val();
                var startDate = $('#txtStartDate').val();
                var endDate = $('#txtEndDate').val();
                var first = $('#uiTxtFirstName').val();
                var last = $('#uiTxtLastName').val();
                var email = $('#uiTxtEmail').val();
                var location = $('#ddlLocation').val();
                var status = "";
                if (curpage == '1') {
                    $('#uiPaginationPrevious').css('display', 'none');
                    $('#uiPaginationNext').css('display', 'block');
                } else {
                    $('#uiPaginationNext').css('display', 'block');
                    $('#uiPaginationPrevious').css('display', 'block');

                }
                var url = "/Provider/ManageAttendeesCourseListAjaxResults.aspx?itemsperpage=" + recordsperpage + '&page=' + curpage;
                var totalshown = parseInt(recordsperpage) * parseInt(curpage);
                if (title.length > 0) {
                    url = url + '&title=' + title;
                }
                if (startDate.length > 0) {
                    url = url + '&startdate=' + startDate;
                }
                if (endDate.length > 0) {
                    url = url + '&enddate=' + endDate;
                }
                if (location.length > 0)
                {
                    url = url + '&location' + location;
                }
                if (first.length > 0)
                {
                    url = url + "&first=" + first;
                }
                if (last.length > 0)
                {
                    url = url + "&last=" + last;
                }
                if (email.length > 0)
                {
                    url = url + "&email=" + email;  
                }
                var res;
                $.ajax({
                    type: "GET",
                    url: url.replace('ManageAttendeesCourseListAjaxResults', 'ManageAttendeesCourseListAjaxResultCount'),
                    dataType: "text",
                    success: function (data) {
                        $("#uiLblCourseCount").text(data);
                        secondPart(data, url, totalshown);

                    }

                });

            }
            function secondPart(data, url, totalshown) {
                $.ajax({
                    type: "GET",
                    url: url,
                    dataType: "html",
                    success: function (data) {
                        $("#results").html(data);
                    }
                });
                if (parseInt($("#uiLblCourseCount").text()) <= totalshown) {
                    $('#uiPaginationNext').css('display', 'none');

                }
            }
        });        

    </script>
         <div style="width:100%;margin-top:20px;">        
            <div class="title" style="float:left;">
                    Attendees
                </div>        
            <div style="float:right;">
                    <asp:Literal ID="uiLitResultsMessage" runat="server"></asp:Literal>
            </div>        
         </div>    
    <br />
            <div class="uk-grid smallBoxBorder" style="margin-left:0;margin-top:20px;padding-bottom:20px;">
            <div class="uk-width-1-1 uk-padding-small">
                <div class="smallBoxTitle">Search Attendees</div>
            </div>
            <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                <div class="smallBoxLabel">
                    Course Title
                </div>
                <asp:TextBox ID="txtTitle" CssClass="frmMainSearch" runat="server" MaxLength="80" Text="" style="width:90%;" ClientIDMode="Static"></asp:TextBox>                
              </div>
            <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                      <div class="smallBoxLabel">Course State/Province</div>
                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="solidBorder" style="width:90%;" ClientIDMode="Static">
                        </asp:DropDownList>
            </div>
            <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">                                                                             
                        <div class="smallBoxLabel">Start Date</div>
                        <asp:TextBox ID="txtStartDate" runat="server" MaxLength="12" style="width:90%;z-index:99999;" CssClass="frmMainSearch" ClientIDMode="Static"></asp:TextBox>                        
            </div>
            <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                <div class="smallBoxLabel">
                    Attendee First Name
                </div>
                <asp:TextBox ID="uiTxtFirstName" CssClass="frmMainSearch" runat="server" MaxLength="80" Text="" style="width:90%;" ClientIDMode="Static"></asp:TextBox>                
              </div>
                <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                <div class="smallBoxLabel">
                    Attendee Last Name
                </div>
                <asp:TextBox ID="uiTxtLastName" CssClass="frmMainSearch" runat="server" MaxLength="80" Text="" style="width:90%;" ClientIDMode="Static"></asp:TextBox>                
              </div>
                <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                     <div class="smallBoxLabel">End Date</div>
                        <asp:TextBox ID="txtEndDate" runat="server" MaxLength="12" style="width:90%;z-index:99999;" CssClass="frmMainSearch" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                <div class="smallBoxLabel">
                    Attendee Email Address
                </div>
                <asp:TextBox ID="uiTxtEmail" CssClass="frmMainSearch" runat="server" MaxLength="80" Text="" style="width:90%;" ClientIDMode="Static"></asp:TextBox>                
              </div>
                <div class="uk-width-1-1 uk-width-small-1-2 uk-width-medium-1-3">
                    &nbsp;
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
        <div class="uk-width-1-2 showpagesdiv" style="float:right;text-align:right;margin-top:20px;">
           <input id="HiddenRecordsPerPage" type="hidden" value="25"  />
            Show <a id="uiLnkShow25" runat="server" class="itemsPerPage">2</a>
            <a id="uiLnkShow50" runat="server" class="itemsPerPage" style="margin-left:10px;">4</a>
            <a id="uiLnkShowAll" runat="server" class="itemsPerPage" style="margin-left:10px;">ALL</a>            
        </div>        
        <div class="uk-width-1-1">        
            <hr />
        </div>
    </div>
       
    <!--Hidden field used to check weather save or not the participant list form-->
    <div id="message" style="text-align: left;">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
        <input id="HiddenSaveInformation" type="hidden" value="N" runat="server" />
        <input id="HiddenRedirectUrl" type="hidden" value="1" runat="server" />        
    </div>
    <div id="results"></div>


    <a id="uiPaginationPrevious">&laquo; Pevious</a>
    <a id="uiPaginationNext" style="margin-left:20px;">Next &raquo;</a>
    <input type="hidden" id="uihdncurpage" value="1" />
    
    <asp:Repeater id="rptParticipantList"  runat="server">
        <HeaderTemplate>
            <div class="uk-grid">
                    <div class="uk-width-3-10">
                        Last Name
                    </div>  
                <div class="uk-width-3-10">
                        First Name
                    </div>        
                <div class="uk-width-4-10"></div>                                            
        </HeaderTemplate>
        <ItemTemplate>
            <div class="uk-width-3-10">
                        <a href="EditAttendees.aspx?ParticipantID=<%#Eval("ID").ToString()%>&CourseID=<%#Request.QueryString["courseid"].ToString() %>"><%#Eval("LastName").ToString()%></a>
                    </div>  
                <div class="uk-width-3-10">
                        <%#Eval("FirstName").ToString()%>
                    </div>        
                <div class="uk-width-4-10"></div>
            
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>

    </asp:Repeater>        
    
</asp:Content>
