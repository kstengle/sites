<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="PendingCourses.aspx.cs" Inherits="Admin_PendingCourses" Title="Admin: Pending Courses | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/providerStyle.css" />
    <script type="text/javascript">
    
    ///The Course object is created to store individual course details
    function courseObj(cId,cName,pName)
    {
	    this.courseId = cId;
	    this.courseName = cName;
	    this.providerName = pName;
    }
    
    ///The Course Object Array is created to store all courses details
    courseObjectArray = new Array();
        
    ///Write the generated array value
    <%=jsCourseObject%>
    
    ///Upon pressing the Reject Course link, the system will pop up an alert 
    ///to the administrator asking them to confirm their action
    function RejectCourse(indexId)
    {
        courses = courseObjectArray[indexId];
        
        ///Generating confirm message
        var message = "Warning: You are about to reject " + courses.courseName + " from the <%=LACESConstant.LACES_TEXT%> system. This action is permanent."
        message = message + " If you reject this course, you may want to contact " + courses.providerName + " to explain why the course was rejected.";
        message = message + "\n\nAre you sure you wish to reject " + courses.courseName + "?";
        
        ///Convert to slash(/)
        while(message.indexOf("&#47;") >= 0)
        {
            message = message.replace("&#47;","/");
        }
          
        var agree = confirm(message);
        
        //If confirm to delete
        if(agree)
        {
            window.location.replace("?<%=LACESConstant.QueryString.COURSE_ID%>=" + courses.courseId);
            
        }        
    }
    
    ///Check all items and if found any item checked then return true 
    ///otherwise return false with giving message
    function CheckAprove()
    {
        //Get items count
        var courses = <%=dlCourseList.Items.Count%>;        
        var i=0;
        
        for(i=0; i<courses; i++ )     
        { 
            var offset = i;
            if(offset < 10)
                offset = "0"+offset;
            //create the id dynamically
            var approveCtrl = 'ctl00_ContentPlaceHolderLeftPane_dlCourseList_ctl' + offset + '_chkApprove';
            if(document.getElementById(approveCtrl) != null && document.getElementById(approveCtrl).checked == true)
                {
                document.getElementById("divErr").style.display = "none";
                return true;
                }
        }
        if(document.getElementById("divMsg") != null)
            document.getElementById("divMsg").style.display = "none";
        
        //Display error message    
        document.getElementById("divErr").style.display = "block";
        document.getElementById("divErr").innerHTML = "<br/>Please select course(s) to approve.<br/>&nbsp;";
        //Scroll to top
        scrollTo(0,0);
        return false;
    }
    
    function OpenProviderApplicationPage()
    {
        window.open("../Provider/ProviderApplication.aspx","provider");
    }
    
    </script>

    <!--Start Left Content Place Holder-->
    <div class="title">
        Pending Courses
    </div>
    <!--Admin Welcome Message-->
    <div class="moreLineHeight">
        These courses have been registered by approved providers. Review does not indicate
        <%=LACESConstant.LACES_TEXT%>
        endorsement of the courses, but serves as a filter to ensure that there is no inappropriate
        material posted to the
        <%=LACESConstant.LACES_TEXT%>
        website.
    </div>
    <div id="divErr" class="errorDiv" style="display: none">
    </div>
    <div id="divMsg" class="padding10">
        <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
    </div>
    <div>
        <div class="topBox">
        <h5>Find Courses</h5>
        <p><strong>To Narrow Your Options, Uncheck:</strong></p>
        <asp:CheckBoxList ID="uiChxBoxListStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"></asp:CheckBoxList>
        <hr />
        <p><strong>Search:</strong></p>
        <div class="cell">
            <p>Keyword</p>
            <asp:TextBox ID="uiTxtKeyword" runat="server"></asp:TextBox>
        </div>
        <div class="cell">
            <p>State/Province</p>
            <asp:DropDownList ID="drpState" runat="server"></asp:DropDownList>
        </div>
        <div class="cell">
            <p>Available Courses from</p>
            <div class="startDate">
                <asp:TextBox autocomplete="off" ID="uiTxtStartDate" CssClass="frmDateBox" runat="server" MaxLength="20"></asp:TextBox>
                <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=uiTxtStartDate.ClientID %>', 'mm/dd/yyyy')" src="../images/icon_calendar.jpg"  class="CalenderIcon" />
            </div>
            <div class="endDate">
                <asp:TextBox autocomplete="off" ID="uiTxtEndDate" CssClass="frmDateBox" runat="server" MaxLength="20"></asp:TextBox>
                <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=uiTxtEndDate.ClientID %>', 'mm/dd/yyyy')" src="../images/icon_calendar.jpg"  class="CalenderIcon" />
            </div>
        </div>
        <div class="cell subject">
            <p>Subject</p>
            <asp:DropDownList ID="drpSubject" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Value="" Text="Select Subject"></asp:ListItem>           
            </asp:DropDownList>
        </div>
        <asp:LinkButton ID="uiLnkSearch" runat="server" Text="Search"></asp:LinkButton>
        <div style="clear:both"></div>
    </div>

        <!--DalaList to display Course items-->
        <asp:DataList ID="dlCourseList" CssClass="listStyle" runat="server" Width="100%"
            OnItemCreated="dlCourseList_ItemCreated">
            <%--Starting Item Templete--%>
            <ItemTemplate>
                <div class="searchMsg">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="searchTableLeftTd">
                            Course
                            </td>
                            <td class="searchTableRightTd">
                                <!--Display Title linked to Course Details-->
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="searchTableLeftTd">
                                Date Entered
                            </td>
                            <td class="searchTableRightTd">
                                <!--Display Date Entered-->
                                <%#Convert.ToDateTime(Eval("DateEntered")).ToString("MM/dd/yyyy")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="searchTableLeftTd">Organization
                            </td>
                            <td class="searchTableRightTd">
                                <!--Display Provider Name linked to Provider Details-->
                                <asp:Label ID="lblProvider" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="searchTableLeftTd">
                            </td>
                            <td class="searchTableRightTd">
                                <br />
                                <!--Approve Checkbox-->
                                <asp:CheckBox runat="server" ID="chkApprove" />
                                Approve
                                <!--Hidden Field to store current Course Id-->
                                <asp:HiddenField ID="hidCourseID" runat="server" Value='<%# Eval("ID") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="searchTableLeftTd">
                            </td>
                            <td class="searchTableRightTd">
                                <!--Reject Course-->
                                <asp:Label ID="lblRejectCourse" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <!--If not last item display horizontal line-->
                <%--<% if (hrcount++ < pendingCourses.Count)
                   {%>
                <hr size="1" />
                <%} %>--%>
            </ItemTemplate>
            <%--End Item Templete--%>
        </asp:DataList>
    </div>
     <div id="NoResult" runat="server" visible="false">
        No records found.
    </div>
    <div style="text-align: left">
        <br />
        <br />
        <asp:Button ID="btnApprove" runat="server" Text="APPROVE SELECTED COURSE(S)" ToolTip="APPROVE SELECTED COURSE(S)"
            OnClientClick="return CheckAprove()" OnClick="btnApprove_Click" CssClass="commonButton btn240" />&nbsp;&nbsp;&nbsp;

<%--            <asp:Button ID="btnAddProvider" runat="server" Text="Add a Course Provider" ToolTip="Add a Course Provider"
                CssClass="commonButton btn163" UseSubmitBehavior="false" OnClientClick="return OpenProviderApplicationPage()" />
--%>                
    </div>
    <br />
</asp:Content>
