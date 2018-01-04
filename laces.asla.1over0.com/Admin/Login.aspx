<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_Login" Title="Admin: Login | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">    
                <div id="lftPane">
                <div align="left" class="uk-width-1-1 title">
                    Administrator Sign In</div>
                </div>
                <div class="uk-width-1-1 ErrorSummery">            
                <asp:Label ID="lblErrorSummary" runat="server"></asp:Label>
                </div>
                <div class="uk-grid">
                    <div class="uk-width-1-2">
                        <asp:TextBox ID="txtEmail" CssClass="LoginTextBox" runat="server" MaxLength="64"></asp:TextBox>
                    </div>
                    <div class="uk-width-1-2 LoginLabel">
                        User Name
                    </div>
                    <div class="uk-width-1-2">
                        <asp:TextBox ID="txtPassword" CssClass="LoginTextBox" TextMode="Password" runat="server" MaxLength="32"></asp:TextBox>
                    </div>
                    <div class="uk-width-1-2 LoginLabel">
                        Password
                    </div>
                </div>    
                 <div>
                <asp:Button ID="btnSignIn" runat="server" CssClass="commonButton btn107" Text="SIGN IN"
                    OnClientClick="return ValidateProviderLogin()" OnClick="btnSignIn_Click" />
                </div>            
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightane" runat="Server">
    <p class="moreLineHeight">        
        Welcome to the Landscape Architecture Continuing Education System™ Administrator
        Portal. Please login to access the site.
    </p>

</asp:Content>
