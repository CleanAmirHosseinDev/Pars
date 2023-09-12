





(function (web, $) {

    //Document Ready              

    function signOutForExitToLogin() {


        AjaxCallAction("POST", "/api/Securitys/SignOutForExitToLogin", JSON.stringify({ Userid: getlstor("userID"), AreaName: getlstor("roleDesc") }), true, function (res) {
            debugger;
            if (res.isSuccess) {

                dellstor("token");
                dellstor("fullName");
                dellstor("userID");
                dellstor("loginName");
                dellstor("menu");
                dellstor("roleDesc");

                goToUrl("/Account/LoginUser");

            }

        }, false);        

    }

    function initLayout() {

        if (isEmpty(getlstor("token"))) goToUrl("/Account/LoginUser");
        else {

            $(".fullNameInLayout").html(getlstor("fullName"));
            $(".roleDescNameElementLayout").html(getlstor("roleDesc"));
        }

    }

    web.Layout = {

        SignOutForExitToLogin: signOutForExitToLogin,
        InitLayout: initLayout

    };

})(Web, jQuery);