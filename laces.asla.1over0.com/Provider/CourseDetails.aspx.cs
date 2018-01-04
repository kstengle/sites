/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: CourseDetails.aspx
 * Purpose/Function: This is Details Page of Course
 *
 * Author: Wasim Majid
 * Version              Author            Date             Reason 
 * 1.0                 Wasim Majid      01/15/2008   Initial development
 * 1.1                 Tarek Jubaer     01/22/2008   Request New Code (Task 5466)
 * 1.2                 Matiur Rahman    01/29/2008   Update status and start date, end date according to new business logic(task 5825)
 * 1.3                 Matiur Rahman    02/07/2008   task 5980
 * 1.4                 Matiur Rahman    02/28/2008   Updated health of course (task 6318)
 * 1.5                 Alamgir Hossain  07/09/2008   Work on Enhancement 2   
 * 1.6                 Md. kamruzzaman  09/08/2009   Changed Course Notification Mail To Address
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
///  Course Detail Page used to Add/Edit Course and course code for Provider
/// </summary>
public partial class CourseDetails : ProviderBasePage
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
        //this.Title = LACESConstant.LACES_TEXT + " - Course Details";

        // Manage participant button is inactive in add mode
        //btnManageParticipant.Enabled = false;
        uiLnkDistanceLearning.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("DistanceEducationPDF"));
        uiLnkCourseEquiv.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("CourseEquivPDF"));
        uiLnkHSWClassification.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("HSWClassificationPDF"));
        uiLnkCalculatingPDH.HRef = GetPDFURL(LACESUtilities.GetApplicationConstants("CalculatingPDF"));
        if (!IsPostBack)
        {
            trStatus.Attributes.Add("style", "display:none;"); // Status will not be displayed in Add mode

            populateStateList();         // populate state dropdown menu
            populateSubjectList();       // populate subject dropdown menu
          //  populateCourseTypeList();    // populate course type dropdown menu                        
            populateCourseInfo();        // populate course details Information
            if (Session["ReplicateCourseID"] != null && (Request.QueryString["ReplicateCourse"] == "Y" || Request.QueryString["RenewCourse"] == "Y"))
            {
                ReplicateCourse();
                if(Request.QueryString["RenewCourse"] == "Y")
                {
                    uiHdnCourseToRenew.Value = Session["ReplicateCourseID"].ToString();
                    DisableFieldsForAutoApproval();
                }
            }


        }

        initializePage();
        /**
         * Start v1.1
         * If course id is found in URL set request course code type hyperlink to RequestCourseCode.aspx with course id
         * else set request course code type hyperlink to RequestCourseCode.aspx page
         * */
        //string redirectpage = "RequestCourseCode.aspx";
        //if (Request.QueryString["CourseID"] != null)   
        //{
        //    hpRequestNewCodeType.NavigateUrl = "RequestCourseCode.aspx?CourseId=" + Request.QueryString["CourseID"].ToString();
        //    redirectpage= "RequestCourseCode.aspx?CourseId=" + Request.QueryString["CourseID"].ToString();
        //}
        //else
        //    hpRequestNewCodeType.NavigateUrl = "RequestCourseCode.aspx";

        /**
         * End v1.1
         **/

        //hpRequestNewCodeType.Attributes.Add("onclick", "return confirmonCloseLink(\"You have made changes to the course details. Would you like to save them before requesting a new code?\",'" + redirectpage + "')");
        //hpRequestNewCodeType.Attributes.Add("onclick", "return redirectPage('" + redirectpage + "')");
        
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
        txtHidType.Attributes.Add("style", "border:0px solid #ffffff;");
        btnSave.Attributes.Add("onfocus", "document.getElementById('hidAction').value='';");
        
        //txtCourseDesc.Attributes.Add("onkeypress", "return AddCodeOnEnterKey(event);");        
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

        //btnSave.Attributes.Add("onclick", ");


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
        MonitorChangesWithPostBack(rdoStatusList, rdoStatusList.SelectedValue);
        MonitorChangesWithPostBack(chkHealth, chkHealth.Checked.ToString());
        MonitorChangesWithPostBack(txtHidType, txtHidType.Text);
        MonitorChangesWithPostBack(chkCourseEquivalency, chkCourseEquivalency.Checked.ToString());
        
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
            StartupScriptString += " AddNewCourseCodeTypeRow(\"" + courseCode.Description.Replace("\"", "&quot;").Replace("\\","\\\\") + "\",\"" + courseCode.CodeType + "\",\"" + courseCode.CodeID + "\",\"" + txtTitle.ClientID + "\"); ";

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
            txtHidType.Text = courseTypeValue + courseDescValue;
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
    /// 
    private void DisableFieldsForAutoApproval()
    {
        txtTitle.Enabled = false;

        txtRegistrationEligibility.Enabled = false;
        txtDescription.Enabled = false;
        chkDistanceEdu.Enabled = false;
        chkCourseEquivalency.Enabled = false;
        drpSubject.Enabled = false;
        chkHealth.Enabled = false;
        drpHours.Enabled = false;
        drpMinutes.Enabled = false;
        txtLearningOutcome.Enabled = false;
        uiChkNoProprietaryInfo.Enabled = false;
        uiTxtCustomProviderCode.Enabled = false;
    }

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
            
            //Provider provider = (Provider)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // Get provider information from Session 
            ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // get approved provider information from Session 
            
            CourseDataAccess courseDAL = new CourseDataAccess();
            Course course = courseDAL.GetCoursesDetailsByIDandProviderID(courseID, provider.ID); // Get Courses Details from Course ID and Provider ID

            if (course == null || course.ID < 1)
            {
                lblMsg.Text = LACESConstant.Messages.COURSE_NOT_FOUND_IN_PROVIDER;
                return;
            }

            dvCourseDetails.InnerHtml = Server.HtmlEncode(course.Title);//display the title

            trTitle.Attributes.Add("style", "display:none;"); //display in title field
            uiDivTextBox.Visible = false;
            Page.Title = course.Title + "| LA CES™";

            txtTitle.Text = course.Title;
            txtLearningOutcome.Text = course.LearningOutcomes;
            txtInstructors.Text = course.Instructors;
            txtHyperlink.Text = course.Hyperlink;
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
            string tempFullTime = course.Hours.ToString();

            string tempHours = tempFullTime.Substring(0, tempFullTime.IndexOf("."));
            string tempMins = "";
            if (tempFullTime.Length - (tempFullTime.IndexOf(".") + 1) == 2)
            {
                tempMins = tempFullTime.Substring(tempFullTime.IndexOf(".") + 1, 2);
            }
            else if (tempFullTime.Length - (tempFullTime.IndexOf(".") + 1) == 1)
            {
                tempMins = tempFullTime.Substring(tempFullTime.IndexOf(".") + 1, 1) + "0";
            }
            drpHours.SelectedValue = tempHours;
            drpMinutes.SelectedValue = tempMins;
            
            txtDescription.Text = course.Description;
            
            bool statusSelected = false;
            // Populate Informations for status radio button 
            for (int i = 0; i < rdoStatusList.Items.Count; i++)
            {
                if (rdoStatusList.Items[i].Text == course.Status)
                {
                    rdoStatusList.Items[i].Selected = true;
                    statusSelected = true;
                    break;
                }
            }

            if (statusSelected)
            {
                // Status will be displayed in Edit mode
                //trStatus.Attributes.Remove("style");

                ///Updated By Matin on 2/7/2008 for task 5980
                ///Setting the minimum value to compare with start and end date as 1/1/1900
                ///without disabling the compare validators
                // Disable date compare validations
                //cvStartDate.Enabled = false;
                //cvEndDate.Enabled = false;
                //cvStartDate.ValueToCompare = new DateTime(1900, 1, 1).ToString("MM/dd/yyyy");
                //cvEndDate.ValueToCompare = new DateTime(1900, 1, 1).ToString("MM/dd/yyyy");
            }
            else if (course.Status == "Past")
            {
                rdoStatusList.SelectedValue = "CL";
            }

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
         //   populateCourseCodeInfo(courseID);
            uiTxtCustomProviderCode.Text = course.ProviderCustomCourseCode;
            // Populate the participants of the course
            populateParticipantList(courseID);

            
        
        }
        else ///If want to add new course
        {
            dvCourseDetails.InnerHtml = "Add a Course";

            Page.Title = "Add a Course | LA CES™";

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
        //ParticipantDataAccess participantDAL = new ParticipantDataAccess();
        //IList<Participant> participantList = participantDAL.GetAllParticipantByCourse(courseID); // Get Participants of the course        

        //dvParticipantList.InnerHtml = "Participant(s): " + participantList.Count.ToString();
        ////EditParticipants.aspx?ParticipantID=53&CourseID=54
        //if (participantList.Count > 0)
        //{
        //    dvParticipantList.InnerHtml = "<table width='100%' cellpadding='3' cellspacing='0'><tr><td width='50%' class='participantList' align='right'><b>Last Name, </b></td><td class='participantList' left='right'><b>First Name</b></td></tr>";
        //    foreach (Participant participant in participantList)
        //    {
        //        dvParticipantList.InnerHtml += "<tr><td width='50%' align='right'><a onclick='return confirmonCloseLink(\"You have made changes to the course details. Would you like to save them before editing this individual participant?\",\"EditParticipants.aspx?ParticipantID=" + participant.ID.ToString() + "&CourseID=" + courseID + "\");' href='EditParticipants.aspx?ParticipantID=" + participant.ID.ToString() + "&CourseID=" + courseID + "'>" + Server.HtmlEncode(participant.LastName) + "</a>,</td><td>" + Server.HtmlEncode(participant.FirstName) + "</td></tr>";
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
    #endregion

    #region Populate Subject List
    /// <summary>
    ///  Populate Subject List 
    /// </summary>
    private void populateSubjectList()
    {
        SubjectDataAccess subjectDAL = new SubjectDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int SubjectID = int.Parse(LACESUtilities.GetApplicationConstants("SubjectContentID"));
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
    private void populateCourseStatusList()
    {
        CourseStatusDataAccess courseStatusDAL = new CourseStatusDataAccess();
        IList<CourseStatus> courseStatusList = courseStatusDAL.GetAllCourseStatus(); // Get All course Status

        foreach (CourseStatus courseStatus in courseStatusList)
        {
            ListItem item = new ListItem(courseStatus.Notes, courseStatus.StatusCode);
            rdoStatusList.Items.Add(item);
        }
    }
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
    //    //drpCourseType.Items.Add(new ListItem("- Other International -", "OL"));

    //    //if corse code available then visible the corse code table
    //    //if (courseTypeList.Count > 0)
    //    //{
    //    //    corseCodeTable.Style["display"] = "block";
    //    //}
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

    private void saveInformation()
    {
        

        try
        {
            //

            //Provider provider = (Provider)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // get provider information from Session            
            ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // get approved provider information from Session 

            Course course = new Course();
            course.Title = txtTitle.Text.Trim();
            course.Hyperlink = txtHyperlink.Text.Trim();
            course.StartDate = (txtStartDate.Text.Trim() == string.Empty) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(txtStartDate.Text.Trim());
            course.EndDate = (txtEndDate.Text.Trim() == string.Empty) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(txtEndDate.Text.Trim());
            course.Description = (txtDescription.Text.Length > 3600) ? txtDescription.Text.Substring(0, 3600) : txtDescription.Text;
            course.City = txtCity.Text.Trim();
            course.StateProvince = drpState.SelectedValue.Trim();
            course.DistanceEducation = (chkDistanceEdu.Checked == true) ? "Y" : "N";
            course.CourseEquivalency = (chkCourseEquivalency.Checked == true) ? "Y" : "N";            
            course.Hours = drpHours.SelectedValue + "." + drpMinutes.SelectedValue;
            course.LearningOutcomes = (txtLearningOutcome.Text.Length > 3600) ? txtLearningOutcome.Text.Substring(0, 3600) : txtLearningOutcome.Text;
            course.Instructors = txtInstructors.Text.Trim();
            course.RegistrationEligibility = (txtRegistrationEligibility.Text.Length > maxCharecterCountRegistrationEligibility) ? txtRegistrationEligibility.Text.Substring(0, maxCharecterCountRegistrationEligibility) : txtRegistrationEligibility.Text;
            course.ProviderCustomCourseCode = uiTxtCustomProviderCode.Text.Trim();

            //Debugger.Break();
            /*
            string retValue="";
            
            for (int i = 0; i < Request.Form.GetValues("txtOrgState").Length; i++)
            {
                if (Request.Form.GetValues("txtOrgState")[i].Trim() != "")
                    retValue += delimiter + Request.Form.GetValues("txtOrgState")[i];
            }

            course.OrgStateCourseIDNumber = retValue;
            */

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
                        subject = subject + ", " + drpSubject.Items[i].Text;
                    }
                }
            }
            course.Subjects = subject;

            ///Update this section by Matin according to task 6318
            //string health = string.Empty;
            // populate string from selected Items
            course.Healths = (chkHealth.Checked == true) ? "Yes" : "No";          
            course.Hours = drpHours.SelectedValue + "." + drpMinutes.SelectedValue;
            course.ProviderID = provider.ID;
            course.NoProprietaryInfo = (uiChkNoProprietaryInfo.Checked == true) ? "Y" : "N";            
            CourseDataAccess courseDAL = new CourseDataAccess();
            if (Convert.ToInt64(hidCourseID.Value) > 0)
            {
                if (rdoStatusList.SelectedIndex == -1)
                    course.Status = "NP";
                else
                    course.Status = rdoStatusList.SelectedValue;
                course.ID = Convert.ToInt64(hidCourseID.Value);
                courseDAL.UpdateCourseDetails(course); // Update course information
                operation = "update";
            }
            else
            {                
                if (Request.QueryString["RenewCourse"] == "Y" && uiHdnCourseToRenew.Value.Trim().Length>1)
                {
                    course.Status = "OP"; //course is to be renewed so autoapprove
                    course.ID = courseDAL.AddCourseDetails(course); // Add course information
                    CourseDataAccess objCourseDAL = new CourseDataAccess();
                    bool approved = objCourseDAL.ApproveCourseByCourseId(course.ID);
                    operation = "renew";
                }
                else {
                    course.Status = "NP";
                    course.ID = courseDAL.AddCourseDetails(course); // Add course information
                    operation = "add";
                }
                
            }
          //  updateCourseCode(course.ID);
            //Response.Redirect("CourseList.aspx"); // redirect after saving course information
            Session["CourseName"] = course.Title;
            Session["ReplicateCourseID"] = course.ID;
            if (Convert.ToInt64(hidCourseID.Value) < 1)
            {
                if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
                {
                    sendMail(course, drpState.SelectedItem.Text);
                }
            }            
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
    private string[] splitString(string stringList)
    {        
        IList<string> stringArray = new List<string>();
        while (stringList.IndexOf(delimiter) >= 0)
        {
            stringArray.Add(stringList.Substring(0,stringList.IndexOf(delimiter)));
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
    //private void updateCourseCode(long courseID)
    //{
    //    CourseCodeDataAccess codeDAL = new CourseCodeDataAccess();
    //    codeDAL.DeleteCourseCodeByCourseID(courseID); // Delete course codes of the Course

    //    string[] descList = splitString(courseDesc);
    //    string[] typeList = splitString(courseType);
    //    //string[] typeList = courseType.Split('|');

    //    if (!courseType.Equals(string.Empty))
    //    {
    //        for (int i = 0; i < typeList.Length; i++)
    //        {
    //            CourseCode code = new CourseCode();
    //            code.Description = descList[i];
    //            code.CodeID = Convert.ToInt32(typeList[i]);
    //            code.CourseID = courseID;
    //            try
    //            {
    //                codeDAL.AddCourseCode(code); // Add course Code
    //            }
    //            catch(Exception ex)
    //            {
    //                Session["CourseCodeFailed"] = "yes";
    //            }
    //        }
    //    }
    //    populateCourseCodeInfo(courseID); // on Load script for populating the Course code after page refresh.
    //}
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
            Response.Redirect("UploadParticipants.aspx?" + LACESConstant.QueryString.COURSE_ID + "=" + Request.QueryString[LACESConstant.QueryString.COURSE_ID]);
        }
    }
    #endregion

    #region Sending a mail after adding a course to the system
    /// <summary>
    /// Sending a mail after adding a course to the system
    /// </summary>
    /// <param name="course">Details of the course for sending email</param>
    /// <param name="stateProvince">State name</param>
    private void sendMail(Course course, string stateProvince)
    {
        //Provider provider = (Provider)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // get provider information from Session
        ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // get approved provider information from Session 
        string RefrenceURL = String.Empty;
        RefrenceURL = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.ToLower().LastIndexOf(LACESConstant.URLS.PROVIDER_BASE.ToLower())) + LACESConstant.URLS.ADMIN_BASE + "/" + LACESConstant.URLS.PENDING_COURSE;
        string courseDistance = string.Empty;
        if(course.DistanceEducation.Equals("Y"))
        {
            courseDistance = "Yes";
        }
        else
        {
            courseDistance = "No";
        }            
        if(course.StateProvince.Trim().Equals(string.Empty))
        {
            stateProvince = string.Empty;
        }

        StringBuilder EmailBody = new StringBuilder();
        EmailBody.Append("<table border='0' width='100%>");
        EmailBody.Append("<tr><td width='130'>&nbsp;</td><td>&nbsp;</td></tr>");
        EmailBody.Append("<tr><td colspan='2'>A new course has been added to the system by " + Server.HtmlEncode(provider.OrganizationName) + " and is <a href='" + RefrenceURL + "'>pending approval</a>. Please review the details of the course and either approve or reject the course.</td></tr>");
        EmailBody.Append("<tr><td colspan='2'><br/>The details of the new course are as follows:<br/></td></tr>");
        EmailBody.Append("<tr><td valign='top' width='130'>Course Title:</td><td>" + Server.HtmlEncode(course.Title) + "</td></tr>");
        EmailBody.Append("<tr><td valign='top'>Course Hyperlink:</td><td>" + Server.HtmlEncode(course.Hyperlink) + "</td></tr>");
        EmailBody.Append("<tr><td valign='top'>Start Date:</td><td>" + course.StartDate.ToString("MM/dd/yyyy") + "</td></tr>");
        EmailBody.Append("<tr><td valign='top'>End Date:</td><td>" + course.EndDate.ToString("MM/dd/yyyy") + "</td></tr>");
        EmailBody.Append("<tr><td valign='top'>Description:</td><td>" + Server.HtmlEncode(course.Description) + "</td></tr>");
        EmailBody.Append("<tr><td valign='top'>City:</td><td>" + Server.HtmlEncode(course.City) + "</td></tr>");
        EmailBody.Append("<tr><td valign='top'>State:</td><td>" + Server.HtmlEncode(stateProvince) + "</td></tr>");
        EmailBody.Append("<tr><td valign='top'>Distance Education:</td><td>" + courseDistance + "</td></tr>");
        EmailBody.Append("<tr><td valign='top'>Subjects:</td><td>" + course.Subjects + "</td></tr>");
        EmailBody.Append("<tr><td valign='top'>Health:</td><td>" + course.Healths + "</td></tr>");
        EmailBody.Append("<tr><td valign='top'>Hours:</td><td>" + course.Hours + "</td></tr>");
        EmailBody.Append("<tr><td valign='top'>Learning Outcomes:</td><td>" + Server.HtmlEncode(course.LearningOutcomes) + "</td></tr>");
        EmailBody.Append("<tr><td colspan='2'><br/>If you wish to add this course to the " + LACESConstant.LACES_TEXT + " system, please <a href='" + RefrenceURL + "'>manage the list of pending courses</a> in the administration system to approve it.<br/></td></tr>");
        EmailBody.Append("<tr><td colspan='2'><br/>If you are going to reject the course, or wish to discuss the course further, please contact the course provider <a href='mailto:" + provider.ApplicantEmail + "'>via email</a>.<br/></td></tr>");
        EmailBody.Append("</table>");
        string mailBody = EmailBody.ToString();


        try
        {
            SmtpClient smtpClient = new SmtpClient();
            //Get the SMTP server Address from SMTP Web.conf
            smtpClient.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER);
            //Get the SMTP post  25;
            smtpClient.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));

            MailMessage message = new MailMessage();

            message.From = new MailAddress(LACESUtilities.GetAdminFromEmail());
            
            //message.To.Add(LACESUtilities.GetAdminToEmail());
            //message.CC.Add(LACESUtilities.GetAdminCCEmail());

            //Get the email's TO from Web.conf
            message.To.Add(LACESUtilities.GetCourseNotificationMailTo());

            message.Subject = "New Course Added - Pending Approval";
            message.IsBodyHtml = true;
            message.Body = mailBody;    // Message Body
            if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
            {
                smtpClient.Send(message);   //Sending A Mail to Admin
            }
        }
        catch (Exception ex)
        {
            throw ex;
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
    protected void ReplicateCourse()
    {
        try
        {
            uiSpanDuplicateHelpText.Visible = true;
            long courseID = 0;
            courseID = Convert.ToInt64(Session["ReplicateCourseID"]);

            //Provider provider = (Provider)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // Get provider information from Session 
            ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // get approved provider information from Session 

            CourseDataAccess courseDAL = new CourseDataAccess();
            Course course = courseDAL.GetCoursesDetailsByIDandProviderID(courseID, provider.ID); // Get Courses Details from Course ID and Provider ID

            if (course == null || course.ID < 1)
            {
                lblMsg.Text = LACESConstant.Messages.COURSE_NOT_FOUND_IN_PROVIDER;
                return;
            }
            //dvCourseDetails.InnerHtml = Server.HtmlEncode(course.Title);//display the title

            txtTitle.Text = course.Title;

            Page.Title = course.Title + " | LA CES™"; ;

            txtTitle.Text = course.Title;
            txtLearningOutcome.Text = course.LearningOutcomes;
            txtLearningOutcome.Enabled = false;

            txtInstructors.Text = course.Instructors;
            txtHyperlink.Text = course.Hyperlink;
            txtStartDate.Text = course.StartDate.ToString("MM/dd/yyyy");
            txtEndDate.Text = course.EndDate.ToString("MM/dd/yyyy");
            chkDistanceEdu.Checked = (course.DistanceEducation.ToLower()[0] == 'y') ? true : false;
            uiChkNoProprietaryInfo.Checked = (course.NoProprietaryInfo.ToLower()[0] == 'y') ? true : false;
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
            string tempFullTime = course.Hours.ToString();

            string tempHours = tempFullTime.Substring(0, tempFullTime.IndexOf("."));
            string tempMins = "";
            if (tempFullTime.Length - (tempFullTime.IndexOf(".") + 1) == 2)
            {
                tempMins = tempFullTime.Substring(tempFullTime.IndexOf(".") + 1, 2);
            }
            else if (tempFullTime.Length - (tempFullTime.IndexOf(".") + 1) == 1)
            {
                tempMins = tempFullTime.Substring(tempFullTime.IndexOf(".") + 1, 1) + "0";
            }
            drpHours.SelectedValue = tempHours;
            drpMinutes.SelectedValue = tempMins;

            txtDescription.Text = course.Description;

            bool statusSelected = false;
            // Populate Informations for status radio button 
            for (int i = 0; i < rdoStatusList.Items.Count; i++)
            {
                if (rdoStatusList.Items[i].Text == course.Status)
                {
                    rdoStatusList.Items[i].Selected = true;
                    statusSelected = true;
                    break;
                }
            }

            if (statusSelected)
            {
            
            }
            else if (course.Status == "Past")
            {
                rdoStatusList.SelectedValue = "CL";
            }

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
            uiTxtCustomProviderCode.Text = course.ProviderCustomCourseCode;
            ///Update this section by Matin according to task 6318
            // Populate Informations for Health check box
            chkHealth.Checked = (course.Healths.ToLower() == "yes") ? true : false;
            int remainCharacter = maxCharecterCount - txtDescription.Text.Length;
            lblDescriptionCouter.Text = remainCharacter + "&nbsp;&nbsp;  character(s) remains.";
            remainCharacter = maxCharecterCount - txtLearningOutcome.Text.Length;
            lblLearningOutcomeCounter.Text = remainCharacter + "&nbsp;&nbsp;  character(s) remains.";
            
            // Update Course ID hidden field with CourseID
            
            // Populate Course Code Information
            //populateCourseCodeInfo(courseID);

            // Populate the participants of the course
            populateParticipantList(courseID);

            
        }
        catch (Exception Ex)
        {

        }
    }
}
