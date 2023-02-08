//$(".select2").select2();
$(document).ready(function () {
    ValiaDateForm('transactionForm');
    $("#txtCustomer").select2({
        ajax: {
            url: $("#urlSearchCustomer").val(),
            dataType: 'json',
            data: function (params) {
                var query = {
                    search: params.term,
                    type: $("#TransType").val()
                }

                // Query parameters will be ?search=[term]&type=public
                return query;
            }, processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.id +" - " + item.text,
                            attribute: item.attribute,
                            phone:item.phone,
                            id: item.text
                        }
                    })
                };
            }
        },
        tags: true
    });
    $('#txtCustomer').on('select2:select', function (e) {
        var data = e.params.data;
        if (data.id != data.text) {//Nếu id != text=> trường hợp khách hàng cũ. Đưa thông tin sđt lên
            //$("#txtPhone").val(data.id);
            var newState = new Option(data.text, data.phone, true, true);
            $("#txtPhone").append(newState).trigger('change');
        }
        else {
            if ($("#txtPhone option:selected").text() != $("#txtPhone").val()) {//chọn khách hàng mới. thì xóa phone nếu đang phone cũ
                $("#txtPhone").empty().trigger('change');
            }
            
        }
    });


    $("#txtPhone").select2({
        ajax: {
            url: $("#urlSearchCustomer").val(),
            dataType: 'json',
            data: function (params) {
                var query = {
                    search: params.term,
                    type: 1
                }

                // Query parameters will be ?search=[term]&type=public
                return query;
            }, processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.text,
                            id: item.id,
                            attribute:item.attribute,
                        }
                    })
                };
            }
        },
        tags: true
    });
    $('#txtPhone').on('select2:select', function (e) {
        var data = e.params.data;
        if (data.id != data.text) {//Nếu id != text=> trường hợp khách hàng cũ. Đưa thông tin tên lên
            var newState = new Option(data.attribute, data.attribute, true, true);
            $("#txtCustomer").append(newState).trigger('change');
            //$("#txtCustomer").val(data.attribute);
        }
        else {
            if ($("#txtCustomer option:selected").text() != $("#txtCustomer").val()) {//chọn khách hàng mới. thì xóa phone nếu đang phone cũ
                $("#txtCustomer").empty().trigger('change');
            }
        }
    });
    $('.drugName').select2({
        tags: true
    });
    $('.drugName').on('select2:select', function (e) {
        if ($("#TransType").val() == 1) {
            $(this).closest("tr").find(".PriceId").val(numberWithCommas($('.drugName option:selected').attr("InputPrice")));
        }
        else {
            $(this).closest("tr").find(".PriceId").val(numberWithCommas($('.drugName option:selected').attr("OutputPrice")));
        }
        var unitName = $('.drugName option:selected').attr("UnitName");
        var unitNameText = "";
        if (unitName != undefined || unitName != null) {
            unitNameText = $('.drugName option:selected').attr("UnitName");
        }
        $(this).closest("tr").find(".spQuantityAvaiable").html(numberWithCommas($('.drugName option:selected').attr("QuantityAvaiable")) + " - " + unitNameText);
    });
    $(".Quantity").change(function () {
        caculatorPrice($(this));
    });
    $(".Discount").change(function () {
        caculatorPrice($(this));
    });
    $(".PriceId").change(function () {
        caculatorPrice($(this));
    });
    $("#txtDiscount").change(function () {
        CatulatorAmount();
    });
    $("#txtPrice").change(function () {
        CatulatorAmount();
    });
    $(".add-transaction").click(function () {
        var input = $(this);
        var quantity = 0;//input.closest("tr").find(".Quantity").val();
        if ($("#TransType").val() == 1) {//Giao dịch nhập hàng thì lấy số lượng là số lượng package nhập. giao dịch khách thì lấy số lượng là số lượng thực tế lưu vào kho
            quantity = ReplaceNumberWithCommas(input.closest("tr").find(".Quantity").val());
        }
        else {
            quantity = ReplaceNumberWithCommas(input.closest("tr").find(".WarehouseQuantity").val());
        }
        var priceId = ReplaceNumberWithCommas(input.closest("tr").find(".PriceId").val());
        var discount =input.closest("tr").find(".Discount").val();
        var price = quantity * priceId;
        var amount = Math.round(price - price * discount / 100);
        var tranRow = new Object();
        tranRow.DrugName = input.closest("tr").find(".drugName").val();
        tranRow.UnitName = input.closest("tr").find(".Unit option:selected").text();
      
        tranRow.WareHouseUnitName = input.closest("tr").find(".WarehouseUnit option:selected").text();
        tranRow.StringExpireDate = input.closest("tr").find(".ExpireDate").val();
        var tranDetail = new Object();
        tranDetail.Packages = input.closest("tr").find(".Package").val();
        tranDetail.UnitId = input.closest("tr").find(".Unit").val();
        tranDetail.WarehouseQuantity = ReplaceNumberWithCommas(input.closest("tr").find(".WarehouseQuantity").val());
        tranDetail.WarehouseUnit =ReplaceNumberWithCommas(input.closest("tr").find(".WarehouseUnit").val());
        tranDetail.Quantity = quantity;
        tranDetail.Price = price;
        tranDetail.PriceId = priceId;
        tranDetail.Discount = discount;
        tranDetail.Amount = amount;
        tranRow.TranDetail = tranDetail;
        tranRow.TranType = $("#TransType").val();
        btnAddTran(tranRow);
        ClearRowInput(input);
    });
});
function ClearRowInput(input) {
    input.closest("tr").find(".Quantity").val('');
    $(".drugName").val('').trigger('change');
    input.closest("tr").find(".PriceId").val('0');
    input.closest("tr").find(".Discount").val('0');
    input.closest("tr").find(".WarehouseQuantity").val('0');
    input.closest("tr").find(".Package").val('');
    input.closest("tr").find(".Price").val('0');
    input.closest("tr").find(".Amount").val('0');
}
function caculatorPrice(input) {
    var quantity = 0;
    if ($("#TransType").val() == 1) {//Giao dịch nhập hàng thì lấy số lượng là số lượng package nhập. giao dịch khách thì lấy số lượng là số lượng thực tế lưu vào kho
        quantity =ReplaceNumberWithCommas(input.closest("tr").find(".Quantity").val());
    }
    else {
        quantity =ReplaceNumberWithCommas(input.closest("tr").find(".WarehouseQuantity").val());
    }
    var priceId =ReplaceNumberWithCommas(input.closest("tr").find(".PriceId").val());
    var discount = input.closest("tr").find(".Discount").val();
    var amount = ReplaceNumberWithCommas(input.closest("tr").find(".Amount").val());
    var price = quantity * priceId;
    input.closest("tr").find(".Price").val(numberWithCommas(price));
    var amount = Math.round(price - price * discount / 100);
    input.closest("tr").find(".Amount").val(numberWithCommas(amount));
    CaculatorTransactionPrice();
}
function CaculatorTransactionPrice() {
    var temp = 0;
    $(".tran-row").each(function () {
        var input = $(this);
        temp += parseInt(ReplaceNumberWithCommas(input.closest("tr").find(".Amount").val()));
    });
    $("#txtPrice").val(numberWithCommas(temp));
    CatulatorAmount();

}
function CatulatorAmount() {
    var discount = parseFloat($("#txtDiscount").val());
    var amount = Math.round(parseFloat(ReplaceNumberWithCommas($("#txtPrice").val())) - (parseFloat(ReplaceNumberWithCommas($("#txtPrice").val())) * discount) / 100);
    $("#txtAmount").val(numberWithCommas(amount));
}
function btnAddTran(tranRow) {
    $.ajax({
        type: 'POST',
        url: $("#urlAjaxAddTranRow").val(),
        data: jQuery.param({ row: tranRow }),
        async: false,
        success: function (response) {
            $("#tbodyTrans").append(response);
        },
        error: function (error) {
            console.log(error);
            rs = - 1;
        }
    });
    CaculatorTransactionPrice();
}
function btnSave() {
    if (!$("#transactionForm").valid()) {
        return;
    }
    var transaction = new Object();
    var tranObj = new Object();
    tranObj.TransactionCode = $("#txtTransCode").val();
    tranObj.Description = $("#txtTransCode").val();
    tranObj.Type = $("#TransType").val();
    //tranObj.CustomerId = $("#Customer").val();
    tranObj.Phone = $("#txtPhone").val();
    tranObj.Price = ReplaceNumberWithCommas($("#txtPrice").val());
    tranObj.Discount =ReplaceNumberWithCommas($("#txtDiscount").val());
    tranObj.Amount =ReplaceNumberWithCommas($("#txtAmount").val());
    tranObj.Description = $("#txtDescription").val();
    transaction.Transaction = tranObj;
    transaction.CustomerName = $("#txtCustomer").val();
    var tranDetails = new Array();
    $(".tran-row").each(function () {
        var input = $(this);
        var quantity = 0;// input.closest("tr").find(".Quantity").val();
        if ($("#TransType").val() == 1) {//Giao dịch nhập hàng thì lấy số lượng là số lượng package nhập. giao dịch khách thì lấy số lượng là số lượng thực tế lưu vào kho
            quantity =ReplaceNumberWithCommas(input.closest("tr").find(".Quantity").val());
        }
        else {
            quantity =ReplaceNumberWithCommas(input.closest("tr").find(".WarehouseQuantity").val());
        }
        var priceId =ReplaceNumberWithCommas(input.closest("tr").find(".PriceId").val());
        var discount =ReplaceNumberWithCommas(input.closest("tr").find(".Discount").val());
        var price = quantity * priceId;
        var amount = Math.round(price - price * discount / 100);
        var tranRow = new Object();
        tranRow.DrugName = input.closest("tr").find(".DrugName").val();
        tranRow.UnitName = input.closest("tr").find(".Unit option:selected").text();      
        tranRow.WareHouseUnitName = input.closest("tr").find(".WarehouseUnit option:selected").text();
        tranRow.StringExpireDate = input.closest("tr").find(".ExpireDate").val();
        var tranDetail = new Object();
        tranDetail.Packages = input.closest("tr").find(".Packages").val();
        tranDetail.UnitId = input.closest("tr").find(".UnitName").val();
        tranDetail.WarehouseQuantity =ReplaceNumberWithCommas(input.closest("tr").find(".WarehouseQuantity").val());
        tranDetail.WarehouseUnit = input.closest("tr").find(".WarehouseUnit").val();
        tranDetail.Quantity = quantity;
        tranDetail.Price = price;
        tranDetail.PriceId = priceId;
        tranDetail.Discount = discount;
        tranDetail.Amount = amount;
        tranRow.TranDetail = tranDetail;
        tranDetails.push(tranRow);
    });
    transaction.TranDetailInf = tranDetails;
    $.ajax({
        type: 'POST',
        url: $("#urlAjaxSaveTransaction").val(),
        data: jQuery.param({ model: transaction }),
        async: false,
        success: function (response) {
            if (response.resultCode > 0) {
                bootbox.alert("Lưu giao dịch thành công", function () {
                    window.location = $("#urlTransaction").val();
                });
            }
            else {
                bootbox.alert(response.message);
            }
            
            //$("#tbodyTrans").append(response);
        },
        error: function (error) {
            console.log(error);
            rs = - 1;
        }
    });
}
