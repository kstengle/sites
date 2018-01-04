<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="CourseResult.aspx.cs" Inherits="Admin_CourseResult" Title="Admin: Course Search Results | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->

    <script type="text/javascript" language="javascript" src="../javascript/SearchCourse.js"></script>

    <script type="text/javascript">
    ///Upon pressing the Activate/Deactivate Course link, the system will pop up an alert 
    ///to the administrator asking them to confirm their action
    function ActivateDeactivateCourse(courseId, indexId, status) {
        courseName = courseArray[indexId];
        
        ///Convert to slash(/)
        while(courseName.indexOf("&#47;") >= 0)
        {
            courseName = courseName.replace("&#47;","/");
        }
        
        ///Generating confirm message text
        var message = "Warning: You are about to " + status + " " + courseName + " from the <%=LACESConstant.LACES_TEXT%> system." 
        message = message + "\n\nAre you sure you wish to " + status + " " + courseName + "?";
        
        var agree = confirm(message);
        
        //If confirm to delete
        if(agree)
        {
            window.location.replace("?<%=LACESConstant.QueryString.COURSE_ID%>=" + courseId + "&<%=LACESConstant.QueryString.COURSE_STATUS%>=" + status);
        }
    }
    function DeactivateCourse(courseId) {
        alert(courseId);
    }
    </script>

    <div class="title">
        Search Results
    </div>
    <div class="smallBoxBorder">
        <div style="text-align: left;">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
        </div>
        <table width="100%" cellspacing="2">
            <tbody>
                <tr>
                    <td valign="top" align="left">
                            <div class="smallPadding smallBottomPad">
                            <b>Keyword</b></div>
                            
                        <asp:TextBox ID="txtKeyword" CssClass="frmMainSearch" runat="server" MaxLength="80"
                            Text=""></asp:TextBox>
                    </td>
                    <td valign="top" align="left">
                        <div class="smallPadding">
                            <b>Available Courses from</b></div>
                        <div class="title3">
                            Start Date</div>
                        <%--<hr />--%>
                        <asp:CustomValidator Display="None" ID="CustomValidator1" runat="server" ClientValidationFunction="DateValidation"
                            ControlToValidate="txtStartDate" ErrorMessage="Enter valid start date." ForeColor="White"
                            SetFocusOnError="True">*</asp:CustomValidator>
                        <asp:TextBox ID="txtStartDate" runat="server" MaxLength="12" Width="100px" CssClass="frmDateBox"></asp:TextBox>&nbsp;<img
                            alt="Calendar" title="Calendar" onclick="loadCalendar(this, '<%=txtStartDate.ClientID %>', 'mm/dd/yyyy')"
                            src="../images/icon_calendar.jpg" class="CalenderIcon" />
                    </td>
                    <td valign="top" align="left" rowspan="2">
                        
                            <div class="smallPadding smallBottomPad">
                            <b>Providers</b></div>
                            
                        <asp:ListBox ID="lbEducationProvider" Rows="5" runat="server" SelectionMode="Multiple" CssClass="solidBorder">
                        </asp:ListBox>
                    </td>
                    <td valign="top" align="left">
                            <div class="smallPadding smallBottomPad">
                            <b>&nbsp;</b></div>
                        
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" ToolTip="SEARCH" CssClass="commonButton btn84"
                            OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                        <div class="title3">
                            Subjects</div>
                        <%--<hr />--%>
                        <asp:DropDownList ID="ddlSubject" runat="server" CssClass="solidBorder">
                        </asp:DropDownList>
                    </td>
                    <td valign="top" align="left">
                        <div class="title3">
                            End Date</div>
                        <%--<hr />--%>
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateValidation"
                            ControlToValidate="txtEndDate" Display="None" ErrorMessage="Enter valid end date."
                            ForeColor="White" SetFocusOnError="True">*</asp:CustomValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                            ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start date cannot be higher than end date."
                            ForeColor="White" Operator="GreaterThanEqual" Type="Date" SetFocusOnError="True">*</asp:CompareValidator>
                        <asp:TextBox ID="txtEndDate" runat="server" MaxLength="12" Width="100px" CssClass="frmDateBox"></asp:TextBox>&nbsp;<img
                            alt="Calendar" title="Calendar" onclick="loadCalendar(this, '<%=txtEndDate.ClientID %>', 'mm/dd/yyyy')"
                            src="../images/icon_calendar.jpg" class="CalenderIcon" />
                    </td>
                    <td valign="top" align="left">
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="left" colspan="3">
                       <asp:CheckBox ID="uiChkHealthSafetyWelfare" runat="server" />Search for Healthy, Safety, and Welfare (HSW) courses only
                     </td>
                </tr>
                <tr>   
                    <td valign="top" align="left" colspan="3">
                        <asp:CheckBox ID="uiChkDistanceEducation" runat="server" />Search for Distance Education courses only        
                    </td>       
                </tr>
                <tr>
                    <td colspan="3" style="padding-top: 2px">
                    </td>
                </tr>
            </tbody>
        </table>

        <script type="text/javascript">
        ///Select existing value of keyword text box
        document.getElementById('<%=txtKeyword.ClientID%>').select();
        </script>

    </div>
    <br />
    <div>
        <asp:Label ID="lblMessage" CssClass="searchMsg" runat="server"></asp:Label>
        <asp:label ID="uiLitResultsMessage" runat="server"></asp:label>
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
                                    Course
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
                                    Start Date
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Course Start Data and End Date-->
                                    <%=course.StartDate.ToString("MM/dd/yyyy")%>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="searchTableLeftTd">
                                    End Date
                                </td>
                                <td class="searchTableRightTd">
                                    <%=course.EndDate.ToString("MM/dd/yyyy")%>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="searchTableLeftTd">
                                    Provider
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Provider Name Linked to Provider Details Page-->
                                    <a href="ApprovedProviderDetails.aspx?<%=LACESConstant.QueryString.PROVIDER_ID + "=" + course.ProviderID%>">
                                        <%=Server.HtmlEncode(course.ProviderName)%>
                                    </a>
                                    <br />
                                </td>
                            </tr>
                            <%if (getFormattedLocation(course) != "")
                              {%>
                            <tr>
                                <td class="searchTableLeftTd">
                                    Location
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Course Location [City, State]-->
                                    <%=getFormattedLocation(course)%>
                                    <br />
                                </td>
                            </tr>
                            <% }%>
                            <tr>
                                <td class="searchTableLeftTd">
                                    Subjects
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Course Subjects-->
                                    <%=Server.HtmlEncode(course.Subjects.Replace(",", ", "))%>
                                    
                                </td>
                            </tr>
                            <%if(course.Active.ToString() !="F"){%>

                            <tr>
                                <td class="searchTableLeftTd">
                                    &nbsp;
                                </td>
                                <td class="searchTableRightTd">          
                                          <!--Use onclient click to set a hidden field then do a postback-->               
                                <a href="DeactivateCourse.aspx?id=<%=course.ID.ToString() %>">Deactivate</a>
                                
                                    <br />
                                    <br />
                                    
                                </td>
                            </tr>
                            <%} else{%>
                            <tr>
                                <td class="searchTableLeftTd">
                                    &nbsp;
                                </td>
                                <td class="searchTableRightTd">          
                                          <!--Use onclient click to set a hidden field then do a postback-->               
                                Course is inactive
                                
                                    <br />
                                    <br />
                                    
                                </td>
                            </tr>
                                 <br />
                                 <br />
                            <%}%>
                        </table>
                    </td>
                </tr>
                <%--<tr>
                    <td valign="top" bgcolor="#963434" colspan="6">
                        <!--Course Item Separator-->
                        <img alt="" width="1" height="1" src="../images/shim.gif" /></td>
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
