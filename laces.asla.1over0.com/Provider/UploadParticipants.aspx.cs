/*------------------------------------------------------------------------------
 * Project Name     : LACES
 * Client Name      : ASLA
 * Component Name   : UploadParticipants.aspx.cs
 * Purpose/Function : Used to Manage Participants in provider mode.
 * Author           : Alamgir Hossain
 * 
 * Version                 Author             Date              Reason 
 * 1.0                  Alamgir Hossain     01/17/2008      Create and UI Components
 * 1.1                  Matiur Rahman       01/29/2008      Update security to prevent access of details information of other providers
 * 1.2                  Alamgir Hossain     06/17/2008      Add Excel file date upload functionality
 * --------------------------------------------------------------------------------*/

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
using System.IO;
using System.Data.Common;
using System.Diagnostics;
using System.Data.OleDb;

/// <summary>
/// Manage Participants page, used to manage participant for a course in user mode. 
/// The admin can add new participant for a spacific course. 
/// </summary>
public partial class Upload_Participants : ProviderBasePage
{
    #region Upload Excel File

    #region ReadXLFile

    /// <summary>
    /// Read data from Excel file.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private DataTable ReadXLFile(string fileName)
    {
        DataTable dt = new DataTable();
        try
        {
            String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;\"";

            OleDbConnection objConn = new OleDbConnection(sConnectionString);

            // Open connection with the database.
            objConn.Open();

            OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [participants$]", objConn);

            // Create new OleDbDataAdapter that is used to build a DataSet
            // based on the preceding SQL SELECT statement.
            OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

            // Pass the Select command to the adapter.
            objAdapter1.SelectCommand = objCmdSelect;

            // Create new DataSet to hold information from the worksheet.
            DataSet objDataset1 = new DataSet();

            // Fill the DataSet with the information from the worksheet.
            objAdapter1.Fill(objDataset1, "XLData");


            objConn.Close();

            return objDataset1.Tables[0];
        }
        catch (Exception ex)
        {
            //lblMasg.Text = "Cannot read from Excel file for the following error:<br/>Error : " + ex.Message;
            //File.AppendAllText(LogFilePath + "Error.txt", "Cannot read from Excel file for the following error:\nError : " + ex.Message + "\n");
            //throw new Exception("Cannot Read", ex);
            return dt;
        }
    }


    #endregion


    /// <summary>
    /// Uplaod an Excel file 
    /// </summary>
    private void UploadExcelFile()
    {
        if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null)
        {
            try
            {
                //get the course id from query string
                long courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);
                List<Participant> participantList = new List<Participant>();

                //create the file loacton
                string fileLocation = AppDomain.CurrentDomain.BaseDirectory+"xls\\"+ System.DateTime.Now.Ticks + FileUpload1.FileName;
                FileStream oFileStream = File.Create(fileLocation);

                foreach (byte b in FileUpload1.FileBytes)
                {
                    oFileStream.WriteByte(b);
                }
                oFileStream.Close();

                Participant oParticipant = null;
                
                //now load it to UI
                DataTable oDataTable = ReadXLFile(fileLocation);
                foreach (DataRow dr in oDataTable.Rows)
                {
                    oParticipant = new Participant();
                    oParticipant.LastName = dr["Last"].ToString();
                    oParticipant.FirstName = dr["First"].ToString();
                    oParticipant.ASLANumber = dr["ASLA"].ToString();
                    oParticipant.CLARBNumber = dr["CLARB"].ToString();
                    oParticipant.FloridaStateNumber = dr["FL"].ToString();

                    //add in to the participant list
                    participantList.Add(oParticipant);
                }

                if (participantList.Count > 0)
                {
                    //finally add the participant in to the database
                    ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
                    oParticipantDataAccess.AddParticipantByCourse(courseID, participantList);
                }

                //finally remove the excel file
                if (File.Exists(fileLocation))
                {
                    File.Delete(fileLocation);
                }

                Response.Redirect(Request.Url.AbsoluteUri);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    #endregion

    #region GetAllCourses

    /// <summary>
    /// Load all cources in the UI
    /// </summary>
    private void LoadAllCourses()
    {
        //create the coures data access
        CourseDataAccess objCourseDAL = new CourseDataAccess();

        //get the current provider
        Provider1 provider = (Provider1)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER];

        existingCourcesDiv.InnerHtml = "<div class='dvHeader2'>Existing Cources</div>";
        existingCourcesDiv.InnerHtml += "<div class='existingCources'>";
        IList<Course> cources = objCourseDAL.GetCoursesByProviderId(provider.ID, "T");
        foreach (Course c in cources)
        {
            existingCourcesDiv.InnerHtml += "<a href=UploadParticipants.aspx?courseid=" + c.ID + ">" + c.Title + "</a><br/>";
        }
        existingCourcesDiv.InnerHtml += "</div>";

        if (cources.Count <= 0)
        {
            existingCourcesDiv.Visible = false;
        }
        else
        {
            existingCourcesDiv.Visible = true;
        }
    }

    #endregion

    #region Page Load Event Handler

    /// <summary>
    ///  Page Load Event Handler, call every time when the page is loaded.
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if no course define then show the course list
            if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] == null)
            {
                LoadAllCourses();
                uploadFileDiv.Visible = false;
            }
        }
        catch (Exception ex)
        {

            throw;
        }

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
                Response.Redirect(url.Substring(0, url.LastIndexOf("/")) + "/EditParticipants.aspx?" + LACESConstant.QueryString.PARTICIPANT_ID + "=" + val + "&" + LACESConstant.QueryString.COURSE_ID + "=" + courseID);
            }
        }
        
        //focus the default text box
        //row11.Focus();
        
        //get the master page form tag and set default button  
        //HtmlForm masterHtmlForm = (HtmlForm)Master.FindControl("form1");
        //masterHtmlForm.DefaultButton = btnSaveFinish.UniqueID;

        if (!IsPostBack)
        {

            //add javascript functionality to the master page provider details link
            
            //HtmlAnchor anchorMasterProviderDetail = (HtmlAnchor)Master.FindControl("anchorProviderDetails");
            //anchorMasterProviderDetail.Attributes.Add("onclick", "javascript:return CheckChange(1,-1)");
            
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

                    //populate provider information
                    populateProviderInfo();
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
        Provider1 provider = (Provider1)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // Get provider information from Session 

        ParticipantDataAccess participantDAL = new ParticipantDataAccess();
        IList<Participant> participantList = participantDAL.GetAllParticipantByCourseIDProviderID(courseID, provider.ID); // Get Participants of the course

        //prepare the header
        dvParticipantList.InnerHtml = "<table border='0' style='text-align:center;' cellpadding='0' cellspacing='0'>";
        dvParticipantList.InnerHtml += "<tr><td style='text-align:right; vertical-align:middle;' class='participantList'><strong>Last Name,</strong></td><td style='text-align:left; vertical-align:middle;' class='participantList'><strong>First Name</strong></td></tr>";

        //dynamically add rest of the rows
        foreach (Participant participant in participantList)
        {

            dvParticipantList.InnerHtml += "<tr><td onclick='javascript:return CheckChange(3," + participant.ID.ToString() + ")' style='text-align:right; vertical-align:middle;' class='participantList'><a href='EditParticipants.aspx?ParticipantID=" + participant.ID.ToString() + "&CourseID=" + courseID.ToString() + "'>" + Server.HtmlEncode(participant.LastName) + "</a>,</td>";
            dvParticipantList.InnerHtml += "<td style='text-align:left; vertical-align:middle;' class='participantList'>" + Server.HtmlEncode(participant.FirstName) + "</td></tr>";
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
                Provider1 provider = (Provider1)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // Get provider information from Session 
                CourseDataAccess courseDAL = new CourseDataAccess();
                Course course = courseDAL.GetCoursesDetailsByIDandProviderID(courseID, provider.ID); // Get Courses Details from Course ID and Provider ID

                if (course == null || course.ID < 1)
                {
                    //btnSaveAddMore.Enabled = false;
                    //btnSaveFinish.Enabled = false;
                    lblMsg.Text = LACESConstant.Messages.COURSE_NOT_FOUND_IN_PROVIDER;
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

    #region Get Provider Details Information

    /// <summary>
    /// Used to populate provider Details information into UI. Get provider id from current
    /// logged in provider session variable.
    /// </summary>
    private void populateProviderInfo()
    {
        if ((Provider1)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER] != null)
        {
            //get provider detail from session veriable
            Provider1 provider = (Provider1)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // Get provider information from Session 
            Label lblMasterProviderName = (Label)Master.FindControl("lblProviderName");

            //set the provider name to the master page ProviderName label
            lblMasterProviderName.Text = "Signed in as: "+ Server.HtmlEncode(provider.Organization)+ "";
        }
    }

    #endregion

    #region Manage Participants Click Events



    /// <summary>
    /// Call when Update Button Click
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //check the upload file
        if (FileUpload1.PostedFile != null && FileUpload1.FileName != "")
        {
            //Debugger.Break();
            UploadExcelFile();
        }
    }


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
            Response.Redirect(url.Substring(0, url.LastIndexOf("/")) + "/UploadParticipants.aspx?CourseID=" + courseID);
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
            //get the course id from query string
            if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null)
                courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);

            //get course detail by courseid and providerid
            Provider1 provider = (Provider1)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER]; // Get provider information from Session 
            CourseDataAccess courseDAL = new CourseDataAccess();
            Course course = courseDAL.GetCoursesDetailsByIDandProviderID(courseID, provider.ID); // Get Courses Details from Course ID and Provider ID

            ///If the course is related with current provider
            if (course != null && course.ID > 0)
            {
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
                    //add the participant list into the database
                    ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
                    oParticipantDataAccess.AddParticipantByCourse(courseID, participantList);                    
                }
                return true;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return false;
    }

    #endregion
}
