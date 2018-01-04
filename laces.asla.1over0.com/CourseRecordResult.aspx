<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="CourseRecordResult.aspx.cs" Inherits="CourseRecordResult"
    Title="Course Record Results | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <div class="title" id="title" runat="server">
        Search Results
    </div>
    <div>
        <asp:Label ID="lblMessage" CssClass="searchMsg" runat="server"></asp:Label>
    </div>
    <div class="searchMsg">
        <asp:Repeater ID="uiRptCourseRecordResults" runat="server" OnItemDataBound="uiRptCourseRecordResults_ItemDataBound">
            <HeaderTemplate>
                <div class="uk-grid">
            </HeaderTemplate>
            <SeparatorTemplate>
                <div class="uk-width-1-1" style="height: 20px;"></div>
            </SeparatorTemplate>
            <ItemTemplate>
                <div class="uk-width-1-4">
                    Title
                </div>
                <div class="uk-width-3-4">
                    <a href="CourseDetails.aspx?CourseID=<%#Eval("ID").ToString() %>">
                        <%# Server.HtmlEncode(Eval("Title").ToString())%>
                    </a>
                </div>
                <div class="uk-width-1-4">
                    Location
                </div>
                <div class="uk-width-3-4">
                    <!--Course Location [City, State]-->
                    <%#getFormattedLocation(Eval("City").ToString(),Eval("StateProvince").ToString())%>
                </div>
                <div class="uk-width-1-4">
                    Start Date
                </div>
                <div class="uk-width-3-4">
                    <!--Course Start Data and End Date-->
                    <%#DateTime.Parse(Eval("StartDate").ToString()).ToShortDateString()%>
                </div>
                <div class="uk-width-1-4">
                    End Date
                </div>
                <div class="uk-width-3-4">
                    <!--Course Start Data and End Date-->
                    <%#DateTime.Parse(Eval("EndDate").ToString()).ToShortDateString()%>
                </div>
                <asp:PlaceHolder ID="uiphDistanceEducation" runat="server">
                    <div class="uk-width-1-4">
                        Distance Learning
                    </div>
                    <div class="uk-width-3-4">
                        <asp:Label ID="uiLblDistanceLearningText" runat="server"></asp:Label>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="uiPhHours" runat="server">
                    <div class="uk-width-1-4">
                        Hours
                    </div>
                    <div class="uk-width-3-4">
                        <%#Eval("Hours").ToString()%>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="uiPhHealths" runat="server">
                    <div class="uk-width-1-4">
                        Health, Safety and Welfare
                    </div>
                    <div class="uk-width-3-4">
                        <%#Eval("Healths").ToString()%>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="uiPhCourseCodes" runat="server">
                    <div class="uk-width-1-4">
                        Course Codes
                    </div>
                    <div class="uk-width-3-4">
                        <asp:Label ID="uiLblCourseCodes" runat="server"></asp:Label>
                        
                    </div>
                </asp:PlaceHolder>
                <div class="uk-width-1-4">
                    Provider Contact Information
                </div>
                <div class="uk-width-3-4">
                    <!--Provider Name Linked to Provider Details Page-->
                    <a href="ApprovedProviderDetails.aspx?ProviderID=<%#Eval("ProviderID").ToString()%>">
                        <%#Server.HtmlEncode(Eval("ProviderName").ToString())%>
                    </a>
                </div>

            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
        <div id="tdPreviousPage" runat="server"></div>
        <div id="tdNextPage" runat="server"></div>
        <br />
        <br />
        <asp:Button ID="btnExport" runat="server" CssClass="commonButton btn232" Text="DOWNLOAD SEARCH RESULTS" OnClick="btnExport_Click" />
    </div>
    <div style="color: Red">
        <asp:Label ID="uilblNoResultsFound" runat="server"></asp:Label>
    </div>

</asp:Content>
