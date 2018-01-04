using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Provider_AddMultipleCourses : ProviderBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        Response.Cache.SetValidUntilExpires(false);
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        Response.Cache.SetNoStore();

        if (!IsPostBack)
        {
          //  populateCourseTypeList();
            populateStateList();
        }
    }
    //private void populateCourseTypeList()
    //{
    //    CourseTypeDataAccess courseTypeDAL = new CourseTypeDataAccess();
    //    IList<CourseCodeType> courseTypeList = courseTypeDAL.GetAllCourseCodeTypes(); // Get All Course Code

    //    foreach (CourseCodeType courseCodeType in courseTypeList)
    //    {
    //        ListItem item = new ListItem(courseCodeType.CodeType, courseCodeType.ID.ToString());
    //        drpCourseType.Items.Add(item);
    //    }
    //    //drpCourseType.Items.Add(new ListItem("- Other International -", "OL"));

    //    //if corse code available then visible the corse code table
    //    //if (courseTypeList.Count > 0)
    //    //{
    //    //    corseCodeTable.Style["display"] = "block";
    //    //}
    //}
    protected void UploadMultipleCourses(object sender, EventArgs e)
    {
        try
        {
            string Filename = uiFUPSpreadsheet.FileName;
            string ext = Path.GetExtension(Filename);
            if (ext == ".xls" || ext == ".xlsx")
            {
                string strMultiName = uiTxtMultiName.Text;
                string strStartDate = txtStartDate.Text;
                string strEndDate = txtEndDate.Text;
                string strCity = uiTxtCity.Text;
                string strState = drpState.SelectedValue;
         //       string strCourseCodeType = drpCourseType.SelectedItem.Text;
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LACESConnectionString"].ToString());

                conn.Open();
                DataSet ds = new DataSet();
                string sqlStatement = "LACES_InsertMultiCourse";

                SqlCommand cmd = new SqlCommand(sqlStatement, conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EventName", strMultiName);
                cmd.Parameters.AddWithValue("@EventStateDate", strStartDate);
                cmd.Parameters.AddWithValue("@EventEndDate", strEndDate);
                cmd.Parameters.AddWithValue("@EventCity", strCity);
                cmd.Parameters.AddWithValue("@EventState", strState);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                string strUploadCourseID = ds.Tables[0].Rows[0]["MultiCourseID"].ToString();
                UploadExcelFile(strUploadCourseID, "");
            }
            else if(ext==".csv")
            {
                uiLblResults.Text = "Invalid upload type. Only .xlsx and .xls are supported. Please save your csv in this format and resubmit.";
            }else{

                uiLblResults.Text = "Invalid upload type. Only .xlsx and .xls are supported.";
            }
        }
        catch (Exception ex)
        {
        }
    }
    private bool UploadExcelFile(string strUploadCourseID, string strCourseCodeType)
    {
        try
        {
            string fileLocation = "";
            fileLocation = AppDomain.CurrentDomain.BaseDirectory + "xls\\MultipleCourseUpload\\" + System.DateTime.Now.Ticks + uiFUPSpreadsheet.FileName;       
            //get the course id from query string            
            List<Course> courseList = new List<Course>();

            //create the file loacton
            FileStream oFileStream = File.Create(fileLocation);

            foreach (byte b in uiFUPSpreadsheet.FileBytes)
            {
                oFileStream.WriteByte(b);
            }
            oFileStream.Close();
            Course oCourse = null;

            DataTable oDataTable = LACESUtilities.ReadXLFile(fileLocation, "Courses");

            bool useNext = false;
            ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // get approved provider information from Session 
            System.Text.StringBuilder sbResult = new System.Text.StringBuilder();
            sbResult.Append("<br />");
            int CourseUploadSuccess=0;
            int CourseUploadMoreInfo=0;
            int CourseUploadFailed = 0;
            int currentRow = 0;
            if (oDataTable != null)
            {
                foreach (DataRow dr in oDataTable.Rows)
                {
                    oCourse = new Course();

                    string strCol1 = dr[0].ToString().Trim();

                    if (useNext)
                    {
                        string strCourseTitle = dr[0].ToString().Trim();
                        if (strCourseTitle.Length > 0)
                        {

                            oCourse.Title = strCourseTitle;
                            string strCourseRegUrl = dr[1].ToString().Trim();
                            oCourse.Hyperlink = strCourseRegUrl;
                            string strRegEligibility = dr[2].ToString().Trim();
                            oCourse.RegistrationEligibility = strRegEligibility;
                            string strStartDate = dr[3].ToString().Trim();
                            DateTime dtStart;
                            if (DateTime.TryParse(strStartDate, out dtStart))
                            {
                                oCourse.StartDate = dtStart;
                            }
                            else
                            {
                                oCourse.StartDate = DateTime.Parse("1/1/2010");
                            }

                            string strEndDate = dr[4].ToString().Trim();
                            DateTime dtEndDate;
                            if (DateTime.TryParse(strEndDate, out dtEndDate))
                            {
                                oCourse.EndDate = dtEndDate;
                            }
                            else
                            {
                                oCourse.EndDate = DateTime.Parse("1/1/2010");
                            }

                            string strDescription = dr[5].ToString().Trim();
                            oCourse.Description = strDescription;
                            string strCity = dr[6].ToString().Trim();
                            oCourse.City = strCity;
                            string strState = dr[7].ToString().Trim();
                            oCourse.StateProvince = strState;
                            string strDistanceEd = dr[8].ToString();
                            if (strDistanceEd.Length == 0)
                            {
                                oCourse.DistanceEducation = " ";
                            }
                            else
                            {
                                oCourse.DistanceEducation = strDistanceEd;
                            }

                            string strCourseEquivalency = dr[9].ToString();
                            if (strCourseEquivalency.Length == 0)
                            {
                                oCourse.CourseEquivalency = " ";
                            }
                            else
                            {
                                oCourse.CourseEquivalency = strCourseEquivalency;
                            }
                            string strSubjects = dr[10].ToString().Trim();
                            string strSubjects2 = dr[11].ToString().Trim();
                            if (strSubjects2.Length > 0)
                            {
                                strSubjects += ", " + strSubjects2;
                            }
                            oCourse.Subjects = strSubjects;

                            string strHealthSafety = dr[12].ToString().Trim();
                            oCourse.Healths = strHealthSafety;
                            string strPersonalDevelopmentHours = dr[13].ToString().Trim();
                            if (strPersonalDevelopmentHours.IndexOf(".") < 0)
                            {
                                strPersonalDevelopmentHours = strPersonalDevelopmentHours + ".00";
                            }
                            oCourse.Hours = strPersonalDevelopmentHours;
                            string strLearningOutcomes = dr[14].ToString().Trim();
                            oCourse.LearningOutcomes = strLearningOutcomes;
                            string strInstructors = dr[15].ToString().Trim();
                            oCourse.Instructors = strInstructors;
                            string strNoProprietary = dr[16].ToString().ToLower().Trim() == "yes" ? "Y" : "N";
                            oCourse.NoProprietaryInfo = strNoProprietary;
                            string strCourseCode = dr[17].ToString().Trim();
                            oCourse.ProviderID = provider.ID;
                            string strCourseErrors = LACESUtilities.CheckCourseValid(oCourse);
                            //oCourse.OrgStateCourseIDNumber = strCourseCode;
                            if (strCourseErrors.Length == 0)
                            {
                                oCourse.Status = "NP";
                            }
                            else
                            {
                                oCourse.Status = "IC";
                            }
                            oCourse.ProviderCustomCourseCode = strCourseCode;
                            CourseDataAccess cd = new CourseDataAccess();
                            long lngCourseID = -1;
                            try
                            {


                                if (long.TryParse(cd.AddCourseDetails(oCourse).ToString(), out lngCourseID))
                                {
                                    try
                                    {

                                        SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["LACESConnectionString"].ToString());
                                        conn2.Open();
                                        DataSet ds = new DataSet();
                                        string sqlStatement = "LACES_InsertMultiCourse_Courses";

                                        SqlCommand cmd = new SqlCommand(sqlStatement, conn2);

                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@MultiCourseID", strUploadCourseID);
                                        cmd.Parameters.AddWithValue("@CourseID", lngCourseID.ToString());
                                        cmd.ExecuteNonQuery();
                                        conn2.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        Response.Write(ex.Message);
                                        Response.End();
                                    }


                                //    if (strCourseCode.Trim().Length > 0)
                                //    {
                                //        CourseCodeDataAccess codeDAL = new CourseCodeDataAccess();
                                //        CourseCode code = new CourseCode();
                                //        long CodeID = GetCodeIDfromName(strCourseCodeType.Trim());
                                //        if (CodeID > 0)
                                //        {
                                //            //code.Description = descList[i];
                                //            code.CodeID = Convert.ToInt32(CodeID);
                                //            code.CourseID = lngCourseID;
                                //            code.Description = strCourseCode.Trim();
                                //            try
                                //            {
                                //                codeDAL.AddCourseCode(code); // Add course Code
                                //            }
                                //            catch (Exception ex)
                                //            {
                                //                uiLblResults.Text = ex.Message;
                                //                Response.End();
                                //            }
                                //        }
                                //        else
                                //        {
                                //            CourseTypeDataAccess courseTypeDAL = new CourseTypeDataAccess();
                                //            int codeID = courseTypeDAL.AddCodeType(strCourseCodeType.Trim());
                                //            code.CodeID = codeID;
                                //            code.CourseID = lngCourseID;
                                //            code.Description = strCourseCode.Trim();
                                //            try
                                //            {
                                //                codeDAL.AddCourseCode(code); // Add course Code
                                //            }
                                //            catch (Exception ex)
                                //            {
                                //                uiLblResults.Text = ex.Message;
                                //                Response.End();
                                //            }

                                //        }
                                //    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex.Message);
                                Response.End();
                            }







                            if (strCourseErrors.Length == 0)
                            {
                                CourseUploadSuccess++;
                                sbResult.Append("<span style=\"color:green\">SUCCESS </span>" + oCourse.Title + "<hr style=\"background-image: url(http://laces.asla.org/images/dotted.png); background-repeat: repeat-x; border: 0; height: 1px;\" />");
                            }
                            else
                            {
                                CourseUploadMoreInfo++;
                                sbResult.Append("<span style=\"color:orange\">ACTION REQUIRED</span> <a href=\"http://laces.asla.org/provider/CourseDetails.aspx?CourseID=" + lngCourseID.ToString() + "\" class=\"nostyle\">" + oCourse.Title + "</a><br />" + strCourseErrors + "<hr style=\"background-image: url(http://laces.asla.org/images/dotted.png); background-repeat: repeat-x; border: 0; height: 1px;\"/>");
                            }
                        }
                        else
                        {
                            DataRow drN = oDataTable.Rows[currentRow];
                            bool endprocess = true;
                            for (int i = 0; i < 17; i++)
                            {
                                if (drN[i].ToString().Trim().Length > 0)
                                {
                                    CourseUploadFailed++;
                                    sbResult.Append("<span style=\"color:red\"> ERROR: Excel Row " + (currentRow + 2).ToString() + " was missing a title which is required to begin processing</span><br /><hr style=\"background-image: url(http://laces.asla.org/images/dotted.png); background-repeat: repeat-x; border: 0; height: 1px;\"/>");
                                    endprocess = false;
                                    break;
                                }
                            }
                            if (endprocess)
                            {
                                DataRow drN1 = oDataTable.Rows[currentRow + 1];
                                if (drN1[0].ToString().Trim().Length == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (strCol1 == "*Course Title")
                    {
                        useNext = true;
                    }
                    currentRow++;
                    //long newCourse = cd.AddCourseDetails(oCourse);
                }
                if (useNext && (CourseUploadMoreInfo + CourseUploadSuccess > 0))
                {
                    string strMultiName = uiTxtMultiName.Text;
                    string strConfStartDate = txtStartDate.Text;
                    string strConfEndDate = txtEndDate.Text;
                    System.Text.StringBuilder Summary = new System.Text.StringBuilder();
                    Summary.Append("<br /><br /><br /><br /><strong>Thank you for your submission.</strong><br />");
                    Summary.Append("<br /><strong>Event Title: </strong> " + strMultiName + "<br />");
                    Summary.Append("<br /><strong>Start Date: </strong> " + strConfStartDate + "<br />");
                    Summary.Append("<br /><strong>End Date: </strong>" + strConfEndDate + "<br />");


                    Summary.Append("<br />" + (CourseUploadMoreInfo + CourseUploadSuccess).ToString() + " courses have been received.<br />");
                    Summary.Append(CourseUploadSuccess.ToString() + " courses are complete and pending approval by LA CES administrators.<br />");
                    Summary.Append(CourseUploadMoreInfo.ToString() + " courses are incomplete and require immediate attention.<br />");
                    if (CourseUploadFailed > 0)
                    {
                        Summary.Append(CourseUploadFailed.ToString() + " courses failed to upload and need to be resubmitted.<br />");
                    }
                    Summary.Append("<br />Please review the list below or go to <a href=\"http://laces.asla.org/provider/CourseList.aspx?status=NP\">Pending Courses</a> to review your individual course details. All pending courses that require additional information will be listed with a status of \"Action Required\".");
                    Summary.Append("<br /><br /><br />An email with this information will be sent to the primary contact for your account.");

                    Summary.Append("<br /><br /><br />Sincerely,");
                    Summary.Append("<br /><br /><br />LA CES Administrator");

                    uiLblResults.Text = Summary.ToString() + "<hr style=\"background-image: url(http://laces.asla.org/images/images/dotted.png); background-repeat: repeat-x; border: 0; height: 2px;\" />Upload Status – Course Title<br />" + sbResult.ToString();
                    SendEmail(Summary.ToString() + "<hr style=\"background-image: url(http://laces.asla.org/images/dotted.png); background-repeat: repeat-x; border: 0; height: 2px;\" />Upload Status– Course Title<br />" + sbResult.ToString(), provider.ApplicantEmail);
                }
                else
                {
                    BuildExcelError();
                    SendEmail(uiLblResults.Text, provider.ApplicantEmail);
                }
            }
            else
            {
            BuildExcelError();
            
            }
        }
        
        catch (Exception ex)
        {
            BuildExcelError();
            
        }
        return false;
    }    
    protected void BuildExcelError()
    {
        System.Text.StringBuilder strbError = new System.Text.StringBuilder();
        strbError.Append("<br /><br /><span style=\"color:red\">There has been an error with your submission.</span><br /><br />");
        strbError.Append("Please review your spreadsheet for errors to ensure all required fields have been completed and the template has not been altered. <br />");
        strbError.Append("<br />Troubleshooting suggestions:<br />");
        strbError.Append("<ul>");
        strbError.Append("<li>First, please be sure to use our <a href=\"http://laces.asla.org/LACES_Multicourse_Upload.xlsx\"> template</a>.");
        strbError.Append("<li>Then, confirm that you:");
        strbError.Append("<ul><li>Did not add, delete, or move a column in the template spreadsheet.</li>");
        strbError.Append("<li>Included a “Course Title” for each line of text entered.</li>");
        strbError.Append("<li>Completed all required fields denoted with an asterisk (*).</li>");
        strbError.Append("</ul></li></ul>");
        strbError.Append("For further assistance, please review the resources found under <a href=\"http://laces.asla.org/Tools.aspx\">Tools</a> or contact us at <a href=\"mailto:laces@asla.org\">laces@asla.org</a>.");


        uiLblResults.Text = strbError.ToString();
        return;
    }
    protected void SendEmail(string Text, string ProviderEmail)
    {
        try
        {

            SmtpClient smtpClient = new SmtpClient();
            //Get the SMTP server Address from SMTP Web.conf
            smtpClient.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER);
            //Get the SMTP post  25;
            smtpClient.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));

            MailMessage message = new MailMessage();

            message.From = new MailAddress(LACESUtilities.GetAdminFromEmail());

            //message.To.Add(LACESUtilities.GetAdminToEmail());
            //message.CC.Add(LACESUtilities.GetAdminCCEmail());

            //Get the email's TO from Web.conf
            message.To.Add(ProviderEmail);
            
            string BCC = LACESUtilities.GetApplicationConstants(LACESConstant.ADMIN_CONTACT_TO_EMAIL);
            message.CC.Add(LACESUtilities.GetApprovedProviderMailTo());
            
            message.Subject = "New Courses Added - Pending Approval";
            message.IsBodyHtml = true;
            message.Body = Text;    // Message Body
            if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
            {
                smtpClient.Send(message);   //Sending A Mail to Admin
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
        }
    }
    protected long GetCodeIDfromName(string strCourseCode)
    {
        long ID = 0;
        try
        {
            CourseTypeDataAccess courseTypeDAL = new CourseTypeDataAccess();
            CourseCodeType code = new CourseCodeType();
            code = courseTypeDAL.GetCodeTypeByName(Server.HtmlEncode(strCourseCode.Trim()).ToString());
            if (code != null)
            {
                ID = code.ID;
            }
            return ID;
        }
        catch (Exception ex)
        {
            return ID;
        }
    }
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

}