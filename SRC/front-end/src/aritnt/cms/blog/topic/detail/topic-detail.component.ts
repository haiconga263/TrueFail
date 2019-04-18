import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { TopicService, Topic } from '../topic.service';
import { ResultCode } from 'src/core/constant/AppEnums';

import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { appUrl } from 'src/aritnt/cms/app-url';

@Component({
  selector: 'topic-detail',
  templateUrl: './topic-detail.component.html',
  styleUrls: ['./topic-detail.component.css']
})
export class TopicDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private topic: Topic = new Topic();
  private languages: Language[] = [];
 
  //Language
  private modifyLanguages: any[] = [];

  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private topicSvc: TopicService,
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
          topicLanguageId: 0,
          languageId: language.id,
          langShowName: language.code + " - " + language.name,
          topicName: '',
          topicUrl: ''
        });
      });
    }
  }

  private async loadDatasource() {

    var rs = await this.topicSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.topic = rs.data;
      this.modifyLanguages = [];
      this.languages.forEach((language) => {

        var topicLanguage = this.topic.topicLanguages.find(l => l.languageId == language.id);
        if (topicLanguage == null) {
          this.modifyLanguages.push({
            topicLanguageId: 0,
            languageId: language.id,
            langShowName: language.code + " - " + language.name,
            topicName: '',
            topicUrl: ''
          });
        }
        else {
          this.modifyLanguages.push({
            topicLanguageId: topicLanguage.id,
            languageId: language.id,
            langShowName: language.code + " - " + language.name,
            topicName: topicLanguage.topicName,
            topicUrl: topicLanguage.topicUrl
          });
        }
      });
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.topicList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.topic);
    

    this.topic.topicLanguages = [];
    this.modifyLanguages.forEach((item) => {
      this.topic.topicLanguages.push({
        id: item.topicLanguageId,
        topicId: 0,
        languageId: item.languageId,
        topicName: item.topicName,
        topicUrl: item.topicUrl
      });
    });

    if (this.type == "update") {
      this.topicSvc.update(this.topic).subscribe((result: ResultModel<any>) => {
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
      this.topicSvc.add(this.topic).subscribe((result: ResultModel<any>) => {
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
