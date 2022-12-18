
(function (web, $) {

    function initManagerOfParsKyans() {

        AjaxCallAction("POST", "/api/ManagerOfParsKyan/Get_ManagerOfParsKyans", JSON.stringify({ Search: null, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].nameOfManager + "</td><td>" + res.data[i].title.label + "</td><td>" + res.data[i].position.label + "</td><td><a title='ویرایش' href='/Admin/ManagerOfParsKyan/EditManagerOfParsKyan?id=" + res.data[i].managersId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);
            }

        }, true);

    }
    web.ManagerOfParsKyan = {
        InManagerOfParsKyans: initManagerOfParsKyans,
    };

})(Web, jQuery);