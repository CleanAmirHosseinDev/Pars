﻿






(function (web, $) {

    //Document Ready  
    

    function textSearchOnKeyDown(event) {

        if (event.keyCode == 13) $(`button[title='جستجو']`).click();

    }

    function filterGrid() {

        ComboBoxWithSearch('.select2', 'dir');
        get_Roles_Combo();

        AjaxCallAction("POST", "/api/admin/Users/Get_Userss", JSON.stringify({ Search: $("#txtSearch").val(), PageIndex: 1, PageSize: $("#cboSelectCount").val(), RoleId: isEmpty($("#RoleId").val()) ? null : $("#RoleId").val(), }), true, function (res) {

            if (res.isSuccess) {

                var strM = '';
                $("#TotalRowRep").text("جستجو در "+res.rows+" مورد");
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].user.userName == "admin" || res.data[i].role.roleDesc == "مشتری") {
                        strM += "<tr><td>" + (i + 1) + "</td><td>" + (res.data[i].user.realName != null ? res.data[i].user.realName:"") + "</td><td>" + res.data[i].user.userName + "</td><td>" + res.data[i].role.roleDesc + "</td><td>" + (res.data[i].user.status == true ? "فعال" : "غیر فعال") + "</td><td>" + res.data[i].user.mobile + "</td><td><a title='ویرایش' href='/Admin/Users/EditUser?id=" + res.data[i].userId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a><a title='حذف' class='btn btn-danger fontForAllPage' onclick='Web.Users.Delete_Users(" + res.data[i].userId + ");'><i class='fa fa-remove'></i></a>" + "</td></tr>";

                    } else {
                        //<a title='حذف' class='btn btn-danger fontForAllPage' onclick='Web.Users.Delete_Users(" + res.data[i].userId + ");'><i class='fa fa-remove'></i></a>
                        strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].user.realName + "</td><td>" + res.data[i].user.userName + "</td><td>" + res.data[i].role.roleDesc + "</td><td>" + (res.data[i].user.status == true ? "فعال" : "غیر فعال") + "</td><td>" + res.data[i].user.mobile + "</td><td><a title='ویرایش' href='/Admin/Users/EditUser?id=" + res.data[i].userId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a>" + "<a style='margin-right:5px' Title='تنظیمات دسترسی کاربر' href='/Admin/Users/EditUserAccessLevel?id=" + res.data[i].userId + "' class='btn btn-info fontForAllPage'><i class='fa fa-cog'></i></a>" + "</td></tr>";
                    }
                  }
                $("#tBodyList").html(strM);
            }
        }, true);

    }

    function saveUser(e) {

        $(e).attr("disabled", "");

        let status = null;
        if ($('#Status').is(":checked")) {
             status = true;
        }

        else {
             status = false;
        }         

        AjaxCallAction("POST", "/api/admin/Users/Save_Users", JSON.stringify({ Id: $("#Id").val(), RoleId: $("#RoleId").val(), UserID: $("#UserId").val(), User: { RealName: $("#RealName").val(), UserName: $("#UserName").val(), Password: $("#Password").val(), Mobile: $("#Mobile").val(), Email: $("#Email").val(), Status: status} }), true, function (res) {

            $(e).removeAttr("disabled");

            if (res.isSuccess) {
                
                goToUrl("/Admin/Users/Index");
            } else {

                alertB("خطا", res.message, "error");
            }

        }, true);

    }

    function saveUserAccessLevel(e) {
        try {
            $(e).attr("disabled", "");
            
                
                var val1 = [];
                $('select[name="SuperVisorRoles_Arr[]"] option:selected').each(function () {
                    val1.push($(this).val());
                });

                AjaxCallAction("POST", "/api/admin/Users/Save_AccessLevels", JSON.stringify({ Roles: val1.join(","), UserId: $("#UserId").val() }), true, function (res) {
                    $(e).removeAttr("disabled");

                    if (res.isSuccess) {

                        goToUrl("/Admin/Users/Index");
                    } else {

                        alertB("خطا", res.message, "error");
                    }
                }, true);            

        } catch (e) {

        }
    }

    function get_Roles_Combo() {

        AjaxCallAction("POST", "/api/admin/Users/Get_Roles_Combo", JSON.stringify({ PageIndex: 0, PageSize: 0 }), true, function (res) {

            if (res.isSuccess) {

               
                var strM = '<option value="">انتخاب کنید</option>';
                for (var i = 0; i < res.data.length; i++) {
                    strM += " <option value=" + res.data[i].roleId + ">" + res.data[i].roleDesc + "</option>";
                }
                $("#RoleId").html(strM);
            }
        }, true);

    }

    function initUser(id = null, dir = 'rtl') {

        ComboBoxWithSearch('.select2', 'dir');
        get_Roles_Combo();
        
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/Users/Get_Users/" + id, null, true, function (res) {

                if (res != null) {
                    $("#Id").val(res.id);
                    $("#UserId").val(res.userId);
                    $("#RoleId").val(res.roleId);
                    comboBoxWithSearchUpdateText("RoleId", res.role.roleDesc);
                    $("#RealName").val(res.user.realName);
                    $("#UserName").val(res.user.userName);
                    $("#Mobile").val(res.user.mobile);
                    $("#Email").val(res.user.email);
                    $("#Status").prop('checked', res.user.status);
                   
                    getGetAccessLevels(id,dir);
                  
                }
            }, true);
        }
        else ComboBoxWithSearch('.select2', dir);
       
    }

    function initUserAccessLevel(id = null, dir = 'rtl') {

        
        if (!isEmpty(id) && id != 0) {

            AjaxCallAction("GET", "/api/admin/Users/Get_Users/" + id, null, true, function (res) {

                if (res != null) {
                    $("#Id").val(res.id);
                    $("#UserId").val(res.userId);
                    $("#RoleId").val(res.roleId);                 
                    $("#RealName").val(res.user.realName);                   
                    getGetAccessLevels(id, dir);

                }
            }, true);
        }

    }

    function getGetAccessLevels(id = null,dir) {
        if (!isEmpty(id) && id != 0) {
            AjaxCallAction("GET", "/api/admin/Users/GetAccessLevels/" + id, null, true, function (res) {


                var strAccessPanel = "<div class='container mt-3'><div id='accordion'>";

                if (res != null && res.length > 0) {

                    var groupByList = groupBy(res, "labelGroup");

                   
                    for (var i = 0; i < groupByList.length; i++) {

                        //======================== سطح اول =========================================

                        strAccessPanel += "<div class='col-md-12'>";
                        //strAccessPanel += "<div class='panel panel-primary'>";
                        //strAccessPanel += "<div class='panel-heading'>" + groupByList[i][0] + "</div>";
                        //strAccessPanel += "<div class='panel-body' >";
                        
                        strAccessPanel += "<div class='card'><div class='card-header'><a class='collapsed btn' data-bs-toggle='collapse' href='#Collaps" + i + "'><i class='fa fa-plus-square-o' aria-hidden='true'></i>&nbsp;&nbsp;" + groupByList[i][0] + "</a></div>";
                        strAccessPanel += "<div id='Collaps" + i+"' class='collapse' data-bs-parent='#accordion'><div class='card-body'>";

                        var groupByList2 = groupBy(res, "group_Item");
                        for (var j = 0; j < groupByList2.length; j++) {
                            if (groupByList[i][0] === groupByList2[j][1][0].labelGroup) {


                                //====================== سطح دوم ======================= 
                                strAccessPanel += "<div class='col-md-4'>";
                                strAccessPanel += "<div class='panel panel-primary'>";
                                strAccessPanel += "<div class='panel-heading'>" + groupByList2[j][1][0].group_Item + "</div>";
                                strAccessPanel += "<div class='panel-body' style='height:100px;overflow-y:scroll;'>";
                                //========= گزینه ها ========= 
                                strAccessPanel += "<select multiple='multiple' class='multi-select' id='SuperVisorRoles_Arr' name='SuperVisorRoles_Arr[]'>";
                                for (var z = 0; z < groupByList2[j][1].length; z++) {
                                    strAccessPanel += "<option id='" + groupByList2[j][1][z].value + "' value='" + groupByList2[j][1][z].value + "'" + (groupByList2[j][1][z].selected ? "selected" : "") + ">" + groupByList2[j][1][z].text + "</option>";
                                }
                                strAccessPanel += "</select>";
                                //===============================
                                strAccessPanel += "</div>";
                                strAccessPanel += "</div>";
                                strAccessPanel += "</div>";
                            }
                            //=======================================================
                        }
                        strAccessPanel += "</div>";
                        strAccessPanel += "</div>";
                        strAccessPanel += "</div>";
                        strAccessPanel += "</div>";
                         //==========================================================================
                    }
                }
                strAccessPanel += "</div></div>";
                $("#AccessPanel").html(strAccessPanel);
                ComboBoxWithSearch('.select2', dir);
                MultiSelect(".multi-select");
            }, true);
        }
    }

    function delete_Users(id) {

        try {

            debuggerWeb();

            confirmB("", "آیا تمایل به حذف دارید؟", 'error', function () {

                AjaxCallAction("GET", "/api/admin/Users/Delete_Users/" + (isEmpty(id) ? '0' : id), null, true, function (result) {

                    debuggerWeb();

                    if (result.isSuccess) {

                        filterGrid();

                        alertB("", result.message, "success");

                    }
                    else {

                        alertB("خطا", result.message, "error");

                    }

                }, true);

            }, function () {

            }, ["خیر", "بلی"]);

        } catch (e) {

        }

    }

    function updatePass() {

        AjaxCallAction("POST", "/api/admin/Users/UpdatePass_Users", JSON.stringify({ OldPassword: $("#OldPassword").val(), NewPassword: $("#NewPassword").val(), ConfirmPassword: $("#ConfirmPassword").val() }), true, function (result) {

            debuggerWeb();

            if (result.isSuccess) {                

                alertB("", result.message, "success");

            }
            else {

                alertB("خطا", result.message, "error");

            }

        }, true);

    }
    
    web.Users = {
        TextSearchOnKeyDown: textSearchOnKeyDown,
        FilterGrid: filterGrid,
        SaveUser: saveUser,
        Get_Roles_Combo: get_Roles_Combo,
        InitUser: initUser,
        GetGetAccessLevels: getGetAccessLevels,
        SaveUserAccessLevel: saveUserAccessLevel,
        InitUserAccessLevel: initUserAccessLevel,
        Delete_Users: delete_Users,
        UpdatePass: updatePass
      
    };

})(Web, jQuery);