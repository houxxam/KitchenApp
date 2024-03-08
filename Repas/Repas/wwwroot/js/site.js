// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    $("#save-btn").on("click",(function () {

        const destination = {
            Malade: 0,
            Personnel: 1
        };
        $('#malade > tbody > tr').each(function (index, tr) {

            var dateId = $("#date").find('span').attr('id');
            var dest = $(this).closest('div').attr('id');
            var serviceid = $(tr).find('th').attr('serviceid');
            var servicetext = $(tr).find('th').text();
            var typerepas = $("#malade > thead > tr>th");
            $(tr).find('td').each(function (cellIndex, cell) {
                var inputVal = $(cell).find('input').val();
                console.log("SERVICE Name : " + servicetext);
                console.log("SERVICE : " + serviceid);
                console.log("TYPE REPAS : " + typerepas[cellIndex + 1].getAttribute('typerepas'));
                console.log("QTE : " + inputVal);
                console.log("-----------------------------------------");
                var repasData = {
                    TotalRepas: parseInt(inputVal),
                    TypeRepasId: parseInt(typerepas[cellIndex + 1].getAttribute('typerepas')),
                    ServiceId: parseInt(serviceid),
                    destination: parseInt(dest),
                    DateFornitureId: parseInt(dateId)
                    
                };
                var apiEndpoint = 'http://localhost:5092/api/RepasServicesApi';
                sendData(apiEndpoint, repasData);
                //window.location.href = '/';
            });

        });


    }))


    function sendData(apiEndpoint, repasData) {
        $.ajax({
            url: apiEndpoint, // Replace with your API endpoint
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(repasData),
            success: function (data) {
                // Handle success
                console.log('Data sent successfully:', data);
                // Optionally, you can redirect to another page or perform any other action
            },
            error: function (xhr, status, error) {
                // Handle error
                console.error('Error:', error);
            }
        });
    }

    function getRepasServiceByDateId(dateFornitureId) {
        $.ajax({
            url: '/api/RepasServicesApi/date/' + dateFornitureId,
            type: 'GET',
            success: function (data) {
                // Handle success
                console.log('RepasService retrieved:', data);
                // Optionally, you can process the retrieved data
            },
            error: function (xhr, status, error) {
                // Handle error
                console.error('Error:', error);
            }
        });
    }


    var currentUrl = window.location.href;

    // Check if the URL contains "RepasServices/Edit"
    if (currentUrl.includes("RepasServices/Edit")) {
        // Define your function
        getRepasServiceByDateId(39);
    }



});





 


