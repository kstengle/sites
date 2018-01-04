/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: AdminBasePage.cs
 * Purpose/Function: Base functions of Admin section
 * Author: Alamgir Hossain
 * Version              Author              Date            Reason
 * 1.0              Alamgir Hossain      06/19/2008      Create this Page for authentication checking
 --------------------------------------------------------------------------------*/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for VisitorBasePage
/// </summary>
public class VisitorBasePage : System.Web.UI.Page
{
    public VisitorBasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    /// <summary>
    /// Override the onload event handler
    /// </summary>
    /// <param name="e"></param>
    protected override void OnLoad(EventArgs e)
    {
        try
        {
                //load the admin menu bar dynamically
               // HtmlTableCell oHtmlTableCell = (HtmlTableCell)Master.FindControl("tdMenu");
       //         oHtmlTableCell.Controls.Add(LoadControl("~/usercontrols/VisitorMenu.ascx"));
       //
         //       base.OnLoad(e);
        }
        catch (Exception ex)
        {
                
        }
    }
}
