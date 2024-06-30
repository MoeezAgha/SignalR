window.initializeSignatureCanvas = function (canvasId) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext('2d');
    var drawing = false;

    canvas.addEventListener('mousedown', function (e) {
        drawing = true;
        ctx.beginPath();
        ctx.moveTo(e.clientX - canvas.offsetLeft, e.clientY - canvas.offsetTop);
    });

    canvas.addEventListener('mousemove', function (e) {
        if (drawing) {
            ctx.lineTo(e.clientX - canvas.offsetLeft, e.clientY - canvas.offsetTop);
            ctx.stroke();
        }
    });

    canvas.addEventListener('mouseup', function () {
        drawing = false;
    });

    canvas.addEventListener('mouseleave', function () {
        drawing = false;
    });
};
window.saveCanvasAsPng = function (canvasId) {
    var canvas = document.getElementById(canvasId);
    var dataUrl = canvas.toDataURL('image/png');
    return dataUrl;
};
