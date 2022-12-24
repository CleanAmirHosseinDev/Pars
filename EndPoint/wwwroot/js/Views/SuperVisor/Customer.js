






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function saveCustomer(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/customer/Customers/Save_BasicInformationCustomers", JSON.stringify({

            tel: $("#Tel").val(),
            AddressCompany: $("#AddressCompany").val(),
            CompanyName: $("#CompanyName").val(),
            CeoName: $("#CeoName").val(),
            EconomicCode: $("#EconomicCode").val(),
            NationalCode: $("#NationalCode").val(),
            CeoMobile: $("#CeoMobile").val(),
            CeoNationalCode: $("#CeoNationalCode").val(),
            AgentMobile: $("#AgentMobile").val(),
            AgentName: $("#AgentName").val(),
            NamesAuthorizedSignatories: $("#NamesAuthorizedSignatories").val(),
            AmountOsLastSales: isEmpty($("#AmountOsLastSales").val()) ? null : $("#AmountOsLastSales").val(),
            CountOfPersonal: isEmpty($("#CountOfPersonal").val()) ? null : $("#CountOfPersonal").val(),
            Email: $("#Email").val(),
            Tel: $("#Tel").val(),
            PostalCode: $("#PostalCode").val(),
            HowGetKnowCompanyId: isEmpty($("#howGetKnowCompany").val()) ? null : $("#howGetKnowCompany").val(),
            KindOfCompanyId: isEmpty($("#KindOfCompany").val()) ? null : $("#KindOfCompany").val(),
            LastInsuranceList: null,
            LastChangeOfficialNewspaper: null,
            
        }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                if ($("#TypeServiceRequestedId").val() != null) {
                    saveFirstRequestForRating($("#TypeServiceRequestedId").val());
                }

                alertB("ثبت", res.message, "success");
                $("SeeAllRequest").show();
                //  goToUrl("/Customer/Customer/EditCustomer");

            } else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function saveFirstRequestForRating(id) {
       
        AjaxCallAction("POST", "/api/customer/RequestForRating/Save_Request", JSON.stringify({ Request: { KindOfRequest: isEmpty(id) ? null :id } }), true, function (res) {

            if (!res.isSuccess) {
                alertB("هشدار", res.message, "warning");
            }

        }, true);

    }


    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/customer/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "63,27,56", PageIndex: 0, PageSize: 0 }), true, function (res) {

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

    function checkForFirstRequest() {
        Web.RequestForRating.RegistingFirstRequest(function (res) {

            if (res.isSuccess) {
                if (res.data.length > 0) {
                    $("#divTypeServiceRequestedId").hide();
                }

                else {
                    $("#divTypeServiceRequestedId").show();
                }
            } 

        });
    }

   

    web.Customer = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        SaveCustomer: saveCustomer,
        InitCustomer: initCustomer,
        SystemSeting_Combo: systemSeting_Combo,
        CheckForFirstRequest: checkForFirstRequest,
        SaveFirstRequestForRating: saveFirstRequestForRating
    };

})(Web, jQuery);