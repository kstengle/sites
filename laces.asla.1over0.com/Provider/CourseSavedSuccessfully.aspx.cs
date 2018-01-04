/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: CourseSavedSuccessfully.aspx
 * Purpose/Function: This is Page for showing success message after Course Added
 *
 * Author: Wasim Majid
 * Version              Author            Date             Reason 
 * 1.0                 Wasim Majid      01/17/2008   Initial development
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

public partial class Provider_CourseSavedSuccessfully : ProviderBasePage
{
    #region Page load event handler
    /// <summary>
    /// Page load event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //IncreaseLeftContentWidth();
        successMessage(); // populate the message
        
        // define the title of the page
        this.Title = LACESConstant.LACES_TEXT + " - Course Added Successfully | LA CES™";
    }
    #endregion  

    #region populate the success message after saving a course
    /// <summary>
    ///  populate the success message after saving a course
    /// </summary>
    private void successMessage()
    {
        if (Request.QueryString["CourseOperation"] != null)
        {
            // saved course name get from session
            string courseName = Server.HtmlEncode(Session["CourseName"].ToString());

            // course code cheak whether any of the course code failed to save
            string courseCodeFailed = string.Empty;
            if (Session["CourseCodeFailed"]!=null && Session["CourseCodeFailed"].ToString() == "yes")
            {
                courseCodeFailed = " (<span style='color:red;'>failed to save one or more course code</span>)";
            }

            string courseOperation = Request.QueryString["CourseOperation"].ToString(); // get the operation whether add or edit.
            if (courseOperation.ToLower().Equals("update")) // message for update
            {
                lblMessage.Text = "Thank you for saving " + courseName + "" + courseCodeFailed + ". Your changes have been saved to the " + LACESConstant.LACES_TEXT + " system. <br/><br/> <a href='CourseList.aspx'>Click Here</a> to return to the Course List.";
                 
                // define the title of the page in update mode
                this.Title =  LACESConstant.LACES_TEXT + " - Course Updated Successfully";
            }
            else if (courseOperation.ToLower().Equals("renew"))
            {
                lblMessage.Text = "Thank you for renewing " + courseName + "" + courseCodeFailed + ". Your changes have been saved to the " + LACESConstant.LACES_TEXT + " system. <br/><br/> <a href='CourseList.aspx'>Click Here</a> to return to the Course List.";
            }
            else // message for add
            {
                lblMessage.Text = "Thank you for saving " + courseName + "" + courseCodeFailed + ". The course you have entered is a new course and will not be displayed to visitors until it has passed the approval process.";
                lblMessage.Text += "<br /><br /><br />Each course is an individual record in LA CES. If this course is held multiple times, use the LA CES <a href='coursedetails.aspx?ReplicateCourse=Y'>quick form</a> to change the dates, locations, instructors, or other information quickly and create a new version of this course.<br/><br/> <a href='CourseList.aspx'>Click Here</a> to return to the course list.";
            }

            // reset the session of course code
            Session["CourseCodeFailed"] = null;
        }
    }
    #endregion
}
