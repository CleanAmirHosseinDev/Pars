






(function (web, $) {

    //Document Ready              

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/CareerOpportunities/Get_CareerOpportunitiess", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].name + " " + res.data[i].family + "</td><td>" + res.data[i].mobile + "</td><td>" + res.data[i].tel + "</td><td>" + res.data[i].education+ "</td><td><a title='ویرایش' href='/Admin/CareerOpportunities/EditCareerOpportunities?id=" + res.data[i].CareerOpportunitiesId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    web.CareerOpportunities = {       
        FilterGrid: filterGrid,                
    };

})(Web, jQuery);