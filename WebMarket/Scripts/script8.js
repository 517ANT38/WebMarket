let arr = document.querySelectorAll(".bT");
arr.forEach((e, i) => {
    e.addEventListener("click", () => {
        if (e.textContent == "+") {
            document.getElementById("table" + i).style.display = "block";
            e.textContent = "-";
        }
        else {
            document.getElementById("table" + i).style.display = "none";
            e.textContent = "+";
        }
    })
})