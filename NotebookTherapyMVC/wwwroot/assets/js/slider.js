const prev = document.querySelector(".prev");
const next = document.querySelector(".next");
const cards = document.querySelectorAll(".slider_card");
const slider = document.querySelector(".slider_");
let box = document.querySelector(".slider_card");

let counter = 0;
let minusSlideCount = 4;

if (window.matchMedia('(max-width:768px)').matches) {
    minusSlideCount = 2;
}

prev.addEventListener("click", () => {
    counter-=minusSlideCount;
    slider_loop();
        console.log(counter);
})

next.addEventListener("click", () => {
    counter+=minusSlideCount;
    slider_loop();
    console.log(counter);
})

function slider_loop() {
    if (counter >= cards.length) {
        counter = 0;
    }
    if (counter < 0) {
        counter = cards.length-minusSlideCount;
    }
    slider.style.left = `${-counter * box.offsetWidth}px`;
}
