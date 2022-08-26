import { Component, Input, Output, Pipe, PipeTransform, ElementRef, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { projectionForm } from './projectionform';
import { AppSettings } from './AppSettings';
import { ProjectionYear } from './projectionYear';
@Component({
  selector: 'fv-edit',
  templateUrl: './futurevalues.edit.html',

})
export class FutureValuesEdit implements OnInit {
  pForm: projectionForm = new projectionForm();
  headText: string = "Edit";
  @Input() formId: number = 0;
  Settings: AppSettings = new AppSettings();
  constructor(private http: HttpClient, private route: ActivatedRoute, private ElByClassName: ElementRef, private router: Router) {
    this.pForm.id = 0;
    this.pForm.incrementalRate = 0;
    this.pForm.name = "Unnamed";
    this.pForm.presetValue = 0;
    this.pForm.projections = [];
    this.pForm.lowerBoundInterest = 0;
    this.pForm.upperBoundInterest = 0;
    this.pForm.dateCreated = new Date();
  }
  ngOnInit(): void {
    if (this.route.snapshot.params['id'])
      this.formId = Number(this.route.snapshot.params['id']);
    else this.formId = 1;
    this.http.get<projectionForm>(this.Settings.AppUrl + 'api/ProjectionForm/' + this.formId).subscribe(data => {
      this.pForm = data;
    });
  }
  MakeElement(tagName: string, text: string) {
    let elem = document.createElement(tagName);
    elem.append(text);
    return elem;
  }
  updateTable(response: ProjectionYear[]) {
    this.pForm.projections = response as ProjectionYear[];
    let tbl = (<HTMLElement>this.ElByClassName.nativeElement).querySelector(".projection-tbl");
    let tBody = (<HTMLElement>this.ElByClassName.nativeElement).querySelector(".projection-tbl tbody");
    if (tbl && tBody) {
      tbl.classList.remove("d-none");
      tBody.innerHTML = "";
      for (let i = 0; i < response.length; i++) {
        let row = document.createElement("tr");
        row.appendChild(this.MakeElement("td", "" + response[i].year));
        row.appendChild(this.MakeElement("td", "" + response[i].startValue));
        row.appendChild(this.MakeElement("td", "" + response[i].interestRate));
        row.appendChild(this.MakeElement("td", "" + response[i].futureValue));
        tBody.appendChild(row);
      }
    }

  }
  onPreViewClick() {
    var pForm = this.pForm;
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
    this.http.post(this.Settings.AppUrl + 'api/Projection/', toSend).subscribe(
      {
        next: (response) => this.updateTable(response as ProjectionYear[]),
        error: (error) => console.log(error),
      })
  }
  onSaveClick() {
    var pForm = this.pForm;
    let toSend = {
      id: pForm.id,
      presetValue: pForm.presetValue,
      name: pForm.name,
      lowerBoundInterest: pForm.lowerBoundInterest,
      upperBoundInterest: pForm.upperBoundInterest,
      incrementalRate: pForm.incrementalRate,
      maturityYears: pForm.maturityYears,
      dateCreated: new Date(pForm.dateCreated)
    }
    this.http.put(this.Settings.AppUrl + 'api/ProjectionForm/' + pForm.id, toSend).subscribe(
      {
        next: (response) => this.router.navigate(['/fv']),
        error: (error) => console.log(error),
      });
  }
}
