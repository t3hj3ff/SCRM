//Center the element
$.fn.center = function () {
    this.css("position", "absolute");
    this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px");
    this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px");
    return this;
}

//blockUI
function blockUI() {
    $.blockUI({
        css: {
            backgroundColor: 'transparent',
            border: 'none'
        },
        message: '<div class= "loader" ><div class="loader__figure"></div></div>',
        baseZ: 1500,
        overlayCSS: {
            backgroundColor: '#FFFFFF',
            opacity: 0.7
        }
    });
    $('.blockUI.blockMsg').center();
}//end Blockui   