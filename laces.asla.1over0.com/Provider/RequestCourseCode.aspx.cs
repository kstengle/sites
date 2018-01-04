/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: RequestCourseCode.aspx
 * Purpose/Function: Request course code type Page for user to request course code type from administrator
 * 
 * Author: Wasim Majid
 * Version              Author            Date         Reason 
 * 1.0                 Wasim Majid      01/15/2008   Page Creation
 * 1.1                 Tarek Jubaer     01/22/2008   Request New Code (Task 5466)   
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
using System.Net.Mail;

using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
/// <summary>
/// Used for user to request course code type from administrator via email
/// </summary>
public partial class RequestCourseCode : ProviderBasePage
{
    //Public message variable
    public string message;

    #region Page load event handler
    /// <summary>
    /// Page load event handler
    /// Loads the course name based on course id
    /// Generates the redirect URL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            long courseID = 0;
            string courseName = "";
            //If CourseId is defined in URL
            if (Request.QueryString["CourseID"] != null)
            {
                try
                {
                    courseID = Convert.ToInt64(Request.QueryString["CourseID"]);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //Get course information by courseId
                //Provider provider = (Provider)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // Get provider information from Session 
                ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // get approved provider information from Session 

                CourseDataAccess courseDAL = new CourseDataAccess();
                Course course = courseDAL.GetCoursesDetailsByIDandProviderID(courseID, provider.ID); // Get Courses Details from Course ID and Provider ID
                //Generate linked course name to display
                courseName = "<a href=\"CourseDetails.aspx?CourseID=" + courseID + "\">" + Server.HtmlEncode(course.Title) + "</a>";
            }
            else//If courseId is not defined in URL, display None
                courseName = "<i>None</i>";

            //System.Diagnostics.Debugger.Break();


            //Set value for the lblCourseName control of master page
            Label lblCourseName = (Label)Master.FindControl("lblCourseName");
            if (lblCourseName != null)
                lblCourseName.Text = "Selected Course: " + courseName;
        }
        lblMessage.Text = "";
        lblMessage.Visible = false;
        message = "";
        txtCodeType.Focus();
    }
    #endregion

    #region Request button event handler
    /// <summary>
    /// Perform request course code type action on user button click
    /// Generates mail message
    /// Send mail to administrator
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRequest_Click(object sender, EventArgs e)
    {        
        try
        {
            //Get current provider
            //Provider provider = (Provider)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER];
            
            ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // get approved provider information from Session 
            //Set to email address
            
            //Declare new mail
            SmtpClient smtpCl = new SmtpClient();
            MailMessage omsg = new MailMessage();
            //MailMessage

            omsg.From = new MailAddress(LACESUtilities.GetAdminFromEmail());

            omsg.To.Add(LACESUtilities.GetAdminToEmail());
            omsg.CC.Add(LACESUtilities.GetAdminCCEmail());

            //Set mail subject
            omsg.Subject = "New Course Code Requested";
            //Set HTML mail format
            omsg.IsBodyHtml = true;
            string mailBody = Server.HtmlEncode(txtReason.Text.ToString().Trim());
            //Replace new line with <BR>
            mailBody = mailBody.Replace("\r\n", "<BR>");            
            //Generate mail body
            omsg.Body = "Someone has requested a new Course Code be added to the " + LACESConstant.LACES_TEXT + " system. The details of the newly requested code are as follows:<br><br>";
            omsg.Body += "Requested Code: " + Server.HtmlEncode(txtCodeType.Text.ToString()) + "<br>";
            omsg.Body += "Reason: " + mailBody + "<br>";
            omsg.Body += "Requester: " + provider.OrganizationName + "<br>";
            omsg.Body += "Requester's Email: <a href='mailto:" + provider.ApplicantEmail + "'>" + provider.ApplicantEmail + "</a><br><br>";
            omsg.Body += "If you wish to add this code to the " + LACESConstant.LACES_TEXT + " system, please <a href=\"http://" + Request.Url.Host.ToString() + "/admin/ManageCodeTypes.aspx\">manage the list of codes</a> in the administration system.<br><br>";
            omsg.Body += "If you are going to reject the code, or wish to discuss the code further, please contact the course provider at their email address listed above.";
            //Set SMTP host from configuration
            smtpCl.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER).ToString();
            smtpCl.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));
            //Send mail
            if(ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() =="Y")
            {
            smtpCl.Send(omsg);
            }
            //Generate thank you message
            message = "";
            lblMessage.Visible = true;
            lblMessage.Text = "Thank You! Your code request has been successfully submitted.";
            btnRequest.Visible = false;
            //After altering user if courseid is defind then send user to course detials page else send user to course list page
            if (Request.QueryString["CourseID"] != null)
            {
                long courseID = Convert.ToInt64(Request.QueryString["CourseID"]);
                //message += "location.replace(\"CourseDetails.aspx?CourseID=" + courseID + "\");";
                message = "<META http-equiv=\"refresh\" content=\"5;URL=CourseDetails.aspx?CourseID=" + courseID + "\">";
                lblMessage.Text = lblMessage.Text + "<BR>(You will be automatically redirected to course details page in 5 seconds.)";
            }
            else
            {
                message = "<META http-equiv=\"refresh\" content=\"5;URL=CourseList.aspx\">";
                lblMessage.Text = lblMessage.Text + "<BR>(You will be automatically redirected to course listing page in 5 seconds.)";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}
