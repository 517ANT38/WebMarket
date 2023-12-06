
function start() {
    removeChilds(document.body);
    myConfirm("Не достаточно товара на складе .Вернуться в на главную или в корзину для корректировки количества?", redirect);
}
function redirect1(f) {
    if (f) {
        window.location.replace("/Home/Index");
    }
    else window.location.replace("/Home/Basket");
}
