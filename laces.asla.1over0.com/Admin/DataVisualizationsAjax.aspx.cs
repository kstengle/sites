using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;


public partial class Admin_DataVisualizationsAjax : System.Web.UI.Page
{
    public string jsondata = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        string reporttype = Request.QueryString["reporttype"] != null ? Request.QueryString["reporttype"] : "";
        string startdate = Request.QueryString["startdate"] != null ? Request.QueryString["startdate"] : "1/1/1900";
        string enddate = Request.QueryString["enddate"] != null ? Request.QueryString["enddate"] : "1/1/2050";

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
                    jsondata += "{x:" + i.ToString() + ",y: " + count + ", indexLabel: \"" + subject + "\" },";
                    i++;
                }
                jsondata = jsondata.TrimEnd(',');
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
                    jsondata += "{x:" + j.ToString() + ",y: " + count + ", indexLabel: \"" + subject + "\" },";
                    j++;
                }
                jsondata = jsondata.TrimEnd(',');
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
                    string subject =result.ProviderType;
                    string count = result.providercount.Value.ToString();
                    jsondata += "{x:" + k.ToString() + ",y: " + count + ", indexLabel: \"" + subject + "\" },";
                    k++;
                }
                jsondata = jsondata.TrimEnd(',');
                break;
        }

     
    }    

}