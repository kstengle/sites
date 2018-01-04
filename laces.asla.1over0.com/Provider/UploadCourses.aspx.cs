using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pantheon.ASLA.LACES.DataAccess;
using System.Collections.Generic;
using Pantheon.ASLA.LACES.Common;
using System.IO;
using System.Data;
using System.Data.SqlTypes;
public partial class Provider_UploadCourses : ProviderBasePage
{
    string fileLocation = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        //fileLocation = AppDomain.CurrentDomain.BaseDirectory + "xls\\" + System.DateTime.Now.Ticks + FileUpload1.FileName;
        //string strFilename = "Test.xlsx";
        fileLocation = AppDomain.CurrentDomain.BaseDirectory + "xls\\MultipleCourseUpload\\" + System.DateTime.Now.Ticks + FileUpload1.FileName;
       // fileLocation = AppDomain.CurrentDomain.BaseDirectory + "xls\\MultipleCourseUpload\\" + strFilename;        
        //check the upload file
        if (FileUpload1.PostedFile != null && FileUpload1.FileName != "")
        {
            //if (FileUpload1.PostedFile.ContentType == "application/vnd.ms-excel" && FileUpload1.PostedFile.ContentType != "application/xml" && FileUpload1.PostedFile.ContentType != "application/octet-stream")
            //{
                if (UploadExcelFile())
                {
                    //remove the created file from disk
                    //LACESUtilities.RemoveCreatedFileFromDisk(fileLocation);
                }
            //}
        }        
    }
    private bool UploadExcelFile()
    {
        try
        {
            //get the course id from query string            
            List<Course> courseList = new List<Course>();
            
            //create the file loacton
            FileStream oFileStream = File.Create(fileLocation);

            foreach (byte b in FileUpload1.FileBytes)
            {
                oFileStream.WriteByte(b);
            }
            oFileStream.Close();
            Course oCourse = null;
            
            DataTable oDataTable = LACESUtilities.ReadXLFile(fileLocation, "Courses");
            
            bool useNext = false;
            ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // get approved provider information from Session 
            System.Text.StringBuilder sbResult = new System.Text.StringBuilder();
            foreach (DataRow dr in oDataTable.Rows)
            {                
                oCourse = new Course();
                
                string strCol1 = dr[0].ToString().Trim();
                
                if (useNext)
                {
                    bool isAllRequiredFilled = true;
                    string strCourseTitle = dr[0].ToString().Trim();                    
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
                        oCourse.StartDate =(DateTime) SqlDateTime.MinValue;
                    }
                    
                    string strEndDate = dr[4].ToString().Trim();
                    DateTime dtEndDate;
                    if (DateTime.TryParse(strEndDate, out dtEndDate))
                    {
                        oCourse.EndDate = dtStart;
                    }
                    else
                    {
                        oCourse.EndDate =(DateTime) SqlDateTime.MinValue;
                    }
                    
                    string strDescription= dr[5].ToString().Trim();
                    oCourse.Description = strDescription;
                    string strCity = dr[6].ToString().Trim();
                    oCourse.City = strCity;
                    string strState = dr[7].ToString().Trim();
                    oCourse.StateProvince = strStartDate;
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
                    oCourse.Subjects = strSubjects;
                    string strHealthSafety = dr[11].ToString().Trim();
                    oCourse.Healths = strHealthSafety;
                    string strPersonalDevelopmentHours = dr[12].ToString().Trim();
                    oCourse.Hours = strPersonalDevelopmentHours;
                    string strLearningOutcomes = dr[13].ToString().Trim();
                    oCourse.LearningOutcomes = strLearningOutcomes;
                    string strInstructors = dr[14].ToString().Trim();
                    oCourse.Instructors = strInstructors;
                    string strNoProprietary = dr[15].ToString().Trim();
                    
                    string strCourseCode = dr[16].ToString().Trim();
                    oCourse.ProviderID = provider.ID;
                    string strCourseErrors = LACESUtilities.CheckCourseValid(oCourse);
                    //oCourse.OrgStateCourseIDNumber = strCourseCode;
                    if (strCourseErrors.Length==0)
                    {
                        oCourse.Status = "NP";
                    }
                    else
                    {
                        oCourse.Status = "IC";
                    }
                    CourseDataAccess cd = new CourseDataAccess();
                    long lngCourseID;
                    if(long.TryParse(cd.AddCourseDetails(oCourse).ToString(), out lngCourseID))
                    {
                        if (strCourseCode.Trim().Length > 0)
                        {
                            CourseCodeDataAccess codeDAL = new CourseCodeDataAccess();
                            CourseCode code = new CourseCode();
                            //code.Description = descList[i];
                            code.CodeID = Convert.ToInt32(strCourseCode);
                            code.CourseID = lngCourseID;
                            try
                            {
                                codeDAL.AddCourseCode(code); // Add course Code
                            }
                            catch (Exception ex)
                            {
                                uiLblResults.Text = ex.Message;
                                Response.End();
                            }
                        }
                    }
                    if (strCourseErrors.Length == 0)
                    {
                        sbResult.Append("SUCCESS" + lngCourseID.ToString() + " - " + oCourse.Title);
                    }
                    else
                    {
                        sbResult.Append(lngCourseID.ToString() + " - " + oCourse.Title + "<br />" + strCourseErrors);
                    }
                                        
                }
                if (strCol1 == "*Course Title")
                {
                    useNext = true;
                }
                //long newCourse = cd.AddCourseDetails(oCourse);
            }
            uiLblResults.Text = sbResult.ToString();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
        }
        return false;
    }
}