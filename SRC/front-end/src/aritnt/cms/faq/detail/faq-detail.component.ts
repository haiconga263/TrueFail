import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { FaqService, Faq } from '../faq.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'faq-detail',
  templateUrl: './faq-detail.component.html',
  styleUrls: ['./faq-detail.component.css']
})
export class FaqDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private faq: Faq = new Faq();
  private languages: Language[] = [];

 
  //Language
  private modifyLanguages: any[] = [];

  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private faqSvc: FaqService,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.params = params;
      if (this.params['type'] == 'update') {
        this.type = 'update';
        this.id = this.params["id"];
      }
    });
    this.Init();
  }

  async Init() {
    var langRs = await this.languageSvc.getLanguages().toPromise();
    if (langRs.result == ResultCode.Success) {
      this.languages = langRs.data;
    }

    if (this.params['type'] == 'update') {
      await this.loadDatasource();
    }
    else {
      this.modifyLanguages = [];
      this.languages.forEach((language) => {
        this.modifyLanguages.push({
          faqLanguageId: 0,
          languageId: language.id,
          langShowName: language.code + " - " + language.name,
          name: '',
          description: ''
        });
      });
    }
  }

  private async loadDatasource() {

    var rs = await this.faqSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.faq = rs.data;
      this.modifyLanguages = [];
      this.languages.forEach((language) => {

        var faqLanguage = this.faq.faqLanguages.find(l => l.languageId == language.id);
        if (faqLanguage == null) {
          this.modifyLanguages.push({
            faqLanguageId: 0,
            languageId: language.id,
            langShowName: language.code + " - " + language.name,
            question: '',
            answer: ''
          });
        }
        else {
          this.modifyLanguages.push({
            faqLanguageId: faqLanguage.id,
            languageId: language.id,
            langShowName: language.code + " - " + language.name,
            question: faqLanguage.question,
            answer: faqLanguage.answer
          });
        }
      });
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.faqList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.faq);
    

    this.faq.faqLanguages = [];
    this.modifyLanguages.forEach((item) => {
      this.faq.faqLanguages.push({
        id: item.faqLanguageId,
        faqId: 0,
        languageId: item.languageId,
        question: item.question,
        answer: item.answer
      });
    });

    if (this.type == "update") {
      this.faqSvc.update(this.faq).subscribe((result: ResultModel<any>) => {
        if (result.result == ResultCode.Success) {
          //alert
          this.showSuccess(this.lang.instant('Common.UpdateSuccess'));
          this.return();
        }
        else {
          //alert
          this.showError(result.errorMessage);
        }
      });
    }
    else {
      this.faqSvc.add(this.faq).subscribe((result: ResultModel<any>) => {
        if (result.result == ResultCode.Success) {
          //alert
          this.showSuccess(this.lang.instant('Common.AddSuccess'));
          this.return();
        }
        else {
          //alert
          this.showError(result.errorMessage);
        }
      });
    }
  }

 
  ngOnInit() {
  }
}
