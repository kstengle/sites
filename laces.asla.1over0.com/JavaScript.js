//this is required by a contol
//please put it in root folder and don't remove it
function CheckValidatorEvaluateIsValid(val) {
    var control = document.getElementById(val.controltovalidate);
    var warnif = val.warnif == 1 ? true : false;
    return control.checked ^ warnif;
}