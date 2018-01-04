/*------------------------------------------------------------------------------
 * Project Name     : LACES
 * Client Name      : American Society of Landscape Architects(ASLA)
 * Component Name   : EditAttendees.aspx.cs
 * Purpose/Function : Edit a Attendees of a course.
 * Author           : Alamgir Hossain
 * 
 * Version                 Author            Date         Reason 
 * 1.0                  Alamgir Hossain    01/21/08       Create and UI Components
 * 1.1                  Alamgir Hossain    07/09/2008     Work on Enhancement 2  
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
using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;

/// <summary>
/// Used to edit a particular Attendees of a course. Get a particular Attendees id 
/// as a query string and load Attendees information from database for update or delete. 
/// </summary>
public partial class Admin_EditAttendees : AdminBasePage
{

    #region class variable
    //global variable used to store the parent url
    static string referalURL = ""; 
    #endregion

    #region Page Load Event Handler
    /// <summary>
    ///  Page Load Event Handler, call every time when the page is loaded.   
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //set focus the default text box
        txtLastName.Focus();

        //get the master page form tag and set default button  
        HtmlForm masterHtmlForm = (HtmlForm)Master.FindControl("form1");
        masterHtmlForm.DefaultButton = btnSaveFinish.UniqueID;

        if (!IsPostBack)
        {
            try
            {
                //check the parent url
                if (Request.UrlReferrer != null)
                {
                    //save the parent url so we can redirect when operation in current page successfull
                    referalURL = Request.UrlReferrer.AbsoluteUri;
                }

                long courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);
                CourseDataAccess oCourseDataAccess = new CourseDataAccess();
                title.InnerHtml = "Edit Attendee for: " + Server.HtmlEncode(oCourseDataAccess.GetCoursesDetailsByID(courseID).Title);


                //load Attendees information into the UI controls
                LoadParticipant();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    #endregion

    #region Edit Attendeess Click Events

    /// <summary>
    /// Call every time when user click on remove Attendees button.
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnRemovePerticipant_Click(object sender, EventArgs e)
    {

        //Remove Perticipant button click, so remove the Attendees and its related course information from database

        //at first get the Attendees id from query string
        if (Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID] != null)
        {
            try
            {
                //get the Attendees id from query string
                long participantID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID]);
                ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();

                //check weather the Attendees is exist
                Participant currentParticipant = oParticipantDataAccess.GetParticipantByID(participantID);

                if (currentParticipant == null)
                {
                    //null means the Attendees is currently not exist
                    //show Attendees not found error message
                    lblMsg.Text = LACESConstant.Messages.PARTICIPANT_NOT_FOUND_IN_ADMIN;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //get the course id from query string
                    long courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);

                    //check delete successfully
                    if (oParticipantDataAccess.Delete(courseID, participantID))
                    {
                        if (referalURL != "")
                        {
                            //redirect the appropriate page
                            Response.Redirect(referalURL);
                        }
                    }
                    else
                    {
                        //error occur  show error message
                        lblMsg.Text = "Unable to delete attendees.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    /// <summary>
    /// Call when Save & Finish Button Click form UI
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveFinish_Click(object sender, EventArgs e)
    {
        //Save button click, so store the updated information into the database
        //at first get the Attendees id from query string
        if (Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID] != null)
        {
            try
            {
                //get the Attendees id from query string
                long participantID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID]);
                ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
                Participant currentParticipant = new Participant();

                //check weather the participant is exist
                currentParticipant = oParticipantDataAccess.GetParticipantByID(participantID);

                if (currentParticipant == null)
                {
                    //null means the Attendees is currently not exist
                    //show Attendees not found error message
                    lblMsg.Text = LACESConstant.Messages.PARTICIPANT_NOT_FOUND_IN_ADMIN;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //Attendees exist so update

                    currentParticipant.LastName = txtLastName.Text;
                    currentParticipant.FirstName = txtFirstName.Text;
                    currentParticipant.ASLANumber = txtASLA.Text;
                    currentParticipant.CLARBNumber = txtCLARB.Text;
                    currentParticipant.FloridaStateNumber = txtFL.Text;
                    currentParticipant.ID = participantID;
                    currentParticipant.CSLANumber = txtCSLA.Text;
                    currentParticipant.MiddleInitial = txtMiddleName.Text;

                    currentParticipant = oParticipantDataAccess.Update(currentParticipant);
                    

                    //check update successfully
                    if (currentParticipant != null)
                    {
                        if (referalURL != "")
                        {
                            //redirect the appropriate page
                            Response.Redirect(referalURL);
                        }
                    }
                    else
                    {
                        //error occur  show error message
                        lblMsg.Text = "Attendees already exists with same values.<br/><br/>";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    #endregion

    #region Load Attendees Information

    /// <summary>
    /// Use to load Attendees information from Attendees id.
    /// Perticipant id get from query string. Return none. 
    /// </summary>
    private void LoadParticipant()
    {
        //load the course information
        if (populateCourseInfo() == false)
        {
            return;
        }

        //check the query string
        if (Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID] != null)
        {
            try
            {
                //get the Attendees id from query string
                long participantID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID]);
                ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
                Participant currentParticipant=oParticipantDataAccess.GetParticipantByID(participantID);
                
                //check weather the provider is available
                if (currentParticipant != null)
                {
                    //available so assign to UI
                    txtLastName.Text = currentParticipant.LastName;
                    txtFirstName.Text = currentParticipant.FirstName;
                    txtASLA.Text = currentParticipant.ASLANumber;
                    txtCLARB.Text = currentParticipant.CLARBNumber;
                    txtFL.Text = currentParticipant.FloridaStateNumber;
                    txtCSLA.Text = currentParticipant.CSLANumber;
                    txtMiddleName.Text = currentParticipant.MiddleInitial;
                }
                else
                {
                    //show Attendees not found error message
                    lblMsg.Text = LACESConstant.Messages.PARTICIPANT_NOT_FOUND_IN_ADMIN;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    #endregion

    #region Get Course Details Information

    /// <summary>
    /// Populate course details in the UI. Get course id from query string.
    /// </summary>
    /// <returns>True if any course not found provider, otherwise false</returns>
    private bool populateCourseInfo()
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
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                    txtLastName.Text = "";
                    txtFirstName.Text = "";
                    txtASLA.Text = "";
                    txtCLARB.Text = "";
                    txtFL.Text = "";
                    return false;
                }
                divRightMessage.InnerHtml = "Use this page to edit an attendees who attended the " + Server.HtmlEncode(course.Title) + " course. Removing an attendees from the course is a permanent action, and can not be undone.";
            }
            catch (Exception ex)
            {
            }
        }
        return true; 
    }


    #endregion
}
