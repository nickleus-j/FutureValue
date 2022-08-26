import { Component, Input, Output, Pipe, PipeTransform, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { projectionForm } from './projectionform';
import { Observable, of } from 'rxjs';
import { AppSettings } from './AppSettings';
@Component({
  selector: 'fv-detail',
  templateUrl: './futurevalues.details.html',

})
export class FutureValuesDetails implements OnInit {
  pForm: projectionForm = new projectionForm();
  @Input() formId: number = 1;
  constructor(private http: HttpClient, private route: ActivatedRoute) {

  }
  Settings: AppSettings = new AppSettings();
  ngOnInit(): void {
    if (this.route.snapshot.params['id'])
      this.formId = Number(this.route.snapshot.params['id']);
    else this.formId = 1;
    this.http.get<projectionForm>(this.Settings.AppUrl + 'api/ProjectionForm/' + this.formId).subscribe(data => {
      this.pForm = data;
    })
  }
}
