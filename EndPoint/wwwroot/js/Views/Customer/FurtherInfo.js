






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

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

   // $('.OpenUpload').click(function () { $('#CustomeFileupload').trigger('click'); });

    function initFurtherInfo(dir = 'rtl') {

        ComboBoxWithSearch('.select2', dir);
       
        //AjaxCallAction("GET", "/api/customer/Customers/Get_Customers/", null, true, function (res) {            
                

        //        if (res != null) {
        //            $("#AddressCompany").val(res.addressCompany);
        //            $("#CompanyName").val(res.companyName);
        //            $("#CeoName").val(res.ceoName);
        //            $("#EconomicCode").val(res.economicCode);
        //            $("#NationalCode").val(res.nationalCode);
        //            $("#CeoMobile").val(res.ceoMobile);
        //            $("#AgentMobile").val(res.agentMobile);
        //            $("#AgentName").val(res.agentName);
        //            $("#NamesAuthorizedSignatories").val(res.namesAuthorizedSignatories);
        //            $("#AmountOsLastSaels").val(res.amountOsLastSaels);
        //            $("#CountOfPersonal").val(res.countOfPersonal);
        //            $("#Email").val(res.email);
        //            $("#Tel").val(res.tel);
        //            $("#PostalCode").val(res.postalCode);

                    systemSeting_Combo();

           //     }
                           

          //  }, true);       

    }

    web.FurtherInfo = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        initFurtherInfo: initFurtherInfo,
        SystemSeting_Combo: systemSeting_Combo
    };

})(Web, jQuery);