$(document).ready(function () {
    ValiaDateForm("frmForm");

});
$('#btnSave').on('click', function (evt) {
    if (!$("#frmForm").valid()) {
        evt.preventDefault(); //don't submit the form, which a button naturally does
        return;
    }
    bootbox.confirm("Bạn chắc chắn muốn lưu dữ liệu?", function (result) {
        if (result) {
            $("#frmForm").submit();
        }
    });

});

