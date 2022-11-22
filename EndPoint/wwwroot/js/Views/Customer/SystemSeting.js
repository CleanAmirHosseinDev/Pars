






(function (web, $) {


    function systemSeting_Combo(parentCodeArr = null, labelCode = null, ComboName = "") {

        AjaxCallAction("POST", "/api/customer/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: parentCodeArr, LabeCode: labelCode , PageIndex: 0, PageSize:0 }), true, function (res) {

            if (res.isSuccess) {
                var strM = '<option value="">انتخاب کنید</option>';
                for (var i = 0; i < res.data.length; i++) {

                    strM += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].value + "</option>";
                }
                $("#" + ComboName).html(strM);
        }
        }, true);
}


   
    web.SystemSeting = {       
        SystemSeting_Combo: systemSeting_Combo
    };

})(Web, jQuery);