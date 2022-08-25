var ProjectionIndex = {
    OnDeleteLinkClick: function (id) {
        var caller = new Service();
        if (document.querySelector(".input-validation-error")) {
            return;
        }
        caller.delete(AppUrl + "api/ProjectionForm/" + id, {}, function (data) {
            window.location.href = '/ProjectionForm';

        }, function (e) {
            DomExtension.ShowModal("Error", "Delete Not Sucessful");
        });
    },
    AddClickEventToDeleteLink: function () {
        let createButtons = document.querySelectorAll(".delete-link");
        for (let i = 0; i < createButtons.length; i++) {
            let btnCreate = createButtons[i];
            btnCreate.addEventListener('click', function (e) {
                ProjectionIndex.OnDeleteLinkClick(e.target.getAttribute('data-id'));
            });
        }

    }
};
window.onload = (event) => {
    ProjectionIndex.AddClickEventToDeleteLink();
}