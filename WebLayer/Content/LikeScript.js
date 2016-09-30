function changeLikeCount(count,button)
{

    button.prev('.countOfLikes').text(count);

}
function changeLikeStatusTo(isLiked,button)
{
    if(isLiked==true)
    {
        button.attr('style', 'color:#E84062');
        button.data('status', 'True');
        button.children('.likeText').text('Unlike');
        
        

    }
    else if (isLiked==false)
    {
        button.attr('style', 'color:#FBACBC');
        button.data('status', 'False');
        button.children('.likeText').text('Like');
       
    }
    
}


function likeImage(imgId,button) {
    button.attr("disabled", true);
    $.ajax(
       {
           type: 'POST',
           url: '/Like/LikeImage',
           data: JSON.stringify({ imageId: imgId }),
           dataType: 'json',
           contentType: "application/json; charset=utf-8",
           cache: false,
           success: function (data) {
               if (data.result == true) {

                   changeLikeCount(data.countOfLikes, button);
                   changeLikeStatusTo(true, button);
               }
               else {
                   alert(data.message);
               }
           },
           complete: function () {
               button.attr("disabled", false);
           }
       });
}

function unlikeImage(imgId, button) {
    button.attr("disabled", true);
    $.ajax(
       {
           type: 'POST',
           url: '/Like/UnlikeImage',
           data: JSON.stringify({ imageId: imgId }),
           dataType: 'json',
           contentType: "application/json; charset=utf-8",
           cache: false,
           success: function (data) {
               if (data.result == true) {

                   changeLikeCount(data.countOfLikes, button);
                   changeLikeStatusTo(false, button);
               }
               else {
                   alert(data.message);
               }
           },
           complete: function () {
               button.attr("disabled", false);
           }
           });
}





$(document).ready(function () {
    
   
    $(document).on('click','.likeButton',function () {
        
        event.preventDefault();
        var id = $(this).data('imageid');
        
        var that = $(this);
        
        if ($(this).data('status') == 'False') {
           
            likeImage(id,that);
        }
        else if ($(this).data('status') == 'True') {
           
            unlikeImage(id, that);
        }    
        
    });
  }
);