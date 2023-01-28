






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
                        + "<a style='margin-right:5px;color:black' href='/Customer/RequestForRating/RequestReferences?id=" + res.data[i].requestId + "'" + " class='btn btn-info fontForAllPage'> <img src='/css/GlobalAreas/dist/img/timeline-icon.png' style='width:20px' title='مشاهده گردش کار'> گردش کار</a>";
                        if (getlstor("loginName") === res.data[i].destLevelStepAccessRole) {
                            strM += "<a style='margin-right:5px;color:black' title=' ارسال به پارس کیان' class='btn btn-info fontForAllPage' href='/Customer/RequestForRating/Referral/" + res.data[i].requestId + "'> <i class='fa fa-mail-forward' style='color:black'></i>  ارسال به پارس کیان </a>";
                    }
                    if (res.data[i].destLevelStepIndex >= 7) {

                        strM += "<a style='margin-right:5px;color:black' title='اطلاعات تکمیلی' class='btn btn-info fontForAllPage' href='/Customer/FurtherInfo/index/" + res.data[i].requestId + "'><i class='fa fa-info'></i> اطلاعات تکمیلی</a>";

                    }

                      
                    //if (res.data[i].destLevelStepIndex == 4) {
                    //        strM += "<a style='margin-right:5px' title='مشاهده و ارجاع' href='/Customer/RequestForRating/ContractPrinting?id=" + res.data[i].requestId + "' class='btn btn-print fontForAllPage'> <i class='fa fa-print'></i></a>"

                    //    } else if (res.data[i].destLevelStepIndex == 7) {
                    //        strM += "<a style='margin-right:5px' title='ثبت اطلاعات تکمیلی' href='#' class='btn btn-info fontForAllPage'> <i class='fa fa-file-text-o'></i> ثبت اطلاعات تکمیلی</a>"

                    //    }
                    //    else if (res.data[i].destLevelStepIndex == 11) {
                    //        strM += "<a style='margin-right:5px' title='تسویه حساب' href='#' class='btn btn-info fontForAllPage'> <i class='fa fa-file-text-o'></i> تسویه حساب</a>"

                    //    }
                    strM +="</td></tr>";
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

    function saveContractAndFinancialDocument(e) {

        $(e).attr("disabled", "");
        
        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/RequestForRating/Save_ContractAndFinancialDocuments", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                alertB("ثبت", "مدارک با موفقیت ثبت شد.", "success");
                /*$("SeeAllRequest").show();*/
              //  goToUrl("/Customer/RequestForRating/Index");

            } else {

                $("#AmountOsLastSales").val(moneyCommaSepWithReturn($("#AmountOsLastSales").val()));

                alertB("خطا", res.message, "error");
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
                        strTimeLine += "<span class='date'>" + res.data[i].sendTimeStr+"</span>";
                        strTimeLine += "<span class='LTRDirection'>" + res.data[i].sendTimeTimeStr+"</span>";
                        strTimeLine += "</time>";

                        strTimeLine += "<div class='timeline-icon " + bgColor + "'>";
                        strTimeLine += "<i class='entypo-feather'></i>";
                        strTimeLine += "</div>";

                        strTimeLine += "<div class='timeline-label'>";
                        if (i == 0) {
                            strTimeLine += "<h2>";
                            strTimeLine += "<span>ایجاد درخواست توسط : </span>";
                            strTimeLine += "<span class='sender'>" + res.data[i].agentName + " (" + res.data[i].roleDesc + ") " + "</span >";
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

                        htmlB += "<button type='button' style='margin:5px' class='btn btn-info ButtonOpperationLSSlss' onclick='Web.RequestForRating.SaveReferralRequestForRating(this);' data-DLSI='" + encrypt(res.data[i].destLevelStepIndex, keyMaker()) + "' data-LSAR='" + encrypt(res.data[i].levelStepAccessRole, keyMaker()) + "' data-LSS='" + encrypt(res.data[i].levelStepStatus, keyMaker()) + "' data-SC='" + encrypt(res.data[i].smsContent, keyMaker()) + "' data-ST='" + encrypt(res.data[i].smsType, keyMaker()) + "' data-DLSIB='" + encrypt(res.data[i].destLevelStepIndexButton, keyMaker()) +"'>" + res.data[i].destLevelStepIndexButton + "</button>";

                    }

                    $("#bLLSS").html(htmlB);
                    Ckeditor("ReferralExplanation");
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

                if (res != null) {

                    $("#FinancialID").val(res.financialId);
                    $("#RequestID").val(res.requestID);
                    $("#ContentContract").val(res.contentContract);
                    $("#PriceContract").val(res.priceContract);
                    $("#Tax").val(res.tax);
                    if (res.financialDocument != null && res.financialDocument != "") {
                        $("#divDownloadFinancialDocument").html("<a class='btn btn-success' href='/File/Download?path=" + res.financialDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.contractDocument != null && res.contractDocument != "") {
                        $("#divDownload_ContractDocument").html("<a class='btn btn-success' href='/File/Download?path=" + res.contractDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
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

                if (res != null) {
                  
                    $("#ContractShow").html(res.contentContract);
                   
                    $('input[type="text"], textarea').each(function () {
                        //  $(this).attr('readonly', 'readonly');
                        var text_classname = $(this).attr('name');
                        var value = $(this).val();
                        var new_html = ('<label for="' + text_classname + '" id="' + '">' + value + '</label>')
                        $(this).replaceWith(new_html);
                    });
                }

            }, true);
        }


    }

    function printContract(e) {
      
        var divToPrint = document.getElementById('ContractShow');

        var newWin = window.open('', 'Print-Window');

        newWin.document.open();

        newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');

        newWin.document.close();

        setTimeout(function () { newWin.close(); }, 10);
    }

    function showContract() {

        getContractInfo(decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker()));
    }

    

    function saveReferralRequestForRating(e) {

        $(".ButtonOpperationLSSlss").attr("disabled", "");

        var objJ = {};
        objJ.DestLevelStepIndex = decrypt($(e).attr("data-DLSI"), keyMaker());
        objJ.Comment = getDataCkeditor("ReferralExplanation");
        objJ.LevelStepAccessRole = decrypt($(e).attr("data-LSAR"), keyMaker());
        objJ.LevelStepStatus = decrypt($(e).attr("data-LSS"), keyMaker());
        objJ.Request = {};
        objJ.Request.Requestid = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        objJ.Request.KindOfRequest = '0';
        objJ.SmsContent = decrypt($(e).attr("data-SC"), keyMaker());
        objJ.SmsType = !isEmpty(decrypt($(e).attr("data-ST"), keyMaker())) ? decrypt($(e).attr("data-ST"), keyMaker()) : null;
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
        GetDocument: getDocument

    };

})(Web, jQuery);