$(document).ready(function () {



    fillGridUser();



    onSetting = function (e) {


        AjaxCall('/api/admin/City/Get_City/' + e.data("id"),
            null
            , 'GET').done(function (response) {
            
                if (!GetNullEmpetyUndefined(response) && response.cityID > 0) {
                    
                    $('#modelUser').show();

                    $("#ID").val(encrypt(response.cityID.toString(), keyMaker()));
                    $("#roundText").val(response.cityName);
                    //stateID
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
            $(".invalid-feedback").html("فیلد نام استان  اجباری است");
        }
        e.stopPropagation();
    });
    $('#conferm').on('click', function (e) {
        e.preventDefault();

        if (GetNullEmpetyUndefined($("#roundText").val())) {
            $("#roundText")[0].className = "form-control round is-invalid";
            $(".invalid-feedback").html("فیلد نام استان  اجباری است");

        }


        AjaxCall('/api/admin/State/Save_State/',

            JSON.stringify(
                {
                    'StateID': decrypt($("#ID").val(), keyMaker()),
                    'StateName': $("#roundText").val()
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

                response.data.forEach(function (item, i) {
                    temp.push({ "id": item.cityID, "name": item.cityName, "statename": item.state.stateName, "stateid": item.stateID });
                });

                var columnDefs = [{
                    headerName: 'شناسه',
                    field: 'id',
                    width: 200,
                    filter: true,
                    /* checkboxSelection: true,
                     headerCheckboxSelectionFilteredOnly: true,
                     headerCheckboxSelection: true,*/

                },

                {
                    headerName: 'نام شهر',
                    field: 'name',
                    filter: true,
                    width: 200,

                },
                {
                    headerName: 'نام استان',
                    field: 'statename',
                    filter: true,
                    width: 200,
                    //cellRenderer: customAvatarHTML,
                },
                {
                    headerName: 'عملیات',
                    field: 'setting_',
                    filter: true,
                    width: 200,
                    cellRenderer: function (params) {
                        deb();
                        return '<div class="d-flex justify-content-center"><a href="#" style="font-size: 26px;" class="nav-link" data-id="' + params.data.id + '" data-stateid="' + params.data.stateid + '" onclick="onSetting($(this))" data-toggle="modal" data-target="#exampleModalScrollable" > <i class="ficon feather icon-check-square"></i></a> </div>';
                    }
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
};