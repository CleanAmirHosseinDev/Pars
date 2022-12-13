






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/customer/RequestForRating/Get_RequestForRatings", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>"
                        + res.data[i].kindOfRequestName + "</td><td>"
                        + res.data[i].dateOfRequestStr + "</td><td>"
                        + res.data[i].levelStepStatus + "</td><td>"
                        + "<a style='margin-right:5px' title='مشاهده جزییات' " + "onclick = 'Web.RequestForRating.Get_Detail(" + res.data[i].requestId + ");'"
                        + " class='btn btn-detail fontForAllPage'> <i class='fa fa-eye'></i></a>"
                        + "<a style='margin-right:5px' title='مشاهده قرارداد' href='/Customer/RequestForRating/ContractPrinting?id=" +
                        res.data[i].requestId + "' class='btn btn-print fontForAllPage'> <i class='fa fa-print'></i></a>"
                        + "<a style='margin-right:5px' title='ثبت اطلاعات تکمیلی' href='#' class='btn btn-info fontForAllPage'> <i class='fa fa-file-text-o'></i></a>"
                        + "</td></tr>";
                }

                $("#tBodyList").html(strM);
            }

        }, true);

    }


    function get_Detail(id = null) {
        try {

            AjaxCallAction("GET", "/api/customer/RequestForRating/InitReferral/" + id, null, true, function (res) {

                getU("/css/GlobalAreas/Views/Customer/RequestForRating/P_Referral.html", function (resG) {

                    InitModal_Withot_Par("مشاهده جزییات", resG, "", true, "width:100%;");

                    $("#sdklsslks3498sjdkxhjsd_823sa").val(encrypt(id.toString(), keyMaker()));


                    if (res.isSuccess) {

                        var htmlB = "<div class='form-group'><label for='ReferralExplanation'>توضیح ارجاع</label><textarea class='form-control' id='ReferralExplanation' rows='5'></textarea></div>";
                        for (var i = 0; i < res.data.length; i++) {

                            htmlB += "<button type='button' class='btn btn-warning ButtonOpperationLSSlss' onclick='Web.RequestForRating.SaveReferralRequestForRating(this);' data-DLSI='" + encrypt(res.data[i].destLevelStepIndex, keyMaker()) + "' data-LSAR='" + encrypt(res.data[i].levelStepAccessRole, keyMaker()) + "' data-LSS='" + encrypt(res.data[i].levelStepStatus, keyMaker()) + "'>" + res.data[i].destLevelStepIndexButton + "</button>";

                        }

                        $("#bLLSS").html(htmlB);

                    }
                    else {

                        $("#bLLSS").html("");

                    }

                });

            }, true);

        } catch (e) {

        }


    }

    function saveRequestForRating(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/customer/RequestForRating/Save_Request", JSON.stringify({ Request: { KindOfRequest: isEmpty($("#TypeServiceRequestedId").val()) ? null : $("#TypeServiceRequestedId").val() } }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Customer/RequestForRating/Index");

            }
            else {

                alertB("هشدار", res.message, "warning");

            }

        }, true);

    }

    function systemSeting_ComboRequestForRating() {

        AjaxCallAction("POST", "/api/customer/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "63", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strTypeServiceRequestedId = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    strTypeServiceRequestedId += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                }
                $("#TypeServiceRequestedId").html(strTypeServiceRequestedId);
                // $("#TypeServiceRequestedId").val(resSingle.typeServiceRequestedId);
            }
        }, true);
    }

    function initRequestForRating() {


        ComboBoxWithSearch('.select2', 'dir');
        systemSeting_ComboRequestForRating();

    }

    function saveReferralRequestForRating(e) {

        $(".ButtonOpperationLSSlss").attr("disabled", "");

        var objJ = {};
        objJ.DestLevelStepIndex = decrypt($(e).attr("data-DLSI"), keyMaker());
        objJ.Comment = !isEmpty($("#ReferralExplanation").val()) ? $("#ReferralExplanation").val() : null;
        objJ.LevelStepAccessRole = decrypt($(e).attr("data-LSAR"), keyMaker());
        objJ.LevelStepStatus = decrypt($(e).attr("data-LSS"), keyMaker());
        objJ.Request = {};
        objJ.Request.Requestid = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        objJ.Request.KindOfRequest = 0;

        AjaxCallAction("POST", "/api/customer/RequestForRating/Save_Request", JSON.stringify(objJ), true, function (res) {

            $(".ButtonOpperationLSSlss").removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Customer/RequestForRating/Index");

            }
            else {

                alertB("هشدار", res.message, "warning");

            }

        }, true);

    }

    web.RequestForRating = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        SaveRequestForRating: saveRequestForRating,
        InitRequestForRating: initRequestForRating,
        FilterGrid: filterGrid,
        Get_Detail: get_Detail,
        SystemSeting_ComboRequestForRating: systemSeting_ComboRequestForRating,
        SaveReferralRequestForRating: saveReferralRequestForRating
    };

})(Web, jQuery);