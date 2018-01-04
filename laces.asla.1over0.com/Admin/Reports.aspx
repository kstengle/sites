<%@ Page Title="Reports | LA CES™" Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Admin_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">
<script type="text/javascript" language="javascript" src="../javascript/SearchCourse.js"></script>
<asp:Panel id="uiPnlClarbParams" runat="server">
<div id="Div1" class="title" runat="server">
        LA CES Administrator Report
    </div>
            <asp:ImageButton vspace="20" ImageUrl="~/images/downloadReport_BTN.gif" ID="uiImgRunAdminReports" runat="server" Text="Submit" OnClick="GenerateAdminReports" />

<div id="dvCourseDetails" class="title" runat="server">
        CLARB Report
    </div>
    <hr />
    <table cellspacing="3" cellpadding="3" border="0">
        <tr>
            <td class="defaultPageTableRow">
                <span style="padding-right: 7px">Start Date
            </td>
            <td class="defaultPageTableRow">
            <asp:TextBox autocomplete="off" ID="txtStartDate" CssClass="frmDateBox" runat="server"
                        Width="120px" MaxLength="20"></asp:TextBox>
                    <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=txtStartDate.ClientID %>', 'mm/dd/yyyy')"
                        src="../images/icon_calendar.jpg"  class="CalenderIcon" />
            </td>
        </tr>
        <tr>
            <td class="defaultPageTableRow">
            <span style="padding-right: 7px">End Date</span>
            </td>
            <td class="defaultPageTableRow">
                <asp:TextBox autocomplete="off" ID="txtEndDate" CssClass="frmDateBox" runat="server"
                        Width="120px" MaxLength="20"></asp:TextBox>
                    <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=txtEndDate.ClientID %>', 'mm/dd/yyyy')"
                        src="../images/icon_calendar.jpg"  class="CalenderIcon" />
            </td>
        </tr>
        <tr>
            <td class="defaultPageTableRow" colspan="2">
            <span style="padding-right: 7px">Clarb Number</span>
            <asp:RadioButtonList RepeatDirection="Horizontal" RepeatLayout="Table" ID="uiRblClarbNumber" runat="server">
                <asp:ListItem Text="Attendees with CLARB Numbers" Value="1"></asp:ListItem>
                <asp:ListItem Text="ALL Attendees" Value="0"></asp:ListItem>
            </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="searchTableLeftTd">
                <!---CourseID-->
            </td>
            <td class="searchTableRightTd">
                <asp:TextBox ID="txtCourseID" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <asp:ImageButton ImageUrl="~/images/downloadReport_BTN.gif" ID="uiBtnGenerateReport" runat="server" Text="Submit" OnClick="GenerateClarbReports" />
            </td>
        </tr>        
            <tr>
            <td colspan="2">
            <asp:Literal ID="uiLitMessage" runat="server"></asp:Literal>
            </td>
        </tr>        
    </table>
    <hr />
   <div id="Div2" class="title" runat="server">CLARB report by Upload Date (From 6/11/13)</div>
    <table cellspacing="3" cellpadding="3" border="0">
        <tr>
            <td class="defaultPageTableRow">
                <span style="padding-right: 7px">Start Date
            </td>
            <td class="defaultPageTableRow">
            <asp:TextBox autocomplete="off" ID="uiTxtClarbReportByUploadStartDate" CssClass="frmDateBox" runat="server"
                        Width="120px" MaxLength="20"></asp:TextBox>
                    <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=uiTxtClarbReportByUploadStartDate.ClientID %>', 'mm/dd/yyyy')"
                        src="../images/icon_calendar.jpg"  class="CalenderIcon" />
            </td>
        </tr>
        <tr>
            <td class="defaultPageTableRow">
            <span style="padding-right: 7px">End Date</span>
            </td>
            <td class="defaultPageTableRow">
                <asp:TextBox autocomplete="off" ID="uiTxtClarbReportByUploadEndDate" CssClass="frmDateBox" runat="server"
                        Width="120px" MaxLength="20"></asp:TextBox>
                    <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=uiTxtClarbReportByUploadEndDate.ClientID %>', 'mm/dd/yyyy')"
                        src="../images/icon_calendar.jpg"  class="CalenderIcon" />
            </td>
        </tr>
        <tr>
            <td class="defaultPageTableRow" colspan="2">
            <span style="padding-right: 7px">Has CLARB Number</span>
            <asp:RadioButtonList RepeatDirection="Horizontal" RepeatLayout="Table" ID="uiRblClarbReportHasNumber" runat="server">
                <asp:ListItem Text="Attendees with CLARB Numbers" Value="1"></asp:ListItem>
                <asp:ListItem Text="ALL Attendees" Value="0"></asp:ListItem>
            </asp:RadioButtonList>
            </td>
        </tr>       
        <tr>
            <td colspan="2">
            <asp:ImageButton ImageUrl="~/images/downloadReport_BTN.gif" ID="uiBtnReportByUploadDate" runat="server" Text="Submit" OnClick="GenerateClarbReportsByUploadDate" />
            </td>
        </tr>        
            <tr>
            <td colspan="2">
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </td>
        </tr>        
    </table>
</asp:Panel>
<asp:Panel ID="uiPnlClarbResults" runat="server">
    <asp:DataGrid ID="uiDGClarbResults" runat="server" AutoGenerateColumns="true" ShowHeader="true"></asp:DataGrid>
    
    <br /><br />
    
            
</asp:Panel>
</asp:Content>

