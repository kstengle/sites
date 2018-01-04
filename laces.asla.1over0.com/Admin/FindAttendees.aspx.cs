/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: FindAttendees.aspx.cs
 * Purpose/Function: To get input for participant search criteria in Admin section
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/17/2008      Create this Page with initial requirements
 * 1.1              Alamgir Hossain     07/09/2008      Work on Enhancement 2    
 --------------------------------------------------------------------------------*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;

/// <summary>
/// Get Participant Search Criteria from Admin
/// </summary>
public partial class Admin_FindAttendees : AdminBasePage
{
    /// <summary>
    /// Page Load Event to load input fields initially
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ///If Reached this page form Participant Search Result page by clicking search again
            ///And having criteria into the session then load existing selection value
            if (Request.QueryString["SearchAgain"] != null && Session[LACESConstant.SessionKeys.SEARCH_PARTICIPANT_CRITERIA] != null)
            {
                ///Get Search criteria from session
                SearchParticipant objSearchCriteria = (SearchParticipant)Session[LACESConstant.SessionKeys.SEARCH_PARTICIPANT_CRITERIA];
                if (objSearchCriteria != null)
                    loadExistingSelection(objSearchCriteria);
            }

            ///Focus to the first input control
            txtFirstName.Focus();
        }

    }

    /// <summary>
    /// Get Search Criteria and redirect to result page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFindParticipants_Click(object sender, EventArgs e)
    {
        SearchParticipant objParticipant = buildSearchCriteria();
        objParticipant.SortColumn = "LastName";
        objParticipant.SortOrder = "ASC";

        //Add search criteria into session
        Session.Add(LACESConstant.SessionKeys.SEARCH_PARTICIPANT_CRITERIA, objParticipant);
        Response.Redirect("AttendeeSearchResult.aspx");
    }

    /// <summary>
    /// Generate Search criteria from user selection
    /// </summary>
    /// <returns></returns>
    private SearchParticipant buildSearchCriteria()
    {
        SearchParticipant objParticipant = new SearchParticipant();       

        string searchQuery = "";

        ///Criteria for First Name field
        if (txtFirstName.Text.Trim() != string.Empty)
        {            
            searchQuery = "[FirstName] LIKE '" + txtFirstName.Text.Trim().Replace("'", "''") + "%'";            
            objParticipant.FirstName = txtFirstName.Text.Trim();
        }

        ///Criteria for Last Name field
        if (txtLastName.Text.Trim() != string.Empty)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[LastName] LIKE '" + txtLastName.Text.Trim().Replace("'", "''") + "%'";
            objParticipant.LastName = txtLastName.Text.Trim();
        }

        ///Criteria for middle Name field
        if (txtMiddleName.Text.Trim() != string.Empty)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[MiddleInitial] LIKE '" + txtMiddleName.Text.Trim().Replace("'", "''") + "%'";
            objParticipant.MiddleName = txtMiddleName.Text.Trim();
        }



        ///Criteria for ASLA Number field
        if (txtASLA.Text.Trim() != string.Empty)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[ASLAMemberNumber] LIKE '%" + txtASLA.Text.Trim().Replace("'", "''") + "%'";
            objParticipant.ASLANumber = txtASLA.Text.Trim();
        }

        ///Criteria for CLARB Number field
        if (txtCLARB.Text.Trim() != string.Empty)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[CLARBNumber] LIKE '%" + txtCLARB.Text.Trim().Replace("'", "''") + "%'";
            objParticipant.CLARBNumber = txtCLARB.Text.Trim();
        }

        ///Criteria for csla Number field
        if (txtCSLA.Text.Trim() != string.Empty)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[CSLANumber] LIKE '%" + txtCSLA.Text.Trim().Replace("'", "''") + "%'";
            objParticipant.CSLANumber = txtCSLA.Text.Trim();
        }




        ///Criteria for fl Number field
        if (txtFL.Text.Trim() != string.Empty)
        {
            if (searchQuery != "")
                searchQuery += " AND ";
            searchQuery += "[FloridaStateNumber] LIKE '%" + txtFL.Text.Trim().Replace("'", "''") + "%'";
            objParticipant.FLNumber = txtFL.Text.Trim();
        }

        ///For blank search
        if (searchQuery == "")
            searchQuery = " 1 = 1 ";

        objParticipant.WhereCondition = searchQuery;
        return objParticipant;
    }

    /// <summary>
    /// Assign existing search criteria selection
    /// </summary>
    /// <param name="objSearch"></param>
    private void loadExistingSelection(SearchParticipant objSearch)
    {
        ///Assign First Name
        txtFirstName.Text = objSearch.FirstName;

        ///Assign Last Name
        txtLastName.Text = objSearch.LastName;

        ///Assign ASLA Number
        txtASLA.Text = objSearch.ASLANumber;

        ///Assign CLARB Number
        txtCLARB.Text = objSearch.CLARBNumber;

        ///Assign FL State Number
        txtFL.Text = objSearch.FLNumber;

        ///Assign CSLA Number
        txtCSLA.Text = objSearch.CSLANumber;

        ///Assign middle name
        txtMiddleName.Text = objSearch.MiddleName;
    }
}
