<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="EditAttendees.aspx.cs" Inherits="Provider_EditAttendees" Title="Edit Attendee | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->
    <!--Start javascript-->
    <style>
        .attendeegrid div {
            margin-left: 0 !important;
            padding-left: 0 !important;
        }
    </style>
    <script type="text/javascript" language="javascript" src="/laces/javascript/perticipant.js"></script>

    <script type="text/javascript">
        function checkFields(){return checkFieldsInUI("<%=txtLastName.ClientID%>","<%=txtFirstName.ClientID%>","<%=lblMsg.ClientID%>");}
    </script>

    <!--End javascript-->
    <div class="title" id="title" runat="server">
        Edit Attendee
    </div>
    <br />
    <div id="divRightMessage"  runat="server">
        Use this page to edit an attendee who attended the [course name] course. Removing
        an attendee from the course is a permanent action, and can not be undone.
    </div>
    <!-- Any Message of the Page or any Error Validation Starts -->
    <div id="message">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
    </div>
    <br />
    <div class="uk-grid attendeegrid" style="margin-left:0;">
        <div class="uk-width-2-10 tblManageParticipants subHead">Last</div>
        <div class="uk-width-1-10 tblManageParticipants subHead">First</div>
        <div class="uk-width-1-10 tblManageParticipants subHead">Middle</div>
        <div class="uk-width-1-10 tblManageParticipants subHead">ASLA</div>                
        <div class="uk-width-1-10 tblManageParticipants subHead">CLARB</div>                
        <div class="uk-width-1-10 tblManageParticipants subHead">CSLA</div>                
        <div class="uk-width-1-10 tblManageParticipants subHead">FL</div>                
        <div class="uk-width-2-10 tblManageParticipants subHead">Email</div>
        <div class="uk-width-1-1" style="margin-top:20px;"></div>
        <div class="uk-width-2-10 subHead"><asp:TextBox ID="txtLastName" CssClass="frmMainSearch" runat="server" MaxLength="50" Width="90%"></asp:TextBox></div>
        <div class="uk-width-1-10 subHead"><asp:TextBox ID="txtFirstName" CssClass="frmMainSearch" runat="server" MaxLength="50" Width="90%"></asp:TextBox></div>
        <div class="uk-width-1-10 subHead"><asp:TextBox ID="txtMiddleName" CssClass="frmMainSearch" runat="server"  MaxLength="50" Width="90%"></asp:TextBox></div>
        <div class="uk-width-1-10 subHead"><asp:TextBox ID="txtASLA" CssClass="frmMainSearch" runat="server" MaxLength="50" Width="90%"></asp:TextBox></div>                
        <div class="uk-width-1-10 subHead"><asp:TextBox ID="txtCLARB" CssClass="frmMainSearch" runat="server" Width="90%" MaxLength="50"></asp:TextBox></div>                
        <div class="uk-width-1-10 subHead"><asp:TextBox ID="txtCSLA" CssClass="frmMainSearch" runat="server" Width="90%" MaxLength="50"></asp:TextBox></div>                
        <div class="uk-width-1-10 subHead"><asp:TextBox ID="txtFL" CssClass="frmMainSearch" runat="server" Width="90%" MaxLength="50"></asp:TextBox></div>                
        <div class="uk-width-2-10 subHead"><asp:TextBox ID="txtEmail" CssClass="frmMainSearch" runat="server" Width="90%" MaxLength="50"></asp:TextBox></div>
        <div class="uk-width-3-10" style="margin-top:20px;">
        <asp:Button ID="btnRemovePerticipant" runat="server" Text="DELETE ATTENDEE"
                        ToolTip="DELETE ATTENDEE" CssClass="basicButton" OnClick="btnRemovePerticipant_Click" />
        </div>
        <div class="uk-width-3-10" style="margin-top:20px;">
                    <asp:Button ID="btnSaveFinish" runat="server" Text="SAVE" ToolTip="SAVE" CssClass="basicButton"
                        OnClick="btnSaveFinish_Click" OnClientClick="javascript:return checkFields()" />
        </div>
        </div>
        <!-- [End] Left Side Button HTML table -->
    </div>
    <!--End Left Content Place Holder-->
</asp:Content>
