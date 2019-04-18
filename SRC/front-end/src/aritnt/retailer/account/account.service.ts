import { Injectable } from '@angular/core';
import { ResultModel } from 'src/core/models/http.model';
import { Observable } from 'rxjs';
import { AuthenticService } from 'src/core/Authentication/authentic.service';
import { AppConsts } from 'src/core/constant/AppConsts';
import { HttpClient } from '@angular/common/http';
import { Address, Contact } from '../../administrator/geographical/geo.service'

export class Retailer {
  id: number = 0;
  name: string = '';
  taxCode: string = null;
  addressId: number = 0;
  contactId: number = 0;
  imageURL: string = '';

  //mapping
  imageData: string = '';
  address: Address = new Address();
  contact: Contact = new Contact();
}


@Injectable()
export class AccountService {
  constructor(
    private http: HttpClient,
    private authenticSvc: AuthenticService) { }

  private urlRetailer: string = '/api/Retailer/get/byuser';
  private urlUpdateRetailer: string = '/api/Retailer/update';

  get(id: number): Observable<ResultModel<Retailer>> {
    return this.http.get<ResultModel<Retailer>>(`${AppConsts.remoteServiceBaseUrl}${this.urlRetailer}?userId=${id}`, this.authenticSvc.getHttpHeader());
  }

  update(Retailer: Retailer) : Observable<ResultModel<any>>{
    return this.http.post<ResultModel<any>>(`${AppConsts.remoteServiceBaseUrl}${this.urlUpdateRetailer}`, { Retailer }, this.authenticSvc.getHttpHeader());
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
