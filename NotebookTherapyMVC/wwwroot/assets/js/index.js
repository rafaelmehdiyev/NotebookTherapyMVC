//------------- TAB -------------------//

const tabs = document.querySelectorAll(".tabs li");
const cards = document.querySelectorAll(".cards");
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

const slider_cards = document.querySelectorAll(".slider-card");
const right_arrow = document.querySelector(".arrow.right");
const left_arrow = document.querySelector(".arrow.left");

let left = 0;
let card_size = 25.4;
let total_card_size = slider_cards.length * card_size - card_size * 4;

if (window.matchMedia("(max-width:1400px)").matches) {
  card_size = 35;
  total_card_size = slider_cards.length * card_size - card_size * 3;
}

if (window.matchMedia("(max-width:992px)").matches) {
  card_size = 52;
  total_card_size = slider_cards.length * card_size - card_size * 2;
}

if (window.matchMedia("(max-width:768px)").matches) {
  card_size = 105.5;
  total_card_size = slider_cards.length * card_size - card_size * 1;
}

left_arrow.onclick = () => {
  left -= card_size;

  if (left <= 0) {
    left = 0;
  }
  moveCards(left);
  checkArrowVisibility(left);
};

left_arrow.style.opacity = "0";

right_arrow.onclick = () => {
  left += card_size;

  if (left >= total_card_size) {
    left = total_card_size;
  }
  moveCards(left);
  checkArrowVisibility(left);
};

function moveCards(left) {
  for (card of slider_cards) {
    card.style.left = -left + "%";
  }
}

function checkArrowVisibility(pos) {
  if (pos == 0) {
    left_arrow.style.opacity = "0";
  } else {
    left_arrow.style.opacity = "1";
  }
  if (pos >= total_card_size) {
    right_arrow.style.opacity = "0";
  } else {
    right_arrow.style.opacity = "1";
  }
}


//------------- SLIDER BLOG POSTS-------------------//

const prev = document.querySelector(".prev");
const next = document.querySelector(".next");
const cards_slider = document.querySelectorAll(".blog-post");
const slider = document.querySelector(".blogs");
let widthOfBody = document.body.clientWidth;
let box = document.querySelector(".blog-post");

let counter = 0;

prev.addEventListener("click", () => {
  counter--;
  slider_loop(this);
  console.log(i);
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
    slider.style.left = `${-counter * (box.offsetWidth + 128)}px`
  }
}
