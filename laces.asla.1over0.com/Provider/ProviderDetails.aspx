<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="ProviderDetails.aspx.cs" Inherits="Provider_ProviderDetails" Title="Contact Information | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->
    <!--Add javascript-->

    <script type="text/javascript" language="javascript" src="/laces/javascript/provider.js"></script>

    <div class="title">
        Contact Information</div>
    <!-- Any Message of the Page or any Error Validation Starts -->
    <div style="padding-left: 214px;">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label><asp:ValidationSummary
            ID="ValidationSummary2" runat="server" HeaderText="Please correct the following error(s):" />
    </div>
    <!-- Any Message of the Page or any Error Validation Ends -->
    <!-- [Start] Left Side form field HTML table -->
    <table class="LeftAlignTable">
        <tr>
            <td class="frmElement2">
                Organization</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtOrganization" runat="server" CssClass="frmProviderDetailsTextBox"
                    MaxLength="256"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorName"
                        runat="server" ControlToValidate="txtOrganization" ErrorMessage="Enter organization name."
                        SetFocusOnError="True" ForeColor="White">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Street Address</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtStreetAddress" runat="server" CssClass="frmProviderDetailsTextBox"
                    MaxLength="256"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorStreet"
                        runat="server" ControlToValidate="txtStreetAddress" ErrorMessage="Enter street address."
                        SetFocusOnError="True" ForeColor="White">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                City</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtCity" runat="server" CssClass="frmProviderDetailsTextBox" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="frmElement2">
                State/Province</td>
            <td class="moreLineHeight">
                <asp:CompareValidator ID="CompareValidatorState" runat="server" ErrorMessage="Select a state/province."
                    SetFocusOnError="True" ForeColor="White" ControlToValidate="drpState" Operator="NotEqual"
                    ValueToCompare="-State List-" Display="None">*</asp:CompareValidator>
                <asp:DropDownList ID="drpState" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Zip</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtZip" runat="server" CssClass="frmProviderDetailsTextBox" MaxLength="50"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidatorPostalCode" runat="server"
                    ErrorMessage="Enter zip code." SetFocusOnError="True" ForeColor="White" ControlToValidate="txtZip">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Country</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtCountry" runat="server" CssClass="frmProviderDetailsTextBox"
                    MaxLength="100"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="frmElement2">
                Phone</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtPhone" runat="server" CssClass="frmProviderDetailsTextBox" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="frmElement2">
                Fax</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtFax" runat="server" CssClass="frmProviderDetailsTextBox" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="frmElement2">
                Website</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtWebsite" runat="server" CssClass="frmProviderDetailsTextBox"
                    MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="title">
                    Primary Contact</div>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Individual’s Name</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtIndName" runat="server" CssClass="frmProviderDetailsTextBox"
                    MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="frmElement2">
                Position</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtIndPosition" runat="server" CssClass="frmProviderDetailsTextBox"
                    MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="frmElement2">
                Phone</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtIndPhone" runat="server" CssClass="frmProviderDetailsTextBox"
                    MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="frmElement2">
                Fax</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtIndFax" runat="server" CssClass="frmProviderDetailsTextBox" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="frmElement2">
                Email</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="frmProviderDetailsTextBox" MaxLength="64"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Enter email."
                    SetFocusOnError="True" ForeColor="White">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid email."
                        SetFocusOnError="True" ForeColor="White" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ControlToValidate="txtEmail">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Password</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="frmProviderDetailsTextBox"
                    TextMode="Password" MaxLength="32"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        runat="server" ControlToValidate="txtPassword" ErrorMessage="Enter password."
                        SetFocusOnError="True" ForeColor="White">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Password Confirm</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="txtPasswordConfirm" runat="server" CssClass="frmProviderDetailsTextBox"
                    TextMode="Password" MaxLength="32"></asp:TextBox><asp:CompareValidator ID="CompareValidator1"
                        runat="server" ErrorMessage="Confirm password does not match." ControlToValidate="txtPasswordConfirm"
                        ControlToCompare="txtPassword" ForeColor="White" SetFocusOnError="True">*</asp:CompareValidator><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPasswordConfirm"
                            ErrorMessage="Enter password confirm." SetFocusOnError="True" ForeColor="White">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
            </td>
            <td class="moreLineHeight">
                <br />
                <asp:Button ID="Button1" runat="server" Text="SAVE DETAILS" CssClass="commonButton btn107"
                    OnClick="btnSaveDetails_Click" OnClientClick="ResetErrorMsg()" ToolTip="SAVE DETAILS" />
            </td>
        </tr>
    </table>
    <!-- [End] Left Side form field HTML table -->
</asp:Content>
