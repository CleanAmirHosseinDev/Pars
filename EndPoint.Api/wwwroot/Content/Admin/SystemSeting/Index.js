$(document).ready(function () {

    fillGridUser();

    onSetting = function (e) {
        if (false) {
            AjaxCall('/api/admin/SystemSeting/Get_SystemSetings/',


                JSON.stringify(
                    {
                        'PageIndex': 0,
                        'PageSize': 0
                        /*'LabeCode': e.data('labecode')*/
                    })
                , 'POST').done(function (response) {
                    deb();
                    if (response.isSuccess === true) {
                        var str = '';
                        for (var i = 0; i < response.data.length; i++) {
                            str += '<option value="' + response.data[i].systemSetingID + '" >' + response.data[i].value + '</option>';
                        }
                        $('#SeystemId').html(str);
                    }
                    else {
                        Swal.fire({
                            title: "خطا!",
                            text: response.message,
                            type: "error",
                            confirmButtonClass: 'btn btn-primary',
                            buttonsStyling: false,
                            confirmButtonText: 'متوجه شدم!',
                        }).then(function () {
                            window.location.href = "/Admin/Home/index";
                        });

                    }
                }).fail(function (error) {

                    Swal.fire({
                        title: "خطا!",
                        text: "خطای غیر منتظره!",
                        type: "error",
                        confirmButtonClass: 'btn btn-primary',
                        buttonsStyling: false,
                        confirmButtonText: 'متوجه شدم!',
                    }).then(function () {
                        window.location.href = "/Admin/Home/index";
                    });
                });
        }
        AjaxCall('/api/admin/SystemSeting/Get_SystemSeting/' + e.data("id"),
            null
            , 'GET').done(function (response) {

                if (!GetNullEmpetyUndefined(response) && response.systemSetingID > 0) {
                    deb();
                    $('#isActive').prop('checked', response.isActive === 15 ? true : false);
                    $("#ID").val(encrypt(response.systemSetingID.toString(), keyMaker()));
                    $('#Label').val(response.label);
                    $('#LabelCode').val(response.labeCode);

                    $('#BaseAmount').val(response.baseAmount);
                    $('#FromAmount').val(response.fromAmount);
                    $('#ToAmount').val(response.toAmount);

                    $("#roundText").val(response.value);
                    $('#modelG').show();


                }
                else {
                    Swal.fire({
                        title: "خطا!",
                        text: response.message,
                        type: "error",
                        confirmButtonClass: 'btn btn-primary',
                        buttonsStyling: false,
                        confirmButtonText: 'متوجه شدم!',
                    }).then(function () {
                        $('#exampleModalScrollable').modal('hide');
                    });


                }
            }).fail(function (error) {

                Swal.fire({
                    title: "خطا!",
                    text: "خطای غیر منتظره !",
                    type: "error",
                    confirmButtonClass: 'btn btn-primary',
                    buttonsStyling: false,
                    confirmButtonText: 'متوجه شدم!',
                }).then(function () {
                    $('#exampleModalScrollable').modal('hide');
                });
            });




    }

    $('#roundText').on('keyup', function (e) {
        e.preventDefault();
        if (!GetNullEmpetyUndefined($("#roundText").val())) {
            $("#roundText")[0].className = "form-control round";
            $(".invalid-feedback").html("");

        } else {
            $("#roundText")[0].className = "form-control round is-invalid";
            $(".invalid-feedback").html("فیلد مقدار  اجباری است");
        }
        e.stopPropagation();
    });

    $('#conferm').on('click', function (e) {
        e.preventDefault();

        if (GetNullEmpetyUndefined($("#roundText").val())) {
            $("#roundText")[0].className = "form-control round is-invalid";
            $(".invalid-feedback").html("فیلد مقدار اجباری است");

        }


        AjaxCall('/api/admin/SystemSeting/Save_SystemSeting/',

            JSON.stringify(
                {
                    'SystemSetingID': decrypt($("#ID").val(), keyMaker()),
                    'IsActive': ($('#isActive')[0].checked ? 15 : 14),
                    'Label': $("#Label").val(),
                    'LabeCode': $("#LabelCode").val(),
                    'Value': $("#roundText").val(),
                    'BaseAmount': !GetNullEmpetyUndefined($("#BaseAmount").val()) ? parseInt($("#BaseAmount").val()) : null,
                    'FromAmount': !GetNullEmpetyUndefined($("#FromAmount").val()) ? parseFloat($("#FromAmount").val()) : null ,
                    'ToAmount': !GetNullEmpetyUndefined($("#ToAmount").val()) ? parseFloat($("#ToAmount").val()) : null

                })
            , 'POST').done(function (response) {
                if (response.isSuccess === true) {
                    Swal.fire({
                        title: "موفقیت آمیز!",
                        text: response.message,
                        type: "success",
                        confirmButtonClass: 'btn btn-primary',
                        buttonsStyling: false,
                        confirmButtonText: 'متوجه شدم!',
                    }).then(function () {


                        $('#exampleModalScrollable').modal('hide');
                        location.reload();
                    });

                }
                else {
                    Swal.fire({
                        title: "خطا!",
                        text: response.message,
                        type: "error",
                        confirmButtonClass: 'btn btn-primary',
                        buttonsStyling: false,
                        confirmButtonText: 'متوجه شدم!',
                    }).then(function () {

                        $('#exampleModalScrollable').modal('hide');
                    });


                }
            }).fail(function (error) {

                Swal.fire({
                    title: "خطا!",
                    text: "خطای غیر منتظره!",
                    type: "error",
                    confirmButtonClass: 'btn btn-primary',
                    buttonsStyling: false,
                    confirmButtonText: 'متوجه شدم!',
                }).then(function () {
                    $('#exampleModalScrollable').modal('hide');
                });
            });
        e.stopPropagation();
    });
    $('#confermAddOrEdit').on('click', function (e) {
        alert("kjfdshj")
    });

});


var customHTML = function (params) {

    return '<div data-id="' + params.data.id + '" data-name="' + params.data.name + '" data-labeCode="' + params.data.labeCode + '" data-isActive="' + params.data.isActive + '" onclick="onSetting($(this))" data-toggle="modal" data-target="#exampleModalScrollable"  style="width: 100% !important;height: inherit !important;min-width: 50px !important;" >' + params.value + '</div>';

};

fillGridUser = function () {
    AjaxCall('/api/admin/SystemSeting/Get_SystemSetings/',

        JSON.stringify(
            {
                'PageIndex': 1,
                'PageSize': 100
            })
        , 'POST').done(function (response) {
            deb();
            if (response.isSuccess === true) {
                var temp = [];

                response.data.forEach(function (item, i) {
                    temp.push({ "row": (i + 1), "id": item.systemSetingID, "isActive": item.isActive, "isActiveStr": item.isActiveStr, "value": item.value, "labeCode": item.labeCode, "name": item.label, "baseAmount": item.baseAmount, "fromAmount": item.fromAmount, "toAmount": item.toAmount });
                });

                var columnDefs = [{
                    headerName: 'ردیف',
                    field: 'row',
                    width: 200,
                    filter: true,
                    cellRenderer: customHTML
                },
                {
                    headerName: 'نام ',
                    field: 'name',
                    filter: true,
                    width: 200,
                    cellRenderer: customHTML
                },
                {
                    headerName: 'مقدار ',
                    field: 'value',
                    filter: true,
                    width: 200,
                    cellRenderer: customHTML
                },
                {
                    headerName: 'وضعیت',
                    field: 'isActiveStr',
                    filter: true,
                    width: 200,
                    cellRenderer: customHTML

                }

                ];

                fillGrid(temp, columnDefs, columnCountShow = 10, nameGrid = "myGrid");
            }
            else {
                Swal.fire({
                    title: "خطا!",
                    text: response.message,
                    type: "error",
                    confirmButtonClass: 'btn btn-primary',
                    buttonsStyling: false,
                    confirmButtonText: 'متوجه شدم!',
                }).then(function () {
                    window.location.href = "/Admin/Home/index";
                });

            }
        }).fail(function (error) {

            Swal.fire({
                title: "خطا!",
                text: "خطای غیر منتظره!",
                type: "error",
                confirmButtonClass: 'btn btn-primary',
                buttonsStyling: false,
                confirmButtonText: 'متوجه شدم!',
            }).then(function () {
                window.location.href = "/Admin/Home/index";
            });
        });


};