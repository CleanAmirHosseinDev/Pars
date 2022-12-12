






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/Activity/Get_Activitys", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].activityTitleNavigation.label + "</td><td><a title='ویرایش' href='/Admin/Activity/EditActivity?id=" + res.data[i].activityId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveActivity(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/Activity/Save_Activity", JSON.stringify({
            ActivityTitle: $("#ActivityTitle").val(),
            ActivityId: $("#ActivityId").val(),
            ActivityComment: $("#ActivityComment").val()//,
            //Picture1: $("#Picture1").val(),
            //Picture2: $("#Picture2").val()
        }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/Activity/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initActivity(id = null) {

        ComboBoxWithSearch('.select2', 'dir');

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/Activity/Get_Activity/" + id, null, true, function (res) {

                if (res != null) {
                    
                    $("#ActivityId").val(res.activityId);
                    $("#ActivityComment").val(res.activityComment);
                    systemSeting_Combo(res);
                }

            }, true);

        } else {
            systemSeting_Combo(null);
        }

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "150", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strActivityTitle = '<option value="">انتخاب کنید</option>';
               
                for (var i = 0; i < res.data.length; i++) {
                   
                    strActivityTitle += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    
                }
             
                $("#ActivityTitle").html(strActivityTitle);
                if (resSingle!=null) {
                    $("#ActivityTitle").val(resSingle.activityTitle);

                }
             


            }
        }, true);
    }

    web.Activity = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveActivity: saveActivity,
        InitActivity: initActivity,
        SystemSeting_Combo: systemSeting_Combo
    };

})(Web, jQuery);