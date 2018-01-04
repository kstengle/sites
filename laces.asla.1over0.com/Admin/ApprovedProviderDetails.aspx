<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="ApprovedProviderDetails.aspx.cs" Inherits="Admin_ApprovedProviderDetails"
    Title="Admin: Provider Details| LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <div id="PageHeader" class="title" runat="server">
        LA CES™ Approved Provider Application
    </div>
    <!-- Any Message of the Page or any Error Validation Starts -->
    <div style="text-align: left;" class="paddingLeft15">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following error(s):" />
    </div>
    <!-- Any Message of the Page or any Error Validation Ends -->
     <table border="0" cellpadding="0" cellspacing="0" width="90%">
        <tr>
            <td colspan="4" class="APLabel">
                Name of organization:
                <asp:Literal ID="pvOrganizationName" runat="server" Mode="Encode"></asp:Literal>
            </td>
            <td class="APLabel">
                Website:</td>
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
                City:</td>
            <td class="APLabel">
                <asp:Literal ID="pvOrganizationCity" runat="server" Mode="Encode"></asp:Literal>
            </td>
            <td class="APLabel" width="8%">
                State/Prov:</td>
            <td class="APLabel">
                <asp:Literal ID="pvOrganizationState" runat="server" Mode="Encode"></asp:Literal>
            </td>
            <td class="APLabel" width="8%">
                Zip:</td>
            <td class="APLabel">
                <asp:Literal ID="pvOrganizationZip" runat="server" Mode="Encode"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="APLabel">
                Country:</td>
            <td class="APLabel" colspan="5">
                <asp:Literal ID="pvOrganizationCountry" runat="server" Mode="Encode"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="APLabel">
                Phone:</td>
            <td class="APLabel">
                <asp:Literal ID="pvOrganizationPhone" runat="server" Mode="Encode"></asp:Literal>
            </td>
            <td class="APLabel">
                Fax:</td>
            <td class="APLabel" colspan="3">
                <asp:Literal ID="pvOrganizationFax" runat="server" Mode="Encode"></asp:Literal>
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="70%">
        <tr>
            <td class="APLabel" colspan="6">
                Primary contact:</td>
        </tr>
        <tr>
            <td class="APLabel">
                Name:</td>
            <td class="APLabel">
                <asp:Literal ID="pvApplicantName" runat="server" Mode="Encode"></asp:Literal>
            </td>
            <td class="APLabel">
                Position:</td>
            <td colspan="3" class="APLabel">
                <asp:Literal ID="pvApplicantPosition" runat="server" Mode="Encode"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="APLabel" style="width: 8%">
                Phone:</td>
            <td style="width: 32%" class="APLabel">
                <asp:Literal ID="pvApplicantPhone" runat="server" Mode="Encode"></asp:Literal>
            </td>
            <td class="APLabel" style="width: 8%">
                Fax:</td>
            <td style="width: 23%" class="APLabel">
                <asp:Literal ID="pvApplicantFax" runat="server" Mode="Encode"></asp:Literal>
            </td>
            <td class="APLabel" style="width: 8%">
                Email:</td>
            <td style="width: 21%" class="APLabel">
                <asp:Literal ID="pvApplicantEmail" runat="server" Mode="Encode"></asp:Literal>
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="90%">
        <tr>
            <td class="APLabel">
                Explain the nature and mission of your organization:</td>
        </tr>
        <tr>
            <td class="APLabel">
                <div id="pvOrganizationNature" class="APTextarea" runat="server">
                </div>
            </td>
        </tr>
        <tr>
            <td class="APLabel">
                Check below the statement that describes your organization:</td>
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
                to clients and the public.":</td>
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
                    id="uiLnkHSWClassification" runat="server" target="_blank">Click here</a> for more
                information on determining public health, safety, and welfare classification.)
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
                assignment of professional development hours. All courses must be at least 1 PDH in length.(<a id="uiLnkCalculatingPDH" runat="server"
                    target="_blank">Click here</a> for more information on calculating professional
                development hours and <a id="uiLnkDistanceLearning" runat="server" target="_blank">here</a>
                for more information on distance education requirements.)
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
                If you answered "always" or "sometimes" to question 4.4, provide examples of learning objectives/outcomes for two (2) different courses your organization has sponsored. A minimum of three (3) learning objectives/outcomes is required for each course: 
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
                        Submit in the spaces provided below example learning outcomes (you may use one of the examples provided in question 4.5), course outline, and seventy-five-to-one-hundred (75-100) word description of a course:

                    </td>
                </tr>
                <tr>
                    <td>Learning Outcomes
                    </td>
                    <td>
                         <div ID="pvLearningOutcomes" class="APTextarea" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td>Course Outline
                    </td>
                    <td>
                         <div ID="pvCourseOutline" class="APTextarea" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td>Short Description
                    </td>
                    <td>
                         <div ID="pvShortDescription" class="APTextarea" runat="server"></div>
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
                a course. (<a id="uiLnkModelEvaluationForm" runat="server" target="_blank">Click here</a>
                for model evaluation form and <a id="uiLnkModelCertificateForm" runat="server" target="_blank">
                    here</a> for sample certificate of completion.)
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
of the event using the template provided. In addition, approved providers must keep copies of all course materials for a minimum of six (6) years.  In jointly sponsored programs the responsibility for attendance records, ensuring the criteria are met, and retention of course materials, rests with the organization issuing the professional development hours.
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
                        If you answered “no” to question 8.3, does your organization agree to develop an internal review process?

                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    
                    
                    
                        <asp:RadioButtonList Enabled="false" ID="pvInternalReview"
                            runat="server" RepeatDirection="Horizontal">
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
                        Does your organization agree to keep all course materials for a minimum of six (6) years?  (Copies of these materials may be requested by the LA CES Monitoring Committee or state licensure boards.)


                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButtonList Enabled="false" ID="pvKeepMaterials"
                            runat="server" RepeatDirection="Horizontal">
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
                    <li>Only identify courses registered with LA CES as being recognized by LA CES. </li>
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
                information contained in this application is true to the best of my knowledge.</td>
        </tr>
        <tr>
            <td class="APLabel" colspan="6">
                Primary contact:</td>
        </tr>
        <tr>
            <td class="APLabel">
                Name:</td>
            <td class="APLabel">
                <asp:Literal ID="pvAuthorizerName" runat="server" Mode="Encode"></asp:Literal>
            </td>
            <td class="APLabel">
                Position:</td>
            <td colspan="3" class="APLabel">
                <asp:Literal ID="pvAuthorizerPosition" runat="server" Mode="Encode"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="APLabel" style="width: 10%">
                Date:</td>
            <td style="width: 20%" class="APLabel">
                <asp:Literal ID="pvAuthorizerDate" runat="server" Mode="Encode"></asp:Literal>
            </td>
            <td class="APLabel" style="width: 10%">
                Phone:</td>
            <td style="width: 20%" class="APLabel">
                <asp:Literal ID="pvAuthorizerPhone" runat="server" Mode="Encode"></asp:Literal>
            </td>
            <td class="APLabel" style="width: 10%">
                Email:</td>
            <td style="width: 30%" class="APLabel">
                <asp:Literal ID="pvAuthorizerEmail" runat="server" Mode="Encode"></asp:Literal>
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="45%">
        <tr>
            <td class="APLabel" colspan="2">
                LA CES USE ONLY</td>
        </tr>
        <tr>
            <td class="APLabel" style="width: 40%">
                Approval date:</td>
            <td class="APLabel" style="width: 60%">
                <asp:TextBox ID="ApprovalDate" MaxLength="10" runat="server" CssClass="APDateField"
                    ToolTip="Enter date in mm/dd/yyyy format"></asp:TextBox>
                <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=ApprovalDate.ClientID %>', 'mm/dd/yyyy')"
                    src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                <asp:CompareValidator ID="CompareValidator2" ControlToValidate="ApprovalDate" runat="server"
                    ErrorMessage="Enter valid approval date." Type="Date" Operator="DataTypeCheck"
                    SetFocusOnError="True" Display="None"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="APLabel" style="width: 40%">
                Application Received Date:</td>
            <td class="APLabel" style="width: 60%">
                <asp:TextBox ID="uiTxtApplicationReceiveDate" MaxLength="10" runat="server" CssClass="APDateField"
                    ToolTip="Enter date in mm/dd/yyyy format"></asp:TextBox>
                <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=uiTxtApplicationReceiveDate.ClientID %>', 'mm/dd/yyyy')"
                    src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                <asp:CompareValidator ID="CompareValidator3" ControlToValidate="uiTxtApplicationReceiveDate" runat="server"
                    ErrorMessage="Enter valid application received date." Type="Date" Operator="DataTypeCheck"
                    SetFocusOnError="True" Display="None"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="APLabel" style="width: 40%">
                Committee Review Date:</td>
            <td class="APLabel" style="width: 60%">
                <asp:TextBox ID="uiTxtCommitteeReviewDate" MaxLength="10" runat="server" CssClass="APDateField"
                    ToolTip="Enter date in mm/dd/yyyy format"></asp:TextBox>
                <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=uiTxtCommitteeReviewDate.ClientID %>', 'mm/dd/yyyy')"
                    src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                <asp:CompareValidator ID="CompareValidator6" ControlToValidate="uiTxtCommitteeReviewDate" runat="server"
                    ErrorMessage="Enter valid committee review date." Type="Date" Operator="DataTypeCheck"
                    SetFocusOnError="True" Display="None"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="APLabel">
                Deferral date:</td>
            <td class="APLabel">
                <asp:TextBox ID="DeferralDate" MaxLength="10" runat="server" CssClass="APDateField"
                    ToolTip="Enter date in mm/dd/yyyy format"></asp:TextBox>
                <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=DeferralDate.ClientID %>', 'mm/dd/yyyy')"
                    src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="DeferralDate" runat="server"
                    ErrorMessage="Enter valid deferral date." Type="Date" Operator="DataTypeCheck"
                    SetFocusOnError="True" Display="None"></asp:CompareValidator>
            </td>
        </tr>
        <%--<tr>
            <td class="APLabel">
                Denial date:</td>
            <td class="APLabel">
                <asp:TextBox ID="DenialDate" MaxLength="10" runat="server" CssClass="APDateField" ToolTip="Enter date in mm/dd/yyyy format"></asp:TextBox>
                <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=DenialDate.ClientID %>', 'mm/dd/yyyy')"
                    src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                <asp:CompareValidator ID="CompareValidator3" ControlToValidate="DenialDate" runat="server"
                    ErrorMessage="Enter valid denial date." Type="Date" Operator="DataTypeCheck"
                    SetFocusOnError="True" Display="None"></asp:CompareValidator>
            </td>
        </tr>--%>
        <tr>
            <td class="APLabel" style="width: 40%">
                Expiration Date:</td>
            <td class="APLabel" style="width: 60%">
                <asp:TextBox ID="uiLblNextRenewalDate" CssClass="APDateField" runat="server" MaxLength="10"></asp:TextBox>
                <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=uiLblNextRenewalDate.ClientID %>', 'mm/dd/yyyy')"
                    src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                
                <br /><br />&nbsp;&nbsp;OR
                <br /><br />&nbsp;&nbsp;<asp:CheckBox ID="uiChkIsPaymentExempt" runat="server" />Subscription never expires
            </td>
            
        </tr>
        <tr>
            <td class="APLabel" style="width: 40%">
                Renewal Date:</td>
            <td class="APLabel" style="width: 60%">
                <asp:TextBox ID="uiTxtRenewalDate" MaxLength="10" runat="server" CssClass="APDateField"
                    ToolTip="Enter date in mm/dd/yyyy format"></asp:TextBox>
                <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=uiTxtRenewalDate.ClientID %>', 'mm/dd/yyyy')"
                    src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                <asp:CompareValidator ID="CompareValidator7" ControlToValidate="uiTxtRenewalDate" runat="server"
                    ErrorMessage="Enter valid renewel date." Type="Date" Operator="DataTypeCheck"
                    SetFocusOnError="True" Display="None"></asp:CompareValidator>
            </td>
        </tr>
        
        <tr>
            <td class="APLabel">
                Withdrawal date:</td>
            <td class="APLabel">
                <asp:TextBox ID="WithdrawalDate" MaxLength="10" runat="server" CssClass="APDateField"
                    ToolTip="Enter date in mm/dd/yyyy format"></asp:TextBox>
                <img alt="Calender" title="Calender" onclick="loadCalendar(this, '<%=WithdrawalDate.ClientID %>', 'mm/dd/yyyy')"
                    src="../images/icon_calendar.jpg"  class="CalenderIcon" />
                <asp:CompareValidator ID="CompareValidator5" ControlToValidate="WithdrawalDate" runat="server"
                    ErrorMessage="Enter valid withdrawal date." Type="Date" Operator="DataTypeCheck"
                    SetFocusOnError="True" Display="None"></asp:CompareValidator>
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="350">
        <tr>
            <td class="APLabel" style="width: 150px">
                Status:</td>
            <td class="APLabel" style="width: 200px">
                <asp:RadioButtonList ID="pvStatus" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                    <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                    <asp:ListItem Text="Denied" Value="Denied"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
		</table>
	<table border="0" cellpadding="0" cellspacing="0" width="350">
        <tr>
            <td style="width: 150px" class="APLabel">
                Payment:</td>
			<td width="10px"></td>
            <td style="width: 190px;">
                $<asp:TextBox ID="uiTxtPayment" MaxLength="10" runat="server" CssClass="APDateFieldNoPadding"></asp:TextBox>
            </td>
        </tr>
		<tr>
		<td height="5px" colspan="3">&nbsp;</td>
		</tr>
        <tr>
            <td style="width: 150px" class="APLabel">
                Year Monitored:</td>
			<td width="10px">&nbsp;</td>
            <td style="width: 190px">
                <asp:TextBox ID="uiTxtYearMonitored" MaxLength="50" runat="server" CssClass="APDateFieldNoPadding"></asp:TextBox>
            </td>
        </tr>
		<tr>
		<td height="5px" colspan="3">&nbsp;</td>
		</tr>
        <tr>
            <td style="width: 150px" class="APLabel">
                Notes:</td>
            <td width="10px">&nbsp;</td>
            <td style="width: 190px">
                <asp:TextBox ID="uiTxtNotes" width="200" runat="server" CssClass="frmTextArea" Rows="6" TextMode="MultiLine" ></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:Button ID="BtnSave" runat="server" Text="SAVE" CssClass="commonButton btn107"
        OnClick="BtnSave_Click" ToolTip="Save" />
    <br />
    <br />

    <script language="javascript" type="text/javascript">
        //initialize calender
        init();
    </script>

</asp:Content>
