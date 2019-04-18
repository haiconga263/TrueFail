import { Component, Injector, Input } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { CaptionMultipleLanguage, Caption } from 'src/aritrace/common/models/caption.model';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { CaptionService } from 'src/aritrace/common/services/caption.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Language } from 'src/core/common/language.service';
import { CaptionLanguage } from 'src/aritrace/common/models/caption-language.model';
import { ResultModel } from 'src/core/models/http.model';

@Component({
  selector: 'caption-embed',
  templateUrl: './caption-embed.component.html',
  styleUrls: ['./caption-embed.component.css']
})
export class CaptionEmbedComponent extends AppBaseComponent {
  @Input() caption: CaptionMultipleLanguage;

  languages: Language[];
  commonSvc: CommonService;
  captionSvc: CaptionService;
  id: number;

  constructor(
    injector: Injector
  ) {
    super(injector);

    this.commonSvc = injector.get(CommonService);
    this.captionSvc = injector.get(CaptionService);

    this.caption = new CaptionMultipleLanguage({
      //isUsed: true,
      //isCommon: true
    });
  }

  public initialize(id: number = 0) {
    this.id = id;
    if (this.id == null || this.id == 0) {
      this.loadNewCaption()
    }
    else {
      this.loadCaptionById();
    }
  }

  async loadNewCaption() {
    this.caption = new CaptionMultipleLanguage({
      isUsed: true,
      isCommon: true
    });

    this.getDataInit();
  }

  async loadCaptionById() {
    let rs = await this.captionSvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
    }

    this.caption = rs.data;
    this.getDataInit();
  }

  public async getDataInit() {
    this.languages = (await this.commonSvc.getLanguages()).data;
    if (FuncHelper.isNull(this.caption.languages))
      this.caption.languages = [];
    for (var i = 0; i < this.languages.length; i++) {
      let isExist = false;
      for (var j = 0; j < this.caption.languages.length; j++) {
        if (this.caption.languages[j].languageId == this.languages[i].id) {
          isExist = true;
          this.caption.languages[j].langName = this.languages[i].name;
          this.caption.languages[j].langClassIcon = this.languages[i].classIcon;
          break;
        }
      }

      if (!isExist)
        this.caption.languages.push(new CaptionLanguage({
          caption: '',
          captionId: this.caption.id,
          languageId: this.languages[i].id,
          langName: this.languages[i].name,
          langClassIcon: this.languages[i].classIcon,
        }));
    }
  }

  save(callback: (captionId: number) => void) {
    let id = this.caption.id;

    if (this.caption.id == 0 || this.caption.id == null)
      this.captionSvc.insert(this.caption).then((result) => {
        if (result.result == ResultCode.Success)
          callback(result.data['id']);
        else callback(0);
      });
    else
      this.captionSvc.update(this.caption).then((result) => {
        if (result.result == ResultCode.Success)
          callback(id);
        else callback(0);
      });
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
