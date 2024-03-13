// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    var count = 0;
    var rowCount =0;
    const destination = ["malade", "personnel"];
     
    $("#save-btn").on("click", function () {
        var totalCount = 0;
        var completedCount = 0;

        // Loop through each destination
        for (var i = 0; i < destination.length; i++) {
            $('#' + destination[i] + ' tbody tr').each(function (index, tr) {
                var dateId = $("#date").find('span').attr('id');
                var dest = $(this).closest('div').attr('id');
                var serviceid = $(tr).find('th').attr('serviceid');
                var servicetext = $(tr).find('th').text();
                var typerepas = $("#" + destination[i] + " thead tr th");

                $(tr).find('td').each(function (cellIndex, cell) {
                    var inputVal = $(cell).find('input').val();

                    var repasData = {
                        TotalRepas: parseInt(inputVal),
                        TypeRepasId: parseInt(typerepas.eq(cellIndex + 1).attr('typerepas')),
                        ServiceId: parseInt(serviceid),
                        destination: parseInt(dest),
                        DateFornitureId: parseInt(dateId)
                    };
                    var apiEndpoint = 'http://localhost:5092/api/RepasServicesApi';

                    sendData(apiEndpoint, repasData, checkCompletion);

                    // Increment total count
                    totalCount++;
                });
            });
        }

        // Check completion after all API requests are made
        function checkCompletion() {
            completedCount++;
            if (completedCount === totalCount) {
                window.location.href = '/';
                console.log('refresh');
            }
        }
    });


    function sendData(apiEndpoint, repasData, callback) {
        $.ajax({
            url: apiEndpoint,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(repasData),
            success: function (data) {
                console.log('Data sent successfully:', data);
                if (callback) {
                    callback(); // Invoke the callback function if provided
                }
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
                // You may handle errors here if needed
            }
        });
    }


    //function getRepasServiceByDateId(dateFornitureId) {
    //    $.ajax({
    //        url: '/api/RepasServicesApi/date/' + dateFornitureId,
    //        type: 'GET',
    //        success: function (data) {
    //            // Handle success
    //            console.log('RepasService retrieved:', data);
    //            // Optionally, you can process the retrieved data
    //        },
    //        error: function (xhr, status, error) {
    //            // Handle error
    //            console.error('Error:', error);
    //        }
    //    });
    //}


   



});





 


