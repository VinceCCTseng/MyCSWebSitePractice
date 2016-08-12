//------------------------[S]-----------------------------
// This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
// window.onload help the scrpit wait the page loaded, otherwise it won't work. (because the variables are not exist)
window.onload = function () {
function AppViewModel() {
    if (document.getElementById('LblTotalPrice') != null)
    {

        var tprice = ko.observable(window.document.getElementById('LblTotalPrice').innerText);
        this.showGst = ko.observable(tprice() * 0.091);
        this.showExcGstTotal = ko.observable(tprice() * 0.909);
        this.showDeliverFee = 20.00;
        this.showGrandTotal = ko.observable(parseInt(tprice()) + parseInt(this.showDeliverFee));
        //debugger
        //console.log('[log check1] > ' + tprice + ' <');
    }  

}

// Activates knockout.js
ko.applyBindings(new AppViewModel());
}
//------------------------[E]-----------------------------

