(function (web, $) {
    var DataFormList = "";
    //Document Ready   
    function initCorporate(id = null) {
        PersianDatePicker(".DatePicker");
        $("#RequestIdForms").val(id);
        getCustomerInfo(id, dir = 'rtl');
    }

    function getCustomerInfo(id = null) {
        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("POST", "/api/superVisor/RequestForRating/Get_RequestForRatings", JSON.stringify({ RequestId: id, Search: null, PageIndex: 1, PageSize: 1, }), true, function (res) {
                if (res.isSuccess) {
                    for (var i = 0; i < res.data.length; i++) {
                        initCustomer(res.data[i].customerId);
                    }
                }
            }, true);
        }
    }

    function initCustomer(id, dir = 'rtl') {
        AjaxCallAction("GET", "/api/superVisor/Customers/Get_Customers/" + (isEmpty(id) ? '0' : id), null, true, function (res) {
            if (res != null) {
                $("#CutomerName").html("<h4> فرم پاسخ های حاکمیت شرکتی " + res.companyName + "</h4>");
            }
        }, true);
    }

    function intiForm(FormID = null, RequestId = null) {
        let strFormId = "";
        AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataFormQuestionss", JSON.stringify({
            DataFormId: FormID, PageIndex: 0, PageSize: 0, DataFormType: 2, IsActive: 15
        }), true, function (res) {
            if (res.isSuccess) {
                strFormId = generate_strFormId(res.data, RequestId, FormID);
                $("#FormDetail" + FormID).html(strFormId);
            }
        }, true);
    }
    function generate_strFormId(QuestionData, RequestId, FormID) {
        var _DataAnswer;
        var _str_tag = "";
        AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataFromAnswerss", JSON.stringify({
            PageIndex: 0, PageSize: 0, FormID: FormID, RequestId: RequestId
        }), false, function (res) {
            if (res.isSuccess) {
                _DataAnswer = res.data;
            }
        }, true);
        for (let i = 0; QuestionData.length > i; i++) {
            var report = "";
            var answer = _DataAnswer.find(o => o.dataFormQuestionId === QuestionData[i].dataFormQuestionId)

            var dataFormQuestionScore = 0;
            if (!isEmpty(answer)) {

                AjaxCallAction("GET", "/api/superVisor/Corporate/Get_DataFormQuestions/" + answer.dataFormQuestionId, null, false, function (res1) {
                    if (!isEmpty(res1)) {
                        dataFormQuestionScore = res1.score;
                        if (res1.questionType == 'select')
                            AjaxCallAction("GET", "/api/superVisor/Corporate/Get_Option/" + answer.answer.split("_")[0], null, false, function (datares) {
                                if (!isEmpty(datares)) {
                                    dataFormQuestionScore = dataFormQuestionScore * datares.ratio;
                                }
                            }, true);
                        if (res1.questionType == 'yesNo')
                            if (answer.answer == "No")
                                dataFormQuestionScore = 0;
                    }
                }, true);

                AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataFormReport/", JSON.stringify({
                    DataFormAnswerId: !isEmpty(answer.answerId) ? answer.answerId : 0,
                    RequestId: RequestId,
                }), false, function (res) {
                    if (res != null) {
                        report = res;
                    }
                }, true);
            }
            if (!isEmpty(report)) {
                _str_tag += "<div class='form-group'><div class='col-md-12'><h4 style='line-height: 1.5;'>" + QuestionData[i].questionText;
                _str_tag += "</h4></div><div class='col-md-12'><div class='row'><div class='col-md-12'>";
                _str_tag += "<label>پاسخ مشتری : </label><p style='display: inline-block;margin-right: 20px;'>";
                _str_tag += answer.answer == "Yes" || answer.answer == "No" ? answer.answer : answer.answer.split("_")[1]
                _str_tag += "</p></div><div class='col-md-12'><label>توضیحات : </label><p style='display: inline-block;margin-right: 20px;'>" + answer.description;
                _str_tag += "</p></div><div class='col-md-12'><label>امتیاز سیستم : </label><p style='display: inline-block;margin-right: 20px;'>" + dataFormQuestionScore;
                _str_tag += "</p></div><div class='col-md-12'><label>امتیاز کارشناس</label>";
                _str_tag += "<p style='display: inline-block;margin-right: 20px;'><input class='form-control' name='AnalizeScore_" + answer.answerId + "' ";
                _str_tag += "type='number' max='" + QuestionData[i].score + "' value='" + dataFormQuestionScore + "' min='0'></p></div>";
                _str_tag += "<div class='col-md-12'><label>توضیحات کارشناس</label><p style='display: inline-block;margin-right: 20px;'>";
                _str_tag += "<input type='text' name='descriptoin_" + report.dataReportId + "' value='ندارد ...' onfocus='select();'></p></div></div>"
                _str_tag += "<input type='hidden' name='QuestionScore' value='" + QuestionData[i].score + "'></div>"
            }
        }
        return _str_tag;
    }

    function makeDynamicForm(SubCategoryName, PutPlace, FirstItemActive = true, PutTabPane) {
        let ID = $("#RequestIdForms").val();
        if (isEmpty(DataFormList))
            AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataForms", JSON.stringify({ PageIndex: 0, PageSize: 0, DataFormType: 2 }), false, function (res) {
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
        strM += "<a class='btn btn-success' style='height: 35px;' onclick='Web.CorporateSuperVisor.SaveSerializedForm(" + FormId + ");'>ذخیره تغییرات" + FormTitle + "</a></div>";
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
            for (let i = 0; i < counter && counter % 3 == 0; i += 3) {
                let SingleQuestion = ListOfAnswers.slice(0, 3);
                let question_score = SingleQuestion[2].split("=")[1]
                let report_id = SingleQuestion[1].split("=")[0].split("_")[1]
                let description = decodeURIComponent(SingleQuestion[1].split("=")[1]);
                let answer_id = SingleQuestion[0].split("=")[0].split("_")[1]
                let score = SingleQuestion[0].split("=")[1]
                ListOfAnswers = ListOfAnswers.slice(3, ListOfAnswers.length)
                if (!isEmpty(score)) {
                    saveSingelAnswerForm(formId, answer_id, score, question_score, description, report_id);
                }
            }
            alertB("ثبت", "ثبت فرم با موفقیت انجام شد", "success");
        }
        catch (e) {
            alertB("خطا", "عملیات ذخیره سازی با خطا مواجه شد", "error");
        }
    }
    function saveSingelAnswerForm(formId, answer_id, score, question_score, description, reportId) {
        var requestId = $("#RequestId").val();
        // ذخیره سوال در جدول آنالیز نمره
        AjaxCallAction("POST", "/api/superVisor/Corporate/Save_DataFromReport", JSON.stringify({
            DataReportId: reportId,
            RequestId: requestId,
            DataFormAnswerId: answer_id,
            AnalizeScore: score,
            SystemScore: question_score,
            IsActive: 15,
            Description: description,
        }), true, function (data) { }, true);
    }

    web.CorporateSuperVisor = {
        IntiForm: intiForm,
        InitCorporate: initCorporate,
        InitCustomer: initCustomer,
        MakeDynamicForm: makeDynamicForm,
        SaveSerializedForm: saveSerializedForm,
    };

})(Web, jQuery);