/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: ApprovedProviderDetails.aspx
 * Purpose/Function: This page is used dispaly approved provider info
 *
 * Author: Md. Kamruzzaman
 * Version    Author               Date          Reason 
 * 1.0        Md. Kamruzzaman      07/23/2008    Initial development 
 --------------------------------------------------------------------------------*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Pantheon.ASLA.LACES.DataAccess;
using Pantheon.ASLA.LACES.Common;
using System.Xml;
public partial class Provider_ApprovedProviderDetails : System.Web.UI.Page
{
    #region Page Load Event Handler
    /// <summary>
    ///  Page Load Event Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string sessionKeyForProvider = LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER;

        //if current user is inactive
        if (Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] != null)
        {
            sessionKeyForProvider = LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER;
        }

        //if current user is inactive
        if (Session[sessionKeyForProvider] == null)
        {
            Response.Redirect("Login.aspx");
        }
        //show provider spacific menu in edit mode
        else
        {
            //get the provider from session
            ApprovedProvider sessionProvider = (ApprovedProvider)Session[sessionKeyForProvider]; // Get provider information from Session 

            //Show loggedin provider name
            Label lblProviderName = (Label)Master.FindControl("lblProviderName");
            if (lblProviderName != null)
                lblProviderName.Text = "Signed in as: " + Server.HtmlEncode(sessionProvider.OrganizationName);


            //load the provider menu bar
            HtmlTableCell oHtmlTableCell = (HtmlTableCell)Master.FindControl("tdMenu");
            oHtmlTableCell.Controls.Clear();

            //if provider is active,provider menu will be shown
            if (sessionProvider.Status == LACESConstant.ProviderStatus.ACTIVE)
            {
                oHtmlTableCell.Controls.Add(LoadControl("~/usercontrols/ProviderMenu.ascx"));
            }
            //if provider is inactive, inactive provider menu will be shown
            else if (sessionProvider.Status == LACESConstant.ProviderStatus.INACTIVE)
            {
                oHtmlTableCell.Controls.Add(LoadControl("~/usercontrols/InactiveProviderMenu.ascx"));
            }

            try
            {
                //change the login status
                ((HtmlTableCell)Master.FindControl("loginTd")).Attributes["class"] = "loggedIn providerloginStatus";
            }
            catch (Exception ex) { }
        }

        uiLnkCalculatingPDH.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("CalculatingPDF"));
        uiLnkDistanceLearning.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("DistanceEducationPDF"));
        uiLnkModelCertificateForm.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("ModelCertificateForm"));
        uiLnkModelEvaluationForm.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("ModelEvaluationForm"));
        uiLnkHSWClassification.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("HSWClassificationPDF"));
        if (!Page.IsPostBack)
        {
            //now check weather it is in edit provider mode
            if (Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] != null)
            {
                //get the provider from session
                ApprovedProvider sessionProvider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // Get provider information from Session 

                //fill form by provider in session 
                PreparePreviewData(sessionProvider);

            }
            else
            {
                ApprovedProvider sessionProvider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER]; // Get provider information from Session 

                //fill form by provider in session 
                PreparePreviewData(sessionProvider);
            }
        }
    }
    #endregion

    #region Preview Data
    /// <summary>
    /// Prepare data to dislay in preview mode
    /// </summary>
    private void PreparePreviewData(ApprovedProvider currentProvider)
    {
        pvOrganizationName.Text = currentProvider.OrganizationName;
        pvOrganizationStreetAddress.Text = currentProvider.OrganizationStreetAddress;
        pvOrganizationCity.Text = currentProvider.OrganizationCity;
        pvOrganizationState.Text = currentProvider.OrganizationState;
        pvOrganizationZip.Text = currentProvider.OrganizationZip;
        pvOrganizationCountry.Text = currentProvider.OrganizationCountry;
        pvOrganizationPhone.Text = currentProvider.OrganizationPhone;
        pvOrganizationFax.Text = currentProvider.OrganizationFax;
        pvOrganizationWebSite.Text = currentProvider.OrganizationWebSite;

        pvApplicantName.Text = currentProvider.ApplicantName;
        pvApplicantPosition.Text = currentProvider.ApplicantPosition;
        pvApplicantPhone.Text = currentProvider.ApplicantPhone;
        pvApplicantFax.Text = currentProvider.ApplicantFax;
        pvApplicantEmail.Text = currentProvider.ApplicantEmail;

        if (currentProvider.OrganizationNature != null)
            pvOrganizationNature.InnerHtml = Server.HtmlEncode(currentProvider.OrganizationNature).Replace("\r\n", "<br />");

        pvLegallyConstituted.Checked = currentProvider.LegallyConstituted;

        if (currentProvider.LegallyConstitutedDescription != null)
            pvLegallyConstitutedDescription.InnerHtml = Server.HtmlEncode(currentProvider.LegallyConstitutedDescription).Replace("\r\n", "<br />");

        pvRegionallyAccredited.Checked = currentProvider.RegionallyAccredited;

        if (currentProvider.RegionallyAccreditedDescription != null)
            pvRegionallyAccreditedDescription.InnerHtml = Server.HtmlEncode(currentProvider.RegionallyAccreditedDescription).Replace("\r\n", "<br />");

        pvProfessionalAssociation.Checked = currentProvider.ProfessionalAssociation;
        pvFederalOrganization.Checked = currentProvider.FederalOrganization;
        pvStateOrganization.Checked = currentProvider.StateOrganization;
        pvLocalGovernmentAgency.Checked = currentProvider.LocalGovernmentAgency;
        pvOrganizationUnderstandLACES.SelectedValue = currentProvider.OrganizationUnderstandLACES.ToString();
        pvCoursesOfferedAsLACES.SelectedValue = currentProvider.CoursesOfferedAsLACES.ToString();
        if (!currentProvider.CoursesOfferedAsLACES)
        {
            pvAgreeToDesignateCoursesAsLACES.SelectedValue = currentProvider.AgreeToDesignateCoursesAsLACES.ToString();
        }
        pvFollowLACESGuidelines.SelectedValue = currentProvider.FollowLACESGuidelines.ToString();
        if (!currentProvider.FollowLACESGuidelines)
        {
            pvAgreeToFollowLACESGuidelines.SelectedValue = currentProvider.AgreeToFollowLACESGuidelines.ToString();
        }
        if (currentProvider.HowDeterminesCourses != null)
            pvHowDeterminesCourses.InnerHtml = Server.HtmlEncode(currentProvider.HowDeterminesCourses).Replace("\r\n", "<br />");
        
        pvOrganizedAndSystematicProcess.SelectedValue = currentProvider.OrganizedAndSystematicProcess.ToString();

        if (currentProvider.DescribeProcedures != null)
            pvDescribeProcedures.InnerHtml = Server.HtmlEncode(currentProvider.DescribeProcedures).Replace("\r\n", "<br />");

        pvWrittenLearningObjectives.SelectedValue = currentProvider.WrittenLearningObjectives.ToString();

        if (currentProvider.WrittenLearningObjectives == 1 || currentProvider.WrittenLearningObjectives == 2)
        {
            if (currentProvider.DescribeCourse1 != null)
                pvDescribeCourse1.InnerHtml = Server.HtmlEncode(currentProvider.DescribeCourse1).Replace("\r\n", "<br />");
            if (currentProvider.DescribeCourse2 != null)
                pvDescribeCourse2.InnerHtml = Server.HtmlEncode(currentProvider.DescribeCourse2).Replace("\r\n", "<br />");
        }
        else
        {
            pvAgreeToDevelopWrittenObjectives.SelectedValue = currentProvider.AgreeToDevelopWrittenObjectives.ToString();
        }

        if (currentProvider.HowDeterminePersonnelDevelopCourses != null)
            pvHowDeterminePersonnelDevelopCourses.InnerHtml = Server.HtmlEncode(currentProvider.HowDeterminePersonnelDevelopCourses).Replace("\r\n", "<br />");
        if (currentProvider.HowDeterminePersonnelDeliverCourses != null)
            pvHowDeterminePersonnelDeliverCourses.InnerHtml = Server.HtmlEncode(currentProvider.HowDeterminePersonnelDeliverCourses).Replace("\r\n", "<br />");
        pvEvaluateCoursesToEnsureProgramContent.SelectedValue = currentProvider.EvaluateCoursesToEnsureProgramContent.ToString();
        
        if (currentProvider.EvaluateCoursesToEnsureProgramContent)
        {
            if (currentProvider.ProceduresSurveysEvaluationInstrumentsOrganizationUses != null)
                pvProceduresSurveysEvaluationInstrumentsOrganizationUses.InnerHtml = Server.HtmlEncode(currentProvider.ProceduresSurveysEvaluationInstrumentsOrganizationUses).Replace("\r\n", "<br />");
        }
       
        pvFollowLACESCriteriaToUseOnlyMaterials.SelectedValue = currentProvider.FollowLACESCriteriaToUseOnlyMaterials.ToString();
        
        if (currentProvider.HowOrganizationAssessParticipantAttainment != null)
            pvHowOrganizationAssessParticipantAttainment.InnerHtml = Server.HtmlEncode(currentProvider.HowOrganizationAssessParticipantAttainment).Replace("\r\n", "<br />");
       
        pvCoursesEvaluatedByParticipants.SelectedValue = currentProvider.CoursesEvaluatedByParticipants.ToString();
        pvEvaluateProgramsInwaysOtherThanByParticipants.SelectedValue = currentProvider.EvaluateProgramsInwaysOtherThanByParticipants.ToString();

        if (currentProvider.EvaluateProgramsInwaysOtherThanByParticipants == 1 || currentProvider.EvaluateProgramsInwaysOtherThanByParticipants == 3)
        {
            if (currentProvider.ExplainMethodsInwaysOtherThanByParticipants != null)
                pvExplainMethodsInwaysOtherThanByParticipants.InnerHtml = Server.HtmlEncode(currentProvider.ExplainMethodsInwaysOtherThanByParticipants).Replace("\r\n", "<br />");
        }

        pvOrganizationProvideCertificates.SelectedValue = currentProvider.OrganizationProvideCertificates.ToString();
              
        if (!currentProvider.OrganizationProvideCertificates)
        {
            if (currentProvider.HowOrganizationProvideConfirmation != null)
                pvHowOrganizationProvideConfirmation.InnerHtml = Server.HtmlEncode(currentProvider.HowOrganizationProvideConfirmation).Replace("\r\n", "<br />");
        }

        pvAgreeToMaintainCompleteAttendanceRecords.SelectedValue = currentProvider.AgreeToMaintainCompleteAttendanceRecords.ToString();

        if (currentProvider.DescribeOrganizationsRecordkeepingSystem != null)
            pvDescribeOrganizationsRecordkeepingSystem.InnerHtml = Server.HtmlEncode(currentProvider.DescribeOrganizationsRecordkeepingSystem).Replace("\r\n", "<br />");

        pvOrganizationHasAnInternalReviewProcess.SelectedValue = currentProvider.OrganizationHasAnInternalReviewProcess.ToString();
        
        if (currentProvider.OrganizationHasAnInternalReviewProcess)
        {
            if (currentProvider.DescribeWrittenPolicyOrCriteriaReviewProcess != null)
            pvDescribeWrittenPolicyOrCriteriaReviewProcess.InnerHtml = Server.HtmlEncode(currentProvider.DescribeWrittenPolicyOrCriteriaReviewProcess).Replace("\r\n", "<br />");
        }

        pvAgreementOrganizationName.Text = currentProvider.AgreementOrganizationName;
        pvAuthorizerName.Text = currentProvider.AuthorizerName;
        pvAuthorizerPosition.Text = currentProvider.AuthorizerPosition;
        pvAuthorizerDate.Text = currentProvider.AuthorizerDate.ToString("MM/dd/yyyy");
        pvAuthorizerPhone.Text = currentProvider.AuthorizerPhone;
        pvAuthorizerEmail.Text = currentProvider.AuthorizerEmail;
        pvLearningOutcomes.InnerHtml = currentProvider.LearningOutcome;
        pvCourseOutline.InnerHtml = currentProvider.CourseOutline;
        pvShortDescription.InnerHtml = currentProvider.ShortDescription;
        if (!currentProvider.OrganizationHasAnInternalReviewProcess)
        {
            pvInternalReview.SelectedValue = currentProvider.DevelopInternalReview;
        }
        pvKeepMaterials.SelectedValue = currentProvider.KeepAllMaterials;
    }
    #endregion
    protected string GetPDFURL(string PDFLibraryID)
    {
        string pdfurl = "";
        XmlDocument GetPdfXML = new XmlDocument();
        GetPdfXML.Load(LACESUtilities.GetApplicationConstants("URLToGetPDFFilename") + "?id=" + PDFLibraryID);
        pdfurl = LACESUtilities.GetApplicationConstants("ASLABaseURL") + GetPdfXML.SelectSingleNode("root/filename").InnerText; ;


        return pdfurl;

    }
}
