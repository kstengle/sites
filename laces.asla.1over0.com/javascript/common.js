// Removes starting and ending whitespaces
function trim(str)
{
    return LTrim(RTrim(str));
}

// Removes starting whitespaces
function LTrim( value ) {
	
	var re = /\s*((\S+\s*)*)/;
	return value.replace(re, "$1");
	
}

//To scroll the browser
function scrollToTop()
{
   scrollTo(0,0);
}

// Removes ending whitespaces
function RTrim( value ) {
	
	var re = /((\s*\S+)*)\s*/;
	return value.replace(re, "$1");
	
}

// This function returns int from 2 length String
// example - returns '1' from '01'
function checkValue(val)
{
	var value = val.toString();
	if(value.charAt(0) == "0")
	{
		return value.charAt(1);
	}
	return value;
}


// This function returns the days of a perticular month and year
// input parameter Month and Year
function getMonthDate(month,year)
{
    if(month==2)
    {
	    if((year%400)==0)
	    {
		    return 29;
	    }
	    else if((year%100)==0)
	    {
		    return 28;
	    }
	    else if((year%4)==0)
	    {
		    return 29;
	    }
	    else
	    {
		    return 28;
	    }
    }
    else if(month==4 || month==6 || month==9 || month==11)
    {
	    return 30;
    }
    else
    {
	    return 31;
    }
}


// Validate http
function httpValidation(source, arguments)
{
    arguments.IsValid = defaultHttpValidation(arguments.Value);        
}

// Validate default http
function defaultHttpValidation(value)
{
    if(value == "http://")
    {
        return false;
    }
    return true;    
}


// Validate Date
function DateValidation(source, arguments)
{
    arguments.IsValid = validateDate(arguments.Value);        
}

// This function validate Date by regular expression
// returns true on valid date and returns false for invalid date
function validateDate(txtVal)
{
	txtVal = trim(txtVal); // trim the field 
    var dt1 = new Array();
    var reDt1 = /^\d{1,2}\/\d{1,2}\/\d{4}$/; // date regular expression
    var b = reDt1.test(txtVal)  // validate date field with regular expression

    if(b)
    {
        dt1 = txtVal.split("/")
		if(dt1.length !=3)
		{
			return false;	
		}
		
		for(var i=0; i<2; i++)
		{
			dt1[i] = checkValue(dt1[i])	 
		}
		
	    var endDt = getMonthDate(dt1[0],dt1[2]) // Get number of the days of the year and month
	    if(parseInt(dt1[0]) > 12 || parseInt(dt1[0]) <1 || parseInt(dt1[1]) >endDt || parseInt(dt1[1]) <1 || parseInt(dt1[2]) <1000 || parseInt(dt1[2]) >9999)
	    {
		    return false;
	    }

	    return 	true;
    }
    else
    {
	    return false;
    }
}


///Return KeyCode of the Event
function GetKeyCodeofEvent(evt)
{   
    var keyCode="";
         if(window.event)
            { 
                keyCode = window.event.keyCode; 
                evt = window.event; 
            }
        else if (evt)keyCode = evt.which;   
        return  keyCode;

}

/* confirm admin code type delete message */
function confirmCodeTypeDelete()
{
    var del = confirm("Are you sure to delete this code type?");
    return del;
}  


// revert \n.\r,\,' these 4 characters for prompt user 
// for unsave data before navigate to other page
function revertSpecialCharacter(str)
{   
    while(str.indexOf("&#39;") >= 0)
    {
        str = str.replace("&#39;","'");
    }
    while(str.indexOf("&#47;") >= 0)
    {
        str = str.replace("&#47;","\\");
    }  
    while(str.indexOf("~!@=|") >= 0)
    {
        str = str.replace("~!@=|","/");
    }      
    
     return str;
}

// change \n.\r for prompt user 
// for unsave data before navigate to other page
function changeCrt(str)
{   
    while(str.indexOf("\r") >= 0)
    {
        str = str.replace("\r","&#b");
    }      
    while(str.indexOf("\n") >= 0)
    {
        
        str = str.replace("\n","&|b");
    }          
     return str;
}

// This method is handle \n \r in textarea  for different browsers
function pk_fixnewlines_textarea(val) {             
  // Adjust newlines so can do correct character counting for MySQL. MySQL counts a newline as 2 characters.
  if (val.indexOf('\r\n')!=-1)
    ; // this is IE on windows. Puts both characters for a newline, just what MySQL does. No need to alter
  else if (val.indexOf('\r')!=-1)
    val = val.replace ( /\r/g, "\r\n" );        // this is IE on a Mac. Need to add the line feed
  else if (val.indexOf('\n')!=-1)
    val = val.replace ( /\n/g, "\r\n" );        // this is Firefox on any platform. Need to add carriage return
  else 
    ;                                           // no newlines in the textarea  
  return val;
}



// This function returns values for Mutiple Selection Dropdown menu
function getMutipleSelectionDropdownValue(crtl)
{
	var crtl = document.getElementById(crtl);
	
	retValue = "";
	for(var i=0; i< crtl.options.length; i++)
	{
		if(crtl.options[i].selected)
		{
			if(retValue == "")
			{
				retValue = crtl.options[i].value;
			}
			else
			{
				retValue = retValue + ":" + crtl.options[i].value;				
			}
		}
	}
	
	return retValue;
}

///Checking the valid Email address through regular expression. If valid return true else return false.
function IsValidEmail(FieldName)
{
 var RegXEmailAddress=/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
 if(document.getElementById(FieldName).value.match(RegXEmailAddress))
 {
    return true;
 }
 else
    return false;
}

///Remove the default text from text box on onclick event
function RemoveDefaultText(ctrl, defaultText)
{
    if(ctrl.value == defaultText)
        ctrl.value = "";
}

///Get key code
function GetKeyCode(evt)
{
   var keyCode="";
   if(window.event)
   { 
        keyCode = window.event.keyCode; 
        evt = window.event; 
   }
   else if (evt)keyCode = evt.which;
   return keyCode; 
}


//Check is the supplied date format is ok or not
 //the strDate is seperated by '/' character
 function isDateValid(strDate)
 {
    try
    {
        var parsedDate = strDate.split ("/");
        if (parsedDate.length != 3) return false;
        var day, month, year;
        month = parsedDate[0]-1;
        day = parsedDate[1];
        year = parsedDate[2];
        

        var objDate = new Date (strDate);
        var validDate = new Date('1/1/1753');
        
        if(objDate <= validDate ) return false;

        if (month != objDate.getMonth()) return false;
        if (day != objDate.getDate()) return false;
        if (year != objDate.getFullYear()) return false;

        return true;
    }
    catch(err)
    {
        return false;
    }
 }
 

//Purpose: return true if the supplied date is greater then the current date
// date are in mm/dd/yyyy formats
// Author: Alamgir Hossain
// Creation Date: 04/16/2008
function ISFutureDate(control)
{
    if(isDateValid(document.getElementById(control).value)==false) return true;
    
    arr=document.getElementById(control).value.split("/");
    
    if(arr.length != 3 ) return true;
    day = parseInt(arr[1]);
    month= parseInt(arr[0])-1;
    year= parseInt(arr[2]);
    
    var myDate=new Date();
    myDate.setFullYear(year,month,day);

    var today = new Date();
    if (myDate > today)
    {
        return true;
    }
    return false;
}

//check whether the stDate is small or equall to endDate
// return false if stDate is greater than endDate
function CompareDate(stDate, endDate)
{
    var objStDate = new Date (stDate);
    var objEndDate= new Date (endDate);
    if(objStDate>objEndDate) return false;
    return true;
}
function CompareDateWithTwoYears(stDate, endDate) {
    var objStDate = new Date(stDate);
    var objEndDate = new Date(endDate);
    objStDate.setFullYear(objStDate.getFullYear() + 2);
    if (objEndDate > objStDate) return false;
    return true;
}