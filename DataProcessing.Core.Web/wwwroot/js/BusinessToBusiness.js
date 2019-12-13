$(window).load(function () {
    $("#divLoading").hide();
});
$(function () {
    // Handler for .ready() called.
    $("#divLoading").show();
});

$(document).ready(function (eve) {
    //$("#divLoading").show();
    $('#btnBusinessToBusinessSearch').on('click', function (eve) {
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
            ) { //|| tags !== null

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

    //$('#tblSummary').DataTable({
    //    "paging": true // false to disable pagination (or any other option)
    //});
    //$('.dataTables_length').addClass('bs-select');

   
});

function FileCheck($searchId, $type) {
    var $searchRequestCheck = {
        'SearchId': $searchId,
        'Type': $type
    };
    $fileResponse = {};
    $.ajax({
        url: '/BusinessToBusiness/CheckSearchFileAvailable/',
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
        window.location = '/BusinessToBusiness/DownLoadAsExcel/?searchId=' + $searchId;
    } else {
        window.location = '/BusinessToBusiness/DownLoadAsCsv/?searchId=' + $searchId;
    }
}

function UpdateB2BDashBoard(data) {

    var b2bJson = data;
    console.log(data);
    $("#b2SearchTotal").html(b2bJson.searchCount);
    $("#b2bTotal").html(b2bJson.total);
    $("#excelDown").attr('title', b2bJson.searchId);    
    $("#csvDown").attr('title', b2bJson.searchCsvId);
    //for (var key in b2bJson) {
    //    if (b2bJson.hasOwnProperty(key)) {

    //        if (b2CDashBoardItem[key] !== null
    //            && b2CDashBoardItem[key] !== undefined
    //            && b2CDashBoardItem[key] !== 'undefined') {
    //            $('#b2bDashboard').append( ConstructDashboardItem(b2CDashBoardItem[key], b2bJson[key]))
    //        }
    //    }
    //}
    
    
    //alert(JSON.stringify(data));
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

            
    return '<p class="font-600">' + name + ' <span class="text-primary pull-right">' + value + '%</span></p><div class="progress m-b-30"><div class="progress-bar ' + barCls +' progress-animated wow animated animated" role="progressbar" aria-valuenow=' + value + ' aria-valuemin="0" aria-valuemax="100" style="width: ' + value + '%; visibility: visible; animation-name: animationProgress;"></div>';
}
var b2CDashBoardItem = {    
    'companyName': 'Company Name',
    'add1': 'Address1',
    'add2': 'Address2',
    'city': 'City',
    'area': 'Area',
    'pincode': 'Pincode',
    'state': 'State',
    'phoneNew': 'Phone New',
    'mobileNew': 'Mobile New',
    'phone1': 'Phone1',
    'phone2': 'Phone2',
    'mobile1': 'Mobile1',
    'mobile2': 'Mobile2',
    'fax': 'Fax',
    'email': 'Email',
    'email1': 'Email1',
    'web': 'Web',
    'contactPerson': 'Contact Person',
    'designation': 'Designation',
    'designation1': 'Designation1',
    'contactperson1': 'Contact Person1',
    'estYear': 'Est Year',
    'categoryId': 'Category Id',
    'landMark': 'Land Mark',
    'noOfEmp': 'Number Of Employee',
    'country': 'Country'
};
var barColor = ['progress-bar-danger', //0
    'progress-bar-pink', // 1
    'progress-bar-primary', // 2
    'progress-bar-info', // 3
    'progress-bar-warning', // 4
    'progress-bar-success' //5
]; 