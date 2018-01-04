<%@ Page Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true"
    CodeFile="ProviderSearchResults.aspx.cs" Inherits="Admin_ProviderSearchResults"
    Title="Admin: Contact Search Results | LA CES™" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" runat="Server">
    <!--Start Left Content Place Holder-->
    <div class="title">
        Contact Search Results
    </div>
    <div>
        <asp:Label ID="lblMessage" CssClass="padding10" runat="server"></asp:Label>
        <asp:Label ID="uiLitResultsMessage" CssClass="padding10" runat="server"></asp:Label>
    </div>
    <div class="searchMsg">
        <%if (providers != null && providers.Count > 0)
          {
        %>
        <table width="100%" cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <!--Start Looping Searched Item-->
                <%foreach (Pantheon.ASLA.LACES.Common.Provider1 provider in providers)
                  {%>
                <tr>
                    <td valign="top" class="tdListItem">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td class="searchTableLeftTd">
                                    Organization
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Provider Name Linked to provider's detail page-->
                                    <a href="ProviderDetails.aspx?<%=LACESConstant.QueryString.PROVIDER_ID + "=" + provider.ID%>"><%=Server.HtmlEncode(provider.Organization)%></a>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="searchTableLeftTd">
                                    City
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Provider City-->
                                    <%=Server.HtmlEncode(provider.City)%>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="searchTableLeftTd">
                                    State
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Provider State-->
                                    <%=Server.HtmlEncode(provider.State)%>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="searchTableLeftTd">
                                    Status
                                </td>
                                <td class="searchTableRightTd">
                                    <!--Provider Status-->
                                    <%=(provider.Status.ToLower()=="y")? "Active" : "Inactive"%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" bgcolor="#963434" colspan="6">
                        <!--Course Item Separator-->
                        <img alt="" width="1" height="1" src="/laces/images/shim.gif" /></td>
                </tr>
                <%} %>
                <!--End Looping Searched Item-->
                <!--Start Searched Item pagination-->
                <tr>
                    <td colspan="4" class="pagingRow">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 50%" runat="server" id="tdPreviousPage">
                                </td>
                                <td align="right" style="width: 50%" runat="server" id="tdNextPage">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <!--End Searched Item pagination-->
            </tbody>
        </table>
        <%}
          else
          { %>
        <div style="color: Red">
            No results found.</div>
        <%} %>
    </div>
    <!--End Left Content Place Holder-->
</asp:Content>
