import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { ImageService, Image } from '../image.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { appUrl } from '../../app-url';
import { ActivatedRoute, Params } from '@angular/router';
import { ResultModel } from 'src/core/models/http.model';
import { Language } from 'src/core/common/language.service';
import { AppConsts } from 'src/core/constant/AppConsts';

@Component({
  selector: 'image-detail',
  templateUrl: './image-detail.component.html',
  styleUrls: ['./image-detail.component.css']
})
export class ImageDetailComponent extends AppBaseComponent {
  private params: Params;
  private type: string = 'add';
  private id: number = 0;

  private image: Image = new Image();
  private languages: Language[] = [];

 
  //Language
  private modifyLanguages: any[] = [];

  private numberPattern = AppConsts.numberPattern;

  constructor(
    injector: Injector,
    private imageSvc: ImageService,
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
    // else {
    //   this.modifyLanguages = [];
    //   this.languages.forEach((language) => {
    //     this.modifyLanguages.push({
    //       imageLanguageId: 0,
    //       languageId: language.id,
    //       langShowName: language.code + " - " + language.name,
    //       name: '',
    //       description: ''
    //     });
    //   });
    // }
  }

  private async loadDatasource() {

    var rs = await this.imageSvc.get(this.id).toPromise();
    if (rs.result == ResultCode.Success) {
      this.image = rs.data;
      // this.modifyLanguages = [];
      // this.languages.forEach((language) => {

      //   var imageLanguage = this.image.imageLanguages.find(l => l.languageId == language.id);
      //   if (imageLanguage == null) {
      //     this.modifyLanguages.push({
      //       imageLanguageId: 0,
      //       languageId: language.id,
      //       langShowName: language.code + " - " + language.name,
      //       question: '',
      //       answer: ''
      //     });
      //   }
      //   else {
      //     this.modifyLanguages.push({
      //       imageLanguageId: imageLanguage.id,
      //       languageId: language.id,
      //       langShowName: language.code + " - " + language.name,
      //       question: imageLanguage.question,
      //       answer: imageLanguage.answer
      //     });
      //   }
      // });
    }
  }

  private return() {
    console.log('return');
    this.router.navigate([appUrl.imageList]);
  }

  private refresh() {
    this.loadDatasource();
  }

  private save() {
    console.log('save');
    console.log(this.image);
    

    // this.image.imageLanguages = [];
    // this.modifyLanguages.forEach((item) => {
    //   this.image.imageLanguages.push({
    //     id: item.imageLanguageId,
    //     imageId: 0,
    //     languageId: item.languageId,
    //     question: item.question,
    //     answer: item.answer
    //   });
    // });

    if (this.type == "update") {
      this.imageSvc.update(this.image).subscribe((result: ResultModel<any>) => {
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
      this.imageSvc.add(this.image).subscribe((result: ResultModel<any>) => {
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
