using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TextExcelDriver : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fileName = "test.xslx";
        string fileLocation = AppDomain.CurrentDomain.BaseDirectory + "xls\\MultipleCourseUpload\\test.xlsx";
        string workSheetName = "Courses";
        DataTable dt = ReturnExcel(fileLocation, workSheetName);
        Response.Write("ROWS" + dt.Rows.Count.ToString());
        Response.End();
    }
    protected DataTable ReturnExcel(string fileName, string workSheetName)
    {
        DataTable dt = new DataTable();
        try
        {
            //String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;\"";
            int intLastDot = fileName.LastIndexOf(".");
            string strExtension = fileName.Substring(intLastDot);
            String sConnectionString = "";
            if (strExtension.ToLower() == ".xls")
            {
                sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;IMEX=1;\"";
            }
            else if (strExtension.ToLower() == ".xlsx")
            {
                sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;IMEX=1;\"";
            }


            OleDbConnection objConn = new OleDbConnection(sConnectionString);

            // Open connection with the database.
            objConn.Open();


            OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [" + workSheetName + "$]", objConn);

            // Create new OleDbDataAdapter that is used to build a DataSet
            // based on the preceding SQL SELECT statement.
            OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

            // Pass the Select command to the adapter.
            objAdapter1.SelectCommand = objCmdSelect;

            // Create new DataSet to hold information from the worksheet.
            DataSet objDataset1 = new DataSet();

            // Fill the DataSet with the information from the worksheet.
            objAdapter1.Fill(objDataset1, "XLData");


            objConn.Close();
            return objDataset1.Tables[0];
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
            return null;
        }
    }
}