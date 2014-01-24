$(document).ready(function() {
    "use strict";

    <!-- HTML5 for IE8 -->
    document.createElement('header');
    document.createElement('nav');
    document.createElement('section');
    document.createElement('article');
    document.createElement('aside');
    document.createElement('footer');

    <!-- jQuery Brower Rejection -->
    $.reject();

    <!-- Boostrap Carousel -->
    $('.carousel').carousel();

    $('.box').hover(function () {
        $(this).find('.read').fadeToggle(100);
    });
});

$(window).load(function(){
    "use strict";

    <!-- jQuery Black and White Images -->
    $('.bwimg').BlackAndWhite({
        responsive:true,
        speed: {
            fadeIn: 200,
            fadeOut: 200
        }
    });
});