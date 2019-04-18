import { Component, Injector, ViewChild, ViewChildren, QueryList, AfterViewInit } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { GrowingMethodService } from 'src/aritrace/common/services/growing-method.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { Language } from 'src/core/common/language.service';
import { GrowingMethod } from 'src/aritrace/common/models/growing-method.model';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { CaptionEmbedComponent } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.component';
import { FuncHelper } from 'src/core/helpers/function-helper';

@Component({
  selector: 'growing-method-detail',
  templateUrl: './growing-method-detail.component.html',
  styleUrls: ['./growing-method-detail.component.css']
})
export class GrowingMethodDetailComponent extends AppBaseComponent implements AfterViewInit {
  @ViewChildren(CaptionEmbedComponent) viewChildren !: QueryList<CaptionEmbedComponent>;
  growingMethod: GrowingMethod;
  id: string;
  returnUrl: string;
  public caption: CaptionEmbedComponent;

  idLoadDataLocation: boolean = false;
  allLanguages: Language[];

  constructor(
    public injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public growingMethodSvc: GrowingMethodService,
    public configSvc: ConfigService
  ) {
    super(injector);

    this.growingMethod = new GrowingMethod();
    this.saveGrowingMethod = this.saveGrowingMethod.bind(this);
  }

  ngAfterViewInit() {
    this.caption = this.viewChildren.first;
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params[ParamUrlKeys.id];
      this.returnUrl = decodeURIComponent(params[ParamUrlKeys.returnurl]);

      if (FuncHelper.isNull(this.returnUrl))
        this.returnUrl = AppUrlConsts.urlGrowingMethods;

      if (FuncHelper.isNull(this.id)) {
        this.loadNewGrowingMethod()
      }
      else {
        this.loadGrowingMethodById();
      }
    })
  }

  async loadNewGrowingMethod() {
    this.growingMethod = new GrowingMethod({
      isUsed: true
    });

    this.caption.initialize();
    this.getDataInit();
  }

  async loadGrowingMethodById() {
    let rs = await this.growingMethodSvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
    }

    this.growingMethod = rs.data;
    this.caption.initialize(FuncHelper.isNull(this.growingMethod.captionNameId) ? 0 : this.growingMethod.captionNameId);
    this.getDataInit();
  }

  public async getDataInit() {
    this.allLanguages = (await this.commonSvc.getLanguages()).data;
  }

  save() {
    this.caption.save(this.saveGrowingMethod);
  }

  async saveGrowingMethod(captionId: number) {
    if (captionId == 0) return;

    this.growingMethod.captionNameId = captionId;
    if (this.growingMethod.id == 0 || this.growingMethod.id == null) {
      let rs = await this.growingMethodSvc.insert(this.growingMethod);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success) {
        this.router.navigate([AppUrlConsts.urlGrowingMethodDetail], { queryParams: { id: rs.data['id'], returnurl: encodeURIComponent(this.returnUrl) } });
      }
    }
    else {
      let rs = await this.growingMethodSvc.update(this.growingMethod);
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

  }
}
