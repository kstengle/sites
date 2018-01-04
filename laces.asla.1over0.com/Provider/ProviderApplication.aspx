<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="ProviderApplication.aspx.cs" Inherits="Visitor_ProviderApplication" MaintainScrollPositionOnPostback="false"
    Title="Approved Provider Application | LA CES™" %>

<%@ Register TagPrefix="pantheon" Namespace="Pantheon.Web.UI.Validators" Assembly="Pantheon.Web.UI.Validators" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">

    <script type="text/javascript" language="javascript" src="../javascript/ProviderApplication.js"></script>

    <div class="APtitle" runat="server">
        LA CES™ Approved Provider Application
    </div>
    <div class="APsubtext" runat="server">
        Please leave your browser window open in order for the best experience while applying
        to become an approved vendor
    </div>
    <div id="PageHeader" class="APSubtitle" runat="server">
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <!-- Any Message of the Page or any Error Validation Starts -->
            <div style="text-align: left;">
                <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
            </div>
            <div class="moreLineHeight APCriteria">
                An organization completing this application should already be offering continuing
                education programs that meet the LA CES criteria or planning to do so shortly.
            </div>
            <!-- Any Message of the Page or any Error Validation Ends -->
            <table border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td class="APLabel">
                        Name of organization:<asp:TextBox ID="OrganizationName" MaxLength="200" runat="server"
                            CssClass="APTextbox" Width="300px">
                        </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="OrganizationName" ErrorMessage="Enter organization name."
                            SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>Website:<asp:TextBox
                                ID="OrganizationWebSite" MaxLength="200" runat="server" CssClass="APTextbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Street address:<asp:TextBox ID="OrganizationStreetAddress" MaxLength="200" runat="server"
                            CssClass="APTextbox" Width="390px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="OrganizationStreetAddress"
                            ErrorMessage="Enter street address." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        City:<asp:TextBox ID="OrganizationCity" MaxLength="100" runat="server" CssClass="APTextbox">
                        </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                            ControlToValidate="OrganizationCity" ErrorMessage="Enter city." SetFocusOnError="True"
                            Display="None"></asp:RequiredFieldValidator>State/Prov:<asp:TextBox ID="OrganizationState"
                                MaxLength="50" runat="server" CssClass="APTextbox">
                            </asp:TextBox>Zip:<asp:TextBox ID="OrganizationZip" MaxLength="15" runat="server"
                                CssClass="APTextbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="OrganizationZip"
                            ErrorMessage="Enter zip." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Country:<asp:TextBox ID="OrganizationCountry" MaxLength="100" runat="server" CssClass="APTextbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="OrganizationCountry"
                            ErrorMessage="Enter country." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Phone:<asp:TextBox ID="OrganizationPhone" MaxLength="15" runat="server" CssClass="APTextbox">
                        </asp:TextBox>Fax:<asp:TextBox ID="OrganizationFax" MaxLength="15" runat="server"
                            CssClass="APTextbox"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="APLabel">
                        Primary contact:
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Name:<asp:TextBox ID="ApplicantName" MaxLength="100" runat="server" CssClass="APTextbox">
                        </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                            ControlToValidate="ApplicantName" ErrorMessage="Enter name." SetFocusOnError="True"
                            Display="None">
                        </asp:RequiredFieldValidator>Position:<asp:TextBox ID="ApplicantPosition" MaxLength="100"
                            runat="server" CssClass="APTextbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ApplicantPosition"
                            ErrorMessage="Enter position." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Phone:<asp:TextBox ID="ApplicantPhone" MaxLength="15" runat="server" CssClass="APTextbox">
                        </asp:TextBox>Fax:<asp:TextBox ID="ApplicantFax" MaxLength="15" runat="server" CssClass="APTextbox">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Email:<asp:TextBox ID="ApplicantEmail" MaxLength="100" runat="server" CssClass="APTextbox">
                        </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                            ControlToValidate="ApplicantEmail" ErrorMessage="Enter email." SetFocusOnError="True"
                            Display="None"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                runat="server" ControlToValidate="ApplicantEmail" ErrorMessage="Enter valid email."
                                SetFocusOnError="False" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            </asp:RegularExpressionValidator>Email Confirm:<asp:TextBox ID="EmailConfirm" runat="server"
                                CssClass="APTextbox" MaxLength="100">
                            </asp:TextBox><asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="EmailConfirm"
                                ControlToCompare="ApplicantEmail" ErrorMessage="Email and confirm email do not match."
                                SetFocusOnError="True" Display="None"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="EmailConfirm"
                            ErrorMessage="Enter email confirm." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" width="70%">
                <tr>
                    <td class="APLabel">
                        Login:
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Password:<asp:TextBox ID="Password" runat="server" CssClass="APTextbox" TextMode="Password"
                            MaxLength="40">
                        </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="Password" ErrorMessage="Enter password." SetFocusOnError="True"
                            Display="None">
                        </asp:RequiredFieldValidator>Password Confirm:<asp:TextBox ID="PasswordConfirm" runat="server"
                            CssClass="APTextbox" TextMode="Password" MaxLength="40"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="PasswordConfirm"
                            ControlToCompare="Password" ErrorMessage="Password and confirm password do not match."
                            SetFocusOnError="True" Display="None"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="PasswordConfirm"
                            ErrorMessage="Enter password confirm." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="HidPass" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" width="90%">
                <tr>
                    <td class="APLabel">
                        Explain the nature and mission of your organization:
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <asp:TextBox ID="OrganizationNature" runat="server" Columns="75" CssClass="APTextarea"
                            Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Check below the statement that describes your organization:
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <asp:CheckBox ID="LegallyConstituted" runat="server" /><span class="APLabel">Legally
                            constituted organization - manufacturer, service group, firm, other:</span>
                    </td>
                </tr>
                <tr>
                    <td class="APLabelZeroLeftPad">
                        <asp:TextBox ID="LegallyConstitutedDescription" MaxLength="200" runat="server" CssClass="APTextboxZeroLeftPad"
                            Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <asp:CheckBox ID="RegionallyAccredited" runat="server" />
                        Regionally or nationally accredited school, college, or university - list accrediting
                        agency below:
                    </td>
                </tr>
                <tr>
                    <td class="APLabelZeroLeftPad">
                        <asp:TextBox ID="RegionallyAccreditedDescription" MaxLength="200" runat="server"
                            CssClass="APTextboxZeroLeftPad" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <asp:CheckBox ID="ProfessionalAssociation" runat="server" />
                        Professional association or other not-for-profit or nonprofit organization
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <asp:CheckBox ID="FederalOrganization" runat="server" />
                        Federal &nbsp;
                        <asp:CheckBox ID="StateOrganization" runat="server" />
                        State &nbsp;
                        <asp:CheckBox ID="LocalGovernmentAgency" runat="server" />
                        Local government agency
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="BtnStep1Next" runat="server" Text="NEXT" CssClass="commonButton btn107 APButton"
                OnClick="BtnStep1Next_Click" ToolTip="CONTINUE TO NEXT STEP" />
            <br />
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div style="text-align: left;">
                <asp:Label ID="lbl2Msg" runat="server" ForeColor="red"></asp:Label>
            </div>
            <table border="0" cellpadding="0" cellspacing="0" width="90%" valign="top">
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 1.</b> Registered courses must adhere to the LA CES definition of continuing
                        professional education: "Continuing professional education consists of learning
                        experiences that enhance and expand the skills, knowledge, and abilities of practicing
                        landscape architects to remain current and render competent professional service
                        to clients and the public.":
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        1.1
                    </td>
                    <td class="APLabel">
                        Does your organization understand the LA CES definition of continuing professional
                        education and agree to offer courses for landscape architects that meet this definition?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="OrganizationUnderstandLACES" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 2.</b> Registered courses must specify whether the primary subject
                        matter qualifies as meeting the LA CES health, safety, and welfare definition. Seventy-five
                        (75) percent of the course material must qualify under this definition for courses
                        to be identified as meeting the LA CES health, safety, and welfare standard. (<a
                            id="uiLnkHSWClassification" target="_blank" runat="server">Click here</a> for
                        more information on determining public health, safety, and welfare classification.)
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        2.1
                    </td>
                    <td class="APLabel">
                        Are courses offered by your organization designated as meeting the LA CES definition
                        of health, safety, and welfare?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="CoursesOfferedAsLACES" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                            <asp:ListItem Text="Yes" Value="True" ></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                       
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        2.2
                    </td>
                    <td class="APLabel">
                        If you answered "no" to question 2.1, does your organization agree to designate
                        registered courses as meeting the LA CES health, safety, and welfare definition?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                       <asp:RadioButtonList ID="AgreeToDesignateCoursesAsLACES" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True" ></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                        
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 3.</b> Registered courses must comply with LA CES guidelines in the
                        assignment of professional development hours. All courses must be at least 1 PDH in length. (<a id="uiLnkCalculating_PDH" target="_blank"
                            runat="server">Click here</a> for more information on calculating professional
                        development hours and <a id="uiLnkDistance_Education" target="_blank" runat="server">
                            click here</a> for more information on distance education requirements.)

                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        3.1
                    </td>
                    <td class="APLabel">
                        Does your organization follow LA CES guidelines in assigning professional development
                        hours to registered courses?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="FollowLACESGuidelines" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        3.2
                    </td>
                    <td class="APLabel">
                        If you answered "no" to question 3.1, does your organization agree to follow LA
                        CES guidelines in assigning professional development hours to its registered courses?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="AgreeToFollowLACESGuidelines" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 4.</b>  
                        <i>Registered courses must be planned in response to the learning needs of target audiences and include clear and concise written statements of learning objectives/outcomes. Providers are required to include a minimum of three learning objectives/outcomes for each course.</i>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.1
                    </td>
                    <td class="APLabel">
                        Briefly explain below how your organization determines which courses to offer for
                        landscape architects:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="HowDeterminesCourses" runat="server" Columns="75" CssClass="APTextarea"
                            Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.2
                    </td>
                    <td class="APLabel">
                        Does your organization use an organized and systematic process for identifying the
                        professional development needs of landscape architects?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="OrganizedAndSystematicProcess" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.3
                    </td>
                    <td class="APLabel">
                        Describe below any procedures, surveys, or materials that your organization uses
                        to identify the educational needs of landscape architects (samples may be requested
                        by the Application Review Committee):
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="DescribeProcedures" runat="server" Columns="75" CssClass="APTextarea"
                            Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.4
                    </td>
                    <td class="APLabel">
                        Learning objectives/outcomes are written statements of what participants are expected
                        to accomplish as a result of the course. Does your organization develop written
                        learning objectives/outcomes? Check the most appropriate choice below.
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="WrittenLearningObjectives" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                            <asp:ListItem Text="Always" Value="1" ></asp:ListItem>
                            <asp:ListItem Text="Sometimes" Value="2" ></asp:ListItem>
                            <asp:ListItem Text="Never" Value="3" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.5
                    </td>
                    <td class="APLabel">
                        If you answered "always" or "sometimes" to question 4.4, provide examples of learning objectives/outcomes for two (2) different courses your organization has sponsored. A minimum of three (3) learning objectives/outcomes is required for each course: 
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="DescribeCourse1" runat="server" Columns="75" CssClass="APTextarea"
                            Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="DescribeCourse2" runat="server" Columns="75" CssClass="APTextarea"
                            Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.6
                    </td>
                    <td class="APLabel">
                        If you answered "never" to question 4.4, does your organization agree to develop
                        written learning objectives/outcomes for all programs submitted to LA CES?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="AgreeToDevelopWrittenObjectives" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.7
                    </td>
                    <td class="APLabel">
                        Submit in the spaces provided below example learning outcomes (you may use one of
                        the examples provided in question 4.5), course outline, and seventy-five-to-one-hundred
                        (75-100) word description of a course:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        Learning Outcome<asp:TextBox ID="uiTxtLearningOutcome" runat="server" Columns="75"
                            CssClass="APTextarea" Rows="3" TextMode="MultiLine"></asp:TextBox><br />
                        Course Outline<asp:TextBox ID="uiTxtCourseOutline" runat="server" Columns="75" CssClass="APTextarea"
                            Rows="3" TextMode="MultiLine"></asp:TextBox><br />
                        Description<asp:TextBox ID="uiTxtDescription" runat="server" Columns="75" CssClass="APTextarea"
                            Rows="3" TextMode="MultiLine"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 5.</b> Registered courses must use qualified instructional personnel
                        in course development and delivery; include content and instructional methods that
                        are appropriate for the intended learning objectives/outcomes; and use materials
                        that do not contain proprietary information, are educational and generic in nature,
                        and serve to reinforce the learning objectives.
                        <br /><br />
                Course instructors should have experience, knowledge, and credentials relevant to the course they are teaching. The instructor should not act as a salesperson to promote any products or services. 
                <br /><br />
                All course content and materials must be educational, and may not be commercial. The promotion or discussion of proprietary information is strictly forbidden during the course.
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        5.1
                    </td>
                    <td class="APLabel">
                        How does your organization determine the appropriate qualifications for personnel
                        who develop courses:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="HowDeterminePersonnelDevelopCourses" runat="server" Columns="75"
                            CssClass="APTextarea" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        5.2
                    </td>
                    <td class="APLabel">
                        How does your organization determine and evaluate the appropriate qualifications
                        for personnel who deliver courses:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="HowDeterminePersonnelDeliverCourses" runat="server" Columns="75"
                            CssClass="APTextarea" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        5.3
                    </td>
                    <td class="APLabel">
                        Does your organization evaluate courses to ensure that program content and instructional
                        methods are appropriate for the intended learning objectives/outcomes?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="EvaluateCoursesToEnsureProgramContent" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True" OnClick="DisableTextControl('ProceduresSurveysEvaluationInstrumentsOrganizationUses', false)"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False" OnClick="DisableTextControl('ProceduresSurveysEvaluationInstrumentsOrganizationUses', true)"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        5.4
                    </td>
                    <td class="APLabel">
                        If you answered "yes" to question 5.3, please list below any procedures, surveys,
                        and/or other evaluation instruments your organization uses to ensure that program
                        content and instructional methods are appropriate for the intended learning objectives/outcomes
                        (samples may be requested by the Application Review Committee):
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="ProceduresSurveysEvaluationInstrumentsOrganizationUses" runat="server"
                            Columns="75" CssClass="APTextarea" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        5.5
                    </td>
                    <td class="APLabel">
                        Does your organization follow LA CES criteria to use only materials that do not
                        contain proprietary information?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="FollowLACESCriteriaToUseOnlyMaterials" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 6.</b> Registered courses must include a mechanism for assessing participant
                        attainment of the learning objectives/outcomes.
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        6.1
                    </td>
                    <td class="APLabel">
                        How does your organization assess participant attainment of the learning objectives:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="HowOrganizationAssessParticipantAttainment" runat="server" Columns="75"
                            CssClass="APTextarea" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 7.</b> Registered courses must be evaluated by participants and issue
                        a confirmation and verification of completion for each participant who completes
                        a course. (<a id="uiLnkModelEvaluationForm" runat="server" target="_blank">Click here</a>
                        for model evaluation form and <a id="uiLnkModelCertificateForm" target="_blank" runat="server">
                            click here</a> for sample certificate of completion.)
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        7.1
                    </td>
                    <td class="APLabel">
                        Are courses offered by your organization evaluated by the participants?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="CoursesEvaluatedByParticipants" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Sometimes" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        7.2
                    </td>
                    <td class="APLabel">
                        Does your organization evaluate its programs in ways other than by participants?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="EvaluateProgramsInwaysOtherThanByParticipants" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="1" OnClick="DisableTextControl('ExplainMethodsInwaysOtherThanByParticipants', false)"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2" OnClick="DisableTextControl('ExplainMethodsInwaysOtherThanByParticipants', true)"></asp:ListItem>
                            <asp:ListItem Text="Sometimes" Value="3" OnClick="DisableTextControl('ExplainMethodsInwaysOtherThanByParticipants', false)"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        7.3
                    </td>
                    <td class="APLabel">
                        If you answered "yes" or "sometimes" to question 7.2, please explain the methods
                        you use:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="ExplainMethodsInwaysOtherThanByParticipants" runat="server" Columns="75"
                            CssClass="APTextarea" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        7.4
                    </td>
                    <td class="APLabel">
                        Does your organization provide certificates of completion to each individual who
                        satisfactorily completes a course? (Samples may be requested by the Application
                        Review Committee.)
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="OrganizationProvideCertificates" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True" OnClick="DisableTextControl('HowOrganizationProvideConfirmation', true)"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False" OnClick="DisableTextControl('HowOrganizationProvideConfirmation', false)"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        7.5
                    </td>
                    <td class="APLabel">
                        If you answered "no" to question 7.4, how does your organization provide confirmation
                        of attendance to participants:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="HowOrganizationProvideConfirmation" runat="server" Columns="75"
                            CssClass="APTextarea" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 8.</b> Registered courses must maintain complete attendance records
                        that are available to participants on request for a minimum of six (6) years and
                        have a review process in operation that ensures that LA CES criteria are met. Attendance
                        records must be reported on the LA CES website within twenty (20) days of the completion
                        of the event using the template provided. In addition, approved providers must keep
                        copies of all course materials for a minimum of six (6) years. In jointly sponsored
                        programs the responsibility for attendance records, ensuring the criteria are met,
                        and retention of course materials rests with the organization issuing the professional
                        development hours.
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.1
                    </td>
                    <td class="APLabel">
                        Does your organization agree to maintain complete attendance records for registered
                        courses with confirmations available to participants on request for a minimum of
                        six (6) years and to submit records to LA CES within twenty (20) business days?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="AgreeToMaintainCompleteAttendanceRecords" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.2
                    </td>
                    <td class="APLabel">
                        Describe your organization’s recordkeeping system for participant records:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="DescribeOrganizationsRecordkeepingSystem" runat="server" Columns="75"
                            CssClass="APTextarea" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.3
                    </td>
                    <td class="APLabel">
                        Does your organization have an internal review process currently in operation that
                        ensures the LA CES criteria are met for each program? (An internal review process
                        should indicate the roles and responsibilities of individuals who are knowledgeable
                        of the LA CES criteria and review each program for compliance with the criteria.)
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="OrganizationHasAnInternalReviewProcess" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True" OnClick="DisableTextControl('DescribeWrittenPolicyOrCriteriaReviewProcess', false);DisableOptionControl('uiRblInternalReview', true)"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False" OnClick="DisableTextControl('DescribeWrittenPolicyOrCriteriaReviewProcess', true);DisableOptionControl('uiRblInternalReview', false)"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.4
                    </td>
                    <td class="APLabel">
                        If you answered "yes" to question 8.3, please insert any written policy or describe
                        your organization’s criteria review process below:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="DescribeWrittenPolicyOrCriteriaReviewProcess" runat="server" Columns="75"
                            CssClass="APTextarea" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.5
                    </td>
                    <td class="APLabel">
                        If you answered “no” to question 8.3, does your organization agree to develop an
                        internal review process?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="uiRblInternalReview" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.6
                    </td>
                    <td class="APLabel">
                        Does your organization agree to keep all course materials for a minimum of six (6)
                        years? (Copies of these materials may be requested by the LA CES Monitoring Committee
                        or state licensure boards.)
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="uiRblKeepMaterials" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="BtnStep2Prev" CausesValidation="false" runat="server" Text="BACK"
                CssClass="commonButton btn107 APButton" OnClick="BtnStep2Prev_Click" ToolTip="BACK TO PREVIOUS STEP"
                UseSubmitBehavior="False" />
            <asp:Button ID="BtnStep2Next" runat="server" Text="NEXT" CssClass="commonButton btn107 APButton"
                OnClick="BtnStep2Next_Click" OnClientClick="return ValidateForm();" ToolTip="CONTINUE TO NEXT STEP" />
            <br />

            <script language="javascript" type="text/javascript">

                //disable text area
                function DisableTextControl(controlID, disableControl) {
                    var idPrefix = 'ctl00_ContentPlaceHolderLeftPane_';
                    document.getElementById(idPrefix + controlID).disabled = disableControl;
                }

                //disable option button
                function DisableOptionControl(controlID, disableControl) {
                    var idPrefix = 'ctl00_ContentPlaceHolderLeftPane_';
                    
                    document.getElementById(idPrefix + controlID + "_0").disabled = disableControl;
                    document.getElementById(idPrefix + controlID + "_1").disabled = disableControl;
                }

                //following part is used for displaying validation msg
                var optOrganizationUnderstandLACES_0 = document.getElementById("<%=OrganizationUnderstandLACES.ClientID %>_0");
                var optOrganizationUnderstandLACES_1 = document.getElementById("<%=OrganizationUnderstandLACES.ClientID %>_1");

                var optCoursesOfferedAsLACES_0 = document.getElementById("<%=CoursesOfferedAsLACES.ClientID %>_0");
                var optCoursesOfferedAsLACES_1 = document.getElementById("<%=CoursesOfferedAsLACES.ClientID %>_1");

                var optAgreeToDesignateCoursesAsLACES_0 = document.getElementById("<%=AgreeToDesignateCoursesAsLACES.ClientID %>_0");
                var optAgreeToDesignateCoursesAsLACES_1 = document.getElementById("<%=AgreeToDesignateCoursesAsLACES.ClientID %>_1");

                var optFollowLACESGuidelines_0 = document.getElementById("<%=FollowLACESGuidelines.ClientID %>_0");
                var optFollowLACESGuidelines_1 = document.getElementById("<%=FollowLACESGuidelines.ClientID %>_1");

                var optAgreeToFollowLACESGuidelines_0 = document.getElementById("<%=AgreeToFollowLACESGuidelines.ClientID %>_0");
                var optAgreeToFollowLACESGuidelines_1 = document.getElementById("<%=AgreeToFollowLACESGuidelines.ClientID %>_1");

                var txtHowDeterminesCourses = document.getElementById("<%=HowDeterminesCourses.ClientID %>");

                var optOrganizedAndSystematicProcess_0 = document.getElementById("<%=OrganizedAndSystematicProcess.ClientID %>_0");
                var optOrganizedAndSystematicProcess_1 = document.getElementById("<%=OrganizedAndSystematicProcess.ClientID %>_1");

                var txtDescribeProcedures = document.getElementById("<%=DescribeProcedures.ClientID %>");

                var optWrittenLearningObjectives_0 = document.getElementById("<%=WrittenLearningObjectives.ClientID %>_0");
                var optWrittenLearningObjectives_1 = document.getElementById("<%=WrittenLearningObjectives.ClientID %>_1");
                var optWrittenLearningObjectives_2 = document.getElementById("<%=WrittenLearningObjectives.ClientID %>_2");

                var txtDescribeCourse1 = document.getElementById("<%=DescribeCourse1.ClientID %>");
                var txtDescribeCourse2 = document.getElementById("<%=DescribeCourse2.ClientID %>");

                var optAgreeToDevelopWrittenObjectives_0 = document.getElementById("<%=AgreeToDevelopWrittenObjectives.ClientID %>_0");
                var optAgreeToDevelopWrittenObjectives_1 = document.getElementById("<%=AgreeToDevelopWrittenObjectives.ClientID %>_1");

                var txtHowDeterminePersonnelDevelopCourses = document.getElementById("<%=HowDeterminePersonnelDevelopCourses.ClientID %>");
                var txtHowDeterminePersonnelDeliverCourses = document.getElementById("<%=HowDeterminePersonnelDeliverCourses.ClientID %>");

                var optEvaluateCoursesToEnsureProgramContent_0 = document.getElementById("<%=EvaluateCoursesToEnsureProgramContent.ClientID %>_0");
                var optEvaluateCoursesToEnsureProgramContent_1 = document.getElementById("<%=EvaluateCoursesToEnsureProgramContent.ClientID %>_1");

                var txtProceduresSurveysEvaluationInstrumentsOrganizationUses = document.getElementById("<%=ProceduresSurveysEvaluationInstrumentsOrganizationUses.ClientID %>");

                var optFollowLACESCriteriaToUseOnlyMaterials_0 = document.getElementById("<%=FollowLACESCriteriaToUseOnlyMaterials.ClientID %>_0");
                var optFollowLACESCriteriaToUseOnlyMaterials_1 = document.getElementById("<%=FollowLACESCriteriaToUseOnlyMaterials.ClientID %>_1");

                var txtHowOrganizationAssessParticipantAttainment = document.getElementById("<%=HowOrganizationAssessParticipantAttainment.ClientID %>");

                var optCoursesEvaluatedByParticipants_0 = document.getElementById("<%=CoursesEvaluatedByParticipants.ClientID %>_0");
                var optCoursesEvaluatedByParticipants_1 = document.getElementById("<%=CoursesEvaluatedByParticipants.ClientID %>_1");
                var optCoursesEvaluatedByParticipants_2 = document.getElementById("<%=CoursesEvaluatedByParticipants.ClientID %>_2");

                var optEvaluateProgramsInwaysOtherThanByParticipants_0 = document.getElementById("<%=EvaluateProgramsInwaysOtherThanByParticipants.ClientID %>_0");
                var optEvaluateProgramsInwaysOtherThanByParticipants_1 = document.getElementById("<%=EvaluateProgramsInwaysOtherThanByParticipants.ClientID %>_1");
                var optEvaluateProgramsInwaysOtherThanByParticipants_2 = document.getElementById("<%=EvaluateProgramsInwaysOtherThanByParticipants.ClientID %>_2");

                var txtExplainMethodsInwaysOtherThanByParticipants = document.getElementById("<%=ExplainMethodsInwaysOtherThanByParticipants.ClientID %>");

                var optOrganizationProvideCertificates_0 = document.getElementById("<%=OrganizationProvideCertificates.ClientID %>_0");
                var optOrganizationProvideCertificates_1 = document.getElementById("<%=OrganizationProvideCertificates.ClientID %>_1");

                var txtHowOrganizationProvideConfirmation = document.getElementById("<%=HowOrganizationProvideConfirmation.ClientID %>");

                var optAgreeToMaintainCompleteAttendanceRecords_0 = document.getElementById("<%=AgreeToMaintainCompleteAttendanceRecords.ClientID %>_0");
                var optAgreeToMaintainCompleteAttendanceRecords_1 = document.getElementById("<%=AgreeToMaintainCompleteAttendanceRecords.ClientID %>_1");

                var txtDescribeOrganizationsRecordkeepingSystem = document.getElementById("<%=DescribeOrganizationsRecordkeepingSystem.ClientID %>");

                var optOrganizationHasAnInternalReviewProcess_0 = document.getElementById("<%=OrganizationHasAnInternalReviewProcess.ClientID %>_0");
                var optOrganizationHasAnInternalReviewProcess_1 = document.getElementById("<%=OrganizationHasAnInternalReviewProcess.ClientID %>_1");

                var txtDescribeWrittenPolicyOrCriteriaReviewProcess = document.getElementById("<%=DescribeWrittenPolicyOrCriteriaReviewProcess.ClientID %>");

                function ValidateForm() {
                    var focusedControl; //assign control which needs to be focused

                    var lbl2Msg = document.getElementById("<%=lbl2Msg.ClientID%>");
                    lbl2Msg.innerHTML = "";
                    var errorFound = 0;

                    if (txtDescribeWrittenPolicyOrCriteriaReviewProcess.disabled == false &&
                        trim(txtDescribeWrittenPolicyOrCriteriaReviewProcess.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 8.4.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtDescribeWrittenPolicyOrCriteriaReviewProcess;
                        errorFound = 1;
                    }
                    if (!optOrganizationHasAnInternalReviewProcess_0.checked && !optOrganizationHasAnInternalReviewProcess_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 8.3.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optOrganizationHasAnInternalReviewProcess_0;
                        errorFound = 1;
                    }
                    if (trim(txtDescribeOrganizationsRecordkeepingSystem.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 8.2.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtDescribeOrganizationsRecordkeepingSystem;
                        errorFound = 1;
                    }
                    if (!optAgreeToMaintainCompleteAttendanceRecords_0.checked && !optAgreeToMaintainCompleteAttendanceRecords_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 8.1.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optAgreeToMaintainCompleteAttendanceRecords_0;
                        errorFound = 1;
                    }
                    if (txtHowOrganizationProvideConfirmation.disabled == false &&
                        trim(txtHowOrganizationProvideConfirmation.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 7.5.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtHowOrganizationProvideConfirmation;
                        errorFound = 1;
                    }
                    if (!optOrganizationProvideCertificates_0.checked && !optOrganizationProvideCertificates_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 7.4.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optOrganizationProvideCertificates_0;
                        errorFound = 1;
                    }
                    if (txtExplainMethodsInwaysOtherThanByParticipants.disabled == false &&
                        trim(txtExplainMethodsInwaysOtherThanByParticipants.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 7.3.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtExplainMethodsInwaysOtherThanByParticipants;
                        errorFound = 1;
                    }
                    if (!optEvaluateProgramsInwaysOtherThanByParticipants_0.checked && !optEvaluateProgramsInwaysOtherThanByParticipants_1.checked && !optEvaluateProgramsInwaysOtherThanByParticipants_2.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 7.2.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optEvaluateProgramsInwaysOtherThanByParticipants_0;
                        errorFound = 1;
                    }
                    if (!optCoursesEvaluatedByParticipants_0.checked && !optCoursesEvaluatedByParticipants_1.checked && !optCoursesEvaluatedByParticipants_2.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 7.1.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optCoursesEvaluatedByParticipants_0;
                        errorFound = 1;
                    }
                    if (trim(txtHowOrganizationAssessParticipantAttainment.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 6.1.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtHowOrganizationAssessParticipantAttainment;
                        errorFound = 1;
                    }
                    if (!optFollowLACESCriteriaToUseOnlyMaterials_0.checked && !optFollowLACESCriteriaToUseOnlyMaterials_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 5.5.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optFollowLACESCriteriaToUseOnlyMaterials_0;
                        errorFound = 1;
                    }
                    if (txtProceduresSurveysEvaluationInstrumentsOrganizationUses.disabled == false &&
                        trim(txtProceduresSurveysEvaluationInstrumentsOrganizationUses.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 5.4.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtProceduresSurveysEvaluationInstrumentsOrganizationUses;
                        errorFound = 1;
                    }
                    if (!optEvaluateCoursesToEnsureProgramContent_0.checked && !optEvaluateCoursesToEnsureProgramContent_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 5.3.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optEvaluateCoursesToEnsureProgramContent_0;
                        errorFound = 1;
                    }
                    if (trim(txtHowDeterminePersonnelDeliverCourses.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 5.2.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtHowDeterminePersonnelDeliverCourses;
                        errorFound = 1;
                    }
                    if (trim(txtHowDeterminePersonnelDevelopCourses.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 5.1.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtHowDeterminePersonnelDevelopCourses;
                        errorFound = 1;
                    }
                    if (optAgreeToDevelopWrittenObjectives_0.disabled == false &&
                        !optAgreeToDevelopWrittenObjectives_0.checked && !optAgreeToDevelopWrittenObjectives_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 4.6.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optAgreeToDevelopWrittenObjectives_0;
                        errorFound = 1;
                    }
                    if (txtDescribeCourse1.disabled == false && trim(txtDescribeCourse1.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 4.5.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtDescribeCourse1;
                        errorFound = 1;
                    }
                    else if (txtDescribeCourse2.disabled == false && trim(txtDescribeCourse2.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 4.5.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtDescribeCourse2;
                        errorFound = 1;
                    }
                    if (!optWrittenLearningObjectives_0.checked && !optWrittenLearningObjectives_1.checked && !optWrittenLearningObjectives_2.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 4.4.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optWrittenLearningObjectives_0;
                        errorFound = 1;
                    }
                    if (trim(txtDescribeProcedures.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 4.3.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtDescribeProcedures;
                        errorFound = 1;
                    }
                    if (!optOrganizedAndSystematicProcess_0.checked && !optOrganizedAndSystematicProcess_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 4.2.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optOrganizedAndSystematicProcess_0;
                        errorFound = 1;
                    }
                    if (trim(txtHowDeterminesCourses.value) == "") {
                        lbl2Msg.innerHTML = "<li>Enter text in section 4.1.</li>" + lbl2Msg.innerHTML;
                        focusedControl = txtHowDeterminesCourses;
                        errorFound = 1;
                    }
                    if (optAgreeToFollowLACESGuidelines_0.disabled == false &&
                    !optAgreeToFollowLACESGuidelines_0.checked && !optAgreeToFollowLACESGuidelines_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 3.2.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optAgreeToFollowLACESGuidelines_0;
                        errorFound = 1;
                    }
                    if (!optFollowLACESGuidelines_0.checked && !optFollowLACESGuidelines_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 3.1.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optFollowLACESGuidelines_0;
                        errorFound = 1;
                    }
                    if (optAgreeToDesignateCoursesAsLACES_0.disabled == false &&
                        !optAgreeToDesignateCoursesAsLACES_0.checked && !optAgreeToDesignateCoursesAsLACES_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 2.2.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optAgreeToDesignateCoursesAsLACES_0;
                        errorFound = 1;
                    }
                    if (!optCoursesOfferedAsLACES_0.checked && !optCoursesOfferedAsLACES_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 2.1.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optCoursesOfferedAsLACES_0;
                        errorFound = 1;
                    }
                    if (!optOrganizationUnderstandLACES_0.checked && !optOrganizationUnderstandLACES_1.checked) {
                        lbl2Msg.innerHTML = "<li>Select an option at section 1.1.</li>" + lbl2Msg.innerHTML;
                        focusedControl = optOrganizationUnderstandLACES_0;
                        errorFound = 1;
                    }

                    if (errorFound == 1) {
                        lbl2Msg.innerHTML = "Please correct the following error(s):<br /><ul>" + lbl2Msg.innerHTML + "</ul>";
                        focusedControl.focus();

                        setTimeout('scrollTo(0,0)', 20);
                        return false;
                    }

                    return true;
                }
            </script>

        </asp:View>
        <asp:View ID="View3" runat="server">
            <!-- Any Message of the Page or any Error Validation Starts -->
            <div style="text-align: left;">
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="Please correct the following error(s):" />
            </div>
            <!-- Any Message of the Page or any Error Validation Ends -->
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="APAgreement">
                        As an applicant our organization agrees to:<br />
                        <ol>
                            <li>Provide accurate and truthful information to LA CES in all transactions to the best
                                of our knowledge. </li>
                            <li>Conduct our operations and programs in an ethical manner that respects the rights
                                and worth of the professionals we serve. </li>
                            <li>Provide full and accurate disclosure of information about our programs, services,
                                and fees in our promotional materials. </li>
                            <li>Use only the LA CES approved statement of provider recognition on our promotional
                                and educational materials, with the understanding that participation in the LA CES
                                program does not automatically qualify courses as meeting any state continuing education
                                regulations as this decision rests with the state. </li>
                            <li>Only identify courses registered with LA CES as being recognized by LA CES.
                            </li>
                            <li>Report to LA CES any major organizational or program changes within thirty (30)
                                days that impact the operation of the administrative unit on which provider qualifications
                                are based. </li>
                            <li>Accept LA CES monitoring of any programs we provide for purposes of compliance with
                                the criteria. </li>
                            <li>Furnish requested information, work cooperatively with LA CES, and pay fees on a
                                timely basis. </li>
                            <li>Operate within the LA CES criteria and the terms of this agreement or relinquish
                                our approval status after due process. </li>
                            <li>On notification from LA CES, abide by any revisions of the criteria or inform LA
                                CES of our intention to withdraw.<br />
                            </li>
                        </ol>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="padding-bottom: 2px">
                        <asp:CheckBox ID="ckIsAgree" runat="server" ToolTip="Check it if you are agree with the agreement" />
                        <pantheon:CheckValidator ID="CheckValidator1" ControlToValidate="ckIsAgree" SetFocusOnError="true"
                            runat="server" ErrorMessage="Please agree to LA CES terms and conditions by checking the box next to the organization name."
                            Display="None"></pantheon:CheckValidator>
                        Name of organization:<asp:TextBox ID="AgreementOrganizationName" MaxLength="200"
                            runat="server" CssClass="APTextbox" Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="AgreementOrganizationName"
                            ErrorMessage="Enter organization name." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="padding-top: 0px">
                        agrees to abide by all of the foregoing terms and conditions and affirms that the
                        information contained in this application is true to the best of my knowledge.
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Primary contact:
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Name:<asp:TextBox ID="AuthorizerName" MaxLength="100" runat="server" CssClass="APTextbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="AuthorizerName"
                            ErrorMessage="Enter name." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        Position:<asp:TextBox ID="AuthorizerPosition" MaxLength="100" runat="server" CssClass="APTextbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="AuthorizerPosition"
                            ErrorMessage="Enter position." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Date:<asp:TextBox ID="AuthorizerDate" MaxLength="10" runat="server" CssClass="APDateField"
                            ToolTip="Enter date in mm/dd/yyyy format">
                        </asp:TextBox><img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=AuthorizerDate.ClientID %>', 'mm/dd/yyyy')"
                            src="../images/icon_calendar.jpg" class="CalenderIcon" /><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator7" runat="server" ControlToValidate="AuthorizerDate"
                                ErrorMessage="Enter date." SetFocusOnError="True" Display="None">
                            </asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator2" ControlToValidate="AuthorizerDate"
                                runat="server" ErrorMessage="Enter valid date." Type="Date" Operator="DataTypeCheck"
                                SetFocusOnError="True" Display="None"></asp:CompareValidator>Phone:<asp:TextBox ID="AuthorizerPhone"
                                    MaxLength="15" runat="server" CssClass="APTextbox">
                                </asp:TextBox>Email:<asp:TextBox ID="AuthorizerEmail" MaxLength="100" runat="server"
                                    CssClass="APTextbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="AuthorizerEmail"
                            ErrorMessage="Enter email." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="AuthorizerEmail"
                            ErrorMessage="Enter valid email." SetFocusOnError="True" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:HiddenField ID="AgreementPageVisited" runat="server" Value="false" />
            <asp:Button ID="BtnStep3Prev" CausesValidation="false" runat="server" Text="BACK"
                CssClass="commonButton btn107 APButton" OnClick="BtnStep3Prev_Click" ToolTip="BACK TO PREVIOUS STEP"
                UseSubmitBehavior="False" />
            <asp:Button ID="BtnStep3Next" runat="server" Text="NEXT" CssClass="commonButton btn107 APButton"
                OnClick="BtnStep3Next_Click" ToolTip="CONTINUE TO NEXT STEP" />
            <br />

            <script language="javascript" type="text/javascript">
                //initialize calender
                init();
            </script>

        </asp:View>
        <asp:View ID="View4" runat="server">
            <div id="PreviewPageheaderText" runat="server" class="moreLineHeight padding10 APCriteria">
                This page contains data that you have entered in previous steps. If everything looks
                fine proceed to next step and to make any correction go back to previous steps.
            </div>
            <table border="0" cellpadding="0" cellspacing="0" width="90%">
                <tr>
                    <td colspan="4" class="APLabel">
                        Name of organization:
                        <asp:Literal ID="pvOrganizationName" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                    <td class="APLabel">
                        Website:
                    </td>
                    <td class="APLabel">
                        <asp:Literal ID="pvOrganizationWebSite" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="APLabel">
                        Street address:
                        <asp:Literal ID="pvOrganizationStreetAddress" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" width="8%">
                        City:
                    </td>
                    <td class="APLabel">
                        <asp:Literal ID="pvOrganizationCity" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                    <td class="APLabel" width="8%">
                        State/Prov:
                    </td>
                    <td class="APLabel">
                        <asp:Literal ID="pvOrganizationState" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                    <td class="APLabel" width="8%">
                        Zip:
                    </td>
                    <td class="APLabel">
                        <asp:Literal ID="pvOrganizationZip" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Country:
                    </td>
                    <td class="APLabel" colspan="5">
                        <asp:Literal ID="pvOrganizationCountry" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Phone:
                    </td>
                    <td class="APLabel">
                        <asp:Literal ID="pvOrganizationPhone" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                    <td class="APLabel">
                        Fax:
                    </td>
                    <td class="APLabel" colspan="3">
                        <asp:Literal ID="pvOrganizationFax" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" width="70%">
                <tr>
                    <td class="APLabel" colspan="6">
                        Primary contact:
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Name:
                    </td>
                    <td class="APLabel">
                        <asp:Literal ID="pvApplicantName" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                    <td class="APLabel">
                        Position:
                    </td>
                    <td colspan="3" class="APLabel">
                        <asp:Literal ID="pvApplicantPosition" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 8%">
                        Phone:
                    </td>
                    <td style="width: 32%" class="APLabel">
                        <asp:Literal ID="pvApplicantPhone" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                    <td class="APLabel" style="width: 8%">
                        Fax:
                    </td>
                    <td style="width: 23%" class="APLabel">
                        <asp:Literal ID="pvApplicantFax" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                    <td class="APLabel" style="width: 8%">
                        Email:
                    </td>
                    <td style="width: 21%" class="APLabel">
                        <asp:Literal ID="pvApplicantEmail" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" width="90%">
                <tr>
                    <td class="APLabel">
                        Explain the nature and mission of your organization:
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <div id="pvOrganizationNature" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Check below the statement that describes your organization:
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <asp:CheckBox Enabled="false" ID="pvLegallyConstituted" runat="server" />
                        Legally constituted organization - manufacturer, service group, firm, other:
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <div id="pvLegallyConstitutedDescription" style="width: 50%" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <asp:CheckBox Enabled="false" ID="pvRegionallyAccredited" runat="server" />
                        Regionally or nationally accredited school, college, or university - list accrediting
                        agency below:
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <div id="pvRegionallyAccreditedDescription" style="width: 50%" class="APTextarea"
                            runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <asp:CheckBox Enabled="false" ID="pvProfessionalAssociation" runat="server" />
                        Professional association or other not-for-profit or nonprofit organization
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        <asp:CheckBox Enabled="false" ID="pvFederalOrganization" runat="server" />
                        Federal &nbsp;
                        <asp:CheckBox Enabled="false" ID="pvStateOrganization" runat="server" />
                        State &nbsp;
                        <asp:CheckBox Enabled="false" ID="pvLocalGovernmentAgency" runat="server" />
                        Local government agency
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" width="90%">
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 1.</b> Registered courses must adhere to the LA CES definition of continuing
                        professional education: "Continuing professional education consists of learning
                        experiences that enhance and expand the skills, knowledge, and abilities of practicing
                        landscape architects to remain current and render competent professional service
                        to clients and the public.":
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        1.1
                    </td>
                    <td class="APLabel">
                        Does your organization understand the LA CES definition of continuing professional
                        education and agree to offer courses for landscape architects that meet this definition?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvOrganizationUnderstandLACES" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 2.</b> Registered courses must specify whether the primary subject
                        matter qualifies as meeting the LA CES health, safety, and welfare definition. Seventy-five
                        (75) percent of the course material must qualify under this definition for courses
                        to be identified as meeting the LA CES health, safety, and welfare standard. (<a
                            id="uiLnkHSWClassification2" runat="server" target="_blank">Click here</a> for
                        more information on determining public health, safety, and welfare classification.)
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        2.1
                    </td>
                    <td class="APLabel">
                        Are courses offered by your organization designated as meeting the LA CES definition
                        of health, safety, and welfare?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvCoursesOfferedAsLACES" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        2.2
                    </td>
                    <td class="APLabel">
                        If you answered "no" to question 2.1, does your organization agree to designate
                        registered courses as meeting the LA CES health, safety, and welfare definition?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvAgreeToDesignateCoursesAsLACES" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 3.</b> Registered courses must comply with LA CES guidelines in the
                        assignment of professional development hours. All courses must be at least 1 PDH in length. (<a id="uiLnkCalculating_PDH2" runat="server"
                            target="_blank">Click here</a> for more information on calculating professional
                        development hours and <a id="uiLnkDistance_Education2" runat="server" target="_blank">
                            click here</a> for more information on distance education requirements.)
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        3.1
                    </td>
                    <td class="APLabel">
                        Does your organization follow LA CES guidelines in assigning professional development
                        hours to registered courses?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvFollowLACESGuidelines" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        3.2
                    </td>
                    <td class="APLabel">
                        If you answered "no" to question 3.1, does your organization agree to follow LA
                        CES guidelines in assigning professional development hours to its registered courses?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvAgreeToFollowLACESGuidelines" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 4.</b> Registered courses must be planned in response to the learning
                        needs of target audiences and include clear and concise written statements of learning
                        objectives/outcomes.
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.1
                    </td>
                    <td class="APLabel">
                        Briefly explain below how your organization determines which courses to offer for
                        landscape architects:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvHowDeterminesCourses" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.2
                    </td>
                    <td class="APLabel">
                        Does your organization use an organized and systematic process for identifying the
                        professional development needs of landscape architects?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvOrganizedAndSystematicProcess" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.3
                    </td>
                    <td class="APLabel">
                        Describe below any procedures, surveys, or materials that your organization uses
                        to identify the educational needs of landscape architects (samples may be requested
                        by the Application Review Committee):
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvDescribeProcedures" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.4
                    </td>
                    <td class="APLabel">
                        Learning objectives/outcomes are written statements of what participants are expected
                        to accomplish as a result of the course. Does your organization develop written
                        learning objectives/outcomes? Check the most appropriate choice below.
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvWrittenLearningObjectives" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Always" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Sometimes" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Never" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.5
                    </td>
                    <td class="APLabel">
                        If you answered “always” or “sometimes” to question 4.4, list below the learning
                        objectives/outcomes of two (2) different courses your organization has sponsored:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvDescribeCourse1" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvDescribeCourse2" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.6
                    </td>
                    <td class="APLabel">
                        If you answered "never" to question 4.4, does your organization agree to develop
                        written learning objectives/outcomes for all programs submitted to LA CES?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvAgreeToDevelopWrittenObjectives" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        4.7
                    </td>
                    <td class="APLabel">
                        Submit in the spaces provided below example learning outcomes (you may use one of
                        the examples provided in question 4.5), course outline, and seventy-five-to-one-hundred
                        (75-100) word description of a course:
                    </td>
                </tr>
                <tr>
                    <td>
                        Learning Ourcomes
                    </td>
                    <td>
                        <div id="pvLearningOutcomes" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Course Outline
                    </td>
                    <td>
                        <div id="pvCourseOutline" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Short Description
                    </td>
                    <td>
                        <div id="pvShortDescription" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 5.</b> Registered courses must use qualified instructional personnel
                        in course development and delivery; include content and instructional methods that
                        are appropriate for the intended learning objectives/outcomes; and use materials
                        that do not contain proprietary information, are educational and generic in nature,
                        and serve to reinforce the learning objectives.
                        <br /><br />
                Course instructors should have experience, knowledge, and credentials relevant to the course they are teaching. The instructor should not act as a salesperson to promote any products or services. 
                <br /><br />
                All course content and materials must be educational, and may not be commercial. The promotion or discussion of proprietary information is strictly forbidden during the course.
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        5.1
                    </td>
                    <td class="APLabel">
                        How does your organization determine the appropriate qualifications for personnel
                        who develop courses:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvHowDeterminePersonnelDevelopCourses" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        5.2
                    </td>
                    <td class="APLabel">
                        How does your organization determine and evaluate the appropriate qualifications
                        for personnel who deliver courses:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvHowDeterminePersonnelDeliverCourses" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        5.3
                    </td>
                    <td class="APLabel">
                        Does your organization evaluate courses to ensure that program content and instructional
                        methods are appropriate for the intended learning objectives/outcomes?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvEvaluateCoursesToEnsureProgramContent"
                            runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        5.4
                    </td>
                    <td class="APLabel">
                        If you answered "yes" to question 5.3, please list below any procedures, surveys,
                        and/or other evaluation instruments your organization uses to ensure that program
                        content and instructional methods are appropriate for the intended learning objectives/outcomes
                        (samples may be requested by the Application Review Committee):
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvProceduresSurveysEvaluationInstrumentsOrganizationUses" class="APTextarea"
                            runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        5.5
                    </td>
                    <td class="APLabel">
                        Does your organization follow LA CES criteria to use only materials that do not
                        contain proprietary information?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvFollowLACESCriteriaToUseOnlyMaterials"
                            runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 6.</b> Registered courses must include a mechanism for assessing participant
                        attainment of the learning objectives/outcomes.
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        6.1
                    </td>
                    <td class="APLabel">
                        How does your organization assess participant attainment of the learning objectives:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvHowOrganizationAssessParticipantAttainment" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 7.</b> Registered courses must be evaluated by participants and issue
                        a confirmation and verification of completion for each participant who completes
                        a course. (<a id="uiLnkModelEvaluationForm2" runat="server" target="_blank">Click here</a>
                        for model evaluation form and <a id="uiLnkModelCertificateForm2" runat="server" target="_blank">
                            click here</a> for sample certificate of completion.)
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        7.1
                    </td>
                    <td class="APLabel">
                        Are courses offered by your organization evaluated by the participants?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvCoursesEvaluatedByParticipants" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Sometimes" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        7.2
                    </td>
                    <td class="APLabel">
                        Does your organization evaluate its programs in ways other than by participants?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvEvaluateProgramsInwaysOtherThanByParticipants"
                            runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Sometimes" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        7.3
                    </td>
                    <td class="APLabel">
                        If you answered "yes" or "sometimes" to question 7.2, please explain the methods
                        you use:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvExplainMethodsInwaysOtherThanByParticipants" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        7.4
                    </td>
                    <td class="APLabel">
                        Does your organization provide certificates of completion to each individual who
                        satisfactorily completes a course? (Samples may be requested by the Application
                        Review Committee.)
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvOrganizationProvideCertificates" runat="server"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        7.5
                    </td>
                    <td class="APLabel">
                        If you answered "no" to question 7.4, how does your organization provide confirmation
                        of attendance to participants:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvHowOrganizationProvideConfirmation" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APCriteria" colspan="2">
                        <b>Criterion 8.</b> Registered courses must maintain complete attendance records
                        that are available to participants on request for a minimum of six (6) years and
                        have a review process in operation that ensures that LA CES criteria are met. Attendance
                        records must be reported on the LA CES website within twenty (20) days of the completion
                        of the event using the template provided. In addition, approved providers must keep
                        copies of all course materials for a minimum of six (6) years. In jointly sponsored
                        programs the responsibility for attendance records, ensuring the criteria are met,
                        and retention of course materials rests with the organization issuing the professional
                        development hours.
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.1
                    </td>
                    <td class="APLabel">
                        Does your organization agree to maintain complete attendance records for registered
                        courses with confirmations available to participants on request for a minimum of
                        six (6) years and to submit records to LA CES within twenty (20) business days?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvAgreeToMaintainCompleteAttendanceRecords"
                            runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.2
                    </td>
                    <td class="APLabel">
                        Describe your organization’s recordkeeping system for participant records:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvDescribeOrganizationsRecordkeepingSystem" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.3
                    </td>
                    <td class="APLabel">
                        Does your organization have an internal review process currently in operation that
                        ensures the LA CES criteria are met for each program? (An internal review process
                        should indicate the roles and responsibilities of individuals who are knowledgeable
                        of the LA CES criteria and review each program for compliance with the criteria.)
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvOrganizationHasAnInternalReviewProcess"
                            runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.4
                    </td>
                    <td class="APLabel">
                        If you answered "yes" to question 8.3, please insert any written policy or describe
                        your organization’s criteria review process below:
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div id="pvDescribeWrittenPolicyOrCriteriaReviewProcess" class="APTextarea" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.5
                    </td>
                    <td class="APLabel">
                        If you answered “no” to question 8.3, does your organization agree to develop an
                        internal review process?
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvInternalReview" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 20px">
                        8.6
                    </td>
                    <td class="APLabel">
                        Does your organization agree to keep all course materials for a minimum of six (6)
                        years? (Copies of these materials may be requested by the LA CES Monitoring Committee
                        or state licensure boards.)
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvKeepMaterials" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="APAgreement" colspan="6">
                        As an applicant our organization agrees to:<br />
                        <ol>
                            <li>Provide accurate and truthful information to LA CES in all transactions to the best
                                of our knowledge. </li>
                            <li>Conduct our operations and programs in an ethical manner that respects the rights
                                and worth of the professionals we serve. </li>
                            <li>Provide full and accurate disclosure of information about our programs, services,
                                and fees in our promotional materials. </li>
                            <li>Use only the LA CES approved statement of provider recognition on our promotional
                                and educational materials, with the understanding that participation in the LA CES
                                program does not automatically qualify courses as meeting any state continuing education
                                regulations as this decision rests with the state. </li>
                            <li>Only identify courses registered with LA CES as being recognized by LA CES.
                            </li>
                            <li>Report to LA CES any major organizational or program changes within thirty (30)
                                days that impact the operation of the administrative unit on which provider qualifications
                                are based. </li>
                            <li>Accept LA CES monitoring of any programs we provide for purposes of compliance with
                                the criteria. </li>
                            <li>Furnish requested information, work cooperatively with LA CES, and pay fees on a
                                timely basis. </li>
                            <li>Operate within the LA CES criteria and the terms of this agreement or relinquish
                                our approval status after due process. </li>
                            <li>On notification from LA CES, abide by any revisions of the criteria or inform LA
                                CES of our intention to withdraw.<br />
                            </li>
                        </ol>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" colspan="6" style="padding-bottom: 2px">
                        Name of organization:
                        <asp:Literal ID="pvAgreementOrganizationName" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" colspan="6" style="padding-top: 0px">
                        agrees to abide by all of the foregoing terms and conditions and affirms that the
                        information contained in this application is true to the best of my knowledge.
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" colspan="6">
                        Primary contact:
                    </td>
                </tr>
                <tr>
                    <td class="APLabel">
                        Name:
                    </td>
                    <td class="APLabel">
                        <asp:Literal ID="pvAuthorizerName" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                    <td class="APLabel">
                        Position:
                    </td>
                    <td colspan="3" class="APLabel">
                        <asp:Literal ID="pvAuthorizerPosition" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="APLabel" style="width: 10%">
                        Date:
                    </td>
                    <td style="width: 20%" class="APLabel">
                        <asp:Literal ID="pvAuthorizerDate" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                    <td class="APLabel" style="width: 10%">
                        Phone:
                    </td>
                    <td style="width: 20%" class="APLabel">
                        <asp:Literal ID="pvAuthorizerPhone" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                    <td class="APLabel" style="width: 10%">
                        Email:
                    </td>
                    <td style="width: 30%" class="APLabel">
                        <asp:Literal ID="pvAuthorizerEmail" runat="server" Mode="Encode"></asp:Literal>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="BtnStep4Prev" CausesValidation="false" runat="server" Text="EDIT"
                CssClass="commonButton btn107 APButton" OnClick="BtnStep4Prev_Click" ToolTip="BACK TO FIRST STEP"
                UseSubmitBehavior="False" />
            <asp:Button ID="BtnStep4Next" runat="server" Text="SUBMIT" CssClass="commonButton btn107 APButton"
                OnClick="BtnStep4Next_Click" ToolTip="GO TO PAYMENT STEP" />
            <br />
        </asp:View>
        <asp:View ID="View5" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" valign="top">
                        <b>This site is protected with an SSL certificate.</b>
                    </td>
                </tr>
            </table>
            <div id="Div1" runat="server" class="moreLineHeight padding10 APCriteria">
                LA CES Approved Provider Application or Annual Renewal Fee: $295.00
            </div>
            <div style="text-align: left;">
                <asp:Label ID="lbl5Msg" runat="server" ForeColor="red"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" HeaderText="Please correct the following error(s):" />
            </div>
            <table width="60%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td height="10" colspan="2" />
                    </tr>
                    <tr>
                        <td class="APLabel" style="width: 170px">
                            Name on card:
                        </td>
                        <td>
                            <asp:TextBox ID="NameOnCard" MaxLength="200" runat="server" CssClass="APTextbox"
                                Width="210"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="NameOnCard"
                                ErrorMessage="Enter name on card." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="APLabel">
                            Street address:
                        </td>
                        <td>
                            <asp:TextBox ID="CreditCardStreet" MaxLength="200" runat="server" CssClass="APTextbox"
                                Width="210"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="CreditCardStreet"
                                ErrorMessage="Enter street address." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="APLabel">
                            City:
                        </td>
                        <td>
                            <asp:TextBox ID="CreditCardCity" MaxLength="200" runat="server" CssClass="APTextbox"
                                Width="210"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="CreditCardCity"
                                ErrorMessage="Enter city." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="APLabel">
                            State:
                        </td>
                        <td>
                            <asp:TextBox ID="CreditCardState" MaxLength="200" runat="server" CssClass="APTextbox"
                                Width="210"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="CreditCardState"
                                ErrorMessage="Enter state." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="APLabel">
                            Zip:
                        </td>
                        <td>
                            <asp:TextBox ID="CreditCardZip" MaxLength="15" runat="server" CssClass="APTextbox"
                                Width="210"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="CreditCardZip"
                                ErrorMessage="Enter zip." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="CreditCardZip" 
                                ErrorMessage="Enter valid zip." ValidationExpression="\d{5}(-\d{4})?" runat="server"
                                SetFocusOnError="True" Display="None"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="APLabel">
                            Credit card type:
                        </td>
                        <td>
                            <asp:DropDownList ID="CreditCardType" runat="server" Width="210" CssClass="APDropdown">
                                <asp:ListItem Value="" Text="-Select card type-"></asp:ListItem>
                                <asp:ListItem Value="AMEX" Text="American Express"></asp:ListItem>
                                <asp:ListItem Value="Discover" Text="Discover"></asp:ListItem>
                                <asp:ListItem Value="MasterCard" Text="Master Card"></asp:ListItem>
                                <asp:ListItem Value="VISA" Text="VISA"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="CreditcardType"
                                ErrorMessage="Select credit card type." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="APLabel">
                            Credit card number:
                        </td>
                        <td>
                            <asp:TextBox ID="CreditCardNumber" MaxLength="16" runat="server" CssClass="APTextbox"
                                autocomplete="off" Width="210"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="CreditCardNumber"
                                ErrorMessage="Enter credit card number." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="APLabel">
                            Credit card verification code:
                        </td>
                        <td>
                            <asp:TextBox ID="CreditCardVerificationCode" MaxLength="4" runat="server" CssClass="APTextbox"
                                autocomplete="off" Width="210"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="CreditCardVerificationCode"
                                ErrorMessage="Enter credit card verification code." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="APLabel">
                            Credit card expiration Date:
                        </td>
                        <td>
                            <asp:DropDownList ID="CardExpirationMonth" runat="server" Width="110" CssClass="APDropdown">
                                <asp:ListItem Value="">-Select month-</asp:ListItem>
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="CardExpirationYear" runat="server" Width="100" CssClass="APDropdown">
                                <asp:ListItem Value="">-Select year-</asp:ListItem>                                
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="CardExpirationMonth"
                                ErrorMessage="Select credit card expiration month." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="CardExpirationYear"
                                ErrorMessage="Select credit card expiration year." SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <br />
            <br />
            <asp:Button ID="BtnStep5Prev" CausesValidation="false" runat="server" Text="BACK"
                CssClass="commonButton btn107" OnClick="BtnStep5Prev_Click" ToolTip="BACK TO PREVIOUS STEP"
                UseSubmitBehavior="False" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="BtnStep5Next" runat="server" Text="SUBMIT" CssClass="commonButton btn107"
                OnClick="BtnStep5Next_Click" ToolTip="PERFORM ONLINE TRANSACTION AND SUBMIT APPLICATION" />
            <br />
        </asp:View>
    </asp:MultiView>
</asp:Content>
