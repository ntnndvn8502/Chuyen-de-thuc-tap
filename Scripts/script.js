function previewFile() {
    const preview = document.getElementById('previewImg');
    const file = document.getElementById('ImgInput').files[0];
    const string = document.getElementById('RtrAvatar');
    const string2 = document.getElementById('ImageUrl');
    const reader = new FileReader();

    // listen for 'load' events on the FileReader
    reader.addEventListener("load", function () {
        // change the preview's src to be the "result" of reading the uploaded file (below)
        preview.src = reader.result;
        string.value = preview.src;
        string2.value = reader.result;
        preview.style.display = "block";
    }, false);

    // if there's a file, tell the reader to read the data
    // which triggers the load event above
    if (file) {
        reader.readAsDataURL(file);
    }
}
function uploadFile() {
    
    const file = document.getElementById('ImgInput').files[0];
   
    const string2 = document.getElementById('ImageUrl');
    const reader = new FileReader();

    // listen for 'load' events on the FileReader
    reader.addEventListener("load", function () {
        // change the preview's src to be the "result" of reading the uploaded file (below)
        
        string2.value = reader.result;
        
    }, false);

    // if there's a file, tell the reader to read the data
    // which triggers the load event above
    if (file) {
        reader.readAsDataURL(file);
    }
}
function uploadFile2() {

    const file = document.getElementById('ImgInput2').files[0];

    const string2 = document.getElementById('ImageUrl2');
    const reader = new FileReader();

    // listen for 'load' events on the FileReader
    reader.addEventListener("load", function () {
        // change the preview's src to be the "result" of reading the uploaded file (below)

        string2.value = reader.result;

    }, false);

    // if there's a file, tell the reader to read the data
    // which triggers the load event above
    if (file) {
        reader.readAsDataURL(file);
    }
}
$(".openModal").click(function () {
    var text = $(this).children("span").text();
    $("#ImageID").val(text);
    $("#ImgInput2").val("");
    
})
$("#handleNull form").submit(function (e) {
    if ($("#ImgInput").val() === "") {
        e.preventDefault();
        $(".upload-error").text("Chưa upload ảnh");
    }

});
$(document).ready(function () {
    new Splide('.item-3',
        {
            perPage: 3,
            breakpoints: {
                768: {
                    perPage: 1,
                },
                1024: {
                    perPage: 2,
                }
            },
            pagination: false,
            gap: 12
        }).mount();

    

});
$(document).ready(function () {
    new Splide('.item-2',
        {
            perPage: 2,
            breakpoints: {
                768: {
                    perPage: 1,
                },
            },
            pagination: false,
            gap: 12
        }).mount();



});

$("#rate-form").submit(function (e) {
    
    if ($("input[name='Rate']:checked").val() === "0") {
        e.preventDefault();
        $(".upload-error").text("Bạn chưa đánh giá");
    }

});
$("#Booking").submit(function (e) {
    
    var date = $("#ArrivalDate").val();
    var time = $("#ArrivalTime").val();
    

    // Tách lấy ngày, tháng, năm từ chuỗi ngày
    var partsDate = date.split(/[\/-]/);
    var ngay = parseInt(partsDate[0], 10);
    var thang = parseInt(partsDate[1], 10) - 1; // Tháng bắt đầu từ 0
    var nam = parseInt(partsDate[2], 10);

    // Tách lấy giờ và phút từ chuỗi thời gian
    var partsTime = time.split(":");
    var gio = parseInt(partsTime[0], 10);
    var phut = parseInt(partsTime[1], 10);
    
    

    // Tạo đối tượng Date từ các giá trị trên
    var ngayVaGio = new Date(nam, thang, ngay, gio, phut);
    
    console.log(ngayVaGio);
    
    if (ngayVaGio < new Date()) {
        
        e.preventDefault();
        $("#errorToday").text("Thời điểm đặt đã qua thời điểm hiện tại");
    }

    

});
$("#PageEdit").submit(function (e) {
    
    
    var time1 = document.getElementById("OpenTime").value;

    // Lấy giá trị của input thứ hai
    var time2 = document.getElementById("CloseTime").value;
    var inputTimeObj = new Date("1970-01-01T" + time1);
    var inputTime = new Date("1970-01-01T" + time2);
    
    var price1 = document.getElementById("MinPrice").value;
    var price2 = document.getElementById("MaxPrice").value;


    // So sánh giá trị của hai input
    if (inputTimeObj > inputTime) {
        e.preventDefault();
        document.getElementById("errorTime").innerText = "Giờ mở cửa và đóng cửa không hợp lệ"
    }
    if (price1 !== "" && price2 !== "" && price1 >= price2) {
        e.preventDefault();
        document.getElementById("errorPrice").innerText = "Khoảng giá không hợp lệ";
    }

});
$("#pass-btn").click(function () {
    console.log("hello");
    if ($(this).text() === "Hiện") {
        $(this).text("Ẩn");
        $("#pass").show();
        $("#blur").hide();

    } else {
        $(this).text("Hiện");
        $("#blur").show();
        $("#pass").hide();
    }
});


