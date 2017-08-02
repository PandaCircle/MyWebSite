/// <reference path="knockout-3.4.2.js" />

$(function () {
    (function(settings){

        function resouceAddView() {
            var self = this;
            self.ipSelected = ko.observable(true);
            self.telSelected = ko.observable(false);
        }

        var viewmodel = new resouceAddView();

        ko.applyBindings(viewmodel);

        $("#ipadd").click(function () {
            viewmodel.ipSelected(true);
            viewmodel.telSelected(false);
        });

        $("#teladd").click(function () {
            viewmodel.ipSelected(false);
            viewmodel.telSelected(true);
        })

    })(window.resourceSettings)
})