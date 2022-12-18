
(function (web, $) {

    function intRankingOfCompaniesPage() {

      //  AjaxCallAction("POST", "/api/RankingOfCompanies/Get_RankingOfCompaniess", JSON.stringify({ Search: null, PageIndex: 1, PageSize:6 }), true, function (res) {

          //  if (res.isSuccess) {

                var strM = '';
               // for (var i = 0; i < res.data.length; i++) {

                  //  strM += "<tr><td>" + (i + 1) + "</td><td>" + res.data[i].title + "</td><td><img src=" + res.data[i].pictureFull + " style='max-width:50px'/></td><td><a title='ویرایش' href='/Admin/LicensesAndHonors/EditLicensesAndHonors?id=" + res.data[i].licensesAndHonorsId + "' class='btn btn-edit fontForAllPage'><i class='fa fa-edit'></i></a></td></tr>";
                for (var i = 0; i < 6; i++) {

                    strM += "<div class='top_rateing_card'>" +
                        "<div class='c1'>" +
                        "<p>رتبه</p>" +
                        "<div class='c2'>" +
                        "<p>کوتاه مدت<span>AA+</span></p>" +
                        "<p>بلند مدت<span>AAA</span></p>" +
                        "</div>" +
                        "</div>" +
                        "<div class='c3'>" +
                        "<h5>شرکت پتروشیمی شهرآرا</h5>" +
                        "<div class='c4'>"+
                        "<p>وضعیت:<span>در حال پایش</span></p>" +
                        "<p>دورنما:<span>با ثبات</span></p>" +
                        "<p>بخش:<span>پتروشیمی</span></p>" +
                        "</div>" +
                        "<div class='c5'>" +
                        "<p>تاریخ:<span>۱۴۰۱/۰۳/۱۹</span></p>" +
                        "<a class='btn' href='/doc/test.pdf'>مشاهده</a>" +
                        "</div>" +
                        "</div>" +
                        "</div>";
               
                }
                $("#RankingOfCompanies").html(strM);

         //   }

       // }, true);

    }



    web.RankingOfCompanies = {
        IntRankingOfCompaniesPage: intRankingOfCompaniesPage,
    };

})(Web, jQuery);