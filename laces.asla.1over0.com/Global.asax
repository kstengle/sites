<%@ Application Language="C#" %>

<script runat="server">
//void Application_BeginRequest(Object Sender, EventArgs e)
 //   {
  //      string tempurl = Request.Url.OriginalString.ToString();
   //     if (tempurl.IndexOf("ProviderApplication.aspx") > 0)
    //    {
     //       if(!(Request.IsSecureConnection))
      //      {
                
       //         if (Request.QueryString["https"] == null)
        //        {
//                    tempurl = Request.Url.OriginalString.ToString().Replace("http://", "https://");
         //           tempurl = tempurl + ("&https=T");
          //          Response.Redirect(tempurl);
           //     }
        //    }
       // }
        //   string tempurl2 = Request.Url.LocalPath.ToString();
      //  string tempurl3 = Request.ServerVariables["url"].ToString();
        //        tempurl = tempurl.Replace(".com:80", ".com");}
    //}


    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
    /// <summary>
    /// Send mail to the addresses specified in the web.config file when an unhandled error occurs        
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void Application_Error(object sender, EventArgs e) 
    {
        // Code that runs when an unhandled error occurs        
        Exception Ex = Server.GetLastError();
        Application["Ex"] = Ex;
        
        LACESUtilities.ReportError(Ex);
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
