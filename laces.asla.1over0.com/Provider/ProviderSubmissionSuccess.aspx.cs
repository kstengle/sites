/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: ProviderSubmissionSuccess.aspx
 * Purpose/Function: This page is used to show success message after adding/updating provider
 *
 * Author: Md. Kamruzzaman
 * 
 * Version    Author               Date          Reason 
 * 1.0        Md. Kamruzzaman      07/17/2008    Initial development 
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

public partial class Provider_ProviderSubmissionSuccess : VisitorBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //show provider spacific menu in edit mode
        if (Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] != null)
        {
            //get the provider from session
            ApprovedProvider sessionProvider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // Get provider information from Session 

            //Show loggedin provider name
            Label lblProviderName = (Label)Master.FindControl("lblProviderName");
            if (lblProviderName != null)
                lblProviderName.Text = "Signed in as: " + Server.HtmlEncode(sessionProvider.OrganizationName);

            //load the provider menu bar
            HtmlTableCell oHtmlTableCell = (HtmlTableCell)Master.FindControl("tdMenu");
            oHtmlTableCell.Controls.Clear();
            oHtmlTableCell.Controls.Add(LoadControl("~/usercontrols/ProviderMenu.ascx"));

            try
            {
                //change the login status
                ((HtmlTableCell)Master.FindControl("loginTd")).Attributes["class"] = "loggedIn providerloginStatus";
            }
            catch (Exception ex) { }
        }

        //now check weather it is in renew mode
        if (Request.QueryString["Renew"] != null && Request.QueryString["Renew"] == "true")
        {
            lblMsg.Text = "Thank you for renewing your LA CES Approved Provider certification.  If you have any questions, please contact <a href='mailto:laces@asla.org'>laces@asla.org</a>.";
        }
        else if (Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] != null)
        {
            lblMsg.Text = "Thank you for renewing your LA CES Approved Provider certification.  If you have any questions, please contact <a href='mailto:laces@asla.org'>laces@asla.org</a>.";
        }
        else
        {
            lblMsg.Text = "The American Society of Landscape Architects received your application and $295 application fee for the Landscape Architecture Continuing Education System today.<br/><br/>American Society of Landscape Architects<br/>636 I Street, NW<br/>Washington, DC 20001<br/>202-898-2444<br/>FEID - 53-0259019<br/><br/><em>Note: Please print this page for your records and use as a receipt.</em><br /><br />To ensure you receive your login and password and renewal notifications from LA CES, please be sure to add <strong>laces@asla.org</strong> and <strong>lacesapp@asla.org</strong> to your e-mail “safe” list. You can also check your SPAM filter settings. If you have any questions, please contact <a href='mailto:mhanson@asla.org;rleighton@asla.org;?cc=laces@asla.org'> the LA CES administrators</a>.";
        }


    }
}
