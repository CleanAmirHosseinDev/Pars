
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
                if (item.group === menu[i].group && !GetNullEmpetyUndefined(menu[i].link))
                    srt += '<li><a href="' + menu[i].link + '"><i class="feather icon-circle"></i><span id="' + menu[i].value + '" class="menu-item" data-i18n="List">' + menu[i].group_Item + '</span></a></li>';

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
AjaxCall = function (url, data, type) {
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
        headers: { "Authorization": 'Bearer ' + getlstor("token") },
        beforeSend: function (xhr) {
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
fillGrid = function (Data, columnDefs, columnCountShow = 10, nameGrid = "myGrid") {
    try {
        if (document.getElementById("myGrid")) {

            /*** GRID OPTIONS ***/
            var gridOptions = {
                defaultColDef: {
                    sortable: true
                },
                enableRtl: true,
                columnDefs: columnDefs,
                rowSelection: "multiple",
                floatingFilter: true,
                filter: true,
                pagination: true,
                paginationPageSize: columnCountShow,
                pivotPanelShow: "always",
                colResizeDefault: "shift",
                animateRows: true,
                resizable: true
            };

            /*** DEFINED TABLE VARIABLE ***/
            var gridTable = document.getElementById(nameGrid);
            /*** FILTER TABLE ***/
            function updateSearchQuery(val) {
                gridOptions.api.setQuickFilter(val);
            }

            $(".ag-grid-filter").on("keyup", function () {
                updateSearchQuery($(this).val());
            });

            /*** CHANGE DATA PER PAGE ***/
            function changePageSize(value) {
                gridOptions.api.paginationSetPageSize(Number(value));
            }

            $(".sort-dropdown .dropdown-item").on("click", function () {
                var $this = $(this);
                changePageSize($this.text());
                $(".filter-btn").text("1 - " + $this.text() + " of 50");
            });

            /*** EXPORT AS CSV BTN ***/
            $(".ag-grid-export-btn").on("click", function (params) {
                gridOptions.api.exportDataAsCsv();
            });

            //  filter data function
            var filterData = function agSetColumnFilter(column, val) {
                var filter = gridOptions.api.getFilterInstance(column)
                var modelObj = null
                if (val !== "all") {
                    modelObj = {
                        type: "equals",
                        filter: val
                    }
                }
                filter.setModel(modelObj)
                gridOptions.api.onFilterChanged()
            }
            //  filter inside role
            $("#users-list-role").on("change", function () {
                var usersListRole = $("#users-list-role").val();
                filterData("role", usersListRole)
            });
            //  filter inside verified
            $("#users-list-verified").on("change", function () {
                var usersListVerified = $("#users-list-verified").val();
                filterData("is_verified", usersListVerified)
            });
            //  filter inside status
            $("#users-list-status").on("change", function () {
                var usersListStatus = $("#users-list-status").val();
                filterData("status", usersListStatus)
            });
            //  filter inside department
            $("#users-list-department").on("change", function () {
                var usersListDepartment = $("#users-list-department").val();
                filterData("department", usersListDepartment)
            });
            // filter reset
            $(".users-data-filter").click(function () {
                $('#users-list-role').prop('selectedIndex', 0);
                $('#users-list-role').change();
                $('#users-list-status').prop('selectedIndex', 0);
                $('#users-list-status').change();
                $('#users-list-verified').prop('selectedIndex', 0);
                $('#users-list-verified').change();
                $('#users-list-department').prop('selectedIndex', 0);
                $('#users-list-department').change();
            });
            /*** GET TABLE DATA FROM URL ***/
            agGrid
                .simpleHttpRequest({
                    url: "/Content/app-assets/data/users-list.json"
                })
                .then(function (data) {
                    if (!GetNullEmpetyUndefined(Data)) {
                        gridOptions.api.setRowData(Data);
                    }
                });
            new agGrid.Grid(gridTable, gridOptions);


        }
    } catch (e) {

    }
};