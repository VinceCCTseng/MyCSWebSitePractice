//------------------------[S]-----------------------------
// This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
// window.onload is...
window.onload = function () {
function AppViewModel() {
    if (document.getElementById('LblTotalPrice') != null)
    {

        var tprice = window.document.getElementById('LblTotalPrice').innerText;
        this.showGst = ko.observable(tprice * 0.091);
        this.showExcGstTotal = ko.observable(tprice * 0.909);
        this.showDeliverFee = 20.00;
        this.showGrandTotal = ko.observable(parseInt(tprice) + parseInt(this.showDeliverFee));

        //console.log('[log check1] > ' + tprice + ' <');
    }  

}

// Activates knockout.js
ko.applyBindings(new AppViewModel());
}
//------------------------[E]-----------------------------

