






(function (web, $) {

  
    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/LicensesAndHonors/Get_LicensesAndHonorss", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].title + "</td><td><img src=" + res.data[i].pictureFull + " style='max-width:50px'/></td><td><a title='ویرایش' href='/Admin/LicensesAndHonors/EditLicensesAndHonors?id=" + res.data[i].licensesAndHonorsId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

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

  
    web.LicensesAndHonors = {
         FilterGrid: filterGrid,
         InitLicensesAndHonors: initLicensesAndHonors,      
    };

})(Web, jQuery);