<%@ Page Language="C#" MasterPageFile="~/Provider/ProviderMaster.master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage_ErrorPage" Title="LA CES™ - Error Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">    
    <!-- Display header -->
    <div align="right" class="ProviderHeader">Error</div>
    <!-- Display error message -->
    <p align="left"><asp:Label ID="lblErrorMessage" runat="server" Text="The system encountered some problems. Please try again later. If the problem continues contact with the system administrator." ForeColor="Red"></asp:Label></p>
    <!-- Display Go Back link -->
    <a id="back" href="javascript:history.go(-1);" style="cursor: pointer;">Back</a>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightane" Runat="Server">
    &nbsp;
</asp:Content>

