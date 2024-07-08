(function (web, $) {
    var DataFormList = "";
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
                let LoadedDataFromDb = "";
                AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFromAnswerss", JSON.stringify({
                    PageIndex: 0, PageSize: 0, FormID: FormID, RequestId: RequestId
                }), true, function (res) {
                    if (res.isSuccess) {
                        LoadedDataFromDb = res.data;
                        for (let i = 0; LoadedDataFromDb.length > i; i++) {
                            if (LoadedDataFromDb[i].answer == "Yes") {
                                $("input:radio[name='Q_" + LoadedDataFromDb[i].dataFormQuestionId + "'][value='Yes']").prop('checked', true);
                            } else if (LoadedDataFromDb[i].answer == "No") {
                                $("input:radio[name='Q_" + LoadedDataFromDb[i].dataFormQuestionId + "'][value='No']").prop('checked', true);
                            } else {
                                $("#Q_" + LoadedDataFromDb[i].dataFormQuestionId + " option[value='" + LoadedDataFromDb[i].answer + "']").prop("selected", true);
                            }
                            $("input[name*='Description_Q" + LoadedDataFromDb[i].dataFormQuestionId + "']").val(LoadedDataFromDb[i].description);
                        }
                    }
                }, true);
            }
        }, true);
    }
    function generate_strFormId(res, RequestId, FormID) {
        let strFormId = "";
        strFormId += "<div class='col-md-12'>";
        for (var i = 0; i < res.data.length; i++) {
            strFormId += "<div class='form-group'><div class='col-md-12'><h4 style='line-height: 1.5;'>" + res.data[i].questionText + "</h4></div><div class='col-md-12'><div class='row'><div class='col-md-4'>";
            if (res.data[i].questionType == 'select') {
                var options = combo(res.data[i].dataFormQuestionId);
                strFormId += "<select required name='Q_" + res.data[i].dataFormQuestionId + "' id='Q_" + res.data[i].dataFormQuestionId + "' class='form-control' style='padding: 0px 15px;' >" + options + "</select>";
            }
            else if (res.data[i].questionType == 'yesNo') {
                strFormId += "<label class='control-label'>بله</label><input type='radio' required name='Q_" + res.data[i].dataFormQuestionId + "' value='Yes' />";
                strFormId += "<label class='control-label'>خیر</label><input type='radio' required name='Q_" + res.data[i].dataFormQuestionId + "' value='No' />";
            }
            strFormId += "</div><div class='col-md-8'>";
            strFormId += "توضیحات : <input class='form-control' name='Description_Q" + res.data[i].dataFormQuestionId + "' onfocus='select();' type='text' value='ندارد...' /></div></div></div></div>"; 
        }
        strFormId += "</div>";
        return strFormId;
    }
    function combo(QuestionID = null) {
        let strM = '';
        AjaxCallAction("POST", "/api/customer/Corporate/Get_Options", JSON.stringify({ DataFormQuestionsId: QuestionID, PageIndex: 0, PageSize: 0, IsActive: 15 }), false, function (res) {
            if (res.isSuccess) {
                strM = '<option value="0">انتخاب کنید</option>';
                for (var i = 0; i < res.data.length; i++) {
                    strM += "<option value='" + res.data[i].id + "_" + res.data[i].text + "'>" + res.data[i].text + "</option>";
                }
            }
        }, true);
        return strM;
    }
    function makeDynamicForm(SubCategoryName, PutPlace, FirstItemActive = true, PutTabPane) {
        let ID = $("#RequestIdForms").val();
        if (isEmpty(DataFormList))
            AjaxCallAction("POST", "/api/customer/Corporate/Get_DataForms", JSON.stringify({PageIndex: 0, PageSize: 0, DataFormType: 2 }), false, function (res) {
                if (res.isSuccess) {
                    DataFormList = res.data;
                }
            }, true);
        let is_first = FirstItemActive;
        let li_option = "";
        let tabPane = "";
        for (let i = 0; i < DataFormList.length; i++) {
            if (DataFormList[i].formCode.slice(0, 1) === SubCategoryName) {
                if (is_first) {
                    li_option += "<li class='active'><a href='#FormDetailTab" + DataFormList[i].formCode + "' data-toggle='tab' aria-expanded='false' >" + DataFormList[i].formTitle + "</a></li>";
                    tabPane += makeTabPane(DataFormList[i].formCode, DataFormList[i].formTitle, DataFormList[i].formId, ID, is_first)
                    is_first = false;
                }
                else {
                    tabPane += makeTabPane(DataFormList[i].formCode, DataFormList[i].formTitle, DataFormList[i].formId, ID, is_first)
                    li_option += "<li class=''><a href='#FormDetailTab" + DataFormList[i].formCode + "' data-toggle='tab' aria-expanded='false' >" + DataFormList[i].formTitle + "</a></li>";
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
    function makeTabPane(FormCode, FormTitle, FormId, RequestId, FirstItemActive = true) {
        let is_first = FirstItemActive;
        let strM = "";
        if (is_first) {
            strM = "<div class='tab-pane active' id='FormDetailTab" + FormCode + "'>";
        }
        else {
            strM = "<div class='tab-pane' id='FormDetailTab" + FormCode + "'>";
        }
        strM += "<div style='display:flex;justify-content: space-between;align-items: center;'>";
        strM += "<h2 class='fs-title'>" + FormTitle + "</h2>";
        strM += "<a class='btn btn-success' style='height: 35px;' onclick='Web.Corporate.SaveSerializedForm(" + FormId + ");'>ذخیره تغییرات" + FormTitle + "</a></div>";
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
                let answer = decodeURIComponent(SingleQuestion[0].split("=")[1])
                let description = decodeURIComponent(SingleQuestion[1].split("=")[1])
                if (!isEmpty(answer) && answer != "0") {
                    saveSingelAnswerForm(formId, answer, description, question_id);
                }
            }
        }
        catch (e) {
        }
    }
    function saveSingelAnswerForm(formId, answer, description = "" ,dataFormQuestionId) {
        var requestId = $("#RequestId").val();
        var dataFormQuestionScore = 0;
        AjaxCallAction("POST", "/api/customer/Corporate/Save_DataFromAnswers", JSON.stringify({
            Answer: answer,
            Description: description,
            FormId: parseInt(formId),
            RequestId: parseInt(requestId),
            DataFormQuestionId: parseInt(dataFormQuestionId),
            IsActive: 15,
        }), false, function (res) {
            if (res.isSuccess) {
                // سوال رو دریافت کردم تا نمره مربوط به اون رو داشته باشم
                AjaxCallAction("GET", "/api/customer/Corporate/Get_DataFormQuestions/" + dataFormQuestionId, null, false, function (res1) {
                    if (!isEmpty(res1)) {
                        dataFormQuestionScore = res1.score;
                        if (res1.questionType == 'select')
                            AjaxCallAction("GET", "/api/customer/Corporate/Get_Option/" + answer.split("_")[0], null, false, function (datares) {
                                if (!isEmpty(datares)) {
                                    dataFormQuestionScore = dataFormQuestionScore * datares.ratio;
                                }
                            }, true);
                        if (res1.questionType == 'yesNo')
                            if (answer == "No")
                                dataFormQuestionScore = 0;

                        // اگر خواست فرم رو اپدیت کنه یعنی پاسخ اش رو عوض کنه ای دی پاسخش عوض نمی شه فقط
                        // متن پاسخش عوض میشه پس اگر ایدی پاسخ توی کد وضعیت 0 بود یعنی فرم اپدیت شده
                        // وقتی فرم پاسخ اپدیت میشه نیازی به ذخیره فرم آنالیز پاسخ نیست
                        if (res.dataId != 0)
                            // ذخیره سوال در جدول آنالیز نمره
                            AjaxCallAction("POST", "/api/customer/Corporate/Save_DataFromReport", JSON.stringify({
                                RequestId: requestId,
                                DataFormAnswerId: res.dataId,
                                SystemScore: dataFormQuestionScore,
                                AnalizeScore: 0,
                                IsActive: 15,
                            }), true, function (reee) { }, true);
                    }
                }, true);
                alertB("ثبت", res.message, "success")
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