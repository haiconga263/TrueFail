import { Component, Input, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { UrlConsts, AppConsts } from 'src/core/constant/AppConsts';
import { inject } from '@angular/core/testing';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';

@Component({
  selector: 'app-header-inner',
  templateUrl: './header-inner.component.html'
})
export class HeaderInnerComponent extends AppBaseComponent {
  @Input() Username: string;

  constructor(injector: Injector,
    public authenticSvc: AuthenticService
  ) {
    super(injector);
    this.Username = authenticSvc.getSession().userName;
  }

  ngOnInit() {
  }
}
