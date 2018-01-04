<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ParticipantSearchResult.aspx.cs" Inherits="Admin_ParticipantSearchResult"
    Title="Participant Search Results | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->
    <div class="title">
        Participant Search Results
    </div>
    <div>
        <asp:Label ID="lblMessage" CssClass="padding10" runat="server"></asp:Label>
    </div>
    <div class="padding10">
        <%if (participants != null && participants.Count > 0)
          {
        %>
        <table width="100%" cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <!--Start Header Table Row-->
                <tr>
                    <td class="tdListHeader" id="tdLastName" runat="server">
                    </td>
                    <td class="tdListHeader" id="tdFirstName" runat="server">
                    </td>
                    <td class="tdListHeader" id="tdASLA" runat="server">
                    </td>
                    <td class="tdListHeader" id="tdCLARB" runat="server">
                    </td>
                    <td class="tdListHeader" id="tdFL" runat="server">
                    </td>
                    <td class="tdListHeader" id="tdCourses" runat="server">
                    </td>
                </tr>
                <!--End Header Table Row-->
                <!--Start Looping Searched Item-->
                <%foreach (Pantheon.ASLA.LACES.Common.SearchParticipant participant in participants)
                  {%>
                <tr>
                    <td valign="top" class="tdListItem">
                        <!--Participant Last Name Linked to Participant Courses Page-->
                        <a href="ParticipantCourses.aspx?<%=LACESConstant.QueryString.PARTICIPANT_ID + "=" + participant.ID%>">
                            <%=Server.HtmlEncode(participant.LastName)%>
                        </a>
                    </td>
                    <td valign="top" class="tdListItem">
                        <!--Participant First Name-->
                        <%=Server.HtmlEncode(participant.FirstName)%>
                    </td>
                    <td valign="top" class="tdListItem">
                        <!--Participant ASLA Number-->
                        <%=Server.HtmlEncode(participant.ASLANumber)%>
                    </td>
                    <td valign="top" class="tdListItem">
                        <!--Participant CLARB Number-->
                        <%=Server.HtmlEncode(participant.CLARBNumber)%>
                    </td>
                    <td valign="top" class="tdListItem">
                        <!--Participant Fl State Number-->
                        <%=Server.HtmlEncode(participant.FLNumber)%>
                    </td>
                    <td valign="top" class="tdListItem">
                        <!--Participant Course Count-->
                        <%=participant.Courses%>
                    </td>
                </tr>
                <tr>
                    <td valign="top" bgcolor="#963434" colspan="6">
                        <!--Course Item Separator-->
                        <img alt="" width="1" height="1" src="/laces/images/shim.gif" /></td>
                </tr>
                <%} %>
                <!--End Looping Searched Item-->
                <!--Start Previous/Next Link-->
                <tr>
                    <td colspan="6" class="pagingRow">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 50%" runat="server" id="tdPreviousPage">
                                </td>
                                <td align="right" style="width: 50%" runat="server" id="tdNextPage">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <!--End Previous/Next Link-->
            </tbody>
        </table>
        <%}
          else
          { %>
        No result found with selected criteria!
        <%} %>
    </div>
    <!--End Left Content Place Holder-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightane" runat="Server">
    <!--Start Right Content Place Holder-->
    <!--Search selection criteria-->
    <p class="moreLineHeight">
        <asp:Label ID="lblTotal" runat="server"></asp:Label>
        number of search results have been found with the following criteria:<br />
        <br />
        <em>Name</em>:<br />
        <asp:Label ID="lblName" runat="server"></asp:Label><br />
        <br />
        ASLA:<br />
        <asp:Label ID="lblASLA" runat="server"></asp:Label><br />
        <br />
        CLARB:<br />
        <asp:Label ID="lblCLARB" runat="server"></asp:Label><br />
        <br />
        FL:<br />
        <asp:Label ID="lblFL" runat="server"></asp:Label><br />
    </p>
    <div style="text-align: center">
        <asp:Button ID="btnSearchAgain" runat="server" Text="Search Again" ToolTip="Search Again"
            CssClass="commonButton btn107" OnClick="btnSearchAgain_Click" />
    </div>
    <!--End Right Content Place Holder-->
</asp:Content>
