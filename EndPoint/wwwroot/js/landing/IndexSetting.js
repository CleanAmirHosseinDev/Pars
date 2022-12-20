
(function (web, $) {

   
    function baseIndexSetting() {
        Web.AboutUs.InitMoto();
        Web.RankingOfCompanies.IntRankingOfCompaniesPage();
    }
   

    web.IndexSetting = {        
        BaseIndexSetting: baseIndexSetting,
    };

})(Web, jQuery);