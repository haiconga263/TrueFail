import { Component, OnInit, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { UrlConsts } from 'src/core/constant/AppConsts';
import { BaseComponent } from 'src/core/basecommon/base.component';

@Component({
  selector: 'logout',
  templateUrl: './logout.component.html'
})
export class LogoutComponent extends BaseComponent {
  constructor(injector: Injector,
    private router: Router,
    private authenticSvc: AuthenticService
  ) {
    super(injector);
    if (authenticSvc.isAuthenticated()) {
      this.authenticSvc.logout(() => {
        this.router.navigate([UrlConsts.urlLogin]);
      });
    }
    else {
      this.router.navigate([UrlConsts.urlLogin]);
    }
  }

  ngOnInit() {

  }
}
