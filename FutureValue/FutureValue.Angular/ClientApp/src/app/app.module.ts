import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FutureValuesProjectionTable } from './futurevalues/futurevalues.ProjectionTable';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { FutureValuesIndex } from './futurevalues/futurevalues.index';
import { FutureValuesDetails } from "./futurevalues/futurevalues.details"
import { FutureValuesCreate } from './futurevalues/futurevalues.create';
import { FutureValuesEdit } from './futurevalues/futurevalues.edit';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,  FutureValuesProjectionTable,
    FetchDataComponent, FutureValuesIndex, FutureValuesDetails
    , FutureValuesCreate, FutureValuesEdit
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule, 
    RouterModule.forRoot([
      { path: '', component: FutureValuesIndex, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'fv', component: FutureValuesIndex },
      { path: 'fv/:id', component: FutureValuesDetails },
      { path: 'fvcreate', component: FutureValuesCreate },
      { path: 'fvedit/:id', component: FutureValuesEdit },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent, FutureValuesProjectionTable]
})
export class AppModule { }
