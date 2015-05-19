"use strict";

var timeOut;
var timer;

jQuery(function () {
    $.get("/Home/GetSlideImages", null, function (data) {
        var slideInterval = 5000;
        $.supersized({
            // Functionality
            slideshow: 1,
            autoplay: 1,
            start_slide: 1,
            stop_loop: 0,
            random: 0,
            slide_interval: slideInterval,
            transition: 6,
            transition_speed: 1000,
            new_window: 1,
            pause_hover: 0,
            keyboard_nav: 1,
            performance: 1,
            image_protect: 1,

            // Size & Position
            min_width: 0,
            min_height: 0,
            vertical_center: 1,
            horizontal_center: 1,
            fit_always: 0,
            fit_portrait: 1,
            fit_landscape: 0,

            // Components
            slide_links: 'blank',
            thumb_links: 1,
            thumbnail_navigation: 0,
            slides: data,

            // Theme Options
            progress_bar: 1,
            mouse_scrub: 0
        });

        timeOut = 2000 + data.length * slideInterval;
        timer = setTimeout(function () {
            window.clearTimeout(timer);
            goToMainPage();            
        }, timeOut);
    });
});

function goToMainPage() {
    window.location.href = '/home/main';
}
