
(function (web, $) {
  
    function saveCareerOpportunities(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/LicensesAndHonors/Save_LicensesAndHonors", JSON.stringify({
            Name: $("#Name").val(),
            Family: $("#Family").val(),
            CityID: $("#CityID").val(),
            Brithday: $("#Brithday").val(),
            Mobile: $("#Mobile").val(),
            Tel: $("#Tel").val(),
            Address: $("#Address").val(),
            NationalCode: $("#NationalCode").val(),
            Postion: $("#Postion").val(),
            Education: $("#Education").val(),
            ResumeFile: $("#ResumeFile").val(),
           
        }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                alertB("ثبت", res.message, "success");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function intCareerOpportunitiesPage() {
     
        Web.City.GetStatesList();
        systemSeting_Combo();
    }
    function getCitys() {
       
         Web.City.GetCityList();
        
    }
    function systemSeting_Combo() {

        AjaxCallAction("POST", "/api/SystemSeting/Get_SystemSetings", JSON.stringify({ ParentCodeArr: "9", PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {
                var strEducationLevel = '<option value="">انتخاب کنید</option>';
             
                for (var i = 0; i < res.data.length; i++) {                   
                    strEducationLevel += " <option value=" + res.data[i].systemSetingId + ">" + res.data[i].label + "</option>";                  
                }

                $("#EducationLevel").html(strEducationLevel);

            }
        }, true);
    }


    web.CareerOpportunities = {
        GetCitys: getCitys,
        IntCareerOpportunitiesPage: intCareerOpportunitiesPage,
        SaveCareerOpportunities: saveCareerOpportunities,
        SystemSeting_Combo: systemSeting_Combo
    };

})(Web, jQuery);