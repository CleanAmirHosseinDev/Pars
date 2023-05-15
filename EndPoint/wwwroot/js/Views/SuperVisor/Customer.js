
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


                if (res.customerPersonalityType == "223") {

                    $(".form-group").hide();
                    $(".form-group.FormIsShow").show();
                    $(".NotShowRequiredLabel").hide();
                    $("#LabelTypeGroupCompanies").html("نوع فعالیت");
                    $("#LabelEconomicCode").html("شماره کارت بازرگانی");
                    $("#LabelCompanyName").html("نام و نام خانوادگی");
                    $("#Span_Label_AgentMobile").html("شماره موبایل/ شماره موبایل نماینده");
                }
                else {

                    $(".form-group").show();
                    $(".NotShowRequiredLabel").show();
                    $("#LabelTypeGroupCompanies").html("نوع فعالیت شرکتها");
                    $("#LabelEconomicCode").html("شماره ثبت");
                    $("#LabelCompanyName").html("نام شرکت");
                    $("#Span_Label_AgentMobile").html("شماره نماینده");
                }


                $("#AddressCompany").html(res.addressCompany);
                $("#CompanyName").html(res.companyName);
                $("#CeoName").html(res.ceoName);
                $("#EconomicCode").html(res.economicCode);
                $("#EconomicCodeReal").html(res.economicCodeReal);
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

                $("#EmailRepresentative").html(res.emailRepresentative);
                $("#NationalCodeRepresentative").html(res.nationalCodeRepresentative);

                $("#AmountOsLastSales").html(moneyCommaSepWithReturn(!isEmpty(res.amountOsLastSales) ? res.amountOsLastSales.toString() : ''));

                $("#HowGetKnowCompanyId").html(res.howGetKnowCompany != null ? res.howGetKnowCompany.label : '');

                $("#KindOfCompanyId").html(res.kindOfCompany != null ? res.kindOfCompany.label : '');

                $("#divDownload").html("<a class='btn btn-success' href='/File/Download?path=" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                $("#divDownload_AuditedFinancialStatements").html("<a class='btn btn-success' href='/File/Download?path=" + res.auditedFinancialStatementsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                $("#TypeGroupCompanies").text(res.typeGroupCompaniesName);

            }


        }, true);

    }

    web.Customer = {
        FilterGrid: filterGrid,
        InitCustomer: initCustomer
    };

})(Web, jQuery);