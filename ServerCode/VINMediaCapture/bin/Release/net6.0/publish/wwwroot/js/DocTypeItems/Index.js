﻿
function DeleteDocTypeItems(colorId) {
    bootbox.confirm("Bạn chắc chắn muốn xóa dữ liệu?", function (result) {
        if (result) {
            $.ajax({
                type: 'POST',
                url: $("#urlDeleteDocTypeItems").val(),
                data: jQuery.param({ id: colorId }),
                async: false,
                success: function (response) {
                    if (response.resultCode > 0) {
                        bootbox.alert("Xóa dữ liệu thành công", function () {
                            $("#btnSearch").click();
                        });
                    }
                    else {
                        bootbox.alert(response.message);
                    }
                },
                error: function (error) {
                    console.log(error);
                    rs = - 1;
                }
            });
        }
    });
    
}
function PagerClick(page) {
    $("#CurrPage").val(page);
    $("#frmForm").submit();
}