(function (web, $) {
    var DataFormList = "";
    var progresDynamicBar = []
    //Document Ready

    function makeLiProgresBar(progresDynamicBar) {
        let _strM = "";
        for (let i = 0; i < progresDynamicBar.length; i++) {
            if (i == 0)
                _strM += "<li class='active' id='payment'><strong>" + progresDynamicBar[i].label + "</strong></li>";
            else {
                _strM += "<li class='' id='payment'><strong>" + progresDynamicBar[i].label + "</strong></li>";
            }
        }
        _strM += "<li id='confirm'><strong>بارگذاری مدارک</strong></li>";
        return _strM;
    }

    function makeFieldset(fieldId, isFirst = true) {
        let str_M = "";
        if (isFirst) {
            str_M += "<fieldset><button type='button' name='next' class='next action-button' style='border:none;width:90px'>فرم بعدی <i class='fa fa-arrow-left'></i></button>";
            str_M += "<div class='form-card'><div class='row'><div class='col-md-12'><div class='nav-tabs-custom'><ul class='nav nav-tabs' id='TargetTabs" + fieldId + "'></ul>";
            str_M += "<div class='tab-content' id='TabPaneTargetTabs" + fieldId + "'></div></div></div></div></div>";
            str_M += "<button type='button' name='next' class='next action-button' style='border:none;width:90px'>فرم بعدی <i class='fa fa-arrow-left'></i></button></fieldset>";
        } else {
            str_M += "<fieldset><button type='button' name='previous' class='previous action-button-previous' style='border:none;width:90px'><i class='fa fa-arrow-right'></i> فرم قبلی </button>";
            str_M += "<button type='button' name='next' class='next action-button' style='border:none;width:90px'>فرم بعدی <i class='fa fa-arrow-left'></i></button>";
            str_M += "<div class='form-card'><div class='row'><div class='col-md-12'><div class='nav-tabs-custom'><ul class='nav nav-tabs' id='TargetTabs" + fieldId + "'></ul>";
            str_M += "<div class='tab-content' id='TabPaneTargetTabs" + fieldId + "'></div></div></div></div></div>";
            str_M += "<button type='button' name='previous' class='previous action-button-previous' style='border:none;width:90px'><i class='fa fa-arrow-right'></i> فرم قبلی </button>";
            str_M += "<button type='button' name='next' class='next action-button' style='border:none;width:90px'>فرم بعدی <i class='fa fa-arrow-left'></i></button></fieldset>";
        }
        return str_M;
    }

    function makeTabProgresDynamic() {
        if (progresDynamicBar.length == 0)
            AjaxCallAction("POST", "/api/superVisor/SystemSeting/Get_SystemSetings/", JSON.stringify({
                PageIndex: 0, PageSize: 0, ParentCode: 286, SortOrder: "SystemSetingId_A",
            }), false, function (result) { progresDynamicBar = result.data; }, false);
        let _strM = "";
        if (progresDynamicBar.length > 1)
            for (let i = 0; i < progresDynamicBar.length; i++) {
                if (i == 0)
                    _strM += makeFieldset(progresDynamicBar[i].systemSetingId, true);
                else {
                    _strM += makeFieldset(progresDynamicBar[i].systemSetingId, false);
                }
            }
        let _strLi = makeLiProgresBar(progresDynamicBar);
        $("#progressbar").html(_strLi);
        $("#progressbar").after(_strM);
        $(".next").click(function () {

            current_fs = $(this).parent();
            next_fs = $(this).parent().next();

            //Add Class Active
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

            //show the next fieldset
            next_fs.show();
            //hide the current fieldset with style
            current_fs.animate({ opacity: 0 }, {
                step: function (now) {
                    // for making fielset appear animation
                    opacity = 1 - now;

                    current_fs.css({
                        'display': 'none',
                        'position': 'relative'
                    });
                    next_fs.css({ 'opacity': opacity });
                },
                duration: 600
            });
        });

        $(".previous").click(function () {
            current_fs = $(this).parent();
            previous_fs = $(this).parent().prev();

            //Remove class active
            $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

            //show the previous fieldset
            previous_fs.show();

            //hide the current fieldset with style
            current_fs.animate({ opacity: 0 }, {
                step: function (now) {
                    // for making fielset appear animation
                    opacity = 1 - now;

                    current_fs.css({
                        'display': 'none',
                        'position': 'relative'
                    });
                    previous_fs.css({ 'opacity': opacity });
                },
                duration: 600
            });
        });
    }
    function initCorporate(id = null, makeQuestionForm = true) {
        PersianDatePicker(".DatePicker");
        $("#RequestIdForms").val(id);
        getCustomerInfo(id, dir = 'rtl');
        makeTabProgresDynamic();
        if (makeQuestionForm) {
            makeDynamicForm("A", "TargetTabs287", true, "TabPaneTargetTabs287");

            makeDynamicForm("D", "TargetTabs288", true, "TabPaneTargetTabs288");

            makeDynamicForm("E", "TargetTabs289", true, "TabPaneTargetTabs289");

            makeDynamicForm("B", "TargetTabs290", true, "TabPaneTargetTabs290");

            makeDynamicForm("C", "TargetTabs291", true, "TabPaneTargetTabs291");

            makeDynamicForm("F", "TargetTabs292", true, "TabPaneTargetTabs292");

            makeDynamicForm("G", "TargetTabs293", true, "TabPaneTargetTabs293");

            makeDynamicForm("H", "TargetTabs294", true, "TabPaneTargetTabs294");

            makeDynamicDocumentForm("ducument_save", "document_save_pane");
        }
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
        var questionIsNotInReturnCustomer = false;

        AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataFromAnswerss", JSON.stringify({
            PageIndex: 0, PageSize: 0, FormID: FormID, RequestId: RequestId
        }), false, function (res) {
            if (res.isSuccess) {
                _DataAnswer = res.data;
            }
        }, true);
        for (let i = 0; QuestionData.length > i; i++) {
            // بررسی اینکه آیا سوال به مشتری بازگردانده شده است یا خیر
            try {
                AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataFormReportCheck", JSON.stringify({
                    FormID: FormID, RequestId: RequestId, QuestionId: QuestionData[i].dataFormQuestionId
                }), false, function (res) {
                    if (res.isActive == 15 && res.requestId != 0 && res.checkId != 0) {
                        questionIsNotInReturnCustomer = true;
                    }
                }, true);
            } catch {

            }

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
            var formDate = {
                "formCode": "0",
                "categoryId": "0",
            }
            AjaxCallAction("GET", "/api/superVisor/Corporate/Get_DataForm/" + FormID, null, false, function (res) {
                if (res != null) {
                    formDate = res;
                }
            }, true);

            if (!isEmpty(report)) {
                if (questionIsNotInReturnCustomer) {
                    _str_tag += "<div class='form-group' style='border: 2px solid red;'><div class='col-md-12'><h4 style='line-height: 1.5;'>";
                    _str_tag += QuestionData[i].questionText + " <span title='" + QuestionData[i].helpText + "'><i class='fa'></i></span></h4></div><div class='col-md-12' style='border: 2px solid red;'>"
                }
                else {
                    _str_tag += "<div class='form-group'><div class='col-md-12'><h4 style='line-height: 1.5;'>";
                    _str_tag += QuestionData[i].questionText + " <span title='" + QuestionData[i].helpText + "'><i class='fa'></i></span></h4></div><div class='col-md-12'>"
                }
                _str_tag += "<div class='row'><div class='col-md-12'>";
                _str_tag += "<label>پاسخ مشتری : </label><p style='display: inline-block;margin-right: 20px;'>";
                _str_tag += answer.answer == "Yes" || answer.answer == "No" ? answer.answer : answer.answer.split("_")[1]
                _str_tag += "</p></div><div class='col-md-12'><label>توضیحات : </label><p style='display: inline-block;margin-right: 20px;'>" + answer.description;
                _str_tag += "</p></div><div class='col-md-12'><label>امتیاز سیستم : </label><p style='display: inline-block;margin-right: 20px;'>" + dataFormQuestionScore;
                _str_tag += "</p></div><div class='col-md-12'><label>امتیاز کارشناس</label>";
                _str_tag += "<p style='display: inline-block;margin-right: 20px;'><input class='form-control' name='AnalizeScore_" + answer.answerId + "' ";
                _str_tag += "type='number' max='" + QuestionData[i].score + "' value='" + report.analizeScore + "' min='0'></p></div>";
                _str_tag += "<div class='col-md-12'><label>توضیحات کارشناس</label><p style='display: inline-block;margin-right: 20px;'>";
                _str_tag += "<input type='text' name='descriptoin_" + report.dataReportId + "' id='descriptoin_" + report.dataReportId + "' value='ندارد ...' onfocus='select();'></p>";
                _str_tag += '<div class="col-md-12"><a class="btn btn-warning" onclick="return Web.CorporateSuperVisor.ReturnToCustomer(this,';
                _str_tag += QuestionData[i].dataFormQuestionId + "," + answer.answerId + "," + FormID + "," + RequestId + "," + formDate.categoryId + ", '";
                _str_tag += formDate.formCode + "', '" + QuestionData[i].questionType + "', '" + answer.answer + "'," + "''" + "," + dataFormQuestionScore + ",";
                _str_tag += report.dataReportId + ", '" + answer.description + "'," + "''";
                _str_tag += ');">بازگشت به مشتری برای اصلاح</a></div></div>'
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
        li_option += "<li class='active'><a href='#FormDocumentTabDocument287' data-toggle='tab' aria-expanded='true' >سهامداران</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument288' data-toggle='tab' aria-expanded='false' >نقش ذینفعان</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument289' data-toggle='tab' aria-expanded='false' >افشاء و شفافیت</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument290' data-toggle='tab' aria-expanded='false' >هیئت مدیره</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument291' data-toggle='tab' aria-expanded='false' >اطلاعات نهایی و اشخاص وابسته</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument292' data-toggle='tab' aria-expanded='false' >صورت های مالی و حسابرسی</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument293' data-toggle='tab' aria-expanded='false' >کمیته ها</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument294' data-toggle='tab' aria-expanded='false' >سایر</a></li>";

        $("#" + PutPlace).append(li_option);

        tabPane += makeDocumentTabPane("Document287","سهامداران",ID,true);
        tabPane += makeDocumentTabPane("Document288", "نقش ذینفعان", ID, false);
        tabPane += makeDocumentTabPane("Document289", "افشاء و شفافیت", ID, false);
        tabPane += makeDocumentTabPane("Document290", "هیئت مدیره", ID, false);
        tabPane += makeDocumentTabPane("Document291", "اطلاعات نهایی و اشخاص وابسته", ID, false);
        tabPane += makeDocumentTabPane("Document292", "صورت های مالی و حسابرسی", ID, false);
        tabPane += makeDocumentTabPane("Document293", "کمیته ها", ID, false);
        tabPane += makeDocumentTabPane("Document294", "سایر", ID, false);

        $("#" + PutTabPane).append(tabPane);
        let _str287 = "";
        let _str288 = "";
        let _str289 = "";
        let _str290 = "";
        let _str291 = "";
        let _str292 = "";
        let _str293 = "";
        let _str294 = "";
        for (let i = 0; i < DataFormDocumentList.length; i++) {
            let title = DataFormDocumentList[i].title;
            let formId = "Doc" + DataFormDocumentList[i].dataFormDocumentId;
            let helpText = DataFormDocumentList[i].helpText;
            let categoryId = DataFormDocumentList[i].categoryId;
            switch (DataFormDocumentList[i].categoryId) {
                case 287:
                    _str287 += makeFileInput(title, formId, helpText, categoryId, DataFormDocumentList[i].dataFormDocumentId);
                    break;
                case 288:
                    _str288 += makeFileInput(title, formId, helpText, categoryId, DataFormDocumentList[i].dataFormDocumentId);
                    break;
                case 289:
                    _str289 += makeFileInput(title, formId, helpText, categoryId, DataFormDocumentList[i].dataFormDocumentId);
                    break;
                case 290:
                    _str290 += makeFileInput(title, formId, helpText, categoryId, DataFormDocumentList[i].dataFormDocumentId);
                    break;
                case 291:
                    _str291 += makeFileInput(title, formId, helpText, categoryId, DataFormDocumentList[i].dataFormDocumentId);
                    break;
                case 292:
                    _str292 += makeFileInput(title, formId, helpText, categoryId, DataFormDocumentList[i].dataFormDocumentId);
                    break;
                case 293:
                    _str293 += makeFileInput(title, formId, helpText, categoryId, DataFormDocumentList[i].dataFormDocumentId);
                    break;
                case 294:
                    _str294 += makeFileInput(title, formId, helpText, categoryId, DataFormDocumentList[i].dataFormDocumentId);
                    break;
                default:
                    break;
            }
        }
        $("#FormDocumentDocument287").append(_str287);
        $("#FormDocumentDocument288").append(_str288);
        $("#FormDocumentDocument289").append(_str289);
        $("#FormDocumentDocument290").append(_str290);
        $("#FormDocumentDocument291").append(_str291);
        $("#FormDocumentDocument292").append(_str292);
        $("#FormDocumentDocument293").append(_str293);
        $("#FormDocumentDocument294").append(_str294);

        AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataFromAnswersDocuments", JSON.stringify({
            PageIndex: 0, PageSize: 0, FormID: null, RequestId: ID, DataFormQuestionId: null
        }), false, function (res) {
            if (res.isSuccess) {
                LoadedDataFromDb = res.data;
                for (let i = 0; i < LoadedDataFromDb.length; i++) {
                    try {
                        $("#Download_" + LoadedDataFromDb[i].dataFormDocumentId).prop("href", LoadedDataFromDb[i].fileName1Full);
                    } catch { }

                }
            }
        }, true);

    }
    function makeFileInput(inputTitle, inputName, helpText, categoryId, documentId) {
        let RequestId = $("#RequestIdForms").val();
        var _DataAnswer;
        AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataFromAnswerss", JSON.stringify({
            PageIndex: 0, PageSize: 0, FormID: null, RequestId: RequestId
        }), false, function (res) {
            if (res.isSuccess) {
                _DataAnswer = res.data;
            }
        }, true);
        var answer = _DataAnswer.find(o => o.requestId == RequestId && o.dataFormDocumentId == documentId)
        if (isEmpty(answer)) {
            answer = {
                "description": "",
                "fileName1Full": "",
            }
        }
        var DocumentIsNotInReturnCustomer = false;
        // بررسی اینکه آیا سوال به مشتری بازگردانده شده است یا خیر
        try {
            AjaxCallAction("POST", "/api/superVisor/Corporate/Get_DataFormReportCheck", JSON.stringify({
                RequestId: RequestId, DocumentId: documentId
            }), false, function (res) {
                if (res.isActive == 15 && res.requestId != 0 && res.checkId != 0) {
                    DocumentIsNotInReturnCustomer = true;
                }
            }, true);
        } catch {

        }
        let _str = "";
        if (DocumentIsNotInReturnCustomer)
            _str += "<div class='form-group'><div class='col-md-12' style='margin-bottom:10px;border: 2px solid red;'><label class='control-label'>" + inputTitle;
        else {
            _str += "<div class='form-group'><div class='col-md-12' style='margin-bottom:10px'><label class='control-label'>" + inputTitle;
        }
        _str += " <span title='" + helpText + "'><i class='fa'></i></span></label>";
        _str += "<a class='btn btn-success' style='margin-right: 10px;' target='_blank' id='Download_" + inputName.slice(3, inputName.length) + "'>";
        _str += "<i class='fa fa-download'></i> &nbsp;دانلود</a></div>";
        if (DocumentIsNotInReturnCustomer)
            _str += "<div class='col-md-12' style='border: 2px solid red;'><label>توضیحات کارشناس</label>";
        else {
            _str += "<div class='col-md-12'><label>توضیحات کارشناس</label>";
        }
        _str += "<input style='width: 100%;margin: 10px;' type='text' id='descriptoinDoc_" + inputName.slice(3, inputName.length) + "' name='descriptoinDoc_" + inputName.slice(3, inputName.length) + "' ";
        _str += 'value="ندارد ..." onfocus="select();"><a style="margin: 10px;" class="btn btn-warning" onclick="return Web.CorporateSuperVisor.ReturnToCustomerDoc(this,';
        _str += 0 + "," + 0 + "," + 0 + "," + RequestId + "," + categoryId + ",";
        _str += documentId + "," + "''," + "''," + "''," + "'', '" + answer.fileName1Full + "'," + 0 + "," + inputName.slice(3, inputName.length) + "," + "'',";
        _str += "'', ";
        _str += ');">بازگشت به مشتری برای اصلاح</a></div></div>'
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

    function returnToCustomer(el,
        QuestionId, AnswerId, FormId, RequestId, CategoryId,
        FormCode, QuestionType, AnswerBeforEdit, AnswerAfterEdit, SystemScore,
        DataReportId, CostumerDescriptionBeforEdit, CostumerDescriptionAfterEdit
    ) {
        try {
            const p = $($(el).parent().parent().parent().parent().parent());
            confirmB("", "آیا از بازگرداند به مشتری مطمئن هستید توجه کنید امکان بازگشت وجود ندارد", 'error', function () {
                let SuperVisorDescription = $('#descriptoin_' + DataReportId).val();
                AjaxCallAction("POST", "/api/superVisor/Corporate/Save_DataFormReportCheck", JSON.stringify({
                    QuestionId: QuestionId,
                    AnswerId: AnswerId,
                    FormId: FormId,
                    RequestId: RequestId,
                    CategoryId: CategoryId,
                    FormCode: FormCode,
                    QuestionType: QuestionType,
                    AnswerBeforEdit: AnswerBeforEdit,
                    AnswerAfterEdit: AnswerAfterEdit,
                    SystemScore: SystemScore,
                    SuperVisorDescription: SuperVisorDescription,
                    CostumerDescriptionBeforEdit: CostumerDescriptionBeforEdit,
                    CostumerDescriptionAfterEdit: CostumerDescriptionAfterEdit,
                    IsActive: 15,
                }), true, function (data) { p.css('border', 'solid red 2px'); }, true);
                alertB("ثبت", "بعد از تایید نهایی سوالات انتخاب شده به مشتری باز گردانده خواهد شد", "success");
            }, function () {
            }, ["خیر", "بلی"]);
        }
        catch {
            alertB("خطا", "ذخیره موفقیت آمیز نبود مجددا تلاش فرماید", "error");
        }
        return false
    }

    function returnToCustomerDoc(el,
        QuestionId, AnswerId, FormId, RequestId, CategoryId, DocumentId,
        FormCode, QuestionType, AnswerBeforEdit, AnswerAfterEdit, _Document, SystemScore,
        DataReportId, CostumerDescriptionBeforEdit, CostumerDescriptionAfterEdit
    ) {
        try {
            const p = $($(el).parent().parent());
            confirmB("", "آیا از بازگرداند به مشتری مطمئن هستید توجه کنید امکان بازگشت وجود ندارد", 'error', function () {
                let SuperVisorDescription = $('#descriptoinDoc_' + DataReportId).val();
                AjaxCallAction("POST", "/api/superVisor/Corporate/Save_DataFormReportCheck", JSON.stringify({
                    QuestionId: QuestionId,
                    AnswerId: AnswerId,
                    FormId: FormId,
                    RequestId: RequestId,
                    CategoryId: CategoryId,
                    DocumentId: DocumentId,
                    FormCode: FormCode,
                    QuestionType: QuestionType,
                    AnswerBeforEdit: AnswerBeforEdit,
                    AnswerAfterEdit: AnswerAfterEdit,
                    _Document: _Document,
                    SystemScore: SystemScore,
                    SuperVisorDescription: SuperVisorDescription,
                    CostumerDescriptionBeforEdit: CostumerDescriptionBeforEdit,
                    CostumerDescriptionAfterEdit: CostumerDescriptionAfterEdit,
                    IsActive: 15,
                }), true, function (data) { p.css('border', 'solid red 2px'); }, true);
                alertB("ثبت", "بعد از تایید نهایی سوالات انتخاب شده به مشتری باز گردانده خواهد شد", "success");
            }, function () {
            }, ["خیر", "بلی"]);
        }
        catch {
            alertB("خطا", "ذخیره موفقیت آمیز نبود مجددا تلاش فرماید", "error");
        }
        return false
    }

    web.CorporateSuperVisor = {
        IntiForm: intiForm,
        InitCorporate: initCorporate,
        InitCustomer: initCustomer,
        MakeDynamicForm: makeDynamicForm,
        MakeDynamicDocumentForm: makeDynamicDocumentForm,
        SaveSerializedForm: saveSerializedForm,
        Save_AnswersAnalize: save_AnswersAnalize,
        ReturnToCustomer: returnToCustomer,
        ReturnToCustomerDoc: returnToCustomerDoc,
    };

})(Web, jQuery);