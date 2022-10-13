$(document).ready(function () {
    if (true) {
        
        AjaxCall('/api/admin/Users/Get_Userss/',
            
            JSON.stringify(
                {
                    'PageIndex': 1,
                    'PageSize': 10

                })
            , 'POST').done(function (response) {

                if (response.isSuccess === true) {

                    alert("از ای پی آی با موفقیت کال شد تبریک می گوییم به شما دینه ها");
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