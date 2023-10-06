





(function (web, $) {

    //Document Ready              

    function signOutForExitToLogin() {

        AjaxCallActionWithotHeading("POST", "/api/Securitys/SignOutForExitToLogin", JSON.stringify({ Userid: getlstor("userID"), AreaName: getlstor("roleDesc") }), true, function (res) {
            debugger;
            if (res.isSuccess) {

                localStorage.removeItem("token");

                //dellstor("menu");
                localStorage.removeItem("menu");

                dellstor("fullName");
                dellstor("userID");
                dellstor("loginName");
                dellstor("roleDesc");
                goToUrl("/Account/LoginUser");

            }

        }, false);

    }

    function initLayout() {

        if (isEmpty(localStorage.getItem("token")) || isEmpty(localStorage.getItem("menu"))) goToUrl("/Account/LoginUser");
        else {

            $(".fullNameInLayout").html(getlstor("fullName"));

            /*var menu = JSON.parse(getlstor("menu"));*/
            var menu = JSON.parse(localStorage.getItem("menu"));

            var strMenu = "<li><a href='/'><img src='/css/GlobalAreas/dist/img/Logo/hnet.com-image.png' class='' style='width: 20px; height: 20px;' /> <span>سامانه پارس کیان</span></a></li><li class='active'><a href='/Admin'><i class='fa fa-dashboard'></i> <span>داشبورد</span></a></li>";

            if (menu != null && menu.length > 0) {

                menu = menu.filter(p => p.link != "");

                var groupByList = groupBy(menu, "labelGroup");

                for (var i = 0; i < groupByList.length; i++) {

                    strMenu += "<li class='treeview'><a href='#'><i class='" + groupByList[i][1][0].icon + "'></i> <span>" + groupByList[i][0] + "</span><span class='pull-left-container'><i class='fa fa-angle-right pull-left'></i></span></a><ul class='treeview-menu'>";

                    var qL = menu.filter(o => o.labelGroup == groupByList[i][0]);

                    qL.forEach(function (itemC, iC) {

                        strMenu += "<li><a href='" + itemC.link + "'><i class='fa fa-plus'></i>" + itemC.group_Item + "</a></li>";

                    });
                    strMenu += "</ul></li>";

                }

            }

            strMenu += "<li><a href='#' onclick='Web.Layout.SignOutForExitToLogin();'><i class='fa fa-sign-out'></i> <span>خروج</span></a></li>";
            $("#ulMenuMain").html(strMenu);

        }

    }

    web.Layout = {

        SignOutForExitToLogin: signOutForExitToLogin,
        InitLayout: initLayout

    };

})(Web, jQuery);