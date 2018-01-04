// JScript File

        //this javascript method is used to check the event
        function CheckEvent(evt)
        {
           
           //get key code
           var keyCode=GetKeyCodeofEvent(evt);
           
           //check weather it is enter
           if( keyCode  == 13)
           {
                ResetErrorMsg();
                scrollTo(0,0);
           }
            return true; 
        }
        document.onkeypress= CheckEvent;
        
        //used to reset the lblMsg server control
        function ResetErrorMsg()
        {
             var title = document.getElementById('ctl00_ContentPlaceHolderLeftPane_lblMsg');
             title.style.display = "none";
        }