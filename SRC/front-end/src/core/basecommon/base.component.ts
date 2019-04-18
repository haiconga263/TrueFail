import { OnInit, Input, Injector, Injectable, ViewChild, enableProdMode } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from '../common/language.service';
import { AppConsts } from '../constant/AppConsts';
import { AlertService, InforAlert, SuccessAlert, ErrorAlert, AlertTypes, QuestionAlert } from '../alert/alert.service';
import { CollectionNestedOption } from 'devextreme-angular';
import { ConfigService } from '../common/config.service';
import { Modes } from '../constant/AppEnums';

if (AppConsts.mode != Modes.development) {
  enableProdMode();
}

@Injectable()
export abstract class BaseComponent implements OnInit {
  @Input() AppName: string;
  @Input() lang: TranslateService;
  @Input() langCurrent: string;

  languageSvc: LanguageService;
  alertSvc: AlertService;
  configSvc: ConfigService;


  constructor(injector: Injector) {
    this.lang = injector.get(TranslateService);
    this.languageSvc = injector.get(LanguageService);
    this.alertSvc = injector.get(AlertService);
    this.configSvc = injector.get(ConfigService);

    this.lang.setDefaultLang('en');
    let langCurrent = this.languageSvc.getLangCurrent();
    this.useLanguage(langCurrent)
    this.configSvc.pushEvent(() => {
      this.AppName = AppConsts.appName;
    });

    this.evenLangChanged = this.evenLangChanged.bind(this);
    this.languageSvc.pushEventLangChanged(this.evenLangChanged);
  }

  evenLangChanged(lang: string) {
    //console.log(this);
    //console.log(lang);
    this.langCurrent = lang;
  }

  useLanguage(lang: string) {
    this.languageSvc.useLanguage(lang).then(() => {
      this.langCurrent = lang;
    });
  }

  showInfor(title: string) {
    this.alertSvc.alert(new InforAlert({
      title: title
    }));
  }

  showSuccess(title: string) {
    this.alertSvc.alert(new SuccessAlert({
      title: title
    }));
  }

  showError(title: string) {
    this.alertSvc.alert(new ErrorAlert({
      title: title
    }));
  }

  showYesNoQuestion(question: string, okFunction: () => void) {
    this.alertSvc.alert(new QuestionAlert({
      title: question + " ?",
      confirmButtonText: this.lang.instant("Common.Ok"),
      cancelButtonText: this.lang.instant("Common.Cancel"),
      onConfirm: okFunction
    }));
  }

  ngOnInit() {

  }
}
