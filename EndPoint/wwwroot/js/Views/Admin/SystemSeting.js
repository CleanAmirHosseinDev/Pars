






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ LabeCode: !isEmpty($("#LabeCode").val()) ? $("#LabeCode").val() : null, Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';

                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].label + "</td><td>" + res.data[i].value + "</td><td><a title='ویرایش' href='/Admin/SystemSeting/EditSystemSeting?id=" + res.data[i].systemSetingId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function initSystemSeting(id = null) {

        systemSetingList();
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/SystemSeting/Get_SystemSeting/" + id, null, true, function (res) {

                if (res != null) {
                    $("#SystemSetingID").val(res.systemSetingId);
                    $("#Value").val(res.value);
                    $("#LabeCode").val(res.labeCode);
                    comboBoxWithSearchUpdateText("LabeCode", res.label);
                }
            }, true);
        }

    }

    function systemSetingList(dir = 'rtl') {

        ComboBoxWithSearch('.select2', dir);

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {


                var resA = new Array();

                var strM = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {

                    var q = resA.filter(p => p.labeCode == res.data[i].labeCode);
                    if (q.length > 0) continue;

                    resA.push(res.data[i]);
                    strM += " <option value=" + res.data[i].labeCode + ">" + res.data[i].label + "</option>";
                }
                $("#LabeCode").html(strM);
            }

        }, true);
    }

    function saveSystemSeting(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/SystemSeting/Save_SystemSeting", JSON.stringify({ Value: $("#Value").val(), SystemSetingId: $("#SystemSetingID").val(), LabeCode: $("#LabeCode").val(), label: $("#LabeCode option:selected").text() }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/SystemSeting/Index");

            }

        }, true);

    }


    web.SystemSeting = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        InitSystemSeting: initSystemSeting,
        SystemSetingList: systemSetingList,
        SaveSystemSeting: saveSystemSeting
    };

})(Web, jQuery);