import { Component, Injector, ViewChild, ViewChildren, QueryList, AfterViewInit } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { CompanyTypeService } from 'src/aritrace/common/services/company-type.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { Language } from 'src/core/common/language.service';
import { CompanyType } from 'src/aritrace/common/models/company-type.model';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { CaptionEmbedComponent } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.component';
import { FuncHelper } from 'src/core/helpers/function-helper';

@Component({
  selector: 'company-type-detail',
  templateUrl: './company-type-detail.component.html',
  styleUrls: ['./company-type-detail.component.css']
})
export class CompanyTypeDetailComponent extends AppBaseComponent implements AfterViewInit {
  @ViewChildren(CaptionEmbedComponent) viewChildren !: QueryList<CaptionEmbedComponent>;
  companyType: CompanyType;
  id: string;
  returnUrl: string;
  public caption: CaptionEmbedComponent;

  idLoadDataLocation: boolean = false;
  allLanguages: Language[];

  constructor(
    public injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public companyTypeSvc: CompanyTypeService,
    public configSvc: ConfigService
  ) {
    super(injector);

    this.companyType = new CompanyType();
    this.saveCompanyType = this.saveCompanyType.bind(this);
  }

  ngAfterViewInit() {
    this.caption = this.viewChildren.first;
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params[ParamUrlKeys.id];
      this.returnUrl = decodeURIComponent(params[ParamUrlKeys.returnurl]);

      if (FuncHelper.isNull(this.returnUrl))
        this.returnUrl = AppUrlConsts.urlCompanyType;

      if (FuncHelper.isNull(this.id)) {
        this.loadNewCompanyType()
      }
      else {
        this.loadCompanyTypeById();
      }
    })
  }

  async loadNewCompanyType() {
    this.companyType = new CompanyType({
      isUsed: true
    });

    this.caption.initialize();
    this.getDataInit();
  }

  async loadCompanyTypeById() {
    let rs = await this.companyTypeSvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
    }

    this.companyType = rs.data;
    this.caption.initialize(FuncHelper.isNull(this.companyType.captionNameId) ? 0 : this.companyType.captionNameId);
    this.getDataInit();
  }

  public async getDataInit() {
    this.allLanguages = (await this.commonSvc.getLanguages()).data;
  }

  save() {
    this.caption.save(this.saveCompanyType);
  }

  async saveCompanyType(captionId: number) {
    if (captionId == 0) return;

    this.companyType.captionNameId = captionId;
    if (this.companyType.id == 0 || this.companyType.id == null) {
      let rs = await this.companyTypeSvc.insert(this.companyType);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success) {
        this.router.navigate([AppUrlConsts.urlCompanyTypeDetail], { queryParams: { id: rs.data['id'], returnurl: encodeURIComponent(this.returnUrl) } });
      }
    }
    else {
      let rs = await this.companyTypeSvc.update(this.companyType);
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
