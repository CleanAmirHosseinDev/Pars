
var divPageingList_RequestForRatingsAdmin_pageG = 1;
function successCallBack_divPageingList_RequestForRatingsAdmin(res) {

    
    if (res.isSuccess) {
        var n = getlstor("loginName");
        var strM = '';
        for (var i = 0; i < res.data.length; i++) {

            var st = "", st2 = "";
            if (res.data[i].destLevelStepAccessRole == "10" && res.data[i].destLevelStepIndex == "7") {

                st2 = "<span style='font-size:1.5em'> &#128194;</span> ";
            }

            strM += "<tr><td>" + (i + 1) + "</td><td>"
                + res.data[i].requestNo + "</td><td>"
                + (!isEmpty(res.data[i].companyName) ? res.data[i].companyName : '') + "</td><td>"
                + res.data[i].agentMobile + "</td><td>"
                + res.data[i].dateOfRequestStr + "</td>";

            strM += "<td>" + res.data[i].reciveUserName + "</td>"

            strM += "<td>";

            if (!isEmpty(res.data[i].assessment)) {

                var qe = res.data[i].assessment.split(",");

                if (qe.length == 1) {

                    strM += generateAssessment(qe[0]);

                }
                else if (qe.length == 2) {

                    strM += generateAssessment(qe[0]);

                    strM += "\n" + "<hr/>";

                    switch (qe[1]) {

                        case "0":

                            strM += "<span>امتیاز</span>";

                            break;
                        case "1":

                            strM += "<span>مدت زمان فرایند</span>";

                            break;
                        case "2":

                            strM += "<span>کارشناس ارزیابی</span>";

                            break;
                        case "3":

                            strM += "<span>فرایند صدور قرارداد</span>";

                            break;
                        case "4":

                            strM += "<span>فرایند صدور فاکتور</span>";

                            break;

                    }

                }

            }

            strM += "</td>";

            strM += "<td>";

            if (!isEmpty(res.data[i].reasonAssessment1)) {

                strM += res.data[i].reasonAssessment1;

            }

            strM += "</td>"

            if (res.data[i].levelStepSettingIndexID == "29") {
                strM += "<td>" + "<span style='color:red'>&#10060;" + "عدم تایید قرارداد" + "</span>" + "</td><td>";
            } else if (res.data[i].destLevelStepIndexButton == "ارجاع به مشتری جهت اصلاح مشخصات اولیه توسط مشتری") {
                strM += "<td>" + "<span style='color:red'> &#10060; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
            }
            else {
                strM += "<td>" + st2
                    + (res.data[i].levelStepSettingIndexID == "13" ? " &#x2705; " : "")
                    + res.data[i].levelStepStatus + "</td><td>";
            }
          
                strM += "<a title='حذف درخواست' class='btn btn-danger style='margin-left:5px' fontForAllPage' onclick='Web.RequestForRating.CancelRequest(" + res.data[i].requestId + ");'><i class='fa fa-remove'></i></a>";
            

            strM += "<a style='margin-right:5px; color:black' href='/admin/RequestForRating/RequestReferences?id=" + res.data[i].requestId + "'" + " class='btn btn-info fontForAllPage'> <img src='/css/GlobalAreas/dist/img/timeline-icon.png' style='width:20px' title='مشاهده گردش کار'>  </a>"
            strM += "</td></tr>";

        }

        $("#tBodyList").html(strM);



    }
    

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
    

    function filterGrid(id=null) {

        ComboBoxWithSearch('.select2', 'rtl');

        pageingGrid("divPageingList_RequestForRatingsAdmin", "/api/admin/RequestForRating/Get_RequestForRatings", JSON.stringify({ CustomerId:id, PageIndex: 1, PageSize: $("#cboSelectCount").val(), Search: $("#txtSearch").val(), DestLevelStepIndex: isEmpty($("#cboSelectLS").val()) ? null : $("#cboSelectLS").val(), KindOfRequest: !isEmpty($("#cboKindOfRequest").val()) ? $("#cboKindOfRequest").val() : null }));

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

    web.RequestForRating = {
        FillComboLevelStepSettingList: fillComboLevelStepSettingList,
        FilterGrid: filterGrid,
        CancelRequest: cancelRequest,
        InitRequestReferences: initRequestReferences,
        CreateTimeLine: createTimeLine,
        OnchangeKindOfRequest: onchangeKindOfRequest

    };

})(Web, jQuery);
