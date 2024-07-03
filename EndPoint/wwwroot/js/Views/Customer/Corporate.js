
(function (web, $) {

    //Document Ready   
    function initCorporate(id = null) {
        PersianDatePicker(".DatePicker");
        $("#RequestIdForms").val(id);
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
        let strFormId = "";
        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormQuestionss", JSON.stringify({
            DataFormId: FormID, PageIndex: 0, PageSize: 0, DataFormType: 2, IsActive: 15
        }), true, function (res) {
            if (res.isSuccess) {
                let strFormId = generate_strFormId(res, RequestId, FormID);
                $("#FormDetail" + FormID).html(strFormId);
                ComboBoxWithSearch('.select2', 'rtl');
            }
        }, true);
    }
    function generate_strFormId(res, RequestId, FormID) {
        let strFormId = "";
        strFormId += "<div class='col-md-12'>";
        for (var i = 0; i < res.data.length; i++) {
            strFormId += "<div class='form-group'><div class='col-md-12'><h4 style='line-height: 1.5;'>" + res.data[i].questionText + "</h4></div><div class='col-md-12'><div class='row'><div class='col-md-2'>";
            if (res.data[i].questionType == 'select') {
                var options = combo(res.data[i].dataFormQuestionId);
                strFormId += "<select name='Q_" + res.data[i].dataFormQuestionId + "' class='form-control select2' >" + options + "</select>";
            }
            else if (res.data[i].questionType == 'yesNo') {
                strFormId += "<label class='control-label'>بله</label><input type='radio' name='Q_" + res.data[i].dataFormQuestionId + "' value='Yes' />";
                strFormId += "<label class='control-label'>خیر</label><input type='radio' name='Q_" + res.data[i].dataFormQuestionId + "' value='No' />";
            }
            strFormId += "</div><div class='col-md-10'>";
            strFormId += "<input class='form-control' name='Description_Q" + res.data[i].dataFormQuestionId + "' type='text' placeholder='توضیحات' /></div></div></div></div>"; 
        }
        strFormId += "</div>";
        return strFormId;
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
    function makeDynamicForm(SubCategoryName, PutPlace, FirstItemActive = true, PutTabPane) {
        let DataFormList = "";
        let ID = $("#RequestIdForms").val();
        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataForms", JSON.stringify({PageIndex: 0, PageSize: 0, DataFormType: 2 }), false, function (res) {
            if (res.isSuccess) {
                DataFormList = res.data;
            }
        }, true);
        let is_first = FirstItemActive;
        let li_option = "";
        let tabPane = "";
        for (let i = 0; i < DataFormList.length; i++) {
            if (DataFormList[i].formTitle.slice(0, 1) === SubCategoryName) {
                if (is_first) {
                    li_option += "<li class='active'><a href='#FormDetailTab" + DataFormList[i].formTitle + "' data-toggle='tab' aria-expanded='false' >" + DataFormList[i].formTitle + "</a></li>";
                    tabPane += makeTabPane(DataFormList[i].formTitle, DataFormList[i].formId, ID, is_first)
                    is_first = false;
                }
                else {
                    tabPane += makeTabPane(DataFormList[i].formTitle, DataFormList[i].formId, ID, is_first)
                    li_option += "<li class=''><a href='#FormDetailTab" + DataFormList[i].formTitle + "' data-toggle='tab' aria-expanded='false' >" + DataFormList[i].formTitle + "</a></li>";
                }
                
            }
        }
        $("#" + PutPlace).append(li_option);
        $("#" + PutTabPane).append(tabPane);
        for (let i = 0; i < DataFormList.length; i++) {
            if (DataFormList[i].formTitle.slice(0, 1) === SubCategoryName) {
                intiForm(DataFormList[i].formId, ID);
            }
        }  
    }

    function makeTabPane(FormTitle, FormId, RequestId, FirstItemActive = true) {
        let is_first = FirstItemActive;
        let strM = "";
        if (is_first) {
            strM = "<div class='tab-pane active' id='FormDetailTab" + FormTitle + "'>";
        }
        else {
            strM = "<div class='tab-pane' id='FormDetailTab" + FormTitle + "'>";
        }
        strM += "<div style='display:flex;justify-content: space-between;align-items: center;'>";
        strM += "<h2 class='fs-title'>" + FormTitle + "</h2>";
        strM += "<a class='btn btn-success' style='height: 35px;' onclick='Web.Corporate.SaveSerializedForm(" + FormId +");'>ذخیره تغییرات</a></div>";
        strM += "<div style=' border: 2px solid #00c0ef; padding: 30px; border-radius: 5px; margin-bottom: 20px'><form id='frmFrom";
        strM += FormId + "' class='changeData'>";
        strM += "<input type='hidden' id='FormID' name='FormID' value='" + FormId + "' />";
        strM += "<input type='hidden' id='RequestId' name='RequestId' value='" + RequestId + "' />";
        strM += "<div class='row' id='FormDetail" + FormId + "'></div></form></div></div>";
        return strM
    }

    function saveSerializedForm(FormId) {
        let SerializerForm = $("#frmFrom" + FormId).serialize().split('&');
        let formId = SerializerForm[0].split("=")[1];
        let ListOfAnswers = SerializerForm.slice(2, SerializerForm.length);
        let counter = ListOfAnswers.length;
        try {
            for (let i = 0; i < counter && counter % 2 == 0; i += 2) {
                let SingleQuestion = ListOfAnswers.slice(0, 2);
                ListOfAnswers = ListOfAnswers.slice(2, ListOfAnswers.length)
                let question_id = SingleQuestion[0].split("_")[1].split("=")[0]
                let answer = SingleQuestion[0].split("_")[1].split("=")[1]
                let description = decodeURIComponent(SingleQuestion[1].split("=")[1])
                if (!isEmpty(answer)) {
                    saveSingelAnswerForm(formId, answer, description, question_id);
                }
            }
        }
        catch (e) {
        }
    }

    function saveSingelAnswerForm(formId, answer, description = "" ,dataFormQuestionId) {
        var requestId = $("#RequestId").val();
        AjaxCallAction("POST", "/api/customer/Corporate/Save_DataFromAnswers", JSON.stringify({
            Answer: answer,
            Description: description,
            FormId: parseInt(formId),
            RequestId: parseInt(requestId),
            DataFormQuestionId: parseInt(dataFormQuestionId),
        }), false, function (res) {
            if (res.isSuccess) {
                //  alertB("ثبت", res.message, "success");
            }
            else {
                alertB("خطا", res.message, "error");
            }
        }, true);
    }
    
    web.Corporate = {
        IntiForm: intiForm,
        InitCorporate: initCorporate,
        InitCustomer: initCustomer,
        Combo: combo,
        MakeDynamicForm: makeDynamicForm,
        SaveSerializedForm: saveSerializedForm,
    };

})(Web, jQuery);