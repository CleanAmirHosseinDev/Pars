
(function (web, $) {


    function filterGrid(id = null) {

        
        AjaxCallAction("POST", "/api/admin/RequestForRating/Get_RequestForRatings", JSON.stringify({ CustomerId: id, PageIndex: 1, PageSize: $("#cboSelectCount").val(), DestLevelStepIndex: isEmpty($("#cboSelectLS").val()) ? null : $("#cboSelectLS").val(), IsMyRequests: $('#IsMyRequests').is(":checked") }), true, function (res) {

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

                    if (res.data[i].comment.trim() == "عدم تایید قرارداد توسط مشتری") {
                        strM += "<td>" + "<span style='color:red'>&#10060;" + res.data[i].comment + "</span>" + "</td><td>";
                    } else if (res.data[i].destLevelStepIndexButton == "ارجاع به مشتری جهت اصلاح مشخصات اولیه توسط مشتری") {
                        strM += "<td>" + "<span style='color:red'> &#10060; " + res.data[i].destLevelStepIndexButton + "</span>" + "</td><td>";
                    }
                    else {
                        strM += "<td>" + st2 + res.data[i].levelStepStatus + "</td><td>";
                    }
                    if (res.data[i].destLevelStepIndex =="2") {
                        strM +="<a title='حذف درخواست' class='btn btn-danger style='margin-left:5px' fontForAllPage' onclick='Web.RequestForRating.CancelRequest(" + res.data[i].requestId  + ","+id+");'><i class='fa fa-remove'></i></a>";
                    }
                    
                    strM += "<a style='margin-right:5px; color:black' href='/admin/RequestForRating/RequestReferences?id=" + res.data[i].requestId + "'" + " class='btn btn-info fontForAllPage'> <img src='/css/GlobalAreas/dist/img/timeline-icon.png' style='width:20px' title='مشاهده گردش کار'>  </a>"
                    //    + (getlstor("loginName") === res.data[i].destLevelStepAccessRole ? "<a style='margin-right:5px;color:black' title='مشاهده و اقدام' class='btn btn-edit fontForAllPage' href='/SuperVisor/RequestForRating/Referral/" + res.data[i].requestId + "'> <i class='fa fa-mail-forward' style='color:black'></i>  </a>" : "<a style='color:black;margin-right:5px;' title='نمایش پروفایل' href='/SuperVisor/Customers/ShowCustomers?id=" + res.data[i].customerId + "' class='btn btn-default fontForAllPage'><i class='fa fa-eye'></i> </a>");

                    //if ((n == res.data[i].destLevelStepAccessRole && res.data[i].destLevelStepAccessRole == "5") || (n == "5" && res.data[i].destLevelStepAccessRole == "10" && res.data[i].destLevelStepIndex == "7")) {
                    //    strM += "<a style='margin-right:5px;color:black' title='مشاهده اطلاعات تکمیلی' class='btn btn-default fontForAllPage' href='/SuperVisor/FutherInfo/Index/" + res.data[i].requestId + "'><i class='fa fa-file'></i> </a>";
                    //}
                    //if ((n == 8 || n == 1) && res.data[i].destLevelStepIndex > "7") {
                    //    strM += "<a style='margin-right:5px;color:black' title='مشاهده اطلاعات تکمیلی' class='btn btn-default fontForAllPage' href='/SuperVisor/FutherInfo/Index/" + res.data[i].requestId + "'><i class='fa fa-file'></i> </a>";
                    //}
                    //if ((n == 8 || n == 1 || n == 4 || n == 6 || n == 9) && res.data[i].destLevelStepIndex >= "4" && getlstor("loginName") != res.data[i].destLevelStepAccessRole) {
                    //    strM += "<a style='margin-right:5px;color:black' title='اسناد مشتری' class='btn btn-success fontForAllPage' href='/SuperVisor/RequestForRating/RequestDocument/" + res.data[i].requestId + "'><i class='fa fa-file-pdf-o'></i> </a>";
                    //}
                    strM += "</td></tr>";
                    //if (res.data[i].levelStepIndex >= 7) {


                    // }
                }

                $("#tBodyList").html(strM);



            }

        }, true);

    }
    
    function cancelRequest(id,customerId=null) {

        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/admin/RequestForRating/Delete_RequestForRating/" + (isEmpty(id) ? '0' : id), null, true, function (result) {

                    debuggerWeb();

                    if (result.isSuccess) {

                        filterGrid(customerId);

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
                    strTimeLine += "<span class='date'>" + res.data[i].sendTimeStr + "</span>";
                    strTimeLine += "<span class='LTRDirection'>" + res.data[i].sendTimeTimeStr + "</span>";
                    strTimeLine += "</time>";

                    strTimeLine += "<div class='timeline-icon " + bgColor + "'>";
                    strTimeLine += "<i class='entypo-feather'></i>";
                    strTimeLine += "</div>";

                    strTimeLine += "<div class='timeline-label'>";
                    if (i == 0) {
                        strTimeLine += "<h2>";
                        strTimeLine += "<span>ایجاد درخواست توسط : </span>";
                        strTimeLine += "<span class='sender'>" + (res.data[i].agentName == null ? res.data[i].companyName : res.data[i].agentName) + " (" + res.data[i].roleDesc + ") " + "</span >";
                        strTimeLine += "</h2>";

                        //  strTimeLine += "<p><strong>گیرنده ها :</strong></p>";
                        strTimeLine += "<ul>";
                        strTimeLine += "<li>";
                        // strTimeLine += "<span class='reciver' > علی احمدی </span >";
                        // strTimeLine += "<span class='PostText'>";                        
                        // strTimeLine += "<text > [رئیس شورا] </text>";                           
                        // strTimeLine += "</span>";
                        strTimeLine += "<span class='smallFontSize'> [" + res.data[i].levelStepStatus + "]</span>";
                        // strTimeLine += "<span class='redColor'>[ خوانده نشده ]</span>";
                        strTimeLine += "</li>";
                        strTimeLine += "</ul>";
                    }
                    else {
                        if (res.data[i].sendUser == null) {
                            // strTimeLine += "<span>" + res.data[i].roleDesc + ": </span>";
                            // strTimeLine += "<span class='sender'>" + res.data[i].agentName + "</span>";
                        } else {
                            strTimeLine += "<span>" + res.data[i].userRoleDes + ": </span>";
                            strTimeLine += "<span class='sender'>" + res.data[i].realName + "</span>";
                        }
                        strTimeLine += "<br/>";
                        //  strTimeLine += "<span> گیرنده ها : </span>";
                        strTimeLine += "<ul>";
                        strTimeLine += "<li>";
                        // strTimeLine += "<span class='reciver'>مینا</span>";
                        // strTimeLine += "<span class='PostText'>";
                        // strTimeLine += "<text> [رئیس شورا] </text>";
                        // strTimeLine += " </span>";
                        strTimeLine += "<span class='smallFontSize'>";
                        if (i > 1) {
                            strTimeLine += "[" + res.data[i].destLevelStepIndexButton + "]";
                            // strTimeLine += "[ جهت اقدام] <span >[ خوانده شده ] </span><span class='greenColor'>1401/09/21  3:34</span>";
                        } else {
                            strTimeLine += "[" + res.data[i].levelStepStatus + "]";
                            // strTimeLine += "[ جهت اقدام] <span >[ خوانده شده ] </span><span class='greenColor'>1401/09/21  3:34</span>";
                        }

                        strTimeLine += "</span>";
                        strTimeLine += "</li>";
                        strTimeLine += "</ul>";

                    }
                    strTimeLine += "<span></span>";
                    if (i == 0) {
                        strTimeLine += "<div class='customDiv'>";
                        strTimeLine += "<span class='Transcript'>" + res.data[i].kindOfRequestName + "</span>";
                        strTimeLine += "</div>";
                    } else {
                        if (res.data[i].comment != null) {

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
                    strTimeLine += "<time class='timeline-time' datetime='2014-01-10T03:45'><span>اختتام درخواست</span></time>";
                    strTimeLine += "<div class='timeline-icon bg-secondary'><i class='entypo-feather'></i></div>";
                    strTimeLine += "<div class='timeline-label'>";
                    strTimeLine += "<h2>";
                    strTimeLine += "<span>اختتام درخواست </span>";
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

    function initDashboard() {

        AjaxCallAction("GET", "/api/admin/Customers/GetDashboard_Info/", null, true, function (res) {

            if (res != null) {

                $("#CustomerCount").val("" + " نفر");
                $("#CustomerCountWidthRequest").val("" + " نفر");
                $("#CustomerCountWidthoutRequest").val("" + " نفر");
                $("#RequestSamtCount").val("" + " تا");
                $("#ContractCancled").val("" + " تا");
                $("#RequestIsFinished").val("" + " تا");
                $("#WaitForComfiramCommite").val("" + " تا");                
                
            }


        }, true);
    }

    web.RequestForRating = {
       
        FilterGrid: filterGrid,
        CancelRequest: cancelRequest,
        InitRequestReferences: initRequestReferences,
        CreateTimeLine: createTimeLine,
        InitDashboard: initDashboard
       
    };

})(Web, jQuery);