/// <reference path="HashtagScript.js" />
function search(text) {
    $.ajax(
       {
           type: 'GET',
           url: '/Navigation/Search',
           data: { text: text},
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

       
        var text = $(this).text();

        var that = $(this);
        $.fancybox.close();
       
        search(text, that);
        

    });

   
}
);