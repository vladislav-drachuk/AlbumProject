$(document).ready(function () {
    $(document).on('mouseenter', '.fancybox', function () {
        $('.fancybox').fancybox(
            {
                autoSize: true,
                scrolling: 'no',
                fitToView: false, // set the specific size without scaling to the view port
                minWidth: 200, // or whatever, default is 100
                minHeight: 300, // default 100
                maxWidth: 700, // default 9999
                maxHeight: 700  // default 9999

            });
    });

});