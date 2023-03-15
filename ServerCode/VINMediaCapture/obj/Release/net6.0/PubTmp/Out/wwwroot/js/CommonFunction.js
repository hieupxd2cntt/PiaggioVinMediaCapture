$(".select2").select2();
$(".select2tag").select2({
    tags: true
});
$('.datepicker').datepicker({
    format: "dd/mm/yyyy",
    todayBtn: true
});
$(".number").keyup(function (e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var value = Number(e.target.value + e.key) || 0;

    if ((keyCode >= 37 && keyCode <= 40) || (keyCode == 8 || keyCode == 9 || keyCode == 13) || (keyCode >= 48 && keyCode <= 57)) {
      
        $(this).val(numberWithCommas(ReplaceNumberWithCommas($(this).val())));
        return true;
    }
    this.value = this.value.replace(/[^0-9]/, '');
    return false;
});

function numberWithCommas(x) {
    if (x== undefined) {
        x = "";
    }
    if (x== null || x=="") {
        return 0;
    }
    if (typeof(x) == "string") {
        return parseInt(x).toLocaleString('en-US');
    }
    
    return x.toLocaleString('en-US');
}
function ReplaceNumberWithCommas(x) {
    if (x == undefined) {
        x = "";
    }
    var temp = x.replace(/,/g, '');
    if (temp == null || temp == "") {
        return 0;
    }
    return temp;
}