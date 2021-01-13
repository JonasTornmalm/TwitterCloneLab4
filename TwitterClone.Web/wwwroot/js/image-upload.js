function createCroppie() {
  $imgInput = $(".img-area").croppie({
    viewport: {
      width: 320,
      height: 320
    },
    boundary: {
      width: 420,
      height: 420
    }
  });
}
$(document).ready(function () {
  $('#upload-image-button').on('click', function () {
    $('#fileupload').click();
  });
  createCroppie();
  $('#fileupload').change(function () {
    if (this.files && this.files[0]) {
      var ext = $(this).val().split('.').pop().toLowerCase();
      if ($.inArray(ext, ['png', 'jpg', 'jpeg']) == -1) {
        alert('The uploaded file type is not supported.');
      } else {
        var reader = new FileReader();
        reader.onload = function (e) {
          $imgInput.croppie('bind', {
            url: e.target.result
          });
        };
      }
      reader.readAsDataURL(this.files[0]);
      $('.upload-area').fadeOut();
      $('.upload-text').fadeOut();
      $('.img-area').fadeIn();
      $('.reset-image').fadeIn();
    }
  });
  $('#upload-image-form').submit(function (e) {
    $imgInput.croppie('result', {
      type: 'base64',
      size: 'viewport'
    }).then(function (resp) {
      $('#upload-image-hidden').val(resp);
    });
  });
  $('.reset-image').on('click', function () {
    $('.reset-image').fadeOut();
    $('.img-area').fadeOut();
    $('.upload-area').fadeIn();
    $('.upload-text').fadeIn();
    $('#fileupload').val('');
    $imgInput.croppie('destroy');
    createCroppie();
  });
});