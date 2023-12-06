function myConfirm(text, cb) {


    var body = document.body;
    
    var overlay = document.createElement('div');
    overlay.className = 'myConfirm';

    
    var box = document.createElement('div');

    var p = document.createElement('p');
    p.appendChild(document.createTextNode(text));

    
    box.appendChild(p);

    
    var yesButton = document.createElement('button');
    var noButton = document.createElement('button');

    
    yesButton.appendChild(document.createTextNode('На главную'));
    yesButton.addEventListener('click', function () { cb(true);  }, false);
    noButton.appendChild(document.createTextNode('Перейти к действию'));
    noButton.addEventListener('click', function () { cb(false);  }, false);

    box.appendChild(yesButton);
    box.appendChild(noButton);

    overlay.appendChild(box)
    
    body.appendChild(overlay);

}
function redirect(f) {
    if (f) {
        window.location.replace("/Home/Index");
    }
    else window.location.replace("/Home/Arrange");
}
var removeChilds = function (node) {
    var last;
    while (last = node.lastChild) node.removeChild(last);
};