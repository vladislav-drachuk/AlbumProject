function upload(formData, username)
{
    $.ajax({
        type: "POST",
        url: '/Image/Upload',
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.result == true) {
                update(username);
            }
            else {
                alert(result.message);
            }
        },
        error: function (error) {
            alert("errror");
        }
    });

}

function delete_image(imageid, button) {
    
    $.ajax({
        type: "POST",
        url: '/Image/Delete',
        data: JSON.stringify({imageId: imageid}),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        cache: false,
        success: function (msg) {
            var username = button.data('username');
            $.fancybox.close();
            update(username);

        },
        error: function (error) {
            alert("errror");
        }
    });

}

function update(userName)
{
    $.ajax({
        url: "/User/Images",
        type: "GET",
        data: { userName: userName }
    })
    .done(function (partialViewResult) {
      
        
        $("#profile-content").html(partialViewResult);
});
}

$(document).ready(function () {
    $(document).on('click','#uploadSubmit',function () {
       
        event.preventDefault();
        var username = $(this).data("username");
        
        var description = $("#imageText").val();
        var type = "galery";
        if ($('#check_id').is(":checked")) {
            type = "profile";
        }
        alert(type);
        var formData = new FormData();
        var totalFiles = document.getElementById("imageInput").files.length;

        var file = document.getElementById("imageInput").files[0];
        formData.append("FileUpload", file);
        formData.append("description", description);
        formData.append("type", type);
        upload(formData, username);
        $("#uploader").trigger('reset');
        $("#imageInput").val("");

        
        

    });

    $(document).on('click', '.delete', function () {

        event.preventDefault();
        that = $(this);
        var imageid = $(this).data("imageid");
        
        delete_image(imageid, that);
        




    });
 

});

