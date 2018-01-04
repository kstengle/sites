<%@ Page Title="" Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true" CodeFile="FindCourses.aspx.cs" Inherits="Admin_FindCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">
     <script language="javascript" type="text/javascript">
         //initialize calender
         init();
    </script>  
    <link rel="stylesheet" type="text/css" href="/css/providerStyle.css" />
    <style type="text/css">
        .courseitem {
            list-style: none;
            margin-bottom:10px;
            margin-left: -35px;
        }        
        .leftspan {
            display: inline-block;
            width: 100px;
            
        }
        .courserow {
              display: block;
        }
        .page_navigation {
	padding:10px;
	text-align:center;
}      
.page_navigation .page_link {
	padding:2px 5px;
	border:1px solid transparent;
	border-radius:2px;
	-webkit-border-radius:2px;
	-moz-border-radius:2px;
}
.page_navigation .previous_link {
	padding:2px 5px;
}
.page_navigation .next_link {
	padding:2px 5px;
}
.page_navigation .active_page {
	background:none;
	border:1px solid #0E0F76;
	text-decoration:underline;
}

    </style>
    <asp:ScriptManager ID="uiSM" runat="server"></asp:ScriptManager>
    <div class="title"><asp:Literal ID="uiLitHeaderText" runat="server" Text="All Courses"></asp:Literal></div>
    <br />
    

    <div class="topBox">       
        <h5>Find Courses</h5>
        <p><strong>To Narrow Your Options, Uncheck:</strong></p>
        <asp:CheckBoxList ID="uiChxBoxListStatus" runat="server" OnSelectedIndexChanged="FilterCourses" RepeatDirection="Horizontal" AutoPostBack="true"></asp:CheckBoxList><br /><br />
         
        <asp:CheckBoxList ID="uiChkSubmitType" runat="server" OnSelectedIndexChanged="FilterCourses" RepeatDirection="Horizontal" AutoPostBack="true">
             <asp:ListItem Value="S" Text="Individual Pending Course"></asp:ListItem>
             <asp:ListItem Value="M" Text="Multiple Pending Course"></asp:ListItem>

        </asp:CheckBoxList>
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
        <div class="cell subject" style="width:45%;">
            <p>Provider</p>              
                (Hold down Control or Apple key to select more than one)                
                    <asp:ListBox ID="lbEducationProvider" runat="server" SelectionMode="Multiple" CssClass="solidBorder"></asp:ListBox>
        </div>
        <asp:LinkButton ID="uiLnkSearch" runat="server" Text="Search" OnClick="FilterCourses"></asp:LinkButton>
        <div style="clear:both"></div>
    </div>

    <!--Start Left Content Place Holder-->
    <div class="title">
        Course List
    </div>
    <br />


    <!--DalaList to display Course items-->
    <asp:UpdatePanel ID="uiUpdatePnl" runat="server">
        <ContentTemplate>
        <div class="Courses"><ul id="CourseList">
                       
    <asp:Repeater ID="dlCourseList" runat="server" OnItemCreated="dlCourseList_ItemCreated">           
        <%--Starting Item Templete--%>

        <ItemTemplate>           
                <li class="courseitem">
                    <div class="courserow">
                        <span class="leftspan">Course Title:</span>
                        <span class="rightspan"><strong><asp:Label ID="lblTitle" runat="server"></asp:Label></strong></span>                
                    </div>
                    <div class="courserow">
                        <span class="leftspan">Start Date:</span>
                        <span class="rightspan"><strong><asp:Label ID="lblStartDate" runat="server"></asp:Label></strong></span>                
                    </div>
                    <div class="courserow">
                        <span class="leftspan">End Date:</span>
                        <span class="rightspan"><strong><asp:Label ID="lblEndDate" runat="server"></asp:Label></strong></span>                
                    </div>
                     <div class="courserow">
                        <span class="leftspan">Provider:</span>
                        <span class="rightspan"><strong><asp:Label ID="lblProviderName" runat="server"></asp:Label></strong></span>                
                    </div>
                    <div class="courserow">
                        <span class="leftspan">Status:</span>
                        <span class="rightspan">
                            <strong><asp:Label ID="lblStatus" runat="server"></asp:Label></strong>                            
                        </span>
                    </div>                    
                </li>                      
        </ItemTemplate>        
        <%--End Item Templete--%>
    </asp:Repeater>
           
            </ul>             
            <div class="page_navigation"></div>    
        </div>
    
    </ContentTemplate>
        
    </asp:UpdatePanel>
   <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <div id="NoResult" runat="server" visible="false">
        No records found.
    </div>
    <script type="text/javascript" language="javascript" src="/javascript/jquery.pajinate.js"></script>
	<script type="text/javascript">
	    $(document).ready(function () {
	        $('.Courses').pajinate({
	            item_container_id: '#CourseList',
	            items_per_page: 10,
	            num_page_links_to_display: 20
	        });
	    });
	</script>

</asp:Content>

