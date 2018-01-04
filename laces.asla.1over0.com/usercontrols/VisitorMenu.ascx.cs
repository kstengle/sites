/*------------------------------------------------------------------------------
 * Project Name: LA CES™
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: usercontrols_VisitorMenu.ascx.cs
 * Purpose/Function: Top navigation menu for visitor.
 * Author: Alamgir Hossain
 * Version              Author              Date            Reason
 * 1.0              Alamgir Hossain       06/16/2008      Create and initial development
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
/// Top navigation menu for visitor.
/// </summary>
public partial class usercontrols_VisitorMenu : System.Web.UI.UserControl
{

    #region Page Load

    /// <summary>
    /// Call every time when the page will load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //AboutLACESLink.InnerHtml = "<a href='" + LACESUtilities.GetApplicationConstants("AboutLACES_ASLA_CMS_Site_URL") + "'>" + LACESConstant.LACES_TEXT + "</a>";
        //AboutLACESLink.InnerHtml = "<a href='/AboutUs.aspx'>About LA CES</a>";
        
    }

    #endregion
}
