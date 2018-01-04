using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Provider_AddAttendee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PlaceHolder menuph =(PlaceHolder) this.Page.Master.FindControl("uiPhMenu");
        menuph.Controls.Clear();
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
       
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["courseid"] != null)
        {
            long courseid = 0;
            long.TryParse(Request.QueryString["courseid"], out courseid);
            if (courseid > 0)
            {

                List<Participant> participantList = new List<Participant>();

                string last_name = uiTxtLastName.Text;
                string first_name = uiTxtFirstName.Text;
                string middle_name = uiTxtMiddleName.Text;
                string clarb_number = uiTxtCLARBNumber.Text;
                string asla_number = uiTxtASLANumber.Text;
                string csla_number = uiTxtCSLANumber.Text;
                string florida_number = uiTxtFLLicenseNumber.Text;
                string email = uiTxtEmail.Text;

                Participant oParticipant = null;

                oParticipant = new Participant();

                oParticipant.LastName = last_name;
                oParticipant.FirstName = first_name;
                oParticipant.MiddleInitial = middle_name;
                oParticipant.ASLANumber = asla_number;
                oParticipant.CLARBNumber = clarb_number;
                oParticipant.CSLANumber = csla_number;
                oParticipant.FloridaStateNumber = florida_number;
                oParticipant.Email = email;
                //add in to the participant list
                participantList.Add(oParticipant);
                ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
                oParticipantDataAccess.AddParticipantByCourse(courseid, participantList);
                Response.Redirect(Request.Url.PathAndQuery + "&success=t");
            }
        }
    }
}