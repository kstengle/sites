/*------------------------------------------------------------------------------
 * Project Name     : LACES
 * Client Name      : ASLA
 * Component Name   : ManageParticipants.aspx.cs
 * Purpose/Function : Used to Manage Participants in admin mode.
 * Author           : Alamgir Hossain
 * 
 * Version                 Author            Date         Reason 
 * 1.0                  Alamgir Hossain    01/17/08   Create and UI Components
 * 
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
/// Manage Participants page, used to manage participant for a course in admin mode. 
/// The admin can add new participant for a spacific course. 
/// </summary>
public partial class Provider_ManageParticipants : AdminBasePage
{
    #region Page Load Event Handler

    /// <summary>
    ///  Page Load Event Handler, call every time when the page is loaded.
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //if the hidden field is yes that means choose ok from javascript confirm 
        //so same the participants to the database
        if(HiddenSaveInformation.Value == "Y")
        {
            //reset the hidden field
            HiddenSaveInformation.Value = "N";
            
            //save and go to clicked url
            AddParticipants();

            //redirect to the course detail page
            string url = Request.Url.AbsoluteUri;
            long courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);

            //this is the hidden field redirect value, -1 means course deatil page
            //-2 means provider details page, >0 means edit participant page and this is the
            //participant id
            int val=Convert.ToInt16(HiddenRedirectUrl.Value);
            if (val == -1) //course details
            {
                //redirect to course detail page
                Response.Redirect(url.Substring(0, url.LastIndexOf("/")) + "/CourseDetails.aspx?" + LACESConstant.QueryString.COURSE_ID + "=" + courseID);
            }
            else if (val == -2) //provider details
            {
                //redirect to provider detail page
                Response.Redirect(url.Substring(0, url.LastIndexOf("/")) + "/ProviderDetails.aspx");
            }
            else if (val > 0) //edit participant, and this is the participant id 
            {
                //redirect to edit participant page
                Response.Redirect(url.Substring(0, url.LastIndexOf("/")) + "/EditParticipants.aspx?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + val+"&CourseID=" + courseID.ToString());

            }
        }
        
        //focus the default text box
        row11.Focus();
        
        //get the master page form tag and set default button  
        HtmlForm masterHtmlForm = (HtmlForm)Master.FindControl("form1");
        masterHtmlForm.DefaultButton = btnSaveFinish.UniqueID;

        if (!IsPostBack)
        {

            if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null)
            {
                try
                {
                    //get the course id from query string
                    long courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);

                    //populate ParticipantList
                    populateParticipantList(courseID);

                    //populate course information
                    populateCourseInfo();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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

        ParticipantDataAccess participantDAL = new ParticipantDataAccess();
        IList<Participant> participantList = participantDAL.GetAllParticipantByCourse(courseID); // Get Participants of the course

        //prepare the header
        dvParticipantList.InnerHtml = "<table border='0' style='text-align:center;' cellpadding='0' cellspacing='0'>";
        dvParticipantList.InnerHtml += "<tr><td style='text-align:right; vertical-align:middle;' class='participantList'><strong>Last Name,</strong></td><td style='text-align:left; vertical-align:middle;' class='participantList'><strong>First Name</strong></td></tr>";

        //dynamically add rest of the rows
        foreach (Participant participant in participantList)
        {

            dvParticipantList.InnerHtml += "<tr><td onclick='javascript:return CheckChange(3," + participant.ID.ToString() + ")' style='text-align:right; vertical-align:top;' class='participantList'><a href='EditParticipants.aspx?ParticipantID=" + participant.ID.ToString() + "&CourseID=" + courseID.ToString() + "'>" + Server.HtmlEncode(participant.LastName) + "</a>,</td>";
            dvParticipantList.InnerHtml += "<td style='text-align:left; vertical-align:top;' class='participantList'>" + Server.HtmlEncode(participant.FirstName) + " <small><a href='ParticipantCourses.aspx?ParticipantID=" + participant.ID.ToString() + "'>[courses]</a></small></td></tr>";
        }
        dvParticipantList.InnerHtml += "</table>";
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
                    lblMsg.Text = LACESConstant.Messages.COURSE_NOT_FOUND_IN_ADMIN;
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

    #region Manage Participants Click Events

    /// <summary>
    /// Call when Save & Add More Button Click
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveAddMore_Click(object sender, EventArgs e)
    {
        if (AddParticipants())
        {
            //redirect to the course detail page
            string url = Request.Url.AbsoluteUri;
            long courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);
            Response.Redirect(url.Substring(0, url.LastIndexOf("/")) + "/ManageParticipants.aspx?CourseID=" + courseID);
        }
    }

    /// <summary>
    /// Call when Save & Finish Button Click
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveFinish_Click(object sender, EventArgs e)
    {
        if(AddParticipants())
        {
            //redirect to the course detail page
            string url = Request.Url.AbsoluteUri;
            long courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);
            Response.Redirect(url.Substring(0, url.LastIndexOf("/")) + "/CourseDetails.aspx?CourseID=" + courseID);
        }
    }

    #endregion
    
    #region Add participants

    /// <summary>
    /// Used to add participants for a specific course. course id is get form query string
    /// </summary>
    /// <returns>Return true if add successfull, otherwise return false</returns>
    private bool AddParticipants()
    {
        try
        {
            long courseID = 0;
            List<Participant> participantList = new List<Participant>();
            Participant participant;
            string firstnameControl = "", lastnameControl = "", ASLANumberControl = "", CLARBNumControl = "", StateNumberControl = "";
            string tempValue = "ctl00$ContentPlaceHolderLeftPane$row";
            for (int row = 1; row <= 20; row++)
            {
                participant = new Participant();

                //get a row's columns
                lastnameControl = tempValue + row + "1";
                firstnameControl = tempValue + row + "2";
                ASLANumberControl = tempValue + row + "3";
                CLARBNumControl = tempValue + row + "4";
                StateNumberControl = tempValue + row + "5";

                //get the dynamic TextBox controls 
                participant.LastName = ((TextBox)(this.FindControl(lastnameControl))).Text;
                participant.FirstName = ((TextBox)(this.FindControl(firstnameControl))).Text;
                participant.ASLANumber = ((TextBox)(this.FindControl(ASLANumberControl))).Text;
                participant.CLARBNumber = ((TextBox)(this.FindControl(CLARBNumControl))).Text;
                participant.FloridaStateNumber = ((TextBox)(this.FindControl(StateNumberControl))).Text;

                //check weather the required field is not empty
                if (participant.LastName != "" && participant.FirstName != "")
                {
                    //add in to the participant list
                    participantList.Add(participant);
                }
            }
            if (participantList.Count > 0)
            {
                if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null)
                {
                    //get the course id from query string
                    courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);

                    //add the participant list into the database
                    ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
                    oParticipantDataAccess.AddParticipantByCourse(courseID, participantList);                    
                }
            }
            return true;
            
        }
        catch (Exception ex)
        {
            return false;
        }
        return false;
    }

    #endregion
}
