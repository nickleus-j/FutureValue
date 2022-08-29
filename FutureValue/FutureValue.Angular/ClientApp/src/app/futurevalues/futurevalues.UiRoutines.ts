import { Component, ElementRef } from '@angular/core';
import { AppSettings } from './AppSettings';
import { ProjectionYear } from './projectionYear';
import { projectionForm } from './projectionform';
import { HttpClient } from '@angular/common/http';
export class FutureValuesUiRoutines {
  public static isHtmlFormReadyForApi(UpperBoundInterest: string, LowerBoundInterest: string) {
    return !(parseFloat(UpperBoundInterest) < parseFloat(LowerBoundInterest));
  }
  public static MakeElement(tagName: string, text: string) {
    let elem = document.createElement(tagName);
    elem.append(text);
    return elem;
  }
  public static updateTable(response: ProjectionYear[], ElByClassName: ElementRef) {

    let tbl = (<HTMLElement>ElByClassName.nativeElement).querySelector(".projection-tbl");
    let tBody = (<HTMLElement>ElByClassName.nativeElement).querySelector(".projection-tbl tbody");
    if (tbl && tBody) {
      tbl.classList.remove("d-none");
      tBody.innerHTML = "";
      for (let i = 0; i < response.length; i++) {
        let row = document.createElement("tr");
        row.appendChild(this.MakeElement("td", "" + response[i].year));
        row.appendChild(this.MakeElement("td", "" + response[i].startValue.toFixed(2)));
        row.appendChild(this.MakeElement("td", "" + response[i].interestRate.toFixed(2)));
        row.appendChild(this.MakeElement("td", "" + response[i].futureValue.toFixed(2)));
        tBody.appendChild(row);
      }
    }
  }
  public static RegeneratePreviewTable(pForm: projectionForm, http: HttpClient, ElByClassName: ElementRef) {
    let Settings: AppSettings = new AppSettings();
    let toSend = {
      id: pForm.id,
      presetValue: pForm.presetValue,
      name: pForm.name,
      lowerBoundInterest: pForm.lowerBoundInterest,
      upperBoundInterest: pForm.upperBoundInterest,
      incrementalRate: pForm.incrementalRate,
      maturityYears: pForm.maturityYears,
      dateCreated: pForm.dateCreated
    }
    if (!FutureValuesUiRoutines.isHtmlFormReadyForApi('' + pForm.upperBoundInterest, '' + pForm.lowerBoundInterest)) {
      alert("Upper Bound value  must never be lower than lower bound value");
      return;
    }
    http.post(Settings.AppUrl + 'api/Projection/', toSend).subscribe(
      {
        next: (response) => {
          pForm.projections = response as ProjectionYear[];
          FutureValuesUiRoutines.updateTable(response as ProjectionYear[], ElByClassName)
        },
        error: (error) => alert(error.error.title),
      });
  }
}
