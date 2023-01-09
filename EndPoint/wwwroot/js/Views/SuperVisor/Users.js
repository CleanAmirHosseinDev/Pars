






(function (web, $) {

    //Document Ready      

    function updatePass() {

        AjaxCallAction("POST", "/api/superVisor/Users/UpdatePass_Users", JSON.stringify({ OldPassword: $("#OldPassword").val(), NewPassword: $("#NewPassword").val(), ConfirmPassword: $("#ConfirmPassword").val() }), true, function (result) {

            debuggerWeb();

            if (result.isSuccess) {

                alertB("", result.message, "success");

            }
            else {

                alertB("خطا", result.message, "error");

            }

        }, true);

    }

    web.Users = {
        UpdatePass: updatePass
    };

})(Web, jQuery);