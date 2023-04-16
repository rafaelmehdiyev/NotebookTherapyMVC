let btns = document.querySelectorAll(".btn")
let hiddens = document.querySelectorAll(".hidden")
console.log(hiddens);

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
const hiddenSidebar = document.querySelector(".hiddenSidebar")
filterBtn.addEventListener("click", () => {
    hiddenSidebar.classList.toggle("block")
})
