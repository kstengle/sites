using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Provider_ManageAttendeesCourseListAjaxResultCount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER];

        if (provider != null)
        {
            string courseTitle = Request.QueryString["title"] != null ? Request.QueryString["title"] : "";
            string StartDate = Request.QueryString["startdate"] != null ? Request.QueryString["startdate"] : "";
            string EndDate = Request.QueryString["enddate"] != null ? Request.QueryString["enddate"] : "";
            string Location = Request.QueryString["location"] != null ? Request.QueryString["location"] : "";
            string FirstName = Request.QueryString["first"] != null ? Request.QueryString["first"] : "";
            string LastName = Request.QueryString["last"] != null ? Request.QueryString["last"] : "";
            string Email = Request.QueryString["email"] != null ? Request.QueryString["email"] : "";
            string strOrderBy;
            if (Request.QueryString["Sort"] != null && Request.QueryString["Sort"].Length > 0)
            {
                strOrderBy = Request.QueryString["Sort"].ToString();
            }
            else
            {
                strOrderBy = "S";
            }
            //create the coures data access
            CourseDataAccess objCourseDAL = new CourseDataAccess();

            //get the current provider

            IList<Course> courses;
            if (FirstName.Length == 0 && LastName.Length == 0 && Email.Length == 0)
            {
                courses = objCourseDAL.GetCoursesByProviderId(provider.ID, strOrderBy);
            }
            else
            {
                courses = objCourseDAL.GetCoursesByProviderIdAndAttendee(provider.ID, strOrderBy, FirstName, LastName, Email);
            }
            if (courseTitle.Length > 0)
            {
                courses = courses.Where(x => x.Title.ToLower().Contains(courseTitle.ToLower())).ToList();
            }
            if (StartDate.Length > 0)
            {
                DateTime dtStartDate = new DateTime();
                if (DateTime.TryParse(StartDate, out dtStartDate))
                {
                    courses = courses.Where(x => x.StartDate >= dtStartDate && x.EndDate >= dtStartDate).ToList();
                }
            }
            if (EndDate.Length > 0)
            {
                DateTime dtEndDate = new DateTime();
                if (DateTime.TryParse(EndDate, out dtEndDate))
                {
                    courses = courses.Where(x => x.EndDate < dtEndDate).ToList();
                }
            }
            Response.Write(courses.Count.ToString());
        }
    }
}