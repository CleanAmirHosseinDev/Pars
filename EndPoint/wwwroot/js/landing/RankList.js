

function initPage() {

    
    LoadData();

   

}

function clickSortingGrid(e) {

    clickSortingGridWithConfig(e, "thtrtheadtableSortingGrid_LandingRankList");

    LoadData();

}

function LoadData() {


    AjaxCallActionWithReturnHtml("GET", "/Home/RankList_Data", { Search: isEmpty($("#Search").val()) ? '' : $("#Search").val(), SortOrder: $(".thtrtheadtableSortingGrid_LandingRankList[data-Selected]").attr("data-Selected"), CustomerID: getlstor("customerID") }, true, function (res) {

        $(".mainRowFull").remove();
        $(".jtable").append(res);                

    }, true);

}

function SummaryRankingAuten() {

    confirmB("", "برای دانلود خلاصه گزارش باید داخل سامانه ورود کنید؟", "warning", function () {

        goToUrl("/Account/Login?u=werew");

    }, function () {

    }, ["خیر", "بلی"]);

}