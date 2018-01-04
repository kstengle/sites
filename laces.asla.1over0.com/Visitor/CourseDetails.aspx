<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="CourseDetails.aspx.cs" Inherits="Visitor_CourseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <div class="title">
        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
    </div>
    <div style="text-align: left;" class="paddingLeft245">
        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
    </div>
    <!-- Any Message of the Page or any Error Validation Ends -->
    <div class="uk-grid">
        <asp:PlaceHolder runat="server" id="uiPhRegEligibility">
            <div class="uk-width-1-4">
                Registration Eligibility
            </div>
             <div class="uk-width-3-4">
                    <asp:Label ID="lblRegEligibility" runat="server" Text=""></asp:Label>
              </div>                  
          </asp:PlaceHolder>  
              <div class="uk-width-1-4">
                        Start Date
              </div>
                <div class="uk-width-3-4">
                        <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>
                </div>
                <div class="uk-width-1-4">
                        End Date
                  </div>
                  <div class="uk-width-3-4">
                        <asp:Label ID="lblEndDate" runat="server" Text=""></asp:Label>
                    </div>
                <div class="uk-width-1-4">
                        Description
                </div>
                <div class="uk-width-3-4">
                        <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
                </div>
                <asp:PlaceHolder runat="server" id="uiPhCity">
                    <div class="uk-width-1-4">
                        Location
                    </div>
                    <div class="uk-width-3-4">
                        <asp:Label ID="lblCityState" runat="server" Text=""></asp:Label>
                    </div>
                </asp:PlaceHolder>                                
                <div class="uk-width-1-4">
                        Distance Learning
                </div>
                <div class="uk-width-3-4">
                        <asp:Label ID="lblDistanceEdu" runat="server" Text=""></asp:Label>
                </div>
                <div class="uk-width-1-4">
                        Course Equivalency
                </div>
                <div class="uk-width-3-4">                    
                        <asp:Label ID="lblCourseEquiv" runat="server" Text=""></asp:Label>
                </div>                
                <div class="uk-width-1-4">
                        Subjects
                 </div>
                <div class="uk-width-3-4">                    
                        <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label>
                </div>
                <div class="uk-width-1-4">
                        Health, Safety and Welfare
                </div>
                <div class="uk-width-3-4">
                        <asp:Label ID="lblHealthSafty" runat="server" Text=""></asp:Label>
                    </div>
                <div class="uk-width-1-4">
                        Hours
                 </div>
                <div class="uk-width-3-4">
                        <asp:Label ID="lblHour" runat="server" Text=""></asp:Label>
                    </div>
                 <div class="uk-width-1-4">
                        Learning Outcomes
                  </div>
                   <div class="uk-width-3-4">
                        <asp:Label ID="lblLeariningOutcome" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:PlaceHolder ID="uiPhInstructors" runat="server">                        
                    <div class="uk-width-1-4">
                        Instructors
                    </div>
                    <div class="uk-width-3-4">
                        <asp:Label ID="lblInstructors" runat="server" Text=""></asp:Label>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="uiPhURL" runat="server">                 
                    <div class="uk-width-1-4">
                        Website Registration
                    </div>
                    <div class="uk-width-3-4">
                        <a id="uiAWebsiteRegistration" runat="server"></a>
                    </div>                
                    </asp:PlaceHolder>
                <div class="uk-width-1-4">
                        Course Codes
                   </div>
        <div class="uk-width-3-4">                    
                        <asp:Label ID="lblCourseCode" runat="server" Text=""></asp:Label>
                    </div>
                <div class="uk-width-1-4">
                        Provider
                    </div>
        <div class="uk-width-3-4">
                        <asp:Label ID="lblProvider" runat="server" Text=""></asp:Label>
                  </div>
                <div class="uk-width-1-1" style="margin-top:30px; margin-bottom:30px;">
                    <a href="<%=onclick %>" class="basicButton" target="_blank">GO HERE TO REGISTER</a>                        
                   </div>          
    </div>
</asp:Content>
