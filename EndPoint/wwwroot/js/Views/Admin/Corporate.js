
(function (web, $) {
    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function getDataFormQuestionsList() {
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataFormQuestionss", JSON.stringify({ PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                for (var i = 0; i < res.data.length; i++) {
                    strM += " <option value=" + res.data[i].stateId + ">" + res.data[i].stateName + "</option>";
                }
                $("#StateId").html(strM);
            }
        }, true);

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
                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].questionText.slice(0, 75) + " ..." + "</td><td>" + dataFormTitle + "</td><td>" + type + "</td><td>" + res.data[i].score + "</td><td>" + res.data[i].questionOrder + "</td><td><a title='ویرایش' href='/Admin/Corporate/EditDataFormQuestions?id=" + res.data[i].dataFormQuestionId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";
                }
                $("#tBodyList").html(strM);
            }
        }, true);
    }
    function dataFormQuestionsOptioneFilterGrid() {
        AjaxCallAction("POST", "/api/admin/Corporate/Get_DataFormQuestionsOptiones", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val() }), true, function (res) {
            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");
                var strM = '';
                for (var i = 0; i < res.data.length; i++) {
                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].text + "</td><td>" + res.data[i].dataFormQuestionsId + "</td><td>" + res.data[i].ratio + "</td><td><a title='ویرایش' href='/Admin/Corporate/EditDataFormQuestionsOptione?id=" + res.data[i].id + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";
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
        InitDataForm: initDataForm,
        GetDataFormQuestionsList: getDataFormQuestionsList,
        TextSearchOnKeyDown: textSearchOnKeyDown,
        SaveDataFormQuestions: saveDataFormQuestions,
        SaveDataForm: saveDataForm,
        MakeDataFormComboBoxForSelectSubCategory: makeDataFormComboBoxForSelectSubCategory,
    };

})(Web, jQuery);