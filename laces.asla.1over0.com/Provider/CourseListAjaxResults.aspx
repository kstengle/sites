<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CourseListAjaxResults.aspx.cs" Inherits="Provider_CourseListAjaxResults" %>
<form runat="server">
<input type="hidden" runat="server" id="uiHdnResultCount" />
<asp:Repeater ID="dlCourseList" runat="server" OnItemCreated="dlCourseList_ItemCreated">
        <HeaderTemplate>
            <div class="uk-grid existingCourcesGrid">
        </HeaderTemplate>
        <%--Starting Item Templete--%>
        <ItemTemplate>
            <div class="uk-width-1-1 HomePageEntryFields">                                                                    
                <strong><asp:Label ID="lblTitle" runat="server"></asp:Label></strong>
            </div>
            <div class="uk-width-1-1 uk-width-medium-1-3 HomePageEntryFields">
                Start Date:<strong><asp:Label ID="lblStartDate" runat="server"></asp:Label></strong>
            </div>            
            <div class="uk-width-1-1 uk-width-medium-1-3 HomePageEntryFields">
                    End Date:<strong><asp:Label ID="lblEndDate" runat="server"></asp:Label></strong>
            </div>

            <div class="uk-width-1-1 uk-width-medium-1-3 HomePageEntryFields">
                    Status:<strong><asp:Label ID="lblStatus" runat="server"></asp:Label></strong>
                    <asp:LinkButton CssClass="courseActionLinks" ID="btnStatus" runat="server" OnClick="btnStatus_OnClick" CommandName='<%# Eval( "Status" ) %>' CommandArgument='<%# Eval( "ID" ) %>'></asp:LinkButton>
            </div>
            <div class="uk-width-1-1 uk-width-medium-1-3 HomePageEntryFields">
                    <asp:LinkButton  CssClass="courseActionLinks" ID="uilbRenewButton" runat="server" OnClick="btnRenew_OnClick" CommandName='Renew' CommandArgument='<%# Eval( "ID" ) %>'>Renew</asp:LinkButton>
            </div>
            <div class="uk-width-1-1 uk-width-medium-1-3 HomePageEntryFields">            
                    <asp:LinkButton  CssClass="courseActionLinks" ID="uiLbReplicateButton" runat="server" OnClick="btnReplicate_OnClick" CommandName='Replicate' CommandArgument='<%# Eval( "ID" ) %>'>Revise and resubmit</asp:LinkButton>
            </div>
            <div class="uk-width-1-1 uk-width-medium-1-3 HomePageEntryFields">
                    <asp:LinkButton  CssClass="courseActionLinks" ID="uilbActiveButton" runat="server" OnClick="btnActive_OnClick" CommandName='<%# Eval( "Active" ) %>' CommandArgument='<%# Eval( "ID" ) %>'></asp:LinkButton>                
            </div>       
            <div class="uk-width-1-1">
                <hr />
            </div>                
        </ItemTemplate>        
        <FooterTemplate>
            </div>
        </FooterTemplate>

    </asp:Repeater>
<div id="NoResult" runat="server" visible="false">
        No records found.
    </div>
  </form>  