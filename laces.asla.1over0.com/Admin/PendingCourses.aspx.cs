/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: PendingCourses.aspx.cs
 * Purpose/Function: Display Admin welcome page with pending courselist
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/09/2008      Create this Page with initial requirements
 * 1.1              Alamgir Hossain     07/09/2008      Work on Enhancement 2    
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
using System.Text;
using System.Net.Mail;
/// <summary>
/// Displaying All Pending Courses at the admin welcome screen
/// </summary>
public partial class Admin_PendingCourses : AdminBasePage
{
    //Using to count horizontal lines
    protected int hrcount = 1;
    
    //Using to populate javascript course object
    protected string jsCourseObject = "";
    
    //Using to store pending course items
    protected IList<Course> pendingCourses = new List<Course>();

    int itemIndex = 0;

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("FindCourses.aspx?status=NP");
        //Debugger.Break();

        lblMsg.Visible = false;
        
        if (!IsPostBack)
        {
            CourseDataAccess objCourseDAL = new CourseDataAccess();
            
            //If having query string of CourseID considering to delete the course item
            if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null
                && Request.QueryString[LACESConstant.QueryString.COURSE_ID].ToString() != "")
            {
                long courseId = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID].ToString());
                Course deletingCourse = objCourseDAL.GetCoursesDetailsByID(courseId);

                lblMsg.Visible = true;                    
                ///If course deleted by another admin
                if (deletingCourse == null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Selected course has been deleted by another admin.";
                }
                ///If course is not as "New / Pending Approval" status
                else if (deletingCourse.Status != "New / Pending Approval")
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Selected course is not \"New / Pending Approval\" status currently.";
                }
                else
                {
                    bool deleted = objCourseDAL.Delete(courseId);//call dal method to delete

                    ApprovedProviderDataAccess objProviderDAL = new ApprovedProviderDataAccess();
                    ApprovedProvider provider = objProviderDAL.GetApprovedProviderByID(deletingCourse.ProviderID);

                    //If deleted the course item show the message
                    if (deleted)
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "You have rejected " + Server.HtmlEncode(deletingCourse.Title) + " from the " + LACESConstant.LACES_TEXT + " system.";
                        lblMsg.Text += " Please contact <a href='mailto:" + provider.ApplicantEmail + "'>" + Server.HtmlEncode(provider.OrganizationName) + "</a>";
                        lblMsg.Text += " to explain why the course was rejected.";
                    }
                }
                
            }

            ///Populate pending course list
            pendingCourses = objCourseDAL.GetAllPendingCourses();
            dlCourseList.DataSource = pendingCourses;
            dlCourseList.DataBind();

            if (pendingCourses == null || pendingCourses.Count == 0)
            {
                if (lblMsg.Visible == false)
                {
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Black;
                    lblMsg.Text = "No course found.";
                }
                btnApprove.Enabled = false;
            }
        }
    }

    ///// <summary>
    ///// Redirect to Add New Provider page
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnAddProvider_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("ProviderDetails.aspx");
    //}

    /// <summary>
    /// Approve Selected Courses
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        CourseDataAccess objCourseDAL = new CourseDataAccess();
        
        ///Loop all items in data list to check approve checkbox for approve one course
        foreach (DataListItem dl in dlCourseList.Items)
        {
            CheckBox chkApp = (CheckBox)dl.FindControl("chkApprove");
            
            ///Get Course Id from hidden field
            HiddenField hidCourseID = (HiddenField)dl.FindControl("hidCourseID");
            if (chkApp.Checked == true)
            {
                long courseId = Convert.ToInt64(hidCourseID.Value);
                if (courseId > 0)
                {
                    bool approved = objCourseDAL.ApproveCourseByCourseId(courseId);
                    if (approved)
                    {
                        Course approvedCourse = objCourseDAL.GetCoursesDetailsByID(courseId);
                        string strTitle = approvedCourse.Title;
                        string strHyperlink = approvedCourse.Hyperlink;
                        string strStartDate = approvedCourse.StartDate.ToShortDateString();
                        string strEndDate = approvedCourse.EndDate.ToShortDateString();
                        string strDesc = approvedCourse.Description;
                        string strCity = approvedCourse.City;
                        string strState = approvedCourse.StateProvince;
                        string strDistanceEd = approvedCourse.DistanceEducation;
                        string strSubjects = approvedCourse.Subjects;
                        string strHours = approvedCourse.Hours;
                        string strLearningOutcomes = approvedCourse.LearningOutcomes;
                        ApprovedProviderDataAccess apda = new ApprovedProviderDataAccess();
                        ApprovedProvider ap = apda.GetApprovedProviderByID(approvedCourse.ProviderID);
                        string strSendEmaiTo = ap.ApplicantEmail;

                        StringBuilder strbApprovedEmail = new StringBuilder();
                        string strEmailTitle = "An LA CES Course has been approved";
                        string strBodyIntro = "Congratulations, a course has been approved by LA CES administrators. Please review the information below carefully as it may have been revised during the approval process. <br />";
                        string strBodyEnd = "Please contact <a href=\"mailto:laces@asla.org\"> LA CES administrators </a> with any questions about the course.";

                        strbApprovedEmail.Append(strBodyIntro + "<br />");
                        strbApprovedEmail.Append("Course Title: " + strTitle + "<br />");
                        strbApprovedEmail.Append("Course Hyperlink: " + strHyperlink + "<br />");
                        strbApprovedEmail.Append("Start Date: " + strStartDate + "<br />");
                        strbApprovedEmail.Append("End Date: " + strEndDate + "<br />");
                        strbApprovedEmail.Append("Description: " + strDesc + "<br />");
                        strbApprovedEmail.Append("City: " + strCity + "<br />");
                        strbApprovedEmail.Append("State: " + strState + "<br />");
                        strbApprovedEmail.Append("Distance Education: " + strDistanceEd + "<br />");
                        strbApprovedEmail.Append("Subject: " + strSubjects + "<br />");
                        strbApprovedEmail.Append("Hours: " + strHours + "<br />");
                        strbApprovedEmail.Append("Learning Outcomes: " + strLearningOutcomes + "<br /><br />");
                        strbApprovedEmail.Append(strBodyEnd + "<br />");
                        SmtpClient smtpClient = new SmtpClient();

                        //Get the SMTP server Address from SMTP Web.conf
                        smtpClient.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER);

                        //Get the SMTP port  25;
                        smtpClient.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));

                        //create the message body
                        MailMessage message = new MailMessage();


                        message.From = new MailAddress(LACESUtilities.GetAdminFromEmail());
                        message.To.Add(new MailAddress(strSendEmaiTo));
                        message.Subject = strEmailTitle;
                        message.IsBodyHtml = true;
                        message.Body = strbApprovedEmail.ToString();

                        if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
                        {
                            try
                            {
                                smtpClient.Send(message);
                              //  Response.Write(strSendEmaiTo);
                              //  Response.End();
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        ///Re-load pending courses
        pendingCourses = objCourseDAL.GetAllPendingCourses();
        dlCourseList.DataSource = pendingCourses;
        dlCourseList.DataBind();

        ///Show success message
        lblMsg.Visible = true;
        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = "Selected course(s) approved successfully.";        
    }
    
    /// <summary>
    /// OnItemCreated event call this method to populate formatted "Course Title" and "Provider Name"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dlCourseList_ItemCreated(object sender, DataListItemEventArgs e)
    {
        DataListItem dlItem = e.Item;
        if (dlItem.DataItem != null)
        {
            ///Html encode the title text
            string title = DataBinder.Eval(dlItem.DataItem, "Title").ToString();
            string courseId = DataBinder.Eval(dlItem.DataItem, "ID").ToString();

            ///Display Title linked to Course Details page
            Label lblTitle = (Label)dlItem.FindControl("lblTitle");
            if (lblTitle != null)
            {
                lblTitle.Text = "<a href='CourseDetails.aspx?" + LACESConstant.QueryString.COURSE_ID;
                lblTitle.Text += "=" + courseId + "'>" + Server.HtmlEncode(title) + "</a>";
            }

            ///Html encode the provider name
            string providerName = DataBinder.Eval(dlItem.DataItem, "ProviderName").ToString();
            string providerId = DataBinder.Eval(dlItem.DataItem, "ProviderID").ToString();

            ///Display Provider Name linked to Provider Details
            Label lblProvider = (Label)dlItem.FindControl("lblProvider");
            if (lblProvider != null)
            {
                lblProvider.Text = "<a href='ApprovedProviderDetails.aspx?" + LACESConstant.QueryString.PROVIDER_ID;
                lblProvider.Text += "=" + providerId + "'>" + Server.HtmlEncode(providerName) + "</a>";
            }

            ///Populate Reject Course link
            Label lblRejectCourse = (Label)dlItem.FindControl("lblRejectCourse");

            ///Generating array of course object for javascript
            jsCourseObject += "courseObjectArray[" + itemIndex + "] = new courseObj(\"" + courseId + "\",\"" +
                                title.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("/", "&#47;") + "\", \"" +
                                providerName.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("/", "&#47;") + "\");";
            lblRejectCourse.Text = "<a href=\"#\" onclick=\"RejectCourse('" + itemIndex + "');return false;\">Reject Course</a>";
            itemIndex++;
        }
    }
    //protected void FilterCourses(object sender, EventArgs e)
    //{


    //    string strStatus = "";
    //    foreach (ListItem li in uiChxBoxListStatus.Items)
    //    {
    //        if (li.Selected)
    //        {
    //            strStatus += strStatus.Length == 0 ? li.Value : "," + li.Value;
    //        }
    //    }
    //    if (strStatus.Length == 0)
    //    {
    //        strStatus = "NP,OP,CL,PT,IC";
    //    }

    //    string strStartDate = uiTxtStartDate.Text;
    //    string strEndDate = uiTxtEndDate.Text;
    //    string strKeyword = uiTxtKeyword.Text;
    //    string strState = drpState.SelectedValue;
    //    string strSubject = drpSubject.SelectedValue;
    //    FilterCoursesBind(strStartDate, strEndDate, strKeyword, strState, strSubject, strStatus);

    //}
    //private void populateCourseStatusList(string strFilter)
    //{
    //    CourseStatusDataAccess courseStatusDAL = new CourseStatusDataAccess();
    //    IList<CourseStatus> courseStatusList = courseStatusDAL.GetAllCourseStatus(); // Get All course Status

    //    foreach (CourseStatus courseStatus in courseStatusList)
    //    {
    //        ListItem item = new ListItem(courseStatus.Notes, courseStatus.StatusCode, true);
    //        if (strFilter.Length == 0 || strFilter.IndexOf(courseStatus.StatusCode) >= 0)
    //        {
    //            item.Selected = true;
    //        }
    //        uiChxBoxListStatus.Items.Add(item);

    //    }

    //}
    //protected void FilterCoursesBind(string strStartDate, string strEndDate, string strKeyword, string strState, string strSubject, string strStatus)
    //{
    //    int totalCount = 0;
    //    ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER];
    //    CourseDataAccess objCourseDAL = new CourseDataAccess();
    //    IList<Course> courseResult = new List<Course>();
    //    courseResult = objCourseDAL.GetPagedCourseBySearchAdmin(strStartDate, strEndDate, strKeyword, 0, 10000, strStatus,"", strSubject, strState, "",ref totalCount);
    //    if (courseResult.Count < 1)
    //    {
    //        dlCourseList.Visible = false;
    //        NoResult.Visible = true;
    //    }
    //    else
    //    {
    //        dlCourseList.DataSource = courseResult;
    //        dlCourseList.DataBind();
    //    }

    //}
}
