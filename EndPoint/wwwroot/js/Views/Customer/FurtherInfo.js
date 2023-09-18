
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
                getFurtherInfo(ID);
                intiFormShow(26, '1,2,3,4,11,12', ID);
                
                break;
            case 2:
                intiFormShow(3, "1,3,11", ID);
                
                break;
            case 3:
                getCorporateGovernance(ID);
                intiFormShow(16, "1,2,3", ID);
                intiFormShow(17, "1,2,3", ID);
               
                break;
            case 4:
                intiForm(11);
                intiFormSingelAnswer(11, ID);
               
                break;
            case 5:
                intiFormShow(13, "1,2,3,11", ID);
              
                break;
            case 7:
                getValueChain(ID);
                //intiFormQuestion(7, "D");
                //intiFormSingelAnswer(7, ID);
                //intiFormQuestion(18, "D");
                //intiFormSingelAnswer(18, ID);
                //intiFormQuestion(19, "D");
                //intiFormSingelAnswer(18, ID);
                //intiFormQuestion(20, "D");
                //intiFormSingelAnswer(20, ID);
                
            case 9:
                intiFormShow(9, '1,2,11', ID);
                intiFormShow(10, '1,11', ID);
                
                break;
            case 14:

                //intiForm(14);
                //intiFormSingelAnswer(14, ID);
                getPublicActivities(ID);
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
                    PC = '9,20,30,125';
                    break;
                case 3:
                    PC = '250';
                    break;
                case 16:
                    PC = '9';
                    break;
                case 9:
                    PC = '9';
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
                            if (res.data[i].parentCode == 9) {
                                strMemberEductionID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                            } else if (res.data[i].parentCode == 20) {
                                strUniversityID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                            }

                        }
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
        PersianDatePicker(".DatePicker");
        $("#RequestIdForms").val(id);
        initReferral(id);        
        intiTab(1);
        initCustomer();
    }

    function intiFormShow(Id = null, Columns = null, RequestId = null) {

        intiForm(Id, RequestId);
        intiFormAnswer(Id, Columns, RequestId);


    }


    function intiForm(FormID = null, RequestId = null) {

        //AjaxCallAction("POST", "/api/customer/FurtherInfo/Get_DataFormQuestionss", JSON.stringify({ DataFormId: FormID, PageIndex: 0, PageSize: 0 }), true, function (res) {

        //    if (res.isSuccess) {

        //        var strFormId = '';
        //        var Filename = 1;
        //        for (var i = 0; i < res.data.length; i++) {
        //            if (i == 0) {
        //                strFormId += "<Input type='hidden' id='RequestId' name='RequestId' value='" + RequestId + "'/>";
        //            }

        //            if (i == 0) {
        //                strFormId += "<div class='col-lg-6'>";
        //            } else if (i == Math.round((res.data.length) / 2) && res.data.length > 1) {
        //                strFormId += "</div><div class='col-lg-6'>";
        //            }

        //            strFormId += "<div class='form-group'><div class='col-md-12' style='margin-bottom:10px'><label class='control-label'  for=''>" + res.data[i].questionText + "<span class='RequiredLabel'>*</span></label>";
        //            if (res.data[i].questionType == 'select') {
        //                strFormId += "<select name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' class='form-control select2' ></select>";
        //            } else if (res.data[i].questionType == 'textarea') {
        //                strFormId += "<textarea name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' class='form-control' ></textarea>";
        //            }
        //            else if (res.data[i].questionType == 'checkbox') {
        //                strFormId += "<input type='" + res.data[i].questionType + "' name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' style='text-align:right; width: 30px' />";
        //            } else if (res.data[i].questionType == 'file') {
        //                strFormId += "<input type='" + res.data[i].questionType + "' name='Result_Final_FileName" + Filename + "' id='" + res.data[i].questionName + "' /><div id='Div" + res.data[i].questionName + "'></div>";
        //                Filename++;
        //            }

        //            else {
        //                strFormId += "<input type='" + res.data[i].questionType + "' name='Answer" + res.data[i].questionOrder + "' id='" + res.data[i].questionName + "' placeholder='" + res.data[i].questionText + "' class='form-control' />";
        //            }
        //            strFormId += "</div>";
        //            strFormId += "</div>";
        //            if (i == res.data.length && res.data.length > 1) {
        //                strFormId += "</div>";
        //            }
        //        }
        //        $("#FormDetail" + FormID).html(strFormId);
           systemSeting_Combo(FormID);
        //    }
        //}, true);
    }

    function intiFormQrid(FormID = null, Name = null, RequestId = null) {

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

    function getCorporateGovernance(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/customer/FurtherInfo/Get_CorporateGovernances/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {
                    $("#CorporateGovernanceId").val(res.data.corporateGovernanceId);
                    $("#RequestId").val(res.data.requestId);
                    $("#CompanyWebSite").val(res.data.companyWebSite);
                    $("#HighProductKnowledge").val(res.data.highProductKnowledge);
                    $("#HaveRepresentativeStr").prop("checked", res.data.haveRepresentative);
                   // $("#HaveRepresentative").val(res.data.haveRepresentative);
                    $("#HaveAuditCommitteeStr").prop("checked",res.data.haveAuditCommittee);
                   
                    if (res.data.organazationChart != null && res.data.organazationChart != "") {
                        $("#DivOrganazationChart").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.organazationChartFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.organizationalDuties != null && res.data.organizationalDuties != "") {
                        $("#DivOrganizationalDuties").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.organizationalDutiesFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.riskManagementGuidelines != null && res.data.riskManagementGuidelines != "") {
                        $("#DivRiskManagementGuidelines").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.riskManagementGuidelinesFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.transactionRegulations != null && res.data.transactionRegulations != "") {
                        $("#DivTransactionRegulations").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.transactionRegulationsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.deductionTaxAccount != null && res.data.deductionTaxAccount != "") {
                        $("#DivDeductionTaxAccount").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.deductionTaxAccountFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.crmSoftwareContract != null && res.data.crmSoftwareContract != "") {
                        $("#DivCrmSoftwareContract").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.crmSoftwareContractFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.crmSoftwareContract != null && res.data.crmSoftwareContract != "") {
                        $("#DivCrmSoftwareContract").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.crmSoftwareContractFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.representativeFile != null && res.data.representativeFile != "") {
                        $("#DivRepresentativeFile").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.representativeFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.letterOfCommendation != null && res.data.letterOfCommendation != "") {
                        $("#DivLetterOfCommendation").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.letterOfCommendationFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.proceedings != null && res.data.proceedings != "") {
                        $("#DivProceedings").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.proceedingsFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.inovationFile != null && res.data.inovationFile != "") {
                        $("#DivInovationFile").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.inovationFileFull + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    
                }

            }, true);
        }
    }

    function getValueChain(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/customer/FurtherInfo/Get_ValueChain/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {
                    $("#ValueChainId").val(res.data.valueChainId);
                    $("#ValueChainId1").val(res.data.valueChainId);
                    $("#ValueChainId2").val(res.data.valueChainId);
                    $("#ValueChainId3").val(res.data.valueChainId);
                    $("#RequestId").val(res.data.requestId);
                   

                    if (res.data.vc1 != null && res.data.vc1 != "") {
                        $("#DivVc1").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }

                    if (res.data.vc2 != null && res.data.vc2 != "") {
                        $("#DivVc2").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc2Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }

                    if (res.data.vc3 != null && res.data.vc3 != "") {
                        $("#DivVc3").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc3Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }

                    if (res.data.vc4 != null && res.data.vc4 != "") {
                        $("#DivVc4").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc4Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }

                    if (res.data.vc4 != null && res.data.vc4 != "") {
                        $("#DivVc4").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc4Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc5 != null && res.data.vc5 != "") {
                        $("#DivVc5").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc5Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc6 != null && res.data.vc6 != "") {
                        $("#DivVc6").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc6Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc7 != null && res.data.vc7 != "") {
                        $("#DivVc7").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc7Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc8 != null && res.data.vc8 != "") {
                        $("#DivVc8").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc8Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc9 != null && res.data.vc9 != "") {
                        $("#DivVc9").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc9Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc10 != null && res.data.vc10 != "") {
                        $("#DivVc10").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc10Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc11 != null && res.data.vc11 != "") {
                        $("#DivVc11").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc11Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc12 != null && res.data.vc12 != "") {
                        $("#DivVc12").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc12Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc13 != null && res.data.vc13 != "") {
                        $("#DivVc13").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc13Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc14 != null && res.data.vc14 != "") {
                        $("#DivVc14").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc14Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc15 != null && res.data.vc15 != "") {
                        $("#DivVc15").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc15Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc16 != null && res.data.vc16 != "") {
                        $("#DivVc16").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc16Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc17 != null && res.data.vc17 != "") {
                        $("#DivVc17").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc17Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc18 != null && res.data.vc18 != "") {
                        $("#DivVc18").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc18Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc19 != null && res.data.vc19 != "") {
                        $("#DivVc19").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc19Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc20 != null && res.data.vc20 != "") {
                        $("#DivVc20").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc20Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc21 != null && res.data.vc21 != "") {
                        $("#DivVc21").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc21Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc22 != null && res.data.vc22 != "") {
                        $("#DivVc22").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc22Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc23 != null && res.data.vc23 != "") {
                        $("#DivVc23").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc23Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc24 != null && res.data.vc24 != "") {
                        $("#DivVc24").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc24Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }
                    if (res.data.vc25 != null && res.data.vc25!= "") {
                        $("#DivVc25").html("<a class='btn btn-info' href='/File/Download?path=" + res.data.vc25Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                    }

                }

            }, true);
        }
    }



    function getFurtherInfo(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/customer/FurtherInfo/Get_FurtherInfo/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {

                    $("#FurtherInfoId").val(res.data.furtherInfoId);

                    if (res.data.lastAuditingTaxList != null && res.data.lastAuditingTaxList != "") {
                        $("#divDownloadLastAuditingTaxList").html(GetFullFilePath(res.data.lastAuditingTaxListFull));
                    }
                    if (res.data.statementTaxList != null && res.data.statementTaxList != "") {
                        $("#divDownloadStatementTaxList").html(GetFullFilePath(res.data.statementTaxListFull));
                    }
                    if (res.data.lastChangeOfficialNewspaper != null && res.data.lastChangeOfficialNewspaper != "") {
                        $("#divDownloadLastChangeOfficialNewspaper").html(GetFullFilePath(res.data.lastChangeOfficialNewspaperFull));
                    }
                    if (res.data.statuteDoc != null && res.data.statuteDoc != "") {
                        $("#divDownloadStatuteDoc").html(GetFullFilePath(res.data.statuteDocFull));
                    }
                    if (res.data.officialNewspaper != null && res.data.officialNewspaper != "") {
                        $("#divDownloadOfficialNewspaper").html(GetFullFilePath(res.data.officialNewspaperFull));
                    }
                }

            }, true);
        }
    }


    function getPublicActivities(id = null) {

        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/customer/FurtherInfo/Get_PublicActivities/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

                if (res.isSuccess) {

                    $("#IsPublicActivityFileStr").prop("checked", res.data.isPublicActivityFile);
                    $("#Investment").val(res.data.investment);
                    $("#EmploymentDisabled").val(res.data.employmentDisabled);
                    $("#PublicActivitiesID").val(res.data.publicActivitiesID);
                   
                }

            }, true);
        }
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
                                if (res.data[i].fileName != "" && res.data[i].fileName!=null) {
                                    $("#DivHumanResourcesFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivHumanResourcesFile").html("");
                                }
                                break;
                        }
                    }

                }
                if (FormID == 7) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 47:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1!=null) {
                                    $("#DivVc1").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc1").html("");
                                }
                                break;
                            case 48:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc2").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc2").html("");
                                }
                                break;
                            case 49:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc3").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc3").html("");
                                }
                                break;
                            case 50:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc4").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc4").html("");
                                }
                                break;

                        }
                    }

                }
                if (FormID == 18) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 51:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc5").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc5").html("");
                                }
                                break;
                            case 52:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc6").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc6").html("");
                                }
                                break;
                            case 53:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc7").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc7").html("");
                                }
                                break;
                            case 54:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc8").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc8").html("");
                                }
                                break;
                            case 55:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc9").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc9").html("");
                                }
                                break;
                            case 56:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc10").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc10").html("");
                                }
                                 break;
                            case 57:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc11").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc11").html("");
                                }
                                break;
                            case 58:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc12").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc12").html("");
                                }
                                break;

                        }
                    }

                }
                if (FormID == 19) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 59:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc13").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc13").html("");
                                }
                                break;
                            case 60:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc14").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc14").html("");
                                }
                                break;
                            case 61:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc15").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc15").html("");
                                }
                                break;
                            case 62:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc16").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc16").html("");
                                }
                                break;
                            case 63:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc17").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc17").html("");
                                }
                                break;
                            case 64:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc18").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc18").html("");
                                }
                                break;
                            case 65:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc19").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc19").html("");
                                }
                                break;
                            case 66:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc20").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc20").html("");
                                }
                               break;
                            case 67:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc21").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc21").html("");
                                }
                                break;

                        }
                    }

                }
                if (FormID == 20) {
                    for (var i = 0; i < res.data.length; i++) {
                        switch (res.data[i].dataFormQuestionId) {
                            case 68:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc22").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc22").html("");
                                }
                                 break;
                            case 69:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc23").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc23").html("");
                                }
                               break;
                            case 70:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc24").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc24").html("");
                                }
                                break;
                            case 71:
                                if (res.data[i].fileName1 != "" && res.data[i].fileName1 != null) {
                                    $("#DivVc25").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                else {
                                    $("#DivVc25").html("");
                                }
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
                                else {
                                    $("#DivOrganazationChart").html("");
                                }
                                break;
                            case 29:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivOrganizationalDuties").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                }
                                else {
                                    $("#DivOrganizationalDuties").html("");
                                }
                                //  $("#DivOrganizationalDuties").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 30:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivRiskManagementGuidelines").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                }
                                else {
                                    $("#DivRiskManagementGuidelines").html("");
                                }
                                break;
                            case 31:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivTransactionRegulations").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                }
                                else {
                                    $("#DivTransactionRegulations").html("");
                                }
                               break;
                            case 31:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivTransactionRegulations").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                }
                                break;
                            case 32:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivDeductionTaxAccount").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                } else {
                                    $("#DivDeductionTaxAccount").html("");
                                }
                               break;
                            case 33:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivCrmSoftwareContract").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                } else {
                                    $("#DivCrmSoftwareContract").html("");
                                }
                                break;
                            case 35:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivRepresentativeFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                } else {
                                    $("#DivRepresentativeFile").html("");
                                }
                                break;
                            case 36:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivletterOfCommendation").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                } else {
                                    $("#DivletterOfCommendation").html("");
                                }
                                break;
                            case 37:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivInovationFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                } else {
                                    $("#DivInovationFile").html("");
                                }
                                break;
                            case 40:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivProceedings").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                } else {
                                    $("#DivProceedings").html("");
                                }
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
                                else {
                                    $("#DivLastAuditingTaxList").html("");
                                }
                                // $("#DivLastAuditingTaxList").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 98:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivLastChangeOfficialNewspaper").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                } else {
                                    $("#DivLastChangeOfficialNewspaper").html("");
                                }
                                // $("#DivLastChangeOfficialNewspaper").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 99:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivstatuteDoc").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                } else {
                                    $("#DivstatuteDoc").html("");
                                }
                                // $("#DivstatuteDoc").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                                break;
                            case 100:
                                if (res.data[i].fileName1 != null && res.data[i].fileName1 != "") {
                                    $("#DivOfficialNewspaper").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");

                                } else {
                                    $("#DivOfficialNewspaper").html("");
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
                               
                                strFormAnswer += ((res.data[i].fileName1 != null && res.data[i].fileName1 !="") ? "<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>" : "") + "</td><td>";
                              
                                break;
                            case "12":
                                strFormAnswer += ((res.data[i].fileName2 != null && res.data[i].fileName1 != "")? "<a href='/File/Download?path=" + res.data[i].fileName2Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>" : "") + "</td><td>";
                              break;
                        }
                    }

                    strFormAnswer += "<button title='ویرایش' class='btn btn-warring fontForAllPage' type='button' onclick='Web.FurtherInfo.GetFormAnswer(" + FormID + "," + res.data[i].answerTableId + ")' ><i class='fa fa-edit'></i></button>";
                    if ($("#sdfcddf").val()=="1") {
                        strFormAnswer += "<button style='margin-right:5px' type='button' title='حذف' class='btn btn-warrnig fontForAllPage changeData' onclick='Web.FurtherInfo.Delete_DataFormAnswerTables(" + FormID + ',"' + ColumNum + '",' + res.data[i].answerTableId + ");'><i class='fa fa-remove'></i></button>";
                    }
                    strFormAnswer += "</td></tr>";
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

                       
                        if (res.data[0].answer6!=null) {
                            if (res.data[0].answer6 == "on") {
                                $('#frmFrom' + FormID).find('input[name=Answer6]').prop("checked", true);
                            } else {
                                $('#frmFrom' + FormID).find('input[name=Answer6]').prop("checked", false);
                            }
                        } else {
                            $('#frmFrom' + FormID).find('input[name=Answer6]').prop("checked", false);
                        }
                        $('#frmFrom' + FormID).find('input[name=Answer5]').val(res.data[0].answer5);
                        $('#frmFrom' + FormID).find('input[name=Answer6]').val(res.data[0].answer6);
                        $('#frmFrom' + FormID).find('input[name=Answer7]').val(res.data[0].answer7);
                        $('#frmFrom' + FormID).find('input[name=Answer8]').val(res.data[0].answer8);
                        $('#frmFrom' + FormID).find('input[name=Answer9]').val(res.data[0].answer9);
                        $('#frmFrom' + FormID).find('input[name=Answer10]').val(res.data[0].answer10);
                        if (res.data[0].fileName1 != null && res.data[0].fileName1 != "") {
                            $("#DivAcademicDegreePicture").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        if (res.data[0].fileName2 != null && res.data[0].fileName2 != "") {
                            $("#DivPicture2").html("<a href='/File/Download?path=" + res.data[i].fileName2Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        if (res.data[0].fileName3 != null && res.data[0].fileName3 != "") {
                            $("#DivPicture3").html("<a href='/File/Download?path=" + res.data[i].fileName3Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        if (res.data[0].fileName4 != null && res.data[0].fileName4 != "") {
                            $("#DivPicture4").html("<a href='/File/Download?path=" + res.data[i].fileName4Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        $("#btnform1").html("ویرایش");
                    }
                    else if (FormID == 3) {
                        //1,3,11
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);

                        $('#IsGuideLineOrProcess').val(res.data[0].answer3Val);
                        comboBoxWithSearchUpdateText("IsGuideLineOrProcess", res.data[0].answer3);

                        if (res.data[0].fileName1 != null && res.data[0].fileName1 != "") {
                            $("#DivGuidelinesAndRegulationsFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        } else {
                            $("#DivGuidelinesAndRegulationsFile").html("");
                        }
                        $("#btnform3").html("ویرایش");

                    } else if (FormID == 4) {

                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer3]').val(res.data[0].answer3);
                        $('#frmFrom' + FormID).find('input[name=Answer4]').val(res.data[0].answer4);
                        if (res.data[0].fileName1 != null && res.data[0].fileName1 != "") {
                            $("#DivCertificateAndPermitAndStandardFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivCertificateAndPermitAndStandardFile").html("");
                        }
                        $("#btnform4").html("ویرایش");
                    }
                    else if (FormID == 5) {

                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer3]').val(res.data[0].answer3);
                        $('#frmFrom' + FormID).find('input[name=Answer4]').val(res.data[0].answer4);
                        if (res.data[0].fileName1 != null && res.data[0].fileName1 != "") {
                            $("#DivAwardsFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivAwardsFile").html("");
                        }
                        $("#btnform5").html("ویرایش");
                    }

                    else if (FormID == 8) {
                        if (res.data[0].fileName1 != null && res.data[0].fileName1 != "") {
                            $("#DivSwotFile1").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivSwotFile1").html("");
                        }
                        if (res.data[0].fileName2 != null && res.data[0].fileName1 != "") {
                            $("#DivSwotFile2").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivSwotFile2").html("");
                        }
                        $("#btnform8").html("ویرایش");
                    }
                    else if (FormID == 9) {
                        //1,2,11
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#DegreeOfEducation2').val(res.data[0].answer2Val);
                        comboBoxWithSearchUpdateText("DegreeOfEducation2", res.data[0].answer2);
                        if (res.data[0].fileName1 != null && res.data[0].fileName1 != "") {
                            $("#DivDegreeFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivDegreeFile").html("");
                        }
                        $("#btnform9").html("ویرایش");
                    }
                    else if (FormID == 10) {
                        //1,2,11
                        $('#ReportTitle').val(res.data[0].answer1);

                        if (res.data[0].fileName1 != null && res.data[0].fileName1 != "") {
                            $("#DivReportFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivReportFile").html("");
                        }
                        $("#btnform10").html("ویرایش");
                    }
                    else if (FormID == 12) {
                        //1,2,11
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer2]').val(res.data[0].answer2);
                        $('#frmFrom' + FormID).find('input[name=Answer3]').val(res.data[0].answer3);
                        $('#frmFrom' + FormID).find('input[name=Answer4]').val(res.data[0].answer4);
                        if (res.data[0].fileName1 != null && res.data[0].fileName1!="") {
                            $("#DivCertificationCourses2").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivCertificationCourses2").html("");
                        }
                        $("#btnform12").html("ویرایش");
                    }
                    else if (FormID == 13) {
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer2]').val(res.data[0].answer2);
                        $('#PostInSociety').val(res.data[0].answer3Val);
                        if (res.data[0].fileName1 != null && res.data[0].fileName1!= "") {
                            $("#DivCertificationSociety").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivCertificationSociety").html("");
                        }
                        comboBoxWithSearchUpdateText("PostInSociety", res.data[0].answer3);
                        $("#btnform13").html("ویرایش");
                    }
                    else if (FormID == 15) {
                        //1,2,11
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer2]').val(res.data[0].answer2);
                        if (res.data[0].fileName1 != null && res.data[0].fileName1!="") {
                            $("#DivPublicActivityFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivPublicActivityFile").html("");
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
                        $('#frmFrom' + FormID).find('input[name=Answer3]').val(res.data[0].answer3);
                        $("#btnform17").html("ویرایش");

                    } else if (FormID == 22) {
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        if (res.data[0].fileName1 != null && res.data[0].fileName1 != "") {
                            $("#DivInvestmentFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivInvestmentFile").html("");
                        }
                        $("#btnform22").html("ویرایش");
                    }
                    else if (FormID == 24) {
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        if (res.data[0].fileName1 != null && res.data[0].fileName1 != "") {
                            $("#DivEmploymentDisabledFile").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivEmploymentDisabledFile").html("");
                        }
                        $("#btnform24").html("ویرایش");
                    }
                    else if (FormID == 26) {
                        $('#frmFrom' + FormID).find('input[name=Answer1]').val(res.data[0].answer1);
                        $('#frmFrom' + FormID).find('input[name=Answer2]').val(res.data[0].answer2);
                        $('#frmFrom' + FormID).find('input[name=Answer3]').val(res.data[0].answer3);
                        $('#frmFrom' + FormID).find('input[name=Answer4]').val(res.data[0].answer4);

                        if (res.data[0].fileName1 != null && res.data[0].fileName1 != "") {
                            $("#DivCertificationCourses").html("<a href='/File/Download?path=" + res.data[i].fileName1Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivCertificationCourses").html("");
                        }
                        if (res.data[0].fileName2 != null && res.data[0].fileName2 != "") {
                            $("#DivCertificationCoursesAttach").html("<a href='/File/Download?path=" + res.data[i].fileName2Full + "' target='_blank'><i class='fa fa-download'></i>&nbsp;دانلود</a>");
                        }
                        else {
                            $("#DivCertificationCoursesAttach").html("");
                        }
                        $("#btnform26").html("ویرایش");
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
                        if (FormID==1) {
                            clearFormDetail1();
                        } else if (FormID == 26) {
                            clearFormDetail26();
                        }
                        else if (FormID == 3) {
                            clearFormDetail3();
                        }
                        else if (FormID == 4) {
                            clearFormDetail4();
                        }
                        else if (FormID == 5) {
                            clearFormDetail5();
                        }
                        else if (FormID == 16) {
                            clearFormDetail16();
                        }
                        else if (FormID == 17) {
                            clearFormDetail17();
                        }
                        else if (FormID == 8) {
                            clearFormDetail8();
                        }
                        else if (FormID == 9) {
                            clearFormDetail9();
                        }
                        else if (FormID == 10) {
                            clearFormDetail10();
                        }
                        else if (FormID == 12) {
                            clearFormDetail12();
                        }
                        else if (FormID == 13) {
                            clearFormDetail13();
                        }
                        else if (FormID == 15) {
                            clearFormDetail15();
                        }
                        else if (FormID == 22) {
                            clearFormDetail22();
                        }
                        else if (FormID == 24) {
                            clearFormDetail24();
                        }
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
                    clearFormDetail1();
                    initFurtherInfo($("#RequestIdForms").val());
                }
                else {

                    intiFormShow(FormId, ColumnNum, $("#RequestIdForms").val());
                    $(e).html('افزودن');
                }
                if (FormId == 26) {
                    clearFormDetail26();
                } else if (FormId == 3) {
                    clearFormDetail3();
                }
                else if (FormId == 4) {
                    clearFormDetail4();
                }
                else if (FormId == 5) {
                    clearFormDetail5();
                }
                else if (FormId == 16) {
                    clearFormDetail16();
                }
                else if (FormId == 17) {
                    clearFormDetail17();
                }
                else if (FormId == 8) {
                    clearFormDetail8();
                }
                else if (FormId == 9) {
                    clearFormDetail9();
                }
                else if (FormId == 10) {
                    clearFormDetail10();
                }
                else if (FormId == 12) {
                    clearFormDetail12();
                }
                else if (FormId == 13) {
                    clearFormDetail13();
                }
                else if (FormId == 15) {
                    clearFormDetail15();
                }
                else if (FormId == 22) {
                    clearFormDetail22();
                } else if (FormID == 24) {
                    clearFormDetail24();
                }
                document.getElementById("frmFrom" + FormId).reset();
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

    function saveFurtherInfo(e) {

        $(e).attr("disabled", "");

        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/FurtherInfo/Save_FurtherInfo", fill_AjaxCallActionPostSaveFormWithUploadFile("FormDe2"), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {
               // intiFormSingelAnswer(FormId, $("#RequestIdForms").val());
                intiTab(1);
                alertB("ثبت", "اطلاعات ثبت شد", "success");
            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function saveCorporateGovernance(e,id=null) {

        $(e).attr("disabled", "");

        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/FurtherInfo/Save_CorporateGovernance", fill_AjaxCallActionPostSaveFormWithUploadFile("FormDe6"), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {
                getCorporateGovernance(id);
                // intiFormSingelAnswer(FormId, $("#RequestIdForms").val());
                alertB("ثبت", "اطلاعات ثبت شد", "success");
            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function saveValueChain(e,FormName=null) {

        $(e).attr("disabled", "");

        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/FurtherInfo/Save_ValueChain", fill_AjaxCallActionPostSaveFormWithUploadFile(FormName), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {
                // intiFormSingelAnswer(FormId, $("#RequestIdForms").val());
                intiTab(7);
                alertB("ثبت", "اطلاعات ثبت شد", "success");
            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function savePublicActivities(e) {

        $(e).attr("disabled", "");

        AjaxCallActionPostSaveFormWithUploadFile("/api/customer/FurtherInfo/Save_PublicActivities", fill_AjaxCallActionPostSaveFormWithUploadFile("FormPublicActivities"), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {
                // intiFormSingelAnswer(FormId, $("#RequestIdForms").val());
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

    function initReferral(id = null) {

        AjaxCallAction("GET", "/api/Customer/RequestForRating/InitReferral/" + id, null, true, function (res) {

            if (res.isSuccess) {

                $("#sdklsslks3498sjdkxhjsd_823sa").val(encrypt(id.toString(), keyMaker()));
                $("#sdklsslks3498sjdkxhjsd_823sdel").val(res.data[0].sendUser);
                var htmlB = "";
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[0].levelStepIndex == 7 && res.data[0].levelStepAccessRole==5) {
                        htmlB += "<button type='button' id='btnreq' style='margin:5px' class='btn btn-info ButtonOpperationLSSlss' onclick='Web.RequestForRating.SaveReferralRequestForRating(this);'" + "data-SIndex='" + res.data[i].levelStepSettingIndexId +"' data-DLSI='" + encrypt(res.data[i].destLevelStepIndex, keyMaker()) + "' data-LSAR='" + encrypt(res.data[i].levelStepAccessRole, keyMaker()) + "' data-LSS='" + encrypt(res.data[i].levelStepStatus, keyMaker()) + "' data-SC='" + encrypt(res.data[i].smsContent, keyMaker()) + "' data-ST='" + res.data[i].smsType + "' data-DLSIB='" + encrypt(res.data[i].destLevelStepIndexButton, keyMaker()) + "'>" + res.data[i].destLevelStepIndexButton + "</button>";

                    } 
                }
                $("#bLLSS").html(htmlB);
                $("#sdfcddf").val(1);
                
            } else {
                $(".changeData").remove();
                $("#EndfurtherInfo").html("اطلاعات شما برای پارس کیان جهت بررسی و ارزیابی ارسال شده است و قابل ویرایش نمی باشد.");
                
            }
           

        }, true);

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

    //function validate() {

    //    $("form[id='frmFrom1']").validate({
    //        // Specify validation rules
    //        rules: {
    //            "Answer1": {
    //                required: function () {
    //                    return true;
    //                },
    //                minlength: 5,
    //                maxlength: 50
    //            },

    //        },
    //        // Specify validation error messages
    //        messages: {
    //            "Answer1": {
    //                required: function () {
    //                    return "  لطفا نام و نام خانوادگی را را وارد کنید";
    //                },
    //                minlength: " باید حداقل 5 حرف باشد",
    //                maxlength: " باید حداکثر 50 حرف باشد"
    //            },

    //        },
    //        // Make sure the form is submitted to the destination defined
    //        // in the "action" attribute of the form when valid
    //        submitHandler: function (form) {
    //            Web.FurtherInfo.SaveForm("#btnform1", 1, '1,2,7,8');
    //        }
    //    });

    //    }
    $("#frmFrom1 input,textarea").on("focusout", function () {

        $(this).valid();

    });

    $(document).ready(function () {

        $("form[id='frmFrom1']").validate({
            // Specify validation rules
            rules: {
                "Answer1": {
                    required: function () {
                        return true;
                    },
                    minlength: 5,
                    maxlength: 50
                },
                "Answer2": {
                    required: function () {
                        return true;
                    },
                    minlength:2,
                    maxlength: 50
                },
                "Answer3": {
                    required: function () {
                        return $("#MemberEductionID").val() != "223";
                    },
                },

            },
            // Specify validation error messages
            messages: {
                "Answer1": {
                    required: function () {
                        return "لطفا نام و نام خانوادگی را وارد کنید";
                    },
                    minlength: " باید حداقل 5 حرف باشد",
                    maxlength: " باید حداکثر 50 حرف باشد"
                },
                "Answer2": {
                    required: function () {
                        return "لطفا پست سازمانی را وارد کنید";
                    },
                    minlength: " باید حداقل 3 حرف باشد",
                    maxlength: " باید حداکثر 50 حرف باشد"
                },
                "Answer3": {
                    required: function () {
                        return "لطفا آخرین مدرک تحصیلی را انتخاب کنید";
                    },
                },

            },
            // Make sure the form is submitted to the destination defined
            // in the "action" attribute of the form when valid
            submitHandler: function (form) {
                Web.FurtherInfo.SaveForm("#btnform1", 1, '1,2,7,8');
            }
        });

        $("form[id='frmFrom26']").validate({
            // Specify validation rules
            rules: {
                "Answer1": {
                    required: function () {
                        return true;
                    },
                    minlength: 5,
                    maxlength: 50
                },
                "Answer2": {
                    required: function () {
                        return true;
                    },
                    minlength: 2,
                    maxlength: 50
                },
              
            },
            // Specify validation error messages
            messages: {
                "Answer1": {
                    required: function () {
                        return "لطفا نام و نام خانوادگی را وارد کنید";
                    },
                    minlength: " باید حداقل 5 حرف باشد",
                    maxlength: " باید حداکثر 50 حرف باشد"
                },
                "Answer2": {
                    required: function () {
                        return "لطفا نام دوره را وارد کنید";
                    },
                    minlength: " باید حداقل 5 حرف باشد",
                    maxlength: " باید حداکثر 50 حرف باشد"
                },
               

            },
            // Make sure the form is submitted to the destination defined
            // in the "action" attribute of the form when valid
            submitHandler: function (form) {
                Web.FurtherInfo.SaveForm(this, 26, '1,2,3,4,11');
            }
        });

        $("form[id='frmFrom3']").validate({
            // Specify validation rules
            rules: {
                "Answer1": {
                    required: function () {
                        return true;
                    },
                    minlength: 5,
                    maxlength: 50
                },
              

            },
            // Specify validation error messages
            messages: {
                "Answer1": {
                    required: function () {
                        return "لطفا عنوان دستورالعمل ها و آیین نامه ها یا روش اجرایی را وارد کنید";
                    },
                    minlength: " باید حداقل 5 حرف باشد",
                    maxlength: " باید حداکثر 50 حرف باشد"
                },
              

            },
            // Make sure the form is submitted to the destination defined
            // in the "action" attribute of the form when valid
            submitHandler: function (form) {
                Web.FurtherInfo.SaveForm(this, 3, '1,3,11');
            }
        });
       
    });

    function initCustomer(dir = 'rtl') {

        ComboBoxWithSearch('.select2', dir);

        AjaxCallAction("GET", "/api/customer/Customers/Get_Customers/", null, true, function (res) {


            if (res != null) {
                $("#CutomerName").html("<h4> فرم اطلاعات تکمیلی " + res.companyName+"</h4>");
            }

        }, true);

    }
    function clearFormDetail1() {
        $('#btnform1').html('افزودن');       
        $('#frmFrom1').find('input[name=AnswerTableId]').val(0);
        $('#DivPicture2').html('');
        $('#DivPicture3').html('');
        $('#DivPicture4').html('');
        $('#DivAcademicDegreePicture').html('');
        $('#frmFrom1').get(0).reset();
    }
    function clearFormDetail26() {
        $('#btnform26').html('افزودن');
        $('#frmFrom26').find('input[name=AnswerTableId]').val(0);
        $('#DivCertificationCourses').html('');
        $('#DivCertificationCoursesAttach').html('');
        $('#frmFrom26').get(0).reset();
    }
    function clearFormDetail3() {
        $('#btnform3').html('افزودن');
        $('#DivGuidelinesAndRegulationsFile').html('');
        $('#frmFrom3').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom3').get(0).reset();
    }
    function clearFormDetail4() {
        $('#btnform4').html('افزودن');
        $('#DivCertificateAndPermitAndStandardFile').html('');
        $('#frmFrom4').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom4').get(0).reset();
    }
    function clearFormDetail5() {
         $('#DivAwardsFile').html(''); 
        $('#btnform5').html('افزودن');       
        $('#frmFrom5').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom5').get(0).reset();
    }
    function clearFormDetail16() {      
        $('#btnform16').html('افزودن');
        $('#frmFrom16').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom16').get(0).reset();
    }
    function clearFormDetail17() {
        $('#btnform17').html('افزودن');
        $('#frmFrom17').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom17').get(0).reset();
    }
    function clearFormDetail8() {
        $('#btnform8').html('افزودن');
        $('#DivSwotFile2').html('');
        $('#DivSwotFile1').html('');
        $('#frmFrom8').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom8').get(0).reset();
    }
    function clearFormDetail9() {
        $('#btnform9').html('افزودن');
        $('#DivDegreeFile').html(''); 
        $('#frmFrom9').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom9').get(0).reset();
    }
    function clearFormDetail10() {
        $('#btnform10').html('افزودن');
        $('#DivReportFile').html('');
        $('#frmFrom10').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom10').get(0).reset();
    }
    function clearFormDetail12() {
        $('#btnform12').html('افزودن');
        $('#DivCertificationCourses2').html('');
        $('#frmFrom12').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom12').get(0).reset();
    }
    function clearFormDetail13() {
        $('#btnform13').html('افزودن');
        $('#DivCertificationSociety').html('');
        $('#frmFrom13').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom13').get(0).reset();
    }
    function clearFormDetail15() {
        $('#btnform15').html('افزودن');
        $('#DivPublicActivityFile').html('');
        $('#frmFrom15').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom15').get(0).reset();
    }
    function clearFormDetail22() {
        $('#btnform22').html('افزودن');
        $('#DivInvestmentFile').html('');
        $('#frmFrom22').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom22').get(0).reset();
    }
    function clearFormDetail24() {
        $('#btnform24').html('افزودن');
        $('#DivEmploymentDisabledFile').html('');
        $('#frmFrom24').find('input[name=AnswerTableId]').val(0);
        $('#frmFrom24').get(0).reset();
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
        IntiFormQrid: intiFormQrid,
        SaveFurtherInfo: saveFurtherInfo,
        SaveCorporateGovernance: saveCorporateGovernance,
        GetFurtherInfo: getFurtherInfo,
        SaveValueChain: saveValueChain,
        InitReferral: initReferral,
        GetCorporateGovernance: getCorporateGovernance,
        GetValueChain: getValueChain,
        SavePublicActivities: savePublicActivities,
        GetPublicActivities: getPublicActivities,
        InitCustomer: initCustomer,
        ClearFormDetail1: clearFormDetail1,
        ClearFormDetail26: clearFormDetail26,
        ClearFormDetail3: clearFormDetail3,
        ClearFormDetail4: clearFormDetail4,
        ClearFormDetail5: clearFormDetail5,
        ClearFormDetail16: clearFormDetail16,
        ClearFormDetail17: clearFormDetail17,
        ClearFormDetail8: clearFormDetail8,
        ClearFormDetail9: clearFormDetail9,
        ClearFormDetail10: clearFormDetail10,
        ClearFormDetail12: clearFormDetail12,
        ClearFormDetail13: clearFormDetail13,
        ClearFormDetail15: clearFormDetail15,
        ClearFormDetail22: clearFormDetail22,
        ClearFormDetail24: clearFormDetail24
    };

})(Web, jQuery);