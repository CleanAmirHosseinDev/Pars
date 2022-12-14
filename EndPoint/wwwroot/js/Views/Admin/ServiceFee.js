






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/ServiceFee/Get_ServiceFees", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].kindOfServiceNavigation.label +
                        "</td><td>" + moneyCommaSepWithReturn(res.data[i].fromCompanyRange.toString()) + "</td><td>" + moneyCommaSepWithReturn(res.data[i].toCompanyRange.toString()) +
                        "</td><td>" + moneyCommaSepWithReturn(res.data[i].fixedCost.toString()) + "</td><td>" + moneyCommaSepWithReturn(res.data[i].variableCost.toString()) +
                        "</td><td><a title='ویرایش' href='/Admin/ServiceFee/EditServiceFee?id=" +
                        res.data[i].serviceFeeId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveServiceFee(e) {

        $(e).attr("disabled", "");

        RemoveAllCharForPrice("FixedCost");
        RemoveAllCharForPrice("VariableCost");

        AjaxCallAction("POST", "/api/admin/ServiceFee/Save_ServiceFee", JSON.stringify({
           
            ServiceFeeId: $("#ServiceFeeId").val(),
           // ServiceFeeComment: $("#ServiceFeeComment").val(),
            FromCompanyRange: $("#FromCompanyRange").val(),
            ToCompanyRange: $("#ToCompanyRange").val(),
            FixedCost: $("#FixedCost").val(),
            VariableCost: $("#VariableCost").val(),
            KindOfService: $("#KindOfService").val(),


        }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/ServiceFee/Index");

            }
            else {

                $("#FixedCost").val(moneyCommaSepWithReturn($("#FixedCost").val()));
                $("#VariableCost").val(moneyCommaSepWithReturn($("#VariableCost").val()));

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initServiceFee(id = null) {
        ComboBoxWithSearch('.select2', 'dir');

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/ServiceFee/Get_ServiceFee/" + id, null, true, function (res) {

                if (res != null) {
                    
                    $("#ServiceFeeId").val(res.serviceFeeId);                   
                    $("#FromCompanyRange").val(res.fromCompanyRange);
                    $("#ToCompanyRange").val(res.toCompanyRange);
                    $("#FixedCost").val(moneyCommaSepWithReturn(res.fixedCost.toString()));
                    $("#VariableCost").val(moneyCommaSepWithReturn(res.variableCost.toString()));
                    systemSeting_Combo(res);
                }

            }, true);

        }
        else {
            systemSeting_Combo(null);
        }

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "63", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfService = '<option value="">انتخاب کنید</option>';
               
                for (var i = 0; i < res.data.length; i++) {
                   
                    strKindOfService += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";                    
                }
             
                $("#KindOfService").html(strKindOfService);
                if (resSingle!=null) {
                    $("#KindOfService").val(resSingle.kindOfService);
                } 
            }
        }, true);
    }

    web.ServiceFee = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveServiceFee: saveServiceFee,
        InitServiceFee: initServiceFee,
        SystemSeting_Combo: systemSeting_Combo
    };

})(Web, jQuery);