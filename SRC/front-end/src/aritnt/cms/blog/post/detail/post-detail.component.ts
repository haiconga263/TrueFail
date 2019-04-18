import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { PostService, Post } from '../post.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { appUrl } from 'src/aritnt/cms/app-url';
import { TopicService } from '../../topic/topic.service';
import { FuncHelper } from 'src/core/helpers/function-helper';

@Component({
  selector: 'post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.css']
})
export class PostDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;
  // topicSvc: TopicService;

  private post: Post = new Post();
  private languages: Language[] = [];
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};
  private companies:  Post[] = [];

  //Language
  private modifyLanguages: any[] = [];

  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private postSvc: PostService,
    private activatedRoute: ActivatedRoute,
    private topicSvc: TopicService,
  ) {
    super(injector);
    // this.topicSvc = injector.get(TopicService)
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
    var topicRs = await this.topicSvc.getsOnly().toPromise();
    
    if (topicRs.result == ResultCode.Success) {
      debugger
      this.dropdownList = topicRs.data;
    }
    

    if (this.params['type'] == 'update') {
      await this.loadDatasource();
    }
    // else {
    //   this.modifyLanguages = [];
    //   this.languages.forEach((language) => {
    //     this.modifyLanguages.push({
    //       postLanguageId: 0,
    //       languageId: language.id,
    //       langShowName: language.code + " - " + language.name,
    //       name: '',
    //       description: ''
    //     });
    //   });
    // }
  }

  private async loadDatasource() {

    var rs = await this.postSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.post = rs.data;
      debugger
      this.selectedItems = rs.data.topics;
      

      // this.modifyLanguages = [];
      // this.languages.forEach((language) => {

      //   var postLanguage = this.post.postLanguages.find(l => l.languageId == language.id);
      //   if (postLanguage == null) {
      //     this.modifyLanguages.push({
      //       postLanguageId: 0,
      //       languageId: language.id,
      //       langShowName: language.code + " - " + language.name,
      //       question: '',
      //       answer: ''
      //     });
      //   }
      //   else {
      //     this.modifyLanguages.push({
      //       postLanguageId: postLanguage.id,
      //       languageId: language.id,
      //       langShowName: language.code + " - " + language.name,
      //       question: postLanguage.question,
      //       answer: postLanguage.answer
      //     });
      //   }
      // });
    }
    
    
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.postList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.post);


    // this.post.postLanguages = [];
    // this.modifyLanguages.forEach((item) => {
    //   this.post.postLanguages.push({
    //     id: item.postLanguageId,
    //     postId: 0,
    //     languageId: item.languageId,
    //     question: item.question,
    //     answer: item.answer
    //   });
    // });

    if (this.type == "update") {
      this.postSvc.update(this.post).subscribe((result: ResultModel<any>) => {
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
      this.postSvc.add(this.post).subscribe((result: ResultModel<any>) => {
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
