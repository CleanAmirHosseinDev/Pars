






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/AboutUs/Get_AboutUss", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].kindOfServiceNavigation.label + "</td><td>" + res.data[i].fromCompanyRange + "</td><td>" + res.data[i].toCompanyRange + "</td><td>" + res.data[i].fixedCost + "</td><td>" + res.data[i].variableCost + "</td><td><a title='ویرایش' href='/Admin/AboutUs/EditAboutUs?id=" + res.data[i].AboutUsId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveAboutUs(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/AboutUs/Save_AboutUs", JSON.stringify({ AboutUsName: $("#AboutUsName").val(), AboutUsId: $("#AboutUsId").val() }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/AboutUs/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initAboutUs(id = null) {
        ComboBoxWithSearch('.select2', 'dir');

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/AboutUs/Get_AboutUs/" + id, null, true, function (res) {

                if (res != null) {
                    
                    $("#AboutUsId").val(res.AboutUsId);
                    $("#AboutUsComment").val(res.AboutUsComment);
                    systemSeting_Combo(res);
                }

            }, true);

        }

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "150", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strAboutUsTitle = '<option value="">انتخاب کنید</option>';
               
                for (var i = 0; i < res.data.length; i++) {
                   
                    strAboutUsTitle += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    
                }
             
                $("#AboutUsTitle").html(strAboutUsTitle);              
                $("#AboutUsTitle").val(resSingle.AboutUsTitle);



            }
        }, true);
    }

    web.AboutUs = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveAboutUs: saveAboutUs,
        InitAboutUs: initAboutUs,
        SystemSeting_Combo: systemSeting_Combo
    };

})(Web, jQuery);