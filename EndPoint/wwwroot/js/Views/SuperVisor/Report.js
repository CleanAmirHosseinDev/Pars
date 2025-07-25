
var divPageingList_TotalNumberCustomersApprovedContractSupervisor_pageG = 1;
function successCallBack_divPageingList_TotalNumberCustomersApprovedContractSupervisor(res) {

    if (res.isSuccess) {

        $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            strM += "<tr><td>" + (i + 1) + "</td><td>"
                + res.data[i].requestNo + "</td><td>"

                + res.data[i].sendTimeStr + "</td><td>"
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

var divPageingList_TotalNumberCustomersWithoutRegistrationSupervisor_pageG = 1;
function successCallBack_divPageingList_TotalNumberCustomersWithoutRegistrationSupervisor(res) {

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

var divPageingList_TotalNumberApplicationsAssessmentMinistryPrivacySupervisor_pageG = 1;
function successCallBack_divPageingList_TotalNumberApplicationsAssessmentMinistryPrivacySupervisor(res) {

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

var divPageingList_TotalNumberCorporateRequest_pageG = 1;
function successCallBack_divPageingList_TotalNumberCorporateRequest(res) {

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

var divPageingList_NumberCodedFiles_Supervisor_pageG = 1;
function successCallBack_divPageingList_NumberCodedFiles_Supervisor(res) {

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
                + res.data[i].agentMobile + "</td><td>"
                + (!isEmpty(res.data[i].finalPriceContract) ? res.data[i].finalPriceContract : '0') + "</td><td>"
                + (!isEmpty(res.data[i].codalDate) ? res.data[i].codalDate : '') + "</td><td>"
                + (!isEmpty(res.data[i].codalNumber) ? res.data[i].codalNumber : '') + "</td></tr>";
        }

        $("#tBodyList").html(strM);
    }

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne_Supervisor_pageG = 1;
function successCallBack_divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne_Supervisor(res) {

    if (res.isSuccess) {

        $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            strM += "<tr><td>" + res.data[i].row + "</td><td>"
                + (!isEmpty(res.data[i].companyName) ? res.data[i].companyName : '') + "</td><td>"
                + res.data[i].dateOfRequestStr + "</td><td>"
                + res.data[i].lastDateReferrals + "</td><td>"
                + res.data[i].lastSituation + "</td><td>"
                + "<a href='~/superVisor/RequestForRating/RequestReferences?id='" + res.data[i].requestID + "'>مشاهده گردش</a></td><td>"
                + res.data[i].waitingTimeInThisSituation + "</td></tr>";
        }

        $("#tBodyList").html(strM);
    }

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne2_Supervisor_pageG = 1;
function successCallBack_divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne2_Supervisor(res) {

    if (res.isSuccess) {

        $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            strM += "<tr><td>" + res.data[i].row + "</td><td>"
                + (!isEmpty(res.data[i].userName) ? res.data[i].userName : '') + "</td><td>"
                + (!isEmpty(res.data[i].roleName) ? res.data[i].roleName : '') + "</td><td>"
                + res.data[i].numberCompletedRequests + "</td><td>"
                + res.data[i].numberOpenAndCurrentRequests + "</td><td>"
                + res.data[i].averageResponseTimeRequestsStageSendingAdditionalInformationCustomer + "</td><td><a href='/Supervisor/Report/PerformanceReportEvaluationStaffInDetail_ReportOne?id=" + res.data[i].reciveUser + "' target='_blank' title='گزارش 1'><i class='fa fa-eye'></i></a></td></tr>";
        }

        $("#tBodyList").html(strM);
    }

}

(function (web, $) {

    //Document Ready  


    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function excelTotalNumberCustomersApprovedContract() {

        window.open('/SuperVisor/Report/GetTotalNumberCustomersApprovedContract1?FromDateStr=' + $("#FromDateStr").val() + '&ToDateStr=' + $("#ToDateStr").val() + "&Search=" + $("#txtSearch").val(), '_blank');

    }

    function filterReportGrid_TotalNumberCustomersApprovedContract() {

        pageingGrid("divPageingList_TotalNumberCustomersApprovedContractSupervisor", "/SuperVisor/Report/GetTotalNumberCustomersApprovedContract", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));

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

        pageingGrid("divPageingList_TotalNumberCustomersWithoutRegistrationSupervisor", "/SuperVisor/Report/GetTotalNumberCustomersWithoutRegistration", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));

    }

    function excelTotalNumberCustomersWithoutRegistration() {

        window.open('/SuperVisor/Report/GetTotalNumberCustomersWithoutRegistration1?FromDateStr=' + $("#FromDateStr").val() + '&ToDateStr=' + $("#ToDateStr").val() + "&Search=" + $("#txtSearch").val(), '_blank');

    }

    function init_TotalNumberApplicationsAssessmentMinistryPrivacy() {

        PersianDatePicker(".DatePicker");

        filterReportGrid_TotalNumberApplicationsAssessmentMinistryPrivacy();

    }

    function filterReportGrid_TotalNumberApplicationsAssessmentMinistryPrivacy() {

        pageingGrid("divPageingList_TotalNumberApplicationsAssessmentMinistryPrivacySupervisor", "/SuperVisor/Report/GetTotalNumberApplicationsAssessmentMinistryPrivacy", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));

    }

    function excelTotalNumberApplicationsAssessmentMinistryPrivacy() {

        window.open('/SuperVisor/Report/GetTotalNumberApplicationsAssessmentMinistryPrivacy1?FromDateStr=' + $("#FromDateStr").val() + '&ToDateStr=' + $("#ToDateStr").val() + "&Search=" + $("#txtSearch").val(), '_blank');

    }

    function init_NumberCodedFiles() {

        PersianDatePicker(".DatePicker");

        filterReportGrid_NumberCodedFiles();

    }

    function filterReportGrid_NumberCodedFiles() {

        pageingGrid("divPageingList_NumberCodedFiles_Supervisor", "/SuperVisor/Report/GetNumberCodedFiles", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));

    }

    function excelNumberCodedFiles() {

        window.open('/SuperVisor/Report/GetNumberCodedFiles1?FromDateStr=' + $("#FromDateStr").val() + '&ToDateStr=' + $("#ToDateStr").val() + "&Search=" + $("#txtSearch").val(), '_blank');

    }

    function indexBoxSupervisor() {
        AjaxCallAction("POST", "/SuperVisor/Report/IndexBoxSuperVisor", {}, true, function (res) {

            $("#TotalNumberCustomersApprovedContract").html(res.totalNumberCustomersApprovedContract);

            $("#TotalNumberCustomersWithoutRegistration").html(res.totalNumberCustomersWithoutRegistration);

            $("#TotalNumberApplicationsAssessmentMinistryPrivacy").html(res.totalNumberApplicationsAssessmentMinistryPrivacy);

            $("#NumberCodedFiles").html(res.numberCodedFiles);

            $("#NumberCorporateCustomer").html(res.numberCorporateCustomer);

        }, true);

        let CorporateAccess = ["11", "12"];
        let ContractAccess = ["4", "9", "11", "12"];

        if (CorporateAccess.includes(getlstor("loginName"))) {
            $("#ShowBoxSamt").remove();
        }
        if (!ContractAccess.includes(getlstor("loginName"))) {
            $("#ShowBoxCorporate").remove();
        }
    }

    function initPerformanceReportEvaluationStaffInDetail_ReportOne(id = 0) {

        AjaxCallAction("GET", "/api/Supervisor/RequestForRating/Get_UsersByRole/0", null, true, function (resGet) {

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

        pageingGrid("divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne_Supervisor", "/Supervisor/Report/GetPerformanceReportEvaluationStaffInDetail_ReportOne", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val(), ReciveUser: $("#cboReciveUser").val(), FromLastDateReferrals: $("#FromLastDateReferrals").val(), ToLastDateReferrals: $("#ToLastDateReferrals").val(), cboSelectLS: $("#cboSelectLS").val() }));

    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

    function initPerformanceReportEvaluationStaffInDetail_ReportOne2() {

        PersianDatePicker(".DatePicker");
        filterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne2();

    }

    function filterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne2() {

        pageingGrid("divPageingList_PerformanceReportEvaluationStaffInDetail_ReportOne2_Supervisor", "/Supervisor/Report/GetPerformanceReportEvaluationStaffInDetail_ReportOne2", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));

    }

    function fillComboLevelStepSettingList() {

        AjaxCallAction("POST", "/api/supervisor/RequestForRating/Get_LevelStepSetings", JSON.stringify({ PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    strKindOfCompany += " <option value=" + res.data[i].levelStepIndex + ">" + res.data[i].levelStepStatus + "</option>";
                }

                $("#cboSelectLS").html(strKindOfCompany);



            }

            AjaxCallAction("POST", "/api/supervisor/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "63", PageIndex: 0, PageSize: 0 }), true, function (res) {

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

        AjaxCallAction("POST", "/api/supervisor/RequestForRating/Get_LevelStepSetings", JSON.stringify({ PageIndex: 0, PageSize: 0, KindOfRequest: !isEmpty($(e).val()) ? $(e).val() : null }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfCompany = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    strKindOfCompany += " <option value=" + res.data[i].levelStepIndex + ">" + res.data[i].levelStepStatus + "</option>";
                }

                $("#cboSelectLS").html(strKindOfCompany);



            }

        }, true);

    }

    function init_TotalNumberCorporateRequest() {
        PersianDatePicker(".DatePicker");
        filterReportGrid_TotalNumberCorporateRequest();
    }

    function filterReportGrid_TotalNumberCorporateRequest() {
        pageingGrid("divPageingList_TotalNumberCorporateRequest", "/SuperVisor/Report/GetTotalNumberCorporateRequest", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));
    }

    function excelTotalNumberCorporateRequest() {
        window.open('/SuperVisor/Report/GetExcelTotalNumberCorporateRequest?FromDateStr=' + $("#FromDateStr").val() + '&ToDateStr=' + $("#ToDateStr").val() + "&Search=" + $("#txtSearch").val(), '_blank');
    }

    web.Report = {
        OnchangeKindOfRequest: onchangeKindOfRequest,
        FillComboLevelStepSettingList: fillComboLevelStepSettingList,
        TextSearchOnKeyDown: textSearchOnKeyDown,
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
        IndexBoxSupervisor: indexBoxSupervisor,
        InitPerformanceReportEvaluationStaffInDetail_ReportOne: initPerformanceReportEvaluationStaffInDetail_ReportOne,
        FilterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne: filterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne,
        InitPerformanceReportEvaluationStaffInDetail_ReportOne2: initPerformanceReportEvaluationStaffInDetail_ReportOne2,
        FilterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne2: filterReportGrid_PerformanceReportEvaluationStaffInDetail_ReportOne2,

        Init_TotalNumberCorporateRequest: init_TotalNumberCorporateRequest,
        FilterReportGrid_TotalNumberCorporateRequest: filterReportGrid_TotalNumberCorporateRequest,
        ExcelTotalNumberCorporateRequest: excelTotalNumberCorporateRequest,
        FilterStalledRequestsReport: filterStalledRequestsReport,
    };

    var divPageingList_StalledRequestsReport_pageG = 1;
    function successCallBack_divPageingList_StalledRequestsReport(res) {
        if (res.isSuccess) {
            $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
            var strM = '';
            for (var i = 0; i < res.data.length; i++) {
                strM += "<tr><td>" + (i + 1) + "</td><td>"
                    + res.data[i].companyName + "</td><td>"
                    + res.data[i].requestNo + "</td><td>"
                    + res.data[i].dateOfRequestStr + "</td><td>"
                    + res.data[i].status + "</td><td>"
                    + res.data[i].delayInDays + "</td></tr>";
            }
            $("#tBodyList").html(strM);
        }
    }

    function filterStalledRequestsReport() {
        pageingGrid("divPageingList_StalledRequestsReport", "/SuperVisor/Report/GetStalledRequestsReport", JSON.stringify({
            PageIndex: 1,
            PageSize: $("#cboSelectCount").val(),
            FromDateStr: $("#FromDateStr").val(),
            ToDateStr: $("#ToDateStr").val(),
            Category: $("#cboCategory").val()
        }));
    }

})(Web, jQuery);