






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        ComboBoxWithSearch('.select2', 'dir');

        AjaxCallAction("POST", "/api/admin/NewsAndContent/Get_NewsAndContents", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), KindOfContent: !isEmpty($("#KindOfContent").val()) ? $("#KindOfContent").val() : null }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    let rlink = "";
                    if (res.data[i].directLink != null && res.data[i].directLink.length>0) {
                        rlink = "/Page/" + res.data[i].directLink;
                    } else {
                        rlink = "/Page/" + res.data[i].contentId;
                    }
                    strM += `<tr><td>${(i + 1)}</td > <td>${res.data[i].contentId}</td > <td>${res.data[i].title}</td ><td>${(res.data[i].kindOfContentNavigation != null ? res.data[i].kindOfContentNavigation.label : '')}</td><td>${res.data[i].directLink == null ? "<span style='color: gray; '> - </span>" : res.data[i].directLink}</td ><td><a title='ویرایش' href='/Admin/NewsAndContent/EditNewsAndContent?id=${res.data[i].contentId}' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='مشاهده' href='javascript:void(0)' onclick="window.open('${rlink}', '_blank')" class='btn btn-primary fontForAllPage'><i class='fa fa-eye'></i></a><a title='حذف' class='btn btn-danger fontForAllPage' onclick='Web.NewsAndContent.Delete_NewsAndContent(${res.data[i].contentId});'><i class='fa fa-remove'></i></a></td></tr> `;

                }
                $("#tBodyList").html(strM);                

            }

        }, true);

    }

    function delete_NewsAndContent(id) {

        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/admin/NewsAndContent/Delete_NewsAndContent/" + (isEmpty(id) ? '0' : id), null, true, function (result) {

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

    function saveNewsAndContent(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/NewsAndContent/Save_NewsAndContent", JSON.stringify({
            ContentId: $("#ContentId").val(),
            Title: $("#Title").val(),
            Body: getDataCkeditor("Body"),
            KindOfContent: $("#KindOfContent").val(),
            Keywords: $("#Keywords").val(),
            Summary: $("#Summary").val(),
            MeteDesc: $("#MeteDesc").val(),
            DirectLink: $("#DirectLink").val(),
            Result_Final_ContentPic: $("#hid_ContentPic").val()
        }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/NewsAndContent/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initNewsAndContent(id = null) {

        ComboBoxWithSearch('.select2', 'dir');        

        if (isEmpty(id) || id == 0) {

            Ckeditor("Body");

            Tagsinput("Keywords");

        }

        AjaxCallAction("GET", "/api/admin/NewsAndContent/Get_NewsAndContent/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

            if (res != null) {

                $("#ContentId").val(res.contentId);
                $("#Title").val(res.title);
                $("#Body").val(res.body);
                Ckeditor("Body");
                $("#KindOfContent").val(res.kindOfContent);
                $("#imgUpload_ContentPic").attr("src", res.contentPicFull);
                $("#Summary").val(res.summary);
                $("#MeteDesc").val(res.meteDesc);
                $("#DirectLink").val(res.directLink);

                $("#Keywords").val(res.keywords);
                Tagsinput("Keywords");

                systemSeting_Combo(!isEmpty(id) && id != 0 ? res : null);
            }

        }, true);

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "60", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfContent = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {
                    strKindOfContent += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";

                }
                $("#KindOfContent").html(strKindOfContent);
                if (resSingle != null) {

                    $("#KindOfContent").val(resSingle.kindOfContent);
                }
            }
        }, true);
    }

    web.NewsAndContent = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveNewsAndContent: saveNewsAndContent,
        InitNewsAndContent: initNewsAndContent,
        Delete_NewsAndContent: delete_NewsAndContent,
        SystemSeting_Combo: systemSeting_Combo
    };

})(Web, jQuery);