
(function (web, $) {

    //Document Ready              


    $("#frmFormMain input,textarea").on("focusout", function () {

        $(this).valid();

    });

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function saveCustomer(e) {
        let req_id = 0;
        $("#btnOpperationRun").attr("disabled", "");
        //if ($("#TypeServiceRequestedId").val() == '254' && $("#QuestionLevel").val() == '0')
        //    alertB("خطا", "لطفا سطح سوالات قرار داد را تنظیم نمایید", "warning", "بله متوجه شدم", function () {});
        //else {
        RemoveAllCharForPrice("AmountOsLastSales");
        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/Customers/Save_BasicInformationCustomers", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), false, function (res) {
            if (res.isSuccess) {
                req_id = res.resultId;
                /*alertB("ثبت", res.message, "success");*/
                /*$("SeeAllRequest").show();*/
                setlstor("fullName", $("#CompanyName").val());
                if (req_id != 0) {
                    $("#RequestId").val(req_id);
                    AjaxCallActionPostSaveFormWithUploadFile("/api/customer/RequestForRating/Save_SaveCustomerRequestInformation", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), false, function (res) {
                    }, false);
                }
                if ($("#TypeServiceRequestedId").val() == null) {
                    $(".fullNameInLayout").html(getlstor("fullName"));
                    $("#AmountOsLastSales").val(moneyCommaSepWithReturn($("#AmountOsLastSales").val()));
                    /*alertB("ثبت", "پروفایل شما ویرایش شد.", "success");*/
                    alertB("ثبت", "پروفایل شما ویرایش شد", "success", "بله متوجه شدم", function () {
                    });
                } else {
                    alertB("ثبت", "پروفایل شما ویرایش شد", "success", "بله متوجه شدم", function () {
                    });
                }
                goToUrl("/Customer/RequestForRating/Index");
            }
            else {
                $("#AmountOsLastSales").val(moneyCommaSepWithReturn($("#AmountOsLastSales").val()));
                alertB("خطا", res.message, "error");
                $("#btnOpperationRun").removeAttr("disabled");
            }
        }, false);
    }

    function saveMobileUser(e) {
        let req_id = 0;
        $("#btnSaveMobileUser").attr("disabled", "");
        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/Customers/Save_BasicInformationCustomers", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), false, function (res) {
            $("#btnSaveMobileUser").removeAttr("disabled");
            if (res.isSuccess) {
                req_id = res.resultId;
                $("#RequestId").val(req_id);
                alertB("ثبت", res.message, "success");
            }
            else {
                alertB("خطا", res.message, "error");
            }
        }, true);

        if (req_id != 0) {
            AjaxCallActionPostSaveFormWithUploadFile("/api/customer/RequestForRating/Save_SaveCustomerRequestInformation", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), false, function (res) {
                goToUrl("/Customer/RequestForRating/Index");
            }, false);
        }
    }


    function systemSeting_Combo(resSingle, showdrp) {


        AjaxCallAction("POST", "/api/customer/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "126,63,27,56,221", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';
                var strHowGetKnowCompany = '<option value="">انتخاب کنید</option>';
                var strTypeServiceRequestedId = '<option value="">انتخاب کنید</option>';
                var strCustomerPersonalityType = '<input type="hidden" name="CustomerPersonalityType" id="CustomerPersonalityType">';
                var strTypeGroupCompanies = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].parentCode == 56) {
                        strHowGetKnowCompany += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                    else if (res.data[i].parentCode == 63) {
                        strTypeServiceRequestedId += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                    else if (res.data[i].parentCode == 27) {
                        strKindOfCompany += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                    else if (res.data[i].parentCode == 221 && isEmpty(resSingle.customerPersonalityType)) {
                        strCustomerPersonalityType += "<input type='radio' value=" + res.data[i].systemSetingId + " name='CustomerPersonalityTypeStr' id='CustomerPersonalityTypeStr' onchange='Web.Customer.OnChangeCustomerPersonalityType(this);' style='margin-right: 30px;'>" + res.data[i].label;
                    }
                    else if (res.data[i].parentCode == 126) {
                        strTypeGroupCompanies += "<option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                }


                $("#divCustomerPersonalityTypeRadio").html(strCustomerPersonalityType);
                $("#CustomerPersonalityTypeStr[value='" + resSingle.customerPersonalityType + "']").prop("checked", true);

                $("#CustomerPersonalityType").val(resSingle.customerPersonalityType);

                if (!isEmpty(resSingle.customerPersonalityType)) $("#divCustomerPersonalityType").hide();

                temp_OnChangeCustomerPersonalityType(resSingle.customerPersonalityType);


                $("#TypeGroupCompanies").html(strTypeGroupCompanies);
                $("#TypeGroupCompanies").val(resSingle.typeGroupCompanies);

                $("#KindOfCompanyId").html(strKindOfCompany);
                $("#HowGetKnowCompanyId").html(strHowGetKnowCompany);

                $("#HowGetKnowCompanyId").val(resSingle.howGetKnowCompanyId);
                $("#KindOfCompanyId").val(resSingle.kindOfCompanyId);

                if (showdrp) {
                    $("#TypeServiceRequestedId").html(strTypeServiceRequestedId);
                    let selected = getlstor("code");
                    $("#TypeServiceRequestedId").val(selected);
                    if (selected == 254) {
                        $("#CountOfPersonel").val(0);
                        $("#CountOfPersonal").val(0);
                        $("#AmountOfLastSale").val(0);
                        $("#AmountOsLastSales").val(0);
                        $("#hide_information").hide();
                        $("#TypeOfRequestLevel").show();
                        dellstor("code");
                    }
                }

                validate();

                $("#CountOfPersonal").keyup();
                $("#AmountOsLastSales").keyup();
            }
        }, true);
    }

    function validate() {

        $.validator.addMethod(
            "CeoMobileValidation",
            function (value, element) {
                return CheckMobile(value);
            },
            "موبایل مدیر عامل خود را به درستی وارد نمایید"
        );
        $.validator.addMethod(
            "AgentMobileValidation",
            function (value, element) {
                return CheckMobile(value);
            },
            function () {
                return $("#Span_Label_AgentMobile").text() + " " + ($("#CustomerPersonalityType").val() != "223" ? "شرکت/مشاور " : " ") + " خود را به درستی وارد نمایید";
            }
        );
        $.validator.addMethod(
            "TelValidation",
            function (value, element) {
                return CheckTel(value);
            },
            function () {
                return "شماره تلفن ثابت خود را به درستی وارد نمایید";
            }
        );

        $.validator.addMethod(
            "AmountOsLastSalesValidation",
            function (value, element) {
                return value.indexOf("/") !== -1 ? false : true;
            },
            function () {
                return "مبلغ وارد شده نامعتبر می باشد لطفا مبلغ را دستی وارد کنید";
            }
        );

        $("form[id='frmFormMain']").validate({
            // Specify validation rules
            rules: {
                "CompanyName": {
                    required: function () {
                        return true;
                    },
                    minlength: 5,
                    maxlength: 50
                },
                "CeoMobile": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                    minlength: 11,
                    maxlength: 11,
                    CeoMobileValidation: $("#CeoMobile").val(),
                },
                "CeoName": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                    minlength: 5,
                    maxlength: 50
                },
                "CeoNationalCode": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                    minlength: 10,
                    maxlength: 10
                },
                "EconomicCode": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                    minlength: 3,
                    maxlength: 50
                },
                "NationalCode": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                    minlength: 10,
                    maxlength: 11
                },
                "NationalCodeRepresentative": {
                    minlength: 10,
                    maxlength: 10
                },
                "AgentName": {
                    required: function () {
                        return true;
                    },
                    minlength: 5,
                    maxlength: 50
                },
                "EmailRepresentative": {
                    minlength: 5,
                    maxlength: 50,
                    email: true
                },
                "AgentMobile": {
                    required: function () {
                        return true;
                    },
                    minlength: 11,
                    maxlength: 11,
                    AgentMobileValidation: $("#AgentMobile").val(),
                },
                "AddressCompany": {
                    required: function () {
                        return true;
                    },
                },
                "CountOfPersonal": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                    maxlength: 9
                },
                "HowGetKnowCompanyId": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                },
                "KindOfCompanyId": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                },
                "TypeGroupCompanies": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                },
                "Email": {
                    required: function () {
                        return true;
                    },
                    minlength: 5,
                    maxlength: 50,
                    email: true
                },
                "Tel": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                    minlength: 11,
                    maxlength: 11,
                    TelValidation: $("#Tel").val(),
                },
                "PostalCode": {
                    required: function () {
                        return true;
                    },
                    minlength: 10,
                    maxlength: 10
                },
                "EconomicCodeReal": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                    minlength: 10,
                    maxlength: 12
                },
                "AmountOsLastSales": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                    maxlength: 18,
                    AmountOsLastSalesValidation: $("#AmountOsLastSales").val(),
                },
                "QuestionLevel": {
                    required: function () {
                        if ($("#TypeServiceRequestedId").val() == '254' && $("#QuestionLevel").val() == '0')
                            return true;
                        else {
                            return false;
                        }
                    },
                },
            },
            // Specify validation error messages
            messages: {
                "QuestionLevel": {
                    required: function () {
                        return "لطفا " + "سطح سوالات مورد تقاضا" + " را وارد کنید";
                    },
                },
                "CompanyName": {
                    required: function () {
                        return "لطفا " + $("#LabelCompanyName").text() + " را وارد کنید";
                    },
                    minlength: $("#LabelCompanyName").text() + " باید حداقل 5 حرف باشد",
                    maxlength: $("#LabelCompanyName").text() + " باید حداکثر 50 حرف باشد"
                },
                "CeoMobile": {
                    required: function () {
                        return "لطفا موبایل مدیر عامل را وارد کنید";
                    },
                    minlength: "موبایل مدیر عامل باید حداقل 11 حرف باشد",
                    maxlength: "موبایل مدیر عامل باید حداکثر 11 حرف باشد",
                },
                "CeoName": {
                    required: function () {
                        return "لطفا نام مدیر عامل را وارد کنید";
                    },
                    minlength: "نام مدیر عامل باید حداقل 5 حرف باشد",
                    maxlength: "نام مدیر عامل باید حداکثر 50 حرف باشد"
                },
                "CeoNationalCode": {
                    required: function () {
                        return "لطفا کد ملی مدیر عامل را وارد کنید";
                    },
                    minlength: "کد ملی مدیر عامل باید حداقل 10 حرف باشد",
                    maxlength: "کد ملی مدیر عامل باید حداکثر 10 حرف باشد"
                },
                "EconomicCode": {
                    required: function () {
                        return "لطفا " + $("#LabelEconomicCode").text() + " را وارد کنید";
                    },
                    minlength: $("#LabelEconomicCode").text() + " باید حداقل 3 حرف باشد",
                    maxlength: $("#LabelEconomicCode").text() + " باید حداکثر 50 حرف باشد"
                },
                "NationalCode": {
                    required: function () {
                        return "لطفا شناسه ملی  شرکت را وارد کنید";
                    },
                    minlength: "شناسه ملی  شرکت باید حداقل 10 حرف باشد",
                    maxlength: "شناسه ملی  شرکت باید حداکثر 11 حرف باشد"
                },
                "NationalCodeRepresentative": {
                    minlength: "کد ملی نماینده شرکت/ مشاور باید حداقل 10 حرف باشد",
                    maxlength: "کد ملی نماینده شرکت/ مشاور باید حداکثر 10 حرف باشد"
                },
                "AgentName": {
                    required: function () {
                        return "لطفا نام نماینده شرکت/ مشاور شرکت را وارد کنید";
                    },
                    minlength: "نام نماینده شرکت/ مشاور شرکت باید حداقل 5 حرف باشد",
                    maxlength: "نام نماینده شرکت/ مشاور شرکت باید حداکثر 50 حرف باشد"
                },
                "EmailRepresentative": {
                    minlength: "ایمیل نماینده شرکت/ مشاور باید حداقل 5 حرف باشد",
                    maxlength: "ایمیل نماینده شرکت/ مشاور باید حداکثر 50 حرف باشد",
                    email: "لطفا ایمیل نماینده شرکت/ مشاور معتبر وارد کنید"
                },
                "AgentMobile": {
                    required: function () {
                        return "لطفا " + $("#Span_Label_AgentMobile").text() + ($("#CustomerPersonalityType").val() != "223" ? "شرکت/مشاور " : " ") + " را وارد کنید";
                    },
                    minlength: function () {
                        return $("#Span_Label_AgentMobile").text() + " " + ($("#CustomerPersonalityType").val() != "223" ? "شرکت/مشاور " : "") + " باید حداقل 11 حرف باشد";
                    },
                    maxlength: function () {
                        return $("#Span_Label_AgentMobile").text() + " " + ($("#CustomerPersonalityType").val() != "223" ? "شرکت/مشاور " : "") + " باید حداکثر 11 حرف باشد";
                    }
                },
                "AddressCompany": {
                    required: function () {
                        return "لطفا آدرس را وارد کنید";
                    },
                },
                "CountOfPersonal": {
                    required: function () {
                        return "لطفا تعداد کارکنان " + ($("#CustomerPersonalityType").val() != "223" ? "شرکت " : " ") + " را وارد کنید";
                    },
                    maxlength: function () {
                        return "تعداد کارکنان " + ($("#CustomerPersonalityType").val() != "223" ? "شرکت " : "") + " باید حداکثر 9 حرف باشد";
                    }
                },
                "HowGetKnowCompanyId": {
                    required: function () {
                        return "لطفا نحوه آشنایی با شرکت را انتخاب کنید";
                    },
                },
                "KindOfCompanyId": {
                    required: function () {
                        return "لطفا نوع  فعالیت شرکت را انتخاب کنید";
                    },
                },
                "TypeGroupCompanies": {
                    required: function () {
                        return "لطفا " + $("#LabelTypeGroupCompanies").text() + " را انتخاب کنید";
                    },
                },
                "Email": {
                    required: function () {
                        return "لطفا ایمیل شرکت را وارد کنید";
                    },
                    minlength: "ایمیل شرکت باید حداقل 5 حرف باشد",
                    maxlength: "ایمیل شرکت باید حداکثر 50 حرف باشد",
                    email: "لطفا ایمیل شرکت معتبر وارد کنید"
                },
                "Tel": {
                    required: function () {
                        return "لطفا شماره تلفن ثابت را وارد کنید";
                    },
                    minlength: "شماره تلفن ثابت باید حداقل 11 حرف باشد",
                    maxlength: "شماره تلفن ثابت باید حداکثر 11 حرف باشد"
                },
                "PostalCode": {
                    required: function () {
                        return "لطفا کد پستی را وارد کنید";
                    },
                    minlength: "کد پستی باید حداقل 10 حرف باشد",
                    maxlength: "کد پستی باید حداکثر 10 حرف باشد"
                },
                "EconomicCodeReal": {
                    required: function () {
                        return "لطفا کد اقتصادی را وارد کنید";
                    },
                    minlength: "کد اقتصادی باید حداقل 10 حرف باشد",
                    maxlength: "کد اقتصادی باید حداکثر 12 حرف باشد"
                },
                "AmountOsLastSales": {
                    required: function () {
                        return "لطفا درآمد عملیاتی بر اساس صورت های مالی حسابرسی شده را وارد کنید";
                    },
                    maxlength: "درآمد عملیاتی بر اساس صورت های مالی حسابرسی شده باید حداکثر 18 رقم باشد"
                },
            },
            // Make sure the form is submitted to the destination defined
            // in the "action" attribute of the form when valid
            submitHandler: function (form) {
                if ($("#TypeServiceRequestedId").val() == '254' && $("#QuestionLevel").val() == '0')
                    alertB("خطا", "لطفا سطح سوالات قرار داد را تنظیم نمایید", "warning", "بله متوجه شدم", function () { });
                else {
                    
                    if ($("#divTypeServiceRequestedId:hidden").length === 0) {
                        confirmB("", "مشتری محترم در نظر داشته باشید چون معیار و مبنای شروع ارزیابی بر اساس اطلاعات پایه شما می باشد لذا در صورت اطمینان از صحت ورود اطلاعات تایید کنید پس از تایید امکان ویرایش وجود ندارد", "warning", function () {
                            Web.Customer.SaveCustomer(this);
                        }, function () {

                        }, ["انصراف از ثبت", "بله مطمئنم"]);
                    } else {
                        Web.Customer.SaveCustomer(this);
                    }
                }
            }
        });

    }

    function checkForFirstRequest(resSingle) {
        Web.RequestForRating.RegistingFirstRequest(function (res) {

            if (res.isSuccess) {
                if (res.data.length > 0) {
                    $("#divTypeServiceRequestedId").hide();
                    systemSeting_Combo(resSingle, false);
                    $('#hide_information').hide();
                    $('#TypeOfRequestLevel').hide();
                }

                else {
                    $("#divTypeServiceRequestedId").show();
                    systemSeting_Combo(resSingle, true);
                }
            }

        });
    }

    function makeComboForQuestionLevel() {
        let strM = "";
        AjaxCallAction("POST", "/api/customer/Corporate/Get_QuestionLevels", JSON.stringify({
            PageIndex: 0,
            PageSize: 0,
            IsActive: 15,
        }), false, function (res) {
            if (res.isSuccess) {
                strM = '<option value="0">انتخاب کنید</option>';
                for (var i = 0; i < res.data.length; i++) {
                    strM += "<option value='" + res.data[i].questionLevelId + "'>" + res.data[i].levelTitle + "</option>";
                }
            }
        }, false);
        return strM;
    }

    function initCustomer(dir = 'rtl') {
        ComboBoxWithSearch('.select2', dir);
        
        let selecet_item = makeComboForQuestionLevel();
        $("#QuestionLevel").html(selecet_item);
        
        $('#TypeServiceRequestedId').on('select2:select', function (e) {
            let _select_id = e.params.data.id;
            if (_select_id == 254) {
                $("#CountOfPersonel").val(0);
                $("#AmountOfLastSale").val(0);
                $("#hide_information").hide();
                $("#TypeOfRequestLevel").show();
            } else {
                $("#hide_information").show();
                $("#CountOfPersonel").val("");
                $("#AmountOfLastSale").val("");
                $("#TypeOfRequestLevel").hide();
            }
        });
        AjaxCallAction("GET", "/api/customer/Customers/Get_Customers/", null, true, function (res) {
            if (res != null) {
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
                Tagsinput("NamesAuthorizedSignatories");

                $("#CountOfPersonal").val(res.countOfPersonal);

                $("#Email").val(res.email);
                $("#Tel").val(res.tel);
                $("#PostalCode").val(res.postalCode);
                $("#AmountOsLastSales").val(moneyCommaSepWithReturn(!isEmpty(res.amountOsLastSales) ? res.amountOsLastSales.toString() : ''));

                $("#EmailRepresentative").val(res.emailRepresentative);
                $("#NationalCodeRepresentative").val(res.nationalCodeRepresentative);

                $("input[name='AuditedFinancialStatements']").val(res.auditedFinancialStatements);
                $("input[name='LastInsuranceList']").val(res.lastInsuranceList);

                $("input[name='ScanCustomerNationalCard']").val(res.scanCustomerNationalCard);
                $("input[name='ScanManagerNationalCard']").val(res.scanManagerNationalCard);
                if (res.scanCustomerNationalCard != null && res.scanCustomerNationalCard != "") {
                    $("#divDownload_ScanCustomerNationalCard").html("<a class='btn btn-success' href='" + res.scanCustomerNationalCardFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                }
                if (res.scanManagerNationalCard != null && res.scanManagerNationalCard != "") {
                    $("#divDownload_ScanManagerNationalCard").html("<a class='btn btn-success' href='" + res.scanManagerNationalCardFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                }

                if (res.lastInsuranceList != null && res.lastInsuranceList != "") {
                    $("#divDownload").html("<a class='btn btn-success' href='" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                }
                if (res.auditedFinancialStatements != null && res.auditedFinancialStatements != "") {
                    $("#divDownload_AuditedFinancialStatements").html("<a class='btn btn-success' href='" + res.auditedFinancialStatementsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                }
                checkForFirstRequest(res);
                // systemSeting_Combo(res);
            }
            else {
                Tagsinput("NamesAuthorizedSignatories");
            }

        }, true);
    }

    function onChangeCustomerPersonalityType(e) {

        temp_OnChangeCustomerPersonalityType($(e).val());

    }

    function temp_OnChangeCustomerPersonalityType(val) {

        $("label[class='error']").text('');

        if (val == "223") {

            $(".form-group").hide();
            $(".form-group.FormIsShow").show();
            $(".NotShowRequiredLabel").hide();
            $("#LabelTypeGroupCompanies").html("نوع فعالیت");
            $("#LabelEconomicCode").html("شماره کارت بازرگانی");
            $("#LabelCompanyName").html("نام و نام خانوادگی");
            $("#Span_Label_AgentMobile").html("شماره موبایل / شماره موبایل نماینده ");
            $("#LabelAgentName").html("نام نماینده ");

            $("#spanScanCustomerNationalCardReq").show();
            $("#spanScanManagerNationalCardReq").hide();
        }
        else {

            $(".form-group").show();
            $(".NotShowRequiredLabel").show();
            $("#LabelTypeGroupCompanies").html("نوع فعالیت شرکتها");
            $("#LabelEconomicCode").html("شماره ثبت");
            $("#LabelCompanyName").html("نام شرکت");
            $("#Span_Label_AgentMobile").html("شماره نماینده");

            $("#spanScanCustomerNationalCardReq").hide();
            $("#spanScanManagerNationalCardReq").hide();
        }

        $("#CustomerPersonalityType").val(val);

    }

    function onKeyPressInCountOfPersonal(event) {

        if ($(event).val() == "0") {
            $("#spanListLastBimeReq").hide();
        }
        else {
            $("#spanListLastBimeReq").show();
        }

    }

    function onkeuUpInAmountOsLastSales(e) {

        if ($(e).val() == "0") {

            $("#Span_Label_Result_Final_AuditedFinancialStatements").hide();

        }
        else {
            $("#Span_Label_Result_Final_AuditedFinancialStatements").show();
        }

        TextBoxOnlyPrice(e);

    }

    web.Customer = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        SaveCustomer: saveCustomer,
        InitCustomer: initCustomer,
        SystemSeting_Combo: systemSeting_Combo,
        CheckForFirstRequest: checkForFirstRequest,
        OnChangeCustomerPersonalityType: onChangeCustomerPersonalityType,
        OnKeyPressInCountOfPersonal: onKeyPressInCountOfPersonal,
        OnkeuUpInAmountOsLastSales: onkeuUpInAmountOsLastSales,
        SaveMobileUser: saveMobileUser
    };

})(Web, jQuery);