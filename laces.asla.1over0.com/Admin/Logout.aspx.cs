/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: Logout.aspx.cs
 * Purpose/Function: Admin logout functionality
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/23/2008      Create this Page with logout functionality
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
/// Admin logout functionality
/// </summary>
public partial class Admin_Logout : System.Web.UI.Page
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[LACESConstant.SessionKeys.LOGEDIN_ADMIN] != null)
        {
            Session[LACESConstant.SessionKeys.LOGEDIN_ADMIN] = null;
        }
        
        ///Redirect to lLogin page
        Response.Redirect(LACESConstant.URLS.ADMIN_LOGIN);
    }
}
