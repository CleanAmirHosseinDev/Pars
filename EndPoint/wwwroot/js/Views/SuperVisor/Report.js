
var divPageingList_TotalNumberCustomersApprovedContractSupervisor_pageG = 1;
function successCallBack_divPageingList_TotalNumberCustomersApprovedContractSupervisor(res) {

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
                + res.data[i].agentMobile + "</td></tr>";
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
                + res.data[i].agentMobile + "</td></tr>";
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
                + res.data[i].agentMobile + "</td></tr>";
        }

        $("#tBodyList").html(strM);
    }

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

(function (web, $) {

    //Document Ready  


    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }             

    function excelTotalNumberCustomersApprovedContract() {

        window.open('/SuperVisor/Report/GetTotalNumberCustomersApprovedContract1?FromDateStr=' + $("#FromDateStr").val() + '&ToDateStr=' + $("#ToDateStr").val() + "&Search=" + $("#txtSearch").val(), '_blank');

    }

    function filterReportGrid_TotalNumberCustomersApprovedContract() {        

        pageingGrid("divPageingList_TotalNumberCustomersApprovedContractSupervisor", "/SuperVisor/Report/GetTotalNumberCustomersApprovedContract", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(),FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }));

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

        }, true);
    }

    web.Report = {        
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
        IndexBoxSupervisor: indexBoxSupervisor
    };

})(Web, jQuery);