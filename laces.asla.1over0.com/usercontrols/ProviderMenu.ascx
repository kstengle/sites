<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProviderMenu.ascx.cs"
    Inherits="usercontrols_ProviderMenu" %>

<div class="desktopmenu uk-visible-large">
    <ul>         
         <li class="menuItem menuItemFirst">
            <a href="/Provider/Tools.aspx" runat="server">Tools</a>
        </li>        
        <li class="menuSeparator">|</li>
         <li class="menuItem">
            <a href="/Provider/AddCourses.aspx" runat="server">Add Courses</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a id="A1" href="/Provider/CourseList.aspx?status=NP" runat="server">Pending Courses</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="/Provider/CourseList.aspx?status=OP" runat="server">Approved Courses</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="/Provider/CourseList.aspx" runat="server">All Courses</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="/Provider/ManageAttendees.aspx" runat="server">Attendees</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="/Provider/ContactDetails.aspx" runat="server">Contact Info</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="/Provider/Renew.aspx" runat="server">Renew</a>
        </li>
        <li class="menuSeparator">|</li>
        <li class="menuItem">
            <a href="/Provider/Logout.aspx" runat="server">Logout</a>
        </li>
    </ul>
</div>
<div id="mobilemenu" class="uk-hidden-large">                       
         <nav class="navigation">
         <ul class="nav__list">            
              <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="/Provider/Tools.aspx" class="nav__link homelink"><span class="nav__link-name" style="text-overflow: initial;">Tools</span></a>                  
                </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="/Provider/AddCourses.aspx"  class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Add Courses</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="/Provider/CourseList.aspx?status=NP" class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Pending Courses</span></a>
            </li> 
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="/Provider/CourseList.aspx?status=OP" class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Approved Courses</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="/Provider/CourseList.aspx" class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">All Courses</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="/Provider/ManageAttendees.aspx" class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Attendees</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="/Provider/ContactDetails.aspx" class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Contact Info</span></a>
            </li>
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="/Provider/Renew.aspx" class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Renew</span></a>
            </li>            
             <li class="nav__item nav__item--primary nav__item--no-drop nav__item--mobile">
                  <a href="/Provider/Logout.aspx" class="nav__link"><span class="nav__link-name" style="text-overflow: initial;">Logout</span></a>
            </li>            
        </ul>

         </nav>
        </div>
<!--Menu Section Ends -->
