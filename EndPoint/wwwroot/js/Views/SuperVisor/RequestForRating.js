
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
                        + res.data[i].kindOfRequest + "</td><td>"
                        + (res.data[i].kindOfRequestNavigation != null ? res.data[i].kindOfRequestNavigation.label : '') + "</td><td>"
                        + res.data[i].statusStr + "</td><td>"
                        + "<a style='margin-right:5px' title='مشاهده گردش' class='btn btn-detail fontForAllPage'> <i class='fa fa-eye'></i></a>"
                        + "<a style='margin-right:5px' title='مشاهده و ارجاع' class='btn btn-detail fontForAllPage'> <i class='fa fa-eye'></i></a>"
                        + "</td></tr>";
                }

                $("#tBodyList").html(strM);
            }

        }, true);

    }

    web.RequestForRating = {
        FilterGrid: filterGrid,
        TextSearchOnKeyDown: textSearchOnKeyDown
    };

})(Web, jQuery);