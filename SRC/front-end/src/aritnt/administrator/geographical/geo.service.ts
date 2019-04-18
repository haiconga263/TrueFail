import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService, Session } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';

export class Country {
  id: number = 0;
  code: string = '';
  name: string = '';
  phoneCode: string = '';
  isUsed: boolean = false;
}

export class Region {
  id: number = 0;
  code: string = '';
  name: string = '';
  countryId: number = 0;
  isUsed: boolean = false;
}

export class Province {
  id: number = 0;
  code: string = '';
  name: string = '';
  phoneCode: string = '';
  countryId: number = 0;
  regionId: number = 0;
  isUsed: boolean = false;
}

export class District {
  id: number = 0;
  code: string = '';
  name: string = '';
  countryId: number = 0;
  provinceId: number = 0;
  isUsed: boolean = false;
}

export class Ward {
  id: number = 0;
  code: string = '';
  name: string = '';
  countryId: number = 0;
  provinceId: number = 0;
  districtId: number = 0;
  isUsed: boolean = false;
}

export class Address {
  id: number = 0;
  street: string = '';
  countryId: number = null;
  provinceId: number = null;
  districtId: number = null;
  wardId: number = null;
  longitude: number = 0;
  latitude: number = 0;
  isUsed: boolean = false;
}

export class Contact {
  id: number = 0;
  phone: string = '';
  email: string = '';
  gender: string = '';
  isUsed: boolean = false;
}


@Injectable()
export class GeoService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlCountries: string = '/api/Geo/get/countries';
  private urlAddCountry: string = '/api/Geo/add/country';
  private urlUpdateCountry: string = '/api/Geo/update/country';
  private urlDeleteCountry: string = '/api/Geo/delete/country';

  getCountries(): Observable<ResultModel<Country[]>> {
    return this.http.get<ResultModel<Country[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlCountries}`, this.authenticSvc.getHttpHeader());
  }

  addCountry(country: Country) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddCountry}`, { country }, this.authenticSvc.getHttpHeader());
  }

  updateCountry(country: Country) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateCountry}`, { country }, this.authenticSvc.getHttpHeader());
  }

  deleteCountry(countryId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteCountry}`, { countryId }, this.authenticSvc.getHttpHeader());
  }

  private urlRegions: string = '/api/Geo/get/regions';
  private urlAddRegion: string = '/api/Geo/add/region';
  private urlUpdateRegion: string = '/api/Geo/update/region';
  private urlDeleteRegion: string = '/api/Geo/delete/region';

  getRegions(): Observable<ResultModel<Region[]>> {
    return this.http.get<ResultModel<Region[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlRegions}`, this.authenticSvc.getHttpHeader());
  }

  addRegion(region: Region) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddRegion}`, { region }, this.authenticSvc.getHttpHeader());
  }

  updateRegion(region: Region) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateRegion}`, { region }, this.authenticSvc.getHttpHeader());
  }

  deleteRegion(regionId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteRegion}`, { regionId }, this.authenticSvc.getHttpHeader());
  }

  private urlProvinces: string = '/api/Geo/get/Provinces';
  private urlAddProvince: string = '/api/Geo/add/Province';
  private urlUpdateProvince: string = '/api/Geo/update/Province';
  private urlDeleteProvince: string = '/api/Geo/delete/Province';

  getProvinces(): Observable<ResultModel<Province[]>> {
    return this.http.get<ResultModel<Province[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlProvinces}`, this.authenticSvc.getHttpHeader());
  }

  addProvince(Province: Province) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddProvince}`, { Province }, this.authenticSvc.getHttpHeader());
  }

  updateProvince(Province: Province) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateProvince}`, { Province }, this.authenticSvc.getHttpHeader());
  }

  deleteProvince(ProvinceId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteProvince}`, { ProvinceId }, this.authenticSvc.getHttpHeader());
  }

  private urlDistricts: string = '/api/Geo/get/Districts';
  private urlAddDistrict: string = '/api/Geo/add/District';
  private urlUpdateDistrict: string = '/api/Geo/update/District';
  private urlDeleteDistrict: string = '/api/Geo/delete/District';

  getDistricts(): Observable<ResultModel<District[]>> {
    return this.http.get<ResultModel<District[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDistricts}`, this.authenticSvc.getHttpHeader());
  }

  addDistrict(District: District) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddDistrict}`, { District }, this.authenticSvc.getHttpHeader());
  }

  updateDistrict(District: District) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateDistrict}`, { District }, this.authenticSvc.getHttpHeader());
  }

  deleteDistrict(DistrictId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteDistrict}`, { DistrictId }, this.authenticSvc.getHttpHeader());
  }

  private urlWards: string = '/api/Geo/get/Wards';
  private urlAddWard: string = '/api/Geo/add/Ward';
  private urlUpdateWard: string = '/api/Geo/update/Ward';
  private urlDeleteWard: string = '/api/Geo/delete/Ward';

  getWards(): Observable<ResultModel<Ward[]>> {
    return this.http.get<ResultModel<Ward[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlWards}`, this.authenticSvc.getHttpHeader());
  }

  addWard(Ward: Ward) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddWard}`, { Ward }, this.authenticSvc.getHttpHeader());
  }

  updateWard(Ward: Ward) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateWard}`, { Ward }, this.authenticSvc.getHttpHeader());
  }

  deleteWard(WardId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteWard}`, { WardId }, this.authenticSvc.getHttpHeader());
  }
}
