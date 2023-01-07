
(function (web, $) {

    //Document Ready              

    function filterGrid() {

        AjaxCallAction("POST", "/api/superVisor/Customers/Get_Customerss", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].agentName + "</td><td>" + res.data[i].agentMobile + "</td><td>" + res.data[i].tel + "</td><td><a title='نمایش' href='/SuperVisor/Customers/ShowCustomers?id=" + res.data[i].customerId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-eye'></i></a></td></tr>";

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
                $("#AmountOsLastSales").html(res.amountOsLastSales);                

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