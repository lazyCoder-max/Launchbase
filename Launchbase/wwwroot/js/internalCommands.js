window.playSound = async function () {
    var audio = document.getElementById("swapAudio");

    if (audio.paused) {
        await new Promise((resolve) => {
            audio.addEventListener("ended", resolve, { once: true });
            audio.play();
        });
    } else {
        audio.currentTime = 0;
    }
};
