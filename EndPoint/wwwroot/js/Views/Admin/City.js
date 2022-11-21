






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/City/Get_Citys", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].state.stateName+"</td><td>" + res.data[i].cityName + "</td><td><a title='ویرایش' href='/Admin/City/EditCity?id=" + res.data[i].cityId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function getStatesList() {

        AjaxCallAction("POST", "/api/admin/City/Get_States_Combo", JSON.stringify({ PageIndex: 0, PageSize:0}), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {
                        strM += " <option value=" + res.data[i].stateId + ">" + res.data[i].stateName + "</option>"; 
                }
                $("#StateId").html(strM);
            }
        }, true);

    }

    function saveCity(e) {

        $(e).attr("disabled", "");

        
        AjaxCallAction("POST", "/api/admin/City/Save_City", JSON.stringify({ CityName: $("#CityName").val(), CityId: $("#CityId").val(), StateId: $("#StateId").val() }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/City/Index");

            }
         else {

            alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initCity(id = null, dir = 'rtl') {

        ComboBoxWithSearch('.select2', dir);
        
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/City/Get_City/" + id, null, true, function (res) {                
               
                if (res != null) {
                    getStatesList();
                    $("#CityId").val(res.cityId);
                    $("#CityName").val(res.cityName);                   
                    $("#StateId").val(res.stateId);
                    comboBoxWithSearchUpdateText("StateId", res.state.stateName);

                }
            }, true);

        }

    }

    web.City = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveCity: saveCity,
        InitCity: initCity,
        GetStatesList: getStatesList
    };

})(Web, jQuery);