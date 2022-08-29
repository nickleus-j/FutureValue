import { Component, Input, Output, Pipe, PipeTransform, OnInit, Injectable } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { projectionForm } from './projectionform';
import { Observable, of } from 'rxjs';
import { AppSettings } from './AppSettings';

@Injectable({ providedIn: 'root' })
@Component({
  selector: 'fv-index',
  templateUrl: './futurevalues.index.html',

})
export class FutureValuesIndex implements OnInit {
  forms: projectionForm[] = [];
  constructor(private http: HttpClient, private router: Router) { }
  Settings: AppSettings = new AppSettings();
  ngOnInit(): void {
    this.http.get<projectionForm[]>(this.Settings.AppUrl + 'api/ProjectionForm').subscribe(data => {
      this.forms = data;
    })
  }
  onDelete(index: number) {
    this.http.delete<projectionForm>(this.Settings.AppUrl + 'api/ProjectionForm/' + index).subscribe(data => {
      this.router.navigate(['/fv']);
    })
  }
}
