
(function (web, $) {

    function initAboutUs() {

        AjaxCallAction("GET", "/api/AboutUs/Get_AboutUs/", null, true, function (res) {

            if (res != null) {

                $("#CompanyName").text(res.companyName);
                $("#Subject").text(res.subject);
                $("#AboutUSContent").html(res.aboutUscontent);               
            }

        }, true);

    }

    function initVisionAndMission() {

        AjaxCallAction("GET", "/api/AboutUs/Get_AboutUs/", null, true, function (res) {

            if (res != null) {
                $("#VisionAndMission").html(res.visionAndMission);               
            }

        }, true);

    }

    function initContanctUs() {

        AjaxCallAction("GET", "/api/AboutUs/Get_AboutUs/", null, true, function (res) {

            if (res != null) {
                $("#Address").text(res.address);
                $("#Tel1").text(res.tel1);
                $("#Tel2").text(res.tel2);
                $("#Tel3").text(res.tel3);
                $("#Tel4").text(res.tel4);
                $("#Tel5").text(res.tel5);
                $("#Mobile1").text(res.mobile1);
                $("#Mobile2").text(res.mobile2);
                $("#FaxNumber").text(res.faxNumber);
                $("#Email").text(res.email);
                $("#Instagram").text(res.instagram);
                $("#Whatsapp").text(res.whatsapp);
                $("#Facebook").text(res.facebook);
                $("#Telegram").text(res.telegram);              
            }

        }, true);

    }

    function initMoto() {

        AjaxCallAction("GET", "/api/AboutUs/Get_AboutUs/", null, true, function (res) {

            if (res != null) {              
                $("#Moto1").text(res.moto1);
                $("#Moto2").text(res.moto2);
                $("#Moto3").text(res.moto3);
                $("#Moto4").text(res.moto4);
                $("#Moto5").text(res.moto5);
            }

        }, true);

    }

    function initOrganazationCharts() {

        AjaxCallAction("GET", "/api/AboutUs/Get_AboutUs/", null, true, function (res) {

            if (res != null) {
                $("#OrganazationChart").html(res.organazationChart);              
            }

        }, true);

    }

    web.AboutUs = {
        InitAboutUs: initAboutUs,
        InitVisionAndMission: initVisionAndMission,
        InitContanctUs: initContanctUs,
        InitMoto: initMoto,
        InitOrganazationCharts:initOrganazationCharts
    };

})(Web, jQuery);