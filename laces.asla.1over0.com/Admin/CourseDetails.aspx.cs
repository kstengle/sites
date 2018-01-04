/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: CourseDetails.aspx
 * Purpose/Function: Course Detail Page used to Add/Edit Course and course code for Administrator
 *
 * Author: Wasim Majid
 * Version              Author            Date             Reason 
 * 1.0                 Wasim Majid      01/24/2008   Initial development 
 * 1.1                 Matiur Rahman    01/29/2008   Update status and start date, end date according to new business logic(task 5825)
 * 1.2                 Matiur Rahman    02/07/2008   task 5980
 * 1.3                 Matiur Rahman    02/28/2008   Updated health of course (task 6318)
 * 1.4                 Alamgir Hossain  07/05/2008   Work on Enhancement 2    
 --------------------------------------------------------------------------------*/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Xml;

using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
using System.Diagnostics;

/// <summary>
///  Course Detail Page used to Add/Edit Course and course code for Administrator
/// </summary>
public partial class Admin_CourseDetails : AdminBasePage
{
    #region Local Variables
    string courseDesc = string.Empty;       // Used for Course Descrition hidden field
    string courseType = string.Empty;       // Used for Course Type hidden field
    int maxCharecterCount = 3600;           // Maximum characters for Learning Outcome and Description
    int maxCharecterCountRegistrationEligibility = 150;           // Maximum characters for RegistrationEligibility
    string courseTypeValue = string.Empty;  // Used for Prompting user for unsaved Course Code when navigating
    string courseDescValue = string.Empty;  // Used for Prompting user for unsaved Course Code when navigating
    string operation = string.Empty;        // Used for flag add/edit operation
    string delimiter = "<~||>";

    #endregion

    #region Page Load Event Handler
    /// <summary>
    ///  Page Load Event Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // title of the page 
        //this.Title = LACESConstant.LACES_TEXT +" - Course Details";
        
        // Manage participant button is inactive in add mode
        //btnManageParticipant.Enabled = false;
        // disable Save button for Administrator until populate the Course Information

        uiLnkDistanceLearning.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("DistanceEducationPDF"));
        uiLnkHSWClassification.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("HSWClassificationPDF"));
        uiLnkCalculatingPDH.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("CalculatingPDF"));
        uiLnkCourseEquiv.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("CourseEquivPDF"));
        btnSave.Enabled = false;

        if (!IsPostBack)
        {
            //trStatus.Attributes.Add("style", "display:none;"); // Status will not be displayed in Add mode

            populateStateList();         // populate state dropdown menu
            populateSubjectList();       // populate subject dropdown menu
            ///Commentd by Matin according to new business logic
            //populateCourseStatusList();  // populate status dropdown menu
            //populateCourseTypeList();    // populate course type dropdown menu                        
            populateCourseInfo();        // populate course details Information            
        }        

        //string redirectpage = "ManageCodeTypes.aspx";
        //hpRequestNewCodeType.NavigateUrl = "ManageCodeTypes.aspx";
        //hpRequestNewCodeType.Attributes.Add("onclick", "return redirectPage('" + redirectpage + "')");

        initializePage();

        if (Request.Form["hidAction"] != null && !Request.Form["hidAction"].ToString().Equals(string.Empty))
        {
            string redirect = Request.Form["hidAction"].ToString();
            saveInformation();
            Response.Redirect(redirect);
        }
    }
    #endregion

    #region Initialize Page and Attach Javascript Methods of different Control's Event
    /// <summary>
    ///  Initialize Page and Attach Javascript Methods of different Control's Event
    /// </summary>
    private void initializePage()
    {

        courseDesc = hidCourseDesc.Value;
        courseType = hidCourseTypes.Value;
        hidCourseTypes.Value = string.Empty;
        hidCourseDesc.Value = string.Empty;

        // attach events for maximum character counter
        //txtHidType.Attributes.Add("style", "border:1px solid #ffffff;");
        //txtCourseDesc.Attributes.Add("onkeypress", "return AddCodeOnEnterKey(event);");
        btnSave.Attributes.Add("onfocus", "document.getElementById('hidAction').value='';");
        txtLearningOutcome.Attributes.Add("onblur", "textCounter('" + txtLearningOutcome.ClientID + "','" + lblLearningOutcomeCounter.ClientID + "','" + maxCharecterCount + "')");
        txtLearningOutcome.Attributes.Add("oninput", "textCounter('" + txtLearningOutcome.ClientID + "','" + lblLearningOutcomeCounter.ClientID + "','" + maxCharecterCount + "')");
        txtLearningOutcome.Attributes.Add("onkeyup", "textCounter('" + txtLearningOutcome.ClientID + "','" + lblLearningOutcomeCounter.ClientID + "','" + maxCharecterCount + "')");
        txtLearningOutcome.Attributes.Add("onkeydown", "textCounter('" + txtLearningOutcome.ClientID + "','" + lblLearningOutcomeCounter.ClientID + "','" + maxCharecterCount + "')");
        txtLearningOutcome.Attributes.Add("onkeypress", "textCounter('" + txtLearningOutcome.ClientID + "','" + lblLearningOutcomeCounter.ClientID + "','" + maxCharecterCount + "')");
        txtLearningOutcome.Attributes.Add("onmousemove", "textCounter('" + txtLearningOutcome.ClientID + "','" + lblLearningOutcomeCounter.ClientID + "','" + maxCharecterCount + "')");
        txtLearningOutcome.Attributes.Add("onpaste", "setTimeout(\"textCounter('" + txtLearningOutcome.ClientID + "','" + lblLearningOutcomeCounter.ClientID + "','" + maxCharecterCount + "')\", 150);");

        txtRegistrationEligibility.Attributes.Add("onblur", "textCounter('" + txtRegistrationEligibility.ClientID + "','" + lblRegistrationEligibilityCouter.ClientID + "','" + maxCharecterCountRegistrationEligibility + "')");
        txtRegistrationEligibility.Attributes.Add("oninput", "textCounter('" + txtRegistrationEligibility.ClientID + "','" + lblRegistrationEligibilityCouter.ClientID + "','" + maxCharecterCountRegistrationEligibility + "')");
        txtRegistrationEligibility.Attributes.Add("onkeyup", "textCounter('" + txtRegistrationEligibility.ClientID + "','" + lblRegistrationEligibilityCouter.ClientID + "','" + maxCharecterCountRegistrationEligibility + "')");
        txtRegistrationEligibility.Attributes.Add("onkeydown", "textCounter('" + txtRegistrationEligibility.ClientID + "','" + lblRegistrationEligibilityCouter.ClientID + "','" + maxCharecterCountRegistrationEligibility + "')");
        txtRegistrationEligibility.Attributes.Add("onkeypress", "textCounter('" + txtRegistrationEligibility.ClientID + "','" + lblRegistrationEligibilityCouter.ClientID + "','" + maxCharecterCountRegistrationEligibility + "')");
        txtRegistrationEligibility.Attributes.Add("onmousemove", "textCounter('" + txtRegistrationEligibility.ClientID + "','" + lblRegistrationEligibilityCouter.ClientID + "','" + maxCharecterCountRegistrationEligibility + "')");
        txtRegistrationEligibility.Attributes.Add("onpaste", "setTimeout(\"textCounter('" + txtRegistrationEligibility.ClientID + "','" + lblRegistrationEligibilityCouter.ClientID + "','" + maxCharecterCountRegistrationEligibility + "')\", 150);");        

        txtDescription.Attributes.Add("onkeyup", "textCounter('" + txtDescription.ClientID + "','" + lblDescriptionCouter.ClientID + "','" + maxCharecterCount + "')");
        txtDescription.Attributes.Add("onkeydown", "textCounter('" + txtDescription.ClientID + "','" + lblDescriptionCouter.ClientID + "','" + maxCharecterCount + "')");
        txtDescription.Attributes.Add("onkeypress", "textCounter('" + txtDescription.ClientID + "','" + lblDescriptionCouter.ClientID + "','" + maxCharecterCount + "')");
        txtDescription.Attributes.Add("onblur", "textCounter('" + txtDescription.ClientID + "','" + lblDescriptionCouter.ClientID + "','" + maxCharecterCount + "')");
        txtDescription.Attributes.Add("onmousemove", "textCounter('" + txtDescription.ClientID + "','" + lblDescriptionCouter.ClientID + "','" + maxCharecterCount + "')");
        txtDescription.Attributes.Add("oninput", "textCounter('" + txtDescription.ClientID + "','" + lblDescriptionCouter.ClientID + "','" + maxCharecterCount + "')");
        txtDescription.Attributes.Add("onpaste", "setTimeout(\"textCounter('" + txtDescription.ClientID + "','" + lblDescriptionCouter.ClientID + "','" + maxCharecterCount + "')\", 150);");
        //txtDescription.Attributes.Add("onbeforepaste", "textCounter('" + txtDescription.ClientID + "','" + lblDescriptionCouter.ClientID + "','" + maxCharecterCount + "')");

        ///Add event for distance education check box
        chkDistanceEdu.Attributes.Add("onclick", "checkDistanceEducation(this," + txtCity.ClientID + "," + drpState.ClientID + ")");

        //btnSave.Attributes.Add("onclick", "getFinalValue('" + hidCourseTypes.ClientID + "','" + hidCourseDesc.ClientID + "');javascript:needToConfirm = false;setTimeout('resetFlag()', 250);");

        checkUnsaveData();
    }
    #endregion

    #region Check Unsave Information before navigation
    /// <summary>
    /// Check Unsave Information before navigation
    /// </summary>
    private void checkUnsaveData()
    {
        MonitorChangesWithPostBack(txtCity, txtCity.Text);
        MonitorChangesWithPostBack(txtDescription, txtDescription.Text);
        MonitorChangesWithPostBack(txtEndDate, txtEndDate.Text);
        MonitorChangesWithPostBack(txtStartDate, txtStartDate.Text);
        MonitorChangesWithPostBack(txtHyperlink, txtHyperlink.Text);
        MonitorChangesWithPostBack(txtTitle, txtTitle.Text);
        MonitorChangesWithPostBack(txtLearningOutcome, txtLearningOutcome.Text);
        MonitorChangesWithPostBack(txtInstructors, txtInstructors.Text);     
        MonitorChangesWithPostBack(chkDistanceEdu, chkDistanceEdu.Checked.ToString());
        MonitorChangesWithPostBack(drpHours, drpHours.SelectedValue);
        MonitorChangesWithPostBack(drpState, drpState.SelectedValue);
        MonitorChangesWithPostBack(txtRegistrationEligibility, txtRegistrationEligibility.Text);
        MonitorChangesWithPostBack(drpSubject, drpSubject.SelectedValue);        
        MonitorChangesWithPostBack(chkHealth, chkHealth.Checked.ToString());
        //MonitorChangesWithPostBack(txtHidType, txtHidType.Text);
    }
    #endregion

    #region Populate Course Code Information
    /// <summary>
    ///  Populate Course Code Information
    /// </summary>
    /// <param name="courseID"></param>
    private void populateCourseCodeInfo(long courseID)
    {
        CourseCodeDataAccess courseCodeDAL = new CourseCodeDataAccess();
        IList<CourseCode> courseCodeList = courseCodeDAL.GetAllCourseCodeByCourseID(courseID); // Get all Course code of the course

        // On load Script for Populate Course Code Dynamic Table in Edit Mode
        string StartupScriptString = "<script language=\"JavaScript\">";
        StartupScriptString += "function initializeValueOnLoad(){ init(); ";

        foreach (CourseCode courseCode in courseCodeList)
        {
            StartupScriptString += " AddNewCourseCodeTypeRow(\"" + courseCode.Description.Replace("\"", "&quot;").Replace("\\", "\\\\") + "\",\"" + courseCode.CodeType + "\",\"" + courseCode.CodeID + "\",\"" + txtTitle.ClientID + "\"); ";

            // Starts - used for Prompting user for unsaved Course Code when navigating
            if (courseTypeValue == string.Empty)
            {
                courseTypeValue = courseCode.CodeID.ToString();
                courseDescValue = courseCode.Description.Replace("\"", "\"");
            }
            else
            {
                courseTypeValue = courseTypeValue + delimiter + courseCode.CodeID.ToString();
                courseDescValue = courseDescValue + delimiter + courseCode.Description.Replace("\"", "\"");  //&quot;
            }
            //txtHidType.Text = courseTypeValue + courseDescValue;
            // Ends - used for Prompting user for unsaved Course Code when navigating
        }
        StartupScriptString += " } ";
        StartupScriptString += " window.onload = initializeValueOnLoad; ";
        StartupScriptString += " </script> ";

        Page.ClientScript.RegisterStartupScript(System.Type.GetType("System.String"), "PageIni", StartupScriptString);
    }
    #endregion

    #region Get Course Details Information
    /// <summary>
    ///  Get Course Details Information
    /// </summary>
    private void populateCourseInfo()
    {
        long courseID = 0;

        ///Set value to compare for start and end date as current date
        //cvStartDate.ValueToCompare = DateTime.Today.ToString("MM/dd/yyyy");
        //cvEndDate.ValueToCompare = DateTime.Today.ToString("MM/dd/yyyy");

        if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null)
        {
            //in edit mode text of btnSave will be changed
            btnSave.Text = "SAVE CHANGES";

            courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);            
            
            CourseDataAccess courseDAL = new CourseDataAccess();
            Course course = courseDAL.GetCoursesDetailsByID(courseID); // Get Courses Details from Course ID

            if (course == null || course.ID < 1)
            {
                lblMsg.Text = LACESConstant.Messages.COURSE_NOT_FOUND_IN_ADMIN;
                return;
            }

          //  dvCourseDetails.InnerHtml = Server.HtmlEncode(course.Title);//display the title

          //  trTitle.Attributes.Add("style", "display:none;"); //display in title field

            Page.Title = course.Title + " | LA CES™";


            txtTitle.Text = course.Title;
            txtDescription.Text = course.Description;
            txtLearningOutcome.Text = course.LearningOutcomes;
            txtInstructors.Text = course.Instructors;
            txtHyperlink.Text = course.Hyperlink;
            if (course.Hyperlink.IndexOf("@") > 0 && course.Hyperlink.IndexOf(".") > 0)
            {
                Label note = new Label();
                note.ForeColor =System.Drawing.Color.Red;
                note.Text = "This will be rendered as an email";
                uiPhHyperlinkNote.Controls.Add(note);
            }




            txtStartDate.Text = course.StartDate.ToString("MM/dd/yyyy");
            txtEndDate.Text = course.EndDate.ToString("MM/dd/yyyy");
            chkDistanceEdu.Checked = (course.DistanceEducation.ToLower()[0] == 'y') ? true : false;
            if (chkDistanceEdu.Checked)
            {
                txtCity.Enabled = false;
                drpState.Enabled = false;
            }
            else
            {
                txtCity.Text = course.City;
                drpState.SelectedValue = course.StateProvince.Trim();
            }
            if (course.CourseEquivalency.Length > 0)
            {
                chkCourseEquivalency.Checked = (course.CourseEquivalency.ToLower()[0] == 'y') ? true : false;
            }
            string tempFullTime = course.Hours.ToString();
            string tempMins = "";
            string tempHours;
            if (tempFullTime.Length > 0)
            {
                
                if (tempFullTime.IndexOf(".") > 0)
                {
                    tempHours = tempFullTime.Substring(0, tempFullTime.IndexOf("."));
                    if (tempFullTime.Length - (tempFullTime.IndexOf(".") + 1) == 2)
                    {
                        tempMins = tempFullTime.Substring(tempFullTime.IndexOf(".") + 1, 2);
                    }
                    else if (tempFullTime.Length - (tempFullTime.IndexOf(".") + 1) == 1)
                    {
                        tempMins = tempFullTime.Substring(tempFullTime.IndexOf(".") + 1, 1) + "0";
                    }
                }
                else
                {
                    tempHours = tempFullTime;
                }
                
               
                drpHours.SelectedValue = tempHours;
                drpMinutes.SelectedValue = tempMins;
            }
            ViewState["ProviderID"] = course.ProviderID.ToString();

            bool statusSelected = false;            

            txtRegistrationEligibility.Text = course.RegistrationEligibility;
            // Update counter Text
            int remainCharacterRegistrationEligibility = maxCharecterCountRegistrationEligibility - txtRegistrationEligibility.Text.Length;
            lblRegistrationEligibilityCouter.Text = remainCharacterRegistrationEligibility + "&nbsp;&nbsp;  character(s) remains.";

            // Populate Informations for subject drop down menu
            string[] subjectList = course.Subjects.Split(',');
            if (course.Subjects != string.Empty)
            {
                for (int i = 0; i < drpSubject.Items.Count; i++)
                {
                    for (int j = 0; j < subjectList.Length; j++)
                    {
                        if (drpSubject.Items[i].Text.Trim() == subjectList[j].Trim())
                        {
                            drpSubject.Items[i].Selected = true;
                        }
                    }
                }
            }

            ///Update this section by Matin according to task 6318
            // Populate Informations for Health check box
            chkHealth.Checked = (course.Healths.ToLower() == "yes") ? true : false;
            uiChkNoProprietaryInfo.Checked = (course.NoProprietaryInfo.ToLower() == "y") ? true : false;
            uiTxtCustomProviderCode.Text = course.ProviderCustomCourseCode;
            //string[] heathList = course.Healths.Split(',');
            //if (course.Healths != string.Empty)
            //{
            //    for (int i = 0; i < chkHealthList.Items.Count; i++)
            //    {
            //        for (int j = 0; j < heathList.Length; j++)
            //        {
            //            if (chkHealthList.Items[i].Text == heathList[j].Trim())
            //            {
            //                chkHealthList.Items[i].Selected = true;
            //            }
            //        }
            //    }
            //}

            // Update counter Text
            int remainCharacter = maxCharecterCount - txtDescription.Text.Length;
            lblDescriptionCouter.Text = remainCharacter + "&nbsp;&nbsp;  character(s) remains.";
            remainCharacter = maxCharecterCount - txtLearningOutcome.Text.Length;
            lblLearningOutcomeCounter.Text = remainCharacter + "&nbsp;&nbsp;  character(s) remains.";
                        
            // Update Course ID hidden field with CourseID
            hidCourseID.Value = course.ID.ToString();

            // Populate Course Code Information
            populateCourseCodeInfo(courseID);

            // Populate the participants of the course
            populateParticipantList(courseID);

            // title of the page in edit mode
            //this.Title = LACESConstant.LACES_TEXT + " - Course Details: " + Server.HtmlEncode(course.Title);

            // Manage participant button is enable in edit mode
            //btnManageParticipant.Enabled = true;
            // Enable Save button for Administrator    
            uiHidLastStatus.Value = course.Status;
            foreach (ListItem i in uiRblStatus.Items)
            {
                if (i.Text == course.Status)
                {
                    i.Selected = true;
                   
                }
            }
            btnSave.Enabled = true;

            //btnManageParticipant.Attributes.Add("onclick", "return confirmonCloseLink(\"You have made changes to the course details. Would you like to save them before Managing Participants?\",'ManageParticipants.aspx?CourseID=" + courseID + "')");

            //string[] orgList = splitString(course.OrgStateCourseIDNumber);
            //txtOrgStateDiv.InnerHtml = "";
            //for (int i = 1; i < orgList.Length; i++)
            //{
            //    txtOrgStateDiv.InnerHtml += "<div class='txtOrgState'><input name='txtOrgState' type='text' style='display:block; width:209px' class='frmDateBox' value=" + Server.HtmlEncode(orgList[i]) + " /></div>";
            //}
        }
        else ///If want to add new course
        {
           // dvCourseDetails.InnerHtml = "Add a Course";

            Page.Title = "Add a Course" + " | LA CES™"; ;

            txtTitle.Focus();

            //Initialize value for start and end date
            txtStartDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            txtEndDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
        }
    }
    #endregion

    #region Populate the participants of the course
    /// <summary>
    ///  Populate the participants of the course
    /// </summary>
    /// <param name="courseID"></param>
    private void populateParticipantList(long courseID)
    {
        ParticipantDataAccess participantDAL = new ParticipantDataAccess();
        IList<Participant> participantList = participantDAL.GetAllParticipantByCourse(courseID); // Get Participants of the course        

        ////EditParticipants.aspx?ParticipantID=53&CourseID=54

        //dvParticipantList.InnerHtml = "Participant(s): " + participantList.Count.ToString();

        //if (participantList.Count > 0)
        //{
        //    dvParticipantList.InnerHtml = "<table width='100%' cellpadding='3' cellspacing='0'><tr><td width='50%' class='participantList' align='right'><b>Last Name, </b></td><td class='participantList' left='right'><b>First Name</b></td></tr>";
        //    foreach (Participant participant in participantList)
        //    {
        //        dvParticipantList.InnerHtml += "<tr><td width='50%' align='right' valign='top'><a onclick='return confirmonCloseLink(\"You have made changes to the course details. Would you like to save them before editing this individual participant?\",\"EditParticipants.aspx?ParticipantID=" + participant.ID.ToString() + "&CourseID=" + courseID.ToString() + "\");' href='EditParticipants.aspx?ParticipantID=" + participant.ID.ToString() + "&CourseID=" + courseID.ToString() + "'>" + Server.HtmlEncode(participant.LastName) + "</a>,</td><td valign='top'>" + Server.HtmlEncode(participant.FirstName) + " &nbsp; <small><a href='ParticipantCourses.aspx?ParticipantID=" + participant.ID.ToString() + "'>[courses]</a></small></td></tr>";
        //    }

        //    dvParticipantList.InnerHtml += "</table>";
        //}
    }
    #endregion

    #region Populate State List
    /// <summary>
    ///  Populate State List 
    /// </summary>
    private void populateStateList()
    {
        StateDataAccess stateDAL = new StateDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int StateProvidenceID = int.Parse(LACESUtilities.GetApplicationConstants("StateProvinceContentID"));
        IList<State> stateList = stateDAL.GetAllStates(StateProvidenceID, webroot); // Get all States 

        ListItem defaultItem = new ListItem("-State List-", "");
        drpState.Items.Add(defaultItem);

        foreach (State state in stateList)
        {
            ListItem item = new ListItem(state.StateName, state.StateCode);
            drpState.Items.Add(item);
        }
        drpState.Items.Add(new ListItem("- Other International -", "OL"));
    }
    #endregion

    #region Populate Subject List
    /// <summary>
    ///  Populate Subject List 
    /// </summary>
    private void populateSubjectList()
    {
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int SubjectID = int.Parse(LACESUtilities.GetApplicationConstants("SubjectContentID"));
        SubjectDataAccess subjectDAL = new SubjectDataAccess();
        IList<Subject> subjectList = subjectDAL.GetAllSubjects(SubjectID, webroot); // Get all Subjects

        foreach (Subject subject in subjectList)
        {
            ListItem item = new ListItem(subject.SubjectName, subject.SubjectID.ToString());
            drpSubject.Items.Add(item);
        }
    }
    #endregion

    #region Populate Course Status List
    /// <summary>
    ///  Populate Course Status List
    /// </summary>
    //private void populateCourseStatusList()
    //{
    //    CourseStatusDataAccess courseStatusDAL = new CourseStatusDataAccess();
    //    IList<CourseStatus> courseStatusList = courseStatusDAL.GetAllCourseStatus(); // Get All course Status
        
    //    foreach (CourseStatus courseStatus in courseStatusList)
    //    {
    //        ListItem item = new ListItem(courseStatus.Notes, courseStatus.StatusCode);
    //        rdoStatusList.Items.Add(item);
    //    }
        
    //}
    #endregion

    #region Populate Course Type List
    /// <summary>
    ///  Populate Course Type List 
    /// </summary>
    //private void populateCourseTypeList()
    //{
    //    CourseTypeDataAccess courseTypeDAL = new CourseTypeDataAccess();
    //    IList<CourseCodeType> courseTypeList = courseTypeDAL.GetAllCourseCodeTypes(); // Get All Course Code

    //    foreach (CourseCodeType courseCodeType in courseTypeList)
    //    {
    //        ListItem item = new ListItem(courseCodeType.CodeType, courseCodeType.ID.ToString());
    //        drpCourseType.Items.Add(item);
    //    }


    //    //if corse code available then visible the corse code table
    //    //if (courseTypeList.Count > 0)
    //    //{
    //    //    corseCodeTable.Style["display"] = "block";
    //    //}


    //    //drpCourseType.Items.Add(new ListItem("- Other International -", "OL"));
    //}
    #endregion

    #region Save Course Information
    /// <summary>
    ///  Save Course Information
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInformation();
        Response.Redirect("CourseSavedSuccessfully.aspx?CourseOperation=" + operation + ""); // redirect after saving course information
    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null
                && Request.QueryString[LACESConstant.QueryString.COURSE_ID].ToString() != "")
        {
            
            CourseDataAccess objCourseDAL = new CourseDataAccess();
            long courseId = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID].ToString());
            Course deletingCourse = objCourseDAL.GetCoursesDetailsByID(courseId);
            string coursetitle = deletingCourse.Title;
            string strSubject = coursetitle + " – Course Declined";
           
            ApprovedProviderDataAccess apda = new ApprovedProviderDataAccess();
            ApprovedProvider ap = apda.GetApprovedProviderByID(deletingCourse.ProviderID);
            string strSendEmaiTo = ap.ApplicantEmail;

            bool deleted = objCourseDAL.Delete(courseId);//call dal method to delete
            SendDeleteForeverEmail(strSendEmaiTo,strSubject, coursetitle);
            Response.Redirect("FindCourses.aspx?status=NP"); // redirect after saving course information
        }
    
    }
    protected void SendDeleteForeverEmail(string strSendEmailTo, string subject, string title)
    {
        StringBuilder strbDeletedBody = new StringBuilder();
        strbDeletedBody.Append("Dear LA CES Provider, <br /><br />");
        strbDeletedBody.Append("Thank you for submitting " + title + " to LA CES.  Unfortunately, we are unable to approve this course at this time as it does not meet LA CES <a href=\"http://laces.asla.org/ApprovedProviderGuidelines.aspx\">guidelines</a>.  We encourage you to resubmit the course after revising it to meet LA CES guidelines. Please contact us at <a href=\"mailto:laces@asla.org\">laces@asla.org</a> with any questions or for more information. <br /><br />");

        strbDeletedBody.Append("Sincerely,<br /><br />");

        strbDeletedBody.Append("LA CES Administrator");

        SmtpClient smtpClient = new SmtpClient();

        //Get the SMTP server Address from SMTP Web.conf
        smtpClient.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER);

        //Get the SMTP port  25;
        smtpClient.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));

        //create the message body
        MailMessage message = new MailMessage();


        message.From = new MailAddress(LACESUtilities.GetAdminFromEmail());
        message.To.Add(new MailAddress(strSendEmailTo));
        message.CC.Add(new MailAddress(LACESUtilities.GetApplicationConstants(LACESConstant.ADMIN_CONTACT_TO_EMAIL)));
        message.Subject = subject;
        message.IsBodyHtml = true;
        message.Body = strbDeletedBody.ToString();

        if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
        {
            try
            {
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    private void saveInformation()
    {
        try
        {
            Course course = new Course();
            course.Title = txtTitle.Text.Trim();
            course.Hyperlink = txtHyperlink.Text.Trim();
            course.StartDate = (txtStartDate.Text.Trim() == string.Empty) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(txtStartDate.Text.Trim());
            course.EndDate = (txtEndDate.Text.Trim() == string.Empty) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(txtEndDate.Text.Trim());
            course.Description = (txtDescription.Text.Length > 3600) ? txtDescription.Text.Substring(0, 3600) : txtDescription.Text;
            course.DistanceEducation = (chkDistanceEdu.Checked == true) ? "Y" : "N";
            course.CourseEquivalency = (chkCourseEquivalency.Checked == true) ? "Y" : "N";
            course.ProviderCustomCourseCode = uiTxtCustomProviderCode.Text;
            //string retValue = "";

            //for (int i = 0; i < Request.Form.GetValues("txtOrgState").Length; i++)
            //{
            //    if (Request.Form.GetValues("txtOrgState")[i].Trim() != "")
            //        retValue += delimiter + Request.Form.GetValues("txtOrgState")[i];
            //}

            //course.OrgStateCourseIDNumber = retValue;



            if (chkDistanceEdu.Checked == false)
            {
                course.City = txtCity.Text.Trim();
                course.StateProvince = drpState.SelectedValue.Trim();
            }

            course.Hours = drpHours.SelectedValue + "." + drpMinutes.SelectedValue;
            course.LearningOutcomes = (txtLearningOutcome.Text.Length > 3600) ? txtLearningOutcome.Text.Substring(0, 3600) : txtLearningOutcome.Text;
            course.Instructors = txtInstructors.Text.Trim();
            course.RegistrationEligibility = (txtRegistrationEligibility.Text.Length > maxCharecterCountRegistrationEligibility) ? txtRegistrationEligibility.Text.Substring(0, maxCharecterCountRegistrationEligibility) : txtRegistrationEligibility.Text;


            string subject = string.Empty;
            // populate string from selected Items
            for (int i = 0; i < drpSubject.Items.Count; i++)
            {
                if (drpSubject.Items[i].Selected == true)
                {
                    if (subject == string.Empty)
                    {
                        subject = drpSubject.Items[i].Text;
                    }
                    else
                    {
                        subject = subject + ", " + drpSubject.Items[i].Text.Trim();
                    }
                }
            }
            course.Subjects = subject;

            ///Update this section by Matin according to task 6318
            //string health = string.Empty;
            // populate string from selected Items
            course.Healths = (chkHealth.Checked == true) ? "Yes" : "No";

            //for (int i = 0; i < chkHealthList.Items.Count; i++)
            //{
            //    if (chkHealthList.Items[i].Selected == true)
            //    {
            //        if (health == string.Empty)
            //        {
            //            health = chkHealthList.Items[i].Value;
            //        }
            //        else
            //        {
            //            health = health + ", " + chkHealthList.Items[i].Value;
            //        }
            //    }
            //}
            //course.Healths = health;
            course.Hours = drpHours.SelectedValue + "." + drpMinutes.SelectedValue;
            course.ProviderID = Convert.ToInt64(ViewState["ProviderID"]);
            course.NoProprietaryInfo = (uiChkNoProprietaryInfo.Checked == true) ? "Y" : "N";
            course.Status = uiRblStatus.SelectedValue;
            CourseDataAccess courseDAL = new CourseDataAccess();
            if (Convert.ToInt64(hidCourseID.Value) > 0)
            {
                ///Added by Matin according to new business logic              
                course.ID = Convert.ToInt64(hidCourseID.Value);
                courseDAL.UpdateCourseDetails(course); // Update course information
                operation = "update";
            }
            else
            {
                course.Status = "NP";
                course.ID = courseDAL.AddCourseDetails(course); // Add course information
                operation = "add";
            }
            updateCourseCode(course.ID);
            string oldStatus = uiHidLastStatus.Value;
            if (course.Status == "IC" && uiTxtEmailText.Text.Length>0)
            {
                SendMoreDataNeededEmail(course);
            }
            else if (course.Status == "OP" && oldStatus != "OP")
            {
                SendApprovedEmail(course.ID);
            }

            //Response.Redirect("CourseList.aspx"); // redirect after saving course information
            Session["AdminCourseName"] = course.Title;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Return string array by spliting the string by a delimiter
    /// <summary>
    /// Return string array by spliting the string by a delimiter
    /// </summary>
    /// <param name="stringList">string</param>
    /// <returns>Array of String</returns>
    /// 
    protected void SendMoreDataNeededEmail(Course c)
    {
       string strMessageBody = uiTxtEmailText.Text.Replace("\r\n", "<br />");
        ApprovedProviderDataAccess apda = new ApprovedProviderDataAccess();
        ApprovedProvider ap = apda.GetApprovedProviderByID(c.ProviderID);
        string strSendEmaiTo = ap.ApplicantEmail;
        SmtpClient smtpClient = new SmtpClient();

        //Get the SMTP server Address from SMTP Web.conf
        smtpClient.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER);

        //Get the SMTP port  25;
        smtpClient.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));

        //create the message body
        MailMessage message = new MailMessage();


        message.From = new MailAddress(LACESUtilities.GetAdminFromEmail());
        message.To.Add(new MailAddress(strSendEmaiTo));
        message.CC.Add(new MailAddress(LACESUtilities.GetApplicationConstants(LACESConstant.ADMIN_CONTACT_TO_EMAIL)));
        message.Subject = "LA CES Course Needs More Information";
        message.IsBodyHtml = true;
        message.Body = strMessageBody;

        if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
        {
            try
            {
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    protected void SendApprovedEmail(long courseId)
    {
        CourseDataAccess objCourseDAL = new CourseDataAccess();
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
                               
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }    

    private string[] splitString(string stringList)
    {
        IList<string> stringArray = new List<string>();
        while (stringList.IndexOf(delimiter) >= 0)
        {
            stringArray.Add(stringList.Substring(0, stringList.IndexOf(delimiter)));
            stringList = stringList.Substring(stringList.IndexOf(delimiter) + delimiter.Length);
        }

        //if (!stringList.Trim().Equals(string.Empty))
        {
            stringArray.Add(stringList);
        }

        string[] retString = new string[stringArray.Count];

        int count = 0;
        foreach (string str in stringArray)
        {
            retString[count] = str;
            count++;
        }

        return retString;
    }
    #endregion

    #region Update Course Code of the Course
    /// <summary>
    ///  Update Course Code of the Course
    /// </summary>
    /// <param name="courseID">CourseId</param>
    private void updateCourseCode(long courseID)
    {
        CourseCodeDataAccess codeDAL = new CourseCodeDataAccess();
        codeDAL.DeleteCourseCodeByCourseID(courseID); // Delete course codes of the Course

        string[] descList = splitString(courseDesc);
        string[] typeList = splitString(courseType);
        //string[] typeList = courseType.Split('|');

        if (!courseType.Equals(string.Empty))
        {
            for (int i = 0; i < typeList.Length; i++)
            {
                CourseCode code = new CourseCode();
                code.Description = descList[i];
                code.CodeID = Convert.ToInt32(typeList[i]);
                code.CourseID = courseID;
                try
                {
                    codeDAL.AddCourseCode(code); // Add course Code
                }
                catch (Exception ex)
                {
                    Session["AdminCourseCodeFailed"] = "yes";
                }
            }
        }
        populateCourseCodeInfo(courseID); // on Load script for populating the Course code after page refresh.
    }
    #endregion

    #region Manage Participant Click Event
    /// <summary>
    ///  Manage Participant Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnManageParticipant_Click(object sender, EventArgs e)
    {
        if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null)
        {
            string strCourseID = Request.QueryString[LACESConstant.QueryString.COURSE_ID];
            Response.Redirect("UploadParticipants.aspx?" + LACESConstant.QueryString.COURSE_ID + "=" + strCourseID);
        }
    }
    #endregion

    protected string GetPDFURL(string PDFLibraryID)
    {
        string pdfurl = "";
        XmlDocument GetPdfXML = new XmlDocument();
        GetPdfXML.Load(LACESUtilities.GetApplicationConstants("URLToGetPDFFilename") + "?id=" + PDFLibraryID);
        pdfurl = LACESUtilities.GetApplicationConstants("ASLABaseURL") + GetPdfXML.SelectSingleNode("root/filename").InnerText; ;


        return pdfurl;

    }   
}
