






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
                    var st = "", st2 = "";
                    if (res.data[i].destLevelStepAccessRole == "10" && res.data[i].destLevelStepIndex == "7") {

                        st2 = "<span style='font-size:1.5em'> &#128194;</span> ";
                    }
                    strM += "<tr><td>" + (i + 1) + "</td><td>"
                        + res.data[i].kindOfRequestName + "</td><td>"
                        + res.data[i].dateOfRequestStr + "</td>";
                    if (res.data[i].sendUser!=null) {
                        strM +="<td style='font-size:x-small'>"+ res.data[i].comment + "</td>";
                    } else {
                        strM += "<td></td>";
                    }
                    
                    if (res.data[i].comment.trim() == "عدم تایید قرارداد توسط مشتری") {
                        strM += "<td>" + "<span style='color:red'>&#10060;" + res.data[i].comment + "</span>" + "</td><td>";
                    } else if (res.data[i].destLevelStepIndexButton == "ارجاع به مشتری جهت اصلاح مشخصات اولیه توسط مشتری") {
                        strM += "<td>" + "<span style='color:red'> &#10060; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
                    }
                    else {
                        strM += "<td>" + st2 + res.data[i].levelStepStatus + "</td><td>";
                    }
                    strM += "<a style='margin-right:5px;color:black' href='/Customer/RequestForRating/RequestReferences?id=" + res.data[i].requestId + "'" + " class='btn btn-info fontForAllPage'> <img src='/css/GlobalAreas/dist/img/timeline-icon.png' style='width:20px' title='مشاهده گردش کار'> </a>";
                    if (getlstor("loginName") === res.data[i].destLevelStepAccessRole) {
                        if (res.data[i].destLevelStepIndex == 4) {
                            strM += "<a style='margin-right:5px;' title='تایید قرارداد و بارگذاری قرارداد ' class='btn btn-success fontForAllPage' href='/Customer/RequestForRating/Referral/" + res.data[i].requestId + "'> <i class='fa fa-mail-forward'></i> تایید قرارداد و بارگذاری قرارداد </a>";

                        }

                        if (res.data[i].destLevelStepIndex == 11 && getlstor("loginName") === res.data[i].destLevelStepAccessRole) {
                            strM += "<a style='margin-right:5px;color:black' title='بارگذاری سند تسویه نهایی ' class='btn btn-info fontForAllPage' href='/Customer/RequestForRating/Referral/" + res.data[i].requestId + "'> <i class='fa fa-mail-forward' style='color:black'></i> بارگذاری سند تسویه نهایی </a>";

                        }
                    }
                    if (res.data[i].destLevelStepIndexButton == "ارجاع به مشتری جهت اصلاح مشخصات اولیه توسط مشتری") {
                        strM += "<a style='margin-right:5px;color:black' title='اصلاح/ تکمیل اطلاعات'  class='btn btn-edit fontForAllPage' href='/Customer/Customer/EditCustomer/" + res.data[i].requestId + "'><i class='fa fa-edit'></i> اصلاح/ تکمیل اطلاعات اولیه</a>";
                        strM += "<button style='margin-right:5px;color:black' class='btn btn-info fontForAllPage' type='button'  onclick='Web.RequestForRating.SaveReferralRequestForRatingAgain(this," + res.data[i].requestId + ");'>ارسال مجدد </button >";

                    }
                    if (res.data[i].destLevelStepIndex == 7) {
                        strM += "<a style='margin-right:5px;color:black' title='اطلاعات تکمیلی' class='btn btn-success fontForAllPage' href='/Customer/FurtherInfo/index/" + res.data[i].requestId + "'><i class='fa fa-edit'></i> اطلاعات تکمیلی</a>";

                    } else if (res.data[i].destLevelStepIndex >= 7 || res.data[i].destLevelStepIndexButton === "ارجاع به کارشناس ارزیاب جهت مشاهده اطلاعات تکمیلی") {
                        strM += "<a style='margin-right:5px;color:black' title='اطلاعات تکمیلی' class='btn btn-default fontForAllPage' href='/Customer/FurtherInfo/index/" + res.data[i].requestId + "'><i class='fa fa-eye'></i> اطلاعات تکمیلی</a>";

                    }
                    if (res.data[i].contractDocument!=null) {
                        strM += "<a style='margin-right:5px;color:black' title='اسناد مشتری' class='btn btn-success fontForAllPage' href='/Customer/RequestForRating/RequestDocument/" + res.data[i].requestId + "'><i class='fa fa-file-pdf-o'></i> </a>";
                    }

                  
                    strM += "</td></tr>";
                }

                $("#tBodyList").html(strM);
            }

        }, true);

    }

    function checkIsfinish(id = null) {
        var isFinish = false;
        AjaxCallAction("POST", "/api/customer/RequestForRating/Get_RequestForRatings", JSON.stringify({ Search: null, PageIndex: 1, PageSize: 1, RequestId: id }), true, function (res) {

            if (res.isSuccess) {
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].isFinished) {
                        isFinish = true;
                    }
                }
            }

        }, false);
        return isFinish;

    }

    function registingFirstRequest(successCallback = null) {
        AjaxCallAction("POST", "/api/customer/RequestForRating/Get_RequestForRatings", JSON.stringify({ Search: null, PageIndex: 1, PageSize: 1 }), true, function (res) {

            if (successCallback == null) {

                if (res.isSuccess) {

                    if (res.data.length == 0) {
                        goToUrl("/Customer/Customer/EditCustomer");

                    }
                }

            }
            else {

                successCallback(res);

            }
        }, false);

    }

    function get_Detail(id = null) {
        try {

            AjaxCallAction("GET", "/api/customer/RequestForRating/InitReferral/" + id, null, true, function (res) {

                getU("/css/GlobalAreas/Views/Customer/RequestForRating/P_Referral.html", function (resG) {

                    InitModal_Withot_Par("مشاهده جزییات", resG, "", true, "width:100%;");

                    $("#sdklsslks3498sjdkxhjsd_823sb").val(encrypt(id.toString(), keyMaker()));


                    if (res.isSuccess) {

                        var htmlB = "<div class='form-group'><label for='ReferralExplanation'>توضیح ارجاع</label><textarea class='form-control' id='ReferralExplanation' rows='5'></textarea></div>";
                        for (var i = 0; i < res.data.length; i++) {

                            htmlB += "<button type='button' id='btnReference' class='btn btn-warning ButtonOpperationLSSlss' onclick='Web.RequestForRating.SaveReferralRequestForRating(this);' data-DLSI='" + encrypt(res.data[i].destLevelStepIndex, keyMaker()) + "' data-LSAR='" + encrypt(res.data[i].levelStepAccessRole, keyMaker()) + "' data-LSS='" + encrypt(res.data[i].levelStepStatus, keyMaker()) + "'>" + res.data[i].destLevelStepIndexButton + "</button>";

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

                //  alertB("پیام", res.message, "success");

                goToUrl("/Customer/RequestForRating/Index");

            }
            else {

                alertB("هشدار", res.message, "warning");

            }

        }, true);

    }

    function saveContractAndFinancialDocument(e) {
        if ($("#EvaluationFile").val() != null && $("#EvaluationFile").val() != "") {
            if (document.getElementById("LastFinancialDocument").files.length == 0) {
                alertB("هشدار", "فایل  سند تسویه را انتخاب نکرده اید!.", "warning");
            } else {
                $(e).attr("disabled", "");

                AjaxCallActionPostSaveFormWithUploadFile("/api/customer/RequestForRating/Save_ContractAndFinancialDocuments", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), true, function (res) {

                    $(e).removeAttr("disabled");

                    if (res.isSuccess) {
                        $("#btnreq").click();
                        // alertB("ثبت", "مدارک با موفقیت ثبت شد.", "success");
                        /*$("SeeAllRequest").show();*/
                        //  goToUrl("/Customer/RequestForRating/Index");

                    } else {

                        $("#AmountOsLastSales").val(moneyCommaSepWithReturn($("#AmountOsLastSales").val()));

                        alertB("خطا", res.message, "error");
                    }

                }, true);

            }
        }
        else if (document.getElementById("ContractDocument").files.length == 0 || document.getElementById("FinancialDocument").files.length == 0) {
            alertB("هشدار", "فایل قرارداد یا سند پرداخت را انتخاب نکرده اید!.", "warning");
        }
        else {
            $(e).attr("disabled", "");

            AjaxCallActionPostSaveFormWithUploadFile("/api/customer/RequestForRating/Save_ContractAndFinancialDocuments", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), true, function (res) {

                $(e).removeAttr("disabled");

                if (res.isSuccess) {
                    $("#btnreq").click();
                    // alertB("ثبت", "مدارک با موفقیت ثبت شد.", "success");
                    /*$("SeeAllRequest").show();*/
                    //  goToUrl("/Customer/RequestForRating/Index");

                } else {

                    $("#AmountOsLastSales").val(moneyCommaSepWithReturn($("#AmountOsLastSales").val()));

                    alertB("خطا", res.message, "error");
                }

            }, true);

        }

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

    function initRequestReferences(id = null) {

        AjaxCallAction("POST", "/api/customer/RequestForRating/Get_RequestForRatings", JSON.stringify({ Search: null, PageIndex: 1, PageSize: 1, RequestId: id }), true, function (res) {

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

    function createTimeLine(id = null, isFinish) {

        AjaxCallAction("POST", "/api/customer/RequestForRating/Get_RequestReferencessService", JSON.stringify({ RequestId: id }), true, function (res) {

            if (res != null) {
                var strTimeLine = '';
                var left = false;

                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].levelStepAccessRole == "10" || res.data[i].sendUser == null) {
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
                                //  strTimeLine += "<span>" + res.data[i].roleDesc + ": </span>";
                                //  strTimeLine += "<span class='sender'>" + res.data[i].agentName + "</span>";
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
                            strTimeLine += "[" + res.data[i].levelStepStatus + "]";
                            // strTimeLine += "[ جهت اقدام] <span >[ خوانده شده ] </span><span class='greenColor'>1401/09/21  3:34</span>";
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

    function initReferral(id = null) {

        AjaxCallAction("GET", "/api/Customer/RequestForRating/InitReferral/" + id, null, true, function (res) {

            if (res.isSuccess) {

                getU("/css/GlobalAreas/Views/Customer/RequestForRating/P_Referral.html", function (resG) {

                    $("#divMAS").html(resG);
                    $("#sdklsslks3498sjdkxhjsd_823sa").val(encrypt(id.toString(), keyMaker()));
                    $("#phch").show();

                    var htmlB = "";
                    for (var i = 0; i < res.data.length; i++) {

                        htmlB += "<button type='button' id='btnreq' style='margin:5px' class='btn btn-info ButtonOpperationLSSlss' onclick='Web.RequestForRating.SaveReferralRequestForRating(this);' data-DLSI='" + encrypt(res.data[i].destLevelStepIndex, keyMaker()) + "' data-LSAR='" + encrypt(res.data[i].levelStepAccessRole, keyMaker()) + "' data-LSS='" + encrypt(res.data[i].levelStepStatus, keyMaker()) + "' data-SC='" + encrypt(res.data[i].smsContent, keyMaker()) + "' data-ST='" + res.data[i].smsType + "' data-DLSIB='" + encrypt(res.data[i].destLevelStepIndexButton, keyMaker()) + "'>" + res.data[i].destLevelStepIndexButton + "</button>";

                    }

                    $("#bLLSS").html(htmlB);
                    showContract();
                    // Ckeditor("ReferralExplanation");
                });
            }
            else {

                $("#divMAS").html("<div class='alert alert-error text-center' style='font-size: 20px;'>شما اجازه انجام این عملیات را ندارید</div>");
                $("#phch").hide();
                $("#sdklsslks3498sjdkxhjsd_823sa").val(encrypt('0', keyMaker()));
                $("#bLLSS").html("");
                Ckeditor("ReferralExplanation");
            }

        }, true);

    }

    function getContractInfo(id = null) {

        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("POST", "/api/customer/RequestForRating/Get_RequestForRatings", JSON.stringify({ RequestId: id, Search: null, PageIndex: 1, PageSize: 1, }), true, function (res) {

                if (res.isSuccess) {

                    getU("/css/GlobalAreas/Views/customer/RequestForRating/P_Contract.html", function (resG) {

                        $("#contractInfo").html(resG);
                        $("#RequestId").val(id);
                        for (var i = 0; i < res.data.length; i++) {

                            initContract(res.data[i].requestId);
                        }

                    });

                }

            }, true);
        }
    }

    function getDocument() {
        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/customer/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {
                    if (res.data.contractCode == null) {
                        $("#LoadContractCustomer").remove();

                    } else {
                        $("#frmOkContract").remove();
                        $("#ContractCusmerMsg").remove();
                        $("#FinancialID").val(res.data.financialId);
                        $("#RequestID").val(res.data.requestID);
                        $("#ContentContract").val(res.data.contentContract);
                        $("#PriceContract").val(res.data.priceContract);
                        $("#EvaluationFile").val(res.data.evaluationFile);
                        $("#DisCountMoney").val(res.data.disCountMoney);
                        $("#DicCountPerecent").val(res.data.dicCountPerecent);
                        $("#ContractCode").val(res.data.contractCode);
                        $("#FinalPriceContract").val(res.data.finalPriceContract);
                        $("#ContractDocumentCustomer").val(res.data.contractDocumentCustomer);
                        $("#ContractMainCode").val(res.data.contractMainCode);
                        $("#CommitteeEvaluationFile").val(res.data.committeeEvaluationFile);
                        $("#Tax").val(res.data.tax);
                        if ((res.data.committeeEvaluationFile != null && res.data.committeeEvaluationFile != "") || (res.data.evaluationFile != null && res.data.evaluationFile != "")) {
                            $("#FirstDoc").remove();
                            $("#ContractDocument").val(res.data.contractDocument);
                            $("#FinancialDocument").val(res.data.financialDocument);

                        }
                        else {
                            $("#UpLastFinancialDocument").remove();
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
                    }

                }

            }, true);
        }
    }

    function showDocument() {

        getU("/css/GlobalAreas/Views/customer/RequestForRating/P_Document.html", function (resG) {

            $("#showDocumentRep").html(resG);
            getDocument();

        });
    }

    function initContract(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/customer/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {
                    if (res.data.contractCode != null) {
                        if ((res.data.committeeEvaluationFile != null && res.data.committeeEvaluationFile != "") || (res.data.evaluationFile != null && res.data.evaluationFile != "")) {
                            var strhtml = "<div class='bc'><p> قرارداد نمایش متن</p><div id='divDownloadContractDocumentCustomer' ></div ><br/><br/>";

                            $("#divDownloadContract").html(strhtml);
                            $("#divDownloadContract").show();
                            $("#divDownloadContractDocumentCustomer").html("<a style='margin-left:5px' href='/customer/RequestForRating/ContractPrint/" + id + "' class='btn btn-default fontForAllPage' onclick = 'Web.RequestForRating.PrintContract(this)' > <i class='fa fa-print'></i>   نمایش قرارداد  </a><a style='margin-right:5px' href='/customer/RequestForRating/PrintFactor/" + id + "' class='btn btn-default fontForAllPage' onclick = 'Web.RequestForRating.PrintPerFactor(this)' > <i class='fa fa-print'></i> پیش  فاکتور  </a >");

                        } else {
                            var strhtml = "<div class='bc'><p> قرارداد زیر را چاپ و سپس همه صفحات آن <span style='color:red; font-size:1.5em;'>امضاء شود</span> و سپس آن را از تب <span style='color: forestgreen; font - weight: bolder'>بارگذاری اسناد قرارداد و پرداخت</span> به همراه سند تسویه بارگذاری کنید.</p><div id='divDownloadContractDocumentCustomer' ></div ><br/><br/>";

                            strhtml += "<br/><p>" + " نکته: هنگام چاپ حتما اندازه کاغذ را روی A4 تنظیم کنید و گزینه Background graphics  را انتخاب کنید." + "</p></div>";
                            $("#divDownloadContract").html(strhtml);
                            $("#divDownloadContract").show();
                            $("#divDownloadContractDocumentCustomer").html("<a style='margin-left:5px' href='/customer/RequestForRating/ContractPrint/" + id + "' class='btn btn-default fontForAllPage' onclick = 'Web.RequestForRating.PrintContract(this)' > <i class='fa fa-print'></i>   نمایش قرارداد  </a ><a href='/customer/RequestForRating/PrintFactor/" + id + "' class='btn btn-default fontForAllPage' onclick = 'Web.RequestForRating.PrintPerFactor(this)' > <i class='fa fa-print'></i> پیش  فاکتور  </a ><br/><br/><br/><span>علت عدم تایید</span><input type='text' id='Comment2' name='Comment2' class='form-control' /><button style='margin-right:5px' class='btn btn-edit fontForAllPage' onclick='Web.RequestForRating.SaveReferralRequestForRatingCancel(this," + id + ")'>عدم تایید </button>");

                        }

                    } else {
                        $("#FinancialID").val(res.data.financialId);
                        $("#RequestID").val(res.data.requestID);
                        $("#ContentContract").val(res.data.contentContract);
                        $("#PriceContract").val(res.data.priceContract);

                        $("#DisCountMoney").val(res.data.disCountMoney);
                        $("#DicCountPerecent").val(res.data.dicCountPerecent);
                        $("#ContractCode").val(res.data.contractCode);
                        $("#FinalPriceContract").val(res.data.finalPriceContract);
                        $("#ContractDocumentCustomer").val(res.data.contractDocumentCustomer);

                        var strhtml = "<div class='bc'><a href='/customer/RequestForRating/ContractPrint/" + id + "' class='btn btn-default fontForAllPage' onclick = 'Web.RequestForRating.PrintContract(this)' > <i class='fa fa-print'></i> پیش نمایش نمونه قرارداد  </a ><a style='margin-right:5px' href='/customer/RequestForRating/PrintFactor/" + id + "' class='btn btn-default fontForAllPage' onclick = 'Web.RequestForRating.PrintPerFactor(this)' > <i class='fa fa-print'></i> پیش  فاکتور  </a ><br/><br/>";
                        strhtml += "<p>آیا قرارداد را تایید می کنید؟ در صورت عدم تایید قرارداد، در خواست شما به کارشناس ارسال خواهد شد </p><input type = 'hidden' id ='RequestId' name ='RequestId' value='" + id + "' />";
                        strhtml += " <button type='button' style='margin-left:5px' class='btn btn-success fontForAllPage' onclick='Web.RequestForRating.OkContract(this," + id + ")'>تایید  قرارداد</button><br/><br/><span>علت عدم تایید</span><input type='text' id='Comment2' name='Comment2' class='form-control' /><button style='margin-left:5px' class='btn btn-edit fontForAllPage' onclick='Web.RequestForRating.SaveReferralRequestForRatingCancel(this," + id + ")' type='button'>عدم تایید قرارداد </button><br /></div>";
                        $("#divOkContract").html(strhtml);
                    }
                    $("#ContractShow").html(res.data.contentContract);

                    //$('input[type="text"], textarea').each(function () {
                    //    //  $(this).attr('readonly', 'readonly');
                    //   // var text_classname = $(this).attr('name');
                    //    var value = $(this).val();
                    //    var new_html = ('<storang>' + value + '</storang>');
                    //    $(this).replaceWith(new_html);
                    //});
                    //$(".hClass").remove();
                }

            }, true);
        }


    }

    //function okContract(e) {

    //    var id = $("#RequestId").val();
    //    if (!isEmpty(id) && id != 0) {

    //        var objJ = {};
    //        //objJ.Values = [];
    //        //objJ.Values.push({ Text: "test1", ValueObj:"12345"});;
    //        objJ.FinancialId = 46;

    //        AjaxCallAction("POST", "/api/customer/RequestForRating/Save_ContractAndFinancialDocumentsNoForm", JSON.stringify(objJ), true, function (res) {

    //            if (res.isSuccess) {

    //                if (res.data.contractMainCode != null && res.data.contractMainCode != "") {
    //                    $("#divOkContract").remove();
    //                    var strhtml = "<div class='bc'><p> قرارداد زیر را دانلود و سپس آن را امضاء کنید و سپس آن را از تب <span style='color: forestgreen; font - weight: bolder'>بارگذاری اسناد قرارداد و پرداخت</span> به همراه سند تسویه بارگذاری کنید.</p><div id='divDownloadContractDocumentCustomer' ></div ></div>";
    //                    $("#divDownloadContract").html(strhtml);

    //                    $("#divDownloadContract").show();

    //                    $("#divDownloadContractDocumentCustomer").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.contractDocumentCustomerFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
    //                }
    //            }

    //        }, true);
    //    }
    //}

    function okContract(e, id = null) {


        $(e).removeAttr("disabled");
        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/RequestForRating/Save_ContractAndFinancialDocuments", fill_AjaxCallActionPostSaveFormWithUploadFile("frmOkContract"), true, function (res) {

            if (res.isSuccess) {

                $("#divOkContract").remove();
                var strhtml = "<div class='bc'><p> قرارداد زیر را چاپ و سپس همه صفحات آن <span style='color:red; font-size:1.5em;'>امضاء شود</span> و سپس آن را از تب <span style='color: forestgreen; font - weight: bolder'>بارگذاری اسناد قرارداد و پرداخت</span> به همراه سند تسویه بارگذاری کنید.</p><div id='divDownloadContractDocumentCustomer' ></div ><br/><br/>";
               
                strhtml += "<br/><br/><br/><span>علت عدم تایید</span><input type='text' id='Comment2' name='Comment2' class='form-control' /><button style='margin-right:5px' class='btn btn-edit fontForAllPage' onclick='Web.RequestForRating.SaveReferralRequestForRatingCancel(this," + id + ")'>عدم تایید </button><br/><p>" + " نکته: هنگام چاپ حتما اندازه کاغذ را روی A4 تنظیم کنید و گزینه Background graphics  را انتخاب کنید." + "</p></div>";
                $("#divDownloadContract").html(strhtml);
                $("#divDownloadContract").show();
                $("#divDownloadContractDocumentCustomer").html("<a href='/customer/RequestForRating/ContractPrint/" + id + "' class='btn btn-default fontForAllPage' onclick = 'Web.RequestForRating.PrintContract(this)' > <i class='fa fa-print'></i>   نمایش قرارداد  </a ><a style='margin-right:5px;' href='/customer/RequestForRating/PrintFactor/" + id + "' class='btn btn-default fontForAllPage' onclick = 'Web.RequestForRating.PrintPerFactor(this)' > <i class='fa fa-print'></i> پیش  فاکتور  </a >");

            } else {
                alertB("خطا", res.message, "error");
            }

        }, true);




    }

    function cancelContract(e) {
        var m = $("#Comment2").val()
        $("#Comment").val(m);
        $("#btnreq").click();
    }
    function cancelContracAfterConfirm(e) {
        $("#Comment").val(" درخواست اصلاح قرارداد");
        $("#btnreq").click();
    }

    function printContract(e) {

        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        goToUrl("/customer/RequestForRating/ContractPrint/" + id);
    }
    function printPerFactor(id = null) {
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/customer/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {

                    $("#SaveDate").html(res.data.saveDateStr);
                    $("#PriceFee").html(moneyCommaSepWithReturn(res.data.finalPriceContract.toString()));
                    $("#PriceFee2").html(moneyCommaSepWithReturn(res.data.finalPriceContract.toString()));
                    $("#TaxPrice").html(moneyCommaSepWithReturn(res.data.tax.toString()));
                    $("#TotalPrice").html(moneyCommaSepWithReturn((res.data.finalPriceContract + res.data.tax).toString()));
                    initCustomer();

                }

            }, true);
        }
    }
    function initCustomer() {

        AjaxCallAction("GET", "/api/customer/Customers/Get_Customers/", null, true, function (res) {

            if (res != null) {
                $("#CustomerName2").html(res.companyName);
                $("#CustomerName").html(res.companyName);
                $("#EconomicCode").html(res.economicCode);
                $("#NationalCode").html(res.nationalCode);
                $("#PostalCode").html(res.postalCode);
                $("#AddressCompany").html(res.addressCompany);
                $("#Tel").html(res.tel);
            }
        }, true);

    }

    function printContracting(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/customer/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {

                    $("#ContractShow").html(res.data.contentContract);
                    if (res.data.contractCode != null && res.data.contractCode != "") {
                        $('.PageContractFirst').find('input[name=SaveDate]').val(res.data.saveDateStr);
                        $('.PageContractFirst').find('input[name=ContractCode]').val(res.data.contractCode);


                    } else {
                        $("#ptr").html("<p> این نسخه قرارداد اصلی نمی باشد.</p>");
                    }

                    $("#ContractCode").html(res.data.contractCode);
                    $('input[type="text"], textarea').each(function () {
                        //  $(this).attr('readonly', 'readonly');
                        // var text_classname = $(this).attr('name');
                        var value = $(this).val();
                        var new_html = ('<storang>' + value + '</storang>');
                        $(this).replaceWith(new_html);
                    });
                    if (res.data.contentContract != null && res.data.contentContract != "") {
                        $("#ptr").show();
                    } else {
                        $("#ptr").hide();
                    }

                }

            }, true);
        }
    }
    function printDiv() {
        $("#ptr").hide();
        window.print();
    }

    function showContract() {

        getContractInfo(decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker()));
    }



    function saveReferralRequestForRating(e, Comment = null) {

        $(".ButtonOpperationLSSlss").attr("disabled", "");
        var commenttxt = $("#Comment").val() == null ? Comment : $("#Comment").val();
        var objJ = {};
        objJ.DestLevelStepIndex = decrypt($(e).attr("data-DLSI"), keyMaker());
        objJ.Comment = commenttxt;// Comment; // $("#Comment").val();// getDataCkeditor("ReferralExplanation");
        objJ.LevelStepAccessRole = decrypt($(e).attr("data-LSAR"), keyMaker());
        objJ.LevelStepStatus = decrypt($(e).attr("data-LSS"), keyMaker());
        objJ.Request = {};
        objJ.Request.Requestid = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        objJ.Request.KindOfRequest = '0';
        objJ.SmsContent = decrypt($(e).attr("data-SC"), keyMaker());
        objJ.SmsType = !isEmpty($(e).attr("data-ST")) ? $(e).attr("data-ST") === 'true' : null;
        objJ.DestLevelStepIndexButton = decrypt($(e).attr("data-DLSIB"), keyMaker());

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

    function saveReferralRequestForRatingAgain(e, Requestid = null) {


        var objJ = {};
        objJ.DestLevelStepIndex = "2";
        objJ.Comment = "اطلاعات اصلاح شد";// getDataCkeditor("ReferralExplanation");
        objJ.LevelStepAccessRole = "1";
        objJ.LevelStepStatus = "در انتظار بررسی مشخصات اولیه مشتری توسط مسئول امور ارزیابی";
        objJ.Request = {};
        objJ.Request.Requestid = Requestid;
        objJ.Request.KindOfRequest = '0';
        objJ.SmsContent = null;
        objJ.SmsType = null;
        objJ.DestLevelStepIndexButton = "در انتظار بررسی مشخصات اولیه مشتری توسط مسئول امور ارزیابی";

        AjaxCallAction("POST", "/api/customer/RequestForRating/Save_Request", JSON.stringify(objJ), true, function (res) {


            if (res.isSuccess) {

                alertB("ثبت", "درخواست شما ارسال مجدد ارسال شد", "success");
                goToUrl("/Customer/RequestForRating/Index");
            }
            else {

                alertB("هشدار", res.message, "warning");

            }

        }, true);

    }

    function saveReferralRequestForRatingCancel(e, Requestid = null) {


        var objJ = {};
        objJ.DestLevelStepIndex = "3";
        objJ.Comment = $("#Comment2").val();
        objJ.LevelStepAccessRole = "4";
        objJ.LevelStepStatus = "در انتظار تنظیم و اصلاح قرارداد توسط کارشناس امور قراردادها";
        objJ.Request = {};
        objJ.Request.Requestid = Requestid;
        objJ.Request.KindOfRequest = '0';
        objJ.SmsContent = null;
        objJ.SmsType = null;
        objJ.DestLevelStepIndexButton = "در انتظار تنظیم و اصلاح قرارداد توسط کارشناس امور قراردادها";

        AjaxCallAction("POST", "/api/customer/RequestForRating/Save_Request", JSON.stringify(objJ), true, function (res) {


            if (res.isSuccess) {

                alertB("ثبت", "درخواست شما به مسئول قرارداد ارسال شد منتظر تماس ما باشید", "success", "بله متوجه شدم", function () {

                    goToUrl("/Customer/RequestForRating/Index");

                });

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
        SaveReferralRequestForRating: saveReferralRequestForRating,
        RegistingFirstRequest: registingFirstRequest,
        InitRequestReferences: initRequestReferences,
        InitReferral: initReferral,
        CheckIsfinish: checkIsfinish,
        CreateTimeLine: createTimeLine,
        InitContract: initContract,
        GetContractInfo: getContractInfo,
        ShowContract: showContract,
        PrintContract: printContract,
        SaveContractAndFinancialDocument: saveContractAndFinancialDocument,
        ShowDocument: showDocument,
        GetDocument: getDocument,
        PrintContracting: printContracting,
        OkContract: okContract,
        CancelContract: cancelContract,
        PrintDiv: printDiv,
        CancelContracAfterConfirm: cancelContracAfterConfirm,
        SaveReferralRequestForRatingAgain: saveReferralRequestForRatingAgain,
        PrintPerFactor: printPerFactor,
        InitCustomer: initCustomer,
        SaveReferralRequestForRatingCancel: saveReferralRequestForRatingCancel

    };

})(Web, jQuery);