const navMenu = document.getElementById("nav-menu"),
  toggleMenu = document.getElementById("toggle-menu"),
  closeMenu = document.getElementById("close-menu"),
  dropDown = document.querySelector(".drop_down"),
  megaMenu = document.querySelector(".megamenu");

dropDown.addEventListener("click", () => {
  megaMenu.classList.toggle("open");
});

toggleMenu.addEventListener("click", () => {
  navMenu.classList.toggle("show_menu");
});

closeMenu.addEventListener("click", () => {
  navMenu.classList.remove("show_menu");
});

let media = window.matchMedia("(max-width: 1200px)");

function ChangeOfHeader() {
  if (media.matches) {
    document.querySelector("nav").style.height = "4rem";
    document.getElementById("navbar").style.display = "none";
    document.querySelector(".navbar_responsive").style.display = "flex";
  }
  else {
    document.querySelector("nav").style.height = "8rem";
    // document.getElementById("navbar").style.display = "block";
    window.onscroll = function () {
      scrollFunction();
    };
  }
}

function scrollFunction() {
  if (document.body.scrollTop > 80 || document.documentElement.scrollTop > 80) {
    document.getElementById("navbar").style.display = "none";
    document.querySelector("nav").style.height = "4rem";
    document.querySelector(".megamenu").style.top = "4rem";
    document.querySelector(".icons_scroll").style.display = "block";
    document.querySelector(".icons_scroll").style.top = "1.5rem";
  } else {
    document.getElementById("navbar").style.display = "flex";
    document.querySelector("nav").style.height = "8rem";
    document.querySelector(".megamenu").style.top = "8rem";
    document.querySelector(".icons_scroll").style.display = "none";
    document.querySelector(".icons_scroll").style.top = "5rem";
  }
}

ChangeOfHeader();

// window.addEventListener("resize", function () {
//   console.log(window.innerWidth);
// })
