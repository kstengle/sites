/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: SingleColumnMaster.master.cs
 * Purpose/Function: This master page is designed for Single Column
 * Author: Alamgir Hossain
 * Version              Author              Date            Reason
 * 1.0              Alamgir Hossain       06/15/2008      Create this Page with initial requirements
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
/// Master Page for Single Column Section
/// </summary>
public partial class SingleColumnMaster : System.Web.UI.MasterPage
{
    protected bool LoadJavascript = true;

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.TemplateControl.AppRelativeVirtualPath.ToLower().IndexOf("addattendee.aspx")  <1)
        {

            if (Session[LACESConstant.SessionKeys.LOGEDIN_ADMIN] == null || Session[LACESConstant.SessionKeys.LOGEDIN_ADMIN].ToString() != "Yes")
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
        }
        //for aboutus and ApprovedProviderGuidelines page do not include javascript files
        //otherwise it will produce error
        if (Request.CurrentExecutionFilePath.ToUpper().Contains("/LACES/ABOUTUS.ASPX")
            || Request.CurrentExecutionFilePath.ToUpper().Contains("/LACES/APPROVEDPROVIDERGUIDELINES.ASPX"))
        {
            LoadJavascript = false;
        }
    }
}
