/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: CourseResult.aspx.cs
 * Purpose/Function: To display course search result in visitor section
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0                  Matiur Rahman       01/22/2008      Create this Page with initial requirements
 * 1.1                  Alamgir Hossain     07/09/2008      Work on Enhancement 2    
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
using System.Diagnostics;
using System.Globalization;
/// <summary>
/// Display course search result of found search criteria into the session 
/// otherwise redirect to FindACourse page to select criteria
/// </summary>
public partial class Visitor_CourseResult : System.Web.UI.Page
{

    //Using to store course search result
    protected IList<Course> courseResult = new List<Course>();
    
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CourseDataAccess objCourseDAL = new CourseDataAccess();


            lblMessage.Visible = false;

            ///Get Search criteria from session
            SearchCourse objSearchCriteria = (SearchCourse)Session[LACESConstant.SessionKeys.SEARCH_VISITOR_CRITERIA];


            //Populate Subject Dropdown List
            populateSubjectList();

            //Populate Provider ListBox
            populateProviders();
            if (objSearchCriteria != null)
            {
                populateExistingValues(objSearchCriteria);


                if (Request.QueryString[LACESConstant.QueryString.PAGE_INDEX] != null)
                {
                    objSearchCriteria.PageIndex = Convert.ToInt32(Request.QueryString[LACESConstant.QueryString.PAGE_INDEX].ToString());
                }

                string orderBy = "";
                if (objSearchCriteria.SortColumn.ToLower() == "location")
                    orderBy = "[tblCourseDetails].[City] " + objSearchCriteria.SortOrder + ", [tblCourseDetails].[StateProvince] " + objSearchCriteria.SortOrder;
                else
                    orderBy = objSearchCriteria.SortColumn + " " + objSearchCriteria.SortOrder;

                int totalCount = 0;

                ///Get Search result by query
                try
                {
                    //Debugger.Break();

                    courseResult = objCourseDAL.GetPagedCourseBySearch(objSearchCriteria.WhereCondition, objSearchCriteria.PageIndex,
                        LACESConstant.SEARCH_RESULT_PAGE_SIZE, orderBy, ref totalCount);
                    uiRptCourseSearchResults.DataSource = courseResult;
                    uiRptCourseSearchResults.DataBind();
                    //int pagenumber = ;
                    string resultsNumber = "";
                    if (objSearchCriteria.PageIndex == 0)
                    {
                        resultsNumber = "1 -";
                    }
                    else
                    {
                        resultsNumber = ((objSearchCriteria.PageIndex * 10) + 1).ToString() + " - ";
                    }
                    int pageMax = ((objSearchCriteria.PageIndex + 1) * LACESConstant.SEARCH_RESULT_PAGE_SIZE);
                    if (pageMax <= totalCount)
                    {
                        resultsNumber += ((objSearchCriteria.PageIndex + 1) * 10).ToString();
                    }
                    else
                    {
                        resultsNumber += totalCount.ToString();
                    }
                    uiLitResultsMessage.Text = resultsNumber + " out of " + totalCount.ToString();

                }
                catch (Exception ex)
                {
                    string t = ex.Message;
                }
                
                setPreviousNextPage(objSearchCriteria, totalCount);

                ///Store Changed result in the session
                Session[LACESConstant.SessionKeys.SEARCH_VISITOR_CRITERIA] = objSearchCriteria;
            }
            txtKeyword.Focus();
            txtKeyword.Attributes.Add("onclick", "javascript:RemoveDefaultText(this,'Search Here');");
        }

    }

    /// <summary>
    /// Populate new search result depending on new selection criteria
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //string searchCriteria = buildSearchCriteria();
        SearchCourse objSearchCourse = buildSearchCriteria();
        objSearchCourse.SortColumn = "StartDate";
        objSearchCourse.SortOrder = "DESC";

        //Add search criteria into session
        Session.Add(LACESConstant.SessionKeys.SEARCH_VISITOR_CRITERIA, objSearchCourse);
        Response.Redirect("CourseResult.aspx");
    }

    /// <summary>
    /// Redirect to choose search criteria again
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchAgain_Click(object sender, EventArgs e)
    {
        Response.Redirect("WelcomeSearch.aspx?SearchAgain=true");
    }

    /// <summary>
    /// Set Previous Page, Next Page text and link
    /// </summary>
    /// <param name="objSearch"></param>
    /// <param name="totalCount"></param>
    private void setPreviousNextPage(SearchCourse objSearch, int totalCount)
    {
        ///For previous page link
        if (objSearch.PageIndex > 0)
        {
            PreviousPage.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + (objSearch.PageIndex - 1) + "\">";
            PreviousPage.InnerHtml += "&lt; Previous " + LACESConstant.SEARCH_RESULT_PAGE_SIZE + "</a>";
        }

        ///For next page link
        int remainingRecords = totalCount - ((objSearch.PageIndex + 1) * LACESConstant.SEARCH_RESULT_PAGE_SIZE);
        if (remainingRecords > 0)
        {
            int nextPageSize = LACESConstant.SEARCH_RESULT_PAGE_SIZE;
            if (nextPageSize > remainingRecords)
                nextPageSize = remainingRecords;
            NextPage.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + (objSearch.PageIndex + 1) + "\">";
            NextPage.InnerHtml += "Next &gt; " + nextPageSize + "</a> out of " + totalCount.ToString()  ;
        }
    }

    /// <summary>
    /// Get formatted location string with city and state
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
    protected string getFormattedLocation(Course course)
    {
        ///If State is empty
        if (course.StateProvince.Trim() == string.Empty)
            return Server.HtmlEncode(course.City);
        ///If city is empty
        else if (course.City == string.Empty)
            return course.StateProvince;
        ///If city and state both exist
        else
            return Server.HtmlEncode(course.City) + ", " + course.StateProvince;
    
    }

    #region Populate Subject List
    /// <summary>
    ///  Populate Subject List from Database
    /// </summary>
    private void populateSubjectList()
    {
        SubjectDataAccess subjectDAL = new SubjectDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int SubjectID = int.Parse(LACESUtilities.GetApplicationConstants("SubjectContentID"));
        IList<Subject> subjectList = subjectDAL.GetAllSubjects(SubjectID, webroot);

        ListItem defaultItem = new ListItem("- Subject Area -", "");
        ddlSubject.Items.Add(defaultItem);
        foreach (Subject subject in subjectList)
        {
            ListItem item = new ListItem(subject.SubjectName, subject.SubjectName);
            ddlSubject.Items.Add(item);
        }
    }
    #endregion

    #region Populate Providers
    /// <summary>
    ///  Populate all approved providers
    /// </summary>
    private void populateProviders()
    {
        //get all approved providers
        IList<ApprovedProvider> oApprovedProviders;
        ApprovedProviderDataAccess oApprovedProviderDataAccess = new ApprovedProviderDataAccess();
        int totalCount = 0;
        oApprovedProviders = oApprovedProviderDataAccess.GetPagedApprovedProviderSearch("tblApprovedProvider.Status = 'Active'", 0, Int16.MaxValue, "OrganizationName", "asc", ref totalCount);

        foreach (ApprovedProvider ap in oApprovedProviders)
        {
            string orgName = ap.OrganizationName;
            if (orgName.Length > 35)
                orgName = orgName.Substring(0, 32) + "...";
            lbEducationProvider.Items.Add(new ListItem(orgName, ap.ID.ToString()));
        }        
    }
    #endregion
    /// <summary>
    /// Generate Search criteria from user selection
    /// </summary>
    /// <returns></returns>
    private SearchCourse buildSearchCriteria()
    {
        SearchCourse objSearchCourse = new SearchCourse();
        objSearchCourse.Keywords = txtKeyword.Text.Trim();

        ///Split search keword using space
        string[] kewords = objSearchCourse.Keywords.Replace("'", "''").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        string searchQuery = "";

        ///Criteria for Title field
        ///Considering always checked
        //if (chkTitle.Checked)
        //{
            string titleQuery = "";

            ///If having double quotes consider exact match
            if (objSearchCourse.Keywords.StartsWith("\"") && objSearchCourse.Keywords.EndsWith("\""))
            {
                string exactText = objSearchCourse.Keywords.Substring(1, objSearchCourse.Keywords.Length - 2);
                titleQuery += "[Title] LIKE '" + exactText.Replace("'", "''") + "%' OR [Title] LIKE '% " + exactText.Replace("'", "''") + "%'";
            }
            else
            {
                ///Loop for every keyword
                foreach (string keyword in kewords)
                {
                    if (titleQuery != "")
                        titleQuery += " OR ";
                    titleQuery += "[Title] LIKE '" + keyword + "%' OR [Title] LIKE '% " + keyword + "%'";
                }
            }
            searchQuery += titleQuery;
            objSearchCourse.SearchFields = "Title";
        //}

        ///Criteria for Description field
        ///Considering always checked
        //if (chkDescription.Checked)
        //{
            string descriptionQuery = "";

            ///If having double quotes consider exact match
            if (objSearchCourse.Keywords.StartsWith("\"") && objSearchCourse.Keywords.EndsWith("\""))
            {
                string exactText = objSearchCourse.Keywords.Substring(1, objSearchCourse.Keywords.Length - 2);
                descriptionQuery += "[Description] LIKE '%" + exactText.Replace("'", "''") + "%'";
            }
            else
            {
                ///Loop for every keyword
                foreach (string keyword in kewords)
                {
                    if (descriptionQuery != "")
                        descriptionQuery += " OR ";
                    descriptionQuery += "[Description] LIKE '%" + keyword + "%'";
                }
            }

            if (searchQuery != "")
            {
                searchQuery += " OR ";
            }
            searchQuery += descriptionQuery;

            if (objSearchCourse.SearchFields != "")
            {
                objSearchCourse.SearchFields += " / ";
            }
            objSearchCourse.SearchFields += "Description";
        //}

        ///Criteria for LearningOutcomes field
        ///Considering always checked
        //if (chkLearningOutcomes.Checked)
        //{
            string lOQuery = "";

            ///If having double quotes consider exact match
            if (objSearchCourse.Keywords.StartsWith("\"") && objSearchCourse.Keywords.EndsWith("\""))
            {
                string exactText = objSearchCourse.Keywords.Substring(1, objSearchCourse.Keywords.Length - 2);
                lOQuery += "[LearningOutcomes] LIKE '%" + exactText.Replace("'", "''") + "%'";
            }
            else
            {
                ///Loop for every keyword
                foreach (string keyword in kewords)
                {
                    if (lOQuery != "")
                        lOQuery += " OR ";
                    lOQuery += "[LearningOutcomes] LIKE '%" + keyword + "%'";
                }
            }

            if (searchQuery != "")
            {
                searchQuery += " OR ";
            }
            searchQuery += lOQuery;

            if (objSearchCourse.SearchFields != "")
            {
                objSearchCourse.SearchFields += " / ";
            }
            objSearchCourse.SearchFields += "Outcomes";
        //}

        if (searchQuery != "")
            searchQuery = "(" + searchQuery + ")";

        ///Criteria for Subject Area
        if (ddlSubject.SelectedIndex > 0)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[Subjects] LIKE '%" + ddlSubject.SelectedValue + "%'";
            objSearchCourse.Subject = ddlSubject.SelectedValue;
        }

        ///Criteria for Distance Education
        ///Considering always checked
        //if (chkDistanceEdu.Checked)
        //{
            //if (searchQuery != "")
            //    searchQuery += " AND ";
            //searchQuery += "[DistanceEducation] = 'Y'";
            //objSearchCourse.Location = "Distance Education";
        //}
        /////Criteria for Location
        //else if (ddlLocation.SelectedIndex > 0)
        //{
        //    if (searchQuery != "")
        //        searchQuery += " AND ";
        //    searchQuery += "[tblCourseDetails].[StateProvince] = '" + ddlLocation.SelectedValue + "'";
        //    objSearchCourse.Location = ddlLocation.SelectedValue;
        //}

        // search for educational provider
        string eduList = "";
        for (int i = 0; i < lbEducationProvider.Items.Count; i++)
        {
            if (lbEducationProvider.Items[i].Selected)
            {
                if (eduList != "") eduList += " OR ";
                eduList += "tblApprovedProvider.[ID] = '" + lbEducationProvider.Items[i].Value + "'";
                objSearchCourse.Providers += "," + lbEducationProvider.Items[i].Value;
            }
        }
        if (eduList != "")
        {
            if (searchQuery != "")
                searchQuery += " AND (" + eduList + ")";
            else searchQuery += "(" + eduList + ")";

            objSearchCourse.Providers = objSearchCourse.Providers.Substring(1);
        }

        ///Criteria for Start Date
        ///

        if (txtStartDate.Text.Trim() != string.Empty && txtEndDate.Text.Trim() != string.Empty)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[StartDate] <= '" + txtEndDate.Text.Trim() + "' AND [EndDate] >='" + txtStartDate.Text.Trim() + "'";
        }
        else
        {

            if (txtStartDate.Text.Trim() != string.Empty)
            {
                if (searchQuery != "")
                    searchQuery += " AND ";
                searchQuery += "[StartDate] >= '" + txtStartDate.Text.Trim() + "'";
                objSearchCourse.StartDate = txtStartDate.Text.Trim();
            }

            ///Criteria for End Date
            if (txtEndDate.Text.Trim() != string.Empty)
            {
                if (searchQuery != "")
                    searchQuery += " AND ";
                searchQuery += "[EndDate] <= '" + txtEndDate.Text.Trim() + "'";
                objSearchCourse.EndDate = txtEndDate.Text.Trim();
            }
        }
        if (uiChkDistanceEducation.Checked)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[DistanceEducation] >= 'Y'";
            objSearchCourse.DistanceEducationOnly = "Y";
        }
        if (uiChkHealthSafetyWelfare.Checked)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[Health] >= 'Yes'";
            objSearchCourse.HSWOnly = "Yes";
        }
        if (searchQuery == "")
            searchQuery = "[tblCourseDetails].[Status] = 'OP' AND [EndDate] >= '" + DateTime.Today + "' AND [tblApprovedProvider].[Status] = 'Active' AND [Active]='T'";
        else
            searchQuery += " AND ([tblCourseDetails].[Status] = 'OP' AND [EndDate] >= '" + DateTime.Today + "' AND [tblApprovedProvider].[Status] = 'Active' AND [Active]='T')";

        objSearchCourse.WhereCondition = searchQuery;
        return objSearchCourse;
    }

    /// <summary>
    /// Populate controls with previous search search criteria
    /// </summary>
    /// <param name="objSearchCriteria"></param>
    private void populateExistingValues(SearchCourse objSearchCriteria)
    {
        if (objSearchCriteria.Keywords != "")
            txtKeyword.Text = objSearchCriteria.Keywords;

        if(objSearchCriteria.StartDate != "")
            txtStartDate.Text = objSearchCriteria.StartDate;

        if (objSearchCriteria.EndDate != "")
            txtEndDate.Text = objSearchCriteria.EndDate;

        if (objSearchCriteria.Subject != "")
            ddlSubject.SelectedValue = objSearchCriteria.Subject;
        if (objSearchCriteria.HSWOnly != "")
            uiChkHealthSafetyWelfare.Checked=true;
        if (objSearchCriteria.DistanceEducationOnly != "")
            uiChkDistanceEducation.Checked = true;
        if (objSearchCriteria.Providers != "")
        {
            string[] providers = objSearchCriteria.Providers.Split(',');
            for (int i = 0; i < providers.Length; i++)
            {
                for (int j = 0; j < lbEducationProvider.Items.Count; j++)
                {
                    if (providers[i] == lbEducationProvider.Items[j].Value)
                    {
                        lbEducationProvider.Items[j].Selected = true;
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Set left content palce holder width 75% and right content place holder width 25% using css
    /// </summary>
    //public void IncreaseLeftContentWidth()
    //{
    //    HtmlTableCell tdLftPane = (HtmlTableCell)Master.FindControl("lftPane");
    //    if (tdLftPane != null)
    //        tdLftPane.Attributes.Add("class", "leftPane75");

    //    HtmlTableCell tdRgtPane = (HtmlTableCell)Master.FindControl("rgtPane");
    //    if (tdRgtPane != null)
    //        tdRgtPane.Attributes.Add("class", "rightPane25");
    //}    
}
