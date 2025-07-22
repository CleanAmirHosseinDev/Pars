


var divPageingList_RequestForRatingsAdmin_pageG = 1;
function successCallBack_divPageingList_RequestForRatingsAdmin(res) {
    if (!res.isSuccess) return;

    var strM = '';
    for (var i = 0; i < res.data.length; i++) {
        var row = res.data[i];
        var st2 = (row.destLevelStepAccessRole == "10" && row.destLevelStepIndex == "7")
            ? "<span style='font-size:1.5em'>&#128194;</span>"
            : "";

        strM += "<tr>";

        strM += "<td>" + (i + 1) + "</td>";
        strM += "<td>" + row.requestNo + "</td>";
        strM += "<td>" + (isEmpty(row.companyName) ? '' : row.companyName) + "</td>";
        strM += "<td>" + row.agentMobile + "</td>";
        strM += "<td>" + row.dateOfRequestStr + "</td>";
        strM += "<td>" + row.reciveUserName + "</td>";

        strM += "<td>";
        if (!isEmpty(row.assessment)) {
            var qe = row.assessment.split(",");
            strM += generateAssessment(qe[0]);
            if (qe.length == 2) {
                strM += "<hr/>";
                strM += {
                    "0": "امتیاز",
                    "1": "مدت زمان فرایند",
                    "2": "کارشناس ارزیابی",
                    "3": "فرایند صدور قرارداد",
                    "4": "فرایند صدور فاکتور"
                }[qe[1]] || "";
            }
        }
        strM += "</td>";

        strM += "<td>" + (isEmpty(row.reasonAssessment1) ? '' : row.reasonAssessment1) + "</td>";

        strM += "<td>";
        if (row.levelStepSettingIndexID == "29") {
            strM += "<span style='color:red'>&#10060; عدم تایید قرارداد</span>";
        } else if (row.destLevelStepIndexButton == "ارجاع به مشتری جهت اصلاح مشخصات اولیه توسط مشتری") {
            strM += "<span style='color:red'>&#10060; " + row.destLevelStepIndexButton + "</span>";
        } else {
            strM += st2;
            if (row.levelStepSettingIndexID == "13") strM += " &#x2705; ";
            strM += row.levelStepStatus;
        }
        strM += "</td>";

        strM += "<td>" + row.lastStatusChangeDateStr + "</td>";

        strM += "<td>";
        strM += "<a title='حذف درخواست' class='btn btn-danger fontForAllPage' style='margin-left:5px' onclick='Web.RequestForRating.CancelRequest(" + row.requestId + ");'><i class='fa fa-remove'></i></a>";

        if (row.customerRequestInformationId != 0 && row.contractDocument == null) {
            strM += "<a style='margin-right:5px;color:black' title='بروزرسانی اطلاعات مالی بیمه' class='btn btn-default fontForAllPage' href='/admin/RequestForRating/EditCustomerRequestInformation/" + row.requestId + "'><i class='fa fa-edit'></i> بروزرسانی اطلاعات مالی بیمه</a>";
        }

        strM += "<a style='margin-right:5px;color:black' href='/admin/RequestForRating/RequestReferences?id=" + row.requestId + "' class='btn btn-info fontForAllPage'>";
        strM += "<img src='/css/GlobalAreas/dist/img/timeline-icon.png' style='width:20px' title='مشاهده گردش کار'></a>";
        strM += "</td>";

        strM += "</tr>";
    }

    $("#tBodyList").html(strM);
}

function generateAssessment(qe) {

    switch (qe) {

        case "0":
            /*return "<i class='far fa-laugh-beam' style='color:#006400;' title='خیلی خوشحال'></i>";*/
            return "خیلی خوشحال";
        case "1":
            /*return "<i class='far fa-grin-beam' style='color:#006400;' title='خوشحال'></i>";*/
            return "خوشحال";
        case "2":
            /*return "<i class='far fa-meh' style='color:#006400;' title='معمولی'></i>";*/
            return "معمولی";
        case "3":
            /*return "<i class='far fa-flushed' style='color:#006400;' title='غمگین'></i>";*/
            return "غمگین";
        case "4":
            /*return "<i class='far fa-angry' style='color:#006400;' title='افسرده'></i>";*/
            return "افسرده";
    }

}

(function (web, $) {
    function textSearchOnKeyDown(event) {
        if (event.keyCode == 13) $(`button[title='جستجو']`).click();
    }

    function filterGrid(id = null) {
        ComboBoxWithSearch('.select2', 'rtl');

        const request = {
            CustomerId: id,
            PageIndex: 1,
            PageSize: $("#cboSelectCount").val(),
            Search: $("#txtSearch").val(),
            DestLevelStepIndex: isEmpty($("#cboSelectLS").val()) ? null : $("#cboSelectLS").val(),
            KindOfRequest: !isEmpty($("#cboKindOfRequest").val()) ? $("#cboKindOfRequest").val() : null,
            SortColumn: currentSortColumn,
            SortDirection: currentSortDirection
        };

        pageingGrid("divPageingList_RequestForRatingsAdmin", "/api/admin/RequestForRating/Get_RequestForRatings", JSON.stringify(request));
    }

    let currentSortColumn = null;
    let currentSortDirection = null;
    function clickSortingRequestForRatingGrid(e) {
        const $th = $(e);
        const sortFieldAsc = $th.attr("data-A");
        const sortFieldDesc = $th.attr("data-D");

        let columnBaseName = sortFieldAsc ? sortFieldAsc.replace("_A", "") : null;
        if (!columnBaseName) return;

        if (currentSortColumn === columnBaseName) {
            currentSortDirection = currentSortDirection === "ASC" ? "DESC" : "ASC";
        } else {
            currentSortColumn = columnBaseName;
            currentSortDirection = "ASC";
        }

        $(".sortable").removeAttr("data-sort-order");
        $(".sortable .sort-icon")
            .removeClass("fa-arrow-up fa-arrow-down")
            .css("transform", "");

        $th.attr("data-sort-order", currentSortDirection.toLowerCase());

        const $icon = $th.find(".sort-icon");
        if (currentSortDirection === "ASC") {
            $icon.addClass("fa-arrow-up").css("transform", "rotate(180deg)");
        } else {
            $icon.addClass("fa-arrow-down").css("transform", "rotate(0deg)");
        }

        filterGrid();
    }

    function cancelRequest(id) {

        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/admin/RequestForRating/Delete_RequestForRating/" + (isEmpty(id) ? '0' : id), null, true, function (result) {

                    debuggerWeb();

                    if (result.isSuccess) {
                        let yCustomerI = $("#MyCustomerID").val();
                        filterGrid(yCustomerI);

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

    function initRequestReferences(id = null) {

        AjaxCallAction("POST", "/api/admin/RequestForRating/Get_RequestForRatings", JSON.stringify({ Search: null, PageIndex: 1, PageSize: 1, RequestId: id }), true, function (res) {

            if (res.isSuccess) {
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].isFinished) {
                        createTimeLine(id, true);
                    } else {
                        createTimeLine(id, false);
                    }
                }
            }

        }, true);

    }

    function createTimeLine(id = null, isFinish) {

        AjaxCallAction("POST", "/api/admin/RequestForRating/Get_RequestReferencessService", JSON.stringify({ RequestId: id }), true, function (res) {

            if (res != null) {
                var strTimeLine = '';
                var left = false;

                for (var i = 0; i < res.data.length; i++) {

                    var bgColor = i == 0 ? "bg-secondary" : "bg-info";

                    strTimeLine += "<article class='timeline-entry " + (left ? " left-aligned" : "") + "'>";
                    strTimeLine += "<div class='timeline-entry-inner'>";

                    strTimeLine += "<time class='timeline-time' datetime=''>";
                    strTimeLine += "</time>";

                    strTimeLine += "<div class='timeline-icon " + bgColor + "'>";
                    strTimeLine += "<i class='entypo-feather'></i>";
                    strTimeLine += "</div>";

                    strTimeLine += "<div class='timeline-label'>";
                    if (i == 0) {
                        var titleText = res.data[i].companyName == null ? res.data[i].agentName : res.data[i].companyName;

                        $("#CutomerName").html("<h3 style='font-size:20px;font-weight:bold;text-align:right;margin:10px'>گردش کار - " + titleText + "</h3>");

                        strTimeLine += "<span class='smallFontSize' style='font-weight:bold'>";
                        strTimeLine += (res.data[i].agentName == null ? res.data[i].companyName : res.data[i].agentName) + " : [" + res.data[i].kindOfRequestName + "]</span >";

                        strTimeLine += "<div style='float:left;color:green'><span class='date smallFontSize'>( تاریخ: " + res.data[i].sendTimeStr + "</span>";
                        strTimeLine += "<span class='LTRDirection smallFontSize'> ساعت: " + res.data[i].sendTimeTimeStr + " )</span></div>";

                    }
                    else {
                        if (res.data[i].sendUser == null) {
                            strTimeLine += "<span class='boxCounter'>" + i.toString() + "</span>"
                                + (res.data[i].levelStepSettingIndexID == "29" ? " &#x274C; " : "")
                                + (res.data[i].levelStepSettingIndexID == "7" ? " &#x2705; " : "")
                                + (res.data[i].levelStepSettingIndexID == "13" ? " &#x2705; " : "")
                                + "<span class='sender'>ارجاع از: مشتری" + (res.data[i].agentName == null ? " (" + res.data[i].companyName + ")" : " (" + res.data[i].agentName + ")") + "</span><br/>";
                            strTimeLine += " <span>به: " + res.data[i].roleDesReciver + " </span>";
                            strTimeLine += "<span class='sender'>" + " (" + res.data[i].reciverName + ")" + "</span>";
                        }
                        else if (res.data[i].reciverName == null) {
                            strTimeLine += "<span class='boxCounter'>" + i.toString() + "</span>"
                                + (res.data[i].levelStepSettingIndexID == "4" ? " &#x1F4DD;" : "")
                                + (res.data[i].levelStepSettingIndexID == "11" ? " &#x1F4C2;" : "")
                                + (res.data[i].levelStepSettingIndexID == "26" ? " &#x2705;" : "")
                                + " <span> ارجاع از:  " + res.data[i].userRoleDes + " (" + res.data[i].realName + ")"
                                + " </span><br/>";
                            strTimeLine += " <span>به: " + "مشتری" + " </span>";
                            strTimeLine += "<span class='sender'>" + (res.data[i].agentName == null ? " (" + res.data[i].companyName + ")" : " (" + res.data[i].agentName + ")") + "</span>";
                        }
                        else {
                            strTimeLine += "<span class='boxCounter'>" + i.toString() + "</span>"
                                + (res.data[i].levelStepSettingIndexID == "17" ? " &#x2705;" : "")
                                + (res.data[i].levelStepSettingIndexID == "28" ? " &#x274C;" : "")
                                + (res.data[i].levelStepSettingIndexID == "23" ? " &#x1F4CA;" : "")
                                + (res.data[i].roleDesReciver != null ? " <span> ارجاع از:  " + res.data[i].userRoleDes + " (" + res.data[i].realName + ")"
                                    + " </span><br/>" : "");
                            strTimeLine += " <span>به: " + res.data[i].roleDesReciver + " </span>";
                            strTimeLine += "<span class='sender'>" + " (" + res.data[i].reciverName + ")" + "</span>";
                        }
                        strTimeLine += "<br/>";
                        strTimeLine += "<span class='smallFontSize'>";
                        strTimeLine += "[" + res.data[i].destLevelStepIndexButton + "]";
                        // strTimeLine += "[ جهت اقدام] <span >[ خوانده شده ] </span><span class='greenColor'>1401/09/21  3:34</span>";
                        strTimeLine += "</span>";
                        strTimeLine += "<div style='float:left;color:green'><span class='date smallFontSize'>( تاریخ: " + res.data[i].sendTimeStr + "</span>";
                        strTimeLine += "<span class='LTRDirection smallFontSize'> ساعت: " + res.data[i].sendTimeTimeStr + ")</span></div>";


                    }
                    strTimeLine += "<span></span>";
                    if (i != 0) {
                        if (res.data[i].comment != null && res.data[i].comment != "") {

                            strTimeLine += "<div class='customDiv'>توضیحات<hr/>";
                            strTimeLine += "<span class='Transcript'>" + res.data[i].comment + "</span>";
                            strTimeLine += "</div>";
                        }
                    }
                    strTimeLine += "</div>";
                    //res.data[i].kindOfRequestName

                    strTimeLine += "</div>";
                    strTimeLine += "</article>";
                    left = !left;


                }
                if (isFinish) {
                    strTimeLine += "<article class='timeline-entry " + (left ? " left-aligned" : "") + "'>";
                    strTimeLine += "<div class='timeline-entry-inner'>";
                    strTimeLine += "<time class='timeline-time' datetime='2014-01-10T03:45'></time>";
                    strTimeLine += "<div class='timeline-icon bg-secondary'><i class='entypo-feather'></i></div>";
                    strTimeLine += "<div class='timeline-label'>";
                    strTimeLine += "<h2>";
                    strTimeLine += "<span>تایید نهایی و اختتام درخواست </span>";
                    // strTimeLine += "<span class='sender''>مهندس</span>";
                    strTimeLine += "</h2>";
                    strTimeLine += "</div>";
                    strTimeLine += "</article>";
                } else {
                    strTimeLine += "<article class='timeline-entry begin'>";
                    strTimeLine += "<div class='timeline-entry-inner'>";
                    strTimeLine += "<div class='timeline-icon' style='-webkit-transform: rotate(-90deg); -moz-transform: rotate(-90deg);'>";
                    strTimeLine += "<i class='entypo-flight'></i>";
                    strTimeLine += "</div>";
                    strTimeLine += "</div>";
                    strTimeLine += "</article>";
                }

                $("#TimeLine").html(strTimeLine);

            }

        }, true);

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

                if (res.lastInsuranceList != null && res.lastInsuranceList != "") {
                    $("#divDownload").html("<a class='btn btn-success' href='" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                }
                if (res.auditedFinancialStatements != null && res.auditedFinancialStatements != "") {
                    $("#divDownload_AuditedFinancialStatements").html("<a class='btn btn-success' href='" + res.auditedFinancialStatementsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                }
                $("input[name='ScanCustomerNationalCard']").val(res.scanCustomerNationalCard);
                $("input[name='ScanManagerNationalCard']").val(res.scanManagerNationalCard);
                if (res.scanCustomerNationalCard != null && res.scanCustomerNationalCard != "") {
                    $("#divDownload_ScanCustomerNationalCard").html("<a class='btn btn-success' href='" + res.scanCustomerNationalCardFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                }
                if (res.scanManagerNationalCard != null && res.scanManagerNationalCard != "") {
                    $("#divDownload_ScanManagerNationalCard").html("<a class='btn btn-success' href='" + res.scanManagerNationalCardFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                }
                systemSeting_Combo(res);
            }

        }, true);

    }

    function initCustomerRequestInformation(id) {
        let selecet_item = makeComboForQuestionLevel();
        $("#QuestionLevel").html(selecet_item);

        AjaxCallAction("POST", "/api/admin/RequestForRating/Get_CustomerRequestInformations", JSON.stringify(
            { RequestId: id }
        ), true, function (res) {
            if (!isEmpty(res)) {
                $("#CustomerId").val(res.CustomerId);
                $("#CountOfPersonel").val(res.countOfPersonel);
                $("#AmountOfLastSale").val(moneyCommaSepWithReturn(!isEmpty(res.amountOfLastSale) ? res.amountOfLastSale.toString() : ''));
                if (res.lastInsuranceListFull != "/FileUpload/Customers/no-photo.png") {
                    $("#divDownload").html("<a class='btn btn-success' href='" + res.lastInsuranceListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                }
                if (res.lastAuditingTaxListFull != "/FileUpload/Customers/no-photo.png") {
                    $("#divDownload_LastAuditingTaxList").html("<a class='btn btn-success' href='" + res.lastAuditingTaxListFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                }
                if (res.questionLevelId != 0 && !isEmpty(res.questionLevelI))
                    $("#QuestionLevel" + " option[value='" + res.questionLevelId + "']").prop("selected", true);
            }

        }, true);
        $('#TypeServiceRequestedId').on('select2:select', function (e) {
            let _select_id = e.params.data.id;
            if (_select_id == 254) {
                $("#CountOfPersonel").val(0);
                $("#AmountOfLastSale").val(0);
                $("#hide_information").hide();
                $("#TypeOfRequestLevel").show();
            } else {
                $("#hide_information").show();
                $("#CountOfPersonel").val("");
                $("#AmountOfLastSale").val("");
                $("#TypeOfRequestLevel").hide();
            }
        });
    }

    function editCustomerRequestInformation(e) {
        $(e).attr("disabled", "");
        //let req_id = $("#RequestId").val();
        AjaxCallActionPostSaveFormWithUploadFile("/api/admin/RequestForRating/Save_SaveCustomerRequestInformation", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), false, function (res) {
            alertB("اطلاعات به روز شد.");
        }, false);
        $(e).removeAttr("disabled");
        //goToUrl("/admin/RequestForRating/Index/" + req_id);
    }

    function makeComboForQuestionLevel() {
        let strM = "";
        AjaxCallAction("POST", "/api/Admin/Corporate/Get_QuestionLevels", JSON.stringify({
            PageIndex: 0,
            PageSize: 0,
            IsActive: 15,
        }), false, function (res) {
            if (res.isSuccess) {
                strM = '<option value="0">انتخاب کنید</option>';
                for (var i = 0; i < res.data.length; i++) {
                    strM += "<option value='" + res.data[i].questionLevelId + "'>" + res.data[i].levelTitle + "</option>";
                }
            }
        }, false);
        return strM;
    }

    web.RequestForRating = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FillComboLevelStepSettingList: fillComboLevelStepSettingList,
        FilterGrid: filterGrid,
        CancelRequest: cancelRequest,
        InitRequestReferences: initRequestReferences,
        CreateTimeLine: createTimeLine,
        OnchangeKindOfRequest: onchangeKindOfRequest,
        InitCustomerRequestInformation: initCustomerRequestInformation,
        EditCustomerRequestInformation: editCustomerRequestInformation,
        ClickSortingRequestForRatingGrid: clickSortingRequestForRatingGrid,
        TextSearchOnKeyDown: textSearchOnKeyDown
    };

})(Web, jQuery);
