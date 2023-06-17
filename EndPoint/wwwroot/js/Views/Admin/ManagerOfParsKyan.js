
(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/ManagerOfParsKyan/Get_ManagerOfParsKyans", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].nameOfManager + "</td><td>" + res.data[i].title.label + "</td><td>" + res.data[i].position.label + "</td><td><a title='ویرایش' href='/Admin/ManagerOfParsKyan/EditManagerOfParsKyan?id=" + res.data[i].managersId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' class='btn btn-danger fontForAllPage' onclick='Web.ManagerOfParsKyan.Delete_ManagerOfParsKyan(" + res.data[i].managersId + ");'><i class='fa fa-remove'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveManagerOfParsKyan(e) {

        $(e).attr("disabled", "");

        AjaxCallActionPostSaveFormWithUploadFile("/api/admin/ManagerOfParsKyan/Save_ManagerOfParsKyan", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFormMain"), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/ManagerOfParsKyan/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initManagerOfParsKyan(id = null, dir = 'rtl') {

        ComboBoxWithSearch('.select2', dir);

        AjaxCallAction("GET", "/api/admin/ManagerOfParsKyan/Get_ManagerOfParsKyan/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

            if (res != null) {

                $("#ManagersId").val(res.managersId);
                $("#NameOfManager").val(res.nameOfManager);
                $("#TwitterId").val(res.twitterId);
                $("#ResumeSummary").val(res.resumeSummary);
                $("#imgUpload_Picture").attr("src", res.pictureFull);
                $("#EmailAddress").val(res.emailAddress);
                
                $("#divDownload").html("<a href='/File/Download?path=" + res.resumeFileFull +"' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                systemSeting_Combo(!isEmpty(id) && id != 0 ? res : null);
            }

        }, true);



    }    

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "1,142", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strPositionID = '<option value="">انتخاب کنید</option>';
                var strTitleID = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].parentCode == 1) {
                        strPositionID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 142) {
                        strTitleID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                }

                $("#PositionID").html(strPositionID);
                $("#TitleID").html(strTitleID);
                if (resSingle != null) {
                    $("#PositionID").val(resSingle.positionId);
                    $("#TitleID").val(resSingle.titleId);
                }
            }
        }, true);
    }

    function delete_ManagerOfParsKyan(id) {

        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/admin/ManagerOfParsKyan/Delete_ManagerOfParsKyan/" + (isEmpty(id) ? '0' : id), null, true, function (result) {

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

    web.ManagerOfParsKyan = {        
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveManagerOfParsKyan: saveManagerOfParsKyan,
        InitManagerOfParsKyan: initManagerOfParsKyan,
        Delete_ManagerOfParsKyan: delete_ManagerOfParsKyan
    };

})(Web, jQuery);