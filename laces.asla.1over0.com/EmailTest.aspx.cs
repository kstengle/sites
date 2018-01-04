using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class EmailTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MailMessage message = new MailMessage();

        string ToEmailAddress = "kstengle@1over0tech.com";
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = LACESUtilities.GetApplicationConstants(LACESConstant.SMTPSERVER);
        //Get the SMTP post  25;
        smtpClient.Port = Convert.ToInt32(LACESUtilities.GetApplicationConstants(LACESConstant.SMTPPORT));
        
            message.From = new MailAddress("nagresta@1over0tech.com");
            message.To.Add(new MailAddress(ToEmailAddress));

            message.Subject = "Test Subject";
            message.IsBodyHtml = true;
            message.Body = "TEST";//Send Email to Friend Message Body            
           smtpClient.Send(message);//Sending A Mail to the Sender Friend            
           Response.Write("Email sent");
    }
}
