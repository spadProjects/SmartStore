'use strict';

// navbar dropdown on click_____ getting the screen width
function top_menu_event_handler() {
    $(document).on("click", "#mainMenu .dropdown-toggle", function () {
        var innerwidth = $(window).innerWidth();
        if (innerwidth > 992) {
            return false;
        } else {
            return true;
        }
    });
}

$(document).ready(function () {
    //page loader
    $('.loader').delay(900).queue(function () {
        $(this).remove();
    });
    //disable the navbar dropdown click event on large screen
    top_menu_event_handler();
    $(window).on("resize", function () {
        top_menu_event_handler();
    });

    // navbar dropdown shows on click on mobiles and tablets
    $(document).on("click", ".dropdown-menu a.dropdown-toggle", function (e) {
        $(this).next(".dropdown-menu").toggleClass('show');
        return false;
    });

    // navbar actions (login , cart) dropdown
    $(document).on("click", '.dropdown-link', function () {
        // fetch the target - trigger
        var target = $(this).attr('data-target');
        // state of the target (false: hide)
        var state = $('#' + target).attr('data-collapse');

        // hide all targets
        $('.dropdown-wrapper').attr('data-collapse', 'false').hide();

        if (state == 'false') { // trigger target is not shown - normal

            // show target menu
            $('#mainNavbarDropdown #' + target).attr('data-collapse', 'true').slideDown(120);
        }
    });

    // close actions on click the body
    $('body').on("click", function (evt) {
        var target = $(evt.target);
        if (!target.parents('.dropdown-wrapper').length && !target.parents('.main-navbar-action__btn').length && !target.parents('.bs-canvas-left').length) {
            $('.dropdown-wrapper').attr('data-collapse', 'false').hide();
            $(document).on("click", ".bs-canvas-close", function () {

            });
        }
    });


    // initializing the nice select plugin
    $('select').niceSelect();

    // select product color checkbox handler
    $(document).on("change", ".widget__body input[type=checkbox]", function () {
        var el = $(this).closest(".color-list__item").find('label');
        if (this.checked) {
            el.addClass('active');
        } else {
            el.removeClass('active');
        }
    });

    // add to wish list button
    $(document).on("click", ".wishlist__icon", function () {
        $(this).toggleClass('icon-heart-fill wishlist-checked');
    });

    // collapsed nav filter options
    $(document).on("click", ".bs-canvas-close, .bs-canvas-overlay", function () {
        var elm = $(this).hasClass('bs-canvas-close') ? $(this).closest('.bs-canvas') : $('.bs-canvas');
        elm.removeClass('mr-0 ml-0');
        $('.bs-canvas-overlay').remove();
        $('body').css('overflow', '');
        return false;
    });
    $(document).on("click", ".pull-bs-canvas-left", function () {
        $('body').prepend('<div class="bs-canvas-overlay bg-dark position-fixed w-100 h-100"></div>');
        $('body').css('overflow', 'hidden');
        $('.bs-canvas-right').addClass('mr-0');
        return false;
    });

    // price range slider plugin
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 2000,
        values: [0, 1000],
        slide: function (event, ui) {
            $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
        }
    });
    $("#amount").val("$" + $("#slider-range").slider("values", 0) +
        " - $" + $("#slider-range").slider("values", 1));

    // single product Read more button
    $(document).on("click", ".readmore-btn", function () {
        var elm = $(".product-details__content--desc p");

        if (elm.hasClass("notifopen")) {
            elm.animate({
                height: "113px"
            }, 500);
            elm.removeClass("notifopen");
            $('.up-down', this).removeClass('fa-angle-up').addClass('fa-angle-down');
        } else {
            elm.addClass("notifopen");
            elm.animate({
                height: document.getElementById(elm.attr("id")).scrollHeight + 'px'
            }, 500)
            $('.up-down', this).removeClass('fa-angle-down').addClass('fa-angle-up');
        }
    });

    // item shop quantity select box
    $(document).on("click", ".quantity__minus", function () {
        var $input = $(this).parent().find('input');
        var count = parseInt($input.val()) - 1;
        count = count < 1 ? 1 : count;
        $input.val(count);
        $input.change();
        return false;
    });
    $(document).on("click", ".quantity__plus", function () {
        var $input = $(this).parent().find('input');
        $input.val(parseInt($input.val()) + 1);
        $input.change();
        return false;
    });

    // share button social media list
    $(document).on('click', '.share-social', function () {
        var show = $(this).attr('data-show');

        if (show == 'false') {
            $(this).closest(".product-info__act").find('.social-item').removeClass('hideme').animate({"width": '45px'}, 300);
            $(this).attr('data-show', 'true');
        } else {
            $(this).closest(".product-info__act").find('.social-item').animate({"width": '0px'}, 300, function () {
                $(this).closest(".product-info__act").find('.social-item').addClass('hideme');
            });
            $(this).attr('data-show', 'false');
        }
    });

    // show and hide password form input
    $(document).on('click', '.showhidepassword', function () {
        var el = $(this).closest(".password-box").find('input');
        if (el.attr('type') == 'password') {
            el.attr('type', 'text');
            $('i', this).removeClass('fa-eye-slash').addClass('fa-eye');
        } else {
            el.attr('type', 'password');
            $('i', this).removeClass('fa-eye').addClass('fa-eye-slash');
        }
    });

    // home page smart shop buttons
    $(document).on('click', '.smart-shop__btn .btn', function () {
        var target_box = $(this).closest(".smart-shop__btn").find('.smart-shop__btn--details');
        if (target_box.hasClass("hide")) {
            target_box.removeClass('hide');
        } else {
            target_box.addClass('hide');
        }
    });

    // owl carousel default settings
    var settings = {
        loop: true,
        dots: false,
        nav: true,
        navText: ["<i class='fas fa-chevron-left'></i>", "<i class='fas fa-chevron-right'></i>"],
        margin: 30,
        responsiveClass: true,
    };

    // home page owl carousel product card
    $('#itemSliderBlue').owlCarousel(jQuery.extend({}, settings, {
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 1,

            },
            1000: {
                items: 2,
            }
        }
    }));

    // home page owl carousel product light card
    $('#itemSliderLight').owlCarousel(jQuery.extend({}, settings, {
        responsive: {
            0: {
                items: 1,
            },
            768: {
                items: 2,
            },
            992: {
                items: 3,

            },
            1200: {
                items: 4,
            }
        }
    }));

    // owl carousel client
    $('#brandSlider').owlCarousel(jQuery.extend({}, settings, {
        nav: false,
        loop: false,
        responsive: {
            0: {
                items: 2,
            },
            600: {
                items: 3,
            },
            1000: {
                items: 5,
            }
        }
    }));

    // owl carousel team
    $('#teamCarousel').owlCarousel(jQuery.extend({}, settings, {
        nav: true,
        loop: true,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 2,
            },
            1000: {
                items: 4,
            }
        }
    }));

    // owl carousel team
    $('#blogPostList').owlCarousel(jQuery.extend({}, settings, {
        nav: false,
        loop: true,
        dots: true,
        responsive: {
            0: {
                items: 1,
            },
            768: {
                items: 1,
            },
            1000: {
                items: 2,
            }
        }
    }));

    // single product gallery
    if ($(".gallery-on-top").length > 0) {
        // initialize zoom plugin on product image
        $('.gallery-on-top img')
            .wrap('<span style="display:block"></span>')
            .css('display', 'block')
            .parent()
            .zoom({
                url: $('.gallery-on-top img').attr('src'),
                callback: function () {
                }
            });

        // dynamic image handler for item's photo
        $(document).on('click', '.gallery-thumbs .item', function () {
            var target = $(this).find('img').attr('data-target');

            // change main picture
            var image = $('.gallery-on-top img')

            image.attr('src', target);
            // change zoom picture by new one
            image.parent().zoom({url: target});
        });
    }

    // owl carousel single product image gallery
    $('.gallery-thumbs').owlCarousel(jQuery.extend({}, settings, {
        margin: 10,
        loop: false,
        responsive: {
            0: {
                items: 2,
            },
            600: {
                items: 2,
            },
            1000: {
                items: 4,
            }
        }
    }));

    // home page demo 02 owl carousel hero header
    $('#heroHeaderSlider').owlCarousel(jQuery.extend({}, settings, {
        dots: true,
        animateIn: 'fadeIn',
        animateOut: 'fadeOut',
        margin: 0,
        responsive: {
            0: {
                items: 1,
                nav: false,
            },
            600: {
                items: 1,
                nav: true,
            },
            1000: {
                items: 1,
                nav: true,
            }
        }
    }));

    //  home page owl carousel testimonial
    $('#testimonial').owlCarousel(jQuery.extend({}, settings, {
        nav: false,
        dots: true,
        margin: 30,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 1,

            },
            1000: {
                items: 1,
            }
        }
    }));

    // shop page owl carousel
    $('#shopListSlider').owlCarousel(jQuery.extend({}, settings, {
        dots: true,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        margin: 0,
        item: 1,
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    }));

    // scroll up button
    var btn = $('#back-to-top');
    $(window).scroll(function () {
        if ($(window).scrollTop() > 300) {
            btn.addClass('show');
        } else {
            btn.removeClass('show');
        }
    });

    btn.on('click', function (e) {
        e.preventDefault();
        $('html, body').animate({scrollTop: 0}, '500');
    });

    // video player____play/pause on click
    var mainvideoplaylist = [];
    $('.video-wrapper video, .video-wrapper button').on("click", function () {
        var vidid = $(this).attr('data-vid');

        if ($(this).is("button")) {
            var btn_el = $(this);
            var video_el = $(this).closest(".video-wrapper").find('video');
        } else {
            var btn_el = $(this).closest(".video-wrapper").find('button');
            var video_el = $(this);
        }

        if ($.inArray(vidid, mainvideoplaylist) < 0) {
            mainvideoplaylist.push(vidid);
            document.getElementById(vidid).play();
            video_el.prop("controls", true);
            btn_el.fadeOut();

        } else {
            mainvideoplaylist.splice($.inArray(vidid, mainvideoplaylist), 1);
            document.getElementById(vidid).pause();
            video_el.prop("controls", false);
            btn_el.fadeIn();
        }
    });

    // compare page responsive table
    if ($("#responsivetbl").length > 0) {

        var first_row = $('#responsivetbl tbody tr:first-child td');
        var default_col = 1;
        $(first_row).each(function (index) {
            if ($(this).hasClass('default')) default_col = index;
        });

        $('#mobileimgplaceholder div').html('<img class="img-fluid" src="' + $('thead th:nth-child(' + (default_col + 1) + ') .card-item__bg img').attr('src') + '">');
        $('#mobileimgplaceholder h2').html($('thead th:nth-child(' + (default_col + 1) + ') .card-item__body--title h4').html());

        $(document).on("click", "#responsivetbl li", function () {
            var pos = $(this).index() + 2;

            $('#mobileimgplaceholder div').html('<img class="img-fluid" src="' + $('thead th:nth-child(' + pos + ') .card-item__bg img').attr('src') + '">');
            $('#mobileimgplaceholder h2').html($('thead th:nth-child(' + pos + ') .card-item__body--title h4').html());
            $("tr").find('td:not(:eq(0))').hide();
            $('td:nth-child(' + pos + ')').css('display', 'table-cell');
            $("tr").find('th:not(:eq(0))').hide();
            $('li').removeClass('active');
            $(this).addClass('active');
        });
    }

    // coming soon page countdown plugin
    $(function () {
        $('#countdown').countdown({
            year: 2021, // YYYY Format
            month: 10, // 1-12
            day: 5, // 1-31
            hour: 0, // 24 hour format 0-23
            minute: 0, // 0-59
            second: 0, // 0-59
        });
    });

    // account tabs url
    if ($("#v-pills-tab.account-tabs").length > 0) {
        var hash = location.hash.replace(/^#/, '');
        hash += '-tab';
        if (hash.length > 0 && $('.account-tabs #' + hash + '.nav-link').length > 0) {
            $('#' + hash).tab('show');
        }
    }

    // navbar search box
    $(document).on("click", ".search-wrapper__btn", function () {
        $('.search-wrapper__box').fadeToggle();
    });

    // search form typeahead
    var substringMatcher = function (strs) {
        return function findMatches(q, cb) {
            var matches, substringRegex;

            // an array that will be populated with substring matches
            matches = [];

            // regex used to determine if a string contains the substring `q`
            var substrRegex = new RegExp(q, 'i');

            // iterate through the pool of strings and for any string that
            // contains the substring `q`, add it to the `matches` array
            $.each(strs, function (i, str) {
                if (substrRegex.test(str)) {
                    matches.push(str);
                }
            });

            cb(matches);
        };
    };

    var items = ['Armchair', 'Sofa', 'Couch', 'Dining chair', 'Chair',
        'Dining table', 'Table', 'Desk', 'Coffee table', 'Mattress', 'Lightning',
        'Rugs', 'Textiles', 'Office desk', 'Bed', 'Cushions', 'Kitchen', 'Tv & media',
        'Side tables', 'Room dividers', 'Wardrobes', 'Mirrors', 'Candles',
        'Vases & bowls', 'Frames', 'Clocks', 'Paper shop', 'Accessories', 'Kitchen services',
        'Desk chairs', 'Drawer', 'Storage cabinets', 'Plants'
    ];

    $('#the-basics .typeahead').typeahead({
            hint: true,
            highlight: true,
            minLength: 1
        },
        {
            name: 'items',
            source: substringMatcher(items)
        });

    //wow animation
    var wow = new WOW(
        {
            boxClass: 'wow',
            animateClass: 'animated',
            offset: 90,
            mobile: true,
            live: true
        }
    )
    wow.init();
});






