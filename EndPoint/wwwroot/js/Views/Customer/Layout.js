





(function (web, $) {

    //Document Ready              

    function signOutForExitToLogin() {

        dellstor("token");        
        dellstor("fullName");
        dellstor("customerID");
        dellstor("userID");

        goToUrl("/Account/LoginC");

    }

    function initLayout() {

        if (isEmpty(getlstor("token"))) goToUrl("/Account/LoginC");
        else {

            $(".fullNameInLayout").html(getlstor("fullName"));            

        }

    }

    web.Layout = {

        SignOutForExitToLogin: signOutForExitToLogin,
        InitLayout: initLayout

    };

})(Web, jQuery);