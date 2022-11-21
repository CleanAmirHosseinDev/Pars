






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }
  
    function saveCustomer(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/customer/Customers/Save_BasicInformationCustomers", JSON.stringify({

            tel: $("#Tel").val(),
            //AddressCompany: $("#AddressCompany").val(),
            //CompanyName: $("#CompanyName").val(),
            //CeoName: $("#CeoName").val(),
            //EconomicCode: $("#EconomicCode").val(),
            //NationalCode: $("#NationalCode").val(),
            //CeoMobile: $("#CeoMobile").val(),
            //AgentMobile:$("#AgentMobile").val(),
            //AgentName: $("#AgentName").val(),
            //NamesAuthorizedSignatories:$("#NamesAuthorizedSignatories").val(),
            //AmountOsLastSaels:$("#AmountOsLastSaels").val(),
            //CountOfPersonal: $("#CountOfPersonal").val(),
            //Email: $("#Email").val(),
            //Tel: $("#Tel").val(),
            //PostalCode: $("#PostalCode").val(),
            //HowGetKnowCompanyId:$("#howGetKnowCompany").val(),
            //KindOfCompanyId:$("#KindOfCompany").val(),
            //TypeServiceRequestedId: $("#TypeServiceRequestedId").val()

        }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {
                alertB("ثبت", res.message, "success");
                goToUrl("/Customer/Customer/EditCustomer");

            } else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initCustomer() {
       
        AjaxCallAction("GET", "/api/customer/Customers/Get_Customers/", null, true, function (res) {

                if (res != null) {
                    $("#AddressCompany").val(res.addressCompany);
                    $("#CompanyName").val(res.companyName);
                    $("#CeoName").val(res.ceoName);
                    $("#EconomicCode").val(res.economicCode);
                    $("#NationalCode").val(res.nationalCode);
                    $("#CeoMobile").val(res.ceoMobile);
                    $("#AgentMobile").val(res.agentMobile);
                    $("#AgentName").val(res.agentName);
                    $("#NamesAuthorizedSignatories").val(res.namesAuthorizedSignatories);
                    $("#AmountOsLastSaels").val(res.amountOsLastSaels);
                    $("#CountOfPersonal").val(res.countOfPersonal);
                    $("#Email").val(res.email);
                    $("#Tel").val(res.tel);
                    $("#PostalCode").val(res.postalCode);
                    $("#HowGetKnowCompany").val(res.howGetKnowCompanyId);
                    $("#KindOfCompany").val(res.kindOfCompanyId);
                    $("#TypeServiceRequestedId").val(res.typeServiceRequestedId);
                    
                }

            }, true);       

    }

    web.Customer = {
        TextSearchOnKeyDown: textSearchOnKeyDown,       
        SaveCustomer: saveCustomer,
        InitCustomer: initCustomer
    };

})(Web, jQuery);