import { Component, OnInit, Input, Injector } from '@angular/core';
import { LoginService } from '../login/login.service';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/core/basecommon/base.component';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { UrlConsts, AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent extends BaseComponent {
  @Input() AppName: string = AppConsts.appName;

  constructor(injector: Injector,
    public loginSvc: LoginService,
    public router: Router,
    public authenticSvc: AuthenticService
  ) {
    super(injector);
  }

  ngOnInit() {
    if (this.authenticSvc.isAuthenticated()) {
      this.router.navigate([UrlConsts.urlHome]);
    }
  }

}
