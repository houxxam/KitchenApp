// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    var count = 0;
    var rowCount =0;
    const destination = ["malade", "personnel"];
    var apiEndpoint = 'http://localhost:5092/api/RepasServicesApi';

    

    // AJOUTER
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





    //MODIFIER
    $("#edit-btn").on("click", function () {
        var totalCount = 0;
        var completedCount = 0;
        const destination = ["malade", "personnel"];
        
        //Date Id
        var dateId = $("#date").find('span').attr('id');

        for (var i = 0; i < destination.length; i++) {
            $('#edit-' + destination[i] + ' tbody tr').each(function (index, tr) {
               //Destination Id
                var dest = $(this).closest('div').attr('id');
                //Service Id
                var serviceid = $(tr).find('td').attr('ServiceId');
                var servicetext = $(tr).find('th').text();
                var typerepas = $("#edit-" + destination[i] + " thead tr th");
               
          

                $(tr).find('td').each(function (cellIndex, cell) {
                    var inputVal = $(cell).find('input').val();
                    
                    if (!isNaN(inputVal)) {
                        var repasId = $(cell).find('label').attr("repasId");
                        //var typerepas = $(this).closest('table').find('th').eq(cellIndex + 1).attr('TypeRepas');
                        var TypeRepasId=  parseInt(typerepas.eq(cellIndex).attr('typerepas'));
                       
                            var repasData = {
                                Id: parseInt(repasId),
                                TotalRepas: parseInt(inputVal),
                                TypeRepasId: TypeRepasId,
                                ServiceId: parseInt(serviceid),
                                destination: parseInt(dest),
                                DateFornitureId: parseInt(dateId)
                            };
                        totalCount++;
                        console.log(repasData);
                        updateRepasService(repasData.Id, repasData,checkCompletion);
                        
                        
                        console.log(totalCount);
                        
                    } 
                });
                
                
            });
        }
        // Check completion after all API requests are made
        function checkCompletion() {

            completedCount++;
           
            if (completedCount === totalCount) {
                window.location.replace('/Repasservices');
                console.log('refresh');
            }
        }

    });


    function updateRepasService(id, repasService, callback) {
        $.ajax({
            url: apiEndpoint +'/'+ id, // Replace ControllerName with your actual controller name
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(repasService),
            success: function (_response) {
                console.log("Data updated successfully:", _response);
                // Optionally, perform any actions after successful update
                if (callback) {
                    callback(); // Invoke the callback function if provided
                }
            },
            error: function (xhr, status, error) {
                console.error("Error updating data:", xhr.responseText);
                // Optionally, handle errors or display error messages
            }
        });
    }



    new Calendar('#calendar');
    // Register events
    document.querySelector('#calendar').addEventListener('clickDay', function (e) {


        var day = e.date.getDate();
        var month = e.date.getMonth() + 1; // Months are zero-based, so we add 1
        var year = e.date.getFullYear();

   
        var dayString = day < 10 ? '0' + day : day.toString();
        var monthString = month < 10 ? '0' + month : month.toString();

        
        var formattedDate = year + '-' + monthString + '-' + dayString + 'T00:00:00';

        
        checkDataExists(formattedDate);
    });

    // Function to check if data exists for a given date
    function checkDataExists(date) {
       
        $.ajax({
            url: '/api/HomeApi/date/' + date,
            type: 'GET',
            success: function (response) {
                
                if (response) {
                    console.log('Data exists for ' + date + ':', response);

                    getTypeRepasCount(date,response.id);
                } else {
                    console.log('No data found for ' + date);
                    $('#exist-div').hide();
                    $("#myModal").modal("show");
                    $("#modal-text-message").text(date);
                    $("#modal-yes-btn").on("click", function () {
                        createNewDateForniture(date);
                       // window.location.replace('/repasServices/create/');
                    });
                }
            },
            error: function (xhr, status, error) {
                console.error('Error occurred while checking data:', error);
                // Handle errors if necessary
            }
        });
    }

    function createNewDateForniture(date) {
        var newDateForniture = {
            FornitureDate: date
            
        };

        $.ajax({
            url: '/api/HomeApi/date/create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(newDateForniture),
            success: function (response) {
                console.log('New DateForniture created successfully:', response);
                // Optionally, perform additional actions after successful creation
                
                window.location.replace('/RepasServices/Create?date=' + response.fornitureDate + '&id=' + response.id);
               
            },
            error: function (xhr, status, error) {
                console.error('Error occurred while creating new DateForniture:', error);
                // Handle errors if necessary
            }
        });
        
    }

    function getTypeRepasCount(date,dateId) {
        $.ajax({
            url: '/api/repasservicesApi/test/' + dateId,
            type: 'GET',
            success: function (response) {
                // Your success handling code here
                $('#exist-div').fadeIn();


                // Clear thead and tbody before appending new data
                $('#exist-div thead').empty();
                $('#exist-div tbody').empty();
                $('#exist-div thead').append('<tr>');
                // Add the date clicked as the first column header in the thead
                $('#exist-div thead tr').append('<th> Date </th>');
                $('#exist-div tbody').append('<tr><td><a class="text-decoration-none text-primary" href="/RepasServices/Details/' + dateId + '">' + formatDate(date) + '</a></td></tr>');

                // Iterate through each item in the response to populate thead and tbody
                $.each(response, function (index, item) {

                    // Add the name to the thead
                    $('#exist-div thead tr').append('<th>' + item.name + '</th>');

                    // Add the count to the tbody
                    $('#exist-div tbody tr').append('<td>' + item.count + '</td>');
                });
            },
            error: function (xhr, status, error) {
                console.error('Error occurred while fetching data:', error);
                // Handle errors if necessary
            }
        });
    }

    function formatDate(inputDate) {
        // Parse the input date string
        const date = new Date(inputDate);

        // Extract day, month, and year components
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0'); // Month is zero-based
        const year = date.getFullYear();

        // Format the date as 'dd-mm-yyyy'
        return `${day}-${month}-${year}`;
    }

    
});







 


