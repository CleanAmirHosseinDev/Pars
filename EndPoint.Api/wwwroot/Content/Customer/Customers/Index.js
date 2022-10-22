$(document).ready(function () {

    fillData();

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

        AjaxCall('/api/Customer/Customers/Save_BasicInformationCustomers/',

            JSON.stringify(
                {
                    'CompanyName': $('#CompanyName').val(),
                    'Email': $('#Email').val(),
                    'NamesAuthorizedSignatories': $('#NamesAuthorizedSignatories').val(),
                    'KindOfCompanyID': $('#KindOfCompanyID').val(),
                    'TypeServiceRequestedID': $('#TypeServiceRequestedID').val(),
                    'HowGetKnowCompanyId': $('#HowGetKnowCompanyId').val(),
                    'NationalCode': $('#NationalCode').val(),
                    'PostalCode': $('#PostalCode').val(),
                    'EconomicCode': $('#EconomicCode').val(),
                    'Tel': $('#Tel').val(),
                    'AgentName': $('#AgentName').val(),
                    'AgentMobile': $('#AgentMobile').val(),
                    'CeoName': $('#CeoName').val(),
                    'CeoMobile': $('#CeoMobile').val(),
                    'CountOfPersonal': $('#CountOfPersonal').val(),
                    'AmountOsLastSaels': $('#AmountOsLastSaels').val(),
                    'AddressCompany': $('#AddressCompany').val(),

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
        e.stopPropagation();
    });

});

var customHTML = function (params) {

    return '<div data-id="' + params.data.id + '" data-name="' + params.data.name + '" data-labeCode="' + params.data.labeCode + '" data-isActive="' + params.data.isActive + '" onclick="onSetting($(this))" data-toggle="modal" data-target="#exampleModalScrollable"  style="width: 100% !important;height: inherit !important;min-width: 50px !important;" >' + params.value + '</div>';

};

fillData = function () {

    AjaxCall('/api/Customer/SystemSeting/Get_SystemSetings/', JSON.stringify(
        {
            'LabeCodeArr': "1005,1015,1010",
        }), 'POST').done(function (response) {
            deb();
            if (response.isSuccess === true) {
          
         
                for (var i = 0; i < response.data.length; i++) {
                    if (response.data[i].labeCode === 1005) {      //نوع شرکت
                        setSelect('#KindOfCompanyID', response.data[i].systemSetingId, response.data[i].value);
                    } else if (response.data[i].labeCode === 1015) {  //نوع خدمت
                        setSelect('#TypeServiceRequestedID', response.data[i].systemSetingId, response.data[i].value);
                    } else if (response.data[i].labeCode === 1010) {//چگونه شناسه شرکت را بشناسیم
                        setSelect('#HowGetKnowCompanyId', response.data[i].systemSetingId, response.data[i].value);
                    }
                }


                //دریافت تمام فیلد ها
                AjaxCall('/api/Customer/Customers/Get_Customers/', null, 'GET').done(function (response) {
                    deb();
                    if (!GetNullEmpetyUndefined(response)) {
                        var temp = [];

                        $('#CompanyName').val(response.companyName);
                        $('#Email').val(response.email);
                        $('#KindOfCompanyID').val(response.kindOfCompanyId);
                        $('#NamesAuthorizedSignatories').val(response.namesAuthorizedSignatories);
                        $('#TypeServiceRequestedID').val(response.typeServiceRequestedId);
                        $('#HowGetKnowCompanyId').val(response.howGetKnowCompanyId);
                        $('#NationalCode').val(response.nationalCode);
                        $('#PostalCode').val(response.postalCode);
                        $('#EconomicCode').val(response.economicCode);
                        $('#EconomicCode').val(response.economicCode);
                        $('#Tel').val(response.tel);
                        $('#AgentName').val(response.agentName);
                        $('#AgentMobile').val(response.agentMobile);
                        $('#CeoName').val(response.ceoName);
                        $('#CeoMobile').val(response.ceoMobile);
                        $('#CountOfPersonal').val(response.countOfPersonal);
                        $('#AmountOsLastSaels').val(response.amountOsLastSaels);
                        $('#AddressCompany').val(response.addressCompany);
                        flag = false;
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
                            flag = false;
                            window.location.href = "/Customer/Home/index";
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
                        window.location.href = "/Customer/Home/index";
                    });
                });
            } else {
                Swal.fire({
                    title: "خطا!",
                    text: response.message,
                    type: "error",
                    confirmButtonClass: 'btn btn-primary',
                    buttonsStyling: false,
                    confirmButtonText: 'متوجه شدم!',
                }).then(function () {
                    window.location.href = "/Customer/Home/index";
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
                window.location.href = "/Customer/Home/index";
            });
        });
};