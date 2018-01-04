/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: ProviderForgotPassword.aspx.cs
 * Purpose/Function: Provider Forget Password.
 * 
 * 
 * Author: Syed Abul Bashar
 * Version                 Author            Date         Reason 
 * 1.0                  Syed Abul Bashar   01/06/08   IU Component
 
 * 
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
using System.Net.Mail ;
using System.Text;
using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess; 
/// <summary>
/// Use to Send Forgot Password Email to Provider with valid email address 
/// 
/// </summary>
public partial class Provider_ProviderForgotPassword : VisitorBasePage
{
    #region memeber variables
    protected string postbackMessage;
    protected bool isInvalidEmail = false;
    #endregion

    #region Load Handler
    /// <summary>
    /// Load Event handler. Check provider exist in the database and send email to provider address
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">event argument</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //HtmlTableCell htmlTableRow = (HtmlTableCell)Master.FindControl("tdMenu");

        //htmlTableRow.InnerHtml="<img width=\"16\" height=\"30\" alt=\"\" src=\"../images/shim.gif\" /> ";
        //HtmlForm masterHtmlForm = (HtmlForm)Master.FindControl("form1");
        //masterHtmlForm.DefaultButton = btnSubmit.UniqueID;
        if (!IsPostBack)
        {
            txtEmail.Focus(); 
        }

        ///Assign welcome text
        string ContactEmailAddresses = TranformEmailAddresses();
        lblContactLink.Text = "<a href=\"mailto:" + ContactEmailAddresses + "\">contact the " + LACESConstant.LACES_TEXT + " Administrators</a> ";
                    
    }

    #endregion

    #region Button Handler
    /// <summary>
    /// Submit Button event handler. Check Email Address exist in the database and send forgot password Email to Provider.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CheckAuthentication();


    }

    /// <summary>
    /// Cancel Button Event Handler. Return to Provider Login page after clicking cancel button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }
    #endregion

    #region Private Functions

    /// <summary>
    /// Checking Provider email address exist in the database and send forgot password email to provider email address.
    /// </summary>
    private void CheckAuthentication()
    {
        try
        {
            string DecryptedPassword = String.Empty;
            ApprovedProviderDataAccess providerDAL = new ApprovedProviderDataAccess();

            ApprovedProvider provider = providerDAL.GetApprovedProviderByEmail(txtEmail.Text);
            if (provider != null && provider.ID > 0)
            {
                ///Decrypt provider password.
                DecryptedPassword = LACESUtilities.Decrypt(provider.Password);
Response.Write(DecryptedPassword);
Response.End();
                SmtpClient smtpClient = new SmtpClient();
                //Get the SMTP server Address from SMTP Web.conf
                smtpClient.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER);
                //Get the SMTP post  25;
                smtpClient.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));
                //send email
                bool IsSent =SendEmail(smtpClient, DecryptedPassword);
               //Checking the email has sent
                if (IsSent == true)
                {
                    lblErrorSummary.Visible = false;
                    lblPostBackMessage.Text = LACESConstant.Messages.FORGETPASSWORD_POSTBACK_MESSAGE.Replace("{0}", txtEmail.Text.Trim());
                    tblControls.Visible = false;
                }
            }
            //if the provider does not existing in the database send invalid email address message.
            else
            {
                //if the provider does not existing in the database send invalid email address message.
                if (IsPostBack == true)
                {

                    string ContactEmailAddresses = TranformEmailAddresses();
                    string AdminEmailWithFormat = "<a href=\"mailto:" + ContactEmailAddresses + "\">contact the " + LACESConstant.LACES_TEXT + " Administrators</a> ";

                    lblErrorSummary.Text = LACESConstant.Messages.PORGETPASSEORD_INVALID_MESSAGE.Replace("{0}", AdminEmailWithFormat);
                    lblPostBackMessage.Text = "";
                    isInvalidEmail = true;
                }
                else
                {
                    lblErrorSummary.Text = "";
                    lblPostBackMessage.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Send mail to Provider matches email address with saved email address
    /// </summary>
    /// <param name="smtpClient">SMTL object</param>
    /// <param name="password">Descrypted password of provider</param>
    /// <returns>confirm sending</returns>
    private bool SendEmail(SmtpClient smtpClient,string password)
    {

        try
        {

            MailMessage message = new MailMessage();

            string ToEmailAddress = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(ToEmailAddress.Trim()))
            {
                message.From = new MailAddress(LACESUtilities.GetAdminFromEmail());
                message.To.Add(new MailAddress(ToEmailAddress));

                message.Subject = LACESConstant.FORGETPASSWORD_SUBJECT;
                message.IsBodyHtml = true;
                message.Body = GetSendMessage(ToEmailAddress, password);//Send Email to Friend Message Body
                if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
                {
                    smtpClient.Send(message);//Sending A Mail to the Sender Friend
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Get Email Message Body
    /// </summary>
    /// <param name="adminEmailID">Administrator Email Address</param>
    /// <param name="recipientEmailID">Reciient Email Address</param>
    /// <param name="password">Password of Recipient</param>
    /// <returns></returns>
    private string GetSendMessage(string recipientEmailID, string password)
    {

        string RefrenceURL = String.Empty;
        RefrenceURL = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/Provider/" ))+ LACESConstant.URLS.PROVIDER_LOGIN;
        
        StringBuilder EmailBody = new StringBuilder();
        EmailBody.Append("<html>");
        EmailBody.Append("<head>");
        EmailBody.Append("<title>Title</title>");
        EmailBody.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">");
        EmailBody.Append("</head>");
        EmailBody.Append("<body style=\"font-family: Arial, verdana, Helvetica, sans-serif; font-size:13px;\">");
        string MailBody = @"<div>Someone has request password assistance for the " + LACESConstant.LACES_TEXT + " system at the email address, ‘" + recipientEmailID + "’. The following information has been requested:</div></br></br>" +
                            "<p><div>User Name: " +recipientEmailID+"</div>"+
                            "<div>Password: " +password +"</div></p>"+
                            "<p><div>You can utilize this sign-in information for the Course Provider section of the " + LACESConstant.LACES_TEXT + " system at the following URL: " +
                            "<a href=\"" + RefrenceURL + "\">" + RefrenceURL + "</a>.</p><p> If you require further assistance, please contact the " + LACESConstant.LACES_TEXT + " Administrators at the following email address: "
                            + "<a href=\"mailto:" + TranformEmailAddresses() + "\">" + LACESConstant.LACES_TEXT + " Administrators' Emails</a>.</p></div>";
        EmailBody.Append(MailBody);
        EmailBody.Append("</body>");
        EmailBody.Append("</html>");
        return EmailBody.ToString();
    }

    /// <summary>
    /// Tranform email addresses given in web.conf into proper format
    /// </summary>
    /// <param name="EmailAddresses">Email Address in web.conf</param>
    /// <returns>formated email address to contact</returns>
    private string TranformEmailAddresses()
    {
        string ContactEmailAddressesTo = LACESUtilities.GetAdminToEmail();
        string ContactEmailAddressesCC = LACESUtilities.GetAdminCCEmail();
        string FormatedAddresses = ContactEmailAddressesTo.Replace(",", ";") + "?cc=" + ContactEmailAddressesCC;        
        return FormatedAddresses;
    }
    #endregion
}
