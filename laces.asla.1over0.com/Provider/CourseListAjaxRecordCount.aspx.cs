using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Provider_CourseListAjaxRecordCount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER];

        if (provider != null)
        {
            string stritemsperpage = Request.QueryString["itemsperpage"] != null ? Request.QueryString["itemsperpage"] : "99999";
            int itemsPerPage = 99999;
            if (int.TryParse(stritemsperpage, out itemsPerPage)) { }

            string strpage = Request.QueryString["page"] != null ? Request.QueryString["page"] : "1";
            int page = 1;
            if (int.TryParse(strpage, out page)) { }

            string strKeyword = Request.QueryString["keyword"] != null ? Request.QueryString["keyword"] : "";
            string strEndDate = Request.QueryString["enddate"] != null ? Request.QueryString["enddate"] : "";
            string strStartDate = Request.QueryString["startdate"] != null ? Request.QueryString["startdate"] : "";
            string strState = Request.QueryString["state"] != null ? Request.QueryString["state"] : "";
            string strStatus = Request.QueryString["status"] != null ? Request.QueryString["status"] : "";
            string strSubject = Request.QueryString["subject"] != null ? Request.QueryString["subject"] : "";
            int totalCount = 0;
            CourseDataAccess objCourseDAL = new CourseDataAccess();
            IList<Course> courseResult = new List<Course>();
            courseResult = objCourseDAL.GetPagedCourseBySearchByProvider(strStartDate, strEndDate, strKeyword, 1, 99999, strStatus, provider.ID, strSubject, strState, ref totalCount);
            Response.Write(courseResult.Count.ToString());
        }
    }
}