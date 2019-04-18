import { Component, Injector, ViewChild, ViewChildren, QueryList, AfterViewInit } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { GLNService } from 'src/aritrace/common/services/gln.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { Language } from 'src/core/common/language.service';
import { ParamUrlKeys } from 'src/core/constant/AppConsts';
import { CaptionEmbedComponent } from 'src/aritrace/administrator/configuration/language-settings/captions/embed/caption-embed.component';
import { FuncHelper } from 'src/core/helpers/function-helper';
import { GLN } from 'src/aritrace/common/models/gln.model';
import { Company } from '../../../common/models/company.model';
import { Country } from 'src/aritrace/common/models/country.model';
import { SelectListItem } from 'src/core/models/input.model';

@Component({
  selector: 'gln-detail',
  templateUrl: './gln-detail.component.html',
  styleUrls: ['./gln-detail.component.css']
})
export class GLNDetailComponent extends AppBaseComponent {
  gln: GLN;
  id: string;
  returnUrl: string;

  idLoadDataLocation: boolean = false;
  allLanguages: Language[];
  allCompanies: Company[];
  allCountries: Country[];
  companies: SelectListItem[];
  countries: SelectListItem[];

  constructor(
    public injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public glnSvc: GLNService,
    public configSvc: ConfigService
  ) {
    super(injector);

    this.gln = new GLN();
    this.cancel = this.cancel.bind(this);
  }

  async loadNewGLN() {
    this.gln = new GLN({
      isUsed: true
    });
    this.getDataInit();
  }

  async loadGLNById() {
    let rs = await this.glnSvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
    }

    this.gln = rs.data;
    this.getDataInit();
  }

  public async getDataInit() {
    this.allLanguages = (await this.commonSvc.getLanguages()).data;
    this.allCompanies = (await this.commonSvc.getCompanies()).data;
    this.allCountries = (await this.commonSvc.getCountries()).data;
    this.countries = [];
    this.allCountries.forEach(x => this.countries.push(new SelectListItem({ value: x.id, text: x.name })));
    this.companies = [];
    this.allCompanies.forEach(x => this.companies.push(new SelectListItem({ value: x.id, text: x.name })));
  }

  async save() {
    if (this.gln.id == 0 || this.gln.id == null) {
      let rs = await this.glnSvc.insert(this.gln);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success) {
        this.router.navigate([AppUrlConsts.urlApiGLNDetail], { queryParams: { id: rs.data['id'], returnurl: encodeURIComponent(this.returnUrl) } });
      }
    }
    else {
      let rs = await this.glnSvc.update(this.gln);
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
      if (this.id == '' || this.id == null) {
        this.loadNewGLN()
      }
      else {
        this.loadGLNById();
      }
    })
  }
}
