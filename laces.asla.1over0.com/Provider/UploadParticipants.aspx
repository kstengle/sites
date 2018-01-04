<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="UploadParticipants.aspx.cs" Inherits="Upload_Participants" Title="Upload Participants| LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">

    <script type="text/javascript">
       function checkField()
       {
           var control = document.getElementById('<%= FileUpload1.ClientID %>');
           if(control.value!="")
           {
                return true;
           }
           return false;
       }
    </script>
    <script type="text/javascript" language="javascript" src="/laces/javascript/perticipant.js"></script>

    <!--Hidden field used to check weather save or not the participant list form-->
    <div id="message" style="text-align: left;">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
        <input id="HiddenSaveInformation" type="hidden" value="N" runat="server" />
        <input id="HiddenRedirectUrl" type="hidden" value="1" runat="server" />
    </div>
    <div runat="server" id="existingCourcesDiv">
    </div>
    
    
    <!-- [End] Left Side Button HTML table -->
    <div class="uploadExcelDivClass" id="uploadFileDiv" runat="server">
        <div class="HeaderText paddingLeft90">
            Existing Participants</div>
        <div id="dvParticipantList" runat="server" class="paddingLeft110">
        </div>
        <div class="HeaderText paddingLeft90">
            Upload Participants
        </div>
        <div class="paddingLeft110">
            <!-- Upload Excel File -->
            Upload MS Excel File<br />
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Upload" CssClass="commonButton btn107"
                OnClick="btnUpload_Click" OnClientClick="javascript:return checkField()" />
            <br />
        </div>
    </div>
    <!--End Content Place Holder-->
</asp:Content>
