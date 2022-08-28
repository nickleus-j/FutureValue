var ProjectionCreate = {
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
            id: 0,
            "presetValue": parseFloat(document.querySelector(".PresetValue").value),
            "name": "unnamed",
            "lowerBoundInterest": parseFloat(document.querySelector(".LowerBoundInterest").value),
            "upperBoundInterest": parseFloat(document.querySelector(".UpperBoundInterest").value),
            "incrementalRate": parseFloat(document.querySelector(".IncrementalRate").value),
            "maturityYears": parseFloat(document.querySelector(".MaturityYears").value),
            "dateCreated": "2022-08-24T23:25:36.345Z"
        }, function (data) {
            if (data == undefined || data == null) {
                DomExtension.ShowModal("Form was not Saved", "Error ");
            }
            let tbl = document.querySelector(".projection-tbl");
            let tBody = tbl.querySelector("tbody");
            tbl.classList.remove("d-none");
            tBody.innerHTML = "";
            for (let i = 0; i < data.length; i++) {
                let row = document.createElement("tr");
                row.appendChild(DomExtension.MakeElement("td", "" + data[i].year));
                row.appendChild(DomExtension.MakeElement("td", "" + data[i].startValue.toFixed(2)));
                row.appendChild(DomExtension.MakeElement("td", "" + data[i].interestRate.toFixed(2)));
                row.appendChild(DomExtension.MakeElement("td", "" + data[i].futureValue.toFixed(2)));
                tBody.appendChild(row);
            }

        }, function (e) {
            DomExtension.ShowModal("Error in contacting Server", "Try again later " + e);
        });
    },
    OnCreateButtonClick: function () {
        var caller = new Service();
        if (document.querySelector(".input-validation-error") || document.querySelector(".text-danger span")) {
            return;
        }
        if (parseFloat(document.querySelector(".UpperBoundInterest").value) < parseFloat(document.querySelector(".LowerBoundInterest").value)) {
            DomExtension.ShowModal("Error", "Upper Bound value  must never be lower than lower bound value");
            return;
        }
        caller.postWithData(AppUrl + "api/ProjectionForm", {
            id: 0,
            "presetValue": parseFloat(document.querySelector(".PresetValue").value),
            "name": document.querySelector(".Name").value,
            "lowerBoundInterest": parseFloat(document.querySelector(".LowerBoundInterest").value),
            "upperBoundInterest": parseFloat(document.querySelector(".UpperBoundInterest").value),
            "incrementalRate": parseFloat(document.querySelector(".IncrementalRate").value),
            "maturityYears": parseFloat(document.querySelector(".MaturityYears").value),
            "dateCreated": "2022-08-24T23:25:36.345Z"
        }, function (data) {
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