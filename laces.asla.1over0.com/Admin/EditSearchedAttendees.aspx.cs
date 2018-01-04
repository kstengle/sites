using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using Pantheon.ASLA.LACES.DataAccess;
using Pantheon.ASLA.LACES.Common;


public partial class Admin_EditSearchedAttendees : AdminBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {       
    }
    protected void PopulateAttendeeList(string strFirstName, string strLastName, string strASLAMemberNumber, string strCLARBNumber, string strFloridaStateNumber, string strMiddleInitial, string strCSLANumber, string strEmail)
    {
        StringBuilder strbSearchedFor = new StringBuilder();
        strbSearchedFor.Append(strFirstName.Length > 0 ? "First Name: " + strFirstName + "<br />" : "");
        strbSearchedFor.Append(strLastName.Length > 0 ? "Last Name: " + strLastName + "<br />" : "");
        strbSearchedFor.Append(strASLAMemberNumber.Length > 0 ? "ASLA Member Number: " + strASLAMemberNumber + "<br />" : "");
        strbSearchedFor.Append(strCLARBNumber.Length > 0 ? "CLARB Number: " + strCLARBNumber + "<br />" : "");
        strbSearchedFor.Append(strFloridaStateNumber.Length > 0 ? "Florida State Number: " + strFloridaStateNumber + "<br />" : "");
        strbSearchedFor.Append(strCSLANumber.Length > 0 ? "CSLA Number: " + strCSLANumber + "<br />" : "");
        strbSearchedFor.Append(strEmail.Length > 0 ? "Email: " + strEmail + "<br />" : "");
        uiLitSearchedFor.Text = strbSearchedFor.ToString();
        uiGVAllAttendees.DataSource =null;
        uiGVAllAttendees.DataBind();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LACESConnectionString"].ToString());

        conn.Open();
        DataSet ds = new DataSet();
        string sqlStatement = "ASLA_SearchParticipants";

        SqlCommand cmd = new SqlCommand(sqlStatement, conn);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FirstName", strFirstName);
        cmd.Parameters.AddWithValue("@LastName", strLastName);
        cmd.Parameters.AddWithValue("@ASLAMemberNumber", strASLAMemberNumber);
        cmd.Parameters.AddWithValue("@CLARBNumber", strCLARBNumber);
        cmd.Parameters.AddWithValue("@FloridaStateNumber", strFloridaStateNumber);
        cmd.Parameters.AddWithValue("@MiddleInitial", strMiddleInitial);
        cmd.Parameters.AddWithValue("@CSLANumber", strCSLANumber);
        cmd.Parameters.AddWithValue("@email", strEmail);
        
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        adapter.Fill(ds);
        uiGVAllAttendees.DataSource = ds;
        uiGVAllAttendees.DataBind();


    }
    protected void gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strFirstName = "", strLastName = "", strASLAMemberNumber = "", strCLARBNumber = "", strFloridaStateNumber = "", strMiddleInitial = "", strCSLANumber = "", strEmail = "", strID="";
        WebControl wc = e.CommandSource as WebControl;
        GridViewRow row = wc.NamingContainer as GridViewRow;
        object strid = (object)uiGVAllAttendees.DataKeys[row.RowIndex].Value;
        TextBox tbID = (TextBox)row.FindControl("uiTxtParticipantID");
        if (tbID != null)
        {
            strID = tbID.Text;
        }

        long participantID;
        if (long.TryParse(strID, out participantID))
        {
            TextBox tbFirstName = (TextBox)row.FindControl("uiTxtFirstName");
            strFirstName = tbFirstName != null ? tbFirstName.Text : "";
            TextBox tbLastName = (TextBox)row.FindControl("uiTxtLastName");
            strLastName = tbLastName != null ? tbLastName.Text : "";
            TextBox tbAslaMemberNumber = (TextBox)row.FindControl("uiTxtASLAMemberNumber");
            strASLAMemberNumber = tbAslaMemberNumber != null ? tbAslaMemberNumber.Text : "";
            TextBox tbCLARBNumber = (TextBox)row.FindControl("uiTxtCLARBNumber");
            strCLARBNumber = tbCLARBNumber != null ? tbCLARBNumber.Text : "";
            TextBox tbFloridaNumber = (TextBox)row.FindControl("uiTxtFloridaStateNumber");
            strFloridaStateNumber = tbFloridaNumber != null ? tbFloridaNumber.Text : "";
            TextBox tbMiddleInitial = (TextBox)row.FindControl("uiTxtMiddleInitial");
            strMiddleInitial = tbMiddleInitial != null ? tbMiddleInitial.Text : "";
            TextBox tbCSLANumber = (TextBox)row.FindControl("uiTxtCSLANumber");
            strCSLANumber = tbCLARBNumber != null ? tbCSLANumber.Text : "";
            TextBox tbEmail = (TextBox)row.FindControl("uiTxtEmail");

            
            Label lblResponse = (Label)row.FindControl("uiLblResponse");
            strEmail = tbEmail != null ? tbEmail.Text : "";
            try
            {
                ParticipantDataAccess oParticipantDataAccess = new ParticipantDataAccess();
                Participant currentParticipant = new Participant();

                //check weather the participant is exist
                currentParticipant = oParticipantDataAccess.GetParticipantByID(participantID);

                if (currentParticipant != null)
                {
                    //Attendees exist so update

                    currentParticipant.LastName = strLastName;
                    currentParticipant.FirstName = strFirstName;
                    currentParticipant.ASLANumber = strASLAMemberNumber;
                    currentParticipant.CLARBNumber = strCLARBNumber;
                    currentParticipant.FloridaStateNumber = strFloridaStateNumber;
                    currentParticipant.ID = participantID;
                    currentParticipant.CSLANumber = strCSLANumber;
                    currentParticipant.MiddleInitial = strMiddleInitial;
                    currentParticipant.Email = strEmail;
                    currentParticipant = oParticipantDataAccess.Update(currentParticipant);
                    lblResponse.Text = "Updated";
                }
            }
            catch (Exception ex)
            {
                lblResponse.Text = ("Error:" + ex.Message);
            }
        }

    }
    protected void btnFindParticipants_Click(object sender, EventArgs e)
    {
        string strFirstName = txtFirstName.Text;
        string strLastName = txtLastName.Text;
        string strCSLA = txtCSLA.Text;
        string strEmail = txtEmail.Text;
        string strFLNumber = txtFL.Text;
        string strMiddle = txtMiddleName.Text;
        string strASLANumber =txtASLA.Text;
        string strCLARB = txtCLARB.Text;
        PopulateAttendeeList(strFirstName, strLastName, strASLANumber, strCLARB, strFLNumber, strMiddle, strCSLA, strEmail);

    }
}