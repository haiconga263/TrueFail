import { Component, Injector } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { AppUrlConsts, CommonConsts, ObjectTypes, Genders } from 'src/aritrace/common/app-constants';
import { CompanyViewModel, Company } from 'src/aritrace/common/models/company.model';
import { ActivatedRoute } from '@angular/router';
import { Contact } from 'src/aritrace/common/models/contact.model';
import { Address } from 'src/aritrace/common/models/address.model';
import { CompanyType } from 'src/aritrace/common/models/company-type.model';
import { AppConsts, UrlConsts, ParamUrlKeys } from 'src/core/constant/AppConsts';
import { Province } from 'src/aritrace/common/models/province.model';
import { Country } from 'src/aritrace/common/models/country.model';
import { Ward } from 'src/aritrace/common/models/ward.model';
import { District } from 'src/aritrace/common/models/district.model';
import { CommonService } from 'src/aritrace/common/services/common.service';
import { fail } from 'assert';
import { SelectListItem } from 'src/core/models/input.model';
import { CompanyService } from 'src/aritrace/common/services/company.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { ConfigService } from 'src/core/common/config.service';
import { FuncHelper } from 'src/core/helpers/function-helper';

@Component({
  selector: 'company-detail',
  templateUrl: './company-detail.component.html',
  styleUrls: ['./company-detail.component.css']
})
export class CompanyDetailComponent extends AppBaseComponent {
  company: CompanyViewModel;
  id: string;
  returnUrl: string;

  idLoadDataLocation: boolean = false;
  allCountries: Country[];
  allProvinces: Province[];
  allDistricts: District[];
  allWards: Ward[];
  allCompanyTypes: CompanyType[];

  countries: SelectListItem[];
  provinces: SelectListItem[];
  districts: SelectListItem[];
  wards: SelectListItem[];

  constructor(
    injector: Injector,
    public activatedRoute: ActivatedRoute,
    public commonSvc: CommonService,
    public companySvc: CompanyService,
    public configSvc: ConfigService
  ) {
    super(injector);

    this.countryChanged = this.countryChanged.bind(this);
    this.provinceChanged = this.provinceChanged.bind(this);
    this.districtChanged = this.districtChanged.bind(this);
    this.imageChanged = this.imageChanged.bind(this);

    this.company = new CompanyViewModel();
  }

  async loadNewCompany() {
    this.company = new CompanyViewModel({
      logoPath: '',
      isPartner: false,
      isUsed: true,
      contact: new Contact({
        objectType: ObjectTypes.company,
        gender: Genders.male,
      }),
      address: new Address({
        objectType: ObjectTypes.company,
      }),
    });

    this.configSvc.pushEvent(() => {
      this.company.imageData = AppConsts.logoDefaultUrl;
    });
    this.company.isChangedImage = false;
    this.getDataLocation();
  }

  async loadCompanyById() {
    let rs = await this.companySvc.getById(this.id);
    if (rs.result < ResultCode.Success) {
      this.alertSvc.alertByHttpResult(rs);
      this.cancel();
      return;
    }

    this.company = rs.data;
    this.configSvc.pushEvent(() => {
      if (this.company.logoPath == '' || this.company.logoPath == null)
        this.company.imageData = AppConsts.logoDefaultUrl;
      else this.company.imageData = AppConsts.imageDataUrl + '/companies/' + this.company.logoPath + '?' + Date.now();
    });

    this.company.isChangedImage = false;
    this.getDataLocation();
  }

  public async getDataLocation() {
    if (!this.idLoadDataLocation) {
      this.allCountries = (await this.commonSvc.getCountries()).data;
      this.allProvinces = (await this.commonSvc.getProvinces()).data;
      this.allDistricts = (await this.commonSvc.getDistricts()).data;
      this.allWards = (await this.commonSvc.getWards()).data;
      this.allCompanyTypes = (await this.commonSvc.getCompanyTypes()).data;
      this.idLoadDataLocation = true;
    }

    this.countries = [];
    this.allCountries.forEach(x => this.countries.push(new SelectListItem({ value: x.id, text: x.name })));

    this.provinces = [];
    this.allProvinces
      .filter(x => x.countryId == this.company.address.countryId)
      .forEach(x => this.provinces.push(new SelectListItem({ value: x.id, text: x.name })));

    this.districts = [];
    this.allDistricts
      .filter(x => x.countryId == this.company.address.countryId && x.provinceId == this.company.address.provinceId)
      .forEach(x => this.districts.push(new SelectListItem({ value: x.id, text: x.name })));

    this.wards = [];
    this.allWards
      .filter(x => x.countryId == this.company.address.countryId && x.provinceId == this.company.address.provinceId && x.districtId == this.company.address.districtId)
      .forEach(x => this.wards.push(new SelectListItem({ value: x.id, text: x.name })));
  }

  getGenders() {
    return CommonConsts.Genders;
  }

  imageChanged(fileInput: any) {
    if (fileInput.target.files && fileInput.target.files[0]) {
      var reader = new FileReader();
      let _component = this;
      reader.onload = function (e: any) {
        _component.company.imageData = e.target.result;
        _component.company.isChangedImage = true;
      }
      reader.readAsDataURL(fileInput.target.files[0]);
    }
  }

  countryChanged() {
    if (this.idLoadDataLocation) {
      this.provinces = [];
      this.allProvinces.filter(x => x.countryId == this.company.address.countryId)
        .forEach(x => this.provinces.push(new SelectListItem({ value: x.id, text: x.name })));
      this.company.address.provinceId = null;
    }
  }

  provinceChanged() {
    if (this.idLoadDataLocation) {
      this.districts = [];
      this.allDistricts
        .filter(x => x.countryId == this.company.address.countryId && x.provinceId == this.company.address.provinceId)
        .forEach(x => this.districts.push(new SelectListItem({ value: x.id, text: x.name })));
      this.company.address.districtId = null;
    }
  }

  districtChanged() {
    if (this.idLoadDataLocation) {
      this.wards = [];
      this.allWards
        .filter(x => x.countryId == this.company.address.countryId && x.provinceId == this.company.address.provinceId && x.districtId == this.company.address.districtId)
        .forEach(x => this.wards.push(new SelectListItem({ value: x.id, text: x.name })));
      this.company.address.wardId = null;
    }
  }

  async save() {
    if (this.company.id == 0 || this.company.id == null) {
      let rs = await this.companySvc.insert(this.company);
      this.alertSvc.alertByHttpResult(rs);
      if (rs.result == ResultCode.Success) {
        this.router.navigate([AppUrlConsts.urlCompanyDetail], { queryParams: { id: rs.data['id'], returnurl: encodeURIComponent(this.returnUrl) } });
      }
    }
    else {
      let rs = await this.companySvc.update(this.company);
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
        this.returnUrl = AppUrlConsts.urlCompany;

      if (FuncHelper.isNull(this.id)) {
        this.loadNewCompany()
      }
      else {
        this.loadCompanyById();
      }
    })
  }
}
