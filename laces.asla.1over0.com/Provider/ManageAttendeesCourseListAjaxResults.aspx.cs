using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Provider_ManageAttendeesCourseListAjaxResults : System.Web.UI.Page
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
            
                string strrecordsperpage = Request.QueryString["itemsperpage"] != null ? Request.QueryString["itemsperpage"] : "25";
                int recordsPerPage = 25;
                if (int.TryParse(strrecordsperpage, out recordsPerPage)) { };

                string strpage = Request.QueryString["page"] != null ? Request.QueryString["page"] : "1";
                int curpage = 1;
                if (int.TryParse(strpage, out curpage)) { };
                int skip = ((curpage - 1) * recordsPerPage);
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
                if (Location.Length > 0)
                {
                    courses = courses.Where(x => x.StateProvince == Location).ToList();
                }
                gvExistingCourses.DataSource = courses.Skip(skip).Take(recordsPerPage);
                gvExistingCourses.DataBind();            
        }
        else
        {

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
                Utility.tblParticipantCource course = item.tblParticipantCources.Where(x => x.CourseID == thiscourse.ID).OrderByDescending(y => y.DateAdded).FirstOrDefault();
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

}