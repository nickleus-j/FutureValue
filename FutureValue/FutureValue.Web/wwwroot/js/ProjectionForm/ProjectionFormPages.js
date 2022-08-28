var ProjectionFormPages = {
    isHtmlFormReadyForApi: function (UpperBoundInterestSelector, LowerBoundInterestSelctor){
        if (document.querySelector(".input-validation-error") || document.querySelector(".text-danger span")) {
            DomExtension.ShowModal("Error", "Correct Validation Errors in Red");
            return false;
        }
        if (parseFloat(document.querySelector(UpperBoundInterestSelector).value) < parseFloat(document.querySelector(LowerBoundInterestSelctor).value)) {
            DomExtension.ShowModal("Error", "Upper Bound value  must never be lower than lower bound value");
            return false;
        }
        return true;
    },
    GeneratePreviewTableBody: function (apiBody, tableSelector) {
        var caller = new Service();
        caller.postWithData(AppUrl + "api/Projection", apiBody, function (data) {
            if (data == undefined || data == null) {
                DomExtension.ShowModal("Form was not Saved", "Error ");
            }
            let tbl = document.querySelector(tableSelector);
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
    }
}