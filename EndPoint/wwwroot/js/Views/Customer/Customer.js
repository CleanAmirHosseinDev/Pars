






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function saveCustomer(e) {

        $(e).attr("disabled", "");
        RemoveAllCharForPrice("AmountOsLastSales");
        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/Customers/Save_BasicInformationCustomers", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                /*alertB("ثبت", res.message, "success");*/
                /*$("SeeAllRequest").show();*/
                goToUrl("/Customer/RequestForRating/Index");

            } else {

                $("#AmountOsLastSales").val(moneyCommaSepWithReturn($("#AmountOsLastSales").val()));

                alertB("خطا", res.message, "error");
            }

        }, true);

    }


    function systemSeting_Combo(resSingle, showdrp) {

       
        AjaxCallAction("POST", "/api/customer/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "215,63,27,56,219" , PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';
                var strHowGetKnowCompany = '<option value="">انتخاب کنید</option>';
                var strTypeServiceRequestedId = '<option value="">انتخاب کنید</option>';
                var strCustomerPersonalityType = '<option value="">انتخاب کنید</option>';
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
                    else if (res.data[i].parentCode == 215) {
                        strCustomerPersonalityType += "<option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                    else if (res.data[i].parentCode == 219) {
                        strTypeGroupCompanies += "<option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                }

                $("#CustomerPersonalityType").html(strCustomerPersonalityType);
                $("#CustomerPersonalityType").val(resSingle.customerPersonalityType);

                $("#CustomerPersonalityType").change();

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
            }
        }, true);
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

                $("#divDownload").html("<a href='/File/Download?path=" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                $("#divDownload_AuditedFinancialStatements").html("<a href='/File/Download?path=" + res.auditedFinancialStatementsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                checkForFirstRequest(res);
                // systemSeting_Combo(res);


            }


        }, true);

    }

    function onChangeCustomerPersonalityType(e) {

        if ($(e).val() == "216") {

            $(".form-group").hide();
            $(".form-group.FormIsShow").show();

        }
        else {

            $(".form-group").show();

        }

    }

    web.Customer = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        SaveCustomer: saveCustomer,
        InitCustomer: initCustomer,
        SystemSeting_Combo: systemSeting_Combo,
        CheckForFirstRequest: checkForFirstRequest,
        OnChangeCustomerPersonalityType: onChangeCustomerPersonalityType
    };

})(Web, jQuery);