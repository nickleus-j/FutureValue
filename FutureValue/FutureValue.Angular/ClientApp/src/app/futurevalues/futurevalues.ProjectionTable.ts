import { Component, Input, Output, OnInit } from '@angular/core';
import { ProjectionYear } from './projectionYear';

@Component({
  selector: 'fv-projections',
  templateUrl: './futurevalues.ProjectionTable.html',

})

export class FutureValuesProjectionTable implements OnInit {
  @Input() data: ProjectionYear[] = [];
  caption: string = "Values";
  ngOnInit(): void { }
}
