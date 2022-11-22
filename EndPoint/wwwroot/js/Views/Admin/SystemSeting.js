






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({  ParentCode: !isEmpty($("#SystemSetingID").val()) ? $("#SystemSetingID").val() : null, Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';

                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].label + "</td><td><a title='ویرایش' href='/Admin/SystemSeting/EditSystemSeting?id=" + res.data[i].systemSetingId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function initSystemSeting(id = null) {
        
        systemSetingList("ParentCode");
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/SystemSeting/Get_SystemSeting/" + id, null, true, function (res) {

                if (res != null) {
                   $("#SystemSetingID").val(res.systemSetingId);                    
                   $("#Label").val(res.label);
                   $("#ParentCode").val(res.parentCode);                   
                    comboBoxWithSearchUpdateText("ParentCode", res.parenLabel);
                    systemSetingListSub("SubCodes", res.systemSetingId);
                }
            }, true);
        }

    }   

    function systemSetingList(ComboName) {

        ComboBoxWithSearch('.select2', 'rtl');

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({  ParentCode:null, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {

               // var resA = new Array();

                var strM = '<option value="">انتخاب کنید</option>';

                for (var i = 0; i < res.data.length; i++) {

                    //  var q = resA.filter(p => p.ParentCode == res.data[i].labeCode);
                    //  if (q.length > 0) continue;
                    if (res.data[i].parentCode == null) {
                      //  resA.push(res.data[i]);
                        strM += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                  
                }
                $("#" + ComboName).html(strM);
            }

        }, true);
    }

    function systemSetingListSub(ComboName,PC=null) {

        ComboBoxWithSearch('.select2', 'rtl');

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCode: PC, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strM ="";

                for (var i = 0; i < res.data.length; i++) {

                        strM += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";                   

                }
                $("#" + ComboName).html(strM);
            }

        }, true);
    }

    function saveSystemSeting(e) {

        $(e).attr("disabled", "");
       
        AjaxCallAction("POST", "/api/admin/SystemSeting/Save_SystemSeting", JSON.stringify({ NewLabel: $("#NewLabel").val(), SystemSetingID: $("#SystemSetingID").val(), Label: $("#Label").val(), ParentCode: $("#ParentCode").val() }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {               
                var o = new Option($("#NewLabel").val(), "");
                $(o).html($("#NewLabel").val());
                $("#SubCodes").append(o);
                $("#NewLabel").val("");
               // goToUrl("/Admin/SystemSeting/EditSystemSeting?id=" + $("#SystemSetingID").val());
            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }


    web.SystemSeting = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        InitSystemSeting: initSystemSeting,
        SystemSetingList: systemSetingList,
        SaveSystemSeting: saveSystemSeting,
        SystemSetingListSub: systemSetingListSub,
         
    };

})(Web, jQuery);