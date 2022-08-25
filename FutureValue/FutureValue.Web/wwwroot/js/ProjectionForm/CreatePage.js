var ProjectionCreate = {
    OnPreviewButtonClick: function () {
        var caller = new Service();
        if (document.querySelector(".input-validation-error")) {
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
            console.log("error " + e);
        });
    },
    OnCreateButtonClick: function () {
        var caller = new Service();
        if (document.querySelector(".input-validation-error")) {
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
            window.location.href = '/ProjectionForm';

        }, function (e) {
            console.log("error " + e);
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