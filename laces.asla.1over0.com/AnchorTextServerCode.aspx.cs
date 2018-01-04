using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AnchorTextServerCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string upc = Request.QueryString["upc"] != null ? Request.QueryString["upc"] : "";
        uiphreturnvalue.Controls.Add(new LiteralControl("add product " + upc + " to your cart"));
    }
}