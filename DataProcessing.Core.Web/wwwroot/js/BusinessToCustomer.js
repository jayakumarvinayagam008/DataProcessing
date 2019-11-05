$(document).ready(function (eve) {
    //$("#divLoading").show();

    $('#btnBusinessToCustomerSearch').on('click', function (eve) {
        var countries = $('#b2cCountry').val();
        var cities = $('#b2cCity').val();
        var roles = $('#b2cRole').val();
        var ages = $('#b2cAge').val();

        var states = $('#b2cState').val();
        var area = $('#b2cArea').val();
        var salary = $('#b2cSalary').val();
        var experience = $('#b2cExperience').val();

        var tags = $('#customerTags').val();
        if (cities !== null
            || countries !== null
            || cities !== null
            || roles !== null
            || ages !== null
            || states !== null
            || area !== null
            || salary !== null
            || experience !== null) {

            var customerSearch = {
                'Area' : area,
                'Cities': cities,
                'Contries': countries,
                'States': states,
                'Age': ages,
                'Experience': experience,
                'Roles': roles,
                'Salary': salary           
            };
            console.log(customerSearch);

            $.ajax({
                url: '/BusinessToCustomer/Search/',
                type: 'post',
                dataType: 'json',
                data: { searchRequest: customerSearch },
                beforeSend: function (eve) {
                    $("#divLoading").show();
                },
                success: function (data) {
                    UpdateCustomerDashBoard(data);
                    $('#divBlock').show();
                },
                complete: function () {
                    $("#divLoading").hide();
                }
            });
        } else {
            alert('selectedCities');
        }
    });

    //IsAvailable
    //Message
    $('#excelDown').on('click', function (eve) {
        $('#searchRequestMessage').hide();
        var $fileName = $('#excelDown').attr('title');
        var $fileStatus = FileCheck($fileName, 'xlsx');
        if ($fileStatus.isAvailable === true) {
            DownloadFile($fileName, 'xlsx');
        } else {
            $('#searchRequestMessage').show();
            $('#searchRequestMessage').html($fileStatus.message);
        }

    });

    $('#csvDown').on('click', function (eve) {
        $('#searchRequestMessage').hide();
        var $fileName = $('#csvDown').attr('title');
        var $fileStatus = FileCheck($fileName, 'csv');
        if ($fileStatus.isAvailable === true) {
            DownloadFile($fileName, 'csv');
        } else {
            $('#searchRequestMessage').show();
            $('#searchRequestMessage').html($fileStatus.message);
        }
    });

    $(window).on('load', function () {
        $("#divLoading").hide();
    });
});

function FileCheck($searchId, $type) {
    var $searchRequestCheck = {
        'SearchId': $searchId,
        'Type': $type
    };
    $fileResponse = {};
    $.ajax({
        url: '/BusinessToCustomer/CheckSearchFileAvailable/',
        type: 'POST',
        dataType: 'json',
        async: false,
        data: { searchRequestCheck: $searchRequestCheck },
        beforeSend: function (eve) {
            $("#divLoading").show();
        },
        success: function (data) {
            $fileResponse = data;
            $('#divBlock').show();
        },
        complete: function () {
            $("#divLoading").hide();
        }
    });
    return $fileResponse;
}

function DownloadFile($searchId, $type) {
    if ($type === 'xlsx') {
        window.location = '/BusinessToCustomer/DownLoadAsExcel/?searchId=' + $searchId;
    } else {
        window.location = '/BusinessToCustomer/DownLoadAsCsv/?searchId=' + $searchId;
    }
}
function UpdateCustomerDashBoard(data) {

    var customerDataJson = data;
    $("#searchTotal").html(customerDataJson.searchCount);
    $("#total").html(customerDataJson.total);
    $("#excelDown").attr('title', customerDataJson.searchId);    
    $("#csvDown").attr('title', customerDataJson.searchCsvId);
    $('#dashboard').html('');
    //for (var key in customerDataJson) {
    //    if (customerDataJson.hasOwnProperty(key)) {

    //        if (DashBoardItem[key] !== null
    //            && DashBoardItem[key] !== undefined
    //            && DashBoardItem[key] !== 'undefined') {
    //            $('#dashboard').append(ConstructDashboardItem(DashBoardItem[key], customerDataJson[key]))
    //        }
    //    }
    //}
}
function ConstructDashboardItem(name, value) {
    var barCls = barColor[0];
    if (value > 0 && value < 25)
        barCls = barColor[1];
    if (value > 25 && value < 50)
        barCls = barColor[2];
    if (value > 50 && value < 75)
        barCls = barColor[3];
    if (value > 75 && value <= 100)
        barCls = barColor[5];


    return '<p class="font-600">' + name + ' <span class="text-primary pull-right">' + value + '%</span></p><div class="progress m-b-30"><div class="progress-bar ' + barCls + ' progress-animated wow animated animated" role="progressbar" aria-valuenow=' + value + ' aria-valuemin="0" aria-valuemax="100" style="width: ' + value + '%; visibility: visible; animation-name: animationProgress;"></div>';
}
var DashBoardItem = {

    'address': 'Address',
    'address2': 'Address2',
    'annualSalary': 'AnnualSalary',
    'area': 'Area',
    'caste': 'Caste',
    'city': 'City',
    'country': 'Country',
    'dob': 'Dob',
    'email': 'Email',
    'employer': 'Employer',
    'experience': 'Experience',
    'gender': 'Gender',
    'industry': 'Industry',
    'keySkills': 'KeySkills',
    'location': 'Location',
    'mobile2': 'Mobile2',
    'mobileNew': 'MobileNew',
    'name': 'Name',
    'network': 'Network',
    'phoneNew': 'PhoneNew',
    'pincode': 'Pincode',
    'qualification': 'Qualification',
    'roles': 'Roles',
    'state': 'State'

   
};
var barColor = ['progress-bar-danger', //0
    'progress-bar-pink', // 1
    'progress-bar-primary', // 2
    'progress-bar-info', // 3
    'progress-bar-warning', // 4
    'progress-bar-success' //5
]; 