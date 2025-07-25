﻿
(function (web, $) {

    //Document Ready              

    function filterGrid() {

        if (getlstor("roleDesc") != "ارزیاب") {
            AjaxCallAction("POST", "/api/superVisor/Customers/Get_Customerss", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

                if (res.isSuccess) {
                    $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                    var strM = '';
                    for (var i = 0; i < res.data.length; i++) {

                        strM += "<tr><td>" + (i + 1) + "</td><td>" + (isEmpty(res.data[i].companyName) ? '' : res.data[i].companyName)
                            + "</td><td>" + (isEmpty(res.data[i].nationalCode) ? '' : res.data[i].nationalCode)
                            + "</td><td>" + (isEmpty(res.data[i].agentName) ? '' : res.data[i].agentName)
                            + "</td><td>" + (isEmpty(res.data[i].agentMobile) ? '' : res.data[i].agentMobile)
                            + "</td><td>" + res.data[i].saveDateStr
                            + "</td><td>" + (res.data[i].haveRequest ? 'ثبت درخواست' : 'عدم ثبت درخواست')
                            + "</td><td><a title='نمایش' href='/SuperVisor/Customers/ShowCustomers?id=" + res.data[i].customerId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-eye'></i></a></td></tr>";

                    }
                    $("#tBodyList").html(strM);

                }

            }, true);
        }
        else {
            $(".content-header").hide();
            $("#divMainIndexCustomersList").html("<div class='container'><div class='row'><div class='col-md-12'><div class='alert alert-warning text-center'>شما مجوز انجام عملیات را ندارید</div></div></div></div>");
        }

    }

    function initCustomer(id, dir = 'rtl') {

        if (getlstor("roleDesc") != "ارزیاب") {
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
                        $("#divMainScanCustomerNationalCard").show();
                        $("#divMainScanManagerNationalCard").hide();
                    }
                    else {

                        $(".form-group").show();
                        $(".NotShowRequiredLabel").show();
                        $("#LabelTypeGroupCompanies").html("نوع فعالیت شرکتها");
                        $("#LabelEconomicCode").html("شماره ثبت");
                        $("#LabelCompanyName").html("نام شرکت");
                        $("#Span_Label_AgentMobile").html("شماره نماینده");
                        $("#divMainScanCustomerNationalCard").show();
                        $("#divMainScanManagerNationalCard").show();
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

                    if (res.lastInsuranceList != null && res.lastInsuranceList != "") {
                        $("#divDownload").html("<a class='btn btn-success' href='" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                    }
                    if (res.auditedFinancialStatements != null && res.auditedFinancialStatements != "") {
                        $("#divDownload_AuditedFinancialStatements").html("<a class='btn btn-success' href='" + res.auditedFinancialStatementsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                    }
                    if (res.scanCustomerNationalCard != null && res.scanCustomerNationalCard != "") {
                        $("#divDownload_ScanCustomerNationalCard").html("<a class='btn btn-success' href='" + res.scanCustomerNationalCardFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                    }
                    if (res.scanManagerNationalCard != null && res.scanManagerNationalCard != "") {
                        $("#divDownload_ScanManagerNationalCard").html("<a class='btn btn-success' href='" + res.scanManagerNationalCardFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                    }

                    $("#TypeGroupCompanies").text(res.typeGroupCompaniesName);

                }


            }, true);
        }
        else {
            $(".content-header").hide();
            $("#divMainIndexCustomersSingleCustomer").html("<div class='container'><div class='row'><div class='col-md-12'><div class='alert alert-warning text-center'>شما مجوز انجام عملیات را ندارید</div></div></div></div>");
        }

    }

    web.Customer = {
        FilterGrid: filterGrid,
        InitCustomer: initCustomer
    };

})(Web, jQuery);