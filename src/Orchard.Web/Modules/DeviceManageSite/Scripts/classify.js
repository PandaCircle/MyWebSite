/// <reference path="knockout-3.4.2.js" />

$(function () {
    (function(settings){
        
        function resourceModel(data) {
            var self = this;

            self.data = data;
            self.selected = ko.observable();
            self.cssClass = ko.computed(function () {
                var css = '';
                if (self.selected()) {
                    css += 'selected';
                }
            });
        };

        function catagoryData() {
            var self = this;

            self.classified = ko.observableArray();
            self.available = ko.observableArray();
            self.adding = ko.observableArray([]);
            self.removing = ko.observableArray([]);
            self.dirty = ko.computed(function(){
                if(self.adding().length || self.removing().length)
                    return true;
                return false;
            });

        };


        var viewModel = new catagoryData();

        $.map(settings.classifiedResource, function (res, index) {
            viewModel.classified.push(new resourceModel(res))
        });

        $.map(settings.uncataloguedResource, function (res, index) {
            viewModel.available.push(new resourceModel(res))
        });

        function sumbitModify() {
            if (!viewModel.dirty()) { return; }
            var addingIds = [];
            var removingIds = [];
            for(var a=0;a<viewModel.adding().length;a++){
                addingIds.push(a.ResId);
            }
            for(var b= 0;a<viewModel.removing().length;b++){
                removingIds.push(b.ResId);
            }


            $.ajax({
                type: "POST",
                url: settings.postUrl,
                dataType: "json",
                traditional: true,
                data: {
                    addingIds: addingIds,
                    removingIds:removingIds
                }
            }).done(function () {
                console.log("success");
            }
            );
        };

        ko.applyBindings(viewModel);

    })(window.settings);
})