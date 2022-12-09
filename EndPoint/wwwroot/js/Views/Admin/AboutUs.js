






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

   
    function saveAboutUs(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/AboutUs/Save_AboutUs", JSON.stringify({ AboutUsName: $("#AboutUsName").val(), AboutUsId: $("#AboutUsId").val() }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {

                goToUrl("/Admin/AboutUs/Index");

            }
            else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function initAboutUs() {
      
        AjaxCallAction("GET", "/api/admin/AboutUs/Get_AboutUs/", null, true, function (res) {

            if (res != null) {

                $("#AboutUsId").val(res.AboutUsId);
                $("#AboutUsComment").val(res.AboutUsComment);
                $("#CompanyName").val(res.companyName);
                $("#Address").val(res.address);
                $("#Subject").val(res.subject);
                $("#AboutUSContent").val(res.aboutUSContent);
                $("#Tel1").val(res.tel1);
                $("#Tel2").val(res.tel2);
                $("#Tel3").val(res.tel3);
                $("#Tel4").val(res.tel4);
                $("#Tel5").val(res.tel5);
                $("#Mobile1").val(res.mobile1);
                $("#Mobile2").val(res.mobile2);
                $("#FaxNumber").val(res.faxNumber);
                $("#Email").val(res.email);
                $("#Instagram").val(res.instagram);
                $("#Whatsapp").val(res.whatsapp);
                $("#Facebook").val(res.facebook);
                $("#Telegram").val(res.telegram);
                $("#VisionAndMission").val(res.visionAndMission);
                $("#OrganazationChart").val(res.organazationChart);
                $("#Moto1").val(res.Moto1);
                $("#Moto2").val(res.Moto2);
                $("#Moto3").val(res.Moto3);
                $("#Moto4").val(res.Moto4);
                $("#Moto5").val(res.Moto5);
                //

            }

        }, true);

    }

  
    web.AboutUs = {
        TextSearchOnKeyDown: textSearchOnKeyDown,       
        SaveAboutUs: saveAboutUs,
        InitAboutUs: initAboutUs,
       
    };

})(Web, jQuery);