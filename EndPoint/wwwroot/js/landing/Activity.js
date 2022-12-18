
(function (web, $) {

    function initActivity(id = null) {
        AjaxCallAction("GET", "/api/Activity/Get_Activity/" + (isEmpty(id) ? '0' : id), null, true, function (res) {

            if (res != null) {
                $("#ActivityTitle").text(res.activityTitleNavigation.label)
                $("#ActivityComment").html(res.activityComment);
                $("#imgUpload_Picture1").attr("src", res.picture1Full);
                $("#imgUpload_Picture2").attr("src", res.picture2Full);
            }

        }, true);

    }
    web.Activity = {      
        InitActivity: initActivity,       
    };

})(Web, jQuery);