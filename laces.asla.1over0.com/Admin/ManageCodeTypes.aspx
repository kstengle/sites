<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="ManageCodeTypes.aspx.cs" Inherits="Admin_AdminManageCodeTypes" Title="Admin: Manage Code Types | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <div align="right" class="title">
        Manage Code Types</div>
     <br />
    <!-- Any Message of the Page or any Error Validation Starts -->
    <div>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label><asp:ValidationSummary
            ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
    </div>
    <div>
        <asp:TextBox ID="txtCodeType" Width="100px" MaxLength="50" runat="server" CssClass="frmSmallTextBox"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Button ID="btnAddNewCodeType" runat="server" CssClass="commonButton btn194" Text="ADD NEW CODE TYPE"
            OnClick="btnAddNewCodeType_Click" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCodeType"
            ErrorMessage="Enter Code Type." SetFocusOnError="True" Display="None">*</asp:RequiredFieldValidator>
    </div>
       
    <br />
        <!--Code type list table to be populated from code behind-->
    <table id="tblParent" runat="server" width="339" class="LeftAlignTable">
    </table>
    
</asp:Content>
