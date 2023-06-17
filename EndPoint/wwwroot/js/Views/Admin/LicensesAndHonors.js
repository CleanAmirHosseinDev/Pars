






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/LicensesAndHonors/Get_LicensesAndHonorss", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].title + "</td><td><img src=" + res.data[i].pictureFull + " style='max-width:50px'/></td><td><a title='ویرایش' href='/Admin/LicensesAndHonors/EditLicensesAndHonors?id=" + res.data[i].licensesAndHonorsId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' class='btn btn-danger fontForAllPage' onclick='Web.LicensesAndHonors.Delete_LicensesAndHonors(" + res.data[i].licensesAndHonorsId + ");'><i class='fa fa-remove'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveLicensesAndHonors(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/LicensesAndHonors/Save_LicensesAndHonors", JSON.stringify({
            Title: $("#Title").val(),
            LicensesAndHonorsId: $("#LicensesAndHonorsId").val(),
            Result_Final_Picture: $("#hid_Picture").val(),
           
        }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/LicensesAndHonors/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initLicensesAndHonors(id = null) {
            AjaxCallAction("GET", "/api/admin/LicensesAndHonors/Get_LicensesAndHonors/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res != null) {
                    
                    $("#LicensesAndHonorsId").val(res.licensesAndHonorsId);
                    $("#Title").val(res.title);
                    $("#imgUpload_Picture").attr("src", res.pictureFull);
                }

            }, true);
    }

    function delete_LicensesAndHonors(id) {

        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/admin/LicensesAndHonors/Delete_LicensesAndHonors/" + (isEmpty(id) ? '0' : id), null, true, function (result) {

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
  
    web.LicensesAndHonors = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveLicensesAndHonors: saveLicensesAndHonors,
        InitLicensesAndHonors: initLicensesAndHonors,
        Delete_LicensesAndHonors: delete_LicensesAndHonors
    };

})(Web, jQuery);