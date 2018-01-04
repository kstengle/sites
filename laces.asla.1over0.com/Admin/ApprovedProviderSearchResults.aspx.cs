/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: ApprovedProviderSearchResults.aspx
 * Purpose/Function: To display the approved provider search results
 *
 * Author: Md. Kamruzzaman
 * Version    Author               Date          Reason 
 * 1.0        Md. Kamruzzaman      07/09/2008    Initial development 
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
using System.Collections.Generic;
using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;


/// <summary>
/// Display the approved provider search results.
/// </summary>
public partial class Admin_ApprovedProviderSearchResults : AdminBasePage
{
    string sKeyWords = string.Empty;
    protected IList<ApprovedProvider> providers = new List<ApprovedProvider>();
    string sSortColumn = string.Empty;
    string sSortOrder = string.Empty;

    #region Page Load
    /// <summary>
    /// Page Load event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        int iPageIndex = 0;
        int totalCount = 0;

        ApprovedProviderDataAccess oblProvDAL = new ApprovedProviderDataAccess();

        ////if no search keywords for approved provider found in the session then redirect to the FindProviders.aspx page 
        //if (Session[LACESConstant.SessionKeys.SEARCH_APPROVED_PROVIDERS_KEYWORDS] == null)
        //{
        //    Response.Redirect("FindApprovedProviders.aspx");            
        //}

        lblMessage.Visible = false;

        ///Check Sort Column Query String
        if (Request.QueryString[LACESConstant.QueryString.SORT_COLUMN] != null)
        {
            sSortColumn = Request.QueryString[LACESConstant.QueryString.SORT_COLUMN].ToString();
        }
        else
        {
            sSortColumn = "OrganizationName";
        }

        ///Check Sort Order Query String
        if (Request.QueryString[LACESConstant.QueryString.SORT_ORDER] != null)
        {
            sSortOrder = Request.QueryString[LACESConstant.QueryString.SORT_ORDER].ToString();
        }
        else
        {
            sSortOrder = "asc";
        }

        ///Check Current Page Index Query String
        if (Request.QueryString[LACESConstant.QueryString.PAGE_INDEX] != null)
        {
            iPageIndex = Convert.ToInt32(Request.QueryString[LACESConstant.QueryString.PAGE_INDEX].ToString());
        }
        else
        {
            iPageIndex = 0;
        }

        //Create Header row
        //createHeaderRow(sSortColumn, sSortOrder,iPageIndex);

        //Adjust left/right content place holder width
        IncreaseLeftContentWidth();


        providers = oblProvDAL.GetPagedApprovedProviderSearch(buildSearchCriteria(), iPageIndex, LACESConstant.SEARCH_RESULT_PAGE_SIZE, sSortColumn, sSortOrder, ref totalCount);
        string resultsNumber = "";
        if (iPageIndex == 0)
        {
            resultsNumber = "1 -";
        }
        else
        {
            resultsNumber = ((iPageIndex * 10) + 1).ToString() + " - ";
        }
        int pageMax = ((iPageIndex + 1) * LACESConstant.SEARCH_RESULT_PAGE_SIZE);
        if (pageMax <= totalCount)
        {
            resultsNumber += ((iPageIndex + 1) * 10).ToString();
        }
        else
        {
            resultsNumber += totalCount.ToString();
        }
        uiLitResultsMessage.Text = "Search Results: " + resultsNumber + " out of " + totalCount.ToString() + " results"; 
        //Show selecterd criteria in right side
        //showSelectedCriteria(totalCount);

        //Generate PreviousPage NextPage link
        setPreviousNextPage(iPageIndex, totalCount);
    }
    #endregion Page Load

    #region buildSearchCriteria
    /// <summary>
    /// Generate Search criteria from the keywords
    /// </summary>
    /// <returns>Search query where condition</returns>
    private string buildSearchCriteria()
    {
        string searchQuery = "";

        //search by status
        if (Request.QueryString[LACESConstant.QueryString.PROVIDER_STATUS] != null)
        {
            string sStatus = Request.QueryString[LACESConstant.QueryString.PROVIDER_STATUS].ToString();
            if (sStatus == LACESConstant.ProviderStatus.ACTIVE || sStatus == LACESConstant.ProviderStatus.PENDING)
            {
                searchQuery = "[Status]='" + sStatus +"'";// AND (" + searchQuery + ")";
                PageHeader.Text = char.ToUpper(sStatus[0]) + sStatus.Substring(1) + " Providers";
            }
        }
        else//search by criteria
        {
            sKeyWords = Convert.ToString(Session[LACESConstant.SessionKeys.SEARCH_APPROVED_PROVIDERS_KEYWORDS]);

            ///Split search keword using space
            string[] kewords = sKeyWords.Replace("'", "''").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            //Loop for every keyword
            foreach (string keyword in kewords)
            {
                if (searchQuery != "")
                    searchQuery += " OR ";
                searchQuery += "[OrganizationName] LIKE '" + keyword + "%' OR [OrganizationName] LIKE '% " + keyword + "%'";
            }

            //For blank search
            if (searchQuery == "")
                searchQuery = " 1 = 1 ";
        }


        return searchQuery;
    }
    #endregion buildSearchCriteria

    #region btnSearchAgain_Click
    /// <summary>
        /// Search again button click event handler. Redirect to the FindProviders.aspx page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchAgain_Click(object sender, EventArgs e)
    {
        Response.Redirect("FindApprovedProviders.aspx?SearchAgain=true");
    }
    #endregion buildSearchCriteria

    #region setPreviousNextPage
    /// <summary>
    /// Set pagination text and link
    /// </summary>
    /// <param name="PageIndx">Current page index</param>
    /// <param name="totalCount">Total number of result count</param>
    private void setPreviousNextPage(int PageIndx, int totalCount)
    {
        ///For previous page link
        if (PageIndx > 0)
        {
            if (Request.QueryString["Status"] != null)
                tdPreviousPage.InnerHtml = "<a href=\"?Status=" + Request.QueryString["Status"] + "&" + LACESConstant.QueryString.PAGE_INDEX + "=" + (PageIndx - 1) + "&" + LACESConstant.QueryString.SORT_COLUMN + "=" + sSortColumn + "&" + LACESConstant.QueryString.SORT_ORDER + "=" + sSortOrder + "\">";
            else
                tdPreviousPage.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + (PageIndx - 1) + "&" + LACESConstant.QueryString.SORT_COLUMN + "=" + sSortColumn + "&" + LACESConstant.QueryString.SORT_ORDER + "=" + sSortOrder + "\">";
            
            tdPreviousPage.InnerHtml += "&lt; Previous " + LACESConstant.SEARCH_RESULT_PAGE_SIZE + "</a>";
        }

        ///For next page link
        int remainingRecords = totalCount - ((PageIndx + 1) * LACESConstant.SEARCH_RESULT_PAGE_SIZE);
        if (remainingRecords > 0)
        {
            int nextPageSize = LACESConstant.SEARCH_RESULT_PAGE_SIZE;
            if (nextPageSize > remainingRecords)
                nextPageSize = remainingRecords;

            if(Request.QueryString["Status"] != null)
                tdNextPage.InnerHtml = "<a href=\"?Status=" + Request.QueryString["Status"] + "&" + LACESConstant.QueryString.PAGE_INDEX + "=" + (PageIndx + 1) + "&" + LACESConstant.QueryString.SORT_COLUMN + "=" + sSortColumn + "&" + LACESConstant.QueryString.SORT_ORDER + "=" + sSortOrder + "\">";
            else
                tdNextPage.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + (PageIndx + 1) + "&" + LACESConstant.QueryString.SORT_COLUMN + "=" + sSortColumn + "&" + LACESConstant.QueryString.SORT_ORDER + "=" + sSortOrder + "\">";
            
            tdNextPage.InnerHtml += "Next &gt; " + nextPageSize + "</a>";
        }
    }
    #endregion setPreviousNextPage

}
