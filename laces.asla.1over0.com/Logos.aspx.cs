using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Diagnostics;
using Pantheon.ASLA.LACES.Common;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Logos : System.Web.UI.Page
{
    string sessionKeyForProvider = LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER;
    protected void Page_Load(object sender, EventArgs e)
    {
        string uri = LACESUtilities.GetApplicationConstants("logosLACES_ASLA_CMS_Site_URL");
        string relativeURL = "\"" + LACESUtilities.GetApplicationConstants("ImagesASLARelative");
        string absoluteURL = "\"" + LACESUtilities.GetApplicationConstants("ImagesASLAAbsolute");
        logosLACESDiv.InnerHtml = GetExternalWebData(uri).Replace(relativeURL, absoluteURL);
        if (Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] != null)
        {
            sessionKeyForProvider = LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER;
        }

        if (Session[sessionKeyForProvider] != null)
        {
            //get the provider from session
            ApprovedProvider sessionProvider = (ApprovedProvider)Session[sessionKeyForProvider]; // Get provider information from Session 

            //Show loggedin provider name
            Label lblProviderName = (Label)Master.FindControl("lblProviderName");
            if (lblProviderName != null)
                lblProviderName.Text = "Signed in as: " + Server.HtmlEncode(sessionProvider.OrganizationName);


            //load the provider menu bar
            HtmlTableCell oHtmlTableCell = (HtmlTableCell)Master.FindControl("tdMenu");
            oHtmlTableCell.Controls.Clear();

            //if provider is active,provider menu will be shown
            if (sessionProvider.Status == LACESConstant.ProviderStatus.ACTIVE)
            {
                oHtmlTableCell.Controls.Add(LoadControl("~/usercontrols/ProviderMenu.ascx"));
            }
            //if provider is inactive, inactive provider menu will be shown
            else if (sessionProvider.Status == LACESConstant.ProviderStatus.INACTIVE)
            {
                oHtmlTableCell.Controls.Add(LoadControl("~/usercontrols/InactiveProviderMenu.ascx"));
            }

            try
            {
                //change the login status
                ((HtmlTableCell)Master.FindControl("loginTd")).Attributes["class"] = "loggedIn providerloginStatus";
            }
            catch (Exception ex) { }

        }
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
