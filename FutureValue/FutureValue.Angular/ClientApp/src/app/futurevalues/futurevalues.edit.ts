import { Component, Input, Output, Pipe, PipeTransform, ElementRef, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { projectionForm } from './projectionform';
import { AppSettings } from './AppSettings';
import { ProjectionYear } from './projectionYear';
import { FutureValuesUiRoutines } from './futurevalues.UiRoutines'

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
  
  onPreViewClick() {
    FutureValuesUiRoutines.RegeneratePreviewTable(this.pForm, this.http, this.ElByClassName);
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
    if (!FutureValuesUiRoutines.isHtmlFormReadyForApi('' + pForm.upperBoundInterest, '' + pForm.lowerBoundInterest)) {
      alert("Upper Bound value  must never be lower than lower bound value");
      return;
    }
    this.http.put(this.Settings.AppUrl + 'api/ProjectionForm/' + pForm.id, toSend).subscribe(
      {
        next: (response) => this.router.navigate(['/fv']),
        error: (error) => alert(error.error.title),
      });
  }
}
