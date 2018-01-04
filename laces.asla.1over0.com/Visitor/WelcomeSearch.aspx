<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="WelcomeSearch.aspx.cs" Inherits="VisitorWelcomeSearch" Title="LA CES™ - Find a Course - Landscape Architecture Continuing Education System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->

    <script type="text/javascript">
    var chkTitleGlobal = "<%=chkTitle.ClientID %>";
    var chkDescriptionGlobal = "<%=chkDescription.ClientID %>";
    var chkLearningOutcomesGlobal = "<%=chkLearningOutcomes.ClientID %>";
    var chkDistanceEduGlobal = "<%=chkDistanceEdu.ClientID %>";
    var ddlLocationGlobal = "<%=ddlLocation.ClientID %>";    
    </script>

    <script type="text/javascript" language="javascript" src="../javascript/SearchCourse.js"></script>

    <div class="dvHeader">
        Find a Course
    </div>
    <!--Start Right Content Place Holder-->
    <p class="moreLineHeight">
        <!--Visitor Welcome Message-->
        Use this search tool to find continuing education courses offered by providers approved
        through the Landscape Architecture Continuing Education System™. Search courses
        by keyword in the course title, description, and/or learning outcomes. Further target
        your search with the menu of subjects, location and date.<br />
        <br />
        The mission of the
        <%=LACESConstant.LACES_TEXT%>
        program is to assure landscape architects and licensing boards that courses provided
        by
        <%=LACESConstant.LACES_TEXT%>
        -approved providers are of sufficient quality. Only providers that have met
        <%=LACESConstant.LACES_TEXT%>
        requirements may register courses with
        <%=LACESConstant.LACES_TEXT%>
        . Courses that are registered will cover a broad range of subject matter, and providers
        are required to identify those courses that meet the
        <%=LACESConstant.LACES_TEXT%>
        standard of health, safety or welfare subject matter. Be sure to check state mandatory
        continuing education requirements (<a href="../documents/CEChart.pdf" target="_blank">a
            state-by-state chart</a> and <a href="../documents/CE.pdf" target="_blank">detailed
                information</a>) to ensure that any course is compatible with your individual
        state’s requirements.</p>
    <!--End Right Content Place Holder-->
    <div style="text-align: left;">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
    </div>
    <div style="padding-left: 115px;">
    <asp:TextBox ID="txtKeyword" runat="server" MaxLength="80" CssClass="frmMainSearch" placeholder="Search Here"></asp:TextBox>
    </div>
    <!--Start table to enter basic criteria-->
    <table class="LeftAlignTable">
        <tbody>
            <tr>
                <td class="frmElement2">
                    Course Title
                </td>
                <td>
                    <asp:CheckBox ID="chkTitle" runat="server" Checked="true" />
                    <!--validator to validate checkboxes-->
                    <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="validateSearchScope"
                        ControlToValidate="txtKeyword" ErrorMessage="Select at least one check box to continue your search."
                        ForeColor="White">*</asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="frmElement2">
                    Course Description
                </td>
                <td>
                    <asp:CheckBox ID="chkDescription" runat="server" Checked="true" />
                </td>
            </tr>
            <tr>
                <td class="frmElement2">
                    Learning Outcomes
                </td>
                <td>
                    <asp:CheckBox ID="chkLearningOutcomes" runat="server" Checked="true" />
                </td>
            </tr>
            <tr>
                <td class="frmElement2">
                    Subject</td>
                <td>
                    <asp:DropDownList ID="ddlSubject" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="frmElement2">
                    State/Province
                </td>
                <td>
                    <asp:DropDownList ID="ddlLocation" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="frmElement2">
                    Distance Education
                </td>
                <td>
                    <asp:CheckBox ID="chkDistanceEdu" runat="server" Checked="true" Text="" CssClass="singlecheckbox" />
                </td>
            </tr>
            <tr>
                <td class="frmElement2">
                    Start Date
                </td>
                <td>
                    <!--Start date calendar control-->
                    <asp:TextBox ID="txtStartDate" runat="server" MaxLength="12" CssClass="frmDateBox"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateValidation"
                        ControlToValidate="txtStartDate" ErrorMessage="Enter valid start date." ForeColor="White"
                        SetFocusOnError="True" Display="None">*</asp:CustomValidator>
                    <img alt="Calendar" title="Calendar" onclick="loadCalendar(this, '<%=txtStartDate.ClientID %>', 'mm/dd/yyyy')"
                        src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                </td>
            </tr>
            <tr>
                <td class="frmElement2">
                    End Date</td>
                <td>
                    <!--End date calendar control-->
                    <asp:TextBox ID="txtEndDate" runat="server" MaxLength="12" CssClass="frmDateBox"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateValidation"
                        ControlToValidate="txtEndDate" ErrorMessage="Enter valid end date." ForeColor="White"
                        SetFocusOnError="True" Display="None">*</asp:CustomValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start date cannot be higher than end date."
                        ForeColor="White" Operator="GreaterThanEqual" Type="Date" SetFocusOnError="True">*</asp:CompareValidator>
                    <img alt="Calendar" title="Calendar" onclick="loadCalendar(this, '<%=txtEndDate.ClientID %>', 'mm/dd/yyyy')"
                        src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                </td>
            </tr>
            <tr>
                <td class="smallTopPadd">
                </td>
            </tr>
            <tr>
                <td class="smallTopPadd">
                </td>
            </tr>
            
            <tr>
                <td align="right" valign="middle">
                    &nbsp;</td>
                <td align="left" class="moreLineHeight" valign="middle">
                    <!--Search button to initiate search-->
                    <asp:Button ID="btnFindCourses" runat="server" Text="FIND COURSES" OnClick="btnFindCourses_Click"
                        ToolTip="FIND COURSES" CssClass="commonButton btn107" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="padding-left:80px;">
                    <br />
                    <img alt="" title="" width="300" height="180" src="../images/InlineArt.jpg" /></td>
            </tr>
        </tbody>
    </table>
    <!--End table to enter basic criteria-->

    <script type="text/javascript">
        ///Select existing value of keyword text box
        document.getElementById('<%=txtKeyword.ClientID%>').select();
    </script>

</asp:Content>
