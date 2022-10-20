
$(document).ready(function () {

    $('#confirm').on('click', function (e) {
        e.preventDefault();

        e.stopPropagation();
    });

    $('form').on('submit', function (event) {
        event.preventDefault();

        AjaxCall('/api/Securitys/Login', JSON.stringify(
            {
                'Mobile': $("#username").val()


            }), 'POST').done(function (response) {

                if (response.isSuccess === true) {

                    var res = response.data.customerID;
                    Swal.fire({
                        title: 'کد تایید را وارد کنید',
                        input: 'number',
                        inputAttributes: {
                            autocapitalize: 'off'
                        },
                        icon: 'info',
                        showCancelButton: false,
                        allowOutsideClick: false,
                        allowEscapeKey: false,
                        confirmButtonText: 'تایید',
                        showLoaderOnConfirm: true,
                        preConfirm: (login) => {
                            deb();
                            AjaxCall('/api/Securitys/AutenticatedCode', JSON.stringify(
                                {
                                    'Code': login,
                                    //این خط کد زیر هرکار میکنم توی مرورگر نمیاد منظورم کل خطشه یه کاری کن بیاد مشکلت حل میشه
                                    'Bakdslkflkdsflkdslkfkldskfdslflsdkf_dnsfhsdkfh': res
                                }
                            ), 'POST').done(function (response) {

                                    if (response.isSuccess === true) {
                                        setlstor("token", !GetNullEmpetyUndefined(response.data.token) ? response.data.token : "");
                                        setlstor("menu", !GetNullEmpetyUndefined(response.data.menus) && response.data.menus.length > 0 ? JSON.stringify(response.data.menus) : "");
                                        setlstor("fullName", !GetNullEmpetyUndefined(response.data.fullName) ? response.data.fullName : "فاقد نام");
                                        setlstor("userID", !GetNullEmpetyUndefined(response.data.userID) ? response.data.userID : "0");
                                        window.location.href = response.message;

                                    } else {
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
                            //return fetch('/api/Securitys/Login')
                            //    .then(response => {
                            //        deb();
                            //        if (!response.ok) {
                            //            throw new Error(response.statusText)
                            //        }
                            //        return response.json()
                            //    })
                            //    .catch(error => {
                            //        Swal.showValidationMessage(
                            //            `فیلد اجباری است: ${error}`
                            //        )
                            //    })
                        },
                        allowOutsideClick: () => !Swal.isLoading()
                    }).then((result) => {
                        if (result.isConfirmed) {

                        }
                    })

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
