<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="usercontrols_Footer" %>
<div class="mainContent">
    <div class="uk-grid footerlogos">
        <div class="uk-width-1-2 uk-width-small-1-4 uk-width-large-1-6">
            <a href="http://www.asla.org/">
                <img style="border: 0" alt="asla logo" src="~/images/logo/asla_logo.jpg" runat="server" /></a>
        </div>
        <div class="uk-width-1-2 uk-width-small-1-4 uk-width-large-1-6">
            <a href="http://www.csla.ca/">
                <img style="border: 0" alt="csla logo" src="~/images/logo/csl_logo.jpg" runat="server" /></a>
        </div>
        <div class="uk-width-1-2 uk-width-small-1-4 uk-width-large-1-6">
            <a href="http://www.thecela.org/">
                <img style="border: 0" alt="cela logo" src="~/images/logo/cela_logo.png" runat="server" /></a>
        </div>
        <div class="uk-width-1-2 uk-width-small-1-4 uk-width-large-1-6">
            <a href="http://www.clarb.org/">
                <img style="border: 0" alt="clarb logo" src="~/images/logo/clarb_logo.png" runat="server" /></a>
        </div>
        <div class="uk-width-1-2 uk-width-small-1-4 uk-width-large-1-6">
            <a href="http://www.asla.org/AccreditationLAAB.aspx#About_LAAB">
                <img style="border: 0" alt="laab logo" src="~/images/logo/laab_logo.png" runat="server" /></a>
        </div>
        <div class="uk-width-1-2 uk-width-small-1-4 uk-width-large-1-6">
            <a href="http://www.lafoundation.org/">
                <img style="border: 0" alt="laf logo" src="~/images/logo/laf_logo.png" runat="server" /></a>
        </div>
    </div>
    <br />
    <div class="footer">
    </div>
    <div style="text-align: center; padding-left: 42px" class="mainWidth appProviderDiv">
        <!--Footer Text-->
        <p><a href="mailto:laces@asla.org">Contact Us</a></p>
        <p style="color: #999999;">American Society of Landscape Architects &copy; Copyright
            <asp:Literal ID="uiLitCopyrightYear" runat="server"></asp:Literal>
            All rights reserved.</p>

    </div>
</div>
