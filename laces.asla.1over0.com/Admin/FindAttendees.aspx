<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="FindAttendees.aspx.cs" Inherits="Admin_FindAttendees" Title="Admin: Find Attendees | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->

    <script type="text/javascript">
    
    </script>

    <div class="title" align="right">
        Find Attendees
    </div>
    <div class="moreLineHeight">
        You can find course attendees to look up a single attendee and determine what
        courses they have taken.
    </div>
    <br />
    <table border="0" cellpadding="1" cellspacing="1" width="40%" >
        <tbody>
            <tr>
                <td class="frmElement5">
                    First Name
                </td>
                <td>
                    <!--Textbox to get First Name-->
                    <asp:TextBox ID="txtFirstName" CssClass="frmBigTextBox" MaxLength="100" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement5">
                    Last Name
                </td>
                <td>
                    <!--Textbox to get Last Name-->
                    <asp:TextBox ID="txtLastName" CssClass="frmBigTextBox" MaxLength="100" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td class="frmElement5">
                    Middle Initial
                </td>
                <td>
                    <!--Textbox to get Last Name-->
                    <asp:TextBox ID="txtMiddleName" CssClass="frmBigTextBox" MaxLength="100" runat="server"></asp:TextBox>
                </td>
            </tr>

            
            <tr>
                <td class="frmElement5">
                    ASLA
                </td>
                <td>
                    <!--Textbox to get ASLA Number-->
                    <asp:TextBox ID="txtASLA" CssClass="frmSmallTextBox" MaxLength="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement5">
                    CLARB
                </td>
                <td>
                    <!--Textbox to get CLARB Number-->
                    <asp:TextBox ID="txtCLARB" CssClass="frmSmallTextBox" MaxLength="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement5">
                    CSLA
                </td>
                <td>
                    <!--Textbox to get CLARB Number-->
                    <asp:TextBox ID="txtCSLA" CssClass="frmSmallTextBox" MaxLength="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement5">
                    FL
                </td>
                <td>
                    <!--Textbox to get FL State Number-->
                    <asp:TextBox ID="txtFL" CssClass="frmSmallTextBox" MaxLength="50" runat="server"></asp:TextBox>
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
                <td class="frmElement5">
                </td>
                <td>
                    <asp:Button ID="btnFindParticipants" runat="server" Text="FIND ATTENDEES" ToolTip="FIND ATTENDEES"
                        CssClass="commonButton btn153" OnClick="btnFindParticipants_Click" />
                </td>
            </tr>
        </tbody>
    </table>
    <!--End Left Content Place Holder-->
</asp:Content>
