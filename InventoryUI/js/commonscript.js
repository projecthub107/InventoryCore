function pSuccess(updateResult) {

}

$(document).ready(function () {

    var AutoComProduct;

    function split(val) {
        return val.split(/,\s*/);
    }

    function extractLast(term) {
        return split(term).pop();
    }

    $("#head_txtSearch").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "ServiceUtility.aspx/GetProductName",
                data: "{'keyword':'" + $("#head_txtSearch").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    console.log(data.d);
                    response($.map(data.d, function (item) {
                        return {
                            label: item.ProductCode + ' - ' + item.ProductName,
                            ProductName: item.ProductName,
                            ProductCode: item.ProductCode,
                            ProductId: item.ProductId,
                            QuantityInStock: item.QuantityInStock
                        }
                    }));
                },
                error: function () {
                    console.log("there is some error");
                    console.log(ex);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            if (ui.item != null)
                AutoComProduct = ui.item;

            $.ajax({
                type: "POST",
                url: "ServiceUtility.aspx/SetProduct",
                data: "{'product':'" + JSON.stringify(AutoComProduct) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var result = msg;
                    // Do something interesting here.
                },
                error: function (ex) {
                    console.log("there is some error");
                    console.log(ex);
                   
                }
            });

            console.log(AutoComProduct);
        },
        change: function (event, ui) {
            if (ui.item != null)
                AutoComProduct = ui.item;

            console.log(AutoComProduct);
        },
        messages: {
            noResults: "",
            results: function () { }
        },
        search: function () { $(this).addClass('progress'); },
        open: function () { $(this).removeClass('progress'); },
        response: function () { $(this).removeClass('progress'); }
    });

    $("#head_txtUserSearch").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "ServiceUtility.aspx/GetUserName",
                data: "{'keyword':'" + $("#head_txtUserSearch").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    console.log(data.d);
                    response($.map(data.d, function (item) {
                        return {
                            label: item.UserName,
                            UserName: item.UserName,
                            Email: item.Email,
                            UserId: item.UserId
                        }
                    }));
                },
                error: function () {
                    console.log("there is some error");
                    console.log(ex);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            if (ui.item != null)
                AutoComProduct = ui.item;

            $.ajax({
                type: "POST",
                url: "ServiceUtility.aspx/SetUser",
                data: "{'user':'" + JSON.stringify(AutoComProduct) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var result = msg;
                    // Do something interesting here.
                },
                error: function (ex) {
                    console.log("there is some error");
                    console.log(ex);

                }
            });

            console.log(AutoComProduct);
        },
        change: function (event, ui) {
            if (ui.item != null)
                AutoComProduct = ui.item;

            console.log(AutoComProduct);
        },
        messages: {
            noResults: "",
            results: function () { }
        },
        search: function () { $(this).addClass('progress'); },
        open: function () { $(this).removeClass('progress'); },
        response: function () { $(this).removeClass('progress'); }
    });

    //$("#txtSearch")
    //        .on("keydown", function (event) {
    //            if (event.keyCode === $.ui.keyCode.TAB &&
    //                $(this).autocomplete("instance").menu.active) {
    //                event.preventDefault();
    //            }
    //        })
    //       .autocomplete({
    //           source: function (request, response) {
    //               $.ajax({
    //                   type: "POST",
    //                   url: "receiveinventory.aspx/GetSalesPerson",
    //                   data: "{'keyword':'" + extractLast($("#txtSearch").val()) + "'}", // { keyword: extractLast(request.term) },//
    //                   contentType: "application/json; charset=utf-8",
    //                   dataType: "json",
    //                   success: function (data) {
    //                       // console.log(data);
    //                       // response(data);

    //                       var result = data.d;
    //                       response($.map(data.d, function (item) {
    //                           return {
    //                               label: item.ProductCode,
    //                               desc: item.ProductCode,
    //                               value: item.ProductCode
    //                           }
    //                       }));
    //                   },
    //                   error: function (e) {
    //                       //console.log("there is some error");
    //                       //console.log(e);
    //                   }
    //               });
    //           },
    //           minLength: 1,
    //           select: function (event, ui) {
    //               if (ui.item !== null) {
    //                   //console.log(SalesPersonID);
    //                   //SalesPersonID = ui.item.desc;

    //                   var terms = split(this.value);

    //                   // remove the current input
    //                   terms.pop();

    //                   // add the selected item
    //                   terms.push(ui.item.value);

    //                   // add placeholder to get the comma-and-space at the end
    //                   terms.push("");
    //                   this.value = terms.join(", ");
    //                   return false;
    //               }
    //           },
    //           search: function () {
    //               // custom minLength
    //               //var term = extractLast(this.value);
    //               //if (term.length < 2) {
    //               //    return false;
    //               //}
    //           },
    //           focus: function () {
    //               // prevent value inserted on focus
    //               return false;
    //           },
    //           //change: function (event, ui) {
    //           //    if (ui.item !== null) {
    //           //        console.log(SalesPersonID);
    //           //        SalesPersonID = ui.item.desc;
    //           //    }
    //           //},
    //           messages: {
    //               noResults: "",
    //               results: function () { }
    //           },
    //           // search: function () { $(this).addClass('progress'); },
    //           open: function () { $(this).removeClass('progress'); },
    //           response: function () { $(this).removeClass('progress'); }

    //       });
});