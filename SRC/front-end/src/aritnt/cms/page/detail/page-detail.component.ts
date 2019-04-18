import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { appUrl } from 'src/aritnt/cms/app-url';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { PageService, Page } from '../page.service';

@Component({
  selector: 'page-detail',
  templateUrl: './page-detail.component.html',
  styleUrls: ['./page-detail.component.css']
})
export class PageDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;
  // topicSvc: TopicService;

  private page: Page = new Page();
  private languages: Language[] = [];
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};

  //Language
  private modifyLanguages: any[] = [];

  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private pageSvc: PageService,
    private activatedRoute: ActivatedRoute,
  ) {
    super(injector);
    // this.topicSvc = injector.get(TopicService)
    debugger
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
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'topicName',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };
    var langRs = await this.languageSvc.getLanguages().toPromise();
    if (langRs.result == ResultCode.Success) {
      this.languages = langRs.data;
    }
    debugger
    
    

    if (this.params['type'] == 'update') {
      await this.loadDatasource();
    }
    // else {
    //   this.modifyLanguages = [];
    //   this.languages.forEach((language) => {
    //     this.modifyLanguages.push({
    //       pageLanguageId: 0,
    //       languageId: language.id,
    //       langShowName: language.code + " - " + language.name,
    //       name: '',
    //       description: ''
    //     });
    //   });
    // }
  }

  private async loadDatasource() {

    var rs = await this.pageSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.page = rs.data;
      



      // this.modifyLanguages = [];
      // this.languages.forEach((language) => {

      //   var pageLanguage = this.page.pageLanguages.find(l => l.languageId == language.id);
      //   if (pageLanguage == null) {
      //     this.modifyLanguages.push({
      //       pageLanguageId: 0,
      //       languageId: language.id,
      //       langShowName: language.code + " - " + language.name,
      //       question: '',
      //       answer: ''
      //     });
      //   }
      //   else {
      //     this.modifyLanguages.push({
      //       pageLanguageId: pageLanguage.id,
      //       languageId: language.id,
      //       langShowName: language.code + " - " + language.name,
      //       question: pageLanguage.question,
      //       answer: pageLanguage.answer
      //     });
      //   }
      // });
    }
    
    
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.pageList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.page);


    // this.page.pageLanguages = [];
    // this.modifyLanguages.forEach((item) => {
    //   this.page.pageLanguages.push({
    //     id: item.pageLanguageId,
    //     pageId: 0,
    //     languageId: item.languageId,
    //     question: item.question,
    //     answer: item.answer
    //   });
    // });

    if (this.type == "update") {
      debugger
      this.pageSvc.update(this.page).subscribe((result: ResultModel<any>) => {
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
      debugger
      this.pageSvc.add(this.page).subscribe((result: ResultModel<any>) => {
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
