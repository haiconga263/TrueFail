import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Address, Contact } from '../geographical/geo.service';

export class Fulfillment {
  id: number = 0;
  code: string = '';
  name: string = '';
  description: string = '';
  managerId: number = null;
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
export class FulfillmentService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlFulfillments: string = '/api/Fulfillment/gets';
  private urlFulfillment: string = '/api/Fulfillment/get';
  private urlAddFulfillment: string = '/api/Fulfillment/add';
  private urlUpdateFulfillment: string = '/api/Fulfillment/update';
  private urlDeleteFulfillment: string = '/api/Fulfillment/delete';

  gets(): Observable<ResultModel<Fulfillment[]>> {
    return this.http.get<ResultModel<Fulfillment[]>>(`${AppConsts.remoteServiceBaseUrl}${this.urlFulfillments}`, this.authenticSvc.getHttpHeader());
  }
  get(id: number): Observable<ResultModel<Fulfillment>> {
    return this.http.get<ResultModel<Fulfillment>>(`${AppConsts.remoteServiceBaseUrl}${this.urlFulfillment}?id=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(fulfillment: Fulfillment) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateFulfillment}`, { fulfillment }, this.authenticSvc.getHttpHeader());
  }
  add(fulfillment: Fulfillment) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlAddFulfillment}`, { fulfillment }, this.authenticSvc.getHttpHeader());
  }

  delete(fulfillmentId: number) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlDeleteFulfillment}`, { fulfillmentId }, this.authenticSvc.getHttpHeader());
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
