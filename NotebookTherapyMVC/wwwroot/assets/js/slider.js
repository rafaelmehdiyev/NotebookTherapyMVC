const prev = document.querySelector(".prev"),
    next = document.querySelector(".next"),
    cards = document.querySelectorAll(".slider_card"),
    slider = document.querySelector(".slider_");
let box = document.querySelector(".slider_card"),
    counter = 0,
    minusSlideCount = 0;

if (window.matchMedia("(max-width:768px)").matches) {
    minusSlideCount = 2;
} else {
    minusSlideCount = 4;
}

prev.addEventListener("click", () => {
    counter -= minusSlideCount;
    slider_loop();
    console.log(counter);
});

next.addEventListener("click", () => {
    counter += minusSlideCount;
    slider_loop();
    console.log(counter);
});

function slider_loop() {
    if (counter >= cards.length) {
        counter = 0;
    }
    if (counter < 0) {
        counter = cards.length - minusSlideCount;
    }
    slider.style.left = `${-counter * box.offsetWidth}px`;
}

window.addEventListener("resize", function () {
    counter = 0;
    slider.style.left = 0;
    if (window.matchMedia("(max-width:768px)").matches) {
        minusSlideCount = 2;
    } else {
        minusSlideCount = 4;
    }
});