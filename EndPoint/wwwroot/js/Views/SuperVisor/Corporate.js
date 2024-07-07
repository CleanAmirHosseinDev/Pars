(function (web, $) {

    //Document Ready   
    function initCorporate(id = null) {
        PersianDatePicker(".DatePicker");
        $("#RequestIdForms").val(id);
        initCustomer(id);
    }
    function initCustomer(dir = 'rtl', id) {
        ComboBoxWithSearch('.select2', dir);
        AjaxCallAction("GET", "/api/superVisor/Corporate/Get_Customers/" + id, null, true, function (res) {
            if (res != null) {
                $("#CutomerName").html("<h4> فرم پاسخ های حاکمیت شرکتی " + res.companyName + "</h4>");
            }
        }, true);
    }
    function intiForm(FormID = null, RequestId = null) {
        let strFormId = "";
        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormQuestionss", JSON.stringify({
            DataFormId: FormID, PageIndex: 0, PageSize: 0, DataFormType: 2, IsActive: 15
        }), true, function (res) {
            if (res.isSuccess) {
                strFormId = generate_strFormId(res, RequestId, FormID);
                $("#FormDetail" + FormID).html(strFormId);
            }
        }, true);
    }
    function generate_strFormId(QuestionData, RequestId, FormID) {
        var _DataAnswer;
        var _str_tag;
        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFromAnswerss", JSON.stringify({
            PageIndex: 0, PageSize: 0, FormID: FormID, RequestId: RequestId
        }), false, function (res) {
            if (res.isSuccess) {
                _DataAnswer = res.data;
            }
        }, true);
        for (let i = 0; QuestionData.length > i; i++) {
            var report;
            var answer = _DataAnswer.find(o => o.dataFormQuestionID === QuestionData[i].dataFormQuestionId)
            AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataFormReport/", JSON.stringify({
                DataFormAnswerId: answer.answerID,
                RequestId: RequestId,
            }), false, function (res) {
                if (res != null) {
                    report = res;
                }
            }, true);
            _str_tag = "<div class='form-group'><div class='col-md-12'><h4 style='line-height: 1.5;'>" + QuestionData[i].questionText;
            _str_tag += "</h4></div><div class='col-md-12'><div class='row'><div class='col-md-2'><p>" + answer.answer;
            _str_tag += "</p></div><div class='col-md-6'><p>" + answer.description;
            _str_tag += "</p></div><div class='col-md-2'><p>" + report.systemScore;
            _str_tag += "</p></div><div class='col-md-2'><p>" + "<input type='text' value='' />";
            _str_tag += "</p></div></div></div></div><br>";
        }
        return _str_tag;
    }

    function makeDynamicForm(SubCategoryName, PutPlace, FirstItemActive = true, PutTabPane) {
        let DataFormList = "";
        let ID = $("#RequestIdForms").val();
        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataForms", JSON.stringify({ PageIndex: 0, PageSize: 0, DataFormType: 2 }), false, function (res) {
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
    function saveSingelAnswerForm(formId, answer, description = "", dataFormQuestionId) {
        var requestId = $("#RequestId").val();
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
                var dataFormQuestionScore = 0;
                AjaxCallAction("GET", "/api/customer/Corporate/Get_DataFormQuestions/" + dataFormQuestionId, null, false, function (res) {
                    if (res.isSuccess) {
                        dataFormQuestionScore = res.score;
                        if (res.questionType == 'select')
                            AjaxCallAction("GET", "/api/customer/Corporate/Get_Option/" + answer.split("_")[0], null, false, function (datares) {
                                if (datares.isSuccess) {
                                    dataFormQuestionScore = dataFormQuestionScore * datares.ratio;
                                }
                            }, true);
                    }
                }, true);

                // اگر خواست فرم رو اپدیت کنه یعنی پاسخ اش رو عوض کنه ای دی پاسخش عوض نمی شه فقط
                // متن پاسخش عوض میشه پس اگر ایدی پاسخ توی کد وضعیت 0 بود یعنی فرم اپدیت شده
                // وقتی فرم پاسخ اپدیت میشه نیازی به ذخیره فرم آنالیز پاسخ نیست
                if (res.dataId != 0)
                    // ذخیره سوال در جدول آنالیز نمره
                    AjaxCallAction("POST", "/api/customer/Corporate/Save_DataFromReport", JSON.stringify({
                        RequestId: requestId,
                        DataFormAnswerId: res.dataId,
                        SystemScore: parseInt(dataFormQuestionScore),
                        AnalizeScore: 0,
                        IsActive: 15,
                    }), true, undefined, true);

                alertB("ثبت", res.message, "success")
            }
            else {
                alertB("خطا", res.message, "error");
            }
        }, true);
    }

    web.CorporateSuperVisor = {
        IntiForm: intiForm,
        InitCorporate: initCorporate,
        InitCustomer: initCustomer,
        MakeDynamicForm: makeDynamicForm,
        SaveSerializedForm: saveSerializedForm,
    };

})(Web, jQuery);