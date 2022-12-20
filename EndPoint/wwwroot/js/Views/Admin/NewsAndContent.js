






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/NewsAndContent/Get_NewsAndContents", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].title + "</td><td>" + res.data[i].kindOfContent.label + res.data[i].kindOfContent.label+ "</td><td>" + res.data[i].title +  "</td><td><a title='ویرایش' href='/Admin/NewsAndContent/EditNewsAndContent?id=" + res.data[i].newsId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveNewsAndContent(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/NewsAndContent/Save_NewsAndContent", JSON.stringify({
            NewsID: $("#NewsID").val(),
            Title: $("#Title").val(),
            Body: getDataCkeditor("Body") ,//$("#Body").val(),
            KindOfContent: $("#KindOfContent").val(),
            Keywords: $("#Keywords").val(),
            IsConfirmByAdmin: $("#IsConfirmByAdmin").val()
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

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/NewsAndContent/Get_NewsAndContent/" + id, null, true, function (res) {                

                if (res != null) {

                    $("#NewsID").val(res.NewsID);
                    $("#Title").val(res.title);
                    $("#Body").val(res.body);
                    Ckeditor("Body");
                    $("#KindOfContent").val(res.kindOfContent);
                    $("#ContentPic").val(res.contentPic);
                    $("#MetaKeywords").val(res.metaKeywords);
                    $("#MeteDesc").val(res.meteDesc);
                    $("#Keywords").val(res.keywords);
                    $("#IsConfirmByAdmin").val(res.isConfirmByAdmin);
                    systemSeting_Combo(res);
                }

            }, true);

        }
        else {
            Ckeditor("Body");
            systemSeting_Combo(null);
        }

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "60", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKindOfContent= '<option value="">انتخاب کنید</option>';
               
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
        InitNewsAndContent: initNewsAndContent
    };

})(Web, jQuery);