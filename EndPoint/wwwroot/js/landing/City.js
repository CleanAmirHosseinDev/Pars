


(function (web, $) {
    
    function getCityList() {

        var stid = $("#StateID").val();
       
        ComboBoxWithSearch('.select2', 'dir');
        if (!isEmpty(stid) && stid != 0) {
                AjaxCallAction("POST", "/api/City/Get_Citys", JSON.stringify({ StateId: stid, Search: null, PageIndex: 0, PageSize: 0 }), true, function (res) {

                    if (res.isSuccess) {

                        var strM = '<option value="">شهر خود را انتخاب کنید</option>';

                        for (var i = 0; i < res.data.length; i++) {
                            strM += " <option value=" + res.data[i].cityId + ">" + res.data[i].cityName + "</option>";
                        }
                        $("#CityID").html(strM);

                    }

                }, true);
            }
         

    }

    function getStatesList() {
        ComboBoxWithSearch('.select2', 'dir');

        AjaxCallAction("POST", "/api/City/Get_States_Combo", JSON.stringify({ PageIndex: 0, PageSize:0}), true, function (res) {

            if (res.isSuccess) {

                var strM = '<option value="">استان خود را انتخاب کنید</option>';
                for (var i = 0; i < res.data.length; i++) {
                        strM += " <option value=" + res.data[i].stateId + ">" + res.data[i].stateName + "</option>"; 
                }
                $("#StateID").html(strM);
            }
        }, true);

    }

    web.City = {

        GetCityList: getCityList,        
        GetStatesList: getStatesList
    };

})(Web, jQuery);