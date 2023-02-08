function SetMultiStatus(status) {
    $(".chkDugs").each(function myfunction() {
        var ids = "";
        if (this.checkAll) {
            ids += $(this).val()+",";
        }
    });
    if (ids.leng>0) {
        SetStatus(ids, status);
    }
}
$(".chkSelectAllDugs").change(function () {
    var checkAll = this.checked;
    $(".chkDugs").each(function myfunction() {
        $(this).prop('checked', checkAll);
    });
});
function SetStatus(ids, status) {
    var arr = new Array();
    var idArr = ids.split(",");
    for (var i = 0; i < idArr.length; i++) {
        var objWarehouse = new Object();
        objWarehouse.Id = idArr[i];
        objWarehouse.Status = status;
        arr.push(objWarehouse);
    }
    $.ajax({
        type: 'POST',
        url: $("#ajaxSetStatus").val(),
        data: jQuery.param({ warehouses: arr }),
        async: false,
        success: function (response) {
            if (response.resultCode > 0) {
                bootbox.alert("Lưu giao dịch thành công", function () {
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
function PagerClick(page) {
    $("#CurrPage").val(page);
    $("#frmForm").submit()
}
