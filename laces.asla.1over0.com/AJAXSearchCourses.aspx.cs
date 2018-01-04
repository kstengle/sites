using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AJAXSearchCourses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            populateCourseStatusList();
        }
    }
    private void populateCourseStatusList()
    {
        CourseStatusDataAccess courseStatusDAL = new CourseStatusDataAccess();
        IList<CourseStatus> courseStatusList = courseStatusDAL.GetAllCourseStatus(); // Get All course Status

        foreach (CourseStatus courseStatus in courseStatusList)
        {
            ListItem item = new ListItem(courseStatus.Notes, courseStatus.StatusCode, true);
            uiChxBoxListStatus.Items.Add(item);
        }

    }
    protected void FilterCourses(object sender, EventArgs e)
    {
        CourseDataAccess objCourseDAL = new CourseDataAccess();
        int totalCount = 0;
        string strStatus = "";
        foreach (ListItem li in uiChxBoxListStatus.Items)
        {            
            if (li.Selected)
            {
                strStatus += strStatus.Length==0 ? li.Value :"," + li.Value;
            }
        }
        string strStartDate = txtStartDate.Text;
        string strEndDate = txtEndDate.Text;
        string strKeyword = uiTxtKeyword.Text;
        IList<Course> courseResult = new List<Course>();
        courseResult = objCourseDAL.GetPagedCourseBySearch(strStartDate, strEndDate, strKeyword, 1, 10000, strStatus, ref totalCount);
        uiRptCourses.DataSource = courseResult;        
        uiRptCourses.DataBind();
    }
}