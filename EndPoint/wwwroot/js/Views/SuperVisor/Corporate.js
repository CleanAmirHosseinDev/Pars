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
                _str_tag += " <span title='" + QuestionData[i].helpText + "'><i class='fa'></i></span></h4></div><div class='col-md-12'><div class='row'><div class='col-md-12'>";
                _str_tag += "<label>پاسخ مشتری : </label><p style='display: inline-block;margin-right: 20px;'>";
                _str_tag += answer.answer == "Yes" || answer.answer == "No" ? answer.answer : answer.answer.split("_")[1]
                _str_tag += "</p></div><div class='col-md-12'><label>توضیحات : </label><p style='display: inline-block;margin-right: 20px;'>" + answer.description;
                _str_tag += "</p></div><div class='col-md-12'><label>امتیاز سیستم : </label><p style='display: inline-block;margin-right: 20px;'>" + dataFormQuestionScore;
                _str_tag += "</p></div><div class='col-md-12'><label>امتیاز کارشناس</label>";
                _str_tag += "<p style='display: inline-block;margin-right: 20px;'><input class='form-control' name='AnalizeScore_" + answer.answerId + "' ";
                _str_tag += "type='number' max='" + QuestionData[i].score + "' value='" + report.analizeScore + "' min='0'></p></div>";
                _str_tag += "<div class='col-md-12'><label>توضیحات کارشناس</label><p style='display: inline-block;margin-right: 20px;'>";
                _str_tag += "<input type='text' name='descriptoin_" + report.dataReportId + "' value='ندارد ...' onfocus='select();'></p>";
                _str_tag += "<div class='col-md-12'><a class='btn btn-warning' onclick='Web.CorporateSuperVisor.ReturnToCustomer(";
                _str_tag += QuestionData[i].dataFormQuestionId + ");'>بازگشت به مشتری برای اصلاح</a></div></div>"
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
                    li_option += "<li class='active'><a href='#FormDetailTab" + DataFormList[i].formCode + "' data-toggle='tab' aria-expanded='false' >" + DataFormList[i].formCode + "</a></li>";
                    tabPane += makeTabPane(DataFormList[i].formCode, DataFormList[i].formTitle, DataFormList[i].formId, ID, is_first)
                    is_first = false;
                }
                else {
                    tabPane += makeTabPane(DataFormList[i].formCode, DataFormList[i].formTitle, DataFormList[i].formId, ID, is_first)
                    li_option += "<li class=''><a href='#FormDetailTab" + DataFormList[i].formCode + "' data-toggle='tab' aria-expanded='false' >" + DataFormList[i].formCode + "</a></li>";
                }

            }
        }
        $("#" + PutPlace).append(li_option);
        $("#" + PutTabPane).append(tabPane);
        for (let i = 0; i < DataFormList.length; i++) {
            if (DataFormList[i].formCode.slice(0, 1) === SubCategoryName) {
                intiForm(DataFormList[i].formId, ID);
            }
        }
    }
    function makeDynamicDocumentForm(PutPlace, PutTabPane) {
        var DataFormDocumentList = [];
        let ID = $("#RequestIdForms").val();
        AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataFormDocuments", JSON.stringify({ PageIndex: 0, PageSize: 0, IsActive: 15 }), false, function (res) {
            if (res.isSuccess) {
                DataFormDocumentList = res.data;
            }
        }, true);
        let li_option = "";
        let tabPane = "";
        li_option += "<li class='active'><a href='#FormDocumentTabDocument287' data-toggle='tab' aria-expanded='true' >حقوق و رفتار عادلانه با سهامدار</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument288' data-toggle='tab' aria-expanded='false' >نقش ذینفعان</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument289' data-toggle='tab' aria-expanded='false' >افشاء و شفافیت</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument290' data-toggle='tab' aria-expanded='false' >مسئولیت های هیئت مدیره</a></li>";

        $("#" + PutPlace).append(li_option);

        tabPane += makeDocumentTabPane("Document287", "حقوق و رفتار عادلانه با سهامدار", ID, true);
        tabPane += makeDocumentTabPane("Document288", "نقش ذینفعان", ID, false);
        tabPane += makeDocumentTabPane("Document289", "افشاء و شفافیت", ID, false);
        tabPane += makeDocumentTabPane("Document290", "مسئولیت های هیئت مدیره", ID, false);

        $("#" + PutTabPane).append(tabPane);
        let _str287 = "";
        let _str288 = "";
        let _str289 = "";
        let _str290 = "";
        for (let i = 0; i < DataFormDocumentList.length; i++) {
            let title = DataFormDocumentList[i].title;
            let formId = "Doc" + DataFormDocumentList[i].dataFormDocumentId;
            let helpText = DataFormDocumentList[i].helpText;
            switch (DataFormDocumentList[i].categoryId) {
                case 287:
                    _str287 += makeFileInput(title, formId, helpText);
                    break;
                case 288:
                    _str288 += makeFileInput(title, formId, helpText);
                    break;
                case 289:
                    _str289 += makeFileInput(title, formId, helpText);
                    break;
                case 290:
                    _str290 += makeFileInput(title, formId, helpText);
                    break;
                default:
                    break;
            }
        }
        $("#FormDocumentDocument287").append(_str287);
        $("#FormDocumentDocument288").append(_str288);
        $("#FormDocumentDocument289").append(_str289);
        $("#FormDocumentDocument290").append(_str290);

        AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataFromAnswersDocuments", JSON.stringify({
            PageIndex: 0, PageSize: 0, FormID: null, RequestId: ID, DataFormQuestionId: null
        }), false, function (res) {
            if (res.isSuccess) {
                LoadedDataFromDb = res.data;
                for (let i = 0; i < LoadedDataFromDb.length; i++) {
                    $("#Download_" + LoadedDataFromDb[i].dataFormDocumentId).prop("href", LoadedDataFromDb[i].fileName1Full);
                }
            }
        }, true);

    }
    function makeFileInput(inputTitle, inputName, helpText) {
        let _str = "";
        _str += "<div class='form-group'><div class='col-md-12' style='margin-bottom:10px'><label class='control-label'>" + inputTitle;
        _str += " <span title='" + helpText + "'><i class='fa'></i></span></label>";
        _str += "<a class='btn btn-success' style='margin-right: 10px;' target='_blank' id='Download_" + inputName.slice(3, inputName.length) + "'>";
        _str += "<i class='fa fa-download'></i> &nbsp;دانلود</a></div>";
        _str += "<div class='col-md-12'><label>توضیحات کارشناس</label>";
        _str += "<input style='width: 100%;margin: 10px;' type='text' name='descriptoinDoc_" + inputName.slice(3, inputName.length) + "' value='ندارد ...' onfocus='select();'>";
        _str += '<a style="margin: 10px;" class="btn btn-warning" onclick="Web.CorporateSuperVisor.ReturnToCustomerDoc(' + inputName.slice(3, inputName.length) + ');"';
        _str += ">بازگشت به مشتری برای اصلاح</a></div>";
        return _str;
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
        strM += "<a class='btn btn-success' style='height: 35px;' onclick='Web.CorporateSuperVisor.SaveSerializedForm(" + FormId + ");'>ذخیره تغییرات " + FormCode + "</a></div>";
        strM += "<div style=' border: 2px solid #00c0ef; padding: 30px; border-radius: 5px; margin-bottom: 20px'><form id='frmFrom";
        strM += FormId + "' class='changeData'>";
        strM += "<input type='hidden' id='FormID' name='FormID' value='" + FormId + "' />";
        strM += "<input type='hidden' id='RequestId' name='RequestId' value='" + RequestId + "' />";
        strM += "<div class='row' id='FormDetail" + FormId + "'></div></form></div></div>";
        return strM
    }
    function makeDocumentTabPane(FormCode, FormTitle, RequestId, FirstItemActive = true) {
        let is_first = FirstItemActive;
        let strM = "";
        if (is_first) {
            strM = "<div class='tab-pane active' id='FormDocumentTab" + FormCode + "'>";
        }
        else {
            strM = "<div class='tab-pane' id='FormDocumentTab" + FormCode + "'>";
        }
        strM += "<div style='display:flex;justify-content: space-between;align-items: center;'>";
        strM += "<h2 class='fs-title'>" + FormTitle + "</h2></div>";
        strM += "<div style=' border: 2px solid #00c0ef; padding: 30px; border-radius: 5px; margin-bottom: 20px'><form id='frmFrom";
        strM += FormCode + "' class='changeData'>";
        strM += "<input type='hidden' id='RequestId' name='RequestId' value='" + RequestId + "' />";
        strM += "<div class='row' id='FormDocument" + FormCode + "'></div></form></div></div>";
        return strM
    }
    function save_AnswersAnalize(el, FormName) {
        let requestId = $("#RequestIdForms").val();
        $(el).attr("disabled", "");
        let fileInp = document.getElementById("Inp" + FormName.slice(6, FormName.length))
        if (fileInp.files.length != 0) {
            AjaxCallActionPostSaveFormWithUploadFile("/api/superVisor/Corporate/Save_DataFromAnswersUpload", fill_AjaxCallActionPostSaveFormWithUploadFile(FormName), true, function (res) {
                $(el).removeAttr("disabled");
                if (res.isSuccess) {
                    if (res.dataId != 0)
                        // ذخیره سوال در جدول آنالیز نمره
                        AjaxCallAction("POST", "/api/customer/Corporate/Save_DataFromReport", JSON.stringify({
                            RequestId: requestId,
                            DataFormAnswerId: res.dataId,
                            SystemScore: 0,
                            AnalizeScore: 0,
                            IsActive: 15,
                        }), true, function (reee) { }, true);
                    alertB("ثبت", "اطلاعات ثبت شد", "success");
                }
                else {
                    alertB("خطا", "ذخیره موفقیت آمیز نبود مجددا تلاش فرماید", "error");
                }
            }, true);
        }
        else {
            alertB("خطا", "ابتدا فایل را انتخاب نمایید!", "error");
            $(el).removeAttr("disabled");
        }
        return false;
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
        MakeDynamicDocumentForm: makeDynamicDocumentForm,
        SaveSerializedForm: saveSerializedForm,
        Save_AnswersAnalize: save_AnswersAnalize,
    }; 

})(Web, jQuery);