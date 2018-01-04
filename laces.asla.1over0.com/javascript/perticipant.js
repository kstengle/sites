// JScript File

    function checkFieldsInUI(LastName,FirstName,errorMessage)
    {
        var LastNameControl=document.getElementById(LastName);
        var FirstNameControl=document.getElementById(FirstName);
        var errorMessageControl=document.getElementById(errorMessage);
        
        //reset the error message control
        errorMessageControl.style.display = "none";
        errorMessageControl.value = "";
        
        var tempErrorMsg= "Please correct the following error(s):<ul>";
        
        //test for empty for required fields
        if(LastNameControl.value=="" ){tempErrorMsg += "<li>Last name required.</li>";}
        if(FirstNameControl.value=="" ){tempErrorMsg += "<li>First name required.</li>";}
        
        tempErrorMsg+= "</ul>";
        
        //assign the created string to the error message control's innterHTML property
        errorMessageControl.innerHTML =tempErrorMsg;
        
        if(LastNameControl.value=="" )//if last name field is empty
        {
            LastNameControl.focus();
            errorMessageControl.style.display = "block";
            return false;
        }
        if(FirstNameControl.value=="" )//if first name field is empty
        {
            FirstNameControl.focus();
            errorMessageControl.style.display = "block";
            return false;
        }
        //all things are ok so return true
        return true;
    }
    
    
    
    
        //this javascript method is used to check weather the add perticipant
        function IsAllFieldEmpty()
        {
            //check every rows and columns, if found and text then return false
            //otherwise return true
            for(var row=1;row<=20;row++)
            {
                for(var col=1;col<6;col++)
                {
                    var string="ctl00_ContentPlaceHolderLeftPane_row"+""+row+""+ col;
                    var txtControl = document.getElementById(string);
                    if(txtControl.value != "")
                    {
                        //data found so return false
                        return false;
                    }
                }
            }
            //all text box are empty, so return true
            return true;
        }
        
        //used to check a particular row
        function checkRow(rowNumber, common,tempErrorMsg)
        {
            //get textbox controls
            var lastName=document.getElementById(common+""+rowNumber+"1");
            var firstName=document.getElementById(common+""+rowNumber+"2");
            var ASLANumber=document.getElementById(common+""+rowNumber+"3");
            var CLARBNumber=document.getElementById(common+""+rowNumber+"4");
            var StateNumber=document.getElementById(common+""+rowNumber+"5");
            
            //check the condition is the row has empty first name or last name
            //but any other three field has some text, that means need to fill up the row
            if((ASLANumber.value!="" || CLARBNumber.value!="" || StateNumber.value !="" || lastName.value!="" || firstName.value!="") && (lastName.value=="" || firstName.value=="" )) 
            {
                 if(firstName.value=="" )
                 {
                    tempErrorMsg += "<li>First name required.</li>";
                    firstName.focus();
                    ///scrollTo(0,0);
                 }
                 if(lastName.value=="" )
                 {
                    tempErrorMsg += "<li>Last name required.</li>";
                    lastName.focus();
                    
                 }
                 return tempErrorMsg;
            }
            return ""; //this row is ok
        }
        
        //used to go to scroll to top of the page
        function scrollToTop()
        {
             scrollTo(0,0);
        }
        
        //used to prepare error message if any row's required field is empty 
        //but other has data
        function prepareErrorMessage()
        {
            
            
            var errorMessageControl=document.getElementById('ctl00_ContentPlaceHolderLeftPane_lblMsg');
            //errorMessageControl.innerHTML="";
            //reset the error message control
            errorMessageControl.style.display = "none";
            errorMessageControl.value = "";
            
            var tempErrorMsg= "Please correct the following error(s):<ul>";
            for(var row=1;row<=20;row++)
            {
                var retMsg=checkRow(row, "ctl00_ContentPlaceHolderLeftPane_row", tempErrorMsg);
                if(retMsg!="")
                {
                    tempErrorMsg =retMsg+ "</ul>";
                    
                    //assign the created string to the error message control's innterHTML property
                    errorMessageControl.innerHTML =tempErrorMsg;
                    errorMessageControl.style.display = "block";
                    setTimeout('scrollToTop()', 50);
                    
                    return false;
                }
            }
            return true;
        }
        
        //it is used for buttons of this page 
        function checkAllTextForButton()
        {
            if(IsAllFieldEmpty()==true)
            {
                //(document.getElementById('ctl00_ContentPlaceHolderLeftPane_lblMsg')).innerHTML="";
                return true;
            }
            return prepareErrorMessage()
        }
        
        
        //Used to check the empty field and generate the approprite message
        // msgType==1 means provider details link
        // msgType==2 means course detail link
        // msgType==3 means participant list link
        function CheckChange(msgType, perticipantID)
        {
            //create the error messages
            var commonMsg="You have made changes to the participants.\n\n";
            var hiddenControl=document.getElementById('ctl00_ContentPlaceHolderLeftPane_HiddenSaveInformation');
            
            //the this field -1 means course details page
            //-2 means provider details page
            //>0 means edit participant page and the value is the participant id
            var hiddenRedirectUrlControl=document.getElementById('ctl00_ContentPlaceHolderLeftPane_HiddenRedirectUrl');
            var formControl=document.getElementById('aspnetForm');
            hiddenControl.value="N";
             
            if(IsAllFieldEmpty()==false)
            {
                
                if(msgType==1)//provider details link
                {
                     if(confirm(commonMsg+"Are you sure you want to save them before editing the course provider details?")==true)
                     {
                         if(prepareErrorMessage()==true)
                         {
                         
                             hiddenControl.value="Y";
                             hiddenRedirectUrlControl.value="-2";
                             document.forms[0].submit();
                             return false;
                         }
                     }
                     else return true;
                }
                else if(msgType==2)//course detail link
                {
                     if(confirm(commonMsg+"Are you sure you want to save them before returning to the course details?")==true)
                     {
                         if(prepareErrorMessage()==true)
                         {
                             hiddenControl.value="Y";
                             hiddenRedirectUrlControl.value="-1";
                             document.forms[0].submit();
                              return false;
                         }
                     }
                     else return true;
                }
                else if(msgType==3)//participant list link
                {
                     if(confirm(commonMsg+"Are you sure you want to save them before editing this individual participant?")==true)
                     {
                         if(prepareErrorMessage()==true)
                         {
                             hiddenControl.value="Y";
                             hiddenRedirectUrlControl.value=perticipantID;
                             document.forms[0].submit();
                             return false;
                         }
                     }
                     else return true;
                }
                return false;
            }  
        }