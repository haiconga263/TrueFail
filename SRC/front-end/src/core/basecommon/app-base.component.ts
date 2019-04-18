import { OnInit, Input, Injector, Injectable, inject } from '@angular/core';
import { Router, NavigationEnd, ActivationStart, RouterEvent } from '@angular/router';
import { AuthenticService, Session } from '../Authentication/authentic.service';
import { TranslateService } from '@ngx-translate/core';
import { FuncHelper } from '../helpers/function-helper';
import { LanguageService } from '../common/language.service';
import { AppConsts, UrlConsts, ParamUrlKeys } from '../constant/AppConsts';
import { ResultCode, Modes } from '../constant/AppEnums';
import { BaseComponent } from './base.component';
import { ServerDisconnectedService } from '../layout/server-disconnected/server-disconnected.service';
import { WaitingSyncHelper, WaitingSyncKeys } from '../helpers/waiting-sync-helper';
import { ErrorAlert, InforAlert } from '../alert/alert.service';
import { ResultModel } from '../models/http.model';
import { LoginService } from 'src/account/login/login.service';
import { LoginModel } from 'src/account/login/login.model';
import { Title } from '@angular/platform-browser';
import { LayoutService } from 'angular-admin-lte';

@Injectable()
export abstract class AppBaseComponent extends BaseComponent {
  private loginSvc: LoginService;
  public layoutService: LayoutService;

  public router: Router;
  public authenticSvc: AuthenticService;
  public titleService: Title;
  public serverDisconnectedSvc: ServerDisconnectedService;
  public navigationSubscription;

  public isCustomLayout: boolean;

  constructor(injector: Injector) {
    super(injector);
    this.router = injector.get(Router);
    this.authenticSvc = injector.get(AuthenticService);
    this.serverDisconnectedSvc = injector.get(ServerDisconnectedService);
    this.loginSvc = injector.get(LoginService);
    this.titleService = injector.get(Title);
    this.layoutService = injector.get(LayoutService);

    this.navigationSubscription = this.router.events.subscribe((e: any) => {
      if (e instanceof NavigationEnd) {
        if (WaitingSyncHelper.canExcute(WaitingSyncKeys.initAppBaseComponent)) {
          this.checkPermision();
        }

        this.configSvc.pushEvent(() => {
          let title = this.titleService.getTitle();
          title = title.replace(" - " + AppConsts.appName, "");
          title = title.replace(" -", "");
          this.titleService.setTitle(title + " - " + AppConsts.appName);
        });
      }
    });

    this.router.events.subscribe((event: RouterEvent) => {
      if (event instanceof ActivationStart) {
        this.isCustomLayout = !!event.snapshot.data.customLayout;
      }
    });
  }

  logout() {
    this.authenticSvc.logout(() => {
      if (AppConsts.mode == Modes.development) {
        this.router.navigate([UrlConsts.urlHome]);
      }
      else {
        window.open(`${AppConsts.appAccountUrl}/${UrlConsts.urlLogout}`, '_self');
      }
    });
  }

  checkPermision() {
    let url = this.router.url.toLowerCase();
    if (url[0] == '/') {
      url = url.substring(1);
    }

    if (!url.startsWith(UrlConsts.urlAuthenticate))
      this.authenticSvc.checkAccessToken((result) => {
        if (result.result < 0) {
          if (result.result < -10 && FuncHelper.isFunction(this.serverDisconnectedSvc.changedStatus)) {
            this.serverDisconnectedSvc.changedStatus(true);
          }
          else {
            if (AppConsts.mode == Modes.development)
              this.loginDefault();
            else {
              this.alertSvc.alert(new ErrorAlert({ text: 'Your login session has expired, please login again!' }))
                .then(() => {
                  this.logout();
                });
            }
          }
        } else {
          let roles = this.authenticSvc.getSession().roles;
          let isAllowed = false;
          for (var i = 0; i < AppConsts.rolesAllowed.length; i++) {
            for (var j = 0; j < roles.length; j++) {
              if (AppConsts.rolesAllowed[i].toLowerCase() == roles[j].toLowerCase()) {
                isAllowed = true;
                break;
              }
            }
            if (isAllowed) break;
          }

          if (!isAllowed) {
            this.alertSvc.alert(new ErrorAlert({ text: 'You do not have access to the system, please contact the administrator for assistance!' }))
              .then(() => {
                this.logout();
              });
          }
        }
      });
  }

  loginDefault() {
    if (AppConsts.mode != Modes.development)
      return;

    let model = new LoginModel({
      userName: AppConsts.usernameDev,
      password: AppConsts.passwordDev
    });

    this.loginSvc.login(model).subscribe((result) => {
      if (result.result == ResultCode.Success) {
        let param = {};
        param[ParamUrlKeys.accesstoken] = encodeURIComponent(result.data.accessToken);
        param[ParamUrlKeys.lang] = AppConsts.defaultLang;
        this.router.navigate([UrlConsts.urlAuthenticate], { queryParams: param });
      }
      else {
        this.alertSvc.alert(new ErrorAlert({
          title: 'Login defaut fail',
          text: 'Please check Api GateWay, server Database and try again! (AriFE App is running in the development mode)'
        }));
      }
    }, (error) => {
    });
  }

  ngOnInit() {
    super.ngOnInit();
    this.layoutService.isCustomLayout.subscribe((value: boolean) => {
      this.isCustomLayout = value;
    });
  }
}
