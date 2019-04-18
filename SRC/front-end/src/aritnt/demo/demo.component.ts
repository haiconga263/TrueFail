import { Component, OnInit, Injector } from '@angular/core';
import { LayoutService } from 'angular-admin-lte';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { LayoutStore } from 'angular-admin-lte';
import { adminLteConf } from './admin-lte.conf';

@Component({
  selector: 'app-root',
  templateUrl: '../../core/layout/template/default.html',
})
export class DemoComponent extends AppBaseComponent {

  private layoutStore: LayoutStore;

  constructor(
    injector: Injector,
  ) {
    super(injector);
    this.layoutStore = injector.get(LayoutStore);
  }

  ngOnInit() {
    super.ngOnInit();

    let session = this.authenticSvc.getSession();

    if (session.roles.find(x => x == 'administrator' != null)) {
      this.layoutStore.setSidebarLeftMenu(adminLteConf.sidebarLeftMenu_2);
    }
    else {
      this.layoutStore.setSidebarLeftMenu(adminLteConf.sidebarLeftMenu);

    }
  }
}
