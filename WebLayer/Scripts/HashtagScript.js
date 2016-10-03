/// <reference path="HashtagScript.js" />
function search(url, text, button) {
    window.history.pushState("string", "Title", url);
    $.ajax(
       {
           type: 'GET',
           url: url,
           data: { searchText: text },
           dataType: 'html',
           cache: false,
           success: function (data) {
             
               $("#body-content").html(data);
           }
       });
}







$(document).ready(function () {


    $(document).on('click', '.hashtag', function () {
        
        event.preventDefault();

        var url = "/Navigation/Search";
        var text = $(this).text();
        
        var that = $(this);
        $.fancybox.close();
        $(window).scrollTop(0);
        search(url,text, that);
        

    });

   
}
);