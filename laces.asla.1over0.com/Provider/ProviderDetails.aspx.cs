/*------------------------------------------------------------------------------
 * Project Name     : LACES
 * Client Name      : American Society of Landscape Architects(ASLA)
 * Component Name   : ProviderDetails.aspx.cs
 * Purpose/Function : Update current login provider details information.
 * Author           : Alamgir Hossain
 * 
 * Version                 Author            Date         Reason 
 * 1.0                  Alamgir Hossain    01/16/08   Create and UI Components
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
using System.Diagnostics;

/// <summary>
/// Used to update current login provider information. 
/// Provider information are get from session variable
/// </summary>
public partial class Provider_ProviderDetails : ProviderBasePage
{
    #region Page Load Event Handler

    /// <summary>
    ///  Page Load Event Handler, call every time when the page is loaded.
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //reset the error message label 
        lblMsg.Text = "";
        lblMsg.ForeColor = System.Drawing.Color.Red;
  
        //set focus the first textbox
        txtOrganization.Focus();

        if (!IsPostBack)
        {
            //default set the status "Active"
            //radStatus.SelectedIndex = 0;

            //Load the state list
            populateStateList();

            //populate provider information from session
            populateProviderInformation();
        }

        
        
    }

    #endregion

    #region Populate Provider Information

    /// <summary>
    /// Used to load provider information into UI control.
    /// Get provider information from session variable.
    /// Return none.
    /// </summary>
    private void populateProviderInformation()
    {
        try
        {
            //check session variable
            if (Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER] != null)
            {
                //get the provider from session
                Provider1 currentProvider = (Provider1)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // Get provider information from Session 

                //check weather the provider is exist
                if (currentProvider != null)
                {
                    txtOrganization.Text = currentProvider.Organization;
                    txtStreetAddress.Text = currentProvider.StreetAddress;
                    txtCity.Text = currentProvider.City;
                    drpState.SelectedValue = currentProvider.State.Trim();
                    txtZip.Text = currentProvider.Zip;
                    txtCountry.Text = currentProvider.Country;
                    txtPhone.Text = currentProvider.Phone;

                    txtFax.Text = currentProvider.Fax;
                    txtWebsite.Text = currentProvider.Website;
                    txtIndName.Text = currentProvider.IndividualsName;
                    txtIndPosition.Text = currentProvider.IndividualsPosition;
                    txtIndPhone.Text = currentProvider.IndividualsPhone;
                    txtIndFax.Text = currentProvider.IndividualsFax;
                    txtEmail.Text = currentProvider.IndividualsEmail;
                    
                    //radStatus.SelectedValue = currentProvider.Status.Trim();
                    //set the password fields
                    txtPassword.Attributes.Add("value", LACESUtilities.Decrypt(currentProvider.IndividualsPassword));
                    txtPasswordConfirm.Attributes.Add("value", LACESUtilities.Decrypt(currentProvider.IndividualsPassword));

                    //update provider name in the UI
                    Label lblMasterProviderName = (Label)Master.FindControl("lblProviderName");
                    lblMasterProviderName.Text = Server.HtmlEncode("Signed in as: " + currentProvider.Organization);

                }
                else
                {
                    //show error message
                    lblMsg.Text = "Organization not exists.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    #endregion

    #region Manage Provider Click Event

    /// <summary>
    /// Call every time when user click on 'Save Details' button.
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        //create Provider Data Access object
        ProviderDataAccess oProviderDataAccess = new ProviderDataAccess();

        //Check for existence for Email address
        Provider1 oProvider = oProviderDataAccess.GetbyEmail(txtEmail.Text);

        //now check weather it is in edit provider mode
        if (Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER] != null)
        {
            //get the provider from session
            Provider1 currentProvider = (Provider1)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // Get provider information from Session 

            //check weather it is the same person
            if (oProvider != null && oProvider.ID == currentProvider.ID) oProvider = null;
        }

        if (oProvider == null)
        {
            // The email is not already used, means the current provider can use the email
            Provider1 currentProvider = new Provider1();

            currentProvider.Organization = txtOrganization.Text.Trim();
            currentProvider.StreetAddress = txtStreetAddress.Text.Trim();
            currentProvider.City = txtCity.Text.Trim();
            currentProvider.State = drpState.SelectedValue.Trim();
            currentProvider.Zip = txtZip.Text.Trim();
            currentProvider.Country = txtCountry.Text.Trim();
            currentProvider.Phone = txtPhone.Text.Trim();
            currentProvider.Fax = txtFax.Text.Trim();
            currentProvider.Website = txtWebsite.Text.Trim();

            currentProvider.IndividualsName = txtIndName.Text.Trim();
            currentProvider.IndividualsPosition = txtIndPosition.Text.Trim();
            currentProvider.IndividualsPhone = txtIndPhone.Text.Trim();
            currentProvider.IndividualsFax = txtIndFax.Text.Trim();
            currentProvider.IndividualsEmail = txtEmail.Text.Trim();

            //set the password fields
            txtPassword.Attributes.Add("value", txtPassword.Text);
            txtPasswordConfirm.Attributes.Add("value", txtPasswordConfirm.Text); 

            //Encode the password before saving the provider details information 
            currentProvider.IndividualsPassword = LACESUtilities.Encrypt(txtPassword.Text);

            //set the provider status   
            //currentProvider.Status = "Y";

            //now check session variable
            if (Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER] != null)
            {
                //get the provider id from query string
                long providerID = ((Provider1)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]).ID; 

                currentProvider.ID = providerID;

                currentProvider=oProviderDataAccess.Update(currentProvider);

                if (currentProvider == null)
                {
                    lblMsg.Text = "Cannot save data.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //send mail successfull
                    lblMsg.Text = "Contact information updated successfully.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;

                    //update the current provider information in the session 
                    Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER] = currentProvider;

                    //populate provider updated information from session
                    populateProviderInformation();
                }
            }
        }
        else
        {
            //The email is already used, so not allowed
            lblMsg.Text = "Email already exists. Please provide another email.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            txtEmail.Focus();
        }
    }

    #endregion

    #region Populate State List

    /// <summary>
    /// Use to populate state list in the UI. Get state data from database.
    /// Return none.
    /// </summary>
    private void populateStateList()
    {
        //Get all States from DB 
        StateDataAccess stateDAL = new StateDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int StateProvidenceID = int.Parse(LACESUtilities.GetApplicationConstants("StateProvinceContentID"));
        IList<State> stateList = stateDAL.GetAllStates(StateProvidenceID, webroot);

        //Add the default value in to the DropDownList
        ListItem defaultItem = new ListItem("-State List-", "-State List-");
        drpState.Items.Add(defaultItem);

        //Prepare the list as a user friendly version and add into the DropDownList
        foreach (State state in stateList)
        {
            ListItem item = new ListItem(state.StateName, state.StateCode);
            drpState.Items.Add(item);
        }
    }
    #endregion
}
