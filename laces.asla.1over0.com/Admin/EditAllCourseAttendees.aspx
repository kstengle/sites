<%@ Page Title="" Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true" CodeFile="EditAllCourseAttendees.aspx.cs" Inherits="Admin_EditAllCourseAttendees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">
<div class="title" id="title" runat="server">
        Edit Attendee
    </div>
    <br />
    <div id="divRightMessage" runat="server">
        <asp:Label ID="uiLblDeletedAlert" Font-Size="Larger" ForeColor="Red" runat="server" Visible="false"></asp:Label>
        Use this page to edit an attendee who attended the <asp:Label ID="uiLblCourseName" runat="server" ForeColor="Red"></asp:Label> course. Removing
        an attendee from the course is a permanent action, and can not be undone.
    </div>
    <asp:GridView ID="uiGVAllAttendees" runat="server" AutoGenerateColumns="false" OnRowCommand="gridview_RowCommand" DataKeyNames="ID" >
    <AlternatingRowStyle BackColor="Silver" />
    <RowStyle BackColor="white" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>First Name</HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="uiTxtFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Last Name</HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="uiTxtLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>ASLA Member Number</HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="uiTxtASLAMemberNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ASLAMemberNumber") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>CLARB Number</HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="uiTxtCLARBNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CLARBNumber") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Florida State Number</HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="uiTxtFloridaStateNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FloridaStateNumber") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Middle Initial</HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="uiTxtMiddleInitial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MiddleInitial") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>CSLA Number</HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="uiTxtCSLANumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CSLANumber") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Email</HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="uiTxtEmail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"email") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="uiLnkButtonSave" Text="Save" runat="server" CommandArgument="save"></asp:LinkButton><br /><br />
                    <asp:LinkButton ID="uiLnkDelete" Text="DELETE" runat="server" CommandArgument="remove"></asp:LinkButton>
                    <asp:Label id="uiLblResponse" runat="server" ForeColor="Red"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>        
    </asp:GridView>
</asp:Content>

