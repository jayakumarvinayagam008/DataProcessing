$(document).ready(function (eve) {

    $("a[name='lnkSampleB2B']").on('click', function (eve) {
        var id = $(this).attr('id');
        window.location = '/BusinessToBusiness/DownloadSampleTemplate/?sourceId=' + id;
    });
});