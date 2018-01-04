/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: ProviderApplication.aspx
 * Purpose/Function: This page is used to apply for approved provider
 *
 * Author: Md. Kamruzzaman
 * 
 * Version    Author               Date          Reason 
 * 1.0        Md. Kamruzzaman      07/06/2008    Initial development 
 * 1.1        Md. Kamruzzaman      07/20/2008    Paypal Integration and added 2nd step's validations 
 * 1.2        Md. Kamruzzaman      07/29/2008    Renew functionalities implemented 
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
using System.Collections.Generic;
using Pantheon.ASLA.LACES.Common;
using PayPal.Payments.Common.Logging;
using PayPal.Payments.Common.Utility;
using PayPal.Payments.Communication;
using System.Xml;
public partial class Visitor_ProviderApplication : System.Web.UI.Page
{
    #region Member Variables
    string sessionKeyForProvider = LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER;
    #endregion

    //protected void Page_PreLoad(object sender, EventArgs e)
    //{
    //    if (!(Request.IsSecureConnection))
    //    {
    //        string tempurl = Request.Url.OriginalString.ToString().Replace("http://", "https://");
    //        tempurl = tempurl.Replace(".com:80", ".com");
    //        Response.Redirect(tempurl, false);
    //    }
    //}
    #region Page Load Event Handler
    /// <summary>
    ///  Page Load Event Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //System.Diagnostics.Debugger.Break();
        uiLnkHSWClassification.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("HSWClassificationPDF"));
        uiLnkHSWClassification2.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("HSWClassificationPDF"));
        uiLnkCalculating_PDH.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("CalculatingPDF"));
        uiLnkCalculating_PDH2.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("CalculatingPDF"));
        uiLnkDistance_Education.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("DistanceEducationPDF"));
        uiLnkDistance_Education2.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("DistanceEducationPDF"));
        uiLnkModelEvaluationForm.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("ModelEvaluationForm"));
        uiLnkModelEvaluationForm2.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("ModelEvaluationForm"));
        uiLnkModelCertificateForm.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("ModelCertificateForm"));
        uiLnkModelCertificateForm2.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("ModelCertificateForm"));

        //if current user is inactive
        if (Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] != null)
        {
            sessionKeyForProvider = LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER;
        }

        if (!IsPostBack)
        {
            //set active view
            for (int i = DateTime.Now.Year; i < DateTime.Now.Year+20; i++)
            {
                CardExpirationYear.Items.Add(new ListItem( i.ToString(), i.ToString().Substring(2)));

            }
            MultiView1.SetActiveView(View1);

            //now check weather it is in edit provider mode
            if (Session[sessionKeyForProvider] != null)
            {
                //get the provider from session
                ApprovedProvider sessionProvider = (ApprovedProvider)Session[sessionKeyForProvider]; // Get provider information from Session 

                //fill form by provider in session 
                FillFormValuesByProviderObject(sessionProvider);

                //check whether provider wants change or not
                if (Request.QueryString["HasChange"] != null && Request.QueryString["HasChange"] != "true")
                {
                    //set preview data
                    //PreparePreviewData();

                    //set preview page as active view
                    MultiView1.SetActiveView(View3);
                    //set today's date
                    AuthorizerDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
                }
            }
        }

        if (MultiView1.ActiveViewIndex == 1)
        {
            //enable/disable dependent fields of step 2
            EnableDisableFormFields();
        }

        //show provider spacific menu in edit mode
        if (Session[sessionKeyForProvider] != null)
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

        //set currtent view title
        SetPageTitle();

        if (IsRenewReadOnlyMode())
        {
            BtnStep3Next.Text = "CONTINUE";
            BtnStep3Prev.Visible = false;
        }
        else if (IsRenewMode())
        {
            PreviewPageheaderText.Visible = false;
            BtnStep3Next.Text = "CONTINUE";
        }

    }
    #endregion

    #region Navigation Buttons
    /// <summary>
    /// This function fires on step 1 next button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnStep1Next_Click(object sender, EventArgs e)
    {
        //enable/disable dependent fields of step 2
        EnableDisableFormFields();

        MultiView1.SetActiveView(View2);

        SetPageTitle();
        HidPass.Value = Password.Text;
    }

    /// <summary>
    /// This function fires on step 2 prev button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnStep2Prev_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(View1);
        SetPageTitle();
        Password.Attributes.Add("value", HidPass.Value);
        PasswordConfirm.Attributes.Add("value", HidPass.Value);

        //clear message
        lblMsg.Text = "";
    }

    /// <summary>
    /// This function fires on step 2 next button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnStep2Next_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(View3);
        SetPageTitle();

        //pre-fill agreement page if this page has not been visited yet 
        //and provider is not in edit mode
        if (AgreementPageVisited.Value == "false" && ckIsAgree.Enabled)
        {
            AgreementOrganizationName.Text = OrganizationName.Text;
            AuthorizerName.Text = ApplicantName.Text;
            AuthorizerPosition.Text = ApplicantPosition.Text;
            AuthorizerDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            AuthorizerPhone.Text = ApplicantPhone.Text;
            AuthorizerEmail.Text = ApplicantEmail.Text;
            AgreementPageVisited.Value = "true";
        }
    }

    /// <summary>
    /// This function fires on step 3 prev button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnStep3Prev_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(View2);
        SetPageTitle();
    }

    /// <summary>
    /// This function fires on step 3 next button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnStep3Next_Click(object sender, EventArgs e)
    {
        //in readonly renew mode, no need to show preview page
        if (IsRenewReadOnlyMode())
        {
            BtnStep4Next_Click(sender, e);
        }
        else
        {
            MultiView1.SetActiveView(View4);
            SetPageTitle();
            PreparePreviewData();
        }
    }

    /// <summary>
    /// This function fires on step 4 prev button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnStep4Prev_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(View1);
        SetPageTitle();
        Password.Attributes.Add("value", HidPass.Value);
        PasswordConfirm.Attributes.Add("value", HidPass.Value);

        //clear message
        lblMsg.Text = "";
    }

    /// <summary>
    /// This function fires on step 4 next button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnStep4Next_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(View5);
        SetPageTitle();

        //clear message
        lbl5Msg.Text = "";
    }

    /// <summary>
    /// This function fires on step 5 prev button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnStep5Prev_Click(object sender, EventArgs e)
    {
        //in readonly renew mode, no need to show preview page
        if (IsRenewReadOnlyMode())
        {
            MultiView1.SetActiveView(View3);
            SetPageTitle();
        }
        else
        {
            MultiView1.SetActiveView(View4);
            SetPageTitle();
        }
    }

    /// <summary>
    /// This function fires on step 5 next button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnStep5Next_Click(object sender, EventArgs e)
    {
        if (IsRenewMode())
        {
            RenewProviderApplication();
        }
        else
        {
            SaveProviderApplication();
        }
    }
    #endregion

    #region Preview Data
    /// <summary>
    /// Prepare data to dislay in preview step
    /// </summary>
    private void PreparePreviewData()
    {
        pvOrganizationName.Text = OrganizationName.Text;
        pvOrganizationStreetAddress.Text = OrganizationStreetAddress.Text;
        pvOrganizationCity.Text = OrganizationCity.Text;
        pvOrganizationState.Text = OrganizationState.Text;
        pvOrganizationZip.Text = OrganizationZip.Text;
        pvOrganizationCountry.Text = OrganizationCountry.Text;
        pvOrganizationPhone.Text = OrganizationPhone.Text;
        pvOrganizationFax.Text = OrganizationFax.Text;
        pvOrganizationWebSite.Text = OrganizationWebSite.Text;

        pvApplicantName.Text = ApplicantName.Text;
        pvApplicantPosition.Text = ApplicantPosition.Text;
        pvApplicantPhone.Text = ApplicantPhone.Text;
        pvApplicantFax.Text = ApplicantFax.Text;
        pvApplicantEmail.Text = ApplicantEmail.Text;

        pvOrganizationNature.InnerHtml = Server.HtmlEncode(OrganizationNature.Text).Replace("\r\n", "<br />");
        pvLegallyConstituted.Checked = LegallyConstituted.Checked;
        pvLegallyConstitutedDescription.InnerHtml = Server.HtmlEncode(LegallyConstitutedDescription.Text).Replace("\r\n", "<br />");
        pvRegionallyAccredited.Checked = RegionallyAccredited.Checked;
        pvRegionallyAccreditedDescription.InnerHtml = Server.HtmlEncode(RegionallyAccreditedDescription.Text).Replace("\r\n", "<br />");
        pvProfessionalAssociation.Checked = ProfessionalAssociation.Checked;
        pvFederalOrganization.Checked = FederalOrganization.Checked;
        pvStateOrganization.Checked = StateOrganization.Checked;
        pvLocalGovernmentAgency.Checked = LocalGovernmentAgency.Checked;

        if (!string.IsNullOrEmpty(OrganizationUnderstandLACES.SelectedValue))
            pvOrganizationUnderstandLACES.SelectedValue = OrganizationUnderstandLACES.SelectedValue;

        if (!string.IsNullOrEmpty(CoursesOfferedAsLACES.SelectedValue))
            pvCoursesOfferedAsLACES.SelectedValue = CoursesOfferedAsLACES.SelectedValue;

        if (CoursesOfferedAsLACES.SelectedValue.ToLower() == "false")
        {
            if (!string.IsNullOrEmpty(AgreeToDesignateCoursesAsLACES.SelectedValue))
                pvAgreeToDesignateCoursesAsLACES.SelectedValue = AgreeToDesignateCoursesAsLACES.SelectedValue;
        }
        if (!string.IsNullOrEmpty(FollowLACESGuidelines.SelectedValue))
            pvFollowLACESGuidelines.SelectedValue = FollowLACESGuidelines.SelectedValue;
        if (FollowLACESGuidelines.SelectedValue.ToLower() == "false")
        {
            if (!string.IsNullOrEmpty(AgreeToFollowLACESGuidelines.SelectedValue))
                pvAgreeToFollowLACESGuidelines.SelectedValue = AgreeToFollowLACESGuidelines.SelectedValue;
        }
        pvHowDeterminesCourses.InnerHtml = Server.HtmlEncode(HowDeterminesCourses.Text).Replace("\r\n", "<br />");

        if (!string.IsNullOrEmpty(OrganizedAndSystematicProcess.SelectedValue))
            pvOrganizedAndSystematicProcess.SelectedValue = OrganizedAndSystematicProcess.SelectedValue;

        pvDescribeProcedures.InnerHtml = Server.HtmlEncode(DescribeProcedures.Text).Replace("\r\n", "<br />");

        if (!string.IsNullOrEmpty(WrittenLearningObjectives.SelectedValue))
            pvWrittenLearningObjectives.SelectedValue = WrittenLearningObjectives.SelectedValue;

        pvDescribeCourse1.InnerHtml = Server.HtmlEncode(DescribeCourse1.Text).Replace("\r\n", "<br />");
        pvDescribeCourse2.InnerHtml = Server.HtmlEncode(DescribeCourse2.Text).Replace("\r\n", "<br />");

        if (!string.IsNullOrEmpty(AgreeToDevelopWrittenObjectives.SelectedValue))
            pvAgreeToDevelopWrittenObjectives.SelectedValue = AgreeToDevelopWrittenObjectives.SelectedValue;

        pvHowDeterminePersonnelDevelopCourses.InnerHtml = Server.HtmlEncode(HowDeterminePersonnelDevelopCourses.Text).Replace("\r\n", "<br />");
        pvHowDeterminePersonnelDeliverCourses.InnerHtml = Server.HtmlEncode(HowDeterminePersonnelDeliverCourses.Text).Replace("\r\n", "<br />");

        if (!string.IsNullOrEmpty(EvaluateCoursesToEnsureProgramContent.SelectedValue))
            pvEvaluateCoursesToEnsureProgramContent.SelectedValue = EvaluateCoursesToEnsureProgramContent.SelectedValue;

        pvProceduresSurveysEvaluationInstrumentsOrganizationUses.InnerHtml = Server.HtmlEncode(ProceduresSurveysEvaluationInstrumentsOrganizationUses.Text).Replace("\r\n", "<br />");

        if (!string.IsNullOrEmpty(FollowLACESCriteriaToUseOnlyMaterials.SelectedValue))
            pvFollowLACESCriteriaToUseOnlyMaterials.SelectedValue = FollowLACESCriteriaToUseOnlyMaterials.SelectedValue;

        pvHowOrganizationAssessParticipantAttainment.InnerHtml = Server.HtmlEncode(HowOrganizationAssessParticipantAttainment.Text).Replace("\r\n", "<br />");

        if (!string.IsNullOrEmpty(CoursesEvaluatedByParticipants.SelectedValue))
            pvCoursesEvaluatedByParticipants.SelectedValue = CoursesEvaluatedByParticipants.SelectedValue;

        if (!string.IsNullOrEmpty(EvaluateProgramsInwaysOtherThanByParticipants.SelectedValue))
            pvEvaluateProgramsInwaysOtherThanByParticipants.SelectedValue = EvaluateProgramsInwaysOtherThanByParticipants.SelectedValue;

        pvExplainMethodsInwaysOtherThanByParticipants.InnerHtml = Server.HtmlEncode(ExplainMethodsInwaysOtherThanByParticipants.Text).Replace("\r\n", "<br />");

        if (!string.IsNullOrEmpty(OrganizationProvideCertificates.SelectedValue))
            pvOrganizationProvideCertificates.SelectedValue = OrganizationProvideCertificates.SelectedValue;

        pvHowOrganizationProvideConfirmation.InnerHtml = Server.HtmlEncode(HowOrganizationProvideConfirmation.Text).Replace("\r\n", "<br />");

        if (!string.IsNullOrEmpty(AgreeToMaintainCompleteAttendanceRecords.SelectedValue))
            pvAgreeToMaintainCompleteAttendanceRecords.SelectedValue = AgreeToMaintainCompleteAttendanceRecords.SelectedValue;

        pvDescribeOrganizationsRecordkeepingSystem.InnerHtml = Server.HtmlEncode(DescribeOrganizationsRecordkeepingSystem.Text).Replace("\r\n", "<br />");

        if (!string.IsNullOrEmpty(OrganizationHasAnInternalReviewProcess.SelectedValue))
            pvOrganizationHasAnInternalReviewProcess.SelectedValue = OrganizationHasAnInternalReviewProcess.SelectedValue;

        pvDescribeWrittenPolicyOrCriteriaReviewProcess.InnerHtml = Server.HtmlEncode(DescribeWrittenPolicyOrCriteriaReviewProcess.Text).Replace("\r\n", "<br />");

        pvAgreementOrganizationName.Text = AgreementOrganizationName.Text;
        pvAuthorizerName.Text = AuthorizerName.Text;
        pvAuthorizerPosition.Text = AuthorizerPosition.Text;
        pvAuthorizerDate.Text = AuthorizerDate.Text;
        pvAuthorizerPhone.Text = AuthorizerPhone.Text;
        pvAuthorizerEmail.Text = AuthorizerEmail.Text;
        pvLearningOutcomes.InnerHtml = uiTxtLearningOutcome.Text;
        pvCourseOutline.InnerHtml = uiTxtCourseOutline.Text;
        pvShortDescription.InnerHtml = uiTxtDescription.Text;

        if (!string.IsNullOrEmpty(uiRblKeepMaterials.SelectedValue))
            pvKeepMaterials.SelectedValue = uiRblKeepMaterials.SelectedValue;

        if (!string.IsNullOrEmpty(uiRblInternalReview.SelectedValue))
            pvInternalReview.SelectedValue = uiRblInternalReview.SelectedValue;


       // pvInternalReview.SelectedValue = uiRblInternalReview.SelectedValue;
       // pvKeepMaterials.SelectedValue = uiRblKeepMaterials.SelectedValue;
    }
    #endregion

    #region Save Provider Application

    ///<summary>
    ///Insert/Update provider information in database
    ///</summary>
    private void SaveProviderApplication()
    {
        //create Provider Data Access object
        ApprovedProviderDataAccess providerDA = new ApprovedProviderDataAccess();

        //Check for existence for Email address
        ApprovedProvider oProvider = providerDA.GetApprovedProviderByEmail(ApplicantEmail.Text);

        //provider in session
        ApprovedProvider sessionProvider = null;

        bool isUpdate = false;
        bool emailExists = false;

        //now check weather it is in edit provider mode
        if (Session[sessionKeyForProvider] != null)
        {
            //get the provider from session
            sessionProvider = (ApprovedProvider)Session[sessionKeyForProvider]; // Get provider information from Session 

            //provider did not change email
            if (oProvider != null && oProvider.ID == sessionProvider.ID)
            {
                isUpdate = true;
            }
            //provider changed email and that is not set for other provider
            else if (oProvider == null)
            {
                isUpdate = true;
            }
            //provider changed email and that is set for other provider
            else if (oProvider != null && oProvider.ID != sessionProvider.ID)
            {
                //The email is already used, so not allowed
                lblMsg.Text = "Email already exists. Please provide another email.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                MultiView1.SetActiveView(View1);

                //this email already exists
                emailExists = true;
            }
        }
        else if (oProvider != null)
        {
            //The email is already used, so not allowed
            lblMsg.Text = "Email already exists. Please provide another email.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            MultiView1.SetActiveView(View1);

            //this email already exists
            emailExists = true;
        }

        if (emailExists == false)
        {
            ApprovedProvider currentProvider = FIllApprovedProviderObjectByFormValues();

            if (isUpdate)
            {
                currentProvider.ID = sessionProvider.ID;
                currentProvider.Status = sessionProvider.Status;
                currentProvider = providerDA.UpdateApprovedProvider(currentProvider, true);
            }
            else
            {
                currentProvider.Status = LACESConstant.ProviderStatus.PENDING;
                currentProvider.NextRenewalDate = DateTime.Now.AddYears(1);
                currentProvider = providerDA.AddApprovedProvider(currentProvider);
            }

            if (currentProvider == null)
            {
                //exception occured
                lblMsg.Text = "Provider information cannot be saved 2.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                MultiView1.SetActiveView(View1);
            }
            else
            {
                //send mail successfull
                if (isUpdate)
                {
                    //update the current provider information in the session 
                    Session[sessionKeyForProvider] = currentProvider;

                    Response.Redirect("ProviderSubmissionSuccess.aspx", true);
                }
                else
                {
                    //process transaction only when creating provider account

                    //Kevin
                    lbl5Msg.Text = SubmitTransaction(currentProvider.ID);
                    //lbl5Msg.Text = "";
                    if (lbl5Msg.Text != "") //transaction failed
                    {
                        providerDA.DeleteApprovedProvider(currentProvider.ID);
                        currentProvider = null;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        //send mail to admin and applicant
                        currentProvider.Payment = LACESUtilities.GetApplicationConstants(LACESConstant.PaymentInfo.PROVIDER_FEE);
                        currentProvider = providerDA.UpdateApprovedProvider(currentProvider, true);
                        if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
                        {
                          try
                          {
                               
                                //Response.Write("Applicant Email: " + currentProvider.ApplicantEmail + "<br />");
                                //Response.Write("GetApprovedProviderMailTo: " + LACESUtilities.GetApprovedProviderMailTo()+ "<br />");
                                //Response.Write("GetAdminFromEmail" + LACESUtilities.GetAdminFromEmail()+ "<br />");
                                //Response.End();

                                LACESUtilities.SendMailsAfterProviderApplicationSubmitted(currentProvider.ApplicantName, currentProvider.ApplicantEmail, currentProvider.OrganizationName);
                          }
                          catch (Exception ex)
                          {
                              Response.Write(ex.Message);
                              Response.End();
                          }
                        }
                        Response.Redirect("ProviderSubmissionSuccess.aspx", true);
                    }
                }
            }
        }
    }
    #endregion

    #region Renew Provider Application

    ///<summary>
    ///Renew provider application
    ///</summary>
    private void RenewProviderApplication()
    {
        //create Provider Data Access object
        ApprovedProviderDataAccess providerDA = new ApprovedProviderDataAccess();

        //get the provider from session
        ApprovedProvider sessionProvider = (ApprovedProvider)Session[sessionKeyForProvider]; // Get provider information from Session 

        ApprovedProvider currentProvider = new ApprovedProvider();

        bool isError = false;

        if (!IsRenewReadOnlyMode())
        {
            //Check for existence for Email address
            ApprovedProvider oProvider = providerDA.GetApprovedProviderByEmail(ApplicantEmail.Text);

            //provider changed email and that is set for other provider
            if (oProvider != null && oProvider.ID != sessionProvider.ID)
            {
                //The email is already used, so not allowed
                lblMsg.Text = "Email already exists. Please provide another email.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                MultiView1.SetActiveView(View1);

                //this email already exists
                isError = true;
            }
            else
            {
                currentProvider = FIllApprovedProviderObjectByFormValues();
                currentProvider.ID = sessionProvider.ID;
                currentProvider.Status = sessionProvider.Status;
            }
        }
        else
        {
            currentProvider = sessionProvider.Copy();
            currentProvider.AgreementOrganizationName = AgreementOrganizationName.Text;
            currentProvider.AuthorizerName = AuthorizerName.Text;
            currentProvider.AuthorizerPosition = AuthorizerPosition.Text;
            currentProvider.AuthorizerDate = Convert.ToDateTime(AuthorizerDate.Text);
            currentProvider.AuthorizerPhone = AuthorizerPhone.Text;
            currentProvider.AuthorizerEmail = AuthorizerEmail.Text;
        }

        if (!isError)
        {
            //set new renewal date
            currentProvider.NextRenewalDate = sessionProvider.NextRenewalDate.AddYears(1);
            currentProvider.Payment = LACESUtilities.GetApplicationConstants(LACESConstant.PaymentInfo.PROVIDER_FEE); /*Kevin added to populate payment field on renewals*/
            //if provider's current status is inactive then make it pending
            if (currentProvider.Status == LACESConstant.ProviderStatus.INACTIVE)
            {
                //set status to pending so that admin can review
                currentProvider.Status = LACESConstant.ProviderStatus.PENDING;

                //set new renewal date
                currentProvider.NextRenewalDate = DateTime.Now.AddYears(1);
            }


            //set status to active
            currentProvider.Status = LACESConstant.ProviderStatus.ACTIVE;

            //set new renewal date
            currentProvider.NextRenewalDate = DateTime.Now.AddYears(1);
            
            //update provider info
            //if (currentProvider == null)
            //{
            //    lbl5Msg.Text = "NO CURRENT PROVIDER SESSION PROVIDER:" + sessionProvider.ID;
            //    lblMsg.ForeColor = System.Drawing.Color.Red;
            //    Response.End();
            //}
            currentProvider = providerDA.UpdateApprovedProvider(currentProvider, true);

            if (currentProvider == null)
            {
                lbl5Msg.Text = "Provider information cannot be saved 1.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                //process transaction
                lbl5Msg.Text = SubmitTransaction(currentProvider.ID);
                //UNCOMMENT FOR TESTING
                //lbl5Msg.Text = "";
                
                if (lbl5Msg.Text != "") //transaction failed
                {
                    //rollback update
                    sessionProvider = providerDA.UpdateApprovedProvider(sessionProvider,true);

                    currentProvider = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //remove inactinve provider from session
                    Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] = null;

                    //insert newly active provider in session (as logged in provider)
                    Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] = currentProvider;

                    //send mail to admin and applicant
                    if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
                    {
                        try
                        {
                            LACESUtilities.SendMailsAfterProviderApplicationRenewed(currentProvider.ApplicantName, currentProvider.ApplicantEmail, currentProvider.OrganizationName);
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                            Response.End();
                        }
                    }
                    Response.Redirect("ProviderSubmissionSuccess.aspx?Renew=true&Status=" + sessionProvider.Status, true);
                }
            }
        }
    }
    #endregion

    #region Populate an approver provider object from fields from provider object
    /// <summary>
    /// This function fills an approved provider object with form values
    /// </summary>
    /// <returns></returns>
    private ApprovedProvider FIllApprovedProviderObjectByFormValues()
    {
        ApprovedProvider currentProvider = new ApprovedProvider();

        currentProvider.OrganizationName = OrganizationName.Text;
        currentProvider.OrganizationStreetAddress = OrganizationStreetAddress.Text;
        currentProvider.OrganizationCity = OrganizationCity.Text;
        currentProvider.OrganizationState = OrganizationState.Text;
        currentProvider.OrganizationZip = OrganizationZip.Text;
        currentProvider.OrganizationCountry = OrganizationCountry.Text;
        currentProvider.OrganizationPhone = OrganizationPhone.Text;
        currentProvider.OrganizationFax = OrganizationFax.Text;
        currentProvider.OrganizationWebSite = OrganizationWebSite.Text;

        currentProvider.ApplicantName = ApplicantName.Text;
        currentProvider.ApplicantPosition = ApplicantPosition.Text;
        currentProvider.ApplicantPhone = ApplicantPhone.Text;
        currentProvider.ApplicantFax = ApplicantFax.Text;
        currentProvider.ApplicantEmail = ApplicantEmail.Text;

        //set the password fields
        currentProvider.Password = LACESUtilities.Encrypt(HidPass.Value);

        currentProvider.OrganizationNature = OrganizationNature.Text;
        currentProvider.LegallyConstituted = LegallyConstituted.Checked;
        currentProvider.LegallyConstitutedDescription = LegallyConstitutedDescription.Text;
        currentProvider.RegionallyAccredited = RegionallyAccredited.Checked;
        currentProvider.RegionallyAccreditedDescription = RegionallyAccreditedDescription.Text;
        currentProvider.ProfessionalAssociation = ProfessionalAssociation.Checked;
        currentProvider.FederalOrganization = FederalOrganization.Checked;
        currentProvider.StateOrganization = StateOrganization.Checked;
        currentProvider.LocalGovernmentAgency = LocalGovernmentAgency.Checked;
        currentProvider.OrganizationUnderstandLACES = (OrganizationUnderstandLACES.SelectedValue == "True") ? true : false;
        currentProvider.CoursesOfferedAsLACES = (CoursesOfferedAsLACES.SelectedValue == "True") ? true : false;
        currentProvider.AgreeToDesignateCoursesAsLACES = (AgreeToDesignateCoursesAsLACES.SelectedValue == "True") ? true : false;
        currentProvider.FollowLACESGuidelines = (FollowLACESGuidelines.SelectedValue == "True") ? true : false;
        currentProvider.AgreeToFollowLACESGuidelines = (AgreeToFollowLACESGuidelines.SelectedValue == "True") ? true : false;

        currentProvider.HowDeterminesCourses = HowDeterminesCourses.Text;
        currentProvider.OrganizedAndSystematicProcess = (OrganizedAndSystematicProcess.SelectedValue == "True") ? true : false;
        currentProvider.DescribeProcedures = DescribeProcedures.Text;
        currentProvider.WrittenLearningObjectives = (WrittenLearningObjectives.SelectedValue == "") ? 0 : Convert.ToInt32(WrittenLearningObjectives.SelectedValue);
        currentProvider.DescribeCourse1 = DescribeCourse1.Text;
        currentProvider.DescribeCourse2 = DescribeCourse2.Text;
        currentProvider.AgreeToDevelopWrittenObjectives = (AgreeToDevelopWrittenObjectives.SelectedValue == "True") ? true : false;
        currentProvider.HowDeterminePersonnelDevelopCourses = HowDeterminePersonnelDevelopCourses.Text;
        currentProvider.HowDeterminePersonnelDeliverCourses = HowDeterminePersonnelDeliverCourses.Text;
        currentProvider.EvaluateCoursesToEnsureProgramContent = (EvaluateCoursesToEnsureProgramContent.SelectedValue == "True") ? true : false;
        currentProvider.ProceduresSurveysEvaluationInstrumentsOrganizationUses = ProceduresSurveysEvaluationInstrumentsOrganizationUses.Text;
        currentProvider.FollowLACESCriteriaToUseOnlyMaterials = (FollowLACESCriteriaToUseOnlyMaterials.SelectedValue == "True") ? true : false;
        currentProvider.HowOrganizationAssessParticipantAttainment = HowOrganizationAssessParticipantAttainment.Text;
        currentProvider.CoursesEvaluatedByParticipants = (CoursesEvaluatedByParticipants.SelectedValue == "") ? 0 : Convert.ToInt32(CoursesEvaluatedByParticipants.SelectedValue);
        currentProvider.EvaluateProgramsInwaysOtherThanByParticipants = (EvaluateProgramsInwaysOtherThanByParticipants.SelectedValue == "") ? 0 : Convert.ToInt32(EvaluateProgramsInwaysOtherThanByParticipants.SelectedValue);
        currentProvider.ExplainMethodsInwaysOtherThanByParticipants = ExplainMethodsInwaysOtherThanByParticipants.Text;
        currentProvider.OrganizationProvideCertificates = (OrganizationProvideCertificates.SelectedValue == "True") ? true : false;
        currentProvider.HowOrganizationProvideConfirmation = HowOrganizationProvideConfirmation.Text;
        currentProvider.AgreeToMaintainCompleteAttendanceRecords = (AgreeToMaintainCompleteAttendanceRecords.SelectedValue == "True") ? true : false;
        currentProvider.DescribeOrganizationsRecordkeepingSystem = DescribeOrganizationsRecordkeepingSystem.Text;
        currentProvider.OrganizationHasAnInternalReviewProcess = (OrganizationHasAnInternalReviewProcess.SelectedValue == "True") ? true : false;
        currentProvider.DescribeWrittenPolicyOrCriteriaReviewProcess = DescribeWrittenPolicyOrCriteriaReviewProcess.Text;

        currentProvider.AgreementOrganizationName = AgreementOrganizationName.Text;
        currentProvider.AuthorizerName = AuthorizerName.Text;
        currentProvider.AuthorizerPosition = AuthorizerPosition.Text;
        currentProvider.AuthorizerDate = Convert.ToDateTime(AuthorizerDate.Text);
        currentProvider.AuthorizerPhone = AuthorizerPhone.Text;
        currentProvider.AuthorizerEmail = AuthorizerEmail.Text;
        currentProvider.ShortDescription = uiTxtDescription.Text;
        currentProvider.LearningOutcome = uiTxtLearningOutcome.Text;
        currentProvider.CourseOutline = uiTxtCourseOutline.Text;
        currentProvider.DevelopInternalReview = (uiRblInternalReview.SelectedValue == "True") ? "T" : "F";
        currentProvider.KeepAllMaterials = (uiRblKeepMaterials.SelectedValue== "True") ? "T" : "F";


        return currentProvider;
    }
    #endregion

    #region Populate form fields from provider object
    /// <summary>
    /// Populate form fields from provider object
    /// </summary>
    /// <param name="currentProvider"></param>
    private void FillFormValuesByProviderObject(ApprovedProvider currentProvider)
    {
        OrganizationName.Text = currentProvider.OrganizationName;
        OrganizationStreetAddress.Text = currentProvider.OrganizationStreetAddress;
        OrganizationCity.Text = currentProvider.OrganizationCity;
        OrganizationState.Text = currentProvider.OrganizationState;
        OrganizationZip.Text = currentProvider.OrganizationZip;
        OrganizationCountry.Text = currentProvider.OrganizationCountry;
        OrganizationPhone.Text = currentProvider.OrganizationPhone;
        OrganizationFax.Text = currentProvider.OrganizationFax;
        OrganizationWebSite.Text = currentProvider.OrganizationWebSite;

        ApplicantName.Text = currentProvider.ApplicantName;
        ApplicantPosition.Text = currentProvider.ApplicantPosition;
        ApplicantPhone.Text = currentProvider.ApplicantPhone;
        ApplicantFax.Text = currentProvider.ApplicantFax;
        ApplicantEmail.Text = currentProvider.ApplicantEmail;
        EmailConfirm.Text = currentProvider.ApplicantEmail;

        //set the password fields
        HidPass.Value = LACESUtilities.Decrypt(currentProvider.Password);
        Password.Attributes.Add("value", HidPass.Value);
        PasswordConfirm.Attributes.Add("value", HidPass.Value);


        OrganizationNature.Text = currentProvider.OrganizationNature;
        LegallyConstituted.Checked = currentProvider.LegallyConstituted;
        LegallyConstitutedDescription.Text = currentProvider.LegallyConstitutedDescription;
        RegionallyAccredited.Checked = currentProvider.RegionallyAccredited;
        RegionallyAccreditedDescription.Text = currentProvider.RegionallyAccreditedDescription;
        ProfessionalAssociation.Checked = currentProvider.ProfessionalAssociation;
        FederalOrganization.Checked = currentProvider.FederalOrganization;
        StateOrganization.Checked = currentProvider.StateOrganization;
        LocalGovernmentAgency.Checked = currentProvider.LocalGovernmentAgency;
        OrganizationUnderstandLACES.SelectedValue = currentProvider.OrganizationUnderstandLACES.ToString();

        CoursesOfferedAsLACES.SelectedValue = currentProvider.CoursesOfferedAsLACES.ToString();

        if (!currentProvider.CoursesOfferedAsLACES)
        {
            AgreeToDesignateCoursesAsLACES.SelectedValue = currentProvider.AgreeToDesignateCoursesAsLACES.ToString();
            AgreeToDesignateCoursesAsLACES.Enabled = true;
        }

        FollowLACESGuidelines.SelectedValue = currentProvider.FollowLACESGuidelines.ToString();

        if (!currentProvider.FollowLACESGuidelines)
        {
            AgreeToFollowLACESGuidelines.SelectedValue = currentProvider.AgreeToFollowLACESGuidelines.ToString();
            AgreeToFollowLACESGuidelines.Enabled = true;
        }

        HowDeterminesCourses.Text = currentProvider.HowDeterminesCourses;
        OrganizedAndSystematicProcess.SelectedValue = currentProvider.OrganizedAndSystematicProcess.ToString();
        DescribeProcedures.Text = currentProvider.DescribeProcedures;
        WrittenLearningObjectives.SelectedValue = currentProvider.WrittenLearningObjectives.ToString();

        //if (currentProvider.WrittenLearningObjectives == 1 || currentProvider.WrittenLearningObjectives == 2)
        //{
        DescribeCourse1.Text = currentProvider.DescribeCourse1;
        DescribeCourse2.Text = currentProvider.DescribeCourse2;
        //    DescribeCourse1.Enabled = true;
        //    DescribeCourse2.Enabled = true;
        //}
        //else
        //{
        AgreeToDevelopWrittenObjectives.SelectedValue = currentProvider.AgreeToDevelopWrittenObjectives.ToString();
        //    AgreeToDevelopWrittenObjectives.Enabled = true;
        //}

        HowDeterminePersonnelDevelopCourses.Text = currentProvider.HowDeterminePersonnelDevelopCourses;
        HowDeterminePersonnelDeliverCourses.Text = currentProvider.HowDeterminePersonnelDeliverCourses;
        EvaluateCoursesToEnsureProgramContent.SelectedValue = currentProvider.EvaluateCoursesToEnsureProgramContent.ToString();

        //if (currentProvider.EvaluateCoursesToEnsureProgramContent)
        //{
        ProceduresSurveysEvaluationInstrumentsOrganizationUses.Text = currentProvider.ProceduresSurveysEvaluationInstrumentsOrganizationUses;
        //    ProceduresSurveysEvaluationInstrumentsOrganizationUses.Enabled = true;
        //}


        FollowLACESCriteriaToUseOnlyMaterials.SelectedValue = currentProvider.FollowLACESCriteriaToUseOnlyMaterials.ToString();
        HowOrganizationAssessParticipantAttainment.Text = currentProvider.HowOrganizationAssessParticipantAttainment;
        CoursesEvaluatedByParticipants.SelectedValue = currentProvider.CoursesEvaluatedByParticipants.ToString();
        EvaluateProgramsInwaysOtherThanByParticipants.SelectedValue = currentProvider.EvaluateProgramsInwaysOtherThanByParticipants.ToString();

        //if (currentProvider.EvaluateProgramsInwaysOtherThanByParticipants == 1 || currentProvider.EvaluateProgramsInwaysOtherThanByParticipants == 3)
        //{
        ExplainMethodsInwaysOtherThanByParticipants.Text = currentProvider.ExplainMethodsInwaysOtherThanByParticipants;
        //    ExplainMethodsInwaysOtherThanByParticipants.Enabled = true;
        //}

        OrganizationProvideCertificates.SelectedValue = currentProvider.OrganizationProvideCertificates.ToString();

        //if (!currentProvider.OrganizationProvideCertificates)
        //{
        HowOrganizationProvideConfirmation.Text = currentProvider.HowOrganizationProvideConfirmation;
        //    HowOrganizationProvideConfirmation.Enabled = true;
        //}
        AgreeToMaintainCompleteAttendanceRecords.SelectedValue = currentProvider.AgreeToMaintainCompleteAttendanceRecords.ToString();
        DescribeOrganizationsRecordkeepingSystem.Text = currentProvider.DescribeOrganizationsRecordkeepingSystem;
        OrganizationHasAnInternalReviewProcess.SelectedValue = currentProvider.OrganizationHasAnInternalReviewProcess.ToString();

        //if (currentProvider.OrganizationHasAnInternalReviewProcess)
        //{
        DescribeWrittenPolicyOrCriteriaReviewProcess.Text = currentProvider.DescribeWrittenPolicyOrCriteriaReviewProcess;
        //    DescribeWrittenPolicyOrCriteriaReviewProcess.Enabled = true;
        //}

        AgreementOrganizationName.Text = currentProvider.AgreementOrganizationName;
        AuthorizerName.Text = currentProvider.AuthorizerName;
        AuthorizerPosition.Text = currentProvider.AuthorizerPosition;
        AuthorizerDate.Text = currentProvider.AuthorizerDate.ToString("MM/dd/yyyy");
        AuthorizerPhone.Text = currentProvider.AuthorizerPhone;
        AuthorizerEmail.Text = currentProvider.AuthorizerEmail;
        if (!currentProvider.OrganizationHasAnInternalReviewProcess)
        {
            uiRblInternalReview.SelectedValue = currentProvider.DevelopInternalReview;
            uiRblInternalReview.Enabled = true;
        }
        else
        {
            uiRblInternalReview.Enabled = false;
        }
        uiRblKeepMaterials.SelectedValue = currentProvider.KeepAllMaterials;
        uiTxtCourseOutline.Text = currentProvider.CourseOutline;
        uiTxtDescription.Text = currentProvider.ShortDescription;
        uiTxtLearningOutcome.Text = currentProvider.LearningOutcome;
    }
    #endregion

    #region Sets page title
    /// <summary>
    /// Sets page title according to current view
    /// </summary>
    private void SetPageTitle()
    {
        if (MultiView1.ActiveViewIndex == 0)
        {
            PageHeader.InnerHtml = "Step 1 of 5: Provider Information";
        }
        else if (MultiView1.ActiveViewIndex == 1)
        {
            PageHeader.InnerHtml = "Step 2 of 5: Approval Criteria";
            Page.MaintainScrollPositionOnPostBack = true;
        }
        else if (MultiView1.ActiveViewIndex == 2)
        {
            PageHeader.InnerHtml = "Step 3 of 5: Provider Agreement";

            //if renew readonly mode
            if (IsRenewReadOnlyMode())
            {
                PageHeader.InnerHtml = "Provider Agreement";
            }
            Page.MaintainScrollPositionOnPostBack = false;
        }
        else if (MultiView1.ActiveViewIndex == 3)
        {
            PageHeader.InnerHtml = "Step 4 of 5: Print Preview";

            //if renew readonly mode
            if (IsRenewReadOnlyMode())
            {
                PageHeader.InnerHtml = "Print Preview";
            }
        }
        else if (MultiView1.ActiveViewIndex == 4)
        {
            PageHeader.InnerHtml = "Step 5 of 5: Pay pal process";

            //if renew readonly mode
            if (IsRenewReadOnlyMode())
            {
                PageHeader.InnerHtml = "Pay pal process";
            }
        }
    }
    #endregion

    #region Submit Transaction
    /// <summary>
    /// Prepare online transaction data and send to paypal for prossing
    /// </summary>
    /// <returns></returns>
    public string SubmitTransaction(long ProviderID)
    {
        try
        {
            PaymentInformation payment = new PaymentInformation();
            payment.ProviderID = ProviderID;
            payment.NameOnCard = NameOnCard.Text;
            payment.Street = CreditCardStreet.Text;
            payment.City = CreditCardCity.Text;
            payment.State = CreditCardState.Text;
            payment.Zip = CreditCardZip.Text;
            payment.CreditCardType = CreditCardType.Text;
            payment.CreditCardNumber = CreditCardNumber.Text;
            payment.CreditCardVerificationCode = CreditCardVerificationCode.Text;
            payment.CardExpirationMonth = CardExpirationMonth.Text;
            payment.CardExpirationYear = CardExpirationYear.Text;
            payment.TransactionAmount = Convert.ToDouble(LACESUtilities.GetApplicationConstants(LACESConstant.PaymentInfo.PROVIDER_FEE));

            string response = LACESUtilities.ProcessCreditCard(payment);

            string[] responseArr = response.Split(new string[] { "=", "&" }, StringSplitOptions.None);

            if (responseArr.Length > 0)
            {
                if (responseArr[1] == "0") //TRANSACTION SUCCESSFUL
                {
                    payment.TransactionTime = DateTime.Now;
                    payment.TransactionResult = response;

                    //create Provider Data Access object
                    PaymentInformationDataAccess paymentDA = new PaymentInformationDataAccess();
                    payment = paymentDA.AddPaymentInformation(payment);

                    return "";
                }
                else
                {
                    for (int i = 0; i < responseArr.Length; i++)
                    {
                        if (responseArr[i] == "RESPMSG")
                        {
                            response = responseArr[i + 1] + ". Credit card transaction failed.";
                            break;
                        }
                    }
                    return response;
                }
            }
            else
            {
                return "Credit card transaction failed.";
            }

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    #endregion

    #region Enable/Disable Form Fields
    /// <summary>
    /// Enable or disable fields of step 2 according to field selection 
    /// </summary>
    private void EnableDisableFormFields()
    {
        if (CoursesOfferedAsLACES.SelectedValue == "False")
        {
            AgreeToDesignateCoursesAsLACES.Enabled = true;
            //AgreeToDesignateCoursesAsLACES.Items[0].Enabled = true;
            //AgreeToDesignateCoursesAsLACES.Items[1].Enabled = true;
        }
        else if (CoursesOfferedAsLACES.SelectedValue == "True")
        {
            AgreeToDesignateCoursesAsLACES.Enabled = false;
            //AgreeToDesignateCoursesAsLACES.Items[0].Enabled = false;
            //AgreeToDesignateCoursesAsLACES.Items[1].Enabled = false;
        }
        else
        {
            AgreeToDesignateCoursesAsLACES.Enabled = true;
        }

        if (FollowLACESGuidelines.SelectedValue == "False")
        {
            AgreeToFollowLACESGuidelines.Enabled = true;
            //AgreeToFollowLACESGuidelines.Items[0].Enabled = true;
            //AgreeToFollowLACESGuidelines.Items[1].Enabled = true;
        }
        else if (FollowLACESGuidelines.SelectedValue == "True")
        {
            AgreeToFollowLACESGuidelines.Enabled = false;
            //AgreeToFollowLACESGuidelines.Items[0].Enabled = false;
            //AgreeToFollowLACESGuidelines.Items[1].Enabled = false;
        }
        else
        {
            AgreeToFollowLACESGuidelines.Enabled = true;
        }

        if (WrittenLearningObjectives.SelectedValue == "1" || WrittenLearningObjectives.SelectedValue == "2")
        {
            DescribeCourse1.Enabled = true;
            DescribeCourse2.Enabled = true;
            AgreeToDevelopWrittenObjectives.Enabled = false;
            //AgreeToDevelopWrittenObjectives.Items[0].Enabled = false;
            //AgreeToDevelopWrittenObjectives.Items[1].Enabled = false;
        }
        else if (WrittenLearningObjectives.SelectedValue == "3")
        {
            DescribeCourse1.Enabled = false;
            DescribeCourse2.Enabled = false;
            AgreeToDevelopWrittenObjectives.Enabled = true;
            //AgreeToDevelopWrittenObjectives.Items[0].Enabled = true;
            //AgreeToDevelopWrittenObjectives.Items[1].Enabled = true;
        }
        else
        {
            AgreeToDevelopWrittenObjectives.Enabled = true;
        }

        if (EvaluateCoursesToEnsureProgramContent.SelectedValue == "True")
        {
            ProceduresSurveysEvaluationInstrumentsOrganizationUses.Enabled = true;
        }
        else if (EvaluateCoursesToEnsureProgramContent.SelectedValue == "False")
        {
            ProceduresSurveysEvaluationInstrumentsOrganizationUses.Enabled = false;
        }

        if (EvaluateProgramsInwaysOtherThanByParticipants.SelectedValue == "1" || EvaluateProgramsInwaysOtherThanByParticipants.SelectedValue == "3")
        {
            ExplainMethodsInwaysOtherThanByParticipants.Enabled = true;
        }
        else if (EvaluateProgramsInwaysOtherThanByParticipants.SelectedValue == "2")
        {
            ExplainMethodsInwaysOtherThanByParticipants.Enabled = false;
        }

        if (OrganizationProvideCertificates.SelectedValue == "False")
        {
            HowOrganizationProvideConfirmation.Enabled = true;
        }
        else if (OrganizationProvideCertificates.SelectedValue == "True")
        {
            HowOrganizationProvideConfirmation.Enabled = false;
        }

        if (OrganizationHasAnInternalReviewProcess.SelectedValue == "True")
        {
            DescribeWrittenPolicyOrCriteriaReviewProcess.Enabled = true;
        }
        else if (OrganizationHasAnInternalReviewProcess.SelectedValue == "False")
        {
            DescribeWrittenPolicyOrCriteriaReviewProcess.Enabled = false;
        }
    }
    #endregion

    #region Renew mode Or new mode
    /// <summary>
    /// Is provider in renew mode
    /// </summary>
    /// <returns></returns>
    private bool IsRenewMode()
    {
        return Session[sessionKeyForProvider] != null && Request.QueryString["HasChange"] != null;
    }

    /// <summary>
    /// Is provider in read only renew mode
    /// </summary>
    /// <returns></returns>
    private bool IsRenewReadOnlyMode()
    {
        return Session[sessionKeyForProvider] != null && Request.QueryString["HasChange"] != null && Request.QueryString["HasChange"] != "true";
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
