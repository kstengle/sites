<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="AttendeeSearchResult.aspx.cs" Inherits="Admin_AttendeesSearchResult"
    Title="Admin: Search Results | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->
    <div class="title">
        Search Results
    </div>
    <div>
        <asp:Label ID="lblMessage" CssClass="padding10" runat="server"></asp:Label>
    </div>
    <div class="searchMsg">
        <%if (participants != null && participants.Count > 0)
          {
        %>
        <table width="100%" cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <!--Start Looping Searched Item-->
                <%foreach (Pantheon.ASLA.LACES.Common.SearchParticipant participant in participants)
                  {%>
                <tr>
                    <td valign="top" class="tdListItem">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td class="searchTableLeftTd">
                                Last Name
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Participant Last Name Linked to Participant Courses Page-->
                                    <a href="AttendeeCourses.aspx?<%=LACESConstant.QueryString.PARTICIPANT_ID + "=" + participant.ID%>">
                                        <%=Server.HtmlEncode(participant.LastName)%>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="searchTableLeftTd">
                                First Name
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Participant First Name-->
                                    <%=Server.HtmlEncode(participant.FirstName)%>
                                </td>
                            </tr>
                            
                            <%if (Server.HtmlEncode(participant.MiddleName) != "")
                              {%>
                            <tr>
                                <td class="searchTableLeftTd">
                                Middle Initial
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Participant middle Name-->
                                    <%=Server.HtmlEncode(participant.MiddleName)%>
                                </td>
                            </tr>
                            <% }%>
                            
                            <%if (Server.HtmlEncode(participant.ASLANumber)!="")
                              {%>
                            
                            <tr>
                                <td class="searchTableLeftTd">
                                ASLA
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Participant ASLA Number-->
                                    <%=Server.HtmlEncode(participant.ASLANumber)%>
                                </td>
                            </tr>
                            <% }%>
                            <% if (Server.HtmlEncode(participant.CLARBNumber) != "")
                               {%>
                            <tr>
                                <td class="searchTableLeftTd">
                                CLARB
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Participant CLARB Number-->
                                    <%=Server.HtmlEncode(participant.CLARBNumber)%>
                                </td>
                            </tr>
                            <% }%>
                            
                            
                            <% if (Server.HtmlEncode(participant.CSLANumber) != "")
                               {%>
                            <tr>
                                <td class="searchTableLeftTd">
                                CSLA
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Participant CSLA Number-->
                                    <%=Server.HtmlEncode(participant.CSLANumber)%>
                                </td>
                            </tr>
                            <% }%>
                            
                            
                            <% if (Server.HtmlEncode(participant.FLNumber) != "")
                               {%>
                            <tr>
                                <td class="searchTableLeftTd">
                                FL
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Participant Fl State Number-->
                                    <%=Server.HtmlEncode(participant.FLNumber)%>
                                </td>
                            </tr>
                            <% }%>
                            <tr>
                                <td class="searchTableLeftTd">
                                Course Count
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Participant Course Count-->
                                    <%=participant.Courses%>
                                    <br /><br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <%--<tr>
                    <td valign="top" bgcolor="#963434" colspan="6">
                        <!--Course Item Separator-->
                        <img alt="" width="1" height="1" src="/laces/images/shim.gif" /></td>
                </tr>--%>
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
        <div style="color: Red">
            No results found.</div>
        <%} %>
    </div>
    <!--End Left Content Place Holder-->
</asp:Content>

