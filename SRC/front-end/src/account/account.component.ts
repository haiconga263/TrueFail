import { Component, OnInit, Injector } from '@angular/core';
import { LayoutService } from 'angular-admin-lte';
import { BaseComponent } from 'src/core/basecommon/base.component';

@Component({
  selector: 'app-root',
  templateUrl: '../core/layout/template/blank.html',
})
export class AccountComponent extends BaseComponent {
  public customLayout: boolean;

  constructor(injector: Injector) {
    super(injector);
  }

  ngOnInit() {
  }
}
