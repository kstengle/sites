/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: FindProviders.aspx.cs
 * Purpose/Function: To get provider search criteria from Admin
 * Author: Shohel Anwar
 * Version              Author              Date            Reason
 * 1.0              Shohel Anwar       01/22/2008      Create this Page with initial requirements
 * 1.1              Alamgir Hossain    07/09/2008      Work on Enhancement 2  
 * 2.0              Md. Kamruzzaman     12/03/2008      Added custom SearchTextBox   
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
/// Get Provider name to be searched
/// </summary>
public partial class Admin_FindProviders : AdminBasePage
{
    #region Page_Load
    /// <summary>
    /// Page Load Event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //this.Master.FindControl()
            this.Form.DefaultButton = btnFindProviders.UniqueID;
            //Get Search criteria from session if "Search Again" button of ProviderSearchResults.aspx page was clicked
            if (Request.QueryString["SearchAgain"] != null && Session[LACESConstant.SessionKeys.SEARCH_PROVIDERS_KEYWORDS] != null)
            {
                ///Get Search criteria from session
                txtKeyword.Text = Convert.ToString(Session[LACESConstant.SessionKeys.SEARCH_PROVIDERS_KEYWORDS]);
            }
            txtKeyword.Focus();
        }
        txtKeyword.Attributes.Add("onclick", "javascript:RemoveDefaultText(this,'Search Here');");
    }
    #endregion Page_Load

    #region btnFindProviders_Click
    /// <summary>
    /// FindProvider button click event handler.
    /// Adds the search keywords to the session and redirected to the ProviderSearchResults.aspx page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFindProviders_Click(object sender, EventArgs e)
    {   
        //Add search criteria into session
        //Session.Add(LACESConstant.SessionKeys.SEARCH_PROVIDERS_KEYWORDS, txtKeyword.Text.Trim());
        //Response.Redirect("ProviderSearchResults.aspx");

        //Add search criteria into session
        //new implementation
        Session.Add(LACESConstant.SessionKeys.SEARCH_APPROVED_PROVIDERS_KEYWORDS, txtKeyword.Text.Trim());
        Response.Redirect("ApprovedProviderSearchResults.aspx");



    }
    #endregion btnFindProviders_Click
}
