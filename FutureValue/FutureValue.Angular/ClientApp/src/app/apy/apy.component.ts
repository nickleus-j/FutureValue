import { Component } from '@angular/core';

@Component({
  selector: 'app-apy',
  templateUrl: './apy.component.html'
})
export class ApyComponent {
  Rate: number = 1;
  PercentageRate: number = this.Rate / 100;
  NCompound: number = 1;
  Result: number = this.NCompound != 0 ? this.PercentageRate * this.NCompound: 0;
  changeCompound() {
    this.NCompound = this.NCompound + 1;
  }
}
