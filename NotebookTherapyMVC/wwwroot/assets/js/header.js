const navMenu = document.getElementById("nav-menu"),
    toggleMenu = document.getElementById("toggle-menu"),
    closeMenu = document.getElementById("close-menu"),
    dropDown = document.querySelector(".drop_down"),
    megaMenu = document.querySelector(".megamenu"),
    body = document.getElementById("body"),
    nav = document.getElementById("nav");

$(".drop_down").click(function () {
    $(this).find(".megamenu").toggleClass("open")
})

toggleMenu.addEventListener("click", () => {
    navMenu.classList.toggle("show_menu");

    setTimeout(function () {
        body.classList.add("overlay");
        nav.style.position = "absolute";
    }, 400);
});

closeMenu.addEventListener("click", () => {
    navMenu.classList.remove("show_menu");
    body.classList.remove("overlay");
    nav.style.position = "static";
});

function scrollFunction() {
    if (document.body.scrollTop > 80 || document.documentElement.scrollTop > 80) {
        document.getElementById("navbar").style.display = "none";
        document.querySelector("nav").style.height = "4rem";
        $(".megamenu").css('top','4rem')
        $(".icons_scroll").css('display','block')
        $(".icons_scroll").css('top', '1.5rem')
    } else {
        document.getElementById("navbar").style.display = "flex";
        document.querySelector("nav").style.height = "8rem";
        $(".megamenu").css('top', '8rem')
        $(".icons_scroll").css('display', 'none')
        $(".icons_scroll").css('top', '5rem')
    }
}

window.onscroll = function () {
    if (window.matchMedia("(min-width: 1200px)").matches) {
        scrollFunction();
    }
};

window.addEventListener("resize", function () {
    if (window.matchMedia("(max-width: 1200px)").matches) {
        document.getElementById("navbar").style.display = "none";
        document.querySelector("nav").style.height = "4rem";
        $(".megamenu").css('top', '4rem')
        $(".icons_scroll").css('display', 'none')
        $(".icons_scroll").css('top', '1.5rem')
    } else {
        document.getElementById("navbar").style.display = "flex";
        document.querySelector("nav").style.height = "8rem";
        $(".megamenu").css('top', '8rem')
        $(".icons_scroll").css('display', 'none')
        $(".icons_scroll").css('top', '5rem')
    }
});