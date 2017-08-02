/// <reference path="knockout-3.4.2.js" />

$(function () {

    (function (settings) {

        function catagoryViewModel() {
            var self = this;
            self.catagorylist = ko.observableArray();
        }

        var viewModel = new catagoryViewModel();

        ko.applyBindings(viewModel);

        $("#getcatagory").click(function () {
            $.ajax({
                type: "POST",
                url: settings.getCatagoryAction,
                dataType: 'json',
                traditional: true,
                data: {
                    resType: "IP地址",
                    __RequestVerificationToken: settings.antiForgeryToken,
                },
            }).done(function (result) {
                if (result) {
                    var jsonResult = JSON.parse(result);
                    viewModel.catagorylist(jsonResult.catagory)

                    console.log(jsonResult);
                }
            })
        })



    })(window.catagoryEditSettings);
})