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
var jvSliderTextEffect = setInterval(() => { }, 500);
var jvSliderTextEffectCounter = 0;
var jvSliderTextEffectToggle = false;
function jvSlider_gotopage(inpage) {
    clearInterval(jvSliderTextEffect);
    jvSliderTextEffectCounter = 0;
    $(`#cursor_node_${jvSlider_currentPage}`).html("");

    clearTimeout(jvSliderTimeoutVar);
    jvSlider_currentPage = inpage;
    $(".topslider .slider_page .label").each(function (index) {
        $(this).hide();
    });

    $(".slider_text").each(function (index) {
        //$(this).hide();
        //$(this).removeAttr('cursor-animate');
    });

    setTimeout(() => {
        $(".topslider .slider_pages").children().eq(inpage - 1).find(".label").fadeIn();
        //$(`#cursor_node_${jvSlider_currentPage}`).attr('cursor-animate', '');
        //reAnimateEffectiveText("");
    }, 1000);

    $(".topslider .slider_page:first").css({ "margin-right": ($(".topslider .slider_pages").width() * (inpage - 1) * -1) + "px" });

    $(".topslider .slider_buttons button").each(function (index) {
        $(this).removeClass("selected");
    });


    $(".topslider .slider_buttons").children().eq(inpage - 1).addClass("selected");

    jvSliderTimeoutVar = setTimeout(jvSlider_next, 8000);


    $(`#cursor_node_${jvSlider_currentPage}`).html("");
    setTimeout(() => {
        jvSliderTextEffect = setInterval(() => {
            let selcur = `#cursor_node_${jvSlider_currentPage}`;
            let text1 = $(selcur).attr("slider_text");
            let text2 = $(selcur).attr("slider_text2");
            let text2Color = $(selcur).attr("slider_text2_color");
            if (jvSliderTextEffectCounter <= text1.length) {
                $(selcur).html(text1.substring(0, jvSliderTextEffectCounter) + "|");
            } else if (jvSliderTextEffectCounter <= text1.length + text2.length) {
                $(selcur).html(text1 + `<span style='color:${text2Color};'>` + text2.substring(0, jvSliderTextEffectCounter - text1.length) + "|</span>");
            } else {
                if (jvSliderTextEffectCounter % 6 == 0) { jvSliderTextEffectToggle = !jvSliderTextEffectToggle };
                $(selcur).html(text1 + `<span style='color:${text2Color};'>` + text2+"</span>" + (jvSliderTextEffectToggle ? "":"|"));
            }
            jvSliderTextEffectCounter++;
        }, 80);
    }, 1000);
}
function jvSlider_init() {
    var maxcount = $(".topslider .slider_pages").children().length;
    var btns = '';
    for (var i = 1; i <= maxcount; i++) {
        btns += `<button name="slide_` + i + `" onclick="jvSlider_gotopage(` + i + `)"></button>`;
    }
    $(".topslider  .slider_buttons").html(btns);
    jvSlider_gotopage(1);
}
$(function () {
    jvSlider_init();
});






///////sec  5



var countOfRateSlider = 3;
jvRateSlider_currentPage = 1;
function jvRateSlider_next() {
    var maxcount = ($(".sslider .slider_pages").children().length) / 3;
    if (jvRateSlider_currentPage < maxcount) {
        jvRateSlider_currentPage++;
    } else {
        jvRateSlider_currentPage = 1;
    }
    jvRateSlider_gotopage(jvRateSlider_currentPage);
}
var jvRateSliderTimeoutVar;
function jvRateSlider_gotopage(inpage) {
    clearTimeout(jvRateSliderTimeoutVar);
    jvRateSlider_currentPage = inpage;
    $(".sslider .slider_page .label").each(function (index) {
        $(this).hide();
    });
    setTimeout(() => { $(".sslider .slider_pages").children().eq(inpage - 1).find(".label").fadeIn(); }, 1000);

    $(".sslider .slider_page:first").css({ "margin-right": (320 * (inpage - 1) * -1) + "px" });

    $(".sslider .slider_buttons button").each(function (index) {
        $(this).removeClass("selected");
    });


    $(".sslider .slider_buttons").children().eq(inpage - 1).addClass("selected");

    jvRateSliderTimeoutVar = setTimeout(jvRateSlider_next, 5000);
}
function jvRateSlider_init() {
    var maxcount = $(".sslider .slider_pages").children().length;
    var btns = '';
    for (var i = 1; i <= maxcount; i++) {
        btns += `<button onclick="jvRateSlider_gotopage(` + i + `)"></button>`;
    }
    $(".sslider  .slider_buttons").html(btns);
    jvRateSlider_gotopage(1);
}
$(function () {
    jvRateSlider_init();
});
