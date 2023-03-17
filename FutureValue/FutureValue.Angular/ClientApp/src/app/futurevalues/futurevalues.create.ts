import { Component, Input, Output, Pipe, PipeTransform, ElementRef, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { projectionForm } from './projectionform';
import { Observable, of } from 'rxjs';
import { AppSettings } from './AppSettings';
import { ProjectionYear } from './projectionYear';
import { FutureValuesUiRoutines } from './futurevalues.UiRoutines';
@Component({
  selector: 'fv-create',
  templateUrl: './futurevalues.create.html',

})
export class FutureValuesCreate implements OnInit {
  pForm: projectionForm = new projectionForm();
  headText: string = "Create";
  directionsText: string = "Add details here";
  @Input() formId: number = 0;
  constructor(private http: HttpClient, private route: ActivatedRoute, private ElByClassName: ElementRef, private router: Router) {
    this.pForm.id = 0;
    this.pForm.incrementalRate = 0;
    this.pForm.name = "Unnamed";
    this.pForm.presetValue = 0;
    this.pForm.projections = [];
    this.pForm.lowerBoundInterest = 0;
    this.pForm.upperBoundInterest = 0;
  }
  Settings: AppSettings = new AppSettings();
  ngOnInit(): void {

  }

  onPreViewClick() {
    FutureValuesUiRoutines.RegeneratePreviewTable(this.pForm, this.http, this.ElByClassName);
  }
  onCreateClick() {
    var pForm = this.pForm;
    let toSend = {
      id: pForm.id,
      presetValue: pForm.presetValue,
      name: pForm.name,
      lowerBoundInterest: pForm.lowerBoundInterest,
      upperBoundInterest: pForm.upperBoundInterest,
      incrementalRate: pForm.incrementalRate,
      maturityYears: pForm.maturityYears,
    }
    if (pForm.upperBoundInterest < pForm.lowerBoundInterest) {
      alert("Upper Bound value  must never be lower than lower bound value");
      return;
    }
    this.http.post(this.Settings.AppUrl + 'api/ProjectionForm/', toSend).subscribe(
      {
        next: (response) => this.router.navigate(['/fv']),
        error: (error) => alert(error.error.title),
      });
  }

}
