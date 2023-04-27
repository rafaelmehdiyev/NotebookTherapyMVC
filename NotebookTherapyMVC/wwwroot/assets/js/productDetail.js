// TABS
let tabs = document.querySelectorAll(".tabs li")
let tabContents = document.querySelectorAll(".tab-content .wrapper")

tabs.forEach((tab, index) => {
    tab.addEventListener("click", () => {
        tabContents.forEach(content => {
            content.classList.remove("active")
        })
        tabs.forEach(tab => {
            tab.classList.remove("active")
        })
        tabContents[index].classList.add("active")
        tabs[index].classList.add("active")
    })
})

//fav buton
let favbtn = document.querySelector(".favbtn")
let fav = document.querySelectorAll(".fav");
favbtn.addEventListener("click", () => {
    fav.forEach(item => {
        item.classList.toggle("show")
    })
})


// Bottom Carousel
let slides = $(".img-container")
let counter = 0

$(document).on('click', '.left', function () {
    counter--;
    slides = $(".img-container")
    window.matchMedia("(max-width: 768px)").matches ? carouselMobile() : carouselDesktop()
});

$(document).on('click', '.right', function () {
    counter++;
    slides = $(".img-container")
    window.matchMedia("(max-width: 768px)").matches ? carouselMobile() : carouselDesktop()
});

function carouselDesktop() {
    if (counter > slides.length - 4) {
        counter = 0;
    }
    if (counter < 0) {
        counter = slides.length - 4;
    }
    $(".slider").css('left', `${-counter * slides[0].offsetWidth}px`);
}

function carouselMobile() {
    if (counter > slides.length - 1) {
        counter = 0;
    }
    if (counter < 0) {
        counter = slides.length - 1;
    }
    $(".slider").css('left', `${-counter * slides[0].offsetWidth}px`);
}

window.onresize = function () {
    counter = 0;
    widthOfBody = document.body.clientWidth
    $(".slider").css('left', `0px`);
};

// Main Carousel 
var splide = new Splide("#main-slider", {
    width: 700,
    height: 600,
    pagination: false,
    cover: true
});

var thumbnails = document.getElementsByClassName("thumbnail");
var current;

for (var i = 0; i < thumbnails.length; i++) {
    initThumbnail(thumbnails[i], i);
}

function initThumbnail(thumbnail, index) {
    thumbnail.addEventListener("click", function () {
        splide.go(index);
    });
}

splide.on("mounted move", function () {
    var thumbnail = thumbnails[splide.index];
    if (thumbnail) {
        if (current) {
            current.classList.remove("is-active");
        }
        thumbnail.classList.add("is-active");
        current = thumbnail;
    }
});
splide.mount();

