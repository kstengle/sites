<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="FindACourse.aspx.cs" Inherits="Admin_FindACourse" Title="Admin: Find a Course | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->

    <script type="text/javascript" language="javascript" src="../javascript/SearchCourse.js"></script>

    <div class="title">
        Find a Course
    </div>
    <div style="text-align: left;">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
    </div>
    <div>
    <asp:TextBox ID="txtKeyword" runat="server" MaxLength="80" CssClass="frmMainSearch" placeholder="Search Here"></asp:TextBox>
    </div>
    
    <table class="LeftAlignTable">
        <tbody>
            <%--<tr class="hide">
                <td class="frmElement2" style="width: 200px;">
                    Course Title
                </td>
                <td>
                    <!--validator to validate checkboxes-->
                    <asp:CheckBox ID="chkTitle" runat="server" Checked="true" /><asp:CustomValidator
                        ID="CustomValidator3" runat="server" ClientValidationFunction="validateSearchScope"
                        ControlToValidate="txtKeyword" ErrorMessage="Select at least one check box to continue your search."
                        ForeColor="White">*</asp:CustomValidator>
                </td>
            </tr>
            <tr class="hide">
                <td class="frmElement2">
                    Course Description
                </td>
                <td>
                    <asp:CheckBox ID="chkDescription" runat="server" Checked="true" />
                </td>
            </tr>
            <tr class="hide">
                <td class="frmElement2">
                    Learning Outcomes
                </td>
                <td>
                    <asp:CheckBox ID="chkLearningOutcomes" runat="server" Checked="true" />
                </td>
            </tr>--%>
            <%--<tr>
                <td class="frmElement2" colspan="2">
                    Educational provider<br />
                    (Hold down Control or Apple key to select more than one)<br />
                    <br />
                    <asp:ListBox ID="lbEducationProvider" runat="server" SelectionMode="Multiple"></asp:ListBox>
                </td>
                
            </tr>--%>
            <tr>
                <td colspan="2">
                    <div style="padding-bottom:5px">
                        Educational Provider<br />
                        (Hold down Control or Apple key to select more than one)
                    </div>
                    <asp:ListBox ID="lbEducationProvider" runat="server" SelectionMode="Multiple" CssClass="solidBorder"></asp:ListBox>
                </td>
            </tr>
           <%-- <tr>
                <td class="frmElement2" colspan="2">
                   
                    <asp:DropDownList ID="ddlSubject" runat="server">
                    </asp:DropDownList>
                </td>
                
            </tr>--%>
             <tr>
                <td class="defaultPageTableRow" colspan="2">
                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="solidBorder">
                    </asp:DropDownList>
                </td>
            </tr>
           
            <tr>
                <td class="defaultPageTableRow" colspan="2">
                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="solidBorder">
                    </asp:DropDownList>
                </td>
            </tr>
                       
            <tr>
                <td class="defaultPageTableRow" colspan="2">
                    <div class="smallPadding">Available Courses from</div>
                    Start Date
                    <!--Start date calendar control-->
                    <asp:CustomValidator Display="None" ID="CustomValidator1" runat="server" ClientValidationFunction="DateValidation"
                        ControlToValidate="txtStartDate" ErrorMessage="Enter valid start date." ForeColor="White"
                        SetFocusOnError="True">*</asp:CustomValidator>
                    <asp:TextBox ID="txtStartDate" runat="server" MaxLength="12" CssClass="frmDateBox"></asp:TextBox>&nbsp;<img
                        alt="Calendar" title="Calendar" onclick="loadCalendar(this, '<%=txtStartDate.ClientID %>', 'mm/dd/yyyy')"
                        src="../images/icon_calendar.jpg" class="CalenderIcon" />
                </td>
            </tr>
            <tr>
                <td class="defaultPageTableRow" colspan="2">
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
                
                <td class="moreLineHeight" colspan="2">
                    <!--Search button to initiate search-->
                    <asp:Button ID="btnFindCourses" runat="server" Text="FIND COURSES" OnClick="btnFindCourses_Click"
                        ToolTip="FIND COURSES" CssClass="commonButton btn153" />
                </td>
            </tr>
        </tbody>
    </table>

    <script type="text/javascript">
        ///Select existing value of keyword text box
        document.getElementById('<%=txtKeyword.ClientID%>').select();
    </script>

</asp:Content>
