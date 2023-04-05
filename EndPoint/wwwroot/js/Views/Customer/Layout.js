





(function (web, $) {

    //Document Ready              

    function signOutForExitToLogin() {

        dellstor("token");        
        dellstor("fullName");
        dellstor("customerID");
        dellstor("userID");
        dellstor("loginName");


        goToUrl("/Account/Login");

    }

    function initLayout() {

        if (isEmpty(getlstor("token"))) goToUrl("/Account/Login");
        else {

            $(".fullNameInLayout").html(getlstor("fullName"));            

        }

    }

    web.Layout = {

        SignOutForExitToLogin: signOutForExitToLogin,
        InitLayout: initLayout

    };

})(Web, jQuery);