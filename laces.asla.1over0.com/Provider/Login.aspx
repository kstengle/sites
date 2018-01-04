<%@ Page Language="C#" MasterPageFile="~/Provider/ProviderMaster.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Provider_ProviderLogin"
    Title="Provider Login | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">

    <script language="javascript" type="text/javascript">
    
    ///<summary> validation checking of user input</summary>
    function ValidateProviderLogin()
    { 
        var message="";
        var flag=false;
    
        ///Checking the empty string of Email Address
        if(document.forms[0]["<%=txtEmail.ClientID%>"].value.length==0)
        {
             message+="<li>Email address is required.</li>";
             if(flag==false)
             {
              document.forms[0]["<%=txtEmail.ClientID%>"].select();
             }
             flag=true;
        }
        
        ///Checking valid email address
        else if(!IsValidEmail("<%=txtEmail.ClientID%>"))
        {
            message+="<li>Email address is invalid.</li>";
            if(flag==false)
            {
              document.forms[0]["<%=txtEmail.ClientID%>"].select();
            }
            flag=true;
        }
       ///Checking Empty password
        if(document.forms[0]["<%=txtPassword.ClientID%>"].value.length==0)
        {
            message+="<li>Password is required.</li>";
            if(flag==false)
            {
                document.forms[0]["<%=txtPassword.ClientID%>"].select();
            }
            flag=true;
       }
      
       if(flag==true)
       {
            document.getElementById('<%=lblErrorSummary.ClientID%>').innerHTML="";
            document.getElementById('<%=lblErrorSummary.ClientID%>').innerHTML="Please correct the following error(s)." + "<ul>"+message+"</ul>";
            return false;
       }     
       else
       {  
            document.getElementById('<%=lblErrorSummary.ClientID%>').innerHTML="";    
            return true;
       }
       return false;
    }
    
    
    
  
    </script>

    <table border="0" width="375px">
        <tr>
            <td align="right" colspan="2" valign="top">
                <div align="left" class="title">
                    Approved Provider Login</div>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left" class="ErrorSummery">
                <asp:Label ID="lblErrorSummary" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="40%" align="left">
                <asp:TextBox ID="txtEmail" CssClass="LoginTextBox" runat="server" MaxLength="64"></asp:TextBox>
            </td>
            <td valign="middle" align="left" class="LoginLabel">
                Email
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:TextBox ID="txtPassword" CssClass="LoginTextBox" TextMode="Password" runat="server"
                    MaxLength="32"></asp:TextBox>
            </td>
            <td valign="middle" align="left" class="LoginLabel">
                Password
            </td>
        </tr>
        
        <tr>
            <td align="left">
                <br />
                <asp:Button ID="btnSignIn" runat="server" CssClass="commonButton btn107" Text="SIGN IN"
                    OnClientClick="return ValidateProviderLogin()" OnClick="btnSignIn_Click" />
            </td>
            <td valign="middle" align="left" class="LoginLabel">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightane" runat="Server">
    <table>
        <tr>
            <td width="340" valign="top">
                <p class="NormalText">
                    Welcome to the Landscape Architecture Continuing Education System™ Provider Portal.
                    Please login to access the following services:
                    <ul>
                        <li>Continuing Education Course Registration</li>
                        <li>Upload Attendee Information</li>
                        <li>Renew your
                            <%=LACESConstant.LACES_TEXT%>
                            Provider Certification</li>
                    </ul>
                </p>
                <p class="NormalText">
                    Can't Remember <a href="ForgotPassword.aspx">Your Login and/or Password</a>?</p>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
if('<%=isInvalidLogin%>'=='True')
    {
    document.forms[0]["<%=txtEmail.ClientID%>"].select();
        
    }
    </script>

</asp:Content>
