<%@ Page Title="" Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true" CodeFile="AddMultipleCourses.aspx.cs" Inherits="Provider_AddMultipleCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">  
    <link rel="stylesheet" type="text/css" href="../css/providerStyle.css" />
       <script>
           $(function () {
               $(".frmDateBox").datepicker({
                   showOn: "button",
                   buttonImage: "../images/icon_calendar.jpg",
                   buttonImageOnly: true
               });
           });
    </script>
    <script language="javascript" type="text/javascript">
               //initialize calender
               init();
    </script>   
    <div id="dvAddCourses" class="title" runat="server">
        Add Multiple Courses
    </div>
    <div class="multiCourseStep">
        <h3>Step 1. Download the excel template</h3>
        <p>Please use the template provided to upload. You can save the template on your desktop. Please do not edit the column headers<br />
        <br />
        Using our template will ensure successful upload.<br />
        <br />
           <a href="LACES_Multicourse_Upload.xlsx">Download Multiple Course Spreadsheet</a>
            <br /> <br />
            <i>The multiple course spreadsheet is an .xlsx file. Please save your completed spreadsheet as .xlsx or .xls.<br /><br /> We’ve updated the LACES Multiple Course Upload Excel template. Please be sure to download and use the updated template. </i>
        </p>
    </div>
    <div class="multiCourseStep">
        <h3>Step 2. Add conference or event information</h3>
        <div class="multiuploadInfoRow">
            <label>*Event Name</label><asp:TextBox ID="uiTxtMultiName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="uiReqEventName" ControlToValidate="uiTxtMultiName" Text="Event Name Required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        </div>
        <div class="multiuploadInfoRow">
            <label>*Start Date</label>
            <asp:TextBox autocomplete="off" ID="txtStartDate" CssClass="frmDateBox" runat="server"  MaxLength="20"></asp:TextBox>
            <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=txtStartDate.ClientID %>', 'mm/dd/yyyy')" src="../images/icon_calendar.jpg"  class="CalenderIcon" />
            <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="uiTxtMultiName" Text="Start Date Required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        </div>
        <div class="multiuploadInfoRow">
            <label>*End Date</label>
            <asp:TextBox autocomplete="off" ID="txtEndDate" CssClass="frmDateBox" runat="server" MaxLength="20"></asp:TextBox>
            <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=txtEndDate.ClientID %>', 'mm/dd/yyyy')" src="../images/icon_calendar.jpg"  class="CalenderIcon" />
            <asp:RequiredFieldValidator ID="reqEndDate" ControlToValidate="txtEndDate" Text="End Date Required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        </div>
        <div class="multiuploadInfoRow">
            <label>*City</label><asp:TextBox ID="uiTxtCity" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqCity" ControlToValidate="uiTxtCity" Text="City Required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        </div>
        <div class="multiuploadInfoRow">
            <label>*State/Province</label>  <asp:DropDownList ID="drpState" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="reqState" ControlToValidate="drpState" Text="State Required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        </div>
        <%--<div class="multiuploadInfoRow">
            <label>Organization Code</label>
            <asp:DropDownList ID="drpCourseType" runat="server">
            </asp:DropDownList>            
        </div>--%>
        <p>Please select your approved provider identification code from the list provided first. This code was assigned when your organization was approved and can be changed on request by emailing <a href="mailto:laces.asla.org"> laces@asla.org. </a></p>
    </div>
    <div class="multiCourseStep">
        <h3>Step 3. Upload your completed spreadsheet</h3>
        <asp:FileUpload ID="uiFUPSpreadsheet" runat="server" />
        <asp:RequiredFieldValidator ID="reqFile" ControlToValidate="uiFUPSpreadsheet" Text="File Required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <p>Please upload your completed spreadsheet.</p>
        <asp:LinkButton ID="uiLnkUploadSpreadSheet" CssClass="uploadButton" Text="Upload" runat="server" OnClick="UploadMultipleCourses" class="commonButton btn133"></asp:LinkButton>
        
        <asp:Label ID="uiLblResults" runat="server"></asp:Label>
    </div>
</asp:Content>

