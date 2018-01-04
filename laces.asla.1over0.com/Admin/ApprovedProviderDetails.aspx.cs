/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: ApprovedProviderDetails.aspx
 * Purpose/Function: This page is used dispaly approved provider info
 *
 * Author: Md. Kamruzzaman
 * Version    Author               Date          Reason 
 * 1.0        Md. Kamruzzaman      07/14/2008    Initial development 
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

public partial class Admin_ApprovedProviderDetails : AdminBasePage
{
    #region Page Load Event Handler
    /// <summary>
    ///  Page Load Event Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      //  uiLnkDistanceLearning.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("DistanceEducationPDF"));
      //  uiLnkCalculatingPDH.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("CalculatingPDF"));
      //  uiLnkModelCertificateForm.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("ModelCertificateForm"));
      //  uiLnkModelEvaluationForm.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("ModelEvaluationForm"));
      //  uiLnkHSWClassification.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("HSWClassificationPDF"));
        if (!Page.IsPostBack)
        {
            //get id from query string
            if (Request.QueryString[LACESConstant.QueryString.PROVIDER_ID] != null)
            {
                int providerID = 0;

                int.TryParse(Request.QueryString[LACESConstant.QueryString.PROVIDER_ID].ToString(), out providerID);

                if (providerID > 0)
                {
                    ApprovedProviderDataAccess providerDA = new ApprovedProviderDataAccess();
                    ApprovedProvider currentProvider = providerDA.GetApprovedProviderByID(providerID); // Get provider by id

                    //if provider exists
                    if (currentProvider != null) 
                    {
                        PreparePreviewData(currentProvider);
                    }
                }
            }
        }
    }
    #endregion

    #region Save button click handler
    /// <summary>
    /// This function fires on save button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        SaveProvider();
    } 
    #endregion

    #region Update provider info 
    /// <summary>
    /// Update provider info 
    /// </summary>
    private void SaveProvider()
    {
        try
        {
            //get id from query string
            if (Request.QueryString[LACESConstant.QueryString.PROVIDER_ID] != null)
            {
                int providerID = 0;

                int.TryParse(Request.QueryString[LACESConstant.QueryString.PROVIDER_ID].ToString(), out providerID);

                if (providerID > 0)
                {
                    ApprovedProviderDataAccess providerDA = new ApprovedProviderDataAccess();
                    ApprovedProvider currentProvider = providerDA.GetApprovedProviderByID(providerID); // Get provider by id


                    //if provider exists    
                    if (currentProvider != null)
                    {
                        //if currently provider status is pending or inactive and admin manually 
                        //activates him/her, his/her NextRenewalDate will be set to 1 yr later from today
                        if (currentProvider.Status != LACESConstant.ProviderStatus.ACTIVE
                            && pvStatus.SelectedValue == LACESConstant.ProviderStatus.ACTIVE)
                        {

                            currentProvider.NextRenewalDate = DateTime.Now.AddYears(1);
                        }
                        if (uiLblNextRenewalDate.Text.Length > 0)
                        {
                            DateTime dtNextRen;
                            if (DateTime.TryParse(uiLblNextRenewalDate.Text, out dtNextRen))
                            {
                                currentProvider.NextRenewalDate = dtNextRen;
                            }
                        }
                        currentProvider.Status = pvStatus.SelectedValue;

                        if (ApprovalDate.Text != "")
                            currentProvider.ApprovalDate = Convert.ToDateTime(ApprovalDate.Text);

                        if (DeferralDate.Text != "")
                            currentProvider.DeferralDate = Convert.ToDateTime(DeferralDate.Text);

                        //if (DenialDate.Text != "")
                        //    currentProvider.DenialDate = Convert.ToDateTime(DenialDate.Text);
                        if (uiTxtApplicationReceiveDate.Text != "")
                        {
                            currentProvider.ApplicationReceivedDate = Convert.ToDateTime(uiTxtApplicationReceiveDate.Text);
                        }
                        if (uiTxtCommitteeReviewDate.Text != "")
                        {
                            currentProvider.CommiteeApprovedDate = Convert.ToDateTime(uiTxtCommitteeReviewDate.Text);
                        }
                        if (uiTxtRenewalDate.Text != "")
                        {
                            currentProvider.RenewalDate = Convert.ToDateTime(uiTxtRenewalDate.Text);
                        }
                        if (uiTxtPayment.Text != "")
                        {
                            currentProvider.Payment = uiTxtPayment.Text;
                        }
                        if (uiTxtYearMonitored.Text != "")
                        {
                            currentProvider.YearMonitored = uiTxtYearMonitored.Text;
                        }
                        if (uiTxtNotes.Text != "")
                        {
                            currentProvider.Notes = uiTxtNotes.Text;
                        }
                        // if (ExpirationDate.Text != "")
                        //     currentProvider.ExpirationDate = Convert.ToDateTime(ExpirationDate.Text);

                        if (WithdrawalDate.Text != "")
                            currentProvider.WithdrawalDate = Convert.ToDateTime(WithdrawalDate.Text);

                        currentProvider.isPaymentExempt = uiChkIsPaymentExempt.Checked;

                        if (providerDA.UpdateApprovedProvider(currentProvider) != null)
                        {
                            Response.Redirect("../Admin/ProviderUpdatedSuccessfully.aspx?Status=" + currentProvider.Status, true);
                        }
                        else
                        {
                            lblMsg.Text = "Provider information cannot be updated.";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
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

        pvStatus.SelectedValue = currentProvider.Status;

        if (currentProvider.ApprovalDate > ApprovedProvider.MINIMUM_DATE_VALUE)
            ApprovalDate.Text = currentProvider.ApprovalDate.ToString("MM/dd/yyyy");

        if (currentProvider.DeferralDate > ApprovedProvider.MINIMUM_DATE_VALUE)
            DeferralDate.Text = currentProvider.DeferralDate.ToString("MM/dd/yyyy");

        //if (currentProvider.DenialDate > ApprovedProvider.MINIMUM_DATE_VALUE)
        //    DenialDate.Text = currentProvider.DenialDate.ToString("MM/dd/yyyy");

       // if (currentProvider.ExpirationDate > ApprovedProvider.MINIMUM_DATE_VALUE)
       //     ExpirationDate.Text = currentProvider.ExpirationDate.ToString("MM/dd/yyyy");

        if (currentProvider.WithdrawalDate > ApprovedProvider.MINIMUM_DATE_VALUE)
            WithdrawalDate.Text = currentProvider.WithdrawalDate.ToString("MM/dd/yyyy");
        pvLearningOutcomes.InnerHtml = currentProvider.LearningOutcome;
        pvCourseOutline.InnerHtml = currentProvider.CourseOutline;
        pvShortDescription.InnerHtml = currentProvider.ShortDescription;
        if (currentProvider.ApplicationReceivedDate > ApprovedProvider.MINIMUM_DATE_VALUE)
        {
            uiTxtApplicationReceiveDate.Text = currentProvider.ApplicationReceivedDate.ToString("MM/dd/yyyy");
        }
        if (currentProvider.CommiteeApprovedDate > ApprovedProvider.MINIMUM_DATE_VALUE)
        {
            uiTxtCommitteeReviewDate.Text = currentProvider.CommiteeApprovedDate.ToString("MM/dd/yyyy");
        }
        uiTxtNotes.Text = currentProvider.Notes;
        uiTxtPayment.Text = currentProvider.Payment;
        if (currentProvider.RenewalDate > ApprovedProvider.MINIMUM_DATE_VALUE)
        {
            uiTxtRenewalDate.Text = currentProvider.RenewalDate.ToString("MM/dd/yyyy");
        }
        uiLblNextRenewalDate.Text = currentProvider.NextRenewalDate.ToString("MM/dd/yyyy");
        uiTxtYearMonitored.Text = currentProvider.YearMonitored;
        if (!currentProvider.OrganizationHasAnInternalReviewProcess)
        {
            pvInternalReview.SelectedValue = currentProvider.DevelopInternalReview;
        }
            pvKeepMaterials.SelectedValue = currentProvider.KeepAllMaterials;
            if (currentProvider.isPaymentExempt)
            {
                uiChkIsPaymentExempt.Checked=true;
            }
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
