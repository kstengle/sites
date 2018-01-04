<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="RequestCourseCode.aspx.cs" Inherits="RequestCourseCode" Title="Request Course Code Type| LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!-- After sending mail to admin, alert user. -->
    <%=message %>
    <!--Header-->
    <div class="title">
        Request Course Code Type
    </div>
    <!--Information text-->
    <p class="paddingLeft100">
        If the course code you would like to assign is not available, please use the form
        below to request the addition of the course code to the
        <%=LACESConstant.LACES_TEXT%>
        system. Please note, that the
        <%=LACESConstant.LACES_TEXT%>
        system is designed for use by many different course providers, each with their own
        needs. Any code requests will be thoroughly evaluated by the ASLA administrators.
        Your requested code may not be added to the system, but it will be taken under consideration.</p>
    <!-- Any Message of the Page or any Error Validation Starts -->
    <div class="paddingLeft245">
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label><asp:ValidationSummary
            ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
    </div>
    <div class="paddingLeft40">
        <!--Request form-->
        <table class="LeftAlignTable">
            <tbody>
                <tr>
                    <td class="frmElement2">
                        Requested Code Type</td>
                    <td>
                        <label>
                            <asp:TextBox ID="txtCodeType" runat="server" CssClass="frmDateBox" MaxLength="255"
                                Width="209px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCodeType"
                                ErrorMessage="Enter code type." ForeColor="White" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td class="frmElement2 paddingSmall">
                        Reason</td>
                    <td>
                        <asp:TextBox ID="txtReason" runat="server" Columns="32" CssClass="frmDateBox" Rows="15"
                            TextMode="MultiLine" Width="209px" Height="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtReason"
                            ErrorMessage="Enter reason." ForeColor="White" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <br />
                        <asp:Button ID="btnRequest" runat="server" CssClass="commonButton btn194" Text="REQUEST CODE TYPE"
                            OnClick="btnRequest_Click" /></td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
