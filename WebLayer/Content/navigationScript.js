function updateNav(url)
{
 
    $.ajax({
        url: url,
        type: "GET",
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
       
        updateNav(url);

    });
});