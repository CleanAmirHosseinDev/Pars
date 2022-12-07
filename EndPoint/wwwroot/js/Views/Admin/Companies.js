






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/Companies/Get_Companiess", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].companyName + "</td><td>" + res.data[i].kindOfCompanyNavigation.label+ "</td><td>" + res.data[i].companyGroup.label +"</td><td><a title='ویرایش' href='/Admin/Companies/EditCompanies?id=" + res.data[i].companiesId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveCompanies(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/Companies/Save_Companies", JSON.stringify({ CompaniesName: $("#CompaniesName").val(), CompaniesId: $("#CompaniesId").val() }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/Companies/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initCompanies(id = null) {
        ComboBoxWithSearch('.select2', 'dir');

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/Companies/Get_Companies/" + id, null, true, function (res) {

                if (res != null) {
                    
                    $("#CompaniesId").val(res.CompaniesId);
                    $("#CompaniesComment").val(res.CompaniesComment);
                    systemSeting_Combo(res);
                }

            }, true);

        }

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "150", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strCompaniesTitle = '<option value="">انتخاب کنید</option>';
               
                for (var i = 0; i < res.data.length; i++) {
                   
                    strCompaniesTitle += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    
                }
             
                $("#CompaniesTitle").html(strCompaniesTitle);              
                $("#CompaniesTitle").val(resSingle.CompaniesTitle);



            }
        }, true);
    }

    web.Companies = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveCompanies: saveCompanies,
        InitCompanies: initCompanies,
        SystemSeting_Combo: systemSeting_Combo
    };

})(Web, jQuery);