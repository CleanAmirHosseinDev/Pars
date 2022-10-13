$(document).ready(function () {
    if (false) {
        AjaxCall('/api/admin/Users/Get_Userss',
            {
                'PageIndex': 0,
                'PageSize': 1,
                'Search': ""

            }, 'POST', key = "Authorization", val = "Bearer" + getlstor("token")).done(function (response) {

                if (response.isSuccess === true) {

                    alert("sdfgsdg");
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
    }
});