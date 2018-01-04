/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: ParticipantSearchResult.aspx.cs
 * Purpose/Function: To display participant search result in admin section
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/22/2008      Create this Page with initial requirements
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
/// Display Participant search result of found search criteria into the session 
/// otherwise redirect to FindACourse page to select criteria
/// </summary>
public partial class Admin_ParticipantSearchResult : AdminBasePage
{
    protected IList<SearchParticipant> participants = new List<SearchParticipant>();
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchParticipantDataAccess objSearchParticipantDAL = new SearchParticipantDataAccess();

        ///If search criteria not found in session redirect for select criteria again
        if (Session[LACESConstant.SessionKeys.SEARCH_PARTICIPANT_CRITERIA] == null)
        {
            Response.Redirect("FindParticipant.aspx");
        }

        lblMessage.Visible = false;
                
        ///Get Search criteria from session
        SearchParticipant objSearchCriteria = (SearchParticipant)Session[LACESConstant.SessionKeys.SEARCH_PARTICIPANT_CRITERIA];

        ///Check Sort Column Query String
        if (Request.QueryString[LACESConstant.QueryString.SORT_COLUMN] != null)
        {
            objSearchCriteria.SortColumn = Request.QueryString[LACESConstant.QueryString.SORT_COLUMN].ToString();            
        }

        ///Check Sort Order Query String
        if (Request.QueryString[LACESConstant.QueryString.SORT_ORDER] != null)
        {
            objSearchCriteria.SortOrder = Request.QueryString[LACESConstant.QueryString.SORT_ORDER].ToString();            
        }

        ///Check Current Page Index Query String
        if (Request.QueryString[LACESConstant.QueryString.PAGE_INDEX] != null)
        {
            objSearchCriteria.PageIndex = Convert.ToInt32(Request.QueryString[LACESConstant.QueryString.PAGE_INDEX].ToString());
        }

        ///Create Header row
        createHeaderRow(objSearchCriteria.SortColumn, objSearchCriteria.SortOrder);

        ///Adjust left/right content place holder width
        IncreaseLeftContentWidth();

        ///Generate ORDER BY value
        string orderBy = "";
        orderBy = objSearchCriteria.SortColumn + " " + objSearchCriteria.SortOrder;
        
        int totalCount = 0;
        
        ///Get Search result by query
        participants = objSearchParticipantDAL.GetPagedParticipantBySearch(objSearchCriteria.WhereCondition, objSearchCriteria.PageIndex,
            LACESConstant.SEARCH_RESULT_PAGE_SIZE, orderBy, ref totalCount);

        ///Show selecterd criteria in right side
        showSelectedCriteria(objSearchCriteria, totalCount);

        ///Generate PreviousPage NextPage link
        setPreviousNextPage(objSearchCriteria, totalCount);

        ///Store Changed result in the session
        Session[LACESConstant.SessionKeys.SEARCH_PARTICIPANT_CRITERIA] = objSearchCriteria;
    }

    /// <summary>
    /// Redirect to choose search criteria again
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchAgain_Click(object sender, EventArgs e)
    {
        Response.Redirect("FindParticipant.aspx?SearchAgain=true");
    }

    /// <summary>
    /// Show selection criteria at the right side
    /// </summary>
    /// <param name="objSearch"></param>
    /// <param name="totalCount"></param>
    private void showSelectedCriteria(SearchParticipant objSearch, int totalCount)
    {
        ///Display selected search criteria
        lblName.Text = Server.HtmlEncode(objSearch.FirstName);
        if (lblName.Text != string.Empty)
            lblName.Text += " ";
        lblName.Text += Server.HtmlEncode(objSearch.LastName);

        lblASLA.Text = Server.HtmlEncode(objSearch.ASLANumber);
        lblCLARB.Text = Server.HtmlEncode(objSearch.CLARBNumber);
        lblFL.Text = Server.HtmlEncode(objSearch.FLNumber);

        ///Assign total number of result count
        lblTotal.Text = totalCount.ToString();
    }

    /// <summary>
    /// Create header columns content with sort column and sort order
    /// </summary>
    /// <param name="sortColumn"></param>
    /// <param name="sortOrder"></param>
    private void createHeaderRow(string sortColumn, string sortOrder)
    {
        ///Title Header Text
        if (sortColumn.ToLower() == "lastname" && sortOrder.ToLower() == "asc")
            tdLastName.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=LastName&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">Last</a>";
        else
            tdLastName.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=LastName&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">Last</a>";

        ///Date Header Text
        if (sortColumn.ToLower() == "firstname" && sortOrder.ToLower() == "asc")
            tdFirstName.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=FirstName&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">First</a>";
        else
            tdFirstName.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=FirstName&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">First</a>";

        ///Provider Header Text
        if (sortColumn.ToLower() == "asla" && sortOrder.ToLower() == "asc")
            tdASLA.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=ASLA&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">ASLA</a>";
        else
            tdASLA.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=ASLA&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">ASLA</a>";

        ///Location Header Text
        if (sortColumn.ToLower() == "clarb" && sortOrder.ToLower() == "asc")
            tdCLARB.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=CLARB&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">CLARB</a>";
        else
            tdCLARB.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=CLARB&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">CLARB</a>";

        ///Subjects Header Text
        if (sortColumn.ToLower() == "fl" && sortOrder.ToLower() == "asc")
            tdFL.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=FL&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">FL</a>";
        else
            tdFL.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=FL&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">FL</a>";

        ///Status Header Text
        if (sortColumn.ToLower() == "courses" && sortOrder.ToLower() == "asc")
            tdCourses.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=Courses&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">Courses</a>";
        else
            tdCourses.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.SORT_COLUMN + "=Courses&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">Courses</a>";

    }

    /// <summary>
    /// Set Previous Page, Next Page text and link
    /// </summary>
    /// <param name="objSearch"></param>
    /// <param name="totalCount"></param>
    private void setPreviousNextPage(SearchParticipant objSearch, int totalCount)
    {
        ///For previous page link
        if (objSearch.PageIndex > 0)
        {
            tdPreviousPage.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + (objSearch.PageIndex - 1) + "\">";
            tdPreviousPage.InnerHtml += "&lt; Previous " + LACESConstant.SEARCH_RESULT_PAGE_SIZE + "</a>";
        }

        ///For next page link
        int remainingRecords = totalCount - ((objSearch.PageIndex + 1) * LACESConstant.SEARCH_RESULT_PAGE_SIZE);
        if (remainingRecords > 0)
        {
            int nextPageSize = LACESConstant.SEARCH_RESULT_PAGE_SIZE;
            if (nextPageSize > remainingRecords)
                nextPageSize = remainingRecords;
            tdNextPage.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + (objSearch.PageIndex + 1) + "\">";
            tdNextPage.InnerHtml += "Next &gt; " + nextPageSize + "</a>";
        }
    }
}
