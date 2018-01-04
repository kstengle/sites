<%@ Page Title="" Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true" CodeFile="AJAXSearchCourses.aspx.cs" Inherits="AJAXSearchCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">
    <asp:ScriptManager ID="uiSM" runat="server"></asp:ScriptManager>
    Keyword:<asp:TextBox ID="uiTxtKeyword" runat="server"></asp:TextBox><br />
    Subjects:<asp:TextBox ID="uiTxtSubjects" runat="server"></asp:TextBox><br />
    State/Province:<asp:TextBox ID="uiTxtStateProvince" runat="server"></asp:TextBox><br />
    Start Date:<asp:CustomValidator Display="None" ID="CustomValidator1" runat="server" ClientValidationFunction="DateValidation"
                        ControlToValidate="txtStartDate" ErrorMessage="Enter valid start date." ForeColor="White"
                        SetFocusOnError="True">*</asp:CustomValidator>
                    <asp:TextBox ID="txtStartDate" runat="server" MaxLength="12" CssClass="frmDateBox"></asp:TextBox>&nbsp;<img
                        alt="Calendar" title="Calendar" onclick="loadCalendar(this, '<%=txtStartDate.ClientID %>', 'mm/dd/yyyy')"
                        src="images/icon_calendar.jpg" class="CalenderIcon" />
    <br />
    End Date: <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateValidation"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Enter valid end date."
                        ForeColor="White" SetFocusOnError="True">*</asp:CustomValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start date cannot be higher than end date."
                        ForeColor="White" Operator="GreaterThanEqual" Type="Date" SetFocusOnError="True">*</asp:CompareValidator>
                    <asp:TextBox ID="txtEndDate" runat="server" MaxLength="12" CssClass="frmDateBox"></asp:TextBox>&nbsp;<img
                        alt="Calendar" title="Calendar" onclick="loadCalendar(this, '<%=txtEndDate.ClientID %>', 'mm/dd/yyyy')"
                        src="images/icon_calendar.jpg"  class="CalenderIcon" />
    <asp:LinkButton ID="uiLnkSearch" runat="server" OnClick="FilterCourses" Text="Search"></asp:LinkButton>
    <asp:UpdatePanel ID="uiUpdatePnl" runat="server">
        <ContentTemplate>
            <asp:CheckBoxList ID="uiChxBoxListStatus" runat="server" OnSelectedIndexChanged="FilterCourses" RepeatDirection="Horizontal" AutoPostBack="true">
            </asp:CheckBoxList>
            <asp:Repeater ID="uiRptCourses" runat="server">
                <ItemTemplate>
                    <%#Eval("Status") %>|<%#Eval("Title") %><br />
                    StartDate:<%#DateTime.Parse(Eval("StartDate").ToString()).ToShortDateString() %><br />
                    EndDate:<%#DateTime.Parse(Eval("EndDate").ToString()).ToShortDateString() %><br />

                    
                    <br />
                </ItemTemplate>

            </asp:Repeater>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

