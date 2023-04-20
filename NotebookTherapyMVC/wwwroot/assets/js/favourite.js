let favbtn = document.querySelector(".favbtn")
let fav = document.querySelectorAll(".fav");
favbtn.addEventListener("click", () => {
    fav.forEach(item => {
        item.classList.toggle("show")
    })
})
