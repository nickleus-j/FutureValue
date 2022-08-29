var ProjectionEdit = {
    GenerateBodyToSubmit: function () {
        return {
            "id": parseInt(document.querySelector(".formId").value),
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
        ProjectionFormPages.GeneratePreviewTableBody(ProjectionEdit.GenerateBodyToSubmit(), ".projection-tbl");
        
    },
    OnEditButtonClick: function () {
        var caller = new Service();
        if (!ProjectionFormPages.isHtmlFormReadyForApi(".UpperBoundInterest", ".LowerBoundInterest")) {
            return;
        }
        caller.putWithData(AppUrl + "api/ProjectionForm/" + document.querySelector(".formId").value, ProjectionEdit.GenerateBodyToSubmit() , function (data) {
            if (data) {
                window.location.href = '/ProjectionForm';
            }
            else {
                DomExtension.ShowModal("Form was not Saved", "Error ");
            }
        }, function (e) {
            DomExtension.ShowModal("Edit was not Saved", "Error " + e);
        });
    },
    AddClickEventToPreviewBtn: function () {
        document.querySelector(".projection-tbl").classList.add("d-none");
        var btnPreview = document.querySelector(".btn-preview");
        btnPreview.addEventListener('click', function () {
            ProjectionEdit.OnPreviewButtonClick();
        });
    },
    AddClickEventToSaveBtn: function () {
        let btnCreate = document.querySelector(".btn-save");
        btnCreate.addEventListener('click', function () {
            ProjectionEdit.OnEditButtonClick();
        });
    }
};
window.onload = (event) => {
    ProjectionEdit.AddClickEventToPreviewBtn();
    ProjectionEdit.AddClickEventToSaveBtn();
}