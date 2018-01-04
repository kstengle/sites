/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: CourseResult.aspx.cs
 * Purpose/Function: To display course search result in admin section
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/15/2008      Create this Page with initial requirements
 * 1.1              Alamgir Hossain     07/08/2008      Work on Enhancement 2    
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
/// Display course search result of found search criteria into the session 
/// otherwise redirect to FindACourse page to select criteria
/// </summary>
public partial class Admin_CourseResult : AdminBasePage
{
    //Using to populate javascript course object
    protected string jsCourses = "";

    //Using to store course search result
    protected IList<Course> courseResult = new List<Course>();

    int itemIndex = 0;

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        CourseDataAccess objCourseDAL = new CourseDataAccess();

        if (!IsPostBack)
        {
            ///If search criteria not found in session redirect for select criteria again
            if (Session[LACESConstant.SessionKeys.SEARCH_COURSE_CRITERIA] == null)
            {
                Response.Redirect("FindACourse.aspx");
            }

            lblMessage.Visible = false;

            ///If found CousrseID and Status as query sting then change the status for this course
            if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null && Request.QueryString[LACESConstant.QueryString.COURSE_STATUS] != null)
            {
                long courseId = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID].ToString());
                string status = Request.QueryString[LACESConstant.QueryString.COURSE_STATUS].ToString();
                bool updated = false;

                ///Open Course
                if (status.ToLower() == "open")
                {
                    updated = objCourseDAL.ChangeCourseStatus(courseId, "OP");
                }
                ///Close Course
                else if (status.ToLower() == "close")
                {
                    updated = objCourseDAL.ChangeCourseStatus(courseId, "CL");
                }

                ///If updated successfully generate success message
                if (updated)
                {
                    Course updatedCourse = objCourseDAL.GetCoursesDetailsByID(courseId);
                    if (updatedCourse.Status == "Open")
                        updatedCourse.Status = "opened";
                    else
                        updatedCourse.Status = "closed";
                    ///Message store to session
                    Session[LACESConstant.SessionKeys.COURSE_ACTIVE_DEACTIVE_MSG] = "You have " + updatedCourse.Status + " " + Server.HtmlEncode(updatedCourse.Title) + " in the " + LACESConstant.LACES_TEXT + " system.";
                }
                else
                {
                    ///Message store to session
                    Session[LACESConstant.SessionKeys.COURSE_ACTIVE_DEACTIVE_MSG] = "Selected course has been updated by another admin.";
                }

                Response.Redirect("CourseResult.aspx");
            }

            ///If session contain any message, show the message and assign null
            if (Session[LACESConstant.SessionKeys.COURSE_ACTIVE_DEACTIVE_MSG] != null)
            {
                string message = Session[LACESConstant.SessionKeys.COURSE_ACTIVE_DEACTIVE_MSG].ToString();
                lblMessage.Visible = true;
                lblMessage.Text = message;
                if (message == "Selected course has been updated by another admin.")
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                else
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                Session[LACESConstant.SessionKeys.COURSE_ACTIVE_DEACTIVE_MSG] = null;
            }

            ///Get Search criteria from session
            SearchCourse objSearchCriteria = (SearchCourse)Session[LACESConstant.SessionKeys.SEARCH_COURSE_CRITERIA];

            //Populate Subject Dropdown List
            populateSubjectList();

            //Populate Provider ListBox
            populateProviders();

            populateExistingValues(objSearchCriteria);

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
            //createHeaderRow(objSearchCriteria.SortColumn, objSearchCriteria.SortOrder);

            ///Adjust left/right content place holder width
            IncreaseLeftContentWidth();

            ///Generate ORDER BY value
            string orderBy = "";
            if (objSearchCriteria.SortColumn.ToLower() == "location")
                orderBy = "[tblCourseDetails].[City] " + objSearchCriteria.SortOrder + ", [tblCourseDetails].[StateProvince] " + objSearchCriteria.SortOrder;
            else
                orderBy = objSearchCriteria.SortColumn + " " + objSearchCriteria.SortOrder;

            int totalCount = 0;

            ///Get Search result by query
            try
            {
                courseResult = objCourseDAL.GetPagedCourseBySearch(objSearchCriteria.WhereCondition, objSearchCriteria.PageIndex,
                    LACESConstant.SEARCH_RESULT_PAGE_SIZE, orderBy, ref totalCount);
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
                uiLitResultsMessage.Text = "Search Results: " + resultsNumber + " out of " + totalCount.ToString() + " results"; 
            }
            catch (Exception ex) { }
            ///Show selecterd criteria in right side
            //showSelectedCriteria(objSearchCriteria, totalCount);

            ///Generate PreviousPage NextPage link
            setPreviousNextPage(objSearchCriteria, totalCount);

            ///Store Changed result in the session
            Session[LACESConstant.SessionKeys.SEARCH_COURSE_CRITERIA] = objSearchCriteria;
        }
        txtKeyword.Focus();
        txtKeyword.Attributes.Add("onclick", "javascript:RemoveDefaultText(this,'Search Here');");
    }

    /// <summary>
    /// Redirect to choose search criteria again
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchAgain_Click(object sender, EventArgs e)
    {
        Response.Redirect("FindACourse.aspx?SearchAgain=true");
    }

    /// <summary>
    /// Populate new search result depending on new selection criteria
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchCourse objSearchCourse = buildSearchCriteria();
        objSearchCourse.SortColumn = "StartDate";
        objSearchCourse.SortOrder = "DESC";

        //Add search criteria into session
        Session.Add(LACESConstant.SessionKeys.SEARCH_COURSE_CRITERIA, objSearchCourse);
        Response.Redirect("CourseResult.aspx");
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

    /// <summary>
    /// Populate Activate/Deactivate link depending on status
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
    protected string activateDeactivate(Course course)
    {
        if (jsCourses != "")
            jsCourses += ",";
        jsCourses += "\"" + course.Title.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("/", "&#47;") + "\"";
        
        ///If course is not in open status then option will display to activate/open the course
        if (course.Status != "Open")
        {
            return "<a href=\"#\" onclick=\"ActivateDeactivateCourse('" + course.ID + "','" + (itemIndex++) + "','open');return false;\">Activate</a>";
        }
        ///If course is already in open status the option will display to deactivate/close the course
        else
        {
            return "<a href=\"#\" onclick=\"ActivateDeactivateCourse('" + course.ID + "','" + (itemIndex++) + "','close');return false;\">Deactivate</a>";
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
    /// Populate controls with previous search search criteria
    /// </summary>
    /// <param name="objSearchCriteria"></param>
    private void populateExistingValues(SearchCourse objSearchCriteria)
    {
        if (objSearchCriteria.Keywords != "")
            txtKeyword.Text = objSearchCriteria.Keywords;

        if (objSearchCriteria.StartDate != "")
            txtStartDate.Text = objSearchCriteria.StartDate;

        if (objSearchCriteria.EndDate != "")
            txtEndDate.Text = objSearchCriteria.EndDate;

        if (objSearchCriteria.Subject != "")
            ddlSubject.SelectedValue = objSearchCriteria.Subject;

        if (objSearchCriteria.HSWOnly != "")
            uiChkHealthSafetyWelfare.Checked = true;
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
    /// Generate Search criteria from admin selection
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
            searchQuery = " 1 = 1 ";

        objSearchCourse.WhereCondition = searchQuery;
        return objSearchCourse;
    }
    protected void btnActive_OnClick(object sender, EventArgs e)
    {
        LinkButton openCloseButton = sender as LinkButton;

        try
        {
            long courseID = Convert.ToInt32(openCloseButton.CommandArgument);
            string status = openCloseButton.CommandName;

            CourseDataAccess oCourseDataAccess = new CourseDataAccess();

            if (status == "T")
            {
                oCourseDataAccess.ChangeCourseActive(courseID, "F");
            }
            else if (status == "F")
            {
                oCourseDataAccess.ChangeCourseActive(courseID, "T");
            }
        }
        catch
        {
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void DeactivateCourse(Course course)
    {
        string courseTitle = course.Title;
    }
    //{
    //    LinkButton DeActiveButton = new LinkButton();

    //    string strcourseID = courseID.ToString();
    //    DeActiveButton.CommandArgument = strcourseID;
    //    DeActiveButton.CommandName = "KEVIN IS HERE";
    //    DeActiveButton.Text = strcourseID.ToString();
    //    DeActiveButton.Click += "DeactiveCourse_Click";
    //    Deactiveph.Controls.Add(DeActiveButton);
    //}
    //protected void DeactiveCourse_Click(object sender, EventArgs e)
    //{
    //    LinkButton DeActiveButton = sender as LinkButton;
    //    string courseID = DeActiveButton.CommandArgument;
    //}

}
