<%@ Page Language="C#" MasterPageFile="~/PublicMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Title="Landscape Architecture Continuing Education System | LA CES™" %>
<%@ MasterType VirtualPath="~/PublicMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <script src="/bower_components/jquery/dist/jquery.js" type="text/javascript"></script>            
     <script src="/bower_components/uikit/js/uikit.min.js" type="text/javascript"></script>     <script src="/bower_components/jquery/jquery-ui.min.js" type="text/javascript"></script>                   
    <script src="/bower_components/uikit/js/components/notify.js" type="text/javascript"></script>
    <script src="/bower_components/uikit/js/components/sticky.js" type="text/javascript"></script>
    <script src="/bower_components/uikit/js/components/tooltip.js" type="text/javascript"></script>
    <script src="/bower_components/uikit/js/components/slideshow.js" type="text/javascript"></script>
    <script src="/bower_components/uikit/js/components/slideset.js" type="text/javascript"></script>
    <script src="/bower_components/uikit/js/components/slideshow-fx.js" type="text/javascript"></script>	
   
<style type="text/css">
.uk-slidenav {
    display: inline-block;
    box-sizing: border-box;
    width: 60px;
    height: 60px;
    line-height: 60px;
    color: rgba(50, 50, 50, 0.4);
    font-size: 60px;
    text-align: center;
    text-shadow: 0px 1px 0px rgba(0, 0, 0, 0.1);
}

.uk-slidenav:hover, .uk-slidenav:focus {
    outline: none;
    text-decoration: none;
    color: rgba(50, 50, 50, 0.7);
    cursor: pointer;
}

.uk-slidenav:active {
    color: rgba(50, 50, 50, 0.9);
}

.uk-slidenav-previous:before {
    content: "";
    font-family: FontAwesome;	
}

.uk-slidenav-next:before {
    content: "";
    font-family: FontAwesome;
}

.uk-slidenav-position {
    position: relative;
}

.uk-slidenav-position .uk-slidenav {
/*    display: none;*/
    position: absolute;
    top: 50%;
    z-index: 1;
    margin-top: -30px;
	color:#c8c8bf;
}

.uk-slidenav-position:hover .uk-slidenav {
    display: block;
}

.uk-slidenav-position .uk-slidenav-previous {
    left: 10px;
}

.uk-slidenav-position .uk-slidenav-next {
    right: 10px;
}

.uk-slidenav-contrast {
    color: rgba(255, 255, 255, 0.5);
}

.uk-slidenav-contrast:hover, .uk-slidenav-contrast:focus {
   /* color: rgba(255, 255, 255, 0.7);*/
   color: red;
}

.uk-slidenav-contrast:active {
    color: rgba(255, 255, 255, 0.9);
}

.uk-slider {
    position: relative;
    z-index: 0;
    touch-action: pan-y;
}

.uk-slider:not(.uk-grid) {
    margin: 0;
    padding: 0;
    list-style: none;
}

.uk-slider>* {
    position: absolute;
    top: 0;
    left: 0;
}

.uk-slider-container {
    overflow: hidden;
}

.uk-slider:not(.uk-drag) {
    -webkit-transition: -webkit-transform 200ms linear;
    transition: transform 200ms linear;
}

.uk-slider.uk-drag {
    cursor: col-resize;
    -moz-user-select: none;
    -webkit-user-select: none;
    -ms-user-select: none;
    user-select: none;
}

.uk-slider a, .uk-slider img {
    -webkit-user-drag: none;
    user-drag: none;
    -webkit-touch-callout: none;
}

.uk-slider img {
    pointer-events: none;
}

.uk-slider-fullscreen, .uk-slider-fullscreen>li {
    height: 100vh;
}

.uk-slideshow {
    position: relative;
    z-index: 0;
    width: 100%;
    margin: 0;
    padding: 0;
    list-style: none;
    overflow: hidden;
    touch-action: pan-y;
}

.uk-slideshow>li {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    opacity: 0;
}

.uk-slideshow>.uk-active {
    z-index: 10;
    opacity: 1;
}

.uk-slideshow>li>img {
    visibility: hidden;
}

[data-uk-slideshow-slide] {
    cursor: pointer;
}

.uk-slideshow-fullscreen, .uk-slideshow-fullscreen>li {
    height: 100vh;
}

.uk-slideshow-fade-out {
    -webkit-animation: uk-fade 0.5s linear reverse;
    animation: uk-fade 0.5s linear reverse;
}

.uk-slideshow-scroll-forward-in {
    -webkit-animation: uk-slide-right 0.5s ease-in-out;
    animation: uk-slide-right 0.5s ease-in-out;
}

.uk-slideshow-scroll-forward-out {
    -webkit-animation: uk-slide-left 0.5s ease-in-out reverse;
    animation: uk-slide-left 0.5s ease-in-out reverse;
}

.uk-slideshow-scroll-backward-in {
    -webkit-animation: uk-slide-left 0.5s ease-in-out;
    animation: uk-slide-left 0.5s ease-in-out;
}

.uk-slideshow-scroll-backward-out {
    -webkit-animation: uk-slide-right 0.5s ease-in-out reverse;
    animation: uk-slide-right 0.5s ease-in-out reverse;
}

.uk-slideshow-scale-out {
    -webkit-animation: uk-fade-scale-15 0.5s ease-in-out reverse;
    animation: uk-fade-scale-15 0.5s ease-in-out reverse;
}

.uk-slideshow-swipe-forward-in {
    -webkit-animation: uk-slide-left-33 0.5s ease-in-out;
    animation: uk-slide-left-33 0.5s ease-in-out;
}

.uk-slideshow-swipe-forward-out {
    -webkit-animation: uk-slide-left 0.5s ease-in-out reverse;
    animation: uk-slide-left 0.5s ease-in-out reverse;
}

.uk-slideshow-swipe-backward-in {
    -webkit-animation: uk-slide-right-33 0.5s ease-in-out;
    animation: uk-slide-right-33 0.5s ease-in-out;
}

.uk-slideshow-swipe-backward-out {
    -webkit-animation: uk-slide-right 0.5s ease-in-out reverse;
    animation: uk-slide-right 0.5s ease-in-out reverse;
}

.uk-slideshow-swipe-forward-in:before, .uk-slideshow-swipe-backward-in:before {
    content: '';
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    z-index: 1;
    background: rgba(0, 0, 0, 0.6);
    -webkit-animation: uk-fade 0.5s ease-in-out reverse;
    animation: uk-fade 0.5s ease-in-out reverse;
}

[data-uk-sticky].uk-active {
    z-index: 1;
    box-sizing: border-box;
}

.uk-sticky-placeholder>* {
    -webkit-backface-visibility: hidden;
    backface-visibility: hidden;
}

[data-uk-sticky][class*='uk-animation-'] {
    -webkit-animation-duration: 0.2s;
    animation-duration: 0.2s;
}

[data-uk-sticky].uk-animation-reverse {
    -webkit-animation-duration: 0.2s;
    animation-duration: 0.2s;
}

.uk-tooltip {
    display: none;
    position: absolute;
    z-index: 1030;
    box-sizing: border-box;
    max-width: 200px;
    padding: 5px 8px;
    background: #333;
    color: rgba(255, 255, 255, 0.7);
    font-size: 12px;
    line-height: 18px;
}

.uk-tooltip:after {
    content: "";
    display: block;
    position: absolute;
    width: 0;
    height: 0;
    border: 5px dashed #333;
}

.uk-tooltip-top:after, .uk-tooltip-top-left:after, .uk-tooltip-top-right:after {
    bottom: -5px;
    border-top-style: solid;
    border-bottom: none;
    border-left-color: transparent;
    border-right-color: transparent;
    border-top-color: #333;
}

.uk-tooltip-bottom:after, .uk-tooltip-bottom-left:after, .uk-tooltip-bottom-right:after {
    top: -5px;
    border-bottom-style: solid;
    border-top: none;
    border-left-color: transparent;
    border-right-color: transparent;
    border-bottom-color: #333;
}

.uk-tooltip-top:after, .uk-tooltip-bottom:after {
    left: 50%;
    margin-left: -5px;
}

.uk-tooltip-top-left:after, .uk-tooltip-bottom-left:after {
    left: 10px;
}

.uk-tooltip-top-right:after, .uk-tooltip-bottom-right:after {
    right: 10px;
}

.uk-tooltip-left:after {
    right: -5px;
    top: 50%;
    margin-top: -5px;
    border-left-style: solid;
    border-right: none;
    border-top-color: transparent;
    border-bottom-color: transparent;
    border-left-color: #333;
}

.uk-tooltip-right:after {
    left: -5px;
    top: 50%;
    margin-top: -5px;
    border-right-style: solid;
    border-left: none;
    border-top-color: transparent;
    border-bottom-color: transparent;
    border-right-color: #333;
}

</style>

    
<script type="text/javascript">
    $(document).ready(function () {
        $('.searchHelpLink').click(function () {
            $('#searchHelp').show();
        });

        $('#closeBox').click(function () {
            $('#searchHelp').hide();
        });

        $('.professsionalsTab').click(function() {
            $('#LeftSide').css("display", "block");
            $('#RightSide').css("display", "none");
            $('.professsionalsTab').addClass("activetab");
            $('.providersTab').removeClass("activetab");
        });

        $('.providersTab').click(function () {            
            $('#LeftSide').css("display", "none");            
            $('#RightSide').css("display", "block");
            $('#RightSide').css("margin-left", "30px");
            $('#RightSide').css("margin-top", "20px");
            $('#RightSide').removeClass('uk-hidden-small');
            $('.providersTab').addClass("activetab");
            $('.professsionalsTab').removeClass("activetab");
        });
        $('.startDate, .endDate').attr("readonly", "readonly");
        $(".startDate, .endDate").datepicker({          
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
                if ($(event.target).hasClass("ui-datepicker-current")) {
                    $(pickerID).datepicker("setDate", new Date());
                    // $(event.target).datepicker("setDate", new Date());
                }             
            });
        }
    });

       
    $(window).resize(function() {
        $('#ui-datepicker-div').hide();       
    });

</script>    
        <div class="desktopmenu uk-hidden-large uk-margin-large-top uk-width-1-1 uk-grid">
            <div class="uk-width-1-1 uk-width-small-7-10">
                <div class="mobiletabs uk-grid">
                        <div class="uk-width-1-2 professsionalsTab activetab">Professionals</div>
                        <div class="uk-width-1-2 providersTab">Providers</div>
                </div>                
            </div>
            <div class="uk-width-1-1 uk-width-small-3-10 mobiletabsph">
            </div>            
        </div>
        <div id="LeftSide">
                   
            <div class="title">For Professionals</div>
            <div class="uk-margin-top title2">Find a Course</div>            
            <div class="leftAlign">
                    <div class="VisitorStrechTxt">
                        Search for continuing education courses offered by LA CES™ approved providers. Be
                        sure to check state mandatory continuing education requirements to ensure courses
                        are compatible with your state requirements. <a href="http://www.asla.org/StateGovtAffairsLicensure.aspx">Explore</a> detailed information on state continuing education requirements.
                    </div>
                    <div class="defaultPageTableRow">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
                    </div>
                    <div class="defaultPageTableRow">
                       <asp:TextBox ID="txtKeyword" runat="server" MaxLength="80" CssClass="frmMainSearch" placeholder="Search Here" Width="75%" ></asp:TextBox>                        
                    </div>
            </div>                       
            
            <div>
                <div style="width:90%;" class="HomePageEntryFields">
                Educational Provider
                <a class="searchHelpLink uk-hidden-small">Search Help</a>            
             </div>            
                <asp:ListBox ID="lbEducationProvider" runat="server" SelectionMode="Multiple" CssClass="solidBorder" style="width:90%;"></asp:ListBox>
            </div>
            <div  class="HomePageEntryFields">
                <asp:CheckBox ID="uiChkHealthSafetyWelfare" runat="server" ClientIDMode="Static" CssClass="cssForCheckbox" style="padding-top:1%;" />
                    <label class="lblForCheckbox">Search for Healthy, Safety, and Welfare (HSW) courses only</label>
            </div>
            <div class="HomePageEntryFields">
                <asp:CheckBox ID="uiChkDistanceEducation" runat="server" CssClass="cssForCheckbox" />
                <label class="lblForCheckbox">Search for Distance Education courses only</label>
            </div>
            <div class="HomePageEntryFields" style="width:90%;">
                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="solidBorder" style="width:90%;" >
                    </asp:DropDownList>                
            </div>
            <div class="HomePageEntryFields">                
                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="solidBorder">
                    </asp:DropDownList>               
            </div>
            <div class="HomePageEntryFields">
                    <div class="uk-margin-bottom">Available Courses from</div>
                    Start Date
                    <!--Start date calendar control-->
                    <asp:TextBox ID="txtStartDate" CssClass="startDate opportunities__input-box" runat="server" ClientIDMode="Static"></asp:TextBox>
                
            </div>
            <div class="HomePageEntryFields">
                    <span style="padding-right: 7px">End Date</span>
                    <!--End date calendar control-->
                    <asp:TextBox ID="txtEndDate" CssClass="endDate opportunities__input-box" runat="server" ClientIDMode="Static"></asp:TextBox>
                 
            </div>
            <div class="HomePageEntryFields">
                    <!--Search button to initiate search-->
                    <asp:Button ID="btnFindCourses" runat="server" Text="FIND COURSES" OnClick="btnFindCourses_Click"
                        ToolTip="FIND COURSES" CssClass="commonButton btn133" /><br />
                    &nbsp;                
            </div>
            <div class="HomePageEntryFields">
                <a href="/">Sign up for Email Digest of New Courses</a>
                <br />
                <a href="/" class="smalllinktext">Already Receive emails? Update Preferences</a>
            </div>
            <div class="HomePageEntryFields">
                <a href="/CourseRssFeed.aspx"><img src="images/rsslogo.gif" border="0" alt="" class="cssForCheckbox" style="display:inline-block;" id="rssImage"/></a> <label class="lblForCheckbox">Subscribe to RSS feed of new LA CES approved courses</label>
            </div>
        
    
        <script type="text/javascript">
        ///Select existing value of keyword text box
        document.getElementById('<%=txtKeyword.ClientID%>').select();
        </script>

        <!--End table to enter more criteria-->
        <!--End Left Content Place Holder-->
        <div  class="HomePageEntryFields">
            <div>
                    <div class="title2">
                        Download Your Course Record</div>
                    <div class="VisitorStrechTxt defaultPageTableRow">
                        Use this search tool to download your course record by completing all appropriate
                        fields. <a href="#" class="showmodal">Search Help</a><br /><br />
                    </div>
                    <div class="searchHelp" style="display:none;">
                        <div class="title">Search Help</div>
                        <div class="closeBox"><a href="#" class="close">Close X</a></div>
                        <div style="clear:both"></div>
                        <ul style="margin-top: 5px">
                            <li>To get specific results, enter one of the following ID numbers: ASLA, CELA, CLARB, CSLA or FL. If you do not have an ID number please enter your email address.</li>
                            <li>To broaden the search results, please enter your first and last names only.</li>
                            <li>Help ensure that the LA CES system has accurate, up-to-date records by submitting all of your ID numbers to Educational Providers each time you take a course.</li>
                            <li>If you have any questions, please contact <a href="mailto:laces@asla.org">LA CES</a>.</li>
                        </ul>
                    </div>

                    <script type="text/javascript">
                        $(function () {
                            $('.VisitorStrechTxt .showmodal').click(
                                function () {
                                    $('.searchHelp').show();
                                    return false;
                                }
                            );
                            $('.close').click(
                                function () {
                                    $('.searchHelp').hide();
                                    return false;
                                }
                            );
                        });
                    </script>
            </div>
            <div>
                <span id="msgSearch" style="color: Red;">
                </span>
            </div>
            <div class="uk-grid downloadSearchFields">
                <div class="uk-width-1-2">
                    <span class="defaultPageTableRow">Last name</span>
                </div>
                <div class="uk-width-1-2">                
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="solidBorder" Width="140px"></asp:TextBox>                
                </div>
                          
                <div class="uk-width-1-2">
                <span class="defaultPageTableRow">First name</span>
                </div>
                <div class="uk-width-1-2">                
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="solidBorder" Width="140px"></asp:TextBox>
                </div>
                <div class="uk-width-1-2">
                    <span class="defaultPageTableRow">ASLA Member number</span>
                </div>
                <div class="uk-width-1-2 HomePageEntryFields">                
                    <asp:TextBox ID="txtASLANumber" runat="server" CssClass="solidBorder" Width="140px"></asp:TextBox>
					<asp:TextBox ID="txtMiddleInitial" runat="server" CssClass="solidBorder" Width="140px" visible="false"></asp:TextBox>
                </div>
                <div class="uk-width-1-2 HomePageEntryFields">                        
                    <span class="defaultPageTableRow">CLARB Record number</span>
                </div>
                <div class="uk-width-1-2 HomePageEntryFields">                
                    <asp:TextBox ID="txtCLARBNumber" runat="server" CssClass="solidBorder" Width="140px"></asp:TextBox>
                </div>
                <div class="uk-width-1-2 HomePageEntryFields">                        
                    <span class="defaultPageTableRow">CSLA Member number</span>
                </div>
                <div class="uk-width-1-2 HomePageEntryFields">
                    <asp:TextBox ID="txtCSLANumber" runat="server" CssClass="solidBorder" Width="140px"></asp:TextBox>
                </div>             
                <div class="uk-width-1-2 HomePageEntryFields">
                    <span class="defaultPageTableRow">FL License number</span>
                 </div>
                <div class="uk-width-1-2 HomePageEntryFields">                                
                    <asp:TextBox ID="txtFLNumber" runat="server" CssClass="solidBorder" Width="140px"></asp:TextBox>
                </div>
                <div class="uk-width-1-2 HomePageEntryFields">
                    <span class="defaultPageTableRow">Email</span>
                </div>
                <div class="uk-width-1-2 HomePageEntryFields">                    
                    <asp:TextBox ID="txtEmailSearch" runat="server" CssClass="solidBorder" Width="140px"></asp:TextBox>
                </div>
                <div class="uk-width-1-2 HomePageEntryFields">
                    <asp:Button ID="btnSearch" runat="server" Text="SEARCH" ToolTip="SEARCH" CssClass="commonButton btn133"
                        OnClick="btnSearch_Click" />
                </div>
            </div>
       </div>    
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightane" runat="Server">
    <div id="RightSide" class="uk-hidden-small">
        <div class="title">For Providers</div>
        <div class="uk-margin-top title2">Approved Provider Services</div>
        <div class="HomePageEntryFields">
            <div>
                Please login below to access the following approved provider services:
                <ul>
                    <li>Add a Course</li>
                    <li>Course List</li>
                    <li>Upload or Edit Attendees</li>
                    <li>Update Contact Information</li>
                    <li>Renew LA CES Certification</li>
                </ul>
            </div>                
        </div>
         <div>
        <span class="ErrorSummery defaultPageTableRow">
                    <asp:Label ID="lblErrorSummary" runat="server"></asp:Label>
        </span>
        </div>
        <asp:PlaceHolder ID="uiPhLoginArea" runat="server">
        <div class="uk-grid">        
            <div id="trEmail" runat="server" class="uk-width-1-4 HomePageEntryFields">            
                    Email
             </div>
            <div class="uk-width-3-4 HomePageEntryFields">
                    <asp:TextBox ID="txtEmail" CssClass="LoginTextBox" runat="server" MaxLength="64"></asp:TextBox>            
            </div>
            <div id="trPassword" runat="server" class="uk-width-1-4 HomePageEntryFields">
                Password
            </div>
            <div class="uk-width-3-4 HomePageEntryFields">                    
                    <asp:TextBox ID="txtPassword" CssClass="LoginTextBox" TextMode="Password" runat="server"
                        MaxLength="32"></asp:TextBox>
             </div>
              <div id="trLoginButton" runat="server" class="uk-width-1-1 HomePageEntryFields">                                    
                    <asp:Button ID="btnSignIn" runat="server" CssClass="commonButton btn107" Text="SIGN IN"
                        OnClientClick="return ValidateProviderLogin()" OnClick="btnSignIn_Click" />
            </div>
            <div id="trForgotPassword" runat="server">
                    <br />
                    <a href="Provider/ForgotPassword.aspx" class="smalllinktext">Can't Remember Your Login and/or Password? </a>
                    <br /><br />                
            </div>
        </div>
            </asp:PlaceHolder>
            <%--<tr>
                <td colspan="2" class="defaultPageTableRow">
                    <a href="Provider/ProviderApplication.aspx">Become a LA CES Approved provider</a>
                </td>
            </tr>--%>                        
            <div>
                    <div class="title2">
                        Approved Provider Guidelines</div>                
            </div>
            <div>
                    <div>
                        Current and prospective approved providers:<br /> 
                         <a href="#" runat="server" id="approvedProLink">Explore</a> the LA CES™ Approved Provider Guidelines.
                            <br /><br />
                    </div>                
            </div>
            <div>
                    <div class="title2">
                        Become an LA CES™ Approved Provider</div>                
            </div>
            <div>
                    <div>

                        <p><a id="uiLnkApplicationForm" runat="server">Preview</a> the LA CES Approved Provider Application Form.</p>
                        <p><a href="Provider/ProviderApplication.aspx">Submit</a> your LA CES™ Approved Provider Application Form.</p>
						<p><a href="LA CES_Application_Sample.pdf" target="_blank">View</a> a sample LA CES Provider Application Form for more information.</p>
                        
                    </div>                
            </div>
            <div>
                <div class="defaultPageTableRow uk-grid">
                    <div class="uk-width-3-10">
                        <img src="/images/LACESSquareColor_400.jpg" alt="LACES LOGO" />
                    </div>
                    <div class="uk-width-7-10">
                        As an approved provider, you can access our logo section and add LA CES logos to your web site, brochures, and other educational and promotional materials. 
                    </div>
                </div>
            </div>
        
            <div id="uiDivSlidehshow" runat="server" style="margin-top:20px;"></div>
            <%--<div>
                <div class="defaultPageTableRow">
                    <br />
                    <img src="images/laces_canada.jpg"  alt="LACES Canada Logo"/>
                </div>
            </div>
            <div>
                <div class="defaultPageTableRow">
                    <div class="smallText">GENERAL DESIGN HONOR AWARD</div><br />
                    <span class="smallText">Architect & Site Design: WEISS/MANFREDI Architecture/Landscape/Urbanism. Landscape Architecture Consultant: HM White Site Architects. Client: Brooklyn Botanic Garden. Image: Aaron Booher.</span>
                </div>
            </div>--%>
        </div>        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomPane" runat="Server">
</asp:Content>
