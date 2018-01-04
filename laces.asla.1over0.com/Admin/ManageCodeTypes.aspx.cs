/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: ASLA
 * Component Name: ManageCodeTypes.aspx
 * Purpose/Function: Used to manage course code type for administrator
 *
 * Author: Wasim Majid
 * Version              Author            Date             Reason  
 * 1.0                 Tarek Jubaer     01/22/2008   Initial development: Request New Code (Task 5466)
 * 1.1                 Tarek Jubaer     01/22/2008   Manage Course Code Types (Task 5468)
 * 1.1                 Alamgir Hossain  05/09/2008   Work on Enhancement 2  
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
/// Used to manage course code type for administrator
/// </summary>
public partial class Admin_AdminManageCodeTypes : AdminBasePage
{
    protected HtmlTableRow htRow;
    protected HtmlTableCell htCell;
    protected HtmlGenericControl htDiv;
    

    #region Page load event handler
    /// <summary>
    /// Admin manage course code type page load function    
    /// Loads existing course code types for display
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            //generate code type table display
            populateCourseTypeTable();            
        }
        //If code type id found perform delete action
        if (Request.QueryString["CodeTypeID"] != null)
        {
            CourseTypeDataAccess courseTypeDAL = new CourseTypeDataAccess();
            CourseCodeType code = new CourseCodeType();
            //check if code type already exists with given id
            code = courseTypeDAL.GetCodeTypeByID(Convert.ToInt32(Request.QueryString["CodeTypeID"]));
            if (code == null)
            {
                //If not found alert user that code type is already deleted                
                lblMessage.Text = "Code type that you want to delete is already deleted by another admin.<BR>&nbsp;";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                //else perform delete action
                int delCode = 0;
                delCode = courseTypeDAL.DeleteCodeTypeByCodeTypeID(Convert.ToInt32(Request.QueryString["CodeTypeID"]));
                //if delcode is 0 alert user that error in delete operation
                if (delCode == 0)
                {                    
                    lblMessage.Text = "Could not delete code type. This code type may be in use. Please try again.<BR>&nbsp;";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //notify user about delete success                    
                    lblMessage.Text = "Code type deleted successfully.<BR>&nbsp;";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    //generate code type table display
                    populateCourseTypeTable();
                }
            }
        }
        txtCodeType.Focus();   
    }
    #endregion

    #region Populate course type display table
    /// <summary>
    ///  Populate course type display table
    /// </summary>
    private void populateCourseTypeTable()
    {        
        //Get current code types from database
        CourseTypeDataAccess courseTypeDAL = new CourseTypeDataAccess();
        IList<CourseCodeType> courseTypeList = courseTypeDAL.GetAllCourseCodeTypes(); // Get All Course Code
        //Clear table rows
        tblParent.Rows.Clear();
        string innerHTML = "";
        //If no code type found in database
        if (courseTypeList.Count == 0)
        {
            //Create table row
            createHTMLRow(tblParent);
            //Create table column
            createHTMLColumn(htRow);
            //Add column attributes
            htCell.Attributes.Add("align", "center");
            htCell.Attributes.Add("valign", "middle");
            htCell.Attributes.Add("class", "LoginLabel");
            //Display not found message
            innerHTML = "<b>No Code Type found.</b>";
            //Add inner HTML to cell
            addInnerHTML(htCell, innerHTML.ToString());
        }
        else//If code types found in database
        {
            //for all code types in list
            foreach (CourseCodeType courseCodeType in courseTypeList)
            {
                //create new row 
                createHTMLRow(tblParent);
                //create new column
                createHTMLColumn(htRow);
                //add course code in cell
                innerHTML = courseCodeType.CodeType;
                //add column addtibutes
                htCell.Attributes.Add("align", "left");
                htCell.Attributes.Add("valign", "middle");
                htCell.Attributes.Add("width", "50%");
                //add inner HTML to cell
                addInnerHTML(htCell, innerHTML.ToString());
                //create new column
                createHTMLColumn(htRow);
                //display remove link with confirm delete function
                innerHTML = "<a href=\"?CodeTypeID=" + courseCodeType.ID + "\" onClick='javascript:return confirmCodeTypeDelete()'>remove</a>";
                //add attributes to column
                htCell.Attributes.Add("align", "left");
                htCell.Attributes.Add("valign", "middle");
                htCell.Attributes.Add("class", "participantList");
                //add inner HTML to column
                addInnerHTML(htCell, innerHTML.ToString()); 
            }
        }
    }
    #endregion

    #region HTML table generating functions
    /// <summary>
    /// Creates a html column for the given row.
    /// </summary>
    /// <param name="row"></param>
    private void createHTMLColumn(HtmlTableRow row)
    {
        htCell = new HtmlTableCell();
        row.Cells.Add(htCell);
    }

    /// <summary>
    /// Creates a html table row for the given table
    /// </summary>
    /// <param name="htTable"></param>
    private void createHTMLRow(HtmlTable htTable)
    {
        htRow = new HtmlTableRow();
        htTable.Rows.Add(htRow);
    }

    /// <summary>
    /// Creates a div under a table cell
    /// </summary>
    private void createDiv(HtmlTableCell tCell)
    {
        htDiv = new HtmlGenericControl("div");
        tCell.Controls.Add(htDiv);
    }

    /// <summary>
    /// Adds css class to a table cell
    /// </summary>
    /// <param name="tCell"></param>
    /// <param name="cssClass"></param>
    /// <param name="persistExisting">Overwrites the attribute or adds with existing?</param>
    private void addCssClass(HtmlTableCell tCell, string cssClass, bool persistExisting)
    {
        string Classes = "";
        if (persistExisting == true)
        {
            Classes = tCell.Attributes["class"] == null ? "" : tCell.Attributes["class"].ToString();
        }
        Classes = Classes + " " + cssClass;
        tCell.Attributes.Remove("class");
        tCell.Attributes.Add("class", Classes);
    }

    /// <summary>
    /// Adds css class to a HtmlGenericControl
    /// </summary>
    /// <param name="tCell"></param>
    /// <param name="cssClass"></param>
    /// <param name="persistExisting">Overwrites the attribute or adds with existing?</param>
    private void addCssClass(HtmlGenericControl ctrl, string cssClass, bool persistExisting)
    {
        string Classes = "";
        if (persistExisting == true)
        {
            Classes = ctrl.Attributes["class"] == null ? "" : ctrl.Attributes["class"].ToString();
        }
        Classes = Classes + " " + cssClass;
        ctrl.Attributes.Remove("class");
        ctrl.Attributes.Add("class", Classes);
    }

    /// <summary>
    /// Adds inner html to table cell
    /// </summary>
    /// <param name="tCell"></param>
    /// <param name="innerHTML"></param>
    private void addInnerHTML(HtmlTableCell tCell, string innerHTML)
    {
        tCell.InnerHtml = innerHTML;
    }

    /// <summary>
    /// Adds inner html to div
    /// </summary>
    /// <param name="tCell"></param>
    /// <param name="innerHTML"></param>
    private void addInnerHTML(HtmlGenericControl tCell, string innerHTML)
    {
        tCell.InnerHtml = innerHTML;
    }

    /// <summary>
    /// Adds line break
    /// </summary>
    /// <param name="tCell"></param>
    private void addLineBreak(HtmlTableCell tCell)
    {
        HtmlGenericControl br = new HtmlGenericControl("br");
        tCell.Controls.Add(br);
    }

    #endregion

    #region Add new code type button event handler
    /// <summary>
    /// Perform add new code type button action
    /// Adds new code type
    /// Notify user about success or error
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddNewCodeType_Click(object sender, EventArgs e)
    {
        //if code name is not null
        if (txtCodeType.Text.Trim() != "")
        {
            CourseTypeDataAccess courseTypeDAL = new CourseTypeDataAccess();
            CourseCodeType code = new CourseCodeType();
            //check if code type already exists with given name
            code = courseTypeDAL.GetCodeTypeByName(Server.HtmlEncode(txtCodeType.Text.Trim()).ToString());
            if (code == null)
            {
                //if not found name in database add new code type
                int newCodeTypeId = courseTypeDAL.AddCodeType(Server.HtmlEncode(txtCodeType.Text.Trim()).ToString());
                //alert user about status
                if (newCodeTypeId > 0)
                {
                    lblMessage.Text = "Code type added successfully.<BR>&nbsp;";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    txtCodeType.Text = "";
                }
                else
                {                    
                    lblMessage.Text = "Could not add code type. Please try again.<BR>&nbsp;";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                //if given name found in database notify user.                
                lblMessage.Text = "Code type with this name already exists.<BR>&nbsp;";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }        
        //populate code type table
        populateCourseTypeTable();
    }
    #endregion
}
