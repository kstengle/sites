<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="Renew.aspx.cs" Inherits="Visitor_Renew"
    Title="Approved Provider Application | LA CES™" %>

<%@ Register TagPrefix="pantheon" Namespace="Pantheon.Web.UI.Validators" Assembly="Pantheon.Web.UI.Validators" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
<script type="text/javascript" language="javascript" src="../javascript/ProviderApplication.js"></script>

    <div id="PageHeader" class="title" runat="server">
        Renew LA CES™ Approved Provider
    </div>
    <!-- Any Message of the Page or any Error Validation Starts -->
    <div style="text-align: left;">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
    </div>
            
    <div>
        <div class="APLabel">
        <a href="ApprovedProviderDetails.aspx">Click here</a> to review your application currently on file, then select an option below. <br />
        (If only your contact information needs to change, click on the "Contact Info" tab above and make changes there.)<br />
        </div>
        
        <asp:RadioButtonList ID="RadioHasChange" runat="server">
        <asp:ListItem Value="1" Text="My application has no changes" />
        <asp:ListItem Value="2" Text="My application needs to be updated" />
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
        runat="server" ControlToValidate="RadioHasChange" ErrorMessage="Select an option to continue." 
        SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
        
        <br />      
        <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" runat="server"  Text="CONTINUE" CssClass="commonButton btn107" />
        <br />  

    </div>
    
</asp:Content>
