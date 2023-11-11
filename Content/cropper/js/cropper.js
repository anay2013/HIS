$(function () {
    var rot = 0, ratio = 1;
    var CanvasCrop = $.CanvasCrop({
        cropBox: ".imageBox",
        imgSrc: "https://exprohelp.com/his/images/uploadIcon.jpg",
        limitOver: 2
    });


    $('#upload-file').on('change', function () {
        var reader = new FileReader();
        reader.onload = function (e) {
            CanvasCrop = $.CanvasCrop({
                cropBox: ".imageBox",
                imgSrc: e.target.result,
                limitOver: 2
            });
            rot = 0;
            ratio = 1;
        }
        reader.readAsDataURL(this.files[0]);
        this.files = [];
    });

    $("#rotateLeft").on("click", function () {
        rot -= 90;
        rot = rot < 0 ? 270 : rot;
        CanvasCrop.rotate(rot);
    });
    $("#rotateRight").on("click", function () {
        rot += 90;
        rot = rot > 360 ? 90 : rot;
        CanvasCrop.rotate(rot);
    });
    $("#zoomIn").on("click", function () {
        ratio = ratio * 0.9;
        CanvasCrop.scale(ratio);
    });
    $("#zoomOut").on("click", function () {
        ratio = ratio * 1.1;
        CanvasCrop.scale(ratio);
    });
    $("#alertInfo").on("click", function () {
        var canvas = document.getElementById("visbleCanvas");
        var context = canvas.getContext("2d");
        context.clearRect(0, 0, canvas.width, canvas.height);
    });

    $("#crop").on("click", function () {

        var src = CanvasCrop.getDataURL("jpeg");
        //$("body").append("<div style='word-break: break-all;'>"+src+"</div>");  
        //$(".container").append("<img src='" + src + "' />");
        $("#imgSign").prop("src",src);
    });

    console.log("ontouchend" in document);
})