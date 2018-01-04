
/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: CourseDetails.aspx
 * Purpose/Function: The display of Course Details Page
 *
 * Author: Wasim Majid
 * Version              Author            Date             Reason 
 * 1.0                 Wasim Majid      01/27/2008   Initial development
 * 1.1                 Matiur Rahman    02/28/2008   Updated health of course (task 6318)
 * 1.1                 Alamgir Hossain  05/09/2008   Work on Enhancement 2 
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

using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
using System.Diagnostics;

/// <summary>
///  Visitor's Course Detail Page
/// </summary>
public partial class Visitor_CourseDetails : System.Web.UI.Page
{
    protected string onclick = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // title of the page 
        //this.Title =  LACESConstant.LACES_TEXT + " - Course Details";

        if (Request.QueryString[LACESConstant.QueryString.COURSE_ID] != null)
        {
            long courseID = Convert.ToInt64(Request.QueryString[LACESConstant.QueryString.COURSE_ID]);
            populateCourseInformation(courseID);
        }       
    }

    private void populateCourseInformation(long courseID)
    {
        StringBuilder strbKeywords = new StringBuilder();
        CourseDataAccess courseDAL = new CourseDataAccess();
        Course course = courseDAL.GetCoursesDetailsByIDForVisitors(courseID, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)); // Get Courses Details from Course ID

        if (course == null || course.ID < 1)
        {
            lblMsg.Text = LACESConstant.Messages.COURSE_NOT_FOUND_IN_VISITOR;
            return;
        }

        if (course.RegistrationEligibility != "")
        {
            lblRegEligibility.Text = Server.HtmlEncode(course.RegistrationEligibility);
        }
        else {
            uiPhRegEligibility.Visible = false;
        }
        lblTitle.Text = Server.HtmlEncode(course.Title);
        lblStartDate.Text = course.StartDate.ToString("MM/dd/yyyy");
        lblEndDate.Text = course.EndDate.ToString("MM/dd/yyyy");
        //course.LearningOutcomes = (course.LearningOutcomes.Length > 600) ? course.LearningOutcomes.Substring(0, 600) : course.LearningOutcomes;
        //lblLeariningOutcome.Text = "<pre>" + Server.HtmlEncode(course.LearningOutcomes) + "</pre>";
        lblLeariningOutcome.Text = Server.HtmlEncode(course.LearningOutcomes).Replace("\n", "<br/>");
        if (course.Instructors != "")
            lblInstructors.Text = Server.HtmlEncode(course.Instructors);
        else
            uiPhInstructors.Visible = false;
        lblDescription.Text = Server.HtmlEncode(course.Description).Replace("\n", "<br/>");
        
        //lblStateProvience.Text = Server.HtmlEncode(course.StateProvince).Replace("\n", "<br/>");

        //if (lblStateProvience.Text.Trim() == "") trState.Visible = false;

        if (!course.Hyperlink.StartsWith("ftp://") && !course.Hyperlink.StartsWith("https://") && !course.Hyperlink.StartsWith("http://"))
        {
            course.Hyperlink = "http://" + course.Hyperlink;
        }

        if (course.City != "")
        {
            lblCityState.Text = getFormattedLocation(course);
        }
        else
        {
            uiPhCity.Visible = false;
        }
        //if (course.City.Trim().Equals(string.Empty) && course.StateProvince.Trim().Equals(string.Empty))
        //{

        //}
        //else
        //{
        //    lblCityState.Text += "";
        //}
        //lblCityState.Text += course.StateProvince;
        lblDistanceEdu.Text = (course.DistanceEducation == "Y") ? "Yes" : "No";
        lblCourseEquiv.Text = (course.CourseEquivalency == "Y") ? "Yes" : "No";
        lblHealthSafty.Text = (course.Healths == "No") ? "No" : "Yes";

        lblProvider.Text = Server.HtmlEncode(course.ProviderName);
        if(course.Hyperlink.Length>0)
        {          
            if (course.Hyperlink.IndexOf("@") > 0 && course.Hyperlink.IndexOf(".") > 0)
            {
                uiAWebsiteRegistration.HRef = "mailto:" + course.Hyperlink.Replace("http://","");
                uiAWebsiteRegistration.InnerText = course.Hyperlink.Replace("http://", "");
            }
            else
            {
                uiAWebsiteRegistration.HRef = course.Hyperlink;
                uiAWebsiteRegistration.InnerText = course.Hyperlink;
            }
            
        }
        lblHour.Text = course.Hours;
       // onclick = "onclick=window.open('" + course.Hyperlink.Replace("'", "").Replace("<", "").Replace(">", "") + "')";
        onclick = course.Hyperlink.Replace("'", "").Replace("<", "").Replace(">", "");
        // Populate Informations for subject drop down menu
        strbKeywords.Append(course.Subjects);
        strbKeywords.Append("," + course.City + " " + course.StateProvince);
        strbKeywords.Append("," + course.ProviderName);
        strbKeywords.Append("," + course.Instructors);
        HtmlMeta KeywordsMetaTag = new HtmlMeta();
        KeywordsMetaTag.Name = "keywords";
        KeywordsMetaTag.Content = strbKeywords.ToString();       
        this.Page.Header.Controls.Add(KeywordsMetaTag);

        string strDescMeta = course.Description.Length > 150 ? course.Description.Substring(0, 150) : course.Description;
        var lastOperatorIndex = strDescMeta.LastIndexOfAny(new char[] { '.', '?', '!'});
        strDescMeta = strDescMeta.Substring(0, lastOperatorIndex + 1);
        HtmlMeta DescriptionMetaTag = new HtmlMeta();
        DescriptionMetaTag.Name = "description";
        DescriptionMetaTag.Content = strDescMeta.ToString();
        this.Page.Header.Controls.Add(DescriptionMetaTag);



        lblSubject.Text = string.Empty;
        string[] subjectList = course.Subjects.Split(',');
        if (course.Subjects != string.Empty)
        {
            for (int j = 0; j < subjectList.Length; j++)
            {
                if (lblSubject.Text.Trim().Equals(string.Empty))
                {
                    lblSubject.Text = subjectList[j];
                }
                else
                {

                    lblSubject.Text += "<br />" + subjectList[j];
                }
            }
        }
        lblCourseCode.Text = course.ProviderCustomCourseCode;
 
        this.Title =Server.HtmlEncode(course.Title) + " | LA CES™" ;
    }

    /// <summary>
    /// Get formatted location string with city and state
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
    protected string getFormattedLocation(Course course)
    {
        ///If State is empty
        if (course.StateProvince.Trim() == string.Empty)
            return Server.HtmlEncode(course.City);
        ///If city is empty
        else if (course.City == string.Empty)
            return course.StateProvince;
        ///If city and state both exist
        else
            return Server.HtmlEncode(course.City) + ", " + course.StateProvince;

    }
}
