/*------------------------------------------------------------------------------
 * Project Name     : LACES
 * Client Name      : ASLA
 * Component Name   : ManageAttendees.aspx.cs
 * Purpose/Function : Used to Manage Attendees in admin mode.
 * Author           : Alamgir Hossain
 * 
 * Version                 Author             Date              Reason 
 * 1.0                  Alamgir Hossain     07/08/2008      Create and UI Components
 * 1.1                  Alamgir Hossain     07/09/2008      Work on Enhancement 2  
 * --------------------------------------------------------------------------------*/

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
using System.Collections.Generic;
using Pantheon.ASLA.LACES.Common;
using System.IO;
using System.Data.Common;
using System.Diagnostics;
using System.Data.OleDb;

/// <summary>
/// Manage Participants page, used to manage participant for a course in user mode. 
/// The admin can add new participant for a spacific course. 
/// </summary>
public partial class Admin_ManageAttendees : AdminBasePage
{
    #region Page Load Event Handler

    /// <summary>
    ///  Page Load Event Handler, call every time when the page is loaded.
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Debugger.Break();

        if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null)
        {
            try
            {
                //get the course id from query string
                long courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);

                //populate ParticipantList
                populateParticipantList(courseID);

                populateCourseInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            LoadAllCourses();
        }
    }

    #endregion

    #region GetAllCourses

    /// <summary>
    /// Load all cources in the UI
    /// </summary>
    private void LoadAllCourses()
    {
    if (txtKeyword.Text.Trim().Length == 0)
        {
        //create the coures data access
        CourseDataAccess objCourseDAL = new CourseDataAccess();
        int totalCount = 0;
        IList<Course> cources = objCourseDAL.GetPagedCourseBySearch("1=1", 1, 500, "StartDate desc", ref totalCount);

        existingCourcesDiv.InnerHtml = "<div class='title'>Upload or Edit Attendees</div>";
        existingCourcesDiv.InnerHtml += "<div class='existingCources'>";
        existingCourcesDiv.InnerHtml += "<table cellpadding='0' width='100%'>";
		existingCourcesDiv.InnerHtml += "<thead><tr><td class='TableHeader' width='50%'>Course Title</td><td class='TableHeader' width='15%'>Start Date</td>";
		existingCourcesDiv.InnerHtml += "<td class='TableHeader' width='15%'>End Date</td>";
		
		existingCourcesDiv.InnerHtml += "<td colspan='2' class='TableHeader' width='20%' style='text-align:center'>Options</td>";
		//existingCourcesDiv.InnerHtml += "<td class='TableHeader' width='27%'>Upload Attendees in Excel</td><td class='TableHeader' width='15%'>Edit Attendees</td>";
		
		existingCourcesDiv.InnerHtml += "</tr></thead>";
		existingCourcesDiv.InnerHtml += "<tbody id='course_list'>";

        foreach (Course c in cources)
        {
            existingCourcesDiv.InnerHtml += "<tr><td class='grayText'><a href=CourseDetails.aspx?courseid=" + c.ID + ">" + Server.HtmlEncode(c.Title) + "</a></td>";
            existingCourcesDiv.InnerHtml += "<td class='grayText'>" + c.StartDate.ToShortDateString() + "</td>";
            existingCourcesDiv.InnerHtml += "<td class='grayText'>" + c.EndDate.ToShortDateString() + "</td>";
            existingCourcesDiv.InnerHtml += "<td align='center'><a title='Upload Attendees in Excel' href=UploadAttendees.aspx?courseid=" + c.ID + ">Upload</a></td>";
            existingCourcesDiv.InnerHtml += "<td align='center'><a title='Edit Attendees' href=EditAllCourseAttendees.aspx?CourseID=" + c.ID + ">Edit</a></td></tr>";
        }

		existingCourcesDiv.InnerHtml += "</tbody>";
        existingCourcesDiv.InnerHtml += "</table>";
		existingCourcesDiv.InnerHtml += "<div class='page_navigation'></div>";
		existingCourcesDiv.InnerHtml += "</div>";

        if (cources.Count <= 0)
        {
            existingCourcesDiv.Visible = false;
        }
        else
        {
            existingCourcesDiv.Visible = true;
        }
	}
    }

    #endregion

    #region Populate the participants of the course
    /// <summary>
    /// Populate the participants of the course. Get a course id which participant information want to get
    /// from database. Return none.
    /// </summary>
    /// <param name="courseID">Course ID</param>
    private void populateParticipantList(long courseID)
    {
        try
        {
            CourseDataAccess oCourseDataAccess = new CourseDataAccess();


            ParticipantDataAccess participantDAL = new ParticipantDataAccess();
            IList<Participant> participantList = participantDAL.GetAllParticipantByCourse(courseID); // Get Participants of the course

            //prepare the header
            dvParticipantList.InnerHtml = "<div class='title'>Edit Attendees for: " + Server.HtmlEncode(oCourseDataAccess.GetCoursesDetailsByID(courseID).Title) + "</div><div><table border='0' style='text-align:left;' cellpadding='0' cellspacing='0'>";

            if (participantList.Count > 0)
            {
                dvParticipantList.InnerHtml += "<tr><td style='text-align:left; vertical-align:left;' class='participantList'><strong>Last Name</strong></td><td style='text-align:left; vertical-align:middle;' class='participantList'><strong>First Name</strong></td></tr>";

                //dynamically add rest of the rows
                foreach (Participant participant in participantList)
                {
                    dvParticipantList.InnerHtml += "<tr><td style='text-align:left; vertical-align:left;' class='participantList'><a href='EditAttendees.aspx?ParticipantID=" + participant.ID.ToString() + "&CourseID=" + courseID.ToString() + "'>" + Server.HtmlEncode(participant.LastName) + "</a></td>";
                    dvParticipantList.InnerHtml += "<td style='text-align:left; vertical-align:left;' class='participantList'>" + Server.HtmlEncode(participant.FirstName) + "</td></tr>";
                }
            }
            else
            {
                dvParticipantList.InnerHtml += "<tr><td style='color:red'>No attendees found.</td></tr>";
            }
            dvParticipantList.InnerHtml += "</div></table>";
            dvParticipantList.InnerHtml += "<br/><div><a href='../admin/UploadAttendees.aspx?courseid=" + courseID + "'>Upload Attendees in Excel</a><div>";
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    #endregion

#region Search Courses
    protected void Search(object sender, EventArgs e)
    {
        IList<Course> courseResult = new List<Course>();
        SearchCourse objSearchCourse = buildSearchCriteria();
        int itemIndex = 0;
        
        int totalCount = 0;
        string strStartDate = txtStartDate.Text;
        string strEndDate = txtEndDate.Text;
        string strSearchTerm = txtKeyword.Text;
        CourseDataAccess objCourseDAL = new CourseDataAccess(); 
        try
        {
            courseResult = objCourseDAL.GetPagedCourseBySearch(strStartDate, strEndDate, strSearchTerm, 1,
                250,"", ref totalCount);

            string resultsNumber = courseResult.Count.ToString();
           existingCourcesDiv.InnerHtml = "<div class='title'>Upload or Edit Attendees</div>";
        existingCourcesDiv.InnerHtml += "<div class='existingCources'>";
        existingCourcesDiv.InnerHtml += "<table cellpadding='0' width='100%'><tr><td class='TableHeader' width='37%'>Course Title</td><td class='TableHeader' width='11%'>Start Date</td><td class='TableHeader' width='10%'>End Date</td><td class='TableHeader' width='27%'>Upload Attendees in Excel</td><td class='TableHeader' width='15%'>Edit Attendees</td></tr>";
		existingCourcesDiv.InnerHtml += "<tbody id='course_list'>";
        foreach (Course c in courseResult)
        {
            existingCourcesDiv.InnerHtml += "<tr><td class='grayText'><a href=CourseDetails.aspx?courseid=" + c.ID + ">" + Server.HtmlEncode(c.Title) + "</a></td>";
            existingCourcesDiv.InnerHtml += "<td class='grayText'>" + c.StartDate.ToShortDateString() + "</td>";
            existingCourcesDiv.InnerHtml += "<td class='grayText'>" + c.EndDate.ToShortDateString() + "</td>";
            existingCourcesDiv.InnerHtml += "<td align='center'><a href=UploadAttendees.aspx?courseid=" + c.ID + ">Upload</a></td>";
            existingCourcesDiv.InnerHtml += "<td align='center'><a href=EditAllCourseAttendees.aspx?CourseID=" + c.ID + ">Edit</a></td></tr>";
        }
		existingCourcesDiv.InnerHtml += "</tbody>";
        existingCourcesDiv.InnerHtml += "</table>";
		existingCourcesDiv.InnerHtml += "<div class='page_navigation'></div>";
		existingCourcesDiv.InnerHtml += "</div>";

        if (courseResult.Count <= 0)
        {
            existingCourcesDiv.Visible = false;
        }
        else
        {
            existingCourcesDiv.Visible = true;
        }

         //   uiLitResultsMessage.Text = "Search Results: " + resultsNumber + " out of " + totalCount.ToString() + " results";
        }
        catch (Exception ex) { }
    }

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
        if (searchQuery != "")
            searchQuery = "(" + searchQuery + ")";                    
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
#endregion
    #region Get Course Details Information

    /// <summary>
    /// Used to populate course information into UI. It get a course id from query string
    /// which detail information needed. Return none.
    /// </summary>
    private void populateCourseInfo()
    {
        long courseID = 0;
        if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null)
        {
            try
            {
                //get the course id from query string
                courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);

                //get course detail by courseid and providerid
                CourseDataAccess courseDAL = new CourseDataAccess();
                Course course = courseDAL.GetCoursesDetailsByID(courseID); // Get Courses Details from Course ID and Provider ID

                if (course == null || course.ID < 1)
                {
                    lblMsg.Text = LACESConstant.Messages.COURSE_NOT_FOUND_IN_PROVIDER;
                    dvParticipantList.Visible = false;
                    return;
                }

                Label lblMasterCourseName = (Label)Master.FindControl("lblCourseName");
                lblMasterCourseName.Text = "Selected Course: <a href='CourseDetails.aspx?" + LACESConstant.QueryString.COURSE_ID + "=" + Server.HtmlEncode(course.ID.ToString()) + "'>" + Server.HtmlEncode(course.Title) + "</a>";
                lblMasterCourseName.Attributes.Add("onclick", "javascript:return CheckChange(2,-1)");
            }
            catch (Exception ex)
            {
            }
        }
    }


    #endregion
}
