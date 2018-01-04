/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: Visitor_ApprovedProviderDetails.aspx.cs
 * Purpose/Function: Visitor approved provider details
 * Author: Alamgir Hossain
 * 
 * Version          Author                  Date            Reason
 * 1.0              Alamgir Hossain         07/16/2008      Create this Page with initial requirements
 * 1.1              Md. kamruzzaman         07/30/2009      task 8108
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
using Pantheon.ASLA.LACES.DataAccess;
using Pantheon.ASLA.LACES.Common;

public partial class Visitor_ApprovedProviderDetails : System.Web.UI.Page
{
    public ApprovedProvider currentProvider = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //get id from query string
            if (Request.QueryString[LACESConstant.QueryString.PROVIDER_ID] != null)
            {
                int providerID = 0;

                int.TryParse(Request.QueryString[LACESConstant.QueryString.PROVIDER_ID].ToString(), out providerID);

                if (providerID > 0)
                {
                    ApprovedProviderDataAccess providerDA = new ApprovedProviderDataAccess();
                    currentProvider = providerDA.GetApprovedProviderByID(providerID); // Get provider by id

                    //if provider exists
                    if (currentProvider != null) 
                    {
                        PreparePreviewData(currentProvider);
                    }
                }
            }
        }
    }

    #region Preview Data
    /// <summary>
    /// Prepare data to dislay in preview mode
    /// </summary>
    private void PreparePreviewData(ApprovedProvider currentProvider)
    {
        //changed as per task 8108
        PageHeader.InnerText = currentProvider.OrganizationName;
        //pvOrganizationName.Text = currentProvider.OrganizationName;

        if(currentProvider.OrganizationStreetAddress.Length>0)
        {
            pvOrganizationStreetAddress.Text = currentProvider.OrganizationStreetAddress;
        }
        else
        {
            uiPhOrganizationStreetAddress.Visible = false;
        }
        if (currentProvider.OrganizationCity.Length > 0)
        {
            pvOrganizationCity.Text = currentProvider.OrganizationCity;
        }
        else
        {
            uiPhOrganizationCity.Visible = false;
        }
        if (currentProvider.OrganizationState.Length > 0)
        {
            pvOrganizationState.Text = currentProvider.OrganizationState;
        }
        else
        {
            uiPhOrganizationState.Visible = false;
        }
        if (currentProvider.OrganizationZip.Length > 0)
        {
            pvOrganizationZip.Text = currentProvider.OrganizationZip;
        }
        else
        {
            uiPhOrganizationZip.Visible = false;
        }
        if (currentProvider.OrganizationCountry.Length > 0)
        {
            pvOrganizationCountry.Text = currentProvider.OrganizationCountry;
        }
        else
        {
            uiPhOrganizationCountry.Visible = false;
        }
        if (currentProvider.OrganizationPhone.Length > 0)
        {
            pvOrganizationPhone.Text = currentProvider.OrganizationPhone;
        }
        else
        {
            uiPhOrganizationPhone.Visible = false;
        }
        if (currentProvider.OrganizationFax.Length > 0)
        {
            pvOrganizationFax.Text = currentProvider.OrganizationFax;
        }
        else
        {
            uiPhOrganizationFax.Visible = false;
        }
        if (currentProvider.OrganizationWebSite.Length > 0)
        {
            pvOrganizationWebSite.Text = currentProvider.OrganizationWebSite;
        }
        else
        {
            uiPhOrganizationWebSite.Visible = false;
        }
    }
    #endregion
}
