<%@ Page Title="" Language="C#" MasterPageFile="~/SingleColumnMaster.master" AutoEventWireup="true" CodeFile="AddCourses.aspx.cs" Inherits="Provider_AddCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeftPane" Runat="Server">
    <div id="dvAddCourses" class="title" runat="server">
        Add Courses
    </div>
    There are multiple ways to add a course.
    <div>
        <h3><a href="/Provider/CourseDetails.aspx">Add a course</a></h3>
        <p>This is an online form ideal for adding fewer than 10 courses.</p>
    </div>
    <div>
        <a href="AddCourses.aspx">AddCourses.aspx</a>
        <h3><a href="/Provider/AddMultipleCourses.aspx"> Add multiple courses</a></h3>
        <p>This tool is ideal for conferences and and major events with more than 10 courses. LA CES provides a spreadsheet for each bulk upload.
            <a href="LACES_Multicourse_Upload.xlsx">Download Multiple Course Spreadsheet.</a> We’ve updated the LACES Multiple Course Upload Excel template. <br /><br /> Please be sure to download and use the updated template. 

        </p>
    </div>
</asp:Content>

