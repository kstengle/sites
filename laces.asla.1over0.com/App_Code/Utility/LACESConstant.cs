/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: LACESConstant.cs
 * Purpose/Function: Listing of all LACESConstants
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/08/2008      Create this Page
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

/// <summary>
/// List of Constants using in LACES project
/// </summary>
public class LACESConstant
{
    /// <summary>
    /// Constructor
    /// </summary>
    public LACESConstant()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// List of ProviderStatus constants
    /// </summary>
    public class ProviderStatus
    {
        public const string ACTIVE = "Active";
        public const string INACTIVE = "Inactive";
        public const string PENDING = "Pending";
    }

    /// <summary>
    /// List of PaymentGatewayInfo constants
    /// </summary>
    public class PaymentInfo
    {
        public const string USER = "USER";
        public const string VENDOR = "VENDOR";
        public const string PARTNER = "PARTNER";
        public const string PWD = "PWD";
        public const string PROVIDER_FEE = "PROVIDER_FEE";
        
    }

    /// <summary>
    /// List of URL constants
    /// </summary>
    public class URLS
    {
        //Provider Login Page
        public const string ADMIN_BASE = "Admin";
        public const string ADMIN_LOGIN = "~/Admin/Login.aspx";
        public const string PROVIDER_BASE = "Provider";
        public const string PROVIDER_LOGIN = "/Default.aspx";
        public const string PENDING_COURSE = "PendingCourses.aspx";
        public const string COURSE_SUCCESS = "CourseSavedSuccessfully.aspx";
        public const string HOME_PAGE = "~/Default.aspx";
        
    }

    /// <summary>
    /// List of Message constants
    /// </summary>
    public class Messages
    {
        // If requested course not available in admin section
        public const string COURSE_NOT_FOUND_IN_ADMIN = "Course does not exist.<br/><br/>";

        // If requested participant not available in admin section
        public const string PARTICIPANT_NOT_FOUND_IN_ADMIN = "Participant does not exist.<br/><br/>";

        // If requested provider not available in admin section
        public const string PROVIDER_NOT_FOUND_IN_ADMIN = "Provider does not exist.<br/><br/>";
    
        // If requested course not available in provider section
        public const string COURSE_NOT_FOUND_IN_PROVIDER = "Course does not exist or you do not have permission to access this course.<br/><br/>";

        // If requested participant not available in provider section
        public const string PARTICIPANT_NOT_FOUND_IN_PROVIDER = "Participant does not exist or you do not have permission to access this participant.<br/><br/>";

        // If requested course not available in visitor section
        public const string COURSE_NOT_FOUND_IN_VISITOR = "Course is not available.<br/><br/>";

        /// <summary>
        /// When Email ID is not valid
        /// </summary>
        public const string EMAIL_NOT_VALID="Please enter valid email address.";
        
        /// <summary>
        /// Forget Password Postback Message after sending email.
        /// </summary>
        public const string FORGETPASSWORD_POSTBACK_MESSAGE = @"An email containing the sign-in password has been sent to the email address '{0}'. 
                                        Please check your email in a few moments to retrieve your password details.";
        public const string PORGETPASSEORD_INVALID_MESSAGE = @"The email address you entered is not found on system. Please double-check
                                                            the email address you entered, or {0} for
                                                            further assistance.";
        
        /// <summary>
        /// When email/password do not match
        /// </summary>
        public const string INVALID_LOGIN = "Invalid login";
    }

    /// <summary>
    /// List of Session Keys/Variables
    /// </summary>
    public class SessionKeys
    {
        //Session variable to store visitor course search criteria selection
        public const string SEARCH_VISITOR_CRITERIA = "VisitorSearchCriteria";
        //Session variable to store course search criteria selection
        public const string SEARCH_COURSE_CRITERIA = "SearchCourseCriteria";
        //Session variable to store participant search criteria selection
        public const string SEARCH_PARTICIPANT_CRITERIA = "SearchParticipantCriteria";
        //Session variable to store loggedin provider object
        public const string LOGEDIN_PROVIDER = "LOGEDIN_PROVIDER";
        //Session variable to store loggedin admin information
        public const string LOGEDIN_ADMIN = "LOGEDIN_ADMIN";
        //Session variable to store provider search keywords
        public const string SEARCH_PROVIDERS_KEYWORDS = "SearchProvidersKeywords";
        //Session variable to store approved provider search keywords
        public const string SEARCH_APPROVED_PROVIDERS_KEYWORDS = "SearchApprovedProvidersKeywords";
        //Session variable to store message string in course search result page in admin section
        public const string COURSE_ACTIVE_DEACTIVE_MSG = "ActivateDeactivateMessage";
        //Session variable to store loggedin approvedprovider object
        public const string LOGEDIN_APPROVED_PROVIDER = "LOGEDIN_APPROVED_PROVIDER";

        //Session variable to store loggedin inactive approvedprovider object (for accessing renew page only)
        public const string LOGEDIN_INACTIVE_PROVIDER = "LOGEDIN_INACTIVE_PROVIDER";

        //Session variable to store course record object
        public const string COURSE_RECORD_OBJ = "CourseRecord";
    }

    /// <summary>
    /// List of QueryString constants
    /// </summary>
    public class QueryString
    {
        //Query String using for Course ID
        public const string COURSE_ID = "CourseID";
        //Query String using for Provider ID
        public const string PROVIDER_ID = "ProviderID";
        //Query String using for participant ID
        public const string PARTICIPANT_ID = "ParticipantID";

        //Query String using for current sort column
        public const string SORT_COLUMN = "SortColumn";
        //Query String using for current sort order
        public const string SORT_ORDER = "SortOrder";
        //Query String using for current page index
        public const string PAGE_INDEX = "PageIndex";
        //Query String using for current course status
        public const string COURSE_STATUS = "Status";
        
        //Query String using for approved provider status
        public const string PROVIDER_STATUS = "Status";
    }

    /// <summary>
    /// Page Size in search result page
    /// </summary>
    public const int SEARCH_RESULT_PAGE_SIZE = 10;
    /// <summary>
    /// Admin Email ID for Sending Forget Password
    /// </summary>
    public const string ADMIN_EMAIL_ID="LACESAdminEmail";

    /// <summary>
    /// Administrators contact addresses
    /// </summary>
    public const string ADMIN_CONTACT_FROM_EMAIL = "LACESAdminEmailFrom";
    public const string ADMIN_CONTACT_TO_EMAIL = "LACESAdminEmailTo";
    public const string ADMIN_CONTACT_CC_EMAIL = "LACESAdminEmailCC";
    /// <summary>
    /// New Course From Address
    /// </summary>
    public const string NEW_COURSE_FROM_ADDRESS ="LACESNewCourseNotificationEmailAddress";
    
   /// <summary>
   /// Admin Name for sending Forget Password
   /// </summary>
    public const string ADMIN_NAME="LACESAdminName";
    /// <summary>
    /// Forget Password Email Subject
    /// </summary>
    public const string FORGETPASSWORD_SUBJECT = "Provider Forgotten password";

    public const string SMTPSERVER = "SMTPServer";
    public const string SMTPPORT = "SMTPPort";

    public const string CONSTANT_ERROR_REPORT_MAIL_TO = "ErrorReportMailTo"; //error report mail To addresses
    public const string CONSTANT_ERROR_MAIL_SUBJECT = "ErrorMailSubject";//error report mail subject
    public const string CONSTANT_ERROR_MAIL_FROM = "ErrorMailFrom";//error report mail From address

    public const string LACES_TEXT = "LA CES";

    public const string APPROVED_PROVIDER_MAIL_TO = "LACESApprovedProviderMailTo";
    public const string COURSE_NOTIFICATION_MAIL_TO = "LACESCourseNotificationMailTo";

}
