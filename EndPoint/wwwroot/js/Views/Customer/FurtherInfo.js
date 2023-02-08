
(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;

    $(".next").click(function () {

        var countform = Number($("#FormLoadCode").val()) + 1;
        $("#FormLoadCode").val(countform);
        intiTab(countform);

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

        var countform = Number($("#FormLoadCode").val()) - 1;
        $("#FormLoadCode").val(countform);

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

    $('.radio-group .radio').click(function () {
        $(this).parent().find('.radio').removeClass('selected');
        $(this).addClass('selected');
    });

    $(".submit").click(function () {
        return false;
    });



    function intiTab(TabId = null) {
        var ID = $("#RequestIdForms").val();

        switch (TabId) {
            case 1:
                intiFormShow(1, "1,2,7,8", ID);
                intiFormQuestion(2, "R", ID);
                intiFormSingelAnswer(2);
                break;
            case 2:
                intiFormShow(3, "1,3,11", ID);
                break;
            case 3:
                intiForm(6);
                intiFormQuestion(25, "A", ID);
                intiFormSingelAnswer(25, ID);
                intiFormShow(16, "1,2,3", ID);
                intiFormShow(17, "1,2", ID);
                break;
            case 4:
                intiForm(11);
                intiFormSingelAnswer(11, ID);
                break;
            case 5:
                intiFormShow(13, "1,2,3", ID);
                break;
            case 7:
                intiFormQuestion(7, "D");
                intiFormSingelAnswer(7, ID);
                intiFormQuestion(18, "D");
                intiFormSingelAnswer(18, ID);
                intiFormQuestion(19, "D");
                intiFormSingelAnswer(18, ID);
                intiFormQuestion(20, "D");
                intiFormSingelAnswer(20, ID);

            case 9:
                intiFormShow(9, '1,2,11', ID);
                intiFormShow(10, '1,11', ID);
                break;
            case 14:

                intiForm(14);
                intiFormSingelAnswer(14, ID);
                intiFormShow(15, '1,2,11', ID);
                intiFormShow(22, '1,11', ID);
                intiFormShow(24, '1,11', ID);
                break;

        }

    }

    function systemSeting_Combo(FormId = null) {

        if (FormId == 1 || FormId == 3 || FormId == 16 || FormId == 9 || FormId == 13) {
            var PC = '';
            switch (FormId) {
                case 1:
                    PC = '5,9,20,30,125';
                    break;
                case 3:
                    PC = '209';
                    break;
                case 16:
                    PC = '9';
                    break;
                case 9:
                    PC = '9';
                    break;
                case 13:
                    PC = '5';
                    break;
            }

            AjaxCallAction("POST", "/api/customer/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: PC, PageIndex: 0, PageSize: 0 }), true, function (res) {

                if (res.isSuccess) {
                    if (FormId == 1) {
                        var strMemberPostID = '<option value="">انتخاب کنید</option>';
                        var strMemberEductionID = '<option value="">انتخاب کنید</option>';
                        var strUniversityID = '<option value="">انتخاب کنید</option>';
                        var strCompanyDocument = 'option value="">انتخاب کنید</option>';

                        for (var i = 0; i < res.data.length; i++) {
                            if (res.data[i].parentCode == 5) {
                                strMemberPostID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                            } else if (res.data[i].parentCode == 9) {
                                strMemberEductionID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                            } else if (res.data[i].parentCode == 20) {
                                strUniversityID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                            }

                        }


                        $("#MemberPostID").html(strMemberPostID);
                        $("#MemberEductionID").html(strMemberEductionID);
                        $("#UniversityID").html(strUniversityID);
                        $("#CompanyDocument").html(strCompanyDocument);

                    }
                    else if (FormId == 3) {
                        var strIsGuideLineOrProcess = '<option value="">انتخاب کنید</option>';

                        for (var i = 0; i < res.data.length; i++) {
                            strIsGuideLineOrProcess += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                        }

                        $("#IsGuideLineOrProcess").html(strIsGuideLineOrProcess);

                    }
                    else if (FormId == 16 || FormId == 9) {
                        var strDegreeOfEducation = strDegreeOfEducation2 = '<option value="">انتخاب کنید</option>';

                        for (var i = 0; i < res.data.length; i++) {
                            strDegreeOfEducation += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                        }

                        $("#DegreeOfEducation").html(strDegreeOfEducation);
                        $("#DegreeOfEducation2").html(strDegreeOfEducation);
                    }
                    else if (FormId == 13) {
                        var strPostInSociety = '<option value="">انتخاب کنید</option>';

                        for (var i = 0; i < res.data.length; i++) {
                            strPostInSociety += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                        }

                        $("#PostInSociety").html(strPostInSociety);

                    }
                }
            }, true);

            ComboBoxWithSearch('.select2', 'rtl');
        }
    }

    //function getCustomer() {

    //    AjaxCallAction("GET", "/api/customer/Customers/Get_Customers/", null, true, function (res) {
    //        if (res != null) {               
    //            $("#CanSeeFurtherInfo").val(res.tel);
    //        }
    //    }, true);
    //}

    function initFurtherInfo(id = null) {
        $("#RequestIdForms").val(id);
        intiTab(1);
    }

    function intiFormShow(Id = null, Columns = null, RequestId = null) {

        intiForm(Id, RequestId);
        intiFormAnswer(Id, Columns, RequestId);

    }


    function intiForm(FormID = null, RequestId = null) {

        AjaxCallAction("POST", "/api/customer/FurtherInfo/Get_DataFormQuestionss", JSON.stringify({ DataFormId: FormID, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {

                var strFormId = '';
                var Filename = 1;
                for (var i = 0; i < res.data.length; i++) {
                    if (i == 0) {
                        strFormId += "<Input type='hidden' id='RequestId' name='RequestId' value='" + RequestId + "'/>";
                    }

                    if (i == 0) {
                        strFormId += "<div class='col-lg-6'>";
                    } else if (i == Math.round((res.data.length) / 2) && res.data.length > 1) {
                        strFormId += "</div><div class='col-lg-6'>";
                    }

                    strFormId += "<div class='form-group'><div class='col-md-12' style='margin-bottom:10px'><label class='control-label'  for=''>" + res.data[i].questionText + "<span class='RequiredLabel'>*</span></label>";
                    if (res.data[i].questionType == 'select') {
                        strFormId += "<select name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' class='form-control select2' ></select>";
                    } else if (res.data[i].questionType == 'textarea') {
                        strFormId += "<textarea name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' class='form-control' ></textarea>";
                    }
                    else if (res.data[i].questionType == 'checkbox') {
                        strFormId += "<input type='" + res.data[i].questionType + "' name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' style='text-align:right; width: 30px' />";
                    } else if (res.data[i].questionType == 'file') {
                        strFormId += "<input type='" + res.data[i].questionType + "' name='Result_Final_FileName" + Filename + "' id='" + res.data[i].questionName + "' /><div id='Div" + res.data[i].questionName + "'></div>";
                        Filename++;
                    }

                    else {
                        strFormId += "<input type='" + res.data[i].questionType + "' name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' placeholder='" + res.data[i].questionText + "' class='form-control' />";
                    }
                    strFormId += "</div>";
                    strFormId += "</div>";
                    if (i == res.data.length && res.data.length > 1) {
                        strFormId += "</div>";
                    }
                }
                $("#FormDetail" + FormID).html(strFormId);
                systemSeting_Combo(FormID);
            }
        }, true);
    }


    function intiFormQuestion(FormID = null, Name = null, RequestId = null) {

        AjaxCallAction("POST", "/api/customer/FurtherInfo/Get_DataFormQuestionss", JSON.stringify({ DataFormId: FormID, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {

                var strFormId = '';
                var Filename = 1;

                for (var i = 0; i < res.data.length; i++) {

                    if (i == 0) {
                        strFormId += "<div class='col-lg-6'>";
                    } else if (i == Math.round((res.data.length) / 2) && res.data.length > 1) {
                        strFormId += "</div><div class='col-lg-6'>";
                    }

                    strFormId += "<div class='form-group'><div class='col-md-12' style='margin-bottom:10px'><form id='frmFrom" + Name + (i) + "'> <input type='hidden' id='FormID' name='FormID' value='" + FormID + "' />";
                    strFormId += "<input type='hidden' id='DataFormQuestionID' name='DataFormQuestionID' value='" + res.data[i].dataFormQuestionId + "' />";

                    strFormId += "<Input type='hidden' id='RequestId' name='RequestId' value='" + RequestId + "'/>";


                    strFormId += "<label class='control-label'  for=''>" + res.data[i].questionText + "<span class='RequiredLabel'>*</span></label>";
                    if (res.data[i].questionType == 'select') {
                        strFormId += "<select name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' class='form-control select2' ></select>";
                    } else if (res.data[i].questionType == 'textarea') {
                        strFormId += "<textarea name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' class='form-control' ></textarea>";
                    }
                    else if (res.data[i].questionType == 'checkbox') {
                        strFormId += "<input type='" + res.data[i].questionType + "' name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' style='text-align:right; width: 30px' />";
                    } else if (res.data[i].questionType == 'file') {
                        strFormId += "<input type='" + res.data[i].questionType + "' name='Result_Final_FileName" + Filename + "' id='" + res.data[i].questionName + "' /><div id='Div" + res.data[i].questionName + "'></div>";

                    }

                    else {
                        strFormId += "<input type='" + res.data[i].questionType + "' name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' placeholder='" + res.data[i].questionText + "' class='form-control' />";
                    }
                    strFormId += "<button title='ثبت' class='btn btn-default fontForAllPage' type='button' onclick='Web.FurtherInfo.SavesingleForm(this," + '"' + Name + (i) + '"' + ")'> ثبت </button ></form>";

                    strFormId += "</div>";
                    strFormId += "</div>";
                    if (i == res.data.length && res.data.length > 1) {
                        strFormId += "</div>";
                    }


                }
                $("#FormDetail" + FormID).html(strFormId);
                systemSeting_Combo(FormID);
            }
        }, true);
    }

    function intiFormSingelAnswer(FormID = null, Id = null) {
        AjaxCallAction("POST", "/api/customer/FurtherInfo/Get_DataFromAnswerss", JSON.stringify({ FormId: FormID, RequestId: Id, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                if (FormID == 14) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 87:
                                $("#IsPublicActivityFile").prop("checked", res.data[i].answer == "false" ? false : true);
                                break;
                            case 91:
                                $("#Investment").val(res.data[i].answer);
                                break;
                            case 94:
                                $("#EmploymentDisabled").val(res.data[i].answer);
                                break;
                        }
                    }

                }
                if (FormID == 11) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 79:
                                $("#DivHumanResourcesFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                break;

                        }
                    }

                }
                if (FormID == 7) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 47:
                                $("#DivVc1").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 48:
                                $("#DivVc2").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 49:
                                $("#DivVc3").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 50:
                                $("#DivVc4").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;

                        }
                    }

                }
                if (FormID == 18) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 51:
                                $("#DivVc5").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 52:
                                $("#DivVc6").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 53:
                                $("#DivVc7").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 54:
                                $("#DivVc8").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 55:
                                $("#DivVc9").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 56:
                                $("#DivVc10").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 57:
                                $("#DivVc11").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 58:
                                $("#DivVc12").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;

                        }
                    }

                }
                if (FormID == 19) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 59:
                                $("#DivVc13").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 60:
                                $("#DivVc14").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 61:
                                $("#DivVc15").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 62:
                                $("#DivVc16").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 63:
                                $("#DivVc17").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 64:
                                $("#DivVc18").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 65:
                                $("#DivVc19").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 66:
                                $("#DivVc20").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 67:
                                $("#DivVc21").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;

                        }
                    }

                }
                if (FormID == 20) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 68:
                                $("#DivVc22").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 69:
                                $("#DivVc23").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 70:
                                $("#DivVc24").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 71:
                                $("#DivVc25").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                        }
                    }

                }
                if (FormID == 25) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 28:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivOrganazationChart").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                }
                                break;
                            case 29:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivOrganizationalDuties").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                }
                                //  $("#DivOrganizationalDuties").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 30:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivRiskManagementGuidelines").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                }
                                // $("#DivRiskManagementGuidelines").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 31:
                                $("#DivTransactionRegulations").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 31:
                                $("#DivTransactionRegulations").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 32:
                                $("#DivDeductionTaxAccount").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 33:
                                $("#DivCrmSoftwareContract").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 35:
                                $("#DivRepresentativeFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 36:
                                $("#DivletterOfCommendation").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 37:
                                $("#DivInovationFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 40:
                                $("#DivProceedings").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            //
                        }
                    }

                }
                if (FormID == 2) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 97:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivLastAuditingTaxList").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                }
                                // $("#DivLastAuditingTaxList").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 98:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivLastChangeOfficialNewspaper").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                }
                                // $("#DivLastChangeOfficialNewspaper").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 99:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivstatuteDoc").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                }
                                // $("#DivstatuteDoc").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 100:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivOfficialNewspaper").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                }
                                // $("#DivOfficialNewspaper").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;

                        }
                    }

                }
            }

        }, true);
    }

    function intiFormAnswer(FormID = null, ColumNum = null, RequestId = null) {

        AjaxCallAction("POST", "/api/customer/FurtherInfo/Get_DataFormAnswerTabless", JSON.stringify({ FormId: FormID, RequestId: RequestId, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {

                var separatedArray = ColumNum.split(',');
                let strFormAnswer = '';
                for (let i = 0; i < res.data.length; i++) {

                    strFormAnswer += "<tr><td>" + (i + 1) + "</td><td>";

                    for (var j = 0; j < separatedArray.length; j++) {

                        switch (separatedArray[j]) {
                            case "1":
                                strFormAnswer += res.data[i].answer1 + "</td><td>";
                                break;
                            case "2":
                                strFormAnswer += res.data[i].answer2 + "</td><td>";
                                break;
                            case "3":
                                strFormAnswer += res.data[i].answer3 + "</td><td>";
                                break;
                            case "4":
                                strFormAnswer += res.data[i].answer4 + "</td><td>";
                                break;
                            case "5":
                                strFormAnswer += res.data[i].answer5 + "</td><td>";
                                break;
                            case "6":
                                strFormAnswer += res.data[i].answer6 + "</td><td>";
                                break;
                            case "7":
                                strFormAnswer += res.data[i].answer7 + "</td><td>";
                                break;
                            case "8":
                                strFormAnswer += res.data[i].answer8 + "</td><td>";
                                break;
                            case "9":
                                strFormAnswer += res.data[i].answer6 + "</td><td>";
                                break;
                            case "10":
                                strFormAnswer += res.data[i].answer6 + "</td><td>";
                                break;
                            case "11":
                                strFormAnswer += "<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>" + "</td><td>";
                                break;
                            case "12":
                                strFormAnswer += "<a href='/File/Download?path=" + res.data[i].fileName2Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>" + "</td><td>";
                                break;
                        }
                    }

                    strFormAnswer += "<button title='ویرایش' class='btn btn-warring fontForAllPage' type='button' onclick='Web.FurtherInfo.GetFormAnswer(" + FormID + "," + res.data[i].answerTableId + ")' ><i class='fa fa-edit'></i></button><button style='margin-right:5px' type='button' title='حذف' class='btn btn-warrnig fontForAllPage' onclick='Web.FurtherInfo.Delete_DataFormAnswerTables(" + FormID + ',"' + ColumNum + '",' + res.data[i].answerTableId + ");'><i class='fa fa-remove'></i></button></td></tr>";
                }
                $("#FormDetailAnswer" + FormID).html(strFormAnswer);
            }
        }, true);
    }

    function getFormAnswer(FormID = null, AnswerTableId = null) {
        var RequestId = $("#RequestIdForms").val();

        AjaxCallAction("POST", "/api/customer/FurtherInfo/Get_DataFormAnswerTabless", JSON.stringify({ FormId: FormID, AnswerTableId: AnswerTableId, RequestId: RequestId, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {

                // $("#RequestId").val(res.data[0].requestId);
                for (var i = 0; i < res.data.length; i++) {

                    $('#frmFrom' + FormID).find('input[name=AnswerTableId]').val(res.data[0].answerTableId);
                    if (FormID == 1) {

                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);

                        $('#MemberPostID').val(res.data[0].answer2Val);
                        comboBoxWithSearchUpdateText("MemberPostID", res.data[0].answer2);

                        $('#MemberEductionID').val(res.data[0].answer3Val);
                        comboBoxWithSearchUpdateText("MemberEductionID", res.data[0].answer3);

                        $('#UniversityID').val(res.data[0].answer4Val);
                        comboBoxWithSearchUpdateText("UniversityID", res.data[0].answer4);

                        $("#IsMemeberOfBoard").prop("checked", res.data[0].answer5);

                        $('#frmFrom' + FormID).find('input[name=Answer6]').val(res.data[0].answer6);
                        $('#frmFrom' + FormID).find('input[name=Answer7]').val(res.data[0].answer7);
                        $('#frmFrom' + FormID).find('input[name=Answer8]').val(res.data[0].answer8);
                        $('#frmFrom' + FormID).find('input[name=Answer9]').val(res.data[0].answer9);
                        $('#frmFrom' + FormID).find('input[name=Answer10]').val(res.data[0].answer10);
                        if (res.data[0].fileName1Full != null) {
                            $("#DivAcademicDegreePicture").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#btnform1").html("ویرایش");
                    }
                    else if (FormID == 3) {
                        //1,3,11
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);

                        $('#IsGuideLineOrProcess').val(res.data[0].answer3Val);
                        comboBoxWithSearchUpdateText("IsGuideLineOrProcess", res.data[0].answer3);

                        if (res.data[0].fileName1Full != null) {
                            $("#DivGuidelinesAndRegulationsFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#btnform3").html("ویرایش");

                    } else if (FormID == 4) {

                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer3]').val(res.data[0].answer3);
                        $('#frmFrom' + FormID).find('input[name=Answer4]').val(res.data[0].answer4);
                        if (res.data[0].fileName1Full != null) {
                            $("#DivCertificateAndPermitAndStandardFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#btnform4").html("ویرایش");
                    }
                    else if (FormID == 5) {

                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer3]').val(res.data[0].answer3);
                        $('#frmFrom' + FormID).find('input[name=Answer4]').val(res.data[0].answer4);
                        if (res.data[0].fileName1Full != null) {
                            $("#DivAwardsFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#btnform5").html("ویرایش");
                    }

                    else if (FormID == 8) {
                        if (res.data[0].fileName1Full != null) {
                            $("#DivSwotFile1").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        if (res.data[0].fileName2Full != null) {
                            $("#DivSwotFile2").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#btnform8").html("ویرایش");
                    }
                    else if (FormID == 9) {
                        //1,2,11
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#DegreeOfEducation2').val(res.data[0].answer2Val);
                        comboBoxWithSearchUpdateText("DegreeOfEducation2", res.data[0].answer2);
                        if (res.data[0].fileName1Full != null) {
                            $("#DivDegreeFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#btnform9").html("ویرایش");
                    }
                    else if (FormID == 10) {
                        //1,2,11
                        $('#ReportTitle').val(res.data[0].answer1);

                        if (res.data[0].fileName1Full != null) {
                            $("#DivReportFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#btnform10").html("ویرایش");
                    }
                    else if (FormID == 12) {
                        //1,2,11
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer2]').val(res.data[0].answer2);
                        $('#frmFrom' + FormID).find('input[name=Answer3]').val(res.data[0].answer3);
                        $('#frmFrom' + FormID).find('input[name=Answer4]').val(res.data[0].answer4);
                        $("#btnform12").html("ویرایش");
                    }
                    else if (FormID == 13) {
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer2]').val(res.data[0].answer2);
                        $('#PostInSociety').val(res.data[0].answer3Val);
                        comboBoxWithSearchUpdateText("PostInSociety", res.data[0].answer3);
                        $("#btnform13").html("ویرایش");
                    }
                    else if (FormID == 15) {
                        //1,2,11
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer2]').val(res.data[0].answer2);
                        if (res.data[0].fileName1Full != null) {
                            $("#DivPublicActivityFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#btnform15").html("ویرایش");
                    }
                    else if (FormID == 16) {
                        //1,2,3
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#DegreeOfEducation').val(res.data[0].answer2Val);
                        comboBoxWithSearchUpdateText("DegreeOfEducation", res.data[0].answer2);
                        $('#frmFrom' + FormID).find('input[name=Answer3]').val(res.data[0].answer3);
                        $("#btnform16").html("ویرایش");
                    }

                    else if (FormID == 17) {
                        //6,'1,2
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer2]').val(res.data[0].answer2);
                        $("#btnform17").html("ویرایش");

                    } else if (FormID == 22) {
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        if (res.data[0].fileName1Full != null) {
                            $("#DivInvestmentFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#btnform22").html("ویرایش");
                    }
                    else if (FormID == 24) {
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        if (res.data[0].fileName1Full != null) {
                            $("#DivEmploymentDisabledFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#btnform24").html("ویرایش");
                    }


                }

            }
        }, true);
    }


    function delete_DataFormAnswerTables(FormID = null, ColumNum = null, AnswerTableId = null) {


        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/customer/FurtherInfo/Delete_DataFormAnswerTables/" + (isEmpty(AnswerTableId) ? '0' : AnswerTableId), null, true, function (result) {

                    debuggerWeb();

                    if (result.isSuccess) {

                        intiFormShow(FormID, ColumNum, $("#RequestIdForms").val());
                        $('#frmFrom' + FormID).find('input[name=AnswerTableId]').val('0');
                        alertB("", result.message, "success");
                        $("#btnform" + FormID).html('افزودن');

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



    function saveForm(e, FormId = null, ColumnNum = null) {

        $(e).attr("disabled", "");

        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/FurtherInfo/Save_DataFormAnswerTabless", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFrom" + FormId), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {
                $('#frmFrom' + FormId).find('input[name=AnswerTableId]').val('0');
                if (FormId == 1) {
                    initFurtherInfo($("#RequestIdForms").val());
                } else {

                    intiFormShow(FormId, ColumnNum, $("#RequestIdForms").val());
                    $(e).html('افزودن');
                }

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function savesingleForm(e, FormId = null) {

        $(e).attr("disabled", "");

        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/FurtherInfo/Save_DataFromAnswers", fill_AjaxCallActionPostSaveFormWithUploadFile("frmFrom" + FormId), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {
                intiFormSingelAnswer(FormId, $("#RequestIdForms").val());
                alertB("ثبت", "اطلاعات ثبت شد", "success");
            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function saveFormDetailTab14(e) {
        try {

            saveSingelAnswerForm(e, 14, String($("#IsPublicActivityFile").prop('checked')), 87);
            saveSingelAnswerForm(e, 14, $('#EmploymentDisabled').val(), 94);
            saveSingelAnswerForm(e, 14, $('#Investment').val(), 91);
            alertB("ثبت", "اطلاعات ثبت شد", "success");
        } catch (e) {

        }

    }


    function saveForm25(e) {
        saveSingelAnswerForm(e, 6, String($("#HaveRepresentative").prop('checked')), 34);
        saveSingelAnswerForm(e, 6, $('#CompanyWebSite').val(), 94);
        saveSingelAnswerForm(e, 6, $('#HighProductKnowledge').val(), 38);
        saveSingelAnswerForm(e, 6, String($("#HaveAuditCommittee").prop('checked')), 39);
        alertB("ثبت", "اطلاعات ثبت شد", "success");

    }

    function saveSingelAnswerForm(e, formId = null, answer = null, dataFormQuestionId = null) {

        $(e).attr("disabled", "");
        var requestId = $("#RequestId").val();
        AjaxCallAction("POST", "/api/customer/FurtherInfo/Save_DataFromAnswers", JSON.stringify({
            Answer: answer,
            FormID: formId,
            RequestId: requestId,
            DataFormQuestionID: dataFormQuestionId,

        }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                //  alertB("ثبت", res.message, "success");
            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    web.FurtherInfo = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        InitFurtherInfo: initFurtherInfo,
        SystemSeting_Combo: systemSeting_Combo,
        IntiForm: intiForm,
        IntiFormAnswer: intiFormAnswer,
        IntiFormShow: intiFormShow,
        IntiTab: intiTab,
        IntiFormSingelAnswer: intiFormSingelAnswer,
        SaveForm: saveForm,
        SaveSingelAnswerForm: saveSingelAnswerForm,
        SaveFormDetailTab14: saveFormDetailTab14,
        GetFormAnswer: getFormAnswer,
        Delete_DataFormAnswerTables: delete_DataFormAnswerTables,
        SavesingleForm: savesingleForm,
        IntiFormQuestion: intiFormQuestion,
        SaveForm25: saveForm25,

    };

})(Web, jQuery);