/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: AttendeeCourses.aspx.cs
 * Purpose/Function: To display participant's courses in admin section
 * Author: Shohel Anwar
 * Version              Author              Date            Reason
 * 1.0              Shohel Anwar       01/22/2008      Created this Page with initial requirements
 * 1.1              Alamgir Hossain    07/09/2008      Work on Enhancement 2  
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
using System.Collections.Generic;
using Pantheon.ASLA.LACES.Common;

/// <summary>
/// Display participant's courses
/// </summary>
public partial class Admin_AttendeeCourses : AdminBasePage
{
    protected IList<Course> courseResult = new List<Course>();

    #region Page_Load
    /// <summary>
    /// Page load event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        CourseDataAccess objCourseDAL = new CourseDataAccess();
        string sSortColumn = string.Empty;
        string sSortOrder = string.Empty;
        
        
        //If found PARTICIPANT ID as query sting then display the participant info
        if (Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID] != null)
        {
            ///Check Sort Column Query String
            if (Request.QueryString[LACESConstant.QueryString.SORT_COLUMN] != null)
            {
                sSortColumn = Request.QueryString[LACESConstant.QueryString.SORT_COLUMN].ToString();
            }
            else
            {
                sSortColumn = "DateEntered"; // by default sort by DateEntered column
            }

            ///Check Sort Order Query String
            if (Request.QueryString[LACESConstant.QueryString.SORT_ORDER] != null)
            {
                sSortOrder = Request.QueryString[LACESConstant.QueryString.SORT_ORDER].ToString();
            }
            else
            {
                sSortOrder = "asc"; // by default sort in ascending order
            }

            //Create Header row
            //createHeaderRow(sSortColumn, sSortOrder);

            //Adjust left/right content place holder width
            IncreaseLeftContentWidth();

            //System.Diagnostics.Debugger.Break();

            ///Get Search result
            courseResult = objCourseDAL.GetParticipantCourses(Convert.ToInt32(Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString()), sSortColumn, sSortOrder);

            //Display participant information
            ParticipantDataAccess partDAC = new ParticipantDataAccess();
            Participant objPart = partDAC.GetParticipantByID(Convert.ToInt32(Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString()));
            if (objPart != null)
            {
                lblPartLastName.Text = Server.HtmlEncode(objPart.LastName);
                lblPartFirstName.Text = Server.HtmlEncode(objPart.FirstName);
                lblPartASLA.Text = Server.HtmlEncode(objPart.ASLANumber);
                lblPartCLARB.Text = Server.HtmlEncode(objPart.CLARBNumber);
                lblPartFL.Text = Server.HtmlEncode(objPart.FloridaStateNumber);
                lblMiddleName.Text = Server.HtmlEncode(objPart.MiddleInitial);
                lblCSLA.Text = Server.HtmlEncode(objPart.CSLANumber);

                //if no course found then display 0 as number of courses
                lblPartCourses.Text = courseResult != null && courseResult.Count > 0 ? courseResult.Count.ToString() : "0"; 
            }
        } 
    }
    #endregion Page_Load

    //#region createHeaderRow
    ///// <summary>
    ///// Create header columns content with sort column and sort order
    ///// </summary>
    ///// <param name="sortColumn">Order by column name</param>
    ///// <param name="sortOrder">'asc'/'desc'</param>
    //private void createHeaderRow(string sortColumn, string sortOrder)
    //{
    //    ///Title Header Text
    //    if (sortColumn.ToLower() == "title" && sortOrder.ToLower() == "asc")
    //        tdCourse.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=title&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">Course</a>";
    //    else
    //        tdCourse.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=title&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">Course</a>";

    //    ///Provider Header Text
    //    if (sortColumn.ToLower() == "name" && sortOrder.ToLower() == "asc")
    //        tdProvider.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=name&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">Provider</a>";
    //    else
    //        tdProvider.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=name&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">Provider</a>";

    //    ///Start Date Header Text
    //    if (sortColumn.ToLower() == "startdate" && sortOrder.ToLower() == "asc")
    //        tdStartDate.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=StartDate&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">Start Date</a>";
    //    else
    //        tdStartDate.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=StartDate&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">Start Date</a>";

    //    ///End Date Header Text
    //    if (sortColumn.ToLower() == "enddate" && sortOrder.ToLower() == "asc")
    //        tdEndDate.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=EndDate&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">End Date</a>";
    //    else
    //        tdEndDate.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=EndDate&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">End Date</a>";

    //    ///Date Entetred Header Text
    //    if (sortColumn.ToLower() == "dateentered" && sortOrder.ToLower() == "asc")
    //        tdDateEntetred.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=DateEntered&" + LACESConstant.QueryString.SORT_ORDER + "=DESC\">Date Entered</a>";
    //    else
    //        tdDateEntetred.InnerHtml = "<a href=\"?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID].ToString() + "&" + LACESConstant.QueryString.SORT_COLUMN + "=DateEntered&" + LACESConstant.QueryString.SORT_ORDER + "=ASC\">Date Entered</a>";        

    //}
    //#endregion createHeaderRow

}
