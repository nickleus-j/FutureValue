var ProjectionIndex = {
    OnPreviewButtonClick: function () {
        var caller = new Service();
        if (document.querySelector(".input-validation-error") || document.querySelector(".text-danger span")) {
            return;
        }
        if (parseFloat(document.querySelector(".UpperBoundInterest").value) < parseFloat(document.querySelector(".LowerBoundInterest").value)) {
            DomExtension.ShowModal("Error", "Upper Bound value  must never be lower than lower bound value");
            return;
        }
        caller.postWithData(AppUrl + "api/Projection", {
            "id": 0,
            "presetValue": parseFloat(document.querySelector(".PresetValue").value),
            "name": "unnamed",
            "lowerBoundInterest": parseFloat(document.querySelector(".LowerBoundInterest").value),
            "upperBoundInterest": parseFloat(document.querySelector(".UpperBoundInterest").value),
            "incrementalRate": parseFloat(document.querySelector(".IncrementalRate").value),
            "maturityYears": parseFloat(document.querySelector(".MaturityYears").value),
            "dateCreated": "2022-08-24T23:25:36.345Z"
        }, function (data) {
            let panel = document.querySelector(".projection-panel");
            let tbl = document.querySelector(".projection-tbl");
            let tBody = tbl.querySelector("tbody");
            tbl.classList.remove("d-none");
            tBody.innerHTML = "";
            for (let i = 0; i < data.length; i++) {
                let row = document.createElement("tr");
                row.appendChild(DomExtension.MakeElement("td", "" + data[i].year));
                row.appendChild(DomExtension.MakeElement("td", "" + data[i].startValue));
                row.appendChild(DomExtension.MakeElement("td", "" + data[i].interestRate));
                row.appendChild(DomExtension.MakeElement("td", "" + data[i].futureValue));
                tBody.appendChild(row);
            }

        }, function (e) {
            DomExtension.ShowModal("Error in contacting Server", "Try again later "+e);
        });
    },
    OnEditButtonClick: function () {
        var caller = new Service();
        if (document.querySelector(".input-validation-error") || document.querySelector(".text-danger span")) {
            return;
        }
        if (parseFloat(document.querySelector(".UpperBoundInterest").value) < parseFloat(document.querySelector(".LowerBoundInterest").value)) {
            DomExtension.ShowModal("Error", "Upper Bound value  must never be lower than lower bound value");
            return;
        }
        caller.putWithData(AppUrl + "api/ProjectionForm/" + document.querySelector(".formId").value, {
            "id": parseInt(document.querySelector(".formId").value),
            "presetValue": parseFloat(document.querySelector(".PresetValue").value),
            "name": document.querySelector(".Name").value,
            "lowerBoundInterest": parseFloat(document.querySelector(".LowerBoundInterest").value),
            "upperBoundInterest": parseFloat(document.querySelector(".UpperBoundInterest").value),
            "incrementalRate": parseFloat(document.querySelector(".IncrementalRate").value),
            "maturityYears": parseFloat(document.querySelector(".MaturityYears").value),
            "dateCreated": new Date(document.querySelector(".DateCreated").value)
        }, function (data) {
            if (data) {
                window.location.href = '/ProjectionForm';
            }
            else {
                DomExtension.ShowModal("Form was not Saved", "Error ");
            }
        }, function (e) {
            console.log("error " + e);
            DomExtension.ShowModal("Edit was not Saved", "Error " + e);
        });
    },
    AddClickEventToPreviewBtn: function () {
        document.querySelector(".projection-tbl").classList.add("d-none");
        var btnPreview = document.querySelector(".btn-preview");
        btnPreview.addEventListener('click', function () {
            ProjectionIndex.OnPreviewButtonClick();
        });
    },
    AddClickEventToSaveBtn: function () {
        let btnCreate = document.querySelector(".btn-save");
        btnCreate.addEventListener('click', function () {
            ProjectionIndex.OnEditButtonClick();
        });
    }
};
window.onload = (event) => {
    ProjectionIndex.AddClickEventToPreviewBtn();
    ProjectionIndex.AddClickEventToSaveBtn();
}