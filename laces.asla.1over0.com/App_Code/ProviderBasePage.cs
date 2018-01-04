/*------------------------------------------------------------------------------
 * Project Name: LACES
 * Client Name: American Society of Landscape Architects(ASLA)
 * Component Name: ProviderBasePage.cs
 * Purpose/Function: Base functions of Provider section
 * Author: Matiur Rahman
 * Version              Author              Date            Reason
 * 1.0              Matiur Rahman       01/13/2008      Create this Page for authentication checking
 --------------------------------------------------------------------------------*/
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using System.Diagnostics;

using Pantheon.ASLA.LACES.Common;
using Pantheon.ASLA.LACES.DataAccess;

/// <summary>
/// Base Page for Provider section of LACES project to include common functions
/// </summary>
public class ProviderBasePage : System.Web.UI.Page
{
    /// <summary>
    /// constructor
    /// </summary>
    public ProviderBasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Override the onload event handler
    /// </summary>
    /// <param name="e"></param>
    protected override void OnLoad(EventArgs e)
    {
        try
        {
            ///Check Session for logged in provider information
            if (Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER] != null)
            {
                //Provider provider = (Provider)Session[LACESConstant.SessionKeys.LOGEDIN_PROVIDER];
                ApprovedProvider provider = (ApprovedProvider)Session[LACESConstant.SessionKeys.LOGEDIN_APPROVED_PROVIDER]; // get approved provider information from Session 

                if (provider.ID > 0)
                {
                    ///Show loggedin provider name
                    Label lblProviderName = (Label)Master.FindControl("lblProviderName");
                    if (lblProviderName != null)
                        lblProviderName.Text = "Signed in as: " + Server.HtmlEncode(provider.OrganizationName);                  
                    try
                    {
                        //change the login status
                        ((HtmlGenericControl)Master.FindControl("loginTd")).Attributes["class"] = "loggedIn providerloginStatus";
                    }
                    catch (Exception ex) { }
                   
                    base.OnLoad(e);
                }
            }
            else
            {
                Response.Redirect(LACESConstant.URLS.HOME_PAGE);
            }
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// Set left content palce holder width 75% and right content place holder width 25% using css
    /// </summary>
    //public void IncreaseLeftContentWidth()
    //{
    //    HtmlTableCell tdLftPane = (HtmlTableCell)Master.FindControl("lftPane");
    //    if (tdLftPane != null)
    //        tdLftPane.Attributes.Add("class", "leftPane75");

    //    HtmlTableCell tdRgtPane = (HtmlTableCell)Master.FindControl("rgtPane");
    //    if (tdRgtPane != null)
    //        tdRgtPane.Attributes.Add("class", "rightPane25");
    //}

    /// <summary>
    /// This function accepts the webcontrol as parameter which indicates if the control
    /// has any chnge in value the user will get prompt to confirm while trying to 
    /// navigate away from this page
    /// </summary>
    /// <param name="wc"></param>
    /// 
    public void MonitorChangesWithPostBack(WebControl wc, string value)
    {
        if (wc == null)
        {
            return;
        }

        /* In side the following if-else block the controls that needs confirmation 
        is assigned to the array which will later be used by the script */
        if (wc is CheckBoxList || wc is RadioButtonList)
        {
            for (int i = 0; i <= ((ListControl)(wc)).Items.Count - 1; i++)
            {
                ListControl li = (ListControl)(wc);
                ClientScript.RegisterArrayDeclaration("monitorChangesIDs", "\"" + string.Concat(wc.ClientID, "_", i) + "\"");
                ClientScript.RegisterArrayDeclaration("monitorChangesValues", li.Items[i].Selected.ToString().ToLower());
            }
        }
        else if (wc is ListBox)
        {
            ListBox list = (ListBox)(wc);
            string selectedItem = string.Empty;
            for (int i = 0; i < list.Items.Count; i++)
            {                
                if (list.Items[i].Selected)
                {
                    if (selectedItem == string.Empty)
                    {
                        selectedItem = list.Items[i].Value;
                    }
                    else
                    {
                        selectedItem = selectedItem + ":" + list.Items[i].Value;
                    }
                }
            }
            ClientScript.RegisterArrayDeclaration("monitorChangesIDs", "\"" + wc.ClientID + "\"");
            ClientScript.RegisterArrayDeclaration("monitorChangesValues", "\"" + selectedItem + "\"");
        }
        else
        {
            ClientScript.RegisterArrayDeclaration("monitorChangesIDs", "\"" + wc.ClientID + "\"");
            if (value.ToLower() != "true" && value.ToLower() != "false")
            {
                ClientScript.RegisterArrayDeclaration("monitorChangesValues", (value == "" || value == null) ? "''" : "'" + value.Replace("'", "&#39;").Replace("\\", "&#47;").Replace("\n", "&|b").Replace("\r", "&#b").Replace("/","~!@=|") + "'"); 
            }
            else
            {
                ClientScript.RegisterArrayDeclaration("monitorChangesValues", (value == "" || value == null) ? "''" : value.ToLower());
            }
        }
        AssignMonitorChangeValuesOnPageLoadWithPostBack();
    }
    /// <summary>
    /// This function is called internally from MonitorChange Function.
    /// It generates this required javascript to show the confirmation when required.
    /// </summary>
    public void AssignMonitorChangeValuesOnPageLoadWithPostBack()
    {
        if (!(ClientScript.IsStartupScriptRegistered("monitorChangesAssignment")))
        {
            string StartupScriptString = "<script language=\"JavaScript\">";
            //StartupScriptString += "assignInitialValuesForMonitorChanges();";
            StartupScriptString += "</script>";

            //ClientScript.RegisterStartupScript(System.Type.GetType("System.String"), "monitorChangesAssignment", StartupScriptString);

            string ClientScriptString = "<script language=\"JavaScript\">";
            ClientScriptString += " function assignInitialValuesForMonitorChanges() {";
            ClientScriptString += " for (var i = 0; i < monitorChangesIDs.length; i++) {";
            ClientScriptString += " var elem = document.getElementById(monitorChangesIDs[i]);";
            ClientScriptString += " if (elem) if (elem.type == 'checkbox' || elem.type == 'radio') monitorChangesValues[i] = elem.checked; else monitorChangesValues[i] = elem.value;";
            ClientScriptString += " }";
            ClientScriptString += " }";

            ClientScriptString += " var needToConfirm = true;";
            ClientScriptString += " window.onbeforeunload = confirmClose;";//This is the function call that cause the confirmation.
            ClientScriptString += " function confirmClose() {";
            ClientScriptString += " if (!needToConfirm) { needToConfirm = true; return; } ";
            ClientScriptString += " for (var i = 0; i < monitorChangesValues.length; i++) { ";
            ClientScriptString += " var elem = document.getElementById(monitorChangesIDs[i]);   ";
            ClientScriptString += " if (elem) if (((elem.type == 'checkbox' || elem.type == 'radio') && elem.checked != monitorChangesValues[i]) || (elem.type != 'checkbox' && elem.type != 'radio' && elem.type!='select-multiple' && changeCrt(pk_fixnewlines_textarea(elem.value)) != revertSpecialCharacter(monitorChangesValues[i])) || (elem.type=='select-multiple' && monitorChangesValues[i]!=getMutipleSelectionDropdownValue(monitorChangesIDs[i]))) { needToConfirm = false; setTimeout('resetFlag()', 550);";
            ClientScriptString += "return \"You have modified the data entry fields since last savings. If you leave this page, any changes will be lost. To save these changes, click Cancel to return to the page, and then Save the data.\"; }";
            ClientScriptString += " }";

            ClientScriptString += " }";
            ClientScriptString += " function resetFlag() { needToConfirm = true; } ";
            ClientScriptString += "</script>";
            ClientScript.RegisterClientScriptBlock(System.Type.GetType("System.String"), "monitorChangesAssignmentFunction", ClientScriptString);
        }
    }


    #region Confirmation On Page Exit
    /// <summary>
    /// This function accepts the webcontrol as parameter which indicates if the control
    /// has any chnge in value the user will get prompt to confirm while trying to 
    /// navigate away from this page
    /// </summary>
    /// <param name="wc"></param>
    /// 
    public void MonitorChanges(WebControl wc)
    {
        if (wc == null)
        {
            return;
        }

        /* In side the following if-else block the controls that needs confirmation 
        is assigned to the array which will later be used by the script */
        if (wc is CheckBoxList || wc is RadioButtonList)
        {
            for (int i = 0; i <= ((ListControl)(wc)).Items.Count - 1; i++)
            {
                ClientScript.RegisterArrayDeclaration("monitorChangesIDs", "\"" + string.Concat(wc.ClientID, "_", i) + "\"");
                ClientScript.RegisterArrayDeclaration("monitorChangesValues", "null");
            }
        }
        else
        {
            ClientScript.RegisterArrayDeclaration("monitorChangesIDs", "\"" + wc.ClientID + "\"");
            ClientScript.RegisterArrayDeclaration("monitorChangesValues", "null");
        }
        AssignMonitorChangeValuesOnPageLoad();
    }
    /// <summary>
    /// This function is called internally from MonitorChange Function.
    /// It generates this required javascript to show the confirmation when required.
    /// </summary>
    public void AssignMonitorChangeValuesOnPageLoad()
    {
        if (!(ClientScript.IsStartupScriptRegistered("monitorChangesAssignment")))
        {
            string StartupScriptString = "<script language=\"JavaScript\">";
            StartupScriptString += "assignInitialValuesForMonitorChanges();";
            StartupScriptString += "</script>";

            ClientScript.RegisterStartupScript(System.Type.GetType("System.String"), "monitorChangesAssignment", StartupScriptString);

            string ClientScriptString = "<script language=\"JavaScript\">";
            ClientScriptString += " function assignInitialValuesForMonitorChanges() {";
            ClientScriptString += " for (var i = 0; i < monitorChangesIDs.length; i++) {";
            ClientScriptString += " var elem = document.getElementById(monitorChangesIDs[i]);";
            ClientScriptString += " if (elem) if (elem.type == 'checkbox' || elem.type == 'radio') monitorChangesValues[i] = elem.checked; else monitorChangesValues[i] = elem.value;";
            ClientScriptString += " }";
            ClientScriptString += " }";
            ClientScriptString += " var needToConfirm = true;";
            ClientScriptString += " window.onbeforeunload = confirmClose;";//This is the function call that cause the confirmation.
            ClientScriptString += " function confirmClose() {";
            ClientScriptString += " if (!needToConfirm) return;";
            ClientScriptString += " for (var i = 0; i < monitorChangesValues.length; i++) {";
            ClientScriptString += " var elem = document.getElementById(monitorChangesIDs[i]);";
            ClientScriptString += " if (elem) if (((elem.type == 'checkbox' || elem.type == 'radio') && elem.checked != monitorChangesValues[i]) || (elem.type != 'checkbox' && elem.type != 'radio' && elem.value != monitorChangesValues[i])) { needToConfirm = false; setTimeout('resetFlag()', 750);";
            ClientScriptString += "return \"You have modified the data entry fields since last savings. If you leave this page, any changes will be lost. To save these changes, click Cancel to return to the page, and then Save the data.\"; }";
            ClientScriptString += " }";
            ClientScriptString += " }";
            ClientScriptString += " function resetFlag() { needToConfirm = true; } ";
            ClientScriptString += "</script>";
            ClientScript.RegisterClientScriptBlock(System.Type.GetType("System.String"), "monitorChangesAssignmentFunction", ClientScriptString);
        }
    }

    /// <summary>
    /// Buttons that should not give confirm
    /// </summary>
    /// <param name="wc"></param>
    /// 
    public void BypassModifiedMethod(WebControl wc)
    {
        wc.Attributes.Add("onclick", "javascript:needToConfirm = false; setTimeout('resetFlag()', 250);");
        //wc.Attributes["onunload"] = "javascript:needToConfirm = false;";
    }

    #endregion
    
}
