$(document).on("click", "#tab-button>.col-6", function () {
    $("#tab-button>.col-6").removeClass("active");
    $(this).addClass("active");

    $("#tab-content>.col-12").slideUp(500);
    var elm = "#" + $(this).attr("for");
    $(elm).slideDown(500);

});
$(document).ready(function () {

    $('#counter').runCounter({
        start: 0,
        end: 1000,
        duration: 10000
    });

    $("#Mobile_Carousel").carousel({
        interval: 5000,
        wrap: true,
        pause: "hover"
    });


});

// // ------------- VARIABLES ------------- //
// var ticking = false;
// var isFirefox = (/Firefox/i.test(navigator.userAgent));
// var isIe = (/MSIE/i.test(navigator.userAgent)) || (/Trident.*rv\:11\./i.test(navigator.userAgent));
// var scrollSensitivitySetting = 30; //Increase/decrease this number to change sensitivity to trackpad gestures (up = less sensitive; down = more sensitive)
// var slideDurationSetting = 600; //Amount of time for which slide is "locked"
// var currentSlideNumber = 0;
// var totalSlideNumber = $(".background").length;
//
// // ------------- DETERMINE DELTA/SCROLL DIRECTION ------------- //
// function parallaxScroll(evt) {
//     if (isFirefox) {
//         //Set delta for Firefox
//         delta = evt.detail * (-120);
//     } else if (isIe) {
//         //Set delta for IE
//         delta = -evt.deltaY;
//     } else {
//         //Set delta for all other browsers
//         delta = evt.wheelDelta;
//     }
//
//     if (ticking != true) {
//         if (delta <= -scrollSensitivitySetting) {
//             //Down scroll
//             ticking = true;
//             if (currentSlideNumber !== totalSlideNumber - 1) {
//                 currentSlideNumber++;
//                 nextItem();
//             }
//             slideDurationTimeout(slideDurationSetting);
//         }
//         if (delta >= scrollSensitivitySetting) {
//             //Up scroll
//             ticking = true;
//             if (currentSlideNumber !== 0) {
//                 currentSlideNumber--;
//             }
//             previousItem();
//             slideDurationTimeout(slideDurationSetting);
//         }
//     }
// }
//
// // ------------- SET TIMEOUT TO TEMPORARILY "LOCK" SLIDES ------------- //
// function slideDurationTimeout(slideDuration) {
//     setTimeout(function() {
//         ticking = false;
//     }, slideDuration);
// }
//
// // ------------- ADD EVENT LISTENER ------------- //
// var mousewheelEvent = isFirefox ? "DOMMouseScroll" : "wheel";
// window.addEventListener(mousewheelEvent, _.throttle(parallaxScroll, 60), false);
//
// // ------------- SLIDE MOTION ------------- //
// function nextItem() {
//     var $previousSlide = $(".background").eq(currentSlideNumber - 1);
//     $previousSlide.removeClass("up-scroll").addClass("down-scroll");
// }
//
// function previousItem() {
//     var $currentSlide = $(".background").eq(currentSlideNumber);
//     $currentSlide.removeClass("down-scroll").addClass("up-scroll");
// }


$(document).on("click", "#tab-button>.col-4", function () {
    $("#tab-button>.col-4").removeClass("active");
    $(this).addClass("active");

    $("#tab-content>.col-12").slideUp(500);
    var elm = "#" + $(this).attr("for");
    $(elm).slideDown(500);

});
$(document).on("click", "#tab-buttonWT>.col-6", function () {
    $("#tab-buttonWT>.col-6").removeClass("active");
    $(this).addClass("active");

    $("#tab-contentWT>.col-12").slideUp(500);
    var elm = "#" + $(this).attr("for");
    $(elm).slideDown(500);

});
$(document).on("click", "#tab-buttonPMM>.col-6", function () {
    $("#tab-buttonPMM>.col-6").removeClass("active");
    $(this).addClass("active");

    $("#tab-contentPMM>.col-12").slideUp(500);
    var elm = "#" + $(this).attr("for");
    $(elm).slideDown(500);

});
$(document).on("click", "#tab-buttonLR>.col-6", function () {
    $("#tab-buttonLR>.col-6").removeClass("active");
    $(this).addClass("active");

    $("#tab-contentLR>.col-12").slideUp(500);
    var elm = "#" + $(this).attr("for");
    $(elm).slideDown(500);

});
$(document).on("click", "#CRNext", function () {
    $("#CRBox").slideUp(".1");
    $("#CTBox").slideDown(".1");
});
$(document).on("click", "#CTBack", function () {
    $("#CTBox").slideUp(".1");
    $("#CRBox").slideDown(".1");
});
$(document).on("click", "#CTNext", function () {
    $("#CTBox").slideUp(".1");
    $("#COBox").slideDown(".1");
});
$(document).on("click", "#CSNext", function () {
    $("#CSBox").slideUp(".1");
    $("#COBox").slideDown(".1");
});
$(document).on("click", "#CSBack", function () {
    $("#CSBox").slideUp(".1");
    $("#CTBox").slideDown(".1");
});
$(document).on("click", "#CONext", function () {
    $("#COBox").slideUp(".1");
    $("#CDBox").slideDown(".1");
});
$(document).on("click", "#COBack", function () {
    $("#COBox").slideUp(".1");
    $("#CTBox").slideDown(".1");
});
$(document).on("click", "#CANext", function () {
    $("#CDBox").slideUp(".1");
    $("#CABox").slideDown(".1");
});
$(document).on("click", "#CDNext", function () {
    $("#CDBox").slideUp(".1");
    $("#CABox").slideDown(".1");
});
$(document).on("click", "#CDBack", function () {
    $("#CDBox").slideUp(".1");
    $("#COBox").slideDown(".1");
});
$(document).on("click", "#CAConfirmation", function () {
    $("#CABox").slideUp(".1");
    $("#CIBox").slideDown(".1");
});
$(document).on("click", "#CABack", function () {
    $("#CABox").slideUp(".1");
    $("#CDBox").slideDown(".1");
});
$(document).on("click", "#smallMap", function () {
    $("#OMap").fadeIn(".1");
});
$(document).on("click", "#OMap", function () {
    $("#OMap").fadeOut(".1");
});
$(document).on("click", "#CINext", function () {
    $("#CIBox").slideUp(".1");
    $("#CPBox").slideDown(".1");
});
$(document).on("click", "#CIBack", function () {
    $("#CIBox").slideUp(".1");
    $("#CABox").slideDown(".1");
});
$(document).on("click", "#CPBack", function () {
    $("#CPBox").slideUp(".1");
    $("#CIBox").slideDown(".1");
});


// $(document).on(function () {
//     // $(".card-group> .bgHover").hover(function () {
//     //     $(".card-group> .bgHover").css('background-color','rgba(250,250,250)');
//     //    $(".card-group> .card-title").removeClass(".d-none");
//     // });
//     $(".card-group> .bgHover").hover(function () {
//             $(".card-group> .bgHover").css('background-color', 'rgba(250,250,250)');
//             $(".card-group> .card-title").removeClass(".d-none");
//         },
//         function () {
//             $(".card-group> .bgHover").css('background-color', 'initial');
//             $(".card-group> .card-title").addClass(".d-none");
//         });
// });


