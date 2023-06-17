






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        AjaxCallAction("POST", "/api/admin/Contract/Get_Contracts", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {

            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {

                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].kinfOfRequestNavigation.label + "</td><td><a title='پیش نمایش' href='/Admin/Contract/ContractPrint?id=" + res.data[i].contractId + "' class='btn btn-info fontForAllPage'><i class='fa fa-print'></i></a><a title='ویرایش' href='/Admin/Contract/EditContract?id=" + res.data[i].contractId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' class='btn btn-danger fontForAllPage' onclick='Web.Contract.Delete_Contract(" + res.data[i].contractId + ");'><i class='fa fa-remove'></i></a></td></tr>";

                }
                $("#tBodyList").html(strM);

            }

        }, true);

    }

    function saveContract(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/Contract/Save_Contract", JSON.stringify({
            ContractText: getDataCkeditor("ContractText"),
            KinfOfRequest: $("#KinfOfRequest").val(),
            ContractId: $("#ContractId").val(),

        }), true, function (res) {

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

                    $("#ContractId").val(res.contractId);
                    $("#ContractText").val(res.contractText);
                    Ckeditor("ContractText");
                    systemSeting_Combo(res);
                }

            }, true);

        }
        else {
            Ckeditor("ContractText");
            systemSeting_Combo(null);
        }

    }

    function printContract(id=null) {

        goToUrl("/admin/Contract/ContractPrint/" + id);
    }
    

    function printContracting(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/Contract/Get_Contract/" + id, null, true, function (res) {

                if (res != null) {
                   
                    $("#SaveDate").html("1401/01/01");
                    $("#ContractCode").html("11111");
                    $("#ContractShow").html(res.contractText);
                    $('input[type="text"], textarea').each(function () {
                        //  $(this).attr('readonly', 'readonly');
                        // var text_classname = $(this).attr('name');
                        var value = $(this).val();
                        var new_html = ('<storang>' + value + '</storang>');
                        $(this).replaceWith(new_html);
                    });
                    window.print();
                }

            }, true);

        }

       
    }



    function systemSeting_Combo(resSingle) {

        AjaxCallAction("POST", "/api/admin/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "63", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strKinfOfRequest = '<option value="">انتخاب کنید</option>';
               
                for (var i = 0; i < res.data.length; i++) {
                   
                    strKinfOfRequest+= " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    
                }
             
                $("#KinfOfRequest").html(strKinfOfRequest);
                if (resSingle != null) {
                    $("#KinfOfRequest").val(resSingle.kinfOfRequest);

                }
            


            }
        }, true);
    }

    function delete_Contract(id) {

        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/admin/Contract/Delete_Contract/" + (isEmpty(id) ? '0' : id), null, true, function (result) {

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

    web.Contract = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveContract: saveContract,
        InitContract: initContract,
        SystemSeting_Combo: systemSeting_Combo,
        Delete_Contract: delete_Contract,
        PrintContract: printContract,
        PrintContracting: printContracting
    };

})(Web, jQuery);