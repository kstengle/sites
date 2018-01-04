/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: Default.aspx.cs
 * Purpose/Function: Visitor landing page
 * Author: Alamgir Hossain
 * 
 * Version          Author              Date            Reason
 * 1.0              Alamgir Hossain     06/12/2008      Create this Page with initial requirements
 * 1.1              Md. Kamruzzaman     07/31/2008      RedirectToSectionSpecificDefaultPage function created         
 * 2.0              Md. Kamruzzaman     12/03/2008      Added custom SearchTextBox        
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
using System.Xml;
using System.IO;
using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
using System.Net;

public partial class Default : System.Web.UI.Page
{
    #region Member variable
    protected bool isInvalidLogin = false;
    #endregion

    #region Methods

    /// <summary>
    /// Page Load Event to load input fields initially
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlMeta htmlGoogleMeta = new HtmlMeta();
        htmlGoogleMeta.Attributes.Add("name", "google-site-verification");
        htmlGoogleMeta.Attributes.Add("content", "K6O__N94_Ynv_-1cyXov5nLAP0Hej6PqelOB22oi4kY");
        Header.Controls.Add(htmlGoogleMeta);
        //redirect users to their respective section home page
        //RedirectToSectionSpecificDefaultPage();
        uiLnkApplicationForm.HRef=GetPDFURL(LACESUtilities.GetApplicationConstants("ApplicationFormPDF"));

        checkLoggedInProvider();

        //change the footer image path
      //  Master.ChangeFooterImagePath = "";

        //set the approved provider link
        string appString = "ApprovedProviderGuidelines.aspx"; 
        if(appString!="")
            approvedProLink.Attributes["href"] = appString;
        
        if (!IsPostBack)
        {
            
            //get all approved providers
            IList<ApprovedProvider> oApprovedProviders;
            ApprovedProviderDataAccess oApprovedProviderDataAccess = new ApprovedProviderDataAccess();
            int totalCount = 0;
            oApprovedProviders = oApprovedProviderDataAccess.GetPagedApprovedProviderSearch("tblApprovedProvider.Status = 'Active'", 0, Int16.MaxValue, "OrganizationName", "asc", ref totalCount);

            foreach (ApprovedProvider ap in oApprovedProviders)
            {
                string orgName = ap.OrganizationName;
                if (orgName.Length > 48)
                    orgName = orgName.Substring(0, 45) + "...";
                lbEducationProvider.Items.Add(new ListItem(orgName, ap.ID.ToString()));
            }

            //Populate Subject Dropdown List
            populateSubjectList();

            //Populate State Dropdown List
            populateStateList();

            populateSlideshow();
            ///If Reached this page form Course Search Result page by clicking search again
            ///And having criteria into the session then load existing selection value at the visitor section
            if (Request.QueryString["SearchAgain"] != null && Session[LACESConstant.SessionKeys.SEARCH_VISITOR_CRITERIA] != null)
            {
                ///Get Search criteria from session
                SearchCourse objSearchCriteria = (SearchCourse)Session[LACESConstant.SessionKeys.SEARCH_VISITOR_CRITERIA];
                if (objSearchCriteria != null)
                    loadExistingSelection(objSearchCriteria);
            }

            ///Focus to the first input control
            txtKeyword.Focus();
        }

        //txtKeyword.Attributes.Add("onclick", "javascript:RemoveDefaultText(this,'Search Here');");

        //chkDistanceEdu.Attributes.Add("onclick", "initializeLocation(this)");
        //ddlLocation.Attributes.Add("onchange", "uncheckDistanceEducation(this)");
    }

    /// <summary>
    /// Get Search Criteria and redirect to search result page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFindCourses_Click(object sender, EventArgs e)
    {
        //string searchCriteria = buildSearchCriteria();
        SearchCourse objSearchCourse = buildSearchCriteria();
        objSearchCourse.SortColumn = "StartDate";
        objSearchCourse.SortOrder = "DESC";
        using (Utility.ASLA_Laces_ProdEntities item = new Utility.ASLA_Laces_ProdEntities())
        {
            string subject = ddlSubject.SelectedValue;
            string location = ddlLocation.SelectedValue;
            Utility.LACES_Search_Terms terms = new Utility.LACES_Search_Terms();
            terms.Location = location;
            terms.SubjectArea = subject;
            terms.DateSearched = System.DateTime.Now;
            item.LACES_Search_Terms.Add(terms);
            item.SaveChanges();

        }


            //Add search criteria into session
            Session.Add(LACESConstant.SessionKeys.SEARCH_VISITOR_CRITERIA, objSearchCourse);
        Response.Redirect("~/visitor/CourseResult.aspx");
    }

    #region Populate Subject List
    /// <summary>
    ///  Populate Subject List from Database
    /// </summary>
    /// 
    private void populateSlideshow()
    {
        string externalUrl = "http://asla-responsive.dev01.1over0.com/CustomReturnHTMLSnippet.aspx";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(externalUrl);
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream st = resp.GetResponseStream();
        StreamReader sr = new StreamReader(st);
        string buffer = sr.ReadToEnd();
        sr.Close();
        st.Close();
        uiDivSlidehshow.InnerHtml = buffer;
    }
    private void populateSubjectList()
    {
        SubjectDataAccess subjectDAL = new SubjectDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int SubjectID = int.Parse(LACESUtilities.GetApplicationConstants("SubjectContentID"));
        IList<Subject> subjectList = subjectDAL.GetAllSubjects(SubjectID, webroot);

        ListItem defaultItem = new ListItem("- Subject Area -", "");
        ddlSubject.Items.Add(defaultItem);
        foreach (Subject subject in subjectList)
        {
            ListItem item = new ListItem(subject.SubjectName, subject.SubjectName);
            ddlSubject.Items.Add(item);
        }
    }
    #endregion

    #region Populate State List
    /// <summary>
    ///  Populate State/Province List from database
    /// </summary>
    private void populateStateList()
    {
        StateDataAccess stateDAL = new StateDataAccess();
        string webroot = LACESUtilities.GetApplicationConstants("ASLARoot");
        int StateProvidenceID = int.Parse(LACESUtilities.GetApplicationConstants("StateProvinceContentID"));
        IList<State> stateList = stateDAL.GetAllStates(StateProvidenceID, webroot);

        ///Add default item
        ListItem defaultItem = new ListItem("- Location -", "");
        ddlLocation.Items.Add(defaultItem);

        /////Add Distance Education item
        //ListItem distanceEducation = new ListItem("Distance Education", "Distance Education");
        //ddlLocation.Items.Add(distanceEducation);

        foreach (State state in stateList)
        {
            ListItem item = new ListItem(state.StateName, state.StateCode);
            ddlLocation.Items.Add(item);
        }
    }
    #endregion

    /// <summary>
    /// Generate Search criteria from user selection
    /// </summary>
    /// <returns></returns>
    private SearchCourse buildSearchCriteria()
    {

        SearchCourse objSearchCourse = new SearchCourse();
        objSearchCourse.Keywords = txtKeyword.Text.Trim();

        ///Split search keword using space
        string[] kewords = objSearchCourse.Keywords.Replace("'", "''").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        string searchQuery = "";

        ///Criteria for Title field

        string titleQuery = "";

        ///If having double quotes consider exact match
        if (objSearchCourse.Keywords.StartsWith("\"") && objSearchCourse.Keywords.EndsWith("\""))
        {
            string exactText = objSearchCourse.Keywords.Substring(1, objSearchCourse.Keywords.Length - 2);
            titleQuery += "[Title] LIKE '" + exactText.Replace("'", "''") + "%' OR [Title] LIKE '% " + exactText.Replace("'", "''") + "%'";
        }
        else
        {
            ///Loop for every keyword
            foreach (string keyword in kewords)
            {
                if (titleQuery != "")
                    titleQuery += " OR ";
                titleQuery += "[Title] LIKE '" + keyword + "%' OR [Title] LIKE '% " + keyword + "%'";
            }
        }
        searchQuery += titleQuery;
        objSearchCourse.SearchFields = "Title";
        

        ///Criteria for Description field

        string descriptionQuery = "";

        ///If having double quotes consider exact match
        if (objSearchCourse.Keywords.StartsWith("\"") && objSearchCourse.Keywords.EndsWith("\""))
        {
            string exactText = objSearchCourse.Keywords.Substring(1, objSearchCourse.Keywords.Length - 2);
            descriptionQuery += "[Description] LIKE '%" + exactText.Replace("'", "''") + "%'";
        }
        else
        {
            ///Loop for every keyword
            foreach (string keyword in kewords)
            {
                if (descriptionQuery != "")
                    descriptionQuery += " OR ";
                descriptionQuery += "[Description] LIKE '%" + keyword + "%'";
            }
        }

        if (searchQuery != "")
        {
            searchQuery += " OR ";
        }
        searchQuery += descriptionQuery;

        if (objSearchCourse.SearchFields != "")
        {
            objSearchCourse.SearchFields += " / ";
        }
        objSearchCourse.SearchFields += "Description";
        

        ///Criteria for LearningOutcomes field
        string lOQuery = "";

        ///If having double quotes consider exact match
        if (objSearchCourse.Keywords.StartsWith("\"") && objSearchCourse.Keywords.EndsWith("\""))
        {
            string exactText = objSearchCourse.Keywords.Substring(1, objSearchCourse.Keywords.Length - 2);
            lOQuery += "[LearningOutcomes] LIKE '%" + exactText.Replace("'", "''") + "%'";
        }
        else
        {
            ///Loop for every keyword
            foreach (string keyword in kewords)
            {
                if (lOQuery != "")
                    lOQuery += " OR ";
                lOQuery += "[LearningOutcomes] LIKE '%" + keyword + "%'";
            }
        }

        if (searchQuery != "")
        {
            searchQuery += " OR ";
        }
        searchQuery += lOQuery;

        if (objSearchCourse.SearchFields != "")
        {
            objSearchCourse.SearchFields += " / ";
        }
        objSearchCourse.SearchFields += "Outcomes";
        

        if (searchQuery != "")
            searchQuery = "(" + searchQuery + ")";

        ///Criteria for Subject Area
        if (ddlSubject.SelectedIndex > 0)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[Subjects] LIKE '%" + ddlSubject.SelectedValue + "%'";
            objSearchCourse.Subject = ddlSubject.SelectedValue;
        }

        ///Criteria for Distance Education  
        //if (searchQuery != "")
        //    searchQuery += " AND ";
        //searchQuery += "[DistanceEducation] = 'Y'";
        //objSearchCourse.Location = "Distance Education";
       
        ///Criteria for Location
        if (ddlLocation.SelectedIndex > 0)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[tblCourseDetails].[StateProvince] = '" + ddlLocation.SelectedValue + "'";
            objSearchCourse.Location = ddlLocation.SelectedValue;
        }

        // search for educational provider
        string eduList = "";
        for (int i = 0; i < lbEducationProvider.Items.Count; i++)
        {
            if (lbEducationProvider.Items[i].Selected)
            {
                if (eduList != "") eduList += " OR ";
                eduList += "tblApprovedProvider.[ID] = '" + lbEducationProvider.Items[i].Value + "'";
                objSearchCourse.Providers += "," + lbEducationProvider.Items[i].Value;
            }
        }
        if (eduList != "")
        {
            if (searchQuery != "")
                searchQuery += " AND (" + eduList + ")";
            else searchQuery += "(" + eduList + ")";

            objSearchCourse.Providers = objSearchCourse.Providers.Substring(1);
        }
        if (txtStartDate.Text.Trim() != string.Empty && txtEndDate.Text.Trim() != string.Empty)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
                searchQuery += "[StartDate] <= '" + txtEndDate.Text.Trim() + "' AND [EndDate] >='" + txtStartDate.Text.Trim() +"'";
        }
        else
        {
            ///Criteria for Start Date
            if (txtStartDate.Text.Trim() != string.Empty)
            {
                if (searchQuery != "")
                    searchQuery += " AND ";
                searchQuery += "[StartDate] >= '" + txtStartDate.Text.Trim() + "'";
                objSearchCourse.StartDate = txtStartDate.Text.Trim();
            }

            ///Criteria for End Date
            if (txtEndDate.Text.Trim() != string.Empty)
            {
                if (searchQuery != "")
                    searchQuery += " AND ";
                searchQuery += "[EndDate] <= '" + txtEndDate.Text.Trim() + "'";
                objSearchCourse.EndDate = txtEndDate.Text.Trim();
            }
        }
        if (uiChkDistanceEducation.Checked)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[DistanceEducation] >= 'Y'";
            objSearchCourse.DistanceEducationOnly = "Y";
        }
        if (uiChkHealthSafetyWelfare.Checked)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[Health] >= 'Yes'";
            objSearchCourse.HSWOnly = "Yes";
        }
        //Debugger.Break();

        if (searchQuery == "")
            searchQuery = "[tblCourseDetails].[Status] = 'OP' AND [EndDate] >= '" + DateTime.Today + "' AND [tblApprovedProvider].[Status] = 'Active' AND [Active]='T'";
        else
            searchQuery += " AND ([tblCourseDetails].[Status] = 'OP' AND [EndDate] >= '" + DateTime.Today + "' AND [tblApprovedProvider].[Status] = 'Active' AND [Active]='T')";

        objSearchCourse.WhereCondition = searchQuery;
        return objSearchCourse;
    }

    /// <summary>
    /// Assign existing search criteria selection
    /// </summary>
    /// <param name="objSearch"></param>
    private void loadExistingSelection(SearchCourse objSearch)
    {
        ///Assign keyword
        txtKeyword.Text = objSearch.Keywords;

        ///Check check boxes according to previous selection criteria
        //if (objSearch.SearchFields.IndexOf("Title") == -1)
        //    chkTitle.Checked = false;
        //if (objSearch.SearchFields.IndexOf("Description") == -1)
        //    chkDescription.Checked = false;
        //if (objSearch.SearchFields.IndexOf("Outcomes") == -1)
        //    chkLearningOutcomes.Checked = false;

        ///Select subject
        if (objSearch.Subject.Trim() != string.Empty)
            ddlSubject.SelectedValue = objSearch.Subject.Trim();

        ///Select location
        if (objSearch.Location.Trim() != string.Empty)
            ddlLocation.SelectedValue = objSearch.Location.Trim();

        ///Load start date
        if (objSearch.StartDate != string.Empty)
            txtStartDate.Text = objSearch.StartDate;

        ///Load end date
        if (objSearch.EndDate != string.Empty)
            txtEndDate.Text = objSearch.EndDate;
    }

    /// <summary>
    /// Sign In Button handler. 
    /// Use to Match Email Address and password with existing email address and password 
    /// and get Provider information from database
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event Argument</param>
    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        try
        {
            string Email = txtEmail.Text;
            string Password = txtPassword.Text;
            if (String.IsNullOrEmpty(Email))
            {
                DisplayInvalidLogin();
            }
            else
            {
                ///Checking the  Email And Password Match Existing Provider values and save provider details in session
                SignInUser(Email, Password);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// <summary>
    /// Checking the  Email And Password Match Existing Provider values and return provider details
    /// </summary>
    /// <param name="email">Email of Provider</param>
    /// <param name="password">password</param>
    private void SignInUser(string email, string password)
    {
        ///Encrypt password using DES encryption
        string EncriptedPassword = LACESUtilities.Encrypt(password);
        ApprovedProviderDataAccess providerDAL = new ApprovedProviderDataAccess();

        //inactivate all expired providers
        providerDAL.InactivateApprovedProviders(0, DateTime.Now);

        ApprovedProvider provider = providerDAL.GetByEmailandPassword(email, EncriptedPassword);

        if (provider != null && provider.ID > 0 && provider.Status == LACESConstant.ProviderStatus.ACTIVE)
        {
            //Set Active Provider Details to Session
            Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] = provider;

            //Set Inactive Provider Details to null
            Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] = null;

            ///Redirect to CourseListing page of Provider
            Response.Redirect("~/Provider/AddCourses.aspx");
            //Response.Redirect("~/Provider/CourseDetails.aspx");
        }
        else if (provider != null && provider.ID > 0 && provider.Status == LACESConstant.ProviderStatus.INACTIVE)
        {
            //Set Inactive Provider Details to Session
            Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] = provider;

            //Set Active Provider Details to null
            Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] = null;

            ///Redirect to CourseListing page of Provider
            Response.Redirect("~/Provider/Renew.aspx");
        }
        else
        {
            DisplayInvalidLogin();
        }



    }

    private void DisplayInvalidLogin()
    {
        if (IsPostBack == true)
        {  //If Provider Emal addres and password does not match invaid login message is displayed
            lblErrorSummary.Text = LACESConstant.Messages.INVALID_LOGIN;
            txtEmail.Focus();
            isInvalidLogin = true;

        }
        else
        {
            lblErrorSummary.Text = "";
        }
    }
    #endregion

    #region btnSearch_Click

    /// <summary>
    /// Call every time when click one the search page
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //get the ui data
        CourseRecord oCourseRecord = new CourseRecord();
        oCourseRecord.ASLANumber = txtASLANumber.Text.Trim().Replace("'", "''");
        oCourseRecord.CLARBNumber = txtCLARBNumber.Text.Trim().Replace("'", "''");
        oCourseRecord.CSLANumber = txtCSLANumber.Text.Trim().Replace("'", "''");

        //if (txtFirstName.Text.Trim().Length < 3) oCourseRecord.FirstName = txtFirstName.Text.Trim().Replace("'", "''");
        //else
        //{
        //    oCourseRecord.FirstName = txtFirstName.Text.Trim().Substring(0,3).Replace("'", "''");
        //}
        oCourseRecord.FirstName = txtFirstName.Text.Trim().Replace("'", "''");
        oCourseRecord.FLNumber = txtFLNumber.Text.Trim().Replace("'", "''");
        oCourseRecord.LastName = txtLastName.Text.Trim().Replace("'", "''");
        oCourseRecord.MiddleInitial = txtMiddleInitial.Text.Trim().Replace("'", "''");
        oCourseRecord.Email = txtEmailSearch.Text.Trim().Replace("'", "''");
        //save the search data to session 
        Session[LACESConstant.SessionKeys.COURSE_RECORD_OBJ] = oCourseRecord;

        //redirect to the CourseRecordResult page 
        Response.Redirect("CourseRecordResult.aspx?PageIndex=0");//&SortColumn=StartDate&SortOrder=Desc
    }

    #endregion

    #region RedirectToSectionSpecificDefaultPage
    /// <summary>
    /// This function will redirect users to their respective 
    /// section home page if user is logged in
    /// </summary>
    private void RedirectToSectionSpecificDefaultPage()
    {
        //if current user is active provider
        if (Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] != null)
        {
            Response.Redirect("Provider/CourseList.aspx", true);
        }
        //if current user is inactive provider
        else if (Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] != null)
        {
            Response.Redirect("Provider/Renew.aspx", true);
        }
        //if current user is admin
        else if (Session[LACESConstant.SessionKeys.LOGEDIN_ADMIN] != null && Session[LACESConstant.SessionKeys.LOGEDIN_ADMIN].ToString() == "Yes")
        {
            Response.Redirect("Admin/PendingCourses.aspx", true);
        }
    } 
    #endregion

    /// <summary>
    /// Check that user is logged in or not as a provider. If logged in hide login control and display logout link.
    /// </summary>
    private void checkLoggedInProvider()
    {
        //if current user is provider
        if (Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] != null || Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] != null)
        {
            string sessionKeyForProvider = LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER;

            //if current user is inactive
            if (Session[LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER] != null)
            {
                sessionKeyForProvider = LACESConstant.SessionKeys.LOGEDIN_INACTIVE_PROVIDER;
            }

            //get the provider from session
            ApprovedProvider sessionProvider = (ApprovedProvider)Session[sessionKeyForProvider]; // Get provider information from Session 

            //Show loggedin provider name
            Label lblProviderName = (Label)Master.FindControl("lblProviderName");
            if (lblProviderName != null)
                lblProviderName.Text = "Signed in as: " + Server.HtmlEncode(sessionProvider.OrganizationName);


            //load the provider menu bar
            //PlaceHolder phMenu =(PlaceHolder) Master.FindControl("uiPhMenu");
            //phMenu.Controls.Clear();

            ////if provider is active,provider menu will be shown
            //if (sessionProvider.Status == LACESConstant.ProviderStatus.ACTIVE)
            //{
            //    phMenu.Controls.Add(LoadControl("~/usercontrols/ProviderMenu.ascx"));
            //}
            ////if provider is inactive, inactive provider menu will be shown
            //else if (sessionProvider.Status == LACESConstant.ProviderStatus.INACTIVE)
            //{
            //    phMenu.Controls.Add(LoadControl("~/usercontrols/InactiveProviderMenu.ascx"));
            //}

            try
            {
                //change the login status
                ((HtmlTableCell)Master.FindControl("loginTd")).Attributes["class"] = "loggedIn providerloginStatus";
            }
            catch (Exception ex) { }

            uiPhLoginArea.Visible = false;
        }
        else if (Session[LACESConstant.SessionKeys.LOGEDIN_ADMIN] != null && Session[LACESConstant.SessionKeys.LOGEDIN_ADMIN].ToString() == "Yes")
        {
            Label lblAdminName = (Label)Master.FindControl("lblProviderName");
            if (lblAdminName != null)
                lblAdminName.Text = "Signed in as: Administrator";

            //load the admin menu bar dynamically
      //      HtmlTableCell oHtmlTableCell = (HtmlTableCell)Master.FindControl("tdMenu");
      //      oHtmlTableCell.Controls.Clear();
      //      oHtmlTableCell.Controls.Add(LoadControl("~/usercontrols/AdminMenu.ascx"));
        }
    }
    protected string GetPDFURL(string PDFLibraryID)
    {
        string pdfurl="";
        XmlDocument GetPdfXML = new XmlDocument();        
        GetPdfXML.Load(LACESUtilities.GetApplicationConstants("URLToGetPDFFilename") + "?id=" + PDFLibraryID);
        pdfurl =LACESUtilities.GetApplicationConstants("ASLABaseURL") + GetPdfXML.SelectSingleNode("root/filename").InnerText; ;


        return pdfurl;

    }
}
