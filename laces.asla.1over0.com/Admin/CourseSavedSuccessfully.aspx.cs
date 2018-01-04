/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: CourseSavedSuccessfully.aspx
 * Purpose/Function: This is Page for showing success message after Course Updated for Administrator
 *
 * Author: Wasim Majid
 * Version              Author            Date             Reason 
 * 1.0                 Wasim Majid        01/17/2008      Initial development
 * 1.1                 Alamgir Hossain    07/09/2008      Work on Enhancement 2  
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

public partial class Admin_CourseSavedSuccessfully : AdminBasePage
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
        //this.Title =  LACESConstant.LACES_TEXT + " - Course Updated Successfully";
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
            string courseName = Server.HtmlEncode(Session["AdminCourseName"].ToString());

            // course code cheak whether any of the course code failed to save
            string courseCodeFailed = string.Empty;
            if (Session["AdminCourseCodeFailed"] != null && Session["AdminCourseCodeFailed"].ToString() == "yes")
            {
                courseCodeFailed = " (<span style='color:red;'>failed to save one or more course code</span>)";
            }

            string courseOperation = Request.QueryString["CourseOperation"].ToString(); // get the operation whether add or edit.
            if (courseOperation.ToLower().Equals("update")) // message for update
            {
                lblMessage.Text = "Thank you for saving " + courseName + "" + courseCodeFailed + ". Your changes have been saved to the " + LACESConstant.LACES_TEXT + ". <br/><br/><br/>";
            }

            // reset the session of course code
            Session["AdminCourseCodeFailed"] = null;
        }
    }
    #endregion
}
