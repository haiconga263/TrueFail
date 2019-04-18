import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';

@Component({
  selector: 'material',
  templateUrl: '../../../core/layout/template/blank.html'
})
export class MaterialComponent extends AppBaseComponent {

  constructor(
    injector: Injector
  ) {
    super(injector);


  }

  ngOnInit() {
    super.ngOnInit();
  }
}
