
(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/ManagerOfParsKyan/Get_ManagerOfParsKyans", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].nameOfManager + "</td><td>" + res.data[i].title.label + "</td><td>" + res.data[i].position.label + "</td><td><a title='ویرایش' href='/Admin/ManagerOfParsKyan/EditManagerOfParsKyan?id=" + res.data[i].managersId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveManagerOfParsKyan(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/ManagerOfParsKyan/Save_ManagerOfParsKyan", JSON.stringify({ ManagerOfParsKyanName: $("#ManagerOfParsKyanName").val(), ManagerOfParsKyanId: $("#ManagerOfParsKyanId").val() }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/ManagerOfParsKyan/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initManagerOfParsKyan(id = null, dir = 'rtl') {

        ComboBoxWithSearch('.select2', dir);

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/ManagerOfParsKyan/Get_ManagerOfParsKyan/" + id, null, true, function (res) {                

                if (res != null) {

                    $("#ManagerOfParsKyanId").val(res.managersId);
                    $("#ManagerOfParsKyanName").val(res.nameOfManager);
                    $("#TwitterId").val(res.twitterId);
                    $("#ResumeSummary").val(res.resumeSummary);
                    systemSeting_Combo(res);
                }

            }, true);

        }

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "1,142", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strPositionID = '<option value="">انتخاب کنید</option>';
                var strTitleID = '<option value="">انتخاب کنید</option>';
               
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].parentCode == 1) {
                        strPositionID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 142) {
                        strTitleID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } 
                }

                $("#PositionID").html(strPositionID);
                $("#TitleID").html(strTitleID);
               
                $("#PositionID").val(resSingle.positionId);
                $("#TitleID").val(resSingle.titleId);
                

            }
        }, true);
    }

    web.ManagerOfParsKyan = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveManagerOfParsKyan: saveManagerOfParsKyan,
        InitManagerOfParsKyan: initManagerOfParsKyan
    };

})(Web, jQuery);