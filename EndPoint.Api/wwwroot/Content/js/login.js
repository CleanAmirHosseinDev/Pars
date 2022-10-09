
$(document).ready(function () {

    $('#confirm').on('click', function (e) {
        e.preventDefault();

        e.stopPropagation();
    });

    $('form').on('submit', function (event) {
        event.preventDefault();
        var formData = new FormData();
        formData.append("Userneme", $("#username").val());
        formData.append("Password", $("#password").val());

        $.ajax({
            type: 'POST',
            url: '/Home/LoginRequest',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            traditional: true,
            beforeSend: function () {

            },
            complete: function () {

            },
            success: function (response) {
                if (response.resultCode === 200) {

                    window.location.href = response.data;
                    this.submit(); //now submit the form
                }
                else {
                    debugger;

                }
            },
            error: function (response) {

            }
        })



        event.stopPropagation();
    });

});
AjaxCall = function (url, data, type) {
    return $.ajax({
        processData: false,
        url: url,
        type: type ? type : 'GET',
        data: data,
        async: false,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {


        },
        complete: function () {


        }
    });
};