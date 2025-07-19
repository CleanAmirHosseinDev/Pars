function go_prev(filterGrid) {
    let current_page = $("#current_page")
    let page = current_page.data("page");
    if (page > 1) {
        current_page.text(String(page - 1))
        current_page.data("page", page - 1)
        filterGrid(page - 1);
    }
}

function go_next(filterGrid) {
    let current_page = $("#current_page")
    let page = current_page.data("page");
    if (page) {
        current_page.text(String(page + 1))
        current_page.data("page", page + 1)
        filterGrid(page + 1);
    }
}

(function (web, $) {
    var DataFormList = null;
    var DataFormQuestionsList = null;

    function dataFormFilterGrid(page=1) {
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataForms", JSON.stringify({ SortOrder: $(".thtrtheadtableSortingGrid_DataForm_tBodyList[data-Selected]").attr("data-Selected"), Search: $("#txtSearch").val(), PageIndex: page, PageSize: $("#cboSelectCount").val(), DataFormType: 2, IsActive: 15 }), false, function (res) {
            if (res.isSuccess) {
                DataFormList = res.data;
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (let i = 0; i < res.data.length; i++) {
                    let category = getCategoryName(res.data[i].categoryId);
                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].formCode + "</td><td>" + res.data[i].formTitle.slice(0, 75) + "</td><td>" + category + "</td><td><a title='ویرایش' href='/Admin/Corporate/EditDataForm?id=" + res.data[i].formId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' onclick='Web.Corporate.DeleteDataForm(" + res.data[i].formId + ");' class='btn btn-danger fontForAllPage'><i class='fa fa-remove'></i></a></td></tr>";
                }
                $("#tBodyList").html(strM);
            }
        }, true);
    }
    function clickSortingGrid(e) {

        clickSortingGridWithConfig(e, "thtrtheadtableSortingGrid_DataForm_tBodyList");
        dataFormFilterGrid();
    }
    function initDataForm(id = null, dir = 'rtl') {
        ComboBoxWithSearch('.select2', dir);
        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("GET", "/api/admin/Corporate/Get_DataForm/" + id, null, true, function (res) {
                if (res != null) {
                    let category = getCategoryName(res.categoryId);
                    $("#FormId").val(res.formId);
                    $("#FormCode").val(res.formCode);
                    $("#FormTitle").val(res.formTitle);
                    $("#CategoryId").val(res.categoryId);
                    $("#IsTable").val(res.isTable);
                    comboBoxWithSearchUpdateText("CategoryId", category);
                }
            }, true);
        }
    }
    function saveDataForm(e) {

        $(e).attr("disabled", "");
        let FormId = $("#FormId").val();
        let FormTitle = $("#FormTitle").val();
        let FormCode = $("#FormCode").val();
        let CategoryId = $("#CategoryId").val();
        AjaxCallAction("POST", "/api/admin/Corporate/Save_DataForm", JSON.stringify(
            {
                FormId: !isEmpty(FormId) ? FormId : 0,
                FormCode: FormCode,
                FormTitle: FormTitle,
                CategoryId: !isEmpty(CategoryId) ? CategoryId : null,
            }), true, function (res) {

                $(e).removeAttr("disabled");

                if (res.isSuccess) {
                    goToUrl("/Admin/Corporate/DataForm");
                }
                else {
                    alertB("خطا", res.message, "error");
                }
            }, true);
    }
    function deleteDataForm(id) {
        try {
            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {
                AjaxCallAction("GET", "/api/admin/Corporate/Delete_DataForm/" + (isEmpty(id) ? '0' : id),
                    null, true, function (result) {
                        if (result.isSuccess) {
                            dataFormFilterGrid();
                            alertB("", result.message, "success");
                        }
                        else {
                            alertB("خطا", result.message, "error");
                        }
                    }, true);
            }, function () {
            }, ["خیر", "بلی"]);
        } catch (e) {
        }
    }
    function dataFormQuestionsFilterGrid(page = 1) {
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataForms", JSON.stringify({ PageIndex: 0, PageSize: 0, IsActive: 15 }), false, function (res) {
            if (res.isSuccess) {
                DataFormList = res.data;
            }
        }, true);
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataFormQuestionss", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: page, PageSize: $("#cboSelectCount").val(), DataFormType: 2, IsActive: 15 }), true, function (res) {
            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {
                    let type = "";
                    if (res.data[i].questionType == "yesNo") {
                        type = "بله/خیر";
                    }
                    else {
                        type = "لیکرتی";
                    }
                    let dataFormTitle = getObjectWithFormId(DataFormList, res.data[i].dataFormId);
                    if (dataFormTitle) {
                        dataFormTitle = dataFormTitle.formTitle.length > 79 ? dataFormTitle.formTitle.slice(0, 75) + " ..." : dataFormTitle.formTitle;
                    }
                    else {
                        dataFormTitle = "حذف شده";
                    }
                    let questionText = res.data[i].questionText.length > 79 ? res.data[i].questionText.slice(0, 75) + " ..." : res.data[i].questionText;
                    let questionLevel = "بدون سطح";
                    AjaxCallAction("GET", "/api/admin/Corporate/Get_QuestionLevel/" + res.data[i].questionLevel, null, false, function (res) {
                        if (res != null) {
                            questionLevel = res.levelTitle;
                        }
                    }, false);
                    strM += "<tr><td>" + (i + 1) + "</td><td>" + questionText + "</td><td>" + dataFormTitle + "</td><td>" + type + "</td><td>" + res.data[i].score + "</td><td>" + res.data[i].questionOrder + "</td><td>" + questionLevel + "</td><td><a title='ویرایش' href='/Admin/Corporate/EditDataFormQuestions?id=" + res.data[i].dataFormQuestionId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' onclick='Web.Corporate.DeleteDataFormQuestions(" + res.data[i].dataFormQuestionId + ");' class='btn btn-danger fontForAllPage'><i class='fa fa-remove'></i></a></td></tr>";
                }
                $("#tBodyList").html(strM);
            }
        }, true);
    }
    function initDataFormQuestions(id = null, dir = 'rtl') {
        let selecet_item = makeComboForQuestionLevel();
        $("#QuestionLevel").html(selecet_item);
        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("GET", "/api/admin/Corporate/Get_DataFormQuestions/" + id, null, true, function (res) {
                if (res != null) {
                    let type = "";
                    if (res.questionType == "yesNo") {
                        type = "بله/خیر";
                    }
                    else {
                        type = "لیکرتی";
                    }
                    $("#DataFormQuestionId").val(res.dataFormQuestionId);
                    $("#QuestionText").val(res.questionText);
                    $("#QuestionName").val(res.questionName);
                    $("#QuestionType").val(res.questionType);
                    $("#DataFormId").val(res.dataFormId);
                    makeDataFormComboBoxForSelectSubCategory(res.dataFormId);
                    comboBoxWithSearchUpdateText("QuestionType", type);
                    comboBoxWithSearchUpdateText("DataForms", getObjectWithFormId(DataFormList, res.dataFormId).formTitle);
                    $("#QuestionLevel" + " option[value='" + res.questionLevel + "']").prop("selected", true);
                    $("#QuestionOrder").val(res.questionOrder);
                    $("#Score").val(res.score);
                    $("#HelpText").val(res.helpText);
                }
            }, true);
        } else {
            makeDataFormComboBoxForSelectSubCategory(0);
        }
    }
    function saveDataFormQuestions(e) {

        $(e).attr("disabled", "");
        let DataFormQuestionId = $("#DataFormQuestionId").val();
        let DataFormId = $("#DataForms").val();
        let DataFormType = 2;
        let QuestionText = $("#QuestionText").val();
        let QuestionName = $("#QuestionName").val();
        let QuestionType = $("#QuestionType").val();
        let QuestionOrder = $("#QuestionOrder").val();
        let Score = $("#Score").val();
        let HelpText = $("#HelpText").val();
        let QuestionLevel = $("#QuestionLevel").val();

        AjaxCallAction("POST", "/api/admin/Corporate/Save_DataFormQuestions", JSON.stringify(
            {
                DataFormQuestionId: !isEmpty(DataFormQuestionId) ? DataFormQuestionId : 0,
                DataFormId: !isEmpty(DataFormId) ? DataFormId : 0,
                DataFormType: DataFormType,
                QuestionText: QuestionText,
                QuestionName: QuestionName,
                QuestionType: QuestionType,
                QuestionOrder: !isEmpty(QuestionOrder) ? QuestionOrder : 0,
                Score: !isEmpty(Score) ? Score : 0,
                HelpText: HelpText,
                IsActive: 15,
                QuestionLevel: QuestionLevel,
            }), true, function (res) {

                $(e).removeAttr("disabled");

                if (res.isSuccess) {
                    goToUrl("/Admin/Corporate/DataFormQuestions");
                }
                else {
                    alertB("خطا", res.message, "error");
                }
            }, true);
    }
    function deleteDataFormQuestions(id) {
        try {
            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {
                AjaxCallAction("GET", "/api/admin/Corporate/Delete_DataFormQuestions/" + (isEmpty(id) ? '0' : id), null, true, function (result) {
                    if (result.isSuccess) {
                        dataFormQuestionsFilterGrid();
                        alertB("", result.message, "success");
                    }
                    else {
                        alertB("خطا", result.message, "error");
                    }
                }, true);
            }, function () {
            }, ["خیر", "بلی"]);
        } catch (e) {
        }
    }
    function makeComboForQuestionLevel() {
        let strM = "";
        AjaxCallAction("POST", "/api/admin/Corporate/Get_QuestionLevels", JSON.stringify({
            PageIndex: 0,
            PageSize: 0,
            IsActive: 15,
        }), false, function (res) {
            if (res.isSuccess) {
                strM = '<option value="0">انتخاب کنید</option>';
                for (var i = 0; i < res.data.length; i++) {
                    strM += "<option value='" + res.data[i].questionLevelId + "'>" + res.data[i].levelTitle + "</option>";
                }
            }
        }, false);
        return strM;
    }
    function getDataFormQuestionsList() {
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataFormQuestionss", JSON.stringify({ PageIndex: 0, PageSize: 0, IsActive: 15, DataFormType: 2 }), false, function (res) {
            if (res.isSuccess)
                DataFormQuestionsList = res.data;
        }, true);

    }
    function dataFormDocumentFilterGrid(page = 1) {
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataFormDocuments", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: page, PageSize: $("#cboSelectCount").val(), IsActive: 15 }), true, function (res) {
            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {
                    let is_requierd = res.data[i].isRequierd == true ? "اجباری" : "اختیاری";
                    let category = getCategoryName(res.data[i].categoryId);
                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].title + "</td><td>" + category + "</td><td>" + is_requierd + "</td><td><a title='ویرایش' href='/Admin/Corporate/EditDataFormDocument?id=" + res.data[i].dataFormDocumentId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' onclick='Web.Corporate.DeleteDataFormDocument(" + res.data[i].dataFormDocumentId + ");' class='btn btn-danger fontForAllPage'><i class='fa fa-remove'></i></a></td></tr>";
                }
                $("#tBodyList").html(strM);
            }
        }, true);
    }
    function initDataFormDocument(id = null, dir = 'rtl') {
        ComboBoxWithSearch('.select2', dir);
        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("GET", "/api/admin/Corporate/Get_DataFormDocument/" + id, null, true, function (res) {
                if (res != null) {
                    $("#DataFormDocumentId").val(res.dataFormDocumentId);
                    $("#CategoryId").val(res.categoryId);
                    $("#Title").val(res.title);
                    $("#HelpText").val(res.helpText);
                    $("#IsRequired").prop("checked", res.isRequierd);
                }
            }, true);
        }
    }
    function saveDataFormDocument(e) {
        $(e).attr("disabled", "");
        let _DataFormDocumentId = $("#DataFormDocumentId").val();
        let _CategoryId = $("#CategoryId").val();
        let _Title = $("#Title").val();
        let _HelpText = $("#HelpText").val();
        let _IsRequierd = $("#IsRequired").prop("checked");
        AjaxCallAction("POST", "/api/admin/Corporate/Save_DataFormDocument", JSON.stringify(
            {
                DataFormDocumentId: !isEmpty(_DataFormDocumentId) ? _DataFormDocumentId : 0,
                CategoryId: _CategoryId,
                Title: _Title,
                HelpText: _HelpText,
                IsRequierd: _IsRequierd,
            }), true, function (res) {

                $(e).removeAttr("disabled");

                if (res.isSuccess) {
                    goToUrl("/Admin/Corporate/DataFormDocument");
                }
                else {
                    alertB("خطا", res.message, "error");
                }
            }, true);
    }
    function deleteDataFormDocument(id) {
        try {
            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {
                AjaxCallAction("GET", "/api/admin/Corporate/Delete_DataFormDocument/" + (isEmpty(id) ? '0' : id), null, true, function (result) {
                    if (result.isSuccess) {
                        dataFormDocumentFilterGrid();
                        alertB("", result.message, "success");
                    }
                    else {
                        alertB("خطا", result.message, "error");
                    }
                }, true);
            }, function () {
            }, ["خیر", "بلی"]);
        } catch (e) {
        }
    }
    function dataFormQuestionsOptioneFilterGrid(page = 1) {
        getDataFormQuestionsList();
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataFormQuestionsOptiones", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: page, PageSize: $("#cboSelectCount").val(), IsActive: 15 }), true, function (res) {
            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {
                    let questionText = getObjectWithDataFormQuestionsId(DataFormQuestionsList, res.data[i].dataFormQuestionsId);
                    if (questionText) {
                        questionText = questionText.questionText;
                    } else {
                        questionText = "حذف شده";
                    }
                    questionText = questionText.length > 79 ? questionText.slice(0, 75) + " ..." : questionText;
                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].text + "</td><td>" + questionText + "</td><td>" + res.data[i].ratio + "</td><td><a title='ویرایش' href='/Admin/Corporate/EditDataFormQuestionsOptione?id=" + res.data[i].id + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' onclick='Web.Corporate.DeleteDataFormQuestionsOptione(" + res.data[i].id + ");' class='btn btn-danger fontForAllPage'><i class='fa fa-remove'></i></a></td></tr>";
                }
                $("#tBodyList").html(strM);
            }
        }, true);
    }
    function initDataFormQuestionsOptione(id = null, dir = 'rtl') {
        ComboBoxWithSearch('.select2', dir);
        getDataFormQuestionsList();
        makeDataFormQuestionsList();
        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("GET", "/api/admin/Corporate/Get_DataFormQuestionsOptione/" + id, null, true, function (res) {
                if (res != null) {
                    let questionText = getObjectWithDataFormQuestionsId(DataFormQuestionsList, res.dataFormQuestionsId);
                    if (questionText) {
                        questionText = questionText.questionText;
                    } else {
                        questionText = "حذف شده";
                    }
                    questionText = questionText.length > 79 ? questionText.slice(0, 75) + " ..." : questionText;;
                    $("#Text").val(res.text);
                    $("#DataFormQuestionsId").val(res.dataFormQuestionsId);
                    $("#DataFormQuestionsText").text(questionText);
                    $("#Ratio").val(res.ratio);
                    questionText = questionText;
                    comboBoxWithSearchUpdateText("DataFormQuestionsId", questionText);
                    $("#Id").val(id);
                }
            }, true);
        }
        $('#DataFormQuestionsId').on('select2:select', function (e) {
            var data = e.params.data;
            let questionText = getObjectWithDataFormQuestionsId(DataFormQuestionsList, data.id).questionText;
            $("#DataFormQuestionsText").text(questionText);
        });
    }
    function saveDataFormQuestionsOptione(e) {

        $(e).attr("disabled", "");
        let Id = $("#Id").val();
        let Text_ = $("#Text").val();
        let DataFormQuestionsId = $("#DataFormQuestionsId").val();
        let Ratio = $("#Ratio").val();
        if (DataFormQuestionsId != null) {
            AjaxCallAction("POST", "/api/admin/Corporate/Save_DataFormQuestionsOptione", JSON.stringify(
                {
                    Id: !isEmpty(Id) ? Id : 0,
                    Text: Text_,
                    DataFormQuestionsId: DataFormQuestionsId,
                    Ratio: Ratio,
                }), true, function (res) {

                    $(e).removeAttr("disabled");

                    if (res.isSuccess) {
                        goToUrl("/Admin/Corporate/DataFormQuestionsOptione");
                    }
                    else {
                        alertB("خطا", res.message, "error", "متوجه شدم");
                    }
                }, true);
        }
        else {
            alert("ابتدا سوال حذف نشده ای را انتخاب نمایید");
            $(e).removeAttr("disabled");
        }
    }
    function deleteDataFormQuestionsOptione(id) {
        try {
            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {
                AjaxCallAction("GET", "/api/admin/Corporate/Delete_DataFormQuestionsOptione/" + (isEmpty(id) ? '0' : id), null, true, function (result) {
                    if (result.isSuccess) {
                        dataFormQuestionsOptioneFilterGrid();
                        alertB("", result.message, "success");
                    }
                    else {
                        alertB("خطا", result.message, "error");
                    }
                }, true);
            }, function () {
            }, ["خیر", "بلی"]);
        } catch (e) {
        }
    }
    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }
    function makeDataFormQuestionsList() {
        var strM = '';
        for (var i = 0; i < DataFormQuestionsList.length; i++) {
            if (DataFormQuestionsList[i].questionType === "select" && DataFormQuestionsList[i].dataFormType == 2 && DataFormQuestionsList[i].isActive == 15) {
                let questionText = DataFormQuestionsList[i].questionText.length > 79 ? DataFormQuestionsList[i].questionText.slice(0, 75) + " ..." : DataFormQuestionsList[i].questionText;
                strM += " <option value=" + DataFormQuestionsList[i].dataFormQuestionId + ">" + questionText + "</option>";
            }
        }
        $("#DataFormQuestionsId").html(strM);
    }
    function makeDataFormComboBoxForSelectSubCategory(id) {
        let strM = '<option value="">انتخاب کنید</option>';
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataForms", JSON.stringify({ PageIndex: 0, PageSize: 0 }), false, function (res) {
            if (res.isSuccess) {
                DataFormList = res.data;
                // TODO Remove this statement
                strM = '<option value="">انتخاب کنید</option>';
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].formId == id) {
                        strM += " <option value='" + res.data[i].formId + "' selected>" + res.data[i].formTitle + "</option>";
                    }
                    else {
                        strM += " <option value='" + res.data[i].formId + "'>" + res.data[i].formTitle + "</option>";
                    }

                }
                $("#DataForms").html(strM);
            }
            else {
                alertB("خطا", res.message, "error");
            }
        }, true);

    }
    function getObjectWithFormId(object_list, id) {
        return object_list.find(o => o.formId === id);
    }
    function getObjectWithDataFormQuestionsId(object_list, id) {
        return object_list.find(o => o.dataFormQuestionId === parseInt(id));
    }
    function getCategoryName(id) {
        switch (id) {
            case 287:
                category = "حقوق سهامداران";
                break;
            case 288:
                category = "نقش ذینفعان";
                break;
            case 289:
                category = "افشاء و شفافیت";
                break;
            case 290:
                category = "هیئت مدیره";
                break;
            case 291:
                category = " اطلاعات نهانی و اشخاص وابسته";
                break;
            case 292:
                category = " صورت های مالی و حسابرسی";
                break;
            case 293:
                category = " کمیته ها";
                break;
            case 294:
                category = "سایر";
                break;
            default:
                category = "نا مشخص";
                break;
        }
        return category;
    }
    function questionLevelFilterGrid(page = 1) {
        AjaxCallAction("POST", "/api/admin/Corporate/Get_QuestionLevels", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: page, PageSize: $("#cboSelectCount").val(), IsActive: 15 }), false, function (res) {
            if (res.isSuccess) {
                DataFormList = res.data;
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (let i = 0; i < res.data.length; i++) {
                    let category = getCategoryName(res.data[i].categoryId);
                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].levelTitle + "</td><td>" + "<a title='ویرایش' href='/Admin/Corporate/EditQuestionLevel?id=" + res.data[i].questionLevelId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' onclick='Web.Corporate.DeleteQuestionLevel(" + res.data[i].questionLevelId + ");' class='btn btn-danger fontForAllPage'><i class='fa fa-remove'></i></a></td></tr>";
                }
                $("#tBodyList").html(strM);
            }
        }, true);
    }
    function initQuestionLevel(id = null, dir = 'rtl') {
        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("GET", "/api/admin/Corporate/Get_QuestionLevel/" + id, null, true, function (res) {
                if (res != null) {
                    $("#QuestionLevelId").val(res.questionLevelId);
                    $("#LevelTitle").val(res.levelTitle);
                    $("#LevelDescription").val(res.levelDescription);
                }
            }, true);
        }
    }
    function saveQuestionLevel(e) {

        $(e).attr("disabled", "");
        let QuestionLevelId = $("#QuestionLevelId").val();
        let LevelTitle = $("#LevelTitle").val();
        let LevelDescription = $("#LevelDescription").val();
        AjaxCallAction("POST", "/api/admin/Corporate/Save_QuestionLevel", JSON.stringify(
            {
                QuestionLevelId: !isEmpty(QuestionLevelId) ? QuestionLevelId : 0,
                LevelTitle: LevelTitle,
                LevelDescription: LevelDescription,
                isActive: 15,
            }), true, function (res) {

                $(e).removeAttr("disabled");

                if (res.isSuccess) {
                    goToUrl("/Admin/Corporate/QuestionLevel");
                }
                else {
                    alertB("خطا", res.message, "error");
                }
            }, true);
    }
    function deleteQuestionLevel(id) {
        try {
            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {
                AjaxCallAction("GET", "/api/admin/Corporate/Delete_QuestionLevel/" + (isEmpty(id) ? '0' : id),
                    null, true, function (result) {
                        if (result.isSuccess) {
                            questionLevelFilterGrid();
                            alertB("", result.message, "success");
                        }
                        else {
                            alertB("خطا", result.message, "error");
                        }
                    }, true);
            }, function () {
            }, ["خیر", "بلی"]);
        } catch (e) {
        }
    }

    web.Corporate = {
        DataFormFilterGrid: dataFormFilterGrid,
        InitDataForm: initDataForm,
        SaveDataForm: saveDataForm,
        DeleteDataForm: deleteDataForm,

        DataFormQuestionsFilterGrid: dataFormQuestionsFilterGrid,
        InitDataFormQuestions: initDataFormQuestions,
        SaveDataFormQuestions: saveDataFormQuestions,
        GetDataFormQuestionsList: getDataFormQuestionsList,
        DeleteDataFormQuestions: deleteDataFormQuestions,

        DataFormDocumentFilterGrid: dataFormDocumentFilterGrid,
        InitDataFormDocument: initDataFormDocument,
        SaveDataFormDocument: saveDataFormDocument,
        DeleteDataFormDocument: deleteDataFormDocument,

        DataFormQuestionsOptioneFilterGrid: dataFormQuestionsOptioneFilterGrid,
        InitDataFormQuestionsOptione: initDataFormQuestionsOptione,
        SaveDataFormQuestionsOptione: saveDataFormQuestionsOptione,
        DeleteDataFormQuestionsOptione: deleteDataFormQuestionsOptione,

        TextSearchOnKeyDown: textSearchOnKeyDown,
        MakeDataFormComboBoxForSelectSubCategory: makeDataFormComboBoxForSelectSubCategory,

        QuestionLevelFilterGrid: questionLevelFilterGrid,
        InitQuestionLevel: initQuestionLevel,
        SaveQuestionLevel: saveQuestionLevel,
        DeleteQuestionLevel: deleteQuestionLevel,

        PrevLits: go_prev,
        NextList: go_next,
        ClickSortingGrid: clickSortingGrid
    };

})(Web, jQuery);