
(function (web, $) {

    //Document Ready  

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {



                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>"
                        + res.data[i].requestNo + "</td><td>"
                        + res.data[i].agentName + "</td><td>"
                        + res.data[i].agentMobile + "</td>" +
                        "<td>" + res.data[i].levelStepStatus + "</td><td>"
                        + "<a style='margin-right:5px' href='/superVisor/RequestForRating/RequestReferences?id=" + res.data[i].requestId + "'"  + " class='btn btn-info fontForAllPage'> <img src='/css/GlobalAreas/dist/img/timeline-icon.png' style='width:20px' title=' مشاهده گردش کار' /></a>"
                        + (getlstor("loginName") === res.data[i].destLevelStepAccessRole ? "<a style='margin-right:5px' title='ارجاع' class='btn btn-info fontForAllPage' href='/SuperVisor/RequestForRating/Referral/" + res.data[i].requestId + "'> <i class='fa fa-mail-forward' style='color:black'></i></a>" : "")
                        + "</td></tr>";
                }

                $("#tBodyList").html(strM);
            }

        }, true);

    }

    function initReferral(id = null) {

        AjaxCallAction("GET", "/api/superVisor/RequestForRating/InitReferral/" + id, null, true, function (res) {

            if (res.isSuccess) {

                getU("/css/GlobalAreas/Views/SuperVisor/RequestForRating/P_Referral.html", function (resG) {

                    $("#divMAS").html(resG);
                    $("#sdklsslks3498sjdkxhjsd_823sa").val(encrypt(id.toString(), keyMaker()));
                    $("#phch").show();

                    var htmlB = "";
                    for (var i = 0; i < res.data.length; i++) {

                        htmlB += "<button type='button' style='margin:5px' class='btn btn-info ButtonOpperationLSSlss' onclick='Web.RequestForRating.SaveRequestForRating(this);' data-DLSI='" + encrypt(res.data[i].destLevelStepIndex, keyMaker()) + "' data-LSAR='" + encrypt(res.data[i].levelStepAccessRole, keyMaker()) + "' data-LSS='" + encrypt(res.data[i].levelStepStatus, keyMaker()) + "'>" + res.data[i].destLevelStepIndexButton + "</button>";

                    }

                    $("#bLLSS").html(htmlB);

                });


            }
            else {

                $("#divMAS").html("<div class='alert alert-error text-center' style='font-size: 20px;'>شما اجازه انجام این عملیات را ندارید</div>");
                $("#phch").hide();
                $("#sdklsslks3498sjdkxhjsd_823sa").val(encrypt('0', keyMaker()));
                $("#bLLSS").html("");
            }

        }, true);

    }

    function saveRequestForRating(e) {

        $(".ButtonOpperationLSSlss").attr("disabled", "");

        var objJ = {};
        objJ.DestLevelStepIndex = decrypt($(e).attr("data-DLSI"), keyMaker());
        objJ.Comment = !isEmpty($("#ReferralExplanation").val()) ? $("#ReferralExplanation").val() : null;
        objJ.LevelStepAccessRole = decrypt($(e).attr("data-LSAR"), keyMaker());
        objJ.LevelStepStatus = decrypt($(e).attr("data-LSS"), keyMaker());
        objJ.Request = {};
        objJ.Request.Requestid = decrypt($("#sdklsslks3498sjdkxhjsd_823sa").val(), keyMaker());
        objJ.Request.KindOfRequest = 0;

        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Save_Request", JSON.stringify(objJ), true, function (res) {

            $(".ButtonOpperationLSSlss").removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/SuperVisor/RequestForRating/Index");

            }
            else {

                alertB("هشدار", res.message, "warning");

            }

        }, true);

    }

    function initRequestReferences(id = null) {
        AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestReferencessService", JSON.stringify({ RequestId: id }), true, function (res) {

            if (res != null) {

                var strTimeLine = '';
                var left = false;
                for (var i = 0; i < res.data.length; i++) {
                    var bgColor = i == 0 ? "bg-secondary" : "bg-info";
                    strTimeLine += "<article class='timeline-entry " + (left ? " left-aligned" : "") + "'>";
                    strTimeLine += "<div class='timeline-entry-inner'>";

                    strTimeLine += "<time class='timeline-time' datetime=''>";
                    strTimeLine += "<span class='date'>1401/09/26</span>";
                    strTimeLine += "<span class='LTRDirection'>14 :45</span>";
                    strTimeLine += "</time>";

                    strTimeLine += "<div class='timeline-icon " + bgColor + "'>";
                    strTimeLine += "<i class='entypo-feather'></i>";
                    strTimeLine += "</div>";

                    strTimeLine += "<div class='timeline-label'>";
                    if (i == 0) {
                        strTimeLine += "<h2>";
                        strTimeLine += "<span>ایجاد درخواست توسط : </span>";
                        strTimeLine += "<span class='sender'>" + res.data[i].agentName + " (" + res.data[i].roleDesc + ") " + "</span >";
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
                        if (res.data[i].userRoleDes==null) {
                            strTimeLine += "<span>" + res.data[i].roleDesc + ": </span>";
                            strTimeLine += "<span class='sender'>" + res.data[i].agentName + "</span>";
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
                        strTimeLine += "[" + res.data[i].levelStepStatus + "]";
                        // strTimeLine += "[ جهت اقدام] <span >[ خوانده شده ] </span><span class='greenColor'>1401/09/21  3:34</span>";
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

                if (false) {
                    strTimeLine += "<article class='timeline-entry " + (left ? " left-aligned" : "") + "'>";
                    strTimeLine += "<div class='timeline-entry-inner'>";
                    strTimeLine += "<time class='timeline-time' datetime='2014-01-10T03:45'><span>اختتام نامه</span></time>";
                    strTimeLine += "<div class='timeline-icon bg-secondary'><i class='entypo-feather'></i></div>";
                    strTimeLine += "<div class='timeline-label'>";
                    strTimeLine += "<h2>";
                    strTimeLine += "<span>اختتام نامه توسط : </span>";
                    strTimeLine += "<span class='sender''>مهندس</span>";
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


    web.RequestForRating = {
        FilterGrid: filterGrid,
        TextSearchOnKeyDown: textSearchOnKeyDown,
        InitReferral: initReferral,
        SaveRequestForRating: saveRequestForRating,
        InitRequestReferences: initRequestReferences
    };

})(Web, jQuery);