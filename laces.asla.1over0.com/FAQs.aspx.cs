using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Net;
public partial class FAQs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string uri = LACESUtilities.GetApplicationConstants("FAQ_ASLA_CMS_Site_URL");
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
