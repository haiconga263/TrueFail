import { Component, Injector, Input } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { CaptionMultipleLanguage, Caption } from 'src/aritrace/common/models/caption.model';
import { ActivatedRoute } from '@angular/router';
import { Contact } from 'src/aritrace/common/models/contact.model';
import { Address } from 'src/aritrace/common/models/address.model';
import { AppConsts, UrlConsts, ParamUrlKeys } from 'src/core/constant/AppConsts';
import { Province } from 'src/aritrace/common/models/province.model';
import { Country } from 'src/aritrace/common/models/country.model';
import { Ward } from 'src/aritrace/common/models/ward.model';
import { District } from 'src/aritrace/common/models/district.model';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { fail } from 'assert';
import { SelectListItem } from 'src/core/models/input.model';
import { CaptionService } from 'src/aritrace/common/services/caption.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { Language } from 'src/core/common/language.service';
import { CaptionLanguage } from 'src/aritrace/common/models/caption-language.model';

@Component({
  selector: 'caption-detail',
  templateUrl: './caption-detail.component.html',
  styleUrls: ['./caption-detail.component.css']
})
export class CaptionDetailComponent extends AppBaseComponent {
  @Input() caption: CaptionMultipleLanguage;

  id: string;
  returnUrl: string;

  languages: Language[];

  constructor(
    injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public captionSvc: CaptionService,
    public configSvc: ConfigService
  ) {
    super(injector);

    this.caption = new CaptionMultipleLanguage();
  }

  async loadNewCaption() {
    this.caption = new CaptionMultipleLanguage({
      isUsed: true,
    });

    this.getDataInit();
  }

  async loadCaptionById() {
    let rs = await this.captionSvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
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

  async save() {
    if (this.caption.id == 0 || this.caption.id == null) {
      let rs = await this.captionSvc.insert(this.caption);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success) {
        this.router.navigate([AppUrlConsts.urlCaptionDetail], { queryParams: { id: rs.data['id'], returnurl: encodeURIComponent(this.returnUrl) } });
      }
    }
    else {
      let rs = await this.captionSvc.update(this.caption);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success)
        this.router.navigate([this.returnUrl]);
    }
  }

  cancel() {
    this.router.navigate([this.returnUrl]);
  }

  ngOnInit() {
    super.ngOnInit();

    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params[ParamUrlKeys.id];
      this.returnUrl = decodeURIComponent(params[ParamUrlKeys.returnurl]);

      if (FuncHelper.isNull(this.returnUrl))
        this.returnUrl = AppUrlConsts.urlCaption;

      if (FuncHelper.isNull(this.id)) {
        this.loadNewCaption()
      }
      else {
        this.loadCaptionById();
      }
    })
  }
}
