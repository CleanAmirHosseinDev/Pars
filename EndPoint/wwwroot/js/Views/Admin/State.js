






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/State/Get_States", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].stateName + "</td><td><a title='ویرایش' href='/Admin/State/EditState?id=" + res.data[i].stateId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveState(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/State/Save_State", JSON.stringify({ StateName: $("#StateName").val(), StateId: $("#StateId").val() }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/State/Index");

            }

        }, true);

    }

    function initState(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/State/Get_State/" + id, null, true, function (res) {                

                if (res != null) {

                    $("#StateId").val(res.stateId);
                    $("#StateName").val(res.stateName);

                }

            }, true);

        }

    }

    web.State = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveState: saveState,
        InitState: initState
    };

})(Web, jQuery);