<%@ Page Title="" Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true" CodeFile="UploadCourses.aspx.cs" Inherits="Provider_UploadCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">
<asp:FileUpload ID="FileUpload1" runat="server" />

<asp:LinkButton ID="uiLBUpload" runat="server" Text="Upload Courses" OnClick="btnUpload_Click"></asp:LinkButton>
    <br /><asp:Label ID="uiLblResults" runat="server"></asp:Label>
</asp:Content>

