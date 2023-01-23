





(function (web, $) {

    //Document Ready              

    function signOutForExitToLogin() {

        dellstor("token");        
        dellstor("fullName");
        dellstor("userID");
        dellstor("loginName");
        dellstor("menu");
        dellstor("roleDesc");

        goToUrl("/Account/LoginUser");

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