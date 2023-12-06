

function main() {
    let arr = document.querySelectorAll(".inc");
    let arr1 = document.querySelectorAll(".dec");
    let str = 'counterTd';
    
    arr.forEach((elem, i) => {
        
        elem.addEventListener("click", () => {
            incrementCount(i, str);
            let d = Number(document.getElementById(str + i).textContent);
            addEventAddOrDiv(d, Number(elem.value),"CheckCountM","Home");
        });
    });
    arr1.forEach((elem, i) => {
        
        elem.addEventListener("click", () => {
            decrementCount(i, str);
            let d = Number(document.getElementById(str + i).textContent);
            addEventAddOrDiv(d, elem.value, "DecCount", "Home");
        });
    });
}
main(); 