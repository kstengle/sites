/*------------------------------------------------------------------------------
 * Project Name     : LACES
 * Client Name      : ASLA
 * Component Name   : ManageAttendees.aspx.cs
 * Purpose/Function : Used to Manage Attendees in provider mode.
 * Author           : Alamgir Hossain
 * 
 * Version                 Author             Date              Reason 
 * 1.0                  Alamgir Hossain     07/08/2008      Create and UI Components
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
using System.Linq;
using System.Text;
using System.ComponentModel;
/// <summary>
/// Manage Participants page, used to manage participant for a course in user mode. 
/// The admin can add new participant for a spacific course. 
/// </summary>
public partial class Provider_ManageAttendees : ProviderBasePage
{

    #region Page Load Event Handler

    /// <summary>
    ///  Page Load Event Handler, call every time when the page is loaded.
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            populateStateList();

            if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null)
            {
                try
                {
                    //get the course id from query string
                    long courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);

                    //populate ParticipantList
                    populateParticipantList(courseID);

                    populateCourseInfo();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
        
        //foreach (Course c in cources)
        //{
        //    existingCourcesDiv.InnerHtml += "<tr><td class='grayText'>" + Server.HtmlEncode(c.Title) + "</td>";
        //    existingCourcesDiv.InnerHtml += "<td class='grayText'>" + c.StartDate.ToShortDateString() + "</td>";
        //    existingCourcesDiv.InnerHtml += "<td class='grayText'>" + c.EndDate.ToShortDateString() + "</td>";
        //    existingCourcesDiv.InnerHtml += "<td align='center'><a href=UploadAttendees.aspx?courseid=" + c.ID + ">Upload</a></td>";
        //    existingCourcesDiv.InnerHtml += "<td align='center'><a href=ManageAttendees.aspx?courseid=" + c.ID + ">Edit</a></td></tr>";
        //}
        //existingCourcesDiv.InnerHtml += "</table></div>";

        //if (cources.Count <= 0)
        //{
        //    existingCourcesDiv.InnerHtml = "<div class='title'>Upload or Edit Attendees</div>";
        //    existingCourcesDiv.InnerHtml += "<div class='existingCources'>No records found.</div>";
        //}
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
        try
        {
            CourseDataAccess oCourseDataAccess = new CourseDataAccess();

            ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // Get provider information from Session 

            ParticipantDataAccess participantDAL = new ParticipantDataAccess();
            IList<Participant> participantList = participantDAL.GetAllParticipantByCourseIDProviderID(courseID, provider.ID); // Get Participants of the course
            rptParticipantList.DataSource = participantList;
            rptParticipantList.DataBind();
            //prepare the header
            //dvParticipantList.InnerHtml = "<div class='title'>Edit Attendees for: " + Server.HtmlEncode(oCourseDataAccess.GetCoursesDetailsByID(courseID).Title) + "</div><div class=''><table border='0' style='text-align:center;' cellpadding='0' cellspacing='0'>";

            //if (participantList.Count > 0)
            //{
            //    dvParticipantList.InnerHtml += "<tr><td style='text-align:left; vertical-align:middle;' class='participantList'><strong>Last Name</strong></td><td style='text-align:left; vertical-align:middle;' class='participantList'><strong>First Name</strong></td></tr>";

            //    //dynamically add rest of the rows
            //    foreach (Participant participant in participantList)
            //    {

            //        dvParticipantList.InnerHtml += "<tr><td style='text-align:left; vertical-align:middle;' class='participantList'><a href='EditAttendees.aspx?ParticipantID=" + participant.ID.ToString() + "&CourseID=" + courseID.ToString() + "'>" + Server.HtmlEncode(participant.LastName) + "</a></td>";
            //        dvParticipantList.InnerHtml += "<td style='text-align:left; vertical-align:middle;' class='participantList'>" + Server.HtmlEncode(participant.FirstName) + "</td></tr>";
            //    }
            //}
            //else
            //{
            //    dvParticipantList.InnerHtml += "<tr><td style='color:red'>No attendees found.</td></tr>";
            //}
            //dvParticipantList.InnerHtml += "</div></table>";
            //dvParticipantList.InnerHtml += "<br/><div><a href='../provider/UploadAttendees.aspx?courseid=" + courseID + "'>Upload Attendees in Excel</a><div>";
        }
        catch (Exception ex)
        {

            throw ex;
        }
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
                ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // Get provider information from Session 
                CourseDataAccess courseDAL = new CourseDataAccess();
                Course course = courseDAL.GetCoursesDetailsByIDandProviderID(courseID, provider.ID); // Get Courses Details from Course ID and Provider ID

                if (course == null || course.ID < 1)
                {
                    //btnSaveAddMore.Enabled = false;
                    //btnSaveFinish.Enabled = false;
                    lblMsg.Text = LACESConstant.Messages.COURSE_NOT_FOUND_IN_PROVIDER;
                  // dvParticipantList.Visible = false;
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
    private void populateStateList()
    {
        StateDataAccess stateDAL = new StateDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int StateProvidenceID = int.Parse(LACESUtilities.GetApplicationConstants("StateProvinceContentID"));
        IList<State> stateList = stateDAL.GetAllStates(StateProvidenceID, webroot);

        ///Add default item
        ListItem defaultItem = new ListItem("- Location -", "");
        ddlLocation.Items.Add(defaultItem);

        /////Add Distance Education item
        //ListItem distanceEducation = new ListItem("Distance Education", "Distance Education");
        //ddlLocation.Items.Add(distanceEducation);

        foreach (State state in stateList)
        {
            ListItem item = new ListItem(state.StateName, state.StateCode);
            ddlLocation.Items.Add(item);
        }
    }
    
    protected void uilnkDownloadAttendees_Click(object sender, EventArgs e)
    {
        long courseID;

        LinkButton btn = (LinkButton)(sender);
        string yourValue = btn.CommandArgument;
        if (long.TryParse(yourValue, out courseID))
        {
            DataGrid dg = new DataGrid();
            CourseDataAccess oCourseDataAccess = new CourseDataAccess();

            ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // Get provider information from Session 

            ParticipantDataAccess participantDAL = new ParticipantDataAccess();
            IList<Participant> participantList = participantDAL.GetAllParticipantByCourseIDProviderID(courseID, provider.ID); // Get Participants of the course

            DataTable tab = ConvertToDataTable(participantList);
            ConvertToCSV(tab);

        }
    }
    public DataTable ConvertToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties =
           TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        return table;

    }
    public void ConvertToCSV(DataTable table)
    {
        StringBuilder content = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            DataRow dr1 = (DataRow)table.Rows[0];
            int intColumnCount = dr1.Table.Columns.Count;
            int index = 1;

            //add column names
            foreach (DataColumn item in dr1.Table.Columns)
            {
                content.Append(String.Format("\"{0}\"", item.ColumnName));
                if (index < intColumnCount)
                    content.Append(",");
                else
                    content.Append("\r\n");
                index++;
            }

            //add column data
            foreach (DataRow currentRow in table.Rows)
            {
                string strRow = string.Empty;
                for (int y = 0; y <= intColumnCount - 1; y++)
                {
                    strRow += "\"" + currentRow[y].ToString() + "\"";

                    if (y < intColumnCount - 1 && y >= 0)
                        strRow += ",";
                }
                content.Append(strRow + "\r\n");

            }
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.AddHeader("content-disposition", "attachment; filename=ASLAAttendees-" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".csv");
            Response.Write(content.ToString());
            Response.End();
        }
    }
    protected void uiLkDownloadCertificates_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string yourValue = btn.CommandArgument;
    }

    protected void gvExistingCourses_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Course thiscourse = (Course)e.Item.DataItem;
        if (thiscourse != null)
        {
            using (Utility.ASLA_Laces_ProdEntities item = new Utility.ASLA_Laces_ProdEntities())
            {
                Label LblModifiedDate = (Label)e.Item.FindControl("uiLblModifiedDate");
                Utility.tblParticipantCource course = item.tblParticipantCources.Where(x => x.CourseID==thiscourse.ID).OrderByDescending(y =>y.DateAdded).FirstOrDefault();
                if (course != null)
                {
                    LblModifiedDate.Text = course.DateAdded.Value.ToShortDateString();
                }
                else
                {
                    LblModifiedDate.Text = "None";
                }
                
            }
            
        }
    }

    //protected void uiLnkShow_Click(object sender, EventArgs e)
    //{
    //    LinkButton btn = (LinkButton)(sender);
    //    string yourValue = btn.CommandArgument;
    //    HiddenRecordsPerPage.Value = yourValue;
    //    LoadAllCourses();


    //}
}
