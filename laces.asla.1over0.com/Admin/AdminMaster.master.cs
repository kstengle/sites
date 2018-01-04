/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: AdminMaster.master.cs
 * Purpose/Function: This master page is designed for Adminstrator section
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/09/2008      Create this Page with initial requirements
 * 1.1              Shohel Anwar        01/22/2008      Updated the link for find provider menu
 * 1.2              Matiur Rahman       05/08/2008      Changed footer text (Task id 7076)
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
/// Master Page for Admin Section
/// </summary>
public partial class AdminMaster : System.Web.UI.MasterPage
{
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[LACESConstant.SessionKeys.LOGEDIN_ADMIN] == null || Session[LACESConstant.SessionKeys.LOGEDIN_ADMIN].ToString() != "Yes")
        {
            
        }
        else
        {
            uiPhMenu.Controls.Add(LoadControl("~/usercontrols/AdminMenu.ascx"));
        }
    }
}
