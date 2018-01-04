<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ManageParticipants.aspx.cs" Inherits="Provider_ManageParticipants"
    Title="Manage Participants | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->
    <!--Start javascript-->
    <script type="text/javascript" language="javascript" src="/laces/javascript/perticipant.js"></script>
    
    <!--End javascript-->
    <div style="text-align: right;" class="HeaderText">
        Add Participants
    </div>
    
    <!--Hidden field used to check weather save or not the participant list form-->
    <div id="message" style="text-align: left;">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
        <input id="HiddenSaveInformation" type="hidden" value="N" runat="server" />
        <input id="HiddenRedirectUrl" type="hidden" value="1" runat="server" />
    </div>
    
    <!-- [Start] Left Side Add Perticipants HTML table -->
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="text-align: center; background-color: #963434" class="tblManageParticipants">
                Last</td>
            <td style="text-align: center; background-color: #963434" class="tblManageParticipants">
                First</td>
            <td style="text-align: center; background-color: #963434" class="tblManageParticipants">
                ASLA</td>
            <td style="text-align: center; background-color: #963434" class="tblManageParticipants">
                CLARB</td>
            <td style="text-align: center; background-color: #963434" class="tblManageParticipants">
                FL</td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row11" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row12" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row13" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row14" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row15" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row21" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row22" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row23" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row24" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row25" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row31" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row32" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row33" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row34" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row35" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row41" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row42" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row43" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row44" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row45" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row51" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row52" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row53" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row54" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row55" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row61" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row62" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row63" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row64" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row65" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row71" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row72" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row73" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row74" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row75" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row81" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row82" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row83" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row84" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row85" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row91" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row92" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row93" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row94" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row95" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row101" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row102" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row103" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row104" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row105" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row111" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row112" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row113" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row114" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row115" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row121" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row122" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row123" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row124" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row125" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row131" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row132" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row133" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row134" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row135" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row141" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row142" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row143" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row144" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row145" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row151" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row152" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row153" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row154" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row155" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row161" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row162" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row163" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row164" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row165" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row171" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row172" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row173" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row174" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row175" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row181" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row182" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row183" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row184" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row185" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd">
                <asp:TextBox ID="row191" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row192" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row193" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row194" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row195" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tblMPtd" >
                <asp:TextBox ID="row201" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row202" CssClass="frmMPTextBox" runat="server" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row203" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row204" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
            <td class="tblMPtd">
                <asp:TextBox ID="row205" CssClass="frmMPTextBox" runat="server" Width="51px" MaxLength="50"></asp:TextBox></td>
        </tr>
    </table>
    <!-- [End] Left Side Add Perticipants HTML table -->
    <!-- [Start] Left Side Button HTML table -->
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="text-align: center" class="tblMPtd">
                <asp:Button ID="btnSaveAddMore" runat="server" Text="Save & Add More" CssClass="commonButton btn133"
                    OnClick="btnSaveAddMore_Click" OnClientClick="javascript:return checkAllTextForButton()" /></td>
            <td style="text-align: center" class="tblMPtd">
                <asp:Button ID="btnSaveFinish" runat="server" Text="Save & Finish" CssClass="commonButton btn107"
                    OnClick="btnSaveFinish_Click" OnClientClick="javascript:return checkAllTextForButton()"  /></td>
        </tr>
    </table>
    <!-- [End] Left Side Button HTML table -->
    <!--End Left Content Place Holder-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightane" runat="Server">
    <!--Start Right Content Place Holder-->
    <div style="text-align: right;" class="HeaderText">
        Existing Participants</div>
    <div id="dvParticipantList" runat="server">
    </div>
    <!--End Right Content Place Holder-->
</asp:Content>
