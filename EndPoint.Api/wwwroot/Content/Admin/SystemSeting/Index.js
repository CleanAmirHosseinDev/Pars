$(document).ready(function () {

    fillGridUser();

    onSetting = function (e) {

        AjaxCall('/api/admin/SystemSeting/Get_SystemSeting/' + e.data("id"),
            null
            , 'GET').done(function (response) {

                if (!GetNullEmpetyUndefined(response) && response.systemSetingID > 0) {

                    $('#isActive').prop('checked', response.isActive === 15 ? true : false);
                    $("#ID").val(encrypt(response.systemSetingID.toString(), keyMaker()));
                    $('#Label').val(response.label);
                    $('#LabelCode').val(response.labeCode);
                 
                    $('#BaseAmount').val(response.baseAmount);
                    $('#FromAmount').val(response.fromAmount);
                    $('#ToAmount').val(response.toAmount);
                    $('.for-Add').hide();
                    $("#SeystemId").prop("disabled", true);
                    $("#roundText").val(response.value);
                    $('#conferm').show();
                    $('#conferm-addoredit').hide();
                    $('#conferm-cancel').hide();
                    $('#modelG').show();


                }
                else {
                    Swal.fire({
                        title: "خطا!",
                        text: !GetNullEmpetyUndefined(response) && !GetNullEmpetyUndefined(response.message) ? response.message : "خطای غیر منتظره !",
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
                    'FromAmount': !GetNullEmpetyUndefined($("#FromAmount").val()) ? parseFloat($("#FromAmount").val()) : null,
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
                        text: !GetNullEmpetyUndefined(response) && !GetNullEmpetyUndefined(response.message) ? response.message : "خطای غیر منتظره !",
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

        AjaxCall('/api/admin/SystemSeting/Get_SystemSetings/',


            JSON.stringify(
                {
                    'PageIndex': 0,
                    'PageSize': 0
                    /*'LabeCode': e.data('labecode')*/
                })
            , 'POST').done(function (response) {

                if (response.isSuccess === true) {
                    var str = '';

                    var menuNotDuplicate = [];
                    //remove Duplicate
                    $.each(response.data, function (key, value) {
                        var exists = false;
                        $.each(menuNotDuplicate, function (k, val2) {
                            if (value.label == val2.label) { exists = true };
                        });
                        if (exists == false && value.label != "") { menuNotDuplicate.push(value); }
                    });


                    //var str = '<option value="-1">انتخاب کنید</option>';
                   // $('#SeystemId').html(str);
                    $.each(menuNotDuplicate, function (key, value) {

                       // str += '<option data-toAmount="' + value.toAmount + '" data-fromAmount="' + value.fromAmount + '" data-baseAmount="' + value.baseAmount + '" data-label="' + value.label + '" data-isActive="' + value.isActive + '" data-systemSetingID="' + value.systemSetingID + '" value="' + value.labeCode + '">' + value.label + '</option>';
                        setSelect('#SeystemId', value.labeCode, value.label)
                    });

                   // $('#SeystemId').html(str);

                   
                    $("#SeystemId").prop("disabled", false);
                    $('.for-Add').show();
                                $('#conferm').hide();
                    $('#modelG').show();

                }
                else {
                    Swal.fire({
                        title: "خطا!",
                        text: !GetNullEmpetyUndefined(response) && !GetNullEmpetyUndefined(response.message) ? response.message : "خطای غیر منتظره !",
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


    });

    $('#SeystemId').on('change', function (e) {
        e.preventDefault();
        if (e.target.value === "-1") {
            var str = '<option value="-1">انتخاب کنید</option>';
            $('#countrySelect').html(str);
            $('#conferm-addoredit').hide();
            $('#conferm-cancel').hide();
            e.stopPropagation();
            return;
        }
        AjaxCall('/api/admin/SystemSeting/Get_SystemSetings/',


            JSON.stringify(
                {
                    'PageIndex': 0,
                    'PageSize': 0,
                    'LabeCode': e.target.value
                })
            , 'POST').done(function (response) {

                if (response.isSuccess === true) {

                    var str = '<option value="-1">انتخاب کنید</option>';
                    $('#countrySelect').html(str);
                    $.each(response.data, function (key, value) {

                        str += '<option data-toAmount="' + value.toAmount + '" data-fromAmount="' + value.fromAmount + '" data-baseAmount="' + value.baseAmount + '" data-label="' + value.label + '" data-isActive="' + value.isActive + '" data-systemSetingID="' + value.systemSetingID + '" value="' + value.labeCode + '">' + value.value + '</option>';
                    });

                    $('#countrySelect').html(str);

                    $('#conferm-addoredit').show();

                }
                else {
                    Swal.fire({
                        title: "خطا!",
                        text: !GetNullEmpetyUndefined(response) && !GetNullEmpetyUndefined(response.message) ? response.message : "خطای غیر منتظره !",
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
        e.stopPropagation();
    });

    $('#countrySelect').on('click', 'option', function (e) {
        e.preventDefault();

        if (e.target.value === "-1") {
            var str = '<option value="-1">انتخاب کنید</option>';
            $('#countrySelect').html(str);
            $('#conferm-addoredit').hide();
            $('#conferm-cancel').hide();
            e.stopPropagation();
            return;
        }

        $('#isActive').prop('checked', e.target.dataset.isactive === "15" ? true : false);
        $("#ID").val(encrypt(e.target.dataset.systemsetingid.toString(), keyMaker()));
        $('#Label').val(e.target.dataset.label);
        $('#LabelCode').val(e.target.value);
        $("#roundText").val(e.target.label);
        $('#BaseAmount').val(e.target.dataset.baseamount === "null" ? null : e.target.dataset.baseamount);
        $('#FromAmount').val(e.target.dataset.fromamount === "null" ? null : e.target.dataset.fromamount);
        $('#ToAmount').val(e.target.dataset.toamount === "null" ? null : e.target.dataset.toamount);

        $('#conferm-cancel').show();


        e.stopPropagation();
    });

    $('#conferm-cancel').on('click', function (e) {
        e.preventDefault();
        $("#SeystemId").val("-1").change();
        var str = '<option value="-1">انتخاب کنید</option>';
        $('#countrySelect').html(str);
        $("#roundText").val('');
        $('#isActive').prop('checked', false);
        $('#conferm-addoredit').hide();
        $('#conferm-cancel').hide();
     
        e.stopPropagation();
    });

    $('#conferm-addoredit').on('click', function (e) {
        e.preventDefault();

        if (GetNullEmpetyUndefined($("#roundText").val())) {
            $("#roundText")[0].className = "form-control round is-invalid";
            $(".invalid-feedback").html("فیلد مقدار اجباری است");

        }
        var obj;
        if (GetNullEmpetyUndefined($("#ID").val())) {//add
            obj = JSON.stringify(
                {
                    'SystemSetingID': 0,
                    'IsActive': ($('#isActive')[0].checked ? 15 : 14),
                    'Label': $('#SeystemId').find(":selected").text(),
                    'LabeCode': $('#SeystemId').find(":selected").val(),
                    'Value': $("#roundText").val(),
                    'BaseAmount': !GetNullEmpetyUndefined($("#BaseAmount").val()) ? parseInt($("#BaseAmount").val()) : null,
                    'FromAmount': !GetNullEmpetyUndefined($("#FromAmount").val()) ? parseFloat($("#FromAmount").val()) : null,
                    'ToAmount': !GetNullEmpetyUndefined($("#ToAmount").val()) ? parseFloat($("#ToAmount").val()) : null
                });
        } else {//edit
            obj = JSON.stringify(
                {
                    'SystemSetingID': decrypt($("#ID").val(), keyMaker()),
                    'IsActive': ($('#isActive')[0].checked ? 15 : 14),
                    'Label': $("#Label").val(),
                    'LabeCode': $("#LabelCode").val(),
                    'Value': $("#roundText").val(),
                    'BaseAmount': !GetNullEmpetyUndefined($("#BaseAmount").val()) ? parseInt($("#BaseAmount").val()) : null,
                    'FromAmount': !GetNullEmpetyUndefined($("#FromAmount").val()) ? parseFloat($("#FromAmount").val()) : null,
                    'ToAmount': !GetNullEmpetyUndefined($("#ToAmount").val()) ? parseFloat($("#ToAmount").val()) : null
                });
        }
        AjaxCall('/api/admin/SystemSeting/Save_SystemSeting/',
            obj
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
                        $('#conferm-cancel').click();
                    });

                }
                else {
                    Swal.fire({
                        title: "خطا!",
                        text: !GetNullEmpetyUndefined(response) && !GetNullEmpetyUndefined(response.message) ? response.message : "خطای غیر منتظره !",
                        type: "error",
                        confirmButtonClass: 'btn btn-primary',
                        buttonsStyling: false,
                        confirmButtonText: 'متوجه شدم!',
                    }).then(function () {
                        $('#conferm-cancel').click();
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
});

var customHTML = function (params) {

    return '<div data-id="' + params.data.id + '" data-name="' + params.data.name + '" data-labeCode="' + params.data.labeCode + '" data-isActive="' + params.data.isActive + '" onclick="onSetting($(this))" data-toggle="modal" data-target="#exampleModalScrollable"  style="width: 100% !important;height: inherit !important;min-width: 50px !important;" >' + params.value + '</div>';

};

fillGridUser = function () {
    AjaxCall('/api/admin/SystemSeting/Get_SystemSetings/',

        JSON.stringify(
            {
                'PageIndex': 1,
                'PageSize': 500
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