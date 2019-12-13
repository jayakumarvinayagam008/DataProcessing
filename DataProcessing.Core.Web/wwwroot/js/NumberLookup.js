$(document).ready(function (eve) {
    $('#searchRequestMessage, #phoneError').hide();

    $('form').submit(function () {
        var fileName = $("#files").val();
        // Check if empty of not files
        if (fileName === '' && $('#txtPhone').val() === '') {            
            $('#phoneError').show();
            return false;
        }
    });


    $("#btnNumberLookUp").on('click', function (eve) {
        var fileName = $('#hndLookupId').val();
        window.location = '/NumberLookup/DownLoadNumberLookup/?fileName=' + fileName;
    });

    $("#btnLookupDownload").on('click', function (eve) {
        var $networks = [];
        $.each($("input[name='networkProvider']:checked"), function () {
            $networks.push($(this).val());
        });        
        var $numberLookupFilter = {
            lookupId: $("#__RequestVerificationTokenSearchId").val(),
            networks : $networks
        };
        if ($networks.length === 0) {
            $('#searchRequestMessage').show();
            return 0;
        }
        $('#searchRequestMessage').hide();
        $.ajax({
            url: '/NumberLookup/DownloadNetwork/',
            type: 'POST',
            dataType: 'json',
            async: false,
            data: { numberLookupFilter: $numberLookupFilter },
            beforeSend: function (eve) {
                $("#divLoading").show();
            },
            success: function (data) {        
                var fileName = data.fileName;
                window.location = '/NumberLookup/DownLoadNumberLookup/?fileName=' + fileName;
            },
            complete: function () {
                $("#divLoading").hide();  
                $("#txtPhone").val('');
            }
        });
    });

    $("#checkAll").on('click', function (eve) {
        $("input[name=networkProvider]").prop('checked', $(this).prop('checked'));
    });

    $("input[name=networkProvider]").on('click', function (eve) {
        $totalCheckBox = $("input[name=networkProvider]").length;
        if ($("input[name=networkProvider]:checked").length === $totalCheckBox) {
            $("#checkAll").prop('checked', true);
        } else {
            $("#checkAll").prop('checked', false);
        }
    });
});


