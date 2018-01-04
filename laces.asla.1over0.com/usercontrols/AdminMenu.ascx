<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenu.ascx.cs" Inherits="usercontrols_AdminMenu" %>
<!--Start Menu Items-->
<div class="desktopmenu uk-visible-large">
    <ul>         
         <li class="menuItem menuItemFirst">
            <a href="~/Admin/FindCourses.aspx?status=NP" runat="server">Pending Courses</a>
        </li>        
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="~/Admin/FindCourses.aspx" runat="server">Find Courses</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="~/Admin/FindProviders.aspx" runat="server">Find Providers</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="~/Admin/ApprovedProviderSearchResults.aspx?Status=Pending" runat="server">Pending Providers</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="~/Admin/EditSearchedAttendees.aspx" runat="server">Update Attendees</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="~/Admin/ManageAttendees.aspx" runat="server">Upload Attendees</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="~/Admin/Reports.aspx" runat="server">Reports</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="~/Admin/Logout.aspx" runat="server">Logout</a>
        </li>
        </ul>
 </div>    

<div id="mobilemenu" class="uk-hidden-large">                       
         <nav class="navigation">
         <ul class="nav__list">            
              <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="~/Admin/FindCourses.aspx?status=NP" class="nav__link homelink"><span class="nav__link-name" style="text-overflow: initial;">Pending Courses</span></a>                  
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="~/Admin/FindCourses.aspx"  class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Find Courses</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="~/Admin/FindProviders.aspx"  class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Find Providers</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="~/Admin/ApprovedProviderSearchResults.aspx?Status=Pending"  class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Pending Providers</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="~/Admin/EditSearchedAttendees.aspx"  class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Update Attendees</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="~/Admin/ManageAttendees.aspx"  class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Upload Attendees</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="~/Admin/Reports.aspx"  class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Reports</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="~/Admin/Logout.aspx"  class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Logout</span></a>
            </li>             
        </ul>

         </nav>
        </div>


<!--Menu Section Ends -->
