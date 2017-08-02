/// <reference path="knockout-3.4.2.js" />

$(function () {

    (function (settings) {

        function catagoryViewModel() {
            var self = this;
            self.catagorylist = ko.observableArray();
            self.resourcelist = ko.observableArray();
        }

        var viewModel = new catagoryViewModel();

        ko.applyBindings(viewModel);

        $("#catagory_ip").click(function () {
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
                else {
                    viewModel.catagorylist.removeAll();
                }
            })
        })

        $("#catagory_tel").click(function () {
            $.ajax({
                type: "POST",
                url: settings.getCatagoryAction,
                dataType: 'json',
                traditional: true,
                data: {
                    resType: "电话",
                    __RequestVerificationToken: settings.antiForgeryToken,
                },
            }).done(function (result) {
                if (result) {
                    var jsonResult = JSON.parse(result);
                    viewModel.catagorylist(jsonResult.catagory)

                    console.log(jsonResult);
                }
                else {
                    viewModel.catagorylist.removeAll();
                }
            })
        })

        $(document).on("click",".clsAction",function(e) {
            var clsid = e.target.getAttribute("id");
            clsid = clsid.substr(clsid.lastIndexOf("-")+1);
            $.ajax({
                type: "POST",
                url: settings.getClassifiedResourceAction,
                dataType: 'json',
                traditional: true,
                data: {
                    clsid: clsid,
                    __RequestVerificationToken: settings.antiForgeryToken,
                },
            }).done(function (result) {
                if (result) {
                    var jsonResult = JSON.parse(result);
                    viewModel.resourcelist(jsonResult.resources)

                    console.log(jsonResult);
                }
                else {
                    viewModel.catagorylist.removeAll();
                }
            })
        })



    })(window.catagoryEditSettings);
})