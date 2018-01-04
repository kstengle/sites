/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: VisitorMaster.master.cs
 * Purpose/Function: This master page is designed for Visitor section
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/08/2008      Create this Page with initial requirements
 * 1.1              Matiur Rahman       05/08/2008      Changed footer text and removed "Privacy Policy" menu link(Task id 7076)
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

/// <summary>
/// Master Page for Visitor Section
/// </summary>
public partial class PublicMaster : System.Web.UI.MasterPage
{
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] == null && Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] == null)
        {
            ASP.usercontrols_visitormenu_ascx visitorMenu = (ASP.usercontrols_visitormenu_ascx)Page.LoadControl("~/usercontrols/VisitorMenu.ascx");
            uiPhMenu.Controls.Add(visitorMenu);
        }
        else
        {
            string sessionKeyForProvider = LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER;
            ApprovedProvider sessionProvider = (ApprovedProvider)Session[sessionKeyForProvider]; // Get provider information from Session 
            if (sessionProvider.Status == LACESConstant.ProviderStatus.ACTIVE)
            {
                uiPhMenu.Controls.Add(LoadControl("~/usercontrols/ProviderMenu.ascx"));
            }
            //if provider is inactive, inactive provider menu will be shown
            else if (sessionProvider.Status == LACESConstant.ProviderStatus.INACTIVE)
            {
                uiPhMenu.Controls.Add(LoadControl("~/usercontrols/InactiveProviderMenu.ascx"));
            }

        }
    }

    private string m_ChangeFooterImagePath;

    //public string ChangeFooterImagePath
    //{
    //    set { Footer1.RelativePath = value; }
    //}
}
