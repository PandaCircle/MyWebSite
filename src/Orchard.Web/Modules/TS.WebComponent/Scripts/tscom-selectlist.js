$(function () {

  (function () {
    function singleLabel(id) {

      var self = this;
      self.eid = id;
      self.selected = ko.observable(false);
      self.cssClass = ko.computed(function () {
        var css ='';
        if(self.selected()){
          css+='selected';
        }
        return css;
      });
    }

    function labelGroup() {
      var self = this;
      self.selection = ko.observableArray([]);
      self.labelList= ko.observableArray([]);

      self.toggleSelect = function(label) {
        console.log(label);
        var index = $.inArray(label,self.selection());
        if(index==-1){
          self.selection.remove(function(item) {
            return item.eid == label.eid;
          });
          self.selection.push(label);
          label.selected(true);
        }
        else{
          self.selection.remove(label);
          label.selected(false);
          console.log("set false");
        }
      }

      self.makeList = function() {
        self.labelList.removeAll();
        var intArray = ['就是你','草','妈的','打得','的','地方','额'];
        for(var i=0;i<7;i++){
          console.log(intArray[i]);
          self.labelList.push(new singleLabel(intArray[i]));
        }
      }
    }

    var viewModel = new labelGroup();
    viewModel.makeList();

    ko.applyBindings(viewModel);

    var str = ".myul";
    $(str).on("mousedown","li",function(e) {
      console.log(this);
      var label = ko.dataFor(this);
      viewModel.toggleSelect(label);
      console.log(viewModel.selection());
    });

})();
})
