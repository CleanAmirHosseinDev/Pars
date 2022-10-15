$(document).ready(function () {

    var customAvatarHTML = function (params) {
        return "<span class='avatar'><img src='" + params.data.avatar + "' height='32' width='32'></span>" + params.value
    }
    // ag-grid
    /*** COLUMN DEFINE ***/


    // users language select
    if ($("#users-language-select2").length > 0) {
        $("#users-language-select2").select2({
            dropdownAutoWidth: true,
            width: '100%'
        });
    }
    // users music select
    if ($("#users-music-select2").length > 0) {
        $("#users-music-select2").select2({
            dropdownAutoWidth: true,
            width: '100%'
        });
    }
    // users movies select
    if ($("#users-movies-select2").length > 0) {
        $("#users-movies-select2").select2({
            dropdownAutoWidth: true,
            width: '100%'
        });
    }
    // users birthdate date
    if ($(".birthdate-picker").length > 0) {
        $('.birthdate-picker').pickadate({
            format: 'mmmm, d, yyyy'
        });
    }

    fillGridUser();



    onAccessLevels = function (e) {

        $('#id-User').val(e.data("id"));
        for (var i = 0; i < $("a.nav-link").length; i++) {
            if ($("a.nav-link")[i].className === "nav-link" && $("a.nav-link")[i].dataset.id === $('#id-User').val()) {
                $('#roles-user').val($("a.nav-link")[i].dataset.roles);
            }
        }


        const myArray = $('#roles-user').val().split(",");
        AjaxCall('/api/admin/Users/GetAccessLevels/' + e.data("id"),
            null
            , 'GET').done(function (response) {

                if (response.length > 0) {
                    var menu = response;
                    var tempMenu = [];
                    response.forEach(function (item, i) {
                        tempMenu.push({ "group": item.group, "labelGroup": item.labelGroup, "group_Item": item.group_Item });
                    });
                    var menuNotDuplicate = [];
                    //remove Duplicate
                    $.each(tempMenu, function (key, value) {
                        var exists = false;
                        $.each(menuNotDuplicate, function (k, val2) {
                            if (value.group == val2.group) { exists = true };
                        });
                        if (exists == false && value.group != "") { menuNotDuplicate.push(value); }
                    });
                    var str = '';
                    $('#content-types').html(str);
                    str += '<div class="row">';

                    menuNotDuplicate.forEach(function (item, i) {

                        str += '<div class="col-xl-3 col-md-6 col-sm-12">';
                        str += '    <div class="card">';
                        str += '        <div class="card-header mb-1">';
                        str += '            <h4 class="card-title">' + item.labelGroup + '</h4>';
                        str += '        </div>';
                        str += '        <div class="card-content">';
                        str += '            <div class="card collapse-icon accordion-icon-rotate">';
                        str += '                <div class="card-body">';
                        str += '                    <div class="accordion" id="accordionExample' + i + '" data-toggle-hover="true">';
                        str += '                        <div class="collapse-margin">';
                        str += '                            <div class="card-header" id="headingOne' + i + '" data-toggle="collapse" role="button" data-target="#collapseOne' + i + '" aria-expanded="false" aria-controls="collapseOne' + i + '">';
                        str += '                                <span class="lead collapse-title collapsed">';
                        str += '                                    ' + item.group_Item + '';
                        str += '                                </span>';
                        str += '                            </div>';
                        str += '                            <div id="collapseOne' + i + '" class="collapse show" aria-labelledby="headingOne' + i + '" data-parent="#accordionExample' + i + '">';
                        str += '                                <div class="card-body">';

                        for (var i = 0; i < menu.length; i++)
                            if (item.group === menu[i].group) {
                                var result;
                                if (!GetNullEmpetyUndefined(myArray) && myArray.length > 0)
                                    result = myArray.filter(d => d === menu[i].value);

                                str += '                                    <fieldset>';
                                str += '                                        <div class="vs-checkbox-con vs-checkbox-primary">';
                                if (!GetNullEmpetyUndefined(result) && result.length > 0)
                                    str += '                                            <input type="checkbox" checked class="modal-val-user-id" value="' + menu[i].value + '"  value="' + menu[i].value + '">';
                                else
                                    str += '                                            <input type="checkbox" class="modal-val-user-id" value="' + menu[i].value + '">';

                                str += '                                                <span class="vs-checkbox">';
                                str += '                                                    <span class="vs-checkbox--check">';
                                str += '                                                        <i class="vs-icon feather icon-check"></i>';
                                str += '                                                    </span>';
                                str += '                                                </span>';
                                str += '                                                <span class="">' + menu[i].text + '</span>';
                                str += '                                        </div>';
                                str += '                                    </fieldset>';
                            }
                        // srt += '<li><a href="' + menu[i].link + '"><i class="feather icon-circle"></i><span id="' + menu[i].value + '" class="menu-item" data-i18n="List">' + menu[i].group_Item + '</span></a></li>';
                        str += '                                </div>';
                        str += '                            </div>';
                        str += '                        </div>';
                        str += '                    </div>';
                        str += '                </div>';
                        str += '            </div>';
                        str += '        </div>';
                        str += '    </div>';
                        str += '</div>';
                    });

                    str += '</div>';

                    $('#content-types').html(str);
                    $('#modelUser').show();

                }
                else {
                    Swal.fire({
                        title: "خطا!",
                        text: response.message,
                        type: "error",
                        confirmButtonClass: 'btn btn-primary',
                        buttonsStyling: false,
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
                }).then(function () {
                    $('#exampleModalScrollable').modal('hide');
                });
            });
    }

    $('#conferm').on('click', function (e) {
        e.preventDefault();
        let text;
        var tempData = [];
        for (var i = 0; i < $("input[type='checkbox']").length; i++) {

            if ($("input[type='checkbox']")[i].className === "modal-val-user-id" && $("input[type='checkbox']")[i].checked) {
                tempData.push($("input[type='checkbox']")[i].value);
            }

        }
        if (!GetNullEmpetyUndefined(tempData) && tempData.length > 0) {
            text = tempData.toString();
            AjaxCall('/api/admin/Users/Save_AccessLevels/',

                JSON.stringify(
                    {
                        'UserID': $('#id-User').val(),
                        'Roles': text
                    })
                , 'POST').done(function (response) {
                    if (response.isSuccess === true) {
                        Swal.fire({
                            title: "موفقیت آمیز!",
                            text: response.message,
                            type: "success",
                            confirmButtonClass: 'btn btn-primary',
                            buttonsStyling: false,
                        }).then(function () {

                            for (var i = 0; i < $("a.nav-link").length; i++) {
                                if ($("a.nav-link")[i].className === "nav-link" && $("a.nav-link")[i].dataset.id === $('#id-User').val()) {
                                    $("a.nav-link")[i].dataset.roles = text;
                                }
                            }
                            $('#id-User').val('');
                            $('#exampleModalScrollable').modal('hide');
                        });

                    }
                    else {
                        Swal.fire({
                            title: "خطا!",
                            text: response.message,
                            type: "error",
                            confirmButtonClass: 'btn btn-primary',
                            buttonsStyling: false,
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
                    }).then(function () {
                        $('#exampleModalScrollable').modal('hide');
                    });
                });
        }


        e.stopPropagation();
    });


});
fillGridUser = function () {
    AjaxCall('/api/admin/Users/Get_Userss/',

        JSON.stringify(
            {
                'PageIndex': 1,
                'PageSize': 100
            })
        , 'POST').done(function (response) {
            if (response.isSuccess === true) {
                /*var obj = JSON.parse(response.data);*/
                var tempData = [];

                response.data.forEach(function (item, i) {//"roles": item.roles,
                    tempData.push({ "id": item.userID, "username": item.user.userName, "email": item.user.email, "mobile": item.user.mobile, "status": item.user.status === true ? "فعال" : "غیر فعال", "roles": item.roles, });
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
                    headerName: 'نام کاربری',
                    field: 'username',
                    filter: true,
                    width: 200,
                    //cellRenderer: customAvatarHTML,
                },
                {
                    headerName: 'ایمیل',
                    field: 'email',
                    filter: true,
                    width: 200,
                },
                {
                    headerName: 'موبایل',
                    field: 'mobile',
                    filter: true,
                    width: 200,
                },
                {
                    headerName: 'وضعیت',
                    field: 'status',
                    filter: true,
                    width: 200,
                },
                {
                    headerName: 'عملیات',
                    field: 'setting_',
                    filter: true,
                    width: 200,
                    cellRenderer: function (params) {
                        
                        if (params.data.username === "svisor" || params.data.username === "admin") {
                            return '<div class="d-flex justify-content-center"  disabled="disabled"><a href="#" style="font-size: 26px;" class="nav-link" > <i class="ficon feather icon-check-square"></i></a> </div>';
                        } else
                            return '<div class="d-flex justify-content-center"><a href="#" style="font-size: 26px;" class="nav-link" data-id="' + params.data.id + '" data-roles="' + params.data.roles + '" onclick="onAccessLevels($(this))" data-toggle="modal" data-target="#exampleModalScrollable" > <i class="ficon feather icon-check-square"></i></a> </div>';
                    }
                }
                ];

                fillGrid(tempData, columnDefs, columnCountShow = 10, nameGrid = "myGrid");
            }
            else {
                Swal.fire({
                    title: "خطا!",
                    text: response.message,
                    type: "error",
                    confirmButtonClass: 'btn btn-primary',
                    buttonsStyling: false,
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
            }).then(function () {
                $('.logoff').click();
            });
        });
};