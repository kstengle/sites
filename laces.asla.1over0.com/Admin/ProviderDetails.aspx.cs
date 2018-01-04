/*------------------------------------------------------------------------------
 * Project Name     : LACES
 * Client Name      : American Society of Landscape Architects(ASLA)
 * Component Name   : ProviderDetails.aspx.cs
 * Purpose/Function : Add a new provider or edit an existing provider information.
 * Author           : Alamgir Hossain
 * 
 * Version                 Author             Date         Reason 
 * 1.0                  Alamgir Hossain     01/16/08   Create and UI Components
 * 1.1                  Matiur Rahman       01/31/08   Clear form after added successfully
 * 1.1                  Alamgir Hossain     07/05/2008      Work on Enhancement 2    
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


using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
/// <summary>
/// Used to add a new provider in the admin mode or update an existing provider.
/// When used to update it take provider id from query string.
/// </summary>
public partial class Admin_ProviderDetails : AdminBasePage
{
    
    #region Page Load Event Handler

    /// <summary>
    ///  Page Load Event Handler. Call every time when the page is loaded.
    /// </summary>
    /// <param name="sender">Sender object</param>
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
            radStatus.SelectedIndex = 0;

            //Load the state list
            populateStateList();

            //check weather it is in edit provider mode
            if (Request.QueryString[LACESConstant.QueryString.PROVIDER_ID] != null)
            {
                try
                {
                    //get the provider id from query string
                    long providerID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.PROVIDER_ID]);

                    //get provider object from DAL 
                    Provider1 currentProvider = new Provider1();
                    currentProvider.ID = providerID;
                    ProviderDataAccess oProviderDataAccess = new ProviderDataAccess();
                    currentProvider = oProviderDataAccess.GetById(currentProvider.ID);

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
                        radStatus.SelectedValue = currentProvider.Status.Trim();

                        //set the password fields
                        txtPassword.Attributes.Add("value", LACESUtilities.Decrypt(currentProvider.IndividualsPassword));
                        txtPasswordConfirm.Attributes.Add("value", LACESUtilities.Decrypt(currentProvider.IndividualsPassword));
                    }
                    else
                    {
                        //show error message
                        lblMsg.Text = LACESConstant.Messages.PROVIDER_NOT_FOUND_IN_ADMIN;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

    #endregion

    #region Save Provider Click Event

    /// <summary>
    /// Call every time when user click on Save Details button.
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
        if (Request.QueryString[LACESConstant.QueryString.PROVIDER_ID] != null)
        {
            //get the provider id from query string
            long providerID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.PROVIDER_ID]);

            //check weather it is the same person
            if (oProvider !=null && oProvider.ID == providerID) oProvider = null;
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
            currentProvider.Status = radStatus.SelectedValue;

            //now check weather it is in edit provider mode
            if (Request.QueryString[LACESConstant.QueryString.PROVIDER_ID] != null)
            {
                //get the provider id from query string
                long providerID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.PROVIDER_ID]);

                currentProvider.ID = providerID;

                if (oProviderDataAccess.Update(currentProvider) == null)
                {
                    lblMsg.Text = "Cannot save data.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //data saved successfully so prepare and send mail to the respective provider
                    if (SendMailToProvider(true))
                    {
                        //send mail successfull
                        lblMsg.Text = "Contact information updated successfully and email sent to the contact.";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        //send mail error
                        lblMsg.Text = "Contact information updated successfully and email sent to the contact.";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                }
            }

            else //it is in add provider mode, so add a new provider
            {
                //all thing ok, so store provider into the DB
                if (oProviderDataAccess.Add(currentProvider) == null)
                {
                    lblMsg.Text = "Cannot save data.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //data add successfully so prepare and send mail to the respective provider
                    if (SendMailToProvider(false))
                    {
                        //send mail successfull
                        lblMsg.Text = "Contact information added successfully and email sent to the contact.";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        //send mail error
                        lblMsg.Text = "Contact information added successfully and email sent to the contact.";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    clearForm();
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

    #region Send Mail

    /// <summary>
    /// Used to send mail when admin add a new provider or update an existing provider.
    /// Return true is send mail successfull, otherwise return false.
    /// </summary>
    /// <param name="isUpdate">True if provider update mode</param>
    /// <returns>Return true if send mail successfull</returns>
    private bool SendMailToProvider(bool isUpdate)
    {
        try
        {
            SmtpClient smtpClient = new SmtpClient();

            //Get the SMTP server Address from SMTP Web.conf
            smtpClient.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER);

            //Get the SMTP port  25;
            smtpClient.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));

            //create the message body
            MailMessage message = new MailMessage();

            string adminToAddress = LACESUtilities.GetAdminToEmail().Replace(",", ";") + "?cc=" + LACESUtilities.GetAdminCCEmail();
            string ToEmailAddress = Server.HtmlEncode(txtEmail.Text.Trim());

            message.From = new MailAddress(LACESUtilities.GetAdminFromEmail());
            message.To.Add(new MailAddress(txtEmail.Text.Trim()));
            message.Subject = "American Society of Landscape Architects:" + LACESConstant.LACES_TEXT + " Notification";
            message.IsBodyHtml = true;

            string RefrenceURL = String.Empty;
            RefrenceURL = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/", Request.Url.AbsoluteUri.LastIndexOf("/") - 1)) + "/provider/" + LACESConstant.URLS.PROVIDER_LOGIN;

            //create the emailbody
            StringBuilder EmailBody = new StringBuilder();
            EmailBody.Append("<html>");
            EmailBody.Append("<head>");
            EmailBody.Append("<title>Title</title>");
            EmailBody.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">");
            EmailBody.Append("</head>");
            EmailBody.Append("<body style=\"font-family: Arial, verdana, Helvetica, sans-serif; font-size:13px;\">");
            string MailBody = "";


            //if the provider is active
            if (radStatus.SelectedValue == "Y")
            {
                if (isUpdate)
                {
                    MailBody = @"<div>Dear " + Server.HtmlEncode(txtOrganization.Text.Trim()) + ", </br></br> " + LACESConstant.LACES_TEXT + " admin updated your profile as an active provider. Your updated login information are as follows:</div></br>" +
                                    "<p><div>User Name: " + Server.HtmlEncode(txtEmail.Text.Trim()) + "</div>" +
                                    "<div>Password: " + Server.HtmlEncode(txtPassword.Text) + "</div></p>" +
                                    "<p><div>You can access the " + LACESConstant.LACES_TEXT + " system at the following URL: " +
                                    "<a href=\"" + RefrenceURL + "\">" + RefrenceURL + "</a>.</p><p> If you require further assistance, please contact the " + LACESConstant.LACES_TEXT + " Administrators at the following email address: "
                                    + "<a href=\"mailto:" + adminToAddress + "\">" + LACESConstant.LACES_TEXT + " Administrators' Emails</a>.</p></div>";

                }
                else
                {
                    MailBody = @"<div>Dear " + Server.HtmlEncode(txtOrganization.Text.Trim()) + ", </br></br> " + LACESConstant.LACES_TEXT + " admin added you as an active provider. Your login information are as follows:</div></br>" +
                                        "<p><div>User Name: " + Server.HtmlEncode(txtEmail.Text.Trim()) + "</div>" +
                                        "<div>Password: " + Server.HtmlEncode(txtPassword.Text) + "</div></p>" +
                                        "<p><div>You can access the " + LACESConstant.LACES_TEXT + " system at the following URL: " +
                                        "<a href=\"" + RefrenceURL + "\">" + RefrenceURL + "</a>.</p><p> If you require further assistance, please contact the " + LACESConstant.LACES_TEXT + " Administrators at the following email address: "
                                        + "<a href=\"mailto:" + adminToAddress + "\">" + LACESConstant.LACES_TEXT + " Administrators' Emails</a>.</p></div>";
                }
            }
            //else if the provider is inactive 
            else if (radStatus.SelectedValue == "N")
            {
                if (isUpdate)
                {
                    MailBody = @"<div>Dear " + Server.HtmlEncode(txtOrganization.Text.Trim()) + ", </br></br> " + LACESConstant.LACES_TEXT + " admin updated your profile and set you as an inactive provider. Your login information are as follows:</div></br>" +
                                        "<p><div>User Name: " + Server.HtmlEncode(txtEmail.Text.Trim()) + "</div>" +
                                        "<div>Password: " + Server.HtmlEncode(txtPassword.Text) + "</div></p>" +
                                        "<p><div>You can access the " + LACESConstant.LACES_TEXT + " system at the following URL when your account will be activated: " +
                                        "<a href=\"" + RefrenceURL + "\">" + RefrenceURL + "</a>.</p><p> If you require further assistance, please contact the " + LACESConstant.LACES_TEXT + " Administrators at the following email address: "
                                        + "<a href=\"mailto:" + adminToAddress + "\">" + LACESConstant.LACES_TEXT + " Administrators' Emails</a>.</p></div>";

                }
                else
                {
                    MailBody = @"<div>Dear " + Server.HtmlEncode(txtOrganization.Text.Trim()) + ", </br></br> " + LACESConstant.LACES_TEXT + " admin added you as an inactive provider. Your login information are as follows:</div></br>" +
                        "<p><div>User Name: " + Server.HtmlEncode(txtEmail.Text.Trim()) + "</div>" +
                        "<div>Password: " + Server.HtmlEncode(txtPassword.Text) + "</div></p>" +
                        "<p><div>You can access the " + LACESConstant.LACES_TEXT + " system at the following URL when your account will be activated: " +
                        "<a href=\"" + RefrenceURL + "\">" + RefrenceURL + "</a>.</p><p> If you require further assistance, please contact the " + LACESConstant.LACES_TEXT + " Administrators at the following email address: "
                        + "<a href=\"mailto:" + adminToAddress + "\">" + LACESConstant.LACES_TEXT + " Administrators' Emails</a>.</p></div>";
                }
            }

            EmailBody.Append(MailBody);
            EmailBody.Append("</body>");
            EmailBody.Append("</html>");
            message.Body = EmailBody.ToString();

            //finally send mail
            if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
            {
                smtpClient.Send(message);
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #endregion

    #region Populate State List

    /// <summary>
    /// Use to populate state list in the UI. It get state information from database. Return none.
    /// </summary>
    private void populateStateList()
    {

        drpState.Items.Clear();
        //Get all States from DB 
        StateDataAccess stateDAL = new StateDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int StateProvidenceID = int.Parse(LACESUtilities.GetApplicationConstants("StateProvinceContentID"));
        IList<State> stateList = stateDAL.GetAllStates(StateProvidenceID, webroot);

        //Add the default value in to the DropDownList
        ListItem defaultItem = new ListItem("- State List -", "-State List-");
        drpState.Items.Add(defaultItem);

        //Prepare the list as a user friendly version and add into the DropDownList
        foreach (State state in stateList)
        {
            ListItem item = new ListItem(state.StateName, state.StateCode);
            drpState.Items.Add(item);
        }
        drpState.Items.Add(new ListItem("- Other International -", "OI"));
    }
    #endregion

    /// <summary>
    /// Clear form elements
    /// </summary>
    private void clearForm()
    {
        txtOrganization.Text = "";
        txtStreetAddress.Text = "";
        txtCity.Text = "";
        drpState.SelectedIndex = 0;
        radStatus.SelectedIndex = 0;
        txtZip.Text  = "";
        txtCountry.Text  = "";
        txtPhone.Text  = "";
        txtFax.Text  = "";
        txtWebsite.Text  = "";
        txtIndName.Text  = "";
        txtIndPosition.Text  = "";
        txtIndPhone.Text  = "";
        txtIndFax.Text  = "";
        txtEmail.Text  = "";

        txtPassword.Attributes.Add("value", "");
        txtPasswordConfirm.Attributes.Add("value", ""); 
    }
}
