$(document).ready(function () {
    // datepicker JQuery
    $(function () {
        $("#Attachment_AttachmentDate").datepicker($.datepicker.regional["fr"]);
        $('#Attachment_AttachmentDate').datepicker("option", {
            showOtherMonths: true,
            //language: "fr-FR",
            selectOtherMonths: true,
            //format: "dd/mm/yyyy",
            changeMonth: true,
            changeYear: true
        });
        $('#Attachment_AttachmentDate').datepicker("option", "showAnim", "clip");
        // Setter
        $('#Attachment_AttachmentDate').datepicker("option", "dateFormat", "dd/mm/yy");

    });

    var flag = false,
        prevX = 0,
        currX = 0,
        prevY = 0,
        currY = 0,
        dot_flag = false;

    var lineColor = "black";
    var fontSize = 4;

    var imageSaved = document.getElementById("canvasimg");
    sizeCanvasImgFromDb();

    var canvas = document.getElementById('sketchpad');
    var ctx = canvas.getContext('2d');
    var w, h = 0;

    // resize the canvas to fill browser window dynamically
    window.addEventListener('resize', resizeCanvas, false);
    window.addEventListener('orientationchange', resizeCanvas, false);

    function resizeCanvas() {
        w = canvas.width = window.innerWidth;
        h = canvas.height = window.innerHeight;

        /**
         * Drawings need to be inside this function otherwise they will be reset when 
         * you resize the browser window and the canvas goes will be cleared.
         */
        displayDraw();
    }
    resizeCanvas();

    function displayDraw(res, e) {
        // manage mouse events        
        canvas.addEventListener("mousemove", function (e) {
            findxy('move', e)
        }, false);
        canvas.addEventListener("mousedown", function (e) {
            findxy('down', e)
        }, false);
        canvas.addEventListener("mouseup", function (e) {
            findxy('up', e)
        }, false);
        canvas.addEventListener("mouseout", function (e) {
            findxy('out', e)
        }, false);

        // manage touchscreen events
        canvas.addEventListener("touchmove", function (e) {
            findxy('move', e)
            e.preventDefault();
        }, false);
        canvas.addEventListener("touchstart", function (e) {
            findxy('down', e)
            e.preventDefault();
        }, false);
        canvas.addEventListener("touchend", function (e) {
            findxy('up', e)
            e.preventDefault();
        }, false);
        canvas.addEventListener("touchleave", function (e) {
            findxy('out', e)
            e.preventDefault();
        }, false);
    }

    function findxy(res, e) {
        if (res == 'down') {
            prevX = currX;
            prevY = currY;

            currX = (e.changedTouches ? e.changedTouches[0].clientX : e.clientX) - canvas.offsetLeft;
            currY = (e.changedTouches ? e.changedTouches[0].clientY : e.clientY) - canvas.offsetTop;

            flag = true;
            dot_flag = true;
            if (dot_flag) {
                ctx.beginPath();
                ctx.fillStyle = lineColor;
                ctx.fillRect(currX, currY, 2, 2);
                ctx.closePath();
                dot_flag = false;
            }
        }
        if (res == 'up' || res == "out") {
            flag = false;
        }
        if (res == 'move') {
            if (flag) {
                prevX = currX;
                prevY = currY;
                currX = (e.changedTouches ? e.changedTouches[0].clientX : e.clientX) - canvas.offsetLeft;
                currY = (e.changedTouches ? e.changedTouches[0].clientY : e.clientY) - canvas.offsetTop;
                draw();
            }
        }
    }

    // Draw on Canvas
    function draw() {
        ctx.beginPath();
        ctx.moveTo(prevX, prevY);
        ctx.lineTo(currX, currY);
        ctx.strokeStyle = lineColor;
        ctx.lineWidth = fontSize;
        ctx.stroke();
        ctx.closePath();
        ctx.fill();
    }

    // Clear Canvas
    function erase() {
        ctx.clearRect(0, 0, w, h);
    }

    function sizeCanvasImg() {
        // landscape
        if (w > h) {
            imageSaved.style.width = "200px";
            imageSaved.style.height = (imageSaved.style.width * h / w).toString();
        }
        // portrait
        else if (w < h) {
            imageSaved.style.height = "200px";
            imageSaved.style.width = (w * imageSaved.style.height / h).toString();
        }
    }

    function sizeCanvasImgFromDb() {
        // landscape
        if (imageSaved.clientWidth > imageSaved.clientHeight) {
            imageSaved.style.width = "200px";
            imageSaved.style.height = (imageSaved.style.width * h / w).toString();
        }
        // portrait
        else if (imageSaved.clientWidth < imageSaved.clientHeight) {
            imageSaved.style.height = "200px";
            imageSaved.style.width = (w * imageSaved.style.height / h).toString();
        }
    }

    // Save main canvas and display thumbnail
    function save() {
        sizeCanvasImg();

        var myDataURL = canvas.toDataURL();

        imageSaved.src = myDataURL;
        imageSaved.style.display = "inline";
        $('[Id*=blobdata]').val(myDataURL);
        if ($('[Id*=blobdata]').val() != '' && $('[Id*=blobdata]').val().match(/^data:image\/png;base64,/)) {
            $('#Attachment_IsSigned').prop("checked", true).attr("value", "true");
        }
    }

    function showCanvas() {
        $('#form-container').hide();
        $('#navbar-container').hide();
        $('footer').hide();
        $('#layout-container').removeClass("container").removeClass('body-content');
        $('hr').hide();
        $('body').removeClass('body-initial').addClass('body-canvas');
        $('html').addClass('html-canvas');
        $('.canvas-container').show();
    }

    function showForm() {
        $('.canvas-container').hide();
        $('body').removeClass('body-canvas').addClass('body-initial');
        $('html').removeClass('html-canvas');
        $('#layout-container').addClass("container").addClass('body-content');
        $('footer').show();
        $('hr').show();
        $('#navbar-container').show();
        $('#form-container').show();
        erase();
    }

    $('#btnClear').click(function () {
        erase();
    })

    $('#btnReturn').click(function () {
        showForm();
    })

    $('#btnSignature').click(function () {
        showCanvas();
    })

    $('#btnSave').click(function () {
        save();
        $('#btnSignature').hide();
        $('#btnClearCanvas').show();
        showForm();
    })

    $('#btnClearCanvas').click(function () {
        var btnClearCanvas = $(this);
        var btnSignature = $('#btnSignature');
        // custom confirm box
        bootbox.confirm({
            message: "Etes-vous certain(e) de vouloir effacer cette signature ?",
            buttons: {
                cancel: {
                    label: 'Annuler',
                    className: 'btn btn-default pmd-ripple-effect pmd-btn-raised',
                },
                confirm: {
                    label: 'Confirmer',
                    className: 'btn btn-primary pmd-ripple-effect pmd-btn-raised',
                }
            },
            callback: function (result) {
                if (result) {
                    $('#canvasimg').removeAttr("src").hide();
                    $('#blobdata').val("");
                    $('#Attachment_IsSigned').prop("checked", false).attr("value", "false");
                    btnClearCanvas.hide();
                    btnSignature.show();
                }
            }
        });
    });
})

// fix date validator issue on Chrome
$.validator.addMethod("date",
    function (value, element) {
        return this.optional(element) || parseDate(value, "dd-MM-yyyy") !== null;
    })
