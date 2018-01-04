/*------------------------------------------------------------------------------
 * Project Name: LA CES™
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: Admin_ApprovedProviderGuidelines.aspx.cs
 * Purpose/Function: Show ASLA CMS ApprovedProviderGuidelines Content
 * Author: Alamgir Hossain 
 * Version              Author              Date            Reason
 * 1.0              Alamgir Hossain       07/23/2008      Create this Page with static texts
 * 1.1              Humayun Ahmed         07/30/2008      Modified for giving absolute path to resources  
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
using System.IO;

/// <summary>
/// Show ASLA CMS ApprovedProviderGuidelines Content
/// </summary>
public partial class Admin_ApprovedProviderGuidelines : AdminBasePage
{

    #region Page_Load

    /// <summary>
    /// Call every time when the page load occur
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string uri = LACESUtilities.GetApplicationConstants("LACES_App_Pro_Guide_ASLA_CMS_Site_URL");
        appProviderLACESDiv.InnerHtml = GetExternalWebData(uri);
    }

    #endregion
    
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

            /*Start of giving absolute path section*/
            string url = resp.ResponseUri.ToString();

            string rootPath = url.Substring(0, url.IndexOf(resp.ResponseUri.AbsolutePath));

            buffer = SetAbsolutPathForResources(buffer, rootPath);

            /*End of giving absolute path section*/


            return buffer;

        }
        catch (Exception ex)
        {
        }
        return null;
    }
    /// <summary>
    /// Sets absolut path for resources
    /// </summary>
    /// <param name="buffer">Response string</param>
    /// <param name="rootPath">root path of the site</param>
    /// <returns>Response string</returns>
    private static string SetAbsolutPathForResources(string buffer, string rootPath)
    {
        rootPath += "/";

        string src = "src=\"/";
        string formattedSrc = "src=\"" + rootPath;
        buffer = buffer.Replace(src, formattedSrc);//replace src value from relative path to absolute path

        string href = "href=\"/";
        string formattedHref = "href=\"" + rootPath;
        buffer = buffer.Replace(href, formattedHref);//replace href value from relative path to absolute path

        return buffer;
    }

    #endregion
}
