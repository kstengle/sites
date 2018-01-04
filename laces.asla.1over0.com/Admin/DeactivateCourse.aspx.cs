using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pantheon.ASLA.LACES.DataAccess;

public partial class Admin_DeactivateCourse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CourseDataAccess oCourseDataAccess = new CourseDataAccess();
        if(Request.QueryString["id"] != null)
        {
        long courseID = long.Parse(Request.QueryString["id"].ToString());
        oCourseDataAccess.ChangeCourseActive(courseID, "F");
        }
        Response.Redirect("CourseResult.aspx");
    }
}
