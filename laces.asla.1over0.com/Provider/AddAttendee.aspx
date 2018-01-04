<%@ Page Title="" Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true" CodeFile="AddAttendee.aspx.cs" Inherits="Provider_AddAttendee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">
    <div class="uk-grid formGrid">
        <div class="uk-width-1-1 title"><div ID="dvHeader" runat="server"></div></div>
        <div class="uk-width-1-5">First Name</div>
        <div class="uk-width-4-5"><asp:TextBox ID="uiTxtFirstName" runat="server" CssClass="frmMainSearch"></asp:TextBox></div>
        <div class="uk-width-1-5">Middle Name</div>
        <div class="uk-width-4-5"><asp:TextBox ID="uiTxtMiddleName" runat="server" CssClass="frmMainSearch"></asp:TextBox></div>
        <div class="uk-width-1-5">Last Name</div>
        <div class="uk-width-4-5"><asp:TextBox ID="uiTxtLastName" runat="server" CssClass="frmMainSearch"></asp:TextBox></div>
        <div class="uk-width-1-5">Email Address</div>
        <div class="uk-width-4-5"><asp:TextBox ID="uiTxtEmail" runat="server" CssClass="frmMainSearch"></asp:TextBox></div>
        <div class="uk-width-1-5">ASLA #</div>
        <div class="uk-width-4-5"><asp:TextBox ID="uiTxtASLANumber" runat="server" CssClass="frmMainSearch"></asp:TextBox></div>
        <div class="uk-width-1-5">CLARB #</div>
        <div class="uk-width-4-5"><asp:TextBox ID="uiTxtCLARBNumber" runat="server" CssClass="frmMainSearch"></asp:TextBox></div>
        <div class="uk-width-1-5">CSLA #</div>
        <div class="uk-width-4-5"><asp:TextBox ID="uiTxtCSLANumber" runat="server" CssClass="frmMainSearch"></asp:TextBox></div>
        <div class="uk-width-1-5">FL License #</div>
        <div class="uk-width-4-5"><asp:TextBox ID="uiTxtFLLicenseNumber" runat="server" CssClass="frmMainSearch"></asp:TextBox></div>
        <div class="uk-width-1-2">
            <asp:Button ID="btnClear" runat="server" Text="Clear" ToolTip="Clear" CssClass="commonButton btn84" OnClick="btnClear_Click" style="float:left;" />                    
            <asp:Button ID="btnSave" runat="server" Text="Save" ToolTip="Save" CssClass="commonButton btn84" OnClick="btnSave_Click" style="float:left;margin-left:10px;" />
         </div>
    </div>

</asp:Content>

