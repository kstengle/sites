<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true" CodeFile="FindProviders.aspx.cs" Inherits="Admin_FindProviders" Title="Admin: Find Providers | LA CES™"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->
    <div class="title" align="right">
        Find Providers
    </div>   
    <div class="moreLineHeight">
        Search for course providers by their name using the search box bellow.
    </div> 
    <div>
        <br />
        <!--Search keywords text box-->        
        <asp:TextBox ID="txtKeyword" runat="server" MaxLength="80" CssClass="frmMainSearch" placeholder="Search Here"></asp:TextBox>
        <br /><br />
        <asp:Button ID="btnFindProviders" runat="server" Text="FIND PROVIDERS" ToolTip="FIND PROVIDERS"
                        CssClass="commonButton btn153" OnClick="btnFindProviders_Click"/>
    </div>
    <script type="text/javascript">
        ///Select existing value of keyword text box
        document.getElementById('<%=txtKeyword.ClientID%>').select();
    </script>
    <!--End Left Content Place Holder-->
</asp:Content>

