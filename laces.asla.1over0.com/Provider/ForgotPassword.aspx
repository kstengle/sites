<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="ForgotPassword.aspx.cs" Inherits="Provider_ProviderForgotPassword"
    Title="Forgot Password | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">

    <script language="javascript" type="text/javascript">
   
    
    ///<summary> validation checking of user input</summary>
    function ValidateForgotPassword()
    { 
        //Checking validity of Email Address through regular expression. If valid then return true else return false. 
        if(document.forms[0]["<%=txtEmail.ClientID%>"].value.length==0 || !IsValidEmail("<%=txtEmail.ClientID%>"))
        {
            
           var message="Please enter valid email address.";
           
           document.forms[0]["<%=txtEmail.ClientID%>"].select();
         
           document.getElementById('<%=lblErrorSummary.ClientID%>').innerHTML=message;
       
           return false;
                   
        }
        else
        {
          document.getElementById('<%=lblErrorSummary.ClientID%>').innerHTML="";    
          return true;
        }
              
     
    }
    
    </script>

    <div class="title">
        Forgot Password
    </div>

    <div>
        Note: To ensure you receive your login and password and renewal notifications from LA CES, please be sure to add <strong>laces@asla.org</strong> and <strong>lacesapp@asla.org</strong> to your e-mail “safe” list. You can also check your SPAM filter settings. If you have any questions, please <asp:Label ID="lblContactLink" runat="server"></asp:Label>.
    </div>
    <br />
    <div style="color: Green;">
        <asp:Label ID="lblPostBackMessage" runat="server" Text=""></asp:Label>
    </div>
    
    <asp:Label ID="lblErrorSummary" runat="server" Text="" ForeColor="red"></asp:Label>
    <br /><br />
    
    <div id="tblControls" runat="server" class="uk-grid LeftAlignTable" style="width:70%">
        <div class="uk-width-1-4">
            Email
        </div>
        <div class="uk-width-3-4">
                <asp:TextBox ID="txtEmail" CssClass="frmMainSearch LoginTextBox" runat="server" MaxLength="64" Width="60%"></asp:TextBox>
         </div>
        <div class="uk-width-1-4">
            &nbsp;
        </div>
        <div class="uk-width-3-4">                
                <%--Submit Button--%>
                <asp:Button ID="btnSubmit" UseSubmitBehavior="true" runat="server" CssClass="basicButton"
                    Text="SUBMIT" OnClientClick="return ValidateForgotPassword()" OnClick="btnSubmit_Click" style="margin-top:15px;" />
                <span>&nbsp;&nbsp;&nbsp;</span>
                <%--Cancel Button--%>
                <asp:Button ID="btnCancel" runat="server" CssClass="basicButton" Text="CANCEL"
                    OnClick="btnCancel_Click" style="margin-top:15px;" />
            </div>
    </div>

    <script type="text/javascript" language="javascript">
///Check Email Address is not valid in server side to show error message
    if('<%=isInvalidEmail%>'=='True')
    {
        ///Set Focus to Email Address.
        document.forms[0]["<%=txtEmail.ClientID%>"].select();
    }
    </script>

</asp:Content>
