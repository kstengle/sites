/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: ProviderLogin.aspx.cs
 * Purpose/Function: Provider login.
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
using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;

/// <summary>
/// Use to  check  authentication of the provider.Matching provider Email address and Password
/// </summary>
public partial class Provider_ProviderLogin : System.Web.UI.Page
{

    #region Member variable
    protected bool isInvalidLogin = false;
    #endregion

    /// <summary>
    /// Page Load event handler.
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">Event Argument</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch(Exception ex)
        {
            throw ex;
        }

    }
    /// <summary>
    /// Sign In Button handler. 
    /// Use to Match Email Address and password with existing email address and password 
    /// and get Provider information from database
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event Argument</param>
    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        try
        {
            string Email = txtEmail.Text;
            string Password = txtPassword.Text;
            if (String.IsNullOrEmpty(Email))
            {
                DisplayInvalidLogin();
            }
            else
            {
                ///Checking the  Email And Password Match Existing Provider values and save provider details in session
                SignInUser(Email, Password);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// <summary>
    /// Checking the  Email And Password Match Existing Provider values and return provider details
    /// </summary>
    /// <param name="email">Email of Provider</param>
    /// <param name="password">password</param>
    private void SignInUser(string email, string password)
    {
        ///Encrypt password using DES encryption
        string EncriptedPassword = LACESUtilities.Encrypt(password);
        ApprovedProviderDataAccess providerDAL = new ApprovedProviderDataAccess();

        //inactivate all expired providers
        providerDAL.InactivateApprovedProviders(0, DateTime.Now);

        ApprovedProvider provider = providerDAL.GetByEmailandPassword(email, EncriptedPassword);

        

        if (provider != null && provider.ID > 0 && provider.Status== LACESConstant.ProviderStatus.ACTIVE)
        {
            //Set Active Provider Details to Session
            Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] = provider;

            //Set Inactive Provider Details to null
            Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] = null;

            ///Redirect to CourseListing page of Provider
            Response.Redirect("AddCourses.aspx");
        }
        else if (provider != null && provider.ID > 0 && provider.Status == LACESConstant.ProviderStatus.INACTIVE)
        {
            //Set Inactive Provider Details to Session
            Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] = provider;

            //Set Active Provider Details to null
            Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] = null;

            ///Redirect to CourseListing page of Provider
            Response.Redirect("Renew.aspx");
        }
        else
        {
            DisplayInvalidLogin();
        }
    }

    private void DisplayInvalidLogin()
    {
          if (IsPostBack == true)
          {  //If Provider Emal addres and password does not match invaid login message is displayed
              lblErrorSummary.Text = LACESConstant.Messages.INVALID_LOGIN;
              txtEmail.Focus();
              isInvalidLogin = true;
                          
          }
          else
          {
              lblErrorSummary.Text = "";
          }
    }
}
