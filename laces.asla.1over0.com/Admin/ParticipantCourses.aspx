<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ParticipantCourses.aspx.cs" Inherits="Admin_ParticipantCourses" Title="Participant’s Courses | LA CES™" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">
<!--Start Left Content Place Holder-->
    <div class="title">
        Courses
    </div>
    <div class="padding10">
        <%if (courseResult != null && courseResult.Count > 0)
          {
        %>
        <table width="100%" cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <!--Start Header Table Row-->
                <tr>
                    <td class="tdListHeader" id="tdCourse" runat="server">
                    </td>
                    <td class="tdListHeader" id="tdProvider" runat="server">
                    </td>
                    <td class="tdListHeader" id="tdStartDate" runat="server">
                    </td>
                    <td class="tdListHeader" id="tdEndDate" runat="server">
                    </td>
                    <td class="tdListHeader" id="tdDateEntetred" runat="server">
                    </td>
                </tr>
                <!--End Header Table Row-->
                <!--Start Looping Searched Item-->
                <%foreach (Pantheon.ASLA.LACES.Common.Course course in courseResult)
                  {%>
                <tr>
                    <td valign="top" class="tdListItem">
                        <!--Course Title Linked to Course Details Page-->
                        <a href="CourseDetails.aspx?<%=LACESConstant.QueryString.COURSE_ID + "=" + course.ID%>">
                            <%=Server.HtmlEncode(course.Title)%>
                        </a>
                    </td>
                    <td valign="top" class="tdListItem">
                        <!--Provider Name Linked to Provider Details Page-->
                        <a href="ProviderDetails.aspx?<%=LACESConstant.QueryString.PROVIDER_ID + "=" + course.ProviderID%>">
                            <%=Server.HtmlEncode(course.ProviderName)%>
                        </a>
                    </td>
                    <td valign="top" class="tdListItem">
                        <!--Course Start Date-->
                        <%=course.StartDate.ToString("MM/dd/yyyy")%>
                    </td>
                    <td valign="top" class="tdListItem">
                        <!--Course End Date-->
                        <%=course.EndDate.ToString("MM/dd/yyyy")%>
                    </td>
                    <td valign="top" class="tdListItem">
                        <!--Course Date Entered-->
                        <%=course.DateEntered.ToString("MM/dd/yyyy")%>
                    </td>
                </tr>
                <tr>
                    <td valign="top" bgcolor="#963434" colspan="6">
                        <!--Course Item Separator-->
                        <img alt="" width="1" height="1" src="/laces/images/shim.gif" /></td>
                </tr>
                <%} %>
                <!--End Looping Searched Item-->
            </tbody>
        </table>
         <%}
          else
          { %>
          No courses found for this participant!
        <%} %>
    </div>
<!--End Left Content Place Holder-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightane" Runat="Server">
    <!--Start Right Content Place Holder-->
    <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
        <tr>
            <td align="center" colspan="3">
                <div class="title">Participant</div>
            </td>
        </tr>
        <tr>
            <td align="right" class="moreLineHeight"  valign="top">
                <!--Display Participant's Last Name-->
                <asp:Label ID="lblPartLastName" runat="server"></asp:Label>
            </td>
            <td style="width: 8px">
                &nbsp;
            </td>
            <td align="left" style="width:35%" class="moreLineHeight" valign="top">
                Last</td>
        </tr>
        <tr>
            <td align="right" class="moreLineHeight" valign="top">
                <!--Display Participant's First Name-->
                <asp:Label ID="lblPartFirstName" runat="server"></asp:Label></td>
            <td style="width: 8px">
                &nbsp;
            </td>
            <td align="left" style="width:35%" class="moreLineHeight" valign="top">
                First</td>
        </tr>
        <tr>
            <td align="right" class="moreLineHeight"  valign="top">
                <!--Display Participant's ASLA number-->
                <asp:Label ID="lblPartASLA" runat="server"></asp:Label>
            </td>
            <td  style="width: 8px">
                &nbsp;
            </td>
            <td align="left" style="width:35%" class="moreLineHeight"  valign="top">
                ASLA</td>
        </tr>
        <tr>
            <td align="right" class="moreLineHeight"  valign="top">
                <!--Display Participant's CLARB number-->
                <asp:Label ID="lblPartCLARB" runat="server"></asp:Label></td>
            <td  style="width: 8px">
                &nbsp;
            </td>
            <td align="left" style="width:35%" class="moreLineHeight"  valign="top">
                CLARB</td>
        </tr>
        <tr>
            <td align="right" class="moreLineHeight"  valign="top">
                <!--Display Participant's Florida State Number-->
                <asp:Label ID="lblPartFL" runat="server"></asp:Label></td>
            <td  style="width: 8px">
                &nbsp;
            </td>
            <td align="left" style="width:35%" class="moreLineHeight"  valign="top">
                FL</td>
        </tr>
        <tr>
            <td align="right" class="moreLineHeight"  valign="top">
                <!--Display number of courses taken by the participant-->
                <asp:Label ID="lblPartCourses" runat="server"></asp:Label></td>
            <td  style="width: 8px">
                &nbsp;
            </td>
            <td align="left" style="width:35%" class="moreLineHeight"  valign="top">
                Courses</td>
        </tr>
    </table>
    <!--End Right Content Place Holder-->
</asp:Content>

