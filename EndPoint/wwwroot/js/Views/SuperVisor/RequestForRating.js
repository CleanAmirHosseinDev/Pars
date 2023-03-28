
(function (web, $) {

    //Document Ready  

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function fillComboLevelStepSettingList() {

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_LevelStepSetings", JSON.stringify({ PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    strKindOfCompany += " <option value=" + res.data[i].levelStepIndex + ">" + res.data[i].levelStepStatus + "</option>";
                }

                $("#cboSelectLS").html(strKindOfCompany);



            }
        }, true);

    }

    function filterGrid() {

        ComboBoxWithSearch('.select2', 'rtl');

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), DestLevelStepIndex: isEmpty($("#cboSelectLS").val()) ? null : $("#cboSelectLS").val() }), true, function (res) {

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
                    if (res.data[i].comment.trim() == "عدم تایید قرارداد توسط مشتری") {
                        strM += "<td>" + "<span style='color:red'>&#10060;" + res.data[i].comment + "</span>" + "</td><td>";
                    } else if (res.data[i].destLevelStepIndexButton =="ارجاع به مشتری جهت اصلاح مشخصات اولیه توسط مشتری") {
                        strM += "<td>" + "<span style='color:red'> &#10060; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
                    }
                    else {
                        strM += "<td>" + st2 + res.data[i].levelStepStatus + "</td><td>";
                    }

                    strM += "<a style='margin-right:5px; color:black' href='/superVisor/RequestForRating/RequestReferences?id=" + res.data[i].requestId + "'" + " class='btn btn-info fontForAllPage'> <img src='/css/GlobalAreas/dist/img/timeline-icon.png' style='width:20px' title='مشاهده گردش کار'>  </a>"
                        + (getlstor("loginName") === res.data[i].destLevelStepAccessRole ? "<a style='margin-right:5px;color:black' title='مشاهده و اقدام' class='btn btn-edit fontForAllPage' href='/SuperVisor/RequestForRating/Referral/" + res.data[i].requestId + "'> <i class='fa fa-mail-forward' style='color:black'></i>  </a>" : "<a style='color:black;margin-right:5px;' title='نمایش پروفایل' href='/SuperVisor/Customers/ShowCustomers?id=" + res.data[i].customerId + "' class='btn btn-default fontForAllPage'><i class='fa fa-eye'></i> </a>");

                    if ((n == res.data[i].destLevelStepAccessRole && res.data[i].destLevelStepAccessRole == "5") || (n == "5" && res.data[i].destLevelStepAccessRole == "10" && res.data[i].destLevelStepIndex == "7")) {
                        strM += "<a style='margin-right:5px;color:black' title='مشاهده اطلاعات تکمیلی' class='btn btn-default fontForAllPage' href='/SuperVisor/FutherInfo/Index/" + res.data[i].requestId + "'><i class='fa fa-file'></i> </a>";
                    }
                    strM += "</td></tr>";
                    //if (res.data[i].levelStepIndex >= 7) {


                    // }
                }

                $("#tBodyList").html(strM);



            }

        }, true);

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
                    if (!(getlstor("loginName") === "5" || getlstor("loginName") === "8" || getlstor("loginName") === "6" || (getlstor("loginName") === "1" && res.data[0].levelStepIndex >= 8)) || res.data[0].destLevelStepIndex < 5) {
                        $("#svisorShowEvaluationFile").remove();
                    }
                    var htmlB = "";
                    for (var i = 0; i < res.data.length; i++) {
                        $("#sdklsslks3498sjdkxhjsd_823sb").val(res.data[0].levelStepIndex);

                        htmlB += "<button type='button'  style='margin:5px' class='btn btn-info ButtonOpperationLSSlss' onclick='Web.RequestForRating.SaveRequestForRating(this);' data-DLSI='" + encrypt(res.data[i].destLevelStepIndex, keyMaker()) + "' data-LSAR='" + encrypt(res.data[i].levelStepAccessRole, keyMaker()) + "' data-LSS='" + encrypt(res.data[i].levelStepStatus, keyMaker()) + "' data-SC='" + encrypt(res.data[i].smsContent, keyMaker()) + "' data-ST='" + res.data[i].smsType + "' data-DLSIB='" + encrypt(res.data[i].destLevelStepIndexButton, keyMaker()) + "'>" + res.data[i].destLevelStepIndexButton + "</button>";

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
        if (id == null) {
            id = $("#RequestID").val();
        }
        goToUrl("/superVisor/RequestForRating/ContractPrint/" + id);
    }

    function printContracting(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {

                    $("#ContractShow").html(res.data.contentContract);
                    if (res.data.contractCode != null && res.data.contractCode != "") {
                        $("#SaveDate").html(res.data.saveDateStr);

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
                    $("#FinancialDocument").val(res.data.financialDocument);
                    $("#ContractDocument").val(res.data.contractDocument);

                    $("#DisCountMoney").val(moneyCommaSepWithReturn(res.data.disCountMoney == null ? "" : res.data.disCountMoney.toString()));
                    $("#DicCountPerecent").val(res.data.dicCountPerecent);

                    if (getlstor("loginName") === "1" || getlstor("loginName") === "4") {
                        if (res.data.contractDocumentCustomer != null && res.data.contractDocumentCustomer != "") {
                            $("#divDownloadContractDocumentCustomer").html("<a  href='/File/Download?path=" + res.data.contractDocumentCustomerFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
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
                    if (getlstor("loginName") === "4" || $("#sdklsslks3498sjdkxhjsd_823sb").val() > 2) {
                        $('#FinalPriceContract').prop('readonly', true);
                        $('#DisCountMoney').prop('readonly', true);
                        $('#DicCountPerecent').prop('readonly', true);

                    }
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
                    }
                    else {

                        $(".FormIsNotShow").show();
                        $("#LabelEconomicCode").html("شماره ثبت");
                        $(".NotShowRequiredLabel").show();
                        $("#LabelTypeGroupCompanies").html("نوع گروه شرکتها");
                    }

                    $("#AddressCompany").val(res.addressCompany);
                    $("#CompanyName").val(res.companyName);
                    $("#CeoName").val(res.ceoName);
                    $("#EconomicCode").val(res.economicCode);
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
                    $("#divDownload").html("<a class='btn btn-success' href='/File/Download?path=" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    $("#divDownload_AuditedFinancialStatements").html("<a class='btn btn-success' href='/File/Download?path=" + res.auditedFinancialStatementsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
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


    function saveRequestForRating(e) {

        if (decrypt($(e).attr("data-LSAR"), keyMaker()) == '10') {

            $("#hidSeSIRR").val(decrypt($(e).attr("data-LSAR"), keyMaker()));
            $("#SUIRS").html('');
            tempSaveRFR(e);

        }
        else {

            if ($("#hidSeSIRR").val() != decrypt($(e).attr("data-LSAR"), keyMaker())) {

                $(".ButtonOpperationLSSlss").attr("disabled", "");

                $("#hidSeSIRR").val(decrypt($(e).attr("data-LSAR"), keyMaker()));

                AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_UsersByRole/" + decrypt($(e).attr("data-LSAR"), keyMaker()), null, true, function (resGet) {

                    $(".ButtonOpperationLSSlss").removeAttr("disabled");

                    var qD = '';
                    if (resGet.isSuccess) {

                        for (var i = 0; i < resGet.data.length; i++) {

                            qD += "<option value='" + resGet.data[i].userId + "'>" + (!isEmpty(resGet.data[i].user) ? resGet.data[i].user.realName : '') + "</option>";

                        }

                        $("#SUIRS").html(qD);

                    }

                    temojsdkjsdjsdkjkjsdjksd(e);


                });

            }
            else temojsdkjsdjsdkjkjsdjksd(e);

        }
        
    }

    function temojsdkjsdjsdkjkjsdjksd(e) {

        if (isEmpty($('#SUIRS').find(":selected").val())) {

            confirmB("", "برای ارجاع هیچ کاربری انتخاب نکردید آیا می خواهید ادامه دهید؟", "warning", function () {

                tempSaveRFR(e);

            }, function () {

            }, ["خیر", "بلی"]);

        }
        else tempSaveRFR(e);

    }

    function tempSaveRFR(e) {

        $(".ButtonOpperationLSSlss").attr("disabled", "");

        var objJ = {};
        objJ.DestLevelStepIndex = decrypt($(e).attr("data-DLSI"), keyMaker());
        objJ.Comment = getDataCkeditor("ReferralExplanation");
        objJ.LevelStepAccessRole = decrypt($(e).attr("data-LSAR"), keyMaker());
        objJ.LevelStepStatus = decrypt($(e).attr("data-LSS"), keyMaker());

        objJ.ReciveUser = !isEmpty($('#SUIRS').find(":selected").val()) ? $('#SUIRS').val().join(',') : null;

        objJ.Request = {};
        objJ.Request.Requestid = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        objJ.Request.KindOfRequest = 0;
        objJ.SmsContent = decrypt($(e).attr("data-SC"), keyMaker());
        objJ.SmsType = !isEmpty($(e).attr("data-ST")) ? $(e).attr("data-ST") === 'true' : null;
        objJ.DestLevelStepIndexButton = decrypt($(e).attr("data-DLSIB"), keyMaker());

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

    function showEvaluationFile() {
        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
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
                    $("#ContentContract").val(res.data.contentContract);
                    $("#PriceContract").val(res.data.priceContract);
                    $("#Tax").val(res.data.tax);
                    if (getlstor("loginName") === "8") {
                        $("#EvaluationFile").val(res.data.evaluationFile);
                    }
                    $("#DisCountMoney").val(res.data.disCountMoney);
                    $("#DicCountPerecent").val(res.data.dicCountPerecent);
                    $("#FinalPriceContract").val(res.data.finalPriceContract);

                    $("#ContractMainCode").val(res.data.contractMainCode);
                    $("#FinancialDocument").val(res.data.financialDocument);
                    $("#ContractDocument").val(res.data.contractDocument);
                    $("#ContractCode").val(res.data.contractCode);
                    if (getlstor("loginName") !== "4" && getlstor("loginName") !== "1") {
                        $("#ContractDocumentCustomer").val(res.data.contractDocumentCustomer);
                    }

                    if (res.data.financialDocument != null && res.data.financialDocument != "") {
                        $("#divDownloadFinancialDocument").html("<a class='btn btn-success' href='/File/Download?path=" + res.data.financialDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownloadFinancialDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.contractDocument != null && res.data.contractDocument != "") {
                        $("#divDownload_ContractDocument").html("<a class='btn btn-success' href='/File/Download?path=" + res.data.contractDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_ContractDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.evaluationFile != null && res.data.evaluationFile != "") {
                        $("#divDownload_EvaluationFile").html("<a class='btn btn-success' href='/File/Download?path=" + res.data.evaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_EvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.committeeEvaluationFile != null && res.data.committeeEvaluationFile != "") {
                        $("#divDownload_CommitteeEvaluationFile").html("<a class='btn btn-success' href='/File/Download?path=" + res.data.committeeEvaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_CommitteeEvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.lastFinancialDocument != null && res.data.lastFinancialDocument != "") {
                        $("#divDownload_LastFinancialDocument").html("<a class='btn btn-success' href='/File/Download?path=" + res.data.lastFinancialDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_LastFinancialDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.leaderEvaluationFile != null && res.data.leaderEvaluationFile != "") {
                        $("#divDownload_LeaderEvaluationFile").html("<a class='btn btn-success' href='/File/Download?path=" + res.data.leaderEvaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_LeaderEvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
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
                    $("#ContractDocument").val(res.data.contractDocument);
                    $("#ContractCode").val(res.data.contractCode);

                    if (res.data.financialDocument != null && res.data.financialDocument != "") {
                        $("#divDownloadFinancialDocument").html("<a class='btn btn-success' href='/File/Download?path=" + res.data.financialDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownloadFinancialDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.contractDocument != null && res.data.contractDocument != "") {
                        $("#divDownload_ContractDocument").html("<a class='btn btn-success' href='/File/Download?path=" + res.data.contractDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_ContractDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.evaluationFile != null && res.data.evaluationFile != "") {
                        $("#divDownload_EvaluationFile").html("<a class='btn btn-success' href='/File/Download?path=" + res.data.evaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_EvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.data.committeeEvaluationFile != null && res.data.committeeEvaluationFile != "") {
                        $("#divDownload_CommitteeEvaluationFile").html("<a class='btn btn-success' href='/File/Download?path=" + res.data.committeeEvaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
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
                    strTimeLine += "<span class='date'>" + res.data[i].sendTimeStr + "</span>";
                    strTimeLine += "<span class='LTRDirection'>" + res.data[i].sendTimeTimeStr + "</span>";
                    strTimeLine += "</time>";

                    strTimeLine += "<div class='timeline-icon " + bgColor + "'>";
                    strTimeLine += "<i class='entypo-feather'></i>";
                    strTimeLine += "</div>";

                    strTimeLine += "<div class='timeline-label'>";
                    if (i == 0) {
                        strTimeLine += "<h2>";
                        strTimeLine += "<span>ایجاد درخواست توسط : </span>";
                        strTimeLine += "<span class='sender'>" + (res.data[i].agentName == null ? res.data[i].companyName : res.data[i].agentName) + " (" + res.data[i].roleDesc + ") " + "</span >";
                        strTimeLine += "</h2>";

                        //  strTimeLine += "<p><strong>گیرنده ها :</strong></p>";
                        strTimeLine += "<ul>";
                        strTimeLine += "<li>";
                        // strTimeLine += "<span class='reciver' > علی احمدی </span >";
                        // strTimeLine += "<span class='PostText'>";                        
                        // strTimeLine += "<text > [رئیس شورا] </text>";                           
                        // strTimeLine += "</span>";
                        strTimeLine += "<span class='smallFontSize'> [" + res.data[i].levelStepStatus + "]</span>";
                        // strTimeLine += "<span class='redColor'>[ خوانده نشده ]</span>";
                        strTimeLine += "</li>";
                        strTimeLine += "</ul>";
                    }
                    else {
                        if (res.data[i].sendUser == null) {
                            // strTimeLine += "<span>" + res.data[i].roleDesc + ": </span>";
                            // strTimeLine += "<span class='sender'>" + res.data[i].agentName + "</span>";
                        } else {
                            strTimeLine += "<span>" + res.data[i].userRoleDes + ": </span>";
                            strTimeLine += "<span class='sender'>" + res.data[i].realName + "</span>";
                        }
                        strTimeLine += "<br/>";
                        //  strTimeLine += "<span> گیرنده ها : </span>";
                        strTimeLine += "<ul>";
                        strTimeLine += "<li>";
                        // strTimeLine += "<span class='reciver'>مینا</span>";
                        // strTimeLine += "<span class='PostText'>";
                        // strTimeLine += "<text> [رئیس شورا] </text>";
                        // strTimeLine += " </span>";
                        strTimeLine += "<span class='smallFontSize'>";
                        if (i > 1) {
                            strTimeLine += "[" + res.data[i].destLevelStepIndexButton + "]";
                            // strTimeLine += "[ جهت اقدام] <span >[ خوانده شده ] </span><span class='greenColor'>1401/09/21  3:34</span>";
                        } else {
                            strTimeLine += "[" + res.data[i].levelStepStatus + "]";
                            // strTimeLine += "[ جهت اقدام] <span >[ خوانده شده ] </span><span class='greenColor'>1401/09/21  3:34</span>";
                        }

                        strTimeLine += "</span>";
                        strTimeLine += "</li>";
                        strTimeLine += "</ul>";

                    }
                    strTimeLine += "<span></span>";
                    if (i == 0) {
                        strTimeLine += "<div class='customDiv'>";
                        strTimeLine += "<span class='Transcript'>" + res.data[i].kindOfRequestName + "</span>";
                        strTimeLine += "</div>";
                    } else {
                        if (res.data[i].comment != null) {

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
                    strTimeLine += "<time class='timeline-time' datetime='2014-01-10T03:45'><span>اختتام درخواست</span></time>";
                    strTimeLine += "<div class='timeline-icon bg-secondary'><i class='entypo-feather'></i></div>";
                    strTimeLine += "<div class='timeline-label'>";
                    strTimeLine += "<h2>";
                    strTimeLine += "<span>اختتام درخواست </span>";
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

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify({ Search: null, PageIndex: 1, PageSize: 1, RequestId: id }), true, function (res) {

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


                $("#RequestID").val(decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker()));
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
        // var dicMoney = removeComaForString($('#DisCountMoney').val());
        // var disPerecent = $('#DicCountPerecent').val();
        // var m = jQuery.parseHTML(getDataCkeditor("ContentContract"));
        // var Total = null;
        // var FeePrice = removeComaForString($("#FinalPriceContract").val());// $('#DicCountPerecent').val();//removeComaForString($(m).find('input[name="ServiceFeePrice"]').val());
        //  if (dicMoney != null && dicMoney != "") {

        //      Total = FeePrice - dicMoney;
        // }
        // else if (disPerecent != null && disPerecent != "") {
        //      let disprice = ((FeePrice * disPerecent) / 100).toString();
        //      let len = disprice.length;
        //      disprice = disprice.slice(0, len - 5) + "000";
        //      Total = FeePrice - disprice;
        //  } else {

        //      Total = FeePrice;
        // }

        // $("#FinalPriceContract").val(Total);
        // Total = moneyCommaSepWithReturn(Total.toString());
        //// $("#DisCountMoney").val(moneyCommaSepWithReturn(dicMoney.toString()));
        $("#RequestID").val(decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker()));
        // $("#ContractShow").hide();
        // $("#ContractShow").html(m);
        // $('input[type="text"], textarea').each(function () {
        //     if ($(this).attr("name") == "ServiceFeePrice") {
        //         var new_html = ('<input type="text" name="ServiceFeePrice"' + 'value="' + Total + '"/>');
        //         $(this).replaceWith(new_html);
        //     }
        // });

        // var elements = $("#ContractShow").html();
        // setDataCkeditor('ContentContract', elements);  
        saveContractAndFinancialDocuments(e, ShowMsg);
        $("#FinalPriceContract").val(moneyCommaSepWithReturn($("#FinalPriceContract").val()));
        $("#DisCountMoney").val(moneyCommaSepWithReturn($("#DisCountMoney").val()));
        $("#DicCountPerecent").val($("#DicCountPerecent").val());

    }



    $("#frmFrom1 input,textarea").on("focusout", function () {

        $(this).valid();

    });



    web.RequestForRating = {
        FilterGrid: filterGrid,
        TextSearchOnKeyDown: textSearchOnKeyDown,
        InitReferral: initReferral,
        SaveRequestForRating: saveRequestForRating,
        InitRequestReferences: initRequestReferences,
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
        CalDisRiyal: calDisRiyal
    };

})(Web, jQuery);