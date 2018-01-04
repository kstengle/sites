using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Pantheon.ASLA.LACES.DataAccess;
using Pantheon.ASLA.LACES.Common;

public partial class Admin_EditAllCourseAttendees : AdminBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Request.QueryString["CourseID"] != null)
        {
            long lngCourseID;
            if(long.TryParse(Request.QueryString["CourseID"], out lngCourseID))
            {
                if (Request.QueryString["fname"] != null)
                {
                    string strDeletedFirstName = Request.QueryString["fname"];
                    string strDeletedLastName = Request.QueryString["lname"];
                    uiLblDeletedAlert.Text = strDeletedFirstName + " " + strDeletedLastName + " has been deleted <br /><br />";
                    uiLblDeletedAlert.Visible = true;
                }

                PopulateAttendeeList(lngCourseID);
                CourseDataAccess objCourseDAL = new CourseDataAccess();
                Course selectedCourse = objCourseDAL.GetCoursesDetailsByID(lngCourseID);
                if (selectedCourse != null)
                {
                    uiLblCourseName.Text = selectedCourse.Title;
                }
            }
        }        
    }
    protected void PopulateAttendeeList(long lngCourseID)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LACESConnectionString"].ToString());

        conn.Open();
        DataSet ds = new DataSet();
        string sqlStatement = "GetAllParticipantByCourse";

        SqlCommand cmd = new SqlCommand(sqlStatement, conn);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CourseID", lngCourseID);        
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        adapter.Fill(ds);
        uiGVAllAttendees.DataSource = ds;
        uiGVAllAttendees.DataBind();
                
       
    }
    protected void gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strFirstName = "", strLastName = "", strASLAMemberNumber = "", strCLARBNumber = "", strFloridaStateNumber = "", strMiddleInitial = "", strCSLANumber="", strEmail="";
        WebControl wc = e.CommandSource as WebControl;
        GridViewRow row = wc.NamingContainer as GridViewRow;
        object strid = (object)uiGVAllAttendees.DataKeys[row.RowIndex].Value;
        long participantID;
        if (long.TryParse(strid.ToString(), out participantID))
        {
            if (e.CommandArgument.ToString().ToLower() == "save")
            {
                TextBox tbFirstName = (TextBox)row.FindControl("uiTxtFirstName");
                strFirstName = tbFirstName != null ? tbFirstName.Text : "";
                TextBox tbLastName = (TextBox)row.FindControl("uiTxtLastName");
                strLastName = tbLastName != null ? tbLastName.Text : "";
                TextBox tbAslaMemberNumber = (TextBox)row.FindControl("uiTxtASLAMemberNumber");
                strASLAMemberNumber = tbAslaMemberNumber != null ? tbAslaMemberNumber.Text : "";
                TextBox tbCLARBNumber = (TextBox)row.FindControl("uiTxtCLARBNumber");
                strCLARBNumber = tbCLARBNumber != null ? tbCLARBNumber.Text : "";
                TextBox tbFloridaNumber = (TextBox)row.FindControl("uiTxtFloridaStateNumber");
                strFloridaStateNumber = tbFloridaNumber != null ? tbFloridaNumber.Text : "";
                TextBox tbMiddleInitial = (TextBox)row.FindControl("uiTxtMiddleInitial");
                strMiddleInitial = tbMiddleInitial != null ? tbMiddleInitial.Text : "";
                TextBox tbCSLANumber = (TextBox)row.FindControl("uiTxtCSLANumber");
                strCSLANumber = tbCLARBNumber != null ? tbCSLANumber.Text : "";
                TextBox tbEmail = (TextBox)row.FindControl("uiTxtEmail");
                Label lblResponse = (Label)row.FindControl("uiLblResponse");
                strEmail = tbEmail != null ? tbEmail.Text : "";
                try
                {
                    ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
                    Participant currentParticipant = new Participant();

                    //check weather the participant is exist
                    currentParticipant = oParticipantDataAccess.GetParticipantByID(participantID);

                    if (currentParticipant != null)
                    {
                        //Attendees exist so update

                        currentParticipant.LastName = strLastName;
                        currentParticipant.FirstName = strFirstName;
                        currentParticipant.ASLANumber = strASLAMemberNumber;
                        currentParticipant.CLARBNumber = strCLARBNumber;
                        currentParticipant.FloridaStateNumber = strFloridaStateNumber;
                        currentParticipant.ID = participantID;
                        currentParticipant.CSLANumber = strCSLANumber;
                        currentParticipant.MiddleInitial = strMiddleInitial;
                        currentParticipant.Email = strEmail;
                        currentParticipant = oParticipantDataAccess.Update(currentParticipant);
                        lblResponse.Text = "Updated";
                    }
                }
                catch (Exception ex)
                {
                    lblResponse.Text = ("Error:" + ex.Message);
                }
            }
            else if (e.CommandArgument.ToString().ToLower() == "remove")
            {
                ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
                Participant currentParticipant = new Participant();

                //check weather the participant is exist
                currentParticipant = oParticipantDataAccess.GetParticipantByID(participantID);

                if (currentParticipant != null)
                {
                    long lngCourseID;
                    if (long.TryParse(Request.QueryString["CourseID"], out lngCourseID))
                    {
                        bool isDeleted= oParticipantDataAccess.Delete(lngCourseID, participantID);
                        if (isDeleted)
                        {
                            Response.Redirect("EditAllCourseAttendees.aspx?CourseID=" + lngCourseID.ToString() + "&fname=" + currentParticipant.FirstName + "&lname=" + currentParticipant.LastName);
                        }
                    }
                    
                }
            }
        }
      
    }
}