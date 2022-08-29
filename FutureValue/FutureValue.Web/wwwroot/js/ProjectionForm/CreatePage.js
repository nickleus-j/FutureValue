var ProjectionCreate = {
    GenerateBodyToSubmit: function () {
        return {
            "id": 0,
            "presetValue": parseFloat(document.querySelector(".PresetValue").value),
            "name": document.querySelector(".Name").value,
            "lowerBoundInterest": parseFloat(document.querySelector(".LowerBoundInterest").value),
            "upperBoundInterest": parseFloat(document.querySelector(".UpperBoundInterest").value),
            "incrementalRate": parseFloat(document.querySelector(".IncrementalRate").value),
            "maturityYears": parseFloat(document.querySelector(".MaturityYears").value),
            "dateCreated": new Date(document.querySelector(".DateCreated").value)
        };
    },
    OnPreviewButtonClick: function () {
        if (!ProjectionFormPages.isHtmlFormReadyForApi(".UpperBoundInterest", ".LowerBoundInterest")) {
            return;
        }
        ProjectionFormPages.GeneratePreviewTableBody(ProjectionCreate.GenerateBodyToSubmit(), ".projection-tbl");

    },
    OnCreateButtonClick: function () {
        var caller = new Service();
        if (!ProjectionFormPages.isHtmlFormReadyForApi(".UpperBoundInterest", ".LowerBoundInterest")) {
            return;
        }
        caller.postWithData(AppUrl + "api/ProjectionForm", ProjectionCreate.GenerateBodyToSubmit(), function (data) {
            if (data) {
                window.location.href = '/ProjectionForm';
            }
            else {
                DomExtension.ShowModal("Form was not Saved", "Error ");
            }

        }, function (e) {
            DomExtension.ShowModal("Form was not Saved", "Error " + e);
        });
    },
    AddClickEventToPreviewBtn: function () {
        document.querySelector(".projection-tbl").classList.add("d-none");
        var btnPreview = document.querySelector(".btn-preview");
        btnPreview.addEventListener('click', function () {
            ProjectionCreate.OnPreviewButtonClick();
        });
    },
    AddClickEventToCreateBtn: function () {
        let btnCreate = document.querySelector(".btn-create");
        btnCreate.addEventListener('click', function () {
            ProjectionCreate.OnCreateButtonClick();
        });
    }
};
window.onload = (event) => {
    ProjectionCreate.AddClickEventToPreviewBtn();
    ProjectionCreate.AddClickEventToCreateBtn();
}