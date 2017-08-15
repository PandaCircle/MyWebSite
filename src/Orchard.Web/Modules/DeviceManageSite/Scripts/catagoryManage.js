/// <reference path="knockout-3.4.2.js" />

$(function () {

    (function (settings) {


        //resourceitem model
        function resourceItemViewModel(data) {
            var self = this;
            //from server json data
            self.data = data;
            //id
            //content
            self.selected = ko.observable();
            self.hasFocus = ko.observable();
            self.cssClass = ko.computed(function () {
                var css = '';
                if (self.selected()) {
                    //refer to css file styleName
                    css += 'selected';     
                }
                if (self.hasFocus()) {
                    css += 'has-focus';
                }
                return css;
            });

        }

        //=====end resourceitem model======

        //catagory model
        function catagoryIndexModel() {
            var self = this;
            //value
            self.currentCata = ko.observable();
            self.classifiedResult = ko.observableArray();
            self.availableResult = ko.observableArray();
            self.addingSelection = ko.observableArray([]);
            self.removeingSelection = ko.observableArray([]);
            self.dirty = ko.computed(function () {
                var length = addingSelection.length + removeingSelection.length;
                if (length == 0) return false;
                else return true;
            });
            //减少不必要的数据请求
            self.resourceItemPendingRequest = ko.observable(false);
            self.pendingRequest = ko.computed({
                read: function () {
                    return (resourceItemPendingRequest());
                },
                write: function (value) {
                    resourceItemPendingRequest(value);
                }
            });

            self.getClassfiedResource = function (cataId) {

                self.pendingRequest(true);
                $.ajax({
                    type: "POST",
                    url: url,
                    dataType: 'json',
                    traditional:true,
                    data: {
                        cata:cataId
                    }
                }).done(function (data) {
                    var resItems = data.items;
                    for (var i = 0; i < resItems.length; i++) {
                        var item = new resourceItemViewModel(resItems[i]);
                        self.classifiedResult.push(item);
                    }
                }).fail(function (data) {
                    console.error(data);
                }).always(function () {
                    self.pendingRequest(false);
                })
            };

            self.getAvailaleResource = function () {
                self.pendingRequest(true);
                $.ajax({
                    type: 'GET',
                    url: url,
                    cache:false
                }).done()
            };
        }

        //=====end catagory model======

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

                viewModel.resourcelist.removeAll();
                if (result) {
                    var jsonResult = JSON.parse(result);
                    viewModel.catagorylist(jsonResult.catagory);
                    

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

                viewModel.resourcelist.removeAll();
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