import { Injectable } from '@angular/core';
import { TranslateService, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient } from '@angular/common/http';
import { CookieService } from './cookie.service';
import { CookieKeys, AppConsts } from '../constant/AppConsts';
import { Observable } from 'rxjs';
import { AuthenticService } from '../Authentication/authentic.service';
import { ResultModel } from '../models/http.model';
import { FuncHelper } from '../helpers/function-helper';
import { WaitingSyncHelper, WaitingSyncKeys } from '../helpers/waiting-sync-helper';
import { ConfigService } from './config.service';

export class Language {
  id: number = 0;
  code: string;
  name: string;
  description: string;
  classIcon: string;
  countryId: number;
  isUsed: boolean;
  createdBy: number;
  createdDate: Date;
  modifiedBy: number;
  modifiedDate: Date;

  public constructor(init?: Partial<Language>) {
    Object.assign(this, init);
  }
}

@Injectable({
  providedIn: 'root'
})

export class LanguageService {
  private urlLanguages: string = '/api/Language/gets';
  private langChangedEvents: any[];

  constructor(private translateService: TranslateService,
    private cookieService: CookieService,
    private httpClient: HttpClient,
    private authenticService: AuthenticService,
    private configService: ConfigService
  ) {
    this.langChangedEvents = [];

    this.configService.pushEvent(() => {
      this.translateService.reloadLang(this.getLangCurrent());
    })
  }

  getLangCurrent(): string {
    let lang = AppConsts.defaultLang;
    if (this.cookieService.check(CookieKeys.lang))
      lang = this.cookieService.get(CookieKeys.lang);

    return lang;
  }

  pushEventLangChanged(event: (lang: string) => void) {
    this.langChangedEvents.push(event);
  }

  useLanguage(lang: string) {
    if (WaitingSyncHelper.canExcute(WaitingSyncKeys.initLanguage)) {
      this.cookieService.set(CookieKeys.lang, lang);
      for (var i = 0; i < this.langChangedEvents.length; i++) {
        if (FuncHelper.isFunction(this.langChangedEvents[i]))
          this.langChangedEvents[i](lang);
      }

      return this.translateService.use(lang).toPromise();
    }
    else {
      return Promise.resolve();
    }
  }

  getLanguages(): Observable<ResultModel<Language[]>> {
    return this.httpClient.get<ResultModel<Language[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlLanguages}`, this.authenticService.getHttpHeader());
  }
}

export function TranslateHttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, AppConsts.languageUrl, `.json?${Math.round(new Date().getTime() / AppConsts.expiryTime)}`);
}


export class TranslateLoaderHelper implements TranslateLoader {

  constructor(private httpClient: HttpClient) { }

  getTranslation(lang: string): Observable<any> {
    return this.httpClient.get(`${AppConsts.languageUrl}${lang}.json?${Math.round(new Date().getTime() / AppConsts.expiryTime)}`);
  }
}