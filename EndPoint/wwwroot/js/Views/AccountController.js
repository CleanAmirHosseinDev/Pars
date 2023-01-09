




(function (web, $) {

    //Document Ready          

    function loginA(e) {

        try {

            $(e).attr("disabled", "");

            AjaxCallAction("POST", "/api/Securitys/Login", JSON.stringify({ Username: $("#User").val(), Password: $("#Password").val(), CaptchaCodes: $("#form_n1 input[name='CaptchaCodes']").val() }), true, function (res) {

                $(e).removeAttr("disabled");

                if (res.isSuccess) {


                    setlstor("token", res.data.token);
                    setlstor("menu", JSON.stringify(res.data.menus));
                    setlstor("fullName", res.data.fullName);
                    setlstor("userID", res.data.userID);
                    setlstor("loginName", res.data.loginName);

                    goToUrl(res.message);

                }
                else {

                    alertB("خطا", res.message, "error");

                }

            }, true);

        } catch (e) {

        }

    }

    function loginShowPass() {

        try {

            $('#Password').attr('type', $('#Password').attr('type') == 'password' ? 'text' : 'password');

        } catch (e) {

        }

    }

    function loginC(e) {

        
        try {


            if (isEmpty($("#User").val())) {

                alertB("خطا", "شماره تلفن همراه را وارد کنید", "error");
                return;
            }

            $(e).attr("disabled", "");

            AjaxCallAction("POST", "/api/Securitys/Login", JSON.stringify({ Mobile: $("#User").val(), CaptchaCodes: $("#form_n1 input[name='CaptchaCodes']").val(), NationalCode: $("#NationalCode").val() }), true, function (res) {

                $(e).removeAttr("disabled");
                debugger;
                if (res.isSuccess) {
                    showTab2();

                    $("#Bakdslkflkdsflkdslkfkldskfdslflsdkf_dnsfhsdkfh").val(encrypt(res.data.customerID, keyMaker()));

                    $("#Fulllfsdfdsflsfldsfldslflsdlfdslflsdlfldsflldsf").val(encrypt(res.data.fullName, keyMaker()));



                    //$("#divLogin").hide();
                    //$("#divAuthenticatedCode").show();

                }
                else {

                    alertB("خطا", res.message, "error");

                }

            }, false);

        } catch (e) {

        }

    }

    function initLoginC() {

        try {

            $("#divAuthenticatedCode").hide();

        } catch (e) {

        }

    }

    function autenticatedCode(e) {

        try {


            if (isEmpty($("#Code").val())) {

                alertB("خطا", "کد احراز را وارد کنید", "error");
                return;
            }

            $(e).attr("disabled", "");

            AjaxCallAction("POST", "/api/Securitys/AutenticatedCode", JSON.stringify({ CaptchaCodes: $("#form_n1 input[name='CaptchaCodes']").val(), Code: $("#Code").val(), Bakdslkflkdsflkdslkfkldskfdslflsdkf_dnsfhsdkfh: decrypt($("#Bakdslkflkdsflkdslkfkldskfdslflsdkf_dnsfhsdkfh").val(), keyMaker()), Fulllfsdfdsflsfldsfldslflsdlfdslflsdlfldsflldsf: decrypt($("#Fulllfsdfdsflsfldsfldslflsdlfdslflsdlfldsflldsf").val(), keyMaker()) }), true, function (res) {

                $(e).removeAttr("disabled");

                if (res.isSuccess) {

                    debugger;

                    setlstor("token", res.data.token);
                    setlstor("fullName", res.data.fullName);
                    setlstor("customerID", res.data.customerID);
                    setlstor("userID", res.data.userID);
                    setlstor("loginName", res.data.loginName);

                    goToUrl(res.message);


                }
                else {

                    alertB("خطا", res.message, "error");

                }

            }, true);

        } catch (e) {

        }

    }

    function prevLoginC() {

        try {

            $("#divLogin").show();
            $("#divAuthenticatedCode").hide();

        } catch (e) {

        }

    }

    web.AccountController = {
        LoginA: loginA,
        LoginShowPass: loginShowPass,
        LoginC: loginC,
        AutenticatedCode: autenticatedCode,
        InitLoginC: initLoginC,
        PrevLoginC: prevLoginC
    };

})(Web, jQuery);