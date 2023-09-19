
(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function initLoginLog() {

        PersianDatePicker(".DatePicker");

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/LoginLog/Get_LoginLogs", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), FromDateStr: $("#FromDateStr").val(), ToDateStr: $("#ToDateStr").val() }), true, function (res) {

            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + res.data[i].fullName + "</td><td>" + res.data[i].loginDateStr + " - " + res.data[i].loginTimeStr + "</td><td>" + res.data[i].signOutDateStr + " - " + res.data[i].signOutTimeStr + "</td><td>" + res.data[i].ip + "</td><td>" + res.data[i].areaName+"</td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    web.LoginLog = {
        FilterGrid: filterGrid,
        InitLoginLog: initLoginLog,
        TextSearchOnKeyDown: textSearchOnKeyDown
    };

})(Web, jQuery);