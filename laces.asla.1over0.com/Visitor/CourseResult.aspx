<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="CourseResult.aspx.cs" Inherits="Visitor_CourseResult" Title="Search Results | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
                $('.searchHelpLink').click(function () {
                    $('#searchHelp').show();
                });

                $('#closeBox').click(function () {
                    $('#searchHelp').hide();
                });



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
        });        

    </script>
    <!--Start Left Content Place Holder-->
    <script type="text/javascript" language="javascript" src="/javascript/SearchCourse.js"></script>
     <div style="width:100%;">        
            <div class="title" style="float:left;">
                    Courses
                </div>        
            <div style="float:right;">
                    <asp:Literal ID="uiLitResultsMessage" runat="server"></asp:Literal>
            </div>        
     </div>    
    <div style="padding-top:50px;">
        <div style="text-align: left;">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
        </div>
        <div class="uk-grid smallBoxBorder" style="margin-left:0;">
            <div class="uk-width-1-1 uk-padding-small">
                <div class="smallBoxTitle">Search Courses</div>
            </div>
            <div class="uk-width-1-3">
                        <div class="smallBoxLabel">
                            Keyword
                        </div>
                <asp:TextBox ID="txtKeyword" CssClass="frmMainSearch" runat="server" MaxLength="80" Text="" style="width:90%;"></asp:TextBox>                
                      <div class="smallBoxLabel">Subjects</div>
                        <asp:DropDownList ID="ddlSubject" runat="server" CssClass="solidBorder" style="width:90%;">
                        </asp:DropDownList>
            </div>
            <div class="uk-width-1-3">                                                                                                                    
                        <div class="smallBoxLabel">Start Date</div>
                        <asp:CustomValidator Display="None" ID="CustomValidator1" runat="server" ClientValidationFunction="DateValidation"
                            ControlToValidate="txtStartDate" ErrorMessage="Enter valid start date." ForeColor="White"
                            SetFocusOnError="True">*</asp:CustomValidator>
                        <asp:TextBox ID="txtStartDate" runat="server" MaxLength="12" style="width:90%;z-index:99999;" CssClass="frmMainSearch" ClientIDMode="Static"></asp:TextBox>        
                 <div class="smallBoxLabel">End Date</div>
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateValidation"
                            ControlToValidate="txtEndDate" Display="None" ErrorMessage="Enter valid end date."
                            ForeColor="White" SetFocusOnError="True">*</asp:CustomValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                            ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start date cannot be higher than end date."
                            ForeColor="White" Operator="GreaterThanEqual" Type="Date" SetFocusOnError="True">*</asp:CompareValidator>
                        <asp:TextBox ID="txtEndDate" runat="server" MaxLength="12" style="width:90%;z-index:99999;" CssClass="frmMainSearch" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="uk-width-1-3">                                                                                                                        
                       
                       <div class="smallBoxLabel" style="width:95%;">
                            Providers
                            <a class="searchHelpLink" style="float:right;">Search Help</a></div>
                       
                        <asp:ListBox ID="lbEducationProvider" Rows="5" runat="server" SelectionMode="Multiple" style="width:95%;">
                        </asp:ListBox>
             </div>
            <div class="uk-width-2-3">                                 
                    <div class="smallBoxLabel">
                       <asp:CheckBox ID="uiChkHealthSafetyWelfare" runat="server" style="margin-left:-5px;" />Search for Healthy, Safety, and Welfare (HSW) courses only            
                     </div>
                    <div class="smallBoxLabel">           
                        <asp:CheckBox ID="uiChkDistanceEducation" runat="server" style="margin-left:-5px;" />Search for Distance Education courses only        
                    </div>
             </div>                
            <div class="uk-width-1-3" style="padding-bottom:15px;">                                   
                    <div class="smallBoxLabel">&nbsp;</div>
                <div class="smallBoxLabel">
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" ToolTip="SEARCH" CssClass="commonButton btn84" OnClick="btnSearch_Click" style="float:right;" />
                    </div>
             </div>
        </div>

        <script type="text/javascript">
        ///Select existing value of keyword text box
        document.getElementById('<%=txtKeyword.ClientID%>').select();
        </script>

    </div>
    <br />

    <div>
        <asp:Label ID="lblMessage" CssClass="padding10" runat="server"></asp:Label>
    </div>
    <div class="searchMsg"></div>
        <asp:Repeater ID="uiRptCourseSearchResults" runat="server">
            <HeaderTemplate>
                <div class="uk-grid">
            </HeaderTemplate>
            <SeparatorTemplate><div class="uk-width-1-1" style="height:20px;"></div></SeparatorTemplate>
            <ItemTemplate>
                <div class="uk-width-1-1">                
                    <a href="CourseDetails.aspx?CourseID=<%#Eval("ID").ToString() %>">
                                        <%# Server.HtmlEncode(Eval("Title").ToString())%>
                                    </a>                    
                </div>
                <div class="uk-width-1-4">
                    Start Date
                </div>
                <div class="uk-width-3-4">                    
                                        <%#DateTime.Parse(Eval("StartDate").ToString()).ToShortDateString()%>                                                
                </div>
                <div class="uk-width-1-4">
                    End Date
                </div>
                <div class="uk-width-3-4">
                    
                                        <%#DateTime.Parse(Eval("EndDate").ToString()).ToShortDateString()%>
                </div>
                <div class="uk-width-1-4">
                    Provider
                </div>
                <div class="uk-width-3-4">
                    <a href="/ApprovedProviderDetails.aspx?ProviderID=<%#Eval("ProviderID").ToString()%>">                    
                                        <%# Server.HtmlEncode(Eval("ProviderName").ToString())%>         
                      </a>         
                </div>
                <div class="uk-width-1-4">
                    Subjects
                </div>
                <div class="uk-width-3-4">
                    
                                        <%# Server.HtmlEncode(Eval("Subjects").ToString())%>                  
                </div>
                <div class="uk-width-1-1" style="margin-bottom:5px; margin-top:15px;">                    
                      <hr />
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
        <div ID="PreviousPage" runat="server"></div>
        <div ID="NextPage" runat="server"></div>
    <!--End Left Content Place Holder-->
</asp:Content>
