function resizeSlider() {
    var w = $(".main").width();
    if (w > 1600) { w = 1600; }
    $(".slider").css("width", (w - 0.01) + "px");
}
$(window).on('resize', function (e) {
    resizeSlider();
});
$(resizeSlider);

////////////////////sec 1

jvSlider_currentPage = 1;
function jvSlider_next() {
    var maxcount = $(".topslider .slider_pages").children().length;
    if (jvSlider_currentPage < maxcount) {
        jvSlider_currentPage++;
    } else {
        jvSlider_currentPage = 1;
    }
    jvSlider_gotopage(jvSlider_currentPage);
}
function jvSlider_prev() {
    var maxcount = $(".topslider .slider_pages").children().length;
    jvSlider_currentPage--;
    if (jvSlider_currentPage < 1) {
        jvSlider_currentPage = maxcount
    }
    jvSlider_gotopage(jvSlider_currentPage);
}
var jvSliderTimeoutVar;
function jvSlider_gotopage(inpage) {
    clearTimeout(jvSliderTimeoutVar);
    jvSlider_currentPage = inpage;
    $(".topslider .slider_page .label").each(function (index) {
        $(this).hide();
    });

    $(".slider_text").each(function (index) {
        //$(this).hide();
        $(this).removeAttr('cursor-animate');
    });

    setTimeout(() => {
        $(".topslider .slider_pages").children().eq(inpage - 1).find(".label").fadeIn();
        $(`#cursor_node_${jvSlider_currentPage}`).attr('cursor-animate', '');
        reAnimateEffectiveText("");
    }, 1000);

    $(".topslider .slider_page:first").css({ "margin-right": ($(".topslider .slider_pages").width() * (inpage - 1) * -1) + "px" });

    $(".topslider .slider_buttons button").each(function (index) {
        $(this).removeClass("selected");
    });


    $(".topslider .slider_buttons").children().eq(inpage - 1).addClass("selected");

    jvSliderTimeoutVar = setTimeout(jvSlider_next, 6000);
}
function jvSlider_init() {
    var maxcount = $(".topslider .slider_pages").children().length;
    var btns = '';
    for (var i = 1; i <= maxcount; i++) {
        btns += `<button onclick="jvSlider_gotopage(` + i + `)"></button>`;
    }
    $(".topslider  .slider_buttons").html(btns);
    jvSlider_gotopage(1);
}
$(function () {
    jvSlider_init();
});


