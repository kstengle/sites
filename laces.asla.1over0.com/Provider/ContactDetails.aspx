<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="ContactDetails.aspx.cs" Inherits="Provider_ContactDetails" Title="Contact Information | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->
    <!--Add javascript-->

    <script type="text/javascript" language="javascript" src="/laces/javascript/provider.js"></script>

    <div id="PageHeader" runat="server" class="title">
        Contact Information</div>
    <!-- Any Message of the Page or any Error Validation Starts -->
    <div>
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label><asp:ValidationSummary
            ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
    </div>
    <!-- Any Message of the Page or any Error Validation Ends -->
    <!-- [Start] Left Side form field HTML table -->
    <table class="LeftAlignTable">
        <tr>
            <td class="frmElement2">
                Organization</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="OrganizationName" MaxLength="200" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="OrganizationName"
                    ErrorMessage="Enter organization name." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Street Address</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="OrganizationStreetAddress" MaxLength="200" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="OrganizationStreetAddress"
                    ErrorMessage="Enter street address." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                City</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="OrganizationCity" MaxLength="100" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="OrganizationCity"
                    ErrorMessage="Enter city." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                State/Province</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="OrganizationState" MaxLength="50" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Zip</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="OrganizationZip" MaxLength="15" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="OrganizationZip"
                    ErrorMessage="Enter zip." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="OrganizationZip"
                    ErrorMessage="Enter valid zip." ValidationExpression="\d{5}(-\d{4})?" runat="server"
                    SetFocusOnError="True" Display="None"></asp:RegularExpressionValidator>--%>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Country</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="OrganizationCountry" MaxLength="100" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="OrganizationCountry"
                    ErrorMessage="Enter country." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
        </tr>
        <tr>
            <td class="frmElement2">
                Phone</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="OrganizationPhone" MaxLength="15" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Fax</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="OrganizationFax" MaxLength="15" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Website</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="OrganizationWebSite" MaxLength="100" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="title2">
                    Primary Contact</div>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Individual’s Name</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="ApplicantName" MaxLength="100" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ApplicantName"
                    ErrorMessage="Enter name." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Position</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="ApplicantPosition" MaxLength="100" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ApplicantPosition"
                    ErrorMessage="Enter position." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Phone</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="ApplicantPhone" MaxLength="15" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Fax</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="ApplicantFax" MaxLength="15" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Email</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="ApplicantEmail" MaxLength="100" runat="server" CssClass="frmProviderDetailsTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ApplicantEmail"
                    ErrorMessage="Enter email." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ApplicantEmail"
                    ErrorMessage="Enter valid email." SetFocusOnError="True" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Email Confirm</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="ApplicantEmailConfirm" runat="server" CssClass="frmProviderDetailsTextBox" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ApplicantEmailConfirm"
                    ErrorMessage="Enter email confirm." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ApplicantEmailConfirm"
                    ControlToCompare="ApplicantEmail" ErrorMessage="Email and confirm email do not match."
                    SetFocusOnError="True" Display="None"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Password</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="Password" runat="server" CssClass="frmProviderDetailsTextBox" TextMode="Password"
                    MaxLength="40"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="Password"
                    ErrorMessage="Enter password." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="HidPass" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
                Password Confirm</td>
            <td class="moreLineHeight">
                <asp:TextBox ID="PasswordConfirm" runat="server" CssClass="frmProviderDetailsTextBox"
                    TextMode="Password" MaxLength="40"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="PasswordConfirm"
                    ControlToCompare="Password" ErrorMessage="Password and confirm password do not match."
                    SetFocusOnError="True" Display="None"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="PasswordConfirm"
                    ErrorMessage="Enter password confirm." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="frmElement2">
            </td>
            <td class="moreLineHeight">
                <br />
                <asp:Button ID="Button1" runat="server" Text="SAVE CHANGES" CssClass="commonButton btn133"
                    OnClick="btnSaveDetails_Click" OnClientClick="ResetErrorMsg();needToConfirm = false;setTimeout('resetFlag()', 250);"
                    ToolTip="SAVE CHANGES" />
            </td>
        </tr>
    </table>
    <!-- [End] Left Side form field HTML table -->
</asp:Content>
