$(document).ready(function () {
    $("#image").change(function () {
        var fullPath = $(this).val();
        if (fullPath != null && fullPath.length > 0) {
            var filename = fullPath.replace(/^.*[\\\/]/, '')
            $("#txtImageName").html(filename);
        }
    })
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

