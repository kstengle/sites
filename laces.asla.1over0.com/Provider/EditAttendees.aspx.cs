/*------------------------------------------------------------------------------
 * Project Name     : LACES
 * Client Name      : American Society of Landscape Architects(ASLA)
 * Component Name   : EditAttendees.aspx.cs
 * Purpose/Function : Edit a particular Attendees of a course.
 * Author           : Alamgir Hossain
 * 
 * Version                 Author               Date            Reason 
 * 1.0                  Alamgir Hossain     01/21/2008      Create and UI Components
 * 1.1                  Matiur Rahman       01/29/2008      Update security to prevent access of details information of other providers
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
/// Used to edit a particular Attendees of a course. Get Attendees id 
/// as a query string and load Attendees information from database for update or delete. 
/// </summary>
public partial class Provider_EditAttendees : ProviderBasePage
{

    #region class variables
    //global variable used to store the parent url
    static string referalURL = "";

    //Global course id for this class
    long courseID = 0;

    //Global Attendees id for this class
    long participantID = 0;
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

        ///If having the querstring for course id assign this to global variable
        if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null)
            courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);

        ///If having the querstring for Attendees id assign this to global variable
        if (Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID] != null)
            participantID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.PARTICIPANT_ID]);

        //get the master page form tag and set default button  
        HtmlForm masterHtmlForm = (HtmlForm)Master.FindControl("form1");
        masterHtmlForm.DefaultButton = btnSaveFinish.UniqueID;
        
        if (!IsPostBack)
        {
            try
            {

                Course course = getCourseInfo();
                populateCourseInfo(course);

                title.InnerHtml = "Edit Attendee for: " + Server.HtmlEncode(course.Title);

                if (course != null && courseID == course.ID)
                {
                    //load Attendees information into the UI controls
                    LoadParticipant();
                }

                //check the parent url
                if (Request.UrlReferrer != null)
                {
                    //save the parent url so we can redirect when operation in current page successfull
                    referalURL = Request.UrlReferrer.AbsoluteUri;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    #endregion

    #region Remove Attendeess Click Events

    /// <summary>
    /// Call every time when user click on remove Attendees button.
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnRemovePerticipant_Click(object sender, EventArgs e)
    {

        //Remove Perticipant button click, so remove the Attendees and its related course information from database
        try
        {
            Course objCourse = getCourseInfo();

            if (objCourse != null)
            {
                ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();

                //check weather the Attendees is exist
                Participant currentParticipant = oParticipantDataAccess.GetParticipantByIDandCourseID(participantID, courseID);

                if (currentParticipant != null)
                {
                    //check delete successfully
                    if (oParticipantDataAccess.Delete(objCourse.ID, participantID))
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
                    }
                }                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// <summary>
    /// Call every time when 'Save & Finish' Button Click form UI
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveFinish_Click(object sender, EventArgs e)
    {
        //Save button click, so store the updated information into the database        
        try
        {
            Course objCourse = getCourseInfo();

            if (objCourse != null)
            {
                ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
                Participant currentParticipant = new Participant();

                //check weather the participant is exist
                currentParticipant = oParticipantDataAccess.GetParticipantByIDandCourseID(participantID, courseID);

                if (currentParticipant != null)
                {
                    //participant exist so update

                    currentParticipant.LastName = txtLastName.Text;
                    currentParticipant.FirstName = txtFirstName.Text;
                    currentParticipant.ASLANumber = txtASLA.Text;
                    currentParticipant.CLARBNumber = txtCLARB.Text;
                    currentParticipant.FloridaStateNumber = txtFL.Text;
                    currentParticipant.ID = participantID;
                    currentParticipant.CSLANumber = txtCSLA.Text;
                    currentParticipant.MiddleInitial = txtMiddleName.Text;
                    currentParticipant.Email = txtEmail.Text;
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
                    }
                }                
            }
        }
        catch (Exception ex)
        {
            throw ex;
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

        try
        {
            ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
            Participant currentParticipant = oParticipantDataAccess.GetParticipantByIDandCourseID(participantID, courseID);

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
                txtEmail.Text = currentParticipant.Email;
            }
            else
            {
                //show participant not found error message
                lblMsg.Text = LACESConstant.Messages.PARTICIPANT_NOT_FOUND_IN_PROVIDER;
                
                ///Disable buttons
                btnRemovePerticipant.Enabled = false;
                btnSaveFinish.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Populate Course Details Information

    /// <summary>
    /// Populate course details in the UI using course object
    /// </summary>
    /// <param name="course">course object</param>
    /// <returns></returns>
    private void populateCourseInfo(Course course)
    {
        try
        {           
            //get master page label for showing the course name
            Label lblMasterCourseName = (Label)Master.FindControl("lblCourseName");

            //course is null if current provider is not assign for that course
            if (course == null || course.ID < 1)
            {
                ///Disable buttons
                btnRemovePerticipant.Enabled = false;
                btnSaveFinish.Enabled = false;

                lblMsg.Text = LACESConstant.Messages.COURSE_NOT_FOUND_IN_PROVIDER;
            }
            else
            {
                //this is for message in the right side of the page 
                divRightMessage.InnerHtml = "Use this page to edit an attendees who attended the " + Server.HtmlEncode(course.Title) + " course. Removing an attendees from the course is a permanent action, and can not be undone.";

                //assign course name in to the master page control
                lblMasterCourseName.Text = "Selected Course: <a href='CourseDetails.aspx?" + LACESConstant.QueryString.COURSE_ID + "=" + Server.HtmlEncode(course.ID.ToString()) + "'>" + Server.HtmlEncode(course.Title) + "</a>";

                //get course detail by courseid and providerid
                //Provider provider = (Provider)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // Get provider information from Session 
                
                ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // get approved provider information from Session 
            
                //set the provider name to the master page ProviderName label
                Label lblMasterProviderName = (Label)Master.FindControl("lblProviderName");
                lblMasterProviderName.Text = Server.HtmlEncode("Signed in as: " + provider.OrganizationName);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }        
    }

    #endregion

    #region Get Course Details Information
    /// <summary>
    /// Get course details information if any course found otherwise null
    /// </summary>
    /// <returns>course object</returns>
    private Course getCourseInfo()
    {
        try
        {
            //get course detail by courseid and providerid
            //Provider provider = (Provider)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // Get provider information from Session 
            ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // get approved provider information from Session 
            CourseDataAccess courseDAL = new CourseDataAccess();
            return courseDAL.GetCoursesDetailsByIDandProviderID(courseID, provider.ID); // Get Courses Details from Course ID and Provider ID            
        }
        catch (Exception ex)
        {
            throw ex;
        }     
    }

    #endregion
}
