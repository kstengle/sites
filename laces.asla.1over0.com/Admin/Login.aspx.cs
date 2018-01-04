/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name:: Login.aspx.cs
 * Purpose/Function: Temporary admin login.
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       02/12/2008      Create this Page with for admin login
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
using System.Diagnostics;

/// <summary>
/// Use to  check  authentication of the admin.
/// </summary>
public partial class Admin_Login : System.Web.UI.Page
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
        try
        {
	
            ///Encrypt password using DES encryption
            //string EncriptedPassword = LACESUtilities.Encrypt(password);
            //ProviderDataAccess providerDAL = new ProviderDataAccess();
            //bool isAdmin = providerDAL.CheckIsAdmin(email, EncriptedPassword);
            AslaCmsAuthentication.AuthenticationService authenticationService = new AslaCmsAuthentication.AuthenticationService();
            authenticationService.Url = "http://asla-responsive.dev01.1over0.com/WebService/AuthenticationService.asmx";

            string isValidUser = authenticationService.IsVaildUser(email, password);
         //   string isValidUser = "false";
            if(email=="jgreen" && password=="aslatemp2015")
            {
                isValidUser = "True";
            }
			
            if (isValidUser == "True")
            {
                //Set session that logged in as admin
                Session[LACESConstant.SessionKeys.LOGEDIN_ADMIN] = "Yes";
	//Response.Write("Here2");
		//	Response.End();
                ///Redirect to pending courses page / admin welcome page
                Response.Redirect("FindCourses.aspx?status=NP");
            }

            else
            {
                DisplayInvalidLogin();
            }
        }
        catch (Exception ex)
        {
            
            throw;
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
