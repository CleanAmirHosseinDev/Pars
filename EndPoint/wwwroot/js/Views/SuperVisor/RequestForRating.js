﻿var divPageingList_RequestForRatingsSupervisor_pageG = 1;
function successCallBack_divPageingList_RequestForRatingsSupervisor(res) {


    if (res.isSuccess) {
        var n = getlstor("loginName");
        $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            var st = "", st2 = "";
            if (res.data[i].destLevelStepAccessRole == "10" && res.data[i].destLevelStepIndex == "7") {

                st2 = "<span style='font-size:1.5em'> &#128194;</span> ";
            }

            strM += "<tr><td>" + (i + 1) + "</td><td>"
                + res.data[i].requestNo + "</td><td>" 
                + res.data[i].kindOfRequestName + "</td><td>"
                + (!isEmpty(res.data[i].companyName) ? res.data[i].companyName : '') + "</td><td>"
                + res.data[i].nationalCode + "</td><td>"

                + res.data[i].agentMobile + "</td><td>"

                + res.data[i].dateOfRequestStr + "</td>";

            strM += "<td>" + res.data[i].reciveUserName + "</td>"

            if (res.data[i].levelStepSettingIndexID == "29") {
                strM += "<td>" + "<span style='color:red'>&#10060;" + "عدم تایید قرارداد" + "</span>" + "</td><td>";
            } else if (res.data[i].levelStepSettingIndexID == "2") {
                strM += "<td>" + "<span style='color:red'> &#10060; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
            }
            else if (res.data[i].levelStepSettingIndexID == "28") {
                strM += "<td>" + "<span style='color:red'> &#10060; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
            }
            else if (res.data[i].levelStepSettingIndexID == "17") {
                strM += "<td>" + "<span style='color:blue'> &#x2705; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
            }
            else if (res.data[i].levelStepSettingIndexID == "26") {
                strM += "<td>" + "<span style='color:green'> &#x2705; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
            }
            else {
                strM += "<td>" + st2
                    + (res.data[i].levelStepSettingIndexID == "13" ? " &#x2705; " : "")
                    + res.data[i].destLevelStepIndexButton + "</td><td>";
            }

            strM += "<a style='margin-right:5px; color:black' href='/superVisor/RequestForRating/RequestReferences?id=" + res.data[i].requestId + "'" + " class='btn btn-info fontForAllPage'> <img src='/css/GlobalAreas/dist/img/timeline-icon.png' style='width:20px' title='مشاهده گردش کار'>  </a>"
                + (getlstor("loginName") === res.data[i].destLevelStepAccessRole
                    && getlstor("userID") == res.data[i].reciveUser
                    ? "<a style='margin-right:5px;color:black' title='مشاهده و اقدام' class='btn btn-edit fontForAllPage' href='/SuperVisor/RequestForRating/Referral/"
                    + res.data[i].requestId + "'> <i class='fa fa-mail-forward' style='color:black'></i>  </a>" : "<a style='color:black;margin-right:5px; ><i class='fa fa-eye'></i> </a>");

            //if ((n == res.data[i].destLevelStepAccessRole && res.data[i].destLevelStepAccessRole == "5") || (n == "5" && res.data[i].destLevelStepAccessRole == "10" && res.data[i].destLevelStepIndex == "7")) {
            //    strM += "<a style='margin-right:5px;color:black' title='مشاهده اطلاعات تکمیلی' class='btn btn-default fontForAllPage' href='/SuperVisor/FutherInfo/Index/" + res.data[i].requestId + "'><i class='fa fa-file'></i> </a>";
            //}
            if ((n == 8 || n == 1 || n == 5 || n == 4 || n == 9) && Number(res.data[i].levelStepSettingIndexID) >= 12 && Number(res.data[i].levelStepSettingIndexID) != 31 && Number(res.data[i].levelStepSettingIndexID) != 29 && res.data[i].kindOfRequest=="66") {
                strM += "<a style='margin-right:5px;color:black' title='مشاهده اطلاعات تکمیلی' class='btn btn-default fontForAllPage' href='/SuperVisor/FutherInfo/Index/" + res.data[i].requestId + "'><i class='fa fa-id-card-o'></i> </a>";
            }
            if ((n == 12 || n == 11) && Number(res.data[i].levelStepSettingIndexID) > 39 && res.data[i].kindOfRequest == "254") {
                if (Number(res.data[i].levelStepSettingIndexID) >=43) {
                    strM += "<a style='margin-right:5px;color:black' title='خروجی اکسل' class='btn btn-default fontForAllPage' href='/SuperVisor/corporate/Excel_CustomerDataFormReport/" + res.data[i].requestId + "'><i class='fa fa-file-excel-o'></i> </a>";

                }
                strM += "<a style='margin-right:5px;color:black' title='مشاهده پرسشنامه' class='btn btn-default fontForAllPage' href='/SuperVisor/corporate/Index/" + res.data[i].requestId + "'><i class='fa fa-id-card-o'></i> </a>";
            }
            //if ((n == 8 || n == 1 || n == 4 || n == 6 || n == 9) && res.data[i].destLevelStepIndex >= "4" && getlstor("loginName") != res.data[i].destLevelStepAccessRole) {
            else if (res.data[i].kindOfRequest == "254" && res.data[i].destLevelStepIndex >= 102 ) {

                strM += "<a style='margin-right:5px;color:black' title='اسناد مشتری' class='btn btn-success fontForAllPage' href='/SuperVisor/RequestForRating/RequestDocument/" + res.data[i].requestId + "'><i class='fa fa-file-pdf-o'></i> </a>";
            } else if ((res.data[i].kindOfRequest == "66") && (res.data[i].levelStepSettingIndexID >= 4)) {
                strM += "<a style='margin-right:5px;color:black' title='اسناد مشتری' class='btn btn-success fontForAllPage' href='/SuperVisor/RequestForRating/RequestDocument/" + res.data[i].requestId + "'><i class='fa fa-file-pdf-o'></i> </a>";

            }
            if (res.data[i].levelStepSettingIndexID >= 43 && (n=="12" || n=="11")) {
                strM += "<a style='margin-right:5px;color:black' title='نمایش امتیازها' class='btn btn-success fontForAllPage' href='/SuperVisor/corporate/ShowScore/" + res.data[i].requestId + "'><i class='fa fa-star'></i> </a>";
            }
            //  }
            strM += "</td></tr>";
            //if (res.data[i].levelStepIndex >= 7) {


            // }
        }

        $("#tBodyList").html(strM);



    }


}

var divPageingList_RequestForRatingsASuperVisor_pageG = 1;
function successCallBack_divPageingList_RequestForRatingsASuperVisor(res) {


    if (res.isSuccess) {
        var n = getlstor("loginName");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            var st = "", st2 = "";
            if (res.data[i].destLevelStepAccessRole == "10" && res.data[i].destLevelStepIndex == "7") {

                st2 = "<span style='font-size:1.5em'> &#128194;</span> ";
            }

            strM += "<tr><td>" + (i + 1) + "</td><td>"
                + res.data[i].requestNo + "</td><td>"
                + (!isEmpty(res.data[i].companyName) ? res.data[i].companyName : '') + "</td><td>"
                + res.data[i].agentMobile + "</td><td>"
                + res.data[i].dateOfRequestStr + "</td>";

            strM += "<td>" + res.data[i].reciveUserName + "</td>"
            strM += "<td>" + res.data[i].sendTimeStr + "</td>"
            strM += "<td>";

            if (!isEmpty(res.data[i].assessment)) {

                var qe = res.data[i].assessment.split(",");

                if (qe.length == 1) {

                    strM += generateAssessment(qe[0]);

                }
                else if (qe.length == 2) {

                    strM += generateAssessment(qe[0]);

                    strM += "\n" + "<hr/>";

                    switch (qe[1]) {

                        case "0":

                            strM += "<span>امتیاز</span>";

                            break;
                        case "1":

                            strM += "<span>مدت زمان فرایند</span>";

                            break;
                        case "2":

                            strM += "<span>کارشناس ارزیابی</span>";

                            break;
                        case "3":

                            strM += "<span>فرایند صدور قرارداد</span>";

                            break;
                        case "4":

                            strM += "<span>فرایند صدور فاکتور</span>";

                            break;

                    }

                }

            }

            strM += "</td>";

            strM += "<td>";

            if (!isEmpty(res.data[i].reasonAssessment1)) {

                strM += res.data[i].reasonAssessment1;

            }

            strM += "</td>"

            if (res.data[i].levelStepSettingIndexID == "29") {
                strM += "<td>" + "<span style='color:red'>&#10060;" + "عدم تایید قرارداد" + "</span>" + "</td><td>";
            } else if (res.data[i].destLevelStepIndexButton == "ارجاع به مشتری جهت اصلاح مشخصات اولیه توسط مشتری") {
                strM += "<td>" + "<span style='color:red'> &#10060; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
            }
            else {
                strM += "<td>" + st2
                    + (res.data[i].levelStepSettingIndexID == "13" ? " &#x2705; " : "")
                    + res.data[i].levelStepStatus + "</td><td>";
            }
            //if (res.data[i].destLevelStepIndex == "2") {
            //    strM += "<a title='حذف درخواست' class='btn btn-danger style='margin-left:5px' fontForAllPage' onclick='Web.RequestForRating.CancelRequest(" + res.data[i].requestId + ");'><i class='fa fa-remove'></i></a>";
            //}

            strM += "<a style='margin-right:5px; color:black' href='/superVisor/RequestForRating/RequestReferencesA?id=" + res.data[i].requestId + "'" + " class='btn btn-info fontForAllPage'> <img src='/css/GlobalAreas/dist/img/timeline-icon.png' style='width:20px' title='مشاهده گردش کار'>  </a>"
            strM += "</td></tr>";

        }

        $("#tBodyList").html(strM);



    }


}

(function (web, $) {

    //Document Ready  

    function filterGridA() {

        ComboBoxWithSearch('.select2', 'rtl');
        PersianDatePicker(".DatePicker");
        pageingGrid("divPageingList_RequestForRatingsASuperVisor", "/api/superVisor/RequestForRating/Get_RequestForRatingsA", JSON.stringify({
            PageIndex: 1,
            PageSize: $("#cboSelectCount").val(),
            Search: $("#txtSearch").val(),
            DestLevelStepIndex: isEmpty($("#cboSelectLS").val()) ? null : $("#cboSelectLS").val(),
            FromDateStr: $("#FromDateStr").val(),
            ToDateStr: $("#ToDateStr").val(),
            FromSendTimeDateStr: $("#FromSendTimeDateStr").val(),
            ToSendTimeDateStr: $("#ToSendTimeDateStr").val(),
        }));

    }

    function excelTotalFilterGridA() {
        window.open('/SuperVisor/Report/Get_RequestForRatingsA1?FromDateStr=' + $("#FromDateStr").val() +
            '&ToDateStr=' + $("#ToDateStr").val() +
            '&PageIndex=1' +
            '&PageSize=' + $("#cboSelectCount").val() +
            '&DestLevelStepIndex=' + isEmpty($("#cboSelectLS").val()) ? null : $("#cboSelectLS").val() +
            "&Search=" + $("#txtSearch").val(), '_blank');

    }

    function onchangeKindOfRequest(e) {

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_LevelStepSetings", JSON.stringify({
            PageIndex: 0, PageSize: 0, KindOfRequest: !isEmpty($(e).val()) ? $(e).val() : null
        }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    strKindOfCompany += " <option value=" + res.data[i].levelStepIndex + ">" + res.data[i].levelStepStatus + "</option>";
                }

                $("#cboSelectLS").html(strKindOfCompany);



            }

        }, true);

    }

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function fillComboLevelStepSettingList() {
        let requestKind = 0;
        let CorporateAccess = ["11", "12"];
        let ContractAccess = ["4", "9"];

        if (CorporateAccess.includes(getlstor("loginName"))) {
            requestKind = 254;
        }
        else if (ContractAccess.includes(getlstor("loginName"))) {
            requestKind = 1;
        }
        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_LevelStepSetings", JSON.stringify({ PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';
                if (requestKind == 0) {
                    for (var i = 0; i < res.data.length; i++) {
                        if (res.data[i].kindOfRequest != 254 || res.data[i].kindOfRequest != "254")
                            strKindOfCompany += " <option value=" + res.data[i].levelStepIndex + ">" + res.data[i].levelStepStatus + "</option>";
                    }
                }
                if (requestKind == 1) {
                    for (var i = 0; i < res.data.length; i++) {
                        strKindOfCompany += " <option value=" + res.data[i].levelStepIndex + ">" + res.data[i].levelStepStatus + "</option>";
                    }
                }
                else if (requestKind == 254) {
                    for (var i = 0; i < res.data.length; i++) {
                        if (res.data[i].kindOfRequest == 254 || res.data[i].kindOfRequest == "254")
                            strKindOfCompany += " <option value=" + res.data[i].levelStepIndex + ">" + res.data[i].levelStepStatus + "</option>";
                    }
                }

                $("#cboSelectLS").html(strKindOfCompany);

            }

            AjaxCallAction("POST", "/api/superVisor/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "63", PageIndex: 0, PageSize: 0 }), true, function (res) {

                if (res.isSuccess) {
                    var strKindOfRequest = '<option value="">انتخاب کنید</option>';
                    if (requestKind == 0) {
                        for (var i = 0; i < res.data.length; i++) {
                            if (res.data[i].systemSetingId != 254 || res.data[i].systemSetingId != "254")
                                strKindOfRequest += "<option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                        }
                    }
                    else if (requestKind == 1) {
                        for (var i = 0; i < res.data.length; i++) {
                            strKindOfRequest += "<option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                        }
                    }
                    else if (requestKind == 254) {
                        for (var i = 0; i < res.data.length; i++) {
                            if (res.data[i].systemSetingId == 254 || res.data[i].systemSetingId == "254")
                                strKindOfRequest += "<option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                        }
                    }

                    $("#cboKindOfRequest").html(strKindOfRequest);

                }
            }, true);

        }, true);
    }

    function filterGrid() {

        ComboBoxWithSearch('.select2', 'rtl');
        let requestKind = 1;
        let CorporateAccess = ["11", "12"];
        let ContractAccess = ["4", "9"];

        if (CorporateAccess.includes(getlstor("loginName"))) {
            requestKind = 254;
        }
        else if (ContractAccess.includes(getlstor("loginName"))) {
            requestKind = 1;
        }
        console.log(!isEmpty($("#cboKindOfRequest").val()) ? $("#cboKindOfRequest").val() : requestKind == 254 ? 254 : requestKind == 1 ? null : null)
        pageingGrid("divPageingList_RequestForRatingsSupervisor", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify(
            {
                Search: $("#txtSearch").val(),
                PageIndex: 1,
                PageSize: $("#cboSelectCount").val(),
                DestLevelStepIndex: isEmpty($("#cboSelectLS").val()) ? null : $("#cboSelectLS").val(),
                IsMyRequests: $('#IsMyRequests').is(":checked"),
                KindOfRequest: !isEmpty($("#cboKindOfRequest").val()) ? $("#cboKindOfRequest").val() : requestKind == 254 ? 254 : requestKind == 1? null:null
            }));
    }

    function initReferral(id = null) {

        AjaxCallAction("GET", "/api/superVisor/RequestForRating/InitReferral/" + id, null, true, function (res) {

            if (res.isSuccess) {

                getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Referral.html", function (resG) {

                    $("#divMAS").html(resG);

                    ComboBoxWithSearch('.select2', 'rtl');

                    $("#sdklsslks3498sjdkxhjsd_823sa").val(encrypt(id.toString(), keyMaker()));
                    $("#phch").show();
                    var m = getlstor("loginName");
                    if (getlstor("loginName") === "9" || getlstor("loginName") === "8") {
                        $("#svisorShowContract").remove();
                    }

                    if (getlstor("loginName") === "6" || getlstor("loginName") === "5") {
                        $("#svisorShowContract").remove();
                        $("#svisorShowDocument").remove();

                    }

                    if (res.data[0].destLevelStepIndex <= 2 || getlstor("loginName") === "5") {

                        $("#svisorShowDocument").remove();
                    }
                    if (!(getlstor("loginName") === "5"  ||getlstor("loginName") === "8" || getlstor("loginName") === "6" || (getlstor("loginName") === "1" && res.data[0].levelStepIndex >= 8))
                       ) {
                        $("#svisorShowEvaluationFile").remove();
                    }
                    if (((getlstor("loginName") !== "12" && getlstor("loginName") !== "11") || res.data[0].levelStepSettingIndexId <= 39))
                    {
                        $("#svisorShowScore").remove();
                    }
                    var htmlB = "";
                    for (var i = 0; i < res.data.length; i++) {
                        $("#sdklsslks3498sjdkxhjsd_823sb").val(res.data[0].levelStepIndex);
                        htmlB += "<button type='button'  style='margin:5px;border:none;background-color:" + res.data[i].colorButton + "' class='btn btn-info ButtonOpperationLSSlss' onclick='Web.RequestForRating.SaveRequestForRating(this);' data-DLSI='" + encrypt(res.data[i].destLevelStepIndex, keyMaker()) + "' data-LSAR='" + encrypt(res.data[i].levelStepAccessRole, keyMaker()) + "' data-LSS='" + encrypt(res.data[i].levelStepStatus, keyMaker()) + "' data-SC='" + encrypt(res.data[i].smsContent, keyMaker()) + "' data-ST='" + res.data[i].smsType + "' data-DLSIB='" + encrypt(res.data[i].destLevelStepIndexButton, keyMaker()) + "' data-LSSII='" + res.data[i].levelStepSettingIndexId + "'>" + res.data[i].destLevelStepIndexButton + "</button>";

                    }

                    $("#bLLSS").html(htmlB);
                    Ckeditor("ReferralExplanation");
                    showCustomerInfo();
                });


            }
            else {

                $("#divMAS").html("<div class='alert alert-error text-center' style='font-size: 20px;'>" + (isEmpty(res.message) ? "شما اجازه انجام این عملیات را ندارید" : res.message) + "</div>");
                $("#phch").hide();
                $("#sdklsslks3498sjdkxhjsd_823sa").val(encrypt('0', keyMaker()));
                $("#bLLSS").html("");
                Ckeditor("ReferralExplanation");
            }

        }, true);
        //  Ckeditor("ReferralExplanation");


    }

    function printContract(e) {

        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        goToUrl("/superVisor/RequestForRating/ContractPrint/" + id);
    }

    function printContracting(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {

                    $("#ContractShow").html(res.data.contentContract);
                    if (res.data.contractCode != null && res.data.contractCode != "") {
                        $('.PageContractFirst').find('input[name=SaveDate]').val(res.data.saveDateStr);
                        $('.PageContractFirst').find('input[name=ContractCode]').val(res.data.contractCode);


                    } else {
                        $("#ptr").html("<p> این نسخه قرارداد اصلی نمی باشد.</p>");
                    }
                    //$("#SaveDate").html(res.data.saveDateStr);
                    $("#ContractCode").html(res.data.contractCode);
                    $('input[type="text"], textarea').each(function () {
                        //  $(this).attr('readonly', 'readonly');
                        var text_classname = $(this).attr('name');
                        var value = $(this).val();
                        if (text_classname == "NamesAuthorizedSignatories") {
                            var new_html = ('<p style="text-align:center">' + value + '</p>');
                        } else if (text_classname == "NamesAuthorizedSignatories") {

                        }
                        else {
                            var new_html = ('<storang >' + value + '</storang>');
                        }
                        $(this).replaceWith(new_html);
                    });

                }

            }, true);
        }
    }

    function showCustomerInfo() {

        getCustomerInfo();
    }

    function showContract() {

        getContractInfo();
    }

    function initContract(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {

                    $("#FinancialID").val(res.data.financialId);
                    $("#RequestID").val(res.data.requestID);
                    $("#PriceContract").val(moneyCommaSepWithReturn(res.data.priceContract == null ? "" : res.data.priceContract.toString()));
                    $("#FinalPriceContract").val(moneyCommaSepWithReturn(res.data.finalPriceContract == null ? "" : res.data.finalPriceContract.toString()));
                    // $("#FinancialDocument").val(res.data.financialDocument);
                    // $("#ContractDocument").val(res.data.contractDocument);
                    $("#btnDelContract").removeAttr("disabled");
                    $("#DisCountMoney").val(moneyCommaSepWithReturn(res.data.disCountMoney == null ? "" : res.data.disCountMoney.toString()));
                    $("#DicCountPerecent").val(res.data.dicCountPerecent);
                    $("#ContractCode").val(res.data.contractCode);
                    $("#SaveDate").val(res.data.contractCode != null ? res.data.saveDateStr : "");
                    $("#CanSeePreFactorStr").prop("checked", res.data.canSeePreFactor);
                    if (getlstor("loginName") === "1" || getlstor("loginName") === "4") {
                        if (res.data.contractDocumentCustomer != null && res.data.contractDocumentCustomer != "") {
                            $("#divDownloadContractDocumentCustomer").html("<a  href='" + res.data.contractDocumentCustomerFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#ContentCKeditor").html("<textarea name='ContentContract' id='ContentContract'>" + res.data.contentContract + "</textarea>");
                        Ckeditor("ContentContract");

                    }
                    else {

                        $("#ContractShow").addClass("ContractShowStyle");
                        $("#ContractShow").html(res.data.contentContract);
                        $('input[type="text"], textarea').each(function () {
                            //  $(this).attr('readonly', 'readonly');
                            // var text_classname = $(this).attr('name');
                            var value = $(this).val();
                            var new_html = ('<storang id="' + text_classname + '">' + value + '</storang>')
                            $(this).replaceWith(new_html);
                        });
                        $(".hClass").remove();
                    }

                }
                else {

                    initContractNew(id);
                }
            }, true);
        }
    }



    function getCustomerInfo() {

        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());

        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify({ RequestId: id, Search: null, PageIndex: 1, PageSize: 1, }), true, function (res) {

                if (res.isSuccess) {

                    getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Customer.html", function (resG) {

                        $("#customerInfo").html(resG);
                        for (var i = 0; i < res.data.length; i++) {

                            initCustomer(res.data[i].customerId);
                        }

                    });

                }

            }, true);
        }
    }

    function getContractInfo() {

        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());

        if (!isEmpty(id) && id != 0) {

            if ((getlstor("loginName") === "1") || (getlstor("loginName") === "4")) {
                getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Contract-moa.html", function (resG) {

                    $("#contractInfo").html(resG);
                    //if (getlstor("loginName") === "4" || $("#sdklsslks3498sjdkxhjsd_823sb").val() > 2) {
                    //    $('#FinalPriceContract').prop('readonly', true);
                    //    $('#DisCountMoney').prop('readonly', true);
                    //    $('#DicCountPerecent').prop('readonly', true);

                    //}
                    if (getlstor("loginName") === "1") {
                        $("#ContractUp").hide();
                    }
                    initContract(id);
                });
            } else {
                getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Contract.html", function (resG) {

                    $("#contractInfo").html(resG);
                    // initContract(id);

                });
            }

        }
    }

    function showEditContract() {
        if (getlstor("loginName") == "1" || getlstor("loginName") == "4") {

            var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());

            if (!isEmpty(id) && id != 0) {

                if ((getlstor("loginName") === "1") || (getlstor("loginName") === "4")) {
                    getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Contract-moaEdit.html", function (resG) {

                        //$("#contractInfo").html(resG);
                        //if (getlstor("loginName") === "4" || $("#sdklsslks3498sjdkxhjsd_823sb").val() > 2) {
                        //    $('#FinalPriceContract').prop('readonly', true);

                        //}
                        if (getlstor("loginName") === "1") {
                            $("#ContractUp").hide();
                        }
                        initContract(id);
                    });
                } else {
                    getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Contract.html", function (resG) {

                        $("#contractInfo").html(resG);
                        // initContract(id);

                    });
                }

            }
        }
    }

    function initCustomer(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/Customers/Get_Customers/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res != null) {

                    if (res.customerPersonalityType == "223") {

                        $(".FormIsNotShow").hide();
                        $(".form-group.FormIsShow").show();
                        $(".NotShowRequiredLabel").hide();
                        $("#LabelEconomicCode").html("شماره کارت بازرگانی");
                        $("#LabelTypeGroupCompanies").html("نوع فعالیت");
                        $("#Span_Label_AgentMobile").html("شماره موبایل");

                        $(".divMainScanCustomerNationalCard").show();
                        $(".divMainScanManagerNationalCard").hide();
                    }
                    else {

                        $(".FormIsNotShow").show();
                        $("#LabelEconomicCode").html("شماره ثبت");
                        $(".NotShowRequiredLabel").show();
                        $("#LabelTypeGroupCompanies").html("نوع گروه شرکتها");
                        $("#Span_Label_AgentMobile").html("شماره نماینده");

                        $(".divMainScanCustomerNationalCard").show();
                        $(".divMainScanManagerNationalCard").show();
                    }
                    $("#CutomerName").html("<h4> فرم اطلاعات" + res.companyName == null ? res.agentName : res.companyName + "</h4>");


                    $("#AddressCompany").val(res.addressCompany);
                    $("#CompanyName").val(res.companyName);
                    $("#CeoName").val(res.ceoName);
                    $("#EconomicCode").val(res.economicCode);
                    $("#EconomicCodeReal").val(res.economicCodeReal);
                    $("#NationalCode").val(res.nationalCode);
                    $("#CeoNationalCode").val(res.ceoNationalCode);
                    $("#CeoMobile").val(res.ceoMobile);
                    $("#AgentMobile").val(res.agentMobile);
                    $("#AgentName").val(res.agentName);
                    $("#NamesAuthorizedSignatories").val(res.namesAuthorizedSignatories);
                    $("#CountOfPersonal").val(res.countOfPersonal);
                    $("#Email").val(res.email);
                    $("#Tel").val(res.tel);
                    $("#PostalCode").val(res.postalCode);

                    $("#EmailRepresentative").val(res.emailRepresentative);
                    $("#NationalCodeRepresentative").val(res.nationalCodeRepresentative);

                    $("#AmountOsLastSales").val(moneyCommaSepWithReturn(!isEmpty(res.amountOsLastSales) ? res.amountOsLastSales.toString() : ''));
                    if (res.lastInsuranceList != null && res.lastInsuranceList != "") {
                        $("#divDownload").html("<a class='btn btn-success' href='" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                    }
                    if (res.auditedFinancialStatements != null && res.auditedFinancialStatements != "") {
                        $("#divDownload_AuditedFinancialStatements").html("<a class='btn btn-success' href='" + res.auditedFinancialStatementsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                    }
                    if (res.scanCustomerNationalCard != null && res.scanCustomerNationalCard != "") {
                        $("#divDownload_ScanCustomerNationalCard").html("<a class='btn btn-success' href='" + res.scanCustomerNationalCardFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                    }
                    if (res.scanManagerNationalCard != null && res.scanManagerNationalCard != "") {
                        $("#divDownload_ScanManagerNationalCard").html("<a class='btn btn-success' href='" + res.scanManagerNationalCardFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                    }

                    $("#TypeGroupCompanies").val(res.typeGroupCompaniesName);
                    systemSeting_Combo(res);


                }


            }, true);
        }
    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/superVisor/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "63,27,56", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';
                var strHowGetKnowCompany = '<option value="">انتخاب کنید</option>';
                var strTypeServiceRequestedId = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].parentCode == 56) {
                        strHowGetKnowCompany += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 63) {
                        strTypeServiceRequestedId += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 27) {
                        strKindOfCompany += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                }

                $("#KindOfCompany").html(strKindOfCompany);
                $("#HowGetKnowCompany").html(strHowGetKnowCompany);
                $("#TypeServiceRequestedId").html(strTypeServiceRequestedId);

                $("#HowGetKnowCompany").val(resSingle.howGetKnowCompanyId);
                $("#KindOfCompany").val(resSingle.kindOfCompanyId);
                $("#TypeServiceRequestedId").val(resSingle.typeServiceRequestedId);

            }
        }, true);
    }

    function initContractNew(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ServiceFeeAndCustomerByRequest/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res != null) {
                    $("#btnDelContract").attr("disabled", "");

                    if (res.contract == null) {
                        alertB("خطا", "متن قرارداد وجود ندارد", "error");
                    }
                    else {
                        if (getlstor("loginName") === "1" || (getlstor("loginName") === "4")) {
                            $("#RequestID").val(id);
                            $("#ContentCKeditor").html("<textarea name='ContentContract' id='ContentContract'>" + res.contract.contractText + "</textarea>");
                            var m = res.contract.contractText;
                            // $("#PriceContract").val(removeComaForString($(m).find('input[name="ServiceFeePrice"]').val()));
                            var p = removeComaForString($(m).find('input[name="ServiceFeePrice"]').val());
                            var mm = p.toString().slice(-5);
                            var u = p - mm;
                            $("#FinalPriceContract").val(moneyCommaSepWithReturn(u.toString()));

                            // var my = jQuery.parseHTML(getDataCkeditor("ContentContract"));
                            $("#PriceContract").val($(m).find('input[name="ServiceFeePrice"]').val());
                            $("#ContractShow").hide();
                            $("#ContractShow").html(m);

                            $('input[type="text"], textarea').each(function () {
                                if ($(this).attr("name") == "ServiceFeePrice") {
                                    var new_html = ('<input type="text" name="ServiceFeePrice"' + 'value="' + moneyCommaSepWithReturn(u.toString()) + '"/>');
                                    $(this).replaceWith(new_html);
                                }
                            });
                            Ckeditor("ContentContract");
                            var elements = $("#ContractShow").html();
                            setDataCkeditor('ContentContract', elements);

                            if (res.serviceFee == null) {
                                alertB("خطا", "نرخی برای شرایط مشتری پیدا نشد", "error");
                            }
                        }
                    }


                }

            }, true);

        }


    }
    function initContractNewText() {

        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        $("#EditStatuse").val("7");
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ServiceFeeAndCustomerByRequest/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res != null) {
                    $("#btnDelContract").attr("disabled", "");

                    if (res.contract == null) {
                        alertB("خطا", "متن قرارداد وجود ندارد", "error");
                    }
                    else {
                        if (getlstor("loginName") === "1" || (getlstor("loginName") === "4")) {
                            $("#RequestID").val(id);
                            $("#ContentCKeditor").html("<textarea name='ContentContract' id='ContentContract'>" + res.contract.contractText + "</textarea>");
                            var m = res.contract.contractText;
                            $("#PriceContract").val($(m).find('input[name="ServiceFeePrice"]').val());
                            $("#ContractShow").hide();
                            $("#ContractShow").html(m);

                            $('input[type="text"], textarea').each(function () {
                                if ($(this).attr("name") == "ServiceFeePrice") {
                                    var new_html = ('<input type="text" name="ServiceFeePrice"' + 'value="' + moneyCommaSepWithReturn($("#FinalPriceContract").val()) + '"/>');
                                    $(this).replaceWith(new_html);
                                }
                            });
                            Ckeditor("ContentContract");
                            var elements = $("#ContractShow").html();
                            setDataCkeditor('ContentContract', elements);

                            if (res.serviceFee == null) {
                                alertB("خطا", "نرخی برای شرایط مشتری پیدا نشد", "error");
                            }
                        }
                    }


                }

            }, true);

        }


    }


    function getContractCustomer(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {

                    $("#ContractShow").addClass("ContractShowStyle");
                    $("#ContractShow").html(res.data[0].contentContract);
                    $('input[type="text"], textarea').each(function () {
                        //  $(this).attr('readonly', 'readonly');
                        var text_classname = $(this).attr('name');
                        var value = $(this).val();
                        var new_html = ('<label for="' + text_classname + '" id="' + '">' + value + '</label>');
                        $(this).replaceWith(new_html);
                    });
                }

            }, true);

        }


    }

    function updateContract(e) {
        $(e).attr("disabled", "");
        $("#EditStatuse").val("8");
        AjaxCallActionPostSaveFormWithUploadFile("/api/superVisor/RequestForRating/Save_ContractAndFinancialDocuments", fill_AjaxCallActionPostSaveFormWithUploadFile("frmContract"), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {
                $("#btnreq").click();
                alertB("ثبت", "مدارک با موفقیت ثبت شد.", "success");

            } else {

                $("#AmountOsLastSales").val(moneyCommaSepWithReturn($("#AmountOsLastSales").val()));

                alertB("خطا", res.message, "error");
            }

        }, true);
    }

    function saveRequestForRating(e) {


        if ((decrypt($(e).attr("data-LSAR"), keyMaker()) == '10' ||
            decrypt($(e).attr("data-DLSI"), keyMaker()) == '15' || decrypt($(e).attr("data-DLSI"), keyMaker()) == '0'  ) && $(e).attr("data-LSSII") != "26") {

            $("#hidSeSIRR").val(decrypt($(e).attr("data-LSAR"), keyMaker()));
            $("#SUIRS").html('');
            objE = e;
            tempSaveRFR(e);

        }
        else if ($(e).attr("data-LSSII") == "26") {
            temgetCodalInfo(e)
        }
        else temojsdkjsdjsdkjkjsdjksd(e);
    }


    var objE;
    function temojsdkjsdjsdkjkjsdjksd(e) {


        if ($("#hidSeSIRR").val() != decrypt($(e).attr("data-LSAR"), keyMaker())) {

            $(".ButtonOpperationLSSlss").attr("disabled", "");

            $("#hidSeSIRR").val(decrypt($(e).attr("data-LSAR"), keyMaker()));

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_UsersByRole/" + decrypt($(e).attr("data-LSAR"), keyMaker()), null, true, function (resGet) {

                $(".ButtonOpperationLSSlss").removeAttr("disabled");

                var qD = "<option value=''>انتخاب کاربر</option>";
                if (resGet.isSuccess) {

                    for (var i = 0; i < resGet.data.length; i++) {

                        qD += "<option value='" + resGet.data[i].userId + "'>" + (!isEmpty(resGet.data[i].user) ? resGet.data[i].user.realName : '') + "</option>";
                    }

                    var qContent = "<div class='row'><div class='col-md-12'><div class='form-group'><label class='control-label col-md-3'>انتخاب کاربر</label><div class='col-md-9'><select class='form-control select2' style='width: 100% !important;' name='SUIRS' id='SUIRS'>";
                    qContent += qD;
                    qContent += "</select></div></div></div></div>";
                    objE = e;
                    InitModal_Withot_Par('برای ارجاع کاربر را انتخاب کنید', qContent, "Web.RequestForRating.TempSaveRFR();", false, 'width:40%;', 'ارجاع');

                    ComboBoxWithSearchAndModal();

                }


            });

        }
        else ShowModal();


    }

    function temgetCodalInfo(e) {


        $(".ButtonOpperationLSSlss").attr("disabled", "");

        $("#hidSeSIRR").val(decrypt($(e).attr("data-LSAR"), keyMaker()));

        var qContent = "<div><h3>لطفا مشخصات کدال را وارد نمایید.</h3><label for='CodalNumber'>کد رهگیری:</label><br>";
        qContent += "<input type='text' id='CodalNumber' name='CodalNumber' class='form-control' ><br>";
        qContent += "<label for='CodalDate'>تاریخ کدال:</label><br>";
        qContent += "<input type='text' id='CodalDate' name='CodalDate' class='form-control DatePicker'></div>";

        // $(e).attr("data-LSSII") = "26";
        objE = e;
        InitModal_Withot_Par('مشخصات کدال را وارد نمایید.', qContent, "Web.RequestForRating.TempSaveRFR();", false, 'width:40%;', 'اختتام');

        ShowModal();
        PersianDatePicker(".DatePicker");

    }

    function tempSaveRFR(e) {


        if ($(objE).attr("data-LSSII") == 26 || $(objE).attr("data-LSSII") != 11) {

        }
        else if (isEmpty($('#SUIRS').find(":selected").val()) &&
            decrypt($(objE).attr("data-LSAR"), keyMaker()) != '10' &&
            decrypt($(objE).attr("data-DLSI"), keyMaker()) != '15') {

            alertB("هشدار", "کاربر را انتخاب کنید", "warning");
            return;
        }


        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (!res.isSuccess && ($(objE).attr("data-LSSII") != "2" && (($(objE).attr("data-LSSII") == "4" || $(objE).attr("data-LSSII") == "34")))) {
                    alertB("خطا", " کاربر گرامی شما قرارداد را تایید نکرده اید", "error");
                } else {
                    $(".ButtonOpperationLSSlss").attr("disabled", "");

                    var objJ = {};
                    objJ.DestLevelStepIndex = decrypt($(objE).attr("data-DLSI"), keyMaker());
                    objJ.Comment = getDataCkeditor("ReferralExplanation");
                    objJ.LevelStepAccessRole = decrypt($(objE).attr("data-LSAR"), keyMaker());
                    objJ.LevelStepStatus = decrypt($(objE).attr("data-LSS"), keyMaker());
                    objJ.ReciveUser = !isEmpty($('#SUIRS').find(":selected").val()) ? $('#SUIRS').val() : null;
                    objJ.CodalDate = !isEmpty($('#CodalDate').val()) ? $('#CodalDate').val() : null;
                    objJ.CodalNumber = !isEmpty($('#CodalNumber').val()) ? $('#CodalNumber').val() : null;
                    objJ.Request = {};
                    objJ.Request.Requestid = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
                    objJ.Request.KindOfRequest = 0;
                    objJ.SmsContent = decrypt($(objE).attr("data-SC"), keyMaker());
                    objJ.SmsType = !isEmpty($(objE).attr("data-ST")) ? $(objE).attr("data-ST") === 'true' : null;
                    objJ.DestLevelStepIndexButton = decrypt($(objE).attr("data-DLSIB"), keyMaker());
                    objJ.levelStepSettingIndexId = $(objE).attr("data-LSSII");

                    AjaxCallAction("POST", "/api/superVisor/RequestForRating/Save_Request", JSON.stringify(objJ), true, function (res) {

                        $(".ButtonOpperationLSSlss").removeAttr("disabled");

                        if (res.isSuccess) {
                            //if (objJ.DestLevelStepIndex == "3") {
                            //    var m = getDataCkeditor("ContractText");
                            //    var FeePrice = $(m).find('input[name="ServiceFeePrice"]').val();// 
                            //    // var Price = RemoveAllCharForPrice(FeePrice);
                            //   // saveContractAndFinancialDocuments(objJ.Request.Requestid, getDataCkeditor("ContractText"), FeePrice);
                            //}

                            goToUrl("/SuperVisor/RequestForRating/Index");

                        }
                        else {

                            alertB("هشدار", res.message, "warning");

                        }

                    }, true);

                }

            }, true);
        }


    }

    function showEvaluationFile() {
        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        $("#ContractBox").remove();
        //var stepIndex = decrypt($(e).attr("data-DLSI"), keyMaker());

        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify({ RequestId: id, Search: null, PageIndex: 1, PageSize: 1, }), true, function (res) {

                if (res.isSuccess) {
                    for (var i = 0; i < res.data.length; i++) {

                        if (res.data[i].destLevelStepIndex >= 3) {
                            getShowEvaluationFile(id, res);
                        }
                    }
                }

            }, true);
        }


    }

    function showScoreFile() {
        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
       
        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify({ RequestId: id, Search: null, PageIndex: 1, PageSize: 1, }), true, function (res) {

                if (res.isSuccess) {
                    for (var i = 0; i < res.data.length; i++) {

                        if (res.data[i].destLevelStepIndex >= 3) {
                            getShowScoreFile(id, res);
                        }
                    }
                }

            }, true);
        }


    }

    function showDocument() {
        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        //var stepIndex = decrypt($(e).attr("data-DLSI"), keyMaker());

        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify({ RequestId: id, Search: null, PageIndex: 1, PageSize: 1, }), true, function (res) {

                if (res.isSuccess) {
                    for (var i = 0; i < res.data.length; i++) {

                        if (res.data[i].destLevelStepIndex >= 3) {
                            getShowDoument(id);
                        }
                    }
                }

            }, true);
        }



    }
    function showEditDocument(id = null) {

        $("#sdklsslks3498sjdkxhjsd_823sa").val(encrypt(id.toString(), keyMaker()));
        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatingsA", JSON.stringify({ RequestId: id, Search: null, PageIndex: 1, PageSize: 1, }), true, function (res) {

                if (res.isSuccess) {
                    for (var i = 0; i < res.data.length; i++) {

                        if (res.data[i].destLevelStepIndex >= 2) {
                            getShowDoument(id);
                        }
                    }
                }

            }, true);
        }



    }


    function getShowDoument(id = null) {
        if (!isEmpty(id) && id != 0) {
            getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Document.html", function (resG) {

                $("#showDocument").html(resG);
                getDocument(id);

            });
        }
    }

    function getShowEvaluationFile(id = null, res = null) {
        if (!isEmpty(id) && id != 0) {
            getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_EvaluationFile.html", function (resG) {

                $("#showEvaluationFile").html(resG);
                if (res.data[0].levelStepAccessRole == "9" || getlstor("loginName") === "1" || getlstor("loginName") === "8") {
                    $("#frmFormMain2").remove();

                }
                if (getlstor("loginName") === "5") {
                    $("#frmFormMain4").remove();
                    $("#frmFormMain3").remove();
                }
                if (getlstor("loginName") === "1") {
                    $("#frmFormMain2").remove();
                    $("#frmFormMain3").remove();
                }
                if (getlstor("loginName") === "8") {
                    $("#frmFormMain4").remove();
                    $("#frmFormMain2").remove();
                }
                if (getlstor("loginName") === "6") {
                    $("#frmFormMain3").remove();
                    $("#frmFormMain4").remove();
                    $("#frmFormMain2").remove();
                }
                getDocument(id);

            });
        }
    }

    function getDocument(id = null) {
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {
                    $("#FinancialID").val(res.data.financialId);
                    $("#RequestID").val(res.data.requestID);
                    if ((res.data.committeeEvaluationFile != null && res.data.committeeEvaluationFile != "") || res.data.confirmCommitteeEvaluation === "True") {
                        $("#frmFormMain2").remove();
                        $("#frmFormMain4").remove();
                    } else {
                        $("#ConfirmCommitteeEvaluation").val(res.data.confirmCommitteeEvaluation);
                    }
                    if (getlstor("loginName") === "8") {
                        $("#EvaluationFile").val(res.data.evaluationFile);
                    }
                    if (getlstor("loginName") === "6") {
                        $("div.result").remove();
                    }
                    if ((getlstor("loginName") !== "4" && getlstor("loginName") !== "9"  )) {
                        $("#ShowRemainingMoney").remove();

                    } else {
                        $("#RemainingMoney").html(moneyCommaSepWithReturn(!isEmpty(res.data.remainingMoney) ? res.data.remainingMoney.toString() : '0') +" ریال ");
                    }


                    if (res.data.financialDocument != null && res.data.financialDocument != "") {
                        $("#divDownloadFinancialDocument").html("<a class='btn btn-success' href='" + res.data.financialDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownloadFinancialDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.financialDocument2 != null && res.data.financialDocument2 != "") {
                        $("#divDownloadFinancialDocument2").html("<a class='btn btn-success' href='" + res.data.financialDocument2Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownloadFinancialDocument2").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.contractDocument != null && res.data.contractDocument != "") {
                        $("#divDownload_ContractDocument").html("<a class='btn btn-success' href='" + res.data.contractDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_ContractDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.evaluationFile != null && res.data.evaluationFile != "") {
                        $("#divDownload_EvaluationFile").html("<a class='btn btn-success' href='" + res.data.evaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_EvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.committeeEvaluationFile != null && res.data.committeeEvaluationFile != "") {
                        $("#divDownload_CommitteeEvaluationFile").html("<a class='btn btn-success' href='" + res.data.committeeEvaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_CommitteeEvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.lastFinancialDocument != null && res.data.lastFinancialDocument != "") {
                        $("#divDownload_LastFinancialDocument").html("<a class='btn btn-success' href='" + res.data.lastFinancialDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_LastFinancialDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.leaderEvaluationFile != null && res.data.leaderEvaluationFile != "") {
                        $("#divDownload_LeaderEvaluationFile").html("<a class='btn btn-success' href='" + res.data.leaderEvaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_LeaderEvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                }

            }, true);
        }
    }

     function getDocumentScore(id = null) {
        if (!isEmpty(id) && id != 0) {
            $("#RequestID").val(id);
            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {

                    $("#FinancialID").val(res.data.financialId);                  
                 //   $("#EvaluationFile").val(res.data.evaluationFile);
                    if (res.data.evaluationFile != null && res.data.evaluationFile != "") {
                        $("#divDownload_EvaluationFile").html("<a class='btn btn-success' href='" + res.data.evaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_EvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                   
                }

            }, true);
        }
    }


    function getDocumentArzyab(id = null) {
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {
                    $("#FinancialID").val(res.data.financialId);
                    $("#RequestID").val(res.data.requestID);
                    $("#ContentContract").val(res.data.contentContract);
                    $("#PriceContract").val(res.data.priceContract);
                    $("#Tax").val(res.data.tax);
                    // $("#EvaluationFile").val(res.data.evaluationFile);
                    $("#DisCountMoney").val(res.data.disCountMoney);
                    $("#DicCountPerecent").val(res.data.dicCountPerecent);
                    $("#FinalPriceContract").val(res.data.finalPriceContract);
                    $("#ContractDocumentCustomer").val(res.data.contractDocumentCustomer);
                    $("#ContractMainCode").val(res.data.contractMainCode);
                    $("#FinancialDocument").val(res.data.financialDocument);
                    $("#FinancialDocument2").val(res.data.financialDocument2);

                    $("#ContractDocument").val(res.data.contractDocument);
                    $("#ContractCode").val(res.data.contractCode);

                    if (res.data.financialDocument != null && res.data.financialDocument != "") {
                        $("#divDownloadFinancialDocument").html("<a class='btn btn-success' href='" + res.data.financialDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownloadFinancialDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.financialDocument2 != null && res.data.financialDocument2 != "") {
                        $("#divDownloadFinancialDocument2").html("<a class='btn btn-success' href='" + res.data.financialDocument2Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownloadFinancialDocument2").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.contractDocument != null && res.data.contractDocument != "") {
                        $("#divDownload_ContractDocument").html("<a class='btn btn-success' href='" + res.data.contractDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_ContractDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.evaluationFile != null && res.data.evaluationFile != "") {
                        $("#divDownload_EvaluationFile").html("<a class='btn btn-success' href='" + res.data.evaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_EvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.committeeEvaluationFile != null && res.data.committeeEvaluationFile != "") {
                        $("#divDownload_CommitteeEvaluationFile").html("<a class='btn btn-success' href='" + res.data.committeeEvaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_CommitteeEvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                }

            }, true);
        }
    }

    function saveContractAndFinancialDocuments(e, IsShowMsg = null) {

        $(e).attr("disabled", "");

        RemoveAllCharForPrice("DisCountMoney");
        $("#ContentContract").val(getDataCkeditor("ContentContract"));
        AjaxCallActionPostSaveFormWithUploadFile("/api/superVisor/RequestForRating/Save_ContractAndFinancialDocuments", fill_AjaxCallActionPostSaveFormWithUploadFile("frmContract"), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {
                if (IsShowMsg) {
                    $("#btnDelContract").removeAttr("disabled");
                    $("#DisCountMoney").val(moneyCommaSepWithReturn($("#DisCountMoney").val()));
                    $("#FinalPriceContract").val(moneyCommaSepWithReturn($("#FinalPriceContract").val()));
                    $("#PriceContract").val(moneyCommaSepWithReturn($("#PriceContract").val()));

                    alertB("ثبت", " با موفقیت ثبت شد.", "success");
                    initContract(decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker()));
                }
                else {

                    var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
                    goToUrl("/superVisor/RequestForRating/ContractPrint/" + id);
                }

            } else {

                $("#DisCountMoney").val(moneyCommaSepWithReturn($("#DisCountMoney").val()));

                alertB("خطا", res.message, "error");
            }

        }, true);
    }

    function saveContractAndFinancialDocumentsArzyab(e) {

        if (document.getElementById("EvaluationFile").files.length == 0) {
            alertB("هشدار", "فایل اکسل را انتخاب نکرده اید!.", "warning");
        } else {


            $(e).attr("disabled", "");


            AjaxCallActionPostSaveFormWithUploadFile("/api/superVisor/RequestForRating/Save_ContractAndFinancialDocuments", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain2"), true, function (res) {

                $(e).removeAttr("disabled");

                if (res.isSuccess) {
                    showEvaluationFile();
                    alertB("ثبت", " با موفقیت ثبت شد.", "success");


                } else {

                    alertB("خطا", res.message, "error");
                }

            }, true);
        }
    }
    function saveContractAndFinancialDocumentsCommittee(e) {

        if (document.getElementById("CommitteeEvaluationFile").files.length == 0) {
            alertB("هشدار", "فایل اکسل را انتخاب نکرده اید!.", "warning");
        } else {
            $(e).attr("disabled", "");

            AjaxCallActionPostSaveFormWithUploadFile("/api/superVisor/RequestForRating/Save_ContractAndFinancialDocuments", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain3"), true, function (res) {

                $(e).removeAttr("disabled");

                if (res.isSuccess) {
                    showEvaluationFile();
                    alertB("ثبت", " با موفقیت ثبت شد.", "success");


                } else {

                    alertB("خطا", res.message, "error");
                }

            }, true);
        }
    }

    function saveContractAndFinancialDocumentsLeader(e) {

        var fileInput = $.trim($("#LeaderEvaluationFile").val());
        if (fileInput.length == 0) {
            // if (document.getElementById("LeaderEvaluationFile").files.length == 0) {
            alertB("هشدار", "فایل اکسل را انتخاب نکرده اید!.", "warning");
        } else {
            $(e).attr("disabled", "");
            AjaxCallActionPostSaveFormWithUploadFile("/api/superVisor/RequestForRating/Save_ContractAndFinancialDocuments", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain4"), true, function (res) {

                $(e).removeAttr("disabled");

                if (res.isSuccess) {
                    showEvaluationFile();
                    alertB("ثبت", " با موفقیت ثبت شد.", "success");


                } else {

                    alertB("خطا", res.message, "error");
                }

            }, true);
        }
    }
    //function saveContractAndFinancialDocuments(RequestID = null, ContentContract = null, FeePrice = null, DicCountPerecent = null, DisCountMoney=null) {

    //    AjaxCallAction("POST", "/api/superVisor/RequestForRating/Save_ContractAndFinancialDocuments", JSON.stringify({ RequestID: RequestID, ContentContract: ContentContract, PriceContractStr: FeePrice }), true, function (res) {

    //        if (res.isSuccess) {

    //            // goToUrl("/SuperVisor/RequestForRating/Index");

    //        }
    //        else {

    //            alertB("هشدار", res.message, "warning");

    //        }

    //    }, true);

    //}


    function createTimeLine(id = null, isFinish) {

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestReferencessService", JSON.stringify({ RequestId: id }), true, function (res) {

            if (res != null) {
                var strTimeLine = '';
                var left = false;

                for (var i = 0; i < res.data.length; i++) {

                    var bgColor = i == 0 ? "bg-secondary" : "bg-info";

                    strTimeLine += "<article class='timeline-entry " + (left ? " left-aligned" : "") + "'>";
                    strTimeLine += "<div class='timeline-entry-inner'>";

                    strTimeLine += "<time class='timeline-time' datetime=''>";
                    strTimeLine += "</time>";

                    strTimeLine += "<div class='timeline-icon " + bgColor + "'>";
                    strTimeLine += "<i class='entypo-feather'></i>";
                    strTimeLine += "</div>";

                    strTimeLine += "<div class='timeline-label'>";
                    if (i == 0) {
                        var titleText = res.data[i].companyName == null ? res.data[i].agentName : res.data[i].companyName;
                        //var titleTextAgentMobile = res.data[i].agentMobile == null;

                        $("#CutomerName").html("<h3 style='font-size:20px;font-weight:bold;text-align:right;margin:10px'>گردش کار - " + titleText + "</h3>");
                        //$("#AgentMobile").html("<h4 style='font-size:20px;font-weight:bold;text-align:right;margin:10px'> " + titleTextAgentMobile + "</h4>");

                        strTimeLine += "<span class='smallFontSize' style='font-weight:bold'>";
                        strTimeLine += (res.data[i].agentName == null ? res.data[i].companyName : res.data[i].agentName) + " : [" + res.data[i].kindOfRequestName + "]</span >";

                        strTimeLine += "<div style='float:left;color:green'><span class='date smallFontSize'>( تاریخ: " + res.data[i].sendTimeStr + "</span>";
                        strTimeLine += "<span class='LTRDirection smallFontSize'> ساعت: " + res.data[i].sendTimeTimeStr + " )</span></div>";
                    }
                    else {
                        if (res.data[i].sendUser == null) {
                            strTimeLine += "<span class='boxCounter'>" + i.toString() + "</span>"
                                + (res.data[i].levelStepSettingIndexID == "29" ? " &#x274C; " : "")
                                + (res.data[i].levelStepSettingIndexID == "7" ? " &#x2705; " : "")
                                + (res.data[i].levelStepSettingIndexID == "13" ? " &#x2705; " : "")
                                + "<span class='sender'>ارجاع از: مشتری" + (res.data[i].agentName == null ? " (" + res.data[i].companyName + ")" : " (" + res.data[i].agentName + ")") + "</span><br/>";
                            strTimeLine += " <span>به: " + res.data[i].roleDesReciver + " </span>";
                            strTimeLine += "<span class='sender'>" + " (" + res.data[i].reciverName + ")" + "</span>";
                        }
                        else if (res.data[i].reciverName == null) {
                            strTimeLine += "<span class='boxCounter'>" + i.toString() + "</span>"
                                + (res.data[i].levelStepSettingIndexID == "4" ? " &#x1F4DD;" : "")
                                + (res.data[i].levelStepSettingIndexID == "11" ? " &#x1F4C2;" : "")
                                + (res.data[i].levelStepSettingIndexID == "26" ? " &#x2705;" : "")
                                + " <span> ارجاع از:  " + res.data[i].userRoleDes + " (" + res.data[i].realName + ")"
                                + " </span><br/>";
                            strTimeLine += " <span>به: " + "مشتری" + " </span>";
                            strTimeLine += "<span class='sender'>" + (res.data[i].agentName == null ? " (" + res.data[i].companyName + ")" : " (" + res.data[i].agentName + ")") + "</span>";
                        }
                        else {
                            strTimeLine += "<span class='boxCounter'>" + i.toString() + "</span>"
                                + (res.data[i].levelStepSettingIndexID == "17" ? " &#x2705;" : "")
                                + (res.data[i].levelStepSettingIndexID == "28" ? " &#x274C;" : "")
                                + (res.data[i].levelStepSettingIndexID == "23" ? " &#x1F4CA;" : "")
                                + (res.data[i].roleDesReciver != null ? " <span> ارجاع از:  " + res.data[i].userRoleDes + " (" + res.data[i].realName + ")"
                                    + " </span><br/>" : "");
                            strTimeLine += " <span>به: " + res.data[i].roleDesReciver + " </span>";
                            strTimeLine += "<span class='sender'>" + " (" + res.data[i].reciverName + ")" + "</span>";
                        }
                        strTimeLine += "<br/>";
                        strTimeLine += "<span class='smallFontSize'>";
                        strTimeLine += "[" + res.data[i].destLevelStepIndexButton + "]";
                        // strTimeLine += "[ جهت اقدام] <span >[ خوانده شده ] </span><span class='greenColor'>1401/09/21  3:34</span>";
                        strTimeLine += "</span>";
                        strTimeLine += "<div style='float:left;color:green'><span class='date smallFontSize'>( تاریخ: " + res.data[i].sendTimeStr + "</span>";
                        strTimeLine += "<span class='LTRDirection smallFontSize'> ساعت: " + res.data[i].sendTimeTimeStr + ")</span></div>";


                    }
                    strTimeLine += "<span></span>";
                    if (i != 0) {
                        if (res.data[i].comment != null && res.data[i].comment != "") {

                            strTimeLine += "<div class='customDiv'>توضیحات<hr/>";
                            strTimeLine += "<span class='Transcript'>" + res.data[i].comment + "</span>";
                            strTimeLine += "</div>";
                        }
                    }
                    strTimeLine += "</div>";
                    //res.data[i].kindOfRequestName

                    strTimeLine += "</div>";
                    strTimeLine += "</article>";
                    left = !left;


                }
                if (isFinish) {
                    strTimeLine += "<article class='timeline-entry " + (left ? " left-aligned" : "") + "'>";
                    strTimeLine += "<div class='timeline-entry-inner'>";
                    strTimeLine += "<time class='timeline-time' datetime='2014-01-10T03:45'></time>";
                    strTimeLine += "<div class='timeline-icon bg-secondary'><i class='entypo-feather'></i></div>";
                    strTimeLine += "<div class='timeline-label'>";
                    strTimeLine += "<h2>";
                    strTimeLine += "<span>تایید نهایی و اختتام درخواست </span>";
                    // strTimeLine += "<span class='sender''>مهندس</span>";
                    strTimeLine += "</h2>";
                    strTimeLine += "</div>";
                    strTimeLine += "</article>";
                } else {
                    strTimeLine += "<article class='timeline-entry begin'>";
                    strTimeLine += "<div class='timeline-entry-inner'>";
                    strTimeLine += "<div class='timeline-icon' style='-webkit-transform: rotate(-90deg); -moz-transform: rotate(-90deg);'>";
                    strTimeLine += "<i class='entypo-flight'></i>";
                    strTimeLine += "</div>";
                    strTimeLine += "</div>";
                    strTimeLine += "</article>";
                }

                $("#TimeLine").html(strTimeLine);

            }

        }, true);

    }

    function initRequestReferences(id = null) {

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify({ Search: null, PageIndex: 1, PageSize: 1, RequestId: id, ForTimeLine: true }), true, function (res) {

            if (res.isSuccess) {
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].isFinished) {
                        createTimeLine(id, true);
                    } else {
                        createTimeLine(id, false);
                    }
                }
            }

        }, true);

    }

    function uploadContractCustomer(e) {
        if (document.getElementById("ContractDocumentCustomer").files.length == 0) {
            alertB("هشدار", "فایلی قرارداد را انتخاب نکرده اید!.", "warning");
        } else {
            disconut(e, true);
        }

    }

    function calDisPerecent(e) {

        if (event.key === 'Enter') {
            var disPerecent = $('#DicCountPerecent').val();
            var m = jQuery.parseHTML(getDataCkeditor("ContentContract"));

            var FeePrice = removeComaForString($("#PriceContract").val());// $('#DicCountPerecent').val();//removeComaForString($(m).find('input[name="ServiceFeePrice"]').val());
            if (disPerecent != null && disPerecent != "") {
                let disprice = (((FeePrice * disPerecent) / 100).toFixed(0)).toString();
                var m = FeePrice - disprice;
                var mi = m.toString().slice(-5);
                m = m - mi;
                $('#DisCountMoney').val(moneyCommaSepWithReturn(disprice.toString()));
                $("#FinalPriceContract").val(moneyCommaSepWithReturn(m.toString()));
                // $("#lblDicCountPerecent").html(moneyCommaSepWithReturn(disprice));
                var m = jQuery.parseHTML(getDataCkeditor("ContentContract"));
                $("#ContractShow").hide();
                $("#ContractShow").html(m);
                $('input[type="text"], textarea').each(function () {
                    if ($(this).attr("name") == "ServiceFeePrice") {
                        var new_html = ('<input type="text" name="ServiceFeePrice"' + 'value="' + $("#FinalPriceContract").val() + '"/>');
                        $(this).replaceWith(new_html);
                    }
                });

                var elements = $("#ContractShow").html();
                setDataCkeditor('ContentContract', elements);
            } else {

                $("#lblDicCountPerecent").html(0);
            }
        }
    }

    function calDisRiyal(e) {

        $("#lblDicCountPerecent").html('');
        if (event.key === 'Enter') {
            var disRiyal = removeComaForString($('#DisCountMoney').val());

            var FeePrice = removeComaForString($("#PriceContract").val());// $('#DicCountPerecent').val();//removeComaForString($(m).find('input[name="ServiceFeePrice"]').val());
            if (disRiyal != null && disRiyal != "") {
                var m = FeePrice - disRiyal;
                var mi = m.toString().slice(-5);
                m = m - mi;
                var tr = (disRiyal * 100) / FeePrice;
                $('#DicCountPerecent').val((tr.toFixed(2)).toString());
                $("#FinalPriceContract").val(moneyCommaSepWithReturn(m.toString()));
                $("#RequestID").val(decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker()));
                var my = jQuery.parseHTML(getDataCkeditor("ContentContract"));
                $("#ContractShow").hide();
                $("#ContractShow").html(my);
                $('input[type="text"], textarea').each(function () {
                    if ($(this).attr("name") == "ServiceFeePrice") {
                        var new_html = ('<input type="text" name="ServiceFeePrice"' + 'value="' + $("#FinalPriceContract").val() + '"/>');
                        $(this).replaceWith(new_html);
                    }
                });

                var elements = $("#ContractShow").html();
                setDataCkeditor('ContentContract', elements);

            }
        }
    }

    function disconut(e, ShowMsg = true) {

        $("#FinalPriceContract").val(removeComaForString($("#FinalPriceContract").val()));
        $('#DisCountMoney').val(removeComaForString($('#DisCountMoney').val()));
        $('#DicCountPerecent').val(removeComaForString($('#DicCountPerecent').val()));

        $("#RequestID").val(decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker()));

        saveContractAndFinancialDocuments(e, ShowMsg);
        $("#FinalPriceContract").val(moneyCommaSepWithReturn($("#FinalPriceContract").val()));
        $("#DisCountMoney").val(moneyCommaSepWithReturn($("#DisCountMoney").val()));
        $("#DicCountPerecent").val($("#DicCountPerecent").val());

    }

    function deleteContract(e) {

        let id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف قرارداد دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/superVisor/RequestForRating/Delete_ContractAndFinancialDocuments/" + id, null, true, function (result) {

                    debuggerWeb();

                    if (result.isSuccess) {
                        getContractInfo();
                        alertB("", "کاربر گرامی در نظر داشته باشید قرارداد حذف گردید و قرارداد جدید به صورت اتوماتیک با شرایط جدید برای شما نشان داده می شود . در نظر داشته باشید تار زمانیکه مشتری پروفایل خود را کامل نکرده شما دکمه ثبت را نزنید و در صورت اطمینان از تغییرات مشتری دکمه ثبت قرارداد را بزنید. ", "success");
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

    function deleteContractText() {
        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());

        if (!isEmpty(id) && id != 0) {

            if ((getlstor("loginName") === "1") || (getlstor("loginName") === "4")) {
                getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Contract-moaEdit.html", function (resG) {

                    $("#contractInfo").html(resG);
                    //if (getlstor("loginName") === "4" || $("#sdklsslks3498sjdkxhjsd_823sb").val() > 2) {
                    //    $('#FinalPriceContract').prop('readonly', true);
                    //    $('#DisCountMoney').prop('readonly', true);
                    //    $('#DicCountPerecent').prop('readonly', true);

                    //}
                    if (getlstor("loginName") === "1") {
                        $("#ContractUp").hide();
                    }
                    initContractNew(id);
                });
            } else {
                getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Contract.html", function (resG) {

                    $("#contractInfo").html(resG);
                    // initContract(id);

                });
            }

        }
    }

    $("#frmFrom1 input,textarea").on("focusout", function () {

        $(this).valid();

    });

    function printPerFactoring(e) {

        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        goToUrl("/superVisor/RequestForRating/PrintFactor/" + id);
    }

    function printPerFactor(id = null) {
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {

                    $("#SaveDate").html(res.data.saveDateStr);
                    $("#PriceFee1").html(moneyCommaSepWithReturn(res.data.finalPriceContract.toString()));
                    $("#PriceFee2").html(moneyCommaSepWithReturn(res.data.finalPriceContract.toString()));
                    $("#PriceFee3").html(moneyCommaSepWithReturn(res.data.finalPriceContract.toString()));
                    $("#TaxPrice1").html(moneyCommaSepWithReturn(res.data.tax.toString()));
                    $("#TaxPrice2").html(moneyCommaSepWithReturn(res.data.tax.toString()));
                    $("#TotalPrice").html(moneyCommaSepWithReturn((res.data.finalPriceContract + res.data.tax).toString()));
                    $("#TotalPrice2").html(moneyCommaSepWithReturn((res.data.finalPriceContract + res.data.tax).toString()));
                    $("#TotalPrice3").html(moneyCommaSepWithReturn((res.data.finalPriceContract + res.data.tax).toString()));

                    getCustomerInfoFactor(id);
                }

            }, true);
        }
    }

    function getCustomerInfoFactor(id = null) {

        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify({ RequestId: id, Search: null, PageIndex: 1, PageSize: 1, }), true, function (res) {

                if (res.isSuccess) {
                    for (var i = 0; i < res.data.length; i++) {

                        initCustomerFactor(res.data[i].customerId);
                    }
                }

            }, true);
        }
    }


    function initCustomerFactor(id = null) {

        AjaxCallAction("GET", "/api/superVisor/Customers/Get_Customers/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

            if (res != null) {
                $("#CustomerName2").html(res.companyName);
                $("#CustomerName").html(res.companyName);
                $("#CustomerName3").html(res.companyName);
                $("#EconomicCode").html(res.economicCode);
                $("#EconomicCodeReal").html(res.economicCodeReal);
                $("#NationalCode").html(res.nationalCode);
                $("#PostalCode").html(res.postalCode);
                $("#AddressCompany").html(res.addressCompany);
                $("#Tel").html(res.tel);
            }
        }, true);

    }

    function initRequestReferencesA(id = null) {

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatingsA", JSON.stringify({ Search: null, PageIndex: 1, PageSize: 1, RequestId: id }), true, function (res) {

            if (res.isSuccess) {
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].isFinished) {
                        createTimeLine(id, true);
                    } else {
                        createTimeLine(id, false);
                    }
                }
            }

        }, true);

    }

    function getShowScoreFile(id = null, res = null) {
        if (!isEmpty(id) && id != 0) {
            getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Score.html", function (resG) {

                $("#showScore").html(resG);
                if (getlstor("loginName") === "11") {
                    $("#frmFormMain2").remove();
                }
                getDocumentScore(id);

            });
        }
    }

    function getCustomerRequestInformation(id) {
        if (getlstor("loginName") !== "4") {
            $("#frmRemaining").remove();
        }
        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_CustomerRequestInformationsDto", JSON.stringify(
            { RequestId: id}
        ), true, function (res) {
            if (!isEmpty(res)) {
                $("#CountOfPersonal1").val(res.countOfPersonel);
                $("#AmountOsLastSales1").val(moneyCommaSepWithReturn(!isEmpty(res.amountOfLastSale) ? res.amountOfLastSale.toString() : ''));
                if (res.lastInsuranceListFull != "/FileUpload/Customers/no-photo.png")
                    $("#divDownload1").html("<a class='btn btn-success' href='" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                if (res.lastAuditingTaxListFull != "/FileUpload/Customers/no-photo.png")
                    $("#divDownload_AuditedFinancialStatements1").html("<a class='btn btn-success' href='" + res.lastAuditingTaxListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
            }
        }, true);
    }


    function showCustomerRequestInformation() {
        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_CustomerRequestInformation.html", function (resG) {
            $("#showCustomerRequestInformation").html(resG);
            getCustomerRequestInformation(id);
        });
    }


    function saveRemainingMoney(e) {
        if ($("#Remaining").val() == null || $("#Remaining").val() == "" ) {
            alertB("هشدار", "لطفا مبلغ را وارد نمایید.", "warning");
        } else {
            
            $(e).attr("disabled", "");
            var objJ = {};           
            objJ.Requestid = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());           
            objJ.RemainingMoney = $("#Remaining").val();
            AjaxCallAction("POST", "/api/superVisor/RequestForRating/Save_Remaining", JSON.stringify(objJ), true, function (res) {

                $(e).removeAttr("disabled");
                if (res.isSuccess) {
                    alertB("ثبت", " با موفقیت ثبت شد.", "success");
                }
                else {
                    alertB("هشدار", res.message, "warning");
                }

            }, true);
        }
    }

    web.RequestForRating = {
        GetShowScoreFile: getShowScoreFile,
        GetDocumentScore: getDocumentScore,
        ShowScoreFile: showScoreFile,
        TempSaveRFR: tempSaveRFR,
        FilterGrid: filterGrid,
        TextSearchOnKeyDown: textSearchOnKeyDown,
        InitReferral: initReferral,
        SaveRequestForRating: saveRequestForRating,
        InitRequestReferences: initRequestReferences,
        InitRequestReferencesA: initRequestReferencesA,
        ShowCustomerInfo: showCustomerInfo,
        GetCustomerInfo: getCustomerInfo,
        GetContractInfo: getContractInfo,
        InitCustomer: initCustomer,
        ShowContract: showContract,
        InitContract: initContract,
        CreateTimeLine: createTimeLine,
        SaveContractAndFinancialDocuments: saveContractAndFinancialDocuments,
        GetContractCustomer: getContractCustomer,
        ShowDocument: showDocument,
        GetDocument: getDocument,
        GetShowDoument: getShowDoument,
        ShowEvaluationFile: showEvaluationFile,
        GetShowEvaluationFile: getShowEvaluationFile,
        Disconut: disconut,
        InitContractNew: initContractNew,
        PrintContract: printContract,
        PrintContracting: printContracting,
        UploadContractCustomer: uploadContractCustomer,
        SaveContractAndFinancialDocumentsArzyab: saveContractAndFinancialDocumentsArzyab,
        SaveContractAndFinancialDocumentsCommittee: saveContractAndFinancialDocumentsCommittee,
        SaveContractAndFinancialDocumentsLeader: saveContractAndFinancialDocumentsLeader,
        GetDocumentArzyab: getDocumentArzyab,
        FillComboLevelStepSettingList: fillComboLevelStepSettingList,
        CalDisPerecent: calDisPerecent,
        CalDisRiyal: calDisRiyal,
        PrintPerFactor: printPerFactor,
        PrintPerFactoring: printPerFactoring,
        InitCustomerFactor: initCustomerFactor,
        GetCustomerInfoFactor: getCustomerInfoFactor,
        DeleteContract: deleteContract,
        ShowEditDocument: showEditDocument,
        ShowEditContract: showEditContract,
        DeleteContractText: deleteContractText,
        InitContractNewText: initContractNewText,
        UpdateContract: updateContract,
        OnchangeKindOfRequest: onchangeKindOfRequest,
        FilterGridA: filterGridA,
        ExcelTotalFilterGridA: excelTotalFilterGridA,
        SaveRemainingMoney: saveRemainingMoney,
        ShowCustomerRequestInformation: showCustomerRequestInformation,

    };

})(Web, jQuery);