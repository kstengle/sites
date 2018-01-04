using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class CanvasProviders : System.Web.UI.Page
{

    public string jsondata = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LACESConnectionString"].ToString());
        try
        {
            connection.Open();
            DataSet ds = new DataSet();
            string sqlStatement = "Prototype_CoursesProvider";

            SqlCommand cmd = new SqlCommand(sqlStatement, connection);

            cmd.CommandType = CommandType.StoredProcedure;
            if (Request.QueryString["Year"] == null)
            {
                cmd.Parameters.AddWithValue("@Year", "0");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Year", Request.QueryString["Year"]);
            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // DateTime date = new DateTime();
                    string OrganizationName = ds.Tables[0].Rows[i]["OrganizationName"].ToString();
                    string courses = ds.Tables[0].Rows[i]["courses"].ToString();
                    jsondata += "{ x:" + i.ToString() + ", y: " + courses + ", label:\"" + OrganizationName + "\"},";
                }
                jsondata = jsondata.TrimEnd(',');
            }
        }
        catch (Exception ex)
        { }
    }
}