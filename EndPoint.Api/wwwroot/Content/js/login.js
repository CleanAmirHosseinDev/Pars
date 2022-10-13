
$(document).ready(function () {

    $('#confirm').on('click', function (e) {
        e.preventDefault();

        e.stopPropagation();
    });

    $('form').on('submit', function (event) {
        event.preventDefault();

        AjaxCall('/api/Securitys/Login', JSON.stringify(
            {
                'Username': $("#username").val(),
                'Password': $("#password").val()

            }), 'POST').done(function (response) {

                if (response.isSuccess === true) {

                    setlstor("token", !GetNullEmpetyUndefined(response.data.token) ? response.data.token : "");
                    setlstor("menu", !GetNullEmpetyUndefined(response.data.menus) && response.data.menus.length > 0 ? JSON.stringify(response.data.menus) : "");
                    setlstor("fullName", !GetNullEmpetyUndefined(response.data.fullName) ? response.data.fullName : "فاقد نام");
                    setlstor("userID", !GetNullEmpetyUndefined(response.data.userID) ? response.data.userID : "0");
                    setlstor("customerID", !GetNullEmpetyUndefined(response.data.customerID) ? response.data.customerID : "");

                    window.location.href = response.message;
                }
                else {
                    Swal.fire({
                        title: "خطا!",
                        text: response.message,
                        type: "error",
                        confirmButtonClass: 'btn btn-primary',
                        buttonsStyling: false,
                    }).then(function () {

                    });


                }
            }).fail(function (error) {

                Swal.fire({
                    title: "خطا!",
                    text: "خطای غیر منتظره!",
                    type: "error",
                    confirmButtonClass: 'btn btn-primary',
                    buttonsStyling: false,
                }).then(function () {

                });
            });

        event.stopPropagation();
    });

});
