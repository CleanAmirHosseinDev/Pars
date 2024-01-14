
var divPageingList_TotalNumberCustomersApprovedContract_pageG = 1;
function successCallBack_divPageingList_TotalNumberCustomersApprovedContract(res) {

    if (res.isSuccess) {

        $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            strM += "<tr><td>" + (i + 1) + "</td><td>"
                + res.data[i].requestNo + "</td><td>"
                + res.data[i].dateOfRequestStr + "</td><td>"
                + (!isEmpty(res.data[i].companyName) ? res.data[i].companyName : '') + "</td><td>"
                + (!isEmpty(res.data[i].agentName) ? res.data[i].agentName : '') + "</td><td>"
                + res.data[i].nationalCode + "</td><td>"
                + res.data[i].agentMobile + "</td><td>" + res.data[i].finalPriceContract + "</td></tr>";
        }

        $("#tBodyList").html(strM);
    }

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var divPageingList_TotalNumberCustomersWithoutRegistration_pageG = 1;
function successCallBack_divPageingList_TotalNumberCustomersWithoutRegistration(res) {

    if (res.isSuccess) {

        $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            strM += "<tr><td>" + (i + 1) + "</td><td>"
                + res.data[i].customerID + "</td><td>"
                + res.data[i].saveDateStr + "</td><td>"
                + (!isEmpty(res.data[i].companyName) ? res.data[i].companyName : '') + "</td><td>"
                + (!isEmpty(res.data[i].agentName) ? res.data[i].agentName : '') + "</td><td>"
                + res.data[i].nationalCode + "</td><td>"
                + res.data[i].agentMobile + "</td></tr>";
        }

        $("#tBodyList").html(strM);
    }

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var divPageingList_TotalNumberApplicationsAssessmentMinistryPrivacy_pageG = 1;
function successCallBack_divPageingList_TotalNumberApplicationsAssessmentMinistryPrivacy(res) {

    if (res.isSuccess) {

        $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            strM += "<tr><td>" + (i + 1) + "</td><td>"
                + res.data[i].requestNo + "</td><td>"
                + res.data[i].dateOfRequestStr + "</td><td>"
                + (!isEmpty(res.data[i].companyName) ? res.data[i].companyName : '') + "</td><td>"
                + (!isEmpty(res.data[i].agentName) ? res.data[i].agentName : '') + "</td><td>"
                + res.data[i].nationalCode + "</td><td>"
                + res.data[i].agentMobile + "</td><td>" + res.data[i].finalPriceContract + "</td></tr>";
        }

        $("#tBodyList").html(strM);
    }

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var divPageingList_NumberCodedFiles_pageG = 1;
function successCallBack_divPageingList_NumberCodedFiles(res) {

    if (res.isSuccess) {

        $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            strM += "<tr><td>" + (i + 1) + "</td><td>"
                + res.data[i].requestNo + "</td><td>"
                + res.data[i].dateOfRequestStr + "</td><td>"
                + (!isEmpty(res.data[i].companyName) ? res.data[i].companyName : '') + "</td><td>"
                + (!isEmpty(res.data[i].agentName) ? res.data[i].agentName : '') + "</td><td>"
                + res.data[i].nationalCode + "</td><td>"
                + res.data[i].agentMobile + "</td><td>" + res.data[i].finalPriceContract+"</td></tr>";
        }

        $("#tBodyList").html(strM);
    }

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


var divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne_Admin_pageG = 1;
function successCallBack_divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne_Admin(res) {

    if (res.isSuccess) {

        $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            strM += "<tr><td>" + res.data[i].row + "</td><td>"
                + (!isEmpty(res.data[i].companyName) ? res.data[i].companyName : '') + "</td><td>"
                + res.data[i].dateOfRequestStr + "</td><td>"
                + res.data[i].lastDateReferrals + "</td><td>"
                + res.data[i].lastSituation + "</td><td>"
                + res.data[i].waitingTimeInThisSituation + "</td></tr>";
        }

        $("#tBodyList").html(strM);
    }

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne2_Admin_pageG = 1;
function successCallBack_divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne2_Admin(res) {

    if (res.isSuccess) {

        $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            strM += "<tr><td>" + res.data[i].row + "</td><td>"
                + (!isEmpty(res.data[i].userName) ? res.data[i].userName : '') + "</td><td>"
                + (!isEmpty(res.data[i].roleName) ? res.data[i].roleName : '') + "</td><td>"
                + res.data[i].numberCompletedRequests + "</td><td>"
                + res.data[i].numberOpenAndCurrentRequests + "</td><td>"
                + res.data[i].averageResponseTimeRequestsStageSendingAdditionalInformationCustomer + "</td><td><a class='btn btn-info' href='/Admin/Report/PerformanceReportEvaluationStaffInDetail_ReportOne?id=" + res.data[i].reciveUser + "' target='_blank' title='گزارش 1'><i class='fa fa-eye'></i></a></td></tr>";
        }

        $("#tBodyList").html(strM);
    }

}

(function (web, $) {

    //Document Ready  


    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function fillComboFitterList() {


        AjaxCallAction("GET", "/api/Admin/RequestForRating/Get_UsersByRole/0", null, true, function (resGet) {

            var qD = '<option value="0" data-FieldSelectReciveUser="">انتخاب کنید</option>';
            if (resGet.isSuccess) {

                for (var i = 0; i < resGet.data.length; i++) {

                    qD += "<option value='" + resGet.data[i].userId + "' data-FieldSelectReciveUser='ReciveUserStatus' >" + (!isEmpty(resGet.data[i].user) ? resGet.data[i].user.realName : '') + "</option>";

                }

                $("#cboReciveUser").html(qD);
            }

            AjaxCallAction("POST", "/api/Admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "126", PageIndex: 0, PageSize: 0 }), true, function (res) {

                if (res.isSuccess) {
                    var strCompanyGroup = '<option value="0" data-FieldSelectStatusTypeGroupCompanies="">انتخاب کنید</option>';

                    for (var i = 0; i < res.data.length; i++) {
                        strCompanyGroup += " <option value=" + res.data[i].systemSetingId + " data-FieldSelectStatusTypeGroupCompanies='CompanyGroupStatus'>" + res.data[i].label + "</option>";
                    }

                    $("#cboTypeGroupCompanies").html(strCompanyGroup);

                }
            }, true);

        }, true);

    }

    function filterReportGrid() {
        PersianDatePicker(".DatePicker");
        ComboBoxWithSearch('.select2', 'rtl');

        AjaxCallAction("POST", "/api/Admin/RequestForRating/Get_ReportRequestForRatings", JSON.stringify({ PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val(), ReciveUser: $("#cboReciveUser").val(), TypeGroupCompanies: $("#cboTypeGroupCompanies").val() }), true, function (res) {

            if (res.isSuccess) {
                var n = getlstor("loginName");
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    var st = "", st2 = "";
                    if (res.data[i].destLevelStepAccessRole == "10" && res.data[i].destLevelStepIndex == "7") {

                        st2 = "<span style='font-size:1.5em'> &#128194;</span> ";
                    }

                    strM += "<tr><td>" + (i + 1) + "</td><td>"
                        + res.data[i].requestNo + "</td><td>"
                        + res.data[i].dateOfRequestStr + "</td><td>"
                        + (!isEmpty(res.data[i].companyName) ? res.data[i].companyName : '') + "</td><td>"
                        + (!isEmpty(res.data[i].agentName) ? res.data[i].agentName : '') + "</td><td>"
                        + res.data[i].nationalCode + "</td><td>"
                        + res.data[i].agentMobile + "</td><td>";

                    strM += (!isEmpty(res.data[i].evaluationExpert) ? res.data[i].evaluationExpert : '') + "</td>"

                    if (res.data[i].levelStepSettingIndexID == "29") {
                        strM += "<td>" + "<span style='color:red'>&#10060;" + "عدم تایید قرارداد" + "</span>" + "</td><td>";
                    } else if (res.data[i].levelStepSettingIndexID == "2") {
                        strM += "<td>" + "<span style='color:red'> &#10060; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
                    }
                    else if (res.data[i].levelStepSettingIndexID == "28") {
                        strM += "<td>" + "<span style='color:red'> &#10060; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
                    }
                    else if (res.data[i].levelStepSettingIndexID == "17") {
                        strM += "<td>" + "<span style='color:blue'> &#x2705; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
                    }
                    else if (res.data[i].levelStepSettingIndexID == "26") {
                        strM += "<td>" + "<span style='color:green'> &#x2705; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
                    }
                    else {
                        strM += "<td>" + st2
                            + (res.data[i].levelStepSettingIndexID == "13" ? " &#x2705; " : "")
                            + res.data[i].destLevelStepIndexButton + "</td><td></tr>";
                    }
                }

                $("#tBodyList").html(strM);
            }

        }, true);

    }

    function printReport(e) {

        var id = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        goToUrl("/Admin/RequestForRating/ContractPrint/" + id);
    }

    function printContracting(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/Admin/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {

                    $("#ContractShow").html(res.data.contentContract);
                    if (res.data.contractCode != null && res.data.contractCode != "") {
                        $('.PageContractFirst').find('input[name=SaveDate]').val(res.data.saveDateStr);
                        $('.PageContractFirst').find('input[name=ContractCode]').val(res.data.contractCode);


                    } else {
                        $("#ptr").html("<p> این نسخه قرارداد اصلی نمی باشد.</p>");
                    }
                    //$("#SaveDate").html(res.data.saveDateStr);
                    $("#ContractCode").html(res.data.contractCode);
                    $('input[type="text"], textarea').each(function () {
                        //  $(this).attr('readonly', 'readonly');
                        var text_classname = $(this).attr('name');
                        var value = $(this).val();
                        if (text_classname == "NamesAuthorizedSignatories") {
                            var new_html = ('<p style="text-align:center">' + value + '</p>');
                        } else if (text_classname == "NamesAuthorizedSignatories") {

                        }
                        else {
                            var new_html = ('<storang >' + value + '</storang>');
                        }
                        $(this).replaceWith(new_html);
                    });

                }

            }, true);
        }
    }

    function ComboBoxWithSearch(selector = '.select2', dir = 'rtl') {

        try {

            $(selector).select2({
                dir: dir,
                language: {
                    noResults: function (term) {
                        return "نتیجه ای پیدا نشد";
                    }
                }
            });

        } catch (e) {

        }

    }

    function excelUserReport() {

        try {
            window.open('/Admin/Report/getexcell?FromDateStr=' + $("#FromDateStr").val() + '&ToDateStr=' + $("#ToDateStr").val() + '&TypeGroupCompanies=' + $("#cboTypeGroupCompanies option:selected").val() + '&ReciveUser=' + $("#cboReciveUser option:selected").val(), '_blank');

        } catch (e) {

        }



    }

    function indexBoxAdmin() {

        AjaxCallAction("POST", "/Admin/Report/IndexBoxAdmin", {}, true, function (res) {

            $("#TotalNumberCustomersApprovedContract").html(res.totalNumberCustomersApprovedContract);

            $("#TotalNumberCustomersWithoutRegistration").html(res.totalNumberCustomersWithoutRegistration);

            $("#TotalNumberApplicationsAssessmentMinistryPrivacy").html(res.totalNumberApplicationsAssessmentMinistryPrivacy);

            $("#NumberCodedFiles").html(res.numberCodedFiles);

        }, true);

    }

    function excelTotalNumberCustomersApprovedContract() {

        window.open('/Admin/Report/GetTotalNumberCustomersApprovedContract1?FromDateStr=' + $("#FromDateStr").val() + '&ToDateStr=' + $("#ToDateStr").val() + "&Search=" + $("#txtSearch").val(), '_blank');

    }

    function filterReportGrid_TotalNumberCustomersApprovedContract() {

        pageingGrid("divPageingList_TotalNumberCustomersApprovedContract", "/Admin/Report/GetTotalNumberCustomersApprovedContract", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));

    }

    function init_TotalNumberCustomersApprovedContract() {

        PersianDatePicker(".DatePicker");

        filterReportGrid_TotalNumberCustomersApprovedContract();
    }

    function init_TotalNumberCustomersWithoutRegistration() {

        PersianDatePicker(".DatePicker");

        filterReportGrid_TotalNumberCustomersWithoutRegistration();

    }

    function filterReportGrid_TotalNumberCustomersWithoutRegistration() {

        pageingGrid("divPageingList_TotalNumberCustomersWithoutRegistration", "/Admin/Report/GetTotalNumberCustomersWithoutRegistration", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));

    }

    function excelTotalNumberCustomersWithoutRegistration() {

        window.open('/Admin/Report/GetTotalNumberCustomersWithoutRegistration1?FromDateStr=' + $("#FromDateStr").val() + '&ToDateStr=' + $("#ToDateStr").val() + "&Search=" + $("#txtSearch").val(), '_blank');

    }

    function init_TotalNumberApplicationsAssessmentMinistryPrivacy() {

        PersianDatePicker(".DatePicker");

        filterReportGrid_TotalNumberApplicationsAssessmentMinistryPrivacy();

    }

    function filterReportGrid_TotalNumberApplicationsAssessmentMinistryPrivacy() {

        pageingGrid("divPageingList_TotalNumberApplicationsAssessmentMinistryPrivacy", "/Admin/Report/GetTotalNumberApplicationsAssessmentMinistryPrivacy", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));

    }

    function excelTotalNumberApplicationsAssessmentMinistryPrivacy() {

        window.open('/Admin/Report/GetTotalNumberApplicationsAssessmentMinistryPrivacy1?FromDateStr=' + $("#FromDateStr").val() + '&ToDateStr=' + $("#ToDateStr").val() + "&Search=" + $("#txtSearch").val(), '_blank');

    }

    function init_NumberCodedFiles() {

        PersianDatePicker(".DatePicker");

        filterReportGrid_NumberCodedFiles();

    }

    function filterReportGrid_NumberCodedFiles() {

        pageingGrid("divPageingList_NumberCodedFiles", "/Admin/Report/GetNumberCodedFiles", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));

    }

    function excelNumberCodedFiles() {

        window.open('/Admin/Report/GetNumberCodedFiles1?FromDateStr=' + $("#FromDateStr").val() + '&ToDateStr=' + $("#ToDateStr").val() + "&Search=" + $("#txtSearch").val(), '_blank');

    }

    function initPerformanceReportEvaluationStaffInDetail_ReportOne(id = 0) {

        AjaxCallAction("GET", "/api/admin/RequestForRating/Get_UsersByRole/0", null, true, function (resGet) {

            var qD = '<option value="0">انتخاب کنید</option>';
            if (resGet.isSuccess) {

                for (var i = 0; i < resGet.data.length; i++) {

                    qD += "<option value='" + resGet.data[i].userId + "' >" + (!isEmpty(resGet.data[i].user) ? resGet.data[i].user.realName : '') + "</option>";

                }

                $("#cboReciveUser").html(qD);
                $("#cboReciveUser").val(id);

                PersianDatePicker(".DatePicker");
                ComboBoxWithSearch('.select2', 'rtl');
                filterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne();

                fillComboLevelStepSettingList();

            }

        }, true);

    }

    function filterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne() {

        pageingGrid("divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne_Admin", "/Admin/Report/GetPerformanceReportEvaluationStaffInDetail_ReportOne", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val(), ReciveUser: $("#cboReciveUser").val(), FromLastDateReferrals: $("#FromLastDateReferrals").val(), ToLastDateReferrals: $("#ToLastDateReferrals").val(), cboSelectLS: $("#cboSelectLS").val() }));

    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

    function initPerformanceReportEvaluationStaffInDetail_ReportOne2() {

        PersianDatePicker(".DatePicker");
        filterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne2();

    }

    function filterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne2() {

        pageingGrid("divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne2_Admin", "/Admin/Report/GetPerformanceReportEvaluationStaffInDetail_ReportOne2", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));

    }

    function fillComboLevelStepSettingList() {

        AjaxCallAction("POST", "/api/admin/RequestForRating/Get_LevelStepSetings", JSON.stringify({ PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    strKindOfCompany += " <option value=" + res.data[i].levelStepIndex + ">" + res.data[i].levelStepStatus + "</option>";
                }

                $("#cboSelectLS").html(strKindOfCompany);



            }

            AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "63", PageIndex: 0, PageSize: 0 }), true, function (res) {

                if (res.isSuccess) {
                    var strKindOfRequest = '<option value="">انتخاب کنید</option>';

                    for (var i = 0; i < res.data.length; i++) {
                        strKindOfRequest += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }

                    $("#cboKindOfRequest").html(strKindOfRequest);

                }
            }, true);

        }, true);

    }

    function onchangeKindOfRequest(e) {

        AjaxCallAction("POST", "/api/admin/RequestForRating/Get_LevelStepSetings", JSON.stringify({ PageIndex: 0, PageSize: 0, KindOfRequest: !isEmpty($(e).val()) ? $(e).val() : null }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    strKindOfCompany += " <option value=" + res.data[i].levelStepIndex + ">" + res.data[i].levelStepStatus + "</option>";
                }

                $("#cboSelectLS").html(strKindOfCompany);



            }

        }, true);

    }

    web.Report = {
        OnchangeKindOfRequest: onchangeKindOfRequest,
        FillComboLevelStepSettingList: fillComboLevelStepSettingList,
        FillComboFitterList: fillComboFitterList,
        FilterReportGrid: filterReportGrid,
        TextSearchOnKeyDown: textSearchOnKeyDown,
        PrintReport: printReport,
        PrintContracting: printContracting,
        ExcelUserReport: excelUserReport,
        IndexBoxAdmin: indexBoxAdmin,
        ExcelTotalNumberCustomersApprovedContract: excelTotalNumberCustomersApprovedContract,
        FilterReportGrid_TotalNumberCustomersApprovedContract: filterReportGrid_TotalNumberCustomersApprovedContract,
        Init_TotalNumberCustomersApprovedContract: init_TotalNumberCustomersApprovedContract,
        Init_TotalNumberCustomersWithoutRegistration: init_TotalNumberCustomersWithoutRegistration,
        FilterReportGrid_TotalNumberCustomersWithoutRegistration: filterReportGrid_TotalNumberCustomersWithoutRegistration,
        ExcelTotalNumberCustomersWithoutRegistration: excelTotalNumberCustomersWithoutRegistration,
        Init_TotalNumberApplicationsAssessmentMinistryPrivacy: init_TotalNumberApplicationsAssessmentMinistryPrivacy,
        FilterReportGrid_TotalNumberApplicationsAssessmentMinistryPrivacy: filterReportGrid_TotalNumberApplicationsAssessmentMinistryPrivacy,
        ExcelTotalNumberApplicationsAssessmentMinistryPrivacy: excelTotalNumberApplicationsAssessmentMinistryPrivacy,
        Init_NumberCodedFiles: init_NumberCodedFiles,
        FilterReportGrid_NumberCodedFiles: filterReportGrid_NumberCodedFiles,
        ExcelNumberCodedFiles: excelNumberCodedFiles,
        InitPerformanceReportEvaluationStaffInDetail_ReportOne: initPerformanceReportEvaluationStaffInDetail_ReportOne,
        FilterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne: filterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne,
        InitPerformanceReportEvaluationStaffInDetail_ReportOne2: initPerformanceReportEvaluationStaffInDetail_ReportOne2,
        FilterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne2: filterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne2

    };

})(Web, jQuery);