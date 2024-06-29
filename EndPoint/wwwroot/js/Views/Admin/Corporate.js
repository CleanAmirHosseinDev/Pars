
(function (web, $) {
    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }
    var DataFormQuestionsList = null;
    function getDataFormQuestionsList() {
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataFormQuestionss", JSON.stringify({ PageIndex: 0, PageSize: 0 }), false, function (res) {
            if (res.isSuccess)
                DataFormQuestionsList = res.data;
        }, true);

    }
    function makeDataFormQuestionsList() {
        var strM = '';
        for (var i = 0; i < DataFormQuestionsList.length; i++) {
            let questionText = DataFormQuestionsList[i].questionText.length > 79 ? DataFormQuestionsList[i].questionText.slice(0, 75) + " ..." : DataFormQuestionsList[i].questionText;
            strM += " <option value=" + DataFormQuestionsList[i].dataFormQuestionId + ">" + questionText + "</option>";
        }
        $("#DataFormQuestionsId").html(strM);
    }
    
    var DataFormList = null;
    function dataFormFilterGrid() {
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataForms", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), false, function (res) {
            if (res.isSuccess) {
                DataFormList = res.data;
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (let i = 0 ; i < res.data.length; i++) {
                    strM += "<tr><td>" + i + "</td><td>" + res.data[i].formTitle.slice(0, 75) + "</td><td>" + res.data[i].categoryId + "</td><td><a title='ویرایش' href='/Admin/Corporate/EditDataForm?id=" + res.data[i].formId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";
                }
                $("#tBodyList").html(strM);
            }
        }, true);
    }
    function getObjectWithFormId(object_list, id) {
        return object_list.find(o => o.formId === id);
    }
    function getObjectWithDataFormQuestionsId(object_list, id) {
        return object_list.find(o => o.dataFormQuestionId === parseInt(id));
    }
    function dataFormQuestionsFilterGrid() {
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataForms", JSON.stringify({ PageIndex: 0, PageSize: 0 }), false, function (res) {
            if (res.isSuccess) {
                DataFormList = res.data;
            }
        }, true);
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataFormQuestionss", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {
            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {
                    let type = "";
                    if (res.data[i].questionType == "yesNo")
                    {
                        type = "بله/خیر";
                    }
                    else {
                        type = "لیکرتی";
                    }
                    let dataFormTitle = getObjectWithFormId(DataFormList, res.data[i].dataFormId).formTitle;
                    let questionText = res.data[i].questionText.length > 79 ? res.data[i].questionText.slice(0, 75) + " ..." : res.data[i].questionText;
                    strM += "<tr><td>" + (i + 1) + "</td><td>" + questionText + "</td><td>" + dataFormTitle + "</td><td>" + type + "</td><td>" + res.data[i].score + "</td><td>" + res.data[i].questionOrder + "</td><td><a title='ویرایش' href='/Admin/Corporate/EditDataFormQuestions?id=" + res.data[i].dataFormQuestionId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";
                }
                $("#tBodyList").html(strM);
            }
        }, true);
    }
    function dataFormQuestionsOptioneFilterGrid() {
        getDataFormQuestionsList();
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataFormQuestionsOptiones", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {
            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {
                    let questionText = getObjectWithDataFormQuestionsId(DataFormQuestionsList, res.data[i].dataFormQuestionsId).questionText;
                    questionText = questionText.length > 79 ? questionText.slice(0, 75) + " ..." : questionText;
                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].text + "</td><td>" + questionText + "</td><td>" + res.data[i].ratio + "</td><td><a title='ویرایش' href='/Admin/Corporate/EditDataFormQuestionsOptione?id=" + res.data[i].id + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";
                }
                $("#tBodyList").html(strM);
            }
        }, true);
    }

    function initDataFormQuestions(id = null, dir = 'rtl') {

        ComboBoxWithSearch('.select2', dir);
        makeDataFormComboBoxForSelectSubCategory();
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
                    comboBoxWithSearchUpdateText("QuestionType", type);
                    comboBoxWithSearchUpdateText("DataForms", getObjectWithFormId(DataFormList, res.dataFormId).formTitle);
                    $("#QuestionOrder").val(res.questionOrder);
                    $("#Score").val(res.score);
                    $("#HelpText").val(res.helpText);
                }
            }, true);
        }
    }
    function initDataFormQuestionsOptione(id = null, dir = 'rtl') {
        ComboBoxWithSearch('.select2', dir);
        getDataFormQuestionsList();
        makeDataFormQuestionsList();
        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("GET", "/api/admin/Corporate/Get_DataFormQuestionsOptione/" + id, null, true, function (res) {
                if (res != null) {
                    let questionText = getObjectWithDataFormQuestionsId(DataFormQuestionsList, res.dataFormQuestionsId).questionText;
                    $("#Text").val(res.text);
                    $("#DataFormQuestionsId").val(res.dataFormQuestionsId);
                    $("#DataFormQuestionsText").text(questionText);
                    $("#Ratio").val(res.ratio);
                    questionText = questionText.length > 79 ? questionText.slice(0, 75) + " ..." : questionText;
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
    function initDataForm(id = null, dir = 'rtl') {
        ComboBoxWithSearch('.select2', dir);
        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("GET", "/api/admin/Corporate/Get_DataForm/" + id, null, true, function (res) {
                if (res != null) {
                    $("#FormId").val(res.formId);
                    $("#FormTitle").val(res.formTitle);
                    $("#CategoryId").val(res.categoryId);
                    $("#IsTable").val(res.isTable);
                }
            }, true);
        }
    }
    function saveDataForm(e) {

        $(e).attr("disabled", "");
        let FormId = $("#FormId").val();
        let FormTitle = $("#FormTitle").val();
        let CategoryId = $("#CategoryId").val();
        AjaxCallAction("POST", "/api/admin/Corporate/Save_DataForm", JSON.stringify(
            {
                FormId: !isEmpty(FormId) ? FormId : 0,
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
    function saveDataFormQuestions(e) {

        $(e).attr("disabled", "");
        let DataFormQuestionId = $("#DataFormQuestionId").val();
        let DataFormId = $("#DataForms").val();
        let QuestionText = $("#QuestionText").val();
        let QuestionName = $("#QuestionName").val();
        let QuestionType = $("#QuestionType").val();
        let QuestionOrder = $("#QuestionOrder").val();
        let Score = $("#Score").val();
        let HelpText = $("#HelpText").val();
        AjaxCallAction("POST", "/api/admin/Corporate/Save_DataFormQuestions", JSON.stringify(
            {
                DataFormQuestionId: !isEmpty(DataFormQuestionId) ? DataFormQuestionId : 0,
                DataFormId: !isEmpty(DataFormId) ? DataFormId : 0, 
                QuestionText: QuestionText,
                QuestionName: QuestionName,
                QuestionType: QuestionType,
                QuestionOrder: !isEmpty(QuestionOrder) ? QuestionOrder : 0,
                Score: !isEmpty(Score) ? Score : 0,
                HelpText: HelpText,
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
    function saveDataFormQuestionsOptione(e) {

        $(e).attr("disabled", "");
        let Id = $("#Id").val();
        let Text_ = $("#Text").val();
        let DataFormQuestionsId = $("#DataFormQuestionsId").val();
        let Ratio = $("#Ratio").val();
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
                    alertB("خطا", res.message, "error");
                }
            }, true);
    }
    function makeDataFormComboBoxForSelectSubCategory() {
        let strM = '<option value="">انتخاب کنید</option>';
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataForms", JSON.stringify({ PageIndex: 0, PageSize: 0 }), false, function (res) {
            if (res.isSuccess) {
                DataFormList = res.data;
                // TODO Remove this statement
                strM = '<option value="">انتخاب کنید</option>';
                for (var i = 0; i < res.data.length; i++) {
                    strM += " <option value=" + res.data[i].formId + ">" + res.data[i].formTitle + "</option>";
                }
                $("#DataForms").html(strM);
            }
            else {
                alertB("خطا", res.message, "error");
            }
        }, true);

    }



    web.Corporate = {
        DataFormQuestionsOptioneFilterGrid: dataFormQuestionsOptioneFilterGrid,
        DataFormQuestionsFilterGrid: dataFormQuestionsFilterGrid,
        DataFormFilterGrid: dataFormFilterGrid,
        InitDataFormQuestions: initDataFormQuestions,
        InitDataFormQuestionsOptione: initDataFormQuestionsOptione,
        SaveDataFormQuestionsOptione: saveDataFormQuestionsOptione,
        InitDataForm: initDataForm,
        GetDataFormQuestionsList: getDataFormQuestionsList,
        TextSearchOnKeyDown: textSearchOnKeyDown,
        SaveDataFormQuestions: saveDataFormQuestions,
        SaveDataForm: saveDataForm,
        MakeDataFormComboBoxForSelectSubCategory: makeDataFormComboBoxForSelectSubCategory,
    };

})(Web, jQuery);