using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Provider_Tools : ProviderBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string uri = LACESUtilities.GetApplicationConstants("ProviderTools_URL");
        uiLitContent.Text = GetExternalWebData(uri);
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