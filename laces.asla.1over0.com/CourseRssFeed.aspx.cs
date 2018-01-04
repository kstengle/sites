using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
public partial class CourseRssFeed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BuildXML y = new BuildXML();
            SqlDataReader dr = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LACESConnectionString"].ToString());
            SqlCommand command = new SqlCommand("SP_AlLCoursesRSSFeed", conn);
            conn.Open();
            command.CommandType = CommandType.StoredProcedure;
            dr = command.ExecuteReader();



            string Title = "";
            string City = "";
            string StateProvince="";
            string Dates= "";
            string Description="";
            string LongDescription = "";
            string link;
            System.Xml.XmlDocument PeopleXML = new System.Xml.XmlDocument();
            y.CreateDeclaration();
            y.CreateCSSInstructions("/css/RSSFeed.css");            
            XmlElement RootElement = y.CreateRootElement("rss");
            y.AddAttribute(RootElement, "version", "2.0");
            
            XmlElement ChannelElement = y.CreateEmptyNode("channel", RootElement);
            y.CreateEndNode("title", "New Courses by Approved Providers | LA CES™", ChannelElement);
            y.CreateEndNode("description", "A Service of the Landscape Architecture Continuing Education System (LA CES)", ChannelElement);
            y.CreateEndNode("link", "http://www.asla/org", ChannelElement);
            y.CreateEndNode("language", "en-us", ChannelElement);
            y.CreateEndNode("lastBuildDate", System.DateTime.Now.ToString(), ChannelElement);

            while (dr.Read())
            {

                Title = Regex.Replace(dr["Title"].ToString(), "[\u0000-\u001f]", "");
                City = dr["City"].ToString();
                StateProvince = dr["StateProvince"].ToString();
                Dates = dr["StartDate"].ToString() + " - " + dr["EndDate"].ToString();
                Description = dr["Description"].ToString();
                LongDescription = "<b><span class='dvHeader'>Location:</span></b>  " + City + ", " + StateProvince + "<br />";
                LongDescription += "<b>Dates:</b>  " + Dates + "<br />";
                LongDescription += "<b>Course Description:  </b>" + Description;
                link = ConfigurationManager.AppSettings["LacesBaseURL"].ToString() + "/visitor/CourseDetails.aspx?CourseID=" + dr["ID"].ToString();

                XmlElement itemElement = y.CreateEmptyNode("item", ChannelElement);

                //XmlElement EmptyElement = y.CreateEmptyNode("course", RootElement);
                y.CreateCDataNode("title", Title, itemElement);
                y.CreateEndNode("link", link, itemElement);
                y.CreateEndNode("pubdate", "", itemElement);
             
                y.CreateCDataNode("description", LongDescription, itemElement);	
            }
            
            StringBuilder strGoogleAnalytics = new StringBuilder();
            strGoogleAnalytics.Append("<script type=\"text/javascript\">");
            strGoogleAnalytics.Append("(function (i, s, o, g, r, a, m) {");
            strGoogleAnalytics.Append("i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {");
            strGoogleAnalytics.Append("    (i[r].q = i[r].q || []).push(arguments)");
            strGoogleAnalytics.Append("}, i[r].l = 1 * new Date(); a = s.createElement(o),");
            strGoogleAnalytics.Append("m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)");
        strGoogleAnalytics.Append("})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');");

        strGoogleAnalytics.Append("ga('create', 'UA-4677685-1', 'auto');");
        strGoogleAnalytics.Append("ga('send', 'pageview');");

        strGoogleAnalytics.Append("</script>");
            //strGoogleAnalytics.Append("<script type=\"text/javascript\">");
            //strGoogleAnalytics.Append("var gaJsHost = ((\"https:\" == document.location.protocol) ? \"https://ssl.\" : \"http://www.\");");
            //strGoogleAnalytics.Append("document.write(unescape(\"%3Cscript src='\" + gaJsHost + \"google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E\"));");
            //strGoogleAnalytics.Append("</script>");
            //strGoogleAnalytics.Append("<script type=\"text/javascript\">");
            //strGoogleAnalytics.Append("var pageTracker = _gat._getTracker(\"UA-4677685-1\");");
            //strGoogleAnalytics.Append("pageTracker._trackPageview();");
            //strGoogleAnalytics.Append("</script>");
            y.CreateCDataNode("Javascript", strGoogleAnalytics.ToString(), RootElement);
            Response.Write(y.xml.InnerXml.ToString());
            conn.Close();
        }
    }
    protected string FormatForXML(object input)

{

   string data = input.ToString(); //cast input to string

 

   // replace those characters disallowed in XML documents

   data = data.Replace("&", "&amp;");   
   data = data.Replace("'", "&apos;");
   data = data.Replace("<", "&lt;");
   data = data.Replace(">", "&gt;");
   data = data.Replace("\"", "&quot;");   
   return data;

}
}
