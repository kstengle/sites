using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pantheon.ASLA.LACES.DataAccess;
using Pantheon.ASLA.LACES.Common;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net.Mail;
using System.Configuration;
public partial class Admin_FindCourses : AdminBasePage
{

    protected IList<Course> pendingCourses = new List<Course>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            populateProviders();
            populateStateList();
            string statusList = "";
            foreach (ListItem li in uiChkSubmitType.Items)
            {
                li.Selected = true;
                
            }
            if (Request.QueryString["status"] != null)
            {
                statusList = Request.QueryString["status"];
                if (statusList == "NP")
                {
                    uiLitHeaderText.Text = "Pending Courses";
                }
            }
            else
            {
                foreach (ListItem li in uiChxBoxListStatus.Items)
                {                
                    if (statusList.Length == 0)
                    {
                        statusList += li.Value;
                    }
                    else
                    {
                        statusList += "," + li.Value;
                    }
                }
            }
            populateCourseStatusList(statusList);
            populateStateList();
            FilterCoursesBind("", "", "", "", "", statusList, "","");
        }
    }
    private void populateProviders()
    {
        //get all approved providers
        IList<ApprovedProvider> oApprovedProviders;
        ApprovedProviderDataAccess oApprovedProviderDataAccess = new ApprovedProviderDataAccess();
        int totalCount = 0;
        oApprovedProviders = oApprovedProviderDataAccess.GetPagedApprovedProviderSearch("1=1", 0, Int16.MaxValue, "Status, OrganizationName", "asc", ref totalCount);

        foreach (ApprovedProvider ap in oApprovedProviders)
        {
            try
            {
                string orgName = ap.OrganizationName;
                string orgStatus = ap.Status;
                if (orgName.Length > 50)
                    orgName = orgName.Substring(0, 47) + "...";
                if (orgStatus != "Active")
                {
                    orgName = orgStatus + " - " + orgName;
                }
                lbEducationProvider.Items.Add(new ListItem(orgName, ap.ID.ToString()));
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }
        }
    }
    private void populateStateList()
    {
        StateDataAccess stateDAL = new StateDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int StateProvidenceID = int.Parse(LACESUtilities.GetApplicationConstants("StateProvinceContentID"));
        IList<State> stateList = stateDAL.GetAllStates(StateProvidenceID, webroot); // Get all States 

        ListItem defaultItem = new ListItem("- State List -", "");
        drpState.Items.Add(defaultItem);

        foreach (State state in stateList)
        {
            ListItem item = new ListItem(state.StateName, state.StateCode);
            drpState.Items.Add(item);
        }

        //for other International value
        drpState.Items.Add(new ListItem("- Other International -", "OI"));
    }
    protected void dlCourseList_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem dlItem = e.Item;
        if (dlItem.DataItem != null)
        {
            ///Populate Start Date field
            Label lblStartDate = (Label)dlItem.FindControl("lblStartDate");
            if (lblStartDate != null)
            {
                DateTime startDate = Convert.ToDateTime(DataBinder.Eval(dlItem.DataItem, "StartDate"));
                lblStartDate.Text = startDate.ToString("MM/dd/yyyy");
                //if (DateTime.Today >= startDate)
                //    lblStartDate.CssClass = "dateBold";
            }

            ///Populate End Date field
            Label lblEndDate = (Label)dlItem.FindControl("lblEndDate");
            if (lblEndDate != null)
            {
                DateTime endDate = Convert.ToDateTime(DataBinder.Eval(dlItem.DataItem, "EndDate"));
                lblEndDate.Text = endDate.ToString("MM/dd/yyyy");
                //if (DateTime.Today >= endDate)
                //    lblEndDate.CssClass = "dateBold";
            }
            ///Populate Provider
            Label lblProviderName = (Label)dlItem.FindControl("lblProviderName");
            if (lblProviderName != null)
            {
                string ProviderName = Server.HtmlEncode(DataBinder.Eval(dlItem.DataItem, "ProviderName").ToString());
                lblProviderName.Text = ProviderName;

            }

            ///Populate Course Title linked to Course Details page
            Label lblTitle = (Label)dlItem.FindControl("lblTitle");
            if (lblTitle != null)
            {
                ///Html encode the title text
                string title = Server.HtmlEncode(DataBinder.Eval(dlItem.DataItem, "Title").ToString());
                string courseId = DataBinder.Eval(dlItem.DataItem, "ID").ToString();
                lblTitle.Text = "<a href='CourseDetails.aspx?CourseID=" + courseId + "'>" + title + "</a>";
            }

            Label lblStatus = (Label)dlItem.FindControl("lblStatus");
            string curStatus = "";
            switch (DataBinder.Eval(dlItem.DataItem, "Status").ToString())
            {
                case "OP":
                    curStatus="Active";
                    break;
                case "CL":
                    curStatus="Closed";
                    break;
                case "IC":
                    curStatus="Action Required";
                    break;
                case "NP":
                    curStatus="Pending Approval";
                    break;
                case "PT":
                    curStatus = "Archived";
                    break;
            }
            lblStatus.Text = curStatus;


        }
    }

    /// <summary>
    /// Get formatted location string with city and state
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
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
    private void populateCourseStatusList(string strFilter)
    {
        CourseStatusDataAccess courseStatusDAL = new CourseStatusDataAccess();
        IList<CourseStatus> courseStatusList = courseStatusDAL.GetAllCourseStatus(); // Get All course Status

        foreach (CourseStatus courseStatus in courseStatusList)
        {
            ListItem item = new ListItem(courseStatus.Notes, courseStatus.StatusCode, true);
            if (strFilter.Length == 0 || strFilter.IndexOf(courseStatus.StatusCode) >= 0)
            {
                item.Selected = true;
            }
            uiChxBoxListStatus.Items.Add(item);

        }

    }
    protected void FilterCourses(object sender, EventArgs e)
    {
        dlCourseList.DataSource = null;
        dlCourseList.DataBind();

        string strStatus = "";
        foreach (ListItem li in uiChxBoxListStatus.Items)
        {
            if (li.Selected)
            {
                strStatus += strStatus.Length == 0 ? li.Value : "," + li.Value;
            }
        }
        string courseType="";
        foreach (ListItem li in uiChkSubmitType.Items)
        {
            if (li.Selected)
            {
                courseType += courseType.Length == 0 ? li.Value : "," + li.Value;
            }
        }
        if (courseType == "S,M")
        {
            courseType = "";
        }
        string strProviderList = "";
        foreach (ListItem li in lbEducationProvider.Items)
        {
            if (li.Selected)
            {
                strProviderList += strProviderList.Length == 0 ? li.Value : "," + li.Value;
            }
        }
        string strStartDate = uiTxtStartDate.Text;
        string strEndDate = uiTxtEndDate.Text;
        string strKeyword = uiTxtKeyword.Text;
        string strState = drpState.SelectedValue;
        string strSubject = "";
        FilterCoursesBind(strStartDate, strEndDate, strKeyword, strState, strSubject, strStatus, strProviderList, courseType);

    }
    protected void FilterCoursesBind(string strStartDate, string strEndDate, string strKeyword, string strState, string strSubject, string strStatus, string strProviderList, string courseType)
    {
        try
        {
            int totalCount = 0;
            ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER];
            CourseDataAccess objCourseDAL = new CourseDataAccess();
            IList<Course> courseResult = new List<Course>();
            courseResult = objCourseDAL.GetPagedCourseBySearchAdmin(strStartDate, strEndDate, strKeyword, 0, 10000, strStatus, strProviderList, strSubject, strState, courseType, ref totalCount);
            if (courseResult.Count < 1)
            {
                dlCourseList.Visible = false;
                NoResult.Visible = true;
            }
            else
            {

                dlCourseList.DataSource = courseResult;
                dlCourseList.DataBind();
                dlCourseList.Visible = true;
                NoResult.Visible = false;
            }
        }
        catch (Exception ex)
        {
        }
    }
    #region btnStatus_OnClick

    /// <summary>
    /// Call every time when click on the open close link button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnStatus_OnClick(object sender, EventArgs e)
    {
        LinkButton openCloseButton = sender as LinkButton;

        try
        {
            long courseID = Convert.ToInt32(openCloseButton.CommandArgument);
            string status = openCloseButton.CommandName;

            CourseDataAccess oCourseDataAccess = new CourseDataAccess();

            if (status == "Open")
            {
                oCourseDataAccess.ChangeCourseStatus(courseID, "Closed");
            }
            else if (status == "Closed")
            {
                oCourseDataAccess.ChangeCourseStatus(courseID, "Open");
            }
        }
        catch
        {
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnReplicate_OnClick(object sender, EventArgs e)
    {
        LinkButton openCloseButton = sender as LinkButton;

        try
        {
            long courseID = Convert.ToInt32(openCloseButton.CommandArgument);
            string status = openCloseButton.CommandName;

            CourseDataAccess oCourseDataAccess = new CourseDataAccess();

            if (status == "Replicate")
            {
                Session["ReplicateCourseID"] = courseID.ToString();
                Response.Redirect("coursedetails.aspx?ReplicateCourse=Y");
            }
        }
        catch
        {
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    #endregion
    #region btnStatus_OnClick

    /// <summary>
    /// Call every time when click on the open close link button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnActive_OnClick(object sender, EventArgs e)
    {
        LinkButton openCloseButton = sender as LinkButton;

        try
        {
            long courseID = Convert.ToInt32(openCloseButton.CommandArgument);
            string status = openCloseButton.CommandName;

            CourseDataAccess oCourseDataAccess = new CourseDataAccess();

            if (status == "T")
            {
                oCourseDataAccess.ChangeCourseActive(courseID, "F");
            }
            else if (status == "F")
            {
                oCourseDataAccess.ChangeCourseActive(courseID, "T");
            }
        }
        catch
        {
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    #endregion
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        CourseDataAccess objCourseDAL = new CourseDataAccess();

        ///Loop all items in data list to check approve checkbox for approve one course
        foreach (RepeaterItem dl in dlCourseList.Items)
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
                               // smtpClient.Send(message);
                               
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
    
}