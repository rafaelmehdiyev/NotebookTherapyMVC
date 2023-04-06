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


// carousel2
let back = document.querySelector(".left")
let next = document.querySelector(".right")
let slider = document.querySelector(".slider")
let slides = document.querySelectorAll(".img-container")
let counter = 0

back.addEventListener("click", () => {
    counter--;
    carousel()
})

next.addEventListener("click", () => {
    counter++;
    carousel()
})

function carousel() {
    if (counter === slides.length - 4)
        counter = 0;

    if (counter < 0) {
        counter = slides.length - 4;
    }
    slider.style.left = `${-counter * 350}px`

}

// carousel 1 

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
