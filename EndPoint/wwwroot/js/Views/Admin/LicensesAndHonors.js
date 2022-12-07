






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/LicensesAndHonors/Get_LicensesAndHonorss", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].title + "</td><td>" + res.data[i].picture + "</td><td><a title='ویرایش' href='/Admin/LicensesAndHonors/EditLicensesAndHonors?id=" + res.data[i].LicensesAndHonorsId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveLicensesAndHonors(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/LicensesAndHonors/Save_LicensesAndHonors", JSON.stringify({ LicensesAndHonorsName: $("#LicensesAndHonorsName").val(), LicensesAndHonorsId: $("#LicensesAndHonorsId").val() }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/LicensesAndHonors/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initLicensesAndHonors(id = null) {
        ComboBoxWithSearch('.select2', 'dir');

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/LicensesAndHonors/Get_LicensesAndHonors/" + id, null, true, function (res) {

                if (res != null) {
                    
                    $("#LicensesAndHonorsId").val(res.LicensesAndHonorsId);
                    $("#LicensesAndHonorsComment").val(res.LicensesAndHonorsComment);
                    systemSeting_Combo(res);
                }

            }, true);

        }

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "150", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strLicensesAndHonorsTitle = '<option value="">انتخاب کنید</option>';
               
                for (var i = 0; i < res.data.length; i++) {
                   
                    strLicensesAndHonorsTitle += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    
                }
             
                $("#LicensesAndHonorsTitle").html(strLicensesAndHonorsTitle);              
                $("#LicensesAndHonorsTitle").val(resSingle.LicensesAndHonorsTitle);



            }
        }, true);
    }

    web.LicensesAndHonors = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveLicensesAndHonors: saveLicensesAndHonors,
        InitLicensesAndHonors: initLicensesAndHonors,
        SystemSeting_Combo: systemSeting_Combo
    };

})(Web, jQuery);