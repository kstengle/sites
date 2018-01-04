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

public partial class CanvasPieChart : System.Web.UI.Page
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
            string sqlStatement = "ProtoType_ActiveProviders";

            SqlCommand cmd = new SqlCommand(sqlStatement, connection);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string Active = ds.Tables[0].Rows[0]["Active"].ToString();
                string inactive = ds.Tables[0].Rows[0]["Inactive"].ToString();
                jsondata = "{y: " + Active + ", indexLabel: \"Active\" },";
                jsondata += "{y: " + inactive + ", indexLabel: \"InActive\" }";
            }
        }
        catch (Exception ex)
        { }
    }
        
}