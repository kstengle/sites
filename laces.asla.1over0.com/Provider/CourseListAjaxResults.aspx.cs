using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Provider_CourseListAjaxResults : System.Web.UI.Page
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
            uiHdnResultCount.Value = courseResult.Count.ToString();
            if (courseResult.Count < 1)
            {
                dlCourseList.Visible = false;
                NoResult.Visible = true;
            }
            else
            {
                int pageBegin = 0;
                if (page > 1)
                {
                    pageBegin = (itemsPerPage * page) - itemsPerPage;
                }
                dlCourseList.Visible = true;
                NoResult.Visible = false;
                if (itemsPerPage < 999)
                {
                    dlCourseList.DataSource = courseResult.Skip(pageBegin).Take(itemsPerPage);
                }
                else
                {
                    dlCourseList.DataSource = courseResult;
                }
                dlCourseList.DataBind();
            }
        }
    }
    protected void btnActive_OnClick(object sender, EventArgs e)
    {
        LinkButton openCloseButton = sender as LinkButton;

        try
        {
            long courseID = Convert.ToInt32(openCloseButton.CommandArgument);
            string status = openCloseButton.CommandName;

            CourseDataAccess oCourseDataAccess = new CourseDataAccess();

            if (status == "T")
            {
                oCourseDataAccess.ChangeCourseActive(courseID, "F");
            }
            else if (status == "F")
            {
                oCourseDataAccess.ChangeCourseActive(courseID, "T");
            }
        }
        catch
        {
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnRenew_OnClick(object sender, EventArgs e)
    {
        LinkButton openCloseButton = sender as LinkButton;

        try
        {
            long courseID = Convert.ToInt32(openCloseButton.CommandArgument);
            string status = openCloseButton.CommandName;

            CourseDataAccess oCourseDataAccess = new CourseDataAccess();

            if (status == "Renew")
            {
                Session["ReplicateCourseID"] = courseID.ToString();
                Response.Redirect("coursedetails.aspx?RenewCourse=Y");
            }
        }
        catch
        {
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnReplicate_OnClick(object sender, EventArgs e)
    {
        LinkButton openCloseButton = sender as LinkButton;

        try
        {
            long courseID = Convert.ToInt32(openCloseButton.CommandArgument);
            string status = openCloseButton.CommandName;

            CourseDataAccess oCourseDataAccess = new CourseDataAccess();

            if (status == "Replicate")
            {
                Session["ReplicateCourseID"] = courseID.ToString();
                Response.Redirect("coursedetails.aspx?ReplicateCourse=Y");
            }
        }
        catch
        {
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnStatus_OnClick(object sender, EventArgs e)
    {
        LinkButton openCloseButton = sender as LinkButton;

        try
        {
            long courseID = Convert.ToInt32(openCloseButton.CommandArgument);
            string status = openCloseButton.CommandName;

            CourseDataAccess oCourseDataAccess = new CourseDataAccess();

            if (status == "Open")
            {
                oCourseDataAccess.ChangeCourseStatus(courseID, "Closed");
            }
            else if (status == "Closed")
            {
                oCourseDataAccess.ChangeCourseStatus(courseID, "Open");
            }
        }
        catch
        {
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void dlCourseList_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem dlItem = e.Item;
        if (dlItem.DataItem != null)
        {
            ///Populate Start Date field
            Label lblStartDate = (Label)dlItem.FindControl("lblStartDate");
            if (lblStartDate != null)
            {
                DateTime startDate = Convert.ToDateTime(DataBinder.Eval(dlItem.DataItem, "StartDate"));
                lblStartDate.Text = startDate.ToString("MM/dd/yyyy");
                //if (DateTime.Today >= startDate)
                //    lblStartDate.CssClass = "dateBold";
            }

            ///Populate End Date field
            Label lblEndDate = (Label)dlItem.FindControl("lblEndDate");
            if (lblEndDate != null)
            {
                DateTime endDate = Convert.ToDateTime(DataBinder.Eval(dlItem.DataItem, "EndDate"));
                lblEndDate.Text = endDate.ToString("MM/dd/yyyy");
                //if (DateTime.Today >= endDate)
                //    lblEndDate.CssClass = "dateBold";
            }

            ///Populate Course Title linked to Course Details page
            Label lblTitle = (Label)dlItem.FindControl("lblTitle");
            if (lblTitle != null)
            {
                ///Html encode the title text
                string title = Server.HtmlEncode(DataBinder.Eval(dlItem.DataItem, "Title").ToString());
                string courseId = DataBinder.Eval(dlItem.DataItem, "ID").ToString();
                lblTitle.Text = "<a class=\"courseTitleLink\" href='CourseDetails.aspx?CourseID=" + courseId + "'>" + title + "</a>";
            }

            Label lblStatus = (Label)dlItem.FindControl("lblStatus");
            LinkButton btnStatus = (LinkButton)dlItem.FindControl("btnStatus");
            LinkButton btnRenew = (LinkButton)dlItem.FindControl("uilbRenewButton");

            if (lblStatus != null)
            {
                ///Html encode the title text
                string status = Server.HtmlEncode(DataBinder.Eval(dlItem.DataItem, "Status").ToString().Trim());
                switch (status)
                {
                    case "CL":
                        status = "Closed";
                        break;
                    case "IC":
                        status = "Action Required";
                        break;
                    case "NP":
                        status = "Pending Approval";
                        break;
                    case "OP":
                        status = "Open";
                        break;
                    case "PT":
                        status = "Archived";
                        break;

                }
                if (status.ToLower() == "action required" || status.ToLower() == "pending approval")
                {
                    btnRenew.Visible = false;

                }
                if (status.Trim() == "Open")
                {
                    if (btnStatus != null)
                    {
                        btnStatus.Text = "Close Enrollment";
                        //btnStatus.CommandArgument = DataBinder.Eval(dlItem.DataItem, "id").ToString();
                        //btnStatus.CommandName = "Close Enrollment";
                    }
                    lblStatus.Text = "Active/Approved";
                }
                else if (status.Trim() == "Closed")
                {
                    if (btnStatus != null)
                    {
                        btnStatus.Text = "Open Enrollment";
                        //btnStatus.CommandArgument = DataBinder.Eval(dlItem.DataItem, "ID").ToString();
                        //btnStatus.CommandName = "Open Enrollment";
                    }
                    lblStatus.Text = "Enrollments are closed";
                }
                else
                {
                    lblStatus.Text = status.Trim();
                    if (btnStatus != null)
                        btnStatus.Visible = false;
                }
            }
            /*Start of section RegistrationEligibility*/
            //Label lblRegistrationEligibility = (Label)dlItem.FindControl("lblRegistrationEligibility");
            //string registrationEligibility = string.Empty;
            //if (lblRegistrationEligibility != null)
            //{
            //    ///Html encode the lblRegistrationEligibility text
            //    registrationEligibility = Server.HtmlEncode(DataBinder.Eval(dlItem.DataItem, "RegistrationEligibility").ToString());
            //    registrationEligibility=registrationEligibility.Trim();
            //    registrationEligibility = registrationEligibility.Replace("\n", "<br/>");                
            //    lblRegistrationEligibility.Text = registrationEligibility;
            //}
            //if (registrationEligibility == string.Empty)
            //{
            //    HtmlTableRow trRegistrationEligibility = (HtmlTableRow)dlItem.FindControl("trRegistrationEligibility");
            //    if (trRegistrationEligibility != null)
            //    {
            //        trRegistrationEligibility.Attributes.Add("style", "display:none;"); //display iRegistrationEligibility
            //    }
            //}
            /*End of section RegistrationEligibility*/
            /*Start Active*/
            LinkButton uilbActiveButton = (LinkButton)dlItem.FindControl("uilbActiveButton");
            if (DataBinder.Eval(dlItem.DataItem, "Active").ToString() == "T")
            {
                uilbActiveButton.Text = "De-activate";
            }
            else
            {
                uilbActiveButton.Text = "Activate";
            }
            // = DataBinder.Eval(dlItem.DataItem, "Active").ToString();



        }
    }
}