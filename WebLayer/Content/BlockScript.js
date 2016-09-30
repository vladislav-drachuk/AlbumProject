function changeBlockStatusTo(status, button) {

    if (status == false) {
        button.data('status', 'False');
       
        button.children('span').text('Block');

    }
    else if (status == true) {
        button.data('status', 'True');
      
        button.children('span').text('Unblock');

    }
}

/*function changeCountOfFollowers(number)
{
    $('#countOfFollowers').text(number);
}
*/


function blockUser(name, button) {
    button.attr("disabled", true);
    $.ajax(
       {
           type: 'POST',
           url: '/Admin/Block',
           data: JSON.stringify({ userName: name }),
           dataType: 'json',
           contentType: "application/json; charset=utf-8",
           cache: false,
           success: function (data) {
               changeBlockStatusTo(true, button);


           },
           complete: function () {
                  button.attr("disabled", false);
           }
       });

}



function unblockUser(name, button) {
    button.attr("disabled", true);
    $.ajax(
       {
           type: 'POST',
           url: '/Admin/Unblock',
           data: JSON.stringify({ userName: name }),
           dataType: 'json',
           contentType: "application/json; charset=utf-8",
           cache: false,
           success: function (data) {
               
               changeBlockStatusTo(false, button);

           },
           complete: function () {
              
               button.attr("disabled", false);
           }
       });

}




$(document).ready(function () {

    
    $('.blockButton').click(function () {
      
        event.preventDefault();
        var that = $(this);
        var name = that.data('username');
        
        if (that.data('status') == 'True') {

            unblockUser(name, that);
        }
        else if (that.data('status') == 'False') {

            blockUser(name, that);
        }

    });



});