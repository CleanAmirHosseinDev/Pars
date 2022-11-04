




(function (web, $) {

    //Document Ready          

    function loginA(e) {

        try {

            $(e).attr("disabled", "");

            AjaxCallAction("POST", "/api/Securitys/Login", JSON.stringify({ Username: $("#User").val(), Password: $("#Password").val() }), true, function (res) {

                $(e).removeAttr("disabled");

                if (res.isSuccess) {


                    setlstor("token", res.data.token);
                    setlstor("menu",  JSON.stringify(res.data.menus));
                    setlstor("fullName",  res.data.fullName);
                    setlstor("userID", res.data.userID);                    

                    goToUrl(res.message);

                }
                else {

                    alertB("خطا", res.message, "error");                    

                }

            }, false);

        } catch (e) {

        }

    }

    function loginShowPass() {

        try {

            $('#Password').attr('type', $('#Password').attr('type') == 'password' ? 'text' : 'password');

        } catch (e) {

        }

    }


    web.AccountController = {
        LoginA: loginA,
        LoginShowPass: loginShowPass
    };

})(Web, jQuery);