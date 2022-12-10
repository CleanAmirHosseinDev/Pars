






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/CareerOpportunities/Get_CareerOpportunitiess", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].name + " " + res.data[i].family + "</td><td>" + res.data[i].mobile + "</td><td>" + res.data[i].tel + "</td><td>" + res.data[i].education+ "</td><td><a title='ویرایش' href='/Admin/CareerOpportunities/EditCareerOpportunities?id=" + res.data[i].CareerOpportunitiesId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveCareerOpportunities(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/CareerOpportunities/Save_CareerOpportunities", JSON.stringify({
            Name: $("#Name").val(),
            CareerOpportunitiesId: $("#CareerOpportunitiesId").val(),
            Family: $("#Family").val(),
            CityID: $("#CityID").val(),
            Brithday: $("#Brithday").val(),
            Mobile: $("#Mobile").val(),
            Tel: $("#Tel").val(),
            Address: $("#Address").val(),
            NationalCode: $("#NationalCode").val(),
            Postion: $("#Postion").val(),
            Education: $("#Education").val(),
            EducationLevel: $("#EducationLevel").val(),
           
        }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/CareerOpportunities/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initCareerOpportunities(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/CareerOpportunities/Get_CareerOpportunities/" + id, null, true, function (res) {                

                if (res != null) {
                     $("#CareerOpportunitiesId").val(res.CareerOpportunitiesId);
                    $("#Name").val(res.name);
                    $("#Family").val(res.family);
                    $("#CityID").val(res.cityID);
                    $("#Brithday").val(res.brithday);
                    $("#Mobile").val(res.mobile);
                    $("#Tel").val(res.tel);
                    $("#Address").val(res.address);
                    $("#NationalCode").val(res.nationalCode);
                    $("#Postion").val(res.postion);
                    $("#Education").val(res.education);
                    $("#EducationLevel").val(res.educationLevel);
                }

            }, true);

        }

    }

    web.CareerOpportunities = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveCareerOpportunities: saveCareerOpportunities,
        InitCareerOpportunities: initCareerOpportunities
    };

})(Web, jQuery);