(function (web, $) {
    var DataFormList = "";
    //Document Ready   
    function initCorporate(id = null, makeQuestionForm = true) {
        PersianDatePicker(".DatePicker");
        $("#RequestIdForms").val(id);
        initCustomer();

        if (makeQuestionForm) {
            makeDynamicForm("A", "TargetTabs287", true, "TabPaneTargetTabs287");

            makeDynamicForm("B", "TargetTabs287", false, "TabPaneTargetTabs287");

            makeDynamicForm("C", "TargetTabs288", true, "TabPaneTargetTabs288");

            makeDynamicForm("D", "TargetTabs289", true, "TabPaneTargetTabs289");

            makeDynamicForm("E", "TargetTabs290", true, "TabPaneTargetTabs290");

            makeDynamicDocumentForm("ducument_save", "document_save_pane");
        } else {
            var checkReport = "";

            var cat287li = ""
            var cat288li = ""
            var cat289li = ""
            var cat290li = ""

            var cat287Pan = ""
            var cat288Pan = ""
            var cat289Pan = ""
            var cat290Pan = ""

            var cat1287Doc = ""
            var cat1288Doc = ""
            var cat1289Doc = ""
            var cat1290Doc = ""

            var fisrtInGroup287 = true;
            var fisrtInGroup288 = true;
            var fisrtInGroup289 = true;
            var fisrtInGroup290 = true;

            makeDocLiAndPan("ducument_save", "document_save_pane");

            AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormReportChecks/", JSON.stringify({
                PageIndex: 0, PageSize: 0, IsActive: 15
            }), false, function (res) {
                if (res != null) {
                    checkReport = res;
                    for (let i = 0; i < res.data.length; i++) {
                        let dataForm = "";
                        let question = "";
                        AjaxCallAction("GET", "/api/customer/Corporate/Get_DataForm/" + res.data[i].formId, null, false, function (form) { dataForm = form; }, false);
                        AjaxCallAction("GET", "/api/customer/Corporate/Get_DataFormQuestions/" + res.data[i].questionId, null, false, function (form) { question = form; }, false);

                        switch (res.data[i].categoryId) {
                            case 287:
                                if (res.data[i].questionId != 0 && res.data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(res.data[i].formCode, dataForm.formTitle, res.data[i].formId, id, fisrtInGroup287);
                                    cat287li += tempresult[0];
                                    cat287Pan += tempresult[1];
                                } else {

                                }
                                break;
                            case 288:
                                if (res.data[i].questionId != 0 && res.data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(res.data[i].formCode, dataForm.formTitle, res.data[i].formId, id, fisrtInGroup288);
                                    cat288li += tempresult[0];
                                    cat288Pan += tempresult[1];
                                } else {

                                }
                                break;
                            case 289:
                                if (res.data[i].questionId != 0 && res.data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(res.data[i].formCode, dataForm.formTitle, res.data[i].formId, id, fisrtInGroup289);
                                    cat289li += tempresult[0];
                                    cat289Pan += tempresult[1];
                                } else {

                                }
                                break;
                            case 290:
                                if (res.data[i].questionId != 0 && res.data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(res.data[i].formCode, dataForm.formTitle, res.data[i].formId, id, fisrtInGroup290);
                                    cat290li += tempresult[0];
                                    cat290Pan += tempresult[1];
                                } else {

                                }
                                break;
                        }
                    }
                    $("#TargetTabs287").append(cat287li)
                    $("#TabPaneTargetTabs287").append(cat287Pan)

                    $("#TargetTabs288").append(cat288li)
                    $("#TabPaneTargetTabs288").append(cat288Pan)

                    $("#TargetTabs289").append(cat289li)
                    $("#TabPaneTargetTabs289").append(cat289Pan)

                    $("#TargetTabs290").append(cat290li)
                    $("#TabPaneTargetTabs290").append(cat290Pan)
                }
            }, true);

            for (let i = 0; i < checkReport.data.length; i++) {
                let question = "";
                let answer = ""
                AjaxCallAction("GET", "/api/customer/Corporate/Get_DataFormQuestions/" + checkReport.data[i].questionId, null, false, function (form) { question = form; }, false);
                AjaxCallAction("GET", "/api/customer/Corporate/Get_DataFromAnswers/" + checkReport.data[i].answerId, null, false, function (form) { answer = form; }, false);

                if (checkReport.data[i].questionId != 0 && checkReport.data[i].formId != 0) {
                    let strFormId = generate_strFormId(question, id, checkReport.data[i].formId, true);
                    $("#FormDetail" + checkReport.data[i].formId).append(strFormId);
                    if (answer.answer == "Yes") {
                        $("input:radio[name='Q_" + answer.dataFormQuestionId + "'][value='Yes']").prop('checked', true);
                    } else if (answer.answer == "No") {
                        $("input:radio[name='Q_" + answer.dataFormQuestionId + "'][value='No']").prop('checked', true);
                    } else {
                        $("#Q_" + answer.dataFormQuestionId + " option[value='" + answer.answer + "']").prop("selected", true);
                    }
                    $("input[name*='Description_Q" + answer.dataFormQuestionId + "']").val(answer.description);
                } else {

                }
            }

        }
    }

    function makeLiAndPan(formCode, formTitle, formId, reqid, isActive) {
        let li_option = "";
        let tabPane = "";
        if (isActive) {
            li_option = "<li class='active'><a href='#FormDetailTab" + formCode + "' data-toggle='tab' aria-expanded='false' >" + formCode + "</a></li>";
            tabPane = makeTabPane(formCode, formTitle, formId, reqid, isActive)
            isActive = false;
        }
        else {
            tabPane = makeTabPane(formCode, formTitle, formId, reqid, isActive)
            li_option = "<li class=''><a href='#FormDetailTab" + formCode + "' data-toggle='tab' aria-expanded='false' >" + formCode + "</a></li>";
        }
        return [li_option, tabPane];
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
    function generate_strFormId(res, RequestId, FormId, isSingle=false) {
        let strFormId = "";
        if (isSingle) {
            strFormId += "<div class='form-group'><div class='col-md-12'><h4 style='line-height: 1.5;'>" + res.questionText
            strFormId += " <span title='" + res.helpText + "'><i class='fa'></i></span></h4></div><div class='col-md-12'><div class='row'><div class='col-md-4'>";
            if (res.questionType == 'select') {
                var options = combo(res.dataFormQuestionId);
                strFormId += "<select required name='Q_" + res.dataFormQuestionId + "' id='Q_" + res.dataFormQuestionId + "' class='form-control' style='padding: 0px 15px;' >" + options + "</select>";
            }
            else if (res.questionType == 'yesNo') {
                strFormId += "<label class='control-label'>بله</label><input type='radio' required name='Q_" + res.dataFormQuestionId + "' value='Yes' />";
                strFormId += "<label class='control-label'>خیر</label><input type='radio' required name='Q_" + res.dataFormQuestionId + "' value='No' />";
            }
            strFormId += "</div><div class='col-md-8'>";
            strFormId += "<input placeholder='توضیحات' class='form-control' name='Description_Q" + res.dataFormQuestionId + "' onfocus='select();' type='text' value='توضیحات را وارد کنید...' />"
            strFormId += "</div ></div ></div ></div >";
            
            return strFormId;
        } else {
            strFormId = "<div class='col-md-12'>";
            for (var i = 0; i < res.data.length; i++) {
                strFormId += "<div class='form-group'><div class='col-md-12'><h4 style='line-height: 1.5;'>" + res.data[i].questionText
                strFormId += " <span title='" + res.data[i].helpText + "'><i class='fa'></i></span></h4></div><div class='col-md-12'><div class='row'><div class='col-md-4'>";
                if (res.data[i].questionType == 'select') {
                    var options = combo(res.data[i].dataFormQuestionId);
                    strFormId += "<select required name='Q_" + res.data[i].dataFormQuestionId + "' id='Q_" + res.data[i].dataFormQuestionId + "' class='form-control' style='padding: 0px 15px;' >" + options + "</select>";
                }
                else if (res.data[i].questionType == 'yesNo') {
                    strFormId += "<label class='control-label'>بله</label><input type='radio' required name='Q_" + res.data[i].dataFormQuestionId + "' value='Yes' />";
                    strFormId += "<label class='control-label'>خیر</label><input type='radio' required name='Q_" + res.data[i].dataFormQuestionId + "' value='No' />";
                }
                strFormId += "</div><div class='col-md-8'>";
                strFormId += "<input placeholder='توضیحات' class='form-control' name='Description_Q" + res.data[i].dataFormQuestionId + "' onfocus='select();' type='text' value='توضیحات را وارد کنید...' />"
                strFormId += "</div ></div ></div ></div >";
            }
            strFormId += "</div>";
        }
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
            AjaxCallAction("POST", "/api/customer/Corporate/Get_DataForms", JSON.stringify({ PageIndex: 0, PageSize: 0, DataFormType: 2 }), false, function (res) {
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
    function makeDocLiAndPan(putPlace, putTabPan) {
        let ID = $("#RequestIdForms").val();
        let li_option = "";
        let tabPane = "";
        li_option += "<li class='active'><a href='#FormDocumentTabDocument287' data-toggle='tab' aria-expanded='true' >حقوق و رفتار عادلانه با سهامدار</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument288' data-toggle='tab' aria-expanded='false' >نقش ذینفعان</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument289' data-toggle='tab' aria-expanded='false' >افشاء و شفافیت</a></li>";
        li_option += "<li class=''><a href='#FormDocumentTabDocument290' data-toggle='tab' aria-expanded='false' >مسئولیت های هیئت مدیره</a></li>";

        $("#" + putPlace).append(li_option);

        tabPane += makeDocumentTabPane("Document287", "حقوق و رفتار عادلانه با سهامدار", ID, true);
        tabPane += makeDocumentTabPane("Document288", "نقش ذینفعان", ID, false);
        tabPane += makeDocumentTabPane("Document289", "افشاء و شفافیت", ID, false);
        tabPane += makeDocumentTabPane("Document290", "مسئولیت های هیئت مدیره", ID, false);

        $("#" + putTabPan).append(tabPane);
    }
    function makeDynamicDocumentForm(PutPlace, PutTabPane) {
        makeDocLiAndPan(PutPlace, PutTabPane);

        var DataFormDocumentList = [];
        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormDocuments", JSON.stringify({ PageIndex: 0, PageSize: 0, IsActive: 15 }), false, function (res) {
            if (res.isSuccess) {
                DataFormDocumentList = res.data;
            }
        }, true);

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
                    _str287 += makeFileInput(title, formId, helpText, ID);
                    break;
                case 288:
                    _str288 += makeFileInput(title, formId, helpText, ID);
                    break;
                case 289:
                    _str289 += makeFileInput(title, formId, helpText, ID);
                    break;
                case 290:
                    _str290 += makeFileInput(title, formId, helpText, ID);
                    break;
                default:
                    break;
            }
        }
        $("#FormDocumentDocument287").append(_str287);
        $("#FormDocumentDocument288").append(_str288);
        $("#FormDocumentDocument289").append(_str289);
        $("#FormDocumentDocument290").append(_str290);

        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFromAnswersDocuments", JSON.stringify({
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
    function makeFileInput(inputTitle, inputName, helpText, RequestId) {
        let _str = "<form id='frmDoc" + inputName + "'>";
        _str += "<div class='form-group'><div class='col-md-12' style='margin-bottom:10px'><label class='control-label'>" + inputTitle;
        _str += " <span title='" + helpText + "'><i class='fa'></i></span></label>";
        _str += "<input type='file' name='Result_Final_FileName1' accept='image/*,.pdf,.xlsx,' id='Inp" + inputName + "'";
        _str += 'onchange="checkUploadWithFileSiza(this, \'' + inputTitle + '\' , 5);">';
        _str += '<input type="submit" value="ذخیره سازی ' + inputTitle + '" class="btn btn-success" ';
        _str += 'onclick="return Web.Corporate.Save_AnswersUpload(this, \'frmDoc' + inputName + '\')">';
        _str += "<a class='btn btn-success' style='margin-right: 10px;' target='_blank' id='Download_" + inputName.slice(3, inputName.length) + "'><i class='fa fa-download'></i> &nbsp;دانلود</a></div></div>";
        _str += "<input type='hidden' id='FormId' name='FormId' value='0' />";
        _str += "<input type='hidden' id='RequestId' name='RequestId' value='" + RequestId + "' />";
        _str += "<input type='hidden' id='DataFormQuestionId' name='DataFormQuestionId' value='0' />";
        _str += "<input type='hidden' id='DataFormDocumentId' name='DataFormDocumentId' value='" + inputName.slice(3, inputName.length) + "' />";
        _str += "<input type='hidden' id='IsActive' name='IsActive' value='15' /></form>";
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
        strM += "<a class='btn btn-success' style='height: 35px;' onclick='Web.Corporate.SaveSerializedForm(" + FormId + ");'>ذخیره تغییرات " + FormCode + "</a></div>";
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

    function save_AnswersUpload(el, FormName) {
        let requestId = $("#RequestIdForms").val();
        $(el).attr("disabled", "");
        let fileInp = document.getElementById("Inp" + FormName.slice(6, FormName.length))
        if (fileInp.files.length != 0) {
            AjaxCallActionPostSaveFormWithUploadFile("/api/customer/Corporate/Save_DataFromAnswersUpload", fill_AjaxCallActionPostSaveFormWithUploadFile(FormName), true, function (res) {
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
                    $("#Download_" + FormName.slice(6, FormName.length)).prop("href", res.data.fileName1Full);
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
    function saveSingelAnswerForm(formId = "0", answer = "0", description = "", dataFormQuestionId = "0", fileName = "") {
        var requestId = $("#RequestId").val();
        var dataFormQuestionScore = 0;
        AjaxCallAction("POST", "/api/customer/Corporate/Save_DataFromAnswers", JSON.stringify({
            Answer: answer,
            Description: description,
            FormId: parseInt(formId),
            RequestId: parseInt(requestId),
            DataFormQuestionId: parseInt(dataFormQuestionId),
            FileName1: fileName,
            IsActive: 15,
        }), false, function (res) {
            if (res.isSuccess) {
                if (dataFormQuestionId != 0 || dataFormQuestionId != "0") {
                    // سوال رو دریافت کردم تا نمره مربوط به اون رو داشته باشم
                    AjaxCallAction("GET", "/api/customer/Corporate/Get_DataFormQuestions/" + dataFormQuestionId, null, false, function (res1) {
                        if (!isEmpty(res1)) {
                            dataFormQuestionScore = res1.score;
                            if (res1.questionType == 'select') {
                                AjaxCallAction("GET", "/api/customer/Corporate/Get_Option/" + answer.split("_")[0], null, false, function (datares) {
                                    if (!isEmpty(datares)) {
                                        dataFormQuestionScore = dataFormQuestionScore * datares.ratio;
                                    }
                                }, true);
                            } else if (res1.questionType == 'yesNo') {
                                if (answer == "No") {
                                    dataFormQuestionScore = 0;
                                }
                            }

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
                }
                if (res.dataId != 0)
                    // ذخیره سوال در جدول آنالیز نمره
                    AjaxCallAction("POST", "/api/customer/Corporate/Save_DataFromReport", JSON.stringify({
                        RequestId: requestId,
                        DataFormAnswerId: res.dataId,
                        SystemScore: 0,
                        AnalizeScore: 0,
                        IsActive: 15,
                    }), true, function (reee) { }, true);

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
        MakeDynamicDocumentForm: makeDynamicDocumentForm,
        SaveSerializedForm: saveSerializedForm,
        Save_AnswersUpload: save_AnswersUpload,
    };

})(Web, jQuery);