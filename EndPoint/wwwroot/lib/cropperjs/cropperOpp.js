
var avatar;
var cropperjs_image;
var cropperjs_modal;
var cropperjs_contoller;
var cropperjs_the_input;
var cropperjs_maxpixel;
var hid_id;

var modalhtml = `
<div id="dynamic_cropper" style="background: #00000054;position: fixed;z-index: 100000;width: 100vw;height: 100vh;margin: 0;display: flex;align-items: center;justify-content: center;">
        <div style="max-width: 900px;width: 90vw;background: white;padding: 8px;border-radius: 5px;box-shadow: 0px 3px 17px 6px #585858;">
            <div style="padding: 0; display: flex;">
            <button type="button" class="btn btn-secondary cropper_btn" onclick="cropperjsOnBtnClose()">لغو</button>
            <button type="button" class="btn btn-secondary cropper_btn" onclick="cropperjsOnBtnReset()">ریست</button>
            <button type="button" class="btn btn-secondary cropper_btn" onclick="cropperjsOnBtnRotate()" style="background:#ff9426;"><svg style="fill: white;" height="22px" version="1.1" viewBox="0 0 1792 1792" width="22px" xmlns="http://www.w3.org/2000/svg"><path d="M1664 256v448q0 26-19 45t-45 19h-448q-42 0-59-40-17-39 14-69l138-138q-148-137-349-137-104 0-198.5 40.5t-163.5 109.5-109.5 163.5-40.5 198.5 40.5 198.5 109.5 163.5 163.5 109.5 198.5 40.5q119 0 225-52t179-147q7-10 23-12 14 0 25 9l137 138q9 8 9.5 20.5t-7.5 22.5q-109 132-264 204.5t-327 72.5q-156 0-298-61t-245-164-164-245-61-298 61-298 164-245 245-164 298-61q147 0 284.5 55.5t244.5 156.5l130-129q29-31 70-14 39 17 39 59z"/></svg></button>
            <button type="button" class="btn btn-secondary cropper_btn hidelittle" onclick="cropperjsOnBtnRotateCCW()" style="background:#ff9426;"><svg style="fill: white;transform: scaleX(-1);" height="22px" version="1.1" viewBox="0 0 1792 1792" width="22px" xmlns="http://www.w3.org/2000/svg"><path d="M1664 256v448q0 26-19 45t-45 19h-448q-42 0-59-40-17-39 14-69l138-138q-148-137-349-137-104 0-198.5 40.5t-163.5 109.5-109.5 163.5-40.5 198.5 40.5 198.5 109.5 163.5 163.5 109.5 198.5 40.5q119 0 225-52t179-147q7-10 23-12 14 0 25 9l137 138q9 8 9.5 20.5t-7.5 22.5q-109 132-264 204.5t-327 72.5q-156 0-298-61t-245-164-164-245-61-298 61-298 164-245 245-164 298-61q147 0 284.5 55.5t244.5 156.5l130-129q29-31 70-14 39 17 39 59z"/></svg></button>

            <button type="button" class="btn btn-primary cropper_btn" onclick="cropperjsOnBtnCrop()" data-dismiss="modal" style="margin-right: auto !important;">تایید</button>
            </div>
            <div style="margin:10px;">
            <div class="img-container">
                <img id="cropperjs_image" src="">
            </div>
            </div>
        </div>
        <style>
        @@media only screen and (max-width: 420px) {
            .hidelittle{display:none;}
        }
        </style>
</div>
`;

var nextAllowCropper = new Date().getTime();

function onChangePicCropper(input, maxPixel = 1500, miniPicId = 'imgUpload',hidId = '') {
    console.log(new Date().getTime() - nextAllowCropper);
    if (new Date().getTime() < nextAllowCropper) {
        return;
    }
    nextAllowCropper = new Date(new Date().getTime() + 1500).getTime();

    hid_id = hidId;

    $('#dynamic_cropper').remove();
    $('body').prepend(modalhtml);

    avatar = document.getElementById(miniPicId);
    cropperjs_image = document.getElementById('cropperjs_image');


    cropperjs_maxpixel = maxPixel;
    var files = input.files;
    cropperjs_the_input = input;
    var done = function (url) {
        input.value = '';
        input.title = '&nbsp;';
        cropperjs_image.src = url;
        //$alert.hide();
    };
    var reader;
    var file;
    var url;

    if (files && files.length > 0) {
        file = files[0];

        if (URL) {
            done(URL.createObjectURL(file));
        } else if (FileReader) {
            reader = new FileReader();
            reader.onload = function (e) {
                done(reader.result);
            };
            reader.readAsDataURL(file);
        }
    }
    cropperjs_contoller = new Cropper(cropperjs_image, {
        viewMode: 2,
    });
}
function cropperjsOnBtnReset() {
    cropperjs_contoller.resetView();
}
function cropperjsOnBtnClose() {
    $('#dynamic_cropper').remove();
    cropperjs_contoller.destroy();
    cropperjs_contoller = null;
}
function cropperjsOnBtnRotate() {
    cropperjs_contoller.rotateToRight();

}
function cropperjsOnBtnRotateCCW() {
    cropperjs_contoller.rotateToLeft();
}
function cropperjsOnBtnCrop() {

    try {
        var canvas;
        if (cropperjs_contoller) {
            //var orgmax = Math.max(cropperjs_contoller.getImageData().naturalWidth, cropperjs_contoller.getImageData().naturalHeight);
            var selectedmax = cropperjs_maxpixel;//Math.min(orgmax, cropperjs_maxpixel);

            canvas = cropperjs_contoller.getCroppedCanvas({
                width: selectedmax,
                height: selectedmax,
            });

            //initialAvatarURL = avatar.src;
            avatar.src = canvas.toDataURL('image/jpeg', 0.35);

            $("#" + hid_id).val(canvas.toDataURL('image/jpeg', 0.35));

            canvas.toBlob(function (blob) {
                var formData = new FormData();

                formData.append('avatar', blob, 'avatar.jpg');

                let file = new File([blob], "picture" + new Date().getTime() + ".jpg", { type: "mime/type", lastModified: new Date().getTime() });
                let containerx = new DataTransfer();
                containerx.items.add(file);
                cropperjs_the_input.files = containerx.files;
            }, 'image/jpeg', 0.35);
        }
        cropperjsOnBtnClose();

    } catch (e) {
        alert("خطا:" + e);
    }
};