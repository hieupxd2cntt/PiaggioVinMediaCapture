function PagerClick(page) {
    $("#CurrPage").val(page);
    $("#frmForm").submit();
}


function LoadDetailProduct(vinCode, productDoc) {
    $.ajax({
        type: 'POST',
        url: $("#urlLoadDetailProduct").val(),
        data: jQuery.param({ vinCode: vinCode, productDoc: productDoc }),
        async: false,
        success: function (response) {
            debugger;
            $("#detail-body").empty().append(response);
        },
        error: function (error) {
            console.log(error);
            rs = - 1;
        }
    });
}
