$(window).load(function() {
    var total = $('#slider img').length;
    var rand = Math.floor(Math.random()*total);
    $('#slider').nivoSlider({
        effect:'fade',
        animSpeed:500,
        pauseTime:99995000,
        startSlide:rand,
        directionNav:true,
        controlNav:true
    });
});