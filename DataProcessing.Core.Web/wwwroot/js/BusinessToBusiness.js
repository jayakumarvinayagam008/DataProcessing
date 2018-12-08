$(document).ready(function (eve) {
    $('#btnBusinessToBusinessSearch').on('click', function (eve) {
        alert(1)
        var cities = $('#b2bCity').val();
        var states = $('#b2bState').val();
        var countries = $('#b2bCountry').val();
        var area = $('#b2bArea').val();
        var destinations = $('#b2bDesigination').val();
        var businessCategory = $('#b2bCategory').val();
        if (cities !== null
            || states !== null
            || countries !== null
            || area !== null
            || destinations !== null
            || businessCategory !== null
            || tags !== null) {

            var b2bSearch = {
                'Cities': cities,
                'Contries': countries,
                'States': states,
                'Area': area,
                'Designation': destinations,
                'BusinessCategoryId': businessCategory
            };

            $.ajax({
                url: '/BusinessToBusiness/Search/',
                type: 'post',
                dataType: 'json',
                data: { searchRequest: b2bSearch },
                beforeSend: function (eve) {
                    $("#divLoading").show();
                },
                success: function (data) {
                    UpdateB2BDashBoard(data);
                },
                complete: function () {
                    $("#divLoading").hide();
                }
            });
        } else {
            alert('selectedCities');
        }
    });
});

function UpdateB2BDashBoard(data) {

}