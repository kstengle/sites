/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: LACESUtilities.cs
 * Purpose/Function: Common Utility methods
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/03/2008      Create this Page
 * 1.1              Shohel Anwar        01/24/2008      Add methods ReportError and GetMailMessageBody 
 * 1.2              Md. kamruzzaman     09/08/2009      Changed Provider Application Mail To Address
 --------------------------------------------------------------------------------*/
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO;
using System.Net.Mail;
using System.Data.OleDb;
using System.Collections;
using PayPal.Payments.Common.Utility;
using PayPal.Payments.Communication;

using Pantheon.ASLA.LACES.Common;
using System.Data.SqlTypes;

/// <summary>
/// Contain all utility methods using in LACES project
/// </summary>
public class LACESUtilities
{
    /// <summary>
    /// Constractor
    /// </summary>
    public LACESUtilities()
    {
        //
        // TODO: Add constructor logic here
        //
    }
#region Validate Course Data
    public static string CheckCourseValid(Course c)
    {
        string strErrorMessage = "";
        DateTime minDT = DateTime.Parse("1/1/2010");
        if(c.Hyperlink.Length==0)
        {
            strErrorMessage += "Course Registration URL Missing <br />";
        }
        if (c.RegistrationEligibility.Length > 150)
        {
            strErrorMessage += "Registration Eligibility Must not contain more than 150 characters. This has been truncated<br />";
        }
        if (c.StartDate == minDT)
        {
            strErrorMessage += "Start Date is invalid <br />";
        }
        if (c.EndDate == minDT)
        {
            strErrorMessage += "End Date is invalid <br />";
        }
        if (c.EndDate >c.StartDate.AddYears(2))
        {
            strErrorMessage += "The end date must be within 2 years of the start date<br />";
        }
        if (c.EndDate < c.StartDate)
        {
            strErrorMessage += "The end date must be after the start date<br />";
        }
        if (c.Description.Length == 0)
        {
            strErrorMessage += "Description is Required <br />";
        }
        //if (c.Description.Length > 3600)
       // {
        //    strErrorMessage += "Description is more than 3600 characters <br />";
       // }
        if (c.City.Length == 0)
        {
            strErrorMessage += "City is required <br />";
        }
        if (c.StateProvince.Length == 0)
        {
            strErrorMessage += "State is required <br />";
        }
        if (c.DistanceEducation.Trim().Length == 0)
        {
            strErrorMessage += "Distance Education Required  <br />";

        }
        if (c.CourseEquivalency.Trim().Length == 0)
        {
            strErrorMessage += "Course Equivalency Required <br />";
        }
        if (c.Subjects.Length == 0)
        {
            strErrorMessage += "Subjects Required <br />";
        }
        if (c.Healths.Length == 0)
        {
            strErrorMessage += "Health, Safety Welfare is Required<br />";
        }
        if(c.Hours.Length>0)
        {
            double HoursDouble;
            if (double.TryParse(c.Hours, out HoursDouble))
            {
                if (HoursDouble < 1.0)
                {
                    strErrorMessage += "Professional Development Hours must be at least 1.00 <br />";
                }
            }else
            {
                strErrorMessage += "Invalid Entry for Professional Development Hours <br />";
            }
        }else
        {
            strErrorMessage += "Professional Development Hours Required <br />";
        }
        if (c.LearningOutcomes.Length == 0)
        {
            strErrorMessage += "Learning Outcomes Required <br />";
        }
        if (c.Instructors.Length == 0)
        {
            strErrorMessage += "Instructors Required <br />";
        }
        if (c.NoProprietaryInfo.ToLower() !="y")
        {
            strErrorMessage += "Course must not contain proprietary information. <br />";
        }        
        return strErrorMessage;
    }
#endregion
    #region Get Application Configuration Values
    /// <summary>
    /// Returns the value of 'key' that is stored in web.config file
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetApplicationConstants(string key)
    {
        try
        {
            return ConfigurationManager.AppSettings[key];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    
    #region Encryption and Decryption

    /// <summary>
    /// Get the Des Key from Web.conf
    /// </summary>
    /// <returns>DES Provider</returns>
    private static DESCryptoServiceProvider GetDECKey()
    {
        // Create a new DES key.
        DESCryptoServiceProvider key = new DESCryptoServiceProvider();
        key.Key = Convert.FromBase64String(GetApplicationConstants("DESKey"));
        key.IV = Convert.FromBase64String(GetApplicationConstants("DESIV"));
        return key;
    }

    /// <summary>
    /// Encrypt the string.
    /// </summary>
    /// <param name="PlainText">Plain Text to entrypt</param>
    /// <returns></returns>
    public static string Encrypt(string PlainText)
    {
        DESCryptoServiceProvider key = GetDECKey();

        return Encrypt(PlainText, (SymmetricAlgorithm)key);
    }

    /// <summary>
    /// Encrypt the string.
    /// </summary>
    /// <param name="PlainText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Encrypt(string PlainText, SymmetricAlgorithm key)
    {
        // Create a memory stream.
        MemoryStream ms = new MemoryStream();

        // Create a CryptoStream using the memory stream and the 
        // CSP DES key.  
        CryptoStream encStream = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write);

        // Create a StreamWriter to write a string
        // to the stream.
        StreamWriter sw = new StreamWriter(encStream);

        // Write the plaintext to the stream.
        sw.WriteLine(PlainText);

        // Close the StreamWriter and CryptoStream.
        sw.Close();
        encStream.Close();

        // Get an array of bytes that represents the memory stream.
        // and convert it to string using Base64
        string encryptedString = Convert.ToBase64String(ms.ToArray());

        // Close the memory stream.
        ms.Close();

        // Return the encrypted string.
        return encryptedString;
    }

    /// <summary>
    /// Decrypt the byte array.
    /// </summary>
    /// <param name="encryptedString">Encrypted Text</param>
    /// <returns></returns>
    public static string Decrypt(string PlainText)
    {
        DESCryptoServiceProvider key = GetDECKey();

        return Decrypt(PlainText, (SymmetricAlgorithm)key);
    }

    /// <summary>
    /// Decrypt the byte array.
    /// </summary>
    /// <param name="encryptedString"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Decrypt(string encryptedString, SymmetricAlgorithm key)
    {
        byte[] CypherText = Convert.FromBase64String(encryptedString);
        // Create a memory stream to the passed buffer.
        MemoryStream ms = new MemoryStream(CypherText);

        // Create a CryptoStream using the memory stream and the 
        // CSP DES key. 
        CryptoStream encStream = new CryptoStream(ms, key.CreateDecryptor(), CryptoStreamMode.Read);

        // Create a StreamReader for reading the stream.
        StreamReader sr = new StreamReader(encStream);

        // Read the stream as a string.
        string val = sr.ReadLine();

        // Close the streams.
        sr.Close();
        encStream.Close();
        ms.Close();

        return val;
    }

    #endregion

    #region ReportError
    /// <summary>
    /// Send email to the on exception
    /// </summary>
    /// <param name="ex">Exception</param>
    public static void ReportError(Exception ex)
    {
        if (ex.Message == "Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack.")
        {
            return;
        }
        else if (ex.Message == "Thread was being aborted.")
        {
            return;
        }
        else if (ex.Message == "The content you have requested does not exist in the system.")
        {
            return;
        }

        SmtpClient smtpClient = new SmtpClient();
        MailMessage message = new MailMessage();

        try
        {
            //Get the SMTP server Address from Web.conf
            smtpClient.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER);
            //Get the SMTP port from Web.conf
            smtpClient.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));
            //Get the email's TO addresses from Web.conf
            string values = LACESUtilities.GetApplicationConstants(LACESConstant.CONSTANT_ERROR_REPORT_MAIL_TO);
            string[] emailAddesses = values.Split(';');

            //add individual addresses to the MailMessage class instance
            for (int i = 0; i < emailAddesses.Length; i++)
            {
                message.To.Add(emailAddesses[i]);
            }

            //Get the email's Subject from Web.conf
            message.Subject = LACESUtilities.GetApplicationConstants(LACESConstant.CONSTANT_ERROR_MAIL_SUBJECT);
            //Whether the message body would be displayed as html or not
            message.IsBodyHtml = true;
            //Get the email's FROM address from Web.conf
            message.From = new System.Net.Mail.MailAddress(LACESUtilities.GetApplicationConstants(LACESConstant.CONSTANT_ERROR_MAIL_FROM));
            //Get the email's BODY
            message.Body = GetMailMessageBody(ex);
            //Send the mail
            if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
            {
                smtpClient.Send(message);
            }
        }
        catch (Exception exp)
        {
            
        }
        finally
        {

        }
    }
    #endregion

    #region GetMailMessageBody
    /// <summary>
    /// Builds the error report mail message body
    /// </summary>
    /// <param name="ex">Exception</param>
    /// <returns>message body</returns>
    public static string GetMailMessageBody(Exception ex)
    {
        string messageBody = "";

        //create the email body
        messageBody += "<b>Browser: </b>" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version.ToString() + "<br/><br/>";
        messageBody += "<b>Date/Time: </b>" + DateTime.Now + "<br/><br/>";
        messageBody += "<b>Diagnostics: </b><br/>";
        messageBody += "&nbsp;&nbsp;<b>Error message: </b>" + ex.InnerException.Message + "<br/>"; ;
        messageBody += "&nbsp;&nbsp;<b>Stack trace: </b>" + ex.InnerException.StackTrace + "<br/><br/>";
        string referrer = "";
        if (HttpContext.Current.Request.UrlReferrer != null)
        {
            referrer = HttpContext.Current.Request.UrlReferrer.ToString();
        }
        else
        {
            referrer = "Self";
        }
        messageBody += "<b>Referrer: </b>" + referrer + "<br/>";
        messageBody += "<b>Url: </b>" + HttpContext.Current.Request.Url.ToString() + "<br/>";
        messageBody += "<b>Remote address: </b>" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

        return messageBody;
    }
    #endregion

    /// <summary>
    /// Get laces system from email address
    /// </summary>
    /// <returns>from email address</returns>
    public static string GetAdminFromEmail()
    { 
        string addressFromConfig  = GetApplicationConstants(LACESConstant.ADMIN_CONTACT_FROM_EMAIL);
        return addressFromConfig.Replace("(", "<").Replace(")", ">");
    }

    /// <summary>
    /// Get laces admin to email address
    /// </summary>
    /// <returns>to email address</returns>
    public static string GetAdminToEmail()
    {
        string addressFromConfig = GetApplicationConstants(LACESConstant.ADMIN_CONTACT_TO_EMAIL);
        return addressFromConfig.Replace("(", "<").Replace(")", ">");
    }

    /// <summary>
    /// Get laces admin cc email address
    /// </summary>
    /// <returns>cc email address</returns>
    public static string GetAdminCCEmail()
    {
        string addressFromConfig = GetApplicationConstants(LACESConstant.ADMIN_CONTACT_CC_EMAIL);
        return addressFromConfig.Replace("(", "<").Replace(")", ">");
    }

    /// <summary>
    /// Get Approved Provider Mail To Address
    /// </summary>
    /// <returns>Approved Provider Mail To Address</returns>
    public static string GetApprovedProviderMailTo()
    {
        string addressFromConfig = GetApplicationConstants(LACESConstant.APPROVED_PROVIDER_MAIL_TO);
        return addressFromConfig.Replace("(", "<").Replace(")", ">");
    }

    /// <summary>
    /// Get Course Notification Mail To Address
    /// </summary>
    /// <returns>Course Notification Mail To</returns>
    public static string GetCourseNotificationMailTo()
    {
        string addressFromConfig = GetApplicationConstants(LACESConstant.COURSE_NOTIFICATION_MAIL_TO);
        return addressFromConfig.Replace("(", "<").Replace(")", ">");
    }


    #region ReadXLFile

    /// <summary>
    /// Read data from Excel file.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static DataTable ReadXLFile(string fileName, string workSheetName)
    {
        DataTable dt = new DataTable();
        try
        {
            //String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;\"";
            int intLastDot = fileName.LastIndexOf(".");
            string strExtension = fileName.Substring(intLastDot);
            String sConnectionString="";
            if (strExtension.ToLower() == ".xls")
            {
                sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;IMEX=1;\"";
            }
            else if (strExtension.ToLower() == ".xlsx")
            {
                sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;IMEX=1;\"";
            }


            OleDbConnection objConn = new OleDbConnection(sConnectionString);

            // Open connection with the database.
            objConn.Open();


            OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [" + workSheetName + "$]", objConn);

            // Create new OleDbDataAdapter that is used to build a DataSet
            // based on the preceding SQL SELECT statement.
            OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

            // Pass the Select command to the adapter.
            objAdapter1.SelectCommand = objCmdSelect;

            // Create new DataSet to hold information from the worksheet.
            DataSet objDataset1 = new DataSet();

            // Fill the DataSet with the information from the worksheet.
            objAdapter1.Fill(objDataset1, "XLData");


            objConn.Close();

            return objDataset1.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    #endregion

    /// <summary>
    /// Remove the created file in server site
    /// </summary>
    /// <param name="fileLocation"></param>
    public static void RemoveCreatedFileFromDisk(string fileLocation)
    {
        try
        {
            //finally remove the excel file
            if (File.Exists(fileLocation)) File.Delete(fileLocation);
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    /// <summary>
    /// Returns a list of countries as an ArrayList
    /// </summary>
    /// <returns></returns>
    public static ArrayList GetCountryList()
    {
        ArrayList countryList = new ArrayList();
        countryList.Add("United States");
        countryList.Add("Canada");
        countryList.Add("United Kingdom");
        countryList.Add("Australia");
        countryList.Add("Afghanistan");
        countryList.Add("Åland Islands");
        countryList.Add("Albania");
        countryList.Add("Algeria");
        countryList.Add("American Samoa");
        countryList.Add("Andorra");
        countryList.Add("Angola");
        countryList.Add("Anguilla");
        countryList.Add("Antarctica");
        countryList.Add("Antigua and Barbuda");
        countryList.Add("Argentina");
        countryList.Add("Armenia");
        countryList.Add("Aruba");
        countryList.Add("Australia");
        countryList.Add("Austria");
        countryList.Add("Azerbaijan");
        countryList.Add("Bahamas");
        countryList.Add("Bahrain");
        countryList.Add("Bangladesh");
        countryList.Add("Barbados");
        countryList.Add("Belarus");
        countryList.Add("Belgium");
        countryList.Add("Belize");
        countryList.Add("Benin");
        countryList.Add("Bermuda");
        countryList.Add("Bhutan");
        countryList.Add("Bolivia");
        countryList.Add("Bosnia and Herzegovina");
        countryList.Add("Botswana");
        countryList.Add("Bouvet Island");
        countryList.Add("Brazil");
        countryList.Add("British Indian Ocean Territory");
        countryList.Add("Brunei Darussalam");
        countryList.Add("Bulgaria");
        countryList.Add("Burkina Faso");
        countryList.Add("Burundi");
        countryList.Add("Cambodia");
        countryList.Add("Cameroon");
        countryList.Add("Canada");
        countryList.Add("Cape Verde");
        countryList.Add("Cayman Islands");
        countryList.Add("Central African Republic");
        countryList.Add("Chad");
        countryList.Add("Chile");
        countryList.Add("China");
        countryList.Add("Christmas Island");
        countryList.Add("Cocos (Keeling) Islands");
        countryList.Add("Colombia");
        countryList.Add("Comoros");
        countryList.Add("Congo");
        countryList.Add("Congo, The Democratic Republic of The");
        countryList.Add("Cook Islands");
        countryList.Add("Costa Rica");
        countryList.Add("Cote d'Ivoire");
        countryList.Add("Croatia");
        countryList.Add("Cuba");
        countryList.Add("Cyprus");
        countryList.Add("Czech Republic");
        countryList.Add("Denmark");
        countryList.Add("Djibouti");
        countryList.Add("Dominica");
        countryList.Add("Dominican Republic");
        countryList.Add("Ecuador");
        countryList.Add("Egypt");
        countryList.Add("El Salvador");
        countryList.Add("Equatorial Guinea");
        countryList.Add("Eritrea");
        countryList.Add("Estonia");
        countryList.Add("Ethiopia");
        countryList.Add("Falkland Islands (Malvinas)");
        countryList.Add("Faroe Islands");
        countryList.Add("Fiji");
        countryList.Add("Finland");
        countryList.Add("France");
        countryList.Add("French Guiana");
        countryList.Add("French Polynesia");
        countryList.Add("French Southern Territories");
        countryList.Add("Gabon");
        countryList.Add("Gambia");
        countryList.Add("Georgia");
        countryList.Add("Germany");
        countryList.Add("Ghana");
        countryList.Add("Gibraltar");
        countryList.Add("Greece");
        countryList.Add("Greenland");
        countryList.Add("Grenada");
        countryList.Add("Guadeloupe");
        countryList.Add("Guam");
        countryList.Add("Guatemala");
        countryList.Add("Guernsey");
        countryList.Add("Guinea");
        countryList.Add("Guinea-Bissau");
        countryList.Add("Guyana");
        countryList.Add("Haiti");
        countryList.Add("Heard Island and McDonald Islands");
        countryList.Add("Holy See (Vatican City State)");
        countryList.Add("Honduras");
        countryList.Add("Hong Kong");
        countryList.Add("Hungary");
        countryList.Add("Iceland");
        countryList.Add("India");
        countryList.Add("Indonesia");
        countryList.Add("Iran, Islamic Republic of");
        countryList.Add("Iraq");
        countryList.Add("Ireland");
        countryList.Add("Isle of Man");
        countryList.Add("Israel");
        countryList.Add("Italy");
        countryList.Add("Jamaica");
        countryList.Add("Japan");
        countryList.Add("Jersey");
        countryList.Add("Jordan");
        countryList.Add("Kazakhstan");
        countryList.Add("Kenya");
        countryList.Add("Kiribati");
        countryList.Add("Korea, Democratic People's Republic of");
        countryList.Add("Korea, Republic of");
        countryList.Add("Kuwait");
        countryList.Add("Kyrgyzstan");
        countryList.Add("Lao People's Democratic Republic");
        countryList.Add("Latvia");
        countryList.Add("Lebanon");
        countryList.Add("Lesotho");
        countryList.Add("Liberia");
        countryList.Add("Libyan Arab Jamahiriya");
        countryList.Add("Liechtenstein");
        countryList.Add("Lithuania");
        countryList.Add("Luxembourg");
        countryList.Add("Macao");
        countryList.Add("Macedonia, The Former Yugoslav Republic of");
        countryList.Add("Madagascar");
        countryList.Add("Malawi");
        countryList.Add("Malaysia");
        countryList.Add("Maldives");
        countryList.Add("Mali");
        countryList.Add("Malta");
        countryList.Add("Marshall Islands");
        countryList.Add("Martinique");
        countryList.Add("Mauritania");
        countryList.Add("Mauritius");
        countryList.Add("Mayotte");
        countryList.Add("Mexico");
        countryList.Add("Micronesia, Federated States of");
        countryList.Add("Moldova, Republic of");
        countryList.Add("Monaco");
        countryList.Add("Mongolia");
        countryList.Add("Montenegro");
        countryList.Add("Montserrat");
        countryList.Add("Morocco");
        countryList.Add("Mozambique");
        countryList.Add("Myanmar");
        countryList.Add("Namibia");
        countryList.Add("Nauru");
        countryList.Add("Nepal");
        countryList.Add("Netherlands");
        countryList.Add("Netherlands Antilles");
        countryList.Add("New Caledonia");
        countryList.Add("New Zealand");
        countryList.Add("Nicaragua");
        countryList.Add("Niger");
        countryList.Add("Nigeria");
        countryList.Add("Niue");
        countryList.Add("Norfolk Island");
        countryList.Add("Northern Mariana Islands");
        countryList.Add("Norway");
        countryList.Add("Oman");
        countryList.Add("Pakistan");
        countryList.Add("Palau");
        countryList.Add("Palestinian Territory, Occupied");
        countryList.Add("Panama");
        countryList.Add("Papua New Guinea");
        countryList.Add("Paraguay");
        countryList.Add("Peru");
        countryList.Add("Philippines");
        countryList.Add("Pitcairn");
        countryList.Add("Poland");
        countryList.Add("Portugal");
        countryList.Add("Puerto Rico");
        countryList.Add("Qatar");
        countryList.Add("Reunion");
        countryList.Add("Romania");
        countryList.Add("Russian Federation");
        countryList.Add("Rwanda");
        countryList.Add("Saint Helena");
        countryList.Add("Saint Kitts and Nevis");
        countryList.Add("Saint Lucia");
        countryList.Add("Saint Pierre and Miquelon");
        countryList.Add("Saint Vincent and The Grenadines");
        countryList.Add("Samoa");
        countryList.Add("San Marino");
        countryList.Add("Sao Tome and Principe");
        countryList.Add("Saudi Arabia");
        countryList.Add("Senegal");
        countryList.Add("Serbia");
        countryList.Add("Seychelles");
        countryList.Add("Sierra Leone");
        countryList.Add("Singapore");
        countryList.Add("Slovakia");
        countryList.Add("Slovenia");
        countryList.Add("Solomon Islands");
        countryList.Add("Somalia");
        countryList.Add("South Africa");
        countryList.Add("South Georgia and The South Sandwich Islands");
        countryList.Add("Spain");
        countryList.Add("Sri Lanka");
        countryList.Add("Sudan");
        countryList.Add("Suriname");
        countryList.Add("Svalbard and Jan Mayen");
        countryList.Add("Swaziland");
        countryList.Add("Sweden");
        countryList.Add("Switzerland");
        countryList.Add("Syrian Arab Republic");
        countryList.Add("Taiwan, Province of China");
        countryList.Add("Tajikistan");
        countryList.Add("Tanzania, United Republic of");
        countryList.Add("Thailand");
        countryList.Add("Timor-Leste");
        countryList.Add("Togo");
        countryList.Add("Tokelau");
        countryList.Add("Tonga");
        countryList.Add("Trinidad and Tobago");
        countryList.Add("Tunisia");
        countryList.Add("Turkey");
        countryList.Add("Turkmenistan");
        countryList.Add("Turks and Caicos Islands");
        countryList.Add("Tuvalu");
        countryList.Add("Uganda");
        countryList.Add("Ukraine");
        countryList.Add("United Arab Emirates");
        countryList.Add("United Kingdom");
        countryList.Add("United States");
        countryList.Add("United States Minor Outlying Islands");
        countryList.Add("Uruguay");
        countryList.Add("Uzbekistan");
        countryList.Add("Vanuatu");
        countryList.Add("Venezuela");
        countryList.Add("Viet Nam");
        countryList.Add("Virgin Islands, British");
        countryList.Add("Virgin Islands, U.S.");
        countryList.Add("Wallis and Futuna");
        countryList.Add("Western Sahara");
        countryList.Add("Yemen");
        countryList.Add("Zambia");
        countryList.Add("Zimbabwe"); 

        return countryList;
    }

    #region Send mail to admin and provier after submitting provider application
    /// <summary>
    /// Sends mail to admin and provier after submitting provider application 
    /// </summary>
    /// <param name="receiverName"></param>
    /// <param name="receiverEmail"></param>
    public static void SendMailsAfterProviderApplicationSubmitted(string receiverName, string receiverEmail, string strOrganization)
    {
        SmtpClient smtpClient = new SmtpClient();
        MailMessage message = new MailMessage();

        try
        {
            //Get the SMTP server Address from Web.conf
            smtpClient.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER);
            //Get the SMTP port from Web.conf
            smtpClient.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));

            //SEND MAIL TO ADMIN
            
            //Get the email's FROM address from Web.conf
            message.From = new MailAddress(LACESUtilities.GetAdminFromEmail());

            //Get the email's TO from Web.conf
            message.To.Add(LACESUtilities.GetApprovedProviderMailTo());
            //message.To.Add(new MailAddress("kstengle@1over0.com", "kstengle@1over0.com"));

            //Get the email's Subject
            message.Subject = "A new provider application submitted";

            //Whether the message body would be displayed as html or not
            message.IsBodyHtml = true;
            
            //Get the email's BODY
            message.Body = "Dear Admin,<br /><br />" + HttpUtility.HtmlEncode(receiverName) + " from " + strOrganization + " has submitted a new provider application.<br />Please review and send feedback.<br /><br />Thank you.";
            
            //Send the mail to admin
            if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
            {
                smtpClient.Send(message);
            }

            //SEND MAIL TO PROVIDER

            //Get the email's TO address
            message = new MailMessage();

            //Get the email's FROM address from Web.conf
            message.From = new MailAddress(LACESUtilities.GetAdminFromEmail());

            message.To.Add(new MailAddress(receiverEmail, receiverName));
            message.CC.Add(new MailAddress("lacesapp@asla.org", "lacesapp@asla.org"));
            //message.CC.Add(new MailAddress("kstengle@1over0.com", "kstengle@1over0.com"));
            //Get the email's Subject
            message.Subject = "Your LA CES Transaction Has Been Processed ";

            //Whether the message body would be displayed as html or not
            message.IsBodyHtml = true;

            //Get the email's BODY
            message.Body = "Dear " + HttpUtility.HtmlEncode(receiverName) + ",<br /><br />Your LA CES transaction of $295.00 was charged to your credit card on " + System.DateTime.Today.ToShortDateString() + ". Please print this e-mail and keep as a receipt for your records.<br/><br/>Thank you,<br/><br/>LA CES<br/>laces@asla.org";

            //Send the mail to provider
            if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
            {
                smtpClient.Send(message);
            }
        }
        catch (Exception exp)
        {
            
        }
    } 
    #endregion

    #region Send mail to admin and provier after submitting provider renewal application
    /// <summary>
    /// Sends mail to admin and provier after submitting provider application 
    /// </summary>
    /// <param name="receiverName"></param>
    /// <param name="receiverEmail"></param>
    public static void SendMailsAfterProviderApplicationRenewed(string receiverName, string receiverEmail, string receiverOrg)
    {
        SmtpClient smtpClient = new SmtpClient();
        MailMessage message = new MailMessage();

        try
        {
            //Get the SMTP server Address from Web.conf
            smtpClient.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER);
            //Get the SMTP port from Web.conf
            smtpClient.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));

            //SEND MAIL TO ADMIN

            //Get the email's FROM address from Web.conf
            message.From = new MailAddress(LACESUtilities.GetAdminFromEmail());

            //Get the email's TO from Web.conf
            message.To.Add(LACESUtilities.GetApprovedProviderMailTo());

            //message.CC.Add(new MailAddress("kstengle@1over0.com", "kstengle@1over0.com"));
            //Get the email's Subject
            message.Subject = "Provider has been renewed";

            //Whether the message body would be displayed as html or not
            message.IsBodyHtml = true;

            //Get the email's BODY
            message.Body = "Dear Admin,<br /><br />" + HttpUtility.HtmlEncode(receiverName) + " (" + receiverEmail + ") from " + receiverOrg + " has renewed provider application.<br />Please review and send feedback.<br /><br />Thank you.";

            //Send the mail to admin
            if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
            {
                smtpClient.Send(message);
            }

            //SEND MAIL TO PROVIDER

            //Get the email's TO address
            message = new MailMessage();

            //Get the email's FROM address from Web.conf
            message.From = new MailAddress(LACESUtilities.GetAdminFromEmail());

            message.To.Add(new MailAddress(receiverEmail, receiverName));

            message.CC.Add(new MailAddress("lacesapp@asla.org", "lacesapp@asla.org"));
            //message.CC.Add(new MailAddress("kstengle@1over0.com", "kstengle@1over0.com"));

            //Get the email's Subject
            message.Subject = "Your LA CES Transaction Has Been Processed" ;

            //Whether the message body would be displayed as html or not
            message.IsBodyHtml = true;

            //Get the email's BODY
            message.Body = "Dear " + HttpUtility.HtmlEncode(receiverName) + ",<br /><br />Your LA CES transaction of $295.00 was charged to your credit card on " + System.DateTime.Today.ToShortDateString() + ". Please print this e-mail and keep as a receipt for your records.<br/><br/>Thank you,<br/><br/>LA CES<br/>laces@asla.org";


            //Send the mail to provider
            if (ConfigurationManager.AppSettings["SendOutgoingEmail"].ToString() == "Y")
            {
                smtpClient.Send(message);
            }
        }
        catch (Exception exp)
        {

        }
    }
    #endregion

    #region Process Credit Card
    /// <summary>
    /// Process credit card
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    public static string ProcessCreditCard(PaymentInformation payment)
    {
        string response = "";
        string mRequestId = "";
        string transactionReqt = "";

        transactionReqt += "NAME=" + payment.NameOnCard + "&";
        transactionReqt += "ACCT=" + payment.CreditCardNumber + "&";
        transactionReqt += "EXPDATE=" + payment.CardExpirationMonth + payment.CardExpirationYear + "&";
        transactionReqt += "AMT=" + payment.TransactionAmount + "&";
        transactionReqt += "STREET=" + payment.Street + "&";
        transactionReqt += "CITY=" + payment.City + "&";
        transactionReqt += "STATE=" + payment.State + "&";
        transactionReqt += "ZIP=" + payment.Zip + "&";
        transactionReqt += "CVV2=" + payment.CreditCardVerificationCode + "&";

        transactionReqt += "TRXTYPE=S&TENDER=C&";
        transactionReqt += "USER=" + PayflowUtility.AppSettings(LACESConstant.PaymentInfo.USER) + "&";
        transactionReqt += "VENDOR=" + PayflowUtility.AppSettings(LACESConstant.PaymentInfo.VENDOR) + "&";
        transactionReqt += "PARTNER=" + PayflowUtility.AppSettings(LACESConstant.PaymentInfo.PARTNER) + "&";
        transactionReqt += "PWD=" + PayflowUtility.AppSettings(LACESConstant.PaymentInfo.PWD);


        mRequestId = PayflowUtility.RequestId;

        PayflowNETAPI PayflowNETAPI = new PayflowNETAPI();
        //TransactionReqt = "TRXTYPE=S&ACCT=5105105105105101&EXPDATE=0109&TENDER=C&INVNUM=INV12345&AMT=25.12&PONUM=PO12345&STREET=123 Main St.&ZIP=12345&CVV2=123&USER=zmiller636&VENDOR=zmiller636&PARTNER=paypal&PWD=luvspurple1";

        response = PayflowNETAPI.SubmitTransaction(transactionReqt, mRequestId);

        return response;
    } 
    #endregion


}
