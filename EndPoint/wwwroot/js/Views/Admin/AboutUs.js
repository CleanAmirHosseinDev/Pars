






(function (web, $) {

    //Document Ready              

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function saveAboutUs(e) {

        $(e).attr("disabled", "");

        AjaxCallAction("POST", "/api/admin/AboutUs/Save_AboutUs", JSON.stringify({
            CompanyName: $("#CompanyName").val(),
            AboutUsId: $("#AboutUsId").val(),            
            AboutUsComment: $("#AboutUsComment").val(),            
            Address: $("#Address").val(),
            Subject:$("#Subject").val(),           
            Tel1: $("#Tel1").val(),
            Tel2: $("#Tel2").val(),
            Tel3:$("#Tel3").val(),
            tel4: $("#Tel4").val(),
            Tel5:$("#Tel5").val(),
            Mobile1: $("#Mobile1").val(),
            Mobile2:$("#Mobile2").val(),
            FaxNumber:$("#FaxNumber").val(),
            Email: $("#Email").val(),
            Instagram:$("#Instagram").val(),
            Whatsapp: $("#Whatsapp").val(),
            Facebook: $("#Facebook").val(),
            Telegram: $("#Telegram").val(),
            Moto1: $("#Moto1").val(),
            Moto2: $("#Moto2").val(),
            Moto3:$("#Moto3").val(),
            Moto4:$("#Moto4").val(),
            Moto5: $("#Moto5").val(),
            OrganazationChart: getDataCkeditor("OrganazationChart"),
            AboutUSContent:    getDataCkeditor("AboutUSContent"),
            VisionAndMission:  getDataCkeditor("VisionAndMission"),
        }), true, function (res) {

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

                $("#AboutUsId").val(res.aboutUsId);
                $("#AboutUsComment").val(res.aboutUsComment);
                $("#CompanyName").val(res.companyName);
                $("#Address").val(res.address);
                $("#Subject").val(res.subject);
                $("#AboutUSContent").val(res.aboutUscontent);
                Ckeditor("AboutUSContent");
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
                Ckeditor("VisionAndMission");
                $("#OrganazationChart").val(res.organazationChart);
                Ckeditor("OrganazationChart");
                $("#Moto1").val(res.moto1);
                $("#Moto2").val(res.moto2);
                $("#Moto3").val(res.moto3);
                $("#Moto4").val(res.moto4);
                $("#Moto5").val(res.moto5);
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