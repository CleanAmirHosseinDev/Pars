
(function (web, $) {

    //Document Ready  

   
    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function fillComboFitterList() {


        AjaxCallAction("GET", "/api/Admin/RequestForRating/Get_UsersByRole/0" , null, true, function (resGet) {

            var qD = '<option value="0">انتخاب کنید</option>';
            if (resGet.isSuccess) {

                for (var i = 0; i < resGet.data.length; i++) {

                    qD += "<option value='" + resGet.data[i].userId + "'>" + (!isEmpty(resGet.data[i].user) ? resGet.data[i].user.realName : '') + "</option>";

                }

                $("#cboReciveUser").html(qD);
            }

            AjaxCallAction("POST", "/api/Admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "126", PageIndex: 0, PageSize: 0 }), true, function (res) {

                if (res.isSuccess) {
                    var strCompanyGroup = '<option value="0">انتخاب کنید</option>';

                    for (var i = 0; i < res.data.length; i++) {
                        strCompanyGroup += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
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

                    strM += (!isEmpty(res.data[i].evaluationExpert) ? res.data[i].evaluationExpert : '') +"</td>"

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
    web.Report = {
        FillComboFitterList: fillComboFitterList,
        FilterReportGrid: filterReportGrid,
        TextSearchOnKeyDown: textSearchOnKeyDown,
        PrintReport: printReport,
        PrintContracting: printContracting,       
    };

})(Web, jQuery);