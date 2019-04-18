import { Component, OnInit, Input, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from 'src/core/basecommon/base.component';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { UrlConsts, AppConsts, ParamUrlKeys } from 'src/core/constant/AppConsts';
import { FuncHelper } from '../helpers/function-helper';
import { ErrorAlert } from '../alert/alert.service';
import { Modes } from '../constant/AppEnums';

@Component({
  selector: 'app-authentication',
  templateUrl: '../layout/template/blank.html'
})
export class AuthenticationComponent extends BaseComponent {
  @Input() AppName: string = AppConsts.appName;

  constructor(injector: Injector,
    public router: Router,
    private activatedRoute: ActivatedRoute,
    public authenticSvc: AuthenticService
  ) {
    super(injector);
  }

  ngOnInit() {
    super.ngOnInit();
    this.activatedRoute.queryParams.subscribe(params => {
      let token = decodeURIComponent(params[ParamUrlKeys.accesstoken]);
      let lang = params[ParamUrlKeys.lang];

      this.authenticSvc.setAccessToken(token, (result) => {
        if (result.result < 0) {
          if (AppConsts.mode == Modes.development) {
            this.alertSvc.alert(new ErrorAlert({ text: 'Please check Api GateWay, server Database and try again! (AriFE App is running in the development mode)' }))
              .then(() => {
                this.router.navigate(['/']);
              });
          } else {
            this.alertSvc.alert(new ErrorAlert({ text: 'Account authentication error, please contact the administrator for assistance!' }))
              .then(() => {
                window.open(`${AppConsts.appAccountUrl}/${UrlConsts.urlLogout}`, '_self');
              });
          }
        }
        else {
          this.router.navigate([UrlConsts.urlHome]);
          if (!FuncHelper.isNull(lang)) this.useLanguage(lang);
        }
      });
    });
  }

}
