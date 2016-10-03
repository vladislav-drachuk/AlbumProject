function updateFav(url)
{
    $('#load-gif').show();
    $.ajax({
        url: url,
        type: "GET",
        dataType: "html"
     
       
    })
    .done(function (partialViewResult) {
       
        $('#load-gif').hide();
        $("#body-content").html(partialViewResult);
    });
}

function updateSearch(url, text) {

    $.ajax({
        url: url,
        type: "GET",
        data: {searchText: text},
        dataType: "html"


    })
    .done(function (partialViewResult) {


        $("#body-content").html(partialViewResult);
    });
}

$(document).ready(function () {
    $(document).on('click', '#navigation', function () {
        event.preventDefault();
        that = $(this);
       
        var url = that.attr('href');
        window.history.pushState("string", "Title", url);
        updateFav(url);

    });

    $(document).on('click', '#searchButton', function () {
        event.preventDefault();
        that = $(this);
        var url = that.attr('href');
        var text = $('#searchText').val();
       
        updateSearch(url, text);

    });
});