/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: ContactDetails.aspx
 * Purpose/Function: Edit contact details of a provider
 * Author: Md. Kamruzzaman
 * 
 * Version    Author               Date          Reason 
 * 1.0        Md. Kamruzzaman      07/23/2008    Initial development 
 * 1.1        Md. Kamruzzaman      07/29/2008    task 8119
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
using System.Net.Mail;
using System.Text;

/// <summary>
/// Edit contact details of a provider
/// </summary>
public partial class Provider_ContactDetails : ProviderBasePage
{
    #region Page Load Event Handler

    /// <summary>
    ///  Page Load Event Handler. Call every time when the page is loaded.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //now check weather it is in edit provider mode
            if (Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] != null)
            {
                //get the provider from session
                ApprovedProvider sessionProvider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // Get provider information from Session 

                //fill form by provider in session 
                FillFormValuesByProviderObject(sessionProvider);
                //PopulateStateBasedOnCountry(OrganizationState, null);

            }
        }

        //reset the error message label 
        lblMsg.Text = "";
        lblMsg.ForeColor = System.Drawing.Color.Red;

        CheckUnsavedData();
    }

    #endregion

    #region Save Provider Click Event

    /// <summary>
    /// Call every time when user click on Save Details button.
    /// Update provider information in database
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        if (Password.Text == PasswordConfirm.Text)
        {
            HidPass.Value = Password.Text;
        }
        //create Provider Data Access object
        ApprovedProviderDataAccess providerDA = new ApprovedProviderDataAccess();

        //Check for existence for Email address
        ApprovedProvider oProvider = providerDA.GetApprovedProviderByEmail(ApplicantEmail.Text);

        //provider in session
        ApprovedProvider sessionProvider = null;

        //now check weather it is in edit provider mode
        if (Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] != null)
        {
            //get the provider from session
            sessionProvider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // Get provider information from Session 

            //provider changed email and that is set for other provider
            if (oProvider != null && oProvider.ID != sessionProvider.ID)
            {
                //The email is already used, so not allowed
                lblMsg.Text = "Email already exists. Please provide another email.<br /><br />";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                sessionProvider = FIllApprovedProviderObjectByFormValues(sessionProvider);

                //update provider contact info
                ApprovedProvider currentProvider = providerDA.UpdateApprovedProvider(sessionProvider);

                if (currentProvider == null)
                {
                    //exception occured
                    lblMsg.Text = "Contact information information cannot be saved.<br /><br />";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //update the current provider information in the session 
                    Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] = currentProvider;

                    //data saved successfully
                    lblMsg.Text = "Contact information updated successfully.<br /><br />";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
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
    private ApprovedProvider FIllApprovedProviderObjectByFormValues(ApprovedProvider sessionProvider)
    {
        
        sessionProvider.OrganizationName = OrganizationName.Text;
        sessionProvider.OrganizationStreetAddress = OrganizationStreetAddress.Text;
        sessionProvider.OrganizationCity = OrganizationCity.Text;
        sessionProvider.OrganizationState = OrganizationState.Text;
        sessionProvider.OrganizationZip = OrganizationZip.Text;
        sessionProvider.OrganizationCountry = OrganizationCountry.Text;
        sessionProvider.OrganizationPhone = OrganizationPhone.Text;
        sessionProvider.OrganizationFax = OrganizationFax.Text;
        sessionProvider.OrganizationWebSite = OrganizationWebSite.Text;

        sessionProvider.ApplicantName = ApplicantName.Text;
        sessionProvider.ApplicantPosition = ApplicantPosition.Text;
        sessionProvider.ApplicantPhone = ApplicantPhone.Text;
        sessionProvider.ApplicantFax = ApplicantFax.Text;
        sessionProvider.ApplicantEmail = ApplicantEmail.Text;

        //set the password fields
        sessionProvider.Password = LACESUtilities.Encrypt(HidPass.Value);

        return sessionProvider;
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
        ApplicantEmailConfirm.Text = currentProvider.ApplicantEmail;

        //set the password fields
        HidPass.Value = LACESUtilities.Decrypt(currentProvider.Password);
        Password.Attributes.Add("value", HidPass.Value);
        PasswordConfirm.Attributes.Add("value", HidPass.Value);
    }
    #endregion

    #region Check Unsave Information before navigation
    /// <summary>
    /// Check Unsave Information before navigation
    /// </summary>
    private void CheckUnsavedData()
    {
        //commented out (task 8108)
        //MonitorChangesWithPostBack(OrganizationName, OrganizationName.Text);

        MonitorChangesWithPostBack(OrganizationStreetAddress, OrganizationStreetAddress.Text);
        MonitorChangesWithPostBack(OrganizationCity, OrganizationCity.Text);
        MonitorChangesWithPostBack(OrganizationState, OrganizationState.Text);
        MonitorChangesWithPostBack(OrganizationZip, OrganizationZip.Text);
        MonitorChangesWithPostBack(OrganizationCountry, OrganizationCountry.Text);
        MonitorChangesWithPostBack(OrganizationPhone, OrganizationPhone.Text);
        MonitorChangesWithPostBack(OrganizationFax, OrganizationFax.Text);
        MonitorChangesWithPostBack(OrganizationWebSite, OrganizationWebSite.Text);

        MonitorChangesWithPostBack(ApplicantName, ApplicantName.Text);
        MonitorChangesWithPostBack(ApplicantPosition, ApplicantPosition.Text);
        MonitorChangesWithPostBack(ApplicantPhone, ApplicantPhone.Text);
        MonitorChangesWithPostBack(ApplicantFax, ApplicantFax.Text);
        MonitorChangesWithPostBack(ApplicantEmail, ApplicantEmail.Text);
    }
    #endregion
}
