// JScript File

// This method calculate number of characters for the textarea 
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

