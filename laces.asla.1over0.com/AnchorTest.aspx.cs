using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AnchorTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        //string url= "https://my.matterport.com/show/?m=rVZukggqjdn";
        string url = "https://my.matterport.com/show/?mls=1&m=rVZukggqjdn";
        Uri address = new Uri(url);

        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

        using (WebClient webClient = new WebClient())
        {
            var stream = webClient.OpenRead(address);
            using (StreamReader sr = new StreamReader(stream))
            {
                var page = sr.ReadToEnd();
                uiPhTest.Controls.Add(new LiteralControl(page));

            }
            
        }
    }
}