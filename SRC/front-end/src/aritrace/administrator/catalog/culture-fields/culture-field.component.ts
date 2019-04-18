import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';

@Component({
  selector: 'cultureField',
  templateUrl: '../../../../core/layout/template/blank.html'
})
export class CultureFieldComponent extends AppBaseComponent {

  constructor(
    injector: Injector
  ) {
    super(injector);


  }

  ngOnInit() {
    super.ngOnInit();
  }
}
