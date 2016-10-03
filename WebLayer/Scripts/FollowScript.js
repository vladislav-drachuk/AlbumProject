function changeFollowStatusTo(status, button)
{
    
    if (status == false)
    {
        button.data('status', 'False');
        button.children('span').text('Follow');
        button.attr("style", "");

    }
    else if (status == true)
    {
        button.data('status', 'True');
        button.children('span').text('Unfollow');
        
        button.attr("style", "background-color:greenyellow");
    }
}

/*function changeCountOfFollowers(number)
{
    $('#countOfFollowers').text(number);
}
*/


function followUser(name,button) {
    button.attr("disabled", true);
    $.ajax(
       {
           type: 'POST',
           url: '/Follow/Follow',
           data: JSON.stringify({ userName: name }),
           dataType: 'json',
           contentType: "application/json; charset=utf-8",
           cache: false,
           success: function (data) {
              
               if (data.result == true) {
                   changeFollowStatusTo(true, button);
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



function unfollowUser(name, button) {
    button.attr("disabled", true);
    $.ajax(
       {
           type: 'POST',
           url: '/Follow/Unfollow',
           data: JSON.stringify({ userName: name }),
           dataType: 'json',
           contentType: "application/json; charset=utf-8",
           cache: false,
           success: function (data) {
               if (data.result == true) {
                   changeFollowStatusTo(false, button);
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
   
    
   
    $(document).on('click', '.followButton',function () {
        var that = $(this);
        var name = that.data('username');
        
        
        if (that.data('status') == 'True')
         {
            
             unfollowUser(name, that);
            
         }
        else if (that.data('status') == 'False')
         {
             
             followUser(name, that);
             
             
         }

     }
     );
     


  });


