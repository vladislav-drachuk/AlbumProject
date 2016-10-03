function changeAdminStatusTo(status, button) {

    if (status == false) {
        button.data('status', 'False');

        button.children('span').text('Add Admin Rights');

    }
    else if (status == true) {
        button.data('status', 'True');

        button.children('span').text('Remove Admin Rights');

    }
}

/*function changeCountOfFollowers(number)
{
    $('#countOfFollowers').text(number);
}
*/


function addAdminStatus(name, button) {
    button.attr("disabled", true);
    $.ajax(
       {
           type: 'POST',
           url: '/Admin/AddAdminStatus',
           data: JSON.stringify({ userName: name }),
           dataType: 'json',
           contentType: "application/json; charset=utf-8",
           cache: false,
           success: function (data) {
               changeAdminStatusTo(true, button);


           },
           complete: function () {
               button.attr("disabled", false);
           }
       });

}



function removeAdminStatus(name, button) {
    button.attr("disabled", true);
    $.ajax(
       {
           type: 'POST',
           url: '/Admin/RemoveAdminStatus',
           data: JSON.stringify({ userName: name }),
           dataType: 'json',
           contentType: "application/json; charset=utf-8",
           cache: false,
           success: function (data) {

               changeAdminStatusTo(false, button);

           },
           complete: function () {

               button.attr("disabled", false);
           }
       });

}




$(document).ready(function () {
   
    
    $('.addToAdminButton').click(function () {
       
        event.preventDefault();
        var that = $(this);
        var name = that.data('username');

        if (that.data('status') == 'True') {

            removeAdminStatus(name, that);
        }
        else if (that.data('status') == 'False') {

            addAdminStatus(name, that);
        }

    });



});