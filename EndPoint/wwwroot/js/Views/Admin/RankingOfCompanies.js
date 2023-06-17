






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/RankingOfCompanies/Get_RankingOfCompaniess", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + (res.data[i].comany != null ? res.data[i].comany.companyName : '') + "</td><td>" + res.data[i].publishDate + "</td><td>" + res.data[i].longTermRating + "</td><td>" + res.data[i].shortTermRating + "</td><td><a title='ویرایش' href='/Admin/RankingOfCompanies/EditRankingOfCompanies?id=" + res.data[i].rankingId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' class='btn btn-danger fontForAllPage' onclick='Web.RankingOfCompanies.Delete_RankingOfCompanies(" + res.data[i].rankingId + ");'><i class='fa fa-remove'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveRankingOfCompanies(e) {

        $(e).attr("disabled", "");

        AjaxCallActionPostSaveFormWithUploadFile("/api/admin/RankingOfCompanies/Save_RankingOfCompanies", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), true, function (res) {

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
        PersianDatePicker(".DatePicker");
        ComboBoxWithSearch('.select2', 'dir');
        AjaxCallAction("GET", "/api/admin/RankingOfCompanies/Get_RankingOfCompanies/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

            if (res != null) {
                $("#ComanyId").val(res.ComanyId);
                $("#PublishDate").val(res.publishDate);
                $("#LongTermRating").val(res.longTermRating);
                $("#ShortTermRating").val(res.shortTermRating);
                $("#Vision").val(res.Vision);
                $("#RankingId").val(res.rankingId);
                $("#StatusText").val(res.statusText);
                $("#RankingTypeText").val(res.rankingTypeText);
                $("#divDownload").html("<a href='/File/Download?path=" + res.pressReleaseFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                $("#divDownload_SummaryRanking").html("<a href='/File/Download?path=" + res.summaryRankingFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                systemSeting_Combo(!isEmpty(id) && id != 0 ? res : null);
            }
        }, true);

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/Companies/Get_Companiess", JSON.stringify({ Search: null, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strCompany = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    strCompany += " <option value=" + res.data[i].companiesId + ">" + res.data[i].companyName + "</option>";
                }

                $("#ComanyId").html(strCompany);

                if (resSingle != null) {
                    $("#ComanyId").val(resSingle.comanyId);
                }
            }

        }, true);

    }

    function delete_RankingOfCompanies(id) {

        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/admin/RankingOfCompanies/Delete_RankingOfCompanies/" + (isEmpty(id) ? '0' : id), null, true, function (result) {

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


    web.RankingOfCompanies = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveRankingOfCompanies: saveRankingOfCompanies,
        InitRankingOfCompanies: initRankingOfCompanies,
        SystemSeting_Combo: systemSeting_Combo,
        Delete_RankingOfCompanies: delete_RankingOfCompanies
    };

})(Web, jQuery);