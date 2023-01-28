
(function (web, $) {

    //Document Ready  

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {
              
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>"
                        + res.data[i].requestNo + "</td><td>"
                        + res.data[i].agentName + "</td><td>"
                        + res.data[i].agentMobile + "</td><td>"
                        + res.data[i].dateOfRequestStr + "</td>"
                        + "<td>" + res.data[i].levelStepStatus + "</td><td>"
                        + "<a style='margin-right:5px; color:black' href='/superVisor/RequestForRating/RequestReferences?id=" + res.data[i].requestId + "'" + " class='btn btn-info fontForAllPage'> <img src='/css/GlobalAreas/dist/img/timeline-icon.png' style='width:20px' title='مشاهده گردش کار'> گردش کار </a>"
                        + (getlstor("loginName") === res.data[i].destLevelStepAccessRole ? "<a style='margin-right:5px;color:black' title='ارجاع' class='btn btn-info fontForAllPage' href='/SuperVisor/RequestForRating/Referral/" + res.data[i].requestId + "'> <i class='fa fa-mail-forward' style='color:black'></i> ارجاع </a>" : "");

                      if (res.data[i].destLevelStepIndex>=7) {

                        strM +="<a style='margin-right:5px;color:black' title='مشاهده اطلاعات تکمیلی' class='btn btn-info fontForAllPage' href='/SuperVisor/FutherInfo/Index/" + res.data[i].requestId + "'><i class='fa fa-info'></i> اطلاعات تکمیلی</a>";

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
                    $("#sdklsslks3498sjdkxhjsd_823sa").val(encrypt(id.toString(), keyMaker()));
                    $("#phch").show();
                    if (getlstor("loginName") === "9") {
                        $("#svisorShowContract").remove();
                    }
                    var m = getlstor("loginName");
                    if (res.data[0].destLevelStepIndex <= 2 || getlstor("loginName") === "5") {
                       
                        $("#svisorShowDocument").remove();
                    }
                    if (res.data[0].destLevelStepIndex <=6) {
                        $("#svisorShowEvaluationFile").remove();
                        
                    }
                    var htmlB = "";
                    for (var i = 0; i < res.data.length; i++) {

                        htmlB += "<button type='button' style='margin:5px' class='btn btn-info ButtonOpperationLSSlss' onclick='Web.RequestForRating.SaveRequestForRating(this);' data-DLSI='" + encrypt(res.data[i].destLevelStepIndex, keyMaker()) + "' data-LSAR='" + encrypt(res.data[i].levelStepAccessRole, keyMaker()) + "' data-LSS='" + encrypt(res.data[i].levelStepStatus, keyMaker()) + "' data-SC='" + encrypt(res.data[i].smsContent, keyMaker()) + "' data-ST='" + encrypt(res.data[i].smsType, keyMaker()) + "' data-DLSIB='" + encrypt(res.data[i].destLevelStepIndexButton, keyMaker())+"'>" + res.data[i].destLevelStepIndexButton + "</button>";

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


    function showCustomerInfo() {

        getCustomerInfo();
    }

    function showContract() {

        getContractInfo();
    }

    function getCustomerInfo() {

      var id= decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker())

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

            if ((getlstor("loginName") === "1")) {
                getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Contract-moa.html", function (resG) {

                    $("#contractInfo").html(resG);
                    initContract(id);

                });
            } else {
                getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Contract.html", function (resG) {

                    $("#contractInfo").html(resG);
                    initContract(id);

                });
            }
                                
        }
    }

    function initCustomer(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/Customers/Get_Customers/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res != null) {
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
                    $("#AmountOsLastSales").val(moneyCommaSepWithReturn(!isEmpty(res.amountOsLastSales) ? res.amountOsLastSales.toString() : ''));
                    $("#divDownload").html("<a class='btn btn-success' href='/File/Download?path=" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    $("#divDownload_AuditedFinancialStatements").html("<a class='btn btn-success' href='/File/Download?path=" + res.auditedFinancialStatementsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

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

    function initContract(id = null) {
       
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ServiceFeeAndCustomerByRequest/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res != null) {
                    if (getlstor("loginName") === "1") {                        
                        $("#ContentCKeditor").html("<textarea name='ContractText' id='ContractText'>" + res.contract.contractText+"</textarea>");                       
                        Ckeditor("ContractText");

                    }
                    else {
                        
                        getContractCustomer(id);
                    }
                  
                }

            }, true);

        }
       

    }


    function getContractCustomer(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res != null) {

                    $("#ContractShow").addClass("ContractShowStyle");
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


    function saveRequestForRating(e) {

        $(".ButtonOpperationLSSlss").attr("disabled", "");

        var objJ = {};
        objJ.DestLevelStepIndex = decrypt($(e).attr("data-DLSI"), keyMaker());
        objJ.Comment =getDataCkeditor("ReferralExplanation");
        objJ.LevelStepAccessRole = decrypt($(e).attr("data-LSAR"), keyMaker());
        objJ.LevelStepStatus = decrypt($(e).attr("data-LSS"), keyMaker());
        objJ.Request = {};
        objJ.Request.Requestid = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        objJ.Request.KindOfRequest = 0;
        objJ.SmsContent = decrypt($(e).attr("data-SC"), keyMaker());
        objJ.SmsType = !isEmpty(decrypt($(e).attr("data-ST"), keyMaker())) ? decrypt($(e).attr("data-ST"), keyMaker()) : null;
        objJ.DestLevelStepIndexButton = decrypt($(e).attr("data-DLSIB"), keyMaker());

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Save_Request", JSON.stringify(objJ), true, function (res) {

            $(".ButtonOpperationLSSlss").removeAttr("disabled");

            if (res.isSuccess) {
                if (objJ.DestLevelStepIndex == "3") {
                    var m = getDataCkeditor("ContractText");
                    var FeePrice = $(m).find('input[name="ServiceFeePrice"]').val();// 
                    // var Price = RemoveAllCharForPrice(FeePrice);
                    saveContractAndFinancialDocuments(objJ.Request.Requestid, getDataCkeditor("ContractText"), FeePrice);
                }

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
                            getShowEvaluationFile(id,res);
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


    function getShowDoument(id=null) {
        if (!isEmpty(id) && id != 0) {
            getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Document.html", function (resG) {

                $("#showDocument").html(resG);
                getDocument(id);

            });
        }
    }

    function getShowEvaluationFile(id = null,res=null) {
        if (!isEmpty(id) && id != 0) {
            getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_EvaluationFile.html", function (resG) {

                $("#showEvaluationFile").html(resG);
                if (res.data[0].levelStepAccessRole == "9") {
                    $("#frmFormMain2").hide();
                }
                getDocument(id);

            });
        }
    }

    function getDocument(id=null) {
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/superVisor/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res != null) {

                    if (res.financialDocument != null) {
                        $("#divDownloadFinancialDocument").html("<a class='btn btn-success' href='/File/Download?path=" + res.financialDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownloadFinancialDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.contractDocument != null) {
                        $("#divDownload_ContractDocument").html("<a class='btn btn-success' href='/File/Download?path=" + res.contractDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_ContractDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                    if (res.evaluationFile != null) {
                        $("#divDownload_EvaluationFile").html("<a class='btn btn-success' href='/File/Download?path=" + res.evaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    } else {
                        $("#divDownload_EvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
                    }
                }

            }, true);
        }
    }


    function saveContractAndFinancialDocuments(RequestID = null, ContentContract = null, FeePrice=null) {

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Save_ContractAndFinancialDocuments", JSON.stringify({ RequestID: RequestID, ContentContract: ContentContract, PriceContractStr: FeePrice }), true, function (res) {

            if (res.isSuccess) {

               // goToUrl("/SuperVisor/RequestForRating/Index");

            }
            else {

                alertB("هشدار", res.message, "warning");

            }

        }, true);

    }

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
                            if (i>1) {
                                strTimeLine += "[" +  res.data[i].destLevelStepIndexButton + "]";
                            // strTimeLine += "[ جهت اقدام] <span >[ خوانده شده ] </span><span class='greenColor'>1401/09/21  3:34</span>";
                            } else {
                                strTimeLine += "[" +res.data[i].levelStepStatus +"]";
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
        GetShowEvaluationFile: getShowEvaluationFile
       
    };

})(Web, jQuery);