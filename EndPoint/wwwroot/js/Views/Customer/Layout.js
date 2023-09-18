





(function (web, $) {

    //Document Ready              

    function signOutForExitToLogin() {

        AjaxCallActionWithotHeading("POST", "/api/Securitys/SignOutForExitToLogin", JSON.stringify({ Userid: getlstor("customerID"), AreaName: "مشتری" }), true, function (res) {
            debugger;
            if (res.isSuccess) {

                //dellstor("token");
                localStorage.removeItem("token");

                dellstor("fullName");
                dellstor("customerID");
                dellstor("userID");
                dellstor("loginName");


                goToUrl("/Account/Login");

            }

        }, false);

    }

    function initLayout() {

        /*if (isEmpty(getlstor("token"))) goToUrl("/Account/Login");*/
        if (isEmpty(localStorage.getItem("token"))) goToUrl("/Account/Login");
        else {

            $(".fullNameInLayout").html(getlstor("fullName"));

        }

    }

    web.Layout = {

        SignOutForExitToLogin: signOutForExitToLogin,
        InitLayout: initLayout

    };

})(Web, jQuery);