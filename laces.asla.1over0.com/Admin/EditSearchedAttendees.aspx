<%@ Page Title="" Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true" CodeFile="EditSearchedAttendees.aspx.cs" Inherits="Admin_EditSearchedAttendees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">
<div class="title" id="title" runat="server">
        Find Attendee <br />
    <asp:Literal ID="uiLitSearchedFor" runat="server"></asp:Literal>
    </div>
    <br />
    <div id="divRightMessage" runat="server">
        Use this page to edit an attendee based on search criteria:<br /><br />
    </div>
    <table border="0" cellpadding="1" cellspacing="1" width="40%" >
        <tbody>
            <tr>
                <td class="frmElement5">
                    First Name
                </td>
                <td>
                    <!--Textbox to get First Name-->
                    <asp:TextBox ID="txtFirstName" CssClass="frmBigTextBox" MaxLength="100" runat="server"></asp:TextBox>                    
                </td>
            </tr>
            <tr>
                <td class="frmElement5">
                    Last Name
                </td>
                <td>
                    <!--Textbox to get Last Name-->
                    <asp:TextBox ID="txtLastName" CssClass="frmBigTextBox" MaxLength="100" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td class="frmElement5">
                    Middle Initial
                </td>
                <td>
                    <!--Textbox to get Last Name-->
                    <asp:TextBox ID="txtMiddleName" CssClass="frmBigTextBox" MaxLength="100" runat="server"></asp:TextBox>
                </td>
            </tr>

            
            <tr>
                <td class="frmElement5">
                    ASLA
                </td>
                <td>
                    <!--Textbox to get ASLA Number-->
                    <asp:TextBox ID="txtASLA" CssClass="frmSmallTextBox" MaxLength="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement5">
                    CLARB
                </td>
                <td>
                    <!--Textbox to get CLARB Number-->
                    <asp:TextBox ID="txtCLARB" CssClass="frmSmallTextBox" MaxLength="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement5">
                    CSLA
                </td>
                <td>
                    <!--Textbox to get CLARB Number-->
                    <asp:TextBox ID="txtCSLA" CssClass="frmSmallTextBox" MaxLength="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement5">
                    FL
                </td>
                <td>
                    <asp:TextBox ID="txtFL" CssClass="frmSmallTextBox" MaxLength="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement5">
                    Email
                </td>
                <td>                    
                    <asp:TextBox ID="txtEmail" CssClass="frmSmallTextBox" MaxLength="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="smallTopPadd">
                </td>
            </tr>
            <tr>
                <td class="smallTopPadd">
                </td>
            </tr>
            <tr>
                <td class="frmElement5">
                       <asp:Button ID="btnFindParticipants" runat="server" Text="FIND ATTENDEES" ToolTip="FIND ATTENDEES"
                        CssClass="commonButton btn153" OnClick="btnFindParticipants_Click" />
                </td>
                <td>
                 
                </td>
            </tr>
        </tbody>
    </table>
    <br /><br /><br /><br /><br />
    <asp:GridView ID="uiGVAllAttendees" runat="server" AutoGenerateColumns="false" OnRowCommand="gridview_RowCommand" DataKeyNames="ID" Width="100%" >
    <AlternatingRowStyle BackColor="#cccccc" />
    <RowStyle BackColor="white" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>First Name</HeaderTemplate>
                <HeaderStyle Width="13%" />
                <ItemTemplate>
                    <asp:TextBox Width="60" ID="uiTxtFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'></asp:TextBox>
                    <asp:TextBox ID="uiTxtParticipantID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderTemplate>Last Name</HeaderTemplate>
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <asp:TextBox Width="100" ID="uiTxtLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderTemplate>Member Number</HeaderTemplate>
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:TextBox Width="60" ID="uiTxtASLAMemberNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ASLAMemberNumber") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderTemplate>CLARB Number</HeaderTemplate>
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:TextBox Width="60" ID="uiTxtCLARBNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CLARBNumber") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderTemplate>Florida Number</HeaderTemplate>
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:TextBox Width="60" ID="uiTxtFloridaStateNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FloridaStateNumber") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderTemplate>MI</HeaderTemplate>
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <asp:TextBox Width="30" ID="uiTxtMiddleInitial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MiddleInitial") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderTemplate>CSLA Number</HeaderTemplate>
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:TextBox Width="60" ID="uiTxtCSLANumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CSLANumber") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Email</HeaderTemplate>
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <asp:TextBox Width="150" ID="uiTxtEmail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"email") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField>
             <HeaderTemplate>Courses</HeaderTemplate>
                <HeaderStyle Width="300" />
                <ItemTemplate>
                    <ul><asp:Label Text='<%# HttpUtility.HtmlDecode(DataBinder.Eval(Container.DataItem, "courses").ToString())%>' Width="300" runat="server"></asp:Label></ul>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderTemplate></HeaderTemplate>
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <asp:LinkButton ID="uiLnkButtonSave" Text="Save" runat="server" CommandName="Save"></asp:LinkButton>
                    <br /><asp:Label id="uiLblResponse" runat="server" ForeColor="Red"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>        
    </asp:GridView>
</asp:Content>



