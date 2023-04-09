//------------- TAB -------------------//

const tabs = document.querySelectorAll(".tabs li"),
    cards = document.querySelectorAll(".cards");

tabs.forEach((tab, index) => {
    tab.addEventListener("click", () => {
        tabs.forEach((tab) => tab.classList.remove("active"));

        tab.classList.add("active");

        cards.forEach((c) => c.classList.remove("active"));

        cards[index].classList.add("active");
    });
});

tabs[0].click();

//------------- SLIDER 2023 PICKS-------------------//

const slider_cards = document.querySelectorAll(".slider-card"),
    right_arrow = document.querySelector(".arrow.right"),
    left_arrow = document.querySelector(".arrow.left"),
    slider_ = document.querySelector(".slider");

let content = document.querySelector(".content_width"),
    counter_slider = 0,
    minusSlide = 0;

if (window.matchMedia("(min-width: 1200px)").matches) {
    minusSlide = 4;
} else if (window.matchMedia("(min-width: 992px)").matches) {
    minusSlide = 3;
} else if (window.matchMedia("(min-width: 576px)").matches) {
    minusSlide = 2;
} else {
    minusSlide = 1;
}

left_arrow.addEventListener("click", () => {
    counter_slider--;
    slider_function();
});

right_arrow.addEventListener("click", () => {
    counter_slider++;
    slider_function();
    console.log(counter);
});

function slider_function() {
    if (counter_slider >= slider_cards.length - minusSlide) {
        right_arrow.style.pointerEvents = "none";
        right_arrow.style.opacity = 0;
    } else {
        right_arrow.style.pointerEvents = "auto";
        right_arrow.style.opacity = 1;
    }

    if (counter_slider > 0) {
        left_arrow.style.opacity = 1;
        left_arrow.style.pointerEvents = "auto";
    } else {
        left_arrow.style.opacity = 0;
        left_arrow.style.pointerEvents = "none";
    }

    slider_.style.left = `${-counter_slider * content.offsetWidth}px`;
}

//------------- SLIDER BLOG POSTS-------------------//

const prev = document.querySelector(".prev"),
    next = document.querySelector(".next"),
    cards_slider = document.querySelectorAll(".blog-post"),
    slider = document.querySelector(".blogs");
let widthOfBody = document.body.clientWidth,
    box = document.querySelector(".blog-post"),
    counter = 0;

prev.addEventListener("click", () => {
    counter--;
    slider_loop(this);
});

next.addEventListener("click", () => {
    counter++;
    slider_loop(this);
});

function slider_loop(click) {
    if (counter >= cards_slider.length - 1) {
        next.style.pointerEvents = "none";
        next.style.opacity = 0.5;
    } else {
        next.style.pointerEvents = "auto";
        next.style.opacity = 1;
    }

    if (counter > 0) {
        prev.style.opacity = 1;
        prev.style.pointerEvents = "auto";
    } else {
        prev.style.opacity = 0.5;
        prev.style.pointerEvents = "none";
    }

    if (widthOfBody < 1400) {
        slider.style.left = `${-counter * (box.offsetWidth + 64)}px`;
    } else {
        slider.style.left = `${-counter * (box.offsetWidth + 128)}px`;
    }
}

window.onresize = function () {
    slider_.style.left = 0;
    counter_slider = 0;
    left_arrow.style.opacity = 0;
    left_arrow.style.pointerEvents = "none";
    right_arrow.style.opacity = 1;
    right_arrow.style.pointerEvents = "auto";

    if (window.matchMedia("(min-width: 1200px)").matches) {
        minusSlide = 4;
    } else if (window.matchMedia("(min-width: 992px)").matches) {
        minusSlide = 3;
    } else if (window.matchMedia("(min-width: 576px)").matches) {
        minusSlide = 2;
    } else {
        minusSlide = 1;
    }

    slider.style.left = 0;
    counter = 0;

    prev.style.opacity = 0.5;
    prev.style.pointerEvents = "none";
    next.style.opacity = 1;
    next.style.pointerEvents = "auto";

    if (widthOfBody < 1400) {
        slider.style.left = `${-counter * (box.offsetWidth + 64)}px`;
    } else {
        slider.style.left = `${-counter * (box.offsetWidth + 128)}px`;
    }
};