﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
                        updateRepasService(repasData.Id, repasData, checkCompletion);
                          
                    } 
                });
                
                
            });
        }
        // Check completion after all API requests are made
        function checkCompletion() {
            completedCount++;
            if (completedCount === totalCount) {
                window.location.href = '/Repasservices';
                console.log('refresh');
            }
        }

    });


    function updateRepasService(id, repasService) {
        $.ajax({
            url: apiEndpoint +'/'+ id, // Replace ControllerName with your actual controller name
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(repasService),
            success: function (response) {
                console.log("Data updated successfully:", response);
                // Optionally, perform any actions after successful update
            },
            error: function (xhr, status, error) {
                console.error("Error updating data:", xhr.responseText);
                // Optionally, handle errors or display error messages
            }
        });
    }


});





 


