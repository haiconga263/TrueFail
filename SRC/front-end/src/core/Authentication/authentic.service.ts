import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CookieKeys, AppConsts } from '../constant/AppConsts';
import { CookieService } from '../common/cookie.service';
import { ResultModel } from '../models/http.model';
import { FuncHelper } from '../helpers/function-helper';
import { ConfigService } from '../common/config.service';
import { Config } from 'protractor';
import { LanguageService } from '../common/language.service';
import { TranslateService } from '@ngx-translate/core';
import { ResultCode } from '../constant/AppEnums';

export class Session {
  accessToken: string = '';
  id: number = 0;
  userName: string = '';
  email: string = '';
  roles: Array<string> = [];

  constructor() {
    this.accessToken = '';
    this.userName = '';
    this.email = '';
    this.roles = [];
  }
}

@Injectable({
  providedIn: 'root'
})
export class AuthenticService {

  private urlCheckAccessToken: string = '/api/User/checklogin'
  private urlLogout: string = '/api/User/logout'

  constructor(private cookieSvc: CookieService,
    private http: HttpClient,
    private configSvc: ConfigService,
    private translate: TranslateService
  ) {
  }

  isAuthenticated(): boolean {
    return this.cookieSvc.check(CookieKeys.userinfo);
  }

  public getHttpHeader(): { headers: HttpHeaders } {
    let lang = AppConsts.defaultLang;
    if (this.cookieSvc.check(CookieKeys.lang))
      lang = this.cookieSvc.get(CookieKeys.lang);

    if (lang != this.translate.getBrowserLang()) {
      lang = this.translate.getBrowserLang();
      this.cookieSvc.set(CookieKeys.lang, lang);
    }

    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, DELETE, PUT',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'X-Requested-With, Content-Type, Origin, Authorization, Accept, Client-Security-Token, Accept-Encoding',
        'AccessToken': this.getAccessToken(),
        'lang': lang
      })
    };
    return httpOptions;
  }

  logout(callback: () => void) {
    this.configSvc.pushEvent(() => {
      this.http.post<ResultModel<Session>>(`${AppConsts.remoteServiceBaseUrl}${this.urlLogout}`, {}, this.getHttpHeader())
        .subscribe(
          (result) => { this.removeSession(); },
          (error) => { this.removeSession(); });
      callback();
    });
  }

  removeSession() {
    this.cookieSvc.delete(CookieKeys.userinfo)
  }

  saveSession(user: Session) {
    this.cookieSvc.delete(CookieKeys.userinfo)
    this.cookieSvc.set(CookieKeys.userinfo, JSON.stringify(user));
  }

  getSession(): Session {
    let rs: Session;
    if (this.isAuthenticated())
      rs = JSON.parse(this.cookieSvc.get('userinfo'));
    if (rs == null)
      rs = new Session();
    return rs;
  }

  getAccessToken(): string {
    return this.getSession().accessToken;
  }

  checkAccessToken(callback: (result: ResultModel<Session>) => void): void {
    if (this.isAuthenticated())
      this.setAccessToken(this.getAccessToken(), callback);
    else {
      let session: ResultModel<Session> = { data: null, result: -2, errorMessage: 'You signed out' };
      callback(session);
    }
  }

  setAccessToken(token: string, callback: (result: ResultModel<Session>) => void): void {
    this.configSvc.pushEvent(() => {
      this.http.get<ResultModel<Session>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCheckAccessToken}?accessToken=${encodeURIComponent(token)}`)
        .subscribe((result) => {
          if (result.data != null)
            this.saveSession(result.data);
          callback(result);
        }, (error) => {
          callback(new ResultModel<Session>({
            data: null,
            errorMessage: error.code,
            result: ResultCode.error404
          }));
        });
    });
  }
}
