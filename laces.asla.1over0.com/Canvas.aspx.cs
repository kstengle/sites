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

public partial class Canvas : System.Web.UI.Page
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
            string sqlStatement = "Prototype_CoursesByYear";

            SqlCommand cmd = new SqlCommand(sqlStatement, connection);

            cmd.CommandType = CommandType.StoredProcedure;
            if(Request.QueryString["Provider"] ==null)
            {
                cmd.Parameters.AddWithValue("@ProviderID", "0");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ProviderID", Request.QueryString["Provider"]);
            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count> 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                   // DateTime date = new DateTime();
                    string year = ds.Tables[0].Rows[i]["reportyear"].ToString();
                    string courses = ds.Tables[0].Rows[i]["courses"].ToString();
                    jsondata += "{ x:" + i.ToString() + ", y: " + courses + ", label:\"" + year  + "\"},";
                }
                jsondata = jsondata.TrimEnd(',');
            }
        }
        catch (Exception ex)
        { }
    }
    }