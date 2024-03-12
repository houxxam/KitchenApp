// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    var count = 0;
    var rowCount =0;
    const destination = ["malade", "personnel"];
     
    $("#save-btn").on("click", function () {
       
        for (var i = 0; i < destination.length; i++) {
           
            
            $('#' + destination[i] + ' tbody tr').each(function (index, tr) {
                

                var dateId = $("#date").find('span').attr('id');
                var dest = $(this).closest('div').attr('id');
                var serviceid = $(tr).find('th').attr('serviceid');
                var servicetext = $(tr).find('th').text();
                var typerepas = $("#" + destination[i] +" thead tr th");
           
                $(tr).find('td').each(function (cellIndex, cell) {
                    var inputVal = $(cell).find('input').val();


                   
                    rowCount = $('#' + destination[i] + ' tbody td').length;

                    console.log("Row Count =>" + rowCount);
                
                
                    console.log("SERVICE Name: " + servicetext);
                    console.log("SERVICE ID: " + serviceid);
                    console.log("TYPE REPAS: " + typerepas.eq(cellIndex + 1).attr('typerepas'));
                    console.log("QTE: " + inputVal);
                    console.log("-----------------------------------------");
                    var repasData = {
                        TotalRepas: parseInt(inputVal),
                        TypeRepasId: parseInt(typerepas.eq(cellIndex + 1).attr('typerepas')),
                        ServiceId: parseInt(serviceid),
                        destination: parseInt(dest),
                        DateFornitureId: parseInt(dateId)
                    };
                    var apiEndpoint = 'http://localhost:5092/api/RepasServicesApi';
                    sendData(apiEndpoint, repasData);
                
                });
            });
            if (i === destination.length - 1) {
                if (count === rowCount) {
                    
                    window.location.href = '/';
                    console.log('refresh');

                }
            }
        }
        // Move this line outside of the loop
        //window.location.href = '/'; // Redirect after processing all rows
    });



    function sendData(apiEndpoint, repasData) {
        console.log("apiEndpoint : " + apiEndpoint);
        $.post({
            url: apiEndpoint,
            contentType: 'application/json',
            data: JSON.stringify(repasData),
            success: function (data) {
                // Handle success
                count++;
                console.log("Count : "+count);
                console.log("rowCount : " + rowCount);
                console.log('Data sent successfully:', data);
                // Optionally, you can redirect to another page or perform any other action
                
            },
            error: function (xhr, status, error) {
                // Handle error
                console.error('Error:', error);
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





 


