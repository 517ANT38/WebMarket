const incrementCount = (i,str) => {
    let el = document.getElementById(str + i);
    let p = Number(el.textContent);
    if (p < 100) {
        p += 1;
        el.textContent = p;
    }
}
 
const decrementCount = (i,str) => {
    let el = document.getElementById(str + i);
    let p = Number(el.textContent);
    if (p > 0) {
        p -= 1;
        el.textContent = p;
    }
}
function addEventAddOrDiv(d,id, action, controller) {

   

    let model = { count: d, Id_product: id };

    $.ajax({
        type: "POST",
        url: `/${controller}/` + action,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        datatype: "html"
    });

}
function clearProduct() {
    let arr = document.querySelectorAll(".trchekCount");

    arr.forEach((elem) => {
        let a=elem.querySelector(".checkDel");
        if (Number(a.textContent) == 0) {
            elem.remove();
        }
    });
}
function generateHashPass(s) {
    return s;
     
}