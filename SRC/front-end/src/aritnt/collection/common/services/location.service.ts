import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Address, Contact } from '../services/geo.service';

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
export class LocationService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlLocations: string = '/api/retailer/gets/location/byuser';
  private urlLocation: string = '/api/retailer/get/location';
  private urlAddLocation: string = '/api/retailer/add/location';
  private urlUpdateLocation: string = '/api/retailer/update/location';
  private urlDeleteLocation: string = '/api/retailer/delete/location';

  gets(userId: number): Observable<ResultModel<RetailerLocation[]>> {
    return this.http.get<ResultModel<RetailerLocation[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlLocations}?userId=${userId}`, this.authenticSvc.getHttpHeader());
  }
  get(id: number): Observable<ResultModel<RetailerLocation>> {
    return this.http.get<ResultModel<RetailerLocation>>(`${AppConsts.remoteServiceBaseUrl}${this.urlLocation}?locationId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(location: RetailerLocation) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateLocation}`, { location }, this.authenticSvc.getHttpHeader());
  }
  add(location: RetailerLocation) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddLocation}`, { location }, this.authenticSvc.getHttpHeader());
  }

  delete(locationId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteLocation}`, { locationId }, this.authenticSvc.getHttpHeader());
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
