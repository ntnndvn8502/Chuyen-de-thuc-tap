

$("#date-btn").click(function () {
    if ($("#startDate").val() === "") {
        var filteredItems = $("tbody tr").filter(function () {

            var endDate = new Date($("#endDate").val());
            endDate.setDate(endDate.getDate() + 1);
            var dateStr = $(this).find(".ArrivalDate").text(); // Giả sử ngày tháng được lưu trữ trong văn bản của mỗi phần tử <li>   
            var date = new Date(dateStr);
            return date <= endDate;

        });
    } else if ($("#endDate").val() === "") {
        var filteredItems = $("tbody tr").filter(function () {
            var startDate = new Date($("#startDate").val());
            startDate.setDate(startDate.getDate() - 1);
            var dateStr = $(this).find(".ArrivalDate").text(); // Giả sử ngày tháng được lưu trữ trong văn bản của mỗi phần tử <li>   
            var date = new Date(dateStr);
            return date > startDate;

        });
    } else {
        var filteredItems = $("tbody tr").filter(function () {
            var startDate = new Date($("#startDate").val());
            startDate.setDate(startDate.getDate() - 1);
            var endDate = new Date($("#endDate").val());
            endDate.setDate(endDate.getDate() + 1);
            var dateStr = $(this).find(".ArrivalDate").text(); // Giả sử ngày tháng được lưu trữ trong văn bản của mỗi phần tử <li>   
            var date = new Date(dateStr);
            return date > startDate && date < endDate;

        });
    }
    
    
    // Ẩn các phần tử không nằm trong khoảng và hiển thị các phần tử nằm trong khoảng
    $("tbody tr").hide();
    filteredItems.show();
});
$("#textSearch").on("keyup", function () {
    
    var value = $(this).val().toLowerCase();
    $("tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});
$("#statusSearch").change(function () {
    var selectElement = document.getElementById("statusSearch");
    var selectedValue = selectElement.options[selectElement.selectedIndex].value;

    window.location.href = "/Admin/Orders/Index/" + selectedValue;

});
$("#happenSearch").change(function () {
    
    var selectElement = document.getElementById("happenSearch");
    var selectedValue = selectElement.options[selectElement.selectedIndex].value;

    window.location.href = "/Restaurants/Orders/History/" + selectedValue;

});
$(".form-check-input").click(function () {
    
    if ($("#DateSort").is(":checked")) {
        
        $("#DateSort-list").show();
        $("#RateSort-list").hide();
    }
    if ($("#RateSort").is(":checked")) {
        
        $("#RateSort-list").show();
        $("#DateSort-list").hide();
    }
})



