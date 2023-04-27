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