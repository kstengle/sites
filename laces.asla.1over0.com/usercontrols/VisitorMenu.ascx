<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VisitorMenu.ascx.cs" Inherits="usercontrols_VisitorMenu" %>
<!--Menu Section Starts -->
<div class="desktopmenu uk-visible-large">
<ul>
    <li class="menuItem menuItemFirst">
        <a href="~/AboutUs.aspx" runat="server">About Us</a>
    </li>
    <li class="menuSeparator">|</li>
    <li class="menuItem">
        <a id="A2" href="~/ApprovedProviderGuidelines.aspx" runat="server">Guidelines</a>
    </li>
</ul>
</div>
<div id="mobilemenu" class="uk-hidden-large">                       
         <nav class="navigation">
         <ul class="nav__list">
              <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                    <a href="/default.aspx" class="nav__link homelink"><span class="nav__link-name" style="text-overflow: initial;">Home</span></a>
                </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                    <a href="~/AboutUs.aspx" class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">About Us</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                    <a href="~/ApprovedProviderGuidelines.aspx" class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Guidelines</span></a>
            </li>            
        </ul>

         </nav>
        </div>
<!--Menu Section Ends -->
