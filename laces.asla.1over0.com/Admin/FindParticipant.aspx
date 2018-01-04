<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="FindParticipant.aspx.cs" Inherits="Admin_FindParticipant" Title="Admin: Find Participant | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->

    <script type="text/javascript">
    
    </script>

    <div class="title" align="right">
        Find Participants
    </div>
    <div class="moreLineHeight2">
        You can find course participants to look up a single participant and determine what
        courses they have taken.
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td class="frmElement2">
                    First Name
                </td>
                <td>
                    <!--Textbox to get First Name-->
                    <asp:TextBox ID="txtFirstName" CssClass="frmBigTextBox" MaxLength="100" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement2">
                    Last Name
                </td>
                <td>
                    <!--Textbox to get Last Name-->
                    <asp:TextBox ID="txtLastName" CssClass="frmBigTextBox" MaxLength="100" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement2">
                    ASLA
                </td>
                <td>
                    <!--Textbox to get ASLA Number-->
                    <asp:TextBox ID="txtASLA" CssClass="frmSmallTextBox" MaxLength="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement2">
                    CLARB
                </td>
                <td>
                    <!--Textbox to get CLARB Number-->
                    <asp:TextBox ID="txtCLARB" CssClass="frmSmallTextBox" MaxLength="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement2">
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
                <td class="frmElement2">
                </td>
                <td>
                    <asp:Button ID="btnFindParticipants" runat="server" Text="Find Participants" ToolTip="Find Participants"
                        CssClass="commonButton btn133" OnClick="btnFindParticipants_Click" />
                </td>
            </tr>
        </tbody>
    </table>
    <!--End Left Content Place Holder-->
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightane" runat="Server">
    <!--Start Right Content Place Holder-->
    <div class="title">
        &nbsp;
    </div>
   
    <!--End Right Content Place Holder-->
</asp:Content>--%>
