/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: ProviderUpdatedSuccessfully.aspx
 * Purpose/Function: This page is used to show success message after updating provider
 *
 * Author: Md. Kamruzzaman
 * 
 * Version    Author               Date          Reason 
 * 1.0        Md. Kamruzzaman      07/20/2008    Initial development 
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

public partial class Admin_ProviderUpdatedSuccessfully : AdminBasePage
{
    #region Page load event handler
    /// <summary>
    /// Page load event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        lblMsg.Text = "Provider information updated successfully. Provider's current status is '" + Request.QueryString["Status"] + "'.";
    }
    #endregion

}
