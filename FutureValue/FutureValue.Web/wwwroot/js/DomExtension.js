var DomExtension = {
    MakeElement: function (tagName, text) {
        let elem = document.createElement(tagName);
        elem.append(text);
        return elem;
    }
}