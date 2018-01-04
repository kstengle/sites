<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="ApprovedProviderDetails.aspx.cs" Inherits="Visitor_ApprovedProviderDetails"
    Title="Provider Details | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <div class="title"  id="PageHeader" runat="server">
        Provider Details
    </div>

    <div class="uk-grid">
        <asp:PlaceHolder ID="uiPhOrganizationStreetAddress" runat="server">
            <div class="uk-width-1-4">
                    Street Address
            </div>
            <div class="uk-width-3-4">
                    <asp:Literal ID="pvOrganizationStreetAddress" runat="server" Mode="Encode"></asp:Literal>
           </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="uiPhOrganizationCity" runat="server">
            <div class="uk-width-1-4">
                    City
             </div>
                <div class="uk-width-3-4">
                    <asp:Literal ID="pvOrganizationCity" runat="server" Mode="Encode"></asp:Literal>
                </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="uiPhOrganizationState" runat="server">    
            <div class="uk-width-1-4">
                    State/Province
            </div>
             <div class="uk-width-3-4">
                    <asp:Literal ID="pvOrganizationState" runat="server" Mode="Encode"></asp:Literal>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="uiPhOrganizationZip" runat="server">    
            <div class="uk-width-1-4">
                    Zip
              </div>
                <div class="uk-width-3-4">
                    <asp:Literal ID="pvOrganizationZip" runat="server" Mode="Encode"></asp:Literal>
                </div>            
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="uiPhOrganizationCountry" runat="server">    
            <div class="uk-width-1-4">
                    Country
            </div>
                <div class="uk-width-3-4">
                    <asp:Literal ID="pvOrganizationCountry" runat="server" Mode="Encode"></asp:Literal>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="uiPhOrganizationPhone" runat="server">    
            <div class="uk-width-1-4">
                    Phone
              </div>
              <div class="uk-width-3-4">
                    <asp:Literal ID="pvOrganizationPhone" runat="server" Mode="Encode"></asp:Literal>
                </div>
            </asp:PlaceHolder>
         <asp:PlaceHolder ID="uiPhOrganizationFax" runat="server">    
            <div class="uk-width-1-4">
                    Fax
               </div>
                <div class="uk-width-3-4">
                    <asp:Literal ID="pvOrganizationFax" runat="server" Mode="Encode"></asp:Literal>
                </div>
            </asp:PlaceHolder>
         <asp:PlaceHolder ID="uiPhOrganizationWebSite" runat="server">    
            <div class="uk-width-1-4">
                    Website
                </div>
                <div class="uk-width-3-4">
                    <asp:Literal ID="pvOrganizationWebSite" runat="server" Mode="Encode"></asp:Literal>
                </div>
         </asp:PlaceHolder>
      </div> 
              <div style="color: Red">
            <asp:Label ID="uilblNoProviderfound" runat="server"></asp:Label>

              </div>            
</asp:Content>
