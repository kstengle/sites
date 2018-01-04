// JScript File
// initialize calender
window.onload = init; 

// Call from custom validator control to validate check boxes
function validateSearchScope(source, arguments)
{
    arguments.IsValid = checkBoxesValidation(arguments.Value);
} 

// Validate Checkboxes
// If found none of checked return false
function checkBoxesValidation(value)
{    
    if(document.getElementById(chkTitleGlobal).checked)
        return true;
    else if(document.getElementById(chkDescriptionGlobal).checked)
        return true;
    else if(document.getElementById(chkLearningOutcomesGlobal).checked)
        return true;
    document.getElementById(chkTitleGlobal).focus(); 
    return false;   
}

//Select first item from location list when checked distance education check box
function initializeLocation(ctrl)
{
    if(ctrl.checked)
        document.getElementById(ddlLocationGlobal).selectedIndex = 0;
}

//Uncheck distance education checkbox when select a state
function uncheckDistanceEducation(ctrl)
{
    if(ctrl.selectedIndex > 0)
        document.getElementById(chkDistanceEduGlobal).checked = false;
}

