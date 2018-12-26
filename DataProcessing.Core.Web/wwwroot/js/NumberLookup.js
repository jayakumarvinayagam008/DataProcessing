$(document).ready(function (eve) {
    $("#btnNumberLookUp").on('click', function (eve) {
        var fileName = $('#hndLookupId').val();
        window.location = '/NumberLookup/DownLoadNumberLookup/?fileName=' + fileName;
    });
});