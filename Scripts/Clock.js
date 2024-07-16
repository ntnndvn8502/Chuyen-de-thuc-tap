$(document).ready(function () {
    const min = $("#startTime").text();
    const max = $("#endTime").text();
    $('#ArrivalTime').timepicker({
        timeFormat: 'HH:mm',
        interval: 30,
        minTime: min,
        maxTime: max,
        dynamic: false,
        dropdown: true,
        scrollbar: true
    });
    
    
    $("#ArrivalDate").datepicker({
        minDate: new Date(),
    }
        
    );
    
});

