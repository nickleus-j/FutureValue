var DomExtension = {
    MakeElement: function (tagName, text) {
        let elem = document.createElement(tagName);
        elem.append(text);
        return elem;
    },
    ShowModal: function(Title,Text){
        let modalElem = document.querySelector(".main-modal");
        modalElem.classList.add("d-block");
        modalElem.querySelector(".modal-title").innerHTML = Title;
        modalElem.querySelector(".modal-body p").innerHTML = Text;
        setTimeout(() => {
            document.querySelector(".main-modal").classList.remove("d-block");
        }, 5000)
    }
}