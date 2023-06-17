






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/Activity/Get_Activitys", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + (res.data[i].activityTitleNavigation != null ? res.data[i].activityTitleNavigation.label : '') + "</td><td><a title='ویرایش' href='/Admin/Activity/EditActivity?id=" + res.data[i].activityId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' class='btn btn-danger fontForAllPage' onclick='Web.Activity.Delete_Activity(" + res.data[i].activityId + ");'><i class='fa fa-remove'></i></a></td></tr>";

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
            ActivityComment: getDataCkeditor("ActivityComment"),
            Result_Final_Picture1: $("#hid_Picture1").val(),
            Result_Final_Picture2: $("#hid_Picture2").val(),
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

        AjaxCallAction("GET", "/api/admin/Activity/Get_Activity/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

            if (res != null) {

                $("#ActivityId").val(res.activityId);               
                $("#ActivityComment").val(res.activityComment);
                Ckeditor("ActivityComment");
                $("#imgUpload_Picture1").attr("src", res.picture1Full);
                $("#imgUpload_Picture2").attr("src", res.picture2Full);
                systemSeting_Combo(!isEmpty(id) && id != 0 ? res : null);
            }

        }, true);

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "150", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strActivityTitle = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {

                    strActivityTitle += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";

                }

                $("#ActivityTitle").html(strActivityTitle);
                if (resSingle != null) {
                    $("#ActivityTitle").val(resSingle.activityTitle);

                }



            }
        }, true);
    }

    function delete_Activity(id) {

        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/admin/Activity/Delete_Activity/" + (isEmpty(id) ? '0' : id), null, true, function (result) {

                    debuggerWeb();

                    if (result.isSuccess) {

                        filterGrid();

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

    web.Activity = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveActivity: saveActivity,
        InitActivity: initActivity,
        SystemSeting_Combo: systemSeting_Combo,
        Delete_Activity: delete_Activity
    };

})(Web, jQuery);