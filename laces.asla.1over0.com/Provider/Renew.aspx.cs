/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: Renew.aspx
 * Purpose/Function: This page is used to rewnew approved provider
 *
 * Author: Md. Kamruzzaman
 * 
 * Version    Author               Date          Reason 
 * 1.0        Md. Kamruzzaman      07/29/2008    Initial development 
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

public partial class Visitor_Renew : VisitorBasePage
{
    #region Page Load Event Handler
    /// <summary>
    ///  Page Load Event Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string sessionKeyForProvider = LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER;

        //if current user is inactive
        if (Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] != null)
        {
            sessionKeyForProvider = LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER;
        }

        //if current user is inactive
        if (Session[sessionKeyForProvider] == null)
        {
            Response.Redirect("Login.aspx");
        }
        //show provider spacific menu in edit mode
        else
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
    }
    #endregion

    #region Navigation Buttons

    /// <summary>
    /// This function fires on BtnSubmit button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (RadioHasChange.SelectedValue == "1")
        {
            Response.Redirect("ProviderApplication.aspx?HasChange=false");
        }
        else
        {
            Response.Redirect("ProviderApplication.aspx?HasChange=true");
        }
    }

    #endregion

}
