<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="EditAttendees.aspx.cs" Inherits="Admin_EditAttendees" Title="Admin: Edit Attendee | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->
    <!--Start javascript-->

    <script type="text/javascript" language="javascript" src="/laces/javascript/perticipant.js"></script>

    <script type="text/javascript">
        function checkFields(){return checkFieldsInUI("<%=txtLastName.ClientID%>","<%=txtFirstName.ClientID%>","<%=lblMsg.ClientID%>");}
    </script>

    <!--End javascript-->
    <div class="title" id="title" runat="server">
        Edit Attendee
    </div>
    <br />
    <div id="divRightMessage" runat="server">
        Use this page to edit an attendee who attended the [course name] course. Removing
        an attendee from the course is a permanent action, and can not be undone.
    </div>
    <!-- Any Message of the Page or any Error Validation Starts -->
    <div id="message">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
    </div>
    <br />
    <div>
        <!-- [Start] Left Side Update Perticipants HTML table -->
        <table>
            <tr>
                <td style="text-align: left; background-color: #cccccc" class="tblManageParticipants subHead">
                    Last</td>
                <td style="text-align: left; background-color: #cccccc" class="tblManageParticipants subHead">
                    First</td>
                <td style="text-align: left; background-color: #cccccc" class="tblManageParticipants subHead">
                    Middle</td>
                <td style="text-align: left; background-color: #cccccc" class="tblManageParticipants subHead">
                    ASLA</td>
                <td style="text-align: left; background-color: #cccccc" class="tblManageParticipants subHead">
                    CLARB</td>
                <td style="text-align: left; background-color: #cccccc" class="tblManageParticipants subHead">
                    CSLA</td>
                <td style="text-align: left; background-color: #cccccc" class="tblManageParticipants subHead">
                    FL</td>
            </tr>
            <tr>
                <td class="tblMPtd2">
                    <asp:TextBox ID="txtLastName" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
                <td class="tblMPtd2">
                    <asp:TextBox ID="txtFirstName" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
                <td class="tblMPtd2">
                    <asp:TextBox ID="txtMiddleName" CssClass="frmMPTextBox" runat="server" Width="51px"
                        MaxLength="50"></asp:TextBox></td>
                <td class="tblMPtd2">
                    <asp:TextBox ID="txtASLA" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
                <td class="tblMPtd2">
                    <asp:TextBox ID="txtCLARB" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
                <td class="tblMPtd2">
                    <asp:TextBox ID="txtCSLA" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
                <td class="tblMPtd2">
                    <asp:TextBox ID="txtFL" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            </tr>
        </table>
        <!-- [End] Left Side Update Perticipants HTML table -->
        <!-- [Start] Left Side Button HTML table -->
        <table class="LeftAlignTable">
            <tr>
                <td style="text-align: center" class="tblMPtd2">
                    <asp:Button ID="btnRemovePerticipant" runat="server" Text="DELETE ATTENDEE"
                        ToolTip="DELETE ATTENDEE" CssClass="commonButton btn163" OnClick="btnRemovePerticipant_Click" /></td>
                <td style="text-align: center" class="tblMPtd2">
                    <asp:Button ID="btnSaveFinish" runat="server" Text="SAVE" ToolTip="SAVE" CssClass="commonButton btn107"
                        OnClick="btnSaveFinish_Click" OnClientClick="javascript:return checkFields()" /></td>
            </tr>
        </table>
        <!-- [End] Left Side Button HTML table -->
    </div>
</asp:Content>
