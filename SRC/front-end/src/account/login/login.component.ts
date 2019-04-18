import { Component, Injector, Input, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { LoginService } from './login.service';
import { LoginModel, RoleModel } from './login.model';
import { BaseComponent } from 'src/core/basecommon/base.component';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { UrlConsts, AppConsts, ParamUrlKeys } from 'src/core/constant/AppConsts';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DeviceDetectorService } from 'ngx-device-detector';
import { stringify } from 'querystring';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent {
  @Input() VM: LoginModel;
  @Input() msgUsr: string = '';
  @Input() msgPwd: string = '';
  @Input() message: string = '';
  isLoading: boolean = false;
  isShowLoginBox: boolean = true;
  deviceInfo = null;
  roles: RoleModel[];

  constructor(injector: Injector,
    public loginSvc: LoginService,
    public router: Router,
    public authenticSvc: AuthenticService,
    public deviceService: DeviceDetectorService
  ) {
    super(injector);
    this.VM = new LoginModel();
    this.onKey = this.onKey.bind(this);
    this.onCheckDataValidation = this.onCheckDataValidation.bind(this);
    this.roles = [];
  }

  ngOnInit() {
    super.ngOnInit();

    this.VM.deviceInfo = JSON.stringify(this.deviceService.getDeviceInfo());
    this.VM.appName = "AriFEApp"

    if (this.authenticSvc.isAuthenticated()) {
      this.authenticSvc.logout(() => { });
    }
  }

  onKey(event: any) {
    if (event.keyCode == 13) {
      this.login();
    }
  }

  onCheckDataValidation(event: any, name: string = '') {
    let isPass = true;
    if (name == 'username' || name == '')
      if (this.VM.userName == '' || this.VM.userName == null) {
        this.msgUsr = this.lang.instant('Login.UsernameIsRequired');
        isPass = false;
      }
      else this.msgUsr = '';

    if (name == 'password' || name == '')
      if (this.VM.password == '' || this.VM.password == null) {
        this.msgPwd = this.lang.instant('Login.PasswordIsRequired');
        isPass = false;
      }
      else this.msgPwd = '';
    return isPass;
  }

  login() {
    if (!this.onCheckDataValidation(null))
      return;
    this.isLoading = true;
    this.loginSvc.login(this.VM).subscribe((result) => {
      this.isLoading = false;
      let lang = this.langCurrent;
      if (result.result == ResultCode.Success) {
        this.authenticSvc.saveSession(result.data);
        this.roles = [];

        for (var i = 0; i < AppConsts.appMappings.length; i++) {
          for (let j = 0; j < this.authenticSvc.getSession().roles.length; j++) {
            let role = this.authenticSvc.getSession().roles[j];
            if (role == null)
              this.router.navigate([UrlConsts.url403Page]);
            else role = role.toLowerCase();

            if (AppConsts.appMappings[i]["from"].toLowerCase() == role) {
              let url = `${AppConsts.appMappings[i]["to"]}/${UrlConsts.urlAuthenticate}?${ParamUrlKeys.accesstoken}=${encodeURIComponent(this.authenticSvc.getAccessToken())}&${ParamUrlKeys.lang}=${lang}`;
              this.roles.push(new RoleModel({
                url: url,
                name: (AppConsts.appMappings[i]["from"])
              }));
            }
          }
        }

        if (this.roles.length == 0)
          this.message = this.lang.instant('Login.ThisAccountNotPermission');
        else if (this.roles.length == 1)
          window.open(this.roles[0].url, '_self');
        else {
          this.isShowLoginBox = false;
        }

      }
      else {
        this.message = result.errorMessage;
      }
    }, (error) => {
      this.isLoading = false;
      this.message = this.lang.instant('Login.SystemNotResponding');
    });
  }
}

