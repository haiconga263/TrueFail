import { Component, OnInit, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';

@Component({
  selector: 'app-root',
  templateUrl: '../../core/layout/template/default.html',
})
export class AdministratorComponent extends AppBaseComponent {

  constructor(
    injector: Injector,
  ) {
    super(injector);
  }
  
  ngOnInit() {
    super.ngOnInit();
  }
}
