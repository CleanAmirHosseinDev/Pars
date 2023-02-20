




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
                    setlstor("roleDesc", res.data.roleDesc);

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

    function loginC(e, aslkewkdkmscedkwlssdjcm = false, nkekkfjdkjjkjkdjkdjkjkkj = false) {


        try {


            if (isEmpty($("#User").val())) {

                alertB("خطا", "شماره تلفن همراه را وارد کنید", "error");
                return;
            }

            if (isEmpty($("#NationalCode").val())) {
                alertB("خطا", ($("input[name='RadioSelectSha']:checked").val() == "0" ? "شناسه ملی شرکت " :"کد ملی ")+ "را وارد کنید", "error");
                return;
            }

            if ($("#NationalCode").val().length < 10) {
                alertB("خطا", ($("input[name='RadioSelectSha']:checked").val() == "0" ? "شناسه ملی شرکت " : "کد ملی ") + "نمی تواند کمتر از 10 حرف باشد", "error");
                return;
            }

            $(e).attr("disabled", "");

            AjaxCallAction("POST", "/api/Securitys/Login", JSON.stringify({ Mobile: $("#User").val(), CaptchaCodes: $("#form_n1 input[name='CaptchaCodes']").val(), NationalCode: $("#NationalCode").val(), aslkewkdkmscedkwlssdjcm: aslkewkdkmscedkwlssdjcm, nkekkfjdkjjkjkdjkdjkjkkj: nkekkfjdkjjkjkdjkdjkjkkj }), true, function (res) {

                $(e).removeAttr("disabled");
                debugger;
                if (res.isSuccess) {

                    if (res.data.iNSt2 == true) {

                        alertB("", res.message, "success");

                    }
                    else {

                        showTab2();

                        $("#Bakdslkflkdsflkdslkfkldskfdslflsdkf_dnsfhsdkfh").val(encrypt(res.data.customerID, keyMaker()));

                        $("#Fulllfsdfdsflsfldsfldslflsdlfdslflsdlfldsflldsf").val(encrypt(res.data.fullName, keyMaker()));

                    }


                    //$("#divLogin").hide();
                    //$("#divAuthenticatedCode").show();

                }
                else {

                    if (res.data != null && res.data.aslkewkdkmscedkwlssdjcm == true) {

                        confirmB("", res.message, "warning", function () {

                            loginC(e, true);

                        }, function () {

                        }, ["خیر", "بلی"]);

                    }
                    else if (res.data != null && res.data.nkekkfjdkjjkjkdjkdjkjkkj == true) {

                        confirmB("", res.message, "warning", function () {

                            

                        }, function () {

                            loginC(e, false, true);

                        }, ["دسترسی با شماره جدید", "بله متوجه شدم"]);

                    }
                    else alertB("خطا", res.message, "error");

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

    function onChangeCSh(e) {

        if ($(e).val() == "0") {
            $("#NationalCode").attr("placeholder", "شناسه ملی شرکت");
        }
        else {
            $("#NationalCode").attr("placeholder", "کد ملی");
        }

    }

    web.AccountController = {
        LoginA: loginA,
        LoginShowPass: loginShowPass,
        LoginC: loginC,
        AutenticatedCode: autenticatedCode,
        InitLoginC: initLoginC,
        PrevLoginC: prevLoginC,
        OnChangeCSh: onChangeCSh
    };

})(Web, jQuery);