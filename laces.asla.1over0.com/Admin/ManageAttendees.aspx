<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="ManageAttendees.aspx.cs" Inherits="Admin_ManageAttendees" Title="Admin: Upload or Edit Attendees | LA CES™" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
	<style type="text/css">
.existingCources tbody td {
	padding: 6px 0 6px 5px;
	border-bottom:1px solid #f1f1f1;
}
.existingCources thead td {
	padding: 5px;
	background:#fafafa;
	border-bottom:1px solid #eaeaea;
}
.page_navigation {
	padding:10px;
	text-align:center;
}
.page_navigation .page_link {
	padding:2px 5px;
	border:1px solid transparent;
	border-radius:2px;
	-webkit-border-radius:2px;
	-moz-border-radius:2px;
}
.page_navigation .previous_link {
	padding:2px 5px;
}
.page_navigation .next_link {
	padding:2px 5px;
}
.page_navigation .active_page {
	background:none;
	border:1px solid #0E0F76;
	text-decoration:underline;
}
/* override for this page */
.CalenderIcon { vertical-align:text-top; }
	</style>
<script type="text/javascript" language="javascript" src="../javascript/SearchCourse.js"></script>
   <%-- <script type="text/javascript" language="javascript" src="/laces/javascript/perticipant.js"></script>--%>

    <!--Hidden field used to check weather save or not the participant list form-->
    <div id="message" style="text-align: left;">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
        <input id="HiddenSaveInformation" type="hidden" value="N" runat="server" />
        <input id="HiddenRedirectUrl" type="hidden" value="1" runat="server" />
    </div>
    <!--search-->
    <div>
	<div class="title">
        Find a Course
    </div>
        <asp:TextBox ID="txtKeyword" runat="server" MaxLength="80" CssClass="frmMainSearch" style="margin-left:8px;margin-right:10px" placeholder="Search Here"></asp:TextBox>
     !-- span class="smallPadding">Available Courses from</span-->
                    Start Date
                    <!--Start date calendar control-->
                    <asp:CustomValidator Display="None" ID="CustomValidator1" runat="server" ClientValidationFunction="DateValidation"
                        ControlToValidate="txtStartDate" ErrorMessage="Enter valid start date." ForeColor="White"
                        SetFocusOnError="True">*</asp:CustomValidator>
                    <asp:TextBox ID="txtStartDate" runat="server" MaxLength="12" CssClass="frmDateBox"></asp:TextBox>&nbsp;<img
                        alt="Calendar" title="Calendar" onclick="loadCalendar(this, '<%=txtStartDate.ClientID %>', 'mm/dd/yyyy')"
                        src="../images/icon_calendar.jpg" class="CalenderIcon" />

                       <!-- br /><br / -->

                        <span style="padding-right: 7px">End Date</span>
                    <!--End date calendar control-->
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateValidation"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Enter valid end date."
                        ForeColor="White" SetFocusOnError="True">*</asp:CustomValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start date cannot be higher than end date."
                        ForeColor="White" Operator="GreaterThanEqual" Type="Date" SetFocusOnError="True">*</asp:CompareValidator>
                    <asp:TextBox ID="txtEndDate" runat="server" MaxLength="12" CssClass="frmDateBox"></asp:TextBox>&nbsp;<img
                        alt="Calendar" title="Calendar" onclick="loadCalendar(this, '<%=txtEndDate.ClientID %>', 'mm/dd/yyyy')"
                        src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                        <!-- br /><br / -->
                    <asp:Button ID="btnFindCourses" runat="server" Text="FIND COURSES" OnClick="Search"
                        ToolTip="FIND COURSES" CssClass="commonButton btn153" />
    </div>
	<br style="clear:both" />
    <div runat="server" id="existingCourcesDiv" style="margin-top:15px">
    </div>
    <div runat="server" id="dvParticipantList">
    </div>

	<script type="text/javascript" language="javascript" src="../javascript/jquery.pajinate.js"></script>
	<script type="text/javascript">
	$(document).ready(function(){
		$('.existingCources').pajinate({
			item_container_id : '#course_list',
			items_per_page : 50,			
			num_page_links_to_display : 20
		});
	});
	</script>
</asp:Content>
