window.resizeImageFromBase64 = async (dataUrl, maxWidth, maxHeight) => {
    return new Promise((resolve, reject) => {

        const img = new Image();

        img.onload = () => {
            let canvas = document.createElement('canvas');

            let ratio = Math.min(maxWidth / img.width, maxHeight / img.height);
            let width = img.width * ratio;
            let height = img.height * ratio;

            canvas.width = width;
            canvas.height = height;

            const ctx = canvas.getContext('2d');
            ctx.drawImage(img, 0, 0, width, height);

            resolve(canvas.toDataURL("image/jpeg", 0.8));
        };

        img.onerror = err => reject("Image failed to load: " + err);

        img.src = dataUrl;
    });
};
