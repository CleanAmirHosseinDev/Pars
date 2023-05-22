
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
                    
                    //strM += "<a style='margin-right:5px; color:black' href='/superVisor/RequestForRating/RequestReferences?id=" + res.data[i].requestId + "'" + " class='btn btn-info fontForAllPage'> <img src='/css/GlobalAreas/dist/img/timeline-icon.png' style='width:20px' title='مشاهده گردش کار'>  </a>"
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


    web.RequestForRating = {
       
        FilterGrid: filterGrid,
        CancelRequest: cancelRequest
       
    };

})(Web, jQuery);