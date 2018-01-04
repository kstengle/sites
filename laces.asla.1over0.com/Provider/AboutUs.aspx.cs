/*------------------------------------------------------------------------------
 * Project Name: LA CES™
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: Provider_AboutUs.aspx.cs
 * Purpose/Function: Display the message about ASLA > LA CES™
 * Author: Alamgir Hossain
 * Version              Author              Date            Reason
 * 1.0              Alamgir Hossain       07/29/2008      Create this Page with static texts
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
using System.Net;
using System.Diagnostics;
using System.IO;

/// <summary>
/// Display the message about ASLA > LA CES™ system
/// </summary>
public partial class Provider_AboutUs :  ProviderBasePage
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string uri = LACESUtilities.GetApplicationConstants("AboutLACES_ASLA_CMS_Site_URL");
        aboutLACESDiv.InnerHtml = GetExternalWebData(uri);
    }

    #region GetExternalWebData

    /// <summary>
    /// Get a external url and return the url data
    /// </summary>
    /// <param name="externalUrl">The external url</param>
    /// <returns>String representation of the data</returns>
    public static string GetExternalWebData(string externalUrl)
    {
        try
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(externalUrl);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream st = resp.GetResponseStream();
            StreamReader sr = new StreamReader(st);
            string buffer = sr.ReadToEnd();
            sr.Close();
            st.Close();

            return buffer;

        }
        catch (Exception ex)
        {
        }
        return null;
    }

    #endregion

    
}
