
(function (web, $) {

    function initArticleList(id=null) {

        AjaxCallAction("GET", "/api/Article/Get_Articles/"+id, null, true, function (res) {

            if (res != null) {

               
            }

        }, true);

    }
    function initArticle(id = null) {

        AjaxCallAction("GET", "/api/Article/Get_Article/" + id, null, true, function (res) {

            if (res != null) {


            }

        }, true);

    }
    web.Article = {
        initArticle: initArticle,
        initArticleList: InitArticleList
    };

})(Web, jQuery);