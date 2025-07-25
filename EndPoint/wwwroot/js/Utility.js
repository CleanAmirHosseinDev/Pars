﻿

function pageingGrid(idElement, url, data) {

    try {

        var resQ = "<div class='text-center' id=''><ul class='pagination pagination-small' style='margin-bottom: auto;'><li style='cursor:pointer;' id='id_Left'><a onclick='pageingGridCall(\" " + idElement + " \", \"" + url + "\", " + data + ",false);'>صفحه قبل</a></li><li style='cursor:pointer;' id='li_Number' class='active'><a title='صفحه جاری' id='a_Id_Number'>1</a></li><li style='cursor:pointer;' id='li_Right'><a onclick='pageingGridCall(\" " + idElement + " \", \"" + url + "\", " + data + ",true);'>صفحه بعد</a></li></ul></div>";

        $("#" + idElement).html(resQ);
        debugger;
        pageingGridCall(idElement, url, data);


    } catch (e) {

    }

}

function clickSortingGridWithConfig(e, className) {

    $("." + className).find("i[class]").hide();

    $("." + className).removeAttr("data-Selected");

    if ($(e).find("i[class]").attr("class") == "fa fa-arrow-up") {

        $(e).attr("data-Selected", $(e).attr("data-D"));
        $(e).find("i[class]").attr("class", "fa fa-arrow-down");
    }
    else {

        $(e).attr("data-Selected", $(e).attr("data-A"));
        $(e).find("i[class]").attr("class", "fa fa-arrow-up");
    }

    $(e).find("i[class]").show();

}

function AjaxCallActionWithReturnHtml(type, url, data, async, successCallBack, isWait = true) {

    removeCookieInMemoryFree();

    $.ajax({
        type: type,
        url: url,
        data: data,
        dataType: 'html',
        async: async,
        beforeSend: function () {
            //if (isWait)
            showWait();
        },
        success: function (res) {

            try {

                if (res.statusCode == Web.Resources.Code301) {
                    CloseModal();
                    alertB("عدم دسترسی", !isEmpty(res.Message) ? res.Message : Web.Resources.MessageAccessDenied, 'error');
                    return;
                }
                if (res.statusCode == Web.Resources.Code401) {
                    goToUrl(res.redirectResult);
                    return;
                }
                if (res.statusCode == "404") {

                    if (!isEmpty(res.message)) alertB("", res.message, "error");
                    else goToUrl("/Error/Code404");

                    return;
                }
                successCallBack(res);
            } catch (e) {
                alert(e);
            }

        },
        complete: function () {
            //if (isWait)
            hideWait();
        },
        error: function (error) {

            if (error.status == 401) {

                if (url.indexOf("admin") == -1) goToUrl("/Account/Login");
                else goToUrl("/Account/LoginA");


            }

        }
    });

}

function pageingGridCall(tid, url, data, isNext = null) {

    try {
        debuggerWeb();

        var qEval = ("window." + tid + "_pageG").toString().replace(" ", "").replace(" ", "");

        if (isNext != null && !isNext && eval(qEval) === 1) return;

        if (isNext != null && isNext) eval(qEval + "+=" + "1");
        else if (isNext != null && !isNext) eval(qEval + "-=" + "1");

        data = (typeof data === 'string' || data instanceof String) ? JSON.parse(data) : data;
        data.PageIndex = eval(qEval);
        window.AjaxCallAction("POST", url, JSON.stringify(data), true, function (result) {
            debuggerWeb();

            if (result.data.length > 0) {

                if (isNext != null) {


                    $(("#" + tid).toString().replace(" ", "").replace(" ", "")).find("#a_Id_Number").html(eval(qEval));


                }

            }
            else {

                if (isNext != null) {

                    if (isNext) eval(qEval + "-=" + "1");
                    else eval(qEval + "+=" + "1");

                }

            }

            eval(("successCallBack_" + tid).toString().replace(" ", ""))(result);

        }, true);

    } catch (e) {

    }

}

function GetFullFilePath(path) {

    try {

        switch (GetExtension(path).toLowerCase()) {
            case 'pdf':

                return "<a href='" + path + "' target='_blank'><img src='/FileUpload/pdf.jfif' class='img-circle' width='100' height='100' /></a>";

                break;
            case 'xlsx':
            case 'xls':

                return "<a href='" + path + "' target='_blank'><img src='/FileUpload/xls.jfif' class='img-circle' width='100' height='100' /></a>";

                break;
            default:


                return "<a href='" + path + "' target='_blank'><img src='" + path.substring(path.indexOf("/FileUpload"), path.lenth) + "' class='img-circle' width='100' height='100' /></a>";

                break;
        }

    } catch (e) {

    }

}

setlstor = function (k, v) {
    var key = encrypt(k.toString(), keyMaker());
    var val = encrypt(v.toString(), keyMaker());
    localStorage.setItem(key, val);
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
encrypt = function (text, key, revert = false) {

    if (isEmpty(text))
        return '';

    if (typeof text == "boolean") {
        text = text.toString();
    }

    var newText = '';
    for (var i = 0; i < text.length; i++)
        newText += String.fromCharCode(text.charCodeAt(i) + (revert ? key.charCodeAt(Math.abs(key.length - i) % key.length) : key.charCodeAt(i % key.length)));

    return newText;
};
decrypt = function (text, key, revert = false) {

    if (isEmpty(text))
        return '';
    var newText = '';
    for (var i = 0; i < text.length; i++)
        newText += String.fromCharCode(text.charCodeAt(i) - (revert ? key.charCodeAt(Math.abs(key.length - i) % key.length) : key.charCodeAt(i % key.length)));

    return newText;
};

var lastValue;
function FillSelectByAjaxWithSearchInDatabase(selectId, value, e, obj, hasChoosen = true, showLable = true, data = { text: value }, url = "", text = '') {

    var newArray = new Array();
    $("#select2-" + selectId + "-results li").each(function (index, value) {
        if (value.id !== '') newArray.push(value);
    });
    if (newArray.length != 0) return;

    lastValue = e.which === 8 && isEmpty(e.target.value) ? "" : lastValue;

    lastValue = e.target.value != "" ? e.target.value : lastValue;

    if (e.which != 40 && e.which != 38 /*&& (Number.isInteger(Number(lastValue)) ? true : lastValue.length >= 3)*/) {
        EmptySelect(selectId, text);
        $(".select2-selection.select2-selection--single[aria-labelledby='select2-" + selectId + "-container']").closest(".select2-container").siblings('select:enabled').select2('close');
        if (document.querySelectorAll("select[name=" + selectId + "]").length > 0)
            EmptySelect(selectId, text);

        if (isEmpty(url)) return;

        AjaxCallAction("GET", url, data, true, function (state) {

            if (showLable) {
                var optLable = document.createElement("option");
                document.getElementById(selectId).options.add(optLable);
                optLable.text = ("جستجو کنید");
                optLable.value = "";
            }
            for (var i = 0; i < state.length; i++) {
                var opt = document.createElement("option");
                document.getElementById(selectId).options.add(opt);
                opt.text = (state[i].Text);
                opt.value = (state[i].Value);
                opt.selected = state[i].Selected;
            }
            if (hasChoosen) {
                RemoveDisableAttrTag(selectId);
                $('#' + selectId).trigger("chosen:updated");
            }

            $(".select2-selection.select2-selection--single[aria-labelledby='select2-" + selectId + "-container']").closest(".select2-container").siblings('select:enabled').select2('open');

            //document.querySelector('.select2-search__field[aria-controls="select2-' + selectId + '-results"]').focus();

            $(obj).val(lastValue);

        }, true);
    }
}

function strLink(e, item) {

    try {

        var lnk;
        if (!isEmpty(item.Link)) lnk = item.Link;
        else lnk = "/Post_Main/PostDetail?id=" + item.ID + "&title=" + item.Title + "";

        if (item.IsOpenNewTab) window.open(lnk, "_blank");
        else goToUrl(lnk);

    } catch (e) {

    }

}

function AlertDialog(text, type = "success", layout = "bottomLeft", dismissQueue = false, modal = false, timeout = 4000) {

    try {
        debuggerWeb();
        if (!isEmpty(text))
            eval("$.noty.closeAll(); noty({ text: '" + text + "', type: '" + type + "', layout: '" + layout + "', dismissQueue: " + dismissQueue + ", modal: " + modal + ", timeout: " + timeout + " });");

    } catch (e) {

    }

}

function GuidEmpty() {

    try {

        return "00000000-0000-0000-0000-000000000000";

    } catch (e) {

    }

}

function isEmpty(str) {

    try {

        return str === "" || str === null || str === undefined || str === "null" || str === "undefined" ? true : false;

    } catch (e) {

    }

}

function ClearAllMultiMediaWithForm(idForm) {

    try {

        debuggerWeb();

        $("#" + idForm + " img").attr("src", "");
        $("#" + idForm + " img").css("display", "none");

        $("#" + idForm + " video").css("display", "none");
        $("#" + idForm + " video").children().attr("src", "");
        $("#" + idForm + " video").trigger('pause');

        $("#" + idForm + " audio").css("display", "none");
        $("#" + idForm + " audio").children().attr("src", "");
        $("#" + idForm + " audio").trigger('pause');

    } catch (e) {

    }

}

var groupBy = function (xs, key) {
    var res = xs.reduce(function (rv, x) {
        (rv[x[key]] = rv[x[key]] || []).push(x);
        return rv;
    }, {});
    return Object.keys(res).map((key) => [key, res[key]]);
};
function CheckTel(value) {

    return value.indexOf("09") == 0 ? false : true;

}

function CheckMobile(value) {

    return value.indexOf("09") != 0 ? false : true;

}

function onChangeSelectValidation(e) {

    $(e).valid();

}

function removeCookieInMemoryFree() {

    delete_cookie("ai_session");
    delete_cookie("ai_user");

}

function AjaxCallAction(type, url, data, async, successCallBack, isWait = true) {

    try {

        removeCookieInMemoryFree();

        $.ajax({
            cache: false,
            processData: false,
            traditional: true,
            contentType: 'application/json',
            type: type,
            url: url,
            data: data,
            async: async,
            dataType: "json",
            headers: { "Authorization": 'Bearer ' + localStorage.getItem("token") },
            beforeSend: function () {
                if (isWait)
                    showWait();
            },
            success: function (res) {
                try {

                    if (res.statusCode === Web.Resources.Code301) {
                        alertB("عدم دسترسی", !isEmpty(res.Message) ? res.Message : Web.Resources.MessageAccessDenied, 'error');
                        return;
                    }
                    if (res.statusCode === Web.Resources.Code401) {
                        goToUrl(res.redirectResult);
                        return;
                    }
                    if (res.statusCode == "404") {

                        if (!isEmpty(res.message)) alertB("", res.message, "error");
                        else goToUrl("/Error/Code404");

                        return;
                    }
                    successCallBack(res);
                } catch (e) {
                    alert(e);
                }
            },
            complete: function () {
                if (isWait)
                    hideWait();

            },
            error: function (error) {

                if (error.status == 401) {

                    if (url.indexOf("admin") == -1 && url.indexOf("superVisor") == -1) goToUrl("/Account/Login");
                    else goToUrl("/Account/LoginUser");


                }

            }
        });

    } catch (e) {

    }

}
function AjaxCallActionWithotHeading(type, url, data, async, successCallBack, isWait = true) {

    try {

        removeCookieInMemoryFree();

        $.ajax({
            cache: false,
            processData: false,
            traditional: true,
            contentType: 'application/json',
            type: type,
            url: url,
            data: data,
            async: async,
            dataType: "json",
            beforeSend: function () {
                if (isWait)
                    showWait();
            },
            success: function (res) {
                try {
                    if (res.statusCode === Web.Resources.Code301) {
                        alertB("عدم دسترسی", !isEmpty(res.Message) ? res.Message : Web.Resources.MessageAccessDenied, 'error');
                        return;
                    }
                    if (res.statusCode === Web.Resources.Code401) {
                        goToUrl(res.redirectResult);
                        return;
                    }
                    if (res.statusCode == "404") {

                        if (!isEmpty(res.message)) alertB("", res.message, "error");
                        else goToUrl("/Error/Code404");

                        return;
                    }
                    successCallBack(res);
                } catch (e) {
                    alert(e);
                }
            },
            complete: function () {
                if (isWait)
                    hideWait();

            },
            error: function (error) {

                if (error.status == 401) {

                    if (url.indexOf("admin") == -1 && url.indexOf("superVisor") == -1) goToUrl("/Account/Login");
                    else goToUrl("/Account/LoginUser");


                }

            }
        });

    } catch (e) {

    }

}
function AjaxForm(idForm, type, url, data, async, successCallBack, isWait = true) {

    try {

        removeCookieInMemoryFree();

        $("#" + idForm).ajaxForm({
            type: type,
            url: url,
            data: data,
            dataType: "json",
            async: async,
            beforeSend: function () {
                if (isWait)
                    showWait();
            },
            success: function (res) {

                debuggerWeb();

                try {
                    if (res.statusCode === Web.Resources.Code301) {
                        AlertDialog(Web.Resources.MessageAccessDenied, 'error');
                        return;
                    }
                    if (res.statusCode === Web.Resources.Code401) {
                        goToUrl(res.redirectResult);
                        return;
                    }
                    successCallBack(res);
                } catch (e) {
                    alert(e);
                }

            },
            complete: function () {
                if (isWait)
                    hideWait();
            },
            error: function (error) {
                alert(error);
            }
        });

    } catch (e) {

    }

}

function fill_AjaxCallActionPostSaveFormWithUploadFile(idForm) {

    try {

        var formData = new FormData($('#' + idForm)[0])

        $.each($("#" + idForm).serialize().split('&'), function (index, elem) {
            var vals = elem.split('=');
            formData.append(vals[0], vals[1]);
        });

        return formData;

    } catch (e) {

    }

}

function AjaxCallActionPostSaveFormWithUploadFile(url, data, async, successCallBack, isWait = true) {

    try {

        removeCookieInMemoryFree();

        $.ajax({
            type: "POST",
            url: url,
            data: data,
            async: async,
            dataType: "json",
            headers: { "Authorization": 'Bearer ' + localStorage.getItem("token") },
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function () {
                if (isWait)
                    showWait();
            },
            success: function (res) {
                try {
                    successCallBack(res);
                } catch (e) {
                    alert(e);
                }
            },
            complete: function (res) {
                if (isWait)
                    hideWait();

            },
            error: function (error) {

                if (error.status == 401) {

                    if (url.indexOf("admin") == -1 && url.indexOf("superVisor") == -1) goToUrl("/Account/Login");
                    else goToUrl("/Account/LoginUser");


                }

            }
        });

    } catch (e) {

    }

}
function saveForm(idForm, url, type = "POST", data = null, successCallBack = null, isWait = true) {

    try {
        debuggerWeb();
        AjaxForm(idForm, type, url, (data == null ? $("#" + idForm).serialize() : data), true, function (result) {

            debuggerWeb();

            if (result.statusCode === Web.Resources.Code401) {
                goToUrl(result.redirectResult);
                return;
            }

            if (result.Success) {
                resetForm(idForm);

                ClearAllMultiMediaWithForm(idForm);

                if (successCallBack != null) successCallBack(result);
            }
            eval(result.Script);

        }, isWait);

    } catch (e) {

    }

}

function showWait() {

    try {

        document.getElementById("loader").style.display = "block";

    } catch (e) {

    }

}
function hideWait() {

    try {

        document.getElementById("loader").style.display = "none";

    } catch (e) {

    }

}

function resetForm(idForm) {

    try {

        $("#" + idForm + "")[0].reset();

    } catch (e) {

    }

}
function submitForm(idForm) {

    try {

        $("#" + idForm + "").submit();

    } catch (e) {

    }

}
function getFormId(e) {

    try {
        debuggerWeb();
        return $(e).closest("form").attr('id');

    } catch (e) {

    }

}

function changeValueAndCheckedOrUnCheckedInCheckBox(e, idHidElement = null) {

    try {

        debuggerWeb();

        if (!isEmpty(idHidElement)) {
            $("#" + idHidElement).val(e.checked ? "1" : "0")
        }
        else
            return $(e).attr('value', e.checked ? 1 : 0);

    } catch (e) {

    }

}
function unCheckedAndunCheckedCheckBox(selectors, isChecked = false) {

    try {

        $(selectors).prop('checked', isChecked);

    } catch (e) {

    }

}
function checkAllInInput(e, selectors) {

    try {

        debuggerWeb();
        unCheckedAndunCheckedCheckBox(selectors, $(e).prop('checked'));

    } catch (e) {

    }

}
function setValueInListWithSplitChecked(e, idHidElement, isRadioButton = false, splitChar = ',') {

    try {

        debuggerWeb();

        if (!isRadioButton) {

            var q = [];
            var qItemsCurrent = !isEmpty($("#" + idHidElement).val()) ? $("#" + idHidElement).val().split(splitChar) : q;

            for (var i = 0; i < qItemsCurrent.length; i++) {

                if (qItemsCurrent[i] === $(e).val()) continue;
                else q.push(qItemsCurrent[i]);
            }

            if (e.checked) q.push($(e).val());

            $("#" + idHidElement).val(q.join(splitChar));
        }
        else $("#" + idHidElement).val($(e).val());

    } catch (e) {

    }

}

function goToUrl(url) {

    try {

        document.location.href = url;

    } catch (e) {

    }

}

function printDirect(url) {

    try {

        debuggerWeb();

        var newWindow = window.open(url, "_blank");
        newWindow.focus();
        newWindow.print();

    } catch (e) {

    }

}

function downloadFile(filename) {

    try {

        debuggerWeb();
        var element = document.createElement('a');
        element.setAttribute('href', filename);
        element.setAttribute('download', filename);

        element.style.display = 'none';
        document.body.appendChild(element);

        element.click();

        document.body.removeChild(element);

    } catch (e) {

    }

}

function successFillSelectByAjax(selectId, state, hasChoosen, showLable) {

    try {

        if (showLable && $("#" + selectId + " option[value='']").length === 0) createOptionForComboBox(selectId, Web.Resources.Select, "");
        for (var i = 0; i < state.length; i++) {
            var opt = document.createElement("option");
            document.getElementById(selectId).options.add(opt);
            opt.text = (state[i].Text);
            opt.value = (state[i].Value);
            opt.selected = state[i].Selected;
        }
        if (hasChoosen) UpdateChoosenSelect(selectId);

    } catch (e) {

    }

}
function FillSelectByAjax(url, selectId, data, hasChoosen, showLable) {

    try {

        debuggerWeb();
        if (document.querySelectorAll("select[name=" + selectId + "]").length > 0)
            EmptySelect(selectId);

        if (isEmpty(url)) return;
        AjaxCallAction("GET", url, data, true, function (state) {
            debuggerWeb();
            successFillSelectByAjax(selectId, state, hasChoosen, showLable);

        });

    } catch (e) {

    }

}
function createOptionForComboBox(selectId, text, value) {

    try {

        var optLable = document.createElement("option");
        document.getElementById(selectId).options.add(optLable);
        optLable.text = text;
        optLable.value = value;

    } catch (e) {

    }

}
function EmptySelect(id, text = '') {
    try {

        text = isEmpty(text) ? Web.Resources.Select : text;

        $("#" + id).empty();
        createOptionForComboBox(id, text, "");

    } catch (e) {

    }
}
function EmptySelect_Search(id) {

    try {

        var elSel = document.getElementById(id);
        var lengh = elSel.length;
        if (elSel.length > 0) {
            for (var i = 0; i < lengh; i++)
                elSel.remove(elSel[i]);
        }
        var ids = "#" + id;
        $(ids).value = "";
        $(ids).val("");

    } catch (e) {

    }

}
function EmptyChoosenSelect(id) {

    try {

        EmptySelect(id);
        UpdateChoosenSelect(id);

    } catch (e) {

    }

}
function UpdateChoosenSelect(selectId) {

    try {

        $('#' + selectId).trigger("chosen:updated");

    } catch (e) {

    }

}
function DisableChoosenSelect(selectId) {

    try {

        $('#' + selectId).val("");
        $('#' + selectId).attr('disabled', true).trigger("chosen:updated");

    } catch (e) {

    }

}
function RemoveDisableAttrTag(tagId) {

    try {

        $("#" + tagId).removeAttr("disabled");

    } catch (e) {

    }

}
function comboBoxWithSearchUpdateText(selector, text) {

    try {

        $("#select2-" + selector + "-container").text(text);

        $("#select2-" + selector + "-container").attr("title", text);


    } catch (e) {

    }

}

function GetExtension(filename) {

    try {

        if (isEmpty(filename)) return '';
        return filename.split('.').pop();

    } catch (e) {

    }

}

function isImage(filename) {

    try {

        return ["jpg", "jpeg", "bmp", "png", "gif"].includes(filename);

    } catch (e) {

    }
}

function ConfigSection($scope, $http, positionPost, page = 1, sizeListsGlobal = 3, callBack = null, url = '/Post_Main/P_Posts', method = 'GET', sortFieldName = 'Sort', sortOrder = 'ASC', isInitVaribles = true, categoryPostID = 0, userID = null, search = null) {

    try {


        if (isInitVaribles === true) {
            eval("$scope." + positionPost + "= []");
            eval("$scope.temp" + positionPost + "= []");
            eval("$scope.TotalNumbers" + positionPost);
            eval("$scope.size" + positionPost + "=" + sizeListsGlobal);
        }

        $http({ url: url, method: method, params: { PageNum: page, PageSize: sizeListsGlobal, CategoryPostID: categoryPostID, UserID: userID, ManagementPostID: positionPost.split('_').pop(), Search: search, SortFieldName: sortFieldName, SortOrder: sortOrder } }).then(function (res) {

            eval("$scope.temp" + positionPost + "=" + "$scope.temp" + positionPost + ".concat(res.data.Lists)");
            eval("$scope." + positionPost + "=" + "res.data.Lists");


            eval("$scope.TotalNumbers" + positionPost + "=" + res.data.Model_Pageing.TotalNumbers);
            eval("$scope.size" + positionPost + "=" + sizeListsGlobal);
            eval("$scope.size" + positionPost + "=" + "$scope.TotalNumbers" + positionPost + " < $scope.size" + positionPost + " ? $scope.TotalNumbers" + positionPost + " : $scope.size" + positionPost);

            if (callBack !== null) callBack();


        }).catch(function (err) {

        });

    } catch (e) {

    }


}

function Config_Resource(lists, langugeID, IsDefaultLanguageRight, IsOtherLanguageLeft) {

    try {

        if (lists.length > 0) {

            for (var j = 0; j < lists.length; j++) {

                var text = direction(langugeID, IsDefaultLanguageRight, IsOtherLanguageLeft) == 'r' ? lists[j].Value : lists[j].ValueEn;
                var textResult = lists[j].Key.replace("***09388182352***", !isEmpty(text) ? text : "");
                eval(textResult);

            }

        }

    } catch (e) {

    }

}
function Full_Resource() {

    try {

        AjaxCallAction("POST", "/Home/GetResources", {}, true, function (res) {
            Config_Resource(res.Lists, res.langugeID, res.objSingleSetting.IsDefaultLanguageRight, res.objSingleSetting.IsOtherLanguageLeft);

        }, false);

    } catch (e) {

    }

}

function Config_Gallery_For_Video_And_Music(lists, listVideos, listMusics, listImages) {

    try {

        if (lists.length > 0) {

            for (var i = 0; i < lists.length; i++) {
                switch (GetExtension(lists[i].Image).toLowerCase()) {

                    case 'mp4':
                        listVideos.push(lists[i]);
                        break;

                    case 'mp3':
                        listMusics.push(lists[i]);
                        break;
                    default:

                        if (isImage(GetExtension(lists[i].Image).toLowerCase())) listImages.push(lists[i]);

                        break;
                }
            }

        }

    } catch (e) {

    }

}

function createCookie(name, value, days) {
    try {

        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();
        }
        else var expires = "";
        document.cookie = name + "=" + value + expires + "; Secure; path=/";

    } catch (e) {

    }
}
function readCookie(name) {

    try {

        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;

    } catch (e) {

    }

}

function direction(langugeID, IsDefaultLanguageRight, IsOtherLanguageLeft) {

    try {

        if (isEmpty(langugeID) && isEmpty(IsDefaultLanguageRight) && isEmpty(IsOtherLanguageLeft)) return '';

        if (langugeID === 1) {

            if (IsDefaultLanguageRight === true)
                return "r";
            return "l";

        }
        else {

            if (IsOtherLanguageLeft === true)
                return "l";
            return "r";

        }

    } catch (e) {

    }

}

function copyTextInClipboard(text) {

    try {

        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(text).select();
        document.execCommand("copy");
        $temp.remove();

    } catch (e) {

    }

}
function copyInResourceInMultiLanguageForItem(idItem) {

    try {

        debuggerWeb();

        if (window.userIsAuthenticated) {
            if (confirm("Copy Selector Source To Clipboard ?")) {

                copyTextInClipboard($("#" + idItem).attr('data-ResourceKeySelectorForDynamicObjectMultiLanguage'));

                alertB('Success', 'Copy Completed Successfully', 'success'); return false;
            }
        }

    } catch (e) {

    }

}
function initCtxMenu(id, lstItems = []) {

    try {

        var contextMenuTwo = window.CtxMenu("#" + id);

        if (isEmpty(contextMenuTwo._elementClicked)) {

            for (var i = 0; i < lstItems.length; i++) {

                contextMenuTwo.addItem(lstItems[i].text, lstItems[i].callback);

                if (lstItems.length - 1 == i) break;
                contextMenuTwo.addSeparator();

            }

        }

    } catch (e) {

    }

}
function copyInResourceInMultiLanguageForItems(e, lstItems = []) {

    try {

        debuggerWeb();

        if (window.userIsAuthenticated) initCtxMenu($(e).attr("id"), lstItems);

    } catch (e) {

    }

}

function alertB(title, text, icon, buttonOk = "بله متوجه شدم", successCallBack = null) {

    try {

        swal({
            title: title,
            text: text,
            icon: icon,
            button: buttonOk
        }).then(function (isConfirm) {
            if (isConfirm) {

                if (successCallBack != null) successCallBack();

            } else {

            }
        });

    } catch (e) {

    }

}

function confirmB(title, text, icon, successCallBack, CancelCallBack, buttons = ["Cancel", "Ok"], dangerMode = true) {

    try {

        swal({
            title: title,
            text: text,
            icon: icon,
            buttons: buttons,
            dangerMode: dangerMode,
        })
            .then((willDelete) => {
                if (willDelete) {
                    successCallBack();
                } else {
                    CancelCallBack();
                }
            });

    } catch (e) {

    }

}
function alertB_And_confirmB_Close() {

    try {

        swal.close();

    } catch (e) {

    }

}

function Tree(id, url, isCreateItemMenu = false, isViewItemMenu = false, isDeleteItemMenu = false, changedCallBack, deleteCallBack, viewCallBack, readyCallBack) {

    try {

        $('#' + id).jstree({
            "plugins": ["themes", "contextmenu", "dnd", "types"],
            'core': {
                themes: { "stripes": true },
                check_callback: true,
                animation: 0,
                'data': {
                    'url': function (node) {
                        return node.id === '#' ?
                            url :
                            url + node.id;
                    },
                    'data': function (node) {
                        return { 'id': node.id };
                    }
                }
            },
            contextmenu: {
                "items": function (node) {
                    return {
                        "view": isViewItemMenu ? {
                            label: "انتخاب",
                            action: function (obj) {
                                debuggerWeb();
                                viewCallBack(node);
                            }
                        } : false,
                        "create": isCreateItemMenu ? {
                            label: "افزودن",
                            action: function (obj) {
                                debuggerWeb();
                                viewCallBack("");
                            }
                        } : false,
                        "delete": isDeleteItemMenu ? {
                            label: "حذف",
                            action: function () {
                                debuggerWeb();
                                if (confirm("آیا از حذف اطمینان خاطر دارید")) deleteCallBack(id, node);
                            },
                            separator_before: true
                        } : false
                    }
                }
            }
        }).on("changed.jstree", function (e, data) {
            if (data.selected.length) changedCallBack(e, data);
        }).on('ready.jstree', function (e, data) {
            debuggerWeb();
            readyCallBack(e, data);
        });


    } catch (e) {

    }
}
function Delete_Node_Tree(id, node) {

    try {

        $("#" + id).jstree(true).delete_node(node);

    } catch (e) {

    }

}
function Get_Selected_Node_Tree(id) {

    try {

        return $('#' + id).jstree().get_selected(true)[0];

    } catch (e) {

    }

}
function Create_Node_Tree(id, parent = '#', data = {}, insertPosition = 'last', successCallBack = null) {

    try {

        $("#" + id).jstree(true).create_node("#" + parent, data, insertPosition);

    } catch (e) {

    }

}
function Update_Node_Tree(id, node, text) {

    try {

        $("#" + id).jstree('rename_node', node, text);

    } catch (e) {

    }

}
function Select_Node_Tree(id, selectedValues) {

    try {

        $('#' + id).jstree('select_node', selectedValues);

    } catch (e) {

    }

}
function Deselect_Node_Tree(id, selectedValues) {

    try {

        $('#' + id).jstree('deselect_node', selectedValues);

    } catch (e) {

    }

}
function Open_Node_Tree(id, selectedValues) {

    try {

        $('#' + id).jstree('open_node', selectedValues);

    } catch (e) {

    }

}
function CloseAll_Node_Tree(id) {

    try {

        $("#" + id).jstree('close_all');

    } catch (e) {

    }

}
function OpenAll_Node_Tree(id) {

    try {

        $("#" + id).jstree('open_all');

    } catch (e) {

    }

}

function iCheck(checkboxClass, radioClass, selector = 'input', increaseArea = '20%') {

    try {

        $(selector).iCheck({
            checkboxClass: checkboxClass,
            radioClass: radioClass,
            increaseArea: increaseArea // optional
        });

    } catch (e) {

    }

}

function ComboBoxWithSearch(selector = '.select2', dir = 'rtl') {

    try {

        $(selector).select2({
            dir: dir,
            language: {
                noResults: function (term) {
                    return "نتیجه ای پیدا نشد";
                }
            }
        });

    } catch (e) {

    }

}
function ComboBoxWithSearchAndModal(selector = '.select2', idModal = '#modalMain') {

    $(selector).select2({
        dropdownParent: $(idModal),
        dir: 'rtl',
        language: {
            noResults: function (term) {
                return "نتیجه ای پیدا نشد";
            }
        }
    });

}
function ResetComboBoxWithSearch(id) {

    try {

        $("#" + id).val("");

        var strDefaultText = $("#" + id).find("option:first-child").text();
        $("#select2-" + id + "-container").text(strDefaultText);
        $("#select2-" + id + "-container").attr("title", strDefaultText);

    } catch (e) {

    }

}

function DatePicker(selector) {

    try {

        $(selector).datepicker({
            changeYear: true,
            changeMonth: true,
            dateFormat: 'yy/mm/dd'
        });

    } catch (e) {

    }

}

function PersianDatePicker(selector) {

    try {

        $(selector).persianDatepicker({
            showGregorianDate: !1,
            persianNumbers: !0,
            formatDate: "YYYY/0M/0D",
            prevArrow: '\u25c4',
            nextArrow: '\u25ba',
            theme: 'default',
            alwaysShow: !1,
            selectableYears: null,
            selectableMonths: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
            cellWidth: 30, // by px
            cellHeight: 30, // by px
            fontSize: 12, // by px
            isRTL: !1,
            onShow: function () { },
            onHide: function () { },
            onSelect: function () { },
            onRender: function () { }
        });

    } catch (e) {

    }

}

function ClearReadURL(input, defaultUrlName = "", imgShowImage = "imgUpload") {

    try {

        $("#" + $(input).attr("id")).val("");
        $("#" + imgShowImage).attr('src', defaultUrlName);

    } catch (e) {

    }

}
function readURL(input, imgShowImage = "imgUpload", successCallBack = null) {

    try {

        if (input.files && input.files[0]) {

            showWait();

            var reader = new FileReader();

            reader.onload = function (e) {
                $('#' + imgShowImage).attr('src', e.target.result);

                hideWait();

                if (successCallBack != null) successCallBack();

            }

            reader.readAsDataURL(input.files[0]);
        }

    } catch (e) {

    }

}
function UploadChange(e, defaultUrlName = "", imgShowImage = "imgUpload", successCallBack = null) {

    try {

        debuggerWeb();
        if (!isEmpty($(e).val())) {
            if ($(e)[0].files[0].type.indexOf("image") == -1) {
                ClearReadURL(e, defaultUrlName, imgShowImage);
                return false;
            }
            readURL(e, imgShowImage, successCallBack);
        }
        else ClearReadURL(e, defaultUrlName, imgShowImage);

    } catch (e) {

    }

}

var arrIncludePermisionUpload = ["image", "video", "audio"];
function AllClearReadURL(input, idID, idImage, defaultUrlName = "", imgShowImage = "imgUpload", idShowAudioChild = "audioChildUpload", idShowAudioParent = "audioParentUpload", idShowVideoChild = "videoChildUpload", idShowVideoParent = "videoParentUpload") {

    try {

        debuggerWeb();

        $("#" + $(input).attr("id")).val("");

        if ($("#" + idID).val() === GuidEmpty() || $("#" + idID).val() === "0") {

            $('#' + idShowAudioChild).parent()[0].pause();

            $('#' + idShowVideoChild).parent()[0].pause();

            $('#' + idShowAudioParent).attr('style', "display:none;");
            $('#' + imgShowImage).attr('style', "display:none;");
            $('#' + idShowVideoParent).attr('style', "display:none;");

        }
        else {

            switch (GetExtension($("#" + idImage).val())) {

                case 'mp3':

                    $('#' + idShowAudioParent).attr('style', "display:block;");

                    $('#' + idShowAudioChild)[0].src = defaultUrlName;
                    $('#' + idShowAudioChild).parent()[0].load();


                    $('#' + imgShowImage).attr('style', "display:none;");
                    $('#' + idShowVideoParent).attr('style', "display:none;");

                    break;
                case 'mp4':

                    $('#' + idShowAudioParent).attr('style', "display:none;");
                    $('#' + imgShowImage).attr('style', "display:none;");
                    $('#' + idShowVideoParent).attr('style', "display:block;");

                    $('#' + idShowVideoChild)[0].src = defaultUrlName;
                    $('#' + idShowVideoChild).parent()[0].load();

                    break;

                default:

                    $('#' + idShowAudioParent).attr('style', "display:none;");
                    $('#' + imgShowImage).attr('style', "display:block;");
                    $('#' + idShowVideoParent).attr('style', "display:none;");

                    $('#' + imgShowImage).attr('src', defaultUrlName);

                    break;
            }

        }

        hideWait();

    } catch (e) {

    }

}
function AllreadURL(input, imgShowImage = "imgUpload", idShowAudioChild = "audioChildUpload", idShowAudioParent = "audioParentUpload", idShowVideoChild = "videoChildUpload", idShowVideoParent = "videoParentUpload") {

    try {

        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {

                debuggerWeb();

                switch (GetExtension(input.files[0].name).toLowerCase()) {

                    case 'mp3':

                        var $source = $('#' + idShowAudioChild);
                        $source[0].src = URL.createObjectURL(input.files[0]);
                        $source.parent()[0].load();
                        $('#' + idShowAudioParent).attr('style', "display:block;");
                        $('#' + imgShowImage).attr('style', "display:none;");
                        $('#' + idShowVideoParent).attr('style', "display:none;");

                        break;
                    case 'mp4':

                        var $source1 = $('#' + idShowVideoChild);
                        $source1[0].src = URL.createObjectURL(input.files[0]);
                        $source1.parent()[0].load();
                        $('#' + idShowVideoParent).attr('style', "display:block;");
                        $('#' + idShowAudioParent).attr('style', "display:none;");
                        $('#' + imgShowImage).attr('style', "display:none;");

                        break;
                    default:

                        $('#' + imgShowImage).attr('src', e.target.result);
                        $('#' + imgShowImage).attr('style', "display:block;");
                        $('#' + idShowAudioParent).attr('style', "display:none;");
                        $('#' + idShowVideoParent).attr('style', "display:none;");

                        break;
                }

            };

            reader.readAsDataURL(input.files[0]);
            hideWait();
        }

    } catch (e) {

    }

}
function AllUploadChange(e, idID, idImage, defaultUrlName = "", imgShowImage = "imgUpload", idShowAudioChild = "audioChildUpload", idShowAudioParent = "audioParentUpload", idShowVideoChild = "videoChildUpload", idShowVideoParent = "videoParentUpload") {

    try {

        debuggerWeb();

        if (!isEmpty($(e).val())) {

            if (arrIncludePermisionUpload.includes($(e)[0].files[0].type.split("/")[0])) {

                showWait();

                switch (GetExtension($(e)[0].files[0].name).toLowerCase()) {
                    case 'mp3':
                    case 'mp4':
                    case 'jpg':
                    case 'jpeg':
                    case 'bmp':
                    case 'png':
                    case 'gif':
                        AllreadURL(e, imgShowImage, idShowAudioChild, idShowAudioParent, idShowVideoChild, idShowVideoParent);
                        break;
                    default:

                        AllClearReadURL(e, idID, idImage, defaultUrlName, imgShowImage, idShowAudioChild, idShowAudioParent, idShowVideoChild, idShowVideoParent);
                        AlertDialog("فقط فرمت های mp3-mp4-jpg-jpeg-bmp-png-gif پشتیبانی میشود", "warning");

                        break;
                }

            }
            else {

                //AllClearReadURL(e, idID, idImage, defaultUrlName, imgShowImage, idShowAudioChild, idShowAudioParent, idShowVideoChild, idShowVideoParent);
                //AlertDialog("فقط بارگذاری تصویر و فیلم و موسیقی امکان پذیر می باشد", "warning");
                //return false;

            }
        }
        else AllClearReadURL(e, idID, idImage, defaultUrlName, imgShowImage, idShowAudioChild, idShowAudioParent, idShowVideoChild, idShowVideoParent);

    } catch (e) {

    }

}

function ClearUploadForGalleryChange(e, idDivHtml = "selectedFiles", idSpnError = "spnError") {

    try {

        $("#" + $(e).attr("id")).val("");
        $("#" + idDivHtml).html("");
        $("#" + idSpnError).text("");

    } catch (e) {

    }

}
function UploadForGalleryChange(e, idDivHtml = "selectedFiles", idSpnError = "spnError") {

    try {

        debuggerWeb();

        var selDiv = document.querySelector("#" + idDivHtml);
        if (!e.files || !window.FileReader) return;

        if (!isEmpty($(e).val())) showWait();

        $("#" + idSpnError).text("");
        selDiv.innerHTML = "";

        var files = e.files;
        var filesArr = Array.prototype.slice.call(files);
        var i = 0;
        var counter = 0;
        filesArr.forEach(function (f) {

            debuggerWeb();

            var f = files[i];
            //if (!f.type.match("image.*"))
            //    return;

            i += 1;

            if (arrIncludePermisionUpload.includes(f.type.split("/")[0])) {

                var urlName = GetExtension(f.name).toLowerCase();
                switch (urlName) {
                    case 'mp3':
                    case 'mp4':
                    case 'jpg':
                    case 'jpeg':
                    case 'bmp':
                    case 'png':
                    case 'gif':

                        var reader = new FileReader();

                        reader.onload = function (e) {

                            debuggerWeb();

                            counter += 1;

                            if (urlName === "mp3")
                                selDiv.innerHTML += "<audio width='300' controls height='300'><source src=\"" + e.target.result + "\" type='audio/mpeg'>Your browser does not support the audio element.</audio>";
                            else if (urlName === "mp4")
                                selDiv.innerHTML += "<video width='300' controls height='300'><source src=\"" + e.target.result + "\" type='video/mp4'>Your browser does not support the video tag.</video>";
                            else selDiv.innerHTML += "<img width='300' class='img-thumbnail' style='height:300px;' title=\"" + f.name + "\" src=\"" + e.target.result + "\">";

                            if (i === counter) {
                                hideWait();
                            }

                        };

                        reader.readAsDataURL(f);

                        break;
                    default:
                        counter += 1;
                        break;
                }

            }
            else counter += 1;
        });

    } catch (e) {

    }

}

function checkUpload(input, inputName) {

    if (input.files && input.files[0]) {

        var isOk = false;

        switch (GetExtension(input.files[0].name).toLowerCase()) {

            case 'pdf':
            case 'xlsx':
            case 'xls':

                isOk = true;

                break;
            default:

                if (isImage(GetExtension(input.files[0].name).toLowerCase()))
                    isOk = true;
                else isOk = false;

                break;
        }

        if (!isOk) {
            input.value = '';
            alertB('', inputName + " " + 'فرمت های مجاز برای Pdf,Excel,All Images', 'error');
            return;
        }

        if (input.files[0].size > 10000000) {
            input.value = '';
            alertB('', inputName + " " + " باید کمتر از 10 مگ باشد", 'error');
            return;
        }


    }

}

function checkUploadWithFileSiza(input, inputName, fileSiza) {

    if (input.files && input.files[0]) {

        var isOk = false;

        switch (GetExtension(input.files[0].name).toLowerCase()) {

            case 'pdf':
            case 'xlsx':
            case 'xls':

                isOk = true;

                break;
            default:

                if (isImage(GetExtension(input.files[0].name).toLowerCase()))
                    isOk = true;
                else isOk = false;

                break;
        }

        if (!isOk) {
            input.value = '';
            alertB('', inputName + " " + 'فرمت های مجاز برای Pdf,Excel,All Images', 'error');
            return;
        }

        if (input.files[0].size > (1000000 * fileSiza)) {
            input.value = '';
            alertB('', inputName + " " + " باید کمتر از " + fileSiza + " مگابایت  باشد", 'error');
            return;
        }


    }

}

function Ckeditor(id) {
    try {

        if ($("#" + id).length > 0) CKEDITOR.replace(id);

    } catch (e) {

    }
}
function EmptyCkeditor(id) {
    try {
        $("#" + id).val("");
    } catch (e) {

    }
}
function EmptyCkeditors(ids) {
    try {

        if (ids != null) {
            for (var i = 0; i < ids.length; i++) {
                EmptyCkeditor(ids[i]);
            }
        }

    } catch (e) {

    }
}
function getDataCkeditor(id) {

    return CKEDITOR.instances[id].getData();

}
function setDataCkeditor(id, html) {
    CKEDITOR.instances[id].setData(html);
}

function Tagsinput(id) {
    try {
        $("#" + id).tagsinput();
    } catch (e) {

    }
}
function RemoveAllTagsinput(id) {

    try {

        $('#' + id).tagsinput('removeAll');

    } catch (e) {

    }

}

function TextBoxOnlyNumber(e) {

    const inp = e.target;
    inp.value = inp.value.replace(/[^0-9]/g, '');

}

function TextBoxOnlyNumberFloat(e) {
    try {
        e = e || window.event;
        var charCode = e.which || e.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode === 46) {
            if (charCode === 46 && e.target.value.includes('.')) {
                return false;
            }
            return true;
        }

        return false;
    } catch (e) {
    }
}

function InputNoType(e) {

    try {

        debuggerWeb();

        return false;

    } catch (e) {

    }

}

function BackGroundColorInMouseEnterAndLeaveElement(id, bgColorCurent, bgColorHover, isRemove = false) {

    try {

        if (!isRemove) {
            $(id).css('background-color', bgColorHover);
        }
        else {
            $(id).css('background-color', bgColorCurent);
        }

    } catch (e) {

    }

}

function RefreshImageCapcha(idImageCode = "divImageCode", idHidenImage = "PicSecurity") {

    try {

        AjaxCallAction("POST", "/Capcha/getCapcha_Image", {}, true, function (result) {
            if (result.Success) {
                $("#" + idImageCode).html(result.Script);
                $("#" + idHidenImage).val(result.Script);
            }
        }, false);

    } catch (e) {

    }

}

function RemoveAllCharForPrice(id) {

    try {

        $('#' + id).val(!isEmpty($('#' + id).val()) ? removeComaForString($('#' + id).val()) : $('#' + id).val());

    } catch (e) {

    }

}
function TextBoxOnlyPrice(e) {

    try {

        $(e).val(moneyCommaSepWithReturn($(e).val()));

    } catch (e) {

    }

}
function moneyCommaSepWithReturn(ctrl) {

    try {

        if (ctrl != "" && ctrl != null) {
            return AmountMaskE2(ctrl);
        }

    } catch (e) {

    }

}
function AmountMaskE2(amount) {

    try {

        var i, j, mystring, flag;
        if (amount == '')
            return "";
        i = amount.length;
        mystring = "";
        for (j = 0; j < i; j++) {
            if (amount.substring(j, j + 1) == ",") {
                flag = true;
            }
        }
        if (flag == true) {
            amount = DAmountMaskE(amount);
        }
        i = amount.length;
        if (i > 3) {
            for (j = i; j > 0; j = j - 3) {

                if (j > 3) {
                    mystring = "," + amount.substring(j - 3, j) + mystring;

                } else {
                    mystring = amount.substring(0, j) + mystring;
                }
            }
            return mystring;
        } else {

            return amount;
        }

    } catch (e) {

    }

}
function DAmountMaskE(amount) {

    try {

        var i, j, mystring, str;
        i = amount.length;
        mystring = "";
        for (j = i; j >= 0; j -= 1) {
            str = amount.substring(j, j - 1);
            if (str != ",") {
                mystring = str + mystring;
            }
        }
        return mystring;

    } catch (e) {

    }

}
function removeComa(str) {

    try {

        if (str == "" || str == null) {
            return 0;
        }
        str = str.replace(",", "");
        str = str.replace(",", "");
        str = str.replace(",", "");
        str = str.replace(",", "");
        str = str.replace(",", "");
        return parseInt(str);

    } catch (e) {

    }

}
function removeComaForString(str) {

    try {

        if (str == "" || str == null) {
            return 0;
        }
        str = str.replace(",", "");
        str = str.replace(",", "");
        str = str.replace(",", "");
        str = str.replace(",", "");
        str = str.replace(",", "");
        return str;

    } catch (e) {

    }

}

function InitModal_Withot_Par(title, body, actionName, visibleButtonAction = false, widthModal = "", titleButtonAction = "ذخیره") {

    try {

        $("#hMainTitleModal").html(title);
        $("#divMainBodyModal").html(body);
        $("#btnSaveModal").attr("onclick", actionName);
        $("#btnSaveModal").html(titleButtonAction);

        if (!isEmpty(widthModal)) $("#modalMain").find(".modal-dialog.modal-lg").attr("style", widthModal);

        //if (visibleButtonAction) $("#btnSaveModal").remove();

        var _saveButton = document.getElementById("btnSaveModal");
        if (visibleButtonAction) {
            _saveButton.style.display = "none";
        } else {
            _saveButton.style.display = "initial";
        }


        $('#modalMain').modal('show');

    } catch (e) {

    }

}
function InitModal(title, url, data, actionName, visibleButtonAction = false, async = true, widthModal = "", successCallBack = null) {

    try {

        AjaxCallAction("GET", url, data, async, function (res) {

            debuggerWeb();

            InitModal_Withot_Par(title, res, actionName, visibleButtonAction, widthModal);

            if (successCallBack != null) successCallBack();

        }, false);

    } catch (e) {

    }

}
function InitModalWithJsonData(title, url, data, actionName, visibleButtonAction = false, async = true, widthModal = "") {

    try {

        AjaxCallAction("GET", url, data, async, function (res) {

            debuggerWeb();

            if (res.Success) {

                InitModal_Withot_Par(title, res.Html, actionName, visibleButtonAction, widthModal);

            }
            eval(res.Script);

        }, false);

    } catch (e) {

    }

}
function CloseModal() {

    try {

        $('#modalMain').modal('hide');

    } catch (e) {

    }

}
function ShowModal() {

    $('#modalMain').modal('show');

}
function getTableId(e) {

    try {
        return $(e).closest("table").attr('id');

    } catch (e) {

    }

}
function AddToTable(tblId, arrayList, hiddenCell = null, isTfootElement = false) {

    try {

        var table;

        if (!isTfootElement) table = document.getElementById(tblId);
        else table = document.getElementById(tblId).getElementsByTagName('tbody')[0];

        var rowId;

        for (var i = 0; i < arrayList.length; i++) {
            rowId = parseInt(table.rows.length);
            var row = table.insertRow(rowId);
            var indexRow = arrayList[i].length;
            for (var j = 0; j < indexRow; j++) {
                var cell1;
                if (hiddenCell == j)
                    table.rows[i + 1].cells[hiddenCell - 1].innerHTML += arrayList[i][j];
                else {
                    cell1 = row.insertCell(j);
                    cell1.innerHTML = arrayList[i][j];
                }
            }
        }

    } catch (e) {

    }
}
function removeRow(e, currentRowId) {

    try {

        var table = document.getElementById(getTableId(e));
        var rowId = parseInt(table.rows.length);

        for (var i = currentRowId + 1; i < rowId; i++) {
            var r = table.rows[i];
            r.innerHTML = r.innerHTML.replace("[" + (i - 1) + "]", "[" + (i - 2) + "]");
            r.innerHTML = r.innerHTML.replace("[" + (i - 1) + "]", "[" + (i - 2) + "]");
            r.innerHTML = r.innerHTML.replace("[" + (i - 1) + "]", "[" + (i - 2) + "]");
            r.innerHTML = r.innerHTML.replace("[" + (i - 1) + "]", "[" + (i - 2) + "]");
            r.innerHTML = r.innerHTML.replace("[" + (i - 1) + "]", "[" + (i - 2) + "]");
            r.innerHTML = r.innerHTML.replace("[" + (i - 1) + "]", "[" + (i - 2) + "]");
            r.innerHTML = r.innerHTML.replace("[" + (i - 1) + "]", "[" + (i - 2) + "]");


            r.innerHTML = r.innerHTML.replace("(this," + (i) + ")", "(this," + (i - 1) + ")");
            r.innerHTML = r.innerHTML.replace("(this," + (i) + ")", "(this," + (i - 1) + ")");

            if (Number.isInteger(parseInt(r.cells[0].innerHTML.trim()))) r.cells[0].innerHTML = i - 1;
        }
        table.deleteRow(currentRowId);

    } catch (e) {

    }

}

function GetUrlEventTabList(url, divId, successCallBack = null) {

    try {

        debuggerWeb();

        $("#" + divId).html("لطفاً منتظر بمانید...");

        AjaxCallAction("GET", url, {}, true, function (res) {

            debuggerWeb();

            $("#" + divId).html(res);

            if (successCallBack != null) eval(successCallBack);


        }, false);

    } catch (e) {

    }

}
function GetUrlEventTabListCallOneEvent(url, divId, successCallBack = null) {

    try {

        debuggerWeb();

        if (isEmpty($("#" + divId).html().trim())) {

            $("#" + divId).html("لطفاً منتظر بمانید...");

            AjaxCallAction("GET", url, {}, true, function (res) {

                debuggerWeb();

                $("#" + divId).html(res);

                if (successCallBack != null) eval(successCallBack);

            }, false);

        }

    } catch (e) {

    }

}

function CountdownTimerForMinutesAndSeconds(elementSelector, successCallBack = null, ms = 1000, charSplit = ':', timer = "01:59") {

    try {

        var interval = setInterval(function () {


            var timerRes = timer.split(charSplit);
            //by parsing integer, I avoid all extra string processing
            var minutes = parseInt(timerRes[0], 10);
            var seconds = parseInt(timerRes[1], 10);
            --seconds;
            minutes = (seconds < 0) ? --minutes : minutes;
            if (minutes < 0) {
                clearInterval(interval);

                if (successCallBack != null) successCallBack();

            }
            seconds = (seconds < 0) ? 59 : seconds;
            seconds = (seconds < 10) ? '0' + seconds : seconds;
            //minutes = (minutes < 10) ?  minutes : minutes;
            $(elementSelector).html(minutes + charSplit + seconds);
            timer = minutes + charSplit + seconds;

        }, ms);

    } catch (e) {

    }

}

function init_Education_User(title, body, selectorHid_Education_User_Counter) {

    try {

        $("#li_borderInItemsMenuWithAnimation" + $(selectorHid_Education_User_Counter).val()).addClass("borderInItemsMenuWithAnimation");
        $("#i_borderInItemsMenuWithAnimation" + $(selectorHid_Education_User_Counter).val()).addClass("fa-spin");

        confirmB(title, toHtml_Text(body[parseInt($(selectorHid_Education_User_Counter).val())]), 'success', function () {

            debuggerWeb();

            $("#li_borderInItemsMenuWithAnimation" + $(selectorHid_Education_User_Counter).val()).removeClass("borderInItemsMenuWithAnimation");
            $("#i_borderInItemsMenuWithAnimation" + $(selectorHid_Education_User_Counter).val()).removeClass("fa-spin");

            if (parseInt($(selectorHid_Education_User_Counter).val()) < body.length - 1) {

                $(selectorHid_Education_User_Counter).val((parseInt($(selectorHid_Education_User_Counter).val()) + 1).toString());

                init_Education_User(title, body, selectorHid_Education_User_Counter);

                $("#li_borderInItemsMenuWithAnimation" + $(selectorHid_Education_User_Counter).val()).addClass("borderInItemsMenuWithAnimation");
                $("#i_borderInItemsMenuWithAnimation" + $(selectorHid_Education_User_Counter).val()).addClass("fa-spin");

                if ($(selectorHid_Education_User_Counter).val() === "3") {
                    $("#li_MainAllBorrowersWithComplete").addClass("borderInItemsMenuWithAnimation");
                    $("#li_MainAllBorrowersWithComplete > a > i").addClass("fa-spin");
                }

            }
            else {
                alertB_And_confirmB_Close();
                $("#li_MainAllBorrowersWithComplete").removeClass("borderInItemsMenuWithAnimation");
                $("#li_MainAllBorrowersWithComplete > a > i").removeClass("fa-spin");
                $(selectorHid_Education_User_Counter).val("0");
            }

        }, function () {

            debuggerWeb();

            $("#li_MainAllBorrowersWithComplete").removeClass("borderInItemsMenuWithAnimation");
            $("#li_MainAllBorrowersWithComplete > a > i").removeClass("fa-spin");
            $("#li_borderInItemsMenuWithAnimation" + $(selectorHid_Education_User_Counter).val()).removeClass("borderInItemsMenuWithAnimation");
            $("#i_borderInItemsMenuWithAnimation" + $(selectorHid_Education_User_Counter).val()).removeClass("fa-spin");

            if (parseInt($(selectorHid_Education_User_Counter).val()) > 0) {

                $(selectorHid_Education_User_Counter).val((parseInt($(selectorHid_Education_User_Counter).val()) - 1).toString());

                init_Education_User(title, body, selectorHid_Education_User_Counter);

            }
            else alertB_And_confirmB_Close();

            if ($(selectorHid_Education_User_Counter).val() !== "0") {
                $("#li_borderInItemsMenuWithAnimation" + $(selectorHid_Education_User_Counter).val()).addClass("borderInItemsMenuWithAnimation");
                $("#i_borderInItemsMenuWithAnimation" + $(selectorHid_Education_User_Counter).val()).addClass("fa-spin");
            }

        }, ["مرحله قبل", "مرحله بعد"], false);

    } catch (e) {

    }

}

function toHtml_Text(text) {

    try {

        var q = document.createElement("div");
        q.innerHTML = text;

        return q.innerText;

    } catch (e) {

    }

}

function MultiSelect(selector) {

    try {

        $(selector).multiSelect()

    } catch (e) {

    }

}

function searchStringInArray(str, strArray) {
    try {

        for (var j = 0; j < strArray.length; j++) {
            if (strArray[j].match(str)) return j;
        }
        return -1;

    } catch (e) {

    }
}
function mobileTableFix() {
    var mr_tabels = $(".table[nomobile != 'true']")
    mr_tabels.each(function () {
        var itable = $(this);
        //console.log(itable.html());
        var mr_heads = new Array();

        itable.find('thead tr th').each(function (titles) {
            mr_heads.push($(this).html());
        });


        itable.find("tbody tr[responsived != 'true']").each(function () {
            var children = $(this).children();
            if (children.length != mr_heads.length) return;
            for (var i = 0; i < children.length; i++) {

                children.eq(i).html('<span class="thead_on_tr">' + mr_heads[i] + "</span>" + children.eq(i).html());
            }
            $(this).attr('responsived', 'true');
        });


    });
}

var changeLock = false;
window.addEventListener("DOMNodeInserted", function (event) {
    if (changeLock) return;
    changeLock = true;
    setTimeout(() => {
        mobileTableFix();
        changeLock = false;
    }, 100);

}, false);

function filterGlobal(idTextSearch, idComboSelectCount) {

    try {

        debuggerWeb();

        eval("setPagerAjaxTemp_" + idTextSearch)($("#" + idTextSearch).val(), 1, $('#' + idComboSelectCount).val(), 1);

    } catch (e) {

    }

}

Web.Resources = {
    ShowMessageForUser: "ShowMessageForUser",
    MessageWarningForOnlyUploadPhotos: "شما فقط میتوانید تصویر آپلود نمایید",
    MessageTypeWarning: "اخطار",
    MessageFileSuccessfullyLoaded: "فایل با موفقیت بارگذاری شد",
    MessageAreYouSure: "آیا مطمئن هستید؟",
    Select: "انتخاب",
    Code301: "301",
    Code401: "401",
    MessageAccessDenied: "شما اجازه انجام ایم عملیات را ندارید",
    MessageWarningEditAllPagesBossComfirm: "آیتم مورد نظر توسط مدیر سامانه تایید شده است اگر عملیات ویرایش را انجام دهید دوباره باید منتظر تایید مدیر سامانه بمانید"
};


getCookie = function (cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
};

function delete_cookie(name) {
    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

setCookie = function (cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = d.toUTCString();
    expires = expires.replace("GMT", "UTC");
    document.cookie = cname + "=" + cvalue + ";" + " expires= " + expires + "; Secure; path=/";
};


//Document Ready  : 

function debuggerWeb() {

    try {

        //debugger;

    } catch (e) {

    }

}

function getU(path, successCallBack = null, dataType = 'html') {

    $.get(path, function (res) {
        successCallBack(res);
    }, dataType);

}

