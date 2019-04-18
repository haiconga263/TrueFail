import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ResultModel } from "src/core/models/http.model";
import { AppUrlConsts } from "../app-constants";
import { AppConsts } from "src/core/constant/AppConsts";
import { AuthenticService } from "src/core/Authentication/authentic.service";
import { UOM } from "../models/uom.model";
import { Country } from "../models/country.model";
import { District } from "../models/district.model";
import { Province } from "../models/province.model";
import { Ward } from "../models/ward.model";
import { Region } from "../models/region.model";
import { Role } from "../models/role.model";
import { Language } from "src/core/common/language.service";
import { Category } from "../models/category.model";
import { CompanyType } from "../models/company-type.model";
import { Product } from "../models/product.model";
import { Company } from "../models/company.model";
import { CultureField } from "../models/culture-field.model";

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  constructor(private http: HttpClient,
    private authenticSvc: AuthenticService
  ) {
  }

  getRoles(): Promise<ResultModel<Role[]>> {
    return this.http.get<ResultModel<Role[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiRole}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getUOMs(): Promise<ResultModel<UOM[]>> {
    return this.http.get<ResultModel<UOM[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiUOM}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getRegions(): Promise<ResultModel<Region[]>> {
    return this.http.get<ResultModel<Region[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiRegion}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getCountries(): Promise<ResultModel<Country[]>> {
    return this.http.get<ResultModel<Country[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCountry}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getDistricts(): Promise<ResultModel<District[]>> {
    return this.http.get<ResultModel<District[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiDistrict}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getProvinces(): Promise<ResultModel<Province[]>> {
    return this.http.get<ResultModel<Province[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProvince}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getWards(): Promise<ResultModel<Ward[]>> {
    return this.http.get<ResultModel<Ward[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiWard}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getLanguages(): Promise<ResultModel<Language[]>> {
    return this.http.get<ResultModel<Language[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiLanguage}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getCategories(): Promise<ResultModel<Category[]>> {
    return this.http.get<ResultModel<Category[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCategory}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getCompanies(): Promise<ResultModel<Company[]>> {
    return this.http.get<ResultModel<Company[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCompany}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getCompanyTypes(): Promise<ResultModel<CompanyType[]>> {
    return this.http.get<ResultModel<CompanyType[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCompanyType}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }

  getProducts(): Promise<ResultModel<Product[]>> {
    return this.http.get<ResultModel<Product[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiProduct}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }
  
  getCultureFields(): Promise<ResultModel<CultureField[]>> {
    return this.http.get<ResultModel<CultureField[]>>(`${AppConsts.remoteServiceBaseUrl}${AppUrlConsts.urlApiCultureField}/common`, this.authenticSvc.getHttpHeader()).toPromise();
  }
}
