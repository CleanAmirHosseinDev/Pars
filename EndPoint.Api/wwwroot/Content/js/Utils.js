$(document).ready(function () {
    
    $('.logoff').on('click', function (e) {
        e.preventDefault();
        dellstor("token");
        dellstor("menu");
        dellstor("fullName");
        dellstor("userID");
        dellstor("customerID");
        window.location.href = "/Home/Login";
        e.stopPropagation();
    });

});

fillMenu = function () {
    try {

        $('.user-name').html(getlstor("fullName"));
        var menu = JSON.parse(getlstor("menu"));
        var tempMenu = [];
        menu.forEach(function (item, i) {
            tempMenu.push({ "group": item.group, "labelGroup": item.labelGroup });
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

        var srt = '';
        $('#main-menu-navigation').html(srt);

        srt += '<li class=" navigation-header"><span>' + 'منوی مدیریت سامانه' + '</span>';
        menuNotDuplicate.forEach(function (item, i) {
            srt += '<li class=" nav-item"><a href="#"><i class="feather icon-user"></i><span class="menu-title" data-i18n="User">' + item.labelGroup + '</span></a>';
            srt += '<ul class="menu-content">';
            for (var i = 0; i < menu.length; i++)
                if (item.group === menu[i].group)
                    srt += '<li><a href="' + menu[i].link+'"><i class="feather icon-circle"></i><span id="' + menu[i].value + '" class="menu-item" data-i18n="List">' + menu[i].text + '</span></a></li>';

            srt += '</ul>';
            srt += '</li>';
        });
        srt += '</li>';
        $('#main-menu-navigation').html(srt);

    } catch (e) {
        dellstor("token");
        dellstor("menu");
        dellstor("token");
        dellstor("menu");
        dellstor("fullName");
        dellstor("userID");
        dellstor("customerID");
        window.location.href = "/";
    }
};
deb = function () {
    try {
        debugger;
    } catch (e) {

    }
};
AjaxCall = function (url, data, type, key = "", val = "") {
    return $.ajax({
        processData: false,
        url: url,
        type: type ? type : 'GET',
        data: data,
        cache: false,
        contentType: false,
        processData: false,
        traditional: true,
        contentType: 'application/json',
        beforeSend: function (xhr) {
            if (!GetNullEmpetyUndefined(key) && !GetNullEmpetyUndefined(val)) {
                xhr.setRequestHeader(key, val);
            }


            $("#divProcessing").show();
        },
        complete: function () {
            $("#divProcessing").hide();
        }
    });
};
GetNullEmpetyUndefined = function (e) {
    var result = false;
    if (e === undefined || e === null || e === "")
        result = true;

    return result;
};
setlstor = function (k, v) {
    var key = encrypt(k.toString(), keyMaker());
    var val = encrypt(v.toString(), keyMaker());
    localStorage.setItem(key, val);
};
keyMaker = function () {
    return ((new Date()).getTimezoneOffset() / 60) +
        window.screen.width +
        navigator.product +
        window.screen.height +
        navigator.language +
        window.screen.colorDepth +
        navigator.platform +
        window.screen.pixelDepth +
        navigator.userAgent;
};
getlstor = function (k) {

    var t = encrypt(k.toString(), keyMaker());
    var dd = localStorage.getItem(t);
    var tt = decrypt(dd, keyMaker());

    return tt;
};
dellstor = function (k) {
    var t = encrypt(k.toString(), keyMaker());

    localStorage.removeItem(t);
};
encrypt = function (text, key, revert = false) {

    if (GetNullEmpetyUndefined(text))
        return '';
    var newText = '';
    for (var i = 0; i < text.length; i++)
        newText += String.fromCharCode(text.charCodeAt(i) + (revert ? key.charCodeAt(Math.abs(key.length - i) % key.length) : key.charCodeAt(i % key.length)));

    return newText;
};
decrypt = function (text, key, revert = false) {

    if (GetNullEmpetyUndefined(text))
        return '';
    var newText = '';
    for (var i = 0; i < text.length; i++)
        newText += String.fromCharCode(text.charCodeAt(i) - (revert ? key.charCodeAt(Math.abs(key.length - i) % key.length) : key.charCodeAt(i % key.length)));

    return newText;
};