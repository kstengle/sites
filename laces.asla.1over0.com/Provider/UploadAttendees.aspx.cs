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
public partial class Provider_UploadAttendees : ProviderBasePage
{
    #region Upload Excel File

    string fileLocation ="";

    /// <summary>
    /// Uplaod an Excel file 
    /// </summary>
    private bool UploadExcelFile()
    {
        string staticErrorMessage = "There has been an error with your upload. Please ensure you use our template to upload attendees.<br/>&nbsp;";

        lb_error_message.Style["color"] = "red";
        bool blnShowGetNewExcel = false;
        //check the file extension, if not .xls file then show error
        if (!FileUpload1.FileName.EndsWith(".xls") && !FileUpload1.FileName.EndsWith(".xlsx"))
        {
            lb_error_message.InnerHtml = staticErrorMessage + "<br />location=1";// "Your provided file type is not supported.";
            return false;
        }

        //prepare the file path
        fileLocation = AppDomain.CurrentDomain.BaseDirectory + "xls\\" + System.DateTime.Now.Ticks + FileUpload1.FileName;

        try
        {
            //get the course id from query string
            long courseID = 0;
            List<Participant> participantList = new List<Participant>();

            //create the file loacton
            FileStream oFileStream = File.Create(fileLocation);

            foreach (byte b in FileUpload1.FileBytes)
            {
                oFileStream.WriteByte(b);
            }
            oFileStream.Close();
            Participant oParticipant = null;

            //Debugger.Break();

            //now load it to UI
            if (Request.QueryString["courseid"] != null)
            {

                try
                {
                    courseID = Convert.ToInt32(Request.QueryString["courseid"]);
                }
                catch
                {
                    courseID = 0;
                }


                //no course found
                if (courseID == 0)
                {
                    lb_error_message.InnerHtml = staticErrorMessage + "<br />location=2";//"Course ID is invalid/not exists.";
                    return false;
                }

                //get the current login provider 
                ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER];

                //check is the course available or not
                CourseDataAccess oCourseDataAccess = new CourseDataAccess();
                if (oCourseDataAccess.GetCoursesDetailsByIDandProviderID(courseID, provider.ID) == null)
                {
                    lb_error_message.InnerHtml = staticErrorMessage + "<br />location=3";// "Course ID is invalid/not exists.";
                    return false;
                }
            }
            else
            {
                lb_error_message.InnerHtml = staticErrorMessage + "<br />location=4";//"Course ID is invalid.";
                return false;
            }

            DataTable oDataTable = LACESUtilities.ReadXLFile(fileLocation, "Attendees");
			  //  Response.Write(fileLocation);
//            Response.End();
            if (oDataTable == null)
            {
                lb_error_message.InnerHtml = staticErrorMessage + "<br />location=5";// "Attendees' information does not match with the template.";
                return false;
            }

            foreach (DataRow dr in oDataTable.Rows)
            {
                oParticipant = new Participant();

                string last_name = dr["Last"].ToString().Trim();
                string first_name = dr["First"].ToString().Trim();
                string middle_name = dr["Middle"].ToString().Trim();
                string asla_number = dr["ASLA"].ToString().Trim();
                string clarb_number = dr["CLARB"].ToString().Trim();
                string csla_name = dr["CSLA"].ToString().Trim();
                string florida_number = dr["FL"].ToString().Trim();
                string email = "";
                if (dr.Table.Columns.Contains("email"))
                {
                    email = dr["email"].ToString().Trim();
                }
                else
                {
                    blnShowGetNewExcel = true;
                }
               //if (last_name == "" && first_name == "")
                //{
                  //  lb_error_message.InnerHtml = staticErrorMessage + "<br />location=6";//"In your uploaded file, one or more first name/last name are //empty.";
                 //   return false;
                //}

                //if any field is empty then exit the process
                if (last_name != "" && first_name != "")// && asla_number != "" && clarb_number != "" && florida_number != "")
                {
                    oParticipant.LastName = last_name;
                    oParticipant.FirstName = first_name;
                    oParticipant.MiddleInitial = middle_name;
                    oParticipant.ASLANumber = asla_number;
                    oParticipant.CLARBNumber = clarb_number;
                    oParticipant.CSLANumber = csla_name;
                    oParticipant.FloridaStateNumber = florida_number;
                    oParticipant.Email = email;
                    //add in to the participant list
                    participantList.Add(oParticipant);
                }
                else if (csla_name == "" && middle_name == "" && last_name == "" && first_name == "" && asla_number == "" && clarb_number == "" && florida_number == "")
                {
                    //if empty row found then discard the row
                }
                else
                {
                    //some empty field found. so inform the error
                    lb_error_message.InnerHtml = staticErrorMessage + "<br />location=7";// "Attendees' information does not match with the template.";
                    return false;
                }

            }
            if (participantList.Count > 0)
            {
                //finally add the participant in to the database
                ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
                oParticipantDataAccess.AddParticipantByCourse(courseID, participantList);
            }

            //finally remove the excel file
            LACESUtilities.RemoveCreatedFileFromDisk(fileLocation);

            
            if (blnShowGetNewExcel)
            {
                lb_error_message.InnerHtml = "ALERT: Please download new Excel Template that now captures attendee email. This did not prevent your current attendee file from being successfully uploaded.<br/>&nbsp;";
                lb_error_message.Style["color"] = "red";
            }
            else
            {
                lb_error_message.InnerHtml = "Thank you. Your attendee file has been successfully uploaded.<br/>&nbsp;";
                lb_error_message.Style["color"] = "green";
            }
            //Response.Redirect("../Provider/ManageAttendees.aspx?courseid=" + courseID);
        }
        catch (Exception ex)
        {
            lb_error_message.InnerHtml = staticErrorMessage + "<br />location=1" + "<br />" + ex.Message;//"There has been an error with your upload. Please ensure you use our template to upload attendees.";
            if (File.Exists(fileLocation)) File.Delete(fileLocation);
        }

        return true;
    }

    #endregion

    /// <summary>
    /// Call when Update Button Click
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string staticErrorMessage = "There has been an error with your upload. Please ensure you use our template to upload attendees.<br/>&nbsp;";


        //check the upload file
        if (FileUpload1.PostedFile != null && FileUpload1.FileName != "")
        {
             if (FileUpload1.PostedFile.ContentType != "application/vnd.ms-excel" && FileUpload1.PostedFile.ContentType!="application/xml" && FileUpload1.PostedFile.ContentType != "application/octet-stream" && FileUpload1.PostedFile.ContentType.IndexOf("application/vnd.openxmlformats")<0)
            {
                lb_error_message.InnerHtml = staticErrorMessage + "<br />" + FileUpload1.PostedFile.ContentType + "<br />location=8";// "Your provided file type is not supported.";
                return;
            }
            else
            {
                if (UploadExcelFile())
                {
                    //remove the created file from disk
                    LACESUtilities.RemoveCreatedFileFromDisk(fileLocation);
                }
            }
        }
        else
        {
            lb_error_message.InnerHtml = staticErrorMessage + "<br />location=9";//"File not received. Please upload the file again";
            return;
        }
    }


    #region Page Load Event Handler

    /// <summary>
    ///  Page Load Event Handler, call every time when the page is loaded.
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowPageHeader();
    }

    #endregion

    /// <summary>
    /// Display page header containing course title
    /// </summary>
    private void ShowPageHeader()
    {
        dvHeader.InnerHtml = "Upload Attendees in Excel for: ";

        if (Request.QueryString["courseid"] != null)
        {
            long courseid = 0;
            long.TryParse(Request.QueryString["courseid"], out courseid);
            if (courseid > 0)
            {
                //create the coures data access
                CourseDataAccess objCourseDAL = new CourseDataAccess();
                Course selectedCourse = objCourseDAL.GetCoursesDetailsByID(courseid);
                if (selectedCourse != null)
                {
                    dvHeader.InnerHtml += Server.HtmlEncode(selectedCourse.Title);
                }
            }
        }
    }

}
