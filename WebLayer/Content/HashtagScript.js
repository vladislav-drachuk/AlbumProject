/// <reference path="HashtagScript.js" />
function search(text, button) {
    
    $.ajax(
       {
           type: 'GET',
           url: '/Navigation/Search',
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

       
        var text = $(this).text();
        
        var that = $(this);
        $.fancybox.close();
       
        search(text, that);
        

    });

   
}
);