<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageAttendeesCourseListAjaxResults.aspx.cs" Inherits="Provider_ManageAttendeesCourseListAjaxResults" %>
<form runat="server">

<asp:Repeater id="gvExistingCourses"  runat="server" OnItemDataBound="gvExistingCourses_ItemDataBound">        
        <HeaderTemplate>
            <div class="uk-grid existingCourcesGrid">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="uk-width-1-1 title">
                        <%#Eval("Title").ToString() %>
                        
            </div>  
            <div class="uk-width-1-1 uk-width-small-1-3 uk-width-medium-1-4">
                        Start Date: <%#DateTime.Parse(Eval("StartDate").ToString()).ToShortDateString() %>
                    </div>        
            <div class="uk-width-1-1 uk-width-small-1-3 uk-width-medium-1-4">
                       End Date: <%#DateTime.Parse(Eval("EndDate").ToString()).ToShortDateString() %>
            </div>
            <div class="uk-width-1-1 uk-width-small-1-3 uk-width-medium-1-4 uk-visible-medium">
                 &nbsp;
            </div>
            <div class="uk-width-1-1 uk-width-small-1-3 uk-width-medium-1-4">
                 Modified Date: <asp:Label ID="uiLblModifiedDate" runat="server"></asp:Label>
            </div>
            <div class="uk-width-1-1 uk-width-small-1-3 uk-width-medium-1-4">
                <a href="UploadAttendees.aspx?courseid=<%#Eval("ID") %>">Upload Attendees in Excel</a>
            </div>
            <div class="uk-width-1-1 uk-width-small-1-3 uk-width-medium-1-4">
                <a href="AddAttendee.aspx?courseid=<%#Eval("ID") %>" target="_blank">Add Attendee via Tablet</a>
            </div>
            <div class="uk-width-1-1 uk-width-small-1-3 uk-width-medium-1-4">
                <a href="ManageAttendees.aspx?courseid=<%#Eval("ID") %>">Edit Attendees</a>
            </div>
            <div class="uk-width-1-1 uk-width-small-1-3 uk-width-medium-1-4">
                <asp:LinkButton ID="uilnkDownloadAttendees" OnClick="uilnkDownloadAttendees_Click" CommandName="CourseID" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' runat="server" Text="Download Attendees"></asp:LinkButton>                
            </div>
            <div class="uk-width-1-1 uk-width-small-1-3 uk-width-medium-1-4" style="margin-bottom:30px;">            
                <asp:LinkButton ID="uiLkDownloadCertificates" OnClick="uiLkDownloadCertificates_Click" CommandName="CourseID" CommandArgument="ID" runat="server" Text="Generate Certificates"></asp:LinkButton>                
            </div>                
            <div class="uk-width-1-1">
                <hr />
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>

    </asp:Repeater>
 </form>