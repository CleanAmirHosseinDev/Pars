

function initPage() {

    LoadData();

}

function clickSortingGrid(e) {

    clickSortingGridWithConfig(e, "thtrtheadtableSortingGrid_LandingRankList");

    LoadData();

}

function LoadData() {


    AjaxCallActionWithReturnHtml("GET", "/Home/RankList_Data", { Search: isEmpty($("#Search").val()) ? '' : $("#Search").val(), SortOrder:  $(".thtrtheadtableSortingGrid_LandingRankList[data-Selected]").attr("data-Selected")}, true, function (res) {

        $(".mainRowFull").remove();
        $(".jtable").append(res);



    }, true);

}