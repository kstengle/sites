/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: CourseList.aspx.cs
 * Purpose/Function: Display Provider welcome page with courselist related to loggedin provider
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/08/2008      Create this Page with initial requirements
 --------------------------------------------------------------------------------*/
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
using System.Collections.Generic;
using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
using System.Diagnostics;
using System.Linq;
/// <summary>
/// Provider Welcome / Course Listing
/// </summary>
public partial class Provider_ProviderCourseList : ProviderBasePage
{
    protected int hrcount = 1;
    protected IList<Course> coursesRelatedtoProvider = new List<Course>();
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //CourseDataAccess objCourseDAL = new CourseDataAccess();
            //Provider provider = (Provider)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER];
            if (Request.QueryString["status"] != null)
            {
            }
            //{
            //    populateCourseStatusList("");
            //    ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER];
            //    coursesRelatedtoProvider = objCourseDAL.GetCoursesByProviderId(provider.ID, "T");

            //    uiLblCourseCount.Text = coursesRelatedtoProvider.Count.ToString();
            //    ///Populate Course Data List
            //    dlCourseList.DataSource = coursesRelatedtoProvider;
            //    dlCourseList.DataBind();

            //    if (coursesRelatedtoProvider.Count < 1)
            //    {
            //        dlCourseList.Visible = false;
            //        NoResult.Visible = true;
            //    }
            //}
            //else
            //{
            //    populateCourseStatusList(Request.QueryString["status"]);
            //    FilterCoursesBind("", "", "", "", "", Request.QueryString["status"]);
            //}
            DisplayResults();
            populateStateList();
            populateSubjectList();
        }
    }

    /// <summary>
    /// Redirect to Add New Course Page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddNewCourses_Click(object sender, EventArgs e)
    {
        Response.Redirect("CourseDetails.aspx");
    }

    /// <summary>
    /// OnItemCreated event call this method to populate formatted "Start Date", "End Date" and "Course Title"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>    
    

    /// <summary>
    /// Get formatted location string with city and state
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
    protected string getFormattedLocation(string City, string StateProvince)
    {
        ///If State is empty
        if (StateProvince.Trim() == string.Empty)
            return Server.HtmlEncode(City);
        ///If city is empty
        else if (City == string.Empty)
            return StateProvince;
        ///If city and state both exist
        else
            return Server.HtmlEncode(City) + ", " + StateProvince;

    }
    
    protected void FilterCourses(object sender, EventArgs e)
    {
        DisplayResults();

     }    
    #region btnStatus_OnClick

    /// <summary>
    /// Call every time when click on the open close link button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
    #endregion
    #region btnStatus_OnClick

    /// <summary>
    /// Call every time when click on the open close link button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
   
    #endregion
    #region Populate State List
    /// <summary>
    ///  Populate State List 
    /// </summary>
    private void populateStateList()
    {
        StateDataAccess stateDAL = new StateDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int StateProvidenceID = int.Parse(LACESUtilities.GetApplicationConstants("StateProvinceContentID"));
        IList<State> stateList = stateDAL.GetAllStates(StateProvidenceID, webroot); // Get all States 

        ListItem defaultItem = new ListItem("- State List -", "");
        drpState.Items.Add(defaultItem);

        foreach (State state in stateList)
        {
            ListItem item = new ListItem(state.StateName, state.StateCode);
            drpState.Items.Add(item);
        }

        //for other International value
        drpState.Items.Add(new ListItem("- Other International -", "OI"));
    }
    #endregion

    #region Populate Subject List
    /// <summary>
    ///  Populate Subject List 
    /// </summary>
    private void populateSubjectList()
    {
        SubjectDataAccess subjectDAL = new SubjectDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int SubjectID = int.Parse(LACESUtilities.GetApplicationConstants("SubjectContentID"));
        IList<Subject> subjectList = subjectDAL.GetAllSubjects(SubjectID, webroot); // Get all Subjects

        foreach (Subject subject in subjectList)
        {
            ListItem item = new ListItem(subject.SubjectName, subject.SubjectID.ToString());
            drpSubject.Items.Add(item);
        }
    }
    #endregion
    protected void DisplayResults()
    {
        string strStatus = "";
        //if (Request.QueryString["status"] == null)
        //{
        //    foreach (ListItem li in uiChxBoxListStatus.Items)
        //    {
        //        if (li.Selected)
        //        {
        //            strStatus += strStatus.Length == 0 ? li.Value : "," + li.Value;
        //        }
        //    }
        //}
        //else
        //{
        //    strStatus = Request.QueryString["status"];
        //}
        string strStartDate = uiTxtStartDate.Text;
        string strEndDate = uiTxtEndDate.Text;
        string strKeyword = uiTxtKeyword.Text;
        string strState = drpState.SelectedValue;
        string strSubject = drpSubject.SelectedValue;
        int curpage = 1;
        if (Request.QueryString["curpage"] != null)
        {
            if (int.TryParse(Request.QueryString["curpage"], out curpage))
            { }

        }
        int ItemsPerPage = 9999;
        if (int.TryParse(Request.QueryString["itemsperpage"], out ItemsPerPage)) { }
        else {
            ItemsPerPage = 99999;
        }        
            string qstring;
            string previousqstring = "";
            string nextqstring = "";
            //string url = "CourseList.aspx";
            if(strStatus.Length>0)
            {
                previousqstring = "?status=" + strStatus;
                nextqstring = "?status=" + strStatus;
            }
            
            if (Request.QueryString["itemsperpage"] != null)
            {
             
                previousqstring += previousqstring.Length > 0 ? "&itemsperpage=" + Request.QueryString["itemsperpage"] : "?itemsperpage=" + Request.QueryString["itemsperpage"];
                nextqstring += nextqstring.Length > 0 ? "&itemsperpage=" + Request.QueryString["itemsperpage"] : "?itemsperpage=" + Request.QueryString["itemsperpage"];
                if (Request.QueryString["curpage"] != null)
                {
                    int temppage = 1;
                if (int.TryParse(Request.QueryString["curpage"], out temppage))
                {
                    previousqstring = previousqstring.Length > 0 ? previousqstring +  "&curpage=" + (temppage - 1).ToString() : previousqstring + "?curpage=" + (temppage - 1).ToString();
                    nextqstring = nextqstring.Length > 0 ? nextqstring + "&curpage=" + (temppage + 1).ToString() : nextqstring + "?curpage=" + (temppage + 1).ToString();
                }
                else
                {
                    nextqstring += nextqstring.Length > 0 ? "&curpage=1": "?curpage=1";
                }
            }
            else
            {
                nextqstring += nextqstring.Length > 0 ? "&curpage=1" : "?curpage=1";
            }
        }
           // uiPaginationPrevious.Attributes.Add("href", "CourseList.aspx" + previousqstring);
           // uiPaginationNext.Attributes.Add("href", "CourseList.aspx" + nextqstring);
            //FilterCoursesBind(strStartDate, strEndDate, strKeyword, strState, strSubject, strStatus, curpage, ItemsPerPage);

     
    }
}