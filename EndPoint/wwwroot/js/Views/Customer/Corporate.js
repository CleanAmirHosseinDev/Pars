
(function (web, $) {

    //Document Ready   
    function initCorporate(id = null) {
        PersianDatePicker(".DatePicker");
        $("#RequestIdForms").val(id);
       // initReferral(id);        
        intiTab(1);
        initCustomer();
    }

    function initCustomer(dir = 'rtl') {

        ComboBoxWithSearch('.select2', dir);

        AjaxCallAction("GET", "/api/customer/Customers/Get_Customers/", null, true, function (res) {


            if (res != null) {
                $("#CutomerName").html("<h4> فرم پرسشنامه حاکمیت شرکتی " + res.companyName + "</h4>");
            }

        }, true);

    }

    function intiForm(FormID = null, RequestId = null) {

        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormQuestionss", JSON.stringify({ DataFormId: FormID, PageIndex: 0, PageSize: 0, IsActive:15 }), true, function (res) {

            if (res.isSuccess) {

                var strFormId = '';
                var Filename = 1;
                for (var i = 0; i < res.data.length; i++) {
                    if (i == 0) {
                        strFormId += "<Input type='hidden' id='RequestId' name='RequestId' value='" + RequestId + "'/>";
                    }

                    if (i == 0) {
                        strFormId += "<div class='col-lg-6'>";
                         } else if (i == Math.round((res.data.length) / 2) && res.data.length > 1) {
                        strFormId += "</div><div class='col-lg-6'>";
                    }

                    strFormId += "<div class='form-group'><div class='col-md-12' style='margin-bottom:10px'><label class='control-label'  for=''>" + res.data[i].questionText + "<span class='RequiredLabel'>*</span></label>";
                    if (res.data[i].questionType == 'select') {
                        var options = combo(res.data[i].dataFormQuestionId);
                        strFormId += "<select name='question" + res.data[i].dataFormQuestionId + "' id='question" + res.data[i].dataFormQuestionId + "' class='form-control select2' >" + options + "</select>";
                    } else if (res.data[i].questionType == 'textarea') {
                        strFormId += "<textarea name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' class='form-control' ></textarea>";
                    }
                    else if (res.data[i].questionType == 'checkbox') {
                        strFormId += "<input type='" + res.data[i].questionType + "' name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' style='text-align:right; width: 30px' />";
                    } else if (res.data[i].questionType == 'file') {
                        strFormId += "<input type='" + res.data[i].questionType + "' name='Result_Final_FileName" + Filename + "' id='" + res.data[i].questionName + "' /><div id='Div" + res.data[i].questionName + "'></div>";
                        Filename++;
                    }
                    else if (res.data[i].questionType == 'yesNo') {
                        strFormId += "<br/><lable>بله</label><input type='radio' name='Form" + FormID + "' id='" + FormID + res.data[i].dataFormQuestionId + "Yes' />";
                        strFormId += "<lable>خیر</label><input type='radio' name='Form" + FormID + "' id='" + FormID + res.data[i].dataFormQuestionId + "No' />";
                        Filename++;


                    }
                    else {
                        strFormId += "<input type='" + res.data[i].questionType + "' name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' placeholder='" + res.data[i].questionText + "' class='form-control' />";
                    }
                    strFormId += "</div>";
                    strFormId += "</div>";
                    if (i == res.data.length && res.data.length > 1) {
                        strFormId += "</div>";
                    }

                }
                $("#FormDetail" + FormID).html(strFormId);
                ComboBoxWithSearch('.select2', 'rtl');
           }
        }, true);
    }

    function combo(QuestionID = null) {
        let strM = '<option value="">انتخاب کنید</option>';
        AjaxCallAction("POST", "/api/customer/Corporate/Get_Options", JSON.stringify({ DataFormQuestionsId: QuestionID, PageIndex: 0, PageSize: 0, IsActive: 15 }), false, function (res) {
            if (res.isSuccess) {
                strM = '<option value="">انتخاب کنید</option>';
                for (var i = 0; i < res.data.length; i++) {
                    strM += " <option value=" + res.data[i].id + ">" + res.data[i].text + "</option>";
                }
            }
        }, true);
        return strM;
    }

    function intiTab(TabId = null) {
        var ID = $("#RequestIdForms").val();

        switch (TabId) {
            case 1:
                intiForm(26, RequestId = ID);
               // intiFormShow(1, "1,2,7,8", ID);
                break;
        }

    }

    
    web.Corporate = {
        IntiForm: intiForm,
        InitCorporate: initCorporate,
        InitCustomer: initCustomer,
        IntiTab: intiTab,
        Combo: combo
    };

})(Web, jQuery);