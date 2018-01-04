/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: usercontrols_Footer.aspx.cs
 * Purpose/Function: User control for footer
 * Author: Almgir Hossain
 * Version              Author              Date            Reason
 * 1.0              Almgir Hossain       06/23/2008      Create this Page with initial requirements
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
/// User control for footer
/// </summary>
public partial class usercontrols_Footer : System.Web.UI.UserControl
{
    #region Page Load

    /// <summary>
    /// Call every time when page load
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        uiLitCopyrightYear.Text = System.DateTime.Now.Year.ToString();
    }

    #endregion


    #region RelativePath

    private string m_RelativePath = "../";

    /// <summary>
    /// Get or set the relative path of the footer images
    /// </summary>
    public string RelativePath
    {
        get { return m_RelativePath; }
        set { m_RelativePath = value; }
    }


    #endregion
}
