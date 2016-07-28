//------------------------[Begin] Main Function Area-----------------------------
// 0. Delay function
function sleep(delay) {
    var start = new Date().getTime();
    while (new Date().getTime() < start + delay);
}
//Jquery
// 1. (Add item) -Shake the start when click add button
function addStarChangingColour(e) {
    var thestar = window.document.getElementById('myStar');
    $('#myStar').css("color", "yellow");

    if (thestar != null)
    {
        $('#myStar').animate({ left: "+=10px" },150);
        $('#myStar').animate({ left: "-=10px" },150);
        $('#myStar').animate({ left: "0" });        
    }
    $('#myStar').css("color", "grey");
    //--------------badge-----------------
    //console.log('you clicked: updateNumberOfGood [1]');
    var badge = window.document.getElementById('badgeItem');
    //console.log('you clicked: updateNumberOfGood [1]' + '[2]' + +badge + '[3]' );
    badge= 2;
}

// 2. Remove item from Shopping Cart - shake the star up side down
function removeStarChangingColour(e) {
    //console.log('you clicked: remove');
    var thestar = window.document.getElementById('myStar');
    $('#myStar').css("color", "red");

    if (thestar != null) {
        $('#myStar').animate({ top: "+=10px" }, 150);
        $('#myStar').animate({ top: "-=10px" }, 150);
        $('#myStar').animate({ top: "0" });
    }
    $('#myStar').css("color", "grey");
}

// 3. Pick the Date of Birth - show the calender to pickup the dob when user focus on textbox(Version 2.0)
$("body").delegate('#txtdob', "focusin", function () {
    var currentDateString = (window.document.getElementById('txtdob').value).split("-");
    var curDate = new Date(currentDateString[2], currentDateString[1] - 1, currentDateString[0]);
    $(this).datepicker({
        dateFormat: 'dd-mm-yy',
        changeYear: true,
        changeMonth: true,
        yearRange: "100:+50",
        defaultDate: curDate
    });
});

function updateNumberOfGood(e) {

};

//------------------------[End] Main Function Area-----------------------------

//------------------------ToDo: remove-----------------------------
//Old function 

function removeStarChangingColourOld(e) {
    console.log('you clicked: ' + this.id);
    if (window.document.getElementById('myStar') != null) {
        var thestar = window.document.getElementById('myStar');
        var i = 0;
        var _fontsize = 20;

        thestar.style.color = "red";
        //zoom in
        var intverval = setInterval(frame, 20);
        function frame() {
            if (i >= 10) {
                thestar.style.color = "gray";
                clearInterval(intverval);
            }
            else if (i <= 5) {
                i++;
                _fontsize--
                thestar.style.fontSize = _fontsize + "px";
                console.log("in" + _fontsize);
            }
            else {
                i++;
                _fontsize++;
                thestar.style.fontSize = _fontsize + "px";
                console.log("in" + _fontsize);
            }
        }
        //console.log('the color is now set to: ' + window.document.getElementById('myStar').style.color);
    }
}

//Datepickup Old (todo: remove)
function PickDob(e) {

    var currentDateString = (window.document.getElementById('txtdob').value).split("-");
    var curDate = new Date(currentDateString[2], currentDateString[1] - 1, currentDateString[0]);
    var selectDate = new Date();
    //console.log('you clicked PickDob: ' + curDate);

    $('#txtdob').datepicker({
        dateFormat: 'mm-dd-yy',
        changeYear: true,
        changeMonth: true,
        yearRange: "100:+50",
        singleDatePicker: true,
        onSelect: function () {
            selectDate = $(this).datepicker('getDate');
        }
    });

    //console.log('[Log 2]you clicked PickDob: ');
    $('#txtdob').datepicker('setDate', curDate);
    //console.log('[Log 3]you clicked PickDob: ');
    selectDate = $('#txtdob').datepicker('show');
    //console.log('[Log 4]you clicked PickDob: ' + selectDate);
    //$('#txtdob').datepicker('remove');
    //console.log('[Log 5]you clicked PickDob: ');
}
//old - JavaScript
//Add function for the shopping cart star
function addStarChangingColourOld(e) {
    //console.log('you clicked: ' + this.id);
    if (window.document.getElementById('myStar') != null) {
        var thestar = window.document.getElementById('myStar');
        var i = 0;
        var _fontsize = 20;

        thestar.style.color = "yellow";
        //zoom in
        var intverval = setInterval(frame, 20);
        function frame() {
            if (i >= 10) {
                thestar.style.color = "gray";
                clearInterval(intverval);
            }
            else if (i <= 5) {
                i++;
                _fontsize++;
                thestar.style.fontSize = _fontsize + "px";
                console.log("in" + _fontsize);
            }
            else {
                i++;
                _fontsize--;
                thestar.style.fontSize = _fontsize + "px";
                console.log("in" + _fontsize);
            }
        }
        //console.log('the color is now set to: ' + window.document.getElementById('myStar').style.color);
    }
}
//------------------------ToDo: remove-----------------------------