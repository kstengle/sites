//	Initialy written by Tan Ling Wee on 2 Dec 2001
//	Modified by Tanvir gaus 31 Aug 2006
//	email :	tanvir.gaus@gmail.com
var	fixedX = -1 			// x position (-1 if to appear below control)
var	fixedY = -1			// y position (-1 if to appear below control)
var startAt = 1			// 0 - sunday ; 1 - monday
var showWeekNumber = 0	// 0 - don't show; 1 - show
var showToday = 1		// 0 - don't show; 1 - show
var imgDir = "../smartCalendar/"			// directory for images ... e.g. var imgDir="/img/"

var gotoString = "Go To Current Month"
var todayString = "<span class='calToday'>Today is</span>"
var weekString = "Wk"
var scrollLeftMessage = "Click to scroll to previous month. Hold mouse button to scroll automatically."
var scrollRightMessage = "Click to scroll to next month. Hold mouse button to scroll automatically."
var selectMonthMessage = "Click to select a month."
var selectYearMessage = "Click to select a year."
var selectDateMessage = "Select [date] as date." // do not replace [date], it will be replaced by date.

var	crossobj, crossMonthObj, crossYearObj, monthSelected, yearSelected, dateSelected, omonthSelected, oyearSelected, odateSelected, monthConstructed, yearConstructed, intervalID1, intervalID2, timeoutID1, timeoutID2, ctlToPlaceValue, ctlNow, dateFormat, nStartingYear

var	bPageLoaded=false
var	ie=document.all
var	dom=document.getElementById

var	ns4=document.layers
var	today =	new	Date()
var	dateNow	 = today.getDate()
var	monthNow = today.getMonth()
var	yearNow	 = today.getYear()
if (navigator.userAgent.toLowerCase().indexOf('msie 9') != -1 || navigator.userAgent.toLowerCase().indexOf('msie 10') > 0)
{
  yearNow = today.getFullYear();
}
var	imgsrc = new Array("drop1.gif","drop2.gif","left1.gif","left2.gif","right1.gif","right2.gif")
var	img	= new Array()

var bShow = false;

/* hides <select> and <applet> objects (for IE only) */
function hideElement( elmID, overDiv )
{
	if( ie )
	{
		for( i = 0; i < document.all.tags( elmID ).length; i++ )
		{
			obj = document.all.tags( elmID )[i];
			if( !obj || !obj.offsetParent )
			{
				continue;
			}

			// Find the element's offsetTop and offsetLeft relative to the BODY tag.
			objLeft   = obj.offsetLeft;
			objTop    = obj.offsetTop;
			objParent = obj.offsetParent;

			while( objParent.tagName.toUpperCase() != "BODY" )
			{
				objLeft  += objParent.offsetLeft;
				objTop   += objParent.offsetTop;
				objParent = objParent.offsetParent;
			}

			objHeight = obj.offsetHeight;
			objWidth = obj.offsetWidth;

			if(( overDiv.offsetLeft + overDiv.offsetWidth ) <= objLeft );
			else if(( overDiv.offsetTop + overDiv.offsetHeight ) <= objTop );
			else if( overDiv.offsetTop >= ( objTop + objHeight ));
			else if( overDiv.offsetLeft >= ( objLeft + objWidth ));
			else
			{
				obj.style.visibility = "hidden";
			}
		}
	}
}

/*
* unhides <select> and <applet> objects (for IE only)
*/
function showElement( elmID )
{
	if( ie )
	{
		for( i = 0; i < document.all.tags( elmID ).length; i++ )
		{
			obj = document.all.tags( elmID )[i];

			if( !obj || !obj.offsetParent )
			{
				continue;
			}

			obj.style.visibility = "";
		}
	}
}

function HolidayRec (d, m, y, desc)
{
	this.d = d
	this.m = m
	this.y = y
	this.desc = desc
}

var HolidaysCounter = 0
var Holidays = new Array()

function addHoliday (d, m, y, desc)
{
	Holidays[HolidaysCounter++] = new HolidayRec ( d, m, y, desc )
}

if (dom)
{
	for	(i=0;i<imgsrc.length;i++)
	{
		img[i] = new Image
		img[i].src = imgDir + imgsrc[i]
	}
	//document.write ("<div onclick='bShow=true'  id='smart_calendar' onmouseover=\"document.all.close.src='"+imgDir+"close.gif'\" onmouseup=\"document.all.close.src='"+imgDir+"close.gif'\"	style='z-index:+999;position:absolute;visibility:hidden;'><table cellpadding=0 cellspacing=0 width="+((showWeekNumber==1)?250:200)+" style='font-family: Tahoma;font-size:10px;border-width:1px;border-style:solid;border-color: #666666;font-family: Tahoma; font-size:11px}' bgcolor='#ffffff'><tr bgcolor='#D4D0C8'><td><table cellpadding=2 cellspacing=0 width='"+((showWeekNumber==1)?248:218)+"' ><tr><td nowrap style='padding:2px;font-family: Tahoma; font-size:10px;' align=right><font color='#000000'><B><span id='smart_caption'></span></B></font></td><td nowrap align=right><a href='javascript:hideCalendar()' onmousedown=\"document.all.close.src='"+imgDir+"close2.gif'\" onmouseover='popDownYear(); popDownMonth();'><IMG id=close SRC='"+imgDir+"close.gif' WIDTH='16' HEIGHT='14' BORDER='0' ALT='Close the Calendar' align='absmiddle'></a>&nbsp;</td></tr></table></td></tr><tr><td style='padding:5px' bgcolor=#ffffff><span id='smart_content'></span></td></tr>")
	document.write ("<div onclick='bShow=true'  id='smart_calendar' style='z-index:+999;position:absolute; visibility:hidden;'><table cellpadding=0 cellspacing=0 border=0 width=190 style='font-family:tahoma;font-size: 9px;border-width:1px;border-style:solid;border-color: #666666;font-family: Tahoma; font-size:9px}' bgcolor='#ffffff'><tr class='calHeader'><td><table cellpadding=2 cellspacing=0 width='195' ><tr><td nowrap style='padding:2px;font-family: Tahoma; font-size:9px;' align=right><font color='#000000'><B><span id='smart_caption'></span></B></font></td></tr></table></td></tr><tr><td style='padding:0px' bgcolor=#ffffff><span id='smart_content'></span></td></tr>")

	if (showToday==1)
	{
		document.write ("<tr bgcolor=#666666><td style='padding:5px' align=center><span id='smart_lblToday'></span></td></tr>")
	}

	document.write ("</table></div><div id='smart_selectMonth' onmouseover=\"getCalObject('smart_spanMonth').style.borderColor='#666666';\" style='z-index:+999;position:absolute;visibility:hidden;'></div><div id='smart_selectYear' onmouseover=\"getCalObject('smart_spanYear').style.borderColor='#666666';\" style='z-index:+999;position:absolute;visibility:hidden;'></div>");
}

var	monthName =	new	Array("January","February","March","April","May","June","July","August","September","October","November","December")
if (startAt==0)
{
	dayName = new Array	("Sun","Mon","Tue","Wed","Thu","Fri","Sat")
}
else
{
	dayName = new Array	("Mon","Tue","Wed","Thu","Fri","Sat","Sun")
}
var	styleAnchor="text-decoration:none; color:black;"
var	styleLightBorder="border-style:solid; border-width:1px; border-color:#666666;"

function swapImage(srcImg, destImg){
	if (ie)	{ getCalObject(srcImg).setAttribute("src",imgDir + destImg) }
}

function init()	{
	if (!ns4)
	{
		if (!ie) { yearNow += 1900	}
		//----------------
		// BLOCK - Changing FOR IE FF issue
		//----------------
		//crossobj=(dom)?getCalObject("smart_calendar").style : ie? document.all.smart_calendar : document.smart_calendar
		

		crossobj=getCalObject("smart_calendar").style
		
		hideCalendar()

		//crossMonthObj=(dom)?getCalObject("smart_selectMonth").style : ie? document.all.smart_selectMonth	: document.smart_selectMonth
		crossMonthObj=getCalObject("smart_selectMonth").style

		//crossYearObj=(dom)?getCalObject("smart_selectYear").style : ie? document.all.smart_selectYear : document.smart_selectYear
		crossYearObj=getCalObject("smart_selectYear").style
		
		//----------------
		// BLOCK - Ends
		//----------------
		
		monthConstructed=false;
		yearConstructed=false;

		if (showToday==1)
		{
			getCalObject("smart_lblToday").innerHTML =	todayString + " <a onmousemove='window.status=\""+gotoString+"\"' onmouseout='window.status=\"\"' title='"+gotoString+"' class='calToday' href='javascript:monthSelected=monthNow;yearSelected=yearNow;constructCalendar();'>"+dayName[(today.getDay()-startAt==-1)?6:(today.getDay()-startAt)]+", " + dateNow + " " + monthName[monthNow].substring(0,3)	+ "	" +	yearNow	+ "</a>"
		}
		
		sHTML1="<table cellpadding='2' cellspacing='0' border='0' width='100%' id='smart_mainCalTbl'><tr>"
		sHTML1+="<td nowrap align='right' width='90%'>&nbsp;<span id='smart_spanMonth' style='border-style:solid;border-width:1px;border-color:#D4D0C8;cursor:pointer'	onmouseover='swapImage(\"changeMonth\",\"drop2.gif\");this.style.borderColor=\"#666666\";window.status=\""+selectMonthMessage+"\"; popDownYear();' onmouseout='swapImage(\"changeMonth\",\"drop1.gif\");this.style.borderColor=\"#D4D0C8\";window.status=\"\"' onclick='popUpMonth()'></span> "
		sHTML1+="&nbsp; <span id='smart_spanYear' style='border-style:solid;border-width:1px;border-color:#D4D0C8;cursor:pointer' onmouseover='swapImage(\"changeYear\",\"drop2.gif\");this.style.borderColor=\"#666666\";window.status=\""+selectYearMessage+"\"; popDownMonth();' onmouseout='swapImage(\"changeYear\",\"drop1.gif\");this.style.borderColor=\"#D4D0C8\";window.status=\"\"'	onclick='popUpYear()'></span>&nbsp;</td>"
		sHTML1+="<td nowrap width='10%' align='right'><span id='smart_spanLeft' style='border-style:solid;border-width:1px;border-color:#D4D0C8;cursor:pointer' onmouseover='swapImage(\"smart_changeLeft\",\"left2.gif\");this.style.borderColor=\"#666666\";window.status=\""+scrollLeftMessage+"\"; popDownYear(); popDownMonth();' onclick='javascript:decMonth()' onmouseout='clearInterval(intervalID1);swapImage(\"smart_changeLeft\",\"left1.gif\");this.style.borderColor=\"#D4D0C8\";window.status=\"\"' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartDecMonth()\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>&nbsp<IMG id='smart_changeLeft' SRC='"+imgDir+"left1.gif' width=10 height=11 align=middle BORDER=0 style='margin-top:-9px;_margin-top:0px;'></span>&nbsp;"
		sHTML1+="<span id='smart_spanRight' style='border-style:solid;border-width:1px;border-color:#D4D0C8;cursor:pointer'	onmouseover='swapImage(\"smart_changeRight\",\"right2.gif\");this.style.borderColor=\"#666666\";window.status=\""+scrollRightMessage+"\"; popDownYear(); popDownMonth();' onmouseout='clearInterval(intervalID1);swapImage(\"smart_changeRight\",\"right1.gif\");this.style.borderColor=\"#D4D0C8\";window.status=\"\"' onclick='incMonth()' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartIncMonth()\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>&nbsp<IMG id='smart_changeRight' SRC='"+imgDir+"right1.gif'	width=10 height=11  align=middle BORDER=0 style='margin-top:-9px;_margin-top:0px;'>&nbsp;</span></td>"
		sHTML1+="</tr></table>"
		/*
		sHTML1="&nbsp;<span id='smart_spanLeft' style='border-style:solid;border-width:1px;border-color:#D4D0C8;cursor:pointer' onmouseover='swapImage(\"changeLeft\",\"left2.gif\");this.style.borderColor=\"#666666\";window.status=\""+scrollLeftMessage+"\"; popDownYear(); popDownMonth();' onclick='javascript:decMonth()' onmouseout='clearInterval(intervalID1);swapImage(\"changeLeft\",\"left1.gif\");this.style.borderColor=\"#D4D0C8\";window.status=\"\"' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartDecMonth()\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>&nbsp<IMG id='smart_changeLeft' SRC='"+imgDir+"left1.gif' width=10 height=11 align=middle BORDER=0>&nbsp</span>&nbsp;"
		sHTML1+="&nbsp;<span id='smart_spanRight' style='border-style:solid;border-width:1px;border-color:#D4D0C8;cursor:pointer'	onmouseover='swapImage(\"changeRight\",\"right2.gif\");this.style.borderColor=\"#666666\";window.status=\""+scrollRightMessage+"\"; popDownYear(); popDownMonth();' onmouseout='clearInterval(intervalID1);swapImage(\"changeRight\",\"right1.gif\");this.style.borderColor=\"#D4D0C8\";window.status=\"\"' onclick='incMonth()' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartIncMonth()\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>&nbsp<IMG id='smart_changeRight' SRC='"+imgDir+"right1.gif'	width=10 height=11  align=middle BORDER=0>&nbsp</span>&nbsp"
		sHTML1+="&nbsp;<span id='smart_spanMonth' style='width: 82px;border-style:solid;border-width:1px;border-color:#D4D0C8;cursor:pointer'	onmouseover='swapImage(\"changeMonth\",\"drop2.gif\");this.style.borderColor=\"#666666\";window.status=\""+selectMonthMessage+"\"; popDownYear();' onmouseout='swapImage(\"changeMonth\",\"drop1.gif\");this.style.borderColor=\"#D4D0C8\";window.status=\"\"' onclick='popUpMonth()'></span>&nbsp;"
		sHTML1+="&nbsp;<span id='smart_spanYear' style='border-style:solid;border-width:1px;border-color:#D4D0C8;cursor:pointer' onmouseover='swapImage(\"changeYear\",\"drop2.gif\");this.style.borderColor=\"#666666\";window.status=\""+selectYearMessage+"\"; popDownMonth();' onmouseout='swapImage(\"changeYear\",\"drop1.gif\");this.style.borderColor=\"#D4D0C8\";window.status=\"\"'	onclick='popUpYear()'></span>&nbsp;"
		*/
		getCalObject("smart_caption").innerHTML  =	sHTML1

		bPageLoaded=true
	}
}

function hideCalendar()	{
	try
	{
		crossobj.visibility="hidden";
		if (crossMonthObj != null){crossMonthObj.visibility="hidden"}
		if (crossYearObj !=	null){crossYearObj.visibility="hidden"}
	
		showElement( 'SELECT' );
		showElement( 'APPLET' );
	}
	catch(ex)
	{
	}
}

function padZero(num) {
	return (num	< 10)? '0' + num : num ;
}

function constructDate(d,m,y)
{
	sTmp = dateFormat
	sTmp = sTmp.replace	("dd","<e>")
	sTmp = sTmp.replace	("d","<d>")
	sTmp = sTmp.replace	("<e>",padZero(d))
	sTmp = sTmp.replace	("<d>",d)
	sTmp = sTmp.replace	("mmm","<o>")
	sTmp = sTmp.replace	("mm","<n>")
	sTmp = sTmp.replace	("m","<m>")
	sTmp = sTmp.replace	("<m>",m+1)
	sTmp = sTmp.replace	("<n>",padZero(m+1))
	sTmp = sTmp.replace	("<o>",monthName[m])
	return sTmp.replace ("yyyy",y)
}

function closeCalendar() {
	var	sTmp

	//alert("a")
	hideCalendar();
	//alert("a1")	
	ctlToPlaceValue.value =	constructDate(dateSelected,monthSelected,yearSelected)
}

/*** Month Pulldown	***/

function StartDecMonth()
{
	intervalID1=setInterval("decMonth()",80)
}

function StartIncMonth()
{
	intervalID1=setInterval("incMonth()",80)
}

function incMonth () {
	monthSelected++
	if (monthSelected>11) {
		monthSelected=0
		yearSelected++
	}
	constructCalendar()
}

function decMonth () {
	monthSelected--
	if (monthSelected<0) {
		monthSelected=11
		yearSelected--
	}
	constructCalendar()
}

function constructMonth() {
	popDownYear()
	if (!monthConstructed) {
		sHTML =	""
		for	(i=0; i<12;	i++) {
			sName =	monthName[i];
			if (i==monthSelected){
				sName =	"<span class='selectedTxt'>"+sName+"</span>"
			}
			sHTML += "<tr><td id='smart_m" + i + "' onmouseover='this.style.backgroundColor=\"#CAD2E4\"; this.style.borderColor=\"#666666\"' onmouseout='this.style.backgroundColor=\"\"; this.style.borderColor=\"#FFFFFF\"' style='cursor:pointer; border: 1px solid #ffffff;' bgcolor=#ffffff onclick='monthConstructed=false;monthSelected=" + i + ";constructCalendar();popDownMonth();event.cancelBubble=true'>&nbsp;" + sName + "&nbsp;</td></tr>"
		}
		
		
		
		getCalObject("smart_selectMonth").innerHTML = "<table width=70 style='font-family: tahoma; font-size: 10px; border-width:1px; border-style:solid; border-color:#666666; border-Top: 0px;' bgcolor='#FFFFFF' cellspacing=1 cellpadding=0 onmouseover='clearTimeout(timeoutID1)'	onmouseout='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"popDownMonth()\",100);event.cancelBubble=true'>" +	sHTML +	"</table>"		
		monthConstructed=true
	}
}

function popUpMonth()
{
	constructMonth()
	//----------------
	// BLOCK - Changed for IE & FF 
	//----------------
	//crossMonthObj.visibility = (dom||ie)? "visible"	: "show"
	
	if(dom)
	{
		crossMonthObj.visibility ="visible"
	}
	else{
		crossMonthObj.visibility ="show"
	}
	
	//----------------
	// BLOCK - Ends
	//----------------
	//--------Block - Modified-----------
	//leftOffset = parseInt(crossobj.left) + getCalObject("smart_spanMonth").offsetLeft + 12
	leftOffset = parseInt(getCalObject("smart_calendar").offsetLeft) + getCalObject("smart_spanMonth").offsetLeft + 5
	if (ie)
	{
		leftOffset += 1
	}
	//----------
	// Block - possitioning the Yearlist by Wasim Majid
	//----------
	crossMonthObj.left = leftOffset + "px";
	var objTop =	parseInt(crossobj.top) + 19 
	crossMonthObj.top =	objTop + "px";
	
	//--------Block - Modify Ends-----------
	
	hideElement( 'SELECT', getCalObject("smart_selectMonth") );
	hideElement( 'APPLET', getCalObject("smart_selectMonth") );
	getCalObject('smart_spanMonth').style.borderColor='#666666';
}

function popDownMonth()
{
	crossMonthObj.visibility= "hidden";
	getCalObject('smart_spanMonth').style.borderColor='#D4D0C8';
}

/*** Year Pulldown ***/

function incYear()
{
	for	(i=0; i<7; i++){
		newYear	= (i+nStartingYear)+1
		if (newYear==yearSelected)
		{ txtYear =	"&nbsp;<span class='selectedTxt'>"	+ newYear +	"</span>&nbsp;" }
		else
		{ txtYear =	"&nbsp;" + newYear + "&nbsp;" }
		getCalObject("smart_y"+i).innerHTML = txtYear
	}
	nStartingYear ++;
	bShow=true
}

function decYear()
{
	for	(i=0; i<7; i++){
		newYear	= (i+nStartingYear)-1
		if (newYear==yearSelected)
		{ txtYear =	"&nbsp;<span class='selectedTxt'>"	+ newYear +	"</span>&nbsp;" }
		else
		{ txtYear =	"&nbsp;" + newYear + "&nbsp;" }
		getCalObject("smart_y"+i).innerHTML = txtYear
	}
	nStartingYear --;
	bShow=true
}

function selectYear(nYear)
{
	yearSelected=parseInt(nYear+nStartingYear);
	yearConstructed=false;
	constructCalendar();
	popDownYear();
}

function constructYear()
{
	popDownMonth()
	sHTML =	""
	if (!yearConstructed)
	{

		sHTML =	"<tr><td align='center'	onmouseover='this.style.backgroundColor=\"#FFFFFF\"; this.style.borderColor=\"#666666\"' onmouseout='this.style.backgroundColor=\"\"; this.style.borderColor=\"#FFFFFF\"' style='cursor:pointer; border: 1px solid #ffffff;'	onmousedown='clearInterval(intervalID1);intervalID1=setInterval(\"decYear()\",30)' onmouseup='clearInterval(intervalID1)'>-</td></tr>"

		j =	0
		nStartingYear =	yearSelected-3
		for	(i=(yearSelected-3); i<=(yearSelected+3); i++) {
			sName =	i;
			if (i==yearSelected){
				sName =	"<span class='selectedTxt'>" +	sName +	"</span>"
			}

			sHTML += "<tr><td id='smart_y" + j + "' onmouseover='this.style.backgroundColor=\"#CAD2E4\"; this.style.borderColor=\"#666666\"' onmouseout='this.style.backgroundColor=\"\"; this.style.borderColor=\"#FFFFFF\"' style='cursor:pointer; border: 1px solid #ffffff;' bgcolor=#ffffff onclick='selectYear("+j+");event.cancelBubble=true'>&nbsp;" + sName + "&nbsp;</td></tr>"
			j ++;
		}

		sHTML += "<tr><td align='center' onmouseover='this.style.backgroundColor=\"#FFFFFF\"; this.style.borderColor=\"#666666\"' onmouseout='this.style.backgroundColor=\"\"; this.style.borderColor=\"#FFFFFF\"' style='cursor:pointer; border: 1px solid #ffffff;' bgcolor=#ffffff onmousedown='clearInterval(intervalID2);intervalID2=setInterval(\"incYear()\",30)'	onmouseup='clearInterval(intervalID2)'>+</td></tr>"

		getCalObject("smart_selectYear").innerHTML	= "<table width=44 style='font-family: tahoma; font-size: 10px; border-width:1px; border-style:solid; border-color:#666666; border-Top: 0px;' bgcolor='#FFFFFF' onmouseover='clearTimeout(timeoutID2)' onmouseout='clearTimeout(timeoutID2);timeoutID2=setTimeout(\"popDownYear();\",100)' cellspacing=1 cellpadding=0>"	+ sHTML	+ "</table>"

		yearConstructed	= true
	}
}

function popDownYear()
{
	clearInterval(intervalID1)
	clearTimeout(timeoutID1)
	clearInterval(intervalID2)
	clearTimeout(timeoutID2)
	crossYearObj.visibility= "hidden"
	getCalObject('smart_spanYear').style.borderColor='#D4D0C8';
}

function resetColor()
{
	getCalObject('smart_spanMonth').style.borderColor='#D4D0C8';
	getCalObject('smart_spanYear').style.borderColor='#D4D0C8';
}

function popUpYear() {
	var	leftOffset

	constructYear()
	//--------Block - Modified-----------
	//crossYearObj.visibility	= (dom||ie)? "visible" : "show"
	if(dom) crossYearObj.visibility	="visible"
	else crossYearObj.visibility	="show"
	
	//leftOffset = parseInt(crossobj.left) + getCalObject("smart_spanYear").offsetLeft
	leftOffset = parseInt(getCalObject("smart_calendar").offsetLeft,10) + getCalObject("smart_spanYear").offsetLeft
	
	//--------Block - Modify Ends -----------
	if (ie)
	{
		leftOffset += 1
	}
	//----------
	// Block - possitioning the Yearlist by Wasim Majid
	//----------
	crossYearObj.left = leftOffset + "px";
	var popYaer = parseInt(crossobj.top) +	19
	crossYearObj.top = popYaer + "px";
	//----------
	// Block - Ends
	//----------
	
	getCalObject('smart_spanYear').style.borderColor='#666666';
}

/*** calendar ***/
function WeekNbr(n) {
	// Algorithm used:
	// From Klaus Tondering's Calendar document (The Authority/Guru)
	// hhtp://www.tondering.dk/claus/calendar.html
	// a = (14-month) / 12
	// y = year + 4800 - a
	// m = month + 12a - 3
	// J = day + (153m + 2) / 5 + 365y + y / 4 - y / 100 + y / 400 - 32045
	// d4 = (J + 31741 - (J mod 7)) mod 146097 mod 36524 mod 1461
	// L = d4 / 1460
	// d1 = ((d4 - L) mod 365) + L
	// WeekNumber = d1 / 7 + 1

	year = n.getFullYear();
	month = n.getMonth() + 1;
	if (startAt == 0) {
		day = n.getDate() + 1;
	}
	else {
		day = n.getDate();
	}

	a = Math.floor((14-month) / 12);
	y = year + 4800 - a;
	m = month + 12 * a - 3;
	b = Math.floor(y/4) - Math.floor(y/100) + Math.floor(y/400);
	J = day + Math.floor((153 * m + 2) / 5) + 365 * y + b - 32045;
	d4 = (((J + 31741 - (J % 7)) % 146097) % 36524) % 1461;
	L = Math.floor(d4 / 1460);
	d1 = ((d4 - L) % 365) + L;
	week = Math.floor(d1/7) + 1;

	return week;
}

function constructCalendar () 
{
	var aNumDays = Array (31,0,31,30,31,30,31,31,30,31,30,31)

	var dateMessage
	var	startDate =	new	Date (yearSelected,monthSelected,1)
	var endDate

	if (monthSelected==1)
	{
		endDate	= new Date (yearSelected,monthSelected+1,1);
		endDate	= new Date (endDate	- (24*60*60*1000));
		numDaysInMonth = endDate.getDate()
	}
	else
	{
		numDaysInMonth = aNumDays[monthSelected];
	}

	datePointer	= 0
	dayPointer = startDate.getDay() - startAt

	if (dayPointer<0)
	{
		dayPointer = 6
	}

	sHTML =	"<table	 border=0 cellpadding='2' cellspacing='1' class='days'><tr class='weekTr'>"

	if (showWeekNumber==1)
	{
		sHTML += "<td width='15'><b>" + weekString + "</b></td><td width=1 rowspan=7 bgcolor='#d0d0d0' style='padding:0px'><img src='"+imgDir+"divider.gif' width=1></td>"
	}

	for	(i=0; i<7; i++)	{
		sHTML += "<td width='15' align='right'><B>"+ dayName[i]+"</B></td>"
	}
	sHTML +="</tr><tr>"

	if (showWeekNumber==1)
	{
		sHTML += "<td align=right>" + WeekNbr(startDate) + "&nbsp;</td>"
	}

	for	( var i=1; i<=dayPointer;i++ )
	{
		sHTML += "<td>&nbsp;</td>"
	}

	for	( datePointer=1; datePointer<=numDaysInMonth; datePointer++ )
	{
		dayPointer++;
		sHTML += "<td align=right>"
		sStyle=styleAnchor
		if ((datePointer==odateSelected) &&	(monthSelected==omonthSelected)	&& (yearSelected==oyearSelected))
		{ sStyle+=styleLightBorder }

		sHint = ""
		for (k=0;k<HolidaysCounter;k++)
		{
			if ((parseInt(Holidays[k].d)==datePointer)&&(parseInt(Holidays[k].m)==(monthSelected+1)))
			{
				if ((parseInt(Holidays[k].y)==0)||((parseInt(Holidays[k].y)==yearSelected)&&(parseInt(Holidays[k].y)!=0)))
				{
					sStyle+="background-color:#FFDDDD;"
					sHint+=sHint==""?Holidays[k].desc:"\n"+Holidays[k].desc
				}
			}
		}

		var regexp= /\"/g
		sHint=sHint.replace(regexp,"&quot;")

		dateMessage = "onmousemove='window.status=\""+selectDateMessage.replace("[date]",constructDate(datePointer,monthSelected,yearSelected))+"\"' onmouseout='window.status=\"\"' "

		if ((datePointer==dateNow)&&(monthSelected==monthNow)&&(yearSelected==yearNow))
		{
			sHTML += "<b><a "+dateMessage+" title=\"" + sHint + "\" style='"+sStyle+"' href='#calender_call' onClick='javascript:dateSelected="+datePointer+";closeCalendar();'><font color=#ff0000>&nbsp;" + datePointer + "</font>&nbsp;</a></b>"
		}
		else if	(dayPointer % 7 == (startAt * -1)+1)
		{
			sHTML += "<a "+dateMessage+" title=\"" + sHint + "\" style='"+sStyle+"'  href='#calender_call' onClick='javascript:dateSelected="+datePointer + ";closeCalendar();'>&nbsp;<font color=#909090>" + datePointer + "</font>&nbsp;</a>"
		}
		else
		{
			sHTML += "<a "+dateMessage+" title=\"" + sHint + "\" style='"+sStyle+"'  href='#calender_call' onClick='javascript:dateSelected="+datePointer + ";closeCalendar();'>&nbsp;" + datePointer + "&nbsp;</a>"
		}

		sHTML += ""
		if ((dayPointer+startAt) % 7 == startAt) {
			sHTML += "</tr><tr>"
			if ((showWeekNumber==1)&&(datePointer<numDaysInMonth))
			{
				sHTML += "<td align=right>" + (WeekNbr(new Date(yearSelected,monthSelected,datePointer+1))) + "&nbsp;</td>"
			}
		}
	}

	getCalObject("smart_content").innerHTML   = sHTML
	getCalObject("smart_spanMonth").innerHTML = "&nbsp;" +	monthName[monthSelected] + "&nbsp;<IMG id='changeMonth' SRC='"+imgDir+"drop1.gif' WIDTH='12' HEIGHT='10' BORDER=0 align=absmiddle>"
	getCalObject("smart_spanYear").innerHTML =	"&nbsp;" + yearSelected	+ "&nbsp;<IMG id='changeYear' SRC='"+imgDir+"drop1.gif' WIDTH='12' HEIGHT='10' BORDER=0 align=absmiddle>"
}

// This function validate Date by regular expression
// returns true on valid date and returns false for invalid date
function validateDateWithRg(txtVal)
{
	txtVal = trim(txtVal);
    var dt1 = new Array();
    var reDt1 = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
    var b = reDt1.test(txtVal)

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
		
	    var endDt = getMonthDate(dt1[0],dt1[2])
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

function popUpCalendar(ctl, ctl2, format, frameOffSetLeft, frameOffSetTop) {
	var	leftpos=0
	var	toppos=0    
    
	if (bPageLoaded)
	{
		if ( crossobj.visibility ==	"hidden" ) {
			ctlToPlaceValue	= ctl2
			dateFormat=format;

			formatChar = " "
			aFormat	= dateFormat.split(formatChar)
			if (aFormat.length<3)
			{
				formatChar = "/"
				aFormat	= dateFormat.split(formatChar)
				if (aFormat.length<3)
				{
					formatChar = "."
					aFormat	= dateFormat.split(formatChar)
					if (aFormat.length<3)
					{
						formatChar = "-"
						aFormat	= dateFormat.split(formatChar)
						if (aFormat.length<3)
						{
							// invalid date	format
							formatChar=""
						}
					}
				}
			}

			tokensChanged =	0
			if ( formatChar	!= "" )
			{
				// use user's date
				aData =	ctl2.value.split(formatChar)

				for	(i=0;i<3;i++)
				{
					if ((aFormat[i]=="d") || (aFormat[i]=="dd"))
					{
						dateSelected = parseInt(aData[i], 10)
						tokensChanged ++
					}
					else if	((aFormat[i]=="m") || (aFormat[i]=="mm"))
					{
						monthSelected =	parseInt(aData[i], 10) - 1
						tokensChanged ++
					}
					else if	(aFormat[i]=="yyyy")
					{
						yearSelected = parseInt(aData[i], 10)
						tokensChanged ++
					}
					else if	(aFormat[i]=="mmm")
					{
						for	(j=0; j<12;	j++)
						{
							if (aData[i]==monthName[j])
							{
								monthSelected=j
								tokensChanged ++
							}
						}
					}
				}
			}
			//alert(dateSelected)
			//alert(monthSelected)	
			//alert(yearSelected)	
			//alert(dateNow)	
			//alert(monthNow)	
			//alert(yearNow)	
			
			if ((tokensChanged!=3)||isNaN(dateSelected)||isNaN(monthSelected)||isNaN(yearSelected))
			{
				dateSelected = dateNow
				monthSelected =	monthNow
				yearSelected = yearNow
			}
			else
			{
				var ch = "" + monthSelected + "/" + dateSelected + "/" + yearSelected + "";
				if(!validateDateWithRg(ch))
				{
					dateSelected = dateNow
					monthSelected =	monthNow
					yearSelected = yearNow
				}
			}
			
			odateSelected=dateSelected
			omonthSelected=monthSelected
			oyearSelected=yearSelected

			aTag = ctl
			
			do {
				aTag = aTag.offsetParent;
				leftpos	+= aTag.offsetLeft;
				toppos += aTag.offsetTop;
			} while(aTag.tagName!="BODY");

			//crossobj.left =	fixedX==-1 ? ctl.offsetLeft + leftpos + frameOffSetLeft : fixedX
			//crossobj.top = fixedY==-1 ? ctl.offsetTop + toppos + frameOffSetTop + ctl.offsetHeight : fixedY			
			//--------------
			// BLOCK - Set Possitioning
			//--------------
			if(fixedX==-1)
			{
				var lef = ctl.offsetLeft + leftpos + frameOffSetLeft;
				crossobj.left = lef + "px";
			}
			else
			{
				crossobj.left = fixedX;		
			}
			if(fixedY==-1)
			{
				var top = ctl.offsetTop + toppos + frameOffSetTop + ctl.offsetHeight;
				crossobj.top = top + "px";
			}
			else
			{
				crossobj.top = fixedY;		
			}	
			//--------------
			// BLOCK - End set possitioning
			//--------------
			//------------------
			// Block-Div Align - Worked to bring teh div right aligned from the image
			//------------------
			/*
			var wid = crossobj.left;
			var leftS = parseInt(wid.substr(0,(wid.length-2)))
			if(leftS > 750)
			{
				crossobj.left = "780px";
			}
			*/
			var wid = crossobj.left;
			var leftS = parseInt(wid.substr(0,(wid.length-2)))
		
			//if (leftS < 180) leftS =180 - (180 - leftS) 
			//else 
				leftS = leftS - 180;
			crossobj.left = leftS + "px"; 
			//------------------
			// Block-Div Align - Tanvir Gaus - modified on Aug 31, 2006
			//------------------
			
			constructCalendar (1, monthSelected, yearSelected);
			//crossobj.visibility=(dom||ie)? "visible" : "show"
			
			if(dom)crossobj.visibility ="visible"
			else crossobj.visibility ="show"

			hideElement( 'SELECT', getCalObject("smart_calendar") );
			hideElement( 'APPLET', getCalObject("smart_calendar") );

			bShow = true;
		}
		else
		{
			hideCalendar()
			if (ctlNow!=ctl) {popUpCalendar(ctl, ctl2, format, leftpos, toppos)}
		}
		ctlNow = ctl
	}
}

if(ie)
{
document.onkeypress = function hidecal1 ()
{
	hideCalendar()
}
}
document.onclick = function hidecal2 ()
{
	if (!bShow)
	{
		hideCalendar()
	}
	bShow = false
}
//-------------
// Block - Wrapper - Wrapper created of popUpCalendar()
//-------------
function loadCalendar(targetCtrl, targetId, dt_format)
{
	popUpCalendar(targetCtrl, getCalObject(targetId), dt_format, 0, 0)	
}

//-------------------
// Declare Vars
//-----------------
var calIe5 = (document.all && !document.getElementById) ? 1 : 0;
var calIe6 = (document.all && document.getElementById) ? 1 : 0;
var calFF = (!document.all && document.getElementById) ? 1 : 0;
//-------------------
// Get Object
//-----------------
function getCalObject(e) {
	var obj;
	if(calIe5)
		obj = eval("document.all." + e);
	if(calIe6 || calFF)
		obj = document.getElementById(e);
	
	//alert(obj);
	return obj;
}

//-------------
// Block - Wrapper - Ends
//-------------
/*
if(ie)
{
	window.onload = onloadMethod;
	init()
	
}
else
{
	window.onload=onloadMethod;init;
}
*/
