const incrementCount1 = (i, str) => {
    let el = document.getElementById(str + i);
    let p = Number(el.textContent);

    p += 1;
    el.textContent = p;

}
function main() {
    let arr = document.querySelectorAll(".inc");
    let arr1 = document.querySelectorAll(".dec");
    let arr3 = document.querySelectorAll(".edit");
    let str = 'counterTd';

    arr.forEach((elem, i) => {

        elem.addEventListener("click", () => {
            incrementCount1(i, str);
            
        });
    });
    arr1.forEach((elem, i) => {

        elem.addEventListener("click", () => {
            decrementCount(i, str);
            
        });
    });
    arr3.forEach((elem, i) => {

        elem.addEventListener("click", () => {
            
            let d = Number(document.getElementById(str + i).textContent);
            addEventAddOrDiv(d, Number(elem.value), "Edit", "Admin");
        });
    });
}
main();