<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="CourseDetails.aspx.cs" Inherits="Admin_CourseDetails" Title="Admin: Course Details | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">

    <script type="text/javascript">
   
    var txtTitleGlobal = "<%=txtTitle.ClientID %>";
        var btnSaveGlobal = "<%=btnSave.ClientID %>";
        $(document).ready(function () {
        $("#<%=uiRblStatus.ClientID%> input").click(function () {            
            var chkVal = $("#RadioDiv input:radio:checked").val();
            if (chkVal == 'IC') {
                $("#uiTxtEmail").show();
            } else {
                $("#uiTxtEmail").hide();
            }
            });
        });
    </script>
    
    <script type="text/javascript" language="javascript" src="../javascript/Course.js"></script>

    <%--<div id="dvCourseDetails" class="title" runat="server">
        Course Details
    </div>--%>
    <!-- Any Message of the Page or any Error Validation Starts -->
    <div style="text-align: left;">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
    </div>
    <!-- Any Message of the Page or any Error Validation Ends -->
    <!-- [Start] Left Side form field HTML table -->
    <table border="0" cellpadding="1" cellspacing="0" width="100%">
        <tbody>
            <tr id="trTitle" runat="server">
                <td class="frmElement3">
                    *Title
                </td>
                <td>                    
                        <asp:TextBox autocomplete="off" ID="txtTitle" runat="server" CssClass="frmDateBox"
                            MaxLength="100" Width="209px"></asp:TextBox>                    
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                    *Course Registration URL</td>
                <td>
                    <asp:TextBox autocomplete="off" ID="txtHyperlink" runat="server" CssClass="frmDateBox"
                        MaxLength="100" Width="209px"></asp:TextBox>
                    <asp:PlaceHolder ID="uiPhHyperlinkNote" runat="server"></asp:PlaceHolder>
                </td>
            </tr>            
            <tr>
                <td class="frmElement3">
                </td>
                <td>
                    If registration for this course is limited, please briefly explain (e.g. “employees of XYZ Corporation only”).
                </td>
            </tr>
            <tr>
                <td class="frmElement3 paddingSmall">
                    Registration Eligibility</td>                
                <td>
                    <asp:TextBox autocomplete="off" ID="txtRegistrationEligibility" runat="server" Columns="32"
                        CssClass="frmTextArea" Rows="6" TextMode="MultiLine" Width="213px"></asp:TextBox>                    
                    <br />
                    <asp:Label ID="lblRegistrationEligibilityCouter" runat="server" Text="150 &nbsp; character(s) remains."></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td class="frmElement3">
                    *Start Date
                </td>
                <td>
                    <asp:TextBox autocomplete="off" ID="txtStartDate" CssClass="frmDateBox" runat="server"
                        Width="120px" MaxLength="20"></asp:TextBox>
                    <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=txtStartDate.ClientID %>', 'mm/dd/yyyy')"
                        src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                    *End Date</td>
                <td>
                    <asp:TextBox autocomplete="off" ID="txtEndDate" CssClass="frmDateBox" runat="server"
                        Width="120px" MaxLength="20"></asp:TextBox>
                    <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=txtEndDate.ClientID %>', 'mm/dd/yyyy')"
                        src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                </td>
            </tr>
            <tr>
                <td class="smallTopPadd" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="frmElement3 paddingSmall">
                    <span style="display: none;">
                        <asp:TextBox autocomplete="off" ID="TextBox1" runat="server" Width="0px"></asp:TextBox></span>
                    *Description
                </td>
                <td>
                    <asp:TextBox autocomplete="off" ID="txtDescription" runat="server" Columns="32" CssClass="frmDateBox"
                        Rows="15" TextMode="MultiLine" Width="209px" Height="189px"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblDescriptionCouter" runat="server" Text="3600 &nbsp; character(s) remains."></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="smallTopPadd" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                    *City</td>
                <td>
                    <label>
                        <asp:TextBox autocomplete="off" ID="txtCity" runat="server" CssClass="frmDateBox"
                            MaxLength="100" Width="209px"></asp:TextBox>&nbsp;
                    </label>
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                    *State/Province</td>
                <td>
                    <asp:DropDownList ID="drpState" runat="server">
                    </asp:DropDownList>&nbsp;<br />
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                    *Distance Education
                </td>
                <td>
                    <asp:CheckBox ID="chkDistanceEdu" runat="server" ForeColor="Black" Text="" CssClass="singlecheckbox" />
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                </td>
                <td>
                    <a id="uiLnkDistanceLearning" runat="server" target="_blank">Click here</a> for more information on distance education requirements.
                <br />&nbsp;
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                  *Course Equivalency 
                </td>
                <td>
                    <asp:CheckBox ID="chkCourseEquivalency" runat="server" ForeColor="Black" Text="" CssClass="singlecheckbox" />
                    <span class="ifNoSpan">if 'no', leave blank</span>
                </td>
            </tr>
             <tr>
                <td class="frmElement3">
                </td>
                <td>
                    <a id="uiLnkCourseEquiv" runat="server" target="_blank">Click here</a> for more information
                    on course equivalency requirements.
                    <br />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="frmElement3 paddingSmall">
                    *Subjects</td>
                <td>
                    <asp:ListBox ID="drpSubject" Rows="4" runat="server" SelectionMode="Multiple"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                    *Health, Safety and Welfare
                </td>
                <td>
                    <asp:CheckBox ID="chkHealth" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                </td>
                <td>
                    <a id="uiLnkHSWClassification" runat="server" target="_blank">Click here</a> for more information
                    on determining public health, safety and welfare classification.
                </td>
            </tr>
            <tr>
                <td class="smallTopPadd" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                    *Hours</td>
                <td>
                    <asp:DropDownList ID="drpHours" name="drpHours" runat="server" CssClass="frmTextArea"
                        Width="70px">
                        <asp:ListItem Value="0" Selected="True">-</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>                        
                        <asp:ListItem Value="2">2</asp:ListItem>                        
                        <asp:ListItem Value="3">3</asp:ListItem>                        
                        <asp:ListItem Value="4">4</asp:ListItem>                        
                        <asp:ListItem Value="5">5</asp:ListItem>                        
                        <asp:ListItem Value="6">6 </asp:ListItem>                        
                        <asp:ListItem Value="7">7</asp:ListItem>                        
                        <asp:ListItem Value="8">8</asp:ListItem>                        
                        <asp:ListItem Value="9">9</asp:ListItem>                        
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="17">17</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>
                        <asp:ListItem Value="19">19</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="21">21</asp:ListItem>
                        <asp:ListItem Value="22">22</asp:ListItem>
                        <asp:ListItem Value="23">23</asp:ListItem>
                        <asp:ListItem Value="24">24</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                        <asp:ListItem Value="26">26</asp:ListItem>
                        <asp:ListItem Value="27">27</asp:ListItem>
                        <asp:ListItem Value="28">28</asp:ListItem>
                        <asp:ListItem Value="29">29</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="31">31</asp:ListItem>
                        <asp:ListItem Value="32">32</asp:ListItem>
                        <asp:ListItem Value="33">33</asp:ListItem>
                        <asp:ListItem Value="34">34</asp:ListItem>
                        <asp:ListItem Value="35">35</asp:ListItem>
                        <asp:ListItem Value="36">36</asp:ListItem>
                        <asp:ListItem Value="37">37</asp:ListItem>
                        <asp:ListItem Value="38">38</asp:ListItem>
                        <asp:ListItem Value="39">39</asp:ListItem>
                        <asp:ListItem Value="40">40</asp:ListItem>
                    </asp:DropDownList>.
                     <asp:DropDownList ID="drpMinutes" name="drpMinutes" runat="server" CssClass="frmTextArea"
                        Width="70px">
                        <asp:ListItem Value="0" Selected="True">00</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>                        
                        <asp:ListItem Value="50">50</asp:ListItem>                        
                        <asp:ListItem Value="75">75</asp:ListItem>                                                
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                </td>
                <td>
                    <a id="uiLnkCalculatingPDH" runat="server" target="_blank">Click here</a> for more information on
                    calculating professional development hours.
                </td>
            </tr>
            <tr>
                <td class="frmElement3 paddingSmall">
                    *Learning Outcomes
                    <br />
                    A minimum of three learning objectives/outcomes is required for each submitted course. 
                </td>
                <td>
                    <asp:TextBox autocomplete="off" ID="txtLearningOutcome" runat="server" Columns="32"
                        CssClass="frmTextArea" Rows="6" TextMode="MultiLine" Width="213px"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblLearningOutcomeCounter" runat="server" Text="3600 &nbsp; character(s) remains."></asp:Label>
                </td>
            </tr>
            <tr> 
                <td class="frmElement3">
                    *No Proprietary Information
                </td>
                <td>
                    <asp:CheckBox ID="uiChkNoProprietaryInfo" runat="server" value="1" />  I confirm that this course does not include any proprietary information.
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                </td>
                <td>
                     <a href="http://laces.asla.org/ApprovedProviderGuidelines.aspx" target="_blank">Review</a> criterion 5 in LA CES guidelines for more information.
                </td>
            </tr>
             <tr>
                <td class="smallTopPadd" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="smallTopPadd" colspan="2">
                </td>
            </tr>    
            <tr>
                <td class="frmElement3">
                    *Instructors</td>
                <td>
                    <label>
                        <asp:TextBox ID="txtInstructors" runat="server" CssClass="frmDateBox" MaxLength="256" Width="209px"></asp:TextBox>&nbsp;
                    </label>
                </td>
            </tr>
            <tr>
                <td class="smallTopPadd" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="smallTopPadd" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="frmElement3 paddingSmall">
                    Custom Provider Code
                </td>
                <td>
                     <asp:TextBox ID="uiTxtCustomProviderCode" runat="server" CssClass="frmDateBox" MaxLength="256" Width="209px"></asp:TextBox>&nbsp;
                    <!-- Dynamic Table for Course Code Ends -->
                    <!-- Hidden Fields for passing Dynamic Course Code to sercer side Starts -->
                    <asp:HiddenField ID="hidCourseTypes" runat="server" EnableViewState="False" />
                    <asp:HiddenField ID="hidCourseDesc" runat="server" EnableViewState="False" />
                    <!-- Hidden Fields for passing Dynamic Course Code to sercer side Ends -->
                    <!-- Hidden Field of Couser ID used for Edit Mode Starts -->
                    <asp:HiddenField ID="hidCourseID" Value="0" runat="server" />
                    <!-- Hidden Field of Couser ID used for Edit Mode Ends -->
                    <input type="hidden" value="" name="hidAction" id="hidAction" />
                </td>
            </tr>


             <tr>
                <td class="smallTopPadd" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="frmElement3">
                    *Status</td>
                <td>
                    <div id="RadioDiv">
                        <asp:HiddenField ID="uiHidLastStatus" runat="server" />
                   <asp:RadioButtonList ID="uiRblStatus" runat="server">
                       <asp:ListItem Text="Pending Approval" Value="NP"></asp:ListItem>
                       <asp:ListItem Text="Action Required" Value="IC"></asp:ListItem>
                       <asp:ListItem Text="Active" Value="OP"></asp:ListItem>
                       <asp:ListItem Text="Archived" Value="PT"></asp:ListItem>
                   </asp:RadioButtonList>    
                    <div style="display:none" id="uiTxtEmail">
                        <br />
                        <strong>Send Email to Provider with this Body:</strong><br />
                        <asp:TextBox ID="uiTxtEmailText" runat="server" TextMode="MultiLine" Rows="10" Columns="100"></asp:TextBox>
                    </div>
                    </div>
                </td>
            </tr>

            <tr>
                <td class="frmElement22" valign="top">
                </td>
                <td>
                    <br />
                    <asp:Button ID="btnSave" runat="server" CssClass="commonButton btn133" Text="ADD COURSE"
                        OnClick="btnSave_Click" OnClientClick="return checkFormDate()" />
                </td>
            </tr>

            <tr style="margin-top:35px"">
                <td class="frmElement22" valign="top">
                </td>
                <td align="right">
                    <br />
                    <asp:ImageButton ID="Button1" runat="server" ForeColor="Red" Text="DELETE FOREVER"  ImageUrl="~/images/DeleteForever.jpg"
                        OnClick="btDelete_Click" />
                </td>
            </tr>
        </tbody>
    </table>

    <script type="text/javascript">
    
        //visibleNext();
        
        var txtTitleControl = document.getElementById("<%=txtTitle.ClientID %>");
        var txtHyperlinkControl = document.getElementById("<%=txtHyperlink.ClientID %>");
        var btnSaveGlobal = document.getElementById("<%=btnSave.ClientID %>");
        var txtStartDateControl= document.getElementById("<%=txtStartDate.ClientID %>");
        var txtEndDateControl = document.getElementById("<%=txtEndDate.ClientID %>");
        var txtDescriptionControl = document.getElementById("<%=txtDescription.ClientID %>");
        var drpSubjectControl = document.getElementById("<%=drpSubject.ClientID %>");
        var txtLearningOutcomeControl = document.getElementById("<%=txtLearningOutcome.ClientID %>");
        var txtInstructions = document.getElementById("<%=txtInstructors.ClientID%>");
        var lblMsgControl = document.getElementById("<%=lblMsg.ClientID %>");
        var chkPropInfo = document.getElementById("<%=uiChkNoProprietaryInfo.ClientID %>");    
        var rblStatus = document.getElementById("<%=uiRblStatus.ClientID %>");   
        var today = new Date();        

        function checkFormDate()
        {            
            var status = $("#<%=uiRblStatus.ClientID %> input:radio:checked").val();            
            if (status != 'IC') {                
                var tempErrorMsg = "</ul>";
                var errorFound = 0;
                if ($(chkPropInfo).is(":checked") != true) {
                    tempErrorMsg = "<li>Verify course contains no proprietary information.</li>" + tempErrorMsg;
                    txtTitleControl.focus();
                    errorFound = 1;
                }
                if (status == 'OP') {
                    var today = new Date();
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1;//January is 0! 
                    var yyyy = today.getFullYear();
                    if (dd < 10) { dd = '0' + dd }
                    if (mm < 10) { mm = '0' + mm }
                    var today = mm + '/' + dd + '/' + yyyy;
                    if (CompareDate(today, trim(txtEndDateControl.value)) == false) {
                        tempErrorMsg = "<li>The end date has passed so the courst cannot be active</li>" + tempErrorMsg;
                        txtStartDateControl.focus();
                        errorFound = 1;
                    }
                }
                if (trim(txtInstructions.value) == "") {
                    tempErrorMsg = "<li>Enter instructors.</li>" + tempErrorMsg;
                    txtInstructions.focus();
                    errorFound = 1;
                }
                if (trim(txtLearningOutcomeControl.value) == "") {
                    tempErrorMsg = "<li>Enter learning outcomes.</li>" + tempErrorMsg;
                    txtLearningOutcomeControl.focus();
                    errorFound = 1;
                }

                if (trim(drpSubjectControl.value) == "") {
                    tempErrorMsg = "<li>Select course subject.</li>" + tempErrorMsg;
                    drpSubjectControl.focus();
                    errorFound = 1;
                }
                if (trim(txtDescriptionControl.value) == "") {
                    tempErrorMsg = "<li>Enter description.</li>" + tempErrorMsg;
                    txtDescriptionControl.focus();
                    errorFound = 1;
                }
                if (CompareDate(trim(txtStartDateControl.value), trim(txtEndDateControl.value)) == false) {
                    tempErrorMsg = "<li>Start date cannot be greater than end date.</li>" + tempErrorMsg;
                    txtStartDateControl.focus();
                    errorFound = 1;
                }
                if (CompareDateWithTwoYears(trim(txtStartDateControl.value), trim(txtEndDateControl.value)) == false) {
                    tempErrorMsg = "<li>End date must be within 2 years of the start date.</li>" + tempErrorMsg;
                    txtStartDateControl.focus();
                    errorFound = 1;
                }
                if (isDateValid(trim(txtEndDateControl.value)) == false) {
                    tempErrorMsg = "<li>Enter valid end date.</li>" + tempErrorMsg;
                    txtEndDateControl.focus();
                    errorFound = 1;
                }
                if (isDateValid(trim(txtStartDateControl.value)) == false) {
                    tempErrorMsg = "<li>Enter valid start date.</li>" + tempErrorMsg;
                    txtStartDateControl.focus();
                    errorFound = 1;
                }
                if (trim(txtHyperlinkControl.value) == "") {
                    tempErrorMsg = "<li>Enter course hyperlink.</li>" + tempErrorMsg;
                    txtHyperlinkControl.focus();
                    errorFound = 1;
                }
                if (trim(txtTitleControl.value) == "") {
                    tempErrorMsg = "<li>Enter course title.</li>" + tempErrorMsg;
                    txtTitleControl.focus();
                    errorFound = 1;
                }
                if (errorFound == 1) {
                    lblMsgControl.innerHTML = "Please correct the following error(s):<br/><ul>" + tempErrorMsg;

                    setTimeout('scrollTo(0,0)', 5);

                    return false;
                }

                needToConfirm = false;
                setTimeout('resetFlag()', 250);

                return true;
            } else {
                return true;
            }
        }
    
    </script>

</asp:Content>
