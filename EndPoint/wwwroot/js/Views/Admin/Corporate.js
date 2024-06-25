
(function (web, $) {

    //Document Ready   
    //function initCorporate(id = null) {
    //    PersianDatePicker(".DatePicker");
    //    $("#RequestIdForms").val(id);
    //   // initReferral(id);
    //    for (let i = 26; i < 30; i++) {
    //        dellocalstor("intiForm" + i + id);
    //    }
    //    corporateIntiTab(1);
    //    initCustomer();
    //}

    //function initCustomer(dir = 'rtl') {

    //    ComboBoxWithSearch('.select2', dir);

    //    AjaxCallAction("GET", "/api/customer/Customers/Get_Customers/", null, true, function (res) {


    //        if (res != null) {
    //            $("#CutomerName").html("<h4> فرم پرسشنامه حاکمیت شرکتی " + res.companyName + "</h4>");
    //        }

    //    }, true);

    //}

    //function intiForm(FormID = null, RequestId = null) {
    //    let strFormId = getlocalstor("intiForm" + FormID + RequestId);
    //    if (strFormId === "") {
    //        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormQuestionss", JSON.stringify({ DataFormId: FormID, PageIndex: 0, PageSize: 0, IsActive: 15 }), true, function (res) {
    //            if (res.isSuccess) {
    //                strFormId = generate_strFormId(res, RequestId);
    //                setlocalstor("intiForm" + FormID + RequestId, strFormId)
    //                $("#FormDetail" + FormID).html(strFormId);
    //                ComboBoxWithSearch('.select2', 'rtl');
    //            }
    //        }, true);
    //    }
    //    $("#FormDetail" + FormID).html(strFormId);
    //    ComboBoxWithSearch('.select2', 'rtl');
    //}

    //function generate_strFormId(res, RequestId) {
    //    let strFormId = "";
    //    let Filename = 1;
    //    for (var i = 0; i < res.data.length; i++) {
    //        if (i == 0) {
    //            strFormId += "<Input type='hidden' id='RequestId' name='RequestId' value='" + RequestId + "'/><div class='col-md-12'>";
    //        }
    //        strFormId += "<div class='form-group'><div class='col-md-12'><h4 style='line-height: 1.5;'>" + res.data[i].questionText + "</h4></div><div class='col-md-12'><div class='row'><div class='col-md-2'>";
    //        if (res.data[i].questionType == 'select') {
    //            var options = combo(res.data[i].dataFormQuestionId);
    //            strFormId += "<select name='question" + res.data[i].dataFormQuestionId + "' id='question" + res.data[i].dataFormQuestionId + "' class='form-control select2' >" + options + "</select>";
    //        } else if (res.data[i].questionType == 'textarea') {
    //            strFormId += "<textarea name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' class='form-control' ></textarea>";
    //        }
    //        else if (res.data[i].questionType == 'checkbox') {
    //            strFormId += "<input type='" + res.data[i].questionType + "' name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' style='text-align:right; width: 30px' />";
    //        } else if (res.data[i].questionType == 'file') {
    //            strFormId += "<input type='" + res.data[i].questionType + "' name='Result_Final_FileName" + Filename + "' id='" + res.data[i].questionName + "' /><div id='Div" + res.data[i].questionName + "'></div>";
    //            Filename++;
    //        }
    //        else if (res.data[i].questionType == 'yesNo') {
    //            strFormId += "<label class='control-label'>بله</label><input type='radio' name='Form" + FormID + res.data[i].dataFormQuestionId + "' id='" + FormID + res.data[i].dataFormQuestionId + "Yes' />";
    //            strFormId += "<label class='control-label'>خیر</label><input type='radio' name='Form" + FormID + res.data[i].dataFormQuestionId + "' id='" + FormID + res.data[i].dataFormQuestionId + "No' />";
    //            Filename++;
    //        }
    //        else {
    //            strFormId += "<input type='" + res.data[i].questionType + "' name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' placeholder='" + res.data[i].questionText + "' class='form-control' />";
    //        }
    //        strFormId += "</div><div class='col-md-10'>";
    //        strFormId += "<input class='form-control' name='Description_" + FormID + "_" + res.data[i].dataFormQuestionId + "' type='text' id='Description_" + FormID + "_" + res.data[i].dataFormQuestionId + "' placeholder='توضیحات' /></div></div></div></div>";
    //        if (i == res.data.length && res.data.length > 1) {
    //            strFormId += "</div>";
    //        }
    //    }
    //    return strFormId;
    //}


    //function combo(QuestionID = null) {
    //    let strM = '<option value="">انتخاب کنید</option>';
    //    AjaxCallAction("POST", "/api/customer/Corporate/Get_Options", JSON.stringify({ DataFormQuestionsId: QuestionID, PageIndex: 0, PageSize: 0, IsActive: 15 }), false, function (res) {
    //        if (res.isSuccess) {
    //            strM = '<option value="">انتخاب کنید</option>';
    //            for (var i = 0; i < res.data.length; i++) {
    //                strM += " <option value=" + res.data[i].id + ">" + res.data[i].text + "</option>";
    //            }
    //        }
    //    }, true);
    //    return strM;
    //}

    //function corporateIntiTab(TabId = null) {
    //    var ID = $("#RequestIdForms").val();

    //    switch (TabId) {
    //        case 1:
    //            intiForm(26, RequestId = ID);
    //           // intiFormShow(1, "1,2,7,8", ID);
    //            break;
    //        case 2:
    //            intiForm(27, RequestId = ID);
    //            break;
    //        case 3:
    //            intiForm(28, RequestId = ID);
    //            break;
    //    }

    //}

    function dataFormQuestionsOptioneFilterGrid() {
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataFormQuestionsOptiones", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {
            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {
                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].text + "</td><td>" + res.data[i].dataformquestionsid + "</td><td>" + res.data[i].ratio + "</td><td><a title='ویرایش' href='/Admin/Corporate/EditDataFormQuestionsOptione/id=" + res.data[i].id + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";
                }
                $("#tBodyList").html(strM);
            }
        }, true);
    }
    
    web.Corporate = {
        //IntiForm: intiForm,
        //InitCorporate: initCorporate,
        //InitCustomer: initCustomer,
        //CorporateIntiTab: corporateIntiTab,
        //Combo: combo,
        //makeForm: makeForm,
        DataFormQuestionsOptioneFilterGrid: dataFormQuestionsOptioneFilterGrid,
    };

})(Web, jQuery);