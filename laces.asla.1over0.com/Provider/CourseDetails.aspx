<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="CourseDetails.aspx.cs" Inherits="CourseDetails" Title="Course Details | LA CES™" %>

<asp:Content ID="LftTxt" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            $('#txtStartDate, #txtEndDate').attr("readonly", "readonly");
            $("#txtStartDate, #txtEndDate").datepicker({
                beforeShow: function() {
                    setTimeout(function() {
                        //if (window.innerWidth < 480) {
                        //    $('html, body').animate({"scrollTop": 0});
                        //}

                        $(document).unbind('mousedown', $.datepicker._checkExternalClick);
                        styleButtonPanel();
                    }, 0);
                    checkEvents($(this).datepicker());
                },                
                //changeMonth: true,
                //changeYear: true,
                //closeText: "Close",
                //currentText: "Reset",
                //minDate: 0,
                // monthNamesShort: $.datepicker.regional["en"].monthNames,
                onChangeMonthYear: function() {
                    setTimeout(function() {
                        styleButtonPanel();
                    }, 0);
                },
                onClose: function() {
                    setTimeout(function() {
                        $('.uk-modal-close').trigger("click");
                    }, 0);
                }
            });

            $(".hasDatepicker").each(function (index, element) {
                var context = $(this);
                context.on("blur", function (e) {
                    // The setTimeout is the key here.
                    setTimeout(function () {
                        if (!context.is(':focus')) {
                            $(context).datepicker("hide");
                        }
                    }, 250);
                });
            });

            var styleButtonPanel = function() {
                $('.ui-datepicker-buttonpane').addClass("uk-clearfix");
                $('.ui-datepicker-current').removeClass("ui-priority-secondary ui-state-default ui-priority-primary ui-corner-all").addClass("button__form-btn button__form-btn--white");
                $('.ui-datepicker-close').removeClass("ui-priority-secondary ui-state-default ui-priority-primary ui-corner-all").addClass("uk-modal-close button__form-btn button__form-btn--white");
            }

            var checkEvents = function(pickerID) {
                $('.ui-datepicker').on("click", [pickerID], function(event) {
                    console.log(pickerID);
                    if ($(event.target).hasClass("ui-datepicker-current")) {
                        $(pickerID).datepicker("setDate", new Date());
                        // $(event.target).datepicker("setDate", new Date());
                    }             
                });
            }
        });        

    var txtTitleGlobal = "<%=txtTitle.ClientID %>";
    var txtHidTypeGlobal = "<%=txtHidType.ClientID %>";
    </script>

    <script type="text/javascript" language="javascript" src="/javascript/Course.js"></script>
    <p>*Required</p>
    <div class="uk-grid entryform">
    <div id="dvCourseDetails" class="title uk-width-1-1" runat="server">
        Course Details        
    </div>    
    <!-- Any Message of the Page or any Error Validation Starts -->
    <div style="text-align: left;" class="uk-width-1-1">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
    </div>
    <!-- Any Message of the Page or any Error Validation Ends -->
    <!-- [Start] Left Side form field HTML table -->    
     <div id="trTitle" runat="server" class="uk-width-1-4">                
                    *Title
    </div>
    <div class="uk-width-3-4" id="uiDivTextBox" runat="server">
        <asp:TextBox autocomplete="off" ID="txtTitle" runat="server" CssClass="frmDateBox"
            MaxLength="100" Width="60%"></asp:TextBox>
    </div>
    <div class="frmElement3 uk-width-1-4">
                    *Course Registration URL
    </div>
    <div class="uk-width-3-4">
            <asp:TextBox autocomplete="off" ID="txtHyperlink" runat="server" CssClass="frmDateBox" MaxLength="100" Width="60%"></asp:TextBox>
    </div>
    <div id="trStatus" runat="server" class="frmElement3 uk-width-1-1">
                    *Status    
        <asp:RadioButtonList ID="rdoStatusList" runat="server" Width="80px">
            <asp:ListItem Value="OP">Open</asp:ListItem>
            <asp:ListItem Value="CL">Closed</asp:ListItem>
        </asp:RadioButtonList>
    </div>            
    <div class="frmElement3 paddingSmall uk-width-1-4">
            Registration Eligibility
    </div>                
    <div class="uk-width-3-4">
                    <asp:TextBox autocomplete="off" ID="txtRegistrationEligibility" runat="server" 
                        CssClass="frmTextArea" Rows="6" TextMode="MultiLine" Width="60%"></asp:TextBox>                    
                    <br />
                    If registration for this course is limited, please briefly explain (e.g. “employees of XYZ Corporation only”).<br />
                    <asp:Label ID="lblRegistrationEligibilityCouter" runat="server" Text="150 &nbsp; character(s) remains."></asp:Label>
    </div>    
    <div class="frmElement3 uk-width-1-4">
                    *Start Date
    </div>
    <div class="uk-width-3-4">
        <asp:TextBox autocomplete="off" ID="txtStartDate" CssClass="frmDateBox" runat="server"
            Width="120px" MaxLength="20" ClientIDMode="Static"></asp:TextBox>        
    </div>                
    <div class="frmElement3 uk-width-1-4">
                    *End Date
    </div>
    <div class="uk-width-3-4">
            <asp:TextBox autocomplete="off" ID="txtEndDate" CssClass="frmDateBox" runat="server"
                Width="120px" MaxLength="20" ClientIDMode="Static"></asp:TextBox>            
    </div>    
    <div class="frmElement3 paddingSmall uk-width-1-4">
        <span style="display: none;">
            <asp:TextBox autocomplete="off" ID="txtHidType" runat="server" Width="0px"></asp:TextBox></span>
        *Description
    </div>
    <div class="uk-width-3-4">
                    <asp:TextBox autocomplete="off" ID="txtDescription" runat="server" Columns="32" CssClass="frmDateBox"
                        Rows="15" TextMode="MultiLine" Width="60%" Height="189px"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblDescriptionCouter" runat="server" Text="3600 &nbsp; character(s) remains."></asp:Label>
    </div>            
    <div class="frmElement3 uk-width-1-4">
        *City
    </div>
    <div class="uk-width-3-4">
        <asp:TextBox ID="txtCity" runat="server" CssClass="frmDateBox" MaxLength="100" Width="60%"></asp:TextBox>&nbsp;
    </div>    
    <div class="frmElement3 uk-width-1-4">
        *State/Province
    </div>
     <div class="uk-width-3-4">
        <asp:DropDownList ID="drpState" runat="server" style="max-width:100%;">
        </asp:DropDownList>
    </div>                 
    <div class="frmElement3 uk-width-1-4">
        *Distance Education
    </div>
    <div class="uk-width-3-4">
        <asp:CheckBox ID="chkDistanceEdu" runat="server" ForeColor="Black" Text="" CssClass="singlecheckbox" />
                    <span class="ifNoSpan">if 'no', leave blank</span>
    </div>
     <div class="uk-width-1-4"></div>
        <div class="uk-width-3-4">
                    <a runat="server" id="uiLnkDistanceLearning" target="_blank">Learn</a> more information
                    on distance education requirements.
                    <br />
                    &nbsp;
        </div>
        <div class="frmElement3 uk-width-1-4">
                  *Course Equivalency 
        </div>
        <div class="uk-width-3-4">
                    <asp:CheckBox ID="chkCourseEquivalency" runat="server" ForeColor="Black" Text="" CssClass="singlecheckbox" />
                    <span class="ifNoSpan">if 'no', leave blank</span>
        </div>
        <div class="frmElement3 uk-width-1-4"></div>
        <div class="uk-width-3-4">
            <a id="uiLnkCourseEquiv" runat="server" target="_blank">Learn</a> more information
            on course equivalency requirements.
            <br />
            &nbsp;
        </div>            
        <div class="frmElement3 paddingSmall uk-width-1-4">
            *Subjects</div>
        <div class="uk-width-3-4">
                    <asp:ListBox ID="drpSubject" Rows="4" runat="server" SelectionMode="Multiple" style="max-width:100%;"></asp:ListBox>
        </div>
        <div class="frmElement3 uk-width-1-4">
            *Health, Safety and Welfare
        </div>
        <div class="uk-width-3-4">
                    <asp:CheckBox ID="chkHealth" runat="server" Text="" />
                    <span class="ifNoSpan">if 'no', leave blank</span>
        </div>
        <div class="frmElement3 uk-width-1-4"></div>
        <div class="uk-width-3-4">
                    <a id="uiLnkHSWClassification" runat="server" target="_blank">Learn</a> more information
                    on determining public health, safety and welfare classification.                
        </div>        
        <div class="frmElement3 uk-width-1-4">
                   *Professional Development Hours (PDH)
        </div>
        <div class="uk-width-3-4">
                    <asp:DropDownList ID="drpHours" name="drpHours" runat="server" CssClass="frmTextArea"
                        Width="70px">                        
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
            </div>
            <div class="frmElement3 uk-width-1-4"></div>
            <div class="uk-width-3-4">
                    <a id="uiLnkCalculatingPDH" runat="server" target="_blank">Learn</a> more information on
                    calculating professional development hours.
            </div>
            <div class="frmElement3 paddingSmall uk-width-1-4">
                *Learning Outcomes
                <br />
                A minimum of three learning objectives/outcomes is required for each submitted course. 
            </div>
            <div class="uk-width-3-4">
                    <asp:TextBox autocomplete="off" ID="txtLearningOutcome" runat="server" Columns="32"
                        CssClass="frmTextArea" Rows="6" TextMode="MultiLine" Width="60%" ClientIDMode="Static"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblLearningOutcomeCounter" runat="server" Text="3600 &nbsp; character(s) remains."></asp:Label>
            </div>
            <div class="frmElement3 uk-width-1-4">
                *No Proprietary Information
            </div>
            <div class="uk-width-3-4">                
                    <asp:CheckBox ID="uiChkNoProprietaryInfo" runat="server" value="1" /> I confirm this course doesn't include any propietary information.               
            </div>
            <div class="frmElement3 uk-width-1-4"></div>
            <div class="uk-width-3-4">
                     <a href="http://laces.asla.org/ApprovedProviderGuidelines.aspx" target="_blank">Review</a> criterion 5 in LA CES guidelines for more information.
            </div>              
            <div class="frmElement3 uk-width-1-4">
                *Instructors
            </div>
             <div class="uk-width-3-4">                    
                        <asp:TextBox ID="txtInstructors" runat="server" CssClass="frmDateBox" MaxLength="256" Width="60%"></asp:TextBox>&nbsp;
                    <br /><br />Add all applicable organizational codes and state ID numbers below:                
            </div>                                
            <div class="frmElement3 paddingSmall uk-width-1-4">
                    Provider Custom Course Code
                </div>
            <div class="uk-width-3-4">
                    <!-- Dynamic Table for Course Code Starts -->
                    <asp:TextBox ID="uiTxtCustomProviderCode" runat="server" CssClass="frmDateBox" MaxLength="256" Width="60%"></asp:TextBox>&nbsp;
                    <!-- Dynamic Table for Course Code Ends -->
                    <!-- Hidden Fields for passing Dynamic Course Code to sercer side Starts -->
                    <asp:HiddenField ID="hidCourseTypes" runat="server" EnableViewState="False" />
                    <asp:HiddenField ID="hidCourseDesc" runat="server" EnableViewState="False" />
                    <!-- Hidden Fields for passing Dynamic Course Code to sercer side Ends -->
                    <!-- Hidden Field of Couser ID used for Edit Mode Starts -->
                    <asp:HiddenField ID="hidCourseID" Value="0" runat="server" />
                    <!-- Hidden Field of Couser ID used for Edit Mode Ends -->
                    <input type="hidden" value="" name="hidAction" id="hidAction" />
            </div>
            <div class="uk-width-1-4"></div>
            <div class="uk-width-3-4">Course codes are numbers or names used to identify courses. For example, college and universities use course codes such as "LAR311" to identify a specific course. Please select your approved provider identification code from the list provided first. This code was assigned when your organization was approved and can be changed on request by e-mailing <a href="mailto:laces@asla.org">laces@asla.org</a>.
            </div>
            <div class="uk-width-1-4">                    
                            <div style="padding-bottom:51px;"><asp:Button ID="btnSave" runat="server" CssClass="commonButton btn133" Text="ADD COURSE"
                        OnClick="btnSave_Click" OnClientClick="return checkFormDate()" /></div>
                <asp:HiddenField ID="uiHdnCourseToRenew" runat="server" />
                            <div style="padding-left:20px;"><div class="brackets" runat="server" id="uiSpanDuplicateHelpText" visible="false"><asp:label Text="Each course is an individual record in LA CES. If this course is held multiple times, use the “quick form” link on the confirmation page after adding this course.  The LA CES quick form makes it easier to change dates, locations, instructors, or other information quickly and create a new version of this course." ID="uiLblDuplicateHelpText" runat="server"></asp:label></div></div>                        
                    </div>
                    
                    
                </div>            
    <!-- [End] Left Side form field HTML table -->
        </div>
    <script type="text/javascript">
    
//        visibleNext();
    
    
        var txtTitleControl = document.getElementById("<%=txtTitle.ClientID %>");
        var txtHyperlinkControl = document.getElementById("<%=txtHyperlink.ClientID %>");
        var btnSaveGlobal = document.getElementById("<%=btnSave.ClientID %>");
        var txtStartDateControl= document.getElementById("<%=txtStartDate.ClientID %>");
        var txtEndDateControl = document.getElementById("<%=txtEndDate.ClientID %>");
        var txtDescriptionControl = document.getElementById("<%=txtDescription.ClientID %>");
        var drpSubjectControl = document.getElementById("<%=drpSubject.ClientID %>");
        var txtLearningOutcomeControl = document.getElementById("<%=txtLearningOutcome.ClientID %>");
        var lblMsgControl = document.getElementById("<%=lblMsg.ClientID %>");        
        var chkPropInfo = document.getElementById("<%=uiChkNoProprietaryInfo.ClientID %>");    
        var txtInstructions = document.getElementById("<%=txtInstructors.ClientID%>");
        var txtCity = document.getElementById("<%=txtCity.ClientID%>");
        var txtState = document.getElementById("<%=drpState.ClientID%>");
        var chkDistanceEdu = document.getElementById("<%=chkDistanceEdu.ClientID%>");
      
        //focus the title field
        //txtTitleControl.focus();
        
        function checkFormDate()
        {
            
            var tempErrorMsg= "</ul>";
            var errorFound =0;
            if ($(chkPropInfo).is(":checked") != true) {
                tempErrorMsg = "<li>Verify course contains no proprietary information.</li>" + tempErrorMsg;
                txtTitleControl.focus();
                errorFound = 1;
            }
            if ($(chkDistanceEdu).is(":checked") != true) {
                if (trim(txtCity.value) == "") {
                    tempErrorMsg = "<li>Enter City.</li>" + tempErrorMsg;
                    txtCity.focus();
                    errorFound = 1;
                }
                if (trim(txtState.value) == "") {
                    tempErrorMsg = "<li>Enter State.</li>" + tempErrorMsg;
                    txtState.focus();
                    errorFound = 1;
                }
            }
            if(trim(txtLearningOutcomeControl.value)=="" )
            {
                tempErrorMsg = "<li>Enter learning outcomes.</li>" + tempErrorMsg; 
                txtLearningOutcomeControl.focus();
                errorFound=1;
            }
            if (trim(txtInstructions.value) == "") {
                tempErrorMsg = "<li>Enter instructors.</li>" + tempErrorMsg;
                txtInstructions.focus();
                errorFound = 1;
            }
            if(trim(drpSubjectControl.value)=="" )
            {
                tempErrorMsg = "<li>Select course subject.</li>" + tempErrorMsg; 
                drpSubjectControl.focus();
                errorFound=1;
            }
            if(trim(txtDescriptionControl.value)=="" )
            {
                tempErrorMsg = "<li>Enter description.</li>"  + tempErrorMsg; 
                txtDescriptionControl.focus();
                errorFound=1;
            }
            if(CompareDate(trim(txtStartDateControl.value),trim(txtEndDateControl.value)) == false)
            {
                tempErrorMsg = "<li>Start date cannot be greater than end date.</li>"  + tempErrorMsg; 
                txtStartDateControl.focus();
                errorFound=1;
            }
            if (CompareDateWithTwoYears(trim(txtStartDateControl.value), trim(txtEndDateControl.value)) == false) {
                tempErrorMsg = "<li>End date must be within 2 years of the start date.</li>" + tempErrorMsg;
                txtStartDateControl.focus();
                errorFound = 1;
            }

            if(isDateValid(trim(txtEndDateControl.value)) == false)
            {
                tempErrorMsg = "<li>Enter valid end date.</li>"  + tempErrorMsg; 
                txtEndDateControl.focus();
                errorFound=1;
            }
            if(isDateValid(trim(txtStartDateControl.value)) == false)
            {
                tempErrorMsg = "<li>Enter valid start date.</li>" + tempErrorMsg; 
                txtStartDateControl.focus();
                errorFound=1;
            }
            if(trim(txtHyperlinkControl.value)=="")
            {
                tempErrorMsg = "<li>Enter course hyperlink.</li>" + tempErrorMsg; 
                txtHyperlinkControl.focus();
                errorFound=1;
            }
            if(trim(txtTitleControl.value)=="" )
            {
                tempErrorMsg = "<li>Enter course title.</li>"  + tempErrorMsg; 
                txtTitleControl.focus();
                errorFound=1;
            }           
            
            if(errorFound==1)
            {
                lblMsgControl.innerHTML="Please correct the following error(s):<br/><ul>" + tempErrorMsg;
                setTimeout('scrollTo(0,0)', 5);
            
                return false;
            }
            
            ///getFinalValue('" + hidCourseTypes.ClientID + "','" + hidCourseDesc.ClientID + "');"
            
            
            needToConfirm = false;
            setTimeout('resetFlag()', 250);
            
            return true;
        }
    
    </script>

</asp:Content>
