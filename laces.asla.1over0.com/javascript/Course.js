var maxValue = 0; // used for dynamic course code 
window.onload = init; // initialize calender 

/// dilimiter used to separate the course codes when those are 
//  submitted by a hidden field
var dilimiter = "<~||>"; 

/// Add Course button click event
/// Adds a dynamic course code
function AddCourse(corseCodeTable)
{ 
    
    //
    
    document.getElementById(corseCodeTable).style.display = "block";
    
    var drpCourType = document.getElementById(drpCourseTypeGlobal) // ID of Course Type drop down menu
    var selectedItem = document.getElementById(drpCourseTypeGlobal).selectedIndex;  // Course Type drop down menu's selected Index
    var txtDesc = document.getElementById(txtCourseDescGlobal).value; // Course description
    var title = txtTitleGlobal; // Course name

    
    // Adds a dynamic course code row in Course code HTML Table
    AddNewCourseCodeTypeRow(txtDesc,drpCourType.options[selectedItem].text,drpCourType.options[selectedItem].value,title)
    
    // txtHidType hidden field's value is updated to prompt user if user
    // try to navigate other page with unsave data
    document.getElementById(txtCourseDescGlobal).value = "";    
	//var txtHidType = document.getElementById(txtHidTypeGlobal); // txtHidType used for Prompting user for unsaved Course Code when navigating
	var hidCourseTypes = document.getElementById(hidCourseTypesGlobal); 	
	var hidCourseDesc = document.getElementById(hidCourseDescGlobal);
	getFinalValue(hidCourseTypesGlobal,hidCourseDescGlobal);	
	//txtHidType.value = hidCourseTypes.value + hidCourseDesc.value;    	
	needToConfirm = false; setTimeout('resetFlag()', 750);    
}

// Used to remove Course code from Dynamic HTML Table
function removeCourseCodeType(obj,type,desc,ctrl)
{   
    var ctrl = document.getElementById(ctrl) 
	var rw = obj.parentNode.parentNode.parentNode;           
        
    if(trim(ctrl.value) != "") // Prompt message when Course Name text box is not blank
    {
        msg = "Do you wish to remove the code " + type + "" +  desc + " from " + trim(ctrl.value) + "?";	
    }
    else // Prompt message when Course Name text box is blank
    {
        msg = "Do you wish to remove the code " + type + "" +  desc + " from this course?";	
    }
	
	
	var y = confirm(msg) // confirm before deleting the course code
	if(y)
	{
	    rw.parentNode.removeChild(rw)
	    //ShowHideCorseCodeTypeDiv();	
	}
	
    // txtHidType hidden field's value is updated to prompt user if user
    // try to navigate other page with unsave data	
	//var txtHidType = document.getElementById(txtHidTypeGlobal); // txtHidType used for Prompting user for unsaved Course Code when navigating
	var hidCourseTypes = document.getElementById(hidCourseTypesGlobal); 	
	var hidCourseDesc = document.getElementById(hidCourseDescGlobal); 	
	getFinalValue(hidCourseTypesGlobal,hidCourseDescGlobal);	
	//txtHidType.value = hidCourseTypes.value + hidCourseDesc.value;    	
	needToConfirm = false; setTimeout('resetFlag()', 750);	
	
	
	
	
}

// If user try to navigate to other pages by clicking any link
// system will prompt user if there any unsave data
function confirmonCloseLink(message, page) 
{     
    
    var btnSave = document.getElementById(btnSaveGlobal); 	
	for (var i = 0; i < monitorChangesValues.length; i++) 
	{  
	    var hidAction = document.getElementById("hidAction");
		var elem = document.getElementById(monitorChangesIDs[i]);    
		if (elem) 
			if (((elem.type == 'checkbox' || elem.type == 'radio') && elem.checked != monitorChangesValues[i]) || (elem.type != 'checkbox' && elem.type != 'radio' && elem.type!='select-multiple' && changeCrt(pk_fixnewlines_textarea(elem.value)) != revertSpecialCharacter(monitorChangesValues[i])) || (elem.type=='select-multiple' && monitorChangesValues[i]!=getMutipleSelectionDropdownValue(monitorChangesIDs[i]))) 
			{ 
				var yesorno = confirm(message); 
				needToConfirm = false;
				if(!yesorno)
				{
				    return true;
				}
				else
				{				    
				    hidAction.value = page;
				    btnSave.click();				    
				    return false;	    
				}
			} 
	} 
}


function redirectPage(page)
{
}


// Press Enter Key on Add Code Text box
function AddCodeOnEnterKey(ev)
{
     var key;          
     if(window.event)
          key = ev.keyCode; //IE
     else
          key = ev.which; //firefox     

     if(key == 13)
     {
        AddCourse();
        return false;
     }            
}

//show hide the course code type div
function ShowHideCorseCodeTypeDiv()
{
    var corseCodeTableControl = document.getElementById('corseCodeTable'); 	
    if(maxValue>=0) corseCodeTableControl.style.display = "block";
    else corseCodeTableControl.style.display = "none";
}


// Add new course code to Dynamic HTML Table
function AddNewCourseCodeTypeRow(txtDesc,courseText,courseValue,ctrl)
{
    ShowHideCorseCodeTypeDiv();
   
    // replaces every occurance of < > (angular bracket)
    while(txtDesc.indexOf("<") >= 0)
    {
        txtDesc = txtDesc.replace("<","&lt;");
    }     
    while(txtDesc.indexOf(">") >= 0)
    {
        txtDesc = txtDesc.replace(">","&gt;");
    }     
    
    
    // Create Dynamic HTML Row and Cell by DOM	
	var tdCourseType = document.getElementById("tdCourseType");
	var tr = document.createElement("tr");
	var td1 = document.createElement("td");	
	var td2 = document.createElement("td");	
	var td3 = document.createElement("td");			
	var td4 = document.createElement("td");

	td1.innerHTML = courseText + ":";
	td1.align= "right";
	//td1.className= "top";
	td2.id = "cDesc" + maxValue;
	td2.innerHTML = txtDesc;	
	td2.align= "left";
	
	var labelbr = document.createElement("label");
	labelbr.innerHTML="&nbsp;<a onclick='removeCourseCodeType(this,\""+replaceCharacter(td1.innerHTML)+"\",\""+replaceCharacter(td2.innerHTML).replace("\\","\\\\")   +"\",\""+ctrl+"\")' href='javascript:void(0)'>remove</a>";
	td3.appendChild(labelbr);	
    td3.className = "participantList top";	    
	td4.id = "cType" + maxValue;
	td4.innerHTML = courseValue;
	td4.style.display = "none";


	tr.appendChild(td1);
	tr.appendChild(td2);
	tr.appendChild(td3);
	tr.appendChild(td4);
	tdCourseType.appendChild(tr);	
	
	maxValue++; // increament the total number of rows 
}

// The hidden fields are updated with the dynamic Course code to 
//  passed the informations to server side for saving the information
function getFinalValue(objCourseTypes,objCourseDesc)
{
    
    //ShowHideCorseCodeTypeDiv(0);
    
    var hidCourseTypes = document.getElementById(objCourseTypes);
    var hidCourseDesc = document.getElementById(objCourseDesc);        
    
    hidCourseTypes.value = "";
    hidCourseDesc.value = "";
    
	for(var i=0; i<=maxValue; i++)
	{
		var DescId = "cDesc" + i;
		var TypeId = "cType" + i;
		try 
		{	
		    //alert(hidCourseTypes.value)
		    // Error is traped because the course code may be removed by clicking the remove button		
		    if(hidCourseTypes.value == "")
		    {
			    hidCourseTypes.value = document.getElementById(TypeId).innerHTML;
			    hidCourseDesc.value = document.getElementById(DescId).innerHTML;		    
		    }		    
		    else
		    {
			    hidCourseTypes.value = hidCourseTypes.value + dilimiter + document.getElementById(TypeId).innerHTML;
			    hidCourseDesc.value = hidCourseDesc.value + dilimiter + document.getElementById(DescId).innerHTML;
			}
		}
		catch(e){}
	}		
}

// replaces special character 
// replaces '(apostrophe) with &#39;
// replaces "(double quotation mark) with &#39;&#39;
function replaceCharacter(str)
{    
    // replaces every occurance of '(apostrophe)
    while(str.indexOf("'") >= 0)
    {
        str = str.replace("'","&#39;");
    }
    
    // replaces every occurance of "(double quotation mark)
    while(str.indexOf('"') >= 0)
    {
        str = str.replace('"','&#39;&#39;');
    }         
    return str;        
}


/// This method calculate number of characters for the textarea 
function textCounter(ctrl, displayctrl,maxchar)
{
    var ctrl = document.getElementById(ctrl)
    var displayctrl = document.getElementById(displayctrl);
    var contVal = pk_fixnewlines_textarea(ctrl.value)    
    var currentlength = contVal.length;
    var lengthremains = maxchar - currentlength;
    
    if(currentlength > maxchar )
    {
        ctrl.value = contVal.substring(0,maxchar)
    }
    if(lengthremains < 0)
    {
        lengthremains = 0;
    }
    displayctrl.innerHTML = lengthremains + "&nbsp;&nbsp;  character(s) remains.";
}

///Event fire when check or uncheck distance education checkbox. 
///If checked then disable city and state control otherwise enable controls.
function checkDistanceEducation(ctrlDEducation, ctrlCity, ctrlState)
{
    //If found checked
    if(ctrlDEducation.checked)
    {
        ctrlCity.disabled = "disabled";
        ctrlState.disabled = "disabled";
    }
    else
    {
        ctrlCity.disabled = false;
        ctrlState.disabled = false;
    }
}