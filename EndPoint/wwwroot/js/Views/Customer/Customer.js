






(function (web, $) {

    //Document Ready              


    $("#frmFormMain input,textarea").on("focusout", function () {

        $(this).valid();

    });

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function saveCustomer(e) {

        $("#btnOpperationRun").attr("disabled", "");
        RemoveAllCharForPrice("AmountOsLastSales");
        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/Customers/Save_BasicInformationCustomers", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), true, function (res) {

            $("#btnOpperationRun").removeAttr("disabled");

            if (res.isSuccess) {

                /*alertB("ثبت", res.message, "success");*/
                /*$("SeeAllRequest").show();*/
                setlstor("fullName", $("#CompanyName").val());

                if ($("#TypeServiceRequestedId").val() == null) {
                    $(".fullNameInLayout").html(getlstor("fullName"));
                    $("#AmountOsLastSales").val(moneyCommaSepWithReturn($("#AmountOsLastSales").val()));
                    /*alertB("ثبت", "پروفایل شما ویرایش شد.", "success");*/
                    goToUrl("/Customer/RequestForRating/Index");
                } else {
                    goToUrl("/Customer/RequestForRating/Index");
                }

            }
            else {

                $("#AmountOsLastSales").val(moneyCommaSepWithReturn($("#AmountOsLastSales").val()));

                alertB("خطا", res.message, "error");
            }

        }, true);



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
                    $("#TypeServiceRequestedId").val('66');
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
                        return $("#CustomerPersonalityType").val() != "223";
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
                        return $("#CustomerPersonalityType").val() != "223";
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
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                    minlength: 10,
                    maxlength: 10
                },
                "AmountOsLastSales": {
                    required: function () {
                        return $("#CustomerPersonalityType").val() != "223";
                    },
                    maxlength: 18
                },
            },
            // Specify validation error messages
            messages: {
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
                        return "لطفا نوع شرکت را انتخاب کنید";
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
                "AmountOsLastSales": {
                    required: function () {
                        return "لطفا درآمد عملیاتی بر اساس صورت های مالی حسابرسی شده را وارد کنید";
                    },
                    maxlength: "درآمد عملیاتی بر اساس صورت های مالی حسابرسی شده باید حداکثر 18 حرف باشد"
                },
            },
            // Make sure the form is submitted to the destination defined
            // in the "action" attribute of the form when valid
            submitHandler: function (form) {

                if ($("#divTypeServiceRequestedId:hidden").length === 0) {

                    confirmB("", "مشتری محترم در نظر داشته باشید چون معیار و مبنای شروع ارزیابی بر اساس اطلاعات پایه شما می باشد لذا در صورت اطمینان از صحت ورود اطلاعات تایید کنید پس از تایید امکان ویرایش وجود ندارد", "warning", function () {

                        Web.Customer.SaveCustomer(this);

                    }, function () {

                    }, ["انصراف از ثبت", "بله مطمئنم"]);

                }
                else {

                    Web.Customer.SaveCustomer(this);

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
                }

                else {
                    $("#divTypeServiceRequestedId").show();
                    systemSeting_Combo(resSingle, true);
                }
            }

        });
    }

    function initCustomer(dir = 'rtl') {

        ComboBoxWithSearch('.select2', dir);

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

                if (res.lastInsuranceList != null && res.lastInsuranceList != "") {
                    $("#divDownload").html("<a class='btn btn-success' href='/File/Download?path=" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                }
                if (res.auditedFinancialStatements != null && res.auditedFinancialStatements != "") {
                    $("#divDownload_AuditedFinancialStatements").html("<a class='btn btn-success' href='/File/Download?path=" + res.auditedFinancialStatementsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

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
            $("#Span_Label_AgentMobile").html("شماره موبایل");
        }
        else {

            $(".form-group").show();
            $(".NotShowRequiredLabel").show();
            $("#LabelTypeGroupCompanies").html("نوع گروه شرکتها");
            $("#LabelEconomicCode").html("شماره ثبت");
            $("#LabelCompanyName").html("نام شرکت");
            $("#Span_Label_AgentMobile").html("شماره نماینده");
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
        OnkeuUpInAmountOsLastSales: onkeuUpInAmountOsLastSales
    };

})(Web, jQuery);