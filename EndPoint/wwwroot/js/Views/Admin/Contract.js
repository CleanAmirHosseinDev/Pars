






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/Contract/Get_Contracts", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].kinfOfRequestNavigation.label +  "</td><td><a title='ویرایش' href='/Admin/Contract/EditContract?id=" + res.data[i].ContractId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveContract(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/Contract/Save_Contract", JSON.stringify({ ContractName: $("#ContractName").val(), ContractId: $("#ContractId").val() }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/Contract/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initContract(id = null) {
        ComboBoxWithSearch('.select2', 'dir');

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/Contract/Get_Contract/" + id, null, true, function (res) {

                if (res != null) {
                    
                    $("#ContractId").val(res.ContractId);
                    $("#ContractComment").val(res.ContractComment);
                    systemSeting_Combo(res);
                }

            }, true);

        }

    }

    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "150", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strContractTitle = '<option value="">انتخاب کنید</option>';
               
                for (var i = 0; i < res.data.length; i++) {
                   
                    strContractTitle += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    
                }
             
                $("#ContractTitle").html(strContractTitle);              
                $("#ContractTitle").val(resSingle.ContractTitle);



            }
        }, true);
    }

    web.Contract = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveContract: saveContract,
        InitContract: initContract,
        SystemSeting_Combo: systemSeting_Combo
    };

})(Web, jQuery);