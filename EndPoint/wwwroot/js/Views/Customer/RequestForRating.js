






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/customer/RequestForRating/Get_RequestForRatings", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

               

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    var detailbtn = "";
                    var contractbtn = "";
                    var documentbtn = "";
                    if (res.data[i].status == 0) {
                         detailbtn = "<a style='margin-right:5px' title='مشاهده جزییات' " + "onclick = 'Web.RequestForRating.Get_Detail("+res.data[i].requestId+");'"
                             +" class='btn btn-detail fontForAllPage'> <i class='fa fa-eye'></i></a>"
                    }
                    if (res.data[i].status==1) {
                        contractbtn = "<a style='margin-right:5px' title='مشاهده قرارداد' href='/Customer/RequestForRating/ContractPrinting?id=" +
                            res.data[i].requestId + "' class='btn btn-print fontForAllPage'> <i class='fa fa-print'></i></a>"
                    }
                    if (res.data[i].status == 1) {
                        documentbtn = "<a style='margin-right:5px' title='ثبت اطلاعات تکمیلی' href='/Customer/RequestForRating/EditRequestForRating?id=" +
                            res.data[i].requestId + "' class='btn btn-info fontForAllPage'> <i class='fa fa-file-text-o'></i></a>"
                    }

                    strM += "<tr><td>" + (i + 1) + "</td><td>"
                        + res.data[i].kindOfRequest + "</td><td>"
                        + (res.data[i].kindOfRequestNavigation != null ?res.data[i].kindOfRequestNavigation.label:'') + "</td><td>"
                        + res.data[i].statusStr + "</td><td>"
                        + detailbtn
                        + contractbtn
                        + documentbtn
                        + "</td></tr>";
                }

                $("#tBodyList").html(strM);
            }

        }, true);

    }


    function get_Detail(id = null) {
        try {

           // debuggerWeb();
            //InitModal('جزییات درخواست', "/api/customer/RequestForRating/Get_RequestForRatings/", { id: id }, "Web.RequestForRating.Get_Detail_Post(" + id + ");", false, true);
            InitModal_Withot_Par("Mobin ast", "<h1>Monire ast</h1>", "", true, "width:100%;", "Sabt");

        } catch (e) {

        }

       
    }
    function get_Detail_Post(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/customer/RequestForRating/Get_RequestForRatings/" + JSON.stringify({ RequestID: id, PageIndex: 0, PageSize: 0 }), true, function (res) {

                if (res != null) {
                    //  $("#CityId").val(res.cityId);
                    //  $("#CityName").val(res.cityName);                   
                }
            }, true);

        }
    }
    function saveRequestForRating(e) {

        $(e).attr("disabled", "");       

        AjaxCallAction("POST", "/api/customer/RequestForRating/Save_Request", JSON.stringify({ Request: { KindOfRequest: $("#TypeServiceRequestedId").val() } }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                alertB("ثبت", res.message, "success");

            }

        }, true);

    }

    function systemSeting_ComboRequestForRating() {

        AjaxCallAction("POST", "/api/customer/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "63", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strTypeServiceRequestedId = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {                   
                        strTypeServiceRequestedId += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";                    
                }
                $("#TypeServiceRequestedId").html(strTypeServiceRequestedId);
                // $("#TypeServiceRequestedId").val(resSingle.typeServiceRequestedId);
            }
        }, true);
    }

    function initRequestForRating() {

       
        ComboBoxWithSearch('.select2', 'dir');
        //AjaxCallAction("GET", "/api/customer/Customers/Get_Customers/", null, true, function (res) {

        //        if (res != null) {
                  
        //            //
        //            //
        //        }

        //    }, true);  
        systemSeting_ComboRequestForRating();

    }

    web.RequestForRating = {
        TextSearchOnKeyDown: textSearchOnKeyDown,       
        SaveRequestForRating: saveRequestForRating,
        InitRequestForRating: initRequestForRating,
        Get_Detail_Post: get_Detail_Post,
        FilterGrid: filterGrid,
        Get_Detail: get_Detail,
        SystemSeting_ComboRequestForRating: systemSeting_ComboRequestForRating
    };

})(Web, jQuery);