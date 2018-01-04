/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: Logout.aspx.cs
 * Purpose/Function: Provider logout functionality
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/13/2008      Create this Page with logout functionality
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
/// Provider logout functionality
/// </summary>
public partial class Provider_Logout : System.Web.UI.Page
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //log out active provider
        if (Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] != null)
        {
            Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] = null;
        }

        //log out inactive provider
        if (Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] != null)
        {
            Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] = null;
        }
        
        ///Redirect to lLogin page
        Response.Redirect(LACESConstant.URLS.HOME_PAGE);
    }
}
