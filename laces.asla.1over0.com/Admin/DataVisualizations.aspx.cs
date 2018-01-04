using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DataVisualizations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void uiLnkGenerateReport_Click(object sender, EventArgs e)
    {
        string reporttype = uiDDLReportType.SelectedValue;
        string startdate = txtStartDate.Text.Length >0 ? txtStartDate.Text : "1/1/1900";
        string enddate = txtEndDate.Text    .Length>0 ? txtEndDate.Text : "1/1/2050";

        Utility.ASLA_Laces_ProdEntities item = new Utility.ASLA_Laces_ProdEntities();
        switch (reporttype)
        {
            case "coursesbysubject":

                // create all parameters you need
                var cmdText = "[LACES_VisualizationSubjectByDate] @StartDate = @startdate_param, @EndDate = @enddate_param";
                var paramsa = new[]{
                   new SqlParameter("startdate_param", "1/1/1920"),
                   new SqlParameter("enddate_param", "1/1/2020")
                };

                List<Utility.LACES_VisualizationSubjectByDate_Result> queryresults = item.Database.SqlQuery<Utility.LACES_VisualizationSubjectByDate_Result>(cmdText, paramsa).ToList();
                int i = 0;
                foreach (Utility.LACES_VisualizationSubjectByDate_Result result in queryresults)
                {
                    string subject = result.declaration;
                    string count = result.subjectcount.Value.ToString();
                    i++;
                }
                DataTable tab = ConvertToDataTable(queryresults);
                ConvertToCSV(tab);
                break;
            case "coursesbylocation":

                // create all parameters you need
                var cmdText1 = "[LACES_VisualizationSubjectByLocation] @StartDate = @startdate_param, @EndDate = @enddate_param";
                var paramsa1 = new[]{
                   new SqlParameter("startdate_param", "1/1/1920"),
                   new SqlParameter("enddate_param", "1/1/2020")
                };

                List<Utility.LACES_VisualizationSubjectByLocation_Result> queryresults1 = item.Database.SqlQuery<Utility.LACES_VisualizationSubjectByLocation_Result>(cmdText1, paramsa1).ToList();
                int j = 0;
                foreach (Utility.LACES_VisualizationSubjectByLocation_Result result in queryresults1)
                {
                    string subject = result.StateProvince;
                    string count = result.locationcount.Value.ToString();
                    j++;
                }
                DataTable tab1 = ConvertToDataTable(queryresults1);
                ConvertToCSV(tab1);
                break;
            case "providersbytype":
                var cmdText2 = "[LACES_VisualizationNewProviders] @StartDate = @startdate_param, @EndDate = @enddate_param";
                var paramsa2 = new[]{
                   new SqlParameter("startdate_param", "1/1/1920"),
                   new SqlParameter("enddate_param", "1/1/2020")
                };

                List<Utility.LACES_VisualizationNewProviders_Result> queryresults2 = item.Database.SqlQuery<Utility.LACES_VisualizationNewProviders_Result>(cmdText2, paramsa2).ToList();
                int k = 0;
                foreach (Utility.LACES_VisualizationNewProviders_Result result in queryresults2)
                {
                    string subject = result.ProviderType;
                    string count = result.providercount.Value.ToString();
                    k++;
                }
                DataTable tab2 = ConvertToDataTable(queryresults2);
                ConvertToCSV(tab2);
                break;

        }


    }
    public DataTable ConvertToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties =
           TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        return table;

    }
    public void ConvertToCSV(DataTable table)
    {
        StringBuilder content = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            DataRow dr1 = (DataRow)table.Rows[0];
            int intColumnCount = dr1.Table.Columns.Count;
            int index = 1;

            //add column names
            foreach (DataColumn item in dr1.Table.Columns)
            {
                content.Append(String.Format("\"{0}\"", item.ColumnName));
                if (index < intColumnCount)
                    content.Append(",");
                else
                    content.Append("\r\n");
                index++;
            }

            //add column data
            foreach (DataRow currentRow in table.Rows)
            {
                string strRow = string.Empty;
                for (int y = 0; y <= intColumnCount - 1; y++)
                {
                    strRow += "\"" + currentRow[y].ToString() + "\"";

                    if (y < intColumnCount - 1 && y >= 0)
                        strRow += ",";
                }
                content.Append(strRow + "\r\n");

            }
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.AddHeader("content-disposition", "attachment; filename=test-" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".csv");
            Response.Write(content.ToString());
            Response.End();
        }
    }
}