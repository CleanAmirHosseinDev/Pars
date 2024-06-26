
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
    
    function dataFormQuestionsFilterGrid() {
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
                    strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].questionText.slice(0, 75) + " ..." + "</td><td>" + res.data[i].dataFormId + "</td><td>" + type + "</td><td>" + res.data[i].score + "</td><td>" + res.data[i].questionOrder + "</td><td><a title='ویرایش' href='/Admin/Corporate/EditDataFormQuestions?id=" + res.data[i].dataFormQuestionId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";
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

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/Corporate/Get_DataFormQuestions/" + id, null, true, function (res) {
                console.log(res);
                if (res != null) {
                    $("#DataFormQuestionId").val(res.dataFormQuestionId);
                    $("#DataFormId").val(res.dataFormId);
                    $("#QuestionText").val(res.questionText);
                    $("#QuestionName").val(res.questionName);
                    $("#QuestionType").val(res.questionType);
                    //comboBoxWithSearchUpdateText("#QuestionType", res.data[i].questionType)
                    $("#QuestionOrder").val(res.questionOrder);
                    $("#Score").val(res.score);
                    $("#HelpText").val(res.helpText);
                }
            }, true);
        }
    }
    function saveDataFormQuestions(e) {

        //$(e).attr("disabled", "");
        let DataFormQuestionId = $("#DataFormQuestionId").val();
        let DataFormId = $("#DataFormId").val();
        let QuestionText = $("#QuestionText").val();
        let QuestionName = $("#QuestionName").val();
        let QuestionType = $("#QuestionType").val();
        let QuestionOrder = $("#QuestionOrder").val();
        let Score = $("#Score").val();
        let HelpText = $("#HelpText").val();
        AjaxCallAction("POST", "/api/admin/Corporate/Save_DataFormQuestions", JSON.stringify(
            {
                DataFormQuestionId: !isEmpty(DataFormQuestionId) ? 0,
                DataFormId: !isEmpty(DataFormId) ? DataFormId : 0, 
                QuestionText: !isEmpty(QuestionText) ? QuestionText : "",
                QuestionName: !isEmpty(QuestionName) ? QuestionName : "",
                QuestionType: !isEmpty(QuestionType) ? QuestionType : "",
                QuestionOrder: !isEmpty(QuestionOrder) ? QuestionOrder : 0,
                Score: !isEmpty(Score) ? Score : 0,
                HelpText: !isEmpty(HelpText) ? HelpText : "",
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
    

    web.Corporate = {
        DataFormQuestionsOptioneFilterGrid: dataFormQuestionsOptioneFilterGrid,
        DataFormQuestionsFilterGrid: dataFormQuestionsFilterGrid,
        InitdataFormQuestions: initDataFormQuestions,
        GetDataFormQuestionsList: getDataFormQuestionsList,
        TextSearchOnKeyDown: textSearchOnKeyDown,
        SaveDataFormQuestions: saveDataFormQuestions,
    };

})(Web, jQuery);