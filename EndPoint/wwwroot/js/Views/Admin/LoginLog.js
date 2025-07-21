(function (web, $) {
    let searchTimeout;

    function textSearchOnKeyUp(event) {
        clearTimeout(searchTimeout);
        searchTimeout = setTimeout(function () {
            filterGrid(1); 
        }, 500);
    }

    function initLoginLog() {
        PersianDatePicker(".DatePicker");
        $("#txtSearch").on("keyup", textSearchOnKeyUp);
        $("#cboSelectCount").on("change", function () {
            filterGrid(1);
        });

        $("#btnSearch").on("click", function () {
            filterGrid(1);
        });

        filterGrid(1);
    }


    function filterGrid(pageIndex = 1) {
        const pageSize = parseInt($("#cboSelectCount").val()) || 10;
        const sortOrder = $(".thtrtheadtableSortingGrid_LoginLog_tBodyList[data-Selected]").attr("data-Selected") || "";

        const fromDateStr = $("#FromDateStr").val();
        const toDateStr = $("#ToDateStr").val();

        const requestData = {
            SortOrder: sortOrder,
            Search: $("#txtSearch").val(),
            PageIndex: pageIndex,
            PageSize: pageSize,
            FromDateStr1: fromDateStr,
            ToDateStr1: toDateStr
        };

        AjaxCallAction("POST", "/api/admin/LoginLog/Get_LoginLogs", JSON.stringify(requestData), true, function (res) {
            if (res.isSuccess) {
                $("#TotalRowRep").text("جستجو در " + res.rows + " مورد");

                let strM = "";
                for (let i = 0; i < res.data.length; i++) {
                    const rowNumber = ((pageIndex - 1) * pageSize) + i + 1;
                    strM += `<tr>
                    <td>${rowNumber}</td>
                    <td>${res.data[i].fullName}</td>
                    <td>${res.data[i].loginDateStr} - ${res.data[i].loginTimeStr}</td>
                    <td>${res.data[i].signOutDateStr} - ${res.data[i].signOutTimeStr}</td>
                    <td>${res.data[i].ip}</td>
                    <td>${res.data[i].areaName}</td>
                </tr>`;
                }

                $("#tBodyList").html(strM);
                updatePagination(pageIndex, res.rows, pageSize);
            } else {
                alert("خطا در دریافت اطلاعات");
            }
        }, function () {
            alert("خطا در ارتباط با سرور");
        });
    }

    function updatePagination(currentPage, totalRows, pageSize) {
        const totalPages = Math.ceil(totalRows / pageSize);
        const pagination = $("#pagination");
        pagination.empty();

        if (totalPages <= 1) return;

        pagination.append(
            `<li class="${currentPage === 1 ? 'disabled' : ''}">
                <a href="#" onclick="Web.LoginLog.FilterGrid(${currentPage - 1 > 0 ? currentPage - 1 : 1})">صفحه قبل</a>
            </li>`
        );

        const maxPagesToShow = 4;
        let startPage = 1;
        let endPage = totalPages;

        if (totalPages > maxPagesToShow) {
            if (currentPage <= 2) {
                startPage = 1;
                endPage = maxPagesToShow;
            } else if (currentPage >= totalPages - 1) {
                startPage = totalPages - (maxPagesToShow - 1);
                endPage = totalPages;
            } else {
                startPage = currentPage - 1;
                endPage = currentPage + 2 > totalPages ? totalPages : currentPage + 2;
                if (endPage - startPage + 1 < maxPagesToShow) {
                    startPage = endPage - maxPagesToShow + 1;
                }
            }
        }

        if (startPage > 1) {
            pagination.append(
                `<li>
                    <a href="#" onclick="Web.LoginLog.FilterGrid(1)">1</a>
                </li>`
            );
            if (startPage > 2) {
                pagination.append(`<li><span>...</span></li>`);
            }
        }

        for (let i = startPage; i <= endPage; i++) {
            pagination.append(
                `<li class="${currentPage === i ? 'active' : ''}">
                    <a href="#" onclick="Web.LoginLog.FilterGrid(${i})">${i}</a>
                </li>`
            );
        }

        if (endPage < totalPages) {
            if (endPage < totalPages - 1) {
                pagination.append(`<li><span>...</span></li>`);
            }
            pagination.append(
                `<li>
                    <a href="#" onclick="Web.LoginLog.FilterGrid(${totalPages})">${totalPages}</a>
                </li>`
            );
        }

        pagination.append(
            `<li class="${currentPage === totalPages ? 'disabled' : ''}">
                <a href="#" onclick="Web.LoginLog.FilterGrid(${currentPage + 1 <= totalPages ? currentPage + 1 : totalPages})">صفحه بعد</a>
            </li>`
        );
    }

    function clickSortingGridLoginLog(e) {
        const th = $(e.currentTarget);
        const column = th.data("column");
        if (!column) return;

        let currentSortOrder = th.attr("data-sort-order") || "";

        if (currentSortOrder === "asc") {
            currentSortOrder = "desc";
        } else {
            currentSortOrder = "asc";
        }
        $(".sortable").attr("data-sort-order", "");

        th.attr("data-sort-order", currentSortOrder);

        const sortOrder = `${column} ${currentSortOrder}`;

        $("#SortOrderHidden").val(sortOrder);

        filterGrid(1); 
    }

    web.LoginLog = {
        FilterGrid: filterGrid,
        InitLoginLog: initLoginLog,
        ClickSortingGridLoginLog: clickSortingGridLoginLog,
        TextSearchOnKeyUp: textSearchOnKeyUp
    };

})(Web, jQuery);
