/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: FindACourse.aspx.cs
 * Purpose/Function: To get input for search criteria in Admin section
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/10/2008      Create this Page with initial requirements
 * 1.1              Alamgir Hossain     07/09/2008      Work on Enhancement 2  
 * 2.0              Md. Kamruzzaman     12/03/2008      Added custom SearchTextBox   
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
/// Get Course Search Criteria from Admin
/// </summary>
public partial class Admin_FindACourse : AdminBasePage
{
    /// <summary>
    /// Page Load Event to load input fields initially
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Populate Approved Provider ListBox
            populateProviders();

            //Populate Subject Dropdown List
            populateSubjectList();

            //Populate State Dropdown List
            populateStateList();

            ///If Reached this page form Course Search Result page by clicking search again
            ///And having criteria into the session then load existing selection value
            if (Request.QueryString["SearchAgain"] != null && Session[LACESConstant.SessionKeys.SEARCH_COURSE_CRITERIA] != null)
            {
                ///Get Search criteria from session
                SearchCourse objSearchCriteria = (SearchCourse)Session[LACESConstant.SessionKeys.SEARCH_COURSE_CRITERIA];
                if (objSearchCriteria != null)
                    loadExistingSelection(objSearchCriteria);
            }
            else
            {
                ///Commented this code according to internal acceptance changes
                //Initialize value for start and end date
                //txtStartDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
                //txtEndDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            }

            ///Focus to the first input control
            txtKeyword.Focus();
        }

        //txtKeyword.Attributes.Add("onclick", "javascript:RemoveDefaultText(this,'Search Here');");

        //chkDistanceEdu.Attributes.Add("onclick", "initializeLocation(this)");
        //ddlLocation.Attributes.Add("onchange", "uncheckDistanceEducation(this)");
    }

    /// <summary>
    /// Get Search Criteria and redirect to search result page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFindCourses_Click(object sender, EventArgs e)
    {
        //string searchCriteria = buildSearchCriteria();
        SearchCourse objSearchCourse = buildSearchCriteria();
        objSearchCourse.SortColumn = "StartDate";
        objSearchCourse.SortOrder = "DESC";
        
        //Add search criteria into session
        Session.Add(LACESConstant.SessionKeys.SEARCH_COURSE_CRITERIA, objSearchCourse);
        Response.Redirect("CourseResult.aspx");
    }

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
            if (orgName.Length > 50)
                orgName = orgName.Substring(0, 47) + "...";
            lbEducationProvider.Items.Add(new ListItem(orgName, ap.ID.ToString()));
        }
    }
    #endregion

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

    #region Populate Location
    /// <summary>
    ///  Populate State/Province List from database
    /// </summary>
    private void populateStateList()
    {
        StateDataAccess stateDAL = new StateDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int StateProvidenceID = int.Parse(LACESUtilities.GetApplicationConstants("StateProvinceContentID"));
        IList<State> stateList = stateDAL.GetAllStates(StateProvidenceID, webroot);

        ///Add default item
        ListItem defaultItem = new ListItem("- Location -", "");
        ddlLocation.Items.Add(defaultItem);

        /////Add Distance Education item
        //ListItem distanceEducation = new ListItem("Distance Education", "Distance Education");
        //ddlLocation.Items.Add(distanceEducation);

        foreach (State state in stateList)
        {
            ListItem item = new ListItem(state.StateName, state.StateCode);
            ddlLocation.Items.Add(item);
        }

        //for other International value
        ddlLocation.Items.Add(new ListItem("- Other International -", "OI"));
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
        

        ///Criteria for Description field
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
        

        ///Criteria for LearningOutcomes field
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
        //if (chkDistanceEdu.Checked)
        //{
        //    if (searchQuery != "")
        //        searchQuery += " AND ";
        //    searchQuery += "[DistanceEducation] = 'Y'";
        //    objSearchCourse.Location = "Distance Education";
        //}

        ///Criteria for Location
        if(ddlLocation.SelectedIndex > 0)
        {
            if (searchQuery != "")
                searchQuery += " AND ";            
            searchQuery += "[tblCourseDetails].[StateProvince] = '" + ddlLocation.SelectedValue + "'";
            objSearchCourse.Location = ddlLocation.SelectedValue;
        }

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

        if (searchQuery == "")
            searchQuery = " 1 = 1 ";

        objSearchCourse.WhereCondition = searchQuery;
        return objSearchCourse;
    }

    /// <summary>
    /// Assign existing search criteria selection
    /// </summary>
    /// <param name="objSearch"></param>
    private void loadExistingSelection(SearchCourse objSearch)
    {
        ///Assign keyword
        txtKeyword.Text = objSearch.Keywords;
        
        ///Check check boxes according to previous selection criteria
        //if (objSearch.SearchFields.IndexOf("Title") == -1)
        //    chkTitle.Checked = false;
        //if (objSearch.SearchFields.IndexOf("Description") == -1)
        //    chkDescription.Checked = false;
        //if (objSearch.SearchFields.IndexOf("Outcomes") == -1)
        //    chkLearningOutcomes.Checked = false;

        ///Select subject
        if (objSearch.Subject.Trim() != string.Empty)
            ddlSubject.SelectedValue = objSearch.Subject.Trim();

        ///Select location
        if (objSearch.Location.Trim() != string.Empty)
            ddlLocation.SelectedValue = objSearch.Location.Trim();

        ///Load start date
        if (objSearch.StartDate != string.Empty)
            txtStartDate.Text = objSearch.StartDate;

        ///Load end date
        if (objSearch.EndDate != string.Empty)
            txtEndDate.Text = objSearch.EndDate;
    }
}
