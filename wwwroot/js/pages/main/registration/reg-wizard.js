$("#photoUpload").on('change', (function (e) {
    return doPhotoUpload();
}));
function doPhotoUpload() {

    var fileInput = document.getElementById('photoUpload');
    var file = fileInput.files[0];
    var formData = new FormData();
    formData.append('photoUpload', file);

    $.ajax({
        url: '/register/upload-photo',
        type: "POST",
        data: formData,
        contentType: false,
        cache: false,
        processData: false,
        success: function (data) {
            unblockMainView();
            if (!data.IsAuthenticated) {
                //ErrorToast("Upload Error! Try again", "dvError");
                return;
            }
            if (!data.IsSuccessful) {
                var retError = !isEmpty(data.Error) ? data.Error : "Unknown error occured. Please try again later!";
                //ErrorToast(retError, "dvError");
                alert(retError);
                return;
            } else {
                $("#photoView").attr('src', '/AppDocs/TempUploads/' + data.PhotoPath);
                $("#frmPicUpload")[0].reset();
                return false;
            }
        },
        error: function (e) {
            $("#err").html(e).fadeIn();
        }
    });
}
