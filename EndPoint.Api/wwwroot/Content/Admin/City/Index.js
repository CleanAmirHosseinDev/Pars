$(document).ready(function () {
  
    fillGridUser();

    onSetting = function (e) {


        AjaxCall('/api/admin/City/Get_City/' + e.data("id"),
            null
            , 'GET').done(function (response) {

                if (!GetNullEmpetyUndefined(response) && response.cityID > 0) {

                    $('#modalG').show();

                    $("#ID").val(encrypt(response.cityID.toString(), keyMaker()));
                    $('#StateIDH').val(encrypt(response.stateID.toString(), keyMaker()));
                    $("#roundText").val(response.cityName)

                    $("#StateId").val(response.stateID.toString()).change();

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
            $(".invalid-feedback").html("فیلد نام شهر  اجباری است");
        }
        e.stopPropagation();
    });

    $('#conferm').on('click', function (e) {
        e.preventDefault();

        if (GetNullEmpetyUndefined($("#roundText").val())) {
            $("#roundText")[0].className = "form-control round is-invalid";
            $(".invalid-feedback").html("فیلد نام شهر اجباری است");

        }


        AjaxCall('/api/admin/City/Save_City/',

            JSON.stringify(
                {
                    'CityID': decrypt($("#ID").val(), keyMaker()),
                    'StateID': decrypt($("#StateIDH").val(), keyMaker()),
                    'CityName': $("#roundText").val()
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

});
  var customHTML = function (params) {

        return '<div data-id="' + params.data.id + '" data-name="' + params.data.name + '" data-stateID="' + params.data.stateID + '" onclick="onSetting($(this))" data-toggle="modal" data-target="#exampleModalScrollable"  style="width: 100% !important;height: inherit !important;min-width: 50px !important;" >' + params.value + '</div>';

    };

fillGridUser = function () {
    AjaxCall('/api/admin/City/Get_Citys/',

        JSON.stringify(
            {
                'PageIndex': 1,
                'PageSize': 100
            })
        , 'POST').done(function (response) {
            deb();
            if (response.isSuccess === true) {
                var temp = [];
                deb();
                response.data.forEach(function (item, i) {
                    temp.push({ "row": (i + 1), "id": item.cityID, "name": item.cityName, "nameState": item.state.stateName, "StatestateID": item.state.stateID, "stateID": item.stateID });
                });

                var columnDefs = [{
                    headerName: 'ردیف',
                    field: 'row',
                    width: 200,
                    filter: true,
                    cellRenderer: customHTML
                },

                {
                    headerName: 'نام شهر',
                    field: 'name',
                    filter: true,
                    width: 200,
                    //cellRenderer: customAvatarHTML,
                },
                {
                    headerName: 'نام استان',
                    field: 'nameState',
                    filter: true,
                    width: 200,
                    cellRenderer: customHTML
                },
                    //{
                    //    headerName: 'عملیات',
                    //    field: 'setting_',
                    //    filter: true,
                    //    width: 200,
                    //    cellRenderer: function (params) {

                    //        return '<div class="d-flex justify-content-center"><a href="#" style="font-size: 26px;" class="nav-link" data-id="' + params.data.id + '" data-name="' + params.data.name + '" onclick="onSetting($(this))" data-toggle="modal" data-target="#exampleModalScrollable" > <i class="ficon feather icon-check-square"></i></a> </div>';
                    //    }
                    //}
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
                    $('.logoff').click();
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

            });
        });

    AjaxCall('/api/admin/City/Get_States_Combo/',

        JSON.stringify(
            {
                'PageIndex': 1,
                'PageSize': 100
            })
        , 'POST').done(function (response) {
            deb();
            if (response.isSuccess === true) {
                var srt = '';
                for (var i = 0; i < response.data.length; i++) {
                    srt += '<option value="' + response.data[i].stateID + '">' + response.data[i].stateName + '</option>';
                }
                $("#select2-customize-result").html(srt);


            }

        }).fail(function (error) {


        });

    AjaxCall('/api/admin/City/Get_States_Combo/',

        JSON.stringify(
            {
                'PageIndex': 1,
                'PageSize': 100
            })
        , 'POST').done(function (response) {

            if (response.isSuccess === true) {
                var str = '';
                for (var i = 0; i < response.data.length; i++) {
                    str += '<option value="' + response.data[i].stateID + '" >' + response.data[i].stateName + '</option>';
                }
                $('#StateId').html(str);
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