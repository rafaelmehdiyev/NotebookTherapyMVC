// accardion
let btns = document.querySelectorAll(".categories li .d-flex")
let hiddens = document.querySelectorAll(".hidden")

btns.forEach((btn, btnIndex) => {
    btn.addEventListener("click", () => {
        hiddens.forEach((hidden, hiddenIndex) => {
            if (btnIndex === hiddenIndex) {
                hidden.classList.toggle("block")
            }
        })
    })
})

// Click filter
const filterBtn = document.querySelector("#filterBtn")
const sidebar = document.querySelector(".sidebar")
filterBtn.addEventListener("click", () => {
    sidebar.classList.toggle("block")
})

// Input range

let slider = document.querySelector(".range")
let value = document.getElementById("value")
slider.oninput = function () {
    value.innerHTML = `<span class="priceFilter"> 0 $ - ${this.value}$</span></p>`
}

//  fav button
let favbtn = document.querySelectorAll(".favbtn")
let fav = document.querySelectorAll(".fav");

favbtn.forEach(btn => {
    btn.addEventListener("click", (e) => {
        let tags = e.currentTarget.children
        console.log(tags);
        Array.from(tags).forEach(i => {
            i.classList.toggle("show")
        })
    })
})