






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/RankingOfCompanies/Get_RankingOfCompaniess", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].RankingOfCompaniesName + "</td><td><a title='ویرایش' href='/Admin/RankingOfCompanies/EditRankingOfCompanies?id=" + res.data[i].RankingOfCompaniesId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveRankingOfCompanies(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/RankingOfCompanies/Save_RankingOfCompanies", JSON.stringify({ RankingOfCompaniesName: $("#RankingOfCompaniesName").val(), RankingOfCompaniesId: $("#RankingOfCompaniesId").val() }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/RankingOfCompanies/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initRankingOfCompanies(id = null) {
        ComboBoxWithSearch('.select2', 'dir');
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/RankingOfCompanies/Get_RankingOfCompanies/" + id, null, true, function (res) {                

                if (res != null) {
                    $("#ComanyID").val(res.comanyId);
                    $("#PublishDate").val(res.publishDate);
                    $("#LongTermRating").val(res.longTermRating);
                    $("#ShortTermRating").val(res.shortTermRating);
                    $("#Vision").val(res.vision);                   
                    systemSeting_Combo(res);
                }
            }, true);
        }
        else {
            systemSeting_Combo(null);
        }

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/Companies/Get_Companiess", JSON.stringify({ Search: null, PageIndex: 1, PageSize: 1 }), true, function (res) {

            if (res.isSuccess) {
                var strCompany = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    strCompany += " <option value=" + res.data[i].companyId + ">" + res.data[i].companyName + "</option>";
                }

                $("#ComanyID").html(strCompany);
               
                if (resSingle != null) {
                    $("#ComanyID").val(resSingle.comanyId);
                }
            }

        }, true);
       
    }


    web.RankingOfCompanies = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveRankingOfCompanies: saveRankingOfCompanies,
        InitRankingOfCompanies: initRankingOfCompanies,
        SystemSeting_Combo: systemSeting_Combo
    };

})(Web, jQuery);