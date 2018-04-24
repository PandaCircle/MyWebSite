JQuery(function ($) {

    $("#item-add").on('click', function () {
        $(".item-list").append("<li><span>ItemCatalog</span><input name=\"itemname\" /><span>Quantity</span><input name=\"quantity\" /><span>Remark</span><input name=\"remark\" /></li>");
    })
});