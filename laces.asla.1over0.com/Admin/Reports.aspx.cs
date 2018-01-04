using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
public partial class Admin_Reports : AdminBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GenerateClarbReportsByUploadDate(Object sender, EventArgs e)
    {
        try
        {
            uiLitMessage.Text = "";
            SqlDataReader dr = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LACESConnectionString"].ToString());

            SqlCommand command = new SqlCommand("LACES_ReportCLARBByUploadDate", conn);

            if (uiTxtClarbReportByUploadStartDate.Text.Length == 0)
            {
                command.Parameters.AddWithValue("@StartDate", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@StartDate", uiTxtClarbReportByUploadStartDate.Text);
            }
            if (uiTxtClarbReportByUploadEndDate.Text.Length == 0)
            {
                command.Parameters.AddWithValue("@EndDate", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@EndDate", uiTxtClarbReportByUploadEndDate.Text);
            }
         //   if (txtCourseID.Text.Length == 0)
         //   {
                command.Parameters.AddWithValue("@CourseID", DBNull.Value);
          //  }
          //  else
          //  {
          //      command.Parameters.AddWithValue("@CourseID", int.Parse(txtCourseID.Text));
          //  }
                if (uiRblClarbReportHasNumber.SelectedValue.Length == 0)
            {
                command.Parameters.AddWithValue("@MinClarbLength", "0");
            }
            else
            {
                command.Parameters.AddWithValue("@MinClarbLength", int.Parse(uiRblClarbReportHasNumber.SelectedValue));
            }
            conn.Open();
            command.CommandType = CommandType.StoredProcedure;
            dr = command.ExecuteReader();
            if (dr.HasRows)
            {
                uiDGClarbResults.DataSource = dr;
                uiDGClarbResults.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "inline;filename=ASLACLARB.xls");
                Response.Charset = "";
                EnableViewState = false;
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                uiDGClarbResults.RenderControl(oHtmlTextWriter);
                Response.Write(oStringWriter.ToString());
                Response.Flush();
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                uiLitMessage.Text = "No records found";

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
        }
        
    }
    protected void GenerateAdminReports(Object sender, EventArgs e)
    {
        uiLitMessage.Text = "";
        SqlDataReader dr = null;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LACESConnectionString"].ToString());
        SqlCommand command = new SqlCommand("LACES_ReportProviders", conn);
        conn.Open();
        command.CommandType = CommandType.StoredProcedure;
        dr = command.ExecuteReader();
        if (dr.HasRows)
        {
            uiDGClarbResults.DataSource = dr;
            uiDGClarbResults.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "inline;filename=ASLAProviders.xls");
            Response.Charset = "";
            EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            uiDGClarbResults.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.Flush();
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else
        {
            uiLitMessage.Text = "No records found";

        }
    }
    protected void GenerateClarbReports(Object sender, EventArgs e)
    {
        try
        {
            uiLitMessage.Text = "";
            SqlDataReader dr = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LACESConnectionString"].ToString());
            
            SqlCommand command = new SqlCommand("LACES_ReportCLARB", conn);

            if (txtStartDate.Text.Length == 0)
            {
                command.Parameters.AddWithValue("@StartDate", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@StartDate", txtStartDate.Text);
            }
            if (txtEndDate.Text.Length == 0)
            {
                command.Parameters.AddWithValue("@EndDate", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@EndDate", txtEndDate.Text);
            }
            if (txtCourseID.Text.Length == 0)
            {
                command.Parameters.AddWithValue("@CourseID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@CourseID", int.Parse(txtCourseID.Text));
            }
            if (uiRblClarbNumber.SelectedValue.Length == 0)
            {
                command.Parameters.AddWithValue("@MinClarbLength", "0");
            }
            else
            {
                command.Parameters.AddWithValue("@MinClarbLength", int.Parse(uiRblClarbNumber.SelectedValue));
            }
            conn.Open();
            command.CommandType = CommandType.StoredProcedure;
            dr = command.ExecuteReader();
            if (dr.HasRows)
            {
                uiDGClarbResults.DataSource = dr;
                uiDGClarbResults.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "inline;filename=ASLACLARB.xls");
                Response.Charset = "";
                EnableViewState = false;
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                uiDGClarbResults.RenderControl(oHtmlTextWriter);
                Response.Write(oStringWriter.ToString());
                Response.Flush();
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                uiLitMessage.Text = "No records found";

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
        }
    }
    
}