/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: ProviderSearchResults.aspx.cs
 * Purpose/Function: To display the provider search results
 * Author: Shohel Anwar
 * Version              Author              Date            Reason
 * 1.0              Shohel Anwar       01/22/2008      Create this Page with initial requirements
 * 1.1              Alamgir Hossain    07/05/2008      Work on Enhancement 2    
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
/// Display the provider search results.
/// </summary>
public partial class Admin_ProviderSearchResults : AdminBasePage
{
    string sKeyWords = string.Empty;
    protected IList<Provider1> providers = new List<Provider1>();
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
        

        ProviderDataAccess oblProvDAL = new ProviderDataAccess();

        //if no search keywords for provider found in the session then redirect to the FindProviders.aspx page 
        if (Session[LACESConstant.SessionKeys.SEARCH_PROVIDERS_KEYWORDS] == null)
        {
            Response.Redirect("FindProviders.aspx");            
        }

        lblMessage.Visible = false;

        ///Check Sort Column Query String
        if (Request.QueryString[LACESConstant.QueryString.SORT_COLUMN] != null)
        {
            sSortColumn = Request.QueryString[LACESConstant.QueryString.SORT_COLUMN].ToString();
        }
        else
        {
            sSortColumn = "Organization";
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

        ///Create Header row
        //createHeaderRow(sSortColumn, sSortOrder,iPageIndex);

        ///Adjust left/right content place holder width
        IncreaseLeftContentWidth();


        providers = oblProvDAL.GetPagedProviderSearch(buildSearchCriteria(), iPageIndex, LACESConstant.SEARCH_RESULT_PAGE_SIZE, sSortColumn, sSortOrder, ref totalCount);
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


        ///Show selecterd criteria in right side
        //showSelectedCriteria(totalCount);

        ///Generate PreviousPage NextPage link
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
            sKeyWords = Convert.ToString(Session[LACESConstant.SessionKeys.SEARCH_PROVIDERS_KEYWORDS]);

            ///Split search keword using space
            string[] kewords = sKeyWords.Replace("'", "''").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string searchQuery = "";

            ///Loop for every keyword
            foreach (string keyword in kewords)
            {
                if (searchQuery != "")
                    searchQuery += " OR ";
                searchQuery += "[Organization] LIKE '" + keyword + "%' OR [Organization] LIKE '% " + keyword + "%'";
            }

            ///For blank search
            if (searchQuery == "")
                searchQuery = " 1 = 1 ";

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
        Response.Redirect("FindProviders.aspx?SearchAgain=true");
    }
    #endregion buildSearchCriteria

    //#region showSelectedCriteria
    ///// <summary>
    ///// Show selection criteria at the right side
    ///// </summary>
    ///// <param name="objSearch"></param>
    ///// <param name="totalCount"></param>
    //private void showSelectedCriteria(int totalCount)
    //{
    //    ///Display search keywords
    //    lblName.Text = Server.HtmlEncode(Convert.ToString(Session[LACESConstant.SessionKeys.SEARCH_PROVIDERS_KEYWORDS]));
        
    //    ///Assign total number of result count
    //    lblTotal.Text = totalCount.ToString();
    //}
    //#endregion showSelectedCriteria

    //#region createHeaderRow
    ///// <summary>
    ///// Create header columns content with sort column ,sort order and page index
    ///// </summary>
    ///// <param name="sortColumn">Order by column name</param>
    ///// <param name="sortOrder">Sort order - "asc/desc"</param>
    ///// <param name="pageIndex">Current page index</param>
    //private void createHeaderRow(string sortColumn, string sortOrder, int pageIndex)
    //{
    //    ///Provider Name Header Text
    //    if (sortColumn.ToLower() == "Organization" && sortOrder.ToLower() == "asc")
    //        tdProviderName.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + pageIndex.ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=Organization&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">Provider Name</a>";
    //    else
    //        tdProviderName.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + pageIndex.ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=Organization&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">Provider Name</a>";


    //    ///City Header Text
    //    if (sortColumn.ToLower() == "city" && sortOrder.ToLower() == "asc")
    //        tdCity.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + pageIndex.ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=City&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">City</a>";
    //    else
    //        tdCity.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + pageIndex.ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=City&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">City</a>";


    //    ///State Header Text
    //    if (sortColumn.ToLower() == "stateprovince" && sortOrder.ToLower() == "asc")
    //        tdState.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + pageIndex.ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=StateProvince&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">State</a>";
    //    else
    //        tdState.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + pageIndex.ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=StateProvince&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">State</a>";

    //    ///Active/Inactive Header Text
    //    if (sortColumn.ToLower() == "status" && sortOrder.ToLower() == "asc")
    //        tdActiveInactive.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + pageIndex.ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=Status&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">Active/Inactive</a>";
    //    else
    //        tdActiveInactive.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + pageIndex.ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=Status&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">Active/Inactive</a>";

    //}

    //#endregion createHeaderRow

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
            tdNextPage.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + (PageIndx + 1) + "&" + LACESConstant.QueryString.SORT_COLUMN + "=" + sSortColumn + "&" + LACESConstant.QueryString.SORT_ORDER + "=" + sSortOrder + "\">";
            tdNextPage.InnerHtml += "Next &gt; " + nextPageSize + "</a>";
        }
    }
    #endregion setPreviousNextPage

}
