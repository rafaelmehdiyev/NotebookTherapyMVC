const prev = document.querySelector(".fa-arrow-left"),
    next = document.querySelector(".fa-arrow-right"),
    cards = document.querySelectorAll(".slider_card"),
    slider = document.querySelector(".slides");

let box = document.querySelector(".slider_card"),
    counter = 0;

prev.addEventListener("click", () => {
    counter--;
    slider_loop();
    console.log(counter);
});

next.addEventListener("click", () => {
    counter++;
    slider_loop();
    console.log(counter);
});

function slider_loop() {
    if (counter >= cards.length) {
        counter = 0;
    }
    if (counter < 0) {
        counter = cards.length - 1;
    }
    slider.style.left = `${-counter * box.offsetWidth}px`;
}

window.onresize = function () {
    slider.style.left = 0;
    counter = 0;
};