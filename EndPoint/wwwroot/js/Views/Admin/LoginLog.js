(function (web, $) {
    let searchTimeout;

    function textSearchOnKeyDown(event) {
        clearTimeout(searchTimeout);
        searchTimeout = setTimeout(function() {
            const currentPage = $("#pagination li.active a").text() || 1;
            filterGrid(currentPage);
        }, 500); // 500ms delay
    }

    function initLoginLog() {
        PersianDatePicker(".DatePicker");
        $("#txtSearch").on("keydown", textSearchOnKeyDown);
        filterGrid();
    }

    function filterGrid(pageIndex = 1) {
        const requestData = {
            SortOrder: $(".thtrtheadtableSortingGrid_LoginLog_tBodyList[data-Selected]").attr("data-Selected"),
            Search: $("#txtSearch").val(),
            PageIndex: pageIndex,
            PageSize: $("#cboSelectCount").val(),
            FromDateStr: $("#FromDateStr").val(),
            ToDateStr: $("#ToDateStr").val()
        };

        AjaxCallAction("POST", "/api/admin/LoginLog/Get_LoginLogs", JSON.stringify(requestData), true, function (res) {
            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");

                let strM = "";
                for (let i = 0; i < res.data.length; i++) {
                    strM += `<tr>
                        <td>${res.data[i].fullName}</td>
                        <td>${res.data[i].loginDateStr} - ${res.data[i].loginTimeStr}</td>
                        <td>${res.data[i].signOutDateStr} - ${res.data[i].signOutTimeStr}</td>
                        <td>${res.data[i].ip}</td>
                        <td>${res.data[i].areaName}</td>
                    </tr>`;
                }

                $("#tBodyList").html(strM);
                updatePagination(pageIndex, res.rows, requestData.PageSize);
            }
        }, true);
    }

    function updatePagination(currentPage, totalRows, pageSize) {
        const totalPages = Math.ceil(totalRows / pageSize);
        const pagination = $("#pagination");
        pagination.empty();

        if (totalPages <= 1) return;

        // Previous button
        pagination.append(
            `<li class="${currentPage === 1 ? 'disabled' : ''}">
                <a href="#" onclick="Web.LoginLog.FilterGrid(${currentPage - 1})">&laquo;</a>
            </li>`
        );

        for (let i = 1; i <= totalPages; i++) {
            pagination.append(
                `<li class="${currentPage === i ? 'active' : ''}">
                    <a href="#" onclick="Web.LoginLog.FilterGrid(${i})">${i}</a>
                </li>`
            );
        }

        // Next button
        pagination.append(
            `<li class="${currentPage === totalPages ? 'disabled' : ''}">
                <a href="#" onclick="Web.LoginLog.FilterGrid(${currentPage + 1})">&raquo;</a>
            </li>`
        );
    }

    function clickSortingGridLoginLog(e) {
        clickSortingGridWithConfig(e, "thtrtheadtableSortingGrid_LoginLog_tBodyList");
        filterGrid();
    }

    web.LoginLog = {
        FilterGrid: filterGrid,
        InitLoginLog: initLoginLog,
        ClickSortingGridLoginLog: clickSortingGridLoginLog
    };

})(Web, jQuery);
