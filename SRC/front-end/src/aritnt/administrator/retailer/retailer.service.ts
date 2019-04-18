import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Address, Contact } from '../geographical/geo.service';

export class Retailer {
  id: number = 0;
  name: string = '';
  userId: number = null;
  addressId: number = 0;
  contactId: number = 0;
  imageURL: string = '';
  isUsed: boolean = true;
  isCompany: boolean = false;
  taxCode: string = '';

  //mapping
  imageData: string = '';
  address: Address = new Address();
  contact: Contact = new Contact();
  locations: RetailerLocation[] = [];
}

export class RetailerLocation {
  id: number = 0;
  gln: string = '';
  name: string = '';
  description: string = '';
  retailerId: number = 0;
  addressId: number = 0;
  contactId: number = 0;
  imageURL: string = '';
  isUsed: boolean = true;

  //mapping
  imageData: string = '';
  address: Address = new Address();
  contact: Contact = new Contact();
}


@Injectable()
export class RetailerService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlRetailers: string = '/api/Retailer/gets';
  private urlRetailer: string = '/api/Retailer/get';
  private urlAddRetailer: string = '/api/Retailer/add';
  private urlUpdateRetailer: string = '/api/Retailer/update';
  private urlDeleteRetailer: string = '/api/Retailer/delete';

  gets(): Observable<ResultModel<Retailer[]>> {
    return this.http.get<ResultModel<Retailer[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlRetailers}`, this.authenticSvc.getHttpHeader());
  }
  get(id: number): Observable<ResultModel<Retailer>> {
    return this.http.get<ResultModel<Retailer>>(`${AppConsts.remoteServiceBaseUrl}${this.urlRetailer}?RetailerId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(Retailer: Retailer) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateRetailer}`, { Retailer }, this.authenticSvc.getHttpHeader());
  }
  add(Retailer: Retailer) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddRetailer}`, { Retailer }, this.authenticSvc.getHttpHeader());
  }

  delete(RetailerId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteRetailer}`, { RetailerId }, this.authenticSvc.getHttpHeader());
  }

  private urlRetailerLocation: string = '/api/Retailer/get/location';
  private urlRetailerLocations: string = '/api/Retailer/gets/location';
  private urlAddRetailerLocation: string = '/api/Retailer/add/location';
  private urlUpdateRetailerLocation: string = '/api/Retailer/update/location';
  private urlDeleteRetailerLocation: string = '/api/Retailer/delete/location';

  getLocation(id: number): Observable<ResultModel<RetailerLocation>> {
    return this.http.get<ResultModel<RetailerLocation>>(`${AppConsts.remoteServiceBaseUrl}${this.urlRetailerLocation}?locationId=${id}`, this.authenticSvc.getHttpHeader());
  }

  getLocations(retailerId: number): Observable<ResultModel<RetailerLocation[]>> {
    return this.http.get<ResultModel<RetailerLocation[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlRetailerLocations}?retailerId=${retailerId}`, this.authenticSvc.getHttpHeader());
  }

  updateLocation(location: RetailerLocation) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateRetailerLocation}`, { location }, this.authenticSvc.getHttpHeader());
  }
  addLocation(location: RetailerLocation) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddRetailerLocation}`, { location }, this.authenticSvc.getHttpHeader());
  }

  deleteLocation(locationId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteRetailerLocation}`, { locationId }, this.authenticSvc.getHttpHeader());
  }

  getGenders()
  {
    return[
      {
        id: 'M',
        name: 'Male'
      },
      {
        id: 'F',
        name: 'Female'
      },
      {
        id: 'O',
        name: 'Other'
      }
    ];
  }
}
