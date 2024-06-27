


$(function () {
    setTimeout(() => {
        $("#loading_container_j").fadeOut();


    }, 300);
    
});


function showMessageBox(title, text) {
    $('#message_main').fadeIn(300);
    $("#message_title").html(title);
    $("#message_text").html(text);
    $("#ontop_div_mess").show();
    $("#ontop_div_login").hide();
}


toEnNumber = function (strin) {
    var charMap = {
        "۰": "0",
        "۱": "1",
        "۲": "2",
        "۳": "3",
        "۴": "4",
        "۵": "5",
        "۶": "6",
        "٤": "4",
        "٥": "5",
        "٦": "6",
        "۷": "7",
        "۸": "8",
        "۹": "9",
    };
    for (var first in charMap) {
        var sec = charMap[first];
        strin = strin.split(first).join(sec);
    }
    return strin;

};


$(function () {
    $("input[ctype='numonly']").on('input', function (e) {
        var ss = toEnNumber($(this).val());
        $(this).val(ss.replace(/[^0-9]/g, ''));
    });
});


$(function () {
    var s = "";
    $(".jdropdown div .functiondiv").each(function (i) {
        s += "<div>" + $(this).html() + "</div>";
    });
    $("#sideslider_panel").html(s);
});

var sideSliderShown = false;
function toggleSideSlider() {
    if (sideSliderShown) {
        $("#sideslider_main").fadeOut();
        $("#alert").fadeOut();
        $("#sideslider_panel").animate({ 'marginRight': '-80%' }, 1000);
    }
    else {
        $("#sideslider_main").fadeIn();
        $("#alert").fadeIn();
        $("#sideslider_panel").animate({ 'marginRight': '0' }, 500);
    }
    sideSliderShown = !sideSliderShown;
}
window.onscroll = function (ev) {
    window.scrollY

    $(".loadnavi").click();
};


gotouplock = 0;
window.onscroll = function (ev) {
    if (gotouplock == 1) return;
    gotouplock = 1;
    if (window.scrollY < 70) {
        $("#gotoup_button").fadeOut(complete = () => {
            gotouplock = 0;
        });


        /*if ($("header.header").hasClass("header_on_top")) {
            $("header.header").hide();
            $("header.header").fadeIn();
            $("header.header").removeClass("header_on_top");
        }*/
    } else {
        $("#gotoup_button").fadeIn(complete = () => {
            gotouplock = 0;
        });
        /*if (!$("header.header").hasClass("header_on_top")) {
            $("header.header").hide();
            $("header.header").fadeIn();
            $("header.header").addClass("header_on_top");
        }*/
    }
}

