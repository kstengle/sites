/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: CourseRecordResult.aspx
 * Purpose/Function: Result page of Course Record search
 * Author: Alamgir Hossain 
 * Version              Author            Date             Reason 
 * 1.0                 Alamgir Hossain    07/08/2008   Initial development
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

/// <summary>
/// Result page of Course Record search
/// </summary>
public partial class CourseRecordResult : System.Web.UI.Page
{
    #region Class veriables

    string sKeyWords = string.Empty;
    protected IList<Course> courseResult = new List<Course>();
    string sSortColumn = string.Empty;
    //string sSortOrder = string.Empty;


    #endregion

    #region Page_Load

    /// <summary>
    /// Call every time when page load occur 
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        

        int iPageIndex = 0;
        int totalCount = 0;

        #region Check Session Data

        //if no search keywords for course record found in the session then redirect to the FindProviders.aspx page 
        if (Session[LACESConstant.SessionKeys.COURSE_RECORD_OBJ] == null)
        {
            Response.Redirect("Default.aspx");
        }

        CourseRecord oCourseRecord = (CourseRecord)Session[LACESConstant.SessionKeys.COURSE_RECORD_OBJ];
        title.InnerHtml = "Search Results for: " + Server.HtmlEncode(oCourseRecord.FirstName) + " " + Server.HtmlEncode(oCourseRecord.LastName);

        #endregion

        #region Check Query String Data



        ///Check Sort Column Query String
        //if (Request.QueryString[LACESConstant.QueryString.SORT_COLUMN] != null)
        //{
        //    sSortColumn = Request.QueryString[LACESConstant.QueryString.SORT_COLUMN].ToString();
        //}

        /////Check Sort Order Query String
        //if (Request.QueryString[LACESConstant.QueryString.SORT_ORDER] != null)
        //{
        //    sSortOrder = Request.QueryString[LACESConstant.QueryString.SORT_ORDER].ToString();
        //}

        sSortColumn = "StartDate Desc";

        ///Check Current Page Index Query String
        if (Request.QueryString[LACESConstant.QueryString.PAGE_INDEX] != null)
        {
            iPageIndex = Convert.ToInt32(Request.QueryString[LACESConstant.QueryString.PAGE_INDEX].ToString());
        }
        else
        {
            iPageIndex = 0;
        }

        #endregion

        #region Search

        CourseRecordDataAccess courseRecordDAL = new CourseRecordDataAccess();
        courseResult = courseRecordDAL.GetPagedCourseRecordBySearch(buildSearchCriteria(), iPageIndex, LACESConstant.SEARCH_RESULT_PAGE_SIZE, sSortColumn, ref totalCount);
        uiRptCourseRecordResults.DataSource = courseResult;
        uiRptCourseRecordResults.DataBind();
        #endregion

        ///Generate PreviousPage NextPage link
        setPreviousNextPage(iPageIndex, totalCount);
    }

    #endregion

    #region getFormattedLocation

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

protected string getFormattedLocation(string City, string StateProvince)
    {
        
        ///If State is empty
        if (StateProvince.Trim() == string.Empty)
            return Server.HtmlEncode(City);
        ///If city is empty
        else if (City == string.Empty)
            return StateProvince;
        ///If city and state both exist
        else
            return Server.HtmlEncode(City) + ", " + StateProvince;

    } 

    #endregion

    #region buildSearchCriteria
    /// <summary>
    /// Generate Search criteria from the keywords
    /// </summary>
    /// <returns>Search query where condition</returns>
    private string buildSearchCriteria()
    {
        CourseRecord oCourseRecord = (CourseRecord)Session[LACESConstant.SessionKeys.COURSE_RECORD_OBJ];

        string searchQuery = "";

        if (oCourseRecord.ASLANumber != "")
        {
            searchQuery += "ASLAMemberNumber LIKE '" + oCourseRecord.ASLANumber + "%'";
        }

        if (oCourseRecord.CLARBNumber != "")
        {
            if (searchQuery != "") searchQuery += " AND "; 
            searchQuery += "CLARBNumber LIKE '" + oCourseRecord.CLARBNumber + "%'";
        }
        if (oCourseRecord.CSLANumber != "")
        {
            if (searchQuery != "") searchQuery += " AND "; 
            searchQuery += "CSLANumber LIKE '" + oCourseRecord.CSLANumber + "%'";
        }
        if (oCourseRecord.FirstName != "")
        {
            if (searchQuery != "") searchQuery += " AND "; 
            searchQuery += "FirstName LIKE '" + oCourseRecord.FirstName + "%'";
        }
        if (oCourseRecord.FLNumber != "")
        {
            if (searchQuery != "") searchQuery += " AND "; 
            searchQuery += "FloridaStateNumber LIKE '" + oCourseRecord.FLNumber + "%'";
        }
        if (oCourseRecord.LastName != "")
        {
            if (searchQuery != "") searchQuery += " AND "; 
            searchQuery += "LastName LIKE '" + oCourseRecord.LastName + "%'";
        }
        if (oCourseRecord.MiddleInitial != "")
        {
            if (searchQuery != "") searchQuery += " AND "; 
            searchQuery += "MiddleInitial LIKE '" + oCourseRecord.MiddleInitial + "%'";
        }
        if (oCourseRecord.Email != "")
        {
            if (searchQuery != "") searchQuery += " AND ";
            searchQuery += "Email LIKE '" + oCourseRecord.Email + "%'";
        }

        ///For blank search
        if (searchQuery == "")
            searchQuery = " 1 = 1 ";

        return searchQuery;
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
            tdPreviousPage.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + (PageIndx - 1) + "\">";// +"&" + LACESConstant.QueryString.SORT_COLUMN + "=" + sSortColumn + "&" + LACESConstant.QueryString.SORT_ORDER + "=" + sSortOrder + "\">";
            tdPreviousPage.InnerHtml += "&lt; Previous " + LACESConstant.SEARCH_RESULT_PAGE_SIZE + "</a>";
        }

        ///For next page link
        int remainingRecords = totalCount - ((PageIndx + 1) * LACESConstant.SEARCH_RESULT_PAGE_SIZE);
        if (remainingRecords > 0)
        {
            int nextPageSize = LACESConstant.SEARCH_RESULT_PAGE_SIZE;
            if (nextPageSize > remainingRecords)
                nextPageSize = remainingRecords;
            tdNextPage.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PAGE_INDEX + "=" + (PageIndx + 1) + "\">";// +"&" + LACESConstant.QueryString.SORT_COLUMN + "=" + sSortColumn + "&" + LACESConstant.QueryString.SORT_ORDER + "=" + sSortOrder + "\">";
            tdNextPage.InnerHtml += "Next &gt; " + nextPageSize + "</a>";
        }
    }
    #endregion setPreviousNextPage

    #region btnExport_Click

    /// <summary>
    /// Call every time when click on the export link
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int totalCount = 0;
        CourseRecordDataAccess courseRecordDAL = new CourseRecordDataAccess();
        courseResult = courseRecordDAL.GetPagedCourseRecordBySearch(buildSearchCriteria(), 0, Int16.MaxValue, "StartDate", ref totalCount);


        for (int i = 0; i < courseResult.Count; i++)
        {
            if(courseResult[i].DistanceEducation=="N") courseResult[i].DistanceEducation="No";
            else if (courseResult[i].DistanceEducation == "Y") courseResult[i].DistanceEducation = "Yes";
        }

        DataTable dtCources = new DataTable("SearchResult");

        dtCources.Columns.Add("ID", typeof(long));
        dtCources.Columns.Add("StartDate", typeof(string));
        dtCources.Columns.Add("EndDate", typeof(string));
        dtCources.Columns.Add("CourseTitle", typeof(string));
        dtCources.Columns.Add("Location", typeof(string));
        dtCources.Columns.Add("DistanceLearning", typeof(string));
        dtCources.Columns.Add("Hours", typeof(string));
        dtCources.Columns.Add("HSWClassification", typeof(string));
        dtCources.Columns.Add("CourseCodes", typeof(string));
        dtCources.Columns.Add("Provider", typeof(string));
        //dtCources.Columns.Add("ProviderId", typeof(long));

        foreach (Course c in courseResult)
        {
            DataRow newRow = dtCources.NewRow();
            newRow["ID"] = c.ID;
            newRow["StartDate"] = c.StartDate.Month.ToString() + "/" + c.StartDate.Day.ToString() + "/" + c.StartDate.Year.ToString();
            newRow["EndDate"] = c.EndDate.Month.ToString() + "/" + c.EndDate.Day.ToString() + "/" + c.EndDate.Year.ToString();
            newRow["CourseTitle"] = c.Title;
            newRow["Location"] = getFormattedLocation(c);
            newRow["DistanceLearning"] = c.DistanceEducation;
            newRow["Hours"] = c.Hours;
            newRow["HSWClassification"] = c.Healths;
            newRow["CourseCodes"] = populateCourseCodeInfo(c.ID);
            newRow["Provider"] = c.ProviderName;
            dtCources.Rows.Add(newRow);
        }

        int[] iColumns;
        string[] sHeaders;

        if (dtCources != null && dtCources.Rows.Count > 0)
        {
            iColumns = new int[9];

            iColumns.SetValue(1, 0);
            iColumns.SetValue(2, 1);
            iColumns.SetValue(3, 2);
            iColumns.SetValue(4, 3);
            iColumns.SetValue(5, 4);
            iColumns.SetValue(6, 5);
            iColumns.SetValue(7, 6);
            iColumns.SetValue(8, 7);
            iColumns.SetValue(9, 8);

            sHeaders = new string[9];
            sHeaders.SetValue("StartDate", 0);
            sHeaders.SetValue("EndDate", 1);
            sHeaders.SetValue("CourseTitle", 2);
            sHeaders.SetValue("Location", 3);
            sHeaders.SetValue("DistanceLearning", 4);
            sHeaders.SetValue("Hours", 5);
            sHeaders.SetValue("Health,SafetyandWelfare", 6);
            sHeaders.SetValue("CourseCodes", 7);
            sHeaders.SetValue("Provider", 8);

            LACES.ExportData.Export objExport = new LACES.ExportData.Export("Web");
            objExport.ExportDetails(dtCources, iColumns, sHeaders, LACES.ExportData.Export.ExportFormat.CSV, "SearchResult.csv");
        }
        else
        {
            //lblMessage.Visible = true;
            //lblMessage.Text = "No data available to be exported.";
        }
    }

    #endregion

    #region Populate Course Code Information
    /// <summary>
    ///  Populate comma seperated Course Code Information
    /// </summary>
    /// <param name="courseID"></param>
    protected string populateCourseCodeInfo(long courseID)
    {
        CourseCodeDataAccess courseCodeDAL = new CourseCodeDataAccess();
        IList<CourseCode> courseCodeList = courseCodeDAL.GetAllCourseCodeByCourseID(courseID); // Get all Course code of the course

        string courseCodes = "";

        foreach (CourseCode courseCode in courseCodeList)
        {
            courseCodes += courseCode.CodeType.ToString() + ": " + courseCode.Description.Replace("\"", "\"") + ", ";
        }
        
        if(courseCodes.LastIndexOf(',')>1)
            courseCodes = courseCodes.Substring(0, courseCodes.LastIndexOf(','));

        return courseCodes;
    }
    #endregion

#region BuildTitles
    protected string getLocation(Course course)
    {
        long courseID = course.ID;
        string CourseTitle = course.Title;


        return "<a href='visitor/CourseDetails.aspx?CourseID=" + courseID + "'>" + CourseTitle + "</a>";

    }

    #endregion


    protected void uiRptCourseRecordResults_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Course thiscourse = (Course)e.Item.DataItem;
        if (thiscourse != null)
        {
            PlaceHolder phDistanceEducation = (PlaceHolder)e.Item.FindControl("uiphDistanceEducation");
            Label lblDistanceEducation = (Label)e.Item.FindControl("uiLblDistanceLearningText");
            if (thiscourse.DistanceEducation.Length==0)
            {
                phDistanceEducation.Visible = false;
            }else if(thiscourse.DistanceEducation.ToUpper() =="Y")
            {
                lblDistanceEducation.Text = "Yes";
            }
            else
            {
                lblDistanceEducation.Text = "No";
            }
            if(thiscourse.Hours.Length==0)
            {
                PlaceHolder phHours = (PlaceHolder)e.Item.FindControl("uiphHours");
                phHours.Visible = false;
            }
            if(thiscourse.Healths.Length==0)
            {
                PlaceHolder phHealths = (PlaceHolder)e.Item.FindControl("uiPhHealths");
                phHealths.Visible = false;
            }
            string ccodes = populateCourseCodeInfo(thiscourse.ID);
            if(ccodes.Length==0)
            {
                PlaceHolder PhCourseCodes = (PlaceHolder)e.Item.FindControl("uiPhCourseCodes");
                PhCourseCodes.Visible = false;
            }
            else
            {
                Label LblCourseCodes = (Label)e.Item.FindControl("uiLblCourseCodes");
                LblCourseCodes.Text = ccodes;
            }
            



        }
    }
}
