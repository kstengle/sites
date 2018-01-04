<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InactiveProviderMenu.ascx.cs"
    Inherits="usercontrols_InactiveProviderMenu" %>
<!--Menu Section Starts -->
<div class="desktopmenu uk-visible-large">
    <ul>         
         <li class="menuItem menuItemFirst">
            <a href="~/Provider/Renew.aspx" runat="server">Renew</a>
        </li>        
        <li class="menuSeparator">|</li>
         <li class="menuItem">
            <a href="~/Povider/Logout.aspx" runat="server">Log out</a>
        </li>
       
    </ul>
</div>
<div id="mobilemenu" class="uk-hidden-large">                       
         <nav class="navigation">
         <ul class="nav__list">            
              <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="~/Provider/Renew.aspx" class="nav__link homelink"><span class="nav__link-name" style="text-overflow: initial;">Renew</span></a>                  
                </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="~/Provider/Logout.aspx"  class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Logout</span></a>
            </li>
        </ul>
        </nav>
</div>
<!--Menu Section Ends -->
