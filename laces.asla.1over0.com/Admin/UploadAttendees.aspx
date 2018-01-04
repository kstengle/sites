<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="UploadAttendees.aspx.cs" Inherits="Admin_UploadAttendees" Title="Admin: Upload Attendees in Excel | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">

    <script type="text/javascript">
       function checkField()
       {
           var control = document.getElementById('<%= FileUpload1.ClientID %>');
           var lblMsgControl = document.getElementById('<%= lb_error_message.ClientID %>');
           var supportedFile = ".xls"
           
           if(control.value=="")
           {
                lblMsgControl.style["color"] = "red";
                lblMsgControl.innerHTML="Select your Attendees' excel file.<br/>&nbsp;";
                return false;
           }
           
           if(control.value.indexOf('.xls')==-1)
           {
                lblMsgControl.style["color"] = "red";
                lblMsgControl.innerHTML="Your provided file type is not supported.<br/>&nbsp;";
                
                return false;
           }
           if(control.value!="")
           {
                return true;
           }
           return false;
       }
       
       function movefocus(evt)
        {
            var keyCode = GetKeyCode(evt)
            if(keyCode != 9) movefocus1();
        }
        function movefocus1()
        {
            document.getElementById("<%=Button1.ClientID%>").focus();
        }
        
    </script>

    <script type="text/javascript" language="javascript" src="/laces/javascript/perticipant.js"></script>

    <!--Hidden field used to check weather save or not the participant list form-->
    <div id="message" style="text-align: left;">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
        <input id="HiddenSaveInformation" type="hidden" value="N" runat="server" />
        <input id="HiddenRedirectUrl" type="hidden" value="1" runat="server" />
    </div>

    <!-- [End] Left Side Button HTML table -->
    <div id="uploadFileDiv" runat="server">

        <div id="dvParticipantList" runat="server">
        </div>
        <div class="title" id="dvHeader" runat="server"></div>
        <div>
            <div id="lb_error_message" style="color: Red; padding-top: 10px;" runat="server">
            </div>
            <a href="../download/LACES_Attendees_Template.xls">Click here</a> to get MS Excel
            template.
            <br />
            <br />
            Please use the template provided to upload your attendees. You can save our template
            on your desktop. Please don’t edit the column headers. <br />Using our template will ensure
            successful upload.<br />
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" onkeydown='return movefocus(event)'
                onmouseup='if(event.button==2){movefocus1()}' />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="UPLOAD" CssClass="commonButton btn107"
                OnClick="btnUpload_Click" OnClientClick="javascript:return checkField()" />
            <br />
            <br />
            <%--<a href="../download/LACES_Attendees_Template.xls">Click here</a> to get the MS
            Excel template.--%>
        </div>
    </div>
    <!--End Content Place Holder-->
</asp:Content>
