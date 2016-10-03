function loadUserImages(username, button) {
    button.attr("disabled", true);
    var showButton = true;
    $.ajax(
       {
           type: 'GET',
           url: '/User/Images',
           data: { userName: username, loadMore: true },
           dataType: 'json',
           contentType: "application/json; charset=utf-8",
           cache: false,
           success: function (data) {
               if (data.isEnd == true) {
                   showButton = false;
               }
              
              var html = $.parseHTML(data.partialView);
              $('#more-div').replaceWith(data.partialView);
           },
           complete: function () {
               if(showButton===false)
               {
                   $('#more').remove();
               }
           }
       });
}

function loadFavImages(button) {

    button.attr("disabled", true);
    var showButton = true;
    $.ajax(
       {
           type: 'GET',
           url: '/Navigation/Favorites',
           data: { loadMore: true },
           dataType: "html",
           cache: false
          })
           .done(function (partialViewResult) {
               var html = $.parseHTML(partialViewResult);
               $('#more-div').replaceWith(html);
               });
     
}

function loadSearchImages(button, searchText) {

    button.attr("disabled", true);
    var showButton = true;
    $.ajax(
       {
           type: 'GET',
           url: '/Navigation/Search',
           data: { searchText: searchText,loadMore: true },
           dataType: "html",
           cache: false
       })
           .done(function (partialViewResult) {
               var html = $.parseHTML(partialViewResult);
               $('#more-div').replaceWith(html);
           });

}




$(document).ready(function () {


    $(document).on('click','#more',function () {
        if ($(this).data("from") == "favorites") {
            var that = $(this);
            
            loadFavImages(that)
        }
        else if ($(this).data("from") == "search") {
            var key = $(this).data("key");
            
            var that = $(this);

            loadSearchImages(that, key)
        }
        else {
           
            var username = $('#profile-content').data('username');
            var that = $(this);
            loadUserImages(username, that);
        }

    });
}
);