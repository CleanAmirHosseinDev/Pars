function checkAllDocUpload(el) {
    let errorCounter = 0
    document.querySelectorAll("#document_save_pane label span").forEach(function (it) {
        let inputElement = it.parentNode.parentNode.querySelector("input");
        let downloadBtn = it.parentNode.parentNode.querySelector("a");
        if (inputElement.files.length == 0 && downloadBtn.href == "") {
            it.style.fontSize = "15px";
            it.innerHTML = "این مورد ضرروری است!";
            errorCounter += 1;
        } else {
            it.style.fontSize = "25px";
            it.innerHTML = "*";
        }
    })
    if (errorCounter == 0) {
        let count_of_requierd_doc = 0;
        let count_of_requierd_doc_answerd = 0;
        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormDocuments", JSON.stringify({
            PageIndex: 0,
            PageSize: 0,
            IsActive: 15,
            IsRequierd: true
        }), false, function (res) {
            if (res.isSuccess) {
                count_of_requierd_doc = res.data.length;
            }
        }, false);

        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFromAnswerss", JSON.stringify({
            PageIndex: 0,
            PageSize: 0,
            IsActive: 15,
            FormId: 0,
            RequestId: $("#RequestIdForms").val(),
            DataFormQuestionId: 0,
        }), false, function (res) {
            if (res.isSuccess) {
                count_of_requierd_doc_answerd = res.data.length;
            }
        }, false);

        if (count_of_requierd_doc <= count_of_requierd_doc_answerd) {
            current_fs = $(el).parent();
            next_fs = $(el).parent().next();

            //Add Class Active
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

            //show the next fieldset
            next_fs.show();
            //hide the current fieldset with style
            current_fs.animate({
                opacity: 0,
            }, {
                step: function (now) {
                    // for making fielset appear animation
                    opacity = 1 - now;

                    current_fs.css({
                        display: "none",
                        position: "relative",
                    });
                    next_fs.css({
                        opacity: opacity,
                    });
                },
                duration: 600,
            });
            return true;
        }
        else {
            alertB("خطا", "فایل هایی که در کنار آنها علامت * می باشد اجباری هستند توجه نمایید بعد از اپلود فایل باید بر روی ذخیره کلیک نمایید تا اپلود شود", "error", "بله متوجه شدم", function () { });
            return false;
        }
        return false;
    }
    else {
        alertB("خطا", "فایل هایی که در کنار آنها علامت * می باشد اجباری هستند توجه نمایید بعد از اپلود فایل باید بر روی ذخیره کلیک نمایید تا اپلود شود", "error", "بله متوجه شدم", function () { });
    }
}

(function (web, $) {
    var userAccessSaveForm = true
    var DataFormList = "";
    var LoadedDataFromDb = "";
    var LoadedDataFromDbDocument = "";
    var progresDynamicBar = [];
    var VersionQuestion = null; // تعیین ورژن سوالات
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
            AjaxCallAction("POST", "/api/customer/SystemSeting/Get_SystemSetings/", JSON.stringify({
                PageIndex: 0,
                PageSize: 0,
                ParentCode: 286,
                SortOrder: "SystemSetingId_A",
            }), false, function (result) {
                progresDynamicBar = result.data;
            }, false);
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
            current_fs.animate({
                opacity: 0,
            }, {
                step: function (now) {
                    // for making fielset appear animation
                    opacity = 1 - now;

                    current_fs.css({
                        display: "none",
                        position: "relative",
                    });
                    next_fs.css({
                        opacity: opacity,
                    });
                },
                duration: 600,
            });
        });

        $(".previous").click(function () {
            current_fs = $(this).parent();
            previous_fs = $(this).parent().prev();

            //Remove class active
            $("#progressbar li")
                .eq($("fieldset").index(current_fs))
                .removeClass("active");

            //show the previous fieldset
            previous_fs.show();

            //hide the current fieldset with style
            current_fs.animate({
                opacity: 0,
            }, {
                step: function (now) {
                    // for making fielset appear animation
                    opacity = 1 - now;

                    current_fs.css({
                        display: "none",
                        position: "relative",
                    });
                    previous_fs.css({
                        opacity: opacity,
                    });
                },
                duration: 600,
            });
        });
    }

    function initCorporate(id = null, makeQuestionForm = true) {
        PersianDatePicker(".DatePicker");
        $("#RequestIdForms").val(id);
        initCustomer();

        makeTabProgresDynamic();
        if (makeQuestionForm) {

            AjaxCallAction("POST", "/api/customer/RequestForRating/Get_RequestForRatings",
                JSON.stringify({ PageIndex: 1, PageSize: 1, RequestId: id, KindOfRequest: 254 }), false,
                function (res) {
                    if (res.isSuccess)
                        userAccessSaveForm = (res.data[0].destLevelStepIndex <= 105) && (res.data[0].destLevelStepAccessRole == 10)
                }, false);


            if (userAccessSaveForm) {
                let strmsg = "<div class='swal-overlay swal-overlay--show-modal' tabindex='-1' id='message_startsub'>" +
                    "<div class='swal-modal' role='dialog' aria-modal='true' style='width: 1000px!important;'>" +
                    "<div class='swal-text' style=''>" +
                    "مشتری گرامی<br>با سلام و احترام<br>پیش از تکمیل و ثبت نهایی پرسشنامه ارزیابی حاکمیت شرکتی لطفا موارد ذیل دقت نمایید :<br>1 - جهت ارزیابی " + "پرسش های ثبت شده ، بارگذاری مدارک و مستندات طبق فیلدهای طراحی شده در بخش 'بارگذاری مدارک' الزامی است، لذا خواهشمند است نسبت به ارسال مدارک درخواستی بصورت خوانا و دقیق دقت فرمایید.<br>2 - فرمت فایلهای قابل قبول جهت ارسال در بخش 'بارگذاری مدارک' ، PDF, Excel, image است.<br>3 - لطفا پس از پاسخ دادن به سوالات هر فرم، دکمه 'ذخیره سازی' در آن فرم را کلیک کرده و سپس جهت پاسخ دهی به فرم بعدی بروید.<br>4 - با توجه به اینکه سوالات بصورت بلی / خیر طراحی شده است، در صورت عدم امکان پاسخ دهی دقیق به هر سوال، ضمن ارائه توضیحات کد در بخش 'توضیحات' پاسخ خود را از بین دو گزینه موجود انتخاب نمایید.<br>5 - توجه کنید در صورت مغایرت پاسخ های ارسالی با مدارک بارگذاری شده، ضمن عدم تعلق نمره مربوطه، آن سوال مجددا جهت اصلاح پاسخ یا باگذاری مدارک مربوطه بازگشت داده میشود. لذا خواهشمند است جهت تسریع در امر ارزیابی، با دقت نسبت به پاسخ دهی به سوالات و بارگذاری مدارک اقدام نمایید.<br>6 - در صورت بازگشت مجدد سوالات، جهت اصلاح، به توضیحات کارشناس ارزیابی حاکمیت شرکتی توجه فرمایید و در صورت وجود هر گونه ابهام با شماره پشتیبانی کارشناس حاکمیت شرکتی تماس حاصل فرمایید.<br> " +
                    " 7 - بارگذاری مدارک ستاره دار برای انجام ارزیابی، الزامی است. در صورت در دسترس نبودن مدرک مذکور یا عدم موضوعیت سوال مربوطه برای آن شرکت، لطفا پیام، 'مدرک درخواستی در دسترس نیست' یا 'عدم موضوعیت سوال' را در قالب یک فایل PDF برای مدارکی که ارسال آنها الزامی است، بارگذاری کنید. بدیهی است که در این صورت، امتیازی برای این سوال به شرکتی تعلق نمی گیرد.<br>" +
                    " </div> <div class='swal-footer'>" +
                    " <div class='swal-button-container'>" +
                    " <button onclick=\"$('#message_startsub').remove()\" class='swal-button swal-button--confirm'>بله متوجه شدم</button><div class='swal-button__loader'><div></div><div></div><div></div></div>" +
                    "</div></div></div></div>";
                $("#message_start").html(strmsg);
            }


            makeDynamicForm("A", "TargetTabs287", true, "TabPaneTargetTabs287");

            makeDynamicForm("B", "TargetTabs290", true, "TabPaneTargetTabs290");

            makeDynamicForm("C", "TargetTabs291", true, "TabPaneTargetTabs291");

            makeDynamicForm("D", "TargetTabs288", true, "TabPaneTargetTabs288");

            makeDynamicForm("E", "TargetTabs289", true, "TabPaneTargetTabs289");

            makeDynamicForm("F", "TargetTabs292", true, "TabPaneTargetTabs292");

            makeDynamicForm("G", "TargetTabs293", true, "TabPaneTargetTabs293");

            makeDynamicForm("H", "TargetTabs294", true, "TabPaneTargetTabs294");

            makeDynamicDocumentForm("ducument_save", "document_save_pane");

            initReferral(id, true);
        }
        else {
            var checkReport = "";

            var cat287li = "";
            var cat288li = "";
            var cat289li = "";
            var cat290li = "";
            var cat291li = "";
            var cat292li = "";
            var cat293li = "";
            var cat294li = "";

            var cat287Pan = "";
            var cat288Pan = "";
            var cat289Pan = "";
            var cat290Pan = "";
            var cat291Pan = "";
            var cat292Pan = "";
            var cat293Pan = "";
            var cat294Pan = "";

            var fisrtInGroup287 = true;
            var fisrtInGroup288 = true;
            var fisrtInGroup289 = true;
            var fisrtInGroup290 = true;
            var fisrtInGroup291 = true;
            var fisrtInGroup292 = true;
            var fisrtInGroup293 = true;
            var fisrtInGroup294 = true;

            makeDocLiAndPan("ducument_save", "document_save_pane");

            AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormReportChecks/", JSON.stringify({
                PageIndex: 0,
                PageSize: 0,
                IsActive: 15,
                RequestId: id,
                SortOrder: "QuestionId_A",
            }), false, function (res) {
                if (res != null) {
                    checkReport = res;
                    let list_li_data = res.data;

                    list_li_data = res.data.filter((arr, index, self) =>
                        index === self.findIndex((t) => (t.formCode === arr.formCode && t.formCode != "")))

                    for (let i = 0; i < list_li_data.length; i++) {
                        let dataForm = "";
                        let question = "";
                        AjaxCallAction("GET", "/api/customer/Corporate/Get_DataForm/" + list_li_data[i].formId, null, false, function (form) {
                            dataForm = form;
                        }, false);
                        AjaxCallAction("GET", "/api/customer/Corporate/Get_DataFormQuestions/" + list_li_data[i].questionId, null, false, function (form) {
                            question = form;
                        }, false);

                        switch (list_li_data[i].categoryId) {
                            case 287:
                                if (list_li_data[i].questionId != 0 && list_li_data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(list_li_data[i].formCode, dataForm.formTitle, list_li_data[i].formId, id, fisrtInGroup287);
                                    cat287li += tempresult[0];
                                    cat287Pan += tempresult[1];
                                    fisrtInGroup287 = false;
                                }
                                break;
                            case 288:
                                if (list_li_data[i].questionId != 0 && list_li_data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(list_li_data[i].formCode, dataForm.formTitle, list_li_data[i].formId, id, fisrtInGroup288);
                                    cat288li += tempresult[0];
                                    cat288Pan += tempresult[1];
                                    fisrtInGroup288 = false;
                                }
                                break;
                            case 289:
                                if (list_li_data[i].questionId != 0 && list_li_data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(list_li_data[i].formCode, dataForm.formTitle, list_li_data[i].formId, id, fisrtInGroup289);
                                    cat289li += tempresult[0];
                                    cat289Pan += tempresult[1];
                                    fisrtInGroup289 = false;
                                }
                                break;
                            case 290:
                                if (list_li_data[i].questionId != 0 && list_li_data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(list_li_data[i].formCode, dataForm.formTitle, list_li_data[i].formId, id, fisrtInGroup290);
                                    cat290li += tempresult[0];
                                    cat290Pan += tempresult[1];
                                    fisrtInGroup290 = false;
                                }
                                break;

                            case 291:
                                if (list_li_data[i].questionId != 0 && list_li_data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(list_li_data[i].formCode, dataForm.formTitle, list_li_data[i].formId, id, fisrtInGroup291);
                                    cat291li += tempresult[0];
                                    cat291Pan += tempresult[1];
                                    fisrtInGroup291 = false;
                                }
                                break;

                            case 292:
                                if (list_li_data[i].questionId != 0 && list_li_data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(list_li_data[i].formCode, dataForm.formTitle, list_li_data[i].formId, id, fisrtInGroup292);
                                    cat292li += tempresult[0];
                                    cat292Pan += tempresult[1];
                                    fisrtInGroup292 = false;
                                }
                                break;

                            case 293:
                                if (list_li_data[i].questionId != 0 && list_li_data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(list_li_data[i].formCode, dataForm.formTitle, list_li_data[i].formId, id, fisrtInGroup293);
                                    cat293li += tempresult[0];
                                    cat293Pan += tempresult[1];
                                    fisrtInGroup293 = false;
                                }
                                break;

                            case 294:
                                if (list_li_data[i].questionId != 0 && list_li_data[i].formId != 0) {
                                    let tempresult = makeLiAndPan(list_li_data[i].formCode, dataForm.formTitle, list_li_data[i].formId, id, fisrtInGroup294);
                                    cat294li += tempresult[0];
                                    cat294Pan += tempresult[1];
                                    fisrtInGroup294 = false;
                                }
                                break;
                        }
                    }
                    $("#TargetTabs287").append(cat287li);
                    $("#TabPaneTargetTabs287").append(cat287Pan != "" ? cat287Pan : '<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');

                    $("#TargetTabs288").append(cat288li);
                    $("#TabPaneTargetTabs288").append(cat288Pan != "" ? cat288Pan : '<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');

                    $("#TargetTabs289").append(cat289li);
                    $("#TabPaneTargetTabs289").append(cat289Pan != "" ? cat289Pan : '<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');

                    $("#TargetTabs290").append(cat290li);
                    $("#TabPaneTargetTabs290").append(cat290Pan != "" ? cat290Pan : '<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');

                    $("#TargetTabs291").append(cat291li);
                    $("#TabPaneTargetTabs291").append(cat291Pan != "" ? cat291Pan : '<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');

                    $("#TargetTabs292").append(cat292li);
                    $("#TabPaneTargetTabs292").append(cat292Pan != "" ? cat292Pan : '<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');

                    $("#TargetTabs293").append(cat293li);
                    $("#TabPaneTargetTabs293").append(cat293Pan != "" ? cat293Pan : '<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');

                    $("#TargetTabs294").append(cat294li);
                    $("#TabPaneTargetTabs294").append(cat294Pan != "" ? cat294Pan : '<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');
                }
            },
                true
            );

            for (let i = 0; i < checkReport.data.length; i++) {
                let question = "";
                let answer = "";
                let _document = "";
                AjaxCallAction("GET", "/api/customer/Corporate/Get_DataFormQuestions/" + checkReport.data[i].questionId, null, false, function (form) {
                    question = form;
                }, false);
                AjaxCallAction("GET", "/api/customer/Corporate/Get_DataFromAnswers/" + checkReport.data[i].answerId, null, false, function (form) {
                    answer = form;
                }, false);
                if (checkReport.data[i].documentId != null)
                    AjaxCallAction("GET", "/api/customer/Corporate/Get_DataFormDocument/" + checkReport.data[i].documentId, null, false, function (form) {
                        _document = form;
                    }, false);

                if (checkReport.data[i].questionId != 0 && checkReport.data[i].formId != 0) {
                    let strFormId = generate_strFormId(question, id, checkReport.data[i].formId, true, checkReport.data[i].superVisorDescription);
                    $("#FormDetail" + checkReport.data[i].formId).append(strFormId);
                    if (answer.answer == "Yes") {
                        $("input:radio[name='Q_" + answer.dataFormQuestionId + "'][value='Yes']").prop("checked", true);
                    } else if (answer.answer == "No") {
                        $("input:radio[name='Q_" + answer.dataFormQuestionId + "'][value='No']").prop("checked", true);
                    } else {
                        $("#Q_" + answer.dataFormQuestionId + " option[value='" + answer.answer + "']").prop("selected", true);
                    }
                    $("input[name*='Description_Q" + answer.dataFormQuestionId + "']").val(answer.description);
                } else {
                    if (_document != "")
                        makeDocumentFile([_document], checkReport.data[i].superVisorDescription);
                }
            }
            initReferral(id, false);
        }
    }

    function makeLiAndPan(formCode, formTitle, formId, reqid, isActive) {
        let li_option = "";
        let tabPane = "";
        if (isActive) {
            li_option = "<li class='active'><a href='#FormDetailTab" + formCode + "' data-toggle='tab' aria-expanded='false' >" + formCode + "</a></li>";
            tabPane = makeTabPane(formCode, formTitle, formId, reqid, isActive);
            isActive = false;
        } else {
            tabPane = makeTabPane(formCode, formTitle, formId, reqid, isActive);
            li_option = "<li class=''><a href='#FormDetailTab" + formCode + "' data-toggle='tab' aria-expanded='false' >" + formCode + "</a></li>";
        }
        return [li_option, tabPane];
    }

    function initCustomer(dir = "rtl") {
        ComboBoxWithSearch(".select2", dir);
        AjaxCallAction("GET", "/api/customer/Customers/Get_Customers/", null, true, function (res) {
            if (res != null) {
                $("#CutomerName").html(
                    "<h4> فرم پرسشنامه حاکمیت شرکتی " + res.companyName + "</h4>"
                );
            }
        }, true);
    }

    function intiForm(FormID = null, RequestId = null) {
        let strFormId = "";
        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormQuestionss", JSON.stringify({
            DataFormId: FormID,
            PageIndex: 0,
            PageSize: 0,
            DataFormType: 2,
            IsActive: 15,
            Version: VersionQuestion,
        }), false, function (res) {
            if (res.isSuccess) {
                let strFormId = generate_strFormId(res, RequestId, FormID);
                $("#FormDetail" + FormID).html(strFormId);
                AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFromAnswerss", JSON.stringify({
                    PageIndex: 0,
                    PageSize: 0,
                    FormID: FormID,
                    RequestId: RequestId,
                }), true, function (res) {
                    if (res.isSuccess) {
                        LoadedDataFromDb = res.data;
                        for (let i = 0; LoadedDataFromDb.length > i; i++) {
                            if (LoadedDataFromDb[i].answer == "Yes") {
                                $("input:radio[name='Q_" + LoadedDataFromDb[i].dataFormQuestionId + "'][value='Yes']").prop("checked", true);
                            } else if (LoadedDataFromDb[i].answer == "No") {
                                $("input:radio[name='Q_" + LoadedDataFromDb[i].dataFormQuestionId + "'][value='No']").prop("checked", true);
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

    function generate_strFormId(res, RequestId, FormId, isSingle = false, analizeDescription = "") {
        let strFormId = "";
        if (isSingle) {
            strFormId += "<div class='form-group'><div class='col-md-12'><h4 style='line-height: 1.5;'>" + res.questionText;
            if (!isEmpty(res.helpText))
                strFormId += " <span title='" + res.helpText + "'><i class='fa'></i></span>";
            strFormId += "</h4></div><div class='col-md-12'><div class='row'><div class='col-md-4'>"
            if (res.questionType == "select") {
                var options = combo(res.dataFormQuestionId);
                strFormId += "<select required name='Q_" + res.dataFormQuestionId + "' id='Q_" + res.dataFormQuestionId + "' class='form-control' style='padding: 0px 15px;' >" + options + "</select>";
            } else if (res.questionType == "yesNo") {
                strFormId += "<label class='control-label'>بله</label><input type='radio' required name='Q_" + res.dataFormQuestionId + "' value='Yes' />";
                strFormId += "<label class='control-label'>خیر</label><input type='radio' required name='Q_" + res.dataFormQuestionId + "' value='No' />";
            }
            strFormId += "</div><div class='col-md-8'>";
            strFormId += "<input placeholder='توضیحات' class='form-control' name='Description_Q" + res.dataFormQuestionId + "' onfocus='select();' type='text' value='' />";
            strFormId += "</div><div class='col-md-8'>";
            strFormId += "<p style='color: blue;'>توضیح کارشناس : " + analizeDescription + "</p>";
            strFormId += "</div ></div ></div ></div >";
            return strFormId;
        } else {
            strFormId = "<div class='col-md-12'>";
            for (var i = 0; i < res.data.length; i++) {
                strFormId += "<div class='form-group'><div class='col-md-12'><h4 style='line-height: 1.5;'>" + (i + 1) + " - " + res.data[i].questionText;
                if (!isEmpty(res.data[i].helpText))
                    strFormId += " <span title='" + res.data[i].helpText + "'><i class='fa'></i></span>"
                strFormId += "</h4></div><div class='col-md-12'><div class='row'><div class='col-md-4'>";
                if (res.data[i].questionType == "select") {
                    var options = combo(res.data[i].dataFormQuestionId);
                    strFormId += "<select required name='Q_" + res.data[i].dataFormQuestionId + "' id='Q_" + res.data[i].dataFormQuestionId + "' class='form-control' style='padding: 0px 15px;' >" + options + "</select>";
                } else if (res.data[i].questionType == "yesNo") {
                    strFormId += "<label class='control-label'>بله</label><input type='radio' required name='Q_" + res.data[i].dataFormQuestionId + "' value='Yes' />";
                    strFormId += "<label class='control-label'>خیر</label><input type='radio' required name='Q_" + res.data[i].dataFormQuestionId + "' value='No' />";
                }
                strFormId += "</div><div class='col-md-8'>";
                strFormId += "<input placeholder='توضیحات' class='form-control' name='Description_Q" + res.data[i].dataFormQuestionId + "' onfocus='select();' type='text' value='' />";
                strFormId += "</div ></div ></div ></div >"
            }
            strFormId += "</div>";
        }
        return strFormId;
    }

    function combo(QuestionID = null) {
        let strM = "";
        AjaxCallAction("POST", "/api/customer/Corporate/Get_Options", JSON.stringify({
            DataFormQuestionsId: QuestionID,
            PageIndex: 0,
            PageSize: 0,
            IsActive: 15,
        }), false, function (res) {
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
            AjaxCallAction("POST", "/api/customer/Corporate/Get_DataForms", JSON.stringify({
                PageIndex: 0,
                PageSize: 0,
                DataFormType: 2,
                SortOrder: "FormCode_A",
            }), false, function (res) {
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
                    tabPane += makeTabPane(DataFormList[i].formCode, DataFormList[i].formTitle, DataFormList[i].formId, ID, is_first);
                    is_first = false;
                } else {
                    tabPane += makeTabPane(DataFormList[i].formCode, DataFormList[i].formTitle, DataFormList[i].formId, ID, is_first);
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
        li_option += "<li class='active'><a href='#FormDocumentTabDocument287' data-toggle='tab' aria-expanded='true' >بارگذاری مدارک</a></li>";
        //li_option += "<li class='active'><a href='#FormDocumentTabDocument287' data-toggle='tab' aria-expanded='true' >سهامداران</a></li>";
        //li_option += "<li class=''><a href='#FormDocumentTabDocument288' data-toggle='tab' aria-expanded='false' >نقش ذینفعان</a></li>";
        //li_option += "<li class=''><a href='#FormDocumentTabDocument289' data-toggle='tab' aria-expanded='false' >افشاء و شفافیت</a></li>";
        //li_option += "<li class=''><a href='#FormDocumentTabDocument290' data-toggle='tab' aria-expanded='false' >هیئت مدیره</a></li>";
        //li_option += "<li class=''><a href='#FormDocumentTabDocument291' data-toggle='tab' aria-expanded='false' >اطلاعات نهایی و اشخاص وابسته</a></li>";
        //li_option += "<li class=''><a href='#FormDocumentTabDocument292' data-toggle='tab' aria-expanded='false' >صورت های مالی و حسابرسی</a></li>";
        //li_option += "<li class=''><a href='#FormDocumentTabDocument293' data-toggle='tab' aria-expanded='false' >کمیته ها</a></li>";
        //li_option += "<li class=''><a href='#FormDocumentTabDocument294' data-toggle='tab' aria-expanded='false' >سایر</a></li>";

        $("#" + putPlace).append(li_option);

        tabPane += makeDocumentTabPane("Document287", "سهامداران", ID, true);
        tabPane += makeDocumentTabPane("Document288", "نقش ذینفعان", ID, true);
        tabPane += makeDocumentTabPane("Document289", "افشاء و شفافیت", ID, true);
        tabPane += makeDocumentTabPane("Document290", "هیئت مدیره", ID, true);
        tabPane += makeDocumentTabPane("Document291", "اطلاعات نهایی و اشخاص وابسته", ID, true);
        tabPane += makeDocumentTabPane("Document292", "صورت های مالی و حسابرسی", ID, true);
        tabPane += makeDocumentTabPane("Document293", "کمیته ها", ID, true);
        tabPane += makeDocumentTabPane("Document294", "سایر", ID, true);

        $("#" + putTabPan).append(tabPane);
    }

    function makeDynamicDocumentForm(PutPlace, PutTabPane) {
        makeDocLiAndPan(PutPlace, PutTabPane);

        var DataFormDocumentList = [];
        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormDocuments", JSON.stringify({
            PageIndex: 0,
            PageSize: 0,
            IsActive: 15,
            SortOrder: "DataFormDocumentId_A",
        }), false, function (res) {
            if (res.isSuccess) {
                DataFormDocumentList = res.data;
            }
        }, true);

        makeDocumentFile(DataFormDocumentList);
    }

    function makeDocumentFile(DataFormDocumentList, description = "") {
        let ID = $("#RequestIdForms").val();
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
            let isRequierd = DataFormDocumentList[i].isRequierd
            switch (DataFormDocumentList[i].categoryId) {
                case 287:
                    _str287 += makeFileInput(title, formId, helpText, ID, description, isRequierd);
                    break;
                case 288:
                    _str288 += makeFileInput(title, formId, helpText, ID, description, isRequierd);
                    break;
                case 289:
                    _str289 += makeFileInput(title, formId, helpText, ID, description, isRequierd);
                    break;
                case 290:
                    _str290 += makeFileInput(title, formId, helpText, ID, description, isRequierd);
                    break;
                case 291:
                    _str291 += makeFileInput(title, formId, helpText, ID, description, isRequierd);
                    break;
                case 292:
                    _str292 += makeFileInput(title, formId, helpText, ID, description, isRequierd);
                    break;
                case 293:
                    _str293 += makeFileInput(title, formId, helpText, ID, description, isRequierd);
                    break;
                case 294:
                    _str294 += makeFileInput(title, formId, helpText, ID, description, isRequierd);
                    break;
                default:
                    break;
            }
        }

        if (_str287 == "") {
            $("#FormDocumentDocument287").html('<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');
        } else {
            $("#FormDocumentDocument287").append(_str287);
        }

        if (_str288 == "") {
            $("#FormDocumentDocument288").html('<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');
        } else {
            $("#FormDocumentDocument288").append(_str288);
        }

        if (_str289 == "") {
            $("#FormDocumentDocument289").html('<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');
        } else {
            $("#FormDocumentDocument289").append(_str289);
        }

        if (_str290 == "") {
            $("#FormDocumentDocument290").html('<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');
        } else {
            $("#FormDocumentDocument290").append(_str290);
        }

        if (_str291 == "") {
            $("#FormDocumentDocument291").html('<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');
        } else {
            $("#FormDocumentDocument291").append(_str291);
        }

        if (_str292 == "") {
            $("#FormDocumentDocument292").html('<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');
        } else {
            $("#FormDocumentDocument292").append(_str292);
        }

        if (_str293 == "") {
            $("#FormDocumentDocument293").html('<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');
        } else {
            $("#FormDocumentDocument293").append(_str293);
        }

        if (_str294 == "") {
            $("#FormDocumentDocument294").html('<p class="text-primary text-center">سوالی برای نمایش وجود ندارد</p>');
        } else {
            $("#FormDocumentDocument294").append(_str294);
        }

        if (isEmpty(LoadedDataFromDbDocument))
            AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFromAnswersDocuments", JSON.stringify({
                PageIndex: 0,
                PageSize: 0,
                FormID: null,
                RequestId: ID,
                DataFormQuestionId: null,
            }), false, function (res) {
                if (res.isSuccess) {
                    LoadedDataFromDbDocument = res.data;
                }
            }, true);
        for (let i = 0; i < LoadedDataFromDbDocument.length; i++) {
            try {
                $("#Download_" + LoadedDataFromDbDocument[i].dataFormDocumentId).prop("href", LoadedDataFromDbDocument[i].fileName1Full);
                $("#Download_" + LoadedDataFromDbDocument[i].dataFormDocumentId).css("display", "inline-block");
            } catch { }
        }
    }

    function makeFileInput(inputTitle, inputName, helpText, RequestId, analaizeDescription = "", isRequierd = true) {
        let _str = "<form id='frmDoc" + inputName + "'>";
        _str += "<div class='form-group'><div class='col-md-12' style='margin-bottom:10px'><label class='control-label'>" + inputTitle;
        if (!isEmpty(helpText))
            _str += " <span title='" + helpText + "'><i class='fa'></i></span>";
        if (isRequierd)
            _str += "<span style='font-size: 25px;color: red;padding: 10px;'>*</span>"
        _str += "</label><input type='file' name='Result_Final_FileName1' accept='image/*,.pdf,.xlsx,' id='Inp" + inputName + "'";
        _str += "onchange=\"checkUploadWithFileSiza(this, '" + inputTitle + "' , 5);\">";
        if (userAccessSaveForm) {
            _str += '<input type="submit" value="ذخیره فایل" class="btn btn-success" ';
            _str += "onclick=\"return Web.Corporate.Save_AnswersUpload(this, 'frmDoc" + inputName + "')\">";
        }
        _str += "<a class='btn btn-primary' style='margin-right: 10px;display:none;' target='_blank' id='Download_" + inputName.slice(3, inputName.length) + "'>";
        if (analaizeDescription != "") {
            _str += "<i class='fa fa-download'></i> &nbsp;دانلود</a><br/><p style='color:blue;'> توضیحات کارشناس : " + analaizeDescription + "</p></div></div>";
        } else {
            _str += "<i class='fa fa-download'></i> &nbsp;دانلود</a></div></div>";
        }
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
        } else {
            strM = "<div class='tab-pane' id='FormDetailTab" + FormCode + "'>";
        }
        strM += "<div style='display:flex;justify-content: space-between;align-items: center;'>";
        strM += "<h2 class='fs-title'>" + FormTitle + "</h2>";
        if (userAccessSaveForm) {
            strM += "<a class='btn btn-success changeData' style='height: 35px;' onclick='Web.Corporate.SaveSerializedForm(" + FormId + ");'>ذخیره تغییرات " + FormCode + "</a>";
        }
        strM += "</div><div style=' border: 2px solid #00c0ef; padding: 30px; border-radius: 5px; margin-bottom: 20px'><form id='frmFrom";
        strM += FormId + "' class='changeData'>";
        strM += "<input type='hidden' id='FormID' name='FormID' value='" + FormId + "' />";
        strM += "<input type='hidden' id='RequestId' name='RequestId' value='" + RequestId + "' />";
        strM += "<div class='row' id='FormDetail" + FormId + "'></div></form></div></div>";
        return strM;
    }

    function makeDocumentTabPane(FormCode, FormTitle, RequestId, FirstItemActive = true) {
        let is_first = FirstItemActive;
        let strM = "";
        if (is_first) {
            strM = "<div class='tab-pane active' id='FormDocumentTab" + FormCode + "'>";
        } else {
            strM = "<div class='tab-pane' id='FormDocumentTab" + FormCode + "'>";
        }
        strM += "<div style='display:flex;justify-content: space-between;align-items: center;'>";
        strM += "<h2 class='fs-title'>" + FormTitle + "</h2></div>";
        strM += "<div style=' border: 2px solid #00c0ef; padding: 30px; border-radius: 5px; margin-bottom: 20px'><form id='frmFrom";
        strM += FormCode + "' class='changeData'>";
        strM += "<input type='hidden' id='RequestId' name='RequestId' value='" + RequestId + "' />";
        strM += "<div class='row' id='FormDocument" + FormCode + "'></div></form></div></div>";
        return strM;
    }

    function save_AnswersUpload(el, FormName) {
        let requestId = $("#RequestIdForms").val();
        $(el).attr("disabled", "");
        let fileInp = document.getElementById(
            "Inp" + FormName.slice(6, FormName.length)
        );
        showWait();
        if (fileInp.files.length != 0) {
            AjaxCallActionPostSaveFormWithUploadFile("/api/customer/Corporate/Save_DataFromAnswersUpload", fill_AjaxCallActionPostSaveFormWithUploadFile(FormName), true,
                function (res) {
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
                            }), true,
                                function (reee) { },
                                true
                            );
                        $("#Download_" + FormName.slice(9, FormName.length)).prop("href", res.data.fileName1Full);
                        $("#Download_" + FormName.slice(9, FormName.length)).css("display", "inline-block");
                        hideWait();
                        alertB("ثبت", "اطلاعات ثبت شد", "success");
                    } else {
                        alertB("خطا", "ذخیره موفقیت آمیز نبود مجددا تلاش فرماید", "error");
                    }
                },
                true
            );
        } else {
            hideWait();
            alertB("خطا", "ابتدا فایل را انتخاب نمایید!", "error");
            $(el).removeAttr("disabled");
        }
        hideWait();
        return false;
    }

    function saveSerializedForm(FormId) {
        showWait();
        let SerializerForm = $("#frmFrom" + FormId).serializeArray();
        let formId = SerializerForm[0]["value"];
        let SerializerAnswers = SerializerForm.slice(2, SerializerForm.length);
        let list_of_answer = SerializerAnswers.filter(s => s.name.startsWith("Q_"))
        let list_of_description = SerializerAnswers.filter(s => s.name.startsWith("Description_"))
        let counter = SerializerAnswers.length;

        if (list_of_answer.length == list_of_description.length) {
            for (let i = 0; i < counter && counter % 2 == 0; i += 2) {
                let SingleQuestion = SerializerAnswers.slice(0, 2);
                let question_id = SingleQuestion[0]["name"].split("_")[1];
                let answer = SingleQuestion[0]["value"];
                let description = SingleQuestion[1]["value"];

                SerializerAnswers = SerializerAnswers.slice(2, SerializerAnswers.length);

                if (!isEmpty(answer) && answer != "0") {
                    saveSingelAnswerForm(formId, answer, description, question_id);
                }
            }
        }
        else {
            alertB("خطا", "ابتدا به همه سوالات این صفحه پاسخ داده سپس اقدام به ذخیره سازی فرم نمایید", "error", "بله متوجه شدم", function () { });
        }
        hideWait();
    }

    function saveSingelAnswerForm(formId = "0", answer = "0", description = "", dataFormQuestionId = "0", fileName = "") {
        var requestId = $("#RequestId").val();
        var dataFormQuestionScore = 0;
        
        AjaxCallAction("POST", "/api/customer/Corporate/Save_DataFromAnswers",
            JSON.stringify({
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
                        AjaxCallAction("GET", "/api/customer/Corporate/Get_DataFormQuestions/" + dataFormQuestionId, null, false,
                            function (res1) {
                                if (!isEmpty(res1)) {
                                    dataFormQuestionScore = res1.score;
                                    if (res1.questionType == "select") {
                                        AjaxCallAction("GET", "/api/customer/Corporate/Get_Option/" + answer.split("_")[0], null, false,
                                            function (datares) {
                                                if (!isEmpty(datares)) {
                                                    dataFormQuestionScore =
                                                        dataFormQuestionScore * datares.ratio;
                                                }
                                            }, false);
                                    } else if (res1.questionType == "yesNo") {
                                        if (answer == "No") {
                                            dataFormQuestionScore = 0;
                                        }
                                    }
                                    // اگر با موفقیت پاسخ ذخیره شد
                                    if (res.dataId != 0)
                                        // ذخیره سوال در جدول آنالیز نمره
                                        AjaxCallAction("POST", "/api/customer/Corporate/Save_DataFromReport",
                                            JSON.stringify({
                                                RequestId: requestId,
                                                DataFormAnswerId: res.dataId,
                                                SystemScore: dataFormQuestionScore,
                                                AnalizeScore: 0,
                                                IsActive: 15,
                                            }), false, function (reee) { }, false);
                                }
                            }, false);
                    }

                    alertB("ثبت", res.message, "success");
                } else {
                    alertB("خطا", res.message, "error");
                }
        }, true);
        
    }


    function initReferral(id = null, is_check = true) {
        AjaxCallAction("GET", "/api/Customer/RequestForRating/InitReferral/" + id, null, true, function (res) {
            if (res.isSuccess) {

                $("#sdklsslks3498sjdkxhjsd_823sa").val(encrypt(id.toString(), keyMaker()));
                $("#sdklsslks3498sjdkxhjsd_823sdel").val(res.data[0].sendUser);
                var htmlB = "";
                for (var i = 0; i < res.data.length; i++) {
                    if ((res.data[0].levelStepIndex == 105 || res.data[0].levelStepIndex == 106) && res.data[0].levelStepAccessRole == 12) {
                        htmlB += "<button type='button' id='btnreq' style='margin:5px' class='btn btn-info ButtonOpperationLSSlss' onclick='Web.Corporate.CheckAnswerToAllQuestion(this, " + is_check + ");'" + "data-SIndex='" + res.data[i].levelStepSettingIndexId + "' data-DLSI='" + encrypt(res.data[i].destLevelStepIndex, keyMaker()) + "' data-LSAR='" + encrypt(res.data[i].levelStepAccessRole, keyMaker()) + "' data-LSS='" + encrypt(res.data[i].levelStepStatus, keyMaker()) + "' data-SC='" + encrypt(res.data[i].smsContent, keyMaker()) + "' data-ST='" + res.data[i].smsType + "' data-DLSIB='" + encrypt(res.data[i].destLevelStepIndexButton, keyMaker()) + "'>" + res.data[i].destLevelStepIndexButton + "</button>";

                    }
                }
                $("#bLLSS").html(htmlB);
                $("#sdfcddf").val(1);

            } else {
                $("#EndCorporateInfo").html("اطلاعات شما برای پارس کیان جهت بررسی و ارزیابی ارسال شده است و قابل ویرایش نمی باشد.");
            }


        }, true);
    }

    function checkAnswerToAllQuestion(el, is_check = true) {
        if (is_check == true) {
            let countOfQuestion = 0;
            let countOfAnswer = 0;
            showWait();
            AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFromAnswerss", JSON.stringify({
                PageIndex: 0,
                PageSize: 0,
                IsActive: 15,
                RequestId: $("#RequestIdForms").val(),
            }), false, function (res) {
                if (res.isSuccess) {
                    countOfAnswer = res.data.length;
                }
            }, false);

            AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormQuestionss", JSON.stringify({
                PageIndex: 0,
                PageSize: 0,
                IsActive: 15,
                DataFormType: 2,
                Version: VersionQuestion,
            }), false, function (res) {
                countOfQuestion = res.data.length;
            }, false);

            if (countOfAnswer == countOfQuestion) {
                Web.RequestForRating.SaveReferralRequestForRating(el)
            }
            else {
                hideWait();
                alertB("خطا", "ابتدا باید به همه سوالات پاسخ بدهید سپس اقدام به ذخیره سازی نمایید", "error", "بله متوجه شدم", function () { });
            }
        }
        else {
            Web.RequestForRating.SaveReferralRequestForRating(el)
        }
    }

    function initCorporateScore(id) {
        let sum_score = 0;
        let all_score = 0;
        AjaxCallAction("POST", "/api/customer/Corporate/Get_DataFormReports", JSON.stringify({
            PageIndex: 0,
            PageSize: 0,
            IsActive: 15,
            RequestId: id
        }), false, function (res) {
            if (res.isSuccess) {
                console.log(res)
                for (let i = 0; i < res.data.length; i++) {
                    sum_score += res.data[i].analizeScore;
                    all_score += res.data[i].systemScore;
                }
            }
        }, true);
        $("#msform").html('<p>نمره نهایی شما برابر است با ' + sum_score + ' از ' + all_score + ' نمره</p>')
    }

    //function getDocument(id = null) {
    //    if (!isEmpty(id) && id != 0) {

    //        AjaxCallAction("GET", "/api/Customer/RequestForRating/Get_ContractAndFinancialDocuments/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

    //            if (res.isSuccess) {
    //                $("#FinancialID").val(res.data.financialId);
    //                $("#RequestID").val(res.data.requestID);
    //                if ((res.data.committeeEvaluationFile != null && res.data.committeeEvaluationFile != "") || res.data.confirmCommitteeEvaluation === "True") {
    //                    $("#frmFormMain2").remove();
    //                    $("#frmFormMain4").remove();
    //                } else {
    //                    $("#ConfirmCommitteeEvaluation").val(res.data.confirmCommitteeEvaluation);
    //                }
    //                if (getlstor("loginName") === "8") {
    //                    $("#EvaluationFile").val(res.data.evaluationFile);
    //                }
    //                if (getlstor("loginName") === "6") {
    //                    $("div.result").remove();
    //                }


    //                if (res.data.financialDocument != null && res.data.financialDocument != "") {
    //                    $("#divDownloadFinancialDocument").html("<a class='btn btn-success' href='" + res.data.financialDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
    //                } else {
    //                    $("#divDownloadFinancialDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
    //                }
    //                if (res.data.financialDocument2 != null && res.data.financialDocument2 != "") {
    //                    $("#divDownloadFinancialDocument2").html("<a class='btn btn-success' href='" + res.data.financialDocument2Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
    //                } else {
    //                    $("#divDownloadFinancialDocument2").html("<p style='color:silver'>فایلی وجود ندارد</p>");
    //                }
    //                if (res.data.contractDocument != null && res.data.contractDocument != "") {
    //                    $("#divDownload_ContractDocument").html("<a class='btn btn-success' href='" + res.data.contractDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
    //                } else {
    //                    $("#divDownload_ContractDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
    //                }
    //                if (res.data.evaluationFile != null && res.data.evaluationFile != "") {
    //                    $("#divDownload_EvaluationFile").html("<a class='btn btn-success' href='" + res.data.evaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
    //                } else {
    //                    $("#divDownload_EvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
    //                }
    //                if (res.data.committeeEvaluationFile != null && res.data.committeeEvaluationFile != "") {
    //                    $("#divDownload_CommitteeEvaluationFile").html("<a class='btn btn-success' href='" + res.data.committeeEvaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
    //                } else {
    //                    $("#divDownload_CommitteeEvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
    //                }
    //                if (res.data.lastFinancialDocument != null && res.data.lastFinancialDocument != "") {
    //                    $("#divDownload_LastFinancialDocument").html("<a class='btn btn-success' href='" + res.data.lastFinancialDocumentFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
    //                } else {
    //                    $("#divDownload_LastFinancialDocument").html("<p style='color:silver'>فایلی وجود ندارد</p>");
    //                }
    //                if (res.data.leaderEvaluationFile != null && res.data.leaderEvaluationFile != "") {
    //                    $("#divDownload_LeaderEvaluationFile").html("<a class='btn btn-success' href='" + res.data.leaderEvaluationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
    //                } else {
    //                    $("#divDownload_LeaderEvaluationFile").html("<p style='color:silver'>فایلی وجود ندارد</p>");
    //                }
    //            }

    //        }, true);
    //    }
    //}

    web.Corporate = {
        IntiForm: intiForm,
        InitCorporate: initCorporate,
        InitCustomer: initCustomer,
        Combo: combo,
        MakeDynamicForm: makeDynamicForm,
        MakeDynamicDocumentForm: makeDynamicDocumentForm,
        SaveSerializedForm: saveSerializedForm,
        Save_AnswersUpload: save_AnswersUpload,
        InitReferral: initReferral,
        InitCorporateScore: initCorporateScore,
        CheckAnswerToAllQuestion: checkAnswerToAllQuestion,
    };
})(Web, jQuery);