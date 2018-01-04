<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="AttendeeCourses.aspx.cs" Inherits="Admin_AttendeeCourses" Title="Admin: Attendee's Courses | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->
    <div class="title">
        Attendee</div>
    <div class="searchMsg">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td valign="top" class="tdListItem">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td class="searchTableLeftTd">
                                Last Name
                            </td>
                            <td class="searchTableRightTd">
                                <!--Display Participant's Last Name-->
                                <asp:Label ID="lblPartLastName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="searchTableLeftTd">
                                First Name
                            </td>
                            <td class="searchTableRightTd">
                                <!--Display Participant's First Name-->
                                <asp:Label ID="lblPartFirstName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <%if (lblMiddleName.Text != "")
                              {%>
                            <tr>
                                <td class="searchTableLeftTd">
                                Middle Initial
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Participant middle Name-->
                                    <asp:Label ID="lblMiddleName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <% }%>
                         <%if (lblPartASLA.Text != "")
                              {%>
                        <tr>
                            <td class="searchTableLeftTd">
                                ASLA
                            </td>
                            <td class="searchTableRightTd">
                                <!--Display Participant's ASLA number-->
                                <asp:Label ID="lblPartASLA" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <% }%>
                         <%if (lblPartCLARB.Text != "")
                              {%>
                        <tr>
                            <td class="searchTableLeftTd">
                                CLARB
                            </td>
                            <td class="searchTableRightTd">
                                <!--Display Participant's CLARB number-->
                                <asp:Label ID="lblPartCLARB" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <% }%>
                        <% if (lblCSLA.Text != "")
                               {%>
                            <tr>
                                <td class="searchTableLeftTd">
                                CSLA
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Participant CSLA Number-->
                                   <asp:Label ID="lblCSLA" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <% }%>
                        <%if (lblPartFL.Text != "")
                              {%>
                        <tr>
                            <td class="searchTableLeftTd">
                                FL
                            </td>
                            <td class="searchTableRightTd">
                                <!--Display Participant's Florida State Number-->
                                <asp:Label ID="lblPartFL" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <% }%>
                        <tr>
                            <td class="searchTableLeftTd">
                                Course Count
                            </td>
                            <td class="searchTableRightTd">
                                <!--Display number of courses taken by the participant-->
                                <asp:Label ID="lblPartCourses" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="title">
        Courses
    </div>
    <div class="searchMsg">
        <%if (courseResult != null && courseResult.Count > 0)
          {
        %>
        <table width="100%" cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <!--Start Looping Searched Item-->
                <%foreach (Pantheon.ASLA.LACES.Common.Course course in courseResult)
                  {%>
                <tr>
                    <td valign="top" class="tdListItem">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td class="searchTableLeftTd">
                                    Title
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Course Title Linked to Course Details Page-->
                                    <a href="CourseDetails.aspx?<%=LACESConstant.QueryString.COURSE_ID + "=" + course.ID%>">
                                        <%=Server.HtmlEncode(course.Title)%>
                                    </a>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="searchTableLeftTd">
                                    Organization
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Provider Name Linked to Provider Details Page-->
                                    <a href="ApprovedProviderDetails.aspx?<%=LACESConstant.QueryString.PROVIDER_ID + "=" + course.ProviderID%>">
                                        <%=Server.HtmlEncode(course.ProviderName)%>
                                    </a>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="searchTableLeftTd">
                                    Start Date
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Course Start Date-->
                                    <%=course.StartDate.ToString("MM/dd/yyyy")%>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="searchTableLeftTd">
                                    End Date
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Course End Date-->
                                    <%=course.EndDate.ToString("MM/dd/yyyy")%>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="searchTableLeftTd">
                                    Date Entered
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Course Date Entered-->
                                    <%=course.DateEntered.ToString("MM/dd/yyyy")%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top"colspan="6">
                        <br />
                     </td>
                </tr>
                
                <%} %>
                <!--End Looping Searched Item-->
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
