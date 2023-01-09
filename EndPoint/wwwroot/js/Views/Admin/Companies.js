






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

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].companyName + "</td><td>" + res.data[i].kindOfCompanyNavigation.label + "</td><td>" + res.data[i].companyGroup.label + "</td><td><a title='ویرایش' href='/Admin/Companies/EditCompanies?id=" + res.data[i].companiesId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' class='btn btn-danger fontForAllPage' onclick='Web.Companies.Delete_Companies(" + res.data[i].companiesId + ");'><i class='fa fa-remove'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveCompanies(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/Companies/Save_Companies", JSON.stringify({
            CompanyName: $("#CompanyName").val(),
            CompaniesId: $("#CompaniesId").val(),
            KindOfCompany: $("#KindOfCompany").val(),
            CompanyGroupId: $("#CompanyGroupID").val()

        }), true, function (res) {

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
                    
                    $("#CompaniesId").val(res.companiesId);
                    $("#CompanyName").val(res.companyName);                    //
                    systemSeting_Combo(res);
                }

            }, true);

        }
        else {
            systemSeting_Combo(null);
        }

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "27,126", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';
                var strCompanyGroupID = '<option value="">انتخاب کنید</option>';
               
                  
                for (var i = 0; i < res.data.length; i++) {

                    if (res.data[i].parentCode == 27) {
                        strKindOfCompany += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 126) {
                        strCompanyGroupID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";

                    }
                }

                        $("#KindOfCompany").html(strKindOfCompany);
                $("#CompanyGroupID").html(strCompanyGroupID);
                if (resSingle!=null) {
                    $("#KindOfCompany").val(resSingle.kindOfCompany);
                    $("#CompanyGroupID").val(resSingle.companyGroupId);
                }
                     


            }
        }, true);
    }

    function delete_Companies(id) {

        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/admin/Companies/Delete_Companies/" + (isEmpty(id) ? '0' : id), null, true, function (result) {

                    debuggerWeb();

                    if (result.isSuccess) {

                        filterGrid();

                        alertB("", result.message, "success");

                    }
                    else {

                        alertB("خطا", result.message, "error");

                    }

                }, true);

            }, function () {

            }, ["خیر", "بلی"]);

        } catch (e) {

        }

    }

    web.Companies = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveCompanies: saveCompanies,
        InitCompanies: initCompanies,
        SystemSeting_Combo: systemSeting_Combo,
        Delete_Companies: delete_Companies
    };

})(Web, jQuery);