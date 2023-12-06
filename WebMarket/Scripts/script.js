function main() {
    let arr = document.querySelectorAll(".submitB");
    let arr1 = document.querySelectorAll(".incL");
    let arr2 = document.querySelectorAll(".decL");
    let str = "counter";
    arr.forEach((elem, i) => {
        elem.addEventListener("click", () => {
            let el = document.getElementById(str + i);
            let d = Number(el.textContent);
            
            addEventAddOrDiv(1, Number(elem.value), "CheckCount", "Home");
            elem.style.display = 'none';
            document.getElementById("range" + i).style.display='block';
            arr1[i].addEventListener("click", () => {
                incrementCount(i, str);
                let dd = Number(el.textContent);
                addEventAddOrDiv(dd, Number(elem.value), "CheckCountM", "Home");
            });
            arr2[i].addEventListener("click", () => {
                decrementCount(i, str);
                let dd = Number(el.textContent);
                if (dd == 0) {
                    el.textContent = 1;
                    elem.style.display = 'inline';
                    document.getElementById("range" + i).style.display = 'none';
                }
                addEventAddOrDiv(dd, elem.value, "DecCount", "Home");
            });
        });

    });
    
}
main(); 