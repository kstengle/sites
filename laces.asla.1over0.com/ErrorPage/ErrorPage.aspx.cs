/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: ErrorPage.aspx
 * Purpose/Function: ErrorPage class is used to  handle all unknown common errors of LACES system
 * 
 * 
 * Author: Tarek Jubaer
 * Version                 Author            Date         Reason 
 * 1.0                  Tarek Jubaer        01/24/2008   Page Creation (Common Error page. Task: 5476)
 
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
/// <summary>
/// ErrorPage class is used to  handle all unknown common errors of LACES system
/// </summary>
public partial class ErrorPage_ErrorPage : System.Web.UI.Page
{
    #region Page Load Event Hadler
    /// <summary>
    /// Page Load Event Hadler
    /// Replaces the Menu of provider master page.
    /// Look up for the error type
    /// Display file not found error.
    /// Display provider, admin or visitor error.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {        
        try
        {
            //Find menu control of master page
            HtmlTableCell htmlTableRow = (HtmlTableCell)Master.FindControl("tdMenu");
            //Replace menus with blank image
            htmlTableRow.InnerHtml = "<img width=\"16\" height=\"30\" alt=\"\" src=\"../images/shim.gif\" /> ";

            if (Request.QueryString["FileNotFound"] != null) //Look up for file not found error and display appropriate message
                lblErrorMessage.Text = "Sorry. Your requested file is not available.";
            else if (Request.QueryString["aspxerrorpath"] != null) //Look up for unknown error
            {
                string[] errorURL = Request.QueryString["aspxerrorpath"].ToString().Split('/'); //slip aspxerrorpath query string
                for (int i = 0; i < errorURL.Length; i++)   //For all string in the query string
                {
                    if (errorURL[i].ToLower() == "admin")   //if admin found then display error for admin system
                    {
                        lblErrorMessage.Text = "The admin system encountered some problems. Please try again later. If the problem continues contact with the system administrator.";
                        break;
                    }
                    else if (errorURL[i] == "provider")     //if provider found then display error for provider system
                    {
                        lblErrorMessage.Text = "The provider system encountered some problems. Please try again later. If the problem continues contact with the system administrator.";
                        break;
                    }
                    else if (errorURL[i] == "visitor")      //if visitor found then display error for visitor system
                    {
                        lblErrorMessage.Text = "The visitor system encountered some problems. Please try again later. If the problem continues contact with the system administrator.";
                        break;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}
