import { Component,Input,Output } from '@angular/core';

@Component({
  selector: 'app-apy',
  templateUrl: './apy.component.html'
})
export class ApyComponent {
  @Input() Rate: number = 1;
  @Input() PercentageRate: number = this.Rate / 100;
  @Input() NCompound: number = 1;
  @Input() Result: number = this.NCompound != 0 ? Math.pow((1 + this.PercentageRate), this.NCompound)-1 : 0;
  changeCompound() {
    this.PercentageRate = this.Rate / 100;
    this.Result = this.NCompound != 0 ? Math.pow((1+this.PercentageRate), this.NCompound)-1 : 0;
  }
}
