





(function (web, $) {

    //Document Ready              

    function signOutForExitToLogin() {

        dellstor("token");        
        dellstor("fullName");
        dellstor("userID");
        dellstor("loginName");
        dellstor("menu");

        goToUrl("/Account/LoginA");

    }

    function initLayout() {

        if (isEmpty(getlstor("token"))) goToUrl("/Account/LoginA");
        else {

            $(".fullNameInLayout").html(getlstor("fullName"));

        }

    }

    web.Layout = {

        SignOutForExitToLogin: signOutForExitToLogin,
        InitLayout: initLayout

    };

})(Web, jQuery);