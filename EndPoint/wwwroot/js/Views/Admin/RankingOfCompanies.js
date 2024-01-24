






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
                $("#TradingSymbol").val(res.tradingSymbol);
                $("#ComanyId").val(res.ComanyId);
                $("#PublishDate").val(res.publishDate);
                $("#LongTermRating").val(res.longTermRating);
                $("#ShortTermRating").val(res.shortTermRating);
                $("#Vision").val(res.Vision);
                $("#RankingId").val(res.rankingId);
                $("#StatusText").val(res.statusText);
                $("#RankingTypeText").val(res.rankingTypeText);
                $("#divDownload").html("<a href='" + res.pressReleaseFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                $("#divDownload_SummaryRanking").html("<a href='" + res.summaryRankingFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                systemSeting_Combo(!isEmpty(id) && id != 0 ? res : null);
                systemSeting_Combo2(res);
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

    function systemSeting_Combo2(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "257,258,259,260,261", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strLongTermRating = '<option value="">انتخاب کنید</option>';//257
                var strShortTermRating = '<option value="">انتخاب کنید</option>';//258
                var strVision = '<option value="">انتخاب کنید</option>';//259
                var strStatusText = '<option value="">انتخاب کنید</option>';//260
                var strRankingTypeText= '<option value="">انتخاب کنید</option>';//261

                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].parentCode == 257) {
                        strLongTermRating += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 258) {
                        strShortTermRating += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 259) {
                        strVision += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 260) {
                        strStatusText += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 261) {
                        strRankingTypeText += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }                  

                }

                $("#LongTermRating").html(strLongTermRating);
                $("#ShortTermRating").html(strShortTermRating);
                $("#Vision").html(strVision);
                $("#StatusText").html(strStatusText);
                $("#RankingTypeText").html(strRankingTypeText);

                if (resSingle != null) {
                    $("#LongTermRating").val(resSingle.longTermRating);
                    $("#ShortTermRating").html(resSingle.shortTermRating);
                    $("#Vision").html(resSingle.vision);
                    $("#StatusText").html(resSingle.statusText);
                    $("#RankingTypeText").html(resSingle.rankingTypeText);
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
        Delete_RankingOfCompanies: delete_RankingOfCompanies,
        SystemSeting_Combo2: systemSeting_Combo2
    };

})(Web, jQuery);