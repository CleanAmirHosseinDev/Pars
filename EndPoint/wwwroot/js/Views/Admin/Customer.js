
var divPageingList_pageG = 1;
function successCallBack_divPageingList (res) {

    if (res.isSuccess) {

        var strM = '';
        $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
        for (var i = 0; i < res.data.length; i++) {

            strM += "<tr><td>" + (i + 1) + "</td>";
            strM += "<td>" + (isEmpty(res.data[i].companyName) ? '' : res.data[i].companyName) + "</td>";
            strM += "<td>" + (isEmpty(res.data[i].agentName) ? '' : res.data[i].agentName) + "</td>";
            strM += "<td>" + (isEmpty(res.data[i].agentMobile) ? '' : res.data[i].agentMobile) + "</td>";
            strM += "<td>" + res.data[i].saveDateStr + "</td>";
            strM += "<td>" + (res.data[i].isProfileComplete ? 'ثبت درخواست' : 'عدم ثبت درخواست') + "</td>";
            strM += "<td>";
            strM += "<a title='ویرایش'  href='/Admin/Customers/EditCustomers?id=" + res.data[i].customerId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a>";
            strM += "<a title='حذف' class='btn btn-danger style='margin-left:5px' fontForAllPage' onclick='Web.Customer.Delete_Customers(" + res.data[i].customerId + ");'><i class='fa fa-remove'></i></a>";
            strM += "<a title='نمایش لیست درخواست های مشتری' style='margin-left:5px'  class='btn btn-info fontForAllPage' href='/Admin/RequestForRating/Index/" + res.data[i].customerId + "'><i class='fa fa-line-chart'></i></a>";
            strM += "</td></tr>";
        }
        $("#tBodyList").html(strM);

    }

}

(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function saveCustomer(e) {

        $(e).attr("disabled", "");
        RemoveAllCharForPrice("AmountOsLastSales");
        AjaxCallActionPostSaveFormWithUploadFile("/api/admin/Customers/Save_Customers", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                /*alertB("ثبت", res.message, "success");*/
                /*$("SeeAllRequest").show();*/
                goToUrl("/Admin/Customers/Index");

            } else {

                $("#AmountOsLastSales").val(moneyCommaSepWithReturn($("#AmountOsLastSales").val()));
                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "126,63,27,56,221", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';
                var strHowGetKnowCompany = '<option value="">انتخاب کنید</option>';
                var strTypeGroupCompanies = '<option value="">انتخاب کنید</option>';
                var strCustomerPersonalityType = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].parentCode == 56) {
                        strHowGetKnowCompany += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 27) {
                        strKindOfCompany += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                    else if (res.data[i].parentCode == 126) {
                        strTypeGroupCompanies += "<option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                    else if (res.data[i].parentCode == 221) {
                        strCustomerPersonalityType += "<option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
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

            }
        }, true);
    }

    function initCustomer(id, dir = 'rtl') {

        ComboBoxWithSearch('.select2', dir);
        AjaxCallAction("GET", "/api/admin/Customers/Get_Customers/" + (isEmpty(id) ? '0' : id), null, true, function (res) {


            if (res != null) {

                $("#CustomerId").val(res.customerId);
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
                $("#EconomicCodeReal").val(res.economicCodeReal);
                $("#PostalCode").val(res.postalCode);
                $("#AmountOsLastSales").val(moneyCommaSepWithReturn(!isEmpty(res.amountOsLastSales) ? res.amountOsLastSales.toString() : ''));

                $("#EmailRepresentative").val(res.emailRepresentative);
                $("#NationalCodeRepresentative").val(res.nationalCodeRepresentative);

                $("#divDownload").html("<a class='btn btn-success' href='/File/Download?path=" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                $("#divDownload_AuditedFinancialStatements").html("<a class='btn btn-success' href='/File/Download?path=" + res.auditedFinancialStatementsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                systemSeting_Combo(res);

            }


        }, true);

    }
        
    function filterGrid() {

        pageingGrid("divPageingList", "/api/admin/Customers/Get_Customerss", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }));

    }

    function delete_Customers(id) {

        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/admin/Customers/Delete_Customers/" + (isEmpty(id) ? '0' : id), null, true, function (result) {

                    debuggerWeb();

                    if (result.isSuccess) {

                        filterGrid();

                        alertB("", result.message, "success");

                    }
                    else {

                        alertB("خطا", result.message, "error");

                    }

                }, true);

            }, function () {

            }, ["خیر", "بلی"]);

        } catch (e) {

        }

    }

    function onChangeCustomerPersonalityType(e) {

        if ($(e).val() == "223") {

            $(".form-group").hide();
            $(".form-group.FormIsShow").show();
            $(".NotShowRequiredLabel").hide();
            $("#LabelTypeGroupCompanies").html("نوع فعالیت");
            $("#LabelEconomicCode").html("شماره کارت بازرگانی");

        }
        else {

            $(".form-group").show();
            $(".NotShowRequiredLabel").show();
            $("#LabelTypeGroupCompanies").html("نوع گروه شرکتها");
            $("#LabelEconomicCode").html("شماره ثبت");
        }

    }

    web.Customer = {                
        FilterGrid: filterGrid,
        TextSearchOnKeyDown: textSearchOnKeyDown,
        SaveCustomer: saveCustomer,
        InitCustomer: initCustomer,
        SystemSeting_Combo: systemSeting_Combo,
        Delete_Customers: delete_Customers,
        OnChangeCustomerPersonalityType: onChangeCustomerPersonalityType
    };

})(Web, jQuery);
