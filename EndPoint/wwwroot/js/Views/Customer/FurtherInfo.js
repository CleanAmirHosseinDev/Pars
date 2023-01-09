






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

        var countform = Number($("#FormLoadCode").val()) -1;
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

        switch (TabId) {
            case 1:
                intiFormShow(1, "1,2,7,8");
                break;
            case 2:
                intiFormShow(3, "1,2,3");
                break;
            case 3:
                intiForm(6);
                intiFormShow(16, "1,2,3");
                intiFormShow(17, "1,2");
                break;
            case 4:
                intiForm(11);
                break;
            case 5:
                intiFormShow(13, "1,2,3");
                break;
            case 7:
                intiForm(7);
                intiForm(18);
                intiForm(19);
                intiForm(20);
            case 9:
                intiFormShow(9, '1,2');
                intiFormShow(10, '1,2');
                break; 
            case 14:
                //intiFormShow(15, '1,2,3');
                //intiFormShow(22, '1,2');
                
                //intiForm(14);

               
                intiFormShow(24, '1,2');

                intiForm(21);
                intiFormSingelAnswer(21);

                intiForm(23);                
                intiFormSingelAnswer(23);

                break;

        }

    }

    function systemSeting_Combo(dir = 'rtl') {

        ComboBoxWithSearch('.select2', dir);

        AjaxCallAction("POST", "/api/customer/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "5,9,20,30,125",  PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strMemberPostID = '<option value="">انتخاب کنید</option>';
                var strMemberEductionID = '<option value="">انتخاب کنید</option>';
                var strUniversityID = '<option value="">انتخاب کنید</option>';
                var strCompanyDocument = '';
                var strOtherDocument = '';
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].parentCode == 5) {
                        strMemberPostID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 9) {
                        strMemberEductionID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    } else if (res.data[i].parentCode == 20) {
                        strUniversityID += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";
                    }
                     else if (res.data[i].parentCode == 30) {
                        strCompanyDocument += " <tr><td>" + " <div class='form-group'><label class='control-label col-md-4' for=''>" + res.data[i].label + "<span class='RequiredLabel'>*</span></label><div class='col-md-8'><input type='file'  class='form-control'/></div></div></td> <td><a href='#'>مشاهده </a></td></tr>";
                    }
                    else if (res.data[i].parentCode == 125) {
                       
                        strOtherDocument += " <tr><td>" + " <div class='form-group'><label class='control-label col-md-4' for=''>" + res.data[i].label + "<span class='RequiredLabel'>*</span></label><div class='col-md-8'><input type='file'  class='form-control'/></div></div></td> <td><a href='#'>مشاهده </a></td></tr>";
                    }

                }
                

                $("#MemberPostID").html(strMemberPostID);
                $("#MemberEductionID").html(strMemberEductionID);
                $("#UniversityID").html(strUniversityID);
                $("#CompanyDocument").html(strCompanyDocument);
                $("#OtherDocument").html(strOtherDocument);

              //  $("#HowGetKnowCompany").val(resSingle.howGetKnowCompanyId);
              //  $("#KindOfCompany").val(resSingle.kindOfCompanyId);
              //  $("#TypeServiceRequestedId").val(resSingle.typeServiceRequestedId);

                

            }
        }, true);
    }

   

    function initFurtherInfo(dir = 'rtl') {
        intiTab(1);
        ComboBoxWithSearch('.select2', 'dir');
        systemSeting_Combo(dir);
    }

    function intiFormShow(Id=null,Columns=null) {

        intiForm(Id);
        intiFormAnswer(Id, Columns);
       
    }


    function intiForm(FormID=null) {  

        AjaxCallAction("POST", "/api/customer/FurtherInfo/Get_DataFormQuestionss", JSON.stringify({ DataFormId: FormID, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {

                var strFormId = '';
                
                for (var i = 0; i < res.data.length; i++)
                {
                   
                    if (i == 0) {
                        strFormId += "<div class='col-lg-6'>";
                    } else if (i == Math.round((res.data.length) / 2) && res.data.length>1)
                    {
                        strFormId += "</div><div class='col-lg-6'>";
                    }
                    
                    strFormId += "<div class='form-group'><div class='col-md-12' style='margin-bottom:10px'><label class='control-label'  for=''>" + res.data[i].questionText + "<span class='RequiredLabel'>*</span></label>";
                    if (res.data[i].questionType=='select') {
                        strFormId += "<select name='" + res.data[i].questionName + "' id='" + res.data[i].questionName +"' class='form-control select2' ></select>";
                    } else if (res.data[i].questionType == 'textarea') {
                        strFormId += "<textarea name='" + res.data[i].questionName + "' id='" + res.data[i].questionName + "' class='form-control' ></textarea>";
                    }
                    else if (res.data[i].questionType == 'checkbox') {
                        strFormId += "<input type='" + res.data[i].questionType + "' name='" + res.data[i].questionName + "' id='" + res.data[i].questionName + "' style='text-align:right; width: 30px' />";

                    } else {
                        strFormId += "<input type='" + res.data[i].questionType + "' name='" + res.data[i].questionName + "' id='" + res.data[i].questionName + "' placeholder='" + res.data[i].questionText + "' class='form-control' />";
                    }
                    strFormId += "</div>";
                    strFormId += "</div>";
                    if (i == res.data.length  && res.data.length > 1) {
                        strFormId += "</div>";
                    } 
                }
                $("#FormDetail" + FormID).html(strFormId);
            }
        }, true);
    }

    function intiFormSingelAnswer(FormID = null) {
        AjaxCallAction("POST", "/api/customer/FurtherInfo/Get_DataFromAnswerss", JSON.stringify({ FormId: FormID, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                if (FormID==14) {
                    for (var i = 0; i < res.data.length; i++) {
                        $("#IsPublicActivityFile").popover(res.data[0].answer);
                    }
                }
                else if (FormID == 21) {
                    for (var i = 0; i < res.data.length; i++) {
                        $("#Investment").val(res.data[0].answer);
                    }

                }
                else if (FormID == 23) {
                    for (var i = 0; i < res.data.length; i++) {
                        $("#EmploymentDisabled").val(res.data[0].answer);
                    }
                }
            }
           
        }, true);
    }

    function intiFormAnswer(FormID = null,ColumNum=null) {
        AjaxCallAction("POST", "/api/customer/FurtherInfo/Get_DataFormAnswerTabless", JSON.stringify({ FormId: FormID, PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {

                var separatedArray = ColumNum.split(',');
                let strFormAnswer = '';
                for (let i = 0; i < res.data.length; i++) {

                    strFormAnswer += "<tr><td>" +( i+1) + "</td><td>";

                    for (var j = 0; j < separatedArray.length; j++) {

                            switch (separatedArray[j]) {
                                case"1":
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
                            }
                                         

                    }
                       
                    strFormAnswer += "<a title='ویرایش' href='/Customer/FurtherInfo/EditAnswerTableId?id=" + res.data[i].answerTableId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";
                }
                $("#FormDetailAnswer" + FormID).html(strFormAnswer);
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
    };

})(Web, jQuery);