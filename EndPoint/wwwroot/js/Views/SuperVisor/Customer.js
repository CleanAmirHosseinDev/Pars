
(function (web, $) {

    //Document Ready              

    function filterGrid() {

        AjaxCallAction("POST", "/api/superVisor/Customers/Get_Customerss", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + (isEmpty(res.data[i].companyName) ? '' : res.data[i].companyName) + "</td><td>" + (isEmpty(res.data[i].agentName) ? '' : res.data[i].agentName) + "</td><td>" + (isEmpty(res.data[i].agentMobile) ? '' : res.data[i].agentMobile) + "</td><td>" + res.data[i].saveDateStr + "</td><td>" + (res.data[i].isProfileComplete ? 'ثبت درخواست' : 'عدم ثبت درخواست') + "</td><td><a title='نمایش' href='/SuperVisor/Customers/ShowCustomers?id=" + res.data[i].customerId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-eye'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function initCustomer(id,dir = 'rtl') {
        
        AjaxCallAction("GET", "/api/superVisor/Customers/Get_Customers/" + (isEmpty(id) ? '0' : id), null, true, function (res) {


            if (res != null) {

                $("#AddressCompany").html(res.addressCompany);
                $("#CompanyName").html(res.companyName);
                $("#CeoName").html(res.ceoName);
                $("#EconomicCode").html(res.economicCode);
                $("#NationalCode").html(res.nationalCode);
                $("#CeoNationalCode").html(res.ceoNationalCode);
                $("#CeoMobile").html(res.ceoMobile);
                $("#AgentMobile").html(res.agentMobile);
                $("#AgentName").html(res.agentName);
                $("#NamesAuthorizedSignatories").html(res.namesAuthorizedSignatories);
                $("#CountOfPersonal").html(res.countOfPersonal);
                $("#Email").html(res.email);
                $("#Tel").html(res.tel);
                $("#PostalCode").html(res.postalCode);

                $("#AmountOsLastSales").val(moneyCommaSepWithReturn(!isEmpty(res.amountOsLastSales) ? res.amountOsLastSales.toString() : ''));

                $("#HowGetKnowCompanyId").html(res.howGetKnowCompany != null ? res.howGetKnowCompany.label : '');

                $("#KindOfCompanyId").html(res.kindOfCompany != null ? res.kindOfCompany.label : '');
                

            }


        }, true);

    }

    web.Customer = {
        FilterGrid: filterGrid,
        InitCustomer: initCustomer
    };

})(Web, jQuery);